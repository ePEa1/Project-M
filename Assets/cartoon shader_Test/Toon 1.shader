Shader "DGR/Toon"
{
    Properties
    {
		[Header(Texture)]
		[Space(10)]

		[HDR]_Color ("Albedo Color", color) = (1,1,1,1)
        _MainTex ("Albedo", 2D) = "white" {}
		[Space(10)]

		_SpecVal ("SpecVal", int) = 0
		[HDR]_Color2("Specular Color", color) = (1,1,1,1)
		[Space(10)]
		
		_rimPow ("Rim Power", int) = 0
		[HDR]_Color3 ("RimLight Color", color) = (1,1,1,1)
		[Space(10)]

		[HDR]_Color4 ("Outline Color", color) = (1,1,1,1)
		_OutlinePow ("Outline", Range(0,0.02)) = 0.001
		
		[Header(Ramp Texture)]
		[Space(10)]
        _RampTex ("Ramp Texture", 2D) = "white" {}

		//_LitTex ("LightMap", 2D) = "white" {}
		
		[Toggle] _TestToggle ("RampTex On", Float) = 0
		[Toggle] _TestToggle2 ("Ceil Shadow", Float) = 0
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque"}
		LOD 200

		cull off

		CGPROGRAM
		#pragma surface surf Toon addshadow
		#pragma target 3.0

		float4 _Color;
		float4 _Color2;
		float4 _Color3;
		float4 _Color4;

        sampler2D _MainTex;
		sampler2D _RampTex;
		sampler2D _LitTex;
		float _SpecVal;
		float _rimPow;
		float _OutlinePow;

		float _TestToggle;
		float _TestToggle2;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_RampTex;
			float2 uv_LitTex;
			float3 viewDir;
			float3 lightDir;
			float atten;
        };



        void surf (Input IN, inout SurfaceOutput o)
        {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 d = tex2D(_LitTex, IN.uv_LitTex);

			////ToonShader term
			//float diffColor;
			//float ndotl = dot (IN.lightDir , o.Normal);

			//float3 toon = step ( ndotl * (1 - _Color2), d.g )  * 0.5 + 0.5;

			////diffColor = c.rgb * ndotl * _LightColor0.rgb;// * atten
			

			float rim = dot(o.Normal, IN.viewDir);
			float Rim = pow(1 - rim, _rimPow);

			float ceilNum2 = 2;
			float diffColor2 = ceil(Rim * ceilNum2) / 2;

			o.Albedo = c.rgb * _Color;
			o.Emission = c.rgb * diffColor2 * _Color3.rgb;
			o.Alpha = c.a;
        }

		float4 LightingToon (SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
		{
			//ToonShader term
			float diffColor;
			float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;

			float ceilNum = 3;
			diffColor = ceil(ndotl * ceilNum) / 2;

			float4 RampColor = tex2D(_RampTex, float2 (ndotl,0.1));


			float3 H = normalize(lightDir + viewDir);
			float speclit = saturate(dot(s.Normal, H));
			float speclitt = pow(speclit, _SpecVal);

			float SpecSmooth = smoothstep(0.005, 0.01, speclitt);
			float SpecColor = SpecSmooth * 1;
			
			float rim = dot(s.Normal, viewDir);
			float Rim = pow(1 - rim, _rimPow);

			float ceilNum2 = 2;
			float diffColor2 = ceil(Rim * ceilNum2) / 2;

			float4 final;
			final.rgb = (s.Albedo + SpecColor * _Color2) * (1 + diffColor * _TestToggle2 - _TestToggle2) * _LightColor0.rgb;
			final.a = s.Alpha;

			float4 ffinal = final * (1+RampColor*_TestToggle - _TestToggle);//* atten



			//float3 toon = step(ndotl * (1 - _Color2), d.g);

			//diffColor = c.rgb * ndotl * _LightColor0.rgb;// * atten
			return ffinal; //* atten
		}

        ENDCG


		//2pass
		
		Tags 
		{ 
			"RenderType" = "Opaque"
		}

		cull front

		CGPROGRAM
		#pragma surface surf Unlit vertex:vert noshadow noambient
		#pragma target 3.0

		float _OutlinePow;
		float4 _Color4;

		void vert(inout appdata_full v)
		{
			v.vertex.xyz = v.vertex.xyz + v.normal.xyz * _OutlinePow;
		}
		
		struct Input
		{
			float3 viewDir;
			float3 lightDir;
			float atten;
			float4 color:COLOR;
		};


		void surf(Input IN, inout SurfaceOutput o)
		{

		}

		float4 LightingUnlit (SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
		{
			return float4(1, 1, 1, 1) * _Color4;
		}

		ENDCG

		
	}

    FallBack "Diffuse"
}
