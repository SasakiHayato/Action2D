using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatus : ItemBase
{
    private bool m_check = false;
    GameObject m_itemText = null;
    GameObject m_plyer = null;

    private Vector2 vector = Vector2.zero;
    void Start()
    {
        m_itemText = transform.GetChild(0).gameObject;
        m_itemText.SetActive(m_check);
    }

    void Update()
    {
        m_itemText.SetActive(m_check);
        if (!m_check) return;
        m_itemText.transform.position = SetTextPos(vector, m_plyer.transform);
        if (Input.GetButtonDown("Submit1"))
        {
            CheckEnum();

            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_plyer = collision.gameObject;
            m_check = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_plyer = null;
            m_check = false;
        }
    }
}
