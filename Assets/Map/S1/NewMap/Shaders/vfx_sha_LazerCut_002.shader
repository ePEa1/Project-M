﻿Shader "Custom/vfx_sha_LazerCut_001"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" }
        LOD 200
		

		cull off
		zwrite off

        CGPROGRAM
        #pragma surface surf Lazer noshadow noambient
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 lightDir;
			float3 viewDir;
        };

        fixed4 _Color;


        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

		float4 LightingLazer (SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
		{
			return float4 (s.Albedo, s.Alpha);
		}

        ENDCG
    }
    FallBack "Diffuse"
}
