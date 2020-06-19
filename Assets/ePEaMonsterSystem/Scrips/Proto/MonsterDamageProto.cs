using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ProjectM.ePEa.ProtoMon
{
    public class MonsterDamageProto : MonoBehaviour, DamageModel
    {
        [SerializeField] MonsterProto m_owner;
        [SerializeField] GameObject m_damSfx;

        public void TakeDamage(AtkCollider dam)
        {
            m_owner.TakeDamage(dam.atkDamage, dam.knockVec, dam.knockPower);
            if (dam.GetComponent<AtkCollider>().AtkEvent())
            {
                transform.GetComponent<AudioSource>().volume = DataController.Instance.gameData.EffectSound;
                DataController.Instance.SetCombo();
                GameObject sfx = Instantiate(m_damSfx);
                sfx.transform.position = m_owner.transform.position;
            }
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.tag =="PCAtkCollider")
        //    {
        //        m_owner.TakeDamage(other.GetComponent<AtkCollider>().atkDamage, other.GetComponent<AtkCollider>().knockVec, other.GetComponent<AtkCollider>().knockPower);
        //        if (other.GetComponent<AtkCollider>().AtkEvent())
        //        {
        //            transform.GetComponent<AudioSource>().volume = DataController.Instance.gameData.EffectSound;
        //            DataController.Instance.SetCombo();
        //            GameObject sfx = Instantiate(m_damSfx);
        //            sfx.transform.position = m_owner.transform.position;
        //        }
        //    }
        //}
    }
}