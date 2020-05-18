using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPViewManager : MonoBehaviour
{
    public Slider HPGauge;
    public float GaugeVal;
    // Start is called before the first frame update
    void Start()
    {
        HPGauge = GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowHP()
    {
        HPGauge.value = GaugeVal * 0.01f;
        if (HPGauge.value <= 0)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
    }
}
