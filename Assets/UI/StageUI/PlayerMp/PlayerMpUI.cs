using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;
using UnityEngine.UI;

public class PlayerMpUI : MonoBehaviour
{
    [SerializeField] Image m_slider;
    public Text m_text;

    private void Awake()
    {
       
    }

    void Update()
    {
        m_slider.fillAmount = PlayerStats.playerStat.m_currentMp / PlayerStats.playerStat.MaxMp;
        m_text.text = PlayerStats.playerStat.m_currentMp.ToString("N1") + "/" + PlayerStats.playerStat.MaxMp;
    }
}
