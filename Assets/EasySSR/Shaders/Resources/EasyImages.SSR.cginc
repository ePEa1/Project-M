// Upgrade NOTE: commented out 'float4x4 _WorldToCamera', a built-in variable
// Upgrade NOTE: replaced '_WorldToCamera' with 'unity_WorldToCamera'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

#ifndef EASYIMAGES_SSR_INCLUDED
#define EASYIMAGES_SSR_INCLUDED

/////////////////Includes//////////////////////

#include "UnityCG.cginc"

#define _FalloffDistance    _SSR_RayMarchDistance

#if defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)
//////////////////////////////////////
#define MAX_MARCH 12
#define MAX_TRACEBACK_MARCH 4
#define NUM_RAYS 1
//////////////////////////////////////
#else
//////////////////////////////////////
#if QUALITY_LOW
#define MAX_MARCH 12
#define MAX_TRACEBACK_MARCH 4
#define NUM_RAYS 1
//////////////////////////////////////
#elif QUALITY_HIGH
 #define MAX_MARCH 32
#define MAX_TRACEBACK_MARCH 8
#define NUM_RAYS 2
//////////////////////////////////////
#else // QUALITY_MEDIUM
#define MAX_MARCH 16
#define MAX_TRACEBACK_MARCH 8
#define NUM_RAYS 1
//////////////////////////////////////
#endif
#endif

// on d3d9, _CameraDepthTexture is bilinear-filtered. so we need to sample center of pixels.
#if SHADER_API_D3D9 
    #define UVOffset ((_ScreenParams.zw-1.0)*0.5) 
#else
    #define UVOffset 0.0
#endif

//////////////////////////////////////////////////

/////////////////Properties//////////////////////

sampler2D _MainTex;
float4 _MainTex_TexelSize;

sampler2D_float _CameraDepthTexture;
sampler2D _CameraGBufferTexture1;
sampler2D _CameraGBufferTexture2;

sampler2D _VelocityBuffer;

float4x4 _InvViewProj;
float4x4 _PrevViewProj;

uniform sampler2D _ReflectionAccTex;

CBUFFER_START(SSRRarely)

uniform float _SSR_Intensity;
uniform float _SSR_RayMarchDistance;
uniform float _SSR_RayStepBoost;

CBUFFER_END

uniform float4 _BlurOffset;
// uniform float4x4 _WorldToCamera;

//////////////////////////////////////////////////

/////////////////////Types///////////////////////

struct RayHitOut
{
    half hit;
    float advance;
    float2 uv;
};

struct SSRBlurV2F
{
	float4 pos : SV_POSITION;
	float2 uv : TEXCOORD0;
	float2 uvs[8] : TEXCOORD1;
};
//////////////////////////////////////////////////

inline float3x3 tofloat3x3(float4x4 v)
{
    return float3x3(v[0].xyz, v[1].xyz, v[2].xyz);
}
inline float GetDepth(float2 uv)       { return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv); }
inline float3 GetPosition(float2 screen_position, float depth)
{
    float4 pos4 = mul(_InvViewProj, float4(screen_position, depth, 1.0));
    return pos4.xyz / pos4.w;
}
inline float3 GetPosition(float2 uv)
{
    float2 screen_position = uv * 2.0 - 1.0;
    float depth = GetDepth(uv);
    return GetPosition(screen_position, depth);
}
inline float GetLinearDepth(float2 uv) { return LinearEyeDepth(GetDepth(uv)); }
inline half3 GetNormal(float2 uv)      { return tex2D(_CameraGBufferTexture2, uv).xyz * 2.0 - 1.0; }
inline half4 GetSpecular(float2 uv)    { return tex2D(_CameraGBufferTexture1, uv); }
inline float3 GetViewPosition(float2 screen_position, float linear_depth)
{
    float2 p11_22 = float2(unity_CameraProjection._11, unity_CameraProjection._22);
    return float3(screen_position / p11_22, 1.0) * linear_depth;

}
inline float3 GetViewPosition(float2 uv)
{
    float linear_depth = GetLinearDepth(uv);
    return GetViewPosition(uv * 2.0 - 1.0, linear_depth);
}
inline half4 GetVelocity(float2 uv) { return tex2D(_VelocityBuffer, uv); }

