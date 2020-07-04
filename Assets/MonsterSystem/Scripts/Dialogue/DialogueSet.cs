using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueSet : MonoBehaviour
{
    [SerializeField] TextAsset csvfile;
    [SerializeField] string csvname;

    public int[] chaaninum;
    public int[] Oliveraninum;
    public string[] estelleEmotion;
    public string[] serenaEmotion;
    public string[] namelist;
    [TextArea(3, 10)]
    public string[] scenarios;
    public Animator EstelleAnim;
    public Animator SerenaAnim;
    [SerializeField] Image EstelleImg;
    [SerializeField] Image SerenaImg;
    [SerializeField] Image DialBox;
    [SerializeField] Text Names;
    [SerializeField] Text uiText;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;

    private string currentText = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
    private int currentLine = 0;
    private int lastUpdateCharacter = -1;

    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    void Start()
    {
        SetDialogue(csvname);
        SetNextLine();
    }

    void Update()
    {
        if (IsCompleteDisplayText)
        {
            if (currentLine < scenarios.Length && Input.GetMouseButtonDown(0))
            {
                SetNextLine();
            }
        }
        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                timeUntilDisplay = 0;

            }
        }

        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
        if (displayCharacterCount != lastUpdateCharacter)
        {
            uiText.text = currentText.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }
        if (currentLine == scenarios.Length && Input.GetMouseButtonDown(0))
        {
            //SceneManager.LoadScene("Producepotion");
        }
    }

    public void EstelleImgAnim(string emotion)
    {
        if (emotion == "estelle_Def_0")
        {
            EstelleAnim.SetInteger("curEstelle", 0);
        }
        else if (emotion == "estelle_Sup_1")
        {
            EstelleAnim.SetInteger("curEstelle", 1);
        }
        else if (emotion == "estelle_Sup_0")
        {
            EstelleAnim.SetInteger("curEstelle", 2);
        }
        else if (emotion == "estelle_Ang")
        {
            EstelleAnim.SetInteger("curEstelle", 3);
        }
        else if (emotion == "estelle_Ner")
        {
            EstelleAnim.SetInteger("curEstelle", 4);
        }
    }
    public void SerenaImgAnim(string emotion)
    {
        //if (oliveranin == 0)
        //{
        //    Oliverani.SetBool("OliverIn", false);
        //}
        //else if (oliveranin == 1)
        //{
        //    Oliverani.SetBool("OliverIn", true);
        //}
        //else if (oliveranin == 2)
        //{
        //    Oliverani.SetBool("Oliverblack", true);
        //}
        //else if (oliveranin == 3)
        //{
        //    Oliverani.SetBool("Oliverblack", false);
        //}
    }
    void SetNextLine()
    {

        currentText = scenarios[currentLine];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        //EstelleImgAnim(estelleEmotion[currentLine]);
        //SerenaImgAnim(serenaEmotion[currentLine]);
        currentLine++;
        lastUpdateCharacter = -1;


    }

    public void SetDialogue(string name)
    {
        List<Dictionary<string, object>> dialList = CSVReader.Read(csvfile);
        Debug.Log(csvfile.name);
        scenarios = new string[dialList.Count - 1];
        namelist = new string[dialList.Count - 1];

        for (var i = 0; i < dialList.Count-1; i++)
        {
            Debug.Log((string)dialList[i]["script"]);
            namelist[i] = (string)dialList[i]["name"];
            scenarios[i] = (string)dialList[i]["script"];


        }
    } 
    public void SetDialName(string name)
    {
        switch (name)
        {
            case "estelle":

                break;
            case "serena":
                break;
            case "time":
                break;
            case "space":
                break;
        }
    }
}
