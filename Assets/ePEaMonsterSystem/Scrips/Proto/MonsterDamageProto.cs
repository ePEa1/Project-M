﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ProjectM.ePEa.ProtoMon
{
    public class MonsterDamageProto : MonoBehaviour
    {
        [SerializeField] MonsterProto m_owner;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag =="PCAtkCollider")
            {
                m_owner.TakeDamage(other.GetComponent<AtkCollider>().atkDamage, other.GetComponent<AtkCollider>().knockVec, other.GetComponent<AtkCollider>().knockPower);
                if (other.GetComponent<AtkCollider>().AtkEvent())
                {
                    m_owner.PlaySound();
                }
            }
        }
    }
}