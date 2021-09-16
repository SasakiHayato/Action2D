using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiClass : MonoBehaviour
{
    [SerializeField] GameObject m_textObject;
    [SerializeField] GameObject m_selectCanvas;

    void Start()
    {
        m_textObject.SetActive(false);
        m_selectCanvas.SetActive(false);
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
}
