using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum TutorialState
{
    MOVE,
    DEFAULTATK,
    MOVEATK,
    PRACTICE,
    MONSTERSHEILD,
    COMBORESET,
    FREEATK,
}

public class TutorialMainManager : MonoBehaviour
{

    public MoveKeyTest Move;//이동
    public AttackMouseTest Attack;//기본 공격
    public MoveAtkKeyTest MoveAtk;//이동 공격

    public GameObject ReturnPos;
    public GameObject AttackSpawn;
    public GameObject MoveAttackSpawn;
    public GameObject[] FreeAttackPos;


    public GameObject playerObj;
    //public Animator player;
    public GameObject Cam;
    public GameObject PlayerUI;
    public GameObject PracticeAttackPos;
    public GameObject SpawnMonster;
    public GameObject DialogueScreen;
    public GameObject PotalObj;

    public Image DialImage;
    public Text KeyExplain;
    public GameObject TutorialKey;

    public GameObject MoveKey;
    public GameObject AttackMouseKey;
    public GameObject MoveAttackKey;

    public GameObject MPView;
    public GameObject SkillView;

    public Image DialogueBox;
    public Text NameText;
    public Text DialogueText;

    //각 행동 순서들
    [SerializeField] int MoveOrder;
    [SerializeField] int AttackOrder;
    [SerializeField] int SkillSpawnOrder;
    [SerializeField] int MPExplain;
    [SerializeField] int SkillExplain;
    [SerializeField] int MoveAttackOrder;
    [SerializeField] int PracticeMonster;
    [SerializeField] int EndTutorial;

    [SerializeField] float FrontAttack;
    [SerializeField] float FreeMoveAttack;
    [SerializeField] float Shield;
    [SerializeField] float FreeAttack;

    [SerializeField] string[] TutorialStart;
    string ResultText;

    public int DialCount = 0;

    public GameObject Potal;//튜토리얼 끝날 때 돌아가기
    bool DialogueOpen = true;
    bool GetKeyTime = false;
    public bool IsSpawn = false;


    // Start is called before the first frame update
    void Start()
    {
        TutorialKey.SetActive(false);
        MoveKey.SetActive(false);
        AttackMouseKey.SetActive(false);
        MoveAttackKey.SetActive(false);
        MPView.SetActive(false);
        SkillView.SetActive(false);
        AttackSpawn.SetActive(true);
        PotalObj.SetActive(false);
        

        Move.enabled = false;
        Attack.enabled = false;
        MoveAtk.enabled = false;


        playerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Move.IsReady == true)
        {
            DialCount = MoveOrder + 1;
            DialogueOpen = true;
            OpenDialogue();
            MoveKey.SetActive(false);
            Move.IsReady = false;
            Move.enabled = false;
        }

