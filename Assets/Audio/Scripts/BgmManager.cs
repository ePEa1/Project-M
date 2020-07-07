using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BgmManager : MonoBehaviour//이건 스테이지 용도로
{
    public PauseManager pause;
    private void Awake()
    {
        if(GameObject.FindGameObjectWithTag("MainUI") != null)
        {
            pause = GameObject.FindGameObjectWithTag("MainUI").GetComponent<PauseManager>();
        }
    }

    // Start is called before the first frame update
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("MainUI") != null)
        {
            if (pause.IsPause == true)
            {
                GetComponent<AudioSource>().volume = 0;
            }
            if (pause.IsPause == false)
            {
                GetComponent<AudioSource>().volume = DataController.Instance.backgroundSound;

            }

        }

    }
}
