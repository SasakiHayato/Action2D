using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    [SerializeField] PlayerMove m_move;
    [SerializeField] PlayerAttack m_attack;
    [SerializeField] PlayerGravity m_gravity;
    [SerializeField] GameObject m_bullet;
    [SerializeField] FloorCheck m_floor;

    Rigidbody2D m_rb;
    Animator m_anim;
    Transform m_muzzlePos1;
    Transform m_muzzlePos2;

    bool m_freeze;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();

        for (int get = 0; get < 3; get++) { m_attack.SetAttackObject(get); }
        m_attack.SetShieldCollision();

        m_muzzlePos1 = transform.Find("Nozzle");
        m_muzzlePos2 = transform.Find("NozzleCrouch");
    }

    void Update()
    {
        PlayerControl();
    }

    void PlayerControl()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (!m_freeze) { m_move.Move(h, v, m_rb, m_anim); }

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

        if (Input.GetButtonDown("Fire1")) { m_attack.Attack(m_anim); }
        if (Input.GetButtonDown("Fire2")) { m_attack.SubAttack(m_anim, m_move, ref m_freeze); }
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
}
