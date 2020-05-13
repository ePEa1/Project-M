using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;

public class DamageAction : BaseAction
{
    AtkCollider m_enemyAtk; //적 공격범위 데이터

    Vector3 m_startPos; //맞기 시작한 위치
    Vector3 m_finishPos; //맞은 후 도달하는 위치

    float m_knockTime = 0.0f;
    float m_ac;

    protected override BaseAction OnStartAction()
    {
        //체력 차감
        PlayerStats.playerStat.m_currentHp -= m_enemyAtk.atkDamage;

        //맞아서 밀려나는 위치 설정
        m_startPos = m_owner.transform.position;
        m_finishPos = m_startPos + m_enemyAtk.knockVec * m_enemyAtk.knockPower;
        
        m_knockTime = 0.0f;
        m_ac = 1.0f / m_enemyAtk.knockTime;

        return this;
    }

    public override void EndAction()
    {
    }

    protected override void AnyStateAction()
    {
    }

    protected override BaseAction OnUpdateAction()
    {


        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        //회피 안한 상태로 적 공격범위에 닿으면 데미지 판정
        if (other.tag == "enemyAtk" && m_owner.m_currentStat != PlayerFsmManager.PlayerENUM.DODGE)
        {
            m_enemyAtk = other.GetComponent<AtkCollider>();
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DAMAGE);
        }
    }
}
