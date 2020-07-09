using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testUnlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
            SkillManager.UnlockWidth();
        if (Input.GetKeyDown(KeyCode.U))
            SkillManager.UnlockBack();
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(SkillManager.IsWidth + ", " + SkillManager.IsBack + ", " + SkillManager.IsRush);
        }
    }
}
