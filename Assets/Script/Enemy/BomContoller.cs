using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomContoller : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    BommerController m_bommer;
    PlayerContoller m_playerC;
    HitExplosion m_hit;

    GameObject m_player;
    GameObject m_hitObject;

    void Start()
    {
        m_bommer = FindObjectOfType<BommerController>();
        m_playerC = FindObjectOfType<PlayerContoller>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_hit = FindObjectOfType<HitExplosion>();

        m_player = GameObject.Find("player").gameObject;
        m_hitObject = transform.GetChild(0).gameObject;

        m_hitObject.SetActive(false);

        Vector2 vector = m_player.transform.position - this.transform.position;

        m_rigidbody.AddForce( vector, ForceMode2D.Impulse);
        Invoke("Explosion", 3.0f);
    }

    private void Explosion()
    {
        m_hitObject.SetActive(true);
        m_hit.OnTriggerEnter2D(m_playerC.m_collider);
        DestroyBom();
    }

    private void DestroyBom()
    {
        m_bommer.m_attackBool = false;
        Destroy(this.gameObject);
    }
}
