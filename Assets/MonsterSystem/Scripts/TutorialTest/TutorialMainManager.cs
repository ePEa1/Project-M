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

    public MoveKeyTest Move;
    public AttackMouseTest Attacke;
    public MoveAtkKeyTest MoveAtk;

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

    public Image DialogueBox;
    public Text DialogueText;


    [SerializeField] float MoveOrder;
    [SerializeField] float AttackOrder;
    [SerializeField] float MoveAttackOrder;
    //[SerializeField] float FrontAttack;
    [SerializeField] float FreeMoveAttack;
    [SerializeField] float Shield;
    [SerializeField] float FreeAttack;

    [SerializeField] string[] TutorialStart;
    string ResultText;

    public int DialCount = 0;

    public GameObject Potal;//튜토리얼 끝날 때 돌아가기
    bool DailogueOpen = true;
    bool GetKeyTime = false;


    // Start is called before the first frame update
    void Start()
    {
        TutorialKey.SetActive(false);
        MoveKey.SetActive(false);
        AttackMouseKey.SetActive(false);
        PlayerUI.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Move.IsReady == true)
        {
            DialCount = 4;
            DailogueOpen = true;
            TutorialKey.SetActive(false);
            DialogueScreen.SetActive(true);
            MoveKey.SetActive(false);
            Move.IsReady = false;

            Move.enabled = false;

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
        if(DialCount == 3)//일반 이동
        {
            DailogueOpen = false;
            KeyExplain.text = "이동키 : W, A, S, D";
            DialogueScreen.SetActive(false);
            TutorialKey.SetActive(true);
            MoveKey.SetActive(true);
            player.GetComponent<PlayerFsmManager>().enabled = true;
        }
        else if(DialCount == 6)//일반 공격
        {
            DailogueOpen = false;
            KeyExplain.text = "공격 : 왼쪽 마우스";

            DialogueScreen.SetActive(false);
            TutorialKey.SetActive(true);
            AttackMouseKey.SetActive(true);
            player.GetComponent<PlayerFsmManager>().enabled = true;
        }
        else
        {
            player.GetComponent<PlayerFsmManager>().enabled = false;

            ResultText = TutorialStart[DialCount];

        }


    }
}
