using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTimer : MonoBehaviour
{
    float m_time = 0.0f;
    [SerializeField] Text m_timeText;

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;
        m_timeText.text = (Mathf.Floor(m_time / 0.01f) * 0.01f).ToString() + "(sec)";
    }
}
