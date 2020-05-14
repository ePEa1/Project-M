using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMOVE : MonsterFSMState
{

    public Vector3 destination;
    public Vector3 diff;
    public Vector3 groundCheck;
    public float Speed = 0.1f;
    public bool Moving = false;
    public Rigidbody rig;

    public override void BeginState()
    {
        base.BeginState();
        destination = new Vector3((transform.position.x+Random.Range(-10, 10)), transform.position.y, (transform.position.z+Random.Range(-10, 10)));
    }

    private void Awake()
    {

        manager = GetComponent<MonsterFSMManager>();

    }
    private void Start()
    {

    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (!Util.Detect(transform.position, manager.playerObj.transform.position,6))
        {
            manager.SetState(DummyState.CHASE);
            return;
        }

        Util.CKMove(
            manager.gameObject,
    transform,
    destination,
    manager.stat.moveSpeed,
    manager.stat.rotateSpeed,
    manager.stat.fallSpeed);
        StartCoroutine("WaitForIt");



        diff = destination - transform.position;
        if (diff.sqrMagnitude < 0.3f * 0.3f)
        {
            manager.SetState(DummyState.IDLE);
        }


    }
    IEnumerator WaitForIt()
    {
     //   Moving = true;
        Debug.Log("IDLECount");
            yield return new WaitForSeconds(0.1f);
        Moving = false;

    }

}
