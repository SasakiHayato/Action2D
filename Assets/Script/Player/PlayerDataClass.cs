using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataClass : MonoBehaviour
{
    public static PlayerDataClass Instance = new PlayerDataClass();

    [System.NonSerialized] public int m_attackPower = 1;
    [System.NonSerialized] public int m_magicPower = 1;
    [System.NonSerialized] public int m_shieldPower = 1;

    [System.NonSerialized] public int m_subAttack = 0;

    [System.NonSerialized] public int m_Hp = 100;
    [System.NonSerialized] public int m_maxHp = 100;

    public bool m_freeze { get; set; }

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
