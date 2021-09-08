using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAdd : MonoBehaviour
{
    [SerializeField] GameObject m_setObject = default;
    float m_setTime = 0;

    void Update()
    {
        m_setTime += Time.deltaTime;
        if (m_setTime > 0.5f)
        {
            GameObject set = Instantiate(m_setObject);
            set.transform.position = transform.position;
            m_setTime = 0;
        }
    }
}
