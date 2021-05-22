using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField]private float m_playerSpeed = 0;
    [SerializeField]private float m_playerJump = 0;

    Rigidbody2D m_rigidbody;
    Animator m_animator;

    [SerializeField] GroundChack groundChack;
    [SerializeField] public GameObject m_attack;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_attack.SetActive(false);
    }

    void Update()
    {
        if (groundChack.isGround == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Jump
                m_rigidbody.AddForce(transform.up * m_playerJump, ForceMode2D.Impulse);
            }
        }
        Move();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_animator.SetTrigger("Attack");
        }
    }

    void Move()
    {
        float playerSpeed = 0;

        if (Input.GetKey(KeyCode.D))
        {
            playerSpeed += m_playerSpeed;
            m_animator.SetBool("Run", true);
            transform.localScale = new Vector2(1, 1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerSpeed -= m_playerSpeed;
            m_animator.SetBool("Run", true);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            m_animator.SetBool("Run", false);
        }

        m_rigidbody.velocity = new Vector2(playerSpeed, m_rigidbody.velocity.y);
    }

    void AttackStart()
    {
        if (m_attack)
        {
            m_attack.SetActive(true);
        }
    }

    void AttackEnd()
    {
        if (m_attack)
        {
            m_attack.SetActive(false);
        }
    }
    

}
