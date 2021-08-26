using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BommerClass : NewEnemyBase
{
    [SerializeField] NewBehaviorTree m_tree;

    void Update()
    {
        m_tree.Tree();
    }

    public override void Move()
    {
        Debug.Log("行動中");
        m_tree.SetFalseToAction();
    }

    public override void Attack1()
    {
        Debug.Log("攻撃１");
        m_tree.SetFalseToAction();
    }

    public override void Attack2()
    {
        Attack1();
        
    }
}
