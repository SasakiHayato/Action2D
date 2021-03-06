using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataClass
{
    static PlayerDataClass instance = new PlayerDataClass();
    static public PlayerDataClass getInstance() => instance;
    private PlayerDataClass() { }

    int m_attackPower = 1;
    public int SetAttackPower { get => m_attackPower; set { m_attackPower = value; } }
    int m_magicPower = 1;
    public int SetMagicPower { get => m_magicPower; set { m_magicPower = value; } }
    int m_shieldPower = 1;
    public int SetShieldPower { get => m_shieldPower; set { m_shieldPower = value; } }

    int m_attackIdFirst = 0;
    public int SetAttackIdFirst 
    {
        get => m_attackIdFirst; 
        set { m_attackIdFirst = value; } 
    }
    bool m_idFirst = false;
    public bool SetIdBoolFirst
    {
        get => m_idFirst;
        set { m_idFirst = value; }
    }

    int m_attackIdSecond = 0;
    public int SetAttackIdSecond 
    {
        get => m_attackIdSecond; 
        set { m_attackIdSecond = value; }
    }
    bool m_idSecond = false;
    public bool SetIdBoolSecond
    {
        get => m_idSecond;
        set { m_idSecond = value; }
    }

    int m_hp = 100;
    [System.NonSerialized] public int m_maxHp = 100;

    bool m_freeze = false;
    
    public int SetAttack() { return m_attackPower; }
    public int AttackPowerUp(int power) { return m_attackPower += power; }

    public int SetMagic() => m_magicPower;
    public int MagicPowerUp(int power) => m_magicPower += power;

    public int SetShield() => m_shieldPower;
    public int ShieldPowerUp(int power) => m_shieldPower += power;

    public int SetHp() { return m_hp; }
    public int GetHp(int hp) { return m_hp = hp; }

    public bool SetFreeze(bool set) { return m_freeze = set; }
    public bool GetFreeze() { return m_freeze; }
}
