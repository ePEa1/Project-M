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
    [SerializeField] float movePos;

    Vector3 playerVec;
    Vector3 m_startPos;
    Vector3 m_finishPos;

    public Collider DashAtkCol;
    float m_curdashAtk;

    protected override BaseAction OnStartAction()
    {
        CheckDistance();

        m_animator.SetTrigger("DashAtk");
        m_animator.SetBool("IsDashAtk", true);

        if (m_controller.IsMoving())
        {
            Vector3 view = m_owner.transform.position - m_owner.playerCam.position;
            view.y = 0.0f;

            Quaternion dir = m_controller.GetDirection();

            Quaternion playerDir = dir * Quaternion.LookRotation(new Vector3(view.x, 0, view.z));
            Vector3 playerVec = playerDir * new Vector3(0, 0, -movePos);

            m_owner.transform.rotation = playerDir;

            m_startPos = m_owner.transform.position;
            m_finishPos = m_startPos + playerVec;
        }
        else  
        {
            Vector3 playerVec = m_owner.transform.rotation * new Vector3(0, 0, -movePos);

            m_startPos = m_owner.transform.position;
            m_finishPos = m_startPos + playerVec;
        }

        return this;
    }
    public override void EndAction()
    {



    }

    //쓰지 않기
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
        Vector3 beforePos = Vector3.Lerp(m_startPos, m_finishPos, m_AtkDistance.Evaluate(m_curdashAtk * speed));
        m_curdashAtk += Time.deltaTime;
        Vector3 afterPos = Vector3.Lerp(m_startPos, m_finishPos, m_AtkDistance.Evaluate(m_curdashAtk * speed));

        Vector3 tall = new Vector3(0.0f, PlayerStats.playerStat.m_hikingHeight + PlayerStats.playerStat.m_size, 0.0f);

        Vector3 fixedPos = FixedMovePos(m_owner.transform.position+ tall, PlayerStats.playerStat.m_size, (afterPos - beforePos).normalized, Vector3.Distance(beforePos, afterPos),
            m_wall);

        m_owner.transform.position += afterPos - beforePos + fixedPos;
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
        m_curdashAtk = 0;
        m_animator.SetBool("IsDashAtk", false);

    }

    public void CheckDistance()
    {
        Vector3 viewVec = m_owner.transform.position - m_owner.playerCam.transform.position;




        viewVec.y = 0;
        viewVec = viewVec.normalized;



    }
    

}
