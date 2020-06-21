using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAtkCol : MonoBehaviour
{
    public float m_rushSpeed;
    public float m_rushDistance;

    Vector3 m_startPos;
    Vector3 m_finishPos;
    float m_time = 0.0f;
    bool m_isSetup = false;

    public void Setup()
    {
        m_startPos = transform.position;
        m_finishPos = m_startPos + transform.rotation * Vector3.forward * m_rushDistance;
        m_isSetup = true;
    }

    void Update()
    {
        if (m_isSetup)
        {
            if (m_time >= 1)
                Destroy(gameObject);
            transform.position = Vector3.Lerp(m_startPos, m_finishPos, m_time);
            m_time += Time.deltaTime / m_rushSpeed;
        }
    }
}
