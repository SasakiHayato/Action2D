using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonClass : MonoBehaviour
{
    //[SerializeField] UnityEvent[] m_events = new UnityEvent[0];

    public void OnClick()
    {
        GameManager.getInstance().SetCrreantPlay(true);
        GameManager.getInstance().SetScene("Start");
    }
}
