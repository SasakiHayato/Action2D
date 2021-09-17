using System.Collections;
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

    int m_attackPower;
    public int AttackPower { get => m_attackPower; set { m_attackPower = value; } }

    int m_power;
    public int GetPower { get => m_power; set { m_power = value; } }

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
        if (m_parentEnum == SetParent.Player) { get.GetDamage(m_attackPower); }
        if (m_parentEnum == SetParent.Enemy) { EnemyAttack(get); }
    }

    void EnemyAttack(IDamage get)
    {
        EnemyBase enemyBase = m_parent.GetComponent<EnemyBase>();
        if (enemyBase.GetAttackPower <= 0)
        {
            enemyBase.GetAttackPower = GetPower;
        }
        int power = enemyBase.GetAttackPower - ((PlayerDataClass.getInstance().SetShield() - 1) * 10);
        if (power <= 0)
        {
            power = 5;
        }
        get.GetDamage(power);
    }
}
