using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected PlayerFsmManager m_owner; //액션을 사용하는 캐릭터
    protected PlayerController m_controller; //캐릭터 조작키
    protected Animator m_animator; // 캐릭터 애니메이터
    protected AutoTargetManager m_autotarget;//오토타겟

    void Start()
    {
        m_owner = PlayerFsmManager.g_playerFsmManager;
        m_controller = m_owner.m_currentController;
        m_animator = m_owner.m_currentAc;
        m_autotarget = GetComponent<AutoTargetManager>();
            }
   

    /// <summary>
    /// 액션이 시작될 경우 최초 실행되는 이벤트
    /// 자식에서 재작성 필요
    /// </summary>
    /// <returns></returns>
    protected virtual BaseAction OnStartAction()
    {
        return OnUpdateAction();
    }

    /// <summary>
    /// 현재 상태일 때 계속 실행시킬 이벤트
    /// </summary>
    /// <returns></returns>
    protected abstract BaseAction OnUpdateAction();

    /// <summary>
    /// 액션이 종료될 때 실행시킬 이벤트
    /// </summary>
    public abstract void EndAction();

    /// <summary>
    /// 액션이 처음 시작될 때 실행시킬 이벤트
    /// 외부에서 호출하는 용도
    /// </summary>
    /// <returns></returns>
    public BaseAction StartAction()
    {
        return OnStartAction();
    }

    /// <summary>
    /// 지정 상태일 경우 계속 실행되는 액션
    /// 외부에서 호출하는 용도
    /// </summary>
    /// <returns></returns>
    public BaseAction UpdateAction()
    {
        return OnUpdateAction();
    }

    /// <summary>
    /// 상태 관계없이 계속 실행되는 업데이트 이벤트
    /// 실행할거 없으면 그냥 구현만 해놓기
    /// 상태 처리가 꼬일 수 있기 때문에 진짜 필요한 게 아니면 가급적 사용하지 않는 것을 권장함
    /// </summary>
    protected abstract void AnyStateAction();

    private void Update()
    {
        AnyStateAction();
    }
}