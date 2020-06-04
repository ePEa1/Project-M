using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoTargetManager : MonoBehaviour
{

    Camera cam;
    TargetView target;
    Image targetImg;

    bool TargetOn;
    int TargetCount;

    public static List<TargetView> nearOrder = new List<TargetView>();

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        targetImg = gameObject.GetComponent<Image>();

        targetImg.enabled = false;
        TargetOn = false;
        TargetCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)&& !TargetOn)
        {
            if (nearOrder.Count >= 1)
            {
                TargetOn = true;
                targetImg.enabled = true;

                TargetCount = 0;
                target = nearOrder[TargetCount];
            }
        }
        else if((Input.GetKeyDown(KeyCode.X)&&TargetOn)|| nearOrder.Count == 0)
        {
            TargetOn = false;
            targetImg.enabled = false;
            TargetCount = 0;
            target = null;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(TargetCount == nearOrder.Count - 1)
            {
                TargetCount = 0;
                target = nearOrder[TargetCount];
            }
            else
            {
                TargetCount++;
                target = nearOrder[TargetCount];
            }
        }
        if (TargetOn)
        {
            target = nearOrder[TargetCount];
            gameObject.transform.position = cam.WorldToScreenPoint(target.transform.position);

            gameObject.transform.Rotate(new Vector3(0, 0, -1));
        }
    }
}
