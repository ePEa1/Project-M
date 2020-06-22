Shader "DGR/CustomSkybox" {
   Properties {
      _Cube ("Environment Map", Cube) = "white" {}
	  _RotationSpeed ("RotationSpeed", Range(0,1)) = 0.5
	  _Rotation ("Rotation", Range(0,1)) = 0.5
	  _Test ("_Test", Range(0,1)) = 0.5
   }

   SubShader {
      Tags { "RenderType"="Background" "Queue"="Background" }

      Pass {
         ZWrite Off 
         Cull Off

         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag

         samplerCUBE _Cube;
		 float _RotationSpeed;
		 float _Rotation;
		 float _Test;

         struct vertexInput 
		 {
            float4 vertex : POSITION;
            float3 texcoord : TEXCOORD0;
         };

         struct v2f
		 {
            float4 vertex : SV_POSITION;
            float3 texcoord : TEXCOORD0;
         };

         //float4 RotateAroundY(float4 vertex, float radian)
		 //{
		//	float sina, cosa;	
		//	sincos(radian, sina, cosa);

		//	float4x4 m;

		//	m[0] = float4(cosa, 0, sina, 0);
		//	m[1] = float4(0, 1, 0, 0);
		//	m[2] = float4(-sina, 0, cosa, 0);
		//	m[3] = float4(0, 0, 0, 1);

		//	return mul(m, vertex);
		// }


         v2f vert(vertexInput input)
         {
            v2f output;

			float3 appendResult1129 = (float3(vertexInput.vertex.x , ( vertexInput.vertex.y * _Test ) , vertexInput.vertex.z));
			float3 normalizeResult1130 = normalize( appendResult1129 );
			float3 appendResult56 = (float3(cos( radians( ( _Rotation + ( _Time.y * _RotationSpeed ) ) ) ) , 0 , ( sin( radians( ( _Rotation + ( _Time.y * _RotationSpeed ) ) ) ) * -1 )));
			float3 appendResult266 = (float3(0 , _Test , 0));
			float3 appendResult58 = (float3(sin( radians( ( _Rotation + ( _Time.y * _RotationSpeed ) ) ) ) , 0 , cos( radians( ( _Rotation + ( _Time.y * _RotationSpeed ) ) ) )));

            output.vertex = UnityObjectToClipPos(input.vertex);
            output.texcoord = input.texcoord;
			
            return output;
         }

         float4 frag (v2f input)
         {
            return texCUBE (_Cube, float3 ( input.texcoord.x, input.texcoord.y, input.texcoord.z ));
         }
         ENDCG 
      }
   } 	
}