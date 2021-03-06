using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldClass : MonoBehaviour
{
    [SerializeField] GameObject m_parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AttackClass attack = collision.gameObject.GetComponent<AttackClass>();
        if (attack != null)
        {
            bool set = CheckPos(collision.gameObject.transform);
            if (set) attack.IsShield = true;
        }
    }

    bool CheckPos(Transform collision)
    {
        bool result;
        float dir = collision.position.x - m_parent.transform.position.x;
        if (m_parent.transform.localScale.x > 0)
        {
            if (dir < 0) result = false;
            else result = true;
        }
        else
        {
            if (dir < 0) result = true;
            else result = false;
        }

        return result;
    }
}
