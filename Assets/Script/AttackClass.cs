﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackClass : MonoBehaviour
{
    private enum SetParent
    {
        Player,
        Enemy,
    }

    [SerializeField] SetParent m_parentEnum;
    [SerializeField] GameObject m_parent;

    bool m_isShield = false;
    public bool IsShield { get => m_isShield; set { m_isShield = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamage get = collision.GetComponent<IDamage>();
        if (get == null) return;
        
        if (m_isShield)
        {
            Debug.Log("ガード");
            m_isShield = false;
            return;
        }
        if (m_parentEnum == SetParent.Player) { get.GetDamage(PlayerDataClass.getInstance().SetAttack() * 10); }
        if (m_parentEnum == SetParent.Enemy) { EnemyAttack(get); }
    }

    void EnemyAttack(IDamage get)
    {
        EnemyBase enemyBase = m_parent.GetComponent<EnemyBase>();
        get.GetDamage(enemyBase.SetAttackPower());
    }
}
