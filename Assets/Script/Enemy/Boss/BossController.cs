using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : EnemyBase
{
    [SerializeField] private Slider m_hpSlider;

    private Animator m_anim;
    private bool m_attackAction = false;

    private Collider2D m_collider;

    void Start()
    {
        m_hpSlider = m_hpSlider.GetComponent<Slider>();
        m_anim = GetComponent<Animator>();
        m_collider = GetComponent<Collider2D>();

        m_hpSlider.maxValue = ReturnCurrentHp();
        m_hpSlider.value = ReturnCurrentHp();
    }

    void Update()
    {
        if (m_collider.enabled == false) return;

        if (SetFreeze()) return;

        Move();
    }

    public override void Move()
    {
        if (!m_attackAction) { AttackSelect(); }
        else { m_anim.Play("Boss_Idle"); }
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    private void AttackSelect()
    {
        int random = Random.Range(0, 2);
        m_attackAction = true;
        switch (random)
        {
            case 0:
                m_anim.Play("Boss_Attack_1");
                //Attack1();
                break;
            case 1:
                m_anim.Play("Boss_Attack_2");
                //Attack2();
                break;
        }

        StartCoroutine(AttackInterval());
    }

    private IEnumerator AttackInterval()
    {
        yield return new WaitForSeconds(9f);
        m_attackAction = false;
    }

    public override void GetDamage(float damage)
    {
        float hp = ReturnCurrentHp();
        hp -= damage;
        SetHp(hp);
        
        m_hpSlider.value = ReturnCurrentHp();
    }
}
