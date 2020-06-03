using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;


public class DashAtkAction : BaseAction
{
    [SerializeField] AnimationCurve m_AtkDistance;
    [SerializeField] LayerMask m_wall;

    [SerializeField] float speed;

    Vector3 m_startPos;
    Vector3 m_finishPos;

    public Collider DashAtkCol;
    float m_curdashAtk;

    protected override BaseAction OnStartAction()
    {
        m_startPos = m_owner.transform.position;
        m_finishPos = m_startPos * speed;

        m_animator.SetTrigger("DashAtk");
        m_animator.SetBool("IsDashAtk", true);
        return this;
    }
    public override void EndAction()
    {
        //m_animator.ResetTrigger("DashAtk");


    }

    //쓰지 않기
    protected override void AnyStateAction()
    {

    }

    protected override BaseAction OnUpdateAction()
    {
        float ac = 1.0f / PlayerStats.playerStat.m_dodgeTime;

        Vector3 beforePos = Vector3.Lerp(m_startPos, m_finishPos, m_AtkDistance.Evaluate(m_curdashAtk * ac));
        m_curdashAtk += Time.deltaTime;
        Vector3 afterPos = Vector3.Lerp(m_startPos, m_finishPos, m_AtkDistance.Evaluate(m_curdashAtk * ac));

        Vector3 fixedPos = FixedMovePos(m_owner.transform.position, PlayerStats.playerStat.m_size, (afterPos - beforePos).normalized, Vector3.Distance(beforePos, afterPos),
            m_wall);

        m_owner.transform.position += afterPos - beforePos + fixedPos;
        // throw new System.NotImplementedException();
        return this;
    }

    public void SetCollider()
    {
        DashAtkCol.enabled = true;
    }
    public void DeleteCollider()
    {
        DashAtkCol.enabled = false;
    }
    public void CreatEff()
    {
        //이펙트 생성
    }
    public void EndDashAtk()
    {
        if (m_controller.IsMoving())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.MOVE);

        }
        else
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
        }

        m_animator.SetBool("IsDashAtk", false);

    }

}
