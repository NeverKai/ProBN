Shader "Unlit/BillboardUnlit" {
	Properties {
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		[KeywordEnum(X, Y, Z)] _AXIS ("Primary Axis", Float) = 0
		[Toggle] _Mirror ("Mirror", Float) = 0
	}
	SubShader {
		LOD 100
		Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "RenderType" = "Opaque" }
		Pass {
			LOD 100
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "RenderType" = "Opaque" }
			AlphaToMask On
			Cull Off
			GpuProgramID 60174
			Program "vp" {
				SubProgram "d3d11 " {
					Keywords { "_AXIS_X" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_X" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_LOWEND_ON" "_AXIS_X" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_LOWEND_ON" "_AXIS_X" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_X" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat3.xyz;
					    u_xlat1.y = (-unity_MatrixV[0].z);
					    u_xlat1.z = (-unity_MatrixV[1].z);
					    u_xlat1.x = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat10 = dot((-u_xlat1.xyz), (-u_xlat1.xyz));
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat1.xyz = vec3(u_xlat10) * (-u_xlat1.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * in_TEXCOORD1.yyy;
					    u_xlat10 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_X" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat3.xyz;
					    u_xlat1.y = (-unity_MatrixV[0].z);
					    u_xlat1.z = (-unity_MatrixV[1].z);
					    u_xlat1.x = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat10 = dot((-u_xlat1.xyz), (-u_xlat1.xyz));
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat1.xyz = vec3(u_xlat10) * (-u_xlat1.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * in_TEXCOORD1.yyy;
					    u_xlat10 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_LOWEND_ON" "_AXIS_X" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat3.xyz;
					    u_xlat1.y = (-unity_MatrixV[0].z);
					    u_xlat1.z = (-unity_MatrixV[1].z);
					    u_xlat1.x = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat10 = dot((-u_xlat1.xyz), (-u_xlat1.xyz));
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat1.xyz = vec3(u_xlat10) * (-u_xlat1.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * in_TEXCOORD1.yyy;
					    u_xlat10 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" "_AXIS_X" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat3.xyz;
					    u_xlat1.y = (-unity_MatrixV[0].z);
					    u_xlat1.z = (-unity_MatrixV[1].z);
					    u_xlat1.x = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat10 = dot((-u_xlat1.xyz), (-u_xlat1.xyz));
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat1.xyz = vec3(u_xlat10) * (-u_xlat1.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * in_TEXCOORD1.yyy;
					    u_xlat10 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Y" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Y" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Y" }
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
						vec4 unused_0_0[53];
						float _CameraUpScale;
						vec4 unused_0_2[2];
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_7;
						float _AAFactor;
						vec4 unused_0_9[4];
						vec4 _MainTex_ST;
						vec4 unused_0_11[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat3.xyz;
					    u_xlat1.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat1.xxx;
					    u_xlat1.x = (-unity_MatrixV[0].z);
					    u_xlat1.y = (-unity_MatrixV[1].z);
					    u_xlat1.z = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = u_xlat1.xyz * in_TANGENT0.www;
					    u_xlat10 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat2.xyz = vec3(u_xlat10) * u_xlat2.xyz;
					    u_xlat2.xyz = in_TEXCOORD1.xxx * (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.w = u_xlat2.y * _CameraUpScale;
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Y" }
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
						vec4 unused_0_0[53];
						float _CameraUpScale;
						vec4 unused_0_2[2];
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_7;
						float _AAFactor;
						vec4 unused_0_9[4];
						vec4 _MainTex_ST;
						vec4 unused_0_11[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat3.xyz;
					    u_xlat1.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat1.xxx;
					    u_xlat1.x = (-unity_MatrixV[0].z);
					    u_xlat1.y = (-unity_MatrixV[1].z);
					    u_xlat1.z = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = u_xlat1.xyz * in_TANGENT0.www;
					    u_xlat10 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat2.xyz = vec3(u_xlat10) * u_xlat2.xyz;
					    u_xlat2.xyz = in_TEXCOORD1.xxx * (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.w = u_xlat2.y * _CameraUpScale;
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[53];
						float _CameraUpScale;
						vec4 unused_0_2[2];
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_7;
						float _AAFactor;
						vec4 unused_0_9[4];
						vec4 _MainTex_ST;
						vec4 unused_0_11[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat3.xyz;
					    u_xlat1.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat1.xxx;
					    u_xlat1.x = (-unity_MatrixV[0].z);
					    u_xlat1.y = (-unity_MatrixV[1].z);
					    u_xlat1.z = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = u_xlat1.xyz * in_TANGENT0.www;
					    u_xlat10 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat2.xyz = vec3(u_xlat10) * u_xlat2.xyz;
					    u_xlat2.xyz = in_TEXCOORD1.xxx * (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.w = u_xlat2.y * _CameraUpScale;
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[53];
						float _CameraUpScale;
						vec4 unused_0_2[2];
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_7;
						float _AAFactor;
						vec4 unused_0_9[4];
						vec4 _MainTex_ST;
						vec4 unused_0_11[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat3.xyz;
					    u_xlat1.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat1.xxx;
					    u_xlat1.x = (-unity_MatrixV[0].z);
					    u_xlat1.y = (-unity_MatrixV[1].z);
					    u_xlat1.z = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = u_xlat1.xyz * in_TANGENT0.www;
					    u_xlat10 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat2.xyz = vec3(u_xlat10) * u_xlat2.xyz;
					    u_xlat2.xyz = in_TEXCOORD1.xxx * (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.w = u_xlat2.y * _CameraUpScale;
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Z" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Z" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Z" "_LOWEND_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Z" "_LOWEND_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Z" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					float u_xlat7;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat2.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat2.xyz;
					    u_xlat1.x = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat1.xyz;
					    u_xlat7 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat7 = inversesqrt(u_xlat7);
					    u_xlat1.xyz = vec3(u_xlat7) * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * in_TEXCOORD1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Z" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					float u_xlat7;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat2.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat2.xyz;
					    u_xlat1.x = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat1.xyz;
					    u_xlat7 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat7 = inversesqrt(u_xlat7);
					    u_xlat1.xyz = vec3(u_xlat7) * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * in_TEXCOORD1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Z" "_LOWEND_ON" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					float u_xlat7;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat2.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat2.xyz;
					    u_xlat1.x = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat1.xyz;
					    u_xlat7 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat7 = inversesqrt(u_xlat7);
					    u_xlat1.xyz = vec3(u_xlat7) * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * in_TEXCOORD1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Z" "_LOWEND_ON" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					float u_xlat7;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat2.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat2.xyz;
					    u_xlat1.x = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat1.xyz;
					    u_xlat7 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat7 = inversesqrt(u_xlat7);
					    u_xlat1.xyz = vec3(u_xlat7) * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * in_TEXCOORD1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_AXIS_X" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_AXIS_X" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_LOWEND_ON" "_AXIS_X" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" "_AXIS_X" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_AXIS_X" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat4;
					float u_xlat5;
					float u_xlat12;
					void main()
					{
					    u_xlat0.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat0.xyz;
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.y = unity_MatrixV[2].z;
					    u_xlat2.xy = (-u_xlat1.yx);
					    u_xlat1.xy = u_xlat1.xy / unity_MatrixV[1].zz;
					    u_xlat2.z = unity_MatrixV[1].z;
					    u_xlat3.xyz = u_xlat0.yzx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.zxy * u_xlat0.zxy + (-u_xlat3.xyz);
					    u_xlat12 = dot((-u_xlat2.xyz), (-u_xlat2.xyz));
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat2.xyz = vec3(u_xlat12) * (-u_xlat2.xyz);
					    u_xlat2.xyz = u_xlat2.xyz * in_TEXCOORD1.yyy;
					    u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * in_TEXCOORD1.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat12 = u_xlat0.y + (-_WaterLevel);
					    u_xlat1.xy = u_xlat1.xy * vec2(u_xlat12) + u_xlat0.xz;
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat5 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat5;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat1.x = u_xlat1.x + u_xlat1.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat5 * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat12 = u_xlat12 / u_xlat1.x;
					    vs_TEXCOORD1.y = u_xlat12 + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat4 = _WaterLevel * 2.0 + (-u_xlat0.y);
					    u_xlat1 = vec4(u_xlat4) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_X" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat4;
					float u_xlat5;
					float u_xlat12;
					void main()
					{
					    u_xlat0.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat0.xyz;
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.y = unity_MatrixV[2].z;
					    u_xlat2.xy = (-u_xlat1.yx);
					    u_xlat1.xy = u_xlat1.xy / unity_MatrixV[1].zz;
					    u_xlat2.z = unity_MatrixV[1].z;
					    u_xlat3.xyz = u_xlat0.yzx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.zxy * u_xlat0.zxy + (-u_xlat3.xyz);
					    u_xlat12 = dot((-u_xlat2.xyz), (-u_xlat2.xyz));
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat2.xyz = vec3(u_xlat12) * (-u_xlat2.xyz);
					    u_xlat2.xyz = u_xlat2.xyz * in_TEXCOORD1.yyy;
					    u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * in_TEXCOORD1.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat12 = u_xlat0.y + (-_WaterLevel);
					    u_xlat1.xy = u_xlat1.xy * vec2(u_xlat12) + u_xlat0.xz;
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat5 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat5;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat1.x = u_xlat1.x + u_xlat1.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat5 * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat12 = u_xlat12 / u_xlat1.x;
					    vs_TEXCOORD1.y = u_xlat12 + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat4 = _WaterLevel * 2.0 + (-u_xlat0.y);
					    u_xlat1 = vec4(u_xlat4) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_LOWEND_ON" "_AXIS_X" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat4;
					float u_xlat5;
					float u_xlat12;
					void main()
					{
					    u_xlat0.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat0.xyz;
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.y = unity_MatrixV[2].z;
					    u_xlat2.xy = (-u_xlat1.yx);
					    u_xlat1.xy = u_xlat1.xy / unity_MatrixV[1].zz;
					    u_xlat2.z = unity_MatrixV[1].z;
					    u_xlat3.xyz = u_xlat0.yzx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.zxy * u_xlat0.zxy + (-u_xlat3.xyz);
					    u_xlat12 = dot((-u_xlat2.xyz), (-u_xlat2.xyz));
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat2.xyz = vec3(u_xlat12) * (-u_xlat2.xyz);
					    u_xlat2.xyz = u_xlat2.xyz * in_TEXCOORD1.yyy;
					    u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * in_TEXCOORD1.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat12 = u_xlat0.y + (-_WaterLevel);
					    u_xlat1.xy = u_xlat1.xy * vec2(u_xlat12) + u_xlat0.xz;
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat5 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat5;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat1.x = u_xlat1.x + u_xlat1.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat5 * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat12 = u_xlat12 / u_xlat1.x;
					    vs_TEXCOORD1.y = u_xlat12 + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat4 = _WaterLevel * 2.0 + (-u_xlat0.y);
					    u_xlat1 = vec4(u_xlat4) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" "_AXIS_X" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat4;
					float u_xlat5;
					float u_xlat12;
					void main()
					{
					    u_xlat0.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat0.xyz;
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.y = unity_MatrixV[2].z;
					    u_xlat2.xy = (-u_xlat1.yx);
					    u_xlat1.xy = u_xlat1.xy / unity_MatrixV[1].zz;
					    u_xlat2.z = unity_MatrixV[1].z;
					    u_xlat3.xyz = u_xlat0.yzx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.zxy * u_xlat0.zxy + (-u_xlat3.xyz);
					    u_xlat12 = dot((-u_xlat2.xyz), (-u_xlat2.xyz));
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat2.xyz = vec3(u_xlat12) * (-u_xlat2.xyz);
					    u_xlat2.xyz = u_xlat2.xyz * in_TEXCOORD1.yyy;
					    u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * in_TEXCOORD1.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat12 = u_xlat0.y + (-_WaterLevel);
					    u_xlat1.xy = u_xlat1.xy * vec2(u_xlat12) + u_xlat0.xz;
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat5 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat5;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat1.x = u_xlat1.x + u_xlat1.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat5 * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat12 = u_xlat12 / u_xlat1.x;
					    vs_TEXCOORD1.y = u_xlat12 + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat4 = _WaterLevel * 2.0 + (-u_xlat0.y);
					    u_xlat1 = vec4(u_xlat4) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_AXIS_Y" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_AXIS_Y" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_AXIS_Y" }
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
						vec4 unused_0_0[53];
						float _CameraUpScale;
						vec4 unused_0_2[2];
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_7;
						float _AAFactor;
						vec4 unused_0_9[4];
						vec4 _MainTex_ST;
						vec4 unused_0_11[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat4;
					float u_xlat5;
					float u_xlat12;
					void main()
					{
					    u_xlat0.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat0.xyz;
					    u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.y = unity_MatrixV[2].z;
					    u_xlat2.xz = (-u_xlat1.xy);
					    u_xlat1.xy = u_xlat1.xy / unity_MatrixV[1].zz;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat3.xyz = u_xlat0.yzx * u_xlat2.zxy;
					    u_xlat3.xyz = u_xlat2.yzx * u_xlat0.zxy + (-u_xlat3.xyz);
					    u_xlat0.xyz = u_xlat0.xyz * in_TEXCOORD1.yyy;
					    u_xlat2.xyz = u_xlat2.xyz * in_TANGENT0.www;
					    u_xlat12 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat3.xyz = vec3(u_xlat12) * u_xlat3.xyz;
					    u_xlat0.xyz = in_TEXCOORD1.xxx * (-u_xlat3.xyz) + u_xlat0.xyz;
					    u_xlat0.w = u_xlat0.y * _CameraUpScale;
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat2.xyz = (-u_xlat2.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xwz + u_xlat2.xyz;
					    u_xlat12 = u_xlat0.y + (-_WaterLevel);
					    u_xlat1.xy = u_xlat1.xy * vec2(u_xlat12) + u_xlat0.xz;
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat5 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat5;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat1.x = u_xlat1.x + u_xlat1.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat5 * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat12 = u_xlat12 / u_xlat1.x;
					    vs_TEXCOORD1.y = u_xlat12 + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat4 = _WaterLevel * 2.0 + (-u_xlat0.y);
					    u_xlat1 = vec4(u_xlat4) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Y" }
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
						vec4 unused_0_0[53];
						float _CameraUpScale;
						vec4 unused_0_2[2];
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_7;
						float _AAFactor;
						vec4 unused_0_9[4];
						vec4 _MainTex_ST;
						vec4 unused_0_11[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat4;
					float u_xlat5;
					float u_xlat12;
					void main()
					{
					    u_xlat0.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat0.xyz;
					    u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.y = unity_MatrixV[2].z;
					    u_xlat2.xz = (-u_xlat1.xy);
					    u_xlat1.xy = u_xlat1.xy / unity_MatrixV[1].zz;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat3.xyz = u_xlat0.yzx * u_xlat2.zxy;
					    u_xlat3.xyz = u_xlat2.yzx * u_xlat0.zxy + (-u_xlat3.xyz);
					    u_xlat0.xyz = u_xlat0.xyz * in_TEXCOORD1.yyy;
					    u_xlat2.xyz = u_xlat2.xyz * in_TANGENT0.www;
					    u_xlat12 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat3.xyz = vec3(u_xlat12) * u_xlat3.xyz;
					    u_xlat0.xyz = in_TEXCOORD1.xxx * (-u_xlat3.xyz) + u_xlat0.xyz;
					    u_xlat0.w = u_xlat0.y * _CameraUpScale;
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat2.xyz = (-u_xlat2.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xwz + u_xlat2.xyz;
					    u_xlat12 = u_xlat0.y + (-_WaterLevel);
					    u_xlat1.xy = u_xlat1.xy * vec2(u_xlat12) + u_xlat0.xz;
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat5 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat5;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat1.x = u_xlat1.x + u_xlat1.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat5 * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat12 = u_xlat12 / u_xlat1.x;
					    vs_TEXCOORD1.y = u_xlat12 + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat4 = _WaterLevel * 2.0 + (-u_xlat0.y);
					    u_xlat1 = vec4(u_xlat4) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[53];
						float _CameraUpScale;
						vec4 unused_0_2[2];
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_7;
						float _AAFactor;
						vec4 unused_0_9[4];
						vec4 _MainTex_ST;
						vec4 unused_0_11[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat4;
					float u_xlat5;
					float u_xlat12;
					void main()
					{
					    u_xlat0.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat0.xyz;
					    u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.y = unity_MatrixV[2].z;
					    u_xlat2.xz = (-u_xlat1.xy);
					    u_xlat1.xy = u_xlat1.xy / unity_MatrixV[1].zz;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat3.xyz = u_xlat0.yzx * u_xlat2.zxy;
					    u_xlat3.xyz = u_xlat2.yzx * u_xlat0.zxy + (-u_xlat3.xyz);
					    u_xlat0.xyz = u_xlat0.xyz * in_TEXCOORD1.yyy;
					    u_xlat2.xyz = u_xlat2.xyz * in_TANGENT0.www;
					    u_xlat12 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat3.xyz = vec3(u_xlat12) * u_xlat3.xyz;
					    u_xlat0.xyz = in_TEXCOORD1.xxx * (-u_xlat3.xyz) + u_xlat0.xyz;
					    u_xlat0.w = u_xlat0.y * _CameraUpScale;
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat2.xyz = (-u_xlat2.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xwz + u_xlat2.xyz;
					    u_xlat12 = u_xlat0.y + (-_WaterLevel);
					    u_xlat1.xy = u_xlat1.xy * vec2(u_xlat12) + u_xlat0.xz;
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat5 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat5;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat1.x = u_xlat1.x + u_xlat1.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat5 * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat12 = u_xlat12 / u_xlat1.x;
					    vs_TEXCOORD1.y = u_xlat12 + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat4 = _WaterLevel * 2.0 + (-u_xlat0.y);
					    u_xlat1 = vec4(u_xlat4) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[53];
						float _CameraUpScale;
						vec4 unused_0_2[2];
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_7;
						float _AAFactor;
						vec4 unused_0_9[4];
						vec4 _MainTex_ST;
						vec4 unused_0_11[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat4;
					float u_xlat5;
					float u_xlat12;
					void main()
					{
					    u_xlat0.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat0.xyz;
					    u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.y = unity_MatrixV[2].z;
					    u_xlat2.xz = (-u_xlat1.xy);
					    u_xlat1.xy = u_xlat1.xy / unity_MatrixV[1].zz;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat3.xyz = u_xlat0.yzx * u_xlat2.zxy;
					    u_xlat3.xyz = u_xlat2.yzx * u_xlat0.zxy + (-u_xlat3.xyz);
					    u_xlat0.xyz = u_xlat0.xyz * in_TEXCOORD1.yyy;
					    u_xlat2.xyz = u_xlat2.xyz * in_TANGENT0.www;
					    u_xlat12 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat12 = inversesqrt(u_xlat12);
					    u_xlat3.xyz = vec3(u_xlat12) * u_xlat3.xyz;
					    u_xlat0.xyz = in_TEXCOORD1.xxx * (-u_xlat3.xyz) + u_xlat0.xyz;
					    u_xlat0.w = u_xlat0.y * _CameraUpScale;
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat2.xyz = (-u_xlat2.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xwz + u_xlat2.xyz;
					    u_xlat12 = u_xlat0.y + (-_WaterLevel);
					    u_xlat1.xy = u_xlat1.xy * vec2(u_xlat12) + u_xlat0.xz;
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat5 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat5;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat1.x = u_xlat1.x + u_xlat1.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat5 * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat12 = u_xlat12 / u_xlat1.x;
					    vs_TEXCOORD1.y = u_xlat12 + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat4 = _WaterLevel * 2.0 + (-u_xlat0.y);
					    u_xlat1 = vec4(u_xlat4) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_AXIS_Z" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_AXIS_Z" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_AXIS_Z" "_LOWEND_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_AXIS_Z" "_LOWEND_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_AXIS_Z" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat3;
					float u_xlat6;
					void main()
					{
					    u_xlat0.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat0.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat6 = inversesqrt(u_xlat6);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat6 = inversesqrt(u_xlat6);
					    u_xlat1.xyz = vec3(u_xlat6) * u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * in_TEXCOORD1.xxx + u_xlat0.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = u_xlat0.y + (-_WaterLevel);
					    u_xlat1.x = unity_MatrixV[0].z / unity_MatrixV[1].z;
					    u_xlat1.y = unity_MatrixV[2].z / unity_MatrixV[1].z;
					    u_xlat1.xy = u_xlat1.xy * vec2(u_xlat6) + u_xlat0.xz;
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat3 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat3;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat1.x = u_xlat1.x + u_xlat1.x;
					    u_xlat3 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat3 * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat6 = u_xlat6 / u_xlat1.x;
					    vs_TEXCOORD1.y = u_xlat6 + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat2 = _WaterLevel * 2.0 + (-u_xlat0.y);
					    u_xlat1 = vec4(u_xlat2) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Z" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat3;
					float u_xlat6;
					void main()
					{
					    u_xlat0.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat0.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat6 = inversesqrt(u_xlat6);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat6 = inversesqrt(u_xlat6);
					    u_xlat1.xyz = vec3(u_xlat6) * u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * in_TEXCOORD1.xxx + u_xlat0.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = u_xlat0.y + (-_WaterLevel);
					    u_xlat1.x = unity_MatrixV[0].z / unity_MatrixV[1].z;
					    u_xlat1.y = unity_MatrixV[2].z / unity_MatrixV[1].z;
					    u_xlat1.xy = u_xlat1.xy * vec2(u_xlat6) + u_xlat0.xz;
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat3 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat3;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat1.x = u_xlat1.x + u_xlat1.x;
					    u_xlat3 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat3 * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat6 = u_xlat6 / u_xlat1.x;
					    vs_TEXCOORD1.y = u_xlat6 + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat2 = _WaterLevel * 2.0 + (-u_xlat0.y);
					    u_xlat1 = vec4(u_xlat2) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_AXIS_Z" "_LOWEND_ON" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat3;
					float u_xlat6;
					void main()
					{
					    u_xlat0.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat0.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat6 = inversesqrt(u_xlat6);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat6 = inversesqrt(u_xlat6);
					    u_xlat1.xyz = vec3(u_xlat6) * u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * in_TEXCOORD1.xxx + u_xlat0.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = u_xlat0.y + (-_WaterLevel);
					    u_xlat1.x = unity_MatrixV[0].z / unity_MatrixV[1].z;
					    u_xlat1.y = unity_MatrixV[2].z / unity_MatrixV[1].z;
					    u_xlat1.xy = u_xlat1.xy * vec2(u_xlat6) + u_xlat0.xz;
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat3 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat3;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat1.x = u_xlat1.x + u_xlat1.x;
					    u_xlat3 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat3 * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat6 = u_xlat6 / u_xlat1.x;
					    vs_TEXCOORD1.y = u_xlat6 + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat2 = _WaterLevel * 2.0 + (-u_xlat0.y);
					    u_xlat1 = vec4(u_xlat2) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Z" "_LOWEND_ON" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat3;
					float u_xlat6;
					void main()
					{
					    u_xlat0.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat0.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat6 = inversesqrt(u_xlat6);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat6 = inversesqrt(u_xlat6);
					    u_xlat1.xyz = vec3(u_xlat6) * u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * in_TEXCOORD1.xxx + u_xlat0.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = u_xlat0.y + (-_WaterLevel);
					    u_xlat1.x = unity_MatrixV[0].z / unity_MatrixV[1].z;
					    u_xlat1.y = unity_MatrixV[2].z / unity_MatrixV[1].z;
					    u_xlat1.xy = u_xlat1.xy * vec2(u_xlat6) + u_xlat0.xz;
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat3 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat3;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat1.x = u_xlat1.x + u_xlat1.x;
					    u_xlat3 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat3 * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat6 = u_xlat6 / u_xlat1.x;
					    vs_TEXCOORD1.y = u_xlat6 + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat2 = _WaterLevel * 2.0 + (-u_xlat0.y);
					    u_xlat1 = vec4(u_xlat2) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_X" "_LOOSE_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_X" "_LOOSE_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_LOWEND_ON" "_AXIS_X" "_LOOSE_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_LOWEND_ON" "_AXIS_X" "_LOOSE_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_X" "_LOOSE_ON" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat3.xyz;
					    u_xlat1.y = (-unity_MatrixV[0].z);
					    u_xlat1.z = (-unity_MatrixV[1].z);
					    u_xlat1.x = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat10 = dot((-u_xlat1.xyz), (-u_xlat1.xyz));
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat1.xyz = vec3(u_xlat10) * (-u_xlat1.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * in_TEXCOORD1.yyy;
					    u_xlat10 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_X" "_LOOSE_ON" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat3.xyz;
					    u_xlat1.y = (-unity_MatrixV[0].z);
					    u_xlat1.z = (-unity_MatrixV[1].z);
					    u_xlat1.x = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat10 = dot((-u_xlat1.xyz), (-u_xlat1.xyz));
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat1.xyz = vec3(u_xlat10) * (-u_xlat1.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * in_TEXCOORD1.yyy;
					    u_xlat10 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_LOWEND_ON" "_AXIS_X" "_LOOSE_ON" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat3.xyz;
					    u_xlat1.y = (-unity_MatrixV[0].z);
					    u_xlat1.z = (-unity_MatrixV[1].z);
					    u_xlat1.x = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat10 = dot((-u_xlat1.xyz), (-u_xlat1.xyz));
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat1.xyz = vec3(u_xlat10) * (-u_xlat1.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * in_TEXCOORD1.yyy;
					    u_xlat10 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" "_AXIS_X" "_LOOSE_ON" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat3.xyz;
					    u_xlat1.y = (-unity_MatrixV[0].z);
					    u_xlat1.z = (-unity_MatrixV[1].z);
					    u_xlat1.x = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat10 = dot((-u_xlat1.xyz), (-u_xlat1.xyz));
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat1.xyz = vec3(u_xlat10) * (-u_xlat1.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * in_TEXCOORD1.yyy;
					    u_xlat10 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat10);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Y" "_LOOSE_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Y" "_LOOSE_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Y" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Y" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Y" "_LOOSE_ON" }
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
						vec4 unused_0_0[53];
						float _CameraUpScale;
						vec4 unused_0_2[2];
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_7;
						float _AAFactor;
						vec4 unused_0_9[4];
						vec4 _MainTex_ST;
						vec4 unused_0_11[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat3.xyz;
					    u_xlat1.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat1.xxx;
					    u_xlat1.x = (-unity_MatrixV[0].z);
					    u_xlat1.y = (-unity_MatrixV[1].z);
					    u_xlat1.z = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = u_xlat1.xyz * in_TANGENT0.www;
					    u_xlat10 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat2.xyz = vec3(u_xlat10) * u_xlat2.xyz;
					    u_xlat2.xyz = in_TEXCOORD1.xxx * (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.w = u_xlat2.y * _CameraUpScale;
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Y" "_LOOSE_ON" }
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
						vec4 unused_0_0[53];
						float _CameraUpScale;
						vec4 unused_0_2[2];
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_7;
						float _AAFactor;
						vec4 unused_0_9[4];
						vec4 _MainTex_ST;
						vec4 unused_0_11[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat3.xyz;
					    u_xlat1.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat1.xxx;
					    u_xlat1.x = (-unity_MatrixV[0].z);
					    u_xlat1.y = (-unity_MatrixV[1].z);
					    u_xlat1.z = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = u_xlat1.xyz * in_TANGENT0.www;
					    u_xlat10 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat2.xyz = vec3(u_xlat10) * u_xlat2.xyz;
					    u_xlat2.xyz = in_TEXCOORD1.xxx * (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.w = u_xlat2.y * _CameraUpScale;
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Y" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[53];
						float _CameraUpScale;
						vec4 unused_0_2[2];
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_7;
						float _AAFactor;
						vec4 unused_0_9[4];
						vec4 _MainTex_ST;
						vec4 unused_0_11[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat3.xyz;
					    u_xlat1.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat1.xxx;
					    u_xlat1.x = (-unity_MatrixV[0].z);
					    u_xlat1.y = (-unity_MatrixV[1].z);
					    u_xlat1.z = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = u_xlat1.xyz * in_TANGENT0.www;
					    u_xlat10 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat2.xyz = vec3(u_xlat10) * u_xlat2.xyz;
					    u_xlat2.xyz = in_TEXCOORD1.xxx * (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.w = u_xlat2.y * _CameraUpScale;
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Y" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[53];
						float _CameraUpScale;
						vec4 unused_0_2[2];
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_7;
						float _AAFactor;
						vec4 unused_0_9[4];
						vec4 _MainTex_ST;
						vec4 unused_0_11[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_2_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_3_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_3_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat3.xyz;
					    u_xlat1.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat1.xxx;
					    u_xlat1.x = (-unity_MatrixV[0].z);
					    u_xlat1.y = (-unity_MatrixV[1].z);
					    u_xlat1.z = (-unity_MatrixV[2].z);
					    u_xlat2.xyz = u_xlat3.yzx * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat3.zxy + (-u_xlat2.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = u_xlat1.xyz * in_TANGENT0.www;
					    u_xlat10 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat10 = inversesqrt(u_xlat10);
					    u_xlat2.xyz = vec3(u_xlat10) * u_xlat2.xyz;
					    u_xlat2.xyz = in_TEXCOORD1.xxx * (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.w = u_xlat2.y * _CameraUpScale;
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat1.x = u_xlat3.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat3.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Z" "_LOOSE_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Z" "_LOOSE_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Z" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Z" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Z" "_LOOSE_ON" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					float u_xlat7;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat2.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat2.xyz;
					    u_xlat1.x = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat1.xyz;
					    u_xlat7 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat7 = inversesqrt(u_xlat7);
					    u_xlat1.xyz = vec3(u_xlat7) * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * in_TEXCOORD1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Z" "_LOOSE_ON" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					float u_xlat7;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat2.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat2.xyz;
					    u_xlat1.x = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat1.xyz;
					    u_xlat7 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat7 = inversesqrt(u_xlat7);
					    u_xlat1.xyz = vec3(u_xlat7) * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * in_TEXCOORD1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Z" "_LOWEND_ON" "_LOOSE_ON" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					float u_xlat7;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat2.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat2.xyz;
					    u_xlat1.x = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat1.xyz;
					    u_xlat7 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat7 = inversesqrt(u_xlat7);
					    u_xlat1.xyz = vec3(u_xlat7) * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * in_TEXCOORD1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Z" "_LOWEND_ON" "_LOOSE_ON" }
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
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_5;
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					float u_xlat7;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat2.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat2.xyz;
					    u_xlat1.x = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat1.x = inversesqrt(u_xlat1.x);
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * in_TEXCOORD1.yyy;
					    u_xlat1.xyz = in_NORMAL0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat1.xyz;
					    u_xlat7 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat7 = inversesqrt(u_xlat7);
					    u_xlat1.xyz = vec3(u_xlat7) * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * in_TEXCOORD1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * vec4(2.0, 2.0, 2.0, 1.0);
					    return;
					}"
				}
			}
			Program "fp" {
				SubProgram "d3d11 " {
					Keywords { "_AXIS_X" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					vec2 u_xlat3;
					vec2 u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.xy = u_xlat3.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat3.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat6.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat6.xy = (-u_xlat0.xy) + u_xlat6.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_X" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec2 u_xlat2;
					vec2 u_xlat4;
					vec2 u_xlat7;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat1.x);
					    u_xlat1.x = dFdy(u_xlat1.x);
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat4.x);
					    u_xlat1.x = u_xlat1.x * 0.100000001;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat1.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat4.xy = u_xlat4.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat1.xy = (-u_xlat1.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat7.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat7.xy = (-u_xlat1.xy) + u_xlat7.xy;
					    u_xlat1.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat1.xw = u_xlat1.xy / u_xlat7.xy;
					    u_xlat1.xw = clamp(u_xlat1.xw, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat0 = u_xlat1 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2.x<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_LOWEND_ON" "_AXIS_X" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_LOWEND_ON" "_AXIS_X" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_X" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					vec2 u_xlat3;
					vec2 u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.xy = u_xlat3.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat3.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat6.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat6.xy = (-u_xlat0.xy) + u_xlat6.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_X" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec2 u_xlat2;
					vec2 u_xlat4;
					vec2 u_xlat7;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat1.x);
					    u_xlat1.x = dFdy(u_xlat1.x);
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat4.x);
					    u_xlat1.x = u_xlat1.x * 0.100000001;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat1.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat4.xy = u_xlat4.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat1.xy = (-u_xlat1.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat7.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat7.xy = (-u_xlat1.xy) + u_xlat7.xy;
					    u_xlat1.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat1.xw = u_xlat1.xy / u_xlat7.xy;
					    u_xlat1.xw = clamp(u_xlat1.xw, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat0 = u_xlat1 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2.x<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_LOWEND_ON" "_AXIS_X" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" "_AXIS_X" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Y" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					vec2 u_xlat3;
					vec2 u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.xy = u_xlat3.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat3.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat6.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat6.xy = (-u_xlat0.xy) + u_xlat6.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Y" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec2 u_xlat2;
					vec2 u_xlat4;
					vec2 u_xlat7;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat1.x);
					    u_xlat1.x = dFdy(u_xlat1.x);
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat4.x);
					    u_xlat1.x = u_xlat1.x * 0.100000001;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat1.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat4.xy = u_xlat4.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat1.xy = (-u_xlat1.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat7.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat7.xy = (-u_xlat1.xy) + u_xlat7.xy;
					    u_xlat1.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat1.xw = u_xlat1.xy / u_xlat7.xy;
					    u_xlat1.xw = clamp(u_xlat1.xw, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat0 = u_xlat1 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2.x<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Y" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					vec2 u_xlat3;
					vec2 u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.xy = u_xlat3.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat3.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat6.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat6.xy = (-u_xlat0.xy) + u_xlat6.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Y" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec2 u_xlat2;
					vec2 u_xlat4;
					vec2 u_xlat7;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat1.x);
					    u_xlat1.x = dFdy(u_xlat1.x);
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat4.x);
					    u_xlat1.x = u_xlat1.x * 0.100000001;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat1.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat4.xy = u_xlat4.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat1.xy = (-u_xlat1.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat7.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat7.xy = (-u_xlat1.xy) + u_xlat7.xy;
					    u_xlat1.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat1.xw = u_xlat1.xy / u_xlat7.xy;
					    u_xlat1.xw = clamp(u_xlat1.xw, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat0 = u_xlat1 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2.x<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Z" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					vec2 u_xlat3;
					vec2 u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.xy = u_xlat3.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat3.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat6.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat6.xy = (-u_xlat0.xy) + u_xlat6.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Z" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec2 u_xlat2;
					vec2 u_xlat4;
					vec2 u_xlat7;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat1.x);
					    u_xlat1.x = dFdy(u_xlat1.x);
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat4.x);
					    u_xlat1.x = u_xlat1.x * 0.100000001;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat1.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat4.xy = u_xlat4.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat1.xy = (-u_xlat1.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat7.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat7.xy = (-u_xlat1.xy) + u_xlat7.xy;
					    u_xlat1.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat1.xw = u_xlat1.xy / u_xlat7.xy;
					    u_xlat1.xw = clamp(u_xlat1.xw, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat0 = u_xlat1 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2.x<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Z" "_LOWEND_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Z" "_LOWEND_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Z" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					vec2 u_xlat3;
					vec2 u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.xy = u_xlat3.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat3.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat6.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat6.xy = (-u_xlat0.xy) + u_xlat6.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Z" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec2 u_xlat2;
					vec2 u_xlat4;
					vec2 u_xlat7;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat1.x);
					    u_xlat1.x = dFdy(u_xlat1.x);
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat4.x);
					    u_xlat1.x = u_xlat1.x * 0.100000001;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat1.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat4.xy = u_xlat4.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat1.xy = (-u_xlat1.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat7.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat7.xy = (-u_xlat1.xy) + u_xlat7.xy;
					    u_xlat1.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat1.xw = u_xlat1.xy / u_xlat7.xy;
					    u_xlat1.xw = clamp(u_xlat1.xw, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat0 = u_xlat1 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2.x<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Z" "_LOWEND_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Z" "_LOWEND_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_AXIS_X" }
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
						vec4 unused_0_0[47];
						vec4 _MirrorColor2;
						vec4 unused_0_2[8];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					float u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					float u_xlat4;
					void main()
					{
					    u_xlat0 = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0);
					    u_xlat0 = dFdy(u_xlat0);
					    u_xlat0 = abs(u_xlat0) + abs(u_xlat2.x);
					    u_xlat0 = u_xlat0 * 0.100000001;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat2.x = u_xlat2.x * u_xlat0;
					    u_xlat2.x = u_xlat2.x * _LineWidth;
					    u_xlat2.x = u_xlat2.x * -0.800000012 + 0.5;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = (-u_xlat2.x) + 1.0;
					    u_xlat4 = u_xlat0 * u_xlat4 + u_xlat2.x;
					    u_xlat0 = (-u_xlat0) * u_xlat2.x + u_xlat2.x;
					    u_xlat0 = max(u_xlat0, 0.0);
					    u_xlat2.x = min(u_xlat4, 1.0);
					    u_xlat2.x = (-u_xlat0) + u_xlat2.x;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0 = (-u_xlat0) + u_xlat1.w;
					    u_xlat0 = u_xlat0 / u_xlat2.x;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat0 = u_xlat0 * _Color.w;
					    u_xlat0 = u_xlat0 * vs_COLOR0.w;
					    u_xlat2.x = vs_TEXCOORD1.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = u_xlat0 * u_xlat2.x + -0.00999999978;
					    u_xlat0 = u_xlat2.x * u_xlat0;
					    SV_Target0.w = u_xlat0;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat4<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = vec3(u_xlat0) * u_xlat1.xyz + u_xlat2.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_AXIS_X" }
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
						vec4 unused_0_0[47];
						vec4 _MirrorColor2;
						vec4 unused_0_2[8];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					float u_xlat4;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat2.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat2.x = u_xlat2.x * u_xlat0.x;
					    u_xlat2.x = u_xlat2.x * _LineWidth;
					    u_xlat2.x = u_xlat2.x * -0.800000012 + 0.5;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = (-u_xlat2.x) + 1.0;
					    u_xlat4 = u_xlat0.x * u_xlat4 + u_xlat2.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat2.x + u_xlat2.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat2.x = min(u_xlat4, 1.0);
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat0.w;
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * _Color.w;
					    u_xlat0.x = u_xlat0.x * vs_COLOR0.w;
					    u_xlat2.x = vs_TEXCOORD1.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = u_xlat0.x * u_xlat2.x + -0.00999999978;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat4<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_LOWEND_ON" "_AXIS_X" }
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
						vec4 unused_0_0[47];
						vec4 _MirrorColor2;
						vec4 unused_0_2[19];
					};
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0 = _MirrorColor2 * vec4(0.800000012, 0.800000012, 0.800000012, 0.5);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" "_AXIS_X" }
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
						vec4 unused_0_0[47];
						vec4 _MirrorColor2;
						vec4 unused_0_2[19];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0 = _MirrorColor2 * vec4(0.800000012, 0.800000012, 0.800000012, 0.5);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_AXIS_X" }
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
						vec4 unused_0_2[37];
						vec4 _MirrorColor2;
						vec4 unused_0_4[2];
						vec4 _CloudCoverage;
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12[4];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec2 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.x = u_xlat3.x * -0.800000012 + 0.5;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat6 = (-u_xlat3.x) + 1.0;
					    u_xlat6 = u_xlat0.x * u_xlat6 + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat3.x + u_xlat3.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat6, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = (-u_xlat0.x) + u_xlat1.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * _Color.w;
					    u_xlat0.x = u_xlat0.x * vs_COLOR0.w;
					    u_xlat3.xy = vs_TEXCOORD1.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat1.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = dot(u_xlat0.xyw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyw = u_xlat0.xyw + (-u_xlat1.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat0.xyw + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyw;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyw) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat3.yyy * u_xlat1.xyz + u_xlat0.xyw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_X" }
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
						vec4 unused_0_2[37];
						vec4 _MirrorColor2;
						vec4 unused_0_4[2];
						vec4 _CloudCoverage;
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12[4];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					vec2 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.x = u_xlat3.x * -0.800000012 + 0.5;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat6 = (-u_xlat3.x) + 1.0;
					    u_xlat6 = u_xlat0.x * u_xlat6 + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat3.x + u_xlat3.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat6, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat0.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * _Color.w;
					    u_xlat0.x = u_xlat0.x * vs_COLOR0.w;
					    u_xlat3.xy = vs_TEXCOORD1.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat1.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = dot(u_xlat0.xyw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyw = u_xlat0.xyw + (-u_xlat1.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat0.xyw + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyw;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyw) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat3.yyy * u_xlat1.xyz + u_xlat0.xyw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_LOWEND_ON" "_AXIS_X" }
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
						vec4 unused_0_2[37];
						vec4 _MirrorColor2;
						vec4 unused_0_4[2];
						vec4 _CloudCoverage;
						vec4 unused_0_6[16];
					};
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0.xyz = _MirrorColor2.xyz * vec3(0.800000012, 0.800000012, 0.800000012);
					    u_xlat0.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    SV_Target0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = _MirrorColor2.w * 0.5;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" "_AXIS_X" }
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
						vec4 unused_0_2[37];
						vec4 _MirrorColor2;
						vec4 unused_0_4[2];
						vec4 _CloudCoverage;
						vec4 unused_0_6[16];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = _MirrorColor2.xyz * vec3(0.800000012, 0.800000012, 0.800000012);
					    u_xlat0.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    SV_Target0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = _MirrorColor2.w * 0.5;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_AXIS_Y" }
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
						vec4 unused_0_0[47];
						vec4 _MirrorColor2;
						vec4 unused_0_2[8];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					float u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					float u_xlat4;
					void main()
					{
					    u_xlat0 = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0);
					    u_xlat0 = dFdy(u_xlat0);
					    u_xlat0 = abs(u_xlat0) + abs(u_xlat2.x);
					    u_xlat0 = u_xlat0 * 0.100000001;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat2.x = u_xlat2.x * u_xlat0;
					    u_xlat2.x = u_xlat2.x * _LineWidth;
					    u_xlat2.x = u_xlat2.x * -0.800000012 + 0.5;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = (-u_xlat2.x) + 1.0;
					    u_xlat4 = u_xlat0 * u_xlat4 + u_xlat2.x;
					    u_xlat0 = (-u_xlat0) * u_xlat2.x + u_xlat2.x;
					    u_xlat0 = max(u_xlat0, 0.0);
					    u_xlat2.x = min(u_xlat4, 1.0);
					    u_xlat2.x = (-u_xlat0) + u_xlat2.x;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0 = (-u_xlat0) + u_xlat1.w;
					    u_xlat0 = u_xlat0 / u_xlat2.x;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat0 = u_xlat0 * _Color.w;
					    u_xlat0 = u_xlat0 * vs_COLOR0.w;
					    u_xlat2.x = vs_TEXCOORD1.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = u_xlat0 * u_xlat2.x + -0.00999999978;
					    u_xlat0 = u_xlat2.x * u_xlat0;
					    SV_Target0.w = u_xlat0;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat4<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = vec3(u_xlat0) * u_xlat1.xyz + u_xlat2.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_AXIS_Y" }
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
						vec4 unused_0_0[47];
						vec4 _MirrorColor2;
						vec4 unused_0_2[8];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					float u_xlat4;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat2.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat2.x = u_xlat2.x * u_xlat0.x;
					    u_xlat2.x = u_xlat2.x * _LineWidth;
					    u_xlat2.x = u_xlat2.x * -0.800000012 + 0.5;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = (-u_xlat2.x) + 1.0;
					    u_xlat4 = u_xlat0.x * u_xlat4 + u_xlat2.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat2.x + u_xlat2.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat2.x = min(u_xlat4, 1.0);
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat0.w;
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * _Color.w;
					    u_xlat0.x = u_xlat0.x * vs_COLOR0.w;
					    u_xlat2.x = vs_TEXCOORD1.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = u_xlat0.x * u_xlat2.x + -0.00999999978;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat4<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[47];
						vec4 _MirrorColor2;
						vec4 unused_0_2[19];
					};
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0 = _MirrorColor2 * vec4(0.800000012, 0.800000012, 0.800000012, 0.5);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_0[47];
						vec4 _MirrorColor2;
						vec4 unused_0_2[19];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0 = _MirrorColor2 * vec4(0.800000012, 0.800000012, 0.800000012, 0.5);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_AXIS_Y" }
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
						vec4 unused_0_2[37];
						vec4 _MirrorColor2;
						vec4 unused_0_4[2];
						vec4 _CloudCoverage;
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12[4];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec2 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.x = u_xlat3.x * -0.800000012 + 0.5;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat6 = (-u_xlat3.x) + 1.0;
					    u_xlat6 = u_xlat0.x * u_xlat6 + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat3.x + u_xlat3.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat6, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = (-u_xlat0.x) + u_xlat1.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * _Color.w;
					    u_xlat0.x = u_xlat0.x * vs_COLOR0.w;
					    u_xlat3.xy = vs_TEXCOORD1.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat1.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = dot(u_xlat0.xyw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyw = u_xlat0.xyw + (-u_xlat1.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat0.xyw + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyw;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyw) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat3.yyy * u_xlat1.xyz + u_xlat0.xyw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Y" }
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
						vec4 unused_0_2[37];
						vec4 _MirrorColor2;
						vec4 unused_0_4[2];
						vec4 _CloudCoverage;
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12[4];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					vec2 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.x = u_xlat3.x * -0.800000012 + 0.5;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat6 = (-u_xlat3.x) + 1.0;
					    u_xlat6 = u_xlat0.x * u_xlat6 + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat3.x + u_xlat3.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat6, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat0.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * _Color.w;
					    u_xlat0.x = u_xlat0.x * vs_COLOR0.w;
					    u_xlat3.xy = vs_TEXCOORD1.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat1.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = dot(u_xlat0.xyw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyw = u_xlat0.xyw + (-u_xlat1.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat0.xyw + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyw;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyw) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat3.yyy * u_xlat1.xyz + u_xlat0.xyw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_2[37];
						vec4 _MirrorColor2;
						vec4 unused_0_4[2];
						vec4 _CloudCoverage;
						vec4 unused_0_6[16];
					};
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0.xyz = _MirrorColor2.xyz * vec3(0.800000012, 0.800000012, 0.800000012);
					    u_xlat0.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    SV_Target0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = _MirrorColor2.w * 0.5;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Y" "_LOWEND_ON" }
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
						vec4 unused_0_2[37];
						vec4 _MirrorColor2;
						vec4 unused_0_4[2];
						vec4 _CloudCoverage;
						vec4 unused_0_6[16];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = _MirrorColor2.xyz * vec3(0.800000012, 0.800000012, 0.800000012);
					    u_xlat0.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    SV_Target0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = _MirrorColor2.w * 0.5;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_AXIS_Z" }
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
						vec4 unused_0_0[47];
						vec4 _MirrorColor2;
						vec4 unused_0_2[8];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					float u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					float u_xlat4;
					void main()
					{
					    u_xlat0 = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0);
					    u_xlat0 = dFdy(u_xlat0);
					    u_xlat0 = abs(u_xlat0) + abs(u_xlat2.x);
					    u_xlat0 = u_xlat0 * 0.100000001;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat2.x = u_xlat2.x * u_xlat0;
					    u_xlat2.x = u_xlat2.x * _LineWidth;
					    u_xlat2.x = u_xlat2.x * -0.800000012 + 0.5;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = (-u_xlat2.x) + 1.0;
					    u_xlat4 = u_xlat0 * u_xlat4 + u_xlat2.x;
					    u_xlat0 = (-u_xlat0) * u_xlat2.x + u_xlat2.x;
					    u_xlat0 = max(u_xlat0, 0.0);
					    u_xlat2.x = min(u_xlat4, 1.0);
					    u_xlat2.x = (-u_xlat0) + u_xlat2.x;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0 = (-u_xlat0) + u_xlat1.w;
					    u_xlat0 = u_xlat0 / u_xlat2.x;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat0 = u_xlat0 * _Color.w;
					    u_xlat0 = u_xlat0 * vs_COLOR0.w;
					    u_xlat2.x = vs_TEXCOORD1.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = u_xlat0 * u_xlat2.x + -0.00999999978;
					    u_xlat0 = u_xlat2.x * u_xlat0;
					    SV_Target0.w = u_xlat0;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat4<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = vec3(u_xlat0) * u_xlat1.xyz + u_xlat2.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_AXIS_Z" }
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
						vec4 unused_0_0[47];
						vec4 _MirrorColor2;
						vec4 unused_0_2[8];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					float u_xlat4;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat2.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat2.x = u_xlat2.x * u_xlat0.x;
					    u_xlat2.x = u_xlat2.x * _LineWidth;
					    u_xlat2.x = u_xlat2.x * -0.800000012 + 0.5;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = (-u_xlat2.x) + 1.0;
					    u_xlat4 = u_xlat0.x * u_xlat4 + u_xlat2.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat2.x + u_xlat2.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat2.x = min(u_xlat4, 1.0);
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat0.w;
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * _Color.w;
					    u_xlat0.x = u_xlat0.x * vs_COLOR0.w;
					    u_xlat2.x = vs_TEXCOORD1.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = u_xlat0.x * u_xlat2.x + -0.00999999978;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat4<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_AXIS_Z" "_LOWEND_ON" }
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
						vec4 unused_0_0[47];
						vec4 _MirrorColor2;
						vec4 unused_0_2[19];
					};
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0 = _MirrorColor2 * vec4(0.800000012, 0.800000012, 0.800000012, 0.5);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_AXIS_Z" "_LOWEND_ON" }
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
						vec4 unused_0_0[47];
						vec4 _MirrorColor2;
						vec4 unused_0_2[19];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0 = _MirrorColor2 * vec4(0.800000012, 0.800000012, 0.800000012, 0.5);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_AXIS_Z" }
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
						vec4 unused_0_2[37];
						vec4 _MirrorColor2;
						vec4 unused_0_4[2];
						vec4 _CloudCoverage;
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12[4];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec2 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.x = u_xlat3.x * -0.800000012 + 0.5;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat6 = (-u_xlat3.x) + 1.0;
					    u_xlat6 = u_xlat0.x * u_xlat6 + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat3.x + u_xlat3.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat6, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = (-u_xlat0.x) + u_xlat1.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * _Color.w;
					    u_xlat0.x = u_xlat0.x * vs_COLOR0.w;
					    u_xlat3.xy = vs_TEXCOORD1.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat1.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = dot(u_xlat0.xyw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyw = u_xlat0.xyw + (-u_xlat1.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat0.xyw + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyw;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyw) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat3.yyy * u_xlat1.xyz + u_xlat0.xyw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Z" }
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
						vec4 unused_0_2[37];
						vec4 _MirrorColor2;
						vec4 unused_0_4[2];
						vec4 _CloudCoverage;
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12[4];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					vec2 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.x = u_xlat3.x * -0.800000012 + 0.5;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat6 = (-u_xlat3.x) + 1.0;
					    u_xlat6 = u_xlat0.x * u_xlat6 + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat3.x + u_xlat3.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat6, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat0.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * _Color.w;
					    u_xlat0.x = u_xlat0.x * vs_COLOR0.w;
					    u_xlat3.xy = vs_TEXCOORD1.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat1.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = dot(u_xlat0.xyw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyw = u_xlat0.xyw + (-u_xlat1.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat0.xyw + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyw;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyw) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat3.yyy * u_xlat1.xyz + u_xlat0.xyw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_AXIS_Z" "_LOWEND_ON" }
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
						vec4 unused_0_2[37];
						vec4 _MirrorColor2;
						vec4 unused_0_4[2];
						vec4 _CloudCoverage;
						vec4 unused_0_6[16];
					};
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0.xyz = _MirrorColor2.xyz * vec3(0.800000012, 0.800000012, 0.800000012);
					    u_xlat0.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    SV_Target0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = _MirrorColor2.w * 0.5;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Z" "_LOWEND_ON" }
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
						vec4 unused_0_2[37];
						vec4 _MirrorColor2;
						vec4 unused_0_4[2];
						vec4 _CloudCoverage;
						vec4 unused_0_6[16];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = _MirrorColor2.xyz * vec3(0.800000012, 0.800000012, 0.800000012);
					    u_xlat0.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    SV_Target0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = _MirrorColor2.w * 0.5;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_X" "_LOOSE_ON" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					vec2 u_xlat3;
					vec2 u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.xy = u_xlat3.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat3.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat6.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat6.xy = (-u_xlat0.xy) + u_xlat6.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_X" "_LOOSE_ON" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec2 u_xlat2;
					vec2 u_xlat4;
					vec2 u_xlat7;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat1.x);
					    u_xlat1.x = dFdy(u_xlat1.x);
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat4.x);
					    u_xlat1.x = u_xlat1.x * 0.100000001;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat1.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat4.xy = u_xlat4.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat1.xy = (-u_xlat1.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat7.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat7.xy = (-u_xlat1.xy) + u_xlat7.xy;
					    u_xlat1.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat1.xw = u_xlat1.xy / u_xlat7.xy;
					    u_xlat1.xw = clamp(u_xlat1.xw, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat0 = u_xlat1 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2.x<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_LOWEND_ON" "_AXIS_X" "_LOOSE_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_LOWEND_ON" "_AXIS_X" "_LOOSE_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_X" "_LOOSE_ON" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					vec2 u_xlat3;
					vec2 u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.xy = u_xlat3.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat3.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat6.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat6.xy = (-u_xlat0.xy) + u_xlat6.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_X" "_LOOSE_ON" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec2 u_xlat2;
					vec2 u_xlat4;
					vec2 u_xlat7;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat1.x);
					    u_xlat1.x = dFdy(u_xlat1.x);
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat4.x);
					    u_xlat1.x = u_xlat1.x * 0.100000001;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat1.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat4.xy = u_xlat4.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat1.xy = (-u_xlat1.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat7.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat7.xy = (-u_xlat1.xy) + u_xlat7.xy;
					    u_xlat1.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat1.xw = u_xlat1.xy / u_xlat7.xy;
					    u_xlat1.xw = clamp(u_xlat1.xw, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat0 = u_xlat1 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2.x<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_LOWEND_ON" "_AXIS_X" "_LOOSE_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" "_AXIS_X" "_LOOSE_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Y" "_LOOSE_ON" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					vec2 u_xlat3;
					vec2 u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.xy = u_xlat3.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat3.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat6.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat6.xy = (-u_xlat0.xy) + u_xlat6.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Y" "_LOOSE_ON" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec2 u_xlat2;
					vec2 u_xlat4;
					vec2 u_xlat7;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat1.x);
					    u_xlat1.x = dFdy(u_xlat1.x);
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat4.x);
					    u_xlat1.x = u_xlat1.x * 0.100000001;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat1.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat4.xy = u_xlat4.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat1.xy = (-u_xlat1.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat7.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat7.xy = (-u_xlat1.xy) + u_xlat7.xy;
					    u_xlat1.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat1.xw = u_xlat1.xy / u_xlat7.xy;
					    u_xlat1.xw = clamp(u_xlat1.xw, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat0 = u_xlat1 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2.x<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Y" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Y" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Y" "_LOOSE_ON" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					vec2 u_xlat3;
					vec2 u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.xy = u_xlat3.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat3.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat6.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat6.xy = (-u_xlat0.xy) + u_xlat6.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Y" "_LOOSE_ON" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec2 u_xlat2;
					vec2 u_xlat4;
					vec2 u_xlat7;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat1.x);
					    u_xlat1.x = dFdy(u_xlat1.x);
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat4.x);
					    u_xlat1.x = u_xlat1.x * 0.100000001;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat1.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat4.xy = u_xlat4.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat1.xy = (-u_xlat1.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat7.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat7.xy = (-u_xlat1.xy) + u_xlat7.xy;
					    u_xlat1.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat1.xw = u_xlat1.xy / u_xlat7.xy;
					    u_xlat1.xw = clamp(u_xlat1.xw, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat0 = u_xlat1 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2.x<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Y" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Y" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Z" "_LOOSE_ON" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					vec2 u_xlat3;
					vec2 u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.xy = u_xlat3.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat3.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat6.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat6.xy = (-u_xlat0.xy) + u_xlat6.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Z" "_LOOSE_ON" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec2 u_xlat2;
					vec2 u_xlat4;
					vec2 u_xlat7;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat1.x);
					    u_xlat1.x = dFdy(u_xlat1.x);
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat4.x);
					    u_xlat1.x = u_xlat1.x * 0.100000001;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat1.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat4.xy = u_xlat4.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat1.xy = (-u_xlat1.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat7.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat7.xy = (-u_xlat1.xy) + u_xlat7.xy;
					    u_xlat1.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat1.xw = u_xlat1.xy / u_xlat7.xy;
					    u_xlat1.xw = clamp(u_xlat1.xw, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat0 = u_xlat1 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2.x<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AXIS_Z" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AXIS_Z" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Z" "_LOOSE_ON" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					vec2 u_xlat3;
					vec2 u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat3.xy = u_xlat3.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat3.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat6.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat6.xy = (-u_xlat0.xy) + u_xlat6.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Z" "_LOOSE_ON" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec2 u_xlat2;
					vec2 u_xlat4;
					vec2 u_xlat7;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat1.x);
					    u_xlat1.x = dFdy(u_xlat1.x);
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat4.x);
					    u_xlat1.x = u_xlat1.x * 0.100000001;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat1.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat4.xy = u_xlat4.xx * vec2(0.800000012, -0.800000012) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat1.xy = (-u_xlat1.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat7.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat7.xy = (-u_xlat1.xy) + u_xlat7.xy;
					    u_xlat1.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat1.xw = u_xlat1.xy / u_xlat7.xy;
					    u_xlat1.xw = clamp(u_xlat1.xw, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat0 = u_xlat1 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2.x<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AXIS_Z" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AXIS_Z" "_LOWEND_ON" "_LOOSE_ON" }
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
						vec4 unused_0_0[66];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = u_xlat0.www * u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat9 = vs_TEXCOORD1.y;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat2 = u_xlat1.w * u_xlat9 + -0.00999999978;
					    u_xlat9 = u_xlat9 * u_xlat1.w;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb9 = u_xlat2<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    return;
					}"
				}
			}
		}
	}
}