using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShielderController : EnemyBase
{
    GameObject m_shield = default;
    Rigidbody2D m_rb;
    Animator m_anim;

    void Start()
    {
        m_shield = transform.GetChild(0).gameObject;
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
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

        if (ReturnSpeed() != 0) { m_anim.Play("Shielder_Walk"); }
        else { m_anim.Play("Shielder_Idle"); }
    }

    public override void Attack()
    {
        m_anim.Play("Shielder_Attack");
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
}
