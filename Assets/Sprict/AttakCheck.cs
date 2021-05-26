using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakCheck : MonoBehaviour
{
    [SerializeField] PlayerContoller player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyContoller enemy;
        enemy = collision.GetComponent<EnemyContoller>();

        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy.Damage(player.m_attackPower);
        }
    }
}
