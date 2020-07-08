using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : EnemyAction, DamageModel
{
    [SerializeField] GameObject m_damSfx;
    [SerializeField] GameObject m_damEff;

    public void TakeDamage(AtkCollider dam)
    {
        if (m_owner.m_currentStat !="Dead")
        {
            m_owner.GetComponent<BossStats>().TakeDamage(dam.atkDamage);
            GameObject eff = Instantiate(m_damEff);
            eff.transform.position = m_owner.transform.position - dam.knockVec * 1f + Vector3.up * 1.5f;
            if (dam.AtkEvent())
            {
                DataController.Instance.SetCombo();
                GameObject sfx = Instantiate(m_damSfx);
                sfx.transform.position = m_owner.transform.position;
            }
        }
    }

    protected override void EndAction() { }

    protected override void StartAction() { }

    protected override void UpdateAction() { }
}
