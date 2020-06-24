Shader "DGR/SkyboxTEST" {
	 Properties {
				 [HDR]_Color ("Color", Color) = (1,1,1,1)
				 _Blend ("Blend", Range (0, 1) ) = 0.0
				 _Rotation ("Rotation", Range(0, 1)) = 0
				 _Rotation2 ("Rotation2", Range(0, 1)) = 0
				 _Tex ("Cubemap   (HDR)", Cube) = "grey" {}
				 _OverlayTex ("CubemapOverlay (HDR)", Cube) = "grey" {}
				}

SubShader {
       Tags { "Queue"="Background" "RenderType"="Background" }
       Cull Off ZWrite Off

       Pass {

           CGPROGRAM
           #pragma vertex vert
           #pragma fragment frag
           #pragma target 2.0

           #include "UnityCG.cginc"

           samplerCUBE _Tex;
           samplerCUBE _OverlayTex;
           half4 _Tex_HDR;
           half4 _Tint;
           half _Exposure;
           float _Rotation;
		   float _Rotation2;
           float _Blend;
		   float _Color;

           float3 RotateAroundYInDegrees (float3 vertex, float degrees)
           {
               float alpha = degrees * UNITY_PI / 180.0;
               float sina, cosa;
               sincos(alpha, sina, cosa);
               float2x2 m = float2x2(cosa, -sina, sina, cosa);
               return float3(mul(m, vertex.xz), vertex.y).xzy;
           }

           struct appdata_t {
               float4 vertex : POSITION;
			   float4 color : COLOR;
           };

           struct v2f {
               float4 vertex : SV_POSITION;
               float3 texcoord : TEXCOORD0;
			   float4 color : COLOR;
           };

           v2f vert (appdata_t v)
           {
               v2f o;
               float3 rotated = RotateAroundYInDegrees(v.vertex, _Rotation * _Time.y);
               o.vertex = UnityObjectToClipPos(rotated);
               o.texcoord = v.vertex.xyz;
			   o.color = v.color;
               return o;
           }

           float4 frag (v2f i) : SV_Target
           {
               float4 tex = texCUBE (_Tex, i.texcoord);// * _Color
               float4 tex2 = texCUBE (_OverlayTex, i.texcoord);
			   float TexColor = tex.rgb * _Color;
               float4 env = lerp( tex, tex2, _Blend );

               float3 c = DecodeHDR (env, _Tex_HDR);

               return half4(c, 1);
           }
           ENDCG 
       }
}   
Fallback Off

}