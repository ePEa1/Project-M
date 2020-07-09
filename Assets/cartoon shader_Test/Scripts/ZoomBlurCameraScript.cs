using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]

public class ZoomBlurCameraScript : MonoBehaviour
{
    //private Camera cam;

    public float blurSize = 0.1f;

    public Vector2 blurCenterPos = new Vector2(0.5f, 0.5f);
    
    [Range(1,48)]
    public int samples;

    public Material mat3 = null;

    private void Start()
    {
        //cam = GetComponent<Camera>();
    }

    private void OnRenderImage (RenderTexture src, RenderTexture dest)
    {
        
        if (blurSize > 0.0f)
        {
            mat3.SetInt("_Samples", samples);
            mat3.SetFloat("_BlurSize",blurSize);
            mat3.SetVector("_BlurCenterPos", blurCenterPos);
            Graphics.Blit(src, dest, mat3);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
        
    }

    void Update()
    {
        
    }
}
