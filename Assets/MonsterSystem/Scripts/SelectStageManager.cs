using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectStageManager : MonoBehaviour
{
    [SerializeField] string StageOne;
    [SerializeField] string StageTwo;
    [SerializeField] string StageThree;
    [SerializeField] string StageBoss;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GotStageOne()
    {
        LoadingSceneManager.LoadScene(StageOne);
    }

    public void GoStageTwo()
    {
        LoadingSceneManager.LoadScene(StageTwo);
    }

    public void GoStageThree()
    {
        LoadingSceneManager.LoadScene(StageThree);
    }

    public void GoStageBoss()
    {
        LoadingSceneManager.LoadScene(StageBoss);

    }

}
