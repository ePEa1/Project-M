using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class FirstStageTestManager : MonoBehaviour
{

    public Collider Potal;
    public MeshRenderer renderer;
    private void Start()
    {
        renderer.enabled = false;
        Potal.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (DataController.Instance.gameData.firstStageMonster <= 0)
        {
            renderer.enabled = true;
            Potal.enabled = true;
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
