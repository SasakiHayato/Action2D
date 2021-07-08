using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatus : MonoBehaviour
{
    GameObject m_canvas;
    PlayerContoller m_player;

    void Start()
    {
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
        }
        else if (set == 1)
        {
            m_player.m_attackPower += 20;
        }
        Debug.Log(m_player.m_attackPower);

        m_canvas.SetActive(false);
        m_player.m_freeze = false;
    }
}
