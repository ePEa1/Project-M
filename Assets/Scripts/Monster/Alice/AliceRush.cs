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

            transform.position = Vector3.MoveTowards(transform.position, RushPos, 2);

            Vector3 destinationposition = new Vector3(RushPos.x - transform.position.x, 0, RushPos.z - transform.position.z);

            Vector3 diff = RushPos - destinationposition;
            Vector3 groundCheck = diff - RushPos;

            if (groundCheck.sqrMagnitude <= 0.5f)
            {
                IsRush = false;
                CurPatternCheck(AliceAttackState.Combat);
            }
        }
    }
}
