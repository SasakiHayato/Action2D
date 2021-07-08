using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusSelect : MonoBehaviour
{
    GameObject[] m_selectObject = new GameObject[2];

    Vector2 m_selectScale;
    Vector2 m_scale;

    private int m_selectNum = 0;

    ItemStatus m_item;

    void Start()
    {
        m_item = FindObjectOfType<ItemStatus>();

        m_scale = this.transform.localScale;
        m_selectScale = new Vector2(1.5f, 1.5f);

        for (int i = 0; i < m_selectObject.Length; i++)
        {
            m_selectObject[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            m_selectNum = 1;
        }
        else if (h < 0)
        {
            m_selectNum = 0;
        }
        Select();

        if (Input.GetButtonDown("Jump"))
        {
            m_item.SetStatus(m_selectNum);
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
