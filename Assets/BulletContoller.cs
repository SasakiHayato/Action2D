using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContoller : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    void Update()
    {
        transform.Translate(speed / 8, 0, 0);
    }
}
