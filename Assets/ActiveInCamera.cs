using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInCamera : MonoBehaviour
{
    Renderer m_renderer;
    GameObject m_ob;

    private bool m_active = false;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_ob = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (m_ob == null) return;
        if (m_renderer.isVisible)
        {
            if (!m_active)
            {
                m_active = true;
                m_ob.SetActive(m_active);
            }
        }
        else
        {
            if (m_active)
            {
                m_active = false;
                m_ob.SetActive(m_active);
            }
        }

    }
}
