using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatus : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerContoller player;
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerContoller>();
            if (Input.GetButtonDown("Submit"))
            {
                player.m_attackPower += 20;
                Debug.Log(player.m_attackPower);
                Destroy(this.gameObject);
            }
        }
    }
}
