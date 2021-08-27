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

    [SerializeField] SetParent m_parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamage get = collision.GetComponent<IDamage>();
        if (get == null) return;
       
        if (m_parent == SetParent.Player) { get.GetDamage(PlayerDataClass.Instance.SetAttack() * 10); }
        if (m_parent == SetParent.Enemy) { EnemyAttack(get); }
    }

    void EnemyAttack(IDamage get)
    {
        GameObject parent = transform.root.gameObject;
        NewEnemyBase enemyBase = parent.GetComponent<NewEnemyBase>();

        get.GetDamage(enemyBase.SetAttackPower());
    }
}
