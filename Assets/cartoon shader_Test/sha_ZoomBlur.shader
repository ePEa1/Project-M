Shader "DGR/sha_ZoomBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_RGBVal ("RGBVal", float) = 0

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
            float4 _MainTex_TexelSize;
            float2 _BlurCenterPos;
            half _BlurSize;
            half _Samplers;

            float4 frag (v2f IN) : SV_Target
            {
                float4 col = float4(1,1,1,1);
                
                half2 movedTexcoord = IN.uv - _BlurCenterPos;

				for( int i = 0; i < _Samplers; i++ )
                {
                    half Scale = 1.0f - _BlurSize * _MainTex_TexelSize.x * i;
                    col.rgb += tex2D( _MainTex, movedTexcoord * Scale + _BlurCenterPos ).rgb;
                }
                
				col *= 1 / _Samplers;

                float final = col;

                return final;
            }
            ENDCG
        }
    }
}
