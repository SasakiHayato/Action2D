using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeClass : MonoBehaviour
{
    private GameObject m_canvas = null;
    //private GameObject m_imageOb = null;

    private Image m_image = null;

    //private float m_fadeSpeed = 0.02f;

    private bool m_isOut = false;

    void Start()
    {
        m_image = this.gameObject.AddComponent<Image>();
    }

    void Update()
    {
        Debug.Log(m_isOut);
    }

    public void GetCanvas()
    {
        m_canvas = GameObject.Find("Canvas").gameObject;
        
        if (m_canvas != null) { SetFadeImage(); }
    }

    private void SetFadeImage()
    {
        //m_imageOb = Instantiate(gameObject);
        
        //m_imageOb.transform.SetParent(m_canvas.transform);



        
        m_isOut = true;
        //m_image.color = new Color(0, 0, 0, 0);
    }
}
