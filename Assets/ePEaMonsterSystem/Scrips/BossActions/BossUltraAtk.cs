using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUltraAtk : EnemyAction
{
    #region Inspector
    [SerializeField] string m_aniName;
    [SerializeField] GameObject m_sword;
    [SerializeField] float m_swordDelay;
    [SerializeField] int m_swordMany;
    [SerializeField] float m_height;
    [SerializeField] float m_angle;
    [SerializeField] Vector3 m_spawnField;
    #endregion

    #region Value

    bool m_atkStart = false;
    float m_time = 0.0f;
    int m_spawnNum = 0;

    #endregion

    #region Base
    protected override void EndAction()
    {
        m_time = 0.0f;
        m_spawnNum = 0;
        m_atkStart = false;
        m_animator.SetBool("Is" + m_aniName, false);
        m_animator.ResetTrigger(m_aniName);
    }

    protected override void StartAction()
    {
        m_time = 0.0f;
        m_spawnNum = 0;
        m_atkStart = false;
        m_animator.SetBool("Is" + m_aniName, true);
        m_animator.SetTrigger(m_aniName);
    }

    protected override void UpdateAction()
    {
        if (m_atkStart)
        {
            m_time += Time.deltaTime;
            if (m_time >= m_swordDelay)
            {
                m_time -= m_swordDelay;
                m_spawnNum++;

            }
        }
    }
    #endregion

    #region AnimationEvents

    public void AtkStart() => m_atkStart = true;

    public void EndAtk() => m_owner.ChangeStat("Move");

    #endregion


    #region Functions
    void SpawnSword()
    {
        GameObject sword = Instantiate(m_sword);
        sword.transform.position = m_owner.transform.rotation * Quaternion.Euler(new Vector3(0, 90, 0)) * m_spawnField;
    }
    #endregion
}
