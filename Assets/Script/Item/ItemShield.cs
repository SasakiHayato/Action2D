using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : ItemBase
{
    private bool m_check = false;

    void Update()
    {
        if (!m_check) return;
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
            m_check = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_check = false;
        }
    }
}
