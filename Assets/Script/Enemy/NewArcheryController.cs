using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewArcheryController : EnemyBase
{
    [SerializeField] GameObject m_bow;
    Transform m_muzzle = default;
    Rigidbody2D m_rb;
    Animator m_anim;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();

        m_muzzle = transform.GetChild(0);
    }

    void Update()
    {
        if (ReturnFreeze()) return;
        Move();

        FindField();
        FindPlayerToAttack();
    }

    public override void Move()
    {
        m_rb.velocity = new Vector2(ReturnSpeed(), m_rb.velocity.y);

        if (ReturnSpeed() != 0) { m_anim.Play("Archery_Walk"); }
        else { m_anim.Play("Archery_Idle"); }
       
    }

    public override void Attack()
    {
        m_anim.Play("Archery_Attack");
    }

    public override void GetDamage(float damage)
    {
        float hp = ReturnCurrentHp();
        hp -= damage;
        SetHp(hp);

        if (ReturnCurrentHp() <= 0)
        {
            Dead(gameObject.transform);
        }
    }

    public void SetBow()
    {
        GameObject bow = Instantiate(m_bow);
        bow.transform.position = m_muzzle.position;
    }
}
