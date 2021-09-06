using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataClass : MonoBehaviour
{
    public static PlayerDataClass Instance = new PlayerDataClass();

    int m_attackPower = 1;
    [System.NonSerialized] public int m_magicPower = 1;
    [System.NonSerialized] public int m_shieldPower = 1;

    [System.NonSerialized] public int m_subAttack = 0;

    int m_attackId = 0;
    public int SetAttackId { get => m_attackId; set { m_attackId = value; } }

    int m_subAttackId = 0;
    public int SetSubAttackId { get => m_subAttackId; set { m_subAttackId = value; } }

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
