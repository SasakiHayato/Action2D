using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private float m_speed = 3;

    NewArcheryController m_archery;

    void Start()
    {
        m_archery = FindObjectOfType<NewArcheryController>();
        Invoke("DestroyBullet", 2.0f);
    }

    void Update()
    {
        transform.Translate(m_speed / 8, 0, 0);
    }

    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerContoller player;
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerContoller>();
            player.PlayerDamage(m_archery.AddDamage());
            DestroyBullet();
        }
    }
}
