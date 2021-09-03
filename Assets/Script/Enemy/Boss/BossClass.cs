using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossClass : NewEnemyBase, IDamage
{
    BehaviorTree m_tree;
    
    NewBossBulletClass m_bulletClass;
    [SerializeField] BossRoomManager m_roomManager;

    [SerializeField] Transform m_minPos;
    [SerializeField] Transform m_maxPos;
    [SerializeField] Transform m_bossAttackPos;
    [SerializeField] Transform[] m_phase3Pos = new Transform[0];

    [SerializeField] Slider m_hpSlider;

    float m_time;
    int m_hpPasent;
    int m_count = 0;

    Animator m_anim;

    void Start()
    {
        m_bulletClass = GetComponent<NewBossBulletClass>();
        m_tree = GetComponent<BehaviorTree>();
     
        m_anim = GetComponent<Animator>();
        Debug.Log(GetMaxHp());
        m_hpSlider.value = RetuneCrreantHp();
        m_hpPasent = RetuneCrreantHp() / 100;
    }

    void Update()
    {
        m_tree.Tree();
    }

    public void GetDamage(int damage)
    {
        int hp = RetuneCrreantHp() - damage;
        SetHp(hp, gameObject);
        m_hpSlider.value = RetuneCrreantHp();
        if (RetuneCrreantHp() <= 0)
        {
            GameManager.getInstance().SetScene("Start");
        }
    }
    public override void Move()
    {
        LookToPlayer();
        
        if (!Interval(2)) return;
        float posX = Random.Range(m_minPos.position.x, m_maxPos.position.x);
        float posY = Random.Range(m_minPos.position.y, m_maxPos.position.y);

        transform.position = new Vector2(posX, posY);

        m_tree.Interval(0);
    }
    
    public override void Attack()
    {
        if (SetAttack == SetAttackStatus.NormalAttack1)
        {
            m_anim.Play("Boss_Attack_2");
            m_bulletClass.SetEnum(BulletKind.Diamond);
            m_bulletClass.SetPosToDiamond();
            m_tree.Interval(8);
        }
        else if (SetAttack == SetAttackStatus.NormalAttack2)
        {
            m_anim.Play("Boss_Attack_1");

            m_bulletClass.SetEnum(BulletKind.Slash);

            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            m_bulletClass.SetDir(gameObject.transform, player.position.x, player.position.y, 1);

            m_tree.Interval(5);
        }
        else if (SetAttack == SetAttackStatus.SpAttack1)
        {
            gameObject.transform.position = m_bossAttackPos.transform.position;
            StartCoroutine(SetSlash(0));
        }
        else if (SetAttack == SetAttackStatus.SpAttack2)
        {
            m_roomManager.SetEnemy();
        }
        else if (SetAttack == SetAttackStatus.SpAttack3)
        {
            StartCoroutine(SetBullet());
        }
    }
    

    IEnumerator SetSlash(int set)
    {
        m_anim.Play("Boss_Attack_1");
        for (int angle = set; angle < 360; angle += 45)
        {
            float rad = angle * Mathf.Deg2Rad;
            m_bulletClass.SetEnum(BulletKind.Slash);
            m_bulletClass.SetDir(gameObject.transform, Mathf.Sin(rad), Mathf.Cos(rad), 3);
        }
        yield return new WaitForSeconds(4f);
        if (m_count < 2)
        {
            m_count++;
            StartCoroutine(SetSlash(m_count * 15));
        }
        else
        {
            m_tree.Interval(0);
            m_count = 0;
        }
    }

    IEnumerator SetBullet()
    {
        int random = Random.Range(0, m_phase3Pos.Length);
        m_bulletClass.SetEnum(BulletKind.Slash);
        m_bulletClass.SetDir(m_phase3Pos[random], -1, 0, 5);
        yield return new WaitForSeconds(5f);
        if (m_count < 8)
        {
            m_count++;
            StartCoroutine(SetBullet());
        }
        else
        {
            m_tree.Interval(0);
            m_count = 0;
        }
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
