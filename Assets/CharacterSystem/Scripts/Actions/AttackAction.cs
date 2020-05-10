using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BaseAction
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
        return this;
    }
}
