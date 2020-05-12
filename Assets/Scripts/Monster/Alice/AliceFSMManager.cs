﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AliceState
{
    IDLE = 0,
    CHASE,//1
    COMBAT,//2
    DAMAGED,//3
    DEAD//4
}
public enum AliceAttackPattern
{
    WAIT = 0,
    CLOSEATTACKOne,//1
    CLOSEATTACKTwo,//2
    FARATTACK,//3
    SUMMONING,//4
    RUSH,//5
    TELEPORT,//6
}
public class AliceFSMManager : MonoBehaviour,IFSMManager
{
    public AliceState curState;
    public AliceState startState;
    public AliceAttackPattern curAttack;
    public AliceAttackPattern startAttack;
    public CharacterController cc;
    public Animator anim;
    public Camera CloseSight;
    public CharacterController playerCC;

    public float moveSpeed = 3;
    public float rotateSpeed =540.0f;
    public float fallSpeed;
    public float attackRange = 2.0f;


    public bool PlayerIsAttack = false;


    Dictionary<AliceState, AliceFSMState> states = new Dictionary<AliceState, AliceFSMState>();

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        CloseSight = GetComponentInChildren<Camera>();
        playerCC = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

        states.Add(AliceState.IDLE, GetComponent<AliceIDLE>());
        states.Add(AliceState.CHASE, GetComponent<AliceCHASE>());
        states.Add(AliceState.COMBAT, GetComponent<AliceCOMBAT>());
        states.Add(AliceState.DEAD, GetComponent<AliceDEAD>());
    }
    // Start is called before the first frame update
    void Start()
    {
        SetState(startState);
    }

    public void SetState(AliceState newState)
    {
        if (curState == AliceState.DEAD)
            return;
        foreach(AliceFSMState state in states.Values)
        {
            state.enabled = false;
        }
        curState = newState;
        states[curState].enabled = true;
        states[curState].BeginState();
        anim.SetInteger("curState", (int)curState);
    }

    public void SetAttackStatae(AliceAttackPattern newState)
    {
        anim.SetInteger("curAttack", (int)curAttack);
    }

    public void SetDead()
    {
        cc.enabled = false;
        SetState(AliceState.DEAD);
    }
    
    public void NotifyTargetDead()
    {
        SetState(AliceState.IDLE);
        
    }


}
