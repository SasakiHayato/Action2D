using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour, IDamage
{
    [SerializeField] Collider2D m_kick;
    [SerializeField] GameObject m_bullet;
    [SerializeField] FloorCheck m_floor;
    
    Rigidbody2D m_rb;
    Animator m_anim;

    PlayerMove m_move;
    PlayerAttack m_attack;
    PlayerGravity m_gravity;
    [SerializeField] ItemDataBase m_attackData;

    Transform m_muzzlePos1;
    Collider2D m_collision;

    Collider2D m_attackCollision;
    Collider2D m_shieldCollision;

    bool m_freeze;
    [SerializeField] bool m_isDebug;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_collision = GetComponent<Collider2D>();

        m_move = GetComponent<PlayerMove>();
        m_attack = GetComponent<PlayerAttack>();
        m_gravity = GetComponent<PlayerGravity>();

        m_attackCollision = GameObject.Find("bone_12").GetComponent<Collider2D>();
        m_attackCollision.enabled = false;

        m_shieldCollision = GameObject.Find("ShieldCollider").GetComponent<Collider2D>();
        m_shieldCollision.enabled = false;

        m_kick.enabled = false;

        m_muzzlePos1 = transform.Find("Nozzle");

        if (m_isDebug) GameManager.Instance.SetCrreantPlay(m_isDebug);
    }

    void Update()
    {
        if (!GameManager.Instance.GetCrreantPlay()) return;
        if (PlayerDataClass.getInstance().GetFreeze())
        {
            m_move.Move(0, 0, m_anim);
            return;
        }
        
        PlayerControl();
    }

    void PlayerControl()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (!m_freeze) { m_move.Move(h, v, m_anim); }

        if (m_move.CrreantAvoid()) return;

        if (Input.GetButtonDown("Jump") && !m_move.CrreantCrouch())
        {
            if(!m_gravity.JumpCurreant() && m_gravity.JumpCurreantCount() > 0)
            {
                m_gravity.SetJump(true);
                m_gravity.SetPosY(transform.position.y);
                m_gravity.SetJumpCount();
            }
        }
        else if (Input.GetButtonDown("Jump") && m_move.CrreantCrouch()) { m_floor.SetTriger(); }
        bool id1 = PlayerDataClass.getInstance().SetIdBoolFirst;
        bool id2 = PlayerDataClass.getInstance().SetIdBoolSecond;

        if (id1 && Input.GetButtonDown("Fire1"))
        {
            int attackId = 1;
            int id = PlayerDataClass.getInstance().SetAttackIdFirst;
            m_attack.Attack(m_anim, m_attackData, id, attackId);
        }

        if (id2 && Input.GetButtonDown("Fire2"))
        {
            int attackId = 2;
            int id = PlayerDataClass.getInstance().SetAttackIdSecond;
            m_attack.Attack(m_anim, m_attackData, id, attackId);
        }

        if (!id1 && !id2 && Input.GetButtonDown("Fire1"))
        {
            m_attack.Attack(m_anim, m_attackData, -1, 0);
        }
        
        if (Input.GetButtonDown("Fire3")) { m_move.Avoidance(m_collision, h); }

        m_rb.velocity = new Vector2(h * m_move.Speed + m_move.AvoidanceSpeed, m_rb.velocity.y);
    }

    public void GetDamage(int damage)
    {
        m_anim.Play("Player_Damage");
        int hp = PlayerDataClass.getInstance().SetHp() - damage;
        PlayerDataClass.getInstance().GetHp(hp);
        if (PlayerDataClass.getInstance().SetHp() <= 0)
        {
            GameManager.Instance.ResetDungeonCount();
            GameManager.Instance.Deid();
        }
    }

    // AnimetionIventで呼び出し
    public void SetFreeze()
    {
        if (!m_freeze) { m_freeze = true; }
        else { m_freeze = false; }
    }
    public void SetBullet()
    {
        GameObject set = Instantiate(m_bullet, m_muzzlePos1);
        AttackClass attack = set.GetComponent<AttackClass>();

        attack.AttackPower = PlayerDataClass.getInstance().SetMagic() * m_attackData.GetItemId(1).GetAttackPower();
    }
    public void SetCollison()
    {
        if (!m_attackCollision.enabled) m_attackCollision.enabled = true;
        else m_attackCollision.enabled = false;
    }
    public void SetShield()
    {
        if (!m_shieldCollision.enabled) m_shieldCollision.enabled = true;
        else m_shieldCollision.enabled = false;
    }
    public void SetKickColision()
    {
        if (m_kick.enabled == false) m_kick.enabled = true;
        else m_kick.enabled = false;
    }
}
