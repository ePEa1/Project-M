using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : BaseAction
{
    protected override BaseAction OnStartAction()
    {
        Debug.Log("IdleAction.OnStartAction");

        return this;
    }

    public override void EndAction()
    {
        Debug.Log("IdleAction.OnEndAction");
    }

    protected override void AnyStateAction()
    {

    }

    protected override BaseAction OnUpdateAction()
    {
        if (m_controller.IsMoving())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.MOVE);
        }

        Debug.Log("IdleAction.OnUpdateAction");

        return this;
    }
}
