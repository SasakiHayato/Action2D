﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShielderClass : NewEnemyBase
{
    [SerializeField] NewBehaviorTree m_tree;
    Animator m_anim;
    Rigidbody2D m_rb;

    GameObject m_attackCollider = default;
    bool m_attackBool = false;
    
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();

        m_attackCollider = transform.GetChild(0).gameObject;
        m_attackCollider.SetActive(false);
    }

    void Update() { m_tree.Tree(); }

    public override void Move()
    {
        m_rb.velocity = new Vector2(SetSpeed(), m_rb.velocity.y);
        if (SetSpeed() != 0) { m_anim.Play("Shielder_Walk"); }
        else { m_anim.Play("Shielder_Idle"); }

        FieldCheck();
    }

    public override void Attack1()
    {
        m_anim.Play("Shielder_Attack");
        FindPlayerToLook();
        m_tree.Interval(2.5f);
    }

    public override void Attack2()
    {
        FindPlayerToLook();
        m_rb.AddForce(new Vector2(RetuneStepFloat() * -1, 0) * 15, ForceMode2D.Impulse);

        m_tree.Interval(4);
    }

    float RetuneStepFloat()
    {
        float stepPower = 1;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player.position.x < transform.position.x) { }
        else { stepPower *= -1; }

        return stepPower;
    }

    // AnimetioinIventで呼び出し
    void SetAttackCollision()
    {
        if (!m_attackBool)
        {
            m_attackBool = true;
            m_attackCollider.SetActive(true);
        }
        else
        {
            m_attackBool = false;
            m_attackCollider.SetActive(false);
        }
    }
}
