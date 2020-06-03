using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSMState : MonoBehaviour
{
    public MonsterFSMManager manager;

    //상태 시작하는 함수
    public virtual void BeginState()
    {

    }
    //매니저 공통으로 쓰이게 하는 함수.
    private void Awake()
    {
        manager = GetComponent<MonsterFSMManager>();
    }
    // Start is called before the first frame update
    //클래스 상속하면 매니저는 manager를 쓰면됨.
}
