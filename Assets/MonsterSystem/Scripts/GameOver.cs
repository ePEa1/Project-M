using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }

    public void getGameOver()
    {
        
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;

    }
}
