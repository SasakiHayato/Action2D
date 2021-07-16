using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    private float m_speed = 7;
    [SerializeField] private float m_jumpPower = 0;
    
    [System.NonSerialized] public bool m_freeze = false;
    private bool m_attackActive = false;
    private bool m_crouch = false;

    [System.NonSerialized] public int m_Hp = 100;
    [System.NonSerialized] public int m_maxHp = 100;

    [System.NonSerialized] public int m_attackPower = 10;
    [System.NonSerialized] public int m_magicPower = 10;
    [System.NonSerialized] public int m_shieldPower = 10;

    private int m_avoidance = 1;
    private int m_attackCombo = 1;
    [System.NonSerialized] public int m_subAttack = 0;

    [SerializeField] GroundChack m_groundChack;
    
    [System.NonSerialized] public Rigidbody2D m_rigidbody;
    [System.NonSerialized] public Collider2D m_collider;
    private Animator m_animator;

    private GameObject[] m_attack = new GameObject[3];

    [SerializeField] private Transform m_nozzle = null;
    [SerializeField] private Transform m_crouchNuzzle = null;
    private Vector2 m_avoidanceTrans = Vector2.zero;

    [SerializeField] private GameObject m_bulletPlefab = null;
    [System.NonSerialized] public GameObject m_itemSeve = null;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        m_animator = GetComponent<Animator>();

        for (int i = 0; i < m_attack.Length; i++)
        {
            m_attack[i] = transform.GetChild(i).gameObject;
            m_attack[i].SetActive(m_attackActive);
        }

        transform.position = this.transform.position;
        m_avoidanceTrans = this.transform.position;
    }

    void Update()
    { 
        Move();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            SubAttack();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            Avoidance();
        }

        Debug.Log(m_itemSeve);
    }
    
    void Move()
    {
        if (m_freeze) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h == 0 && v == 0)
        {
            m_animator.Play("Player_Idle_anim");
            m_crouch = false;
        }

        if (h != 0)
        {
            m_animator.Play("Player_Run");
            if (h > 0)
            {
                transform.localScale = new Vector2(0.15f, 0.15f);
                m_avoidance = 1;
            }
            if (h < 0)
            {
                transform.localScale = new Vector2(-0.15f, 0.15f);
                m_avoidance = 2;
            }
        }
        if (v < 0)
        {
            m_animator.Play("Player_Crouch");
            m_crouch = true;
        }

        m_rigidbody.velocity = new Vector2(h * m_speed, m_rigidbody.velocity.y);
    }

    void Jump()
    {
        if (m_groundChack.isGround == true || m_groundChack.plyerJumpCount > 0)
        {
            m_rigidbody.AddForce(transform.up * m_jumpPower, ForceMode2D.Impulse);
            m_groundChack.plyerJumpCount--;
        }
    }

    void Attack()
    {
        switch (m_attackCombo)
        {
            case 1:
                m_animator.Play("Player_Attack");
                m_attackCombo = 2;
                break;

            case 2:
                m_animator.Play("Player_Attack2");
                m_attackCombo = 3;
                break;

            case 3:
                m_animator.Play("Player_Attack3");
                m_attackCombo = 1;
                break;
        }
    }

    private void SubAttack()
    {
        switch (m_subAttack)
        {
            case 1:
                Freeze();
                m_animator.Play("Player_Shield");
                break;

            case 2:
                if (m_crouch)
                {
                    m_animator.Play("Player_Magic_crouch");
                    break;
                }

                m_animator.Play("Player_Magic");
                break;
        }
    }

    private void Avoidance()
    {
        switch (m_avoidance)
        {
            case 1:
                transform.Translate(4, 0, 0);
                break;

            case 2:
                transform.Translate(-4, 0, 0);
                break;
        }
    }

    public void PlayerDamage(int damage)
    {
        m_Hp -= damage;
        m_animator.Play("Player_Damage");
        if (m_Hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void ItemCheck(GameObject item)
    {
        if (m_itemSeve == null)
        {
            m_itemSeve = item;
            Debug.Log(m_itemSeve);
        }
        else
        {
            Debug.Log("アイテムあり");
        }
    }

    //攻撃中に入力をうけつけない
    private bool Freeze()
    {
        if (m_freeze)
        {
            m_freeze = false;
        }
        else
        {
            m_freeze = true;
        }

        return m_freeze;
    }

    // 攻撃時の Collider の SetActive
    private void SetCollider()
    {
        if (!m_attackActive)
        {
            m_attackActive = true;
        }
        else
        {
            m_attackActive = false;
        }
        m_attack[m_attackCombo - 1].SetActive(m_attackActive);
    }

    private void SetBullet()
    {
        Instantiate(m_bulletPlefab, m_nozzle);
    }

    private void SetBulletCrouch()
    {
        Instantiate(m_bulletPlefab, m_crouchNuzzle);
    }
}
