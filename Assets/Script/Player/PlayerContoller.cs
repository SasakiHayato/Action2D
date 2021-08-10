using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : PlayerManager
{   
    private bool m_attackActive = false;
    private bool m_shieldBool = false;

    private int m_attackCombo = 1;
    
    private GameObject[] m_attack = new GameObject[3];
    private GameObject m_shield = null;

    [SerializeField] private Transform m_nozzle = null;
    [SerializeField] private Transform m_crouchNuzzle = null;

    [SerializeField] private GameObject m_bulletPlefab = null;

    [SerializeField] private PlayerMove m_move;
    
    void Start()
    {
        m_animator = GetComponent<Animator>();
        
        m_shield = GameObject.Find("ShieldCollider").gameObject;
        m_shield.SetActive(m_shieldBool);

        for (int i = 0; i < m_attack.Length; i++)
        {
            m_attack[i] = transform.GetChild(i).gameObject;
            m_attack[i].SetActive(m_attackActive);
        }
    }

    void Update()
    {
        //if (!GameManager.Instance.CureatPlay()) return;

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            SubAttack();
            if (PlayerDataClass.Instance.m_subAttack == 1)
            {
                if (!m_shieldBool)
                {
                    m_shieldBool = true;
                    m_shield.SetActive(m_shieldBool);
                }
                else
                {
                    m_shieldBool = false;
                    m_shield.SetActive(m_shieldBool);
                }
            }
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
        switch (PlayerDataClass.Instance.m_subAttack)
        {
            case 1:
                Freeze();
                m_animator.Play("Player_Shield");
                break;

            case 2:
                if (m_move.m_crouch)
                {
                    m_animator.Play("Player_Magic_crouch");
                    break;
                }

                m_animator.Play("Player_Magic");
                break;
        }
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
