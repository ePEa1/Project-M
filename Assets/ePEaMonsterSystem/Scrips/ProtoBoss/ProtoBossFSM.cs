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

    [SerializeField] GameObject m_model;

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

    State m_currentState = State.MOVE;

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
        switch(m_currentState)
        {
            case State.MOVE:
                Move();
                break;

            case State.ATK1:
                Atk1();
                break;

            case State.ATK2:
                break;

            case State.ATK3:
                break;
        }
    }

    void Move()
    {
        m_atkDelay -= Time.deltaTime;
        if (m_atkDelay<=0)
        {
            m_currentState = State.ATK1;
        }
    }

    float rushTime = 0.0f;
    float rush = 0.0f;
    float rushEnd = 0.0f;
    Vector3 targetDir = Vector3.zero;
    Vector3 startPos;
    Vector3 finishPos;
    void Atk1()
    {
        m_model.GetComponent<Renderer>().material.color = Color.red;
        

        rushTime = Mathf.Min(0.8f, rushTime + Time.deltaTime);
        if (rushTime >= 0.5f && targetDir == Vector3.zero)
        {
            targetDir = target.position - transform.position;
            targetDir.y = 0;
            targetDir = targetDir.normalized;
            startPos = transform.position;
            finishPos = startPos + targetDir * 20.0f;
        }

        if (rushTime>=0.8f)
        {
            rush = Mathf.Min(1, rush + Time.deltaTime * 5.0f);
            transform.rotation = Quaternion.LookRotation(targetDir);
            transform.position = Vector3.Lerp(startPos, finishPos, rush);
            if (rush == 1)
            {
                rushEnd = Mathf.Min(0.3f, rushEnd + Time.deltaTime);
                if (rushEnd>=0.3f)
                {
                    targetDir = Vector3.zero;
                    rush = 0.0f;
                    rushTime = 0.0f;
                    rushEnd = 0.0f;
                    m_currentState = State.MOVE;
                    m_atkDelay = Random.Range(m_atkDelayMin, m_atkDelayMax);
                    m_model.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
    }
}
