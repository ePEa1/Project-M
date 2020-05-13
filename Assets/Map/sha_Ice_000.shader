Shader "MFD/sha_BattleShader_000"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
		_Color2 ("Color2", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_RimPower ("_RimPower", Range(0,10)) = 0.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 lightDir;
			float3 viewDir;
			float3 color:COLOR;
        };

        half _Glossiness;
        half _Metallic;
		float _RimPower;
        float4 _Color;
		float4 _Color2;


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			float rim = dot ( o.Normal , IN.viewDir );
			float Rim = saturate(pow ( 1 - rim , _RimPower));

            o.Albedo = _Color2;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = Rim;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
