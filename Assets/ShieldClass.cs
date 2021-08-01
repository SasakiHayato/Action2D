using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldClass : MonoBehaviour
{
    [SerializeField] private PlayerManager m_player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            Debug.Log("a");
            m_player.m_shield = true;
        }
    }
}
