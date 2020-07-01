using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAction : MonoBehaviour
{
    protected EnemyFsmManager m_owner;
    protected Animator m_animator;
    bool m_setupOk = false;

    void Start()
    {
        m_setupOk = Setup();
    }

    bool Setup()
    {
        if (transform.parent!=null)
        {
            if (transform.parent.parent!=null)
            {
                if (transform.parent.parent.GetComponent<EnemyFsmManager>()!=null)
                {
                    m_owner = transform.parent.parent.GetComponent<EnemyFsmManager>();
                    m_animator = m_owner.m_currentAni;
                    return true;
                }
            }
        }
        Debug.LogError(gameObject.name + " : 액션 셋팅 에러");
        return false;
    }

    public void OnStartAction()
    {
        if (m_setupOk)
            StartAction();
    }
    protected abstract void StartAction();

    public void OnUpdateAction()
    {
        if (m_setupOk)
            UpdateAction();
    }
    protected abstract void UpdateAction();

    public void OnEndAction()
    {
        if (m_setupOk)
            EndAction();
    }
    protected abstract void EndAction();
}
