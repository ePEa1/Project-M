using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;

public class MoveAction : BaseAction
{
    #region Inspector

    [SerializeField] Transform m_camTransform; //캐릭터 추적하는 카메라 트랜스폼

    #endregion

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

        Vector3 view = m_owner.transform.position - m_camTransform.transform.position;
        view.y = 0;

        Quaternion dir = GetDirection();

        Vector3 moveVec = (dir * view).normalized;

        Quaternion playerDir = dir * Quaternion.LookRotation(new Vector3(view.x, 0.0f, view.z));

        m_owner.transform.rotation = Quaternion.Slerp(m_owner.transform.rotation, playerDir, Time.deltaTime * PlayerStats.playerStat.m_curveSpeed);
        m_owner.transform.position += m_owner.transform.rotation * new Vector3(0.0f, 0.0f, -PlayerStats.playerStat.m_moveSpeed) * Time.deltaTime;

        Debug.Log("MoveAction.OnUpdateAction");

        return this;
    }

    #endregion

    #region Function

    Quaternion GetDirection()
    {
        int h = 0;
        if (Input.GetKey(m_controller.m_leftMove))
            h++;
        if (Input.GetKey(m_controller.m_rightMove))
            h--;

        int v = 0;
        if (Input.GetKey(m_controller.m_frontMove))
            v--;
        if (Input.GetKey(m_controller.m_backMove))
            v++;

        return Quaternion.LookRotation(new Vector3(h, 0, v));
    }

    #endregion
}