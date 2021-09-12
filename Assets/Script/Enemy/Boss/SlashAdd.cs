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
        if (m_setTime > 0.2f)
        {
            GameObject set = Instantiate(m_setObject);
            int rotetion = Random.Range(0, 360);
            set.transform.position = transform.position;
            set.transform.Rotate(0, 0, rotetion, Space.World);
            m_setTime = 0;
        }
    }
}
