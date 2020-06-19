using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventManager : MonoBehaviour
{
    [SerializeField] AniEvent m_manager;

    /// <summary>
    /// 이동 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnMoveEvent(AniEvent.MoveEnum e)
    {
        m_manager.OnMoveEvent(e);
    }

    /// <summary>
    /// 공격 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnAtkEvent(AniEvent.AtkEnum e)
    {
        m_manager.OnAtkEvent(e);
    }

    /// <summary>
    /// 전방이동스킬 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnRushEvent(AniEvent.RushEnum e)
    {
        m_manager.OnRushEvent(e);
    }

    /// <summary>
    /// 회피 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnDodgeEvent(AniEvent.DodgeEnum e)
    {
        m_manager.OnDodgeEvent(e);
    }

    /// <summary>
    /// 피격 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnDamageEvent(AniEvent.DamageEnum e)
    {
        m_manager.OnDamageEvent(e);
    }

    /// <summary>
    /// 좌우 이동스킬 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnDashAtkEvent(AniEvent.DashAtkEnum e)
    {
        m_manager.OnDashAtkEvent(e);
    }

    /// <summary>
    /// 후방 이동 스킬 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnBackAtkEvent(AniEvent.BackAtkEnum e)
    {
        m_manager.OnBackAtkEvent(e);
    }

    /// <summary>
    /// 예시용 함수
    /// </summary>
    /// <param name="e"></param>
    public void OnTestEvent(AniEvent.TestEnum e)
    {
        m_manager.OnTestEvent(e);
    }
}

//※처리 구조※--------------------------------------------------------
//애니메이션 이벤트 키프레임을 넣고싶은 상태의 이벤트배열, enum이 존재
//애니메이션 이벤트 키프레임에서 설정해놓은 이벤트배열, enum에 따라서 인스펙터에서 넣어놓은 이벤트함수를 실행

//◈◈◈애니메이션과 연결된 스크립트이므로 반드시 Animator가 있는 오브젝트에 붙여주어야 정상 작동함◈◈◈

//------------------------------------------------------------------------------
//※만약 이벤트를 추가하고 싶다면※
//1. (이전에 해당 상태의 이벤트배열을 만들지 않았다면)해당 상태의 이벤트를 저장해놓을 이벤트배열, 함수 추가
//2. 추가하고싶은 해당 상태 이벤트의 enum을 추가
//3. 인스펙터에서 추가한 이벤트에 실행시킬 함수 추가
//4. 애니메이션에서 키프레임을 잡을 때 실행시키고 싶은 이벤트배열, enum 선택
//더 자세한 사항은 예시 코드 참고(m_testEvent, TestEnum, OnTestEvent)