using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBackGround : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_sptire = null;
    SpriteRenderer m_clone;

    float m_xPos;
    [SerializeField] private float m_speed = 0;

    void Start()
    {
        m_xPos = m_sptire.transform.position.x;

        m_clone = Instantiate(m_sptire);
        m_clone.transform.Translate(-m_sptire.bounds.size.x, 0, 0);
    }

    void Update()
    {
        m_sptire.transform.Translate(m_speed * Time.deltaTime, 0, 0);
        m_clone.transform.Translate(m_speed * Time.deltaTime, 0, 0);

        if (m_sptire.transform.position.x > m_xPos + m_sptire.bounds.size.x)
        {
            Debug.Log("a");
            m_sptire.transform.Translate(-m_sptire.bounds.size.x * 2, 0, 0);
        }
        if (m_clone.transform.position.x > m_xPos + m_clone.bounds.size.x)
        {
            m_clone.transform.Translate(-m_clone.bounds.size.x * 2, 0, 0);
        }
    }
}
