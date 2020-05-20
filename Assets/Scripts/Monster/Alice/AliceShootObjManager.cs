using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceShootObjManager : AliceFSMState
{
    public Vector3 playerPos;
    public Quaternion playerRot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerRot = Quaternion.LookRotation(playerPos);
        transform.Translate(new Vector3(0, 0, 1));
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerDamage")
        {
            //데미지 넣는 함수
            Destroy(gameObject);
        }
    }
}
