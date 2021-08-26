using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchryClass : NewEnemyBase
{
    [SerializeField] NewBehaviorTree m_tree;
    [SerializeField] ArcheryBowClass m_bowClass;
    [SerializeField] Transform m_muzzle;

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

        if (SetSpeed() != 0) { m_anim.Play("Archery_Walk"); }
        else { m_anim.Play("Archery_Idle"); }

        FieldCheck();
        m_rb.velocity = new Vector2(SetSpeed(), m_rb.velocity.y);
        m_tree.SetFalseToAction();
    }

    public override void Attack1()
    {
        Debug.Log("攻撃1");
        m_anim.Play("Archery_Attack");
        FindPlayerToLook();
        StartCoroutine(SetFalse());
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(3.5f);
        m_tree.SetFalseToAction();
    }

    public override void Attack2() { Attack1(); }

    // animetionIventで呼び出し
    public void SetBow()
    {
        GameObject set = Instantiate(m_bowClass.gameObject);
        set.transform.position = m_muzzle.position;
        ArcheryBowClass bow = set.GetComponent<ArcheryBowClass>();
        bow.SetDir();
    }
}
