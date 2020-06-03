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
    public Collider DamageCol;
    public Animator anim;
    public Camera sight;
    public GameObject playerObj;
    public MobStat stat;


    //상태와 동시에 스크립트 저장
    Dictionary<DummyState, MonsterFSMState> states = new Dictionary<DummyState, MonsterFSMState>();


    private void Awake()
    {
        DamageCol = GetComponentInChildren<Collider>();
        anim = GetComponentInChildren<Animator>();
        sight = GetComponentInChildren<Camera>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        stat = GetComponent<MobStat>();


        //상태 추가
        states.Add(DummyState.IDLE, GetComponent<MonsterIDLE>());
        states.Add(DummyState.MOVE, GetComponent<MonsterMOVE>());
        states.Add(DummyState.CHASE, GetComponent<MonsterCHASE>());
        states.Add(DummyState.ATTACK, GetComponent<MonsterATTACK>());
        states.Add(DummyState.DEAD, GetComponent<MonsterDEAD>());
        states.Add(DummyState.FIRSTATK, GetComponent<MonsterATTACK>());
    }
    // Start is called before the first frame update
    //처음 스테이트 지정
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

    //이미 공격 구현되어있으니 안써도 됨.
    public void AttackCheck()
    { 
       // targetStat.ApplyDamage(stat);
    }


    //죽는 판정 다른 상태 스크립트에 적용할려면 manager.SetDead()라고 하면 됨.
    public void SetDead()
    {
        DamageCol.enabled = false;
        SetState(DummyState.DEAD);
    }


    //플레이어가 죽으면 일반 상태로 돌아옴

    public void NotifyTargetDead()
    {
        SetState(DummyState.IDLE);
        playerObj = null;
    }



}
