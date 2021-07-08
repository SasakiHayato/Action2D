using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatus : MonoBehaviour
{
    GameObject m_canvas;
    PlayerContoller m_player;
    Uicontroller m_ui;

    void Start()
    {
        m_ui = FindObjectOfType<Uicontroller>();

        m_canvas = transform.GetChild(0).gameObject;
        m_canvas.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            m_player = collision.GetComponent<PlayerContoller>();
            if (Input.GetButtonDown("Submit1"))
            {
                m_canvas.SetActive(true);
                m_player.m_freeze = true;
            }
        }
    }

    public void SetStatus(int set)
    {
        if (set == 0)
        {
            m_player.m_magicPower += 20;
            m_ui.m_magicPoint++;
        }
        else if (set == 1)
        {
            m_player.m_attackPower += 20;
            m_ui.m_attackPoint++;
        }

        m_canvas.SetActive(false);
        m_player.m_freeze = false;
    }
}
