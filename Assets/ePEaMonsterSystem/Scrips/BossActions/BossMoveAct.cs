using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveAct : EnemyAction
{
    protected override void EndAction()
    {

    }

    protected override void StartAction()
    {
        Debug.Log("Start - Move Action");
    }

    protected override void UpdateAction()
    {
        Debug.Log("Update - Move Action");
    }
}
