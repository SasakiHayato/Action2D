using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeType
{
    In,
    Out,
}

public class NewFadeClass : MonoBehaviour
{
    Canvas m_canvas;
    CanvasScaler m_canvasScaler;

    Image m_fadeImage;

    FadeType m_type;
    public FadeType Type { get => m_type; set { m_type = value; } }

    string m_sceneName;
    public string Name { get => m_sceneName; set { m_sceneName = value; } }

    float m_alfa;
    [SerializeField] float m_fadeSpeed;

    bool m_isFade = false;
    bool m_retune = true;

    void Start()
    {
        GetCanvas();
        GetCanvasScaler();

        SetFadeImage();
    }

    void Update()
    {
        if (!m_retune) return;
        m_fadeImage.color = new Color(m_fadeImage.color.r, m_fadeImage.color.g, m_fadeImage.color.b, m_alfa);

        if (m_type == FadeType.In)
        {
            m_alfa -= m_fadeSpeed;
            if (m_alfa < 0)
            {
                m_isFade = true;
            }
        }
        else if (m_type == FadeType.Out)
        {
            m_alfa += m_fadeSpeed;
            if (m_alfa > 1)
            {
                m_isFade = true;
            }
        }

        if (m_isFade)
        {
            m_retune = false;
            GameManager.Instance.SetScene(m_sceneName);
        }
    }

    void GetCanvas()
    {
        m_canvas = gameObject.AddComponent<Canvas>();
        m_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }
    void GetCanvasScaler()
    {
        m_canvasScaler = gameObject.AddComponent<CanvasScaler>();
        m_canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        m_canvasScaler.referenceResolution = new Vector2(1600, 900);
    }

    void SetFadeImage()
    {
        m_fadeImage = gameObject.AddComponent<Image>();
        m_fadeImage.color = Color.black;

        if (m_type == FadeType.In) m_alfa = 1;
        else m_alfa = 0;
    }
}
