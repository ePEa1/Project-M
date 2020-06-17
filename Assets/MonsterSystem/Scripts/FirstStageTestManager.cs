using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class FirstStageTestManager : MonoBehaviour
{

    public Collider Potal;
    public MeshRenderer Potalrenderer;

    public GameObject FindMonster;
    private void Start()
    {
        Potalrenderer.enabled = false;
        Potal.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (null == GameObject.FindGameObjectWithTag("DefaultMob")) 
        {
            Potalrenderer.enabled = true;
            Potal.enabled = true;
        }
        else
        {
            Potalrenderer.enabled = false;
            Potal.enabled = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerDamage")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            LoadingSceneManager.LoadScene("StageSelectScene");
        }
    }
}
