using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    int m_attackCombo = 1;
    bool m_attackBool = false;
    bool m_shieldBool = false;

    GameObject[] m_attackCollider = new GameObject[3];
    GameObject m_shieldCollsion = default;

    public void Attack(Animator anim)
    {
        switch (m_attackCombo)
        {
            case 1:
                anim.Play("Player_Attack");
                m_attackCombo = 2;
                break;

            case 2:
                anim.Play("Player_Attack2");
                m_attackCombo = 3;
                break;

            case 3:
                anim.Play("Player_Attack3");
                m_attackCombo = 1;
                break;
        }
    }

    public void SubAttack(Animator anim, PlayerMove move,ref bool freeze)
    {
        switch (PlayerDataClass.Instance.m_subAttack)
        {
            case 1:
                if (!m_shieldBool)
                {
                    m_shieldBool = true;
                    m_shieldCollsion.SetActive(m_shieldBool);
                    anim.Play("Player_Shield");
                }
                else
                {
                    m_shieldBool = false;
                    m_shieldCollsion.SetActive(m_shieldBool);
                    freeze = false;
                }

                
                break;

            case 2:
                if (move.CrreantCrouch())
                {
                    anim.Play("Player_Magic_crouch");
                    break;
                }

                anim.Play("Player_Magic");
                break;
        }
    }

    public void SetCollision()
    {
        if (!m_attackBool)
        {
            m_attackBool = true;
            m_attackCollider[m_attackCombo - 1].SetActive(m_attackBool);
        }
        else
        {
            m_attackBool = false;
            m_attackCollider[m_attackCombo - 1].SetActive(m_attackBool);
        }
    }

    public void SetAttackObject(int get)
    {
        m_attackCollider[get] = transform.GetChild(get).gameObject;
        m_attackCollider[get].SetActive(m_attackBool);
    }

    public void SetShieldCollision() => m_shieldCollsion = transform.Find("ShieldCollider").gameObject;
}
