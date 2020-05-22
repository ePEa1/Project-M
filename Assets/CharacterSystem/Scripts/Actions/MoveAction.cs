using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;

public class MoveAction : BaseAction
{
    [SerializeField] LayerMask m_wall;

    #region events

    protected override BaseAction OnStartAction()
    {
        return this;
    }

    public override void EndAction()
    {

    }

    protected override void AnyStateAction()
    {
        //애니메이션 이동부분 컨트롤
        if (m_controller.IsMoving())
            m_animator.SetBool("IsMoving", true);
        else m_animator.SetBool("IsMoving", false);
    }

    protected override BaseAction OnUpdateAction()
    {
        if (!m_controller.IsMoving())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
        }
        if (m_controller.IsAttack())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.ATK);
        }
        if (m_controller.IsDodge() && PlayerStats.playerStat.m_currentDodgeDelay == 0)
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DODGE);
        }

        else if (m_controller.IsMoving())
        {
            Quaternion v = Quaternion.Euler(0, m_owner.playerCam.transform.eulerAngles.y, 0);

            Quaternion dir = m_controller.GetDirection();

            Quaternion playerDir = dir * v;

            m_owner.transform.rotation = Quaternion.Slerp(m_owner.transform.rotation, playerDir, Time.deltaTime * PlayerStats.playerStat.m_curveSpeed);

            Vector3 moveVec = m_owner.transform.rotation * new Vector3(0.0f, 0.0f, -PlayerStats.playerStat.m_moveSpeed) * Time.deltaTime;

            RaycastHit[] hits = Physics.SphereCastAll(m_owner.transform.position, 0.3f, (m_owner.transform.rotation * -Vector3.forward).normalized, PlayerStats.playerStat.m_moveSpeed * Time.deltaTime, m_wall);
            if (hits.Length>0)
            {
                Vector3 fixedVec = Vector3.zero;
                for(int i=0;i<hits.Length;i++)
                {
                    fixedVec += new Vector3(hits[i].normal.x, 0.0f, hits[i].normal.z).normalized * Vector3.Distance(Vector3.zero, moveVec) * Vector3.Dot(-moveVec.normalized, new Vector3(hits[i].normal.x, 0.0f, hits[i].normal.z).normalized);
                }
                m_owner.transform.position += moveVec + fixedVec;
            }
            else
            {
                m_owner.transform.position += moveVec;
            }
        }
        
        return this;
    }

    #endregion
}