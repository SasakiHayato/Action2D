using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatus : MonoBehaviour
{
    PlayerContoller m_player;
    Uicontroller m_ui;

    private void Start()
    {
        m_ui = FindObjectOfType<Uicontroller>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            m_player = collision.GetComponent<PlayerContoller>();
            if (Input.GetButtonDown("Submit1"))
            {
                m_ui.m_freeze = true;
                m_ui.m_slectCanvas.SetActive(true);
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
            m_player.m_shieldPower += 20;
            m_ui.m_shieldPoint++;
        }
        else if (set == 2)
        {
            m_player.m_attackPower += 20;
            m_ui.m_attackPoint++;
        }
        
        m_player.m_freeze = false;
        m_ui.m_freeze = false;
        m_ui.m_slectCanvas.SetActive(false);
        Destroy(this.gameObject);
    }
}
