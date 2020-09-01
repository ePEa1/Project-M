using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectM.ePEa.PlayerData;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;

public class SkillGaugeManager : MonoBehaviour
{
    public PlayerFsmManager player;

    public Image FrontSkill;//skill_1
    public Image SideSkill;//skill_2
    public Image BackSkill;//skill_3

    public Image FrontIcon;
    public Image SideIcon;
    public Image BackIcon;

    


    [SerializeField] float fadeSpeed;
    float FrontVal;
    float SideVal;
    float BackVal;

    bool CanFront;
    bool CanSide;
    bool CanBack;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFsmManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckFrontUse();
        CheckSideUse();
        CheckBackUse();
    }


    void CheckFrontUse()
    {
        if(SkillManager.IsRush == false)
        {
            FrontIcon.enabled = false;
        }
        else
        {
            if (PlayerStats.playerStat.m_currentMp < PlayerStats.playerStat.m_rushMp || SkillManager.IsRush == false)
            {
                FrontIcon.enabled = false;
            }
            else
            {
                FrontIcon.enabled = true;
            }
        }


    }
    void CheckSideUse()
    {
        if(SkillManager.IsWidth == false)
        {
            SideIcon.enabled = false;
        }
        else
        {
            if (PlayerStats.playerStat.m_currentMp <= PlayerStats.playerStat.m_widthMp)
            {
                SideIcon.enabled = false;
            }
            else
            {
                SideIcon.enabled = true;
            }
        }

    }
    void CheckBackUse()
    {
        if (SkillManager.IsBack == false)
        {
            BackIcon.enabled = false;

        }
        else
        {
            if (PlayerStats.playerStat.m_currentMp <= PlayerStats.playerStat.m_backMp)
            {
                BackIcon.enabled = false;
            }
            else
            {
                BackIcon.enabled = true;
            }
        }
    }
    void SetCantUse(Image back, Image logo)
    {
        back.color = new Vector4(back.color.r, back.color.g, back.color.b, 0);
        logo.color = new Vector4(0, 0, 0, 1);
    }
    void SetUse(Image back, Image logo)
    {
        back.color = new Vector4(back.color.r, back.color.g, back.color.b, 1);
        logo.color = new Vector4(1, 1, 1, 1);
    }



}
