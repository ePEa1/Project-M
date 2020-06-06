using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //조작키 관리
    //여기서 키를 눌렀는지에 대한 내용 체크
    //인스펙터에는 입력 키 배치
    //함수는 조작 판정 구현

    #region Inspector

    //이동키
    [SerializeField] public KeyCode m_frontMove;
    [SerializeField] public KeyCode m_backMove;
    [SerializeField] public KeyCode m_rightMove;
    [SerializeField] public KeyCode m_leftMove;

    //공격키
    [SerializeField] public KeyCode m_attack;

    //회피키
    [SerializeField] public KeyCode m_dodge;

    //연타값
    float leftdash = 0;
    float rightdash = 0;
    float backdash = 0;
    [SerializeField] float dashTime = 1.0f;

    #endregion

    #region Function


    private void Update()
    {
        //IsLeftDashAttack();
        //IsRightDashAttack();
        //IsBackDashAttack();
        leftdash = Mathf.Max(0,leftdash-Time.deltaTime);
        rightdash = Mathf.Max(0, rightdash-Time.deltaTime);
        backdash = Mathf.Max(0, backdash-Time.deltaTime);
    }
    /// <summary>
    /// 이동키 입력했는지 체크
    /// </summary>
    /// <returns></returns>
    public bool IsMoving()
    {
        int h = 0;
        if (Input.GetKey(m_leftMove))
            h++;
        if (Input.GetKey(m_rightMove))
            h--;

        int v = 0;
        if (Input.GetKey(m_frontMove))
            v++;
        if (Input.GetKey(m_backMove))
            v--;

        if (h == 0 && v == 0)
            return false;
        else return true;
    }

    /// <summary>
    /// 현재 입력중인 이동 방향 쿼터니언 값 반환
    /// </summary>
    /// <returns></returns>
    public Quaternion GetDirection()
    {
        
        int h = 0;
        if (Input.GetKey(m_leftMove))
            h++;
        if (Input.GetKey(m_rightMove))
            h--;

        int v = 0;
        if (Input.GetKey(m_frontMove))
            v--;
        if (Input.GetKey(m_backMove))
            v++;

        return Quaternion.LookRotation(new Vector3(h, 0, v));
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

    public bool IsLeftDashAttack()
    {
        if (Input.GetKeyDown(m_leftMove))
        {
            if(leftdash == 0)
            {
                leftdash = dashTime;
            }
            else
            {
                leftdash = 0;
                //Debug.Log("LeftDashCheck");
                return true;
            }
        }
            return false;
    }
    public bool IsRightDashAttack()
    {
        if (Input.GetKeyDown(m_rightMove))
        {
            if (rightdash == 0)
            {
                rightdash = dashTime;
            }
            else
            {
                rightdash = 0;
                //Debug.Log("RightDashCheck");
                return true;
            }
        }
         return false;
    }
    public bool IsBackDashAttack()
    {
        if (Input.GetKeyDown(m_backMove))
        {
            if (backdash == 0)
            {
                backdash = dashTime;
            }
            else
            {
                backdash = 0;
                //Debug.Log("BackDashCheck");
                return true;
            }
        }
        
        return false;
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
