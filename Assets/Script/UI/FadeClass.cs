using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeClass : MonoBehaviour
{
    public enum State
    {
        In,
        Out,
    }

    [SerializeField] State m_state;

    [SerializeField] bool m_isStart;
    [SerializeField] string m_sceneName;

    Image m_fadeImage;
    bool m_isFade = false;
    float m_alfa = 0;

    void Start()
    {
        if (m_isStart) SetIsFade((int)m_state);

        if (m_state == State.In) m_alfa = 1;
        else m_alfa = 0;
    }

    void Update()
    {
        if (!m_isFade) return;

        m_fadeImage.color = new Color(m_fadeImage.color.r, m_fadeImage.color.g, m_fadeImage.color.b, m_alfa);
        if (m_state == State.In)
        {
            if (m_alfa > 0) m_alfa -= 0.02f;
            else
            {
                Debug.Log(m_sceneName);
            }
        }
        else
        {
            if (m_alfa < 1) m_alfa += 0.02f;
            else if (m_isFade)
            {
                m_isFade = false;
                StartCoroutine(GetScene());
            } 
        }
    }

    IEnumerator GetScene()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.SetScene(m_sceneName);
    }

    public void SetIsFade(int num)
    {
        //m_state = m_state(num);
        m_fadeImage = GetComponent<Image>();
        m_isFade = true;
        m_fadeImage.color = new Color(m_fadeImage.color.r, m_fadeImage.color.g, m_fadeImage.color.b, m_alfa);
    }
}
