Shader "DGR/sha_ZoomBlur"
{
    Properties
    {

        _MainTex ("Texture", 2D) = "white" {}

        [Header(BlurPos)]
        _BlurCenterPosX ("BlurCenterPosX", float) = 0.5
        _BlurCenterPosY ("BlurCenterPosY", float) = 0.5

        _BlurQuality ("BQ", Range(0,1)) = 0.5


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

			sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _BlurCenterPosX;
            float _BlurCenterPosY;
            half _BlurSize;
            half _Samplers;
            float _BlurQuality;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }



            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                
                float2 BlurCenter = float2(_BlurCenterPosX, _BlurCenterPosY);
                
                float dis2Center = distance(BlurCenter, i.uv);

                float2 BlurDir = normalize(i.uv - BlurCenter);

                float4 BlurCol = float4 (0,0,0,1);


				for( int BB = 0; BB < _Samplers; i++ )
                {
                    half Scale = i.uv + BlurDir * i * max(0, dis2Center - );
                    
                }
                
				col *= 1 / _Samplers;

                float final = col;

                return final;
            }
            ENDCG
        }
    }
}
