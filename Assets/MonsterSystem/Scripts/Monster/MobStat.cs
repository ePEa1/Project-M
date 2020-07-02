using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStat : MonoBehaviour
{
    [Header("MonsterStat")]
    public float m_atkDelay; //공격 딜레이
     public float m_rushRange;
     public float attackRange;
     public float attackRate;

    public float m_rushSpeed;
    public float moveSpeed = 7.5f;
    public float rotateSpeed = 540;
    //  public float fallSpeed = 20;

    public float m_colliderOpenTime;
    public float m_currentReTime = 0;
    public float m_refillTime;
    public float m_time = 0.0f;

    public bool IsDecrease = false;

    [Header("HP")]
    public float hp;
    public float currentHp;
    public MobStat lastHitBy = null;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHp = 100;
        currentHp = hp;
    }

}
