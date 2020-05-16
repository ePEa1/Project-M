using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AliceAttackState
{
    Combat = 0,
    OneCloseAttack,
    TwoCloseAttack,
    FarAttack,
    Summon,
    Rush,
    Teleport

}
public class AliceCOMBAT : AliceFSMState
{
    public AliceAttackState curAtkState;
    public int[] AttackOrder;
    public int CurFarAtkCut = 90;
    public int startAttack;
    public int teleportAft;

    public bool IsAttack = true;
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

        if(IsAttack == true)
        {
            AliceHPCheck();

        }

        if(IsAttack == false)
        {
            Util.CKRotate(transform, manager.playerObj.transform.position, manager.rotateSpeed);

        }


    }
        
    public void RotatePlayer()
    {
        Util.CKRotate(transform, manager.playerObj.transform.position, manager.rotateSpeed);

    }
    public void FarCheck()
    {
        if (!Util.Detect(transform.position, manager.playerObj.transform.position, 4))
        {
            IsAttack = false;
            //CurPatternCheck(AliceAttackState.Combat);
            manager.SetState(AliceState.CHASE);
            

        }
    }

    public void CloseCheck()
    {
        if (Util.Detect(transform.position, manager.playerObj.transform.position, 4))
        {
            IsAttack = true;
        }
    }

    void AliceHPCheck()
    {
        if(IsAttack == true)
        {
                //bool로 다른 패턴 중에는 적용안하게하기 // 기본공격 근거리

            if (manager.CurAliceHP < CurFarAtkCut)//공격 체크에 -10 넣기
            {
                IsAttack = false;
                CurPatternCheck(AliceAttackState.FarAttack);
                return;
            }
            else
            {
                int curAttack;
                curAttack = Random.Range(1, 3);
                if(curAttack == 1)
                {
                    CurPatternCheck(AliceAttackState.OneCloseAttack);
                }
                else if(curAttack == 2)
                {
                    CurPatternCheck(AliceAttackState.TwoCloseAttack);
                }
            }
        }
    }

    public void CurPatternCheck(AliceAttackState state)
    {
        switch (state)
        {
            case AliceAttackState.Combat://대기
                manager.anim.SetInteger("curAttack", 0);
                break;
            case AliceAttackState.OneCloseAttack://근접 공격 1
                manager.anim.SetInteger("curAttack", 1);
                break;
            case AliceAttackState.TwoCloseAttack://근접 공격 2
                manager.anim.SetInteger("curAttack", 2);
                break;
            case AliceAttackState.FarAttack://원거리 공격
                manager.anim.SetInteger("curAttack", 3);
                break;
            case AliceAttackState.Summon://소환술
                manager.anim.SetInteger("curAttack", 4);
                break;
            case AliceAttackState.Rush://돌진
                manager.anim.SetInteger("curAttack", 5);
                break;
            case AliceAttackState.Teleport://텔레포트
                manager.anim.SetInteger("curAttack", 6);
                break;
        }
    }

    public void SetCOMBATState()
    {
        manager.anim.SetInteger("curAttack", 0);
    }

    void OneCloseAttack()
    {

    }

    void TwoCloseAttack()
    {

    }
    public void FarAttack()
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
    public void TeleportAfter()
    {
        //텔레포트 후 공격 판정
    }
}
