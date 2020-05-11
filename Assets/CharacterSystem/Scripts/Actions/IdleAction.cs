using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : BaseAction
{
    protected override BaseAction OnStartAction()
    {
        return this;
    }

    public override void EndAction()
    {
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

        return this;
    }
}
