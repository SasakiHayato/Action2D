using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomContoller : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    [SerializeField] private Vector2 vector;

    BommerController m_bommer;
    void Start()
    {
        m_bommer = FindObjectOfType<BommerController>();
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_rigidbody.AddForce(vector, ForceMode2D.Impulse);

        Invoke("Explosion", 4.0f);
    }

    private void Explosion()
    {
        m_bommer.m_attackBool = false;
        Destroy(this.gameObject);
    }
}
