using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceDAMAGE : AliceFSMState
{

    public bool IsDamaged = false;
    public override void BeginState()
    {
        base.BeginState();
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (IsDamaged ==true)
        {
            IsDamageCheck();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AtkEvent")
        {
            IsDamaged = true;
        }
    }
    void IsDamageCheck()
    {
        Debug.Log("Isdamage");

    }
    
}
