using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private GameObject m_bullet = default;
    [SerializeField] private Transform[] m_spownPos = new Transform[0];

    public void Attack2()
    {
        float randomX = Random.Range(m_spownPos[0].position.x, m_spownPos[1].position.x);
        
        GameObject bullet = Instantiate(m_bullet);
        bullet.transform.position = new Vector2(randomX, 12);
    }

}
