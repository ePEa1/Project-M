Shader "DGR/sha_RGB_SP"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_RGBVal ("RGBVal", float) = 0
		_Val ("Val", float) = 0
		_RGBVal2 ("RGBVal", float) = 0

        //_BlurCenterPos ("BlurCenterPos", vector) = (0,0,0,0)

    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }


            sampler2D _MainTex;
			float _RGBVal;
            float _Val;
            float _RGBVal2;
            float4 _MainTex_TexelSize;
            float2 _BlurCenterPos;
            half _BlurSize;
            half _Samplers;



            fixed4 frag (v2f IN) : SV_Target
            {
				float4 col; //tex2D(_MainTex, i.uv);
                

				float2 texture2 = IN.uv * 2 - 1;
				float radialGradient = saturate(dot(texture2, texture2));
				radialGradient = radialGradient * radialGradient * radialGradient;

				float colR = tex2D(_MainTex, IN.uv + float2 (_RGBVal2, _RGBVal2) * 0.1 * radialGradient).r;
				float colG = tex2D(_MainTex, IN.uv).g;
				float colB = tex2D(_MainTex, IN.uv - float2 (_RGBVal2, _RGBVal2) * 0.1 * radialGradient).b;

                col.rgb = float3 (colR, colG, colB);

                float final = col;

                return col;
            }
            ENDCG
        }
    }
}
