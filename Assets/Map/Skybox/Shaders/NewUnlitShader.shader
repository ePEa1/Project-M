// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Rotation" {

	Properties {

		_MainTex ("Base (RGB)", 2D) = "white" {}

        	_Speed ("Speed", float) = 30.0 

	}



	SubShader {

		Tags { "RenderType"="Opaque" }

		LOD 200

		

        Pass {

            Blend SrcAlpha OneMinusSrcAlpha 



            CGPROGRAM

            #pragma vertex vert

            #pragma fragment frag



            sampler2D _MainTex;

            float _Speed;



            struct Vertex {

                float4 vertex : POSITION;

                float2 uv_MainTex : TEXCOORD0;

            };



            struct Fragment {

                float4 vertex : POSITION;

                float2 uv_MainTex : TEXCOORD0;

            };



            Fragment vert (Vertex v) {

                Fragment o;



                float sinX = sin(_Speed * _Time);

                float cosX = cos(_Speed * _Time);

                float2x2 rotationMatrix = float2x2( cosX, sinX, 
													-sinX,  cosX );



                o.vertex = UnityObjectToClipPos(v.vertex);

                o.uv_MainTex = mul(v.uv_MainTex - float2(0.5, 0.5), rotationMatrix) + float2(0.5, 0.5);



                return o;

            }



            float4 frag(Fragment IN) : COLOR {

                return tex2D(_MainTex, IN.uv_MainTex);

            }



            ENDCG

        }

	} 



	FallBack "Diffuse"

}