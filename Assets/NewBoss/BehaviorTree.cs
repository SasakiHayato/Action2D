using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    [SerializeField] string[] m_animNames = new string[0];
    [SerializeField] EnemyBase m_base;

    GameObject m_player;
    Animator m_anim;

    public Animator GetAnimator(GameObject get) { return m_anim = get.GetComponent<Animator>(); }
    public GameObject GetPlayer(GameObject get) { return m_player = get; }

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

    // PlayerPos 判定
    ConditionalEnum Conditional()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_action = ActionEnum.True;

        ConditionalEnum conditional = ConditionalEnum.False;

        Vector2 playerVec = player.transform.position;

        float setVec = playerVec.x - transform.position.x;
        float posXAbs = Mathf.Abs(setVec);
        
        if (4.5f >= posXAbs)
        {
            conditional = ConditionalEnum.True;
            m_movement = MovementEnum.Attack1;
        }
        else if (4.5f < posXAbs && 19.5f > posXAbs)
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
            m_base.Move();
        }

        StartCoroutine(SetActiveToFalse());
    }

    void Attack()
    {
        if (m_movement == MovementEnum.Attack1)
        {
            Debug.Log("Attack1");
            m_anim.Play(m_animNames[0]);
        }
        else 
        {
            Debug.Log("Attack2");
            m_anim.Play(m_animNames[1]);
        }
    }

    IEnumerator SetActiveToFalse()
    {
        yield return new WaitForSeconds(2);
        m_action = ActionEnum.False;
    }
}
