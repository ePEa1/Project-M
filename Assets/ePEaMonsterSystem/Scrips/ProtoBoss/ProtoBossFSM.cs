using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoBossFSM : MonoBehaviour
{
    #region Inspector
    [SerializeField] float m_maxHp; //최대체력
    [SerializeField] float m_moveSpeed; //이동속도
    [SerializeField] float m_atkDelayMin; //공격 딜레이
    [SerializeField] float m_atkDelayMax; //공격 딜레이

    #endregion

    #region Value
    float m_currentHp;
    float m_atkDelay;

    Transform target;

    public enum State
    {
        IDLE,
        MOVE,
        ATK1,
        ATK2,
        ATK3,
        ATK4,
        ATK5,

    }

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        m_currentHp = m_maxHp;
        m_atkDelay = Random.Range(m_atkDelayMin, m_atkDelayMax);
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
