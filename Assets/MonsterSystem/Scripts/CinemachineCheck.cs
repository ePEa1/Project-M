using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineCheck : MonoBehaviour
{

    public CinemachineFreeLook playercin;


    [SerializeField] float ShakeDuration;
    [SerializeField] float ShakeAmplitude;
    [SerializeField] float ShakeFrequency;

    [SerializeField] float ShakeElapsedTime = 0f;

    [SerializeField] float YAccelTime;
    [SerializeField] float XAccelTime;

    // Start is called before the first frame update
    void Start()
    {
        YAccelTime = 0.05f;
        XAccelTime = 0.05f;
        if(playercin != null)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetXAcceltime(DataController.Instance.mouseMoving);
        SetYAcceltime(DataController.Instance.mouseMoving);



        if(playercin != null)
        {
            if (ShakeElapsedTime > 0)
            {
                playercin.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = ShakeAmplitude;
                playercin.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = ShakeFrequency;
                playercin.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = ShakeAmplitude;
                playercin.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = ShakeFrequency;
                playercin.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = ShakeAmplitude;
                playercin.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = ShakeFrequency;

                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                playercin.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
                playercin.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
                playercin.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;

                ShakeElapsedTime = 0f;
            }
        }
    }
    void SetXAcceltime(float acTime)
    {
        playercin.m_XAxis.m_AccelTime = acTime;

    }

    void SetYAcceltime(float acTime)
    {
        playercin.m_YAxis.m_AccelTime = acTime;

    }

    public void GetCinemachineShake()
    {
            ShakeElapsedTime = ShakeDuration;
    }

    
}
