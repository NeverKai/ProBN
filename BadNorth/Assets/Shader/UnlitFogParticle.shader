Shader "Unlit/FogParticle" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_FogTex ("Fog Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		_Offset ("Offset", Range(0, 1)) = 0
	}
	SubShader {
		LOD 100
		Tags { "RenderType" = "Transparent" }
		Pass {
			LOD 100
			Tags { "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			GpuProgramID 31167
			Program "vp" {
				SubProgram "d3d11 " {
					"vs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform VGlobals {
						vec4 unused_0_0[56];
						float _WaterLevel;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_4[8];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_2[2];
					};
					in  vec4 in_COLOR0;
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD3;
					out float vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					void main()
					{
					    u_xlat0 = vec4(_WaterLevel) * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = _Time.y * 0.00999999978;
					    u_xlat0.xy = in_POSITION0.xz * vec2(0.25, 0.25) + u_xlat0.xx;
					    vs_TEXCOORD3.xy = in_TEXCOORD0.xy * vec2(0.200000003, 0.200000003) + u_xlat0.xy;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat0.x = dot(in_POSITION0.xz, in_POSITION0.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat2 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1 = u_xlat0.x / u_xlat2;
					    vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
					    vs_COLOR0 = in_COLOR0;
					    return;
					}"
				}
			}
			Program "fp" {
				SubProgram "d3d11 " {
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform PGlobals {
						vec4 unused_0_0[9];
						vec4 _LutLerp;
						vec4 unused_0_2[48];
						vec4 _FogColor;
						vec4 unused_0_4[5];
						vec4 _Color;
						vec4 unused_0_6;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _FogTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD3;
					in  float vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD1 * 12.0 + -2.0;
					    u_xlat1 = textureLod(_MainTex, vs_TEXCOORD0.xy, u_xlat0.x);
					    u_xlat0 = textureLod(_FogTex, vs_TEXCOORD3.xy, u_xlat0.x);
					    u_xlat0.x = u_xlat1.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x + -1.0;
					    u_xlat0.x = u_xlat0.x * vs_COLOR0.w;
					    u_xlat2 = _FogColor.w * _Color.w;
					    u_xlat0.x = u_xlat0.x * u_xlat2;
					    u_xlat0.x = u_xlat0.x * 0.0500000007;
					    u_xlat2 = vs_TEXCOORD1;
					    u_xlat2 = clamp(u_xlat2, 0.0, 1.0);
					    u_xlat2 = (-u_xlat2) + 1.0;
					    SV_Target0.w = u_xlat2 * u_xlat0.x;
					    SV_Target0.xyz = _LutLerp.www * _LutLerp.xyz;
					    return;
					}"
				}
			}
		}
		Pass {
			LOD 100
			Tags { "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			GpuProgramID 106493
			Program "vp" {
				SubProgram "d3d11 " {
					"vs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform VGlobals {
						vec4 unused_0_0[56];
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_3[8];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_2[2];
					};
					in  vec4 in_COLOR0;
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD3;
					out float vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = _Time.y * 0.00999999978;
					    u_xlat0.xy = in_POSITION0.xz * vec2(0.25, 0.25) + u_xlat0.xx;
					    vs_TEXCOORD3.xy = in_TEXCOORD0.xy * vec2(0.200000003, 0.200000003) + u_xlat0.xy;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat0.x = dot(in_POSITION0.xz, in_POSITION0.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat2 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1 = u_xlat0.x / u_xlat2;
					    vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
					    vs_COLOR0 = in_COLOR0;
					    return;
					}"
				}
			}
			Program "fp" {
				SubProgram "d3d11 " {
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform PGlobals {
						vec4 unused_0_0[9];
						vec4 _LutLerp;
						vec4 unused_0_2[40];
						vec4 _CloudCoverage;
						vec4 unused_0_4[7];
						vec4 _FogColor;
						vec4 unused_0_6[5];
						vec4 _Color;
						vec4 unused_0_8;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _FogTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD3;
					in  float vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD1 * 12.0 + -2.0;
					    u_xlat1 = textureLod(_MainTex, vs_TEXCOORD0.xy, u_xlat0.x);
					    u_xlat0 = textureLod(_FogTex, vs_TEXCOORD3.xy, u_xlat0.x);
					    u_xlat0.x = u_xlat1.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x + -1.0;
					    u_xlat0.x = u_xlat0.x * vs_COLOR0.w;
					    u_xlat1 = _FogColor * _Color;
					    SV_Target0.w = u_xlat0.x * u_xlat1.w;
					    u_xlat0.x = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = _FogColor.xyz * _Color.xyz + (-u_xlat0.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    SV_Target0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
			}
		}
	}
}