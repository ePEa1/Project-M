using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;

public class PlayerController : MonoBehaviour
{
    //조작키 관리
    //여기서 키를 눌렀는지에 대한 내용 체크
    //인스펙터에는 입력 키 배치
    //함수는 조작 판정 구현

    #region Inspector

    //이동키
    [SerializeField] KeyCode m_frontMove;
    [SerializeField] KeyCode m_backMove;
    [SerializeField] KeyCode m_rightMove;
    [SerializeField] KeyCode m_leftMove;

    //공격키
    [SerializeField] KeyCode m_attack;
    [SerializeField] KeyCode m_rushAttack;
    [SerializeField] KeyCode m_rightdashAttack;
    [SerializeField] KeyCode m_leftdashAttack;
    [SerializeField] KeyCode m_backAttack;

    //회피키
    [SerializeField] KeyCode m_dodge;

    public bool CanAttack = true;//pause 상태일때 공격 못하게 하기
    

    #endregion

    #region Value

    float h = 0;
    float v = 0;

    #endregion

    #region Function

    /// <summary>
    /// 이동키 입력했는지 체크
    /// </summary>
    /// <returns></returns>
    public bool IsMoving
    {
        get => (Input.GetKey(m_leftMove) || Input.GetKey(m_rightMove) || Input.GetKey(m_frontMove) || Input.GetKey(m_backMove));
    }

    /// <summary>
    /// 방향키 이동방향 계산
    /// </summary>
    public void SetDirectionToKey()
    {
        if (Input.GetKeyDown(m_leftMove))
        {
            h = -1;
        }
        if (Input.GetKeyDown(m_rightMove))
        {
            h = 1;
        }

        if (Input.GetKeyUp(m_leftMove))
        {
            if (Input.GetKey(m_rightMove))
                h = 1;
            else h = 0;
        }
        if (Input.GetKeyUp(m_rightMove))
        {
            if (Input.GetKey(m_leftMove))
                h = -1;
            else h = 0;
        }
        //---------------------------------

        if (Input.GetKeyDown(m_frontMove))
        {
            v = 1;
        }
        if (Input.GetKeyDown(m_backMove))
        {
            v = -1;
        }

        if (Input.GetKeyUp(m_frontMove))
        {
            if (Input.GetKey(m_backMove))
                v = -1;
            else v = 0;
        }
        if (Input.GetKeyUp(m_backMove))
        {
            if (Input.GetKey(m_frontMove))
                v = 1;
            else v = 0;
        }
    }

    /// <summary>
    /// 현재 입력중인 이동 방향 쿼터니언 값 반환
    /// </summary>
    /// <returns></returns>
    public (Quaternion q, Vector3 v) GetDirection()
    {
        return (Quaternion.LookRotation(new Vector3(h, 0, v)), new Vector3(h, 0, v));
    }

    /// <summary>
    /// 공격키 눌렀는지 체크
    /// </summary>
    /// <returns></returns>
    public bool IsAttack()
    {
        if (Input.GetKeyDown(m_attack))
            return true;
        
        else return false;
    }

    public bool IsRushAttack()
    {
        if (Input.GetKeyDown(m_rushAttack) && PlayerStats.playerStat.m_currentMp >= PlayerStats.playerStat.m_rushMp)
            return true;
        else return false;
    }

    public bool IsDashAttack()
    {
        if (Input.GetKeyDown(m_rightdashAttack)  && PlayerStats.playerStat.m_currentMp >= PlayerStats.playerStat.m_widthMp)
        {
            h = 1;
            return true;

        }
        if (Input.GetKeyDown(m_leftdashAttack) && PlayerStats.playerStat.m_currentMp >= PlayerStats.playerStat.m_widthMp)
        {
            h = -1;
            return true;

        }
        else return false;
    }


    public bool IsBackDashAttack()
    {
        if (Input.GetKeyDown(m_backAttack)  && PlayerStats.playerStat.m_currentMp >= PlayerStats.playerStat.m_backMp)
            return true;
        else return false;
    }

    /// <summary>
    /// 회피키 눌렀는지 체크
    /// </summary>
    /// <returns></returns>
    public bool IsDodge()
    {
        if (Input.GetKeyDown(m_dodge))
            return true;
        else return false;
    }

    #endregion
}
