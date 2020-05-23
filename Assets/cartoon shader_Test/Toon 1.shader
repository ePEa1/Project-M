Shader "MFD/Toon"
{
    Properties
    {
		[HDR]_Color ("Color", color) = (1,1,1,1)
		[HDR]_Color2("Color2", color) = (1,1,1,1)
		[HDR]_Color3 ("Color3", color) = (1,1,1,1)
		[HDR]_Color4 ("ShadowColor", color) = (1,1,1,1)

		_rimPow ("Rim Power", int) = 0
		_OutlinePow ("Outline", Range(0,0.02)) = 0.001

        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_LitTex ("LightMap", 2D) = "white" {}
		_SpecVal ("SpecVal", int) = 0
		
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque"}
		LOD 200

		cull back

		CGPROGRAM
		#pragma surface surf Toon
		#pragma target 3.0

		float4 _Color;
		float4 _Color2;
		float4 _Color3;
		float4 _Color4;

        sampler2D _MainTex;
		sampler2D _LitTex;
		float _SpecVal;
		float _rimPow;
		float _OutlinePow;
		

        struct Input
        {
            float2 uv_MainTex;
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

			o.Albedo = c.rgb * _Color;
			o.Emission = Rim * _Color3.rgb;
			o.Alpha = c.a;
        }

		float4 LightingToon (SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
		{
			//ToonShader term
			float diffColor;
			float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;

			float ceilNum = 3;
			diffColor = ceil(ndotl * ceilNum) / 10;


			float3 H = normalize(lightDir + viewDir);
			float speclit = saturate(dot(s.Normal, H));
			float speclitt = pow(speclit, _SpecVal);

			float SpecSmooth = smoothstep(0.005, 0.01, speclitt);
			float SpecColor = SpecSmooth * 1;


			float4 final;
			final.rgb = (s.Albedo + SpecColor * _Color2) * diffColor * _LightColor0.rgb;
			final.a = s.Alpha;

			//float3 toon = step(ndotl * (1 - _Color2), d.g);

			//diffColor = c.rgb * ndotl * _LightColor0.rgb;// * atten
			return final; //* atten
		}

        ENDCG


		//2pass
		
		Tags { "RenderType" = "Opaque"}

		cull front

		CGPROGRAM
		#pragma surface surf Unlit vertex:vert
		#pragma target 3.0

		float _OutlinePow;

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
			return float4(0, 0, 0, 1);
		}

		ENDCG

		
	}

    FallBack "Diffuse"
}
