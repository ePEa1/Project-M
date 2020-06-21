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

    [SerializeField] float SkillCoolTime; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.m_currentController.IsRushAttack() && FrontSkill.fillAmount<=0.99f)
        {
            FrontSkill.fillAmount = 0;
        }
        FrontSkill.fillAmount = (int)Mathf.Lerp(0, 1, SkillCoolTime);
        if (player.m_currentController.IsDashAttack() && FrontSkill.fillAmount <= 0.99f)
        {
            FrontSkill.fillAmount = 0;
        }
        SideSkill.fillAmount = (int)Mathf.Lerp(0, 1, SkillCoolTime);
        if (player.m_currentController.IsBackDashAttack() && FrontSkill.fillAmount <= 0.99f)
        {
            FrontSkill.fillAmount = 0;
        }
        BackSkill.fillAmount = (int)Mathf.Lerp(0, 1, SkillCoolTime);

    }
}
