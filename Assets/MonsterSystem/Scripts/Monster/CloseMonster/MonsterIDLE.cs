using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIDLE : MonsterFSMState
{
    [SerializeField] float idleTime = 5.0f;//대기하는 시간
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
        //6이상 멀어졌을 때 쫓아간다.
        if (Util.Detect(transform.position, manager.playerObj.transform.position,6))
        {
            manager.SetState(DummyState.CHASE);
            return;
        }
        elapsedTime += Time.deltaTime;

        //일정 시간이 지나면 움직이는 함수
        if(elapsedTime >= idleTime)
        {
            elapsedTime = 0;
            move.Moving = true;
            manager.SetState(DummyState.MOVE);
        }
    }
}
