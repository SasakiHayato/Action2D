using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUiClass : MonoBehaviour
{
    [SerializeField] Text hpText;
    [SerializeField] Text timeText;

    [SerializeField] Text m_magicText;
    [SerializeField] Text m_attackText;
    [SerializeField] Text m_shieldText;

    [SerializeField] private Image m_image;
    static Sprite m_set;

    [SerializeField] private Image m_subImage;
    static Sprite m_subSet;

    [SerializeField] private Image m_setFire1;
    [SerializeField] private Image m_setFire2;

    GameUiClass m_gameUi;

    private Slider m_slider;

    void Start()
    {
        m_gameUi = FindObjectOfType<GameUiClass>();
        m_slider = GameObject.Find("Slider").GetComponent<Slider>();

        PlayerDataClass.getInstance().SetIdBoolFirst = false;
        PlayerDataClass.getInstance().SetIdBoolSecond = false;
        if (m_set != null) SetSprite(m_set);
        if (m_subSet != null) SetSprite(m_subSet);
    }

    void Update()
    {
        SliderHp();
        Timer();
        StatuUp();
    }

    private void SliderHp()
    {
        m_slider.value = PlayerDataClass.getInstance().SetHp();
        hpText.text = PlayerDataClass.getInstance().SetHp().ToString("00" + "/１００");
    }

    private void Timer()
    {
        if (!GameManager.Instance.IsDungeon())
        {
            timeText.text = GameManager.Instance.CrreantTime().ToString("0s");
        }
        else timeText.text = GameManager.Instance.SetTime().ToString("0s");
    }

        private void StatuUp()
    {
        m_magicText.text = PlayerDataClass.getInstance().SetMagic().ToString();
        m_attackText.text = PlayerDataClass.getInstance().SetAttack().ToString();
        m_shieldText.text = PlayerDataClass.getInstance().SetShield().ToString();
    }

    public void SetSprite(Sprite set)
    {
        if (!PlayerDataClass.getInstance().SetIdBoolFirst)
        {
            PlayerDataClass.getInstance().SetIdBoolFirst = true;
            m_set = set;
            m_image.sprite = m_set;
            m_gameUi.Fire1.sprite = m_set;
        }
        else if (!PlayerDataClass.getInstance().SetIdBoolSecond)
        {
            PlayerDataClass.getInstance().SetIdBoolSecond = true;
            m_subSet = set;
            m_subImage.sprite = m_subSet;
            m_gameUi.Fire2.sprite = m_subSet;
        }
    }
}
