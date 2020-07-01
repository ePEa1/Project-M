using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMOVE : MonsterFSMState
{

    public Vector3 destination;//이동할 포지션
    public Vector3 diff;//캐릭터와 몬스터 간의 거리
    //public Vector3 groundCheck;
    public bool Moving = false;//움직이는 체크인데 지금은 안씀

    public override void BeginState()
    {
        base.BeginState();
        //이동할 거리 랜덤 정의
        destination = new Vector3((transform.position.x+Random.Range(-10, 10)), transform.position.y, (transform.position.z+Random.Range(-10, 10)));
    }

    private void Awake()
    {
        manager = GetComponent<MonsterFSMManager>();
    }
    private void Start()
    {

    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (!Util.Detect(transform.position, manager.playerObj.transform.position,6))
        {
            manager.SetState(MonsterState.CHASE);
            return;
        }

        Util.CKMove(
            manager.gameObject,
            transform,
            destination,
            manager.stat.moveSpeed,
            manager.stat.rotateSpeed);

        //StartCoroutine("WaitForIt");


        //도착지 근처에 도달하면 일반 상태로 전환
        diff = destination - transform.position;
        if (diff.sqrMagnitude < 0.3f * 0.3f)
        {
            manager.SetState(MonsterState.IDLE);
        }


    }
}
