using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField] private float m_speed = 0;
    [SerializeField] private float m_jumpPower = 0;
    
    private bool m_freeze;
    private bool m_active = false;

    [SerializeField] public int m_Hp = 0;
    [SerializeField] public int m_attackPower = 0;

    private float m_time = 2;
    private int m_num = 1;

    [SerializeField] GroundChack m_groundChack;
    
    Rigidbody2D m_rigidbody;
    Animator m_animator;

    GameObject m_attack1;
    GameObject m_attack2;
    GameObject m_attack3;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        m_attack1 = transform.GetChild(0).gameObject;
        m_attack1.SetActive(m_active);

        m_attack2 = transform.GetChild(1).gameObject;
        m_attack2.SetActive(m_active);

        m_attack3 = transform.GetChild(2).gameObject;
        m_attack3.SetActive(m_active);

        transform.position = this.transform.position;
    }

    void Update()
    {
        Move();
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            m_time -= Time.deltaTime;

            if (m_time < 0)
            {
                m_num = 1;
                m_time = 2;
            }
            Attack();
        }
        
    }

    void Move()
    {
        if (m_freeze) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h == 0 && v == 0)
        {
            m_animator.Play("Player_Idle_anim");
        }

        if (h != 0)
        {
            m_animator.Play("Player_Run");
            if (h > 0)
            {
                transform.localScale = new Vector2(0.15f, 0.15f);
            }
            if (h < 0)
            {
                transform.localScale = new Vector2(-0.15f, 0.15f);
            }
            
        }
        if (v < 0)
        {
            m_animator.Play("Player_Crouch");
        }

        m_rigidbody.velocity = new Vector2(h * m_speed, m_rigidbody.velocity.y);
    }

    void Jump()
    {
        if (m_groundChack.isGround == true || m_groundChack.plyerJumpCount > 0)
        {
            m_rigidbody.AddForce(transform.up * m_jumpPower, ForceMode2D.Impulse);
            m_groundChack.plyerJumpCount--;
        }
    }

    void Attack()
    {
        switch (m_num)
        {
            case 1:
                m_animator.Play("Player_Attack");
                m_num = 2;
                break;

            case 2:
                m_animator.Play("Player_Attack2");
                m_num = 3;
                break;

            case 3:
                m_animator.Play("Player_Attack3");
                m_num = 1;
                break;
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
    //攻撃中に入力をうけつけない
    private bool Freeze()
    {
        if (m_freeze)
        {
            m_freeze = false;
        }
        else
        {
            m_freeze = true;
        }

        return m_freeze;
    }

    private void SetCollider()
    {
        if (!m_active)
        {
            m_active = true;
        }
        else
        {
            m_active = false;
        }
        m_attack1.SetActive(m_active);
    }

    private void SetCollider2()
    {
        if (!m_active)
        {
            m_active = true;
        }
        else
        {
            m_active = false;
        }
        m_attack2.SetActive(m_active);
    }

    private void SetCollider3()
    {
        if (!m_active)
        {
            m_active = true;
        }
        else
        {
            m_active = false;
        }
        m_attack2.SetActive(m_active);
    }
}
