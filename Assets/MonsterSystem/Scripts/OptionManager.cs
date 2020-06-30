using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class OptionManager : MonoBehaviour
{
    public GameObject OptionScreen;
    public GameObject SoundPage;
    public GameObject GraphicPage;
    public GameObject ControlPage;

    public Slider BackGroundSound;
    public Slider EffectsSound;
    public Slider MouseMoving;


    // 그래픽 옵션
    public Dropdown ResolutionDropdown;
    public Dropdown WindowSettingDropdown;

    [SerializeField] int[] resolutionXList;
    [SerializeField] int[] resolutionYList;
    [SerializeField] Resolution[] resolutions;

   
    private void Start()
    {
        DataController.Instance.backgroundSound = (float)DataController.Instance.gameData.BackgroundSound / 100;
        DataController.Instance.effectSound = (float)DataController.Instance.gameData.EffectSound / 100;
        DataController.Instance.mouseMoving = (float)DataController.Instance.gameData.MouseMoving / 100;
        ResolutionDropdown.value = DataController.Instance.gameData.ResolutionNum;
        WindowSettingDropdown.value = DataController.Instance.gameData.WindowNum;
        //OptionScreen.SetActive(false);
        //resolutions = Screen.resolutions;

        //ResolutionDropdown.ClearOptions();

        //List<string> options = new List<string>();

        //int currentResolutionIndex = 0;

        //for(int  i = 0; i<resolutions.Length; i++)
        //{
        //    string option = resolutions[i].width + " x " + resolutions[i].height;
        //    options.Add(option);
        //    //options = options.Distinct().ToList();

        //    if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
        //    {
        //        currentResolutionIndex = i;
        //    }
        //}
        //ResolutionDropdown.AddOptions(options);
        //ResolutionDropdown.value = currentResolutionIndex;
        //ResolutionDropdown.RefreshShownValue();

    }
    // Start is called before the first frame update
    void Awake()
    {
        SoundPage.SetActive(true);
        GraphicPage.SetActive(false);
        ControlPage.SetActive(false);
        BackGroundSound.value = DataController.Instance.backgroundSound ;
        EffectsSound.value = DataController.Instance.effectSound;
        MouseMoving.value = DataController.Instance.mouseMoving;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            DataController.Instance.SetOption(DataController.Option.backGround, (int)(BackGroundSound.value * 100));
            DataController.Instance.SetOption(DataController.Option.effect, (int)(EffectsSound.value * 100));
            
        }
    }

    public void CloseOption()
    {
        OptionScreen.SetActive(false);
    }

    public void OpenOption()
    {
        OptionScreen.SetActive(true);

    }

    public void OpenSoundPage()
    {
        SoundPage.SetActive(true);
        GraphicPage.SetActive(false);
        ControlPage.SetActive(false);

    }
    public void OpenGraphicPage()
    {
        SoundPage.SetActive(false);
        GraphicPage.SetActive(true);
        ControlPage.SetActive(false);

    }
    public void OpenControlPage()
    {
        SoundPage.SetActive(false);
        GraphicPage.SetActive(false);
        ControlPage.SetActive(true);

    }


    public void ResolutionSetting(int resolutionIndex)
    {
        DataController.Instance.gameData.ResolutionNum = resolutionIndex;
        DataController.Instance.gameData.ResolutionX = resolutionXList[resolutionIndex];
        DataController.Instance.gameData.ResolutionY = resolutionYList[resolutionIndex];
        DataController.Instance.SetScreen();

    }

    public void FullScreenSetting(int fullscreenIndex)
    {
        DataController.Instance.gameData.WindowNum = fullscreenIndex;
        switch (fullscreenIndex)
        {
            case 0:
                DataController.Instance.gameData.fullScreen = true;
                break;
            case 1:
                DataController.Instance.gameData.fullScreen = false;
                break;
        }
        DataController.Instance.SetScreen();
    }

    public void MouseMovingSetting(int mousemovingValue)
    {
        DataController.Instance.SetOption(DataController.Option.mouse, (int)((1 - MouseMoving.value) * 100));
    }
}
