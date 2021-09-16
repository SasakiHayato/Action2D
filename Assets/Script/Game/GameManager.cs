using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = new GameManager();

    SceneLoadClass m_loadClass = new SceneLoadClass();

    bool m_isPlay = false;
    bool m_isDungeon = false;
    int m_dungeonCount = 0;
    float m_timer = 0;

    public void SetCrreantPlay(bool set) => m_isPlay = set;
    public bool GetCrreantPlay() => m_isPlay;

    public int SetDungeonCount(int set) => m_dungeonCount += set;
    public int GetDungeonCount() => m_dungeonCount;
    public int ResetDungeonCount() => m_dungeonCount = 0;

    public float SetTime() => m_timer += Time.deltaTime;
    public float CrreantTime() => m_timer;
    
    public bool IsDungeon() => m_isDungeon;
    public bool SetDungeonBool(bool set) => m_isDungeon = set;

    public void SetScene(string set) => m_loadClass.OnLoadScene(set);

    private static bool m_cureated = false;
    private void Awake()
    {
        if (!m_cureated)
        {
            DontDestroyOnLoad(gameObject);
            m_cureated = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
