using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour
{
    [SerializeField] private float speed = 0;

    private bool stay = false;
    Rigidbody2D m_rigidbody;
    GameObject player;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!stay) return;
        
        Vector2 eagleMove = player.transform.position - transform.position;
        m_rigidbody.velocity = new Vector2(eagleMove.x * speed, eagleMove.y * speed);

        if (eagleMove.x < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stay = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stay = false;
        }
    }
}
