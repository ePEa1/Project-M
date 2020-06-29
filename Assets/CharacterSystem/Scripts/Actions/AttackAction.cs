﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;
using ProjectM.ePEa.CamSystem;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;

public class AttackAction : BaseAction
{
    #region Inspectors

    [SerializeField] PCAtkObject[] m_atkData; //공격 데이터
    [SerializeField] BoxCollider m_atkRange; //공격 범위 콜라이더
    [SerializeField] LayerMask m_wall;
    [SerializeField] LayerMask m_enemy;
    [SerializeField] AudioSource[] m_audio;

    #endregion

    #region Value

    int m_nowCombo = 0; //현재 타격 콤보
    int m_maxCombo; //최대 콤보
    int m_currentCombo = 0; //이벤트 실행시 기준 콤보

    bool m_nextAtk = false; //공격 예약이 되있는지 체크
    bool m_nextAtkOk = false; //다음 공격 예약이 가능한 상태인지 체크

    float m_atkTime = 0.0f; //공격 시작 후 경과된 시간

    Vector3 m_startPos; //공격 시작 지점 좌표
    Vector3 m_finishPos; //공격 도착 지점 좌표
    float m_ac; //애니메이션커브에 곱할 값
    Quaternion dir; //회전 값
    Vector3 viewVec;//방향 벡터

    int m_effNum = 0; //n번째 이펙트
    int m_sfxNum = 0; //n번째 효과음
    int m_colNum = 0; //n번째 타격범위

    #endregion

    protected override BaseAction OnStartAction()
    {
        m_nowCombo = 0; //공격 콤보 초기화
        m_currentCombo = 0;

        m_animator.ResetTrigger("Atk");
        m_animator.SetBool("IsAtk", true);
        m_nextAtk = true;

        m_effNum = 0;
        m_sfxNum = 0;
        m_colNum = 0;

        NextAttacking();
        
        return this;
    }

    public override void EndAction()
    {
        //공격 예약해놨던거 다 초기화
        m_nextAtk = false;
        m_nextAtkOk = false;

        m_effNum = 0;
        m_sfxNum = 0;
        m_colNum = 0;

        m_nowCombo = 0; //공격 콤보 초기화
        m_currentCombo = 0;

        //애니메이터에 공격 취소 알림
        m_animator.SetBool("IsAtk", false);
        m_animator.ResetTrigger("Atk");
    }

    protected override void AnyStateAction()
    {   
    }

    protected override BaseAction OnUpdateAction()
    {
        ChangeActions();

        //다음 공격 할건지 체크
        NextAtkCheck();

        PCAtkObject atkData = m_atkData[m_currentCombo];

        //공격중 이동--------------------------------------------
        float before = Mathf.Lerp(0.0f, atkData.distance, atkData.distanceCurve.Evaluate(m_atkTime * m_ac));
        m_atkTime += Time.deltaTime;
        float after = Mathf.Lerp(0.0f, atkData.distance, atkData.distanceCurve.Evaluate(m_atkTime * m_ac));

        if (m_owner.m_currentStat == PlayerFsmManager.PlayerENUM.ATK)
            m_owner.transform.rotation = Quaternion.Euler(0.0f, m_owner.playerCam.transform.eulerAngles.y, 0.0f);

        //넉백 방향 지정-------------
        Vector3 atkVec = m_owner.transform.rotation * Vector3.forward;
        m_atkRange.GetComponent<AtkCollider>().knockVec = atkVec.normalized;
        //---------------------------

        Vector3 dir = Quaternion.Euler(0.0f, m_owner.playerCam.transform.eulerAngles.y, 0.0f) * new Vector3(0.0f, 0.0f, after - before);
        
        Vector3 tall = new Vector3(0.0f, PlayerStats.playerStat.m_hikingHeight + PlayerStats.playerStat.m_size, 0.0f);
        Vector3 fixedPos = FixedMovePos(m_owner.transform.position + tall, PlayerStats.playerStat.m_size, dir.normalized,
            after-before, m_wall);

        PCAtksData data = m_atkData[m_currentCombo].atkData[Mathf.Clamp(m_colNum, 0, m_atkData[m_currentCombo].atkData.Length - 1)];
        RaycastHit hit;
        if (!Physics.BoxCast(m_owner.transform.position + tall + m_owner.transform.rotation * Vector3.forward * -1.0f, new Vector3(data.colSize.x/2, 1.0f, 0.7f),
            m_owner.transform.rotation * Vector3.forward.normalized, out hit, Quaternion.Euler(0, m_owner.playerCam.transform.eulerAngles.y, 0), after - before + 1.0f, m_enemy))
        {
            m_owner.transform.position += dir + fixedPos;
        }
        else if (!Physics.BoxCast(m_owner.transform.position + tall + m_owner.transform.rotation * Vector3.forward * -1.0f, new Vector3(data.colSize.x/2, 1.0f, 0.7f),
            m_owner.transform.rotation * Vector3.forward.normalized, Quaternion.Euler(0, m_owner.playerCam.transform.eulerAngles.y, 0), 2.0f, m_enemy))
        {
            float d = Vector3.Dot(dir.normalized,
                (new Vector3(hit.point.x, 0.0f, hit.point.z) - new Vector3(m_owner.transform.position.x, 0.0f, m_owner.transform.position.z)).normalized);

            m_owner.transform.position += dir.normalized * (Vector3.Distance(new Vector3(hit.point.x, 0.0f, hit.point.z),
                new Vector3(m_owner.transform.position.x, 0.0f, m_owner.transform.position.z)) * d - 0.6f) + fixedPos;
            
        }
        //--------------------------------------------------------

        return this;
    }

