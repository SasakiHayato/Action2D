using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uicontroller : MonoBehaviour
{
    [SerializeField] Text hpText;
    [SerializeField] Text timeText;

    [SerializeField] Text m_magicText;
    [SerializeField] Text m_attackText;
    [SerializeField] Text m_shieldText;

    Slider m_slider;
    [System.NonSerialized] public GameObject m_slectCanvas;
    PlayerContoller player;

    [System.NonSerialized] public int m_magicPoint = 1;
    [System.NonSerialized] public int m_attackPoint = 1;
    [System.NonSerialized] public int m_shieldPoint = 1;

    private float m_seconds = 0;

    [System.NonSerialized] public bool m_freeze = false;

    void Start()
    {
        m_slider = GameObject.Find("Slider").GetComponent<Slider>();
        player = FindObjectOfType<PlayerContoller>();

        m_slectCanvas = GameObject.Find("SelectCanvas");
        m_slectCanvas.SetActive(false);
    }

    void Update()
    {
        SliderHp();
        Timer();
        StatuUp();
        
    }

    private void SliderHp()
    {
        m_slider.value = player.m_Hp;
        hpText.text = player.m_Hp.ToString("00" + "/１００");
    }

    private void Timer()
    {
        if (m_freeze) return;
        m_seconds += Time.deltaTime;
        
        timeText.text = m_seconds.ToString("0s");
    }

    private void StatuUp()
    {
        m_magicText.text = m_magicPoint.ToString();
        m_attackText.text = m_attackPoint.ToString();
        m_shieldText.text = m_shieldPoint.ToString();
    }
}
