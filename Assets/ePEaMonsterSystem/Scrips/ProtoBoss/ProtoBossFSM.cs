using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.ePEa.ProtoMon
{
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
            switch (m_currentState)
            {
                case State.MOVE:
                    Move();
                    break;

                case State.ATK1:
                    Atk1();
                    break;

                case State.ATK2:
                    Atk2();
                    break;

                case State.ATK3:
                    break;
            }

            if (m_currentHp <= 0)
                Destroy(gameObject);
        }

        void Move()
        {
            m_atkDelay -= Time.deltaTime;
            if (m_atkDelay <= 0)
            {
                Vector3 centerPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
                Vector3 targetPos = new Vector3(target.position.x, 0.0f, target.position.z);
                if (Vector3.Distance(centerPos, targetPos) > 5.0f)
                    m_currentState = State.ATK1;
                else
                    m_currentState = State.ATK2;
            }
        }

        float rushTime = 0.0f;
        float rush = 0.0f;
        float rushEnd = 0.0f;
        Vector3 targetDir = Vector3.zero;
        Vector3 startPos;
        Vector3 finishPos;
        [SerializeField] GameObject rushCollider;
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
                rushCollider.GetComponent<AtkCollider>().knockVec = targetDir;
            }

            if (rushTime >= 0.8f)
            {
                rush = Mathf.Min(1, rush + Time.deltaTime * 5.0f);
                transform.rotation = Quaternion.LookRotation(targetDir);
                transform.position = Vector3.Lerp(startPos, finishPos, rush);
                if (rush == 1)
                {
                    rushCollider.SetActive(false);
                    rushEnd = Mathf.Min(0.3f, rushEnd + Time.deltaTime);
                    if (rushEnd >= 0.3f)
                    {
                        targetDir = Vector3.zero;
                        rush = 0.0f;
                        rushTime = 0.0f;
                        rushEnd = 0.0f;
                        m_currentState = State.MOVE;
                        m_atkDelay = Random.Range(m_atkDelayMin, m_atkDelayMax);
                        m_model.GetComponent<Renderer>().material.color = Color.white;
                        rushCollider.SetActive(false);
                    }
                }
                else
                {
                    rushCollider.SetActive(true);
                }
            }
        }

        float melee1 = 0.0f;
        float melee1rush = 0.0f;
        float melee2 = 0.0f;
        float melee2rush = 0.0f;
        float melee2end = 0.0f;
        Vector3 melee1StartPos = Vector3.zero;
        Vector3 melee1FinishPos = Vector3.zero;
        [SerializeField] GameObject melee1Collider;
        Vector3 melee2StartPos = Vector3.zero;
        Vector3 melee2FinishPos = Vector3.zero;
        [SerializeField] GameObject melee2Collider;

        void Atk2()
        {
            m_model.GetComponent<Renderer>().material.color = new Color(1.0f, 0.5f, 0.0f);
            if (melee1StartPos == Vector3.zero)
            {
                melee1StartPos = transform.position;
                melee1FinishPos = target.transform.position - transform.position;
                melee1FinishPos.y = 0;
                melee1Collider.GetComponent<AtkCollider>().knockVec = melee1FinishPos.normalized;
                melee1FinishPos = melee1StartPos + melee1FinishPos.normalized * 5;
            }

            melee1 = Mathf.Min(0.5f, melee1 + Time.deltaTime);
            if (melee1 >= 0.5f)
            {
                melee1rush = Mathf.Min(1, melee1rush + Time.deltaTime * 10.0f);
                transform.position = Vector3.Lerp(melee1StartPos, melee1FinishPos, melee1rush);

                if (melee1rush >= 1)
                {
                    melee1Collider.SetActive(false);
                    if (melee2StartPos == Vector3.zero)
                    {
                        melee2StartPos = transform.position;
                        melee2FinishPos = target.transform.position - transform.position;
                        melee2FinishPos.y = 0.0f;
                        melee2Collider.GetComponent<AtkCollider>().knockVec = melee2FinishPos.normalized;
                        melee2FinishPos = melee2StartPos + melee2FinishPos.normalized * 6;
                    }

                    melee2 = Mathf.Min(0.4f, melee2 + Time.deltaTime);
                    if (melee2 >= 0.4f)
                    {
                        melee2rush = Mathf.Min(1, melee2rush + Time.deltaTime * 10.0f);
                        transform.position = Vector3.Lerp(melee2StartPos, melee2FinishPos, melee2rush);

                        if (melee2rush >= 1)
                        {
                            melee2Collider.SetActive(false);
                            melee2end = Mathf.Min(0.4f, melee2end + Time.deltaTime);
                            if (melee2end >= 0.4f)
                            {
                                m_currentState = State.MOVE;
                                melee1 = 0.0f;
                                melee1rush = 0.0f;
                                melee2 = 0.0f;
                                melee2rush = 0.0f;
                                melee2end = 0.0f;
                                melee1StartPos = Vector3.zero;
                                melee1FinishPos = Vector3.zero;
                                melee2StartPos = Vector3.zero;
                                melee2FinishPos = Vector3.zero;
                                melee1Collider.SetActive(false);
                                melee2Collider.SetActive(false);
                                m_model.GetComponent<Renderer>().material.color = Color.white;
                                m_atkDelay = Random.Range(m_atkDelayMin, m_atkDelayMax);
                            }
                        }
                        else
                        {
                            melee2Collider.SetActive(true);
                        }
                    }
                }
                else
                {
                    melee1Collider.SetActive(true);
                }
            }
        }

        public void TakeDamage(float dam)
        {
            m_currentHp -= dam;
        }
    }

}
