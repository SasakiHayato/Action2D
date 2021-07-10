using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : EnemyBase
{
    private bool m_attackBool = false;
    
    [System.NonSerialized] public Rigidbody2D m_rigidbody;
    Animator m_animator;

    GameObject m_attckCollider;
    

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        m_attckCollider = transform.GetChild(0).gameObject;
        m_attckCollider.SetActive(m_attackBool);
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

    private void Attack()
    {
        m_animator.Play("Enemy_Attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerContoller player;
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerContoller>();
            player.m_rigidbody.AddForce(transform.up * 2, ForceMode2D.Impulse);
            player.PlayerDamage(0);
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
