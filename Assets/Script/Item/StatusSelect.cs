using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusSelect : MonoBehaviour
{
    GameObject[] m_selectObject = new GameObject[3];

    Vector2 m_selectScale;
    Vector2 m_scale;

    private int m_selectNum = 0;
    private bool m_select = false;

    ItemStatus m_item;

    void Start()
    {
        m_item = FindObjectOfType<ItemStatus>();

        m_selectScale = new Vector2(1.2f, 1.2f);

        for (int i = 0; i < m_selectObject.Length; i++)
        {
            m_selectObject[i] = transform.GetChild(i).gameObject;
            m_scale = m_selectObject[i].transform.localScale;
        }
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h == 0)
        {
            m_select = false;
        }
        if (h > 0 && !m_select)
        {
            m_selectNum ++;
            m_select = true;
            if (m_selectNum >= m_selectObject.Length)
            {
                m_selectNum--;
            }
        }
        else if (h < 0 && !m_select)
        {
            m_selectNum --;
            m_select = true;
            if (m_selectNum < 0)
            {
                m_selectNum++;
            }
        }
        
        Select();
        if (Input.GetButtonDown("Submit1"))
        {
            m_item.SetStatus(ref m_selectNum);
        }
    }

    private void Select()
    {
        for (int i = 0; i < m_selectObject.Length; i++)
        {
            m_selectObject[i].transform.localScale = (i == m_selectNum ? m_selectScale : m_scale);
        }
    }
}
