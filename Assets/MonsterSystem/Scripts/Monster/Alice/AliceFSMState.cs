using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceFSMState : MonoBehaviour
{
    public AliceFSMManager manager;
    
    public virtual void BeginState()
    {

    }

    private void Awake()
    {
        manager = GetComponent<AliceFSMManager>();
    }
}
