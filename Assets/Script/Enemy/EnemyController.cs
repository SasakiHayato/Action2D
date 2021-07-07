using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool m_attackBool = false;
    private bool m_freeze = false;

    private float m_speed = 2;

    Rigidbody2D m_rigidbody;
    Animator m_animator;

    GameObject m_attckCollider;

    [SerializeField] Vector2 m_wallRay = Vector2.zero;
    [SerializeField] LayerMask m_wallLayer = 0;
    [SerializeField] Vector2 m_playerRay = Vector2.zero;
    [SerializeField] LayerMask m_playerLayer = 0;

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
            player.PlayerDamage(5);
        }
    }

    public void WallCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_wallRay, m_wallRay.magnitude, m_wallLayer);

        if (hit.collider)
        {
            m_speed = 0;
            StartCoroutine(StartWalk(50));
        }
    }

    private IEnumerator StartWalk(int time)
    {
        for (int i = 0; i < time; i++)
        {
            yield return null;
        }

        m_speed = 2;
        m_speed *= -1;
        m_playerRay *= -1;
        transform.localScale = new Vector2(-0.15f, 0.15f);

    }

    public void PlayerCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_playerRay, m_playerRay.magnitude, m_playerLayer);

        if (hit.collider)
        {
            Attack();
        }
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
