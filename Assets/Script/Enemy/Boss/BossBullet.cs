using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private enum Shot
    {
        Bullet,
        Slash,
    }
    [SerializeField] private Shot m_shot;
   
    private Rigidbody2D m_rb;
    private float m_desTime = 2;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        
        if (m_shot == Shot.Bullet)
        {
            float randomY = Random.Range(1, 5);
            m_rb.AddForce(new Vector2(0, -randomY), ForceMode2D.Impulse);
        }
        else if (m_shot == Shot.Slash)
        {
            GameObject player = GameObject.Find("Player");
            Vector2 dir = SetSlashDir(player);
            m_rb.AddForce(dir, ForceMode2D.Impulse);
        }
        
    }

    void Update()
    {
        m_desTime -= Time.deltaTime;
        if (m_desTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private Vector2 SetSlashDir(GameObject player)
    {
        Vector2 dir = player.transform.position - transform.position;
        Debug.Log(dir);
        return dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
            player.PlayerDamage(10);
        }
    }
}
