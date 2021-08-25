using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator m_animator;
    public Rigidbody2D m_rigidbody { get; set; }

    [SerializeField] private float m_speed = 0;
    
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
                transform.localScale = new Vector2(-0.15f, 0.15f);
            }
            else
            {
                transform.localScale = new Vector2(0.15f, 0.15f);
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
