using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectM.ePEa.ProtoMon;

public class EnemyHPViewManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] int m_hpSize= 5; //hp 줄 수
    [SerializeField] GameObject m_hpBarBox;//묶음
    [SerializeField] GameObject m_hpBar; //hp 줄 프리팹
    [SerializeField] Text m_hpSizeText; //hp 줄 수 텍스트
    [SerializeField] Text m_hpData; //현재 hp 수치 텍스트
    [SerializeField] Image m_hpBox; //hp바 틀
    [SerializeField] GameObject m_TimeObj;//시간 게이지

    //hp바 쉐이킹----------------------------------
    [SerializeField] float m_shakePower = 3.0f;
    [SerializeField] float m_shakeDelay = 0.05f;
    [SerializeField] float m_shakeSpeed = 1.0f;
    //---------------------------------------------

    [SerializeField] Vector3 m_originPos; //hp 바 위치
    [SerializeField] Color[] m_hpColor; //hp 바 색상

    [SerializeField] Image m_shield; //실드 게이지

    #endregion


    #region Value

    //public Slider HPGauge;
    public float m_nowHp;
    public float m_maxHp = 0.0f;

    float m_subHp;

    bool m_isSetting = false;

    float m_hpMaxSize; //hp 한 줄당 체력
    public GameObject[] hps; //hp 줄 객체
    int m_nowSize = 0; //현재 hp 줄

    float m_nowShakePower = 0.0f; //현재 쉐이킹 세기
    float m_nowShakeDelay = 0.0f;

    Vector3 m_centerPos; //hp바 처음 위치

    ProtoBossFSM boss = null;

    #endregion

    void Awake()
    {
        m_centerPos = GetComponent<RectTransform>().position;
        if (GameObject.FindWithTag("Boss") != null)
        {
            m_hpBarBox.SetActive(true);
            boss = GameObject.FindWithTag("Boss").GetComponent<ProtoBossFSM>();
            m_maxHp = boss.m_maxHp;
            Setup();
        }

    }

    void Start()
    {
        if (GameObject.FindWithTag("Boss") != null)
        {
            m_hpBarBox.SetActive(true);
            boss = GameObject.FindWithTag("Boss").GetComponent<ProtoBossFSM>();
            m_maxHp = boss.m_maxHp;
            Setup();
        }
    }

    private void Update()
    {
        if (m_isSetting)
        {
            //hp 바 쉐이킹
            PlayShake();
            //hp줄 수 텍스트 설정
            //m_hpSizeText.fontSize = (int)Mathf.Max(m_fontMinSize, m_hpSizeText.fontSize - Time.deltaTime * m_fontSpeed);

            UpdateShield();

            if (m_nowHp != boss.m_currentHp)
            {
                ChangeHp(boss.m_currentHp);
            }
        }
        if (!m_isSetting)
        {
            if (GameObject.FindWithTag("Boss") != null)
            {
                m_hpBarBox.SetActive(true);
                boss = GameObject.FindWithTag("Boss").GetComponent<ProtoBossFSM>();
                m_maxHp = boss.m_maxHp;
                Setup();
            }
        }
    }

    /// <summary>
    /// hp바 셋팅
    /// </summary>
    public void Setup()
    {
        m_hpMaxSize = m_maxHp / m_hpSize;
        m_nowSize = m_hpSize;
        m_nowHp = m_maxHp;
        m_subHp = m_nowHp;

        //hp바 생성
        hps = new GameObject[m_hpSize];
        for (int i = 0; i < m_hpSize; i++)
        {
            hps[i] = Instantiate(m_hpBar);
            hps[i].transform.parent = transform;
            hps[i].transform.localPosition = m_originPos + Vector3.forward * i * 1.0f;
            //hps[i].GetComponent<Image>().color = m_hpColor[i % m_hpColor.Length];
            hps[i].GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
            hps[i].transform.localScale = new Vector3(1, 1, 1);
        }
        m_hpData.transform.SetAsLastSibling();
        m_hpData.text = ((int)Mathf.Floor(m_nowHp)).ToString() + " / " + m_maxHp.ToString();
        m_hpSizeText.transform.SetAsLastSibling();
        m_hpSizeText.text = "X " + (int)Mathf.Ceil(m_nowHp / m_hpMaxSize);
        m_hpBox.transform.SetAsLastSibling();
        //m_TimeObj.transform.SetAsLastSibling();

        //셋팅 끝 체크
        m_isSetting = true;
    }

    void UpdateShield()
    {
        m_shield.fillAmount = boss.m_currentShield / boss.m_shieldMax;
    }

    /// <summary>
    /// hp 재설정 함수
    /// </summary>
    /// <param name="changeHp"></param>
    public void ChangeHp(float changeHp)
    {
        m_nowHp = changeHp;
        m_nowShakePower = m_shakePower;
        m_hpData.text = ((int)Mathf.Floor(m_nowHp)).ToString() + " / " + m_maxHp.ToString();

        for (int i = 0; i < hps.Length; i++)
        {
            hps[i].GetComponent<Image>().fillAmount = Mathf.Clamp(m_nowHp - i * m_hpMaxSize, 0, m_hpMaxSize) / m_hpMaxSize;
        }

        if (m_nowSize != (int)Mathf.Ceil(m_nowHp / m_hpMaxSize))
            ChangeHpSize((int)Mathf.Ceil(m_nowHp / m_hpMaxSize));
    }

    /// <summary>
    /// hp바 쉐이킹
    /// </summary>
    void PlayShake()
    {
        m_nowShakePower = Mathf.Max(0.0f, m_nowShakePower - Time.deltaTime * m_shakeSpeed);

        m_nowShakeDelay -= Time.deltaTime;

        if (m_nowShakeDelay <= 0.0f)
        {
            m_nowShakeDelay += m_shakeDelay;
            Vector3 shakeVec = new Vector3(Random.Range(-m_nowShakePower, m_nowShakePower), Random.Range(-m_nowShakePower, m_nowShakePower), 0).normalized * m_nowShakePower;
            GetComponent<RectTransform>().position = m_centerPos + new Vector3(shakeVec.x, shakeVec.y, transform.localPosition.z);
        }

        if (m_nowShakePower <= 0.0f)
        {
            m_nowShakeDelay = 0.0f;
            GetComponent<RectTransform>().position = new Vector3(m_centerPos.x, m_centerPos.y, m_centerPos.z);
        }
    }

    void ChangeHpSize(int size)
    {
        m_nowSize = size;
        m_hpSizeText.text = "X " + m_nowSize;
        //m_hpSizeText.fontSize = m_fontMaxSize;
    }
}
