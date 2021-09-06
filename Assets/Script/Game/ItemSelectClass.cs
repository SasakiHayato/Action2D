using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectClass : MonoBehaviour
{
    private enum Type
    {
        Horizontal,
        Vertical,
    }
    [SerializeField] Type m_type;

    private enum Status
    {
        ItemSelece,
        StatusUp,
    }
    [SerializeField] Status m_status;

    [SerializeField] int m_setCount = 0;
    List<GameObject> m_imageObjects = new List<GameObject>();

    bool m_selectBool = false;
    int m_crreantNum = 0;

    Vector2 m_defaultScale = Vector2.zero;
    Vector2 m_selectScale = Vector2.zero;

    void Start()
    {
        for (int i = 0; i < m_setCount; i++)
        {
            GameObject set = transform.GetChild(i).gameObject;
            m_imageObjects.Add(set);

            m_defaultScale = m_imageObjects[i].transform.localScale;
        }

        m_selectScale = m_defaultScale * 1.2f;
    }

    void Update()
    {
        float set = 0;
        if (m_type == Type.Horizontal) set = Input.GetAxisRaw("Horizontal");
        else set = Input.GetAxisRaw("Vertical") * -1;

        Select(set);
        SelectCrreant();

        if (Input.GetButtonDown("Submit1"))
        {
            if (m_status == Status.ItemSelece)
            {
                SetItem();
            }
        }
    }

    void SetItem()
    {
        if (m_crreantNum == 0)
        {

        }
        else if (m_crreantNum == 1)
        {

        }
    }

    void Select(float num)
    {
        if (num == 0)
        {
            m_selectBool = false;
        }

        if (num > 0 && !m_selectBool)
        {
            m_selectBool = true;
            m_crreantNum++;

            if (m_imageObjects.Count - 1 < m_crreantNum) m_crreantNum--;
        }
        if (num < 0 && !m_selectBool)
        {
            m_selectBool = true;
            m_crreantNum--;

            if (0 > m_crreantNum) m_crreantNum++;
        }
    }
    void SelectCrreant()
    {
        for (int i = 0; i < m_imageObjects.Count; i++)
        {
            m_imageObjects[i].transform.localScale = (i == m_crreantNum ? m_selectScale : m_defaultScale);
        }
    }
}
