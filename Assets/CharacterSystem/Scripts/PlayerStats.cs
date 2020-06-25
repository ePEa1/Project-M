using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.ePEa.PlayerData
{
    public class PlayerStats : MonoBehaviour
    {
        #region Inspector

        [SerializeField] public float m_size; //캐릭터 부피
        [SerializeField] public float m_hikingHeight; //오를 수 있는 경사면 높이
        [SerializeField] public float m_gravity; //중력가속도

        [SerializeField] public float m_maxHp; //캐릭터 최대체력
        [SerializeField] public float m_moveSpeed; //캐릭터 이동속도
        [SerializeField] public float m_movePower; //이동 가속도
        [SerializeField] public float m_curveSpeed; //캐릭터 회전속도
        [SerializeField] float m_maxMp; //마나 최대량
        public float MaxMp { get { return m_maxMp; } }
        [SerializeField] float m_minMp; //마나 삭제 최소량
        [SerializeField] float m_mpSpeed; //마나 줄어드는 속도

        [SerializeField] public float m_dodgeDelay; //회피 쿨타임
        [SerializeField] public float m_dodgeTime; //회피 무적 지속시간
        [SerializeField] public float m_dodgeDistance; //회피 거리
        [SerializeField] public float m_timeDashAtk;//이동 스킬 쿨타임

        [SerializeField] public float m_rushMp; //전방이동 사용마나
        [SerializeField] public float m_widthMp; //좌우이동 사용마나
        [SerializeField] public float m_backMp; //후방이동 사용마나
        
        #endregion

        #region Value

        public static PlayerStats playerStat { get; private set; } //외부에서 접근할 때의 용도
        public float m_currentHp { get; private set; } //현재 캐릭터 체력
        public float m_currentDodgeDelay { get; private set; } //현재 회피 쿨타임
        public float m_currentMp { get; private set; } //현재 캐릭터 마나
        public float m_atkPower { get; private set; } //데미지 배율 값
        public float m_powerGage { get; private set; }
        #endregion

        private void Awake()
        {
            //싱글톤인데 또 생성할라하면 삭제시킴
            if (playerStat == null)
            {
                playerStat = this;
                m_atkPower = 1.0f;
                m_powerGage = 0.0f;
                m_currentHp = m_maxHp;
                m_currentMp = m_minMp;
            }
            else
            {
                Debug.Log("Player Stat is enabled");
                Destroy(this.gameObject);
            }

        }

        /// <summary>
        /// </summary>
        /// 캐릭터 체력 차감 이벤트
        /// <param name="damage">깎을 체력 수치</param>
        public void TakeDamage(float damage)
        {
            m_currentHp -= damage;
            //Debug.Log("Player get Damage" + m_currentHp);
            if(m_currentHp <= 0)
            {
                Debug.Log("GameOver");
                transform.GetComponent<PlayerFsmManager>().IsDead = true;

            }
            else
            {
                transform.GetComponent<PlayerFsmManager>().IsDead = false;
            }
        }

        public void GetAtkGage(float gage)
        { m_powerGage += gage; }

        private void Update()
        {
            //회피 쿨타임 갱신
            m_currentDodgeDelay = Mathf.Max(0, m_currentDodgeDelay - Time.deltaTime);

            //마나통 갱신
            if (m_currentMp > m_minMp)
                m_currentMp = Mathf.Max(m_minMp, m_currentMp - Time.deltaTime * m_mpSpeed);
        }

        /// <summary>
        /// 회피 쿨타임 설정 함수
        /// </summary>
        /// <param name="time">설정할 쿨타임 시간</param>
        public void SetDodgeDelay(float time)
        {
            m_currentDodgeDelay = time;
        }

        /// <summary>
        /// 마나 회복 함수
        /// </summary>
        /// <param name="mp">회복 mp 양</param>
        public void GetMp(float mp)
        {
            m_currentMp = Mathf.Min(m_maxMp, m_currentMp + mp);
        }

        /// <summary>
        /// 마나 사용 함수
        /// </summary>
        /// <param name="mp">사용 mp 양</param>
        public void UseMp(float mp)
        {
            m_currentMp = Mathf.Max(0, m_currentMp - mp);
        }
    }
}