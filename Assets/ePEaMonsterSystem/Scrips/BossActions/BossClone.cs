using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClone : MonoBehaviour
{
    [SerializeField] PCAtkObject m_atkObj;
    AtkCollider m_atkCollider;

    public void Awake()
    {
        m_atkCollider = GetComponent<AtkCollider>();
    }

    public void CreateEff()
    {
        Transform ownerTrans = transform.parent;

        PCAtksData data = m_atkObj.atkData[0];

        GameObject eff = Instantiate(data.eff);
        eff.transform.position = ownerTrans.position + Quaternion.Euler(0.0f, ownerTrans.localEulerAngles.y, 0.0f) * data.effPos;
        eff.transform.rotation = Quaternion.Euler(0.0f, ownerTrans.eulerAngles.y, 0.0f) * Quaternion.Euler(data.effDir);
    }

    public void AtkTiming()
    {
        Vector3 targetPos = GameObject.FindWithTag("Player").transform.position;
        targetPos.y = 0;
        Vector3 myPos = transform.parent.position;
        myPos.y = 0;
        m_atkCollider.knockVec = (targetPos - myPos).normalized;
        m_atkCollider.Attacking();
    }

    public void EndAttack()
    {
        Destroy(transform.parent.gameObject);
    }
}
