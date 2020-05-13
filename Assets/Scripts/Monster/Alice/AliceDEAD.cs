using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceDEAD : AliceFSMState
{


    public override void BeginState()
    {
        base.BeginState();
        Destroy(gameObject, 10);
    }
}
