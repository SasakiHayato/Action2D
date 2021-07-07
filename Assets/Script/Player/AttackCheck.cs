using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    [SerializeField] PlayerContoller m_player;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    EnemyManager enemy;
    //    enemy = collision.gameObject.GetComponent<EnemyManager>();

    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        enemy.EnemyDamage(m_player.m_attackPower);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("a");
        }
    }
}
