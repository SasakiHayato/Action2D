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

    public void IsPlay()
    {
        SceneManager.LoadScene("Start");
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
        Debug.Log(m_count);
    }
}
