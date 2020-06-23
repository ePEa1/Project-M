using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectStageManager : MonoBehaviour
{
    [SerializeField] string StageOne;
    [SerializeField] string StageTwo;
    [SerializeField] string StageThree;
    [SerializeField] string StageBoss;

    public AudioSource EffSound;
    public AudioClip mouseOver;
    public AudioClip clicked;

    public void GotStageOne()
    {
        SetSound(clicked, EffSound);

        LoadingSceneManager.LoadScene(StageOne);
    }

    public void GoStageTwo()
    {
        SetSound(clicked, EffSound);

        LoadingSceneManager.LoadScene(StageTwo);
    }

    public void GoStageThree()
    {
        SetSound(clicked, EffSound);

        LoadingSceneManager.LoadScene(StageThree);
    }

    public void GoStageBoss()
    {
        SetSound(clicked, EffSound);

        LoadingSceneManager.LoadScene(StageBoss);

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


}
