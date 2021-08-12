using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShielderController : EnemyBase, IDamage
{
    private Animator m_animator;

    private GameObject game = null; 

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_dSpeed = m_speed;
    }

    void Update()
    {
        if (m_freeze) return;
        WallCheck();
        Move();
        PlayerCheck();
    }

    private void Move()
    {
        m_rigidbody.velocity = new Vector2(m_speed, m_rigidbody.velocity.y);

        if (m_speed != 0)
        {
            m_animator.Play("Shielder_Walk");
        }
        else
        {
            m_animator.Play("Shielder_Idle");
        }
    }

    private void PlayerCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_playerRay, m_playerRay.magnitude, m_playerLayer);

        if (hit.collider)
        {
            Attack();
        }
    }

    private void Attack()
    {
        m_animator.Play("Shielder_Attack");
    }

    public void GetDamage(int damage)
    {
        m_animator.Play("Shielder_Damage");
        m_hp -= damage;

        if (m_hp <= 0)
        {
            ThisDie();
        }
    }
}
