using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceCHASE : AliceFSMState
{
    public override void BeginState()
    {
        base.BeginState();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Util.Detect(manager.CloseSight, 1, manager.playerCC))
        {
            manager.SetState(AliceState.CHASE);
            return;
        }

        Vector3 destination = manager.playerCC.transform.position;

        Util.CKMove(
            manager.cc,
            transform,
            destination,
            manager.moveSpeed,
            manager.rotateSpeed,
            manager.fallSpeed);



        Vector3 destinationposition = new Vector3(destination.x - transform.position.x, 0, destination.z - transform.position.z);
        Vector3 diff = destination - destinationposition;
        Vector3 groundCheck = diff - destination;

        if (groundCheck.sqrMagnitude <= manager.attackRange * manager.attackRange)
        {
            manager.SetState(AliceState.COMBAT);
        }
    }
}
