using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceRush : MonoBehaviour
{
    public Collider RushCol;
    public AliceCOMBAT combat;
    // Start is called before the first frame update
    void Start()
    {
        RushCol.enabled = false;
        combat = GetComponent<AliceCOMBAT>();
    }

    // Update is called once per frame
    void Update()
    {
        if (combat.IsRush == true)
        {

            transform.position = Vector3.MoveTowards(transform.position, combat.RushPos, 2);

            Vector3 destinationposition = new Vector3(combat.RushPos.x - transform.position.x, 0, combat.RushPos.z - transform.position.z);

            Vector3 diff = combat.RushPos - destinationposition;
            Vector3 groundCheck = diff - combat.RushPos;

            if (groundCheck.sqrMagnitude <= 0.5f)
            {
                combat.IsRush = false;
                combat.CurPatternCheck(AliceAttackState.Combat);
            }
        }
    }
    public void SetRushCol()
    {
        RushCol.enabled = true;
    }

    public void DeleteRushCol()
    {
        RushCol.enabled = false;
    }
}
