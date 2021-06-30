using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PController : MonoBehaviour
{
    Animator m_ani;
    Rigidbody2D m_rigidbody;

    void Start()
    {
        transform.position = this.transform.position;
        m_ani = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    [SerializeField] private float speed;
    private float Playerspeed = 0;
    void Update()
    {
        MoveTransFrom();
    }

    void MoveTransFrom()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h == 0)
        {
            m_ani.Play("Player_Idle_anim");
        }
        else
        {
            m_ani.Play("Player_Run");
        }
        transform.Translate(h / 16, v / 16, 0);
    }

    void MoveVelocity()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h > 0)
        {
            m_ani.Play("Player_Run");
            Playerspeed += speed;
            transform.localScale = new Vector2(0.15f, 0.15f);
        }
        if (h < 0)
        {
            m_ani.Play("Player_Run");
            Playerspeed -= speed;
            transform.localScale = new Vector2(-0.15f, 0.15f);
        }

        if (v < 0)
        {
            m_ani.Play("Player_Crouch");
        }

        if (h == 0 && v == 0)
        {
            Playerspeed = 0;
            m_ani.Play("Player_Idle_anim");
        }

        if (Input.GetMouseButtonDown(0))
        {
            m_ani.Play("Player_Attack");
        }
        if (Input.GetMouseButtonDown(1))
        {
            m_ani.Play("Player_Attack2");
        }

        m_rigidbody.velocity = new Vector2(Playerspeed + m_rigidbody.velocity.x, m_rigidbody.velocity.y);
    }
}
