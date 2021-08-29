using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float m_speed = 0;
    bool m_crouch = false;

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

    public bool CrreantCrouch() { return m_crouch; }
}
