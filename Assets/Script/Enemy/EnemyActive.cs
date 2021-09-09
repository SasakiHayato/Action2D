using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : MonoBehaviour
{
    GameObject m_target;
    Transform m_player;

    [SerializeField] Vector2 m_activeVec = Vector2.zero;
    bool m_active = false;

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

        if (absX < m_activeVec.x && absY < m_activeVec.y && !m_active)
        {
            m_target.SetActive(true);
            BehaviourTree tree = m_target.GetComponent<BehaviourTree>();
            tree.IntervalSetFalse(0);
            m_active = true;
        }
        if (absX > m_activeVec.x && absY > m_activeVec.y && m_active)
        {
            m_target.SetActive(false);
            m_active = false;
        }
    }
}
