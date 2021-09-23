using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private enum RoomType
    {
        Dunguon,
        Boss,
        MidWay,
        Start,
    }

    [SerializeField] RoomType m_type;
    GameUiClass m_gameUi;

    void Start()
    {
        if (m_gameUi == null) m_gameUi = FindObjectOfType<GameUiClass>();
        if (m_type == RoomType.Dunguon)
            m_gameUi.TextObjectActive(true, TextManager.TextType.StartText);
        else if (m_type == RoomType.Boss)
            m_gameUi.TextObjectActive(true, TextManager.TextType.BossText);
        else if (m_type == RoomType.MidWay)
            m_gameUi.TextObjectActive(true, TextManager.TextType.MidWayText);
        else if (m_type == RoomType.Start)
            m_gameUi.TextObjectActive(true, TextManager.TextType.GameStartText);
    }
}
