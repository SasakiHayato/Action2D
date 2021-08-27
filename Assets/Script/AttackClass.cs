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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamage get = collision.GetComponent<IDamage>();
        if (get == null) return;
       
        if (m_parentEnum == SetParent.Player) { get.GetDamage(PlayerDataClass.Instance.SetAttack() * 10); }
        if (m_parentEnum == SetParent.Enemy) { EnemyAttack(get); }
    }

    void EnemyAttack(IDamage get)
    {
        Debug.Log(m_parent);
        NewEnemyBase enemyBase = m_parent.GetComponent<NewEnemyBase>();
        Debug.Log(enemyBase);
        get.GetDamage(enemyBase.SetAttackPower());
    }
}
