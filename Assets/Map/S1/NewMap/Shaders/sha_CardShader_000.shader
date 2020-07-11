﻿Shader "MFD/CardDissolveShader"
{
    Properties
    {
		[Header(Albedo)]
		[HDR]_MainColor ("AlbedoColor", color) = (1,1,1,1)
		_MainTex2 ("Albedo (RGB)", 2D) = "white" {}

		[Header(Noise)]
        [HDR]_hdrColor ("Color", Color) = (1,1,1,1)
		[HDR]_hdrColor2 ("Color2", Color) = (1,1,1,1)
        _MainTex ("Noise", 2D) = "white" {}

		[Header(Noise2)]
		_TestTex("Noise2", 2D) = "white" {}
		_NoiseValue ("NoiseValue", Range(-0.05,1)) = 0


    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector" = "True"}
        LOD 200

		zwrite off
		cull off

        CGPROGRAM
        #pragma surface surf Unlit alpha:fade vertex:vert
        #pragma target 3.0


        sampler2D _MainTex; //기본텍스
		sampler2D _MainTex2;

		sampler2D _TestTex; //노이즈 텍스
		float _NoiseValue; //노이즈 보이는 양



		struct appdata_particles 
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 color : COLOR;
			float4 texcoord : TEXCOORD0;

			float4 CustomDataTest : TEXCOORD1; //커스텀 데이터

		};


        struct Input
        {
            float2 uv_MainTex;
			float2 uv_MainTex2;
			float2 uv_TestTex;
			float2 texcoord;
			float4 color;

			float4 CustomDataFinal; //커스텀 데이터
        };

		void vert ( inout appdata_particles v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.uv_MainTex = v.texcoord.xy; //기본 UV표현을 위한 좌표1 (필수)
			o.texcoord = v.texcoord.zw; //기본 UV표현을 위한 좌표2 (필수)
			o.color = v.color;
			o.CustomDataFinal = v.CustomDataTest;
			o.uv_TestTex = v.texcoord.xy;
		}

		float4 _MainColor;
        float4 _hdrColor;
		float4 _hdrColor2;


        void surf (Input IN, inout SurfaceOutput o)
        {
			float4 maintex = tex2D (_MainTex2, IN.uv_MainTex2) * _MainColor;
			float4 noise = tex2D(_TestTex, IN.uv_TestTex);
			float4 c = tex2D(_MainTex, IN.uv_MainTex);

			//float alpha;

			//if (noise.r >= IN.CustomDataFinal.x)
			//	alpha = 1;
			//else
			//	alpha = 0;


			//float outline;

			//if (noise.r >= IN.CustomDataFinal.x * 2)
			//	outline = 0;
			//else
			//	outline = 1;

			//////////////////////////////////////////////////////

			//float alpha2;

			//if (c.r >= IN.CustomDataFinal.y)
			//	alpha2 = 1;
			//else
			//	alpha2 = 0;


			//float outline2;

			//if (c.r >= IN.CustomDataFinal.y * 2)
			//	outline2 = 0;
			//else
			//	outline2 = 1;


			//위 if문들을 아래 step( x , y ) 함수로 대체함 

			float alpha = step(IN.CustomDataFinal.x, noise.r);
			float outline = step(noise.r, IN.CustomDataFinal.x * 1.2);

			//////////////////////////////////////////////////////

			float alpha2 = step(IN.CustomDataFinal.y, c.r);
			float outline2 = step(c.r, IN.CustomDataFinal.y * 1.2);

			o.Albedo = maintex.rgb;
			o.Emission = (outline * _hdrColor.rgb) + (outline2 * _hdrColor.rgb);
			o.Alpha = alpha * alpha2 * IN.color.a;
        }

		float4 LightingUnlit(SurfaceOutput s, float3 viewDir, float3 atten)
		{
			return float4 (s.Emission, s.Alpha);
		}



        ENDCG
    }
    FallBack "Diffuse"
}
