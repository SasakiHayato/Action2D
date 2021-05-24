using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoller : MonoBehaviour
{
    Rigidbody2D m_rigidbody;

    [SerializeField] private float enemySpeed = 0;
    [SerializeField] private float m_enemyHp = 0;

    PlayerContoller m_player;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_player = GetComponent<PlayerContoller>();
    }

    void Update()
    {
        Move();
    }

    public void Damage(float damage)
    {
        m_enemyHp -= damage;
        Debug.Log(m_enemyHp);
    }

    void Move()
    {
        //Vector2 moveVector = (m_player.transform.position - transform.position);
        //m_rigidbody.velocity = new Vector2(moveVector.x * enemySpeed, m_rigidbody.velocity.y);
    }
}
