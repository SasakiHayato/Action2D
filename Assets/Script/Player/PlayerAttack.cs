using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    int m_attackCombo = 0;
    int m_seveAttackId = 0;
    [SerializeField] AttackClass m_attack;

    public void Attack(Animator anim, ItemDataBase dataBase, int id, int attackId)
    {
        if (m_seveAttackId != attackId)
        {
            m_seveAttackId = attackId;
            m_attackCombo = 0;
        }

        m_attack.AttackPower = dataBase.GetItemId(id).GetAttackPower();
        if (dataBase.GetItemId(id).GetId() == 0)
        {
            m_attack.AttackPower *= PlayerDataClass.getInstance().SetAttack();
        }
        else if (dataBase.GetItemId(id).GetId() == 1)
        {
            m_attack.AttackPower *= PlayerDataClass.getInstance().SetMagic();
        }
        Debug.Log(m_attack.AttackPower);
        string name = dataBase.GetItemId(id).GetAnimName(m_attackCombo);
        anim.Play(name);

        if (dataBase.GetItemId(id).GetAnimLength() - 1 > m_attackCombo) m_attackCombo++;
        else m_attackCombo = 0;
    }
}
