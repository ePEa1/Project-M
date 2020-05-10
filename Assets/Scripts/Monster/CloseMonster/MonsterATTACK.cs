using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterATTACK : MonsterFSMState
{
    public MonsterCHASE chase;
    public override void BeginState()
    {
        base.BeginState();

    }

    // Update is called once per frame
    void Update()
    {
        if (!Util.Detect(manager.sight, 1, manager.playerCC))
        {
            manager.SetState(DummyState.CHASE);
            return;
        }
        Vector3 destination = manager.playerCC.transform.position;

        Vector3 destinationposition = new Vector3(destination.x - transform.position.x, 0, destination.z - transform.position.z);
        Vector3 diff = destination - destinationposition;
        Vector3 groundCheck = diff - destination;



        if (groundCheck.sqrMagnitude > manager.stat.attackRange * manager.stat.attackRange)
            {
                manager.SetState(DummyState.CHASE);

            }

        Util.CKRotate(transform, manager.playerCC.transform.position, manager.stat.rotateSpeed);
        
    }
}
