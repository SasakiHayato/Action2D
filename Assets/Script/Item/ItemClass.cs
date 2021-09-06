using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClass : NewItemBase
{
    [SerializeField] Text m_text;

    GameObject m_canvas;

    bool m_active = false;

    void Start()
    {
        m_text.text = DataBase.GetItemId(ItemId).GetName();
        m_canvas = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        m_canvas.SetActive(m_active);

        if (Input.GetButtonDown("Submit1") && m_active)
        {
            SetItem();
            SetAttackId();
            Destroy(gameObject);
        }

        SetVec();
    }

    void SetAttackId()
    {
        int first = PlayerDataClass.Instance.SetAttackIdFirst;
        int second = PlayerDataClass.Instance.SetAttackIdSecond;

        if (first != 0 && second != 0) return;

        if (PlayerDataClass.Instance.SetAttackIdFirst == 0)
        {
            PlayerDataClass.Instance.SetAttackIdFirst = DataBase.GetItemId(ItemId).GetId();
        }
        else
        {
            PlayerDataClass.Instance.SetAttackIdSecond = DataBase.GetItemId(ItemId).GetId();
        }
        
    }

    public void SetVec()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 set = Vector2.zero;

        if (player.position.x < transform.position.x)
        {
            set = new Vector2(transform.position.x + 1, transform.position.y + 1);

        }
        else
        {
            set = new Vector2(transform.position.x - 1, transform.position.y + 1);
        }

        m_canvas.transform.position = set;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) m_active = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) m_active = false;
    }
}
