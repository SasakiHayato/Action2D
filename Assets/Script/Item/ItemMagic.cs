using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagic : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerContoller player;
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerContoller>();
            if (Input.GetButtonDown("Submit1"))
            {
                player.m_subAttack = 2;

                Destroy(this.gameObject);
            }
        }
    }
}
