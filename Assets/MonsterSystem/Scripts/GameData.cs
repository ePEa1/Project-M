using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class GameData
{
    //Options
    public int BackgroundSound = 100;
    public int EffectSound = 100;
    public int MouseMoving = 5;

    public int Shadow = 3;

    public int TexturQuality = 3;

    public bool AmbientOcclution = true;


    public bool Vsync = false;

    public int firstStageMonster = 6;

    public bool StageOneIsOpen = false;
    public bool StageTwoIsOpen = false;
    public bool StageThreeIsOpen = false;
    public bool StageBossIsOpen = false;

    public int ResolutionNum = 0;
    public int WindowNum = 0;
    public int ResolutionX = 1920;
    public int ResolutionY = 1080;
    public bool fullScreen = true;


    //StageCount
    public int FirstStageSavePointOrder = 0;
    public int BossStageSavePointOrder = 0;


    public int PlayerCombo = 0;
    public bool IsCombo = false;

}

