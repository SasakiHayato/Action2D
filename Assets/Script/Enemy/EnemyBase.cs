using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] GameObject m_deidSprite = default;
    [SerializeField] int m_attackPower;
    [SerializeField] float m_hp;
    [SerializeField] float m_speed;
    float m_setSpeed;
    [SerializeField] Vector2 m_rayToWall = Vector2.zero;
    [SerializeField] LayerMask m_layerToWall;
    [SerializeField] Vector2 m_rayToGround = Vector2.zero;
    [SerializeField] LayerMask m_layerToGround;
    [SerializeField] Vector2 m_rayToPlayer = Vector2.zero;
    [SerializeField] LayerMask m_layerToPlayer;

    bool m_freeze = false;

    public abstract void Move();
    public abstract void Attack();
    public abstract void GetDamage(float damage);
    // FindField(), FindToPlayerの呼び出し

    public void FindPlayerToAttack()
    {
        if (ReturnFreeze()) return;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_rayToPlayer, m_rayToPlayer.magnitude, m_layerToPlayer);
        if (hit.collider) { Attack(); }
    }
    public void FindField()
    {
        RaycastHit2D hitToWall = Physics2D.Raycast(transform.position, m_rayToWall, m_rayToWall.magnitude, m_layerToWall);
        if (hitToWall.collider) { SetRestart(); }

        RaycastHit2D hitToGround = Physics2D.Raycast(transform.position, m_rayToGround, m_rayToGround.magnitude, m_layerToGround);
        if (!hitToGround.collider) { SetRestart(); }
    }
    void SetRestart()
    {
        m_setSpeed = m_speed * -1;
        ChengeRay();
        StartCoroutine(RestartMove());
    }

    IEnumerator RestartMove()
    {
        m_speed = 0;
        yield return new WaitForSeconds(2f);
        ChengeMove();
    }

    void ChengeMove()
    {
        if (transform.localScale.x < 0) { transform.localScale = new Vector2(0.15f, 0.15f); }
        else { transform.localScale = new Vector2(-0.15f, 0.15f); }
        m_speed = m_setSpeed;
        
    }
    void ChengeRay()
    {
        m_rayToWall *= -1;
        m_rayToPlayer *= -1;
        m_rayToGround.x *= -1;
    }

    public void Dead(Transform enemy)
    {
        GameObject sprite = Instantiate(m_deidSprite);
        sprite.transform.position = enemy.position;
        sprite.transform.localScale = new Vector2(2, 2);

        Destroy(enemy.gameObject);
    }

    public bool SetFreeze()
    {
        if (!m_freeze) { m_freeze = true; }
        else { m_freeze = false; }

        return m_freeze;
    }

    public bool ReturnFreeze() { return m_freeze; }
    public float ReturnSpeed() { return m_speed; }
    public float ReturnCurrentHp() { return m_hp; }
    public float SetHp(float hp) { return m_hp - hp; }
}
