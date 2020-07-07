using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;


public class DataController : MonoBehaviour
{

    public float backgroundSound = 1;
    public float effectSound = 1;
    public float mouseMoving = 0.5f;

    public enum Option
    {
        backGround,
        effect,
        mouse

    }
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);//다른 씬에서도 지워지지 않게하기
            }

            return _instance;
        }
    }

    string GameDataFlieName = "0608DataFile.json";

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if(_gameData == null)
            {
                //세이브랑 로드
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    public void LoadGameData()
    {
        
        string filePath = Application.dataPath + GameDataFlieName;//Asset폴더에 저장됨.
        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);//파일이 존재할 시 파일의 정보를 읽어낸다.
            
        }
        else
        {
            _gameData = new GameData();//없을시 새로 생성
        }
        Debug.Log("Load");
    }

    public void SaveGameData()
    {
        string TojsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + GameDataFlieName;
        File.WriteAllText(filePath, TojsonData);//경로에 존재하는 파일에 입력
        Debug.Log("Save");
    }

    private static int count = 0;
    private int index;
    void Awake()
    {
        index = count;

        count++;
        if (index == 0)
        {
            return;
        }
        else

            Destroy(gameObject);//씬으로 이동할 시 파일이 두개 이상 존재할 경우 삭제하기.

    }
    public void SetOption(Option option, int value)
    {
        //Debug.Log(value);
        switch (option)
        {
            case Option.backGround:
                Instance.gameData.BackgroundSound = value;
                backgroundSound = (float)Instance.gameData.BackgroundSound / 100;
                //Debug.Log(Instance.gameData.BackgroundSound);
                Debug.Log(backgroundSound);
                break;
            case Option.effect:
                Instance.gameData.EffectSound = value;
                effectSound = (float)Instance.gameData.EffectSound / 100;
                break;
            case Option.mouse:
                Instance.gameData.MouseMoving = value;
                mouseMoving = (float)Instance.gameData.MouseMoving / 100;
                break;
        }
    }

    public void SetCombo()
    {
        Instance.gameData.PlayerCombo += 1;
        Instance.gameData.IsCombo = true;
    }

    public void SetScreen()
    {
        Screen.SetResolution(Instance.gameData.ResolutionX, Instance.gameData.ResolutionY, Instance.gameData.fullScreen);
    }

    public void SetScriptTrue(int count)
    {
        switch (count)
        {
            case 0:
                Instance.gameData.Scriptcount = 1;
                Instance.gameData.ScriptOne = true;
                break;
            case 1:
                Instance.gameData.Scriptcount = 2;
                Instance.gameData.ScriptTwo = true;

                break;
            case 2:
                Instance.gameData.Scriptcount = 3;
                Instance.gameData.ScriptThree = true;

                break;
            case 3:
                Instance.gameData.Scriptcount = 4;
                Instance.gameData.ScriptFour = true;

                break;
            case 4:
                Instance.gameData.Scriptcount = 5;
                Instance.gameData.ScriptFive = true;

                break;
        }
    }
}
//이 데이터를 사용할 시 데이터 추가할 경우 GameData에 변수 지정.
//변수를 다른 스크립트로 지정할땐 헤더에 using System; 지정 후 더하거나 곲할 값들은 DataController.Instance.gameData.변수이름
