using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContoller : MonoBehaviour
{
    private float speed = 3;
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
        //EnemyBase enemy = collision.GetComponent<EnemyBase>();
        IDamage iDamage = collision.GetComponent<IDamage>();
        
        if (iDamage != null)
        {
            //enemy.m_rigidbody.AddForce(transform.up * 3, ForceMode2D.Impulse);
            //iDamage.GetDamage(PlayerDataClass.Instance.m_attackPower);
            
            Destroy(this.gameObject);
        }
    }
}
