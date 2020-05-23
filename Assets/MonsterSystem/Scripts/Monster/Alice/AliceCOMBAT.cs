using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AliceAttackState
{
    Combat = 0,
    OneCloseAttack,
    TwoCloseAttack,

    //텔레포트 후의 공격들
    FarAttack,
    Summon,
    Rush,
    Teleport

}

public class AliceCOMBAT : AliceFSMState
{
    public AliceAttackState curAtkState;
    public AliceAttackState TeleportAfterState;
    public GameObject SummonMonster;
    public GameObject SummonPos;

    public int[] AttackOrder;
    public int CurFarAtkCut = 90;
    public int startAttack;
    public int teleportAft;
    public int Summons = 0;
    public Vector3 RushPos;

    public bool DontMove = false;
    public bool IsAttack = true;
    public bool IsTeleport = false;
    public bool IsRush = false;
    public bool IsSummon = false;
    public bool SummonEnd = false;
    public override void BeginState()
    {
        base.BeginState();
    }

    void Update()
    {
        if (!Util.Detect(transform.position, manager.playerObj.transform.position,4) && DontMove == false)
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

        if (groundCheck.sqrMagnitude > manager.attackRange * manager.attackRange && DontMove == false)
        {
            manager.SetState(AliceState.CHASE);

        }


        if(IsAttack == false)
        {
            Util.CKRotate(transform, manager.playerObj.transform.position, manager.rotateSpeed);

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            IsAttack = false;
            TeleportAfterState = AliceAttackState.Rush;
            CurPatternCheck(AliceAttackState.Teleport);
        }


        if (manager.CurAliceHP <= 0)
        {
            manager.SetState(AliceState.DEAD);
        }
    }
        
    public void RotatePlayer()
    {
        Util.CKRotate(transform, manager.playerObj.transform.position, manager.rotateSpeed);

    }



    //거리 체크
    public void DistanceCheck()
    {
        if (Util.Detect(transform.position, manager.playerObj.transform.position, 4))
        {

            IsAttack = true;
            CurPatternCheck(AliceAttackState.Combat);

        }
        if (!Util.Detect(transform.position, manager.playerObj.transform.position, 4) &&Util.Detect(transform.position, manager.playerObj.transform.position, 10) &&DontMove == false )
        {

            IsAttack = false;
            manager.SetState(AliceState.CHASE);

        }
        //if (!Util.Detect(transform.position, manager.playerObj.transform.position, 10))
        //{
        //    DontMove = true;
        //    CurPatternCheck(AliceAttackState.Rush);
        //}
    }


    public void AliceHPCheck()
    {
        if(IsAttack == true)
        {
            //bool로 다른 패턴 중에는 적용안하게하기 // 기본공격 근거리

            if (manager.CurAliceHP < CurFarAtkCut)//공격 체크에 -10 넣기
            { if (manager.CurAliceHP <= 50 && CurFarAtkCut == 50)
                {
                    IsAttack = false;
                    DontMove = true;
                    //TeleportAfterState = AliceAttackState.Summon;
                    CurPatternCheck(AliceAttackState.Summon);
                    CurFarAtkCut -= 10;
                }
                else
                {
                    IsAttack = false;
                    //TeleportAfterState = AliceAttackState.Rush;
                    //CurPatternCheck(AliceAttackState.Teleport);
                    TeleportAfterState = AliceAttackState.FarAttack;
                    CurPatternCheck(AliceAttackState.Teleport);
                    CurFarAtkCut -= 10;
                }
                return;
            }

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
        DontMove = false;
        if(IsRush == true)
        {
            IsRush = false;
        }
        CurPatternCheck(AliceAttackState.Combat);
    }
    public void ReturnDefaultAttack()//특정 패턴이 끝난 후 일반 공격으로 변경
    {
        IsAttack = true;
        DontMove = false;
        if (IsRush == true)
        {
            IsRush = false;
        }
        if(IsSummon == true)
        {
            IsSummon = false;
        }
        CurPatternCheck(AliceAttackState.Combat);
    }

    void SetRushPos()
    {
        RushPos = manager.playerObj.transform.position;

    }
    void RushAttack()
    {
        IsRush = true;

    }
    public void Teleport()
    {
        DontMove = true;
        
        transform.Translate(new Vector3(0, 0, -10));

        //컨트롤러, 모델링 끄고 위치 이동시켜서 다시 켜기

    }
    public void TeleportAfter()
    {
        Debug.Log("TeleportCheck");
        CurPatternCheck(TeleportAfterState);
        //텔레포트 후 공격 판정
    }


    void Summoning()
    {

        //보호막 생성
        int x, z;

        if (IsSummon == false && SummonEnd == false)
        {
            IsSummon = true;
            Summons = 10;
            transform.position = Vector3.MoveTowards(transform.position, SummonPos.transform.position, 2);
            for (int i = 0; i < 10; i++)
            {
                x = Random.Range(5, 20);
                z = Random.Range(5, 20);
                Instantiate(SummonMonster, new Vector3(SummonPos.transform.position.x + x, 0.9f, transform.transform.position.z + z), Quaternion.identity);
                
            }
        }
        else
            return;
    
    }

    void SummonEndCheck()
    {
        if (Summons <= 0)
        {
            SummonEnd = true;
            ReturnDefaultAttack();
            DistanceCheck();
            
        }
        else
            return;
    }
}
