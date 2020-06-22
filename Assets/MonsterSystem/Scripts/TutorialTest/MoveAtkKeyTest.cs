using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectM.ePEa.PlayerData;


public class MoveAtkKeyTest : MonoBehaviour
{
    public Text Explain;

    int Order;
   
    public GameObject AOnekey;
    public GameObject DOnekey;
    public GameObject SOnekey;
    public GameObject Shiftkey;

    public GameObject FrontMousekey;

    int Count;
    public bool IsReady = false;

    private void Awake()
    {
        AOnekey.SetActive(true);
        DOnekey.SetActive(false);
        SOnekey.SetActive(false);
        Shiftkey.SetActive(true);

        FrontMousekey.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        switch (Order)
        {
            case 0:
                TurnLeft();
                break;
            case 1:
                TurnRight();
                break;
            case 2:
                TurnBack();
                break;
            case 3:
                TurnFront();
                break;
        }

    }
    void TurnLeft()
    {
        Explain.text = "왼쪽 이동 공격";

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DOnekey.SetActive(true);
            AOnekey.SetActive(false);
            Order += 1;
            PlayerStats.playerStat.GetMp(4.0f);
        }

    }
    void TurnRight()
    {
        Explain.text = "오른쪽 이동 공격";

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DOnekey.SetActive(false);
            SOnekey.SetActive(true);
            Order += 1;
            PlayerStats.playerStat.GetMp(4.0f);
        }

    }
    void TurnBack()
    {
        Explain.text = "뒤쪽 이동 공격";

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SOnekey.SetActive(false);
            Shiftkey.SetActive(false);
            FrontMousekey.SetActive(true);

            PlayerStats.playerStat.GetMp(4.0f);
            Order += 1;
        }
    }
    void TurnFront()
    {
        Explain.text = "앞 이동 공격";

        if (Input.GetMouseButtonDown(1))
        {
            IsReady = true;
        }
    }

    void OrderPlus()
    {
        Order += 1;
    }
}
