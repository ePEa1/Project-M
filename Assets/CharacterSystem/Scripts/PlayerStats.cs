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
        [SerializeField] float m_mprefillSpeed; //마나 회복 속도

        [SerializeField] public float m_dodgeDelay; //회피 쿨타임
        [SerializeField] public float m_dodgeTime; //회피 무적 지속시간
        [SerializeField] public float m_dodgeDistance; //회피 거리
        [SerializeField] public float m_timeDashAtk;//이동 스킬 쿨타임

        [SerializeField] public float m_rushMp; //전방이동 사용마나
        [SerializeField] public float m_widthMp; //좌우이동 사용마나
        [SerializeField] public float m_backMp; //후방이동 사용마나

        [SerializeField] float m_atkDelay = 1; //평타 막타 딜레이
        [SerializeField] public AtkPowerData m_powerData; //데미지 배율 데이터
        
        #endregion

        #region Value

        public static PlayerStats playerStat { get; private set; } //외부에서 접근할 때의 용도
        public float m_currentHp { get; private set; } //현재 캐릭터 체력
        public float m_currentDodgeDelay { get; private set; } //현재 회피 쿨타임
        public float m_currentMp { get; private set; } //현재 캐릭터 마나
        public int m_atkLevel { get; private set; } //데미지 배율 레벨
        public float m_atkPower { get; private set; } //데미지 배율 값
        public float m_powerGage { get; private set; } //현재 데미지배율 게이지
        public float m_powerGageMinus { get; private set; } //데미지배율 게이지 보정값
        public float m_currentAtkDelay { get; private set; } //현재 평타딜레이

        float m_maxPowerGage = 0.0f;

        #endregion

        private void Awake()
        {
            //싱글톤인데 또 생성할라하면 삭제시킴
            if (playerStat == null)
            {
                playerStat = this;

                m_atkLevel = 0;
                m_atkPower = 1.0f;
                m_powerGage = 0.0f;
                m_powerGageMinus = 0.0f;
                m_currentHp = m_maxHp;
                m_currentMp = m_minMp;
                ResetAtkDelay();
                for(int i=0;i< m_powerData.level.Length;i++)
                {
                    m_maxPowerGage += m_powerData.level[i].nextGage;
                }
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
        {
            m_powerGage = Mathf.Min(m_maxPowerGage, m_powerGage + gage);
        }

        /// <summary>
        /// 데미지 배율 게이지 차감
        /// </summary>
        public void UpdateAtkGage()
        {
            m_powerGage = Mathf.Max(0, m_powerGage - Time.deltaTime * m_powerData.level[m_atkLevel].minusSpeed);
        }

        /// <summary>
        /// 현재 데미지배율 레벨 설정
        /// </summary>
        void UpdatePowLevel()
        {
            float gage = m_powerGage;
            int currentLevel = 0;
            float gageMinus = 0.0f;

            while (gage > m_powerData.level[currentLevel].nextGage)
            {
                gage -= m_powerData.level[currentLevel].nextGage;
                gageMinus += m_powerData.level[currentLevel].nextGage;
                currentLevel++;
            }

            m_powerGageMinus = gageMinus;
            m_atkLevel = currentLevel;
            m_atkPower = m_powerData.level[currentLevel].power;
        }

        private void Update()
        {
            //회피 쿨타임 갱신
            m_currentDodgeDelay = Mathf.Max(0, m_currentDodgeDelay - Time.deltaTime);

            //--------------------------
            UpdateAtkGage();
            UpdatePowLevel();
            //--------------------------

            //마나통 갱신
            RefillMp();

            //딜레이 갱신
            UpdateAtkDelay();
        }

        void UpdateAtkDelay()
        {
            m_currentAtkDelay = Mathf.Max(0, m_currentAtkDelay - Time.deltaTime);
        }

        public void SetAtKDelay() { m_currentAtkDelay = m_atkDelay; }

        public void ResetAtkDelay()
        {
            m_currentAtkDelay = 0;
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
        /// 마나량 자동조정
        /// </summary>
        void RefillMp()
        {
            if (m_currentMp > m_minMp)
                m_currentMp = Mathf.Max(m_minMp, m_currentMp - Time.deltaTime * m_mpSpeed);
            //if (m_currentMp < m_minMp)
            //    m_currentMp = Mathf.Min(m_minMp, m_currentMp + Time.deltaTime * m_mprefillSpeed);
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