using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage { void GetDamage(int damege); }

public abstract class NewEnemyBase : MonoBehaviour
{
    [SerializeField] int m_hp;
    [SerializeField] float m_speed;
    [SerializeField] int m_attackPower;
    [SerializeField] GameObject m_deadSprite = default;

    public SetAttackStatus SetAttack { get => m_set; set { m_set = value; } }
    SetAttackStatus m_set;
    
    public int MaxHp { get => m_maxHp; set { m_maxHp = m_hp; } }
    int m_maxHp;

    float m_defaultSpeed;
    bool m_setSpeed = false;
    
    public int SetAttackPower() { return m_attackPower; }
    public int RetuneCrreantHp() { return m_hp; }
    public int SetHp(int set, GameObject parent) 
    { 
        if (set <= 0) { Died(parent); }
        return m_hp = set;
    }
    void Died(GameObject parent)
    {
        GameObject set = Instantiate(m_deadSprite);
        set.transform.position = transform.position;
        Destroy(parent);
    }
    public void FieldCheck()
    {
        WallCheck();
        GroundCheck();
    }
    void WallCheck()
    {
        LayerMask wall = LayerMask.GetMask("Wall");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, SetWallRay(), SetWallRay().magnitude, wall);

        if (hit.collider) { ChengeMove(); }
    }
    void GroundCheck()
    {
        LayerMask ground = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, SetGroundRay(), SetGroundRay().magnitude, ground);

        if (!hit.collider) { ChengeMove(); }
    }
    Vector2 SetWallRay()
    {
        Vector2 wallRay = Vector2.zero;
        if (m_speed < 0) { wallRay = new Vector2(-1.5f, 0); }
        else if (m_speed > 0) { wallRay = new Vector2(1.5f, 0); }

        return wallRay;
    }
    Vector2 SetGroundRay()
    {
        Vector2 groundRay = Vector2.zero;
        if (m_speed < 0) { groundRay = new Vector2(-2, -2); }
        if (m_speed > 0) { groundRay = new Vector2(2, -2); }

        return groundRay;
    }
    void ChengeMove()
    {
        m_speed *= -1;
        if (!m_setSpeed) 
        {
            m_setSpeed = true;
            m_defaultSpeed = m_speed;
        }
        StartCoroutine(Chenge());
    }
    IEnumerator Chenge()
    {
        m_speed = 0;
        yield return new WaitForSeconds(3);
        m_setSpeed = false;
        m_speed = m_defaultSpeed;
    }
    public void FindPlayerToLook()
    {
        Vector2 ray = new Vector2(SetSpeed() * 10, 0);
        LayerMask layer = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, ray, ray.magnitude, layer);

        if (!hit.collider)
        { transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y); }
    }
    public float SetSpeed() 
    {
        GetSpeed(m_speed);
        return m_speed;
    }
    void GetSpeed(float get)
    {
        if (get < 0) { transform.localScale = new Vector2(-0.15f, 0.15f); }
        else if (get > 0){ transform.localScale = new Vector2(0.15f, 0.15f); }
    }

    public abstract void Move();
    public abstract void Attack();
}
