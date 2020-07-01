using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MonsterState
{
    IDLE = 0,
    MOVE,//1
    CHASE,//2
    ATTACK,//3
    DAMAGE,//4
    DEAD
}

public class MonsterFSMManager : MonoBehaviour, IFSMManager
{
    public MonsterState currentState;//최근 상태
    public MonsterState startState;//시작 상태
    public Collider DamageCol;//데미지 콜라이더
    public Animator anim;//애니메이터
    public Camera sight;//몬스터 시야
    public GameObject playerObj;//캐릭터 오브젝트
    public MobStat stat;//몬스터 기본 상태




    private void Awake()
    {
        DamageCol = GetComponentInChildren<Collider>();
        anim = GetComponentInChildren<Animator>();
        sight = GetComponentInChildren<Camera>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        stat = GetComponent<MobStat>();


        //상태 추가
    }
    // Start is called before the first frame update
    //처음 스테이트 지정
    void Start()
    {
        SetState(startState);
    }
    public void SetState(MonsterState newState)
    {
        switch (newState)
        {
            case MonsterState.IDLE:
                break;
            case MonsterState.MOVE:
                break;
            case MonsterState.CHASE:
                break;
            case MonsterState.ATTACK:
                break;
            case MonsterState.DAMAGE:
                break;
            case MonsterState.DEAD:
                break;
        }
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
        SetState(MonsterState.DEAD);
    }


    //플레이어가 죽으면 일반 상태로 돌아옴

    public void NotifyTargetDead()
    {
        SetState(MonsterState.IDLE);
        playerObj = null;
    }



}
