using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFsmAnimator : MonoBehaviour
{
    public MonsterFSMPlayer monster;

    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponentInParent<MonsterFSMPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndAttack()
    {
        monster.IsChase = true;
        monster.SetState(MonsterState.Chase);
    }
}
