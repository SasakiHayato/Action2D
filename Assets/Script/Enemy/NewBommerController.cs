using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBommerController : EnemyBase
{
    [SerializeField] GameObject m_bom = default;
    GameObject m_muzzle = default;

    Rigidbody2D m_rb;
    Animator m_anim;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();

        m_muzzle = transform.GetChild(0).gameObject;
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
        if (ReturnSpeed() != 0) { m_anim.Play("Bommer_Walk"); }
        else { m_anim.Play("Bommer_Idle"); }

        m_rb.velocity = new Vector2(ReturnSpeed(), m_rb.velocity.y);
    }

    public override void Attack()
    {
        m_anim.Play("Bommer_Attack");
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

    public void SetBum()
    {
        GameObject bom = Instantiate(m_bom);
        bom.transform.position = m_muzzle.transform.position;
    }
}
