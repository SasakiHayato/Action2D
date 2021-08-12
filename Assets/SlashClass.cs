using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashClass : MonoBehaviour
{
    private float m_desTime = 0;

    void Update()
    {
        m_desTime += Time.deltaTime;

        if (m_desTime > 1.5f)
        {
            Destroy(gameObject);
        }
    }
}
