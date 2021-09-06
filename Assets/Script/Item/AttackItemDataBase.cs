using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemEnum
{
    Attack,
    Magic,
    Shield,
}

[CreateAssetMenu (fileName = "AttackItems")]
public class AttackItemDataBase : ScriptableObject
{
    [SerializeField] List<AttackItems> m_items = new List<AttackItems>();

    public AttackItems GetItemId(int num) => m_items[num];
}

[System.Serializable]
public class AttackItems
{
    [SerializeField] string m_name;
    [SerializeField] string[] m_animName = new string[0];
    [SerializeField] int m_id;
    [SerializeField] GameObject m_object;
    [SerializeField] Sprite m_sprite;

    public string GetName() => m_name;
    public string GetAnimName(int num) => m_animName[num];
    public int GetAnimLength() => m_animName.Length;
    public Sprite GetSprite() => m_sprite;
    public int GetId() => m_id;
}
