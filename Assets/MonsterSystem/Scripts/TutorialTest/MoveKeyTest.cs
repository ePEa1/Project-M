using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveKeyTest : MonoBehaviour
{
    public Image Wkey;
    public Image Akey;
    public Image Skey;
    public Image Dkey;

    public bool IsReady = false;
    bool IsW = false;
    bool IsA = false;
    bool IsS = false;
    bool IsD = false;

    [SerializeField] float RotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsW ==true && IsA == true&& IsS ==true && IsD == true)
        {
            IsReady = true;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (Wkey.transform.eulerAngles.z < 180)
            {
                IsW = true;
                Wkey.transform.Rotate(new Vector3(0, 0, 0), Space.Self);
            }
            else
            {
                Wkey.transform.Rotate(new Vector3(0, 0, RotateSpeed), Space.Self);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (Akey.transform.eulerAngles.z < 180)
            {
                IsA = true;
                Akey.transform.Rotate(new Vector3(0, 0, 0), Space.Self);
            }
            else
            {
                Akey.transform.Rotate(new Vector3(0, 0, RotateSpeed), Space.Self);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (Skey.transform.eulerAngles.z < 180)
            {
                IsS = true;
                Skey.transform.Rotate(new Vector3(0, 0, 0), Space.Self);
            }
            else
            {
                Skey.transform.Rotate(new Vector3(0, 0, RotateSpeed), Space.Self);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Dkey.transform.eulerAngles.z < 180)
            {
                IsD = true;
                Dkey.transform.Rotate(new Vector3(0, 0, 0), Space.Self);
            }
            else
            {
                Dkey.transform.Rotate(new Vector3(0, 0, RotateSpeed), Space.Self);
            }
        }
    }
}
