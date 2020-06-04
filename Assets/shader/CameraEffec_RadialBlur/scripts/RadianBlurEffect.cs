using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class RadianBlurEffect : MonoBehaviour {
    [Range(0.1f,1)]
    public float DownSample;
    public Material RadianBlur;
    [Range(0,1)]
    public float BlurCenterX;
    [Range(0,1)]
    public float BlurCenterY;
    [Range(0,0.1f)]
    public float BlurRatio;
    [Range(0,0.5f)]
    public float ClearDis;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        if (RadianBlur == null)
            Graphics.Blit(source, destination);
        else
        {
            RenderTexture temp0 = RenderTexture.GetTemporary((int)(source.width * DownSample),(int)(source.height * DownSample),0,source.format);
            Graphics.Blit(source,temp0);
            RadianBlur.SetFloat("_RadianCenterX", BlurCenterX);
            RadianBlur.SetFloat("_RadianCenterY", BlurCenterY);
            RadianBlur.SetFloat("_BlurRatio", BlurRatio);
            RadianBlur.SetFloat("_ClearDis", ClearDis);
            Graphics.Blit(temp0, destination, RadianBlur);

            RenderTexture.ReleaseTemporary(temp0);
        }
    }
}
