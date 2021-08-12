using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobEnemyCreate : MonoBehaviour
{
    [SerializeField] GameObject[] m_enemy = new GameObject[0];
    [SerializeField] Transform[] m_setPos = new Transform[0];

    public void SetEnemy()
    {
        for (int i = 0; i < m_setPos.Length; i++)
        {
            int randomEnemy = Random.Range(0, m_enemy.Length);
            GameObject enemy = Instantiate(m_enemy[randomEnemy]);
            enemy.transform.position = m_setPos[i].position;
        }
    }
}
