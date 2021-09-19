using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveClass : MonoBehaviour
{
    [SerializeField] EnemyActive m_setActive = default;
    
    public void GetEnemy(GameObject get, EnemyBase enemyBase) { SetActive(get, enemyBase); }

    void SetActive(GameObject set, EnemyBase enemyBase)
    {
        GameObject setActive = Instantiate(m_setActive.gameObject);
        EnemyActive active = setActive.GetComponent<EnemyActive>();
        active.SetTarget(set);
        active.GetBase = enemyBase;
        active.GetSliderMaxHp = enemyBase.GetHp;
        setActive.transform.position = set.transform.position;
    }
}
