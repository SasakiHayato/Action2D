using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    int m_attackCombo = 0;
    int m_seveAttackId = 0;
   

    public void Attack(Animator anim, ItemDataBase dataBase, int id, int attackId)
    {
        if (m_seveAttackId != attackId)
        {
            m_seveAttackId = attackId;
            m_attackCombo = 0;
        }

        string name = dataBase.GetItemId(id).GetAnimName(m_attackCombo);
        anim.Play(name);

        if (dataBase.GetItemId(id).GetAnimLength() - 1 > m_attackCombo) m_attackCombo++;
        else m_attackCombo = 0;
    }

    //public void SetShieldCollision() => m_shieldCollsion = transform.Find("ShieldCollider").gameObject;
}
