Shader "DGR/SkyBox_Shader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Cube ("Albedo (RGB)", CUBE) = "" {}
    }
    SubShader
    {
        Tags { "RenderType"="Background" }
        LOD 200

        cull off

        CGPROGRAM
        #pragma surface surf Unlit
        #pragma target 3.0

        samplerCUBE _Cube;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_Cube;
            float3 worldNormal;
        };

        fixed4 _Color;


        void surf (Input IN, inout SurfaceOutput o)
        {
            float3 cube = texCUBE (_Cube, IN.worldNormal) * _Color;
            o.Emission = cube.rgb;
        }

		float4 LightingUnlit (SurfaceOutput s, float3 lightDir, float atten)
		{
			return float4 (1,1,1,1);
		}
        ENDCG
    }
    FallBack "Diffuse"
}
