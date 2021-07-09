using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] public float m_speed = 0;
    [SerializeField] public float m_hp = 0;

    [SerializeField] public Vector2 m_playerRay = Vector2.zero;
    [SerializeField] public LayerMask m_playerLayer = 0;

    [SerializeField] Vector2 m_wallRay = Vector2.zero;
    [SerializeField] LayerMask m_wallLayer = 0;

    public bool m_freeze = false;

    public void WallCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_wallRay, m_wallRay.magnitude, m_wallLayer);

        if (hit.collider)
        {
            m_speed = 0;
            StartCoroutine(StartWalk(50));
        }
    }

    private IEnumerator StartWalk(int time)
    {
        for (int i = 0; i < time; i++)
        {
            yield return null;
        }

        m_speed = 2;
        m_speed *= -1;
        m_playerRay *= -1;
        transform.localScale = new Vector2(-0.15f, 0.15f);

    }

    private bool Freeze()
    {
        if (!m_freeze)
        {
            m_freeze = true;
        }
        else
        {
            m_freeze = false;
        }

        return m_freeze;
    }
}
