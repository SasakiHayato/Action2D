using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiClass : MonoBehaviour
{
    [SerializeField] GameObject m_textObject;
    [SerializeField] GameObject m_selectCanvas;
    [SerializeField] GameObject m_setCanvas;
    [SerializeField] GameObject m_gameOverCanvas;

    [SerializeField] Image m_setImage;
    [SerializeField] Image m_fire1;
    [SerializeField] Image m_fire2;

    public Image Fire1 { get => m_fire1; set { m_fire1 = value; } }
    public Image Fire2 { get => m_fire2; set { m_fire2 = value; } }

    bool m_canvasActive = false;

    void Start()
    {
        m_textObject.SetActive(false);
        m_selectCanvas.SetActive(m_canvasActive);
        m_setCanvas.SetActive(false);
        m_gameOverCanvas.SetActive(false);
    }

    public void TextObjectActive(bool set)
    {
        m_textObject.SetActive(set);
        if (set)
        {
            TextManager text = m_textObject.GetComponent<TextManager>();
            text.SetText(TextManager.TextType.HeelText, 0.05f, m_textObject);
        }
    }

    public void SetSelectCanvasActive()
    {
        if (!m_canvasActive) m_canvasActive = true;
        else m_canvasActive = false;

        m_selectCanvas.SetActive(m_canvasActive);
    }

    public void SetCanvasActive(Sprite set)
    {
        m_setImage.sprite = set;
        m_setCanvas.SetActive(true);
    }

    public void SetCanvasFalse() => m_setCanvas.SetActive(false);
    public void GameOverCanvasActive(bool set)
    {
        Debug.Log(m_gameOverCanvas);
        m_gameOverCanvas.SetActive(set);
    }
}
