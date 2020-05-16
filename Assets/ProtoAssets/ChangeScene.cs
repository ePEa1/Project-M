using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string m_test1;
    [SerializeField] string m_test2;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ChangeTo1()
    {
        SceneManager.LoadScene(m_test1);
    }

    public void ChangeTo2()
    {
        SceneManager.LoadScene(m_test2);
    }
}
