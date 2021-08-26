using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryBowClass : MonoBehaviour
{
    [SerializeField] float m_shotPower;

    public void SetDir()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        if (transform.position.x > player.position.x)
        {
            transform.Rotate(0, 180, 0);
            Shot(-1);
        }
        else
        {
            Shot(1);
        }
    }

    void Shot(int x)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(x, 0) * m_shotPower, ForceMode2D.Impulse);
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
