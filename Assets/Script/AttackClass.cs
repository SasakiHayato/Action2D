using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SetParent
{
    Player,
    Enemy,
}

public class AttackClass : MonoBehaviour
{
    [SerializeField] SetParent m_parentEnum;
    [SerializeField] GameObject m_parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        IDamage get = collision.GetComponent<IDamage>();
        if (get == null) return;

        if (m_parentEnum == SetParent.Player) { get.GetDamage(PlayerDataClass.Instance.SetAttack() * 10); }
        if (m_parentEnum == SetParent.Enemy) { EnemyAttack(get); }
    }

    void EnemyAttack(IDamage get)
    {
        NewEnemyBase enemyBase = m_parent.GetComponent<NewEnemyBase>();
        get.GetDamage(enemyBase.SetAttackPower());
    }
}
