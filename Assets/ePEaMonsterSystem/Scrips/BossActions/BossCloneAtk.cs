using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;

public class BossCloneAtk : EnemyAction
{
    #region Inspector
    [SerializeField] string m_aniName;
    [SerializeField] GameObject m_clone;
    [SerializeField] LayerMask m_wall;
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

    public void CreateClone()
    {
        Vector3 targetPos = m_owner.m_player.transform.position;
        targetPos.y = 0;

        GameObject clone = Instantiate(m_clone);
        clone.transform.position = targetPos + TargetBack() + Vector3.up * m_owner.transform.position.y;
        clone.transform.rotation = Quaternion.LookRotation((targetPos - (targetPos + TargetBack())).normalized);
        BossClone comp = clone.transform.GetChild(0).GetComponent<BossClone>();
        int ran = Random.Range(0, 2);
        Debug.Log(ran);
        if (ran == 0) comp.m_isSpin = true;
        else comp.m_isSpin = false;
    }

    public void EndAtk()
    {
        m_owner.ChangeStat("Move");
    }

    #endregion

    #region Functions

    Vector3 TargetBack()
    {
        Vector3 final = Vector3.zero;

        Vector3 back = m_owner.m_player.transform.rotation * Vector3.back * 3.0f;
        Vector3 fixedPos = FixedMovePos(m_owner.m_player.transform.position, 0.5f, back.normalized, 3.0f, m_wall);

        final = back + fixedPos;

        return final;
    }

    #endregion
}
