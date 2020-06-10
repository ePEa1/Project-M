using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BgmManager : MonoBehaviour
{
    public PauseManager pause;
    private void Awake()
    {
        pause = GameObject.FindGameObjectWithTag("MainUI").GetComponent<PauseManager>();

    }

    // Start is called before the first frame update
    void Update()
    {
        if(pause.IsPause == true)
        {
            GetComponent<AudioSource>().volume = 0;

        }
        if (pause.IsPause == false)
        {
            GetComponent<AudioSource>().volume = DataController.Instance.backgroundSound;

        }

    }
}
