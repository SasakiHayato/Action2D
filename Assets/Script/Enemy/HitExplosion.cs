using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitExplosion : MonoBehaviour
{
    GameObject m_player;
    PlayerContoller m_playerC;

    private Vector2 m_vector = new Vector2(10, 10);

    public void Hit()
    {
        if (m_player.transform.position.x < this.transform.position.x)
        {
            Debug.Log("左");
            m_playerC.m_rigidbody.AddForce(new Vector2(-m_vector.x, m_vector.y), ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("右");
            m_playerC.m_rigidbody.AddForce(m_vector, ForceMode2D.Impulse);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        m_player = GameObject.Find("player").gameObject;
        m_playerC = FindObjectOfType<PlayerContoller>();
        Hit();
    }
}
