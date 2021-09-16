using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemEnum
{
    Attack,
    Magic,
    Shield,
    Status,
    Heel,
}

[CreateAssetMenu (fileName = "AttackItems")]
public class ItemDataBase : ScriptableObject
{
    [SerializeField] List<Items> m_items = new List<Items>();

    public Items GetItemId(int num) => m_items[num];
}

[System.Serializable]
public class Items
{
    [SerializeField] int m_attackPower;
    [SerializeField] string m_name;
    [SerializeField] string[] m_animName = new string[0];
    [SerializeField] int m_id;
    [SerializeField] int m_statusId;
    [SerializeField] GameObject m_object;
    [SerializeField] Sprite m_sprite;

    public int GetAttackPower() => m_attackPower;
    public string GetName() => m_name;
    public string GetAnimName(int num) => m_animName[num];
    public int GetAnimLength() => m_animName.Length;
    public Sprite GetSprite() => m_sprite;
    public int GetId() => m_id;
    public GameObject GetObject() => m_object;
    public int GetStatuId() => m_statusId;
}
