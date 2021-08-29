using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    // 重力
    [SerializeField] private float m_fallSpeed = 0;
    private float m_fallTime = 0;

    // 着地判定
    [SerializeField] private Vector2 m_ray = Vector2.zero;
    [SerializeField] LayerMask m_layer = 0;

    // Jump関連
    [SerializeField] private float m_time = 0;
    private float m_jumpStartPosY = 0;
    private int m_jumpCount = 0;
    [SerializeField] private int m_maxJumpCount = 0;
    // 挙動Speed
    private float m_jumpTime = 0;
    [SerializeField] private float m_jumpSpeed = 0;


    // 現在ジャンプしてるかどうか
    private bool m_jumpCurreant = false;

    private Rigidbody2D m_rb;
    private Vector2 m_velocity = Vector2.zero;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_jumpCount = m_maxJumpCount;
    }

    void Update()
    {
        if (m_jumpCurreant)
        {
            ChengeOfPos(InitialVelocity());
            if (FallCheck(InitialVelocity()) <= 0)
            {
                m_jumpTime = 0;
                m_fallTime = 0;
                m_jumpCurreant = false;
            }
        }
    }

    // 初速度計算
    private float InitialVelocity()
    {
        float v0;
        float g = Physics2D.gravity.y * -1;
        float t = m_time;

        v0 = g * t;

        return v0;
    }

    // 変位計算
    private void ChengeOfPos(float v0)
    {
        float g = Physics2D.gravity.y * -1;
        m_jumpTime += Time.deltaTime * m_jumpSpeed;

        float yPos = v0 * m_jumpTime - (g * m_jumpTime * m_jumpTime) / 2;
        
        transform.position = new Vector2(transform.position.x, yPos + m_jumpStartPosY);
    }

    // 速度計算
    private float FallCheck(float v0)
    {
        float v;
        float g = Physics2D.gravity.y * -1;

        v = v0 - g * m_jumpTime;

        return v;
    }

    // 下記: 重力、着地判定
    private void FixedUpdate()
    {
        m_fallTime += Time.deltaTime;
        m_velocity.y = (Physics2D.gravity.y * m_fallTime) * m_fallSpeed;
        m_rb.velocity = new Vector2(m_rb.velocity.x, m_velocity.y);

        //IsGround();
    }

    private void IsGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_ray, m_ray.magnitude, m_layer);

        if (hit.collider)
        {
            m_fallTime = 0;
            m_jumpCount = m_maxJumpCount;
        }
    }

    public bool JumpCurreant() { return m_jumpCurreant; } 
    public bool SetJump(bool set) { return m_jumpCurreant = set; }
    public float SetPosY(float set) { return m_jumpStartPosY = set; }
    public int SetJumpCount() { return m_jumpCount -= 1; }
    public int JumpCurreantCount() { return m_jumpCount; }

    public void ResetCount() => m_jumpCount = m_maxJumpCount;
    public void ResetFall() => m_fallTime = 0;
}
