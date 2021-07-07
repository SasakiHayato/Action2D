using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uicontroller : MonoBehaviour
{
    [SerializeField] Text hpText;
    [SerializeField] Text timeText;

    Slider m_slider;

    PlayerContoller player;

    void Start()
    {
        m_slider = GameObject.Find("Slider").GetComponent<Slider>();
        player = FindObjectOfType<PlayerContoller>();
    }

    void Update()
    {
        SliderHp();
        Timer();
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
}
