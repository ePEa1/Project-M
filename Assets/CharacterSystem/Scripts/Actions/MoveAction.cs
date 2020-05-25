using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;

public class MoveAction : BaseAction
{
    #region Inspector

    [SerializeField] LayerMask m_wall;

    #endregion

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
            Vector3 moveVec = m_owner.transform.rotation * new Vector3(0.0f, 0.0f, -PlayerStats.playerStat.m_moveSpeed) * Time.deltaTime;

            //이동---------------------------------------------
            Vector3 fixedVec = Vector3.zero;
            fixedVec += FixedMovePos(m_owner.transform.position, PlayerStats.playerStat.m_size, (m_owner.transform.rotation * -Vector3.forward).normalized,
                    PlayerStats.playerStat.m_moveSpeed * Time.deltaTime, m_wall);

            m_owner.transform.position += moveVec + fixedVec;
            //--------------------------------------------------
        }
        
        return this;
    }

    #endregion
}