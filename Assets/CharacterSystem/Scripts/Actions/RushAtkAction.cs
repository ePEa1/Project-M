using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;
using ProjectM.ePEa.CamSystem;
using static ProjectM.ePEa.CustomFunctions.CustomFunction;

public class RushAtkAction : BaseAction
{
    #region Inspector
    [SerializeField] float m_damage;

    [SerializeField] float m_rushDistance;
    [SerializeField] float m_rushSpeed;
    [SerializeField] AnimationCurve m_rushCurve;

    [SerializeField] Collider m_atkCollider;
    #endregion

    #region Value

    #endregion


    #region Event
    protected override BaseAction OnStartAction()
    {
        
        return this;
    }
    
    protected override void AnyStateAction()
    {

    }

    protected override BaseAction OnUpdateAction()
    {
        return this;
    }

    public override void EndAction()
    {

    }


    #endregion

    #region Function

    #endregion
}
