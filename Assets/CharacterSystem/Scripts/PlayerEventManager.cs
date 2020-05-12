using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
    [SerializeField] UnityEvent m_nextAtkOkEvent;
    [SerializeField] UnityEvent m_nextAtkCloseEvent;

    public void NextAtkOk()
    {
        m_nextAtkOkEvent.Invoke();
    }

    public void NextAtkClose()
    {
        m_nextAtkCloseEvent.Invoke();
    }
}
