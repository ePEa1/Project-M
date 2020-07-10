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

    public int AntiAliasing = 2;

    public bool AmbientOcclution = true;
    public bool Vsync = false;
    public bool Fog = true;

    public int firstStageMonster = 6;

    public bool StageOneIsOpen = false;
    public bool StageTwoIsOpen = false;
    public bool StageThreeIsOpen = false;
    public bool StageBossIsOpen = false;

    public int ResolutionNum = 4;
    public int WindowNum = 0;
    public int ResolutionX = 1920;
    public int ResolutionY = 1080;
    public bool fullScreen = true;

    //StageCount
    public bool IsIntroShow = false;

    public int StageEndCount = 0;
    public int FirstStageSavePointOrder = 0;
    public int BossStageSavePointOrder = 0;

    public int QuestOrder = 0;
    public int Scriptcount = 0;
    public bool ScriptOne = false;
    public bool ScriptOneEnd = false;
    public bool ScriptTwo = false;
    public bool ScriptThree = false;
    public bool ScriptFour = false;
    public bool ScriptFive = false;
    public int PlayerCombo = 0;
    public bool IsCombo = false;

}

