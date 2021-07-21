using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportClass : MonoBehaviour
{
    CreateMap m_map = new CreateMap();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Select();
        }   
    }

    private void Select()
    {
        bool check = true;

        while (check)
        {

        }
    }
}
