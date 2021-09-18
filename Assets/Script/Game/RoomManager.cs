using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    GameUiClass m_gameUi;

    void Start()
    {
        if (m_gameUi == null) m_gameUi = FindObjectOfType<GameUiClass>();
        m_gameUi.TextObjectActive(true, TextManager.TextType.StartText);
    }
}
