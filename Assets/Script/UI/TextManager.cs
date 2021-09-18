using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public enum TextType
    {
        HeelText,
        StartText,
    }

    public TextType Type { get; set; }
    bool m_setBool = false;
    Text m_text;
    [SerializeField] string[] m_setText = new string[0];

    void Start() => m_text = transform.GetChild(0).gameObject.GetComponent<Text>();
    public void SetText(TextType type, float time, GameObject parent)
    {
        if (!m_setBool) 
        {
            m_setBool = true;
            StartCoroutine(IndicateText((int)type, time, parent));
        }
    }

    IEnumerator IndicateText(int id, float time, GameObject parent)
    {
        Debug.Log("aaaa");
        PlayerDataClass.getInstance().SetFreeze(false);
        if (m_text == null)
        {
            m_text = transform.GetChild(0).gameObject.GetComponent<Text>();
        }
        m_text.text = "";
        yield return new WaitForSeconds(0.5f);
    
        for (int count = 0; count < m_setText[id].Length; count++)
        {
            m_text.text += m_setText[id][count];
            yield return new WaitForSeconds(time);
        }

        yield return new WaitForSeconds(0.5f);
        parent.SetActive(false);
        m_setBool = false;
    }
}
