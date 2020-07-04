using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DialogueManager : MonoBehaviour
{
    private Dialogue curDial;
    [SerializeField]
    private int curposition = -1;

    //이미지 위치 변경 스크립트
    private ConversationManager tweening;
    [Range(0, 0.5f)]
    public float textDelay;
    private bool skipped;
    private bool dialogueEnded;
    private bool isShowingAllText;

    public Text nameText;
    public Image nameBox;
    public Text lineText;
    public GameObject nextImg;
    private string tempText;
    public Image EstelleImg;
    public Image SerenaImg;
    public Image backgroundImg;

    private void Awake()
    {
        //tweening과 log적용
    }

    void OnEnable()
    {
        nextImg.SetActive(false);
        InitDialogueImage();

        dialogueEnded = false;
        isShowingAllText = false;
        skipped = false;

        curDial = DialogueUtility.SetCurrentDialogue();
        nameText.text = curDial.name;
        backgroundImg.sprite = curDial.background;
        StartCoroutine(TypingLine(curDial.line));
        SetDialogueImage();

    }

    void Update()
    {
        if (dialogueEnded)
        {
            nextImg.SetActive(true);
            StopCoroutine("TypingLine");

            if (Input.GetMouseButtonDown(0))
            {
                
            }
            return;
        }
        if (isShowingAllText)
        {
            nextImg.SetActive(true);
        }
        else
        {
            nextImg.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (isShowingAllText)
            {
                curDial = DialogueUtility.SetCurrentDialogue();
                nameText.text = curDial.name;
                backgroundImg.sprite = curDial.background;

                isShowingAllText = false;
                skipped = false;
                StartCoroutine(TypingLine(curDial.line));
                
            }
            else
            {
                skipped = true;
            }
            SetDialogueImage();
        }
        if(curposition == curDial.position)
        {
            tweening.isMoving = false;
        }
        else
        {
            tweening.isMoving = true;

        }
        if(tweening.isMoving && !dialogueEnded)
        {
            tweening.SetChar(curDial.name, nameBox);
            //if (tweening.SetPosition())
            //{
            //    curposition = curDial.position;
            //}
        }

    }

    public void SetCurPosition(int value)
    {
        curposition = value;
    }
    public void SkipButtonUp()
    {
        DialogueUtility.dialogueEnded = true;
    }

    private bool CastButton()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0);

        if (hit.collider != null)
        {
            return true;
        }
        else
            return false;
    }

    private void InitDialogueImage()
    {
        backgroundImg.sprite = null;
        EstelleImg.sprite = null;
        SerenaImg.sprite = null;

        EstelleImg.gameObject.SetActive(false);
        SerenaImg.gameObject.SetActive(false);
    }

    private void SetDialogueImage()
    {
        if(curDial.sprite = null)
        {
            return;
        }

        switch (curDial.position)
        {
            case 0:
                EstelleImg.sprite = curDial.sprite;
                EstelleImg.gameObject.SetActive(true);
                break;
            case 1:
                SerenaImg.sprite = curDial.sprite;
                SerenaImg.gameObject.SetActive(true);
                break;
        }
    }

    IEnumerator TypingLine(string line)
    {
        tempText = "";
        for(int i = 0; i<line.Length; i++)
        {
            if (skipped)
            {
                break;
            }
            tempText = line.Substring(0, i + 1);
            lineText.text = tempText;

            yield return new WaitForSeconds(textDelay);
        }
        lineText.text = line;
        yield return new WaitForSeconds(textDelay);

        isShowingAllText = true;
        if(DialogueUtility.GetDialIndex() >= DialogueUtility.GetDialCount())
        {
            dialogueEnded = true;
            DialogueUtility.dialogueEnded = true;
        }
    }
}
