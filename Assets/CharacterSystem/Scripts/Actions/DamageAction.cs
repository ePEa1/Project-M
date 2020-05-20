﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;

public class DamageAction : BaseAction
{
    #region Inspector

    [SerializeField] AnimationCurve m_knockAC; //넉백 이동 커브
    [SerializeField] GameObject m_damEff; //피격 이펙트

    #endregion

    #region Value

    AtkCollider m_enemyAtk; //적 공격범위 데이터

    Vector3 m_startPos; //맞기 시작한 위치
    Vector3 m_finishPos; //맞은 후 도달하는 위치

    float m_knockTime = 0.0f;
    float m_maxTime;

    float m_ac;

    #endregion

    protected override BaseAction OnStartAction()
    {
        //체력 차감
        PlayerStats.playerStat.TakeDamage(m_enemyAtk.atkDamage);

        //피격 애니메이션 재생
        m_animator.SetBool("IsDamage", true);

        //맞아서 밀려나는 위치 설정
        m_startPos = m_owner.transform.position;
        m_finishPos = m_startPos + m_enemyAtk.knockVec * m_enemyAtk.knockPower;

        m_knockTime = 0.0f;
        m_maxTime = m_enemyAtk.knockTime;
        m_ac = 1.0f / m_maxTime;

        GameObject eff = Instantiate(m_damEff);
        eff.transform.position = transform.position;

        return this;
    }

    public override void EndAction()
    {
    }

    protected override void AnyStateAction()
    {
    }

    protected override BaseAction OnUpdateAction()
    {
        m_knockTime = Mathf.Min(m_maxTime, m_knockTime + Time.deltaTime);
        m_owner.transform.position = Vector3.Lerp(m_startPos, m_finishPos, m_knockAC.Evaluate(m_knockTime * m_ac));

        //넉백 시간 다 끝나면
        if (m_knockTime >= m_maxTime)
        {
            FinishKnock();
        }

        return this;
    }

    /// <summary>
    /// 넉백 다 끝났으면 실행되는 함수
    /// </summary>
    public void FinishKnock()
    {
        m_animator.SetBool("IsDamage", false);

        if (m_controller.IsMoving())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.MOVE);
        else
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
    }

    private void OnTriggerEnter(Collider other)
    {
        //회피 안한 상태로 적 공격범위에 닿으면 데미지 판정
        if (other.tag == "EnemyAtkCollider" && m_owner.m_currentStat != PlayerFsmManager.PlayerENUM.DODGE)
        {
            m_enemyAtk = other.GetComponent<AtkCollider>();
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DAMAGE);
        }
    }
}
