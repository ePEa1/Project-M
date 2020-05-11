using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;

public class MoveAction : BaseAction
{

    #region events

    protected override BaseAction OnStartAction()
    {
        Debug.Log("MoveAction.OnStartAction");

        return this;
    }

    public override void EndAction()
    {
        Debug.Log("MoveAction.OnEndAction");
    }

    protected override void AnyStateAction()
    {

    }

    protected override BaseAction OnUpdateAction()
    {
        if (!m_controller.IsMoving())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
        }

        else if (m_controller.IsMoving())
        {
            Quaternion v = Quaternion.Euler(0, m_owner.playerCam.transform.eulerAngles.y, 0);

            Quaternion dir = m_controller.GetDirection();

            Quaternion playerDir = dir * v;

            m_owner.transform.rotation = Quaternion.Slerp(m_owner.transform.rotation, playerDir, Time.deltaTime * PlayerStats.playerStat.m_curveSpeed);
            m_owner.transform.position += m_owner.transform.rotation * new Vector3(0.0f, 0.0f, -PlayerStats.playerStat.m_moveSpeed) * Time.deltaTime;
        }

        return this;
    }

    #endregion
}