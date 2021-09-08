using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BommerClass : NewEnemyBase, IDamage
{
    [SerializeField] BehaviorTree m_tree;
    [SerializeField] GameObject m_bom;
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
        FieldCheck();

        m_rb.velocity = new Vector2(SetSpeed(), m_rb.velocity.y);
        
        if (SetSpeed() != 0) { m_anim.Play("Bommer_Walk"); }
        else { m_anim.Play("Bommer_Idle"); }
    }
    public override void Attack()
    {
        if (SetAttack == SetAttackStatus.NormalAttack1)
        {
            m_anim.Play("Bommer_Attack");
            FindPlayerToLook();
            m_tree.Interval(6);
        }
    }

    // animetionIventで呼び出し
    public void SetBom()
    {
        GameObject bom = Instantiate(m_bom);
        bom.transform.position = m_muzzle.transform.position;
    }

    public void GetDamage(int damage)
    {
        int hp = RetuneCrreantHp() - damage;
        m_rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
        m_anim.Play("Bommer_Damage");
        SetHp(hp, gameObject);
    }
}
