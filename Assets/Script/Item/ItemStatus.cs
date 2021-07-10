using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatus : ItemBase
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Submit1"))
            {
                CheckEnum();
                m_ui.m_freeze = true;
                m_player.m_freeze = true;
                Destroy(this.gameObject);
            }
        }
    }
}
