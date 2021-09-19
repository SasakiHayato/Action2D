using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShielderClass : EnemyBase, IDamage
{
    [SerializeField] BehaviourTree m_newTree;
    Animator m_anim;
    Rigidbody2D m_rb;

    GameObject m_attackCollider = default;
    bool m_attackBool = false;

    Collider2D m_shieldCollider;
    
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();

        m_attackCollider = transform.GetChild(0).gameObject;
        m_attackCollider.SetActive(false);

        m_shieldCollider = transform.Find("Shield").gameObject.GetComponent<Collider2D>();
    }

    void Update() 
    {
        //m_tree.Tree(); 
        m_newTree.Repeter(this, this.name);
    }
    public override void Move(SetActionType set)
    {
        if (set == SetActionType.Move1)
        {
            m_rb.velocity = new Vector2(SetSpeed(), m_rb.velocity.y);
            if (SetSpeed() != 0) { m_anim.Play("Shielder_Walk"); }
            else { m_anim.Play("Shielder_Idle"); }

            FieldCheck();
            m_newTree.IntervalSetFalse(0);
        }
    }
    public override void Attack(SetActionType set)
    {
        FindPlayerToLook();
        if (set == SetActionType.NoamalAttack1)
        {
            m_anim.Play("Shielder_Attack");
            m_newTree.IntervalSetFalse(2.5f);
        }
        else if (set == SetActionType.NoamalAttack2)
        {
            m_attackCollider.SetActive(true);
            m_rb.AddForce(new Vector2(RetuneStepFloat() * -1, 0) * 15, ForceMode2D.Impulse);
            Invoke("SetAttackFalse", 0.7f);
            m_newTree.IntervalSetFalse(4);
        }
    }

    void SetAttackFalse() => m_attackCollider.SetActive(false);

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
            m_shieldCollider.enabled = false;
            m_attackCollider.SetActive(true);
        }
        else
        {
            m_attackBool = false;
            m_shieldCollider.enabled = true;
            m_attackCollider.SetActive(false);
        }
    }

    public void GetDamage(int damage)
    {
        int hp = RetuneCrreantHp() - damage;
        m_rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
        m_anim.Play("Shielder_Damage");
        SetHp(hp, gameObject);
    }
}
