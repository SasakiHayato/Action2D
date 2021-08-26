using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BommerClass : NewEnemyBase
{
    [SerializeField] NewBehaviorTree m_tree;

    Rigidbody2D m_rb;
    Animator m_anim;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }

    void Update()
    {
        m_tree.Tree();
    }

    public override void Move()
    {
        Debug.Log("行動中");
        FieldCheck();

        m_rb.velocity = new Vector2(SetSpeed(), m_rb.velocity.y);
        
        if (SetSpeed() != 0)
        {
            m_anim.Play("Bommer_Walk");
        }
        else
        {
            m_anim.Play("Bommer_Idle");
        }

        m_tree.SetFalseToAction();
    }

    public override void Attack1()
    {
        Debug.Log("攻撃１");
        m_anim.Play("Bommer_Attack");
        FindPlayerToLook();
        StartCoroutine(SetFale());
    }

    IEnumerator SetFale()
    {
        yield return new WaitForSeconds(4);
        m_tree.SetFalseToAction();
    }

    public override void Attack2() { Attack1(); }
}
