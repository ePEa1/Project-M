using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearTest : MonoBehaviour
{
    public GameObject boss;
    public GameObject GameClearPanel;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Alice");
        GameClearPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(boss.GetComponent<AliceFSMManager>().IsDead == true)
        {
            getGameClear();
        }
    }

    public void getGameClear()
    {
        GameClearPanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
