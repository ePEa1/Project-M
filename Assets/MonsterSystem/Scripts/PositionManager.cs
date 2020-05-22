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

    public float maxXVal;
    public float minXVal;
    public float maxZVal;
    public float minZVal;
    // Start is called before the first frame update
    void Start()
    {
        MaxX = transform.GetChild(0).GetComponent<GameObject>();
        MinX = transform.GetChild(1).GetComponent<GameObject>();
        MaxZ = transform.GetChild(2).GetComponent<GameObject>();
        MinZ = transform.GetChild(3).GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target.transform.position.x > MaxX.transform.position.x)
        {
           // target.transform.position.x = MaxX.transform.position.x;
        }
    }
}
