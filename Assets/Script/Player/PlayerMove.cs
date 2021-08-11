using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator m_animator;
    public Rigidbody2D m_rigidbody { get; set; }

    private float m_speed = 7;
    
    public bool m_crouch { get; private set; }

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (!GameManager.Instance.CureatPlay()) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
    }

    private void Move(float h, float v)
    {
        if (PlayerDataClass.Instance.m_freeze) return;
        if (h == 0 && v == 0)
        {
            m_animator.Play("Player_Idle_anim");
            m_crouch = false;
        }

        if (h != 0)
        {
            m_animator.Play("Player_Run");
            
            if (h < 0)
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (v < 0 && h == 0)
        {
            m_animator.Play("Player_Crouch");
            m_crouch = true;
        }

        m_rigidbody.velocity = new Vector2(h * m_speed, m_rigidbody.velocity.y);
    }
}
