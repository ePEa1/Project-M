using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineCheck : MonoBehaviour
{

    public CinemachineFreeLook playercin;

    [SerializeField] float YAccelTime;
    [SerializeField] float XAccelTime;

    // Start is called before the first frame update
    void Start()
    {
        YAccelTime = 0.05f;
        XAccelTime = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        SetXAcceltime(DataController.Instance.mouseMoving);
        SetYAcceltime(DataController.Instance.mouseMoving);
    }
    void SetXAcceltime(float acTime)
    {
        playercin.m_XAxis.m_AccelTime = acTime;

    }

    void SetYAcceltime(float acTime)
    {
        playercin.m_YAxis.m_AccelTime = acTime;

    }
}
