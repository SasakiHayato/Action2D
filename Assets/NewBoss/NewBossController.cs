using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBossController : EnemyBase
{
    [SerializeField] BehaviorTree m_tree;
    [SerializeField] Transform m_setMinPos;
    [SerializeField] Transform m_setMaxPos;
    GameObject m_player;

    Animator m_anim;

    float m_intervalTime = 0;
    bool m_interval;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_anim = GetComponent<Animator>();
        m_tree.GetAnimator(gameObject);
        m_tree.GetPlayer(m_player);
    }

    void Update()
    {
        m_tree.Tree();
    }

    public override void Move()
    {
        Set();
        //m_anim.Play("Boss_Idle");
        if (Interval(0.5f))
        {
            Debug.Log("動く");
            
        }
    }

    void Set()
    {
        float posX = Random.Range(m_setMinPos.position.x, m_setMaxPos.position.x);
        float posY = Random.Range(m_setMinPos.position.y, m_setMaxPos.position.y);

        Vector2 set = new Vector2(posX, posY);

        transform.position = set;
    }

    bool Interval(float time)
    {
        m_intervalTime += Time.deltaTime;
        Debug.Log(m_intervalTime);

        if (m_intervalTime > time)
        {
            m_intervalTime = 0;
            m_interval = true;
        }
        else
        {
            m_interval = false;
        }

        return m_interval;
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void GetDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
