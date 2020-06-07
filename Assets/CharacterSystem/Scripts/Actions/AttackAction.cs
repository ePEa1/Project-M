using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;

public class AttackAction : BaseAction
{
    #region Inspectors
    
    [SerializeField] float[] m_atkDistance; //타격당 이동거리
    [SerializeField] AnimationCurve[] m_atkDistanceCurve; //타격당 이동 커브
    [SerializeField] float[] m_atkSpeed; //타격당 공격 이동 시간
    [SerializeField] BoxCollider[] m_atkRange; //타격당 공격 범위(콜라이더 오브젝트)
    [SerializeField] GameObject[] m_atkEff; //타격탕 생성시킬 이펙트
    [SerializeField] Vector3[] m_effPos; //타격당 생성시킬 이펙트 위치
    [SerializeField] Vector3[] m_effAngle; //타격당 생성시킬 이펙트 각도
    [SerializeField] AudioSource[] m_atkSfx; //타격당 효과음
    [SerializeField] LayerMask m_wall;
    [SerializeField] LayerMask m_enemy;

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

    

    #endregion

    protected override BaseAction OnStartAction()
    {
        m_nowCombo = 0; //공격 콤보 초기화
        m_currentCombo = 0;

        m_animator.ResetTrigger("Atk");
        m_animator.SetBool("IsAtk", true);
        m_nextAtk = true;
        NextAttacking();

        return this;
    }

    public override void EndAction()
    {
        //공격 예약해놨던거 다 초기화
        m_nextAtk = false;
        m_nextAtkOk = false;
        

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
        if (m_controller.IsDodge() && PlayerStats.playerStat.m_currentDodgeDelay == 0)
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DODGE);
        }

        //다음 공격 할건지 체크
        NextAtkCheck();

        //공격중 이동--------------------------------------------
        Vector3 beforePos = Vector3.Lerp(m_startPos, m_finishPos, m_atkDistanceCurve[m_currentCombo].Evaluate(m_atkTime * m_ac));
        m_atkTime += Time.deltaTime;
        Vector3 afterPos = Vector3.Lerp(m_startPos, m_finishPos, m_atkDistanceCurve[m_currentCombo].Evaluate(m_atkTime * m_ac));

        Vector3 tall = new Vector3(0.0f, PlayerStats.playerStat.m_hikingHeight + PlayerStats.playerStat.m_size, 0.0f);
        Vector3 fixedPos = FixedMovePos(m_owner.transform.position + tall, PlayerStats.playerStat.m_size, (afterPos - beforePos).normalized, Vector3.Distance(beforePos, afterPos),
            m_wall);

        RaycastHit hit;
        if (Physics.SphereCast(m_owner.transform.position + tall, PlayerStats.playerStat.m_size, (afterPos - beforePos).normalized, out hit,
            Vector3.Distance(beforePos, afterPos), m_enemy))
        {
            if (hit.point != Vector3.zero)
            {
                m_owner.transform.position = new Vector3(hit.point.x, m_owner.transform.position.y, hit.point.z) + new Vector3(hit.normal.x, 0.0f, hit.normal.z)
                    * PlayerStats.playerStat.m_size;
            }
        }
        else
            m_owner.transform.position += afterPos - beforePos + fixedPos;
        //--------------------------------------------------------

        return this;
    }

    #region Function

    private void Awake()
    {
        //최대 콤보 설정
        m_maxCombo = m_atkDistance.Length;
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
            if (m_controller.IsMoving())
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
    /// 공격 이동 거리 설정
    /// </summary>
    void SetPath()
    {
        if (m_autotarget.TargetOn)
        {
            viewVec =m_autotarget.targetObj.transform.position - m_owner.transform.position ;
            viewVec.y = 0;
            viewVec = viewVec.normalized;

            dir = Quaternion.LookRotation(-new Vector3(viewVec.x, 0, viewVec.z));
            m_owner.transform.rotation = dir;

            m_startPos = m_owner.transform.position;
            m_finishPos = m_startPos + viewVec * m_atkDistance[m_nowCombo];
        }
        else
        {
             viewVec = m_owner.transform.position - m_owner.playerCam.transform.position;
            viewVec.y = 0;
            viewVec = viewVec.normalized;

            dir = Quaternion.LookRotation(-new Vector3(viewVec.x, 0, viewVec.z));
            m_owner.transform.rotation = dir;

            m_startPos = m_owner.transform.position;
            m_finishPos = m_startPos + viewVec * m_atkDistance[m_nowCombo];
        }







    }

    /// <summary>
    /// 공격 범위 콜라이더 활성화
    /// </summary>
    public void AtkTiming()
    {
        if (m_owner.m_currentStat == PlayerFsmManager.PlayerENUM.ATK)
        {
            m_atkRange[m_currentCombo].GetComponent<AtkCollider>().isAttacking = false;
            StartCoroutine(AtkColliderOnOff(m_atkRange[m_currentCombo]));
            
            Vector3 atkVec = m_owner.transform.rotation * new Vector3(0.0f, 0.0f, -1.0f);

            m_atkRange[m_currentCombo].GetComponent<AtkCollider>().knockVec = atkVec.normalized;
        }
    }

    /// <summary>
    /// 공격 이펙트 생성 이벤트
    /// </summary>
    public void AtkEff()
    {
        if (m_owner.m_currentStat == PlayerFsmManager.PlayerENUM.ATK)
        {
            GameObject eff = Instantiate(m_atkEff[m_currentCombo]);
            eff.transform.rotation = Quaternion.Euler(0.0f, m_owner.transform.eulerAngles.y, 0.0f) * Quaternion.Euler(m_effAngle[m_currentCombo]);
            eff.transform.position = m_owner.transform.position + new Vector3(0.0f, m_effPos[m_currentCombo].y, 0.0f)
                + eff.transform.rotation * -new Vector3(m_effPos[m_currentCombo].x, 0.0f, m_effPos[m_currentCombo].z);
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
            m_atkTime = 0.0f;
            m_animator.SetTrigger("Atk");
            m_ac = 1.0f / m_atkSpeed[m_nowCombo];

            SetPath();
            m_currentCombo = m_nowCombo;

            m_nowCombo++;
            if (m_nowCombo >= m_maxCombo)
            {
                m_nowCombo = 0;
            }

        }
    }

    public void PlaySfx()
    {
        m_atkSfx[m_currentCombo].Play();
        m_atkSfx[m_currentCombo].volume = DataController.Instance.gameData.EffectSound/100;
    }

    /// <summary>
    /// 공격 콜라이더 활성화
    /// </summary>
    /// <param name="atkCol"></param>
    /// <returns></returns>
    IEnumerator AtkColliderOnOff(BoxCollider atkCol)
    {
        atkCol.GetComponent<AtkCollider>().isAttacking = false;
        atkCol.gameObject.SetActive(true);
        float t = 0.1f;

        while (t > 0.0f)
        {
            t -= Time.deltaTime;
            yield return true;
        }
        atkCol.gameObject.SetActive(false);

        yield return null;
    }

    #endregion
}
