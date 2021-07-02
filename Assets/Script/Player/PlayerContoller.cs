using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private float jumpPower = 0;
    private bool attackBool;

    [SerializeField] public int m_Hp = 0;
    [SerializeField] public int m_attackPower = 0;

    [SerializeField] GroundChack groundChack;
    Rigidbody2D m_rigidbody;
    Animator m_animator;
    
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        transform.position = this.transform.position;
    }

    void Update()
    {
        

        Move();
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        //Attack
        if (Input.GetButtonDown("Fire1"))
        {
            m_animator.Play("Player_Attack");
        }
    }

    void Move()
    {
        if (attackBool) return;

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

        m_rigidbody.velocity = new Vector2(h * speed, m_rigidbody.velocity.y);
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
    //攻撃中に入力をうけつけない
    private bool Freeze()
    {
        if (attackBool)
        {
            attackBool = false;
        }
        else
        {
            attackBool = true;
        }

        return attackBool;
    }
}
