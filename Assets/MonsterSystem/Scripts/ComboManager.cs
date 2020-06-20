using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ComboManager : MonoBehaviour
{

    [SerializeField] float ComboWait;
    [SerializeField] float curcomboWait;

    [SerializeField] Text ComboTxt;
    [SerializeField] Slider m_endCombo;
    [SerializeField] float maxendCombo;
    public int MaxCombo;
    public string ComboString;

    [SerializeField] AnimationCurve ScaleCurve;
    [SerializeField] public float duration;//주기 0.05
    [SerializeField] float minScale;
    [SerializeField] float maxScale;
    [SerializeField] Vector3 LimitScale;

    public Image OneImg;
    public Image TenImg;
    public Image HundredImg;
    public Image HitImg;

    [SerializeField] Sprite none;
    [SerializeField] Sprite HitSprite;
    [SerializeField] Sprite[] ComboNumber;

    // Start is called before the first frame update
    void Start()
    {
        HundredImg.GetComponent<Image>().sprite = none;
        TenImg.GetComponent<Image>().sprite = none;
        OneImg.GetComponent<Image>().sprite = none;
        HitImg.GetComponent<Image>().sprite = none;

        MaxCombo = 0;
    }
    private void Awake()
    {
        ComboWait = maxendCombo;       
    }

    // Update is called once per frame
    void Update()
    {

        if (DataController.Instance.gameData.PlayerCombo <= 0)
        {
            HundredImg.GetComponent<Image>().sprite = none;
            TenImg.GetComponent<Image>().sprite = none;
            OneImg.GetComponent<Image>().sprite = none;
            HitImg.GetComponent<Image>().sprite = none;
        }
        else
        {
            if (DataController.Instance.gameData.PlayerCombo < 100)
            {
                if (DataController.Instance.gameData.PlayerCombo < 10)
                {
                    TenImg.GetComponent<Image>().sprite = none;
                    SetImage(OneImg, DataController.Instance.gameData.PlayerCombo);

                }
                HundredImg.GetComponent<Image>().sprite = none;
                SetImage(OneImg, DataController.Instance.gameData.PlayerCombo);
                SetImage(TenImg, DataController.Instance.gameData.PlayerCombo / 10);
            }

        }

        ComboString = DataController.Instance.gameData.PlayerCombo.ToString();

        SetImage(OneImg, DataController.Instance.gameData.PlayerCombo % 10);
        SetImage(TenImg, DataController.Instance.gameData.PlayerCombo % 10);
        SetImage(HundredImg, DataController.Instance.gameData.PlayerCombo % 10%10);

        HitImg.GetComponent<Image>().sprite = HitSprite;

        //ComboTxt.text = DataController.Instance.gameData.PlayerCombo + " hit";

        //ComboDelay();
        if (DataController.Instance.gameData.IsCombo == true)
        {
            StartCoroutine(Shake());
            DataController.Instance.gameData.IsCombo = false;
        }
        ComboWait -= Time.deltaTime;
        if (ComboWait <= 0)
        {
            ComboWait = 0;
            DataController.Instance.gameData.PlayerCombo = 0;
        }

    }

    void ComboDelay()
    {
            ComboWait = Mathf.Max(ComboWait - Time.deltaTime, 0);
        
    }

    public IEnumerator Shake()
    {
        ComboWait = 5.0f;
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        float timer = 0;
        float scaleTime = 0;
        while (timer <= duration)
        {
            Vector3 curScale = transform.localScale;
            float scaleValue = Mathf.Lerp(minScale, maxScale, ScaleCurve.Evaluate(scaleTime));
            Vector3 curveresult = new Vector3(scaleValue, scaleValue, scaleValue);
            transform.localScale = curScale + curveresult;
            if(transform.localScale.x >= LimitScale.x)
            {
                transform.localScale = LimitScale;
            }

            scaleTime += Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
    }

    void SetImage(Image img, int number)
    {
        img.GetComponent<Image>().sprite = ComboNumber[number];
    }
}
