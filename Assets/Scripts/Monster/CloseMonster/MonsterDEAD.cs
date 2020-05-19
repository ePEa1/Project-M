using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDEAD : MonsterFSMState
{
    public override void BeginState()
    {
        base.BeginState();
        Destroy(gameObject,3);
    }
    // Start is called before the first frame update


}
