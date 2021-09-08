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

    [SerializeField] private Image m_subImage;
    static Sprite m_subSet;

    [SerializeField] private Image m_setFire1;
    [SerializeField] private Image m_setFire2;
    [SerializeField] private Image m_setFire3;

    private Slider m_slider;

    GameObject m_selectCanvas;
    bool m_canvasActive = false;
    GameObject m_setCanvas;

    void Start()
    {
        m_slider = GameObject.Find("Slider").GetComponent<Slider>();
        
        m_selectCanvas = GameObject.Find("SelectCanvas");
        m_selectCanvas.SetActive(m_canvasActive);

        m_setCanvas = GameObject.Find("ItemSetCanvas");
        m_setCanvas.SetActive(false);

        PlayerDataClass.Instance.SetIdBoolFirst = false;
        PlayerDataClass.Instance.SetIdBoolSecond = false;
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
        m_slider.value = PlayerDataClass.Instance.SetHp();
        hpText.text = PlayerDataClass.Instance.SetHp().ToString("00" + "/１００");
    }

    private void Timer()
    {
        if (!GameManager.getInstance().IsDungeon())
        {
            timeText.text = GameManager.getInstance().CrreantTime().ToString("0s");
        }
        else timeText.text = GameManager.getInstance().SetTime().ToString("0s");
    }

        private void StatuUp()
    {
        m_magicText.text = PlayerDataClass.Instance.SetMagic().ToString();
        m_attackText.text = PlayerDataClass.Instance.SetAttack().ToString();
        m_shieldText.text = PlayerDataClass.Instance.SetShield().ToString();
    }

    public void SetSprite(Sprite set)
    {
        if (!PlayerDataClass.Instance.SetIdBoolFirst)
        {
            PlayerDataClass.Instance.SetIdBoolFirst = true;
            m_set = set;
            m_image.sprite = m_set;
            m_setFire1.sprite = m_set;
        }
        else if (!PlayerDataClass.Instance.SetIdBoolSecond)
        {
            PlayerDataClass.Instance.SetIdBoolSecond = true;
            m_subSet = set;
            m_subImage.sprite = m_subSet;
            m_setFire2.sprite = m_subSet;
        }
    }

    public void SetCanvasActive(Sprite set)
    {
        m_setFire3.sprite = set;
        m_setCanvas.SetActive(true);
    }
    public void SetSelectCanvasActive()
    {
        if (!m_canvasActive) m_canvasActive = true;
        else m_canvasActive = false;

        m_selectCanvas.SetActive(m_canvasActive);
    }

    public void SetCanvasFalse() => m_setCanvas.SetActive(false);
}
