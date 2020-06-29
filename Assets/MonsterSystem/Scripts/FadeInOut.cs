using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeInOut : MonoBehaviour
{
    public Image FadeInImg;
    public Image FadeOutImg;
    [SerializeField] float FadeSpeed;

    private void Start()
    {
        StartCoroutine(FadeOut());
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
