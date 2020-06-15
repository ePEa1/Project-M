using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProjectM.ePEa.PlayerData;


public class PauseManager : MonoBehaviour
{
    public PlayerFsmManager player;
    public GameObject PausePage;
    public GameObject OptionPage;
    public bool IsPause = false;




    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFsmManager>();
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
                if (PlayerStats.playerStat.m_currentHp <= 0)
                {
                    IsPause = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    player.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
                    player.enabled = false;
                    Cursor.visible = true;

                    IsPause = true;
                    Time.timeScale = 0;
                    PausePage.SetActive(true);
                }
                
            }
        }

    }

    public void Return()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.enabled = true;
        IsPause = false;
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
