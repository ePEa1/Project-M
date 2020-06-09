using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    GameData volumeData;

    private void Awake()
    {
        volumeData = GameObject.Find("GameData").GetComponent<GameData>();
    }

    // Start is called before the first frame update
    void Update()
    {
        GetComponent<AudioSource>().volume = volumeData.BackgroundSound * 0.01f;
    }
}
