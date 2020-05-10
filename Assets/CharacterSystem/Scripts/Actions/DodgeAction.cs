using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeAction : BaseAction
{
    protected override BaseAction OnStartAction()
    {
        return this;
    }

    public override void EndAction()
    {
    }

    protected override void AnyStateAction()
    {
    }

    protected override BaseAction OnUpdateAction()
    {
        return this;
    }

    #region Function

    Quaternion GetDirection()
    {
        int h = 0;
        if (Input.GetKey(m_controller.m_leftMove))
            h++;
        if (Input.GetKey(m_controller.m_rightMove))
            h--;

        int v = 0;
        if (Input.GetKey(m_controller.m_frontMove))
            v--;
        if (Input.GetKey(m_controller.m_backMove))
            v++;

        return Quaternion.LookRotation(new Vector3(h, 0, v));
    }

    #endregion
}