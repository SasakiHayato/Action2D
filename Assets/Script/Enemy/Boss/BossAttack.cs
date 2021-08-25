using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private GameObject m_bullet = default;
    [SerializeField] private GameObject m_slashing = default;

    [SerializeField] private Transform[] m_spownPos = new Transform[0];

    [SerializeField] private float m_waitTime = 0;

    private int m_count = 15;

    public void Attack1()
    {
        SetSlash();
    }

    private void SetSlash()
    {
        GameObject slash = Instantiate(m_slashing);
        slash.transform.position = transform.position;
    }

    public void Attack2()
    { 
        StartCoroutine(SetBullet());
    }

    private IEnumerator SetBullet()
    {
        while(m_count != 0)
        {
            yield return new WaitForSeconds(m_waitTime);
            float randomX = Random.Range(m_spownPos[0].position.x, m_spownPos[1].position.x);

            GameObject bullet = Instantiate(m_bullet);
            bullet.transform.position = new Vector2(randomX, 12);
            m_count--;
        }
        m_count = 15;
    }
}
