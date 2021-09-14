using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomContoller : MonoBehaviour
{
    [SerializeField] GameObject m_explosionObject;
    Collider2D m_hitCollider;
    Rigidbody2D m_rb;

    float m_time = 0;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();

        m_hitCollider = transform.GetChild(0).gameObject.GetComponent<Collider2D>();
        m_hitCollider.enabled = false;

        Vector2 force = ProjectileMotion() * 8.5f;
        m_rb.AddForce(force, ForceMode2D.Impulse);

        StartCoroutine(SetCollider(2.9f));
    }

    void Update()
    {
        m_time += Time.deltaTime;
        if (m_time > 3) Explosion();
    }

    private IEnumerator SetCollider(float time)
    {
        yield return new WaitForSeconds(time);
        m_hitCollider.enabled = true;
    }
   
    private void Explosion()
    {
        GameObject set = Instantiate(m_explosionObject);
        set.transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);
        Destroy(this.gameObject);
    }

    private Vector2 ProjectileMotion()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float v0 = 5;
        float x = player.transform.position.x - this.transform.position.x;
        float t = 3;

        float cos = x / (v0 * t);
        float angle = Mathf.Acos(cos) * (180 / Mathf.PI);

        float rad = angle * Mathf.Deg2Rad;

        Vector2 vector = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        return vector;
    }
}
