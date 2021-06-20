using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private float jumpPower = 0;
    
    [SerializeField] public int m_Hp = 0;
    [SerializeField] public int m_attackPower = 0;

    [SerializeField] GroundChack groundChack;
    Rigidbody2D m_rigidbody;
    Animator m_animator;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        //Jump
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        //Attack
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
            playerSpeed += speed;
            m_animator.SetBool("Run", true);
            transform.localScale = new Vector2(1, 1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerSpeed -= speed;
            m_animator.SetBool("Run", true);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            m_animator.SetBool("Run", false);
        }

        m_rigidbody.velocity = new Vector2(playerSpeed, m_rigidbody.velocity.y);
    }

    void Jump()
    {
        if (groundChack.isGround == true || groundChack.plyerJumpCount > 0)
        {
            m_rigidbody.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
            groundChack.plyerJumpCount--;
        }
    }

    public void PlayerDamage(int damage)
    {
        m_Hp -= damage;
        
        if (m_Hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
