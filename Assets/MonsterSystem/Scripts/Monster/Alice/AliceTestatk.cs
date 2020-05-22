using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceTestatk : MonoBehaviour
{

    public AliceFSMManager manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<AliceFSMManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
