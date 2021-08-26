using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviorTree : MonoBehaviour
{
    [SerializeField] float m_attack1Pos;
    [SerializeField] float m_attack2pos;

    [SerializeField] NewEnemyBase m_enemy;

    private enum ConditionalEnum
    {
        True,
        False,
    }

    private enum MovementEnum
    {
        Attack1,
        Attack2,
    }

    private enum ActionEnum
    {
        True,
        False,
    }

    MovementEnum m_movement;
    ActionEnum m_action = ActionEnum.False;

    public void Tree()
    {
        if (m_action == ActionEnum.True) return;
        Debug.Log("aq");
        ConditionalEnum conditional = Conditional();
        Action(conditional);
    }

    ConditionalEnum Conditional()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_action = ActionEnum.True;

        ConditionalEnum conditional;

        Vector2 playerVec = player.transform.position;

        float setVec = playerVec.x - transform.position.x;
        float posXAbs = Mathf.Abs(setVec);
        
        if (m_attack1Pos >= posXAbs)
        {
            conditional = ConditionalEnum.True;
            m_movement = MovementEnum.Attack1;
        }
        else if (m_attack1Pos < posXAbs && m_attack2pos > posXAbs)
        {
            conditional = ConditionalEnum.True;
            m_movement = MovementEnum.Attack2;
        }
        else { conditional = ConditionalEnum.False; }

        return conditional;
    }

    void Action(ConditionalEnum conditional)
    {
        if (conditional == ConditionalEnum.True) { Attack(); }
        else 
        {
            SetFalseToAction();
            m_enemy.Move();
        }
    }

    void Attack()
    {
        if (m_movement == MovementEnum.Attack1) { m_enemy.Attack1(); }
        else { m_enemy.Attack2(); }
    }

    public void SetFalseToAction() { m_action = ActionEnum.False; }
}
