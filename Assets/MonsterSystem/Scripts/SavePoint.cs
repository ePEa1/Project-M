using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavePoint : MonoBehaviour
{

    public GameObject[] SavePointPos;
    public Vector3 SavePos;

    private void Awake()
    {
        SavePos = SavePointPos[0].transform.position;
    }
    public void SetSavePoint(int value)
    {
        SavePos = SavePointPos[value].transform.position;
    }
}
