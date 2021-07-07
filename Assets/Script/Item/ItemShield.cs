using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerContoller player;
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerContoller>();
            if (Input.GetButtonDown("Submit"))
            {
                player.m_subAttack = 1;

                Destroy(this.gameObject);
            }
        }
    }
}
