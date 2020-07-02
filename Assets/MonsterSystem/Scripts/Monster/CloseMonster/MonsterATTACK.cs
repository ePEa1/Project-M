using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProjectM.ePEa.CustomFunctions.CustomFunction;


public class MonsterATTACK : MonsterFSMState
{
    [SerializeField] AnimationCurve m_atkAc;
    Vector3 m_startPos;
    Vector3 m_endPos;
    Vector3 m_destPos;

    bool IsAtk = false;

    float m_time = 0.0f;
    float m_nowDelay = 0.0f;
    public MonsterCHASE chase;//쫓아가는 상태 
    public Collider AttackCol;//공격 콜리더
    [SerializeField] GameObject m_eff;
    public override void BeginState()
    {
        base.BeginState();

    }
    private void Awake()
    {
        //AttackCol.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!Util.Detect(transform.position, manager.playerObj.transform.position,6))
        {
            manager.SetState(MonsterState.CHASE);
            return;
        }
        Vector3 destination = manager.playerObj.transform.position;
        Vector3 destinationposition = new Vector3(destination.x - transform.position.x, 0, destination.z - transform.position.z);
        Vector3 diff = destination - destinationposition;
        Vector3 groundCheck = diff - destination;
        //일정 거리 멀어졌을 때 쫓아가기
        if (groundCheck.sqrMagnitude > manager.stat.attackRange * manager.stat.attackRange)
            {
                manager.SetState(MonsterState.CHASE);
            }
        Util.CKRotate(transform, manager.playerObj.transform.position, manager.stat.rotateSpeed);
        
    }

    void AtkStart()
    {
       // isAtk = false;
        m_nowDelay = manager.stat.m_atkDelay;
        m_time = 0.0f;
        m_startPos = transform.position;
        m_endPos = transform.position + (manager.playerObj.transform.position - transform.position).normalized * manager.stat.m_rushRange;

        GameObject eff = Instantiate(m_eff);
        eff.transform.parent = transform;
        eff.transform.localPosition = new Vector3(0.0f, 1.0f, 1.0f);

        transform.rotation = Quaternion.LookRotation(m_endPos - m_startPos);
        manager.m_atkCollider.knockPower = 5.0f;
        manager.m_atkCollider.knockVec = (m_endPos - m_startPos).normalized;
    }
    void Atk()
    {
        float ac = 1.0f / manager.stat.m_rushSpeed;

        Vector3 beforePos = Vector3.Lerp(m_startPos, m_endPos, m_atkAc.Evaluate(m_time * ac));
        m_time += Time.deltaTime;
        Vector3 afterPos = Vector3.Lerp(m_startPos, m_endPos, m_atkAc.Evaluate(m_time * ac));

        Vector3 fixedPos = FixedMovePos(transform.position, 0.6f, (afterPos - beforePos).normalized, Vector3.Distance(beforePos, afterPos), manager.m_wall);

        if (!float.IsNaN(fixedPos.x))
            transform.position += afterPos - beforePos + fixedPos;
        else
            transform.position += afterPos - beforePos;

        if (m_time >= manager.stat.m_colliderOpenTime && !IsAtk)
        {
            manager.m_atkCollider.Attacking();
            IsAtk = true;
        }

        if (m_time >= manager.stat.m_rushSpeed)
        {
            AtkEnd();
        }
    }
    void AtkEnd()
    {
        manager.SetState(MonsterState.IDLE);
        m_time = 0.0f;
        m_destPos = new Vector3(transform.position.x, 0.0f, transform.position.z);

    }
    //공격 콜리더 생성
    IEnumerator SetCollider()
    {
        AttackCol.enabled = true;
        yield return new WaitForSeconds(0.2f);
        AttackCol.enabled = false;
    }
}
