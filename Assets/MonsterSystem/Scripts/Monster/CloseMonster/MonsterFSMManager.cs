using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum MonsterState
{
    IDLE = 0,
    MOVE,//1
    CHASE,//2
    ATTACK,//3
    DAMAGE,//4
    DEAD,//5
}

public class MonsterFSMManager : MonoBehaviour, IFSMManager
{
    [Header("MonsterSetObj")]
    public MonsterState currentState;//최근 상태
    [SerializeField] MonsterState startState;//시작 상태
    [SerializeField] Collider DamageCol;//데미지 콜라이더
    [SerializeField] public Animator anim;//애니메이터
    [SerializeField] public Camera sight;//몬스터 시야
    [SerializeField] public GameObject playerObj;//캐릭터 오브젝트
    [SerializeField] public MobStat stat;//몬스터 기본 상태
    [SerializeField] public AtkCollider m_atkCollider;
    [SerializeField] public GameObject m_damEff;
    [SerializeField] public LayerMask m_wall;

    [SerializeField] GameObject m_hpBox;
    [SerializeField] Image m_hpBar;
    [SerializeField] Image m_backhpBar;


    //상태와 동시에 스크립트 저장
    Dictionary<MonsterState, MonsterFSMState> states = new Dictionary<MonsterState, MonsterFSMState>();


    private void Awake()
    {
        DamageCol = GetComponentInChildren<Collider>();
        anim = GetComponentInChildren<Animator>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        stat = GetComponent<MobStat>();


        //상태 추가
        states.Add(MonsterState.IDLE, GetComponent<MonsterIDLE>());
        states.Add(MonsterState.MOVE, GetComponent<MonsterMOVE>());
        states.Add(MonsterState.CHASE, GetComponent<MonsterCHASE>());
        states.Add(MonsterState.ATTACK, GetComponent<MonsterATTACK>());
        states.Add(MonsterState.DEAD, GetComponent<MonsterDEAD>());
    }
    // Start is called before the first frame update
    //처음 스테이트 지정
    void Start()
    {
        SetState(startState);
    }

    public void Update()
    {
        m_hpBox.transform.rotation = Camera.main.transform.rotation;

    }
    public void SetState(MonsterState newState)
    {
        if (currentState == MonsterState.DEAD)
            return;

        foreach (MonsterFSMState state in states.Values)
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
        SetState(MonsterState.DEAD);
    }


    //플레이어가 죽으면 일반 상태로 돌아옴

    public void NotifyTargetDead()
    {
        SetState(MonsterState.IDLE);
        playerObj = null;
    }



}
