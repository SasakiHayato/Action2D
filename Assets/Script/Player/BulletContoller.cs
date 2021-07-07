using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContoller : MonoBehaviour
{
    [SerializeField] private float speed = 0;

    private float m_time = 0;
    void Update()
    {
        transform.Translate(speed / 8, 0, 0);
        m_time += Time.deltaTime;
        if (m_time > 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
