using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : MonoBehaviour
{
    GameObject m_target;
    Transform m_player;

    [SerializeField] Vector2 m_activeVec = Vector2.zero;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public GameObject SetTarget(GameObject set) { return m_target = set; }

    void Update()
    {
        if (m_target == null) return;
        transform.position = m_target.transform.position;

        TargetActive();
    }

    void TargetActive()
    {
        float absX = Mathf.Abs(transform.position.x - m_player.position.x);
        float absY = Mathf.Abs(transform.position.y - m_player.position.y);

        if (absX < m_activeVec.x && absY < m_activeVec.y)
        {
            m_target.SetActive(true);
        }
        else
        {
            m_target.SetActive(false);
        }
    }

    //private void OnBecameVisible() { m_target.SetActive(true); }
    //private void OnBecameInvisible() { m_target.SetActive(false); }
}
