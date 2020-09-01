using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ProjectM.ePEa.CustomFunctions.CustomFunction;

public class BossClone : MonoBehaviour
{
    #region Inspector
    [SerializeField] PCAtkObject[] m_atkObj;
    [SerializeField] AtkCollider[] m_atkCollider;
    [SerializeField] LayerMask m_wall;
    #endregion

    #region Value
    public bool m_isSpin;
    public Vector3 m_startPos;
    public Vector3 m_finishPos;
    float m_time = 0.0f;
    float m_curve;
    #endregion

    private void Start()
    {
        if (!m_isSpin)
        {
            m_startPos = transform.parent.position;
            Vector3 targetPos = GameObject.FindWithTag("Player").transform.position;
            targetPos.y = 0;
            Vector3 myPos = m_startPos;
            myPos.y = 0;
            m_finishPos = m_startPos + (targetPos - myPos).normalized * m_atkObj[1].distance;

            m_curve = m_atkObj[1].rushSpeed;
            GetComponent<Animator>().SetTrigger("LiteAtk");
        }
        else GetComponent<Animator>().SetTrigger("SpinAtk");
    }

    void Update()
    {
        if (!m_isSpin)
        {
            Vector3 before = Vector3.Lerp(m_startPos, m_finishPos, m_atkObj[1].distanceCurve.Evaluate(m_time / m_curve));
            m_time += Time.deltaTime;
            Vector3 after = Vector3.Lerp(m_startPos, m_finishPos, m_atkObj[1].distanceCurve.Evaluate(m_time / m_curve));

            Vector3 fixedPos = FixedMovePos(transform.position, 0.75f, (after - before).normalized, Vector3.Distance(before, after), m_wall);

            transform.parent.position += after - before + fixedPos;
        }
    }

    public void CreateEff()
    {
        Transform ownerTrans = transform.parent;
        PCAtksData data;

        if (m_isSpin)
            data = m_atkObj[0].atkData[0];
        else data = m_atkObj[1].atkData[0];

        GameObject eff = Instantiate(data.eff);
        eff.transform.position = ownerTrans.position + Quaternion.Euler(0.0f, ownerTrans.localEulerAngles.y, 0.0f) * data.effPos;
        eff.transform.rotation = Quaternion.Euler(0.0f, ownerTrans.eulerAngles.y, 0.0f) * Quaternion.Euler(data.effDir);
    }

    public void AtkTiming()
    {
        if (m_isSpin)
        {
            Vector3 targetPos = GameObject.FindWithTag("Player").transform.position;
            targetPos.y = 0;
            Vector3 myPos = transform.parent.position;
            myPos.y = 0;
            m_atkCollider[0].knockVec = (targetPos - myPos).normalized;
            m_atkCollider[0].Attacking();
        }
        else
        {
            m_atkCollider[1].knockVec = (transform.parent.rotation * Vector3.forward).normalized;
            m_atkCollider[1].Attacking();
        }
    }

    public void EndAttack()
    {
        Destroy(transform.parent.gameObject);
    }
}
