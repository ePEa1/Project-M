Shader "Custom/Metal"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
       [HDR] _ECol("SpecColor", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    
        _CubeMap("CubeMap",cube) = ""{}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Toon_Metal
        #pragma target 3.0

        float4 _Color;
        float4 _ECol;
        sampler2D _MainTex;
        samplerCUBE _CubeMap;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldRefl;
        };


        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);

            float4 EC;
            EC = float4(c.rgb, 1) + _ECol;

            o.Albedo = c.rgb;
            o.Emission = (c.rgb * 0.7)+ texCUBE(_CubeMap, WorldReflectionVector(IN,o.Normal)) * c.a * EC;
            o.Alpha = 1;
        }

        float4 LightingToon_Metal(SurfaceOutput s, float3 lightDir, float atten)
        {
            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;

            if ( ndotl > 0.5)
            {
                ndotl = 1;
            }
            else
            {
                ndotl = 0;
            }


            float4 diff;
            diff.rgb = s.Albedo * ndotl * _LightColor0.rgb * atten + s.Emission;
            diff.a = s.Alpha;
            return diff;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
