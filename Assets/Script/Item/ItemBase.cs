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

    void Start()
    {
        m_ui = FindObjectOfType<Uicontroller>();
    }

    public void CheckEnum()
    {
        if (m_status == ItemStatus.Magic)
        {
            PlayerDataClass.Instance.m_subAttack = 2;
            m_ui.m_setSprite = m_ui.m_magicSprite;
        }
        else if (m_status == ItemStatus.Shield)
        {
            PlayerDataClass.Instance.m_subAttack = 1;
            m_ui.m_setSprite = m_ui.m_shieldSprite;
        }
        else if (m_status == ItemStatus.StatusUp)
        {
            m_ui.m_slectCanvas.SetActive(true);
            m_ui.m_freeze = true;
            PlayerDataClass.Instance.m_freeze = true;
        }
        else if (m_status == ItemStatus.Heel)
        {
            if (PlayerDataClass.Instance.m_Hp < PlayerDataClass.Instance.m_maxHp)
            {
                PlayerDataClass.Instance.m_Hp += 30;
                if (PlayerDataClass.Instance.m_Hp > 100)
                {
                    PlayerDataClass.Instance.m_Hp = 100;
                }
            }
        }
    }

    public void SetStatus(ref int set)
    {
        if (set == 0)
        {
            PlayerDataClass.Instance.m_magicPower ++;
        }
        else if (set == 1)
        {
            PlayerDataClass.Instance.m_shieldPower ++;
        }
        else if (set == 2)
        {
            PlayerDataClass.Instance.m_attackPower ++;
        }
        set = 0;

        PlayerDataClass.Instance.m_freeze = false;
        m_ui.m_freeze = false;

        m_ui.m_slectCanvas.SetActive(false);
    }

    public Vector2 SetTextPos(Vector2 vector, Transform player)
    {
        if (player.position.x < transform.position.x)
        {
            vector = new Vector2(transform.position.x + 1, transform.position.y + 1);
        }
        else
        {
            vector = new Vector2(transform.position.x - 1, transform.position.y + 1);
        }

        return vector;
    }
}
