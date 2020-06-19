using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;
using System;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;


public class BackAtkAction : BaseAction
{
    [SerializeField] AnimationCurve m_AtkDistance;
    [SerializeField] LayerMask m_wall;
    [SerializeField] AudioSource atkSound;
    [SerializeField] GameObject m_atkEff;

    [SerializeField] Vector3 atkPos;
    [SerializeField] float speed;
    [SerializeField] float movePos;


    Vector3 playerVec;
    Vector3 m_startPos;
    Vector3 m_finishPos;

    [SerializeField] GameObject m_backAtkCol;
    float m_curbackAtk;

    GameObject m_atkObject;

    protected override BaseAction OnStartAction()
    {
        CheckDistance();
        m_curbackAtk = 0;

        PlayerStats.playerStat.UseMp(PlayerStats.playerStat.m_backMp);

        m_animator.SetTrigger("BackAtk");
        m_animator.SetBool("IsBackAtk", true);
        atkSound.volume = DataController.Instance.gameData.EffectSound / 100;

        Vector3 view = m_owner.transform.position - m_owner.playerCam.position;
        view.y = 0.0f;
        view = view.normalized;

        Quaternion dir = Quaternion.LookRotation(new Vector3(0, 0, 1.0f));

        Quaternion playerDir = dir * Quaternion.LookRotation(new Vector3(view.x, 0, view.z));
        Vector3 playerVec = playerDir * new Vector3(0, 0, -movePos);

        m_owner.transform.rotation = playerDir;

        m_startPos = m_owner.transform.position;
        m_finishPos = m_startPos + playerVec;

        return this;
    }

    public override void EndAction()
    {
        m_animator.SetBool("IsBackAtk", false);
        m_animator.ResetTrigger("BackAtk");
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

        float value = 1.0f / speed;
        Vector3 beforePos = Vector3.Lerp(m_startPos, m_finishPos, m_AtkDistance.Evaluate(m_curbackAtk * value));
        m_curbackAtk += Time.deltaTime;
        Vector3 afterPos = Vector3.Lerp(m_startPos, m_finishPos, m_AtkDistance.Evaluate(m_curbackAtk * value));

        Vector3 tall = new Vector3(0.0f, PlayerStats.playerStat.m_hikingHeight + PlayerStats.playerStat.m_size, 0.0f);

        Vector3 fixedPos = FixedMovePos(m_owner.transform.position + tall, PlayerStats.playerStat.m_size, (afterPos - beforePos).normalized, Vector3.Distance(beforePos, afterPos),
            m_wall);

        m_owner.transform.position += afterPos - beforePos + fixedPos;
        return this;
    }

    public void SetCollider()
    {
        Vector3 view = m_owner.transform.position - m_owner.playerCam.position;
        view.y = 0.0f;
        view = view.normalized;

        m_atkObject = Instantiate(m_backAtkCol);
        m_atkObject.transform.rotation = Quaternion.LookRotation(view);
        m_atkObject.transform.position = m_owner.transform.position;
        m_atkObject.GetComponent<BackAtkCol>().Setup();
        
        m_backAtkCol.GetComponent<AtkCollider>().isAttacking = false;
    }

    public void DeleteCollider()
    {
        if (m_atkObject != null)
            Destroy(m_atkObject);
    }

    /// <summary>
    /// 이펙트 생성 이벤트
    /// </summary>
    public void CreatEff()
    {
        GameObject effobj = Instantiate(m_atkEff);
        effobj.transform.parent = m_owner.transform;
        effobj.transform.position = m_owner.transform.position + atkPos;
    }

    public void EndBackAtk()
    {
        if (m_controller.IsMoving())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.MOVE);
            m_curbackAtk = 0;

        }
        else
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
            m_curbackAtk = 0;

        }
        //StartCoroutine(DelayDashAtk());
    }

    public void CheckDistance()
    {
        Vector3 viewVec = m_owner.transform.position - m_owner.playerCam.transform.position;
        viewVec.y = 0;
        viewVec = viewVec.normalized;
    }

    public void SetSound()
    {
        atkSound.volume = DataController.Instance.effectSound;
        atkSound.Play();
    }


    IEnumerator DelayDashAtk()
    {
        m_owner.DelayDashAtk = true;
        yield return new WaitForSeconds(PlayerStats.playerStat.m_timeDashAtk);
        m_owner.DelayDashAtk = false;
    }


}
