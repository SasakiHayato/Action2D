using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewZombieController : EnemyBase
{
    Animator m_anim;
    Rigidbody2D m_rb;
    GameObject m_attackCollider = default;

    bool m_colliderBool = false;

    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();

        m_attackCollider = transform.GetChild(0).gameObject;
        m_attackCollider.SetActive(m_colliderBool);
    }

    void Update()
    {
        if (ReturnFreeze()) return;
        
        Move();

        FindField();
        FindPlayerToAttack();
    }

    public override void Move()
    {
        m_rb.velocity = new Vector2(ReturnSpeed(), m_rb.velocity.y);

        if (ReturnSpeed() != 0) { m_anim.Play("Enemy_Walk"); }
        else { m_anim.Play("Enemy_Idle"); }
    }

    public override void Attack()
    {
        m_anim.Play("Enemy_Attack");
    }

    public override void GetDamage(float damage)
    {
        float hp = ReturnCurrentHp();
        hp -= damage;
        SetHp(hp);
        
        if (ReturnCurrentHp() <= 0)
        {
            Dead(gameObject.transform);
        }
    }

    public void SetAttack()
    {
        if (!m_colliderBool) { m_colliderBool = true; }
        else { m_colliderBool = false; }

        m_attackCollider.SetActive(m_colliderBool);
    }
}
