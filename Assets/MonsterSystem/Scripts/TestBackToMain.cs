using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestBackToMain : MonoBehaviour
{
    public AliceFSMManager manager;
    public GameObject main;

    public bool IsEnd = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (manager.CurAliceHP <= 0)
        {
            IsEnd = true;
            main.SetActive(true);
        }
        else
        {
            main.SetActive(false);
            IsEnd = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(IsEnd == true)
            {
                SceneManager.LoadScene("StartScene");

            }
        }
    }

   public  void GotoMain()
    {
        SceneManager.LoadScene("StartScene");
    }
}
