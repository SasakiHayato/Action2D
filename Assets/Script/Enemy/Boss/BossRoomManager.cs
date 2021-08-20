using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossRoomManager : MonoBehaviour
{
    [SerializeField] BossController m_boss;

    [SerializeField] GameObject[] m_enemy = new GameObject[0];
    [SerializeField] Transform[] m_setPos = new Transform[0];

    private List<GameObject> m_enemyObs = new List<GameObject>();

    private float m_hpPasent = 0;

    private bool m_enemyActive = false;
    private bool m_action1 = false;

    void Start()
    {
        m_hpPasent = m_boss.ReturnCurrentHp() / 100;
    }

    void Update()
    {
        if (m_boss.ReturnCurrentHp() <= m_hpPasent * 75 && !m_action1)
        {
            m_action1 = true;
            SetEnemy();
            BossActiveFalse();
        }

        if (m_enemyActive)
        {
            //NowEnemyActive();
        }
    }

    private void SetEnemy()
    {
        for (int i = 0; i < m_setPos.Length; i++)
        {
            int randomEnemy = Random.Range(0, m_enemy.Length);
            GameObject enemy = Instantiate(m_enemy[randomEnemy]);
            enemy.transform.position = m_setPos[i].position;

            SetEnemyActive(enemy);
        }

        m_enemyActive = true;
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
