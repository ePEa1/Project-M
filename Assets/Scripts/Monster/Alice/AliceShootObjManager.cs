using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceShootObjManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0, 0, -0.5f) ;
    }
}
