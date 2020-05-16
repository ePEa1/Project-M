using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveGameManager : MonoBehaviour
{
    [SerializeField] Vector3 m_minVector;
    [SerializeField] Vector3 m_maxVector;

    [SerializeField] GameObject m_circle;

    [SerializeField] int m_createNum;

    [SerializeField] Text m_timeText;

    [SerializeField] string m_titleScene;
    [SerializeField] Text m_titleButton;

    int m_nowClearNum = 0;
    bool m_isClear = false;

    public float m_nowTime { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        m_nowTime = 0.0f;
    }

    private void Start()
    {
        CreateCircle();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_createNum != m_nowClearNum)
        {
            m_nowTime += Time.deltaTime;
            m_timeText.text = (m_nowTime - (m_nowTime % 0.01f)).ToString();
        }
        else if (!m_isClear)
        {
            m_isClear = true;
        }

        if (m_isClear)
        {
            m_titleButton.gameObject.SetActive(true);
        }
    }

    public void CircleClear()
    {
        m_nowClearNum++;

        if (m_createNum != m_nowClearNum)
            CreateCircle();
    }

    void CreateCircle()
    {
        Vector3 createVec = new Vector3(Random.Range(m_minVector.x, m_maxVector.x), Random.Range(m_minVector.y, m_maxVector.y), Random.Range(m_minVector.z, m_maxVector.z));

        GameObject circle = Instantiate(m_circle);
        circle.transform.position = createVec;
    }
}
