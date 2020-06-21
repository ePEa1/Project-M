using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackMouseTest : MonoBehaviour
{
    public Image MouseImg;
    public Image MouseGauge;
    public float GaugeVal;
    float Clicked;
    float ClickCount;
    [SerializeField] float minScale;
    [SerializeField] float maxScale;
    [SerializeField] AnimationCurve ScaleCurve;
    [SerializeField] float duration;
    [SerializeField] Vector3 LimitScale;

    public bool IsReady = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GaugeVal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickCount += 1;
            StartCoroutine(Shake());
            GaugeVal += 0.1f;
        }
        if(ClickCount >= 10)
        {
            IsReady = true;
        }
        MouseGauge.fillAmount = GaugeVal;
    }

    public IEnumerator Shake()
    {
        Clicked = 5.0f;
        MouseImg.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        float timer = 0;
        float scaleTime = 0;
        while (timer <= duration)
        {
            Vector3 curScale = MouseImg.transform.localScale;
            float scaleValue = Mathf.Lerp(minScale, maxScale, ScaleCurve.Evaluate(scaleTime));
            Vector3 curveresult = new Vector3(scaleValue, scaleValue, scaleValue);
            MouseImg.transform.localScale = curScale + curveresult;
            if (MouseImg.transform.localScale.x >= LimitScale.x)
            {
                MouseImg.transform.localScale = LimitScale;
            }

            scaleTime += Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
