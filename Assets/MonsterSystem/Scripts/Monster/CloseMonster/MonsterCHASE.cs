using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCHASE : MonsterFSMState
{

    

    public override void BeginState()
    {
        base.BeginState();
    }

    // Update is called once per frame
    void Update()
    {
        //일정 기간 멀어지면 IDLE상태가 되는데 필요하면 주석 풀기
        //if (!Util.Detect(transform.position, manager.playerObj.transform.position))
        //{
        //    manager.SetState(DummyState.IDLE);
        //    return;
        //}
        
        //플레이어 포지션
        Vector3 destination = manager.playerObj.transform.position;

        //플레이어를 향해 움직이기
        Util.CKMove(
            manager.gameObject,
            transform,
            destination,
            manager.stat.moveSpeed,
            manager.stat.rotateSpeed
            );


        //일정거리 안에 들어왔는지 체크
        Vector3 destinationposition = new Vector3(destination.x - transform.position.x, 0, destination.z - transform.position.z);
        Vector3 diff = destination - destinationposition;
        Vector3 groundCheck = diff - destination;

            if (groundCheck.sqrMagnitude <= manager.stat.attackRange * manager.stat.attackRange)
            {
                manager.SetState(DummyState.ATTACK);
            }
        

    }

}
