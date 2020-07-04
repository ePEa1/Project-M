using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueSet : MonoBehaviour
{
    public int currentLine = 0;

    public GameObject DialogueScreen;
    public PauseManager PauseScreen;
    public PlayerFsmManager player;
    public GameObject playerEvents;
    [SerializeField] TextAsset[] csvfile;


    public float XValue = 40f;
    public float YValue = 30f;

    public string[] emotionlist;
    
    public string[] namelist;
    [TextArea(3, 10)]
    public string[] scenarios;
    [SerializeField] Sprite[] DialBoxList;
    public Animator EstelleAnim;
    public Animator SerenaAnim;
    public Image NextImg;
    [SerializeField] Image EstelleImg;
    [SerializeField] Image SerenaImg;
    [SerializeField] Image DialBox;
    [SerializeField] Text Names;
    [SerializeField] Text uiText;

    Vector2 curEstelleMove;
    Vector2 curSerenaMove;
    Vector2 EstelleMove;
    Vector2 SerenaMove;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;

    private string currentText = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
    private int lastUpdateCharacter = -1;

    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFsmManager>(); ;
        playerEvents = player.transform.GetChild(0).gameObject;


        curEstelleMove = EstelleImg.transform.position;
        curSerenaMove = SerenaImg.transform.position;
        EstelleMove = new Vector2(curEstelleMove.x - XValue, curEstelleMove.y - YValue);
        SerenaMove = new Vector2(curSerenaMove.x + XValue, curSerenaMove.y - YValue);


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
            DialogueScreen.SetActive(false);
            //player.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
            player.enabled = true;
            playerEvents.SetActive(true);
            PauseScreen.enabled = true;
            DataController.Instance.gameData.ScriptOne = true;
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
        else if (emotion == "estelle_Def_1")
        {
            EstelleAnim.SetInteger("curEstelle", 5);
        }
    }
    public void SerenaImgAnim(string emotion)
    {
        if (emotion == "serena_Def")
        {
            SerenaAnim.SetInteger("curSerena", 0);
        }
        else if (emotion == "serena_Def_1")
        {
            SerenaAnim.SetInteger("curSerena", 1);
        }
        else if (emotion == "serena_Sad")
        {
            SerenaAnim.SetInteger("curSerena", 2);
        }
        else if (emotion == "serena_Sm")
        {
            SerenaAnim.SetInteger("curSerena", 3);
        }
        else if (emotion == "serena_Tiren")
        {
            SerenaAnim.SetInteger("curSerena", 4);
        }
        else if(emotion == "none")
        {
            SerenaAnim.SetInteger("curSerena", 5);
        }
    }
    void SetNextLine()
    {
        currentText = scenarios[currentLine];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        SetDialName(namelist[currentLine]);
        currentLine++;
        lastUpdateCharacter = -1;


    }

    public void SetDialogue(int name)
    {
        List<Dictionary<string, object>> dialList = CSVReader.Read(csvfile[name]);
        scenarios = new string[dialList.Count];
        namelist = new string[dialList.Count];
        emotionlist = new string[dialList.Count];
        for (var i = 0; i < dialList.Count; i++)
        {
            namelist[i] = (string)dialList[i]["name"];
            scenarios[i] = (string)dialList[i]["script"];
            emotionlist[i] = (string)dialList[i]["emotion"];
            


        }
    } 


    public void SetDialName(string name)
    {
        switch (name)
        {
            case "estelle":
                EstelleImgAnim(emotionlist[currentLine]);
                DialBox.sprite = DialBoxList[1];
                SerenaImg.color = new Vector4(0.3f, 0.3f, 0.3f, 1f);
                EstelleImg.color = new Vector4(1.0f, 1.0f, 1.0f, 1f);

                break;
            case "serena":
                SerenaImgAnim(emotionlist[currentLine]);
                DialBox.sprite = DialBoxList[3];
                EstelleImg.color = new Vector4(0.3f, 0.3f, 0.3f, 1f);
                SerenaImg.color = new Vector4(1.0f, 1.0f, 1.0f, 1f);

                break;
            case "time":
                SerenaImgAnim(emotionlist[currentLine]);
                DialBox.sprite = DialBoxList[2];
                EstelleImg.color = new Vector4(0.3f, 0.3f, 0.3f, 1f);
                SerenaImg.color = new Vector4(1.0f, 1.0f, 1.0f, 1f);

                break;
            case "space":
                EstelleImgAnim(emotionlist[currentLine]);
                DialBox.sprite = DialBoxList[0];
                SerenaImg.color = new Vector4(0.3f, 0.3f, 0.3f, 1f);
                EstelleImg.color = new Vector4(1.0f, 1.0f, 1.0f, 1f);

                break;
        }
    }

    public void StartDialogue()
    {
        playerEvents.SetActive(false);


        DialogueScreen.SetActive(true);
        PauseScreen.enabled = false;

        currentLine = 0;
        SetNextLine();

    }
}
