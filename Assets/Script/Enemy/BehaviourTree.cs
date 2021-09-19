using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SetActionType
{
    NoamalAttack1,
    NoamalAttack2,

    SpAttack1,
    SpAttack2,
    SpAttack3,

    Move1,
}

public class BehaviourTree : MonoBehaviour
{
    private enum Running
    {
        True,
        False,
    }
    Running m_running = Running.False;

    [SerializeField] ConditionalNode m_conditionalSets;
    bool m_firstBool = false;

    public void Repeter(EnemyBase enemyBase, string thisName)
    {
        if (m_running == Running.True) return;
        else m_running = Running.True;
        
        SelectorNode selector = new SelectorNode();
        SequenceNode sequence = new SequenceNode();

        m_conditionalSets.GetName = thisName;
        m_conditionalSets.GetHp = enemyBase.RetuneCrreantHp();
        
        if (!m_firstBool)
        {
            m_firstBool = true;
            m_conditionalSets.GetMaxHp = enemyBase.GetMaxHp();
        }

        selector.Select(m_conditionalSets);
        sequence.Sequence(selector.Bool, m_conditionalSets, enemyBase);
    }

    public void IntervalSetFalse(float time) => StartCoroutine(Interval(time));
    IEnumerator Interval(float time)
    {
        yield return new WaitForSeconds(time);
        m_running = Running.False;
    }
}

// 行動
class ActionNode
{
    public void Action(ConditionalNode cn, EnemyBase eb)
    {
        if (cn.Execution1)
        {
            cn.Execution1 = false;
            eb.Attack(SetActionType.SpAttack1);
        }
        else if (cn.Execution2)
        {
            cn.Execution2 = false;
            eb.Attack(SetActionType.SpAttack2);
        }
        else if (cn.Execution3)
        {
            cn.Execution3 = false;
            eb.Attack(SetActionType.SpAttack3);
        }
        else
        {
            if (cn.Attack1) eb.SetAttack(0.5f, SetActionType.NoamalAttack1);
            else if (cn.Attack2) eb.SetAttack(0.5f, SetActionType.NoamalAttack2);
        }
    }

    public void Move(EnemyBase eb)
    {
        eb.Move(SetActionType.Move1);
    }
}

// 条件判定
[System.Serializable]
class ConditionalNode
{
    [SerializeField] bool m_isSpecial;

    [SerializeField] Vector2 m_findMinVec = Vector2.zero;
    [SerializeField] Vector2 m_findMaxVec = Vector2.zero;

    public string GetName { get; set; }
    public int GetHp { get; set; }
    public float GetMaxHp { get; set; }

    float m_hpPercent;

    public bool Attack1 { get; private set; }
    public bool Attack2 { get; private set; }

    bool m_phase1 = false;
    public bool Execution1 { get; set; }

    bool m_phase2 = false;
    public bool Execution2 { get; set; }

    bool m_phase3 = false;
    public bool Execution3 { get; set; }

    public void Condition(ConditionalNode conditional, SelectorNode selector)
    {
        conditional.Attack1 = false;
        conditional.Attack2 = false;
        SelectorNode.Result result = SelectorNode.Result.False;

        
        CrreantHpCheck(conditional, ref result);
        PlayerCheck(conditional, ref result);
        selector.Bool = result;
    }

    void PlayerCheck(ConditionalNode conditional,ref SelectorNode.Result result)
    {
        Vector2 playerVec = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 thisEnemyVec = GameObject.Find(conditional.GetName).transform.position;

        float absX = Mathf.Abs(playerVec.x - thisEnemyVec.x);
        float absY = Mathf.Abs(playerVec.y - thisEnemyVec.y);

        float findMinVecX = conditional.m_findMinVec.x;
        float findMinVecY = conditional.m_findMinVec.y;
        float findMaxVecX = conditional.m_findMaxVec.x;
        float findMaxVecY = conditional.m_findMaxVec.y;

        if (findMinVecX >= absX && findMinVecY >= absY)
        {
            result = SelectorNode.Result.True;
            conditional.Attack1 = true;
            return;
        }
        else if (findMinVecX < absX && findMaxVecX >= absX && findMaxVecY >= absY)
        {
            result = SelectorNode.Result.True;
            conditional.Attack2 = true;
            return;
        }
    }
    void CrreantHpCheck(ConditionalNode conditional, ref SelectorNode.Result result)
    {
        conditional.m_hpPercent = conditional.GetMaxHp / 100;
        float percent = conditional.m_hpPercent;

        if (!conditional.m_isSpecial) return;
        if (percent * 75 >= conditional.GetHp && !conditional.m_phase1)
        {
            conditional.m_phase1 = true;
            conditional.Execution1 = true;
            result = SelectorNode.Result.True;
            return;
        }
        else if (percent * 50 >= conditional.GetHp && !conditional.m_phase2)
        {
            conditional.m_phase2 = true;
            conditional.Execution2 = true;
            result = SelectorNode.Result.True;
            return;
        }
        else if (percent * 25 >= conditional.GetHp && !conditional.m_phase3)
        {
            conditional.m_phase3 = true;
            conditional.Execution3 = true;
            result = SelectorNode.Result.True;
            return;
        }
    }
}

// ORに相当するNode
class SelectorNode
{
    public enum Result
    {
        True,
        False,
    }
    Result m_result = Result.False;

    public Result Bool 
    { 
        get => m_result; 
        set { m_result = value; }
    }

    public void Select(ConditionalNode cn)
    {
        ConditionalNode conditional = new ConditionalNode();
        conditional.Condition(cn, this);
    }
}

// ANDに相当するNode
class SequenceNode
{
    public enum Result
    {
        True,
        False,
    }
    Result m_result;
    public Result Bool
    {
        get => m_result;
        set
        {
            m_result = value;
        }
    }

    public void Sequence(SelectorNode.Result rslt, ConditionalNode cn, EnemyBase eb)
    {
        Bool = Result.True;
        ActionNode action = new ActionNode();
        
        if (rslt == SelectorNode.Result.True) action.Action(cn, eb);
        else action.Move(eb);
    }
}
