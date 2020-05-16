using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;

public class MoveAction : BaseAction
{
    //true 가 방향 즉시 전환, false가 방향 서서히 전환
    [SerializeField] bool change = false;


    #region events

    protected override BaseAction OnStartAction()
    {
        return this;
    }

    public override void EndAction()
    {

    }

    protected override void AnyStateAction()
    {
        //애니메이션 이동부분 컨트롤
        if (m_controller.IsMoving())
            m_animator.SetBool("IsMoving", true);
        else m_animator.SetBool("IsMoving", false);

        //if (Input.GetKeyDown(KeyCode.M))
        //    change = !change;
    }

    protected override BaseAction OnUpdateAction()
    {
        if (!m_controller.IsMoving())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
        }
        if (m_controller.IsAttack())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.ATK);
        }
        if (m_controller.IsDodge() && PlayerStats.playerStat.m_currentDodgeDelay == 0)
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DODGE);
        }

        else if (m_controller.IsMoving())
        {
            Quaternion v = Quaternion.Euler(0, m_owner.playerCam.transform.eulerAngles.y, 0);

            Quaternion dir = m_controller.GetDirection();

            Quaternion playerDir = dir * v;

            m_owner.transform.rotation = Quaternion.Slerp(m_owner.transform.rotation, playerDir, Time.deltaTime * PlayerStats.playerStat.m_curveSpeed);

            if (change)
                m_owner.transform.position += playerDir * new Vector3(0.0f, 0.0f, -PlayerStats.playerStat.m_moveSpeed) * Time.deltaTime;
            else
                m_owner.transform.position += m_owner.transform.rotation * new Vector3(0.0f, 0.0f, -PlayerStats.playerStat.m_moveSpeed) * Time.deltaTime;
        }

        return this;
    }

    #endregion
}