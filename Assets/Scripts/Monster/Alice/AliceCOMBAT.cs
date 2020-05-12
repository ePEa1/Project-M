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
    }
    void TwoKnifeAttack()
    {

    }
    void OneForkAttack()
    {

    }
    void Summoning()
    {

    }
    void RushSpecial()
    {

    }
}
