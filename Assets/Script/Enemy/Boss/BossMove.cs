using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private Transform[] m_spown = new Transform[0];
    [SerializeField] private float m_activeSetTime = 0;
    private float m_activeTime = 0;

    private bool m_set = false;

    void Start()
    {
        m_activeTime = m_activeSetTime;
    }

    void Update()
    {
        if (!m_set)
        {
            Move();
            transform.localScale = new Vector2(0, 0);
            m_activeTime = m_activeSetTime;
        }
        else
        {
            ActiveBool();
            transform.localScale = new Vector2(0.3f, 0.3f);
        }
        FindPlayer();
    }

    private void Move()
    {
        m_set = true;

        float randomPoX = Random.Range(m_spown[0].position.x, m_spown[1].position.x);
        float randomPoY = Random.Range(m_spown[0].position.y, m_spown[1].position.y);

        transform.position = new Vector2(randomPoX, randomPoY);
    }

    private void ActiveBool()
    {
        m_activeTime -= Time.deltaTime;
        if (m_activeTime < 0)
        {
            m_set = false;
        }
    }

    private void FindPlayer()
    {
        GameObject playerOb = GameObject.Find("Player");
        LookAtPlayer(playerOb.transform);
    }

    private void LookAtPlayer(Transform plyerPos)
    {
        if (transform.position.x < plyerPos.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

}
