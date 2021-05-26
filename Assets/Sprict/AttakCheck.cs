using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakCheck : MonoBehaviour
{
    [SerializeField] PlayerContoller player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyManager enemy;
        enemy = collision.gameObject.GetComponent<EnemyManager>();

        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy.Damage(player.m_attackPower);
        }
    }
}
