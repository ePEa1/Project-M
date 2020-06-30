using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;
using System;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;


public class BackAtkAction : BaseAction
{
    [SerializeField] LayerMask m_wall;
    [SerializeField] AudioSource atkSound;

    [SerializeField] PCAtkObject m_atkData;

    Vector3 m_startPos;
    Vector3 m_finishPos;

    int m_colNum = 0;

    [SerializeField] GameObject m_backAtkCol;
    float m_curbackAtk;

    GameObject m_atkObject;

    protected override BaseAction OnStartAction()
    {
        m_curbackAtk = 0;
        m_colNum = 0;

        PlayerStats.playerStat.UseMp(PlayerStats.playerStat.m_backMp);
        PlayerStats.playerStat.ResetAtkDelay();

        m_animator.SetTrigger("BackAtk");
        m_animator.SetBool("IsBackAtk", true);

        Vector3 view = m_owner.transform.position - m_owner.playerCam.position;
        view.y = 0.0f;
        view = view.normalized;

        Quaternion dir = Quaternion.LookRotation(new Vector3(0, 0, 1.0f));

        Quaternion playerDir = dir * Quaternion.LookRotation(new Vector3(view.x, 0, view.z));
        Vector3 playerVec = playerDir * new Vector3(0, 0, -m_atkData.distance);

        m_owner.transform.rotation = playerDir;

        m_startPos = m_owner.transform.position;
        m_finishPos = m_startPos + playerVec;

        return this;
    }

    public override void EndAction()
    {
        m_curbackAtk = 0;
        m_animator.SetBool("IsBackAtk", false);
        m_animator.ResetTrigger("BackAtk");
    }

    protected override void AnyStateAction()
    {
    }

    protected override BaseAction OnUpdateAction()
    {
        ChangeActions();

        float value = 1.0f / m_atkData.rushSpeed;
        Vector3 beforePos = Vector3.Lerp(m_startPos, m_finishPos, m_atkData.distanceCurve.Evaluate(m_curbackAtk * value));
        m_curbackAtk += Time.deltaTime;
        Vector3 afterPos = Vector3.Lerp(m_startPos, m_finishPos, m_atkData.distanceCurve.Evaluate(m_curbackAtk * value));

        Vector3 tall = new Vector3(0.0f, PlayerStats.playerStat.m_hikingHeight + PlayerStats.playerStat.m_size, 0.0f);

        Vector3 fixedPos = FixedMovePos(m_owner.transform.position + tall, PlayerStats.playerStat.m_size, (afterPos - beforePos).normalized, Vector3.Distance(beforePos, afterPos),
            m_wall);

        m_owner.transform.position += afterPos - beforePos + fixedPos;
        return this;
    }

    /// <summary>
    /// 액션 즉시 변경 이벤트
    /// </summary>
    void ChangeActions()
    {
        if (m_controller.IsDodge() && PlayerStats.playerStat.m_currentDodgeDelay == 0)
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DODGE);
        }
        if (m_controller.IsRushAttack())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.RUSHATK);
        if (m_controller.IsDashAttack())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DASHATK);
    }

    public void SetCollider()
    {
        Vector3 view = m_owner.transform.position - m_owner.playerCam.position;
        view.y = 0.0f;
        view = view.normalized;

        m_atkObject = Instantiate(m_atkData.atkData[m_colNum].eff);
        m_atkObject.transform.rotation = Quaternion.LookRotation(view);
        m_atkObject.transform.position = m_owner.transform.position;
        m_atkObject.GetComponent<AtkCollider>().atkDamage = m_atkData.atkData[0].damage * PlayerStats.playerStat.m_atkPower;
        m_atkObject.GetComponent<BackAtkCol>().Setup();

        m_atkObject.GetComponent<AtkCollider>().AddEvent(gameObject.GetComponent<BackAtkAction>().GetAtkGage);
        m_atkObject.GetComponent<AtkCollider>().isAttacking = false;
    }

    public void DeleteCollider()
    {
        if (m_atkObject != null)
            Destroy(m_atkObject);
    }

    public void EndBackAtk()
    {
        if (m_controller.IsMoving)
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.MOVE);
        }
        else
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
        }
    }

    public void SetSound()
    {
        atkSound.clip = m_atkData.atkData[m_colNum].sfx;
        atkSound.Play();
    }


    IEnumerator DelayDashAtk()
    {
        m_owner.DelayDashAtk = true;
        yield return new WaitForSeconds(PlayerStats.playerStat.m_timeDashAtk);
        m_owner.DelayDashAtk = false;
    }

    public void GetAtkGage()
    {
        PlayerStats.playerStat.GetAtkGage(m_atkData.atkData[m_colNum].getGage);
    }
}
