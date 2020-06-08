using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;


public class DataController : MonoBehaviour
{
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
}
//이 데이터를 사용할 시 데이터 추가할 경우 GameData에 변수 지정.
//변수를 다른 스크립트로 지정할땐 헤더에 using System; 지정 후 더하거나 곲할 값들은 DataController.Instance.gameData.변수이름
