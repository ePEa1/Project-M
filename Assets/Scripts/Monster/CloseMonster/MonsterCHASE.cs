using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCHASE : MonsterFSMState
{

    

    public override void BeginState()
    {
        base.BeginState();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!Util.Detect(transform.position, manager.playerObj.transform.position))
        //{
        //    manager.SetState(DummyState.IDLE);
        //    return;
        //}

        Vector3 destination = manager.playerObj.transform.position;

        Util.CKMove(
            manager.gameObject,
            transform,
            destination,
            manager.stat.moveSpeed,
            manager.stat.rotateSpeed
            );



        Vector3 destinationposition = new Vector3(destination.x - transform.position.x, 0, destination.z - transform.position.z);
        Vector3 diff = destination - destinationposition;
        Vector3 groundCheck = diff - destination;

            if (groundCheck.sqrMagnitude <= manager.stat.attackRange * manager.stat.attackRange)
            {
                manager.SetState(DummyState.ATTACK);
            }
        

    }

}
