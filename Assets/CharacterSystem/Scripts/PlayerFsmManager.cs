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
        DAMAGE,
        DASHATK,
        BACKATK
    }

    public PlayerENUM m_currentStat; //{ get; private set; } //현재 상태
    BaseAction m_currentAction; //현재 실행할 액션
    public PlayerController m_currentController { get; private set; } //캐릭터 조작 처리
    public Animator m_currentAc { get { return m_currentAni; } } // 캐릭터 애니메이터 접근
    public static PlayerFsmManager g_playerFsmManager { get; private set; } //캐릭터 설정
    public Transform playerCam { get { return m_cam; } } //캐릭터 카메라에 접근
    public  AutoTargetManager m_autotarget { get; private set; } //캐릭터 조작 처리

    public bool IsDead = false;
    public bool DelayDashAtk = false;
    
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
        //싱글톤 설정
        if (g_playerFsmManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_currentStat = m_startStat; //시작 시 상태 설정
            m_currentController = GetComponent<PlayerController>(); //컨트롤러 연결
            g_playerFsmManager = this; //싱글톤 객체 설정
            m_currentAction = m_playerActions[(int)m_currentStat].StartAction(); //시작 상태에 따라 액션 실행
            m_cam = GameObject.FindWithTag("MainCamera").transform; //캐릭터가 사용할 카메라 설정
            //m_autotarget = GameObject.FindGameObjectWithTag("TargetUI").GetComponent<AutoTargetManager>();

            m_currentAni.Play("Idle", 0); //시작 시 캐릭터 애니메이션 설정
        }
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

//※처리 구조※----------------------------------
//캐릭터가 가질 수 있는 상태를 Enum에 작성하고, 같은 수만큼의 각 상태 처리 함수(BaseAction)를 m_playerActions에 넣음
//매 업데이트마다 현재 상태 액션에 있는 UpdateAction을 실행시킴
//ChangeAction을 사용하면 현재 상태값을 변경시키고 기존 상태처리 함수의 EndAction을 한번 실행시키고, 바꿀 상태 처리함수의 StartAction을 실행시킴
//그 후 바뀐 상태값에 따라 업데이트에서는 UpdateAction을 계속 실행


//-----------------------------------------------------------
//※만약 새로운 상태를 추가하고싶다면
//1. PlayerEnum 에 새로운 상태를 추가한다
//2. BaseAction을 상속받는 새로운 스크립트를 작성하여 m_playerActions에 집어넣는다