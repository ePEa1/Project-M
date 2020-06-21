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
        if (player.m_currentController.IsRushAttack() && FrontSkill.fillAmount>=0.99f)
        {
            FrontVal = 0;
        }
        FrontVal += Time.deltaTime * SkillCoolTime;
        FrontSkill.fillAmount = Mathf.Min(FrontVal, 1);

        if (player.m_currentController.IsDashAttack() && FrontSkill.fillAmount >= 0.99f)
        {
            SideVal = 0;
        }
        SideVal += Time.deltaTime * SkillCoolTime;
        SideSkill.fillAmount = Mathf.Min(SideVal, 1);

        if (player.m_currentController.IsBackDashAttack() && FrontSkill.fillAmount >= 0.99f)
        {
            BackVal = 0;
        }
        BackVal += Time.deltaTime * SkillCoolTime;
        BackSkill.fillAmount = Mathf.Min(BackVal, 1);


    }
}
