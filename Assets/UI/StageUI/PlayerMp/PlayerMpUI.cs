using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;
using UnityEngine.UI;

public class PlayerMpUI : MonoBehaviour
{
    Slider m_slider;

    private void Awake()
    {
        m_slider = GetComponent<Slider>();
    }

    void Update()
    {
        m_slider.value = PlayerStats.playerStat.m_currentMp / PlayerStats.playerStat.MaxMp;
    }
}