        if (MoveAtk.IsReady == true)
        {
            DialCount = MoveAttackOrder + 1;
            DialogueOpen = true;
            OpenDialogue();
            MoveAttackKey.SetActive(false);
            MoveAtk.IsReady = false;
            MoveAtk.enabled = false;
        }
        if (IsSpawn &&  null == GameObject.FindGameObjectWithTag("Enemy"))
        {
            if(DialCount == AttackOrder)
            {
                DialCount = AttackOrder + 1;
                AttackMouseKey.SetActive(false);
                Attack.IsReady = false;
            }
            else if(DialCount == SkillSpawnOrder)
            {
                DialCount = SkillSpawnOrder + 1;
            }
            DialogueOpen = true;
            OpenDialogue();

            IsSpawn = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (DialogueOpen)
            {
                DialCount += 1;
            }
        }
        SetDialogue(DialCount);
        DialogueText.text = ResultText;
    }


    void SetDialogue(int d)
    {
        if (DialCount == MoveOrder)//일반 이동
        {
            DialogueOpen = false;
            Move.enabled = true;
            KeyExplain.text = "이동키 : W, A, S, D";
            CloseDialogue();
            MoveKey.SetActive(true);
            //playerObj.GetComponent<PlayerFsmManager>().enabled = true;
            //player.enabled = true;
        }
        else if (DialCount == AttackOrder)//일반 공격
        {
            IsSpawn = true;

            PlayerUI.SetActive(true);
            DialogueOpen = false;
            Attack.enabled = true;
            KeyExplain.text = "공격 : 왼쪽 마우스";
            CloseDialogue();
            AttackMouseKey.SetActive(true);
            playerObj.GetComponent<PlayerFsmManager>().enabled = true;

            //player.enabled = true;
        }
        else if (DialCount == SkillSpawnOrder)
        {
            DialogueOpen = false;
            if (IsSpawn == false)
            {
                IsSpawn = true;
            }
            KeyExplain.text = "몬스터들을 해치워보자";

            CloseDialogue();
            MoveAttackSpawn.SetActive(true);
            playerObj.GetComponent<PlayerFsmManager>().enabled = true;

            //player.enabled = true;
        }
        else if (DialCount == MoveAttackOrder)//대쉬 공격
        {
            DialogueOpen = false;
            MoveAtk.enabled = true;

            CloseDialogue();
            MoveAttackKey.SetActive(true);
            playerObj.GetComponent<PlayerFsmManager>().enabled = true;

            //player.enabled = true;
        }
        else if (DialCount == MPExplain)//마나 이미지 표시
        {
            KeyExplain.text = "체력바 밑 MP 확인";
            playerObj.GetComponent<PlayerFsmManager>().enabled = false;

            //player.enabled = false;

            CloseDialogue();
            MPView.SetActive(true);
        }
        else if (DialCount == (MPExplain + 1))
        {
            playerObj.GetComponent<PlayerFsmManager>().enabled = false;

            //player.enabled = false;

            ResultText = TutorialStart[MPExplain + 1];

            OpenDialogue();
            MPView.SetActive(false);
        }
        else if (DialCount == SkillExplain)
        {
            KeyExplain.text = "오른쪽 스킬 확인";
            playerObj.GetComponent<PlayerFsmManager>().enabled = false;

            //player.enabled = false;

            CloseDialogue();
            SkillView.SetActive(true);
        }
        else if (DialCount == (SkillExplain + 1))
        {
            playerObj.GetComponent<PlayerFsmManager>().enabled = false;

            //player.enabled = false;

            ResultText = TutorialStart[SkillExplain + 1];
            OpenDialogue();
            SkillView.SetActive(false);
        }
        else if (DialCount == PracticeMonster)
        {
            DialogueOpen = false;

            KeyExplain.text = "몬스터를 해치우세요!";
            CloseDialogue();
            if (IsSpawn == false)
            {
                GameObject curMonster = Instantiate(SpawnMonster, PracticeAttackPos.transform.position, PracticeAttackPos.transform.rotation);
                curMonster.tag = "Enemy";
                IsSpawn = true;
            }
            playerObj.GetComponent<PlayerFsmManager>().enabled = true;
            //player.enabled = true;
        }
        else if (DialCount == EndTutorial)
        {
            PlayerUI.SetActive(true);
            DialogueOpen = false;
            PotalObj.SetActive(true);
            DialogueScreen.SetActive(false);
            playerObj.GetComponent<PlayerFsmManager>().enabled = true;

            //player.enabled = true;
        }
        else
        {
            playerObj.GetComponent<PlayerFsmManager>().enabled = false;

            //player.enabled = false;
            ResultText = TutorialStart[DialCount];
        }
    }

    void ReturnPlayer()
    {
        //player.transform.position = ReturnPos.transform.position;
    }

    void CloseDialogue()
    {
        PlayerUI.SetActive(true);

        DialogueScreen.SetActive(false);
        TutorialKey.SetActive(true);
        playerObj.GetComponent<PlayerFsmManager>().enabled = true;

       // player.enabled = true;

    }

    void OpenDialogue()
    {
        PlayerUI.SetActive(false);

        TutorialKey.SetActive(false);
        DialogueScreen.SetActive(true);
    }


    void DialShow()
    {
        //DialImage.transform.position = Mathf.Lerp()

    }

}
