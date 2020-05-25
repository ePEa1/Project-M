using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceShootObjManager : MonoBehaviour
{
    public GameObject Alice;
    public GameObject player;
    public Vector3 playerPos;
    public Quaternion playerRot;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Alice = GameObject.FindGameObjectWithTag("Alice");
        transform.rotation = Alice.transform.rotation;
        playerPos = player.transform.position;
    }

    
    // Update is called once per frame
    void Update()
    {

        //transform.Translate(Vector3.forward*2,Space.World);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, 0.8f);
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Debug.Log("IsGround");
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "PlayerDamage")
        {
            //데미지 넣는 함수
            Destroy(gameObject);
        }

    }
}
