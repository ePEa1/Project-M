using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceRush : AliceCOMBAT
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsRush == true)
        {
            Vector3 destination = manager.playerObj.transform.position;

            Util.CKMove(manager.gameObject, manager.transform, RushPos, 50, manager.rotateSpeed);

            Vector3 destinationposition = new Vector3(destination.x - transform.position.x, 0, destination.z - transform.position.z);
            Vector3 diff = destination - destinationposition;
            Vector3 groundCheck = diff - destination;

            if (groundCheck.sqrMagnitude <= 0.5f)
            {
                IsRush = false;
                CurPatternCheck(AliceAttackState.Combat);
            }
        }
    }
}
