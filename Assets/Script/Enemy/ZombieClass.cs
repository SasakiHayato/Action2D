using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieClass : EnemyBase, IDamage
{
    Animator m_anim;
    Rigidbody2D m_rb;

    [SerializeField] BehaviourTree m_newTree;

    GameObject m_collider = default;
    bool m_attackCheck = false;

    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();

        m_collider = transform.GetChild(0).gameObject;
    }

    void Update() 
    {
        m_newTree.Repeter(this, this.name);
    }
    public override void Move(SetActionType set)
    {
        FieldCheck();
        if (set == SetActionType.Move1)
        {
            if (SetSpeed() != 0) { m_anim.Play("Enemy_Walk"); }
            else { m_anim.Play("Enemy_Idle"); }

            m_rb.velocity = new Vector2(SetSpeed(), m_rb.velocity.y);
        }

        m_newTree.IntervalSetFalse(0);
    }
    
    public override void Attack(SetActionType set)
    {
        Debug.Log(set);
        m_anim.Play("Enemy_Attack");
        FindPlayerToLook();
        if (set == SetActionType.NoamalAttack1)
        {
            Debug.Log(m_newTree);
            m_newTree.IntervalSetFalse(5);
            return;
        }
        else if (set == SetActionType.NoamalAttack2)
        {
            m_rb.AddForce(new Vector2(RetuneStepFloat() * -6, 3), ForceMode2D.Impulse);
            m_newTree.IntervalSetFalse(5);
        }
    }
    
    float RetuneStepFloat()
    {
        float stepPower = 1;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player.position.x < transform.position.x) { }
        else { stepPower *= -1; }
        
        return stepPower;
    }

    // AnimetionIventで呼び出し
    public void BackStep()
    {
        m_rb.AddForce(new Vector2(RetuneStepFloat() * 6, 3), ForceMode2D.Impulse);
        //m_tree.Interval(5);
    }

    // AnimetionIventで呼び出し
    public void AttackCollider()
    {
        if (!m_attackCheck)
        {
            m_attackCheck = true;
            m_collider.SetActive(true);
        }
        else 
        {
            m_attackCheck = false;
            m_collider.SetActive(false);
        }
    }

    public void GetDamage(int damage)
    {
        int hp = RetuneCrreantHp() - damage;
        m_rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
        m_anim.Play("Enemy_Damage");
        SetHp(hp, gameObject);
    }
}
