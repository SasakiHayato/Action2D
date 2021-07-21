using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    //[SerializeField] PlayerContoller m_player;
    PlayerDataClass m_playerData;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //PlayerDataClass playerData;
        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy.EnemyDamage(m_playerData.m_attackPower);
        }
    }
}
