using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMainManager : MonoBehaviour
{

    public GameObject player;

    [SerializeField] GameObject PlayerUI;
    [SerializeField] GameObject SpawnMonster;
    [SerializeField] Image DialogueBox;
    [SerializeField] Text DialogueText;
    [SerializeField] GameObject Potal;//튜토리얼 끝날 때 돌아가기


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
