using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyActive : MonoBehaviour
{
    GameObject m_target;
    Transform m_player;
    EnemyBase m_base;
    public EnemyBase GetBase { get => m_base; set { m_base = value; } }

    [SerializeField] GameObject m_slider;

    Slider m_hpSlider;
    int m_setHp = 0;
    public int GetSliderMaxHp { get => m_setHp; set { m_setHp = value; } }

    [SerializeField] Vector2 m_activeVec = Vector2.zero;
    bool m_active = false;
    bool m_isNull = false;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;

        m_hpSlider = m_slider.transform.GetChild(0).GetComponent<Slider>();
        m_hpSlider.maxValue = m_setHp;
        m_hpSlider.value = m_setHp;
    }

    public GameObject SetTarget(GameObject set) { return m_target = set; }

    void Update()
    {
        if (m_target == null)
        {
            if (!m_isNull)
            {
                m_isNull = true;
                m_slider.SetActive(false);
            }
            return;
        }
        if (m_hpSlider.maxValue > m_base.GetHp)
        {
            m_slider.SetActive(true);
            m_hpSlider.value = m_base.GetHp;
        }
        else
        {
            m_slider.SetActive(false);
        }
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
