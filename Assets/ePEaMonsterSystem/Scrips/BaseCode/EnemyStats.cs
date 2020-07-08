using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] protected float m_maxHp; //최대체력
    public float MaxHp { get { return m_maxHp; } }

    //공격 딜레이
    [SerializeField] protected float m_delayMin;
    [SerializeField] protected float m_delayMax;

    public float m_currentHp { get; protected set; }

    protected void Awake()
    {
        m_currentHp = m_maxHp;
    }

    public void TakeDamage(float dam)
    {
        m_currentHp -= dam;
    }
}