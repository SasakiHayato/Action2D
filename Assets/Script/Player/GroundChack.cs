using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChack : MonoBehaviour
{
    public bool isGround = false;
    public int plyerJumpCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            plyerJumpCount = 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
