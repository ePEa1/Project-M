using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterATTACK : MonsterFSMState
{
    public MonsterCHASE chase;//쫓아가는 상태 
    public Collider AttackCol;//공격 콜리더
    public override void BeginState()
    {
        base.BeginState();

    }
    private void Start()
    {


    }
    private void Awake()
    {
        AttackCol.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!Util.Detect(transform.position, manager.playerObj.transform.position,6))
        {
            manager.SetState(MonsterState.CHASE);
            return;
        }

        Vector3 destination = manager.playerObj.transform.position;

        Vector3 destinationposition = new Vector3(destination.x - transform.position.x, 0, destination.z - transform.position.z);
        Vector3 diff = destination - destinationposition;
        Vector3 groundCheck = diff - destination;


        //일정 거리 멀어졌을 때 쫓아가기
        if (groundCheck.sqrMagnitude > manager.stat.attackRange * manager.stat.attackRange)
            {
                manager.SetState(MonsterState.CHASE);

            }

        Util.CKRotate(transform, manager.playerObj.transform.position, manager.stat.rotateSpeed);
        
    }

    //공격 콜리더 생성
    IEnumerator SetCollider()
    {
        AttackCol.enabled = true;
        yield return new WaitForSeconds(0.2f);
        AttackCol.enabled = false;
    }
}
