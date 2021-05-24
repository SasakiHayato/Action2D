using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyContoller enemy;
        PlayerContoller player;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("当たった");
            enemy = GetComponent<EnemyContoller>();
            player = GetComponent<PlayerContoller>();

            enemy.Damage(player.m_attackPower);
        }
    }
}
