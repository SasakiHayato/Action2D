using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoller : MonoBehaviour
{
    Rigidbody2D m_rigidbody;

    [SerializeField] private float enemySpeed = 0;
    [SerializeField] private float m_enemyHp = 0;

    GameObject player;

    private bool stay = false;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (stay == false)
        {
            if (enemySpeed > 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else if (enemySpeed < 0)
            {
                transform.localScale = new Vector2(1, 1);
            }

            m_rigidbody.velocity = new Vector2(enemySpeed, m_rigidbody.velocity.y);
        }
        else if (stay == true)
        {
            ChaseMove();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stay = false;
        }
    }

    void ChaseMove()
    {
        Vector2 moveVector = (player.transform.position - transform.position);
        m_rigidbody.velocity = new Vector2(moveVector.x * enemySpeed, m_rigidbody.velocity.y);

        if (moveVector.x < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (moveVector.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void Damage(float damage)
    {
        m_enemyHp -= damage;
        Debug.Log(m_enemyHp);
    }
}
