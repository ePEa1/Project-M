using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public GameObject PausePage;
    public GameObject OptionPage;
    public bool IsPause = false;


    //public void IsPause()
    //{
    //    PausePage.SetActive(true);
    //}


    // Start is called before the first frame update
    void Start()
    {
        PausePage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPause == true)
            {

                Cursor.lockState = CursorLockMode.Locked;

                IsPause = false;
                Time.timeScale = 1;
                PausePage.SetActive(false);

            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                IsPause = true;
                Time.timeScale = 0;
                PausePage.SetActive(true);
            }
        }

    }

    public void Return()
    {
        Cursor.lockState = CursorLockMode.Locked;

        IsPause = false;
        Time.timeScale = 1;
        PausePage.SetActive(false);

    }
    public void OpenOption()
    {
        OptionPage.SetActive(true);
    }
}
