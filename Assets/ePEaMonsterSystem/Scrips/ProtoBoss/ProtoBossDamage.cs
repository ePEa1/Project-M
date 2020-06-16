using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.ePEa.ProtoMon
{
    public class ProtoBossDamage : MonoBehaviour
    {
        [SerializeField] ProtoBossFSM m_owner;
        [SerializeField] GameObject m_damSfx;
        [SerializeField] GameObject m_damEff;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag =="PCAtkCollider")
            {
                m_owner.TakeDamage(other.GetComponent<AtkCollider>().atkDamage);
                GameObject eff = Instantiate(m_damEff);
                eff.transform.position = m_owner.transform.position - other.GetComponent<AtkCollider>().knockVec * 1f + Vector3.up * 1.5f;

                if (other.GetComponent<AtkCollider>().AtkEvent())
                {
                    DataController.Instance.SetCombo();
                    GameObject sfx = Instantiate(m_damSfx);
                    sfx.transform.position = m_owner.transform.position;
                }
            }
        }
    }
}
