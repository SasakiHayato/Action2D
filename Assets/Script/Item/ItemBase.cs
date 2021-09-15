using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [SerializeField] ItemEnum m_enum;
    [SerializeField] ItemDataBase m_dataBase;
    Uicontroller m_ui;
    GameUiClass m_gameUi;
    ItemSelectClass m_select;

    public GameUiClass GameUi { get => m_gameUi; }
    public ItemDataBase DataBase { get => m_dataBase; }
    public int ItemId { get => (int)m_enum;}

    private void Awake()
    {
        m_ui = FindObjectOfType<Uicontroller>();
        m_gameUi = FindObjectOfType<GameUiClass>();
    }

    public void SetItem() => m_ui.SetSprite(m_dataBase.GetItemId(ItemId).GetSprite());

    public void Select() => m_ui.SetCanvasActive(m_dataBase.GetItemId(ItemId).GetSprite());
    public void SelectStatus() => m_ui.SetSelectCanvasActive();
    public void SetId()
    {
        m_select = FindObjectOfType<ItemSelectClass>();
        m_select.GetData(m_dataBase, ItemId);
    }
}
