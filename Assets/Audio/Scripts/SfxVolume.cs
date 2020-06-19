using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxVolume : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().volume = DataController.Instance.effectSound;
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume = DataController.Instance.effectSound;
    }
}
