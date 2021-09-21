using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BommerClass : EnemyBase, IDamage
{
    [SerializeField] BehaviourTree m_newTree;
    [SerializeField] GameObject m_bom;
    [SerializeField] Transform m_muzzle;

    Rigidbody2D m_rb;
    Animator m_anim;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }

    void Update() => m_newTree.Repeter(this, this.name);

    public override void Attack(SetActionType set)
    {
        if (set == SetActionType.NoamalAttack1)
        {
            m_anim.Play("Bommer_Attack");
            FindPlayerToLook();
            m_newTree.IntervalSetFalse(6);
        }
    }
    public override void Move(SetActionType set)
    {
        if (set == SetActionType.Move1)
        {
            FieldCheck();

            m_rb.velocity = new Vector2(SetSpeed(), m_rb.velocity.y);

            if (SetSpeed() != 0) { m_anim.Play("Bommer_Walk"); }
            else { m_anim.Play("Bommer_Idle"); }
            m_newTree.IntervalSetFalse(0);
        }
    }
    
    // animetionIventで呼び出し
    public void SetBom()
    {
        GameObject bom = Instantiate(m_bom);
        bom.transform.position = m_muzzle.transform.position;
        AttackClass attack = bom.GetComponent<AttackClass>();
        
        attack.GetPower = GetAttackPower;
    }

    public void GetDamage(int damage)
    {
        Debug.Log($"{gameObject.name} : Get :{damage}");
        int hp = RetuneCrreantHp() - damage;
        m_rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
        m_anim.Play("Bommer_Damage");
        SetHp(hp, gameObject);
    }
}
