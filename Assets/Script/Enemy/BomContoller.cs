using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomContoller : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    BommerController m_bommer;

    GameObject m_player;
    GameObject m_hitColliderOb;

    Animator m_animator;

    void Start()
    {
        m_bommer = FindObjectOfType<BommerController>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();

        m_player = GameObject.Find("player").gameObject;
        m_hitColliderOb = transform.GetChild(0).gameObject;
        m_hitColliderOb.SetActive(false);
        
        Vector2 vector = new Vector2();

        Vector2 force = ProjectileMotion(vector) * 8.5f;
        m_rigidbody.AddForce(force, ForceMode2D.Impulse);

        StartCoroutine(SetCollider(2.0f));
    }

    private IEnumerator SetCollider(float time)
    {
        yield return new WaitForSeconds(time);
        m_hitColliderOb.SetActive(true);
    }
   
    private void Explosion()
    {
        PlayerContoller player = m_player.GetComponent<PlayerContoller>();
        Vector2 m_hitVector = default;

        if (this.transform.position.x < m_player.transform.position.x)
        {
            Debug.Log("右");
            m_hitVector = new Vector2(10, 10);
}
        else
        {
            Debug.Log("左");
            m_hitVector = new Vector2(-10, 10);
        }
        player.m_rigidbody.AddForce(m_hitVector, ForceMode2D.Impulse);
        //player.m_rigidbody.AddExplosionForce(10000, this.transform.position, 1000);
        DestroyBom();
    }

    private void DestroyBom()
    {
        m_bommer.m_attackBool = false;
        m_animator.Play("Bom_Explosion");
        //Destroy(this.gameObject);
    }

    private Vector2 ProjectileMotion(Vector2 vector)
    {
        float v0 = 5;
        float x = m_player.transform.position.x - this.transform.position.x;
        float t = 3;

        float cos = x / (v0 * t);

        Debug.Log(Mathf.Acos(cos));
        float angle = Mathf.Acos(cos) * (180 / Mathf.PI);

        Debug.Log(angle);

        float rad = angle * Mathf.Deg2Rad;

        vector = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Debug.Log(vector);


        return vector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Explosion();
            //collision.rig
        }
    }
}
