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
    [SerializeField] float m_zMin;
    [SerializeField] float m_zMax;
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
        if (m_atkStart && m_spawnNum < m_swordMany)
        {
            m_time += Time.deltaTime;
            if (m_time >= m_swordDelay)
            {
                m_time -= m_swordDelay;
                m_spawnNum++;
                SpawnSword();
            }
        }
    }
    #endregion

    #region AnimationEvents

    public void StartAtk() => m_atkStart = true;

    public void EndAtk() => m_owner.ChangeStat("Move");

    #endregion


    #region Functions
    void SpawnSword()
    {
        GameObject sword = Instantiate(m_sword);
        float x = Random.Range(-m_spawnField.x * 0.5f, m_spawnField.x * 0.5f);
        float y = Random.Range(-m_spawnField.y * 0.5f, m_spawnField.y * 0.5f);
        float z = Random.Range(m_zMin, m_zMax);
        float s = Random.Range(0.4f, 0.7f);
        Vector3 spawnPos = new Vector3(x, z, y);
        sword.transform.localScale = Vector3.one * s;
        sword.transform.position = Vector3.up * m_height + m_owner.transform.position + m_owner.transform.rotation * Quaternion.Euler(new Vector3(m_angle, 0, 0)) * spawnPos;
        sword.transform.rotation = m_owner.transform.rotation * Quaternion.Euler(new Vector3(m_angle, 0, 0));
    }
    #endregion
}
