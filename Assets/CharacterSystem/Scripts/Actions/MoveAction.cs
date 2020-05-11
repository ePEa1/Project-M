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
            Vector3 view = m_owner.transform.position - m_owner.playerCam.transform.position;
            view.y = 0;

            Quaternion dir = m_controller.GetDirection();

            Vector3 moveVec = (dir * view).normalized;

            Quaternion playerDir = dir * Quaternion.LookRotation(new Vector3(view.x, 0.0f, view.z));

            m_owner.transform.rotation = Quaternion.Slerp(m_owner.transform.rotation, playerDir, Time.deltaTime * PlayerStats.playerStat.m_curveSpeed);
            var lastPos = m_owner.transform.position; 
            m_owner.transform.position += m_owner.transform.rotation * new Vector3(0.0f, 0.0f, -PlayerStats.playerStat.m_moveSpeed) * Time.deltaTime;
            Debug.Log("dist  : " + (m_owner.transform.position - lastPos).magnitude);
            Debug.Log("MoveAction.OnUpdateAction");
        }

        return this;
    }

    #endregion
}