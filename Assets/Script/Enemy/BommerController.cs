using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BommerController : EnemyBase, IDamage
{
    private Animator m_animator;

    public bool m_attackBool = false;

    [SerializeField] private Transform m_nozzle = null;
    [SerializeField] private GameObject m_bomPlefab = null;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        StartPos();
    }

    void Update()
    {
        Move();
        PlayerCheck();
        WallCheck();
    }

    private void Move()
    {
        if (m_freeze) return;

        if (m_speed == 0)
        {
            m_animator.Play("Bommer_Idle");
        }
        else
        {
            m_animator.Play("Bommer_Walk");
        }

        m_rigidbody.velocity = new Vector2(m_speed, m_rigidbody.velocity.y);
    }

    private void PlayerCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, m_playerRay, m_playerRay.magnitude, m_playerLayer);
        if (hit.collider)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (!m_attackBool)
        {
            m_animator.Play("Bommer_Attack");
        }
    }

    public void AddDamage(int damage)
    {
        m_animator.Play("Bommer_Damage");
        m_hp -= damage;

        if (m_hp <= 0)
        {
            ThisDie();
        }
    }

    private void SetBom()
    {
        m_attackBool = true;
        Instantiate(m_bomPlefab, m_nozzle);
    }

    private bool Freeze()
    {
        if (!m_freeze)
        {
            m_freeze = true;
        }
        else
        {
            m_freeze = false;
        }

        return m_freeze;
    }
}
