using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AtkCollider : MonoBehaviour
{
    public float atkDamage;
    public float knockPower;
    public Vector3 knockVec;
    public float knockTime;

    [SerializeField] UnityEvent m_atkEvents;
    public bool isAttacking { get; set; }

    private void Awake()
    {
        isAttacking = false;
    }

    public void AtkEvent()
    {
        if (!isAttacking)
        {
            m_atkEvents.Invoke();
            isAttacking = true;
        }
    }
}