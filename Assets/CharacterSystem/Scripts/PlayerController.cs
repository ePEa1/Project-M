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

    #endregion

    #region Function

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
