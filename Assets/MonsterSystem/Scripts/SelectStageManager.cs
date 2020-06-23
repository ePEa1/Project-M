using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStageManager : MonoBehaviour
{
    [SerializeField] string StageOne;
    [SerializeField] string StageTwo;
    [SerializeField] string StageThree;
    [SerializeField] string StageBoss;
    [SerializeField] float FadeSpeed;

    public Image FadeInImg;
    public Image FadeOutImg;

    public AudioSource EffSound;
    public AudioClip mouseOver;
    public AudioClip clicked;

    private void Awake()
    {
        StartCoroutine(FadeOut());
    }

    public void GotStageOne()
    {
        SetSound(clicked, EffSound);

        StartCoroutine(FadeInGameStart(StageOne));
    }

    public void GoStageTwo()
    {
        SetSound(clicked, EffSound);

        StartCoroutine(FadeInGameStart(StageTwo));
    }

    public void GoStageThree()
    {
        SetSound(clicked, EffSound);

        StartCoroutine(FadeInGameStart(StageThree));
    }

    public void GoStageBoss()
    {
        SetSound(clicked, EffSound);

        StartCoroutine(FadeInGameStart(StageBoss));

    }

    public void MouseOverButton()
    {
        SetSound(mouseOver, EffSound);
    }

    public void ClickButton()
    {
        SetSound(clicked, EffSound);
    }
    public void SetSound(AudioClip uisound, AudioSource Setplayer)
    {
        Setplayer.volume = DataController.Instance.effectSound;
        Setplayer.Stop();
        Setplayer.clip = uisound;
        Setplayer.time = 0;
        Setplayer.Play();
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
