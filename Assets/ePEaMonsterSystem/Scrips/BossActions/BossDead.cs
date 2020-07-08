using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDead : EnemyAction
{

    #region Base
    protected override void EndAction()
    {
        Destroy(m_owner.gameObject);
    }

    protected override void StartAction()
    {
        m_animator.SetBool("IsDead", true);
        m_animator.SetTrigger("Dead");

        //TEST-------
        Destroy(m_owner.gameObject);
        //-----------
    }

    protected override void UpdateAction()
    {
    }

    private void Update()
    {
        if (m_owner.GetComponent<BossStats>().m_currentHp == 0 && m_owner.m_currentStat != "Dead")
        {
            m_owner.ChangeStat("Dead");
        }
    }
    #endregion
}
