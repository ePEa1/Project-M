using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveAct : EnemyAction
{
    #region Inspector
    [SerializeField] float m_atkDelay = 3.0f;
    [SerializeField] float m_minDistance = 3.0f;
    [SerializeField] float m_moveSpeed = 5.0f;
    #endregion

    #region Value
    float m_currentDelay;
    float m_currentMove = 0.0f;
    #endregion

    #region Base
    private void Awake()
    {
        m_currentDelay = m_atkDelay;
    }

    protected override void EndAction() { }

    protected override void StartAction() { }

    protected override void UpdateAction()
    {
        m_currentDelay = Mathf.Max(0, m_currentDelay - Time.deltaTime);
        ChangeActions();
        Moving();
    }
    #endregion

    #region Functions

    void ChangeActions()
    {
        if (m_currentDelay == 0 && GetDistance() < 3.0f)
        {
            int patton = Random.Range(0, 3);

            switch(patton)
            {
                case 0:
                    m_owner.ChangeStat("DoubleAtk");
                    break;

                case 1:
                    m_owner.ChangeStat("LiteAtk");
                    break;

                case 2:
                    m_owner.ChangeStat("StrongAtk");
                    break;
            }
            m_currentDelay = m_atkDelay;
        }
    }

    Vector3 ToTarget()
    {
        Vector3 targetView = m_owner.m_player.transform.position - m_owner.transform.position;
        targetView.y = 0;
        return targetView.normalized;
    }

    float GetDistance()
    {
        Vector3 targetPos = m_owner.m_player.transform.position;
        targetPos.y = 0;
        Vector3 ownerPos = m_owner.transform.position;
        ownerPos.y = 0;

        return Vector3.Distance(targetPos, ownerPos);
    }

    void Moving()
    {
        if (GetDistance() > m_minDistance)
        {
            m_owner.transform.position += ToTarget() * Time.deltaTime * m_moveSpeed;
            m_owner.transform.rotation = Quaternion.LookRotation(ToTarget());
            m_currentMove = Mathf.Min(1, m_currentMove + Time.deltaTime * 5.0f);
            
        }
        else
        {
            m_owner.transform.rotation = Quaternion.LookRotation(ToTarget());
            m_currentMove = Mathf.Max(0, m_currentMove - Time.deltaTime * 5.0f);
        }
        m_animator.SetFloat("Moving", m_currentMove);
    }

    #endregion
}


//-----------------------------------------------
