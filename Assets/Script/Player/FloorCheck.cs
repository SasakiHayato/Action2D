using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheck : MonoBehaviour
{
    [SerializeField] Collider2D m_collision;
    [SerializeField] PlayerGravity m_gravity;

    [SerializeField] LayerMask m_layer;

    public void SetTriger()
    {
        Vector2 dir = new Vector2(0, -1.5f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dir.magnitude, m_layer);
        if (hit.collider)
        {
            m_collision.enabled = false;
            StartCoroutine(ResetTriger());
        }
        
    }

    IEnumerator ResetTriger()
    {
        yield return new WaitForSeconds(0.25f);
        m_collision.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 12)
        {
            m_gravity.ResetCount();
            m_gravity.ResetFall();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 12)
        {
            m_gravity.ResetFall();
        }
    }
}
