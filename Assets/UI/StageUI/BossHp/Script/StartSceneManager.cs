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

    public GameObject CreditObj;
    // Start is called before the first frame update

    [SerializeField] float FadeTime;
    [SerializeField] string m_nextScene;
    string gotoNextScene;

    void Start()
    {
        DataController.Instance.PlayReset();
        Buttons.SetActive(false);
        OptionPage.SetActive(false);
        CreditObj.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Screen.SetResolution(1920, 1080, true);

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
        if (DataController.Instance.gameData.IsIntroShow == true)
        {
            gotoNextScene = m_nextScene;
        }
        else
        {
            gotoNextScene = "CutScene";
        }
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
                LoadingSceneManager.LoadScene(gotoNextScene);

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
   public void OpenCredit()
    {
        CreditObj.SetActive(true);
    }
    public void CloseCredit()
    {
        CreditObj.SetActive(false);
    }
}
