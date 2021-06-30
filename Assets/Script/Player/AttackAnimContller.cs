using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimContller : MonoBehaviour
{
    [SerializeField] public GameObject m_attack;
    [SerializeField] public GameObject m_attckCombo;
    void Start()
    {
        m_attack.SetActive(false);
        m_attckCombo.SetActive(false);
    }

    //void AttackStart()
    //{
    //    if (m_attack) { m_attack.SetActive(true); }
    //}

    //void AttackEnd()
    //{
    //    if (m_attack) { m_attack.SetActive(false); }
        
    //}

    void ComboAttackStart()
    {
        if (m_attckCombo) { m_attckCombo.SetActive(true); }
    }

    void ComboAttackEnd()
    {
        if (m_attckCombo) { m_attckCombo.SetActive(false); }
    }
}
