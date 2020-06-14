﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public GameObject OptionPage;
    public AudioClip Select;
    public AudioClip Push;
    public AudioSource UISound;
    public AudioSource BackgroundSound;
    // Start is called before the first frame update

    [SerializeField] string m_nextScene;

    void Start()
    {

        UISound = GetComponent<AudioSource>();
        Time.timeScale = 1;
        //RayCastHitButton();
    }
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
    // Update is called once per frame
    void Update()
    {
        BackgroundSound.volume = DataController.Instance.backgroundSound;

        if (Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow)||
            Input.GetKeyDown(KeyCode.RightArrow)||
            Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetSound(Select, UISound);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
        }
    }
    public void StartGameButton()
    {
        SetSound(Push, UISound);

        SceneManager.LoadScene(m_nextScene);
    }
    public void OpenOption()
    {
        OptionPage.SetActive(true);
    }
    public void CloseOption()
    {
        OptionPage.SetActive(false);
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void SetSound(AudioClip uisound, AudioSource Setplayer)
    {
        Setplayer.volume = DataController.Instance.effectSound;
        Setplayer.Stop();
        Setplayer.clip = uisound;
        Setplayer.time = 0;
        Setplayer.Play();
    }
    public void RayCastHitButton()
    {
        RaycastHit hit = new RaycastHit();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(hit.collider.gameObject.tag == "MainUI")
        {
            SetSound(Select, UISound);
        }
    }

}
