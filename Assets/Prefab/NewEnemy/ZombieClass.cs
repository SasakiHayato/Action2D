using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieClass : NewEnemyBase
{
    [SerializeField] NewBehaviorTree m_tree;
    Animator m_anim;
    Rigidbody2D m_rb;

    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        m_tree.Tree();    
    }

    public override void Move()
    {
        Debug.Log("移動");
        FieldCheck();

        if (SetSpeed() != 0) { m_anim.Play("Enemy_Walk"); }
        else { m_anim.Play("Enemy_Idle"); }

        m_rb.velocity = new Vector2(SetSpeed(), m_rb.velocity.y);
    }

    public override void Attack1()
    {
        Debug.Log("攻撃１");
        FindPlayerToLook();
        m_anim.Play("Enemy_Attack");
    }

    public override void Attack2()
    {
        Debug.Log("Attack2");
        FindPlayerToLook();
        m_rb.AddForce(new Vector2(RetuneStepFloat() * -6, 3), ForceMode2D.Impulse);
        m_anim.Play("Enemy_Attack");
        StartCoroutine(SetFalse(5));
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
        StartCoroutine(SetFalse(5));
    }

    IEnumerator SetFalse(float time)
    {
        yield return new WaitForSeconds(time);
        m_tree.SetFalseToAction();
    }
}
