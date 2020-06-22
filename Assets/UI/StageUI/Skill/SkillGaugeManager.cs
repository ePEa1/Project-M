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
    [SerializeField] float SkillCoolTime; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFsmManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.playerStat.m_currentMp <= PlayerStats.playerStat.m_rushMp)
        {
            SetCantUse(FrontSkill, FrontIcon);
        }

        if (PlayerStats.playerStat.m_currentMp <= PlayerStats.playerStat.m_backMp)
        {
            SetCantUse(BackSkill, BackIcon);
        }

        if (PlayerStats.playerStat.m_currentMp <= PlayerStats.playerStat.m_widthMp)
        {
            SetCantUse(SideSkill, SideIcon);
        }



        if (player.m_currentController.IsDashAttack() && FrontSkill.fillAmount >= 0.99f)
        {
            SideVal = 0;
        }

        if (player.m_currentController.IsBackDashAttack() && FrontSkill.fillAmount >= 0.99f)
        {
            BackVal = 0;
        }


    }


    void CheckCanUse()
    {

    }

    void SetCantUse(Image back, Image logo)
    {
        back.color = new Vector4(back.color.r, back.color.g, back.color.b, 0);
        logo.color = new Vector4(0, 0, 0, 1);
    }
    IEnumerator FadeIn(Image FadeImg)
    {
        for (float i = 0f; i >= 0; i += 0.005f * fadeSpeed)
        {
            Color color = new Vector4(0, 0, 0, i);
            FadeImg.color = color;

            if (FadeImg.color.a >= 1)
            {

            }

            yield return null;
        }
    }

    
    IEnumerator FadeOut(Image FadeImg)
    {
        for (float i = 0f; i >= 0; i -= 0.005f * fadeSpeed)
        {
            Color color = new Vector4(0, 0, 0, i);
            FadeImg.color = color;

            if (FadeImg.color.a >= 1)
            {

            }

            yield return null;
        }
    }
}
