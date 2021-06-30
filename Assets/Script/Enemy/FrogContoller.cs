using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogContoller : MonoBehaviour
{
    [SerializeField] private float jumpPower = 0;
    [SerializeField] private float m_xSpeed = 0;
    private float timer = 0;

    [SerializeField] GroundChack groundChack;
    Rigidbody2D m_rigidbody;
    Animator m_animator;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }


    private float a = 0;
    void Update()
    {
        timer += Time.deltaTime; 

        if (timer > 3)
        {
            Move();
            m_rigidbody.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
            
            timer = 0;   
        }

        m_animator.SetFloat("Jump", a ++);
    }

    void Move()
    {
        if (groundChack.isGround == false)
        {
            m_rigidbody.velocity = new Vector2(m_xSpeed, 0);
        }

        m_xSpeed *= -1;
        if (m_xSpeed < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (m_xSpeed > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
}
