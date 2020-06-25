Shader "Custom/CubeMapMetal"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        [HDR]_Color2 ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _CubeMap ("CUBEMap", CUBE) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard
        #pragma target 3.0

        sampler2D _MainTex;
		samplerCUBE _CubeMap;

        struct Input
        {
            float2 uv_MainTex;
			float3 worldRefl;
			float3 worldNormal;
			INTERNAL_DATA
        };

        half _Glossiness;
        half _Metallic;
        float4 _Color;
		float4 _Color2;


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float4 c = tex2D (_MainTex, IN.uv_MainTex);
			float4 d = (texCUBE (_CubeMap, IN.worldRefl) * c) * _Color2;
			o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Albedo = c.rgb;
			o.Emission = d;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
