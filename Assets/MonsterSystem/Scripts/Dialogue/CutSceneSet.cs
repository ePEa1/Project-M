using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneSet : MonoBehaviour
{
    public Image CutSceneImgBack;
    public Image CutSceneImgFront;
    public Image FadeOut;
    public Sprite none;
    public Sprite[] Imglist;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;

    [SerializeField] float fadeSpeed = 2;
    public int currentImg;
    private float currentAlpha = 0;
    private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
    private float lastUpdateAlpha = -1;

    public bool IsCompleteDisplayAlpha
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentImg = 0;
        StartCoroutine(FadeIn());
        SetNextLine();

        CutSceneImgBack.color = new Color(1, 1, 1, 1);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Skip();
        }
        if (currentAlpha >= 1)
        {
            if (CutSceneImgFront.color.a >= 1 && Input.GetMouseButtonDown(0))
            {
                SetNextLine();
            }
        }
        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                CutSceneImgFront.color = new Color(1, 1, 1, 1);

            }
        }

        currentAlpha += 0.005f * fadeSpeed;
        float displayAlpha = (Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentAlpha);
        if (displayAlpha != lastUpdateAlpha)
        {
            CutSceneImgFront.color = new Color(1,1,1,currentAlpha);
            lastUpdateAlpha = displayAlpha;
        }
        if (currentImg == Imglist.Length+1 && Input.GetMouseButtonDown(0))//끝났을 때
        {
           // CutSceneImgBack.color = new Color(1, 1, 1, 0);
            //CutSceneImgFront.color = new Color(1, 1, 1, 0);
            DataController.Instance.gameData.IsIntroShow = true;
            StartCoroutine(FadeInGameOver());

        }
    }
    void SetNextLine()
    {

        if (currentImg == 0)
        {
            CutSceneImgBack.sprite = none;
        }
        else
        {
            CutSceneImgBack.sprite = Imglist[currentImg - 1];
        }
        currentAlpha = 0;
        if(currentImg >= Imglist.Length)
        {
            CutSceneImgFront.sprite = none;
        }
        else
        {
            CutSceneImgFront.sprite = Imglist[currentImg];
        }
        lastUpdateAlpha = -1;
        timeElapsed = Time.time;
        currentImg++;

        //currentText = scenarios[currentLine];
        //timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        //SetDialName(namelist[currentLine]);


    }

    IEnumerator FadeInGameOver()
    {

        for (float i = 0f; i >= 0; i += 0.005f * fadeSpeed)
        {
            Color color = new Vector4(0, 0, 0, i);
            FadeOut.color = color;

            if (FadeOut.color.a >= 1)
            {
                LoadingSceneManager.LoadScene("Boss_BJW 3_SYW_0703_Merge");

            }

            yield return null;
        }
    }
    public void Skip()
    {
        CutSceneImgBack.color = new Color(1, 1, 1, 0);
        CutSceneImgFront.color = new Color(1, 1, 1, 0);
        DataController.Instance.gameData.IsIntroShow = true;
        LoadingSceneManager.LoadScene("Boss_BJW 3_SYW_0703_Merge");

    }
    IEnumerator FadeIn()
    {
        for (float i = 0f; i >= 1; i += 0.005f * fadeSpeed)
        {
            Color color = new Vector4(1, 1, 1, i);
            FadeOut.color = color;

            if (FadeOut.color.a >= 1)
            {
                StopAllCoroutines();
            }

            yield return null;
        }
    }

}
