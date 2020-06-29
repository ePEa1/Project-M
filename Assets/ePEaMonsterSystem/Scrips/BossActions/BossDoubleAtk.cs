using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;

public class BossDoubleAtk : EnemyAction
{
    #region Inspectors

    [SerializeField] PCAtkObject m_atkObject;
    [SerializeField] AtkCollider m_atkCollider;
    [SerializeField] LayerMask m_wall;
    [SerializeField] AudioSource[] m_sfx;

    #endregion

    #region Value

    float m_time = 0.0f;
    float m_curve;

    int m_atkNum = 0;
    int m_effNum = 0;
    int m_sfxNum = 0;

    Vector3 m_startPos;
    Vector3 m_finishPos;

    #endregion

    private void Awake()
    {
        m_atkNum = 0;
        m_effNum = 0;
        m_sfxNum = 0;
    }

    protected override void EndAction()
    {
        m_animator.ResetTrigger("DoubleAtk");
        m_animator.SetBool("IsDoubleAtk", false);

        m_atkNum = 0;
        m_effNum = 0;
        m_sfxNum = 0;
    }

    protected override void StartAction()
    {
        m_startPos = transform.position;
        m_finishPos = transform.position;

        m_animator.SetTrigger("DoubleAtk");
        m_animator.SetBool("IsDoubleAtk", true);

        AnimatorStateInfo info = m_animator.GetCurrentAnimatorStateInfo(0);
        m_curve = info.length * info.speed; 
    }

    protected override void UpdateAction()
    {
        Moving();
    }

    void Moving()
    {
        Vector3 before = Vector3.Lerp(m_startPos, m_finishPos, m_atkObject.distanceCurve.Evaluate(m_time * m_curve));
        m_time += Time.deltaTime;
        Vector3 after = Vector3.Lerp(m_startPos, m_finishPos, m_atkObject.distanceCurve.Evaluate(m_time * m_curve));

        Vector3 fixedPos = FixedMovePos(transform.position, 0.75f, (after - before).normalized, Vector3.Distance(before, after), m_wall);

        transform.position += after - before + fixedPos;
    }

    Vector3 ToTargetView()
    {
        Vector3 targetView = m_owner.m_player.transform.position - m_owner.transform.position;
        targetView.y = 0;

        return targetView.normalized;
    }

    #region Events

    /// <summary>
    /// startPos, finishPos 설정
    /// </summary>
    public void SetLoot()
    {
        m_startPos = transform.position;
        m_finishPos = m_startPos + ToTargetView() * m_atkObject.distance;
    }

    /// <summary>
    /// 공격 판정 타이밍 이벤트
    /// </summary>
    public void AtkTiming()
    {
        
    }

    public void CreateEff()
    {
        GameObject eff = Instantiate(m_atkObject.atkData[m_effNum].eff);
        eff.transform.position = m_owner.transform.rotation * m_atkObject.atkData[m_effNum].effPos;

    }

    public void PlaySFX()
    {

    }

    #endregion
}
