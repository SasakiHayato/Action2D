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

    [SerializeField] public Sprite m_magicSprite;
    [SerializeField] public Sprite m_shieldSprite;

    [SerializeField] private Image m_image;

    [System.NonSerialized] public Sprite m_setSprite = null;

    private Slider m_slider;

    public GameObject m_slectCanvas { get; set; }

    void Start()
    {
        m_slider = GameObject.Find("Slider").GetComponent<Slider>();
        
        m_slectCanvas = GameObject.Find("SelectCanvas");
        m_slectCanvas.SetActive(false);
    }

    void Update()
    {
        SliderHp();
        Timer();
        StatuUp();
        SetSprite();
    }

    private void SliderHp()
    {
        m_slider.value = PlayerDataClass.Instance.SetHp();
        hpText.text = PlayerDataClass.Instance.SetHp().ToString("00" + "/１００");
    }

    private void Timer()
    {   
        timeText.text = GameManager.Instance.GameTime() .ToString("0s");
    }

    private void StatuUp()
    {
        m_magicText.text = PlayerDataClass.Instance.m_magicPower.ToString();
        m_attackText.text = PlayerDataClass.Instance.SetAttack().ToString();
        m_shieldText.text = PlayerDataClass.Instance.m_shieldPower.ToString();
    }

    private void SetSprite()
    {
        m_image.sprite = m_setSprite;
    }
}
