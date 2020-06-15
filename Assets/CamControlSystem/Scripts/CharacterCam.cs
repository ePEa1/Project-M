using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.CamSystem;

public class CharacterCam : MonoBehaviour
{
    #region Inspector

    [SerializeField] Transform target; //쫓아갈 상대 오브젝트

    [SerializeField] Vector3 m_centerPos; //기준점 위치
    [SerializeField] float m_distance; //상대와의 거리
    [SerializeField] float m_moveSpeed; //카메라 추적 속도

    [SerializeField] float m_yMin; //회전 최소값
    [SerializeField] float m_yMax; //회전 최대값
    [SerializeField] float m_xSpeed; //x축 마우스 감도
    [SerializeField] float m_ySpeed; //y축 마우스 감도

    #endregion


    #region Value


    float m_x = 0; //현재 x 회전값
    float m_y = 0; //현재 y 회전값

    public float m_stopTime = 0; //프레임스탑 남은 시간(0이면 프레임 스탑 종료)
    public bool isStop = false; //프레임 스탑 중인지

    //카메라 쉐이킹--------------------
    CustomShaking m_shakeData;
    bool m_isShake = false;
    float m_shakeTime = 0.0f;
    int m_shakeNum = 0;
    //---------------------------------

    #endregion
    private void Awake()
    {
        //카메라 커서 고정
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        SetCamTransform();

        //프레임스탑을 사용하고있다면
        if (isStop)
            PlayStop(); //프레임스탑 처리 함수 실행
    }

    /// <summary>
    /// 프레임스탑 이벤트 호출 함수
    /// </summary>
    /// <param name="t">멈출 시간(초)</param>
    public void StopFrame(float t)
    {
        Time.timeScale = 0.0001f;
        m_stopTime = t * Time.timeScale;
        isStop = true;
    }

    /// <summary>
    /// 실제 프레임스탑 처리 함수
    /// </summary>
    void PlayStop()
    {
        if (m_stopTime == 0)
        {
            Time.timeScale = 1;
            isStop = false; //시간 다되면 프레임스탑 종료
        }
        if (Time.deltaTime < 0.0001f)
            m_stopTime = Mathf.Max(0, m_stopTime - Time.deltaTime);
    }

    /// <summary>
    /// 타겟 추적 기본 카메라
    /// </summary>
    (Quaternion angle, Vector3 pos) ToTarget()
    {
        Vector3[] returnVector = new Vector3[2];

        m_x += Input.GetAxis("Mouse X") * m_xSpeed * Time.deltaTime;
        m_y = Mathf.Clamp(m_y - Input.GetAxis("Mouse Y") * m_ySpeed * Time.deltaTime, m_yMin, m_yMax);

        Quaternion dir = Quaternion.Euler(new Vector3(m_y, m_x, 0));
        Vector3 pos = dir * new Vector3(0.0f, 0.0f, -m_distance) + target.position + m_centerPos;

        //transform.rotation = dir;
        //transform.position = Vector3.Lerp(transform.position, pos, m_moveSpeed*Time.deltaTime);

        return (dir, pos/*Vector3.Lerp(transform.position, pos, m_moveSpeed * Time.deltaTime)*/);
    }

    /// <summary>
    /// 카메라 위치 / 각도 최종계산
    /// </summary>
    void SetCamTransform()
    {
        Quaternion dir;
        Vector3 pos;

        (Quaternion angle, Vector3 pos) defaultTransform = ToTarget(); //타겟 중심 카메라 트랜스폼 계산값
        (Quaternion angle, Vector3 pos) shakeTransform;

        //쉐이킹 중이면 쉐이킹 값 받아오고 아니면 기본값 넣기
        if (m_isShake)
            shakeTransform = ShakeTransform();
        else
        {
            shakeTransform.angle = Quaternion.Euler(0, 0, 0);
            shakeTransform.pos = Vector3.zero;
            defaultTransform.pos = Vector3.Lerp(transform.position, defaultTransform.pos, m_moveSpeed * Time.deltaTime);
        }

        dir = Quaternion.Euler(defaultTransform.angle.eulerAngles + shakeTransform.angle.eulerAngles); //최종적으로 카메라에 적용할 회전값 계산
        pos = defaultTransform.pos + dir * shakeTransform.pos; //최종적으로 카메라에 적용할 위치값 계산

        //최종계산한 값 실제적용
        transform.rotation = dir;
        transform.position = pos;
    }

