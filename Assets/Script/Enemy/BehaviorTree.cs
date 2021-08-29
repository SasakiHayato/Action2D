using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            m_movement = MovementEnum.Attack1;
        }
        else if (m_attack1PosX < posXAbs && m_attack2PosX > posXAbs && m_attack2PosMinY <= posYAbs && m_attack2PosMaxY > posYAbs)
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

    public void Interval(float time) { StartCoroutine(WaitTime(time)); }
    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        SetFalseToAction();
    }

    void SetFalseToAction() { m_action = ActionEnum.False; }
}
