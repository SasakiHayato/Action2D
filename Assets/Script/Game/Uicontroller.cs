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

    [SerializeField] private Image m_image;
    static Sprite m_set;

    private Slider m_slider;

    public GameObject m_slectCanvas { get; set; }

    void Start()
    {
        m_slider = GameObject.Find("Slider").GetComponent<Slider>();
        
        m_slectCanvas = GameObject.Find("SelectCanvas");
        m_slectCanvas.SetActive(false);

        if (m_set != null) SetSprite(m_set);
    }

    void Update()
    {
        SliderHp();
        Timer();
        StatuUp();
    }

    private void SliderHp()
    {
        m_slider.value = PlayerDataClass.Instance.SetHp();
        hpText.text = PlayerDataClass.Instance.SetHp().ToString("00" + "/１００");
    }

    private void Timer() => timeText.text = GameManager.getInstance().SetTime().ToString("0s");

    private void StatuUp()
    {
        m_magicText.text = PlayerDataClass.Instance.m_magicPower.ToString();
        m_attackText.text = PlayerDataClass.Instance.SetAttack().ToString();
        m_shieldText.text = PlayerDataClass.Instance.m_shieldPower.ToString();
    }

    public void SetSprite(Sprite set)
    {
        m_set = set;
        m_image.sprite = m_set;
    }
}
