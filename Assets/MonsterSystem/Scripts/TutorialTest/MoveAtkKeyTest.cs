using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAtkKeyTest : MonoBehaviour
{
    public GameObject AOnekey;
    public GameObject DOnekey;
    public GameObject Shiftkey;


    int Count;
    public bool IsReady = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Count += 1;
            }
        }

        if (Count >= 4)
        {
            IsReady = true;
        }
    }
}
