using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : MonoBehaviour
{
    GameObject m_target;
    
    public GameObject SetTarget(GameObject set)
    {
        return m_target = set;
    }

    void Update()
    {
        transform.position = m_target.transform.position;
    }

    private void OnBecameVisible() { m_target.SetActive(true); }
    private void OnBecameInvisible() { m_target.SetActive(false); }
}
