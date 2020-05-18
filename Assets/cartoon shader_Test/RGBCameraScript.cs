using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]

public class RGBCameraScript : MonoBehaviour
{

    public Material mat2;
    //private Camera cam;

    [Range(0f,0.5f)]
    public float RGBVal;


    private void Start()
    {
        //cam = GetComponent<Camera>();
    }

    private void OnRenderImage (RenderTexture src, RenderTexture dest)
    {
        mat2.SetFloat("_RGBVal", RGBVal);
        Graphics.Blit(src, dest, mat2);
    }

    void Update()
    {
        
    }
}
