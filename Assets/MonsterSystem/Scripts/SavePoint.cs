using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavePoint : MonoBehaviour
{
    [SerializeField] int SavePointCount = 1;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DataController.Instance.gameData.FirstStageSavePointOrder = SavePointCount;
            Debug.Log("SavePointCount  :  " + SavePointCount);
        }
    }
}
