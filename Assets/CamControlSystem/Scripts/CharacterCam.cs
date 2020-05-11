﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCam : MonoBehaviour
{
    [SerializeField]
    Transform target; //쫓아갈 상대 오브젝트

    [SerializeField]
    Vector3 m_centerPos; //기준점 위치
    [SerializeField]
    float m_distance; //상대와의 거리
    [SerializeField]
    float m_moveSpeed; //카메라 추적 속도

    [SerializeField]
    float m_yMin; //회전 최소값
    [SerializeField]
    float m_yMax; //회전 최대값
    [SerializeField]
    float m_xSpeed; //x축 마우스 감도
    [SerializeField]
    float m_ySpeed; //y축 마우스 감도

    float m_x = 0; //x 회전값
    float m_y = 0; //y 회전값

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        SetCamPositionToMouse();
    }

    /// <summary>
    /// 타겟 추적 기본 카메라
    /// </summary>
    void SetCamPositionToMouse()
    {
        m_x += Input.GetAxis("Mouse X") * m_xSpeed * Time.deltaTime;
        m_y = Mathf.Clamp(m_y - Input.GetAxis("Mouse Y") * m_ySpeed * Time.deltaTime, m_yMin, m_yMax);

        //Vector3 targetVec = transform.position + (target.position - transform.position) * 0.1f;

        Quaternion dir = Quaternion.Euler(new Vector3(m_y, m_x, 0));
        Vector3 pos = dir * new Vector3(0.0f, 0.0f, -m_distance) + target.position + m_centerPos;

        transform.rotation = Quaternion.Lerp(transform.rotation, dir, m_moveSpeed);
        transform.position = Vector3.Lerp(transform.position, pos, m_moveSpeed);
    }
}
