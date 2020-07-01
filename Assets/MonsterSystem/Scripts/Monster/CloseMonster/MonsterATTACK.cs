using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterATTACK : MonsterFSMState
{

    Vector3 m_startPos;
    Vector3 m_endPos;


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
        AttackCol.enabled = false;
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
        m_nowDelay = manager.m_atkDelay;
        m_time = 0.0f;
        m_startPos = transform.position;
        m_endPos = transform.position + (target.position - transform.position).normalized * m_rushRange;

        GameObject eff = Instantiate(m_eff);
        eff.transform.parent = transform;
        eff.transform.localPosition = new Vector3(0.0f, 1.0f, 1.0f);

        transform.rotation = Quaternion.LookRotation(m_endPos - m_startPos);
        manager.m_atkCollider.knockPower = 5.0f;
        manager.m_atkCollider.knockVec = (m_endPos - m_startPos).normalized;
    }


    //공격 콜리더 생성
    IEnumerator SetCollider()
    {
        AttackCol.enabled = true;
        yield return new WaitForSeconds(0.2f);
        AttackCol.enabled = false;
    }
}
