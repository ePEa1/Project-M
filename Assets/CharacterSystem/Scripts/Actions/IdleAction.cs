using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;
using UnityEngine.SceneManagement;

public class IdleAction : BaseAction
{
    protected override BaseAction OnStartAction()
    {
        return this;
    }

    public override void EndAction()
    {
    }

    protected override void AnyStateAction()
    {
        //씬 재시작
        if (Input.GetKeyDown(KeyCode.J))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    protected override BaseAction OnUpdateAction()
    {
        //어느 상태로도 이동할 수 있도록 처리
        if (m_controller.IsMoving())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.MOVE);
        }
        if (m_controller.IsAttack())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.ATK);
        }
        if (m_controller.IsDodge() && PlayerStats.playerStat.m_currentDodgeDelay == 0)
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DODGE);
        }
        if (m_controller.IsRushAttack())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.RUSHATK);
        if (m_controller.IsRightDashAttack() || m_controller.IsLeftDashAttack())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DASHATK);
        }
        if (m_controller.IsBackDashAttack())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.BACKATK);
        }
        return this;
    }
}

//※처리 구조※
//대기 상태이므로 이 다음 어떤 행동을 취하든지 바로 상태가 바뀔 수 있도록 키입력 받자마자 상태변경 함수 실행