    /// <summary>
    /// 카메라 쉐이킹에 따른 위치, 회전값 계산
    /// </summary>
    /// <returns></returns>
    (Quaternion angle, Vector3 pos) ShakeTransform()
    {
        Quaternion dir = Quaternion.Euler(0, 0, 0);
        Vector3 pos = Vector3.zero;

        if (m_shakeData.ShakingData.Length>0)
        {
            ShakingKey[] shakingData = m_shakeData.ShakingData;
            Vector3 startPos;
            Vector3 startDir;

            float ac = 1 / shakingData[m_shakeNum].skeyTime;

            if (m_shakeNum>0)
            {
                startPos = shakingData[m_shakeNum - 1].sKeyPos;
                startDir = shakingData[m_shakeNum - 1].sKeyDir;
            }
            else
            {
                startPos = Vector3.zero;
                startDir = Vector3.zero;
            }

            pos = Vector3.Lerp(startPos, shakingData[m_shakeNum].sKeyPos, shakingData[m_shakeNum].sKeyCurve.Evaluate(m_shakeTime * ac));
            dir = Quaternion.Euler(Vector3.Lerp(startDir, shakingData[m_shakeNum].sKeyDir, shakingData[m_shakeNum].sKeyCurve.Evaluate(m_shakeTime * ac)));

            m_shakeTime += Time.deltaTime;
            if (m_shakeTime >= shakingData[m_shakeNum].skeyTime)
            {
                m_shakeTime -= shakingData[m_shakeNum].skeyTime;
                m_shakeNum++;
                if (shakingData.Length <= m_shakeNum)
                {
                    m_isShake = false;
                    m_shakeTime = 0.0f;
                    m_shakeNum = 0;
                    m_shakeData = null;
                }
            }
        }

        return (dir, pos);
    }

    public void SetShake(CustomShaking shakeData)
    {
        m_shakeTime = 0.0f;
        m_shakeNum = 0;
        m_shakeData = shakeData;
        m_isShake = true;
    }
}

//※처리 구조※--------------------------------------------------------
//SetCamTransform 에서 최종적으로 카메라 위치/회전값을 적용시킴
//값 계산은 각각의 스크립트에서 반환된 값을 합치는 방식

//현재는 캐릭터를 중심으로 카메라가 이동하는것만 구현되어있기 때문에 그에 대한 위치 / 회전 계산을 하는 ToTarget의 반환값을 바로 적용시킴
//카메라 쉐이킹을 추가하려 한다면 리턴형식이 (Quaternion angle, Vector3 pos)인 함수를 추가로 만든 후 그 안에서 쉐이킹 했을 경우 카메라의 위치 / 회전값을 반환시켜서
//SetCamTransform() 안에서 캐릭터중심으로 계산된 값이 반환되는 ToTarget 함수의 반환값과 같이 계산을 해줘서 추가하는것

//예제용으로 TestFixedCam 함수 만들어놓음

//------------------------------------------------------------------------------
//※만약 현재 처리방식이 마음에 들지 않거나 이해가 안되는 등, 모종의 이유로 이전 그대로 코드를 쓰겠다 하면
//1. ToTarget의 리턴형식을 void로 변경한 후, 83, 84줄에 주석처리되있는 부분 활성화
//2. Update에서 ToTarget 실행
//3. SetCamTransform 삭제