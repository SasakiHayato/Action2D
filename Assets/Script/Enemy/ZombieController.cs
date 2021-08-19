using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : EnemyClass, IDamage
{
    private bool m_attackBool = false;
    
    Animator m_animator;

    GameObject m_attckCollider;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        m_attckCollider = transform.GetChild(0).gameObject;
        m_attckCollider.SetActive(m_attackBool);

        StartPos();
        m_dSpeed = m_speed;
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

        if (m_speed != 0)
        {
            m_animator.Play("Enemy_Walk");
        }
        else
        {
            m_animator.Play("Enemy_Idle");
        }
    }

    public void GetDamage(int damage)
    {
        m_animator.Play("Enemy_Damage");
        m_hp -= damage;
        
        if (m_hp <= 0)
        {
            ThisDie();
        }
    }

    private void Attack()
    {
        m_animator.Play("Enemy_Attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMove move;
        PlayerContoller player;
        if (collision.gameObject.CompareTag("Player"))
        {
            move = collision.GetComponent<PlayerMove>();
            player = collision.GetComponent<PlayerContoller>();
            move.m_rigidbody.AddForce(transform.up * 2, ForceMode2D.Impulse);
            player.PlayerDamage(m_attackPower);
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

    private void SetCollision()
    {
        if (!m_attackBool)
        {
            m_attackBool = true;
        }
        else
        {
            m_attackBool = false;
        }

        m_attckCollider.SetActive(m_attackBool);
    }
}
