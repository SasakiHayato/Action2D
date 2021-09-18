using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public enum GameDifficulty
    {
        Easy,
        Normal,
        Hard,
        Extra,
    }

    public static GameManager Instance = new GameManager();

    SceneLoadClass m_loadClass;
    NewFadeClass m_fadeClass;
    GameUiClass m_gameUi;

    GameObject m_select;
    GameObject m_start;

    static GameDifficulty m_gameType;
    
    public GameDifficulty GetGameEnum() => m_gameType;
 
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

    public void SetScene(string set)
    {
        m_loadClass = FindObjectOfType<SceneLoadClass>();
        m_loadClass.OnLoadScene(set);
    }

    public void IsFadeAndSetScene(FadeType type, string sceneName)
    {
        m_fadeClass = FindObjectOfType<NewFadeClass>();
        m_fadeClass.Type = type;
        m_fadeClass.Name = sceneName;
        m_fadeClass.Retune = true;
    }

    public void IsPlay()
    {
        SetCrreantPlay(true);
        IsFadeAndSetScene(FadeType.Out, "Start");
    }

    public void Deid()
    {
        SetCrreantPlay(false);
        m_gameUi = FindObjectOfType<GameUiClass>();
        m_gameUi.GameOverCanvasActive(true);
        m_gameUi.Fire1.sprite = null;
        m_gameUi.Fire2.sprite = null;
    }
    public void ReStart()
    {
        PlayerDataClass.getInstance().GetHp(100);

        PlayerDataClass.getInstance().SetAttackPower = 1;
        PlayerDataClass.getInstance().SetMagicPower = 1;
        PlayerDataClass.getInstance().SetShieldPower = 1;
    }

    public void SetGameDifficulty(int set)
    {
        m_gameType = (GameDifficulty)Enum.ToObject(typeof(GameDifficulty), set);
        
        m_select.SetActive(false);
        m_start.SetActive(true);
    }

    public void SetSelectActive()
    {
        m_select.SetActive(true);
        m_start.SetActive(false);
    }

    private static bool m_cureated = false;
    private void Awake()
    {
        if (GameObject.Find("SelectImage") != null && m_select == null)
        {
            m_select = GameObject.Find("SelectImage");
            m_select.SetActive(false);
        }
        if (GameObject.Find("ButtonImage") != null && m_start == null)
        {
            m_start = GameObject.Find("ButtonImage");
        }
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
