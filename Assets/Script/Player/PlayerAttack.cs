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

    public void AttackFirst(Animator anim, AttackItemDataBase dataBase)
    {
        if (PlayerDataClass.Instance.SetAttackIdFirst == 1)
        {
            //int id = PlayerDataClass.Instance.SetAttackIdFirst;
            //string set = dataBase.GetItemId(id - 1).GetAnimName(m_attackCombo);
            //anim.Play(set);
            //m_attackCombo++;

            //if (m_attackCombo >= dataBase.GetItemId(id - 1).GetAnimLength())
            //{
            //    m_attackCombo = 0;
            //}
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
        
    }

    public void AttackSecond(Animator anim, PlayerMove move,ref bool freeze)
    {
        switch (PlayerDataClass.Instance.SetAttackIdSecond)
        {
            case 3:
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
    public void SetAttackObject(int get)
    {
        m_attackCollider[get] = transform.GetChild(get).gameObject;
        m_attackCollider[get].SetActive(m_attackBool);
    }

    public void SetShieldCollision() => m_shieldCollsion = transform.Find("ShieldCollider").gameObject;
}
