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
    bool m_isSpcial = false;
    public bool IsSpcial { get => m_isSpcial; set { m_isSpcial = value; } }

    public void SetDir(Transform parent, float x, float y, float power)
    {
        Vector2 set = new Vector2(x, y);
        Shot(set, parent, power);
    }

    public void SetPosToDiamond() => Shot(Vector2.zero, null, 1);

    void Shot(Vector2 setVec, Transform parent, float power)
    {
        if (m_kind == BulletKind.Slash)
        {
            Vector2 parentVec = parent.position;
            GameObject set = Instantiate(m_slash, parent.position, Quaternion.identity);
            Rigidbody2D rb = set.GetComponent<Rigidbody2D>();
            Vector2 shot = Vector2.zero;
            if (!m_isSpcial)
            {
                shot = setVec - parentVec;
            }
            else
            {
                shot = setVec;
                m_isSpcial = false;
            }
            
            set.transform.position = parent.position;
            rb.AddForce(shot * power, ForceMode2D.Impulse);
            StartCoroutine(DesBullet(set));
        }
        else { StartCoroutine(SetDiamond()); }
    }

    IEnumerator DesBullet(GameObject set)
    {
        yield return new WaitForSeconds(6f);
        Destroy(set);
    }

    IEnumerator SetDiamond()
    {
        while(m_count >= 0)
        {
            yield return new WaitForSeconds(0.5f);
            float x = Random.Range(m_minPos.position.x, m_maxPos.position.x);
            GameObject set = Instantiate(m_diamond);
            set.transform.position = new Vector2(x, 12);
            StartCoroutine(DesBullet(set));
            m_count--;
        }
        m_count = 15;
    }

    public BulletKind SetEnum(BulletKind set) { return m_kind = set; }
}
