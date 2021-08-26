using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossRoomManager : MonoBehaviour
{
    [SerializeField] BossClass m_boss;

    [SerializeField] GameObject[] m_enemy = new GameObject[0];
    [SerializeField] Transform[] m_setPos = new Transform[0];

    private List<GameObject> m_enemyObs = new List<GameObject>();

    public void SetEnemy()
    {
        for (int i = 0; i < m_setPos.Length; i++)
        {
            int randomEnemy = Random.Range(0, m_enemy.Length);
            GameObject enemy = Instantiate(m_enemy[randomEnemy]);
            enemy.transform.position = m_setPos[i].position;

            SetEnemyActive(enemy);
        }
    }

    private void SetEnemyActive(GameObject enemy)
    {
        m_enemyObs.Add(enemy);
    }

    private void BossActiveFalse()
    {
        SpriteRenderer bossImage = m_boss.GetComponent<SpriteRenderer>();
        bossImage.color = new Color(bossImage.color.r, bossImage.color.g, bossImage.color.b, 0.5f);

        Collider2D bosscollisoin = m_boss.GetComponent<Collider2D>();
        bosscollisoin.enabled = false;
    }
}
