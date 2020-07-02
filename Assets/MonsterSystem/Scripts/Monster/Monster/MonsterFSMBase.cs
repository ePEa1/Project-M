using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MonsterState
{
    Idle = 0,
    Move, //1
    Chase,//2
    Attack,//3
    Damage,//4
    Dead,//5
    Alert//6
}


public class MonsterFSMBase : MonoBehaviour
{
    //CharacterController 와 Animator 컴포넌트를 제어하는 변수.
    public Animator anim;
    public Transform player;


        //개체(몬스터, 캐릭터 등)의 상태변화를 제어하는 변수.
        public MonsterState CHState;


    //개체의 상태가 바꼈는지 체크하는 변수.
    public bool isNewState;



    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }


    //모든 개체는 씬에 생성되는 순간 Idle 상태가 되며, FSMMain 코루틴 메소드를 실행한다.
    protected virtual void OnEnable()
    {
        CHState = MonsterState.Idle;
        StartCoroutine(FSMMain());

    }

    IEnumerator FSMMain()
    {
        //상태가 바뀌면 IEnumerator CHState.ToString() 메소드를 실행한다. 처음은 IEnumerator  Idle() 실행
        while (true)
        {
            isNewState = false;
            yield return StartCoroutine(CHState.ToString());
        }
    }

    //개체의 상태가 바뀔때마다 메소드가 실행된다.
    public void SetState(MonsterState newState)
    {
        isNewState = true;
        CHState = newState;

        //개체가 가진 Animator 컴포넌트의 state Parameters 에게 상태변화 값을 전달한다. 
        anim.SetInteger("CurrentState", (int)CHState);
    }




    //모든 개체는 Idle 상태를 가진다.
    protected virtual IEnumerator Idle()
    {
        do
        {
            //1프레임에 한번만 체크한다.
            yield return null;

        } while (!isNewState); //do 문 종료조건.
    }
    //protected virtual IEnumerator Move()
    //{
    //    do
    //    {
    //        yield return null;

    //    } while (!isNewState);
    //}
    //protected virtual IEnumerator Chase()
    //{
    //    do
    //    {
    //        yield return null;
    //    } while (!isNewState);
    //}


    //protected virtual IEnumerator Attack()
    //{
    //    do
    //    {
    //        yield return null;
    //    } while (!isNewState);
    //}

    //protected virtual IEnumerator Dead()
    //{
    //    do
    //    {
    //        yield return null;
    //    } while (!isNewState);
    //}
}
