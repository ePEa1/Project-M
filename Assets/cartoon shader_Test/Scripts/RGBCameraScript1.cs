using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]

public class RGBCameraScript1 : MonoBehaviour
{
    //private Camera cam;

    //[Range(0f,0.5f)]
    public AnimationCurve RGBVal2 = AnimationCurve.Linear(0.0f,0.0f,1.0f,1.0f);
    public float RGBVal;

   // public AnimationCurve RGBVal2 = AnimationCurve.Linear(0.0f,0.0f,1.0f,1.0f);

    public Material mat2 = null;

    private void Start()
    {
        //cam = GetComponent<Camera>();
    }

    private void OnRenderImage (RenderTexture src, RenderTexture dest)
    {
        mat2.SetFloat("_RGBVal", RGBVal2.Evaluate(RGBVal));
        Graphics.Blit(src, dest, mat2);
    }

    void Update()
    {
        
    }
}
