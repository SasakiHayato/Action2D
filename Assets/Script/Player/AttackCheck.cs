using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        Debug.Log(enemy);
        if (enemy != null)
        { 
            enemy.GetDamage(PlayerDataClass.Instance.m_attackPower * 10);
        }
    }
}
