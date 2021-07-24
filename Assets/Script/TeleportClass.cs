using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportClass : MonoBehaviour
{
    private enum Status
    {
        Start,
        Goal,
    }

    [SerializeField] private Status m_status;

    GameManager m_manager = new GameManager();
    private bool m_check = false;

    void Update()
    {
        if (!m_check) return;

        if (Input.GetButtonDown("Submit1"))
        {
            if (m_status == Status.Start)
            {
                m_manager.LoadD();
            }
            else if (m_status == Status.Goal)
            {
                m_manager.LoadM();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_check = true;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_check = false;
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
