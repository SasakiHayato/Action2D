using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float m_speed = 0;
    bool m_crouch = false;
    bool m_avoid = false;

    public void Move(float h, float v, Rigidbody2D rigidbody, Animator animator)
    {
        if (h == 0 && v == 0)
        {
            animator.Play("Player_Idle_anim");
            m_crouch = false;
        }

        if (h != 0)
        {
            animator.Play("Player_Run");
            
            if (h < 0) { transform.localScale = new Vector2(-0.15f, 0.15f); }
            else { transform.localScale = new Vector2(0.15f, 0.15f); }
        }

        if (v < 0 && h == 0)
        {
            animator.Play("Player_Crouch");
            m_crouch = true;
        }

        rigidbody.velocity = new Vector2(h * m_speed, rigidbody.velocity.y);
    }

    public void Avoidance(Sprite player ,Sprite set, Collider2D collider, Rigidbody2D rb, float h)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = set;
        collider.enabled = false;
        m_avoid = true;
        StartCoroutine(ResetAvoid(collider, renderer, player));
    }

    IEnumerator ResetAvoid(Collider2D collider, SpriteRenderer sprite, Sprite set)
    {
        yield return new WaitForSeconds(1f);
        collider.enabled = true;
        m_avoid = false;
        sprite.sprite = set;
    }

    public bool CrreantCrouch() { return m_crouch; }
    public bool CrreantAvoid() { return m_avoid; }
}
