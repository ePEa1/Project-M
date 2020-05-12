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
        if (Util.Detect(manager.CloseSight, 1, manager.playerCC))
        {
            manager.SetState(AliceState.CHASE);
        }
        //if (!Util.Detect(manager.CloseSight, 1, manager.playerCC))
        //{
        //    manager.SetState(AliceState.CHASE);
        //}
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
    }
        
    void OneCloseAttack()
    {

    }
    void TwoCloseAttack()
    {

    }
    void FarAttack()
    {

    }
    void Summoning()
    {

    }
    void RushAttack()
    {

    }
    void Teleport()
    {

    }
}
