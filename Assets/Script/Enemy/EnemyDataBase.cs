using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "EnemysData")]
public class EnemyDataBase : ScriptableObject
{
    [SerializeField] List<Enemys> m_enemys = new List<Enemys>();

    public Enemys GetEnemyData(int id) => m_enemys[id];
    public int GetEnemyLength { get => m_enemys.Count; }
}

[System.Serializable]
public class Enemys
{
    [SerializeField] string m_name;
    [SerializeField] int m_hp;
    [SerializeField] int m_attackPower;
    [SerializeField] GameObject m_object;

    public string Name { get => m_name; }
    public int Hp { get => m_hp; }
    public int AttackPower { get => m_attackPower; }
    public GameObject EnemyObject { get => m_object; }
}
