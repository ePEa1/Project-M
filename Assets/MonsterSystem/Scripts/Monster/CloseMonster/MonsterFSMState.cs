using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSMState : MonoBehaviour
{
    public MonsterFSMManager manager;


    public virtual void BeginState()
    {

    }

    private void Awake()
    {
        manager = GetComponent<MonsterFSMManager>();
    }
    // Start is called before the first frame update

}
