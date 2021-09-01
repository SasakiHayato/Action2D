using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchryClass : NewEnemyBase, IDamage
{
    [SerializeField] BehaviorTree m_tree;
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
        if (SetSpeed() != 0) { m_anim.Play("Archery_Walk"); }
        else { m_anim.Play("Archery_Idle"); }

        FieldCheck();
        m_rb.velocity = new Vector2(SetSpeed(), m_rb.velocity.y);
    }

    public override void Attack()
    {
        if (SetAttack == SetAttackStatus.NormalAttack1)
        {
            m_anim.Play("Archery_Attack");
            FindPlayerToLook();
            m_tree.Interval(4);
        }
    }

    public void GetDamage(int damage)
    {
        int hp = RetuneCrreantHp() - damage;
        m_rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
        m_anim.Play("Archery_Damage");
        SetHp(hp, gameObject);
    }

    // animetionIventで呼び出し
    public void SetBow()
    {
        GameObject set = Instantiate(m_bowClass.gameObject);
        set.transform.position = m_muzzle.position;
        ArcheryBowClass bow = set.GetComponent<ArcheryBowClass>();
        bow.SetDir(gameObject.transform);
    }
}
