using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : MonoBehaviour
{
    GameObject m_target;

    private void Awake() { m_target = transform.GetChild(0).gameObject; }
    private void OnBecameVisible() { m_target.SetActive(true); }
    private void OnBecameInvisible() { m_target.SetActive(false); }
}
