using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectClass : MonoBehaviour
{
    private enum Type
    {
        Horizontal,
        Vertical,
    }
    [SerializeField] Type m_type;

    private enum Status
    {
        ItemSelect,
        StatusUp,

        Button,
    }
    [SerializeField] Status m_status;

    [SerializeField] int m_setCount = 0;
    [SerializeField] UnityEvent[] m_events;

    List<GameObject> m_imageObjects = new List<GameObject>();
    PlayerUiClass m_ui;
    GameUiClass m_gameUi;
    ItemDataBase m_dataBase;

    bool m_selectBool = false;
    int m_crreantNum = 0;
    int m_saveId = 0;
    int m_getId = 0;

    Vector2 m_defaultScale = Vector2.zero;
    Vector2 m_selectScale = Vector2.zero;

    void Start()
    {
        m_ui = FindObjectOfType<PlayerUiClass>();
        m_gameUi = FindObjectOfType<GameUiClass>();
        for (int i = 0; i < m_setCount; i++)
        {
            GameObject set = transform.GetChild(i).gameObject;
            m_imageObjects.Add(set);
            m_defaultScale = m_imageObjects[i].transform.localScale;
        }

        m_selectScale = m_defaultScale * 1.2f;
    }

    void Update()
    {
        float set = 0;
        if (m_type == Type.Horizontal) set = Input.GetAxisRaw("Horizontal");
        else set = Input.GetAxisRaw("Vertical") * -1;

        Select(set);
        SelectCrreant();

        if (Input.GetButtonDown("Submit1"))
        {
            if (m_status == Status.ItemSelect) SetItem();
            else if (m_status == Status.StatusUp) StatusUp();
            else if (m_status == Status.Button) SetEvent();
            PlayerDataClass.getInstance().SetFreeze(false);
        }
    }

    void SetEvent() => m_events[m_crreantNum].Invoke();

    void SetItem()
    {
        if (m_crreantNum == 0)
        {
            m_saveId = PlayerDataClass.getInstance().SetAttackIdFirst;
            PlayerDataClass.getInstance().SetAttackIdFirst = m_dataBase.GetItemId(m_getId).GetId();
            PlayerDataClass.getInstance().SetIdBoolFirst = false;
        }
        else if (m_crreantNum == 1)
        {
            m_saveId = PlayerDataClass.getInstance().SetAttackIdSecond;
            PlayerDataClass.getInstance().SetAttackIdSecond = m_dataBase.GetItemId(m_getId).GetId();
            PlayerDataClass.getInstance().SetIdBoolSecond = false;
        }

        m_ui.SetSprite(m_dataBase.GetItemId(m_getId).GetSprite());
        m_gameUi.SetCanvasFalse();

        GameObject set = Instantiate(m_dataBase.GetItemId(m_saveId).GetObject());
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        set.transform.position = player.position;
    }

    void StatusUp()
    {
        if (m_crreantNum == 0) PlayerDataClass.getInstance().MagicPowerUp(1);
        else if (m_crreantNum == 1) PlayerDataClass.getInstance().ShieldPowerUp(1);
        else if (m_crreantNum == 2) PlayerDataClass.getInstance().AttackPowerUp(1);

        m_gameUi.SetSelectCanvasActive();
    }

    public void GetData(ItemDataBase dataBase, int id)
    {
        m_dataBase = dataBase;
        m_getId = id;
    }
    void Select(float num)
    {
        if (num == 0)
        {
            m_selectBool = false;
        }

        if (num > 0 && !m_selectBool)
        {
            m_selectBool = true;
            m_crreantNum++;

            if (m_imageObjects.Count - 1 < m_crreantNum) m_crreantNum--;
        }
        if (num < 0 && !m_selectBool)
        {
            m_selectBool = true;
            m_crreantNum--;

            if (0 > m_crreantNum) m_crreantNum++;
        }
    }
    void SelectCrreant()
    {
        for (int i = 0; i < m_imageObjects.Count; i++)
        {
            m_imageObjects[i].transform.localScale = (i == m_crreantNum ? m_selectScale : m_defaultScale);
        }
    }
}
