using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIDLE : MonsterFSMState
{
    public float idleTime = 5.0f;
    public float elapsedTime;
    public MonsterMOVE move;
    public override void BeginState()
    {
        base.BeginState();
        elapsedTime = 0;
    }
    public void Awake()
    {
        move = GetComponent<MonsterMOVE>();
        manager = GetComponent<MonsterFSMManager>();
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (!Util.Detect(transform.position, manager.playerObj.transform.position,6))
        {
            manager.SetState(DummyState.CHASE);
            return;
        }
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= idleTime)
        {
            elapsedTime = 0;
            move.Moving = true;
            manager.SetState(DummyState.MOVE);
        }
    }
}
