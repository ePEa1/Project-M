using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;

namespace ProjectM.ePEa.ProtoMon
{
    public class ProtoBossFSM : MonoBehaviour, ConnectRader
    {
        #region Inspector
        [SerializeField] public float m_maxHp; //최대체력
        [SerializeField] float m_moveSpeed; //이동속도
        [SerializeField] float m_atkDelayMin; //공격 딜레이
        [SerializeField] float m_atkDelayMax; //공격 딜레이

        //[SerializeField] GameObject //m_model;

        [SerializeField] public float m_shieldMax;
        [SerializeField] float m_refillTime;
        [SerializeField] float m_refillSpeed;

        [SerializeField] Animator m_animator;

        [SerializeField] LayerMask m_wall;

        #endregion

        #region Value
        public float m_currentHp { get; private set; }
        float m_atkDelay;
        public float m_currentShield { get; private set; }
        float m_shieldTime = 0.0f;

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
            m_currentShield = m_shieldMax;
            m_atkDelay = Random.Range(m_atkDelayMin, m_atkDelayMax);
            target = GameObject.FindWithTag("Player").transform;
        }

        void Start()
        {
            AddTarget();
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
                    Atk3();
                    break;
            }

            if (m_currentHp <= 0)
            {
                DestroyTarget();
                Destroy(gameObject);
            }

            if (m_shieldTime == 0)
                m_currentShield = Mathf.Min(m_shieldMax, m_currentShield + Time.deltaTime * m_refillSpeed);

