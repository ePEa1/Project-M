using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeInOut : MonoBehaviour
{
    public Image FadeImg;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeIn()
    {
        Vector4 FadeInAlpha;

        for (int i = 0; i < 255; i++)
        {
            FadeInAlpha = new Vector4(1, 1, 1, i);

            FadeImg.color = FadeInAlpha;
            if(FadeImg.color.a == 1)
            {
                
            }
        }
        yield return null;
    }

}
