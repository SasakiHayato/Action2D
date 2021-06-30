using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumContoller : MonoBehaviour
{
    Rigidbody2D m_rigidbody;
    GameObject player;

    [SerializeField] private float enemySpeed = 0;
    [SerializeField] GroundChack groundChack;

    private bool stay = false;
    
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (groundChack.isGround == false) return;

        if (stay == false)
        {
            Move();
        }
        else if (stay == true)
        {
            ChaseMove();
        }
        WallCheck();
    }

    private void Move()
    {
        if (enemySpeed > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (enemySpeed < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }

        m_rigidbody.velocity = new Vector2(enemySpeed, m_rigidbody.velocity.y);
    }

    void ChaseMove()
    {
        Vector2 moveVector = (player.transform.position - transform.position);
        m_rigidbody.velocity = new Vector2(moveVector.x * enemySpeed, m_rigidbody.velocity.y);

        if (moveVector.x < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (moveVector.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    [SerializeField] Vector2 rayWall = Vector2.zero;
    [SerializeField] LayerMask mask = 0;

    private void WallCheck()
    {
        Vector2 vector = this.transform.position;
        Debug.DrawLine(vector, vector + rayWall);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, rayWall, rayWall.magnitude, mask);

        if (hit.collider)
        {
            enemySpeed *= -1;
            rayWall *= -1;
        }
    }

    //PlayerFind
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stay = true;
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
