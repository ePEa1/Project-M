﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;

public class MoveAction : BaseAction
{
    #region Inspector

    [SerializeField] LayerMask m_wall; //막히는 오브젝트 레이어
    [SerializeField] float m_aniBlendSpeed; //블렌딩 속도
    [SerializeField] AudioSource m_footSfx;
    [SerializeField] AudioClip[] m_footSounds;

    #endregion

    #region Value

    float m_gravity = 0.0f;
    float m_aniBlend = 0.0f;

    #endregion

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
            m_aniBlend = Mathf.Min(1, m_aniBlend + Time.deltaTime * m_aniBlendSpeed);
            //m_animator.SetBool("IsMoving", true);
        else m_aniBlend = Mathf.Max(0, m_aniBlend - Time.deltaTime * m_aniBlendSpeed);

        m_animator.SetFloat("Moving", m_aniBlend);

        m_controller.SetDirectionToKey();

        //중력(+경사면) 연산---------------------------------
        float hikingHeight = PlayerStats.playerStat.m_hikingHeight;
        float gravity = m_gravity * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(m_owner.transform.position + Vector3.up * hikingHeight, Vector3.down, out hit, hikingHeight + gravity, m_wall))
        {
            m_gravity = 0.0f;
            m_owner.transform.position = new Vector3(m_owner.transform.position.x, hit.point.y, m_owner.transform.position.z);
        }
        else
        {
            m_owner.transform.position += Vector3.down * gravity;
            m_gravity += Time.deltaTime * PlayerStats.playerStat.m_gravity;
        }
        //--------------------------------------------------------
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
        if (m_controller.IsRushAttack())
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.RUSHATK);
        if(m_controller.IsDashAttack())
        {
            m_owner.ChangeAction(PlayerFsmManager.PlayerENUM.DASHATK);
        }

        else if (m_controller.IsMoving())
        {
            Quaternion v = Quaternion.Euler(0, m_owner.playerCam.transform.eulerAngles.y, 0);

            Quaternion dir = m_controller.GetDirection().q;

            Quaternion playerDir = dir * v;

            m_owner.transform.rotation = Quaternion.Slerp(m_owner.transform.rotation, playerDir, Time.deltaTime * PlayerStats.playerStat.m_curveSpeed);
            Vector3 moveVec = m_owner.transform.rotation * new Vector3(0.0f, 0.0f, PlayerStats.playerStat.m_moveSpeed) * Time.deltaTime;

            //이동---------------------------------------------
            Vector3 fixedVec = Vector3.zero;
            Vector3 tall = new Vector3(0.0f, PlayerStats.playerStat.m_hikingHeight + PlayerStats.playerStat.m_size, 0.0f);
            fixedVec += FixedMovePos(m_owner.transform.position + tall, PlayerStats.playerStat.m_size, (m_owner.transform.rotation * Vector3.forward).normalized,
                    PlayerStats.playerStat.m_moveSpeed * Time.deltaTime, m_wall);

            m_owner.transform.position += moveVec + fixedVec;
            //--------------------------------------------------
        }
        
        return this;
    }

    public void FootStep()
    {
        m_footSfx.clip = m_footSounds[Random.Range(0, m_footSounds.Length)];
        m_footSfx.Play();
    }

    #endregion
}

//