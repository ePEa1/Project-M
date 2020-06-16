using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxVolume : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume = DataController.Instance.effectSound;
    }
}
