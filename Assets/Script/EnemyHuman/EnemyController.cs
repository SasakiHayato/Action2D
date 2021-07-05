using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float m_speed = 2;

    private bool m_freeze = false;
    private bool m_attackBool = false;

    Rigidbody2D m_rigidbody;
    Animator m_animator;

    private Vector2 m_wallRay = new Vector2 (1.5f, 0);
    [SerializeField] private Vector2 m_groundRay = Vector2.zero;

    [SerializeField] LayerMask m_wallLayer = 0;
    [SerializeField] LayerMask m_groungLayer = 0;

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
        GroundCheck();
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

    private void WallCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_wallRay, m_wallRay.magnitude, m_wallLayer);
        
        if (hit.collider)
        {
            m_speed = 0;
            StartCoroutine(StartWalk(50));
        }
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_groundRay, m_groundRay.magnitude, m_groungLayer);

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
        transform.localScale = new Vector2(-0.15f, 0.15f);
        
    }

    private void Attack()
    {
        m_animator.Play("Enemy_Attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
