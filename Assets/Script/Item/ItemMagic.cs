using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagic : ItemBase
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Submit1"))
            {
                CheckEnum();
                //m_playerData.ItemCheck(1);
                Destroy(this.gameObject);
            }
        }
    }
}
