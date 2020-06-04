using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static ProjectM.ePEa.CustomFunctions.CustomFunction;

namespace ProjectM.ePEa.ProtoMon
{
    public class MonsterProto : MonoBehaviour
    {
        #region Inspector

        [SerializeField] float m_atkRange; //공격 가능 사거리
        [SerializeField] float m_backRange; //후퇴 거리
        [SerializeField] float m_atkDelay; //공격 딜레이

        [SerializeField] float m_moveSpeed; //이동속도

        [SerializeField] float m_rushRange; //공격 시 전진거리
        [SerializeField] float m_rushSpeed; //공격 이동량 커브 속도
        [SerializeField] AnimationCurve m_atkAc; //공격 시 이동량 커브

        [SerializeField] Animator m_animator;

        [SerializeField] LayerMask m_wall;

        [SerializeField] float m_maxHp;

        [SerializeField] AtkCollider m_atkCollider;
        [SerializeField] float m_colliderOpenTime;

        [SerializeField] GameObject m_eff;
        #endregion

        #region Value

        NavMeshAgent m_navi;
        Transform target; //쫓아갈 캐릭터
        Vector3 m_startPos;
        Vector3 m_endPos;

        float m_nowHp;

        float m_time = 0.0f;
        float m_nowDelay = 0.0f;

        enum state
        {
            MOVE,
            ATK,
            DAMAGE,
            DEAD
        }

        state m_nowState = state.MOVE;

        #endregion

        private void Awake()
        {
            target = GameObject.FindWithTag("Player").transform;

            m_navi = GetComponent<NavMeshAgent>();
            m_navi.SetDestination(target.position);
            m_navi.updateRotation = false;

            m_nowHp = m_maxHp;
        }

        // Update is called once per frame
        void Update()
        {
            switch (m_nowState)
            {
                case state.MOVE:
                    Move();
                    break;

                case state.ATK:
                    Atk();
                    break;

                case state.DAMAGE:
                    break;

                case state.DEAD:
                    break;
            }

            if (m_nowState != state.MOVE)
            {
                m_navi.speed = 0;
                m_navi.velocity = Vector3.zero;
            }

            m_nowDelay = Mathf.Max(0, m_nowDelay - Time.deltaTime);

            if (m_nowHp <= 0)
                Destroy(gameObject);
        }

        void Move()
        {
            m_navi.SetDestination(target.position);
            Vector3 charPos = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 destPos = new Vector3(m_navi.destination.x, 0, m_navi.destination.z);
            if (Vector3.Distance(charPos, destPos) < m_atkRange)
            {
                m_navi.speed = 0;
                m_navi.velocity = Vector3.zero;

                if (m_nowDelay == 0)
                {
                    AtkStart();
                }
            }
            else
            {
                m_navi.speed = m_moveSpeed;
                Quaternion dir = Quaternion.LookRotation(m_navi.desiredVelocity);
                transform.rotation = Quaternion.Slerp(transform.rotation, dir, Time.deltaTime * 8.0f);
            }
        }

        void Atk()
        {
            float ac = 1.0f / m_rushSpeed;

            Vector3 beforePos = Vector3.Lerp(m_startPos, m_endPos, m_atkAc.Evaluate(m_time * ac));
            m_time += Time.deltaTime;
            Vector3 afterPos = Vector3.Lerp(m_startPos, m_endPos, m_atkAc.Evaluate(m_time * ac));

            Vector3 fixedPos = FixedMovePos(transform.position, 0.6f, (afterPos - beforePos).normalized, Vector3.Distance(beforePos, afterPos), m_wall);

            transform.position += afterPos - beforePos + fixedPos;

            if (m_time >= m_colliderOpenTime)
            {
                m_atkCollider.gameObject.SetActive(true);
            }

            if (m_time >= m_rushSpeed)
            {
                AtkEnd();
                m_atkCollider.gameObject.SetActive(false);
            }
        }

        void AtkStart()
        {
            m_startPos = transform.position;
            m_endPos = transform.position + (target.position - transform.position).normalized * m_rushRange;
            m_animator.SetTrigger("Atk");

            GameObject eff = Instantiate(m_eff);
            eff.transform.parent=transform;
            eff.transform.localPosition = new Vector3(0.0f,1.0f,1.0f);

            transform.rotation = Quaternion.LookRotation(m_endPos - m_startPos);
            m_nowState = state.ATK;
            m_atkCollider.GetComponent<AtkCollider>().knockPower = 3.0f;
            m_atkCollider.GetComponent<AtkCollider>().knockVec = -(m_endPos - m_startPos).normalized;
        }

        void AtkEnd()
        {
            m_nowState = state.MOVE;
            m_time = 0.0f;

            m_nowDelay = m_atkDelay;
        }

        public void TakeDamage(float damage)
        {
            m_nowHp -= damage;
        }
    }
}
