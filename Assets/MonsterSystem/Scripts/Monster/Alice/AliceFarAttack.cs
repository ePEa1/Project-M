using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceFarAttack : MonoBehaviour
{
    int ObjSize = 6;
    int SetOne;
    int SetTwo;
    int SetThree;
    public GameObject Pos1;
    public GameObject Pos2;
    public GameObject Pos3;

    public GameObject ShootObj1;
    public GameObject ShootObj2;
    public GameObject ShootObj3;
    public GameObject ShootObj4;
    public GameObject ShootObj5;

    List<GameObject> ShootObjs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        ShootObjs.Add(ShootObj1);
        ShootObjs.Add(ShootObj2);
        ShootObjs.Add(ShootObj3);
        ShootObjs.Add(ShootObj4);
        ShootObjs.Add(ShootObj5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OneSetObject()
    {
        SetOne = Random.Range(0, 5);
        Instantiate(ShootObjs[SetOne], Pos1.transform.position, Quaternion.identity);
    }
    public void TwoSetObject()
    {
        SetTwo = Random.Range(0, 5);
        Instantiate(ShootObjs[SetTwo], Pos2.transform.position, Quaternion.identity);

    }

    public void ThreeSetObject()
    {
        SetThree = Random.Range(0, 5);
        Instantiate(ShootObjs[SetThree], Pos3.transform.position, Quaternion.identity);

    }
}
