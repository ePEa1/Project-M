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
                DontDestroyOnLoad(_container);
            }

            return _instance;
        }
    }

    public string GameDataFlieName = "0608DataFile.json";

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if(_gameData == null)
            {
                //세이브랑 로드
            }
            return _gameData;
        }
    }

    public void LoadGameData()
    {
        string filePath = Application.dataPath + GameDataFlieName;
        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
            
        }
        else
        {
            _gameData = new GameData();
        }

    }

    public void SaveGameData()
    {
        string TojsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + GameDataFlieName;
        File.WriteAllText(filePath, TojsonData);
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

            Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
