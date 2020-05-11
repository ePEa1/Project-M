using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;

public class PlayerFsmManager : MonoBehaviour
{
    #region Value

    public enum PlayerENUM //플레이어 상태 리스트
    {
        IDLE = 0,
        MOVE,
        ATK,
        DODGE,
        DAMAGE
    }
    
    public PlayerENUM m_currentStat { get; private set; } //현재 상태
    BaseAction m_currentAction; //현재 실행할 액션
    public PlayerController m_currentController { get; private set; } //캐릭터 조작 처리
    public static PlayerFsmManager g_playerFsmManager { get; private set; } //캐릭터 설정
    public Transform playerCam { get { return m_cam; } } //캐릭터 카메라에 접근
    #endregion

    #region Inspector

    [SerializeField] PlayerENUM m_startStat; //시작시 상태

    [SerializeField] BaseAction[] m_playerActions; //액션 모음
    [SerializeField] Animator m_currentAni; //캐릭터 애니메이터
    [SerializeField] Transform m_cam; //캐릭터 추적 카메라

    #endregion

    #region Function

    private void Awake()
    {
        m_currentStat = m_startStat; //시작 시 상태 설정
        m_currentController = GetComponent<PlayerController>(); //컨트롤러 연결
        g_playerFsmManager = this;
        m_currentAction = m_playerActions[(int)m_currentStat].StartAction(); //시작 상태에 따라 액션 실행
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_currentAction.UpdateAction(); //현재 상태에 맞는 액션 실행
    }

    /// <summary>
    /// 상태 변경 이벤트
    /// </summary>
    /// <param name="stat">바꿀 플레이어 상태</param>
    public void ChangeAction(PlayerENUM stat)
    {
        BaseAction changeAction = m_playerActions[(int)stat];

        //변경하려는 상태가 현재 상태와 다를 경우
        if (m_currentAction != changeAction)
        {
            //상태 종료 이벤트 실행, 
            m_currentAction.EndAction();

            //현재 실행할 상태 변경
            m_currentAction = changeAction;
            m_currentStat = stat;

            m_currentAction.StartAction(); //액션 시작 이벤트 실행
        }
    }

    #endregion
}