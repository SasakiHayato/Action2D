using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour, IDamage
{
    [SerializeField] private int m_hp = 0;
    [SerializeField] private Slider m_hpSlider;

    [SerializeField] private MobEnemyCreate m_mob;

    private Animator m_animator;

    private bool m_action = false;
    private bool m_attackAction = false;

    private float m_hpPasent = 0;

    void Start()
    {
        m_hpSlider = m_hpSlider.GetComponent<Slider>();
        m_animator = GetComponent<Animator>();
        
        m_hpSlider.maxValue = m_hp;
        m_hpSlider.value = m_hp;

        m_hpPasent = m_hp / 100;
    }

    void Update()
    {
        if (m_action) return;
        if (!m_attackAction)
        {
            AttackSelect();
        }
        else
        {
            m_animator.Play("Boss_Idle");
        }
        
    }

    private void AttackSelect()
    {
        int random = Random.Range(0, 2);
        //int random = 1;
        m_attackAction = true;
        switch (random)
        {
            case 0:
                m_animator.Play("Boss_Attack_1");
                //Attack1();
                break;
            case 1:
                m_animator.Play("Boss_Attack_2");
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

    public void GetDamage(int damage)
    {
        m_hp -= damage;
        m_hpSlider.value = m_hp;
        HpPasentCheck();
    }

    private void HpPasentCheck()
    {
        if (m_hp <= m_hpPasent * 80)
        {
            m_mob.SetEnemy();
        }
    }

    public bool Frezze()
    {
        if (!m_action)
        {
            m_action = true;
        }
        else
        {
            m_action = false;
        }

        return m_action;
    }
}
