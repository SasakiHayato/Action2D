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

    public void SetDir(Transform parent, float x, float y, float power) => Shot(x, y, parent, power);

    public void SetPosToDiamond() => Shot(0, 0, null, 1);

    void Shot(float x, float y, Transform parent, float power)
    {
        if (m_kind == BulletKind.Slash)
        {
            GameObject set = Instantiate(m_slash);
            Rigidbody2D rb = set.GetComponent<Rigidbody2D>();
            set.transform.position = parent.position;
            rb.AddForce(new Vector2(x, y) * power, ForceMode2D.Impulse);
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
