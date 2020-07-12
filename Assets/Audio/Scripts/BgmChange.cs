using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmChange : MonoBehaviour
{
    public PauseManager pause;
    public AudioSource source;
    public AudioClip defaultbgm;
    public AudioClip bossbgm;


    bool IsFade = false;
    bool EndChange = false;
    [SerializeField] float fadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        source.clip = defaultbgm;
        source.volume = DataController.Instance.backgroundSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("MainUI") != null)
        {
            if (pause.IsPause == true)
            {
                GetComponent<AudioSource>().volume = 0;
            }
            if (pause.IsPause == false)
            {
                GetComponent<AudioSource>().volume = DataController.Instance.backgroundSound;

            }

        }

        if (DataController.Instance.gameData.ScriptFive == true && IsFade ==false)
        {
            StartCoroutine(FadeOut());
            IsFade = true;

        }
        if(EndChange == true)
        {
            source.volume = DataController.Instance.backgroundSound;
        }

    }
    IEnumerator FadeInAndTurnBgm()
    {
        source.clip = bossbgm;
        source.Play();

        for (float i = 0f; i >= 0; i += 0.005f * fadeSpeed)
        {
            source.volume = i;
            if (source.volume >= DataController.Instance.backgroundSound)
            {
                EndChange = true;
                StopAllCoroutines();
            }
            yield return null;
        }
    }
    IEnumerator FadeOut()
    {
        for (float i = DataController.Instance.backgroundSound; i <= DataController.Instance.backgroundSound; i -= 0.005f * fadeSpeed)
        {
            source.volume = i;
            if(source.volume <= 0)
            {
                StopAllCoroutines();
                StartCoroutine(FadeInAndTurnBgm());
            }
            yield return null;
        }
    }

}
