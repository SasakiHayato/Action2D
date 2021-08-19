using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryController : EnemyClass, IDamage
{
    [SerializeField] private Transform m_nozzle;
    [SerializeField] private GameObject m_bow;

    Animator m_animator;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        m_dSpeed = m_speed;

        StartPos();
    }

    void Update()
    {
        if (m_freeze) return;
        Move();
        WallCheck();
        PlayerCheck();
    }

    private void Move()
    {
        m_rigidbody.velocity = new Vector2(m_speed, m_rigidbody.velocity.y);

        if (m_speed == 0)
        {
            m_animator.Play("Archery_Idle");
        }
        else
        {
            m_animator.Play("Archery_Walk");
        }
    }

    public void GetDamage(int damage)
    {
        m_animator.Play("Archery_Damage");
        m_hp -= damage;

        if (m_hp <= 0)
        {
            ThisDie();
        }
    }

    public void PlayerCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_playerRay, m_playerRay.magnitude, m_playerLayer);

        if (hit.collider)
        {
            Attack();
        }
    }

    private void Attack()
    {
        m_animator.Play("Archery_Attack");
    }

    private void SetNozzle()
    {
        Instantiate(m_bow, m_nozzle);
    }
}
