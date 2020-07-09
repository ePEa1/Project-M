using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]

public class RGBCameraScript : MonoBehaviour
{
    //private Camera cam;

    #region Inspectors

    //[Range(0f,0.5f)]    
    [SerializeField] AnimationCurve RGBVal2 = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    [SerializeField] float m_animSpeed = 1.0f;

    #endregion

    #region Value

    float RGBVal;

    #endregion

    // public AnimationCurve RGBVal2 = AnimationCurve.Linear(0.0f,0.0f,1.0f,1.0f);

    public Material mat2 = null;

    private void Start()
    {
        //cam = GetComponent<Camera>();
    }

    private void OnRenderImage (RenderTexture src, RenderTexture dest)
    {
        mat2.SetFloat("_RGBVal", RGBVal2.Evaluate(1 - RGBVal));
        Graphics.Blit(src, dest, mat2);
    }

    public void PlayAnimation()
    {
        RGBVal = 1.0f;
    }

    private void Update()
    {
        RGBVal = Mathf.Max(0, RGBVal - Time.deltaTime * m_animSpeed);
    }
}
