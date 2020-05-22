using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public GameObject target;
    public Vector3 LimitPos;
    public GameObject MaxX;
    public GameObject MinX;
    public GameObject MaxZ;
    public GameObject MinZ;

    public Vector3 targetPos;

    public float maxXVal;
    public float minXVal;
    public float maxZVal;
    public float minZVal;
    // Start is called before the first frame update
    void Start()
    {
        //MaxX = transform.GetChild(0).GetComponent<GameObject>();
        //MinX = transform.GetChild(1).GetComponent<GameObject>();
        //MaxZ = transform.GetChild(2).GetComponent<GameObject>();
        //MinZ = transform.GetChild(3).GetComponent<GameObject>();
        maxXVal = MaxX.transform.position.x;
        minXVal = MinX.transform.position.x;
        maxZVal = MaxZ.transform.position.z;
        minZVal = MinZ.transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        targetPos = target.transform.position;
        if(target.transform.position.x >= maxXVal)
        {
           target.transform.position = new Vector3(maxXVal, target.transform.position.y, target.transform.position.z);
        }
        if (target.transform.position.x <= minZVal)
        {
            target.transform.position = new Vector3(minXVal, target.transform.position.y, target.transform.position.z);
        }
        if (target.transform.position.z >= maxZVal)
        {
            target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, maxZVal);
        }
        if (target.transform.position.z <= minZVal)
        {
            target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, minZVal);
        }
    }

    
}
