using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUltraSkill : EnemyAction
{
    #region Inspector
    [SerializeField] string m_aniName;
    [SerializeField] GameObject m_eff;
    #endregion

    #region Base
    protected override void EndAction()
    {
        m_animator.SetBool("Is" + m_aniName, false);
        m_animator.ResetTrigger(m_aniName);
    }

    protected override void StartAction()
    {
        m_animator.SetBool("Is" + m_aniName, true);
        m_animator.SetTrigger(m_aniName);
    }

    protected override void UpdateAction()
    {

    }
    #endregion

}
