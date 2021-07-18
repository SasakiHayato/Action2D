using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomContoller : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    BommerController m_bommer;

    GameObject m_player;

    void Start()
    {
        m_bommer = FindObjectOfType<BommerController>();
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_player = GameObject.Find("player").gameObject;
        
        Vector2 vector = new Vector2();

        Vector2 force = ProjectileMotion(vector) * 4;
        m_rigidbody.AddForce(force, ForceMode2D.Impulse);
        Invoke("Explosion", 5.0f);
    }

    private void Explosion()
    {
        DestroyBom();
    }

    private void DestroyBom()
    {
        m_bommer.m_attackBool = false;
        Destroy(this.gameObject);
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
}
