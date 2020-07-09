Shader "Hidden/NewImageEffectShader"
{
    Properties
    {
		_LineColor ("LineColor", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
		_DepthMul ("DepthMul", Range(0,3)) = 0
		_DepthPow ("DepthPow", Range(0,3)) = 0
    }
    SubShader
    {
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

			float4 _LineColor;
            sampler2D _MainTex;
			sampler2D _CameraDepthNormalsTexture;
			float4  _CameraDepthNormalsTexture_TexelSize;
			float _DepthMul;
			float _DepthPow;

			float Compare(float baseDepth, float2 uv, float2 offset)
			{

				float4 neighborDepthNormal = tex2D(_CameraDepthNormalsTexture, uv + (_CameraDepthNormalsTexture_TexelSize.xy * offset));
				float3 neighborNormal;
				float neighborDepth;
				DecodeDepthNormal(neighborDepthNormal, neighborDepth, neighborNormal);
				neighborDepth = neighborDepth * _ProjectionParams.z;
				
				return baseDepth - neighborDepth;

			}


            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

				float4 depthNormal = tex2D(_CameraDepthNormalsTexture, i.uv);
				float3 normal;
				float depth;
				DecodeDepthNormal(depthNormal, depth, normal);

				depth = depth * _ProjectionParams.z;

				float depthDifference = Compare(depth, i.uv, float2(1, 0));
				depthDifference += Compare(depth, i.uv, float2(0, 1));
				depthDifference += Compare(depth, i.uv, float2(0, -1));
				depthDifference += Compare(depth, i.uv, float2(-1, 0));

				depthDifference = depthDifference * _DepthMul;
				depthDifference = saturate(depthDifference);
				depthDifference = pow(depthDifference, _DepthPow);

				float4 final;
				final.rgb = col * (1 - depthDifference * (1-_LineColor));
				final.a = 1;

				return final;


            }
            ENDCG
        }
    }
}
