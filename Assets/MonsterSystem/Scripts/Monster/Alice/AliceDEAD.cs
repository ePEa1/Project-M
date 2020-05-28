using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceDEAD : AliceFSMState
{


    public override void BeginState()
    {

        Destroy(gameObject, 0.5f);
    }
}
