using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillManager
{
    static bool m_isWidth = false;
    static bool m_isBack = false;
    static bool m_isRush = false;

    public static bool IsWidth { get { return m_isWidth; } }
    public static bool IsBack { get { return m_isBack; } }
    public static bool IsRush { get { return m_isRush; } }

    public static void UnlockWidth() => m_isWidth = true;
    public static void UnlockBack() => m_isBack = true;
    public static void UnlockRush() => m_isRush = true;

    public static void Setup()
    {
        m_isWidth = false;
        m_isBack = false;
        m_isRush = false;
    }
}
