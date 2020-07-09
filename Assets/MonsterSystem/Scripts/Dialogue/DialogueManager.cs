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
            SkillManager.Setup();
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

        if(DataController.Instance.gameData.ScriptTwo == false && DataController.Instance.gameData.ScriptOneEnd ==true)
        {
            dialogue.FadeOutImg.gameObject.SetActive(false);
            dialogue.FadeOutImg.color = new Color(0, 0, 0, 0);

            dialogue.SetDialogue(1);
            dialogue.StartDialogue();
            dialogueScreen.SetActive(true);
            SkillManager.UnlockWidth();
            // player.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
            QuestManager.questManager.SetQuest(QuestManager.QuestType.EnemyKill);

            player.enabled = false;
            SerenaImg.SetActive(false);
            // playerEvents.SetActive(false);

            DataController.Instance.gameData.ScriptTwo = true;

        }
        if (DataController.Instance.gameData.FirstStageSavePointOrder == 1)
        {
            QuestManager.questManager.SetQuest(QuestManager.QuestType.LoadEnemyKill);

        }
        if (DataController.Instance.gameData.ScriptThree == false && DataController.Instance.gameData.StageEndCount == 1)
        {
            dialogue.FadeOutImg.gameObject.SetActive(false);
            dialogue.FadeOutImg.color = new Color(0, 0, 0, 0);

            dialogue.SetDialogue(2);
            dialogue.StartDialogue();
            dialogueScreen.SetActive(true);
            // player.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);
            QuestManager.questManager.SetQuest(QuestManager.QuestType.Move);

            player.enabled = false;
            SerenaImg.SetActive(false);
            // playerEvents.SetActive(false);
            SkillManager.UnlockBack();
            DataController.Instance.gameData.ScriptThree = true;

        }
        if (DataController.Instance.gameData.ScriptFour == false && DataController.Instance.gameData.StageEndCount == 2)
        {
            dialogue.FadeOutImg.gameObject.SetActive(false);
            dialogue.FadeOutImg.color = new Color(0, 0, 0, 0);

            dialogue.SetDialogue(3);
            dialogue.StartDialogue();
            dialogueScreen.SetActive(true);
            // player.ChangeAction(PlayerFsmManager.PlayerENUM.IDLE);

            player.enabled = false;
            SerenaImg.SetActive(false);
            // playerEvents.SetActive(false);
            QuestManager.questManager.SetQuest(QuestManager.QuestType.Move);

            SkillManager.UnlockRush();
            DataController.Instance.gameData.ScriptFour = true;

        }
        if (DataController.Instance.gameData.ScriptFive == false && DataController.Instance.gameData.FirstStageSavePointOrder == 2)
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
            QuestManager.questManager.SetQuest(QuestManager.QuestType.BossKill);

            DataController.Instance.gameData.ScriptFive = true;
        }
    }

}
