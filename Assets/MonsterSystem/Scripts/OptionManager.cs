using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.Rendering.PostProcessing;

public class OptionManager : MonoBehaviour
{
    public PostProcessVolume volume;
    public AmbientOcclusion ambient;
    public PostProcessLayer pp_layer;
   

    public Light DirectLight;
    public GameObject OptionScreen;
    public GameObject SoundPage;
    public GameObject GraphicPage;
    public GameObject ControlPage;

    public Button VsyncButton;
    public GameObject VsyncOn;
    public GameObject VsyncOff;

    public Button AmbientOcclusion;
    public GameObject AmbientOcclusionOn;
    public GameObject AmbientOcclusionOff;

    public Button FogButton;
    public GameObject FogOn;
    public GameObject FogOff;

    public Slider BackGroundSound;
    public Slider EffectsSound;
    public Slider MouseMoving;

    // 그래픽 옵션
    public Dropdown ResolutionDropdown;
    public Dropdown WindowSettingDropdown;
    public Dropdown ShadowSettiongDropdown;
    public Dropdown TextureQualityDropdown;
    public Dropdown AntiAliasingDropdown;

    [SerializeField] int[] resolutionXList;
    [SerializeField] int[] resolutionYList;
    [SerializeField] Resolution[] resolutions;

   
    private void Start()
    {
        if(GameObject.FindGameObjectWithTag("PostProcessing") != null)
        {
            volume = GameObject.FindGameObjectWithTag("PostProcessing").GetComponent<PostProcessVolume>();
            volume.profile.TryGetSettings(out ambient);

        }
        if (GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            pp_layer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessLayer>();
        }
        DirectLight = GameObject.FindGameObjectWithTag("DirectionalLight").GetComponent<Light>();
        DataController.Instance.backgroundSound = (float)DataController.Instance.gameData.BackgroundSound / 100;
        DataController.Instance.effectSound = (float)DataController.Instance.gameData.EffectSound / 100;
        DataController.Instance.mouseMoving = (float)DataController.Instance.gameData.MouseMoving / 100;
        ResolutionDropdown.value = DataController.Instance.gameData.ResolutionNum;
        WindowSettingDropdown.value = DataController.Instance.gameData.WindowNum;
        ShadowSettiongDropdown.value = DataController.Instance.gameData.Shadow;
        TextureQualityDropdown.value = DataController.Instance.gameData.TexturQuality;
        AntiAliasingDropdown.value = DataController.Instance.gameData.AntiAliasing;


    }
    // Start is called before the first frame update
    void Awake()
    {
        SoundPage.SetActive(true);
        GraphicPage.SetActive(false);
        ControlPage.SetActive(false);
        BackGroundSound.value = DataController.Instance.backgroundSound ;
        EffectsSound.value = DataController.Instance.effectSound;
        MouseMoving.value = 1-DataController.Instance.mouseMoving;
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

    public void ShadowSetting(int shadow)
    {
        DataController.Instance.gameData.Shadow = shadow;
        switch (shadow)
        {
            case 0:
                DirectLight.shadowResolution = UnityEngine.Rendering.LightShadowResolution.Low;
                break;

            case 1:
                DirectLight.shadowResolution = UnityEngine.Rendering.LightShadowResolution.Medium;
                break;

            case 2:
                DirectLight.shadowResolution = UnityEngine.Rendering.LightShadowResolution.High;
                break;

            case 3:
                DirectLight.shadowResolution = UnityEngine.Rendering.LightShadowResolution.VeryHigh;
                break;
        }
    }

    public void QualityLevelSetting(int qualitylevalValue)
    {
        DataController.Instance.gameData.TexturQuality = qualitylevalValue;
        switch (qualitylevalValue)
        {
            case 0:
                QualitySettings.SetQualityLevel(0);
                break;

            case 1:
                QualitySettings.SetQualityLevel(2);
                break;

            case 2:
                QualitySettings.SetQualityLevel(3);
                break;

            case 3:
                QualitySettings.SetQualityLevel(5);
                break;
        }
    }

    public void AntiAliasingSetting(int antialiasingValue)
    {
        DataController.Instance.gameData.AntiAliasing = antialiasingValue;
        switch (antialiasingValue)
        {
            case 0:
                pp_layer.antialiasingMode = PostProcessLayer.Antialiasing.None;
                break;
            case 1:
                pp_layer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
                break;
            case 2:
                pp_layer.antialiasingMode = PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing;
                break;
            case 3:
                pp_layer.antialiasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;
                break;
        }
    }

    public void MouseMovingSetting(Slider mousemovingValue)
    {
        Debug.Log(DataController.Instance.mouseMoving);

        float mmValue = 1 - mousemovingValue.value;
        DataController.Instance.SetOption(DataController.Option.mouse, (int)(mmValue * 100));
    }

    public void BackgroundSetting(Slider backgroundValue)
    {
        DataController.Instance.SetOption(DataController.Option.backGround, (int)(backgroundValue.value * 100));
    }

    public void EffectSoundSetting(Slider effectsoundValue)
    {
        DataController.Instance.SetOption(DataController.Option.effect, (int)(effectsoundValue.value * 100));
    }

    public void VSyncSetting()
    {
        if(DataController.Instance.gameData.Vsync == false)
        {
            //Vsync켜기
            VsyncOff.SetActive(false);
            VsyncOn.SetActive(true);
            QualitySettings.vSyncCount = 1;
            DataController.Instance.gameData.Vsync = true;
        }
        else if(DataController.Instance.gameData.Vsync == true)
        {
            //Vsync끄기
            VsyncOff.SetActive(true);
            VsyncOn.SetActive(false);
            QualitySettings.vSyncCount = 0;
            DataController.Instance.gameData.Vsync = false;

        }
    }

    public void FogSetting()
    {
        if (DataController.Instance.gameData.Fog == false)
        {
            //Fog켜기
            FogOff.SetActive(false);
            FogOn.SetActive(true);
            RenderSettings.fog = true;
            DataController.Instance.gameData.Fog = true;
        }
        else if (DataController.Instance.gameData.Fog == true)
        {
            //Fog끄기
            FogOff.SetActive(true);
            FogOn.SetActive(false);
            RenderSettings.fog = false;

            DataController.Instance.gameData.Fog = false;

        }
    }

    public void AmbientOcclusionSetting()
    {

        if (DataController.Instance.gameData.AmbientOcclution == false)
        {
            //Fog켜기
            AmbientOcclusionOff.SetActive(false);
            AmbientOcclusionOn.SetActive(true);
            ambient.enabled.value = true;
            DataController.Instance.gameData.AmbientOcclution = true;
        }
        else if (DataController.Instance.gameData.AmbientOcclution == true)
        {
            //Fog끄기
            AmbientOcclusionOff.SetActive(true);
            AmbientOcclusionOn.SetActive(false);
            ambient.enabled.value = false;
            DataController.Instance.gameData.AmbientOcclution = false;

        }
    }

}
