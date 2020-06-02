using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAtkAction : BaseAction
{
    [SerializeField] AnimationCurve AtkDistance;
    [SerializeField] float speed;

    public Collider DashAtkCol;

    protected override BaseAction OnStartAction()
    {
        DashAtkCol = GetComponentInChildren<GameObject>().GetComponent<Collider>();
        m_animator.SetTrigger("DashAtk");
        return this;
    }
    public override void EndAction()
    {
        m_animator.ResetTrigger("DashAtk");
    }

    //쓰지 않기
    protected override void AnyStateAction()
    {
    }

    protected override BaseAction OnUpdateAction()
    {

        throw new System.NotImplementedException();
    }

    public void SetCollider()
    {
        DashAtkCol.enabled = true;
    }
    public void DeleteCollider()
    {
        DashAtkCol.enabled = false;
    }
    public void CreatEff()
    {
        //이펙트 생성
    }

}
