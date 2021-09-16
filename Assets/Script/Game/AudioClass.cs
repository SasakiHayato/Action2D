using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClass : MonoBehaviour
{
    private enum SceneEnum
    {
        Title,
        Start,
        Dungeon,
    }
    [SerializeField] private SceneEnum m_enum;

    AudioSource m_source = null;
    [SerializeField] private AudioClip m_click = null;
    [SerializeField] private AudioClip[] m_bgm;

    [SerializeField] private float m_volume = 0;

    private void Start()
    {
        m_source = GetComponent<AudioSource>();
        
        if (m_enum == SceneEnum.Title)
        {
            m_source.clip = m_bgm[0];
        }
        if (m_enum == SceneEnum.Start)
        {
            m_source.clip = m_bgm[1];
        }
        if (m_enum == SceneEnum.Dungeon)
        {
            m_source.clip = m_bgm[2];
        }
        m_source.volume = m_volume;
        m_source.Play();
    }

    public void Onclick()
    {
        m_source.Stop();
        m_source.PlayOneShot(m_click, m_volume / 4);
    }
}
