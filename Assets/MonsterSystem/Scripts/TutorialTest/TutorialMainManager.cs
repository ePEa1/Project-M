using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject PracticeAttackPos;
   
    public GameObject[] FreeAttackPos;


    public GameObject player;
    public GameObject Cam;
    public GameObject PlayerUI;

    public GameObject SpawnMonster;
    public GameObject DialogueScreen;

    public Text KeyExplain;
    public GameObject TutorialKey;

    public GameObject MoveKey;
    public GameObject AttackMouseKey;
    public GameObject MoveAttackKey;

    public Image MPView;
    public Image SkillView;

    public Image DialogueBox;
    public Text NameText;
    public Text DialogueText;


    [SerializeField] int MoveOrder;
    [SerializeField] int AttackOrder;
    [SerializeField] int MPExplain;
    [SerializeField] int SkillExplain;
    [SerializeField] int MoveUICheck;
    [SerializeField] int MoveAttackOrder;
    [SerializeField] int PracticeMonster;

    [SerializeField] float FrontAttack;
    [SerializeField] float FreeMoveAttack;
    [SerializeField] float Shield;
    [SerializeField] float FreeAttack;

    [SerializeField] string[] TutorialStart;
    string ResultText;

    public int DialCount = 0;

    public GameObject Potal;//튜토리얼 끝날 때 돌아가기
    bool DailogueOpen = true;
    bool GetKeyTime = false;
    bool IsSpawn = false;


    // Start is called before the first frame update
    void Start()
    {
        TutorialKey.SetActive(false);
        MoveKey.SetActive(false);
        AttackMouseKey.SetActive(false);
        MoveAttackKey.SetActive(false);
        Move.enabled = false;
        Attack.enabled = false;
        MoveAtk.enabled = false;

        NameText.text = "공간의 주관자";
        PlayerUI.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Move.IsReady == true)
        {
            DialCount = MoveOrder + 1;
            DailogueOpen = true;
            TutorialKey.SetActive(false);
            DialogueScreen.SetActive(true);
            MoveKey.SetActive(false);
            Move.IsReady = false;

            Move.enabled = false;

        }
        if (Attack.IsReady == true)
        {
            DialCount = AttackOrder + 1;
            DailogueOpen = true;
            TutorialKey.SetActive(false);
            DialogueScreen.SetActive(true);
            AttackMouseKey.SetActive(false);
            Attack.IsReady = false;

            Attack.enabled = false;
        }
        if(MoveAtk.IsReady == true)
        {
            DialCount = MoveAttackOrder + 1;
            DailogueOpen = true;
            TutorialKey.SetActive(false);
            DialogueScreen.SetActive(true);
            MoveAttackKey.SetActive(false);
            MoveAtk.IsReady = false;

            MoveAtk.enabled = false;
        }
        if (IsSpawn &&  null == GameObject.FindGameObjectWithTag("Enemy"))
        {
            DialCount = 13;
            DailogueOpen = true;
            TutorialKey.SetActive(false);
            DialogueScreen.SetActive(true);

            IsSpawn = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DailogueOpen)
            {
                DialCount += 1;
            }
            
        }
        SetDialogue(DialCount);
        DialogueText.text = ResultText;
    }


    void SetDialogue(int d)
    {
        if(DialCount == MoveOrder)//일반 이동
        {
            DailogueOpen = false;
            Move.enabled = true;
            KeyExplain.text = "이동키 : W, A, S, D";
            DialogueScreen.SetActive(false);
            TutorialKey.SetActive(true);
            MoveKey.SetActive(true);
            player.GetComponent<PlayerFsmManager>().enabled = true;
        }
        else if(DialCount == AttackOrder)//일반 공격
        {
            PlayerUI.SetActive(true);
            DailogueOpen = false;
            Attack.enabled = true;
            KeyExplain.text = "공격 : 왼쪽 마우스";
            DialogueScreen.SetActive(false);
            TutorialKey.SetActive(true);
            AttackMouseKey.SetActive(true);
            player.GetComponent<PlayerFsmManager>().enabled = true;
        }
        else if(DialCount == MoveAttackOrder)//대쉬 공격
        {
            DailogueOpen = false;
            MoveAtk.enabled = true;

            DialogueScreen.SetActive(false);
            TutorialKey.SetActive(true);
            MoveAttackKey.SetActive(true);
            player.GetComponent<PlayerFsmManager>().enabled = true;
        }
        else if(DialCount == MPExplain)//마나 이미지 표시
        {
            DialogueScreen.SetActive(false);
           
        }
        else if(DialCount == PracticeMonster)
        {
            DailogueOpen = false;

            KeyExplain.text = "몬스터를 해치우세요!";
            DialogueScreen.SetActive(false);
            TutorialKey.SetActive(true);
            if(IsSpawn == false)
            {
                GameObject curMonster = Instantiate(SpawnMonster, PracticeAttackPos.transform.position, PracticeAttackPos.transform.rotation);
                curMonster.tag = "Enemy";

                IsSpawn = true;
            }
            player.GetComponent<PlayerFsmManager>().enabled = true;
        }
        else if(DialCount == FreeAttack)//자유 전투
        {
            DailogueOpen = false;

            KeyExplain.text = "몬스터를 해치우세요!";
            DialogueScreen.SetActive(false);
            TutorialKey.SetActive(true);
            if (IsSpawn == false)
            {
                for(int i = 0; i <= FreeAttackPos.Length; i++)
                {
                    GameObject curMonster = Instantiate(SpawnMonster, FreeAttackPos[i].transform.position, FreeAttackPos[i].transform.rotation);
                    curMonster.tag = "Enemy";
                }
                IsSpawn = true;
            }
            player.GetComponent<PlayerFsmManager>().enabled = true;
        }
        else
        {
            player.GetComponent<PlayerFsmManager>().enabled = false;

            ResultText = TutorialStart[DialCount];

        }
    }

    void ReturnPlayer()
    {
        player.transform.position = ReturnPos.transform.position;
    }
}
