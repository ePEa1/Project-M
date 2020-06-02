﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AtkCollider : MonoBehaviour
{
    public float atkDamage; //가할 데미지 수치
    public float knockPower; //넉백시킬 거리
    public Vector3 knockVec; //넉백시킬 방향
    public float knockTime; //넉백 유지시간

    [SerializeField] UnityEvent m_atkEvents; //공격이 들어갔을 때 실행시킬 이벤트
    public bool isAttacking { get; set; } //공격이 들어갔는지 체크

    private void Awake()
    {
        isAttacking = false;
    }

    /// <summary>
    /// 공격이 들어갔을 때 맞은 오브젝트쪽에서 실행시켜야 하는 함수
    /// </summary>
    /// <returns></returns>
    public bool AtkEvent()
    {
        if (!isAttacking)
        {
            m_atkEvents.Invoke();
            isAttacking = true;
            return true;
        }
        else return false;
    }
}

//※처리 구조※---------------------------------------
//맞는 쪽에서 이 스크립트에 들어가있는 정보를 가지고 피격 처리
//여기에 들어간 정보를 안쓰면 안쓰는대로 처리 가능(넉백 유지시간을 아예 사용을 안한다던가)
//맞은 오브젝트는 반드시 AtkEvent를 실행시켜야 함
//이벤트가 실행됬는지는 bool값을 반환해준 것으로 알 수 있음

//◈◈◈반드시 공격 범위 콜라이더가 들어있는 오브젝트에 넣어주어야 정상 작동 함◈◈◈