using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.ePEa.PlayerData
{
    public class PlayerStats : MonoBehaviour
    {
        public static PlayerStats playerStat { get; private set; }

        [SerializeField] public float m_maxHp; //캐릭터 최대체력
        [SerializeField] public float m_moveSpeed; //캐릭터 이동속도
        [SerializeField] public float m_movePower; //이동 가속도
        [SerializeField] public float m_curveSpeed; //캐릭터 회전속도

        [SerializeField] public float m_dodgeDelay; //회피 쿨타임
        [SerializeField] public float m_dodgeTime; //회피 무적 지속시간

        private void Awake()
        {
            //싱글톤인데 또 생성할라하면 삭제시킴
            if (playerStat == null)
                playerStat = this;
            else
            {
                Debug.Log("Player Stat is enabled");
                Destroy(this.gameObject);
            }
        }
    }
}