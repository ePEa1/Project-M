using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavePoint : MonoBehaviour
{
    [SerializeField] int SavePointCount;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "PCAtkCollider")
        {
            DataController.Instance.gameData.FirstStageSavePointOrder = SavePointCount;
            Debug.Log("SavePointCount  :  " + SavePointCount);
        }
    }
}
