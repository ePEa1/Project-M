using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceSummons : MonsterFSMState
{
    public AliceCOMBAT master;
    public bool IsDead = false;

    // Start is called before the first frame update
    void Start()
    {
    }
    private void Awake()
    {
        manager = GetComponent<MonsterFSMManager>();
        master = GameObject.FindGameObjectWithTag("Alice").GetComponent<AliceCOMBAT>();
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.currentState == DummyState.DEAD)
        {
            if(IsDead == false)
            {
                master.Summons -= 1;
                IsDead = true;
                return;
            }

        }
    }
}
