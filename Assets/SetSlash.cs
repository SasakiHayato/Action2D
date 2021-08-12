using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSlash : MonoBehaviour
{
    [SerializeField] GameObject m_slash = default;
    private float m_setTime = 0;
    void Update()
    {
        m_setTime += Time.deltaTime;

        if (m_setTime > 0.25f)
        {
            GameObject slash = Instantiate(m_slash);
            float randomZ = Random.Range(0, 360);
            slash.transform.rotation = Quaternion.Euler(0, 0, randomZ);
            slash.transform.position = transform.position;
            m_setTime = 0;
        }
    }
}
