using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomContoller : MonoBehaviour
{
    GameObject m_player;
    GameObject m_hitColliderOb;

    Animator m_anim;
    Rigidbody2D m_rb;
    NewBommerController m_bommer;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_bommer = FindObjectOfType<NewBommerController>();

        m_hitColliderOb = transform.GetChild(0).gameObject;
        m_hitColliderOb.SetActive(false);
        
        Vector2 vector = new Vector2();

        Vector2 force = ProjectileMotion(vector) * 8.5f;
        m_rb.AddForce(force, ForceMode2D.Impulse);

        StartCoroutine(SetCollider(2.0f));
    }

    private IEnumerator SetCollider(float time)
    {
        yield return new WaitForSeconds(time);
        m_hitColliderOb.SetActive(true);
    }
   
    private void Explosion()
    {
        PlayerMove player = m_player.GetComponent<PlayerMove>();
        Vector2 hitVector = default;

        Vector2 force = ExplosionAngle(hitVector) * 8;
        //player.m_rigidbody.AddForce(force, ForceMode2D.Impulse);
        
        DestroyBom();
    }

    private void DestroyBom()
    {
        m_bommer.SetFreeze();
        m_anim.Play("Bom_Explosion");
        //Destroy(this.gameObject);
    }

    private Vector2 ProjectileMotion(Vector2 vector)
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        float v0 = 5;
        float x = m_player.transform.position.x - this.transform.position.x;
        float t = 3;

        float cos = x / (v0 * t);
        float angle = Mathf.Acos(cos) * (180 / Mathf.PI);

        float rad = angle * Mathf.Deg2Rad;

        vector = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        return vector;
    }

    private Vector2 ExplosionAngle(Vector2 vector)
    {
        float angle;
        if (this.transform.position.x < m_player.transform.position.x) { angle = 45; }
        else { angle = 135; }
        float rad = angle * Mathf.Deg2Rad;

        vector = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        return vector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { Explosion(); }
    }
}
