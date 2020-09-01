using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;

public class BossNormalAtk : EnemyAction
{
    #region Inspectors

    [SerializeField] string m_aniName;
    [SerializeField] PCAtkObject m_atkObject;
    [SerializeField] AtkCollider[] m_atkCollider;
    [SerializeField] LayerMask m_wall;
    [SerializeField] AudioSource[] m_sfx;

    #endregion

    #region Value

    float m_time = 0.0f;
    float m_curve = 100.0f;

    int m_atkNum = 0;
    int m_effNum = 0;
    int m_sfxNum = 0;

    Vector3 m_startPos;
    Vector3 m_finishPos;

    bool m_isView = true;

    #endregion

    #region Base

    private void Awake()
    {
        m_atkNum = 0;
        m_effNum = 0;
        m_sfxNum = 0;
    }

    protected override void EndAction()
    {
        m_animator.ResetTrigger(m_aniName);
        m_animator.SetBool("Is" + m_aniName, false);

        m_atkNum = 0;
        m_effNum = 0;
        m_sfxNum = 0;
        m_time = 0.0f;
    }

    protected override void StartAction()
    {
        m_startPos = transform.position;
        m_finishPos = transform.position;

        m_animator.SetTrigger(m_aniName);
        m_animator.SetBool("Is" + m_aniName, true);

        m_curve = m_atkObject.rushSpeed;
        m_isView = true;
    }

    protected override void UpdateAction()
    {
        Moving();
        if (m_isView)
        {
            SetLoot();
            m_owner.transform.rotation = Quaternion.LookRotation((m_finishPos - m_startPos).normalized);
        }
    }

    #endregion

    #region Functions

    void Moving()
    {
        Vector3 before = Vector3.Lerp(m_startPos, m_finishPos, m_atkObject.distanceCurve.Evaluate(m_time / m_curve));
        m_time += Time.deltaTime;
        Vector3 after = Vector3.Lerp(m_startPos, m_finishPos, m_atkObject.distanceCurve.Evaluate(m_time / m_curve));

        Vector3 fixedPos = FixedMovePos(transform.position, 0.75f, (after - before).normalized, Vector3.Distance(before, after), m_wall);

        m_owner.transform.position += after - before + fixedPos;
    }

    Vector3 ToTargetView()
    {
        Vector3 targetView = m_owner.m_player.transform.position - m_owner.transform.position;
        targetView.y = 0;

        return targetView.normalized;
    }

    /// <summary>
    /// startPos, finishPos 설정
    /// </summary>
    void SetLoot()
    {
        m_startPos = transform.position;
        m_finishPos = m_startPos + ToTargetView() * m_atkObject.distance;
    }

    #endregion

    #region AnimationEvents

    /// <summary>
    /// 공격 판정 타이밍 이벤트
    /// </summary>
    public void AtkTiming()
    {
        PCAtksData data = m_atkObject.atkData[m_atkNum];

        m_atkCollider[m_atkNum].atkDamage = data.damage;
        m_atkCollider[m_atkNum].knockVec = (m_finishPos - m_startPos).normalized;
        m_atkCollider[m_atkNum].Attacking();
        m_atkNum++;
    }

    /// <summary>
    /// 이펙트 생성 이벤트
    /// </summary>
    public void CreateEff()
    {
        PCAtksData data = m_atkObject.atkData[m_effNum];
        GameObject eff = Instantiate(data.eff);
        eff.transform.rotation = Quaternion.Euler(0.0f, m_owner.transform.eulerAngles.y, 0.0f) * Quaternion.Euler(data.effDir);
        eff.transform.position = m_owner.transform.position + Quaternion.Euler(0.0f, m_owner.transform.eulerAngles.y, 0.0f) * data.effPos;
        m_effNum++;
    }

    /// <summary>
    /// 효과음 재생 이벤트
    /// </summary>
    public void PlaySFX()
    {
        m_sfx[m_sfxNum].clip = m_atkObject.atkData[m_sfxNum].sfx;
        m_sfx[m_sfxNum].Play();
        m_sfxNum++;
    }

    public void EndAtk() => m_owner.ChangeStat("Move");

    public void OnView() => m_isView = true;

    public void CloseView() => m_isView = false;

    #endregion
}
