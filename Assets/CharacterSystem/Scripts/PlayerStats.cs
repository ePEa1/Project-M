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

        [SerializeField] public float m_dodgeDelay; //회피 쿨타임
        [SerializeField] public float m_dodgeTime; //회피 무적 지속시간
        [SerializeField] public float m_dodgeDistance; //회피 거리

        #endregion

        #region Value

        public PlayerFsmManager manager;
        public static PlayerStats playerStat { get; private set; } //외부에서 접근할 때의 용도
        public float m_currentHp { get; private set; } //현재 캐릭터 체력
        public float m_currentDodgeDelay { get; private set; } //현재 회피 쿨타임

        #endregion

        private void Awake()
        {
            
            //싱글톤인데 또 생성할라하면 삭제시킴
            if (playerStat == null)
            {
                playerStat = this;
                m_currentHp = m_maxHp;
            }
            else
            {
                Debug.Log("Player Stat is enabled");
                Destroy(this.gameObject);
            }
            transform.GetComponent<PlayerFsmManager>().HPcheck = m_currentHp;

        }

        /// <summary>
        /// </summary>
        /// 캐릭터 체력 차감 이벤트
        /// <param name="damage">깎을 체력 수치</param>
        public void TakeDamage(float damage)
        {
            m_currentHp -= damage;
            transform.GetComponent<PlayerFsmManager>().HPcheck = m_currentHp;
            Debug.Log("Player get Damage" + m_currentHp);
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


        private void Update()
        {
            //회피 쿨타임 갱신
            m_currentDodgeDelay = Mathf.Max(0, m_currentDodgeDelay - Time.deltaTime);
        }

        /// <summary>
        /// 회피 쿨타임 설정 함수
        /// </summary>
        /// <param name="time">설정할 쿨타임 시간</param>
        public void SetDodgeDelay(float time)
        {
            m_currentDodgeDelay = time;
        }
    }
}

//