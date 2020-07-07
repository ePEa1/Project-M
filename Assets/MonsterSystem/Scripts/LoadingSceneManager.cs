using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;


    [SerializeField]
    Image progressBar;
    [SerializeField] Image RotHourglass;
    float RotateSpeed = 3;
    public Image ConceptScree;
    [SerializeField] Text TipsTxt;
    [SerializeField] Sprite[] ConceptImg;


    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0, 2);
        ConceptScree.sprite = ConceptImg[random];
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            RotHourglass.transform.Rotate(new Vector3(0, 0, RotateSpeed), Space.Self);

            timer += Time.deltaTime;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;

                }

            }
        }
    }


}