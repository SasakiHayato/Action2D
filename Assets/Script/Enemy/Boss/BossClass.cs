using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossClass : EnemyBase, IDamage
{
    [SerializeField] BehaviourTree m_newTree;
    
    NewBossBulletClass m_bulletClass;
    [SerializeField] BossRoomManager m_roomManager;

    [SerializeField] Transform m_minPos;
    [SerializeField] Transform m_maxPos;
    [SerializeField] Transform m_bossAttackPos;
    [SerializeField] Transform[] m_phase3Pos = new Transform[0];

    [SerializeField] Slider m_hpSlider;
    [SerializeField] GameObject m_slash;

    int m_count = 0;
    [SerializeField] int m_hp;

    Animator m_anim;

    void Start()
    {
        GetHp = m_hp;
        MaxHp = m_hp;
        m_bulletClass = GetComponent<NewBossBulletClass>();
     
        m_anim = GetComponent<Animator>();
        Debug.Log(GetMaxHp());
        m_hpSlider.maxValue = GetMaxHp();
        m_hpSlider.value = GetMaxHp();
        
    }

    void Update()
    {
        m_newTree.Repeter(this, this.name);
    }
    public override void Attack(SetActionType set)
    {
        if (set == SetActionType.NoamalAttack1)
        {
            m_anim.Play("Boss_Attack_2");
            m_bulletClass.SetEnum(BulletKind.Diamond);
            m_bulletClass.SetPosToDiamond();
            m_newTree.IntervalSetFalse(8);
        }
        else if (set == SetActionType.NoamalAttack2)
        {
            m_anim.Play("Boss_Attack_1");

            m_bulletClass.SetEnum(BulletKind.Slash);

            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            m_bulletClass.SetDir(gameObject.transform, player.position.x, player.position.y, 1);

            m_newTree.IntervalSetFalse(5);
        }
        else if (set == SetActionType.SpAttack1)
        {
            gameObject.transform.position = m_bossAttackPos.transform.position;
            StartCoroutine(SetSlash(0));
        }
        else if (set == SetActionType.SpAttack2)
        {
            m_roomManager.SetEnemy();
            m_newTree.IntervalSetFalse(0);
        }
        else if (set == SetActionType.SpAttack3)
        {
            m_roomManager.SetEnemy();
            StartCoroutine(SetBullet());
            m_newTree.IntervalSetFalse(0);
        }
    }
    public override void Move(SetActionType set)
    {
        if (set == SetActionType.Move1)
        {
            LookToPlayer();

            float posX = Random.Range(m_minPos.position.x, m_maxPos.position.x);
            float posY = Random.Range(m_minPos.position.y, m_maxPos.position.y);

            transform.position = new Vector2(posX, posY);

            m_newTree.IntervalSetFalse(3);
        }
    }
    public void GetDamage(int damage)
    {
        Debug.Log("aaa");
        int hp = RetuneCrreantHp() - damage;
        SetHp(hp, gameObject);
        m_hpSlider.value = RetuneCrreantHp();
        Debug.Log(hp);
        if (RetuneCrreantHp() <= 0)
        {
            GameManager.Instance.SetScene("Start");
        }
    }
    
    IEnumerator SetSlash(int set)
    {
        m_anim.Play("Boss_Attack_1");
        for (int angle = set; angle < 360; angle += 45)
        {
            float rad = angle * Mathf.Deg2Rad;
            m_bulletClass.SetEnum(BulletKind.Slash);
            m_bulletClass.IsSpcial = true;
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
            m_newTree.IntervalSetFalse(0);
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
            m_newTree.IntervalSetFalse(0);
            m_count = 0;
        }
    }

    void LookToPlayer()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (transform.position.x > player.position.x) { transform.localScale = new Vector2(-0.3f, 0.3f); }
        else { transform.localScale = new Vector2(0.3f, 0.3f); }
    }
    public void SetPos() => transform.position = m_bossAttackPos.position;
}