float Jitter(float3 p)
{
    float v = dot(p,1.0);
    return frac(sin(v)*53259.7483);
}

half4 DepthToVelocityFrag(v2f_img i) : SV_Target
{
	float2 uv = i.uv;
    float2 screenPos = uv * 2.0 - 1.0;

	float depth = GetDepth(uv);
	if (depth >= 1.0) { return 0.0; }
	float3 p = GetPosition(screenPos, depth);

	float4 ppos4 = mul(_PrevViewProj, float4(p.xyz, 1.0));
	float2 pspos = ppos4.xy / ppos4.w;
	float2 prev_uv = pspos * 0.5 + 0.5 + UVOffset;

	return half4(uv - prev_uv, 0, 0);
}

inline RayHitOut Raycast(float adv, float3 p, float3 vp, float3 n, float smoothness, float march_step, const int max_march, const int max_traceback)
{
    float3x3 proj = tofloat3x3(unity_CameraProjection);

    float3 cam_dir = normalize(p - _WorldSpaceCameraPos);
    float3 ref_dir = normalize(reflect(cam_dir, n.xyz));
    float3 ref_vdir = mul(tofloat3x3(unity_WorldToCamera), ref_dir);

    half hit = 0.0;
    float3 ray_view_pos = 0.0;
    float2 ray_uv = 0.0;
    float ray_depth;
    float ref_depth;

    // raycast
	UNITY_UNROLL
    for(int m=0; m<max_march; ++m) 
	{
        adv += march_step;     
        ray_view_pos = vp + ref_vdir * adv;
        float3 ray_proj_pos = mul(proj, ray_view_pos);
        ray_uv = ray_proj_pos.xy / ray_view_pos.z * 0.5 + 0.5;
        ray_depth = ray_view_pos.z;
        ref_depth = GetLinearDepth(ray_uv);
        if(ray_depth > ref_depth) 
		{
            hit = 1.0;
            break;
        }
		march_step *= (1.0+_SSR_RayStepBoost);
    }

    // trace back
	UNITY_UNROLL
    for(int b=0; b<max_traceback; ++b) 
	{
        adv -= march_step / (max_traceback+1);
        ray_view_pos = vp + ref_vdir * adv;

        float3 ray_proj_pos = mul(proj, ray_view_pos);
        float2 ray_uv_traceback = ray_proj_pos.xy / ray_view_pos.z * 0.5 + 0.5 ;
        float ray_depth_traceback = ray_view_pos.z;
        float ref_depth_traceback = GetLinearDepth(ray_uv_traceback);

        if(ray_depth_traceback < ref_depth_traceback) 
		{
            break;
        }

        ray_uv = ray_uv_traceback;
        ray_depth = ray_depth_traceback;
        ref_depth = ref_depth_traceback;
    }

    if((ray_depth - ref_depth) > march_step) hit = 0.0;

    RayHitOut r;
    r.hit = hit;
    r.advance = adv;
    r.uv = ray_uv;
    return r;
}

inline float4 SampleHitColor(RayHitOut ray, float smoothness)
{
    float2 edge = abs(ray.uv * 2.0 - 1.0);
    float edge_attr = pow(1.0 - max(edge.x, edge.y), 0.5);
	float4 hit_color;
    hit_color.a = max(1.0 - (ray.advance / _FalloffDistance), 0.0) * edge_attr * smoothness * ray.hit;
    hit_color.rgb = tex2D(_MainTex, ray.uv).rgb;
	return hit_color;
}

