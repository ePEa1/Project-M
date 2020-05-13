using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceCOMBAT : AliceFSMState
{

    public int[] AttackOrder;
    public int curAttck;
    public int startAttack;
    public override void BeginState()
    {
        base.BeginState();
    }

    void Update()
    {
        if (!Util.Detect(transform.position, manager.playerCC.transform.position))
        {
            manager.SetState(AliceState.CHASE);
            return;
        }
        //if (!Util.Detect(manager.CloseSight, 1, manager.playerCC))
        //{
        //    manager.SetState(AliceState.CHASE);
        //}
        Vector3 destination = manager.playerCC.transform.position;

        Vector3 destinationposition = new Vector3(destination.x - transform.position.x, 0, destination.z - transform.position.z);
        Vector3 diff = destination - destinationposition;
        Vector3 groundCheck = diff - destination;



        if (groundCheck.sqrMagnitude > manager.attackRange * manager.attackRange)
        {
            manager.SetState(AliceState.CHASE);

        }

        Util.CKRotate(transform, manager.playerCC.transform.position, manager.rotateSpeed);
        switch (curAttck)
        {
            case 0://대기
                
                break;
            case 1://근접 공격 1
                OneCloseAttack();
                break;
            case 2://근접 공격 2
                TwoCloseAttack();
                break;
            case 3://원거리 공격
                FarAttack();
                break;
            case 4://소환술
                Summoning();
                break;
            case 5://돌진
                RushAttack();
                break;
            case 6://텔레포트
                Teleport();
                break;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            curAttck = 1;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            curAttck = 2;
        }
    }
        
    void OneCloseAttack()
    {
        manager.anim.SetInteger("curAttack", 1);

    }

    void TwoCloseAttack()
    {
        manager.anim.SetInteger("curAttack", 2);
    }
    void FarAttack()
    {
        manager.anim.SetInteger("curAttack", 3);

    }
    void Summoning()
    {
        manager.anim.SetInteger("curAttack", 4);
    }
    void RushAttack()
    {
        manager.anim.SetInteger("curAttack", 5);
    }
    void Teleport()
    {
        //컨트롤러, 모델링 끄고 위치 이동시켜서 다시 켜기
        manager.anim.SetInteger("curAttack", 6);
    }
}
