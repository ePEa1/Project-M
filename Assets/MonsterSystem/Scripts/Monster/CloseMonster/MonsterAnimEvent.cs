using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimEvent : MonoBehaviour
{
    public MonsterFSMManager manager;


    private void Awake()
    {
        manager = transform.root.GetComponent<MonsterFSMManager>();
    }

    void AttachHitCheck()
    {
        manager.AttackCheck();
    }

}
