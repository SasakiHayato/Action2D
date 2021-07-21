using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    private enum ItemStatus
    { 
        Magic,
        Shield,

        StatusUp,
        Heel,
    }

    [SerializeField] private ItemStatus m_status;

    [System.NonSerialized] public Uicontroller m_ui;

    public PlayerDataClass m_playerData;

    void Start()
    {
        m_ui = FindObjectOfType<Uicontroller>();
    }

    public void CheckEnum()
    {
        if (m_status == ItemStatus.Magic)
        {
            m_playerData.m_subAttack = 2;
            m_ui.m_setSprite = m_ui.m_magicSprite;
        }
        else if (m_status == ItemStatus.Shield)
        {
            m_playerData.m_subAttack = 1;
            m_ui.m_setSprite = m_ui.m_shieldSprite;
        }
        else if (m_status == ItemStatus.StatusUp)
        {
            m_ui.m_slectCanvas.SetActive(true);
            m_ui.m_freeze = true;
            m_playerData.m_freeze = true;
        }
        else if (m_status == ItemStatus.Heel)
        {
            if (m_playerData.m_Hp < m_playerData.m_maxHp)
            {
                m_playerData.m_Hp += 30;
                if (m_playerData.m_Hp > 100)
                {
                    m_playerData.m_Hp = 100;
                }
            }
        }
    }

    public void SetStatus(ref int set)
    {
        if (set == 0)
        {
            m_playerData.m_magicPower += 20;
            m_ui.m_magicPoint++;
        }
        else if (set == 1)
        {
            m_playerData.m_shieldPower += 20;
            m_ui.m_shieldPoint++;
        }
        else if (set == 2)
        {
            m_playerData.m_attackPower += 20;
            m_ui.m_attackPoint++;
        }
        set = 0;

        m_playerData.m_freeze = false;
        m_ui.m_freeze = false;

        m_ui.m_slectCanvas.SetActive(false);
    }
}
