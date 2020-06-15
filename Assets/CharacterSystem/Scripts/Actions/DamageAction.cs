using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;
using UnityEngine.Events;
using System;
using static ProjectM.ePEa.CustomFunctions.CustomFunction;

public class DamageAction : BaseAction
{
    #region Inspector

    [SerializeField] AnimationCurve m_knockAC; //넉백 이동 커브
    [SerializeField] GameObject m_damEff; //피격 이펙트
    [SerializeField] LayerMask m_wall;
    [SerializeField] AudioSource m_damSound;
    public PlayerHP playerhp;

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

        //피격 시 이벤트 실행
        OnDamAnimation();

        //피격 사운드 재생
        m_damSound.volume = DataController.Instance.effectSound;

        m_damSound.Play();


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

        Vector3 beforePos = Vector3.Lerp(m_startPos, m_finishPos, m_knockAC.Evaluate(m_knockTime * m_ac));
        m_knockTime = Mathf.Min(m_maxTime, m_knockTime + Time.deltaTime);
        Vector3 afterPos = Vector3.Lerp(m_startPos, m_finishPos, m_knockAC.Evaluate(m_knockTime * m_ac));

        Vector3 tall = new Vector3(0.0f, PlayerStats.playerStat.m_hikingHeight + PlayerStats.playerStat.m_size, 0.0f);
        Vector3 fixedPos = FixedMovePos(m_owner.transform.position + tall, PlayerStats.playerStat.m_size, (afterPos - beforePos).normalized,
            Vector3.Distance(afterPos, beforePos), m_wall);

        m_owner.transform.position += afterPos - beforePos + fixedPos;

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
        if (other.tag == "EnemyAtkCollider" && DamageOk())
        {
            m_enemyAtk = other.GetComponent<AtkCollider>();
            playerhp.DamageDecrease();
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DAMAGE);
        }
    }

    /// <summary>
    /// 데미지 받을 수 있는지 체크
    /// </summary>
    /// <returns></returns>
    bool DamageOk()
    {
        if (m_owner.m_currentStat == PlayerFsmManager.PlayerENUM.DODGE || m_owner.m_currentStat == PlayerFsmManager.PlayerENUM.DASHATK ||
            m_owner.m_currentStat == PlayerFsmManager.PlayerENUM.DAMAGE)
            return false;
        else return true;
    }

    /// <summary>
    /// 피격 즉시 실행되는 이벤트 함수
    /// </summary>
    public void OnDamAnimation()
    {
        //피격 연출효과 실행
        m_owner.playerCam.GetComponent<RGBCameraScript>().PlayAnimation();
;    }
}
