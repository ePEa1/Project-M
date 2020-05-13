using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTime : MonoBehaviour
{
    [SerializeField] float m_lifeTime; //생존 시간
    float m_currentTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        m_currentTime += Time.deltaTime;
        if (m_lifeTime <= m_currentTime)
            Destroy(gameObject);
    }
}
