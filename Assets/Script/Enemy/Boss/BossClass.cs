using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClass : NewEnemyBase
{
    [SerializeField] NewBehaviorTree m_tree;
    [SerializeField] Transform m_minPos;
    [SerializeField] Transform m_maxPos;

    [SerializeField] GameObject m_slashing;
    [SerializeField] GameObject m_bullet;
    [SerializeField] Transform[] m_spownPos = new Transform[0];

    float m_time;
    int m_count = 15;

    Animator m_anim;

    void Start()
    {
        m_anim = GetComponent<Animator>();
    }
    void Update()
    {
        m_tree.Tree();
    }

    public override void Move()
    {
        LookToPlayer();
        Debug.Log("行動中");
        if (!Interval(2)) return;
        float posX = Random.Range(m_minPos.position.x, m_maxPos.position.x);
        float posY = Random.Range(m_minPos.position.y, m_maxPos.position.y);

        transform.position = new Vector2(posX, posY);

        m_tree.Interval(0);
    }

    public override void Attack1()
    {
        Debug.Log("攻撃１");
        m_anim.Play("Boss_Attack_2");
        StartCoroutine(SetBullet());
    }

    public override void Attack2()
    {
        Debug.Log("攻撃２");
        
        m_anim.Play("Boss_Attack_1");
        GameObject slash = Instantiate(m_slashing);
        slash.transform.position = transform.position;
        m_tree.Interval(5);
    }

    private IEnumerator SetBullet()
    {
        while (m_count != 0)
        {
            yield return new WaitForSeconds(0.5f);
            float randomX = Random.Range(m_spownPos[0].position.x, m_spownPos[1].position.x);

            GameObject bullet = Instantiate(m_bullet);
            bullet.transform.position = new Vector2(randomX, 12);
            m_count--;
        }
        m_count = 15;
        m_tree.Interval(0);
    }

    bool Interval(float time)
    {
        bool interval;

        m_time += Time.deltaTime;
        if (m_time > time)
        {
            interval = true;
            m_time = 0;
        }
        else
        {
            interval = false;
        }

        return interval;
    }
    void LookToPlayer()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (transform.position.x > player.position.x) { transform.localScale = new Vector2(-0.3f, 0.3f); }
        else { transform.localScale = new Vector2(0.3f, 0.3f); }
    }
}
