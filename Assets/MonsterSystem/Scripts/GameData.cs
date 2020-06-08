using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
public class GameData 
{
    public float BackgroundSound = 1;
    public float EffectSound = 1;
    public float MouseMoving = 0.1f;
}

//사운드 입력받을 땐 DataController.Instance.gameData.변수 이름/100으로 하기..는 이거 내가 왜이래 놨나 몰라..
