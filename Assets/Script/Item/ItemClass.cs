﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClass : ItemBase
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

        if (Input.GetButtonDown("Submit1") && m_active) SetStatus();
        SetVec();
    }

    void SetStatus()
    {
        PlayerDataClass.Instance.SetFreeze(true);
        if (DataBase.GetItemId(ItemId).GetStatuId() == 2)
        {
            SelectStatus();
            Destroy(this.gameObject);
            return;
        }
        else if (DataBase.GetItemId(ItemId).GetStatuId() == 3)
        {
            if (PlayerDataClass.Instance.SetHp() < PlayerDataClass.Instance.m_maxHp)
            {
                int heel = PlayerDataClass.Instance.SetHp() + 30;
                PlayerDataClass.Instance.GetHp(heel);
                if (PlayerDataClass.Instance.SetHp() > 100)
                {
                    PlayerDataClass.Instance.GetHp(100);
                }
            }
            else
            {
                Debug.Log("Max");
                return;
            }
            Destroy(this.gameObject);
            return;
        }
        bool first = PlayerDataClass.Instance.SetIdBoolFirst;
        bool second = PlayerDataClass.Instance.SetIdBoolSecond;

        if (first && second)
        {
            Select();
            SetId();
            Destroy(this.gameObject);
            return;
        }

        if (!first)
        {
            PlayerDataClass.Instance.SetAttackIdFirst = DataBase.GetItemId(ItemId).GetId();
        }
        else
        {
            PlayerDataClass.Instance.SetAttackIdSecond = DataBase.GetItemId(ItemId).GetId();
        }

        SetItem();
        Destroy(this.gameObject);
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