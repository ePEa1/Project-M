using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterDamage : MonsterFSMBase, DamageModel
{
    public MonsterFSMPlayer monster;
    [SerializeField] GameObject damageSound;
    // Start is called before the first frame update
    void Start()
    {
       // monster = GetComponentInParent<MonsterFSMPlayer>();
    }
    

    public void TakeDamage(AtkCollider dam)
    {
        monster.TakeDamage(dam.atkDamage, dam.knockVec, dam.knockPower);
        monster.anim.Rebind();
        monster.anim.Play("DAMAGE");
        monster.SetState(MonsterState.Damage);

        if (dam.GetComponent<AtkCollider>().AtkEvent())
        {

            GameObject sfx = Instantiate(damageSound);
            sfx.transform.position = monster.transform.position;
        }
    }
}
