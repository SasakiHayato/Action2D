using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour, IDamage
{
    [SerializeField] private int m_hp = 0;
    [SerializeField] private Slider m_hpSlider;

    private Animator m_animator;

    private bool m_action = false;
    private bool m_attackAction = false;

    void Start()
    {
        m_hpSlider = m_hpSlider.GetComponent<Slider>();
        m_animator = GetComponent<Animator>();
        
        m_hpSlider.maxValue = m_hp;
        m_hpSlider.value = m_hp;
    }

    void Update()
    {
        if (m_action) return;

        if (!m_attackAction)
        {
            AttackSelect();
        }
        
    }

    private void AttackSelect()
    {
        //int random = Random.Range(0, 2);
        int random = 1;
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
    }

    public void GetDamage(int damage)
    {
        m_hp -= damage;
        m_hpSlider.value = m_hp;
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
