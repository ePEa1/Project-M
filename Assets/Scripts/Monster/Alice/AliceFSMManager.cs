using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AliceState
{
    IDLE = 0,
    CHASE,
    COMBAT,
    ATTACK,
    DEAD
}

public class AliceFSMManager : MonoBehaviour,IFSMManager
{
    public AliceState curState;
    public AliceState startState;
    public CharacterController cc;
    public Animator anim;
    public Camera sight;
    public CharacterController playrCC;


    public bool PlayerIsAttack = false;


    Dictionary<AliceState, AliceFSMState> states = new Dictionary<AliceState, AliceFSMState>();

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        sight = GetComponent<Camera>();
        playrCC = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

        states.Add(AliceState.IDLE, GetComponent<AliceIDLE>());
        states.Add(AliceState.DEAD, GetComponent<AliceDEAD>());
        states.Add(AliceState.COMBAT, GetComponent<AliceCOMBAT>());
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
