using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;
using UnityEngine.UI;

public class PlayerMpUI : MonoBehaviour
{
    [SerializeField] Image m_slider;

    private void Awake()
    {
       
    }

    void Update()
    {
        m_slider.fillAmount = PlayerStats.playerStat.m_currentMp / PlayerStats.playerStat.MaxMp;
    }
}