            m_shieldTime = Mathf.Max(0, m_shieldTime - Time.deltaTime);
        }

        void Move()
        {
            m_animator.SetBool("IsRush", false);
            m_animator.ResetTrigger("Rush");
            m_atkDelay -= Time.deltaTime;
            if (m_atkDelay <= 0)
            {
                Vector3 centerPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
                Vector3 targetPos = new Vector3(target.position.x, 0.0f, target.position.z);
                if (Vector3.Distance(centerPos, targetPos) > 6.0f)
                {
                    m_currentState = State.ATK1;
                    m_animator.SetBool("IsRush", true);
                    m_animator.SetTrigger("Rush");
                }
                else
                {
                    int patton = Random.Range(0, 2);
                    switch(patton)
                    {
                        case 0:
                            m_currentState = State.ATK2;
                            m_animator.SetBool("IsAtk2", true);
                            m_animator.SetTrigger("Atk2");
                            break;

                        case 1:
                            m_currentState = State.ATK3;
                            m_animator.SetBool("IsAtk3", true);
                            m_animator.SetTrigger("Atk3");
                            break;
                    }
                }
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
            //m_model.GetComponent<Renderer>().material.color = Color.red;

            rushTime = Mathf.Min(0.8f, rushTime + Time.deltaTime);
            if (rushTime < 0.5f /*&& targetDir == Vector3.zero*/)
            {
                targetDir = target.position - transform.position;
                targetDir.y = 0;
                targetDir = targetDir.normalized;
                transform.rotation = Quaternion.LookRotation(targetDir);
                startPos = transform.position;
                finishPos = startPos + targetDir * 20.0f;
                rushCollider.GetComponent<AtkCollider>().knockVec = targetDir;
            }

            if (rushTime >= 0.8f)
            {
                transform.rotation = Quaternion.LookRotation(targetDir);

                Vector3 beforePos = Vector3.Lerp(startPos, finishPos, rush);
                rush = Mathf.Min(1, rush + Time.deltaTime * 5.0f);
                Vector3 afterPos = Vector3.Lerp(startPos, finishPos, rush);

                Vector3 fixedPos = FixedMovePos(transform.position + Vector3.up * 0.75f, 0.75f, (afterPos - beforePos).normalized, Vector3.Distance(beforePos, afterPos), m_wall);

                transform.position += afterPos - beforePos + fixedPos;
                if (rush == 1)
                {
                    m_animator.SetBool("IsRush", false);
                    //rushCollider.SetActive(false);
                    rushEnd = Mathf.Min(0.3f, rushEnd + Time.deltaTime);
                    if (rushEnd >= 0.3f)
                    {
                        targetDir = Vector3.zero;
                        rush = 0.0f;
                        rushTime = 0.0f;
                        rushEnd = 0.0f;
                        m_currentState = State.MOVE;
                        m_atkDelay = Random.Range(m_atkDelayMin, m_atkDelayMax);
                        //m_model.GetComponent<Renderer>().material.color = Color.white;
                        //rushCollider.SetActive(false);
                    }
                }
                else
                {
                    rushCollider.GetComponent<AtkCollider>().Attacking();
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
        bool melee2atk1 = false;
        bool melee2atk2 = false;
        void Atk2()
        {
            //m_model.GetComponent<Renderer>().material.color = new Color(1.0f, 0.5f, 0.0f);
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
                if (melee1rush >= 1)
                {
                    //melee1Collider.SetActive(false);
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
                        Vector3 beforePos = Vector3.Lerp(melee2StartPos, melee2FinishPos, melee2rush);
                        melee2rush = Mathf.Min(1, melee2rush + Time.deltaTime * 10.0f);
                        Vector3 afterPos = Vector3.Lerp(melee2StartPos, melee2FinishPos, melee2rush);
                        Vector3 fixedPos = FixedMovePos(transform.position + Vector3.up * 0.75f, 0.75f, (afterPos - beforePos).normalized, Vector3.Distance(beforePos, afterPos), m_wall);
                        transform.position += afterPos - beforePos + fixedPos;

                        if (melee2rush >= 1)
                        {
                            //melee2Collider.SetActive(false);
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
                                melee2atk1 = false;
                                melee2atk2 = false;
                                //m_model.GetComponent<Renderer>().material.color = Color.white;
                                m_atkDelay = Random.Range(m_atkDelayMin, m_atkDelayMax);
                                m_animator.SetBool("IsAtk2", false);
                            }
                        }
                        else
                        {
                            if (!melee2atk2)
                            {
                                melee2Collider.GetComponent<AtkCollider>().Attacking();
                                melee2atk2 = true;
                            }
                        }
                    }
                }
                else
                {
                    Vector3 beforePos = Vector3.Lerp(melee1StartPos, melee1FinishPos, melee1rush);
                    melee1rush = Mathf.Min(1, melee1rush + Time.deltaTime * 10.0f);
                    Vector3 afterPos = Vector3.Lerp(melee1StartPos, melee1FinishPos, melee1rush);
                    Vector3 fixedPos = FixedMovePos(transform.position + Vector3.up * 0.75f, 0.75f, (afterPos - beforePos).normalized, Vector3.Distance(afterPos, beforePos), m_wall);
                    transform.position += afterPos - beforePos + fixedPos;
                    if (!melee2atk1)
                    {
                        melee1Collider.GetComponent<AtkCollider>().Attacking();
                        melee2atk1 = true;
                    }
                }
            }
        }

        public void TakeDamage(float dam)
        {
            float d = dam;
            if (m_currentShield > 0)
            {
                m_currentShield -= d;
                if (m_currentShield < 0)
                {
                    d = Mathf.Abs(m_currentShield);
                    m_currentShield = 0;
                }
                else d = 0;
            }
            m_currentHp -= d;
            m_shieldTime = m_refillTime;
        }


        float atk3 = 0.0f;
        float atk3end = 0.0f;
        [SerializeField] GameObject circle;
        [SerializeField] GameObject circleDam;
        bool isAtk3 = false;
        void Atk3()
        {
            atk3 = Mathf.Min(0.6f, atk3 + Time.deltaTime);
            circle.SetActive(true);
            if (atk3 >= 0.6f)
            {
                Vector3 dir = target.position - transform.position;
                dir.y = 0;
                dir = dir.normalized;
                circleDam.GetComponent<AtkCollider>().knockVec = dir;
                circle.SetActive(false);
                if (!isAtk3)
                {
                    circleDam.GetComponent<AtkCollider>().Attacking();
                    isAtk3 = true;
                }
                //circleDam.SetActive(true);
                atk3end = Mathf.Min(0.3f, atk3end + Time.deltaTime);
                if (atk3end>=0.3f)
                {
                    atk3 = 0.0f;
                    atk3end = 0.0f;
                    isAtk3 = false;
                    circle.SetActive(false);
                    //circleDam.SetActive(false);
                    m_currentState = State.MOVE;
                    m_atkDelay = Random.Range(m_atkDelayMin, m_atkDelayMax);
                    m_animator.SetBool("IsAtk3", false);
                }
            }
        }

        public void AddTarget()
        {
            EnemyRader rader = GameObject.FindWithTag("EnemyRader").GetComponent<EnemyRader>();
            if (rader != null)
            {
                rader.AddTarget(transform);
            }
        }

        public void DestroyTarget()
        {
            EnemyRader rader = GameObject.FindWithTag("EnemyRader").GetComponent<EnemyRader>();
            if (rader != null)
            {
                rader.DestroyTarget(transform);
            }
        }
    }

}
