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

    Slider m_slider;

    PlayerContoller player;

    [System.NonSerialized] public int m_magicPoint = 1;
    [System.NonSerialized] public int m_attackPoint = 1;

    void Start()
    {
        m_slider = GameObject.Find("Slider").GetComponent<Slider>();
        player = FindObjectOfType<PlayerContoller>();

       
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

    private float seconds = 0;
    private void Timer()
    {
        seconds += Time.deltaTime;
        
        timeText.text = seconds.ToString("0s");
    }

    private void StatuUp()
    {
        m_magicText.text = m_magicPoint.ToString();
        m_attackText.text = m_attackPoint.ToString();
    }
}
