using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour, IDamage
{
    [SerializeField] GameObject m_bullet;
    [SerializeField] FloorCheck m_floor;
    [SerializeField] Sprite m_avoid;
    [SerializeField] Sprite m_player;

    Rigidbody2D m_rb;
    Animator m_anim;

    PlayerMove m_move;
    PlayerAttack m_attack;
    PlayerGravity m_gravity;
    [SerializeField] AttackItemDataBase m_attackData;

    Transform m_muzzlePos1;
    Transform m_muzzlePos2;
    Collider2D m_collision;

    Collider2D m_attackCollision;

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

        //m_attack.SetShieldCollision();

        m_muzzlePos1 = transform.Find("Nozzle");
        m_muzzlePos2 = transform.Find("NozzleCrouch");

        if (m_isDebug)
        {
            GameManager.getInstance().SetCrreantPlay(m_isDebug);
        }
    }

    void Update()
    {
        //if (PlayerDataClass.Instance.GetFreeze()) return;
        if (!GameManager.getInstance().GetCrreantPlay()) return;
        
        PlayerControl();
    }

    void PlayerControl()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (!m_freeze) { m_move.Move(h, v, m_rb, m_anim); }
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

        
        if (Input.GetButtonDown("Fire1")) { m_attack.AttackFirst(m_anim, m_attackData); }
        if (Input.GetButtonDown("Fire2")) { m_attack.AttackSecond(m_anim, m_move, ref m_freeze); }
        if (Input.GetButtonDown("Fire3")) { m_move.Avoidance(m_player, m_avoid, m_collision, m_rb, h); }
    }

    public void GetDamage(int damage)
    {
        m_anim.Play("Player_Damage");
        int hp = PlayerDataClass.Instance.SetHp() - damage;
        PlayerDataClass.Instance.GetHp(hp);
        if (PlayerDataClass.Instance.SetHp() <= 0)
        {
            PlayerDataClass.Instance.GetHp(100);
            GameManager.getInstance().ResetDungeonCount();
            GameManager.getInstance().SetScene("Start");
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
        if(!m_move.CrreantCrouch()) { Instantiate(m_bullet, m_muzzlePos1); }
        else { Instantiate(m_bullet, m_muzzlePos2); }
    }
    public void SetCollison()
    {
        if (!m_attackCollision.enabled) m_attackCollision.enabled = true;
        else m_attackCollision.enabled = false;
    }
}
