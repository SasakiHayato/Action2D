using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataClass : MonoBehaviour
{
    public static PlayerDataClass Instance = new PlayerDataClass();

    int m_attackPower = 1;
    [System.NonSerialized] public int m_magicPower = 1;
    [System.NonSerialized] public int m_shieldPower = 1;

    int m_attackIdFirst = 0;
    public int SetAttackIdFirst 
    {
        get => m_attackIdFirst; 
        set { m_attackIdFirst = value; } 
    }

    int m_attackIdSecond = 0;
    public int SetAttackIdSecond 
    {
        get => m_attackIdSecond; 
        set { m_attackIdSecond = value; }
    }

    int m_hp = 100;
    [System.NonSerialized] public int m_maxHp = 100;

    bool m_freeze = false;
    
    public int SetAttack() { return m_attackPower; }
    public int AttackPowerUp(int power) { return m_attackPower += power; }

    public int SetHp() { return m_hp; }
    public int GetHp(int hp) { return m_hp = hp; }

    public bool SetFreeze(bool set) { return m_freeze = set; }
    public bool GetFreeze() { return m_freeze; }

    private static bool m_cureated = false;

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
}
