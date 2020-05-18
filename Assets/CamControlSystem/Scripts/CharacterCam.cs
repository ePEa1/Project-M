using System.Collections;
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

    float m_stopTime = 0;

    AnimationCurve[] m_shakeCurve;
    float m_shakeTime = 0.0f;
    float m_shakeTimeMax = 0.0f;
    Vector3 m_shakeVec = Vector3.zero;

    private void Awake()
    {
        m_shakeCurve = new AnimationCurve[2];

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        SetCamPositionToMouse();

        PlayStop();
    }

    /// <summary>
    /// 프레임스탑
    /// </summary>
    /// <param name="t"></param>
    public void StopFrame(float t)
    {
        Time.timeScale = 0.001f;
        m_stopTime = t * Time.timeScale;
    }


    void PlayStop()
    {
        m_stopTime = Mathf.Max(0, m_stopTime - Time.deltaTime);
        if (m_stopTime == 0)
            Time.timeScale = 1;
    }

    /// <summary>
    /// 타겟 추적 기본 카메라
    /// </summary>
    void SetCamPositionToMouse()
    {
        m_x += Input.GetAxis("Mouse X") * m_xSpeed * Time.deltaTime;
        m_y = Mathf.Clamp(m_y - Input.GetAxis("Mouse Y") * m_ySpeed * Time.deltaTime, m_yMin, m_yMax);

        Quaternion dir = Quaternion.Euler(new Vector3(m_y, m_x, 0));
        Vector3 pos = dir * new Vector3(0.0f, 0.0f, -m_distance) + target.position + m_centerPos;

        transform.rotation = dir;//Quaternion.Slerp(transform.rotation, dir, m_moveSpeed);
        transform.position = Vector3.Lerp(transform.position, pos, m_moveSpeed*Time.deltaTime);
    }

    /// <summary>
    /// 커브 카메라쉐이킹
    /// </summary>
    void PlayShakeToCurve()
    {
        if (m_shakeTimeMax != 0.0f)
        {

        }
    }
}
