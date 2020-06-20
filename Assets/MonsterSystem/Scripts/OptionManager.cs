using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OptionManager : MonoBehaviour
{
    public GameObject OptionScreen;
    public GameObject SoundPage;
    public GameObject GraphicPage;
    public GameObject ControlPage;

    public Slider BackGroundSound;
    public Slider EffectsSound;
    public Slider MouseMoving;


    private void Start()
    {
        //OptionScreen.SetActive(false);
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
            DataController.Instance.SetOption(DataController.Option.mouse, (int)(MouseMoving.value * 100));

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
}
