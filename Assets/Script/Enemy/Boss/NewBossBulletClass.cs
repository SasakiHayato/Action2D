using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletKind
{
    Slash,
    Diamond,
}

public class NewBossBulletClass : MonoBehaviour
{
    BulletKind m_kind;

    [SerializeField] GameObject m_slash;
    [SerializeField] GameObject m_diamond;

    [SerializeField] Transform m_minPos;
    [SerializeField] Transform m_maxPos;

    float m_count = 15;

    public void SetDirToFindPlayer(Transform parent)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Shot(player.position.x, player.position.y, parent);
    }
    public void SetPosToDiamond() => Shot(0, 0, null);

    void Shot(float x, float y, Transform parent)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (m_kind == BulletKind.Slash)
        {
            GameObject set = Instantiate(m_slash);
            set.transform.position = parent.position;
            rb.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
        }
        else
        {
            StartCoroutine(SetDiamond());
        }
    }

    IEnumerator SetDiamond()
    {
        while(m_count >= 0)
        {
            yield return new WaitForSeconds(0.5f);
            float x = Random.Range(m_minPos.position.x, m_maxPos.position.x);
            GameObject set = Instantiate(m_diamond);
            set.transform.position = new Vector2(x, 12);
            m_count--;
        }
        m_count = 15;
    }

    public BulletKind SetEnum(BulletKind set) { return m_kind = set; }
}
