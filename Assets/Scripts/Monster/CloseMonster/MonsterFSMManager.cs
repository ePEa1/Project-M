using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DummyState
{
    IDLE = 0,
    MOVE,//1
    CHASE,//2
    ATTACK,//3
    DAMAGE,//4
    DEAD,//5
    FIRSTATK
}

public class MonsterFSMManager : MonoBehaviour, IFSMManager
{
    public DummyState currentState;
    public DummyState startState;
    public CharacterController cc;
    public Animator anim;
    public Camera sight;
    public CharacterController playerCC;
    public MobStat stat;



    Dictionary<DummyState, MonsterFSMState> states = new Dictionary<DummyState, MonsterFSMState>();


    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        sight = GetComponentInChildren<Camera>();
        playerCC = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        stat = GetComponent<MobStat>();

        states.Add(DummyState.IDLE, GetComponent<MonsterIDLE>());
        states.Add(DummyState.MOVE, GetComponent<MonsterMOVE>());
        states.Add(DummyState.CHASE, GetComponent<MonsterCHASE>());
        states.Add(DummyState.ATTACK, GetComponent<MonsterATTACK>());
        states.Add(DummyState.DEAD, GetComponent<MonsterDEAD>());
        states.Add(DummyState.FIRSTATK, GetComponent<MonsterATTACK>());
    }
    // Start is called before the first frame update
    void Start()
    {
        SetState(startState);
    }
    public void SetState(DummyState newState)
    {
        if (currentState == DummyState.DEAD)
            return;

         foreach(MonsterFSMState state in states.Values)
        {
            state.enabled = false;
        }
        currentState = newState;
        states[currentState].enabled = true;
        states[currentState].BeginState();
        anim.SetInteger("CurrentState", (int)currentState);
        }

    public void AttackCheck()
    {
        //CharacterStat targetStat = playerCC.GetComponent<CharacterStat>();

       // targetStat.ApplyDamage(stat);
    }



    public void SetDead()
    {
        cc.enabled = false;
        SetState(DummyState.DEAD);
    }

    public void NotifyTargetDead()
    {
        SetState(DummyState.IDLE);
        playerCC = null;
    }



}
