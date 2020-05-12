using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceIDLE : AliceFSMState
{
    public override void BeginState()
    {
        base.BeginState();
    }

    private void Update()
    {
        if(Util.Detect(manager.CloseSight, 1, manager.playrCC) || manager.PlayerIsAttack == true)
        {
            manager.SetState(AliceState.COMBAT);
            return;
        }
    }
}
