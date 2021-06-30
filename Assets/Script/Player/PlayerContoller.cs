using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    //[SerializeField] private float speed = 0;
    [SerializeField] private float jumpPower = 0;
    
    [SerializeField] public int m_Hp = 0;
    [SerializeField] public int m_attackPower = 0;

    [SerializeField] GroundChack groundChack;
    //Rigidbody2D m_rigidbody;
    Animator m_animator;
    //==================================================================

    private float g = -9.81f;

    //==================================================================
    void Start()
    {
        //m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        transform.position = this.transform.position;
    }

    void Update()
    {
        TMove();
        
        //========================
        //Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //Attack
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_animator.Play("Player_Attack");
        }
    }

    void TMove()
    {
        if (attackBool) return;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h == 0 && v == 0)
        {
            m_animator.Play("Player_Idle_anim");
        }
        else if (v < 0)
        {
            m_animator.Play("Player_Crouch");
        }
        else
        {
            if (h < 0)
            {
                transform.localScale = new Vector2(-0.15f, 0.15f);
            }
            if (h > 0)
            {
                transform.localScale = new Vector2(0.15f, 0.15f);
            }

            m_animator.Play("Player_Run");
        }

        transform.Translate(h / 10, 0, 0);
    }

    private float playerSpeed = 0;
    void Move()
    {
        //if (Input.GetKey(KeyCode.D))
        //{
        //    playerSpeed += speed;

        //    m_animator.Play("Player_Run");
        //    transform.localScale = new Vector2(0.15f, 0.15f);
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    playerSpeed -= speed;

        //    m_animator.Play("Player_Run");
        //    transform.localScale = new Vector2(-0.15f, 0.15f);
        //}
        //else if (Input.GetKey(KeyCode.S))
        //{
        //    m_animator.Play("Player_Crouch");
        //}
        //else
        //{
        //    playerSpeed = 0;
        //    m_animator.Play("Player_Idle_anim");
        //}

        //m_rigidbody.velocity = new Vector2(playerSpeed, m_rigidbody.velocity.y);
    }

    void Jump()
    {
        //if (groundChack.isGround == true || groundChack.plyerJumpCount > 0)
        //{
        //    m_rigidbody.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        //    groundChack.plyerJumpCount--;
        //}
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
    private bool attackBool;
    private void StartAttack()
    {
        attackBool = true;
    }

    private void EndAttack()
    {
        attackBool = false;
    }
}
