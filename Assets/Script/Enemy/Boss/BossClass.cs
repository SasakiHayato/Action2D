using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClass : NewEnemyBase, IDamage
{
    BehaviorTree m_tree;
    BossBehevior m_bossTree;
    NewBossBulletClass m_bulletClass;

    [SerializeField] Transform m_minPos;
    [SerializeField] Transform m_maxPos;
    [SerializeField] Transform m_bossAttackPos;

    float m_time;
    int m_hpPasent;
    
    Animator m_anim;

    void Start()
    {
        m_bulletClass = GetComponent<NewBossBulletClass>();
        m_tree = GetComponent<BehaviorTree>();
        m_bossTree = GetComponent<BossBehevior>();
        m_anim = GetComponent<Animator>();

        m_hpPasent = RetuneCrreantHp() / 100;
    }

    void Update()
    {
        if (m_bossTree.CrreantEnum() == ActionEnum.False) { m_tree.Tree(); }
        m_bossTree.Tree();
    }

    public void GetDamage(int damage)
    {
        int hp = RetuneCrreantHp() - damage;
        SetHp(hp, gameObject);
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
    
    public override void Attack()
    {
        if (GetStatus() == SetAttackStatus.NormalAttack1)
        {
            m_anim.Play("Boss_Attack_2");
            m_bulletClass.SetEnum(BulletKind.Diamond);
            m_bulletClass.SetPosToDiamond();
            m_tree.Interval(8);
        }
        else if (GetStatus() == SetAttackStatus.NormalAttack2)
        {
            m_anim.Play("Boss_Attack_1");

            m_bulletClass.SetEnum(BulletKind.Slash);

            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            m_bulletClass.SetDir(gameObject.transform, player.position.x, player.position.y, 1);

            m_tree.Interval(5);
        }
    }
    int m_count = 0;
    public void SpecialAttack1()
    {
        StartCoroutine(Set(0));
    }
    IEnumerator Set(int set)
    {
        m_anim.Play("Boss_Attack_1");
        for (int angle = set; angle < 360; angle += 45)
        {
            float rad = angle * Mathf.Deg2Rad;
            m_bulletClass.SetDir(gameObject.transform, Mathf.Sin(rad), Mathf.Cos(rad), 3);
        }
        yield return new WaitForSeconds(4f);
        if (m_count < 2)
        {
            m_count++;
            StartCoroutine(Set(m_count * 15));
        }
        else
        {
            m_bossTree.SetActionFalse(0);
        }
    }
    public void SpecialAttack2()
    {
        Debug.Log("b");
    }
    public void SpecialAttack3()
    {
        Debug.Log("c");
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

    public float SetHp() => m_hpPasent;
    public void SetPos() => transform.position = m_bossAttackPos.position;
}
