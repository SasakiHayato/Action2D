using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    static GameManager instance = new GameManager();
    static public GameManager getInstance() => instance;
    private GameManager() { }

    SceneClass scene = new SceneClass();

    bool m_isPlay = false;
    int m_dungeonCount = 0;
    float m_timer = 0;

    

    public void SetCrreantPlay(bool set) => m_isPlay = set;
    public bool GetCrreantPlay() => m_isPlay;

    public int SetDungeonCount() => m_dungeonCount + 1;
    public int GetDungeonCount() => m_dungeonCount;
    public int ResetDungeonCount() => m_dungeonCount = 0;

    public float SetTime() => m_timer += Time.deltaTime;

    public void SetScene(string set) => scene.OnLoadScene(set);
}
