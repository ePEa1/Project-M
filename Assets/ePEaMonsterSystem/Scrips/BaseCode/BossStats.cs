using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : EnemyStats
{
    [SerializeField] float m_maxShield;
    public float MaxShield { get { return m_maxShield; } }
    [SerializeField] float m_refillDelay;
    [SerializeField] float m_refillSpeed;

    public float m_currentShield { get; private set; }
    float m_currentRefill = 0;

    void Awake()
    {
        base.Awake();
        m_currentShield = m_maxShield;
    }

    private void Update()
    {
        RefillShield();
    }

    void RefillShield()
    {
        m_currentRefill = Mathf.Max(0, m_currentRefill - Time.deltaTime);
        if (m_currentRefill <= 0)
        {
            m_currentShield = Mathf.Min(m_maxShield, m_currentShield + Time.deltaTime * m_refillSpeed);
        }
    }

    public void ResetRefillDelay() => m_currentRefill = m_refillDelay;

    public void TakeDamage(float dam)
    {
        ResetRefillDelay();
        if (m_currentShield > dam)
            m_currentShield -= dam;
        else
        {
            float d = dam - m_currentShield;
            m_currentShield = 0;
            m_currentHp = Mathf.Max(0, m_currentHp - d);
        }
    }
}
