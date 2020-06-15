using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] AnimationCurve FadeOut; //넉백 이동 커브
    public GameObject player;
    public GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerFsmManager>().IsDead == true)
        {
            getGameOver();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("StartScene");
            }
        }
    }

    public void getGameOver()
    {
        Cursor.visible = true;

        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;

    }
    public void GotoMain()
    {
        Time.timeScale = 1;
        Debug.Log(Time.timeScale);
        SceneManager.LoadScene("StartScene");

    }
}
