using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpinAtk : EnemyAction
{
    #region Inspector
    [SerializeField] string m_aniName;
    [SerializeField] PCAtkObject m_atkObj;
    [SerializeField] AtkCollider m_atkCollider;
    [SerializeField] AudioSource m_sfx;
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

    protected override void UpdateAction() { }
    #endregion

    #region AnimationEvents

    public void AtkTiming()
    {
        m_atkCollider.atkDamage = m_atkObj.atkData[0].damage;
        m_atkCollider.knockVec = TargetView();
        m_atkCollider.Attacking();
    }

    public void CreateEff()
    {
        PCAtksData data = m_atkObj.atkData[0];

        GameObject eff = Instantiate(data.eff);
        eff.transform.position = m_owner.transform.position + Quaternion.Euler(0.0f, m_owner.transform.localEulerAngles.y, 0.0f) * data.effPos;
        eff.transform.rotation = Quaternion.Euler(0.0f, m_owner.transform.eulerAngles.y, 0.0f) * Quaternion.Euler(data.effDir);
    }

    public void PlaySFX()
    {
        m_sfx.clip = m_atkObj.atkData[0].sfx;
        m_sfx.Play();
    }

    public void EndAtk() => m_owner.ChangeStat("Move");
    #endregion

    #region Functions

    Vector3 TargetView()
    {
        Vector3 view = m_owner.m_player.transform.position - m_owner.transform.position;
        view.y = 0;
        return view.normalized;
    }

    #endregion
}
