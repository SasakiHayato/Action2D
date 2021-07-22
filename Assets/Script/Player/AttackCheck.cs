﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamage iDmage = collision.GetComponent<IDamage>();
        
        if (iDmage != null)
        {
            Debug.Log("a");
            iDmage.AddDamage(PlayerDataClass.Instance.m_attackPower);
        }
    }
}
