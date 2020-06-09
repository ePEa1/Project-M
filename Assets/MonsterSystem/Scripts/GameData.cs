using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class GameData
{
    public int BackgroundSound = 100;
    public int EffectSound = 100;
    public int MouseMoving = 100;
}

//사운드 입력받을 땐 DataController.Instance.gameData.변수 이름/100으로 하기..는 이거 내가 왜이래 놨나 몰라..
