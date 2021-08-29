using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int m_itemSeve { get; set; }
    public Animator m_animator { get; set; }
    public bool m_shield { get; set; }

    public void ItemCheck(int item)
    {
        if (m_itemSeve == 0)
        {
            m_itemSeve = item;
            Debug.Log(m_itemSeve);
        }
        else
        {
            Debug.Log("アイテムあり");
        }
    }

    //攻撃中に入力をうけつけない
    //public bool Freeze()
    //{
    //    if (PlayerDataClass.Instance.m_freeze)
    //    {
    //        PlayerDataClass.Instance.m_freeze = false;
    //    }
    //    else
    //    {
    //        PlayerDataClass.Instance.m_freeze = true;
    //    }

    //    return PlayerDataClass.Instance.m_freeze;
    //}

}