half4 SSRReflectionsFrag(v2f_img i) : SV_Target
{
    float2 uv = i.uv;
    float2 screenPos = uv * 2.0 - 1.0;

    float depth = GetDepth(uv);
    if(depth == 1.0) { return half4(0,0,0,0); }

    float3 pos = GetPosition(screenPos, depth);
    float3 viewPos = GetViewPosition(screenPos, LinearEyeDepth(depth));
    float3 normal = GetNormal(uv);
    float4 smoothness = GetSpecular(uv).w;
    float4 velocity = GetVelocity(uv);

    float2 prev_uv      = uv - velocity.xy;
    float4 ref_color    = tex2D(_ReflectionAccTex, prev_uv);

    float march_step = _SSR_RayMarchDistance / MAX_MARCH;
    float adv = 0;

    RayHitOut hit = Raycast(adv, pos, viewPos, normal, smoothness, march_step, MAX_MARCH, MAX_TRACEBACK_MARCH);

    float4 hit_color = SampleHitColor(hit, smoothness);

    ref_color = lerp(hit_color, ref_color , 0.5);

    return ref_color;
}

SSRBlurV2F SSRBlurVert (appdata_img v) 
{
	SSRBlurV2F o;
	o.pos = UnityObjectToClipPos(v.vertex);

	o.uv.xy = v.texcoord.xy;
	
	o.uvs[0] =  v.texcoord.xy + _BlurOffset.xy * float2(1,1);
	o.uvs[1] =  v.texcoord.xy + _BlurOffset.xy * float2(-1,-1);
	o.uvs[2] =  v.texcoord.xy + _BlurOffset.xy * float2(2,2);
	o.uvs[3] =  v.texcoord.xy + _BlurOffset.xy * float2(-2,-2);
	o.uvs[4] =  v.texcoord.xy + _BlurOffset.xy * float2(3,3);
	o.uvs[5] =  v.texcoord.xy + _BlurOffset.xy * float2(-3,-3);
	o.uvs[6] =  v.texcoord.xy + _BlurOffset.xy * float2(4,4);
	o.uvs[7] =  v.texcoord.xy + _BlurOffset.xy * float2(-4,-4);

	return o;  
}
		
half SamePlane(half3 n, float2 uv)
{
	half3 otherN = GetNormal(uv);
	half diff = dot(otherN,n);
	return step(0.15,diff);
}

half4 SSRBlurFrag (SSRBlurV2F i) : SV_Target 
{
	const float weights[5] = {0.18, 0.16, 0.13, 0.09,0.05};
	half4 color = float4 (0,0,0,0);
	half3 n = GetNormal(i.uv);
	color += weights[0] * tex2D (_ReflectionAccTex, i.uv);
	color += weights[1] * tex2D (_ReflectionAccTex, i.uvs[0]) * SamePlane(n,i.uvs[0]);
	color += weights[1] * tex2D (_ReflectionAccTex, i.uvs[1]) * SamePlane(n,i.uvs[1]);
	color += weights[2] * tex2D (_ReflectionAccTex, i.uvs[2]) * SamePlane(n,i.uvs[2]);
	color += weights[2] * tex2D (_ReflectionAccTex, i.uvs[3]) * SamePlane(n,i.uvs[3]);
	color += weights[3] * tex2D (_ReflectionAccTex, i.uvs[4]) * SamePlane(n,i.uvs[4]);
	color += weights[3] * tex2D (_ReflectionAccTex, i.uvs[5]) * SamePlane(n,i.uvs[5]);	
	color += weights[4] * tex2D (_ReflectionAccTex, i.uvs[6]) * SamePlane(n,i.uvs[6]);	
	color += weights[4] * tex2D (_ReflectionAccTex, i.uvs[7]) * SamePlane(n,i.uvs[7]);		
	float cut = sign(GetSpecular(i.uv).w);
	color.a *= cut;
	return color;
}

float4 SSRCombineFrag(v2f_img i) : SV_Target
{
    float4 color = tex2D(_MainTex, i.uv);
    float4 reflection_color = tex2D(_ReflectionAccTex, i.uv);
    float alpha = saturate(reflection_color.a * _SSR_Intensity);
    return float4(lerp(color.rgb, reflection_color.rgb, alpha), 1.0);
}

#endif