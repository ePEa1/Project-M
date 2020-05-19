using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;

public class DodgeAction : BaseAction
{
    #region Inspector

    [SerializeField] AnimationCurve m_dodgeCurve; //회피 이동애니 커브

    #endregion

    #region Value

    float m_nowDodge = 0.0f; //회피 이벤트 지속 시간 체크용

    Vector3 m_startPos;
    Vector3 m_finishPos;

    #endregion

    protected override BaseAction OnStartAction()
    {
        if (m_controller.IsMoving())
        {
            Vector3 view = m_owner.transform.position - m_owner.playerCam.position;
            view.y = 0.0f;

            Quaternion dir = m_controller.GetDirection();

            Quaternion playerDir = dir * Quaternion.LookRotation(new Vector3(view.x, 0, view.z));
            Vector3 playerVec = playerDir * new Vector3(0, 0, -PlayerStats.playerStat.m_dodgeDistance);

            //회피 방향에 맞게 회전
            m_owner.transform.rotation = playerDir;
             
            //회피 이동 애니메이션을 위한 출발, 도착 포지션 갱신
            m_startPos = m_owner.transform.position;
            m_finishPos = m_startPos + playerVec;
        }
        else //키입력 안됬으면 
        {
            //현재 바라보는 방향으로 회피
            Vector3 playerVec = m_owner.transform.rotation * new Vector3(0, 0, -PlayerStats.playerStat.m_dodgeDistance);

            m_startPos = m_owner.transform.position;
            m_finishPos = m_startPos + playerVec;
        }

        //회피 애니메이션 실행
        m_animator.SetBool("IsDodge", true);

        //회피 딜레이 설정
        PlayerStats.playerStat.SetDodgeDelay(PlayerStats.playerStat.m_dodgeDelay);

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
        //커브에서 0~1 값으로 만드려고 곱하기 위해 만들어둔거
        float ac = 1.0f / PlayerStats.playerStat.m_dodgeTime;

        m_nowDodge += Time.deltaTime;

        //캐릭터 포지션 이동
        m_owner.transform.position = Vector3.Lerp(m_startPos, m_finishPos, m_dodgeCurve.Evaluate(m_nowDodge * ac));

        //회피 끝났으면 회피종료함수 실행
        //이거 애니메이션 이벤트로 뺄거라 애니 나오면 지워야됨
        //if (m_nowDodge > PlayerStats.playerStat.m_dodgeTime)
        //{
        //    FinishDodge();
        //}

        return this;
    }

    #region Function

    /// <summary>
    /// 회피 끝나면 실행시킬 내용들
    /// </summary>
    public void FinishDodge()
    {
        m_nowDodge = 0.0f;

        if (m_controller.IsMoving())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.MOVE);
        }
        else m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);

        m_animator.SetBool("IsDodge", false);
    }

    #endregion
}