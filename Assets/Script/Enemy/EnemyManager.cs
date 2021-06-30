using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int m_hp = 0;
    [SerializeField] private int m_attackPower = 0;
    private int maxHp;

    [SerializeField] Slider slider;
    GameObject canvas;
    Animator m_animator;

    void Start()
    {
        canvas = transform.GetChild(0).gameObject;

        canvas.SetActive(false);
        slider.value = m_hp;
        slider.maxValue = 50;
        maxHp = m_hp;
    }

    void HpActive()
    {
        if (m_animator != null)
        {

            if (maxHp > m_hp)
            {
                canvas.SetActive(true);
            }
            if (m_hp <= 0)
            {
                canvas.SetActive(false);
                m_animator.Play("enemy_death_anim");
                StartCoroutine(DestoryFrame(45));
            }
        }
        else
        {
            m_animator = GetComponent<Animator>();
        }
    }

    private IEnumerator DestoryFrame(float frame)
    {
        for (int i = 0; i < frame; i++)
        {
            yield return null;
        }
        Destroy(this.gameObject);
    }

    public void EnemyDamage(int damage)
    {
        m_hp -= damage;
        slider.value = m_hp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerContoller player = collision.gameObject.GetComponent<PlayerContoller>();

        if (collision.gameObject.CompareTag("Player"))
        {
            player.PlayerDamage(m_attackPower);
        }
    }
}
