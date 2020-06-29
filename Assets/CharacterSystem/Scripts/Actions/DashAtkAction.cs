using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;
using System;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;


public class DashAtkAction : BaseAction
{
    [SerializeField] PCAtkObject m_atkData;

    [SerializeField] LayerMask m_wall;
    [SerializeField] AudioSource atkSound;
    
    Vector3 m_startPos;
    Vector3 m_finishPos;
    [SerializeField] Collider DashAtkCol;
    float m_curdashAtk;

    protected override BaseAction OnStartAction()
    {

        m_animator.SetTrigger("DashAtk");
        m_animator.SetBool("IsDashAtk", true);

        SetCollider();

        PlayerStats.playerStat.UseMp(PlayerStats.playerStat.m_widthMp);
        PlayerStats.playerStat.ResetAtkDelay();

        Vector3 view = m_owner.transform.position - m_owner.playerCam.position;
        view.y = 0.0f;
        view = view.normalized;

        Quaternion dir = Quaternion.LookRotation(new Vector3(m_controller.GetDirection().v.x, 0, 0));

        Quaternion playerDir = dir * Quaternion.LookRotation(new Vector3(view.x, 0, view.z));
        Vector3 playerVec = playerDir * new Vector3(0, 0, m_atkData.distance);

        m_owner.transform.rotation = playerDir;

        m_startPos = m_owner.transform.position;
        m_finishPos = m_startPos + playerVec;

        return this;
    }
    public override void EndAction()
    {
        m_curdashAtk = 0;
        DeleteCollider();
        m_animator.ResetTrigger("DashAtk");
        m_animator.SetBool("IsDashAtk", false);
    }

    //쓰지 않기
    protected override void AnyStateAction()
    {
    }

    protected override BaseAction OnUpdateAction()
    {

        float value = 1.0f / m_atkData.rushSpeed;
        Vector3 beforePos = Vector3.Lerp(m_startPos, m_finishPos, m_atkData.distanceCurve.Evaluate(m_curdashAtk * value));
        m_curdashAtk += Time.deltaTime;
        Vector3 afterPos = Vector3.Lerp(m_startPos, m_finishPos, m_atkData.distanceCurve.Evaluate(m_curdashAtk * value));

        Vector3 tall = new Vector3(0.0f, PlayerStats.playerStat.m_hikingHeight + PlayerStats.playerStat.m_size, 0.0f);

        Vector3 fixedPos = FixedMovePos(m_owner.transform.position+ tall, PlayerStats.playerStat.m_size, (afterPos - beforePos).normalized, Vector3.Distance(beforePos, afterPos),
            m_wall);

        m_owner.transform.position += afterPos - beforePos + fixedPos;
        return this;
    }

    public void SetCollider()
    {
        DashAtkCol.GetComponent<AtkCollider>().atkDamage = m_atkData.atkData[0].damage * PlayerStats.playerStat.m_atkPower;
        DashAtkCol.GetComponent<AtkCollider>().isAttacking = false;
        DashAtkCol.gameObject.SetActive(true);
    }
    public void DeleteCollider()
    {
        DashAtkCol.gameObject.SetActive(false);
    }
    public void CreatEff()
    {
        GameObject effobj = Instantiate(m_atkData.atkData[0].eff);
        effobj.transform.parent = m_owner.transform;
        effobj.transform.position = m_owner.transform.position;
        //이펙트 생성
    }

    /// <summary>
    /// 바로 바뀌는 액션
    /// </summary>
    void ChangeActions()
    {
        if (m_controller.IsBackDashAttack())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.BACKATK);
        if (m_controller.IsRushAttack())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.RUSHATK);
    }

    public void EndDashAtk()
    {
        if (m_controller.IsMoving)
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.MOVE);

        }
        else
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
        }
        
        StartCoroutine(DelayDashAtk());
    }
    
    public void SetSound()
    {
        atkSound.clip = m_atkData.atkData[0].sfx;
        atkSound.Play();
    }

    public void GetGage()
    {
        PlayerStats.playerStat.GetAtkGage(m_atkData.atkData[0].getGage);
    }

    IEnumerator DelayDashAtk()
    {
        m_owner.DelayDashAtk = true;
        yield return new WaitForSeconds(PlayerStats.playerStat.m_timeDashAtk);
        m_owner.DelayDashAtk = false;
    }

    IEnumerator DelayTimeScale()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0.7f;
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 1;
    }
}
