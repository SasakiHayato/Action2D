using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BommerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private Animator m_animator;

    private bool m_freeze = false;
    public bool m_attackBool = false;

    [SerializeField] private float m_speed = 0;

    [SerializeField] private Vector2 m_ray = Vector2.zero;
    [SerializeField] private LayerMask m_layer = 0;

    [SerializeField] private Transform m_nozzle = null;
    [SerializeField] private GameObject m_bomPlefab = null;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        PlayerCheck();
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
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, m_ray, m_ray.magnitude, m_layer);
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
