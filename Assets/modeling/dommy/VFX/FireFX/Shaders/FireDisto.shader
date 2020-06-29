Shader "Custom/FireDisTo"
{
    Properties
    {
        [HDR]_Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MainTex2 ("Albedo2 (RGB)", 2D) = "white" {}
		_RNG ("Pow", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
        LOD 200

        CGPROGRAM
        #pragma surface surf Unlit alpha:fade
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _MainTex2;
		float _RNG;
	

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_MainTex2;
			float3 lightDir;
			float3 viewDir;
			float atten;
        };

        float4 _Color;

        void surf (Input IN, inout SurfaceOutput o)
        {
            float4 d = tex2D (_MainTex2, float2( IN.uv_MainTex2.x, IN.uv_MainTex2.y - _Time.y )) * _RNG;
            float4 c = tex2D (_MainTex, saturate(IN.uv_MainTex + d.r)) * _Color;
			//float3 LL = lerp (c, d, 0.5);
            o.Emission = c.rgb;
            o.Alpha = c.a;
        }

		float4 LightingUnlit (SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
		{
			
			return float4 (s.Emission,s.Alpha);
		
		}
        
		ENDCG
    }
    FallBack "Diffuse"
}
