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

    [SerializeField] AnimationCurve ScaleCurve;
    [SerializeField] public float duration;//주기 0.05
    [SerializeField] float minScale;
    [SerializeField] float maxScale;



    // Start is called before the first frame update
    void Start()
    {
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
            ComboTxt.enabled = false;
        }
        else
        {
            ComboTxt.enabled = true;
        }
        ComboTxt.text = DataController.Instance.gameData.PlayerCombo + "\n" + "Combo";

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
        transform.localScale = new Vector3(1, 1, 1);
        float timer = 0;
        float scaleTime = 0;
        while (timer <= duration)
        {
            Vector3 curScale = transform.localScale;
            float scaleValue = Mathf.Lerp(minScale, maxScale, ScaleCurve.Evaluate(scaleTime));
            Vector3 curveresult = new Vector3(scaleValue, scaleValue, scaleValue);
            transform.localScale = curScale + curveresult;
            

            scaleTime += Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }


    }

}
