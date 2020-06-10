using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestBackToMain : MonoBehaviour
{

    public bool IsEnd = false;


   public  void GotoMain()
    {
        Time.timeScale = 1;
        Debug.Log(Time.timeScale);
        SceneManager.LoadScene("StartScene");

    }
}
