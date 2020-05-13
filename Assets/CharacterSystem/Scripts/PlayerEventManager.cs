using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
    [SerializeField] UnityEvent m_nextAtkOkEvent;
    [SerializeField] UnityEvent m_nextAtkCloseEvent;
    [SerializeField] UnityEvent m_atkTime;
    [SerializeField] UnityEvent m_createEff;

    public void NextAtkOk()
    {
        m_nextAtkOkEvent.Invoke();
    }

    public void NextAtkClose()
    {
        m_nextAtkCloseEvent.Invoke();
    }

    public void AtkTime()
    {
        m_atkTime.Invoke();
    }

    public void CreateEff()
    {
        m_createEff.Invoke();
    }
}
