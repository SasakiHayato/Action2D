using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveImage : MonoBehaviour
{
    SpriteRenderer m_target;
    float r, g, b;
    float m_alfa = 1;
    void Start()
    {
        m_target = GetComponent<SpriteRenderer>();
        r = m_target.color.r;
        g = m_target.color.g;
        b = m_target.color.b;
    }

    void Update()
    {
        m_target.color = new Color(r, g, b, m_alfa);
        m_alfa -= Time.deltaTime / 2;
        if (m_alfa < 0)
            Destroy(gameObject);
    }
}
