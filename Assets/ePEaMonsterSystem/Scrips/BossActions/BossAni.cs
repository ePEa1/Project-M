using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAni : MonoBehaviour
{
    [SerializeField] BossAniEvents events;

    public enum DoubleAtk
    {
        AtkTiming,
        CreateEff,
        PlaySFX,
        EndAtk,
        OnView,
        CloseView
    };

    public enum LiteAtk
    {
        AtkTiming,
        CreateEff,
        PlaySFX,
        EndAtk,
        OnView,
        CloseView
    };

    public enum StrongAtk
    {
        AtkTiming,
        CreateEff,
        PlaySFX,
        EndAtk,
        OnView,
        CloseView
    };

    public enum SpinAtk
    {
        AtkTiming,
        CreateEff,
        PlaySFX,
        EndAtk,
    };

    public enum CloneAtk
    {
        CreateClone,
        EndAtk
    };

    public enum UltraAtk
    {
        StartAtk,
        EndAtk
    };

    public void OnDoubleAtk(DoubleAtk e) => events.m_doubleAtk[(int)e].Invoke();
    public void OnLiteAtk(LiteAtk e) => events.m_liteAtk[(int)e].Invoke();
    public void OnStrongAtk(StrongAtk e) => events.m_strongAtk[(int)e].Invoke();
    public void OnSpinAtk(SpinAtk e) => events.m_spinAtk[(int)e].Invoke();
    public void OnCloneAtk(CloneAtk e) => events.m_cloneAtk[(int)e].Invoke();
    public void OnUltraAtk(UltraAtk e) => events.m_ultraAtk[(int)e].Invoke();
}
