Shader "Hidden/EasyImages/EasySSR"
{
	Properties
	{
		[HideInInspector]_MainTex ("Texture", 2D) = "white" {}

		[HideInInspector]_SSR_Intensity ("SSR Intensity", Float) = 1
		[HideInInspector]_SSR_RayMarchDistance ("SSR RayMarchDistance", Float) = 7.5
		[HideInInspector]_SSR_RayStepBoost ("SSR RayStepBoost", Float) = 0

	}

	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		////////////////Velocity///////////////////
		Pass // #0
		{
			Name "Velocity"

			CGPROGRAM

			#pragma vertex vert_img
			#pragma fragment DepthToVelocityFrag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "EasyImages.SSR.cginc"

			ENDCG
		}

		/////////// SSR.Reflections //////////////
		Pass // #1
		{
			Name "SSRReflections"
			ZTest off Cull Off ZWrite Off
			CGPROGRAM

			#pragma target 3.0
			#pragma multi_compile QUALITY_MEDIUM QUALITY_LOW QUALITY_HIGH
			#pragma vertex vert_img
			#pragma fragment SSRReflectionsFrag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "EasyImages.SSR.cginc"

			ENDCG
		}
		/////////// SSR.Blur //////////////
		Pass // #2
		{
			Name "SSRBlurFrag"

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex SSRBlurVert
			#pragma fragment SSRBlurFrag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "EasyImages.SSR.cginc"

			ENDCG
		}
		/////////// SSR.Combine //////////////
		Pass // #3
		{
			Name "SSRCombine"

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert_img
			#pragma fragment SSRCombineFrag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "EasyImages.SSR.cginc"

			ENDCG
		}		
	}
}
