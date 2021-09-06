using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewItemBase : MonoBehaviour
{
    private enum Item
    {
        Attack,
        Magic,
        Shield,
    }

    [SerializeField] Item m_enum;
    [SerializeField] AttackItemDataBase m_dataBase;
    Uicontroller m_ui;

    public AttackItemDataBase DataBase { get => m_dataBase; }
    public int ItemId { get => (int)m_enum;}

    private void Awake() => m_ui = FindObjectOfType<Uicontroller>();
    public void SetItem() => m_ui.SetSprite(m_dataBase.GetItemId(ItemId).GetSprite());
}
