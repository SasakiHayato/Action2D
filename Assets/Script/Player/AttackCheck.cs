using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    [SerializeField] PlayerContoller m_player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ZombieController enemy = collision.GetComponent<ZombieController>();
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy.EnemyDamage(m_player.m_attackPower);
        }
    }
}
