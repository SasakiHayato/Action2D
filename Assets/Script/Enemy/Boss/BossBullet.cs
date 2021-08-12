using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private Rigidbody2D m_rb;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();

        float randomY = Random.Range(1, 5);
        m_rb.AddForce(new Vector2(0, randomY), ForceMode2D.Impulse);
    }

}
