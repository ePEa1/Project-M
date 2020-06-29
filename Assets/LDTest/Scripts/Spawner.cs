using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public string m_enemyName;

    [SerializeField] GameObject m_melee;
    [SerializeField] GameObject m_boss;

    float m_time = 0.0f;
    
    void Update()
    {
        m_time = Mathf.Min(1, m_time + Time.deltaTime);
        if (m_time>=1)
        {
            GameObject e = null;
            switch (m_enemyName)
            {
                case "Melee":
                    e = Instantiate(m_melee);
                    break;

                case "Boss":
                    e = Instantiate(m_boss);
                    break;
            }
            e.transform.localPosition = transform.position;
            e.transform.rotation = transform.rotation;

            Destroy(gameObject);
        }
    }
}
