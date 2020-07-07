using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FadeInOut : MonoBehaviour
{
    public Image FadeInImg;
    public Image FadeOutImg;
    public GameObject player;
    [SerializeField] float FadeSpeed;
    

    private void Start()
    {
        StartCoroutine(FadeOut());

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    void Update()
    {
        if (player.GetComponent<PlayerFsmManager>().IsDead == true)
        {
            StartCoroutine(FadeInGameOver());
        }
    }

    public void GetFadeOut()
    {
    }
    IEnumerator FadeInGameStart(string scene)
    {

        for (float i = 0f; i >= 0; i += 0.005f * FadeSpeed)
        {
            Color color = new Vector4(0, 0, 0, i);
            FadeOutImg.color = color;

            if (FadeOutImg.color.a >= 1)
            {
                LoadingSceneManager.LoadScene(scene);

            }

            yield return null;
        }
    }
    IEnumerator FadeInGameOver()
    {

        for (float i = 0f; i >= 0; i += 0.005f * FadeSpeed)
        {
            Color color = new Vector4(0, 0, 0, i);
            FadeOutImg.color = color;

            if (FadeOutImg.color.a >= 1)
            {
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }

            yield return null;
        }
    }
    IEnumerator FadeOut()
    {
        for (float i = 1f; i <= 1; i -= 0.005f * FadeSpeed)
        {
            Color color = new Vector4(0, 0, 0, i);
            FadeInImg.color = color;

            yield return null;
        }
    }

}
