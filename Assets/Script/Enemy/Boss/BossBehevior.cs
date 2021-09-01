using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionEnum
{
    True,
    False,
}

public class BossBehevior : MonoBehaviour
{
    [SerializeField] BossClass m_boss;

    private enum ConditionalEnum
    {
        True,
        False,
    }

    private enum ActionKind
    {
        Action1,
        Action2,
        Action3,
    }

    ActionEnum m_actionEnum = ActionEnum.False;
    ActionKind m_actionKind;

    bool m_action1 = false;
    bool m_action2 = false;
    bool m_action3 = false;

    public void Tree()
    {
        if (m_actionEnum == ActionEnum.True) return;

        ConditionalEnum conditional = Conditional();
        Action(conditional);
    }

    ConditionalEnum Conditional()
    {
        ConditionalEnum conditional = ConditionalEnum.False;
        
        if (m_boss.SetHp() * 75 >= m_boss.RetuneCrreantHp() && !m_action1)
        {
            conditional = ConditionalEnum.True;
            m_actionKind = ActionKind.Action1;
            m_action1 = true;
        }
        else if (m_boss.SetHp() * 50 >= m_boss.RetuneCrreantHp() && !m_action2)
        {
            conditional = ConditionalEnum.True;
            m_actionKind = ActionKind.Action2;
            m_action2 = true;
        }
        else if (m_boss.SetHp() * 30 >= m_boss.RetuneCrreantHp() && !m_action3)
        {
            conditional = ConditionalEnum.True;
            m_actionKind = ActionKind.Action3;
            m_action3 = true;
        }

        return conditional;
    }

    void Action(ConditionalEnum conditional)
    {
        if (conditional == ConditionalEnum.True) { SetAction(); }
        else { Debug.Log("なし"); }
    }

    void SetAction()
    {
        m_actionEnum = ActionEnum.True;
        m_boss.SetPos();

        if (m_actionKind == ActionKind.Action1)
        {
            m_boss.SpecialAttack1();
        }
        else if (m_actionKind == ActionKind.Action2)
        {
            m_boss.SpecialAttack2();
        }
        else if (m_actionKind == ActionKind.Action3)
        {
            m_boss.SpecialAttack3();
        }
    }

    public ActionEnum CrreantEnum() => m_actionEnum;
    public void SetActionFalse(float time) => StartCoroutine(WaitTime(time));
    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        m_actionEnum = ActionEnum.False;
    }
}
