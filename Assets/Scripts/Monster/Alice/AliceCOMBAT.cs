using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceCOMBAT : AliceFSMState
{

    public int[] AttackOrder;
    public int[] FarAttackOrder = { 90, 80, 70, 60, 50, 40, 30, 20, 10 };//이러는게 나을지 원거리 공격 후 curAliceHP -10을 해서 다시 체크하는지
    public int CurFarAtkCut = 90;
    public int curAttck;
    public int startAttack;
    public override void BeginState()
    {
        base.BeginState();
    }

    void Update()
    {
        if (!Util.Detect(transform.position, manager.playerObj.transform.position,4))
        {
            manager.SetState(AliceState.CHASE);
            return;
        }

        //if (!Util.Detect(manager.CloseSight, 1, manager.playerCC))
        //{
        //    manager.SetState(AliceState.CHASE);
        //}
        Vector3 destination = manager.playerObj.transform.position;

        Vector3 destinationposition = new Vector3(destination.x - transform.position.x, 0, destination.z - transform.position.z);
        Vector3 diff = destination - destinationposition;
        Vector3 groundCheck = diff - destination;

        if (groundCheck.sqrMagnitude > manager.attackRange * manager.attackRange)
        {
            manager.SetState(AliceState.CHASE);

        }

        CurPatternCheck();
        Util.CKRotate(transform, manager.playerObj.transform.position, manager.rotateSpeed);



    }
        
    void AliceHPCheck()
    {
        if (manager.CurAliceHP < 100)//bool로 다른 패턴 중에는 적용안하게하기 // 기본공격 근거리
        {
            Debug.Log(curAttck);
            curAttck = Random.Range(1, 3);
        }
        if (manager.CurAliceHP < CurFarAtkCut)//공격 체크에 -10 넣기
        {
            curAttck = 3;

        }
    }
    void CurPatternCheck()
    {
        switch (curAttck)
        {
            case 0://대기
                manager.anim.SetInteger("curAttack", 0);
                break;
            case 1://근접 공격 1
                manager.anim.SetInteger("curAttack", 1);
                break;
            case 2://근접 공격 2
                manager.anim.SetInteger("curAttack", 2);
                break;
            case 3://원거리 공격
                manager.anim.SetInteger("curAttack", 3);
                break;
            case 4://소환술
                manager.anim.SetInteger("curAttack", 4);
                break;
            case 5://돌진
                manager.anim.SetInteger("curAttack", 5);
                break;
            case 6://텔레포트
                manager.anim.SetInteger("curAttack", 6);
                break;
        }
    }


    void OneCloseAttack()
    {

    }

    void TwoCloseAttack()
    {

    }
    void FarAttack()
    {
        CurFarAtkCut -= 10;
    }
    void Summoning()
    {
    }
    void RushAttack()
    {
        Util.CKMove(manager.gameObject, manager.transform, manager.playerObj.transform.position, 20, manager.rotateSpeed);
    }
    void Teleport()
    {
        
        //컨트롤러, 모델링 끄고 위치 이동시켜서 다시 켜기
    }
}
