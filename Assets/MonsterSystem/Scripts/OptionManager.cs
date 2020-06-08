using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OptionManager : MonoBehaviour
{
    public GameObject OptionScreen;
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
        BackGroundSound.value = DataController.Instance.gameData.BackgroundSound/100;
        EffectsSound.value = DataController.Instance.gameData.EffectSound / 100;
        MouseMoving.value = DataController.Instance.gameData.MouseMoving / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            DataController.Instance.gameData.BackgroundSound = BackGroundSound.value*100;
            DataController.Instance.gameData.EffectSound = EffectsSound.value * 100;
            DataController.Instance.gameData.MouseMoving = MouseMoving.value * 100;
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
}
