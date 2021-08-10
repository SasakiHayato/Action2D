using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator m_animator;
    //private Rigidbody2D m_rigidbody;
    public Rigidbody2D m_rigidbody { get; set; }

    [SerializeField] private GroundChack m_groundChack;

    private float m_speed = 7;
    [SerializeField] private float m_jumpPower = 0;

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

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            Avoidance(h);
        }
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
            transform.localScale = new Vector2(0.15f * h, 0.15f);
        }

        if (v < 0 && h == 0)
        {
            m_animator.Play("Player_Crouch");
            m_crouch = true;
        }

        m_rigidbody.velocity = new Vector2(h * m_speed, m_rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (m_groundChack.isGround == true || m_groundChack.plyerJumpCount > 0)
        {
            m_rigidbody.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);
            m_groundChack.plyerJumpCount--;
        }
    }

    private void Avoidance(float h)
    {
        transform.Translate(4 * h, 0, 0);
    }
}
