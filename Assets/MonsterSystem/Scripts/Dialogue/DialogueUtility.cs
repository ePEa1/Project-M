using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUtility : MonoBehaviour
{
    public static List<Dialogue> dialogues = new List<Dialogue>();
    public TextAsset dialoguefile;
    private static int dialcount = 0;
    private static int dialIndex = 0;

    public static int curState;
    public static int saveDialogueInfo;
    public static bool dialogueEnded;

    public static int GetDialCount()
    {
        return dialcount;
    }
    public static int GetDialIndex()
    {
        return dialIndex;
    }

    public static Dialogue SetCurrentDialogue()
    {
        return dialogues[dialIndex++];
    }

    public static void InitDialogue(TextAsset dialogue)
    {
        dialogues.Clear();
        dialogueEnded = false;
        dialcount = 0;
        dialIndex = 0;

        List<Dictionary<string, object>> dialList = CSVReader.Read(dialogue);

        for (var i = 0; i < dialList.Count; i++)
        {
            Dialogue dial = new Dialogue();
            string name = (string)dialList[i]["name"];

            dial.name = name;
            Debug.Log(name);
            string emotion = (string)dialList[i]["emotion"];
            dial.emotion = emotion;
            Debug.Log(emotion);

            dial.script = (string)dialList[i]["script"];

            //string spriteFile = (string)dialList[i]["illust"];
            //이미지 적용
            //dial.sprite = Resources.Load<Sprite>(spriteFile) as Sprite;
            //dial.background = Resources.Load<Sprite>("Images/background/" + (string)dialList[i]["background"]) as Sprite;


            dialogues.Add(dial);
            dialcount++;
        }
    }

    //private static string Dial_SetPlayerSprite()
    //{
    //    string spriteFile = null;
    //}

}
