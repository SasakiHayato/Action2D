using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] public float m_speed = 0;
    [SerializeField] public float m_hp = 0;
    [System.NonSerialized] public float m_nowHp = 0;
    [SerializeField] public int m_attackPower = 0;
    [System.NonSerialized] public float m_dSpeed;

    [SerializeField] public Vector2 m_playerRay = Vector2.zero;
    [SerializeField] public LayerMask m_playerLayer = 0;

    [SerializeField] Vector2 m_wallRay = Vector2.zero;
    [SerializeField] LayerMask m_wallLayer = 0;

   [SerializeField] private GameObject m_bloodPrefub;

    public bool m_freeze = false;

    [System.NonSerialized] public Rigidbody2D m_rigidbody;

    public void WallCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_wallRay, m_wallRay.magnitude, m_wallLayer);

        if (hit.collider)
        {
            m_speed = 0;
            StartCoroutine(StartWalk(50));
            m_playerRay *= -1;
            m_wallRay *= -1;
        }
    }

    public void StartPos()
    {
        if (m_speed < 0)
        {
            transform.localScale = new Vector2(-0.15f, 0.15f);
            m_playerRay *= -1;
            m_wallRay *= -1;
        }
        else
        {
            transform.localScale = new Vector2(0.15f, 0.15f);
        }
    }

    private IEnumerator StartWalk(int time)
    {
        for (int i = 0; i < time; i++)
        {
            yield return null;
        }
        
        MoveCheck();
    }

    private void MoveCheck()
    {
        if (transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(0.15f, 0.15f);
            m_speed = -m_dSpeed;
        }
        else
        {
            transform.localScale = new Vector2(-0.15f, 0.15f);
            m_speed = m_dSpeed;
        }
    }

    public void ThisDie()
    {
        GameObject blood = Instantiate(m_bloodPrefub, this.transform);
        blood.transform.parent = null;
        blood.transform.localScale = new Vector2(2, 2);

        Vector2 vector = blood.transform.position;
        blood.transform.position = new Vector3(vector.x, vector.y, 1);

        Destroy(this.gameObject);
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
