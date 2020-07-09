Shader "DGR/Barricades"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_TestTex ("Test", 2D) = "bump" {}
		_Cube ("Cube", Cube) = "" {}
		[HDR]_Color("Color", Color) = (0,0.4381459,0.4622641,1)
		_Color2("Color2", Color) = (0.03484337,0.225924,0.254717,1)
		[HDR]_Color3("Color3", Color) = (0.09433961,0.09433961,0.09433961,1)
		_DepthMaxDistance("Depth Maximum Distance", float) = 1
		_SpecValue ("SpecValue", Range(1,30)) = 3
		_SpecValue2 ("SpecValue2", Range(50,500)) = 50
		_AlphaValue ("Opacity", Range(0,1)) = 0.5
		_ScrollValue ("ScrollValue", Range(0,10)) = 1
		_Noise ("Noise", float) = 0.777
		_Foam ("Foam", float) = 0.4
		_Metallic ("Metallic", Range(0,1)) = 0
		_Smoothness ("Smoothness", Range(0,3)) = 0.8
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }//"Queue" = "Transparent"
		//Tags { "RenderType" = "Opaque" }
        LOD 200

		GrabPass{}

		Cull Back
		Zwrite off

        CGPROGRAM
        #pragma surface surf Standard alpha:fade vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _TestTex;
		sampler2D _CameraDepthTexture;
		sampler2D _GrabTexture;
		float _DepthMaxDistance; 
		samplerCUBE _Cube;
		float _SpecValue;
		float _SpecValue2;
		float _ScrollValue;
		float _Noise;
		float _Foam;
		float _Metallic;
		float _Smoothness;
		float _AlphaValue;


		void vert ( inout appdata_full v )
		{
			float wave;
			wave = sin ( abs ( ( v.texcoord.x * 2 - 1 ) * 100 ) + _Time.y) * 0.003;
			wave += sin ( abs ( ( v.texcoord.y * 2 - 1 ) * 100 ) + _Time.y) * 0.003;

			v.vertex.y += wave / 4;
		}

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_TestTex;
			float3 worldRefl;
			float3 viewDir;
			float3 lightDir;
			float4 screenPos;
			float4 worldPos;
			INTERNAL_DATA
        };

        float4 _Color;
		float4 _Color2;
		float4 _Color3;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			UNITY_TRANSFER_DEPTH(o);
            float3 c = UnpackNormal ( tex2D ( _TestTex, IN.uv_TestTex + _Time.x * 0.5 ) );
			float3 d = UnpackNormal ( tex2D ( _TestTex, IN.uv_TestTex - _Time.x * 0.6 ) );
			o.Normal = (c + d) * 0.1;

			

			float3 reflcolor = texCUBE ( _Cube, WorldReflectionVector ( IN, o.Normal ) ) * 4;

			////rim term
			//float rim = saturate(dot(o.Normal, IN.viewDir));
			//rim = pow(1 - rim,9);

			//Specular term
			float3 H = normalize ( IN.lightDir + IN.viewDir );
			float spec = saturate ( dot ( H, o.Normal ) );
			spec = pow ( spec, _SpecValue2 );

			//Depth term
			float existingDepth01 = tex2Dproj ( _CameraDepthTexture , UNITY_PROJ_COORD( IN.screenPos ) ).r;
			float existingDepthLinear = LinearEyeDepth ( existingDepth01 );

			float depthDifference = existingDepthLinear - IN.screenPos.w;

			float waterDepthDifference01 = saturate ( depthDifference / _DepthMaxDistance );
			float4 waterColor = lerp( _Color , _Color2 , waterDepthDifference01 );
			
			float4 e = tex2D(_MainTex, float2(IN.uv_MainTex.y, IN.uv_MainTex.x + _Time.x ));
			float4 f = tex2D(_MainTex, float2(IN.uv_MainTex.y + (_Time.x * _ScrollValue), IN.uv_MainTex.x ));
			float foam = saturate(depthDifference / _Foam);
			float foamFinal = foam * _Noise;

			float noise1 = f > foamFinal ? 3 : 0;
			float waterFoam = noise1 * _Color3;

			//UNITY_TRANSFER_FOG(o, o.screenPos)

			o.Emission = ((reflcolor * depthDifference));
			o.Emission = waterColor + waterFoam;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
            o.Alpha = _AlphaValue;
        }

		

		//float4 LightingWaterSpecular (SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
		//{

		//	//Specular term
		//	float3 H = normalize ( lightDir + viewDir );
		//	float spec = saturate ( dot ( H, s.Normal ) );
		//	spec = pow ( spec, _SpecValue2 );

		//	//final term
		//	float4 final;
		//	final.rgb = spec * _Color3.rgb * _SpecValue;
		//	final.a = s.Alpha + spec;

		//	return final;
		//}

        ENDCG
    }
    FallBack "Diffuse"
}
