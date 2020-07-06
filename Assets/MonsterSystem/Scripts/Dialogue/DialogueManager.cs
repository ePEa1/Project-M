using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DialogueManager : MonoBehaviour
{
    public DialogueSet dialogue;
    public GameObject SerenaImg;
    public PlayerFsmManager player;
    public GameObject dialogueScreen;
    //public GameObject playerEvents;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFsmManager>();
       
        //playerEvents = player.transform.GetChild(0).gameObject;


        //dialogue = GetComponent<DialogueSet>();
        //dialogue.enabled = false;
    }
    private void Update()
    {
        if(DataController.Instance.gameData.ScriptOne == false && DataController.Instance.gameData.FirstStageSavePointOrder ==0)
        {
            dialogue.SetDialogue(0);
            dialogue.StartDialogue();
            dialogueScreen.SetActive(true);
            //  player.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
            dialogue.FadeOutImg.color = new Color(0, 0, 0, 1);
              player.enabled = false;
          // playerEvents.SetActive(false);
            SerenaImg.SetActive(false);
            DataController.Instance.gameData.ScriptOne = true;
        }
        if(DataController.Instance.gameData.ScriptFive == false && DataController.Instance.gameData.FirstStageSavePointOrder == 2)
        {
            dialogue.FadeOutImg.gameObject.SetActive(false);
            dialogue.FadeOutImg.color = new Color(0, 0, 0, 0);

            dialogue.SetDialogue(4);
            dialogue.StartDialogue();
            dialogueScreen.SetActive(true);
           // player.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);

            player.enabled = false;
            SerenaImg.SetActive(true);
          // playerEvents.SetActive(false);

            DataController.Instance.gameData.ScriptFive = true;
        }
    }

}
