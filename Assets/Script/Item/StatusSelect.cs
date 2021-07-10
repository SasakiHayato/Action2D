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

    ItemStatus m_item;

    void Start()
    {
        m_item = FindObjectOfType<ItemStatus>();

        m_selectScale = new Vector2(1.2f, 1.2f);

        for (int i = 0; i < m_selectObject.Length; i++)
        {
            m_selectObject[i] = transform.GetChild(i).gameObject;
            Debug.Log(m_selectObject[i]);
            m_scale = m_selectObject[i].transform.localScale;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_selectNum ++;
            if (m_selectNum >= m_selectObject.Length)
            {
                m_selectNum--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            m_selectNum --;
            if (m_selectNum < 0)
            {
                m_selectNum++;
            }
        }
        
        Select();
        Debug.Log(m_selectNum);
        if (Input.GetButtonUp("Jump"))
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
