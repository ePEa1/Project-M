using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
    [SerializeField] UnityEvent[] m_atkEvent;
    [SerializeField] UnityEvent[] m_dodgeEvent;

    public enum AtkEnum
    {
        NextAtkOpen,
        NextAtkClose,
        AtkHitTime,
        CreateEffect
    }

    public enum DodgeEnum
    {
        DodgeEnd
    }

    /// <summary>
    /// 공격 애니메이션에서 실행시킬 이벤트
    /// </summary>
    /// <param name="e"></param>
    public void OnAtkEvent(AtkEnum e)
    {
        m_atkEvent[(int)e].Invoke();
    }

    public void OnDodgeEvent(DodgeEnum e)
    {
        m_dodgeEvent[(int)e].Invoke();
    }
}
