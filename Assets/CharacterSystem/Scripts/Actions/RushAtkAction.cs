using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;
using ProjectM.ePEa.CamSystem;
using static ProjectM.ePEa.CustomFunctions.CustomFunction;
using ProjectM.ePEa.CamSystem;

public class RushAtkAction : BaseAction
{
    #region Inspector

    [SerializeField] PCAtkObject m_atkData;
    [SerializeField] BoxCollider m_atkCollider;
    [SerializeField] AudioSource[] m_audio;
    [SerializeField] LayerMask m_wall;
    [SerializeField] LayerMask m_enemy;
    #endregion

    #region Value

    Vector3 m_startPos;
    Vector3 m_endPos;
    Vector3 m_view;

    int m_colNum = 0;
    int m_effNum = 0;
    int m_sfxNum = 0;

    float ac = 1.0f;
    float m_time = 0.0f;

    bool m_isNextAction = false;
    PlayerFsmManager.PlayerENUM m_nextAction = PlayerFsmManager.PlayerENUM.IDLE;
    #endregion


    #region Event
    protected override BaseAction OnStartAction()
    {
        m_colNum = 0;
        m_effNum = 0;
        m_sfxNum = 0;

        m_time = 0;

        PlayerStats.playerStat.UseMp(PlayerStats.playerStat.m_rushMp);

        m_isNextAction = false;
        m_nextAction = PlayerFsmManager.PlayerENUM.IDLE;

        m_view = m_owner.transform.position - m_owner.playerCam.position;
        m_view.y = 0;
        m_view = m_view.normalized;

        m_startPos = m_owner.transform.position;
        m_endPos = m_startPos + m_view * m_atkData.distance;

        ac = 1 / m_atkData.rushSpeed;

        m_owner.transform.rotation = Quaternion.LookRotation(m_view);
        m_atkCollider.GetComponent<AtkCollider>().knockVec = m_view;

        m_animator.SetTrigger("Rush");
        m_animator.SetBool("IsRush", true);

        return this;
    }
    
    protected override void AnyStateAction()
    {

    }

    protected override BaseAction OnUpdateAction()
    {

        //이동------------------------------------------------------
        Vector3 before = Vector3.Lerp(m_startPos, m_endPos, m_atkData.distanceCurve.Evaluate(m_time));
        m_time += Time.deltaTime * ac;
        Vector3 after = Vector3.Lerp(m_startPos, m_endPos, m_atkData.distanceCurve.Evaluate(m_time));

        Vector3 tall = new Vector3(0, PlayerStats.playerStat.m_size + PlayerStats.playerStat.m_hikingHeight, 0);
        Vector3 fixedPos = FixedMovePos(m_owner.transform.position + tall, PlayerStats.playerStat.m_size, m_view,
            Vector3.Distance(before, after), m_wall);

        RaycastHit hit;

        if (!Physics.BoxCast(m_owner.transform.position + tall + m_owner.transform.rotation * Vector3.forward * -1.0f, new Vector3(3f, 1.0f, 1f), m_view, out hit,
            Quaternion.Euler(m_owner.transform.rotation.eulerAngles), Vector3.Distance(after, before)+1, m_enemy))
        {
            m_owner.transform.position += after - before + fixedPos;
        }
        else if (!Physics.BoxCast(m_owner.transform.position + tall + m_owner.transform.rotation * Vector3.forward * -1.0f, new Vector3(3f, 1.0f, 1f), m_view,
            Quaternion.Euler(m_owner.transform.rotation.eulerAngles), 0, m_enemy))
        {
            Vector3 dir = (new Vector3(hit.point.x, 0.0f, hit.point.z) - new Vector3(m_owner.transform.position.x, 0.0f, m_owner.transform.position.z)).normalized;
            float d = Vector3.Dot(dir, m_view);

            m_owner.transform.position += m_view * (Vector3.Distance(new Vector3(hit.point.x, 0.0f, hit.point.z),
                new Vector3(m_owner.transform.position.x, 0.0f, m_owner.transform.position.z)) * d - 0.6f) + fixedPos;
        }
        //-----------------------------------------------------------

        OtherActionCheck();

        if (m_isNextAction)
            NextActionInput();

        return this;
    }

    public override void EndAction()
    {
        m_isNextAction = false;
        m_nextAction = PlayerFsmManager.PlayerENUM.IDLE;
        m_time = 0;

        m_animator.ResetTrigger("Rush");
        m_animator.SetBool("IsRush", false);
    }

    /// <summary>
    /// 다음 행동 예약 받기 시작 이벤트
    /// </summary>
    public void NextActionOpen()
    {
        m_isNextAction = true;
    }

    /// <summary>
    /// 예약된 행동 적용 이벤트
    /// </summary>
    public void NextActionCheck()
    {
        if (m_nextAction != PlayerFsmManager.PlayerENUM.IDLE)
            m_owner.ChangeAction(m_nextAction);
    }

    /// <summary>
    /// 타격 콜라이더 활성화
    /// </summary>
    public void AtkHitTime()
    {
        PCAtksData data = m_atkData.atkData[m_colNum];
        m_atkCollider.GetComponent<AtkCollider>().atkDamage = data.damage;
        m_atkCollider.GetComponent<BoxCollider>().size = data.colSize;
        m_atkCollider.GetComponent<BoxCollider>().center = data.colCenter;
        m_atkCollider.GetComponent<AtkCollider>().isAttacking = false;

        m_atkCollider.GetComponent<AtkCollider>().Attacking();

        //StartCoroutine(AtkColliderOnOff(m_atkCollider));

        m_colNum++;
    }

    /// <summary>
    /// 공격 종료 이벤트
    /// </summary>
    public void EndAttack()
    {
        if (m_controller.IsMoving())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.MOVE);
        else
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
    }

    /// <summary>
    /// 이펙트 생성 이벤트
    /// </summary>
    public void CreateEff()
    {
        PCAtksData data = m_atkData.atkData[m_effNum];
        GameObject eff = Instantiate(data.eff);
        eff.transform.rotation = Quaternion.Euler(0.0f, m_owner.transform.eulerAngles.y + 180.0f, 0.0f) * Quaternion.Euler(data.effDir);
        eff.transform.position = m_owner.transform.position + Vector3.up * data.effPos.y + m_owner.transform.rotation * new Vector3(data.effPos.x, 0.0f, data.effPos.z);
        

        m_effNum++;
    }

    /// <summary>
    /// 효과음 재생 이벤트
    /// </summary>
    public void PlaySfx()
    {
        m_audio[m_sfxNum].clip = m_atkData.atkData[m_sfxNum].sfx;
        m_audio[m_sfxNum].Play();
        m_sfxNum++;
    }

    /// <summary>
    /// 적 타격 성공시 카메라 쉐이킹 효과
    /// </summary>
    public void Shaking()
    {
        m_owner.playerCam.GetComponent<CharacterCam>().SetShake(m_atkData.shakeData);
    }

    #endregion

    #region Function

    /// <summary>
    /// 액션 변경 모음
    /// </summary>
    void OtherActionCheck()
    {
        if (m_controller.IsDodge())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DODGE);
        if (m_controller.IsDashAttack())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DASHATK);
        if (m_controller.IsBackDashAttack())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.BACKATK);
    }

    /// <summary>
    /// 다음 행동 예약받기
    /// </summary>
    void NextActionInput()
    {
        if (m_controller.IsAttack())
            m_nextAction = PlayerFsmManager.PlayerENUM.ATK;
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
