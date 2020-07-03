using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (dam.GetComponent<AtkCollider>().AtkEvent())
        {
            monster.anim.Rebind();
            monster.anim.Play("DAMAGE");

            monster.TakeDamage(dam.atkDamage, dam.knockVec, dam.knockPower);
            monster.SetState(MonsterState.Damage);
            GameObject sfx = Instantiate(damageSound);
            sfx.transform.position = monster.transform.position;
        }
    }
}
