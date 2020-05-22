using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterATTACK : MonsterFSMState
{
    public MonsterCHASE chase;
    public Collider AttackCol;
    public override void BeginState()
    {
        base.BeginState();

    }
    private void Start()
    {
        AttackCol = transform.GetChild(2).GetComponent<Collider>();
        AttackCol.enabled = false;

    }
    // Update is called once per frame
    void Update()
    {
        if (!Util.Detect(transform.position, manager.playerObj.transform.position,6))
        {
            manager.SetState(DummyState.CHASE);
            return;
        }
        Vector3 destination = manager.playerObj.transform.position;

        Vector3 destinationposition = new Vector3(destination.x - transform.position.x, 0, destination.z - transform.position.z);
        Vector3 diff = destination - destinationposition;
        Vector3 groundCheck = diff - destination;



        if (groundCheck.sqrMagnitude > manager.stat.attackRange * manager.stat.attackRange)
            {
                manager.SetState(DummyState.CHASE);

            }

        Util.CKRotate(transform, manager.playerObj.transform.position, manager.stat.rotateSpeed);
        
    }

    IEnumerator SetCollider()
    {
        AttackCol.enabled = true;
        yield return new WaitForSeconds(0.2f);
        AttackCol.enabled = false;
    }
}
