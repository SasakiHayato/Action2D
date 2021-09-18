using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportClass : MonoBehaviour
{
    private enum Status
    {
        Start,
        Goal,

        Teleport,
    }

    [SerializeField] private Status m_status;

    private bool m_check = false;

    static List<int> m_xTelePos = new List<int>();
    static List<int> m_yTelePos = new List<int>();

    static int m_hight = 0;
    static int m_wide = 0;

    void Update()
    {
        if (!m_check) return;

        if (Input.GetButtonDown("Submit1"))
        {
            if (m_status == Status.Start)
            {
                GameManager.Instance.SetScene("Dungeon");
                RemoveList();
            }
            else if (m_status == Status.Goal)
            {
                GameManager.Instance.SetScene("Midway");
                //GameManager.Instance.IsFadeAndSetScene(FadeType.Out, "Midway");
                RemoveList();
            }
            else if (m_status == Status.Teleport)
            {
                Teleport();
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

    public void AddTeleportPos(int x, int y)
    {
        m_xTelePos.Add(x);
        m_yTelePos.Add(y);
    }

    public void SetMap(int h, int v)
    {
        m_hight = h;
        m_wide = v;
    }

    private void Teleport()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        int random = Random.Range(0, m_xTelePos.Count);
        player.transform.position = new Vector3(m_xTelePos[random] * 8 - m_hight / 2, m_yTelePos[random] * 8 - m_wide / 2, 0);
    }

    private void RemoveList()
    {
        if (m_yTelePos == null) return;
        for (int i = 0; i < m_xTelePos.Count; i++)
        {
            m_xTelePos.Remove(i);
            m_yTelePos.Remove(i);
        }
    }
}
