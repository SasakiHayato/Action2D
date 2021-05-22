using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoller : MonoBehaviour
{
    Rigidbody2D m_rigidbody;

    [SerializeField] private float enemySpeed = 0;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        m_rigidbody.velocity = new Vector2( enemySpeed, m_rigidbody.velocity.y);
    }
}
