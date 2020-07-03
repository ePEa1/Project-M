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

    void AtkEndAction()
    {
        monster.AtkEnd();
    }
    void EndAction()
    {
        monster.SetState(MonsterState.Idle);
    }
}
