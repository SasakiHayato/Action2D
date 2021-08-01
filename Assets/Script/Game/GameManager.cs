using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = new GameManager();

    private static int m_count = 0;
    private static bool m_cureated = false;
    private static bool m_isPlay = false;

    private static float m_timer = 0;

    public void IsPlay()
    {
        m_isPlay = true;
        LoadS();
    }

    private void LoadS()
    {
        SceneManager.LoadScene("Start");
    }

    public bool CureatPlay()
    {
        return m_isPlay;
    }

    private bool EndPlay()
    {
        m_isPlay = false;
        return m_isPlay;
    }

    public float GameTime()
    {
        m_timer += Time.deltaTime;
        return m_timer;
    }

    public void LoadD()
    {
        CountCheck();
        
        if (m_count == 3)
        {
            SceneManager.LoadScene("BossRoom");
            return;
        }

        SceneManager.LoadScene("Dungeon");
    }

    public void LoadM()
    {
        SceneManager.LoadScene("Midway");
    }

    void Awake()
    {
        if (!m_cureated)
        {
            DontDestroyOnLoad(Instance);
            m_cureated = true;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void CountCheck()
    {
        m_count++;
    }
}
