using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ConnectRader
{
    void AddTarget();
    void DestroyTarget();
}

public class EnemyRader : MonoBehaviour
{
    [SerializeField] GameObject m_rader;
    [SerializeField] float m_raderRadius;
    [SerializeField] float m_distanceMax;
    [SerializeField] float m_distanceMin;

    Dictionary<Transform, GameObject> m_raderData = new Dictionary<Transform, GameObject>();

    // Update is called once per frame
    void Update()
    {
        SetRaderDir();
    }

    void SetRaderDir()
    {
        foreach(var raderData in m_raderData)
        {
            Vector3 dir = raderData.Key.position - transform.position;
            dir.y = 0;

            float dis = Vector3.Distance(new Vector3(0, 0, 0), dir);
            dir = dir.normalized;

            float al = Mathf.Clamp(1 - Mathf.Max(0, dis - m_distanceMin) / m_distanceMax, 0, 1);
            Color originCol = raderData.Value.GetComponent<Renderer>().material.color;
            raderData.Value.GetComponent<Renderer>().material.color = new Color(originCol.r, originCol.g, originCol.b, al);
            raderData.Value.transform.rotation = Quaternion.LookRotation(dir);
            raderData.Value.transform.position = transform.position + dir * m_raderRadius;
        }
    }

    public void AddTarget(Transform t)
    {
        GameObject obj = Instantiate(m_rader);
        obj.transform.parent = transform;
        obj.transform.position = transform.position;
        m_raderData.Add(t, obj);
    }

    public void DestroyTarget(Transform enemy)
    {
        Destroy(m_raderData[enemy]);
        m_raderData.Remove(enemy);
    }
}
