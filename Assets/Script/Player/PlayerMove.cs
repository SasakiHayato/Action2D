using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float m_speed = 0;
    public float Speed { get => m_speed; private set { m_speed = value; } }
    public float AvoidanceSpeed { get; set; }
    bool m_crouch = false;
    bool m_avoid = false;

    public void Move(float h, float v, Animator animator)
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
    }

    public void Avoidance(Collider2D collider, float h)
    {
        collider.enabled = false;
        m_avoid = true;
        if (transform.localScale.x > 0)
            AvoidanceSpeed = 20;
        else if (transform.localScale.x < 0)
            AvoidanceSpeed = -20;
        StartCoroutine(ResetAvoid(collider, h));
    }

    IEnumerator ResetAvoid(Collider2D collider, float h)
    {
        yield return new WaitForSeconds(0.2f);
        collider.enabled = true;
        m_avoid = false;
        AvoidanceSpeed = 0;
    }

    public bool CrreantCrouch() { return m_crouch; }
    public bool CrreantAvoid() { return m_avoid; }
}
