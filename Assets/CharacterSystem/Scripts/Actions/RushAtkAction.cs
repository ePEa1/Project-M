using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.ePEa.PlayerData;
using ProjectM.ePEa.CamSystem;
using static ProjectM.ePEa.CustomFunctions.CustomFunction;

public class RushAtkAction : BaseAction
{
    #region Inspector

    [SerializeField] PCAtkObject m_atkData;
    [SerializeField] BoxCollider m_atkCollider;
    [SerializeField] AudioSource[] m_audio;
    [SerializeField] LayerMask m_wall;
    [SerializeField] LayerMask m_enemy;
    #endregion

    #region Value

    Vector3 m_startPos;
    Vector3 m_endPos;
    Vector3 m_view;

    float ac = 1.0f;
    float m_time = 0.0f;
    #endregion


    #region Event
    protected override BaseAction OnStartAction()
    {
        m_view = m_owner.transform.position - m_owner.playerCam.position;
        m_view.y = 0;
        m_view = m_view.normalized;

        m_startPos = m_owner.transform.position;
        m_endPos = m_startPos + m_view * m_atkData.distance;

        ac = 1 / m_atkData.rushSpeed;

        m_owner.transform.rotation = Quaternion.LookRotation(m_view);
        m_atkCollider.GetComponent<AtkCollider>().knockVec = m_view;

        m_animator.SetTrigger("Rush");

        return this;
    }
    
    protected override void AnyStateAction()
    {

    }

    protected override BaseAction OnUpdateAction()
    {
        Vector3 before = Vector3.Lerp(m_startPos, m_endPos, m_time);
        m_time += Time.deltaTime * ac;
        Vector3 after = Vector3.Lerp(m_startPos, m_endPos, m_time);

        Vector3 tall = new Vector3(0, PlayerStats.playerStat.m_size + PlayerStats.playerStat.m_hikingHeight, 0);
        Vector3 fixedPos = FixedMovePos(m_owner.transform.position + tall, PlayerStats.playerStat.m_size, m_view,
            Vector3.Distance(before, after), m_wall);

        RaycastHit hit;

        if (!Physics.BoxCast(m_owner.transform.position + tall + m_owner.transform.rotation * Vector3.forward * -1.0f, new Vector3(1.7f, 1.0f, 1f), m_view, out hit,
            Quaternion.Euler(m_owner.transform.rotation.eulerAngles), Vector3.Distance(after, before)+1, m_enemy))
        {
            m_owner.transform.position += after - before + fixedPos;
        }
        else if (!Physics.BoxCast(m_owner.transform.position + tall + m_owner.transform.rotation * Vector3.forward * -1.0f, new Vector3(1.7f, 1.0f, 1f), m_view,
            Quaternion.Euler(m_owner.transform.rotation.eulerAngles), 0, m_enemy))
        {
            Vector3 dir = (new Vector3(hit.point.x, 0.0f, hit.point.z) - new Vector3(m_owner.transform.position.x, 0.0f, m_owner.transform.position.z)).normalized;
            float d = Vector3.Dot(dir, m_view);

            m_owner.transform.position += m_view * (Vector3.Distance(new Vector3(hit.point.x, 0.0f, hit.point.z),
                new Vector3(m_owner.transform.position.x, 0.0f, m_owner.transform.position.z)) * d - 0.6f) + fixedPos;
        }

            return this;
    }

    public override void EndAction()
    {
        m_animator.ResetTrigger("Rush");
    }
    
    #endregion

    #region Function



    #endregion
}
