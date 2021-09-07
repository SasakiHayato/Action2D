using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    private enum ItemStatus
    { 
        StatusUp,
        Heel,
    }

    [SerializeField] private ItemStatus m_status;

    public Uicontroller m_ui { get; set; }

    private void Awake()
    {
        m_ui = FindObjectOfType<Uicontroller>();
    }

    public void CheckEnum()
    {
        if (m_status == ItemStatus.StatusUp)
        {
            m_ui.m_slectCanvas.SetActive(true);
            PlayerDataClass.Instance.SetFreeze(true);
        }
        else if (m_status == ItemStatus.Heel)
        {
            if (PlayerDataClass.Instance.SetHp() < PlayerDataClass.Instance.m_maxHp)
            {
                int heel = PlayerDataClass.Instance.SetHp() + 30;
                PlayerDataClass.Instance.GetHp(heel);
                if (PlayerDataClass.Instance.SetHp() > 100)
                {
                    PlayerDataClass.Instance.GetHp(100);
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
            PlayerDataClass.Instance.AttackPowerUp(1);
        }
        set = 0;

        PlayerDataClass.Instance.SetFreeze(false);

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
