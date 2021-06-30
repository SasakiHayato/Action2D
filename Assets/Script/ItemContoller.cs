using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContoller : MonoBehaviour
{
    [SerializeField] private int m_powerUp = 0;

    Animator m_animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_animator = GetComponent<Animator>();
        PlayerContoller player;

        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerContoller>();

            player.m_attackPower += m_powerUp;
            Debug.Log(player.m_attackPower);

            m_animator.Play("item_feedback_anim");
            StartCoroutine(Delaytframe(60));
        }
    }

    private IEnumerator Delaytframe(float count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
