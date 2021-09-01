using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SetAttackStatus
{ 
    NormalAttack1,
    NormalAttack2,

    SpAttack1,
    SpAttack2,
    SpAttack3,
}


public class BehaviorTree : MonoBehaviour
{
    [SerializeField] float m_attack1PosX;
    [SerializeField] float m_attack2PosX;

    [SerializeField] float m_attack1PosMinY;
    [SerializeField] float m_attack1PosMaxY;

    [SerializeField] float m_attack2PosMinY;
    [SerializeField] float m_attack2PosMaxY;

    [SerializeField] NewEnemyBase m_enemy;

    private enum ConditionalEnum
    {
        True,
        False,
    }

    private enum ActionEnum
    {
        True,
        False,
    }

    private enum Distance
    {
        Near,
        Far,
    }

    private enum RemainingHp
    {
        Remaining75,
        Remaining50,
        Remaining30,

        False,
    }

    Distance m_distance;
    ActionEnum m_action = ActionEnum.False;

    bool m_phase1 = false;
    bool m_phase2 = false;
    bool m_phase3 = false;


    public void Tree()
    {
        RemainingHp remaining = RemainingHp.False;
        RemainingHp set = Remaining(ref remaining);
        if (set !=  RemainingHp.False)
        {
            m_action = ActionEnum.True;
            Attack(remaining);
        }
        else
        {
            Debug.Log("条件なし");
        }
        if (m_action == ActionEnum.True) return;
        
        ConditionalEnum conditional = Conditional();
        Action(conditional);
    }

    ConditionalEnum Conditional()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_action = ActionEnum.True;

        ConditionalEnum conditional;

        Vector2 playerVec = player.transform.position;

        float setVecX = playerVec.x - transform.position.x;
        float posXAbs = Mathf.Abs(setVecX);
        
        float setVecY = playerVec.y - transform.position.y;
        float posYAbs = Mathf.Abs(setVecY);

        if (m_attack1PosX >= posXAbs && m_attack1PosMinY <= posYAbs && m_attack1PosMaxY > posYAbs)
        {
            conditional = ConditionalEnum.True;
            m_distance = Distance.Near;
        }
        else if (m_attack1PosX < posXAbs && m_attack2PosX > posXAbs && m_attack2PosMinY <= posYAbs && m_attack2PosMaxY > posYAbs)
        {
            conditional = ConditionalEnum.True;
            m_distance = Distance.Far;
        }
        else { conditional = ConditionalEnum.False; }

        return conditional;
    }

    void Action(ConditionalEnum conditional)
    {
        RemainingHp remaining = RemainingHp.False;
        if (conditional == ConditionalEnum.True) 
        {
            Attack(remaining);
        }
        else 
        {
            m_enemy.Move();
            SetFalseToAction();
        }
    }

    RemainingHp Remaining(ref RemainingHp set)
    {
        if ((m_enemy.MaxHp / 100) * 75 >= m_enemy.RetuneCrreantHp() && !m_phase1)
        {
            m_phase1 = true;
            set = RemainingHp.Remaining75;
        }
        if ((m_enemy.MaxHp / 100) * 50 >= m_enemy.RetuneCrreantHp() && !m_phase2)
        {
            m_phase2 = true;
            set = RemainingHp.Remaining50;
        }
        if ((m_enemy.MaxHp / 100) * 30 >= m_enemy.RetuneCrreantHp() && !m_phase3)
        {
            m_phase3 = true;
            set = RemainingHp.Remaining30;
        }
        //else set = RemainingHp.False;

        return set;
    }

    void Attack(RemainingHp set)
    {
        if (set == RemainingHp.False)
        {
            if (m_distance == Distance.Near) { m_enemy.SetAttack = SetAttackStatus.NormalAttack1; }
            if (m_distance == Distance.Far) { m_enemy.SetAttack = SetAttackStatus.NormalAttack2; }
        }
        else
        {
            if (set == RemainingHp.Remaining75) { m_enemy.SetAttack = SetAttackStatus.SpAttack1; }
            if (set == RemainingHp.Remaining50) { m_enemy.SetAttack = SetAttackStatus.SpAttack2; }
            if (set == RemainingHp.Remaining30) { m_enemy.SetAttack = SetAttackStatus.SpAttack3; }
        }
        
        m_enemy.Attack();
    }

    public void Interval(float time) { StartCoroutine(WaitTime(time)); }
    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        SetFalseToAction();
    }
    void SetFalseToAction() { m_action = ActionEnum.False; }
}
