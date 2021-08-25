using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveClass : MonoBehaviour
{
    [SerializeField] EnemyActive m_setActive = default;
    
    public void GetEnemy(GameObject get) { SetActive(get); }

    void SetActive(GameObject set)
    {
        GameObject setActive = Instantiate(m_setActive.gameObject);
        EnemyActive active = setActive.GetComponent<EnemyActive>();
        active.SetTarget(set);
        setActive.transform.position = set.transform.position;
    }
}
