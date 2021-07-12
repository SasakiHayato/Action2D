using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeel : ItemBase
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Submit1"))
            {
                if (m_player.m_Hp == 100)
                {
                    Debug.Log("無理");
                }
                else
                {
                    CheckEnum();
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
