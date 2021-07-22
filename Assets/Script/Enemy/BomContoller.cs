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
        Vector2 hitVector = default;


        Vector2 force = ExplosionAngle(hitVector) * 8;
        player.m_rigidbody.AddForce(force, ForceMode2D.Impulse);
        
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
        m_player = GameObject.Find("Player").gameObject;
        float v0 = 5;
        float x = m_player.transform.position.x - this.transform.position.x;
        float t = 3;

        float cos = x / (v0 * t);

        Debug.Log(Mathf.Acos(cos));
        float angle = Mathf.Acos(cos) * (180 / Mathf.PI);

        Debug.Log(angle);

        float rad = angle * Mathf.Deg2Rad;

        vector = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Debug.Log("Th" + vector);


        return vector;
    }

    private Vector2 ExplosionAngle(Vector2 vector)
    {
        float angle;
        if (this.transform.position.x < m_player.transform.position.x)
        {
            Debug.Log("右");
            angle = 45;
        }
        else
        {
            Debug.Log("左");
            angle = 135;
        }
        Debug.Log(angle);
        float rad = angle * Mathf.Deg2Rad;

        vector = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Debug.Log("EX " + vector);

        return vector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Explosion();
        }
    }
}
