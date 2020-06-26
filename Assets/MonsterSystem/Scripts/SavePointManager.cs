using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavePointManager : MonoBehaviour
{
    public GameObject player;
    [SerializeField] GameObject[] SavePointList;

    private void Awake()
    {
        Time.timeScale = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = SavePointList[DataController.Instance.gameData.FirstStageSavePointOrder].transform.position;
    }


}
