using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterProto : MonoBehaviour
{
    #region Inspector

    [SerializeField] float m_atkRange; //공격 사거리
    [SerializeField] float m_backRange; //후퇴 거리
    [SerializeField] float m_atkDelay; //공격 딜레이

    [SerializeField] float m_moveSpeed; //이동속도

    [SerializeField] AnimationCurve m_atkAc; //공격 시 이동량 커브
    
    #endregion

    #region Value

    NavMeshAgent m_navi;
    Transform target; //쫓아갈 캐릭터
    
    enum state
    {
        MOVE,
        ATK,
        DAMAGE,
        DEAD
    }

    state m_nowState = state.MOVE;

    #endregion

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;

        m_navi = GetComponent<NavMeshAgent>();
        m_navi.SetDestination(target.position);
        m_navi.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_nowState)
        {
            case state.MOVE:
                Move();
                break;

            case state.ATK:
                Atk();
                break;

            case state.DAMAGE:
                break;

            case state.DEAD:
                break;
        }
    }

    void Move()
    {
        m_navi.SetDestination(target.position);
        Vector3 charPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 destPos = new Vector3(m_navi.destination.x, 0, m_navi.destination.z);
        if (Vector3.Distance(charPos, destPos) < m_atkRange)
        {
            m_navi.speed = 0;
            m_navi.velocity = Vector3.zero;
            //if ()
        }
        else
        {
            m_navi.speed = m_moveSpeed;
        }
    }

    void Atk()
    {

    }
}
