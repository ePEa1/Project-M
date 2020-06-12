using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProjectM.ePEa.CustomFunctions.CustomFunction;
using UnityEngine.UI;

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

        [SerializeField] GameObject m_damEff;

        [SerializeField] AnimationCurve m_damAc;

        [SerializeField] float m_refilMax = 3.0f;

        [SerializeField] Slider m_hpBar;
        [SerializeField] Slider m_refilBar;
        [SerializeField] Image m_backhpBar;

        bool IsDecrease = false;
        #endregion

        #region Value

        Transform target; //쫓아갈 캐릭터
        Vector3 m_destPos;

        Vector3 m_startPos;
        Vector3 m_endPos;

        float m_refil = 3.0f;

        float m_nowHp;

        float m_time = 0.0f;
        float m_nowDelay = 0.0f;

        Vector3 m_knockStart;
        Vector3 m_knockEnd;

        float m_knockTime = 0.0f;

        public enum state
        {
            MOVE,
            ATK,
            DAMAGE,
            DEAD
        }

        public state m_nowState = state.MOVE;

        float m_changeDest = 0.0f;

        #endregion

        private void Awake()
        {
            target = GameObject.FindWithTag("Player").transform;
            m_destPos = transform.position;

            m_refil = m_refilMax;
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
                    Damage();
                    break;

                case state.DEAD:
                    break;
            }

            m_refil = Mathf.Min(m_refil + Time.deltaTime, m_refilMax);
            if (m_refil == m_refilMax)
            {
                m_nowHp = m_maxHp;
            }
            m_nowDelay = Mathf.Max(0, m_nowDelay - Time.deltaTime);

            m_hpBar.value = m_nowHp / m_maxHp;
            m_refilBar.value = m_refil / m_refilMax;

            if (m_nowHp <= 0)
                Destroy(gameObject);

            if (IsDecrease)
            {
                m_backhpBar.GetComponent<Image>().fillAmount = Mathf.Lerp(m_backhpBar.fillAmount, m_hpBar.value, Time.deltaTime*5.0f );
                if (m_hpBar.value >= m_backhpBar.fillAmount - 0.01f)
                {
                    IsDecrease = false;
                    m_backhpBar.fillAmount = m_hpBar.value;

                }
            }

        }

        void Move()
        {
            Vector3 charPos = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 destPos = new Vector3(target.position.x, 0, target.position.z);

            if (Vector3.Distance(charPos, destPos) > m_atkRange)
                m_changeDest = Mathf.Max(0, m_changeDest - Time.deltaTime);

            if (m_changeDest <= 0)
            {
                m_destPos = destPos + Quaternion.Euler(0.0f, Random.Range(-60, 60), 0.0f) * (charPos - destPos).normalized * Random.Range(0, m_atkRange);
                m_changeDest = Random.Range(1, 4);
            }

            if (Vector3.Distance(charPos, destPos) < m_atkRange && m_nowDelay==0)
            {
                AtkStart();
            }
            else if (Vector3.Distance( charPos, m_destPos) >1.0f)
            {
                transform.position += (m_destPos - charPos).normalized * m_moveSpeed * Time.deltaTime;
                Quaternion dir = Quaternion.LookRotation((m_destPos - charPos).normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, dir, Time.deltaTime * 3.0f);
            }
        }




        void Atk()
        {
            float ac = 1.0f / m_rushSpeed;

            Vector3 beforePos = Vector3.Lerp(m_startPos, m_endPos, m_atkAc.Evaluate(m_time * ac));
            m_time += Time.deltaTime;
            Vector3 afterPos = Vector3.Lerp(m_startPos, m_endPos, m_atkAc.Evaluate(m_time * ac));

            Vector3 fixedPos = FixedMovePos(transform.position, 0.6f, (afterPos - beforePos).normalized, Vector3.Distance(beforePos, afterPos), m_wall);

            if (!float.IsNaN(fixedPos.x))
                transform.position += afterPos - beforePos + fixedPos;
            else
                transform.position += afterPos - beforePos;

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
            m_nowDelay = m_atkDelay;
            m_time = 0.0f;
            m_startPos = transform.position;
            m_endPos = transform.position + (target.position - transform.position).normalized * m_rushRange;
            m_animator.SetTrigger("Atk");

            GameObject eff = Instantiate(m_eff);
            eff.transform.parent=transform;
            eff.transform.localPosition = new Vector3(0.0f,1.0f,1.0f);

            transform.rotation = Quaternion.LookRotation(m_endPos - m_startPos);
            m_nowState = state.ATK;
            m_atkCollider.GetComponent<AtkCollider>().knockPower = 5.0f;
            m_atkCollider.GetComponent<AtkCollider>().knockVec = (m_endPos - m_startPos).normalized;
        }

        void AtkEnd()
        {
            m_nowState = state.MOVE;
            m_time = 0.0f;

            m_destPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        }

        public void TakeDamage(float damage, Vector3 knockDir, float knockPower)
        {
            m_nowHp -= damage;
            
            m_knockTime = 0;
            m_knockStart = new Vector3(transform.position.x, 0.0f, transform.position.z);
            m_knockEnd = m_knockStart + knockDir * knockPower;
            m_nowState = state.DAMAGE;
            transform.rotation = Quaternion.LookRotation(-knockDir);
            GameObject eff = Instantiate(m_damEff);
            eff.transform.position = transform.position + Vector3.up;

            m_refil = 0;
            Invoke("HPDecrease",0.5f);

        }

        void Damage()
        {
            m_atkCollider.gameObject.SetActive(false);
            Vector3 before = Vector3.Lerp(m_knockStart, m_knockEnd, m_damAc.Evaluate(m_knockTime));
            m_knockTime += Time.deltaTime * 3.5f;
            Vector3 after = Vector3.Lerp(m_knockStart, m_knockEnd, m_damAc.Evaluate(m_knockTime));

            Vector3 fixedPos = FixedMovePos(transform.position, 0.6f, (after - before).normalized, Vector3.Distance(before, after), m_wall);

            transform.position += after - before + fixedPos;

            if (m_knockTime > 1)
            {
                m_nowState = state.MOVE;
            }
        }

        void HPDecrease()
        {
            IsDecrease = true;
        }
        
    }
}
