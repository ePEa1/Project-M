using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public GameObject OptionPage;
    [SerializeField] GameObject PressAnyButtonScreen;
    [SerializeField] GameObject Buttons;
    [SerializeField] Image FadeImg;
    [SerializeField] float FadeSpeed;
    public AudioClip Select;
    public AudioClip Push;
    public AudioSource UISound;
    public AudioSource BackgroundSound;
    [SerializeField] AnimationCurve FadeinOut;
    [SerializeField] AnimationCurve PressButtonFadeOut;
    // Start is called before the first frame update

    [SerializeField] float FadeTime;
    [SerializeField] string m_nextScene;

    void Start()
    {
        Buttons.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        FadeImg.color = new Vector4(0, 0, 0, 0);
        UISound = GetComponent<AudioSource>();
        Time.timeScale = 1;
        //RayCastHitButton();
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundSound.volume = DataController.Instance.backgroundSound;

        if (Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow)||
            Input.GetKeyDown(KeyCode.RightArrow)||
            Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetSound(Select, UISound);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
        }
    }
    public void StartGameButton()
    {
        SetSound(Push, UISound);

        StartCoroutine(FadeInGameStart());
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
    public void SetSound(AudioClip uisound, AudioSource Setplayer)
    {
        Setplayer.volume = DataController.Instance.effectSound;
        Setplayer.Stop();
        Setplayer.clip = uisound;
        Setplayer.time = 0;
        Setplayer.Play();
    }
    public void HitButton()
    {
            SetSound(Select, UISound);
    }
    public void PressAnyButton()
    {

        PressAnyButtonScreen.SetActive(false);
        Buttons.SetActive(true);


        Debug.Log("check");
    }

    IEnumerator FadeInGameStart()
    {

        for (float i = 0f; i >= 0; i += 0.005f*FadeSpeed)
        {
            Color color = new Vector4(0, 0, 0, i);
            FadeImg.color = color;

            if(FadeImg.color.a >= 1)
            {
                LoadingSceneManager.LoadScene(m_nextScene);

            }

            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        for (float i = 0f; i >= 0; i -= 0.005f * FadeSpeed)
        {
            Color color = new Vector4(0, 0, 0, i);
            FadeImg.color = color;

            yield return null;
        }
    }

}
