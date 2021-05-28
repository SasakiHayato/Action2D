using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField]private float m_playerSpeed = 0;
    [SerializeField]private float m_playerJump = 0;
    [SerializeField] private int m_Hp = 0;

    Rigidbody2D m_rigidbody;
    Animator m_animator;

    [SerializeField] GroundChack groundChack;

    [SerializeField] public float m_attackPower = 0;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        Debug.Log(m_attackPower);
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
            //Attack
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage();
        }
    }

    private void Damage()
    {
        m_Hp--;
        Debug.Log(m_Hp);

        if (m_Hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
