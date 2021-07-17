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

    [System.NonSerialized] public PlayerContoller m_player;
    [System.NonSerialized] public Uicontroller m_ui;

    void Start()
    {
        m_player = FindObjectOfType<PlayerContoller>();
        m_ui = FindObjectOfType<Uicontroller>();
    }

    public void CheckEnum()
    {
        if (m_status == ItemStatus.Magic)
        {
            m_player.m_subAttack = 2;
            m_ui.m_setSprite = m_ui.m_magicSprite;
        }
        else if (m_status == ItemStatus.Shield)
        {
            m_player.m_subAttack = 1;
            m_ui.m_setSprite = m_ui.m_shieldSprite;
        }
        else if (m_status == ItemStatus.StatusUp)
        {
            m_ui.m_slectCanvas.SetActive(true);
            m_ui.m_freeze = true;
            m_player.m_freeze = true;
        }
        else if (m_status == ItemStatus.Heel)
        {
            if (m_player.m_Hp < m_player.m_maxHp)
            {
                m_player.m_Hp += 30;
                if (m_player.m_Hp > 100)
                {
                    m_player.m_Hp = 100;
                }
            }
        }
    }

    public void SetStatus(ref int set)
    {
        if (set == 0)
        {
            m_player.m_magicPower += 20;
            m_ui.m_magicPoint++;
        }
        else if (set == 1)
        {
            m_player.m_shieldPower += 20;
            m_ui.m_shieldPoint++;
        }
        else if (set == 2)
        {
            m_player.m_attackPower += 20;
            m_ui.m_attackPoint++;
        }
        set = 0;

        m_player.m_freeze = false;
        m_ui.m_freeze = false;

        m_ui.m_slectCanvas.SetActive(false);
    }
}
