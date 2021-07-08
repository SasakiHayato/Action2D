using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContoller : PlayerContoller
{
    private float speed = 3;

    private float m_time = 0;
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
        EnemyController enemy;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.GetComponent<EnemyController>();
            enemy.EnemyDamage(m_magicPower);
            enemy.m_rigidbody.AddForce(transform.up * 3, ForceMode2D.Impulse);
            //Destroy(this.gameObject);
        }
    }
}
