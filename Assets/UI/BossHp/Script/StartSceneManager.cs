using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public GameObject OptionPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGameButton()
    {
        SceneManager.LoadScene("GraphiWithMonster");
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
}
