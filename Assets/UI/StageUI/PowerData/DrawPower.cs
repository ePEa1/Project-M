using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;
using UnityEngine.UI;

public class DrawPower : MonoBehaviour
{
    [SerializeField] Image m_damageText;
    [SerializeField] Text m_powerText;
    [SerializeField] Image m_powerGage;
    [SerializeField] int m_fontSizeMin;
    [SerializeField] int m_fontSizeMax;
    [SerializeField] float m_fontSpeed;
    [SerializeField] float m_textMinX;
    [SerializeField] float m_textMaxX;

    float m_currentPower = 1.0f;

    float m_currentMaxGage = 10.0f;
    float m_currentGage = 0.0f;

    float m_fontTime = 0.0f;

    private void Start()
    {
        ChangePower();
    }

    void Update()
    {
        UpdateGage();
        UpdateFontSize();

        if (m_currentPower != PlayerStats.playerStat.m_atkPower)
        {
            ChangePower();
        }

        if (m_powerGage.fillAmount == 0 && m_powerGage.IsActive())
            m_powerGage.gameObject.SetActive(false);
        if (!m_powerGage.IsActive() && m_powerGage.fillAmount > 0)
            m_powerGage.gameObject.SetActive(true);
    }

    /// <summary>
    /// 배율 게이지 업데이트
    /// </summary>
    void UpdateGage()
    {
        m_powerGage.fillAmount = Mathf.Max(0, (PlayerStats.playerStat.m_powerGage - PlayerStats.playerStat.m_powerGageMinus)) / m_currentMaxGage;
    }
    
    void UpdateFontSize()
    {
        Vector3 originVec = m_damageText.transform.localPosition;
        originVec.x = 0;

        m_powerText.fontSize = (int)Mathf.Lerp(m_fontSizeMin, m_fontSizeMax, m_fontTime);
        //m_damageText.transform.localPosition = originVec + Vector3.left * Mathf.Lerp(m_textMinX, m_textMaxX, m_fontTime);
        m_fontTime = Mathf.Max(0, m_fontTime - Time.deltaTime * m_fontSpeed);
    }

    /// <summary>
    /// 데미지 배율 레벨 변경 시 실행 이벤트
    /// </summary>
    void ChangePower()
    {
        m_currentPower = PlayerStats.playerStat.m_atkPower;
        if (m_currentPower > 1)
            m_powerText.text =  m_currentPower.ToString();
        else m_powerText.text =  m_currentPower + ".0";
        m_powerText.fontSize = m_fontSizeMax;
        m_fontTime = 1.0f;
        m_currentMaxGage = PlayerStats.playerStat.m_powerData.level[PlayerStats.playerStat.m_atkLevel].nextGage;
    }
}
