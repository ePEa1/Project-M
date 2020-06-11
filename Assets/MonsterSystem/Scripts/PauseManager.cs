using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public PlayerController player;
    public GameObject PausePage;
    public GameObject OptionPage;
    public bool IsPause = false;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PausePage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPause == true)
            {
                Return();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                IsPause = true;
                player.m_attack = KeyCode.None;
                Time.timeScale = 0;
                PausePage.SetActive(true);
            }
        }

    }

    public void Return()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        IsPause = false;
        player.m_attack = KeyCode.Mouse0;
        Time.timeScale = 1;
        OptionPage.SetActive(false);
        PausePage.SetActive(false);
    }

    public void OpenOption()
    {
        OptionPage.SetActive(true);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
