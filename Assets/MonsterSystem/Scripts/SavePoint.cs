using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavePoint : MonoBehaviour
{
    [SerializeField] int SavePointCount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerDamage")
        {
            DataController.Instance.gameData.FirstStageSavePointOrder = SavePointCount;
            Debug.Log("SavePointCount  :  " + SavePointCount);
        }
    }
}