    #region Function

    private void Awake()
    {
        //최대 콤보 설정
        m_maxCombo = m_atkData.Length;
    }

    /// <summary>
    /// 다른 액션으로 바뀌는 경우 정리
    /// </summary>
    void ChangeActions()
    {
        if (m_controller.IsDodge() && PlayerStats.playerStat.m_currentDodgeDelay == 0)
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DODGE);
        if (m_controller.IsRushAttack())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.RUSHATK);
        if (m_controller.IsDashAttack())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DASHATK);
        if (m_controller.IsBackDashAttack())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.BACKATK);
    }

    /// <summary>
    /// 공격 예약 가능 상태로 변경
    /// </summary>
    public void NextAtkOk()
    {
        m_nextAtkOk = true;
        m_nextAtk = false;
    }

    /// <summary>
    /// 공격 상태 종료
    /// </summary>
    public void EndAttack()
    {
        //m_nextAtkOk = false;

        if (!m_nextAtk)
        {
            if (m_controller.IsMoving)
                m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.MOVE);
            else m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
        }
    }

    /// <summary>
    /// 다음 공격 예약 체크
    /// </summary>
    void NextAtkCheck()
    {
        if (m_nextAtkOk && m_controller.IsAttack())
        {
            m_nextAtk = true;
        }
    }

    /// <summary>
    /// 공격 범위 콜라이더 활성화
    /// </summary>
    public void AtkTiming()
    {
        if (m_owner.m_currentStat == PlayerFsmManager.PlayerENUM.ATK)
        {
            PCAtksData data = m_atkData[m_currentCombo].atkData[m_colNum];

            m_atkRange.center = data.colCenter;
            m_atkRange.size = data.colSize;

            m_atkRange.GetComponent<AtkCollider>().isAttacking = false;
            m_atkRange.GetComponent<AtkCollider>().atkDamage = data.damage * PlayerStats.playerStat.m_atkPower;

            m_atkRange.GetComponent<AtkCollider>().Attacking();

            m_colNum++;
        }
    }

    /// <summary>
    /// 공격 이펙트 생성 이벤트
    /// </summary>
    public void AtkEff()
    {
        if (m_owner.m_currentStat == PlayerFsmManager.PlayerENUM.ATK)
        {
            PCAtksData effs = m_atkData[m_currentCombo].atkData[m_effNum];
            GameObject eff = Instantiate(effs.eff);
            eff.transform.rotation = Quaternion.Euler(0.0f, m_owner.transform.eulerAngles.y + 180, 0.0f) * Quaternion.Euler(effs.effDir);
            eff.transform.position = m_owner.transform.position + new Vector3(0.0f, effs.effPos.y, 0.0f)
                + m_owner.transform.rotation * new Vector3(effs.effPos.x, 0.0f, effs.effPos.z);
            m_effNum++;
        }
    }

    /// <summary>
    /// 다음 공격 예약한거 반영 이벤트
    /// </summary>
    public void NextAttacking()
    {
        m_nextAtkOk = false;

        if (m_nextAtk) //다음 공격 예약 했을 경우
        {
            m_effNum = 0;
            m_sfxNum = 0;
            m_colNum = 0;

            m_startPos = m_owner.transform.position;
            m_atkTime = 0.0f;
            m_animator.SetTrigger("Atk");
            m_ac = 1.0f / m_atkData[m_nowCombo].rushSpeed;

            m_currentCombo = m_nowCombo;

            m_nowCombo++;
            if (m_nowCombo >= m_atkData.Length)
                PlayerStats.playerStat.SetAtKDelay();
            if (m_nowCombo >= m_maxCombo)
            {
                m_nowCombo = 0;
            }

        }
    }

    /// <summary>
    /// 카메라 쉐이킹 이벤트
    /// </summary>
    public void Shaking()
    {
        //m_owner.playerCam.GetComponent<CharacterCam>().SetShake(m_atkData[m_currentCombo].shakeData);
        m_owner.playerCinemachine.GetComponent<CinemachineCheck>().GetCinemachineShake();
    }

    public void GetAtkGage()
    {
        PlayerStats.playerStat.GetAtkGage(m_atkData[m_currentCombo].atkData[m_colNum].getGage);
    }

    /// <summary>
    /// 공격 효과음 재생
    /// </summary>
    public void PlaySfx()
    {
        PCAtksData effs = m_atkData[m_currentCombo].atkData[m_sfxNum];

        m_audio[m_sfxNum].clip = effs.sfx;
        m_audio[m_sfxNum].Play();

        m_sfxNum++;
    }

    /// <summary>
    /// 마나회복 이벤트
    /// </summary>
    public void GetMp()
    {
        PlayerStats.playerStat.GetMp(m_atkData[m_currentCombo].atkData[m_colNum].getMp);
    }

    ///// <summary>
    ///// 공격 콜라이더 활성화
    ///// </summary>
    ///// <param name="atkCol"></param>
    ///// <returns></returns>
    //IEnumerator AtkColliderOnOff(BoxCollider atkCol)
    //{
    //    atkCol.GetComponent<AtkCollider>().isAttacking = false;
    //    atkCol.gameObject.SetActive(true);
    //    float t = 0.05f;

    //    while (t > 0.0f)
    //    {
    //        t -= Time.deltaTime;
    //        yield return true;
    //    }
    //    atkCol.gameObject.SetActive(false);

    //    yield return null;
    //}

    #endregion
}
