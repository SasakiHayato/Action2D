using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPostion : MonoBehaviour
{
    [SerializeField] Transform m_parent;
    [SerializeField] float m_zPos;
    void Update()
    {
        Vector2 seVec = m_parent.position;
        transform.position = new Vector3(seVec.x, seVec.y, m_zPos);
    }
}
