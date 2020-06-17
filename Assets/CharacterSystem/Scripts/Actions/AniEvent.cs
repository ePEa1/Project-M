using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AniEvent : MonoBehaviour
{
    [SerializeField] UnityEvent[] m_moveEvent;
    [SerializeField] UnityEvent[] m_atkEvent;
    [SerializeField] UnityEvent[] m_dodgeEvent;
    [SerializeField] UnityEvent[] m_damageEvent;
    [SerializeField] UnityEvent[] m_dashatkEvent;
    [SerializeField] UnityEvent[] m_backatkEvent;

    [SerializeField] UnityEvent[] m_testEvent;

    public enum MoveEnum
    {
        FootstepPlay
    }

    public enum BackAtkEnum
    {
        SetCollider,
        DeleteCollider,
        CreatEff,
        EndBackAtk,
        SetSound
    }

    public enum DashAtkEnum
    {
        SetCollider,
        DeleteCollider,
        CreatEff,
        EndDashAtk,
        SetSound
    }
    public enum AtkEnum
    {
        NextAtkOpen,
        NextAtkCheck,
        AtkHitTime,
        CreateEffect,
        EndAttack,
        PlaySfx
    }

    public enum DodgeEnum
    {
        DodgeEnd,
        NextAtkOpen
    }

    public enum DamageEnum
    {
        DamageEnd
    }

    public enum TestEnum
    {
        test
    }

    /// <summary>
    /// 이동 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnMoveEvent(MoveEnum e)
    {
        m_moveEvent[(int)e].Invoke();
    }

    /// <summary>
    /// 공격 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnAtkEvent(AtkEnum e)
    {
        m_atkEvent[(int)e].Invoke();
    }

    /// <summary>
    /// 회피 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnDodgeEvent(DodgeEnum e)
    {
        m_dodgeEvent[(int)e].Invoke();
    }

    /// <summary>
    /// 피격 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnDamageEvent(DamageEnum e)
    {
        m_damageEvent[(int)e].Invoke();
    }


    /// <summary>
    /// 좌우 이동스킬 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnDashAtkEvent(DashAtkEnum e)
    {
        m_dashatkEvent[(int)e].Invoke();
    }

    /// <summary>
    /// 후방 이동 스킬 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnBackAtkEvent(BackAtkEnum e)
    {
        m_backatkEvent[(int)e].Invoke();
    }

    /// <summary>
    /// 예시용 함수
    /// </summary>
    /// <param name="e"></param>
    public void OnTestEvent(TestEnum e)
    {
        m_testEvent[(int)e].Invoke();
    }
}
