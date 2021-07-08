using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContoller : MonoBehaviour
{
    private float speed = 3;
    private float m_time = 0;

    PlayerContoller m_player;

    void Start()
    {
        m_player = FindObjectOfType<PlayerContoller>();
    }

    void Update()
    {
        transform.Translate(speed / 8, 0, 0);
        m_time += Time.deltaTime;
        if (m_time > 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ZombieController enemy;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.GetComponent<ZombieController>();
            enemy.EnemyDamage(m_player.m_magicPower);
            enemy.m_rigidbody.AddForce(transform.up * 3, ForceMode2D.Impulse);
            //Destroy(this.gameObject);
        }
    }
}
