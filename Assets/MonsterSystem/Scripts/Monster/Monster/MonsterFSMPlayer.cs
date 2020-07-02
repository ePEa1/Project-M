using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSMPlayer : MonsterFSMBase
{
    [Header("SeObject")]


    [Header("MonsterState")]
    public int currentHP = 100;
    public int maxHP = 100;
    public float attack = 40.0f;  // 공격력
    public float attackRange = 1.5f; // 공격범위



    protected override IEnumerator Idle()
    {
        do
        {
            yield return null;
        } while (!isNewState);
    }

    protected virtual IEnumerator Run()
    {
        do
        {
            yield return null;
        } while (!isNewState);
    }
    protected virtual IEnumerator AttackRun()
    {
        do
        {
            yield return null;
        } while (!isNewState);
    }


    protected virtual IEnumerator Attack()
    {
        do
        {
            yield return null;
        } while (!isNewState);
    }

    protected virtual IEnumerator Dead()
    {
        do
        {
            yield return null;
        } while (!isNewState);
    }

    protected virtual IEnumerator Skill1()
    {
        do
        {
            yield return null;
        } while (!isNewState);
    }

}
