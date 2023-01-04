Shader "Unlit/Waterfall" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		_SelectionColor ("Selection Color", Vector) = (1,1,1,1)
		[Toggle] _Selected ("Selected", Float) = 0
		[Toggle] _Mirror ("Mirror", Float) = 0
	}
	SubShader {
		LOD 100
		Tags { "RenderType" = "Opaque" }
		Pass {
			LOD 100
			Tags { "RenderType" = "Opaque" }
			GpuProgramID 56605
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
						vec4 unused_0_0[44];
						vec4 _FoamColor;
						vec4 unused_0_2[2];
						vec4 _MirrorColor2;
						vec4 unused_0_4[8];
						float _WaterLevel;
						vec4 unused_0_6[7];
						vec4 _MainTex_ST;
						vec4 unused_0_8;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = in_TEXCOORD0.y * 0.5;
					    u_xlat3.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = max(u_xlat3.x, u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + _Time.y;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    vs_TEXCOORD0.y = u_xlat0.x;
					    u_xlat3.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    vs_TEXCOORD0.x = u_xlat3.x * 1.20000005;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat0.x = u_xlat0.x * 13.3392019 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat3.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat3.x = inversesqrt(u_xlat3.x);
					    u_xlat3.xyz = u_xlat3.xxx * in_NORMAL0.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * in_COLOR0.xxx;
					    u_xlat0.x = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = (-u_xlat3.y) * 2.0 + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * 0.300000072 + 0.899999976;
					    vs_COLOR1 = vec4(u_xlat6) * _MirrorColor2;
					    u_xlat0.xyz = u_xlat0.xyw * in_COLOR0.xxx;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat0.xyz;
					    u_xlat2.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat2.xy = u_xlat1.xz + u_xlat2.xy;
					    u_xlat9 = cos(u_xlat2.x);
					    u_xlat2.x = sin(u_xlat2.y);
					    u_xlat9 = u_xlat9 + u_xlat2.x;
					    u_xlat9 = u_xlat9 * in_COLOR0.x;
					    u_xlat9 = u_xlat9 * 0.0199999996;
					    u_xlat0.xyz = vec3(u_xlat9) * in_TANGENT0.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat2 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat2;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat2;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = _FoamColor * vec4(1.10000002, 1.10000002, 1.10000002, 1.10000002);
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" }
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
						vec4 unused_0_0[44];
						vec4 _FoamColor;
						vec4 unused_0_2[2];
						vec4 _MirrorColor2;
						vec4 unused_0_4[8];
						float _WaterLevel;
						vec4 unused_0_6[7];
						vec4 _MainTex_ST;
						vec4 unused_0_8;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = in_TEXCOORD0.y * 0.5;
					    u_xlat3.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = max(u_xlat3.x, u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + _Time.y;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    vs_TEXCOORD0.y = u_xlat0.x;
					    u_xlat3.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    vs_TEXCOORD0.x = u_xlat3.x * 1.20000005;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat0.x = u_xlat0.x * 13.3392019 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat3.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat3.x = inversesqrt(u_xlat3.x);
					    u_xlat3.xyz = u_xlat3.xxx * in_NORMAL0.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * in_COLOR0.xxx;
					    u_xlat0.x = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = (-u_xlat3.y) * 2.0 + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * 0.300000072 + 0.899999976;
					    vs_COLOR1 = vec4(u_xlat6) * _MirrorColor2;
					    u_xlat0.xyz = u_xlat0.xyw * in_COLOR0.xxx;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat0.xyz;
					    u_xlat2.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat2.xy = u_xlat1.xz + u_xlat2.xy;
					    u_xlat9 = cos(u_xlat2.x);
					    u_xlat2.x = sin(u_xlat2.y);
					    u_xlat9 = u_xlat9 + u_xlat2.x;
					    u_xlat9 = u_xlat9 * in_COLOR0.x;
					    u_xlat9 = u_xlat9 * 0.0199999996;
					    u_xlat0.xyz = vec3(u_xlat9) * in_TANGENT0.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat2 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat2;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat2;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = _FoamColor * vec4(1.10000002, 1.10000002, 1.10000002, 1.10000002);
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_SELECTED_ON" }
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
						vec4 unused_0_0[44];
						vec4 _FoamColor;
						vec4 unused_0_2[2];
						vec4 _MirrorColor2;
						vec4 unused_0_4[8];
						float _WaterLevel;
						vec4 unused_0_6[7];
						vec4 _MainTex_ST;
						vec4 unused_0_8;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = in_TEXCOORD0.y * 0.5;
					    u_xlat3.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = max(u_xlat3.x, u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + _Time.y;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    vs_TEXCOORD0.y = u_xlat0.x;
					    u_xlat3.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    vs_TEXCOORD0.x = u_xlat3.x * 1.20000005;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat0.x = u_xlat0.x * 13.3392019 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat3.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat3.x = inversesqrt(u_xlat3.x);
					    u_xlat3.xyz = u_xlat3.xxx * in_NORMAL0.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * in_COLOR0.xxx;
					    u_xlat0.x = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = (-u_xlat3.y) * 2.0 + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * 0.300000072 + 0.899999976;
					    vs_COLOR1 = vec4(u_xlat6) * _MirrorColor2;
					    u_xlat0.xyz = u_xlat0.xyw * in_COLOR0.xxx;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat0.xyz;
					    u_xlat2.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat2.xy = u_xlat1.xz + u_xlat2.xy;
					    u_xlat9 = cos(u_xlat2.x);
					    u_xlat2.x = sin(u_xlat2.y);
					    u_xlat9 = u_xlat9 + u_xlat2.x;
					    u_xlat9 = u_xlat9 * in_COLOR0.x;
					    u_xlat9 = u_xlat9 * 0.0199999996;
					    u_xlat0.xyz = vec3(u_xlat9) * in_TANGENT0.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat2 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat2;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat2;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = _FoamColor * vec4(1.10000002, 1.10000002, 1.10000002, 1.10000002);
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" }
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
						vec4 unused_0_0[44];
						vec4 _FoamColor;
						vec4 unused_0_2[2];
						vec4 _MirrorColor2;
						vec4 unused_0_4[8];
						float _WaterLevel;
						vec4 unused_0_6[7];
						vec4 _MainTex_ST;
						vec4 unused_0_8;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat5;
					float u_xlat6;
					void main()
					{
					    u_xlat0.x = in_TEXCOORD0.y * 0.5;
					    u_xlat3.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = max(u_xlat3.x, u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + _Time.y;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    vs_TEXCOORD0.y = u_xlat0.x;
					    u_xlat3.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    vs_TEXCOORD0.x = u_xlat3.x * 1.20000005;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat0.x = u_xlat0.x * 13.3392019 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat3.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat3.x = inversesqrt(u_xlat3.x);
					    u_xlat3.xyz = u_xlat3.xxx * in_NORMAL0.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * in_COLOR0.xxx;
					    u_xlat0.x = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = (-u_xlat3.y) * 2.0 + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * 0.300000072 + 0.899999976;
					    u_xlat0.xyw = u_xlat0.xyw * in_COLOR0.xxx;
					    u_xlat0.xyw = u_xlat0.xyw * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat0.xyw = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat0.xyw;
					    u_xlat2.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat2.xy = u_xlat1.xz + u_xlat2.xy;
					    u_xlat2.x = cos(u_xlat2.x);
					    u_xlat5 = sin(u_xlat2.y);
					    u_xlat2.x = u_xlat5 + u_xlat2.x;
					    u_xlat2.x = u_xlat2.x * in_COLOR0.x;
					    u_xlat2.x = u_xlat2.x * 0.0199999996;
					    u_xlat0.xyw = u_xlat2.xxx * in_TANGENT0.xyz + u_xlat0.xyw;
					    u_xlat0.xyw = u_xlat0.xyw + u_xlat1.xyz;
					    u_xlat2 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat0.wwww + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    vs_COLOR0.w = _FoamColor.w * 1.10000002;
					    u_xlat0.x = in_TANGENT0.y * 0.100000001 + 1.0;
					    u_xlat0.x = u_xlat0.x * 0.0800000057;
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat0.xxx;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    u_xlat2.w = u_xlat6 * _MirrorColor2.w;
					    u_xlat0 = _MirrorColor2 * vec4(u_xlat6) + (-u_xlat2);
					    u_xlat1.x = in_POSITION0.y + _WaterLevel;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat1.x = u_xlat1.x * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.800000012;
					    vs_COLOR1 = u_xlat1.xxxx * u_xlat0 + u_xlat2;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[44];
						vec4 _FoamColor;
						vec4 unused_0_2[2];
						vec4 _MirrorColor2;
						vec4 unused_0_4[8];
						float _WaterLevel;
						vec4 unused_0_6[7];
						vec4 _MainTex_ST;
						vec4 unused_0_8;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat5;
					float u_xlat6;
					void main()
					{
					    u_xlat0.x = in_TEXCOORD0.y * 0.5;
					    u_xlat3.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = max(u_xlat3.x, u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + _Time.y;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    vs_TEXCOORD0.y = u_xlat0.x;
					    u_xlat3.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    vs_TEXCOORD0.x = u_xlat3.x * 1.20000005;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat0.x = u_xlat0.x * 13.3392019 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat3.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat3.x = inversesqrt(u_xlat3.x);
					    u_xlat3.xyz = u_xlat3.xxx * in_NORMAL0.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * in_COLOR0.xxx;
					    u_xlat0.x = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = (-u_xlat3.y) * 2.0 + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * 0.300000072 + 0.899999976;
					    u_xlat0.xyw = u_xlat0.xyw * in_COLOR0.xxx;
					    u_xlat0.xyw = u_xlat0.xyw * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat0.xyw = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat0.xyw;
					    u_xlat2.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat2.xy = u_xlat1.xz + u_xlat2.xy;
					    u_xlat2.x = cos(u_xlat2.x);
					    u_xlat5 = sin(u_xlat2.y);
					    u_xlat2.x = u_xlat5 + u_xlat2.x;
					    u_xlat2.x = u_xlat2.x * in_COLOR0.x;
					    u_xlat2.x = u_xlat2.x * 0.0199999996;
					    u_xlat0.xyw = u_xlat2.xxx * in_TANGENT0.xyz + u_xlat0.xyw;
					    u_xlat0.xyw = u_xlat0.xyw + u_xlat1.xyz;
					    u_xlat2 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat0.wwww + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    vs_COLOR0.w = _FoamColor.w * 1.10000002;
					    u_xlat0.x = in_TANGENT0.y * 0.100000001 + 1.0;
					    u_xlat0.x = u_xlat0.x * 0.0800000057;
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat0.xxx;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    u_xlat2.w = u_xlat6 * _MirrorColor2.w;
					    u_xlat0 = _MirrorColor2 * vec4(u_xlat6) + (-u_xlat2);
					    u_xlat1.x = in_POSITION0.y + _WaterLevel;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat1.x = u_xlat1.x * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.800000012;
					    vs_COLOR1 = u_xlat1.xxxx * u_xlat0 + u_xlat2;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_SELECTED_ON" }
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
						vec4 unused_0_0[44];
						vec4 _FoamColor;
						vec4 unused_0_2[2];
						vec4 _MirrorColor2;
						vec4 unused_0_4[8];
						float _WaterLevel;
						vec4 unused_0_6[7];
						vec4 _MainTex_ST;
						vec4 unused_0_8;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat5;
					float u_xlat6;
					void main()
					{
					    u_xlat0.x = in_TEXCOORD0.y * 0.5;
					    u_xlat3.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = max(u_xlat3.x, u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + _Time.y;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    vs_TEXCOORD0.y = u_xlat0.x;
					    u_xlat3.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    vs_TEXCOORD0.x = u_xlat3.x * 1.20000005;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat0.x = u_xlat0.x * 13.3392019 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat3.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat3.x = inversesqrt(u_xlat3.x);
					    u_xlat3.xyz = u_xlat3.xxx * in_NORMAL0.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * in_COLOR0.xxx;
					    u_xlat0.x = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = (-u_xlat3.y) * 2.0 + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * 0.300000072 + 0.899999976;
					    u_xlat0.xyw = u_xlat0.xyw * in_COLOR0.xxx;
					    u_xlat0.xyw = u_xlat0.xyw * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat0.xyw = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat0.xyw;
					    u_xlat2.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat2.xy = u_xlat1.xz + u_xlat2.xy;
					    u_xlat2.x = cos(u_xlat2.x);
					    u_xlat5 = sin(u_xlat2.y);
					    u_xlat2.x = u_xlat5 + u_xlat2.x;
					    u_xlat2.x = u_xlat2.x * in_COLOR0.x;
					    u_xlat2.x = u_xlat2.x * 0.0199999996;
					    u_xlat0.xyw = u_xlat2.xxx * in_TANGENT0.xyz + u_xlat0.xyw;
					    u_xlat0.xyw = u_xlat0.xyw + u_xlat1.xyz;
					    u_xlat2 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat0.wwww + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    vs_COLOR0.w = _FoamColor.w * 1.10000002;
					    u_xlat0.x = in_TANGENT0.y * 0.100000001 + 1.0;
					    u_xlat0.x = u_xlat0.x * 0.0800000057;
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat0.xxx;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    u_xlat2.w = u_xlat6 * _MirrorColor2.w;
					    u_xlat0 = _MirrorColor2 * vec4(u_xlat6) + (-u_xlat2);
					    u_xlat1.x = in_POSITION0.y + _WaterLevel;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat1.x = u_xlat1.x * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.800000012;
					    vs_COLOR1 = u_xlat1.xxxx * u_xlat0 + u_xlat2;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" }
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[23];
						vec3 _SunDir;
						vec4 unused_0_7[4];
						vec4 _SideSunColor;
						vec4 unused_0_9[2];
						float _Year;
						vec4 unused_0_11[2];
						vec4 _FoamColor;
						vec4 unused_0_13[2];
						vec4 _MirrorColor2;
						vec4 unused_0_15;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_19[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_30[2];
						vec4 _MainTex_ST;
						vec4 unused_0_32;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					 vec4 phase0_Output0_0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat21;
					float u_xlat30;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.x = in_TEXCOORD0.y * 0.5;
					    u_xlat20 = u_xlat10.x * u_xlat10.x;
					    u_xlat10.x = max(u_xlat20, u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + _Time.y;
					    u_xlat10.x = u_xlat10.x + (-in_TANGENT0.w);
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat30 = u_xlat10.x * 13.3392019 + u_xlat1.x;
					    u_xlat30 = u_xlat1.z + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat2.xyz = u_xlat2.xxx * in_NORMAL0.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * in_COLOR0.xxx;
					    u_xlat30 = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat30 = (-u_xlat1.z) + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.xzw = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat30 = (-u_xlat2.y) * 2.0 + 2.0;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = u_xlat30 * 0.300000072 + 0.899999976;
					    u_xlat2.xyz = u_xlat2.xzw * in_COLOR0.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat2.xyz;
					    u_xlat3.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat3.xy = u_xlat1.xz + u_xlat3.xy;
					    u_xlat32 = cos(u_xlat3.x);
					    u_xlat3.x = sin(u_xlat3.y);
					    u_xlat32 = u_xlat32 + u_xlat3.x;
					    u_xlat32 = u_xlat32 * in_COLOR0.x;
					    u_xlat32 = u_xlat32 * 0.0199999996;
					    u_xlat2.xyz = vec3(u_xlat32) * in_TANGENT0.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    phase0_Output0_0.w = u_xlat0.x + 0.5;
					    u_xlat0.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    phase0_Output0_0.x = u_xlat0.x * 1.20000005;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.y = u_xlat0.x / u_xlat1.x;
					    u_xlat10.y = clamp(u_xlat10.y, 0.0, 1.0);
					    phase0_Output0_0.yz = u_xlat10.xy;
					    u_xlat3 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat3;
					    u_xlat3 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat3;
					    gl_Position = u_xlat3 + unity_MatrixVP[3];
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat2.xz;
					    u_xlat2.xzw = u_xlat2.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat2.xzw = max(u_xlat2.xzw, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xzw = min(u_xlat2.xzw, u_xlat3.xyz);
					    u_xlat0.x = (-u_xlat3.w) * 0.5 + u_xlat2.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat2.xyz = u_xlat2.xzw + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat1.x = u_xlat2.y * u_xlat10.x;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat1.x;
					    u_xlat6.z = u_xlat2.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat12.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat1.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat8.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.zzz * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat10.x * u_xlat12.y;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat10.x;
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat10.x;
					    u_xlat19.x = u_xlat12.z * u_xlat5.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat3.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xy = abs(_SunDir.yx);
					    u_xlat4.xyz = u_xlat3.zxw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat10.x = u_xlat3.x + u_xlat3.y;
					    u_xlat1.xz = u_xlat2.xy * u_xlat4.xy;
					    u_xlat1.x = u_xlat1.z + u_xlat1.x;
					    u_xlat10.x = u_xlat1.z / u_xlat10.x;
					    u_xlat1.x = u_xlat4.z * u_xlat2.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.600000024 + 0.400000006;
					    u_xlat21 = u_xlat0.x;
					    u_xlat21 = clamp(u_xlat21, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat0.x, u_xlat21);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat12.xyz = vec3(u_xlat21) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat12.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat2.xyz = u_xlat1.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat10.xxx + u_xlat2.xyz;
					    u_xlat0.x = _FlashDir.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat10.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat10.x + 1.0;
					    u_xlat3.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _FoamColor.xyz;
					    u_xlat0.x = dot(u_xlat3.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = _FoamColor.xyz * u_xlat2.xyz + (-u_xlat0.xxx);
					    u_xlat2.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _LutLerp.www;
					    u_xlat2.xyz = _LutLerp.xyz * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat4.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat10.yyy * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    u_xlat2.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat4.xyz = (-u_xlat2.xyz) + _LutLerp.www;
					    u_xlat2.xyz = _LutLerp.xyz * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat4.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat10.yyy * u_xlat4.xyz + u_xlat2.xyz;
					    vs_COLOR0.w = _FoamColor.w;
					    u_xlat2 = vec4(u_xlat30) * _MirrorColor2;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    vs_COLOR1.w = u_xlat2.w;
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(u_xlat30) + (-u_xlat0.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat3.xyz;
					    u_xlat0.xyw = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat1.x = dot(u_xlat0.xyw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyw = u_xlat0.xyw + (-u_xlat1.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat0.xyw + u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					vs_TEXCOORD0 = phase0_Output0_0.xy;
					vs_TEXCOORD1 = phase0_Output0_0.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[23];
						vec3 _SunDir;
						vec4 unused_0_7[4];
						vec4 _SideSunColor;
						vec4 unused_0_9[2];
						float _Year;
						vec4 unused_0_11[2];
						vec4 _FoamColor;
						vec4 unused_0_13[2];
						vec4 _MirrorColor2;
						vec4 unused_0_15;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_19[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_30[2];
						vec4 _MainTex_ST;
						vec4 unused_0_32;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					 vec4 phase0_Output0_0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat21;
					float u_xlat30;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.x = in_TEXCOORD0.y * 0.5;
					    u_xlat20 = u_xlat10.x * u_xlat10.x;
					    u_xlat10.x = max(u_xlat20, u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + _Time.y;
					    u_xlat10.x = u_xlat10.x + (-in_TANGENT0.w);
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat30 = u_xlat10.x * 13.3392019 + u_xlat1.x;
					    u_xlat30 = u_xlat1.z + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat2.xyz = u_xlat2.xxx * in_NORMAL0.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * in_COLOR0.xxx;
					    u_xlat30 = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat30 = (-u_xlat1.z) + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.xzw = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat30 = (-u_xlat2.y) * 2.0 + 2.0;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = u_xlat30 * 0.300000072 + 0.899999976;
					    u_xlat2.xyz = u_xlat2.xzw * in_COLOR0.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat2.xyz;
					    u_xlat3.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat3.xy = u_xlat1.xz + u_xlat3.xy;
					    u_xlat32 = cos(u_xlat3.x);
					    u_xlat3.x = sin(u_xlat3.y);
					    u_xlat32 = u_xlat32 + u_xlat3.x;
					    u_xlat32 = u_xlat32 * in_COLOR0.x;
					    u_xlat32 = u_xlat32 * 0.0199999996;
					    u_xlat2.xyz = vec3(u_xlat32) * in_TANGENT0.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    phase0_Output0_0.w = u_xlat0.x + 0.5;
					    u_xlat0.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    phase0_Output0_0.x = u_xlat0.x * 1.20000005;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.y = u_xlat0.x / u_xlat1.x;
					    u_xlat10.y = clamp(u_xlat10.y, 0.0, 1.0);
					    phase0_Output0_0.yz = u_xlat10.xy;
					    u_xlat3 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat3;
					    u_xlat3 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat3;
					    gl_Position = u_xlat3 + unity_MatrixVP[3];
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat2.xz;
					    u_xlat2.xzw = u_xlat2.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat2.xzw = max(u_xlat2.xzw, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xzw = min(u_xlat2.xzw, u_xlat3.xyz);
					    u_xlat0.x = (-u_xlat3.w) * 0.5 + u_xlat2.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat2.xyz = u_xlat2.xzw + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat1.x = u_xlat2.y * u_xlat10.x;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat1.x;
					    u_xlat6.z = u_xlat2.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat12.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat1.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat8.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.zzz * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat10.x * u_xlat12.y;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat10.x;
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat10.x;
					    u_xlat19.x = u_xlat12.z * u_xlat5.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat3.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xy = abs(_SunDir.yx);
					    u_xlat4.xyz = u_xlat3.zxw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat10.x = u_xlat3.x + u_xlat3.y;
					    u_xlat1.xz = u_xlat2.xy * u_xlat4.xy;
					    u_xlat1.x = u_xlat1.z + u_xlat1.x;
					    u_xlat10.x = u_xlat1.z / u_xlat10.x;
					    u_xlat1.x = u_xlat4.z * u_xlat2.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.600000024 + 0.400000006;
					    u_xlat21 = u_xlat0.x;
					    u_xlat21 = clamp(u_xlat21, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat0.x, u_xlat21);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat12.xyz = vec3(u_xlat21) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat12.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat2.xyz = u_xlat1.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat10.xxx + u_xlat2.xyz;
					    u_xlat0.x = _FlashDir.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat10.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat10.x + 1.0;
					    u_xlat3.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _FoamColor.xyz;
					    u_xlat0.x = dot(u_xlat3.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = _FoamColor.xyz * u_xlat2.xyz + (-u_xlat0.xxx);
					    u_xlat2.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _LutLerp.www;
					    u_xlat2.xyz = _LutLerp.xyz * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat4.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat10.yyy * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    u_xlat2.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat4.xyz = (-u_xlat2.xyz) + _LutLerp.www;
					    u_xlat2.xyz = _LutLerp.xyz * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat4.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat10.yyy * u_xlat4.xyz + u_xlat2.xyz;
					    vs_COLOR0.w = _FoamColor.w;
					    u_xlat2 = vec4(u_xlat30) * _MirrorColor2;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    vs_COLOR1.w = u_xlat2.w;
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(u_xlat30) + (-u_xlat0.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat3.xyz;
					    u_xlat0.xyw = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat1.x = dot(u_xlat0.xyw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyw = u_xlat0.xyw + (-u_xlat1.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat0.xyw + u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					vs_TEXCOORD0 = phase0_Output0_0.xy;
					vs_TEXCOORD1 = phase0_Output0_0.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_SELECTED_ON" }
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[23];
						vec3 _SunDir;
						vec4 unused_0_7[4];
						vec4 _SideSunColor;
						vec4 unused_0_9[2];
						float _Year;
						vec4 unused_0_11[2];
						vec4 _FoamColor;
						vec4 unused_0_13[2];
						vec4 _MirrorColor2;
						vec4 unused_0_15;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_19[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_30[2];
						vec4 _MainTex_ST;
						vec4 unused_0_32;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					 vec4 phase0_Output0_0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat21;
					float u_xlat30;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.x = in_TEXCOORD0.y * 0.5;
					    u_xlat20 = u_xlat10.x * u_xlat10.x;
					    u_xlat10.x = max(u_xlat20, u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + _Time.y;
					    u_xlat10.x = u_xlat10.x + (-in_TANGENT0.w);
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat30 = u_xlat10.x * 13.3392019 + u_xlat1.x;
					    u_xlat30 = u_xlat1.z + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat2.xyz = u_xlat2.xxx * in_NORMAL0.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * in_COLOR0.xxx;
					    u_xlat30 = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat30 = (-u_xlat1.z) + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.xzw = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat30 = (-u_xlat2.y) * 2.0 + 2.0;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = u_xlat30 * 0.300000072 + 0.899999976;
					    u_xlat2.xyz = u_xlat2.xzw * in_COLOR0.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat2.xyz;
					    u_xlat3.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat3.xy = u_xlat1.xz + u_xlat3.xy;
					    u_xlat32 = cos(u_xlat3.x);
					    u_xlat3.x = sin(u_xlat3.y);
					    u_xlat32 = u_xlat32 + u_xlat3.x;
					    u_xlat32 = u_xlat32 * in_COLOR0.x;
					    u_xlat32 = u_xlat32 * 0.0199999996;
					    u_xlat2.xyz = vec3(u_xlat32) * in_TANGENT0.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    phase0_Output0_0.w = u_xlat0.x + 0.5;
					    u_xlat0.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    phase0_Output0_0.x = u_xlat0.x * 1.20000005;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.y = u_xlat0.x / u_xlat1.x;
					    u_xlat10.y = clamp(u_xlat10.y, 0.0, 1.0);
					    phase0_Output0_0.yz = u_xlat10.xy;
					    u_xlat3 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat3;
					    u_xlat3 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat3;
					    gl_Position = u_xlat3 + unity_MatrixVP[3];
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat2.xz;
					    u_xlat2.xzw = u_xlat2.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat2.xzw = max(u_xlat2.xzw, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xzw = min(u_xlat2.xzw, u_xlat3.xyz);
					    u_xlat0.x = (-u_xlat3.w) * 0.5 + u_xlat2.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat2.xyz = u_xlat2.xzw + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat1.x = u_xlat2.y * u_xlat10.x;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat1.x;
					    u_xlat6.z = u_xlat2.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat12.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat1.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat8.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.zzz * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat10.x * u_xlat12.y;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat10.x;
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat10.x;
					    u_xlat19.x = u_xlat12.z * u_xlat5.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat3.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xy = abs(_SunDir.yx);
					    u_xlat4.xyz = u_xlat3.zxw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat10.x = u_xlat3.x + u_xlat3.y;
					    u_xlat1.xz = u_xlat2.xy * u_xlat4.xy;
					    u_xlat1.x = u_xlat1.z + u_xlat1.x;
					    u_xlat10.x = u_xlat1.z / u_xlat10.x;
					    u_xlat1.x = u_xlat4.z * u_xlat2.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.600000024 + 0.400000006;
					    u_xlat21 = u_xlat0.x;
					    u_xlat21 = clamp(u_xlat21, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat0.x, u_xlat21);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat12.xyz = vec3(u_xlat21) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat12.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat2.xyz = u_xlat1.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat10.xxx + u_xlat2.xyz;
					    u_xlat0.x = _FlashDir.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat10.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat10.x + 1.0;
					    u_xlat3.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _FoamColor.xyz;
					    u_xlat0.x = dot(u_xlat3.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = _FoamColor.xyz * u_xlat2.xyz + (-u_xlat0.xxx);
					    u_xlat2.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _LutLerp.www;
					    u_xlat2.xyz = _LutLerp.xyz * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat4.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat10.yyy * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    u_xlat2.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat4.xyz = (-u_xlat2.xyz) + _LutLerp.www;
					    u_xlat2.xyz = _LutLerp.xyz * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat4.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat10.yyy * u_xlat4.xyz + u_xlat2.xyz;
					    vs_COLOR0.w = _FoamColor.w;
					    u_xlat2 = vec4(u_xlat30) * _MirrorColor2;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    vs_COLOR1.w = u_xlat2.w;
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(u_xlat30) + (-u_xlat0.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat3.xyz;
					    u_xlat0.xyw = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat1.x = dot(u_xlat0.xyw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyw = u_xlat0.xyw + (-u_xlat1.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat0.xyw + u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					vs_TEXCOORD0 = phase0_Output0_0.xy;
					vs_TEXCOORD1 = phase0_Output0_0.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" }
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[23];
						vec3 _SunDir;
						vec4 unused_0_7[7];
						float _Year;
						vec4 unused_0_9[2];
						vec4 _FoamColor;
						vec4 unused_0_11[2];
						vec4 _MirrorColor2;
						vec4 unused_0_13;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_17[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_27[3];
						vec4 _MainTex_ST;
						vec4 unused_0_29;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					 vec4 phase0_Output0_0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat21;
					float u_xlat30;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.x = in_TEXCOORD0.y * 0.5;
					    u_xlat20 = u_xlat10.x * u_xlat10.x;
					    u_xlat10.x = max(u_xlat20, u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + _Time.y;
					    u_xlat10.x = u_xlat10.x + (-in_TANGENT0.w);
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat30 = u_xlat10.x * 13.3392019 + u_xlat1.x;
					    u_xlat30 = u_xlat1.z + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat2.xyz = u_xlat2.xxx * in_NORMAL0.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * in_COLOR0.xxx;
					    u_xlat30 = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat30 = (-u_xlat1.z) + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.xzw = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat30 = (-u_xlat2.y) * 2.0 + 2.0;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = u_xlat30 * 0.300000072 + 0.899999976;
					    u_xlat2.xyz = u_xlat2.xzw * in_COLOR0.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat2.xyz;
					    u_xlat3.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat3.xy = u_xlat1.xz + u_xlat3.xy;
					    u_xlat32 = cos(u_xlat3.x);
					    u_xlat3.x = sin(u_xlat3.y);
					    u_xlat32 = u_xlat32 + u_xlat3.x;
					    u_xlat32 = u_xlat32 * in_COLOR0.x;
					    u_xlat32 = u_xlat32 * 0.0199999996;
					    u_xlat2.xyz = vec3(u_xlat32) * in_TANGENT0.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    phase0_Output0_0.w = u_xlat0.x + 0.5;
					    u_xlat3.x = unity_MatrixV[0].z / unity_MatrixV[1].z;
					    u_xlat3.y = unity_MatrixV[2].z / unity_MatrixV[1].z;
					    u_xlat1.xz = u_xlat3.xy * u_xlat1.xx + u_xlat2.xz;
					    u_xlat0.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.y = u_xlat0.x / u_xlat1.x;
					    u_xlat10.y = clamp(u_xlat10.y, 0.0, 1.0);
					    phase0_Output0_0.yz = u_xlat10.xy;
					    u_xlat0.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    phase0_Output0_0.x = u_xlat0.x * 1.20000005;
					    u_xlat2.w = _WaterLevel * 2.0 + (-u_xlat2.y);
					    u_xlat3 = u_xlat2.wwww * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat3;
					    u_xlat3 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat3;
					    gl_Position = u_xlat3 + unity_MatrixVP[3];
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat2.xyz = in_TANGENT0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat0.x = (-u_xlat3.w) * 0.5 + u_xlat2.w;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat1.x = u_xlat2.y * u_xlat10.x;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat1.x;
					    u_xlat6.z = u_xlat2.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat12.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat1.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat8.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.zzz * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat10.x * u_xlat12.y;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat10.x;
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat10.x;
					    u_xlat19.x = u_xlat12.z * u_xlat5.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat3.y = abs(_SunDir.y);
					    u_xlat3.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xyz = u_xlat3.xyz;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat1.xz = u_xlat2.xy * u_xlat3.xy;
					    u_xlat10.x = u_xlat1.z + u_xlat1.x;
					    u_xlat10.x = u_xlat3.z * u_xlat2.z + u_xlat10.x;
					    u_xlat10.x = u_xlat10.x * 0.600000024 + 0.200000003;
					    u_xlat1.x = u_xlat0.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat21 = max(u_xlat0.x, u_xlat1.x);
					    u_xlat21 = (-u_xlat21) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat2.yzx;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(u_xlat21) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat2.xyz = u_xlat10.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat3.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    u_xlat3.xyz = _CloudCoverage.yyy * u_xlat3.xyz + u_xlat0.xxx;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _LutLerp.www;
					    u_xlat3.xyz = _LutLerp.xyz * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat10.yyy * u_xlat5.xyz + u_xlat3.xyz;
					    vs_COLOR0.w = _FoamColor.w;
					    u_xlat3 = vec4(u_xlat30) * _MirrorColor2;
					    u_xlat0.x = dot(u_xlat3.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.w = u_xlat3.w;
					    u_xlat3.xyz = _MirrorColor2.xyz * vec3(u_xlat30) + (-u_xlat0.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat3.xyz + u_xlat0.xxx;
					    u_xlat3.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat3.xyz + u_xlat0.xyw;
					    u_xlat3.xyz = (-u_xlat0.xyw) + u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat10.yyy * u_xlat3.xyz + u_xlat0.xyw;
					    u_xlat3.xyz = (-u_xlat2.xyz) + u_xlat0.xyw;
					    u_xlat0.x = in_POSITION0.y + _WaterLevel;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.800000012;
					    u_xlat3.w = 0.0;
					    u_xlat2 = u_xlat0.xxxx * u_xlat3 + u_xlat2;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    vs_COLOR1.w = u_xlat2.w;
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					vs_TEXCOORD0 = phase0_Output0_0.xy;
					vs_TEXCOORD1 = phase0_Output0_0.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[23];
						vec3 _SunDir;
						vec4 unused_0_7[7];
						float _Year;
						vec4 unused_0_9[2];
						vec4 _FoamColor;
						vec4 unused_0_11[2];
						vec4 _MirrorColor2;
						vec4 unused_0_13;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_17[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_27[3];
						vec4 _MainTex_ST;
						vec4 unused_0_29;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					 vec4 phase0_Output0_0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat21;
					float u_xlat30;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.x = in_TEXCOORD0.y * 0.5;
					    u_xlat20 = u_xlat10.x * u_xlat10.x;
					    u_xlat10.x = max(u_xlat20, u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + _Time.y;
					    u_xlat10.x = u_xlat10.x + (-in_TANGENT0.w);
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat30 = u_xlat10.x * 13.3392019 + u_xlat1.x;
					    u_xlat30 = u_xlat1.z + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat2.xyz = u_xlat2.xxx * in_NORMAL0.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * in_COLOR0.xxx;
					    u_xlat30 = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat30 = (-u_xlat1.z) + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.xzw = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat30 = (-u_xlat2.y) * 2.0 + 2.0;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = u_xlat30 * 0.300000072 + 0.899999976;
					    u_xlat2.xyz = u_xlat2.xzw * in_COLOR0.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat2.xyz;
					    u_xlat3.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat3.xy = u_xlat1.xz + u_xlat3.xy;
					    u_xlat32 = cos(u_xlat3.x);
					    u_xlat3.x = sin(u_xlat3.y);
					    u_xlat32 = u_xlat32 + u_xlat3.x;
					    u_xlat32 = u_xlat32 * in_COLOR0.x;
					    u_xlat32 = u_xlat32 * 0.0199999996;
					    u_xlat2.xyz = vec3(u_xlat32) * in_TANGENT0.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    phase0_Output0_0.w = u_xlat0.x + 0.5;
					    u_xlat3.x = unity_MatrixV[0].z / unity_MatrixV[1].z;
					    u_xlat3.y = unity_MatrixV[2].z / unity_MatrixV[1].z;
					    u_xlat1.xz = u_xlat3.xy * u_xlat1.xx + u_xlat2.xz;
					    u_xlat0.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.y = u_xlat0.x / u_xlat1.x;
					    u_xlat10.y = clamp(u_xlat10.y, 0.0, 1.0);
					    phase0_Output0_0.yz = u_xlat10.xy;
					    u_xlat0.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    phase0_Output0_0.x = u_xlat0.x * 1.20000005;
					    u_xlat2.w = _WaterLevel * 2.0 + (-u_xlat2.y);
					    u_xlat3 = u_xlat2.wwww * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat3;
					    u_xlat3 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat3;
					    gl_Position = u_xlat3 + unity_MatrixVP[3];
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat2.xyz = in_TANGENT0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat0.x = (-u_xlat3.w) * 0.5 + u_xlat2.w;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat1.x = u_xlat2.y * u_xlat10.x;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat1.x;
					    u_xlat6.z = u_xlat2.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat12.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat1.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat8.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.zzz * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat10.x * u_xlat12.y;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat10.x;
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat10.x;
					    u_xlat19.x = u_xlat12.z * u_xlat5.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat3.y = abs(_SunDir.y);
					    u_xlat3.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xyz = u_xlat3.xyz;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat1.xz = u_xlat2.xy * u_xlat3.xy;
					    u_xlat10.x = u_xlat1.z + u_xlat1.x;
					    u_xlat10.x = u_xlat3.z * u_xlat2.z + u_xlat10.x;
					    u_xlat10.x = u_xlat10.x * 0.600000024 + 0.200000003;
					    u_xlat1.x = u_xlat0.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat21 = max(u_xlat0.x, u_xlat1.x);
					    u_xlat21 = (-u_xlat21) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat2.yzx;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(u_xlat21) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat2.xyz = u_xlat10.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat3.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    u_xlat3.xyz = _CloudCoverage.yyy * u_xlat3.xyz + u_xlat0.xxx;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _LutLerp.www;
					    u_xlat3.xyz = _LutLerp.xyz * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat10.yyy * u_xlat5.xyz + u_xlat3.xyz;
					    vs_COLOR0.w = _FoamColor.w;
					    u_xlat3 = vec4(u_xlat30) * _MirrorColor2;
					    u_xlat0.x = dot(u_xlat3.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.w = u_xlat3.w;
					    u_xlat3.xyz = _MirrorColor2.xyz * vec3(u_xlat30) + (-u_xlat0.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat3.xyz + u_xlat0.xxx;
					    u_xlat3.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat3.xyz + u_xlat0.xyw;
					    u_xlat3.xyz = (-u_xlat0.xyw) + u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat10.yyy * u_xlat3.xyz + u_xlat0.xyw;
					    u_xlat3.xyz = (-u_xlat2.xyz) + u_xlat0.xyw;
					    u_xlat0.x = in_POSITION0.y + _WaterLevel;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.800000012;
					    u_xlat3.w = 0.0;
					    u_xlat2 = u_xlat0.xxxx * u_xlat3 + u_xlat2;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    vs_COLOR1.w = u_xlat2.w;
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					vs_TEXCOORD0 = phase0_Output0_0.xy;
					vs_TEXCOORD1 = phase0_Output0_0.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_SELECTED_ON" }
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[23];
						vec3 _SunDir;
						vec4 unused_0_7[7];
						float _Year;
						vec4 unused_0_9[2];
						vec4 _FoamColor;
						vec4 unused_0_11[2];
						vec4 _MirrorColor2;
						vec4 unused_0_13;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_17[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_27[3];
						vec4 _MainTex_ST;
						vec4 unused_0_29;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					 vec4 phase0_Output0_0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat21;
					float u_xlat30;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.x = in_TEXCOORD0.y * 0.5;
					    u_xlat20 = u_xlat10.x * u_xlat10.x;
					    u_xlat10.x = max(u_xlat20, u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + _Time.y;
					    u_xlat10.x = u_xlat10.x + (-in_TANGENT0.w);
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat30 = u_xlat10.x * 13.3392019 + u_xlat1.x;
					    u_xlat30 = u_xlat1.z + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat2.xyz = u_xlat2.xxx * in_NORMAL0.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * in_COLOR0.xxx;
					    u_xlat30 = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat30 = (-u_xlat1.z) + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.xzw = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat30 = (-u_xlat2.y) * 2.0 + 2.0;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = u_xlat30 * 0.300000072 + 0.899999976;
					    u_xlat2.xyz = u_xlat2.xzw * in_COLOR0.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat2.xyz;
					    u_xlat3.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat3.xy = u_xlat1.xz + u_xlat3.xy;
					    u_xlat32 = cos(u_xlat3.x);
					    u_xlat3.x = sin(u_xlat3.y);
					    u_xlat32 = u_xlat32 + u_xlat3.x;
					    u_xlat32 = u_xlat32 * in_COLOR0.x;
					    u_xlat32 = u_xlat32 * 0.0199999996;
					    u_xlat2.xyz = vec3(u_xlat32) * in_TANGENT0.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    phase0_Output0_0.w = u_xlat0.x + 0.5;
					    u_xlat3.x = unity_MatrixV[0].z / unity_MatrixV[1].z;
					    u_xlat3.y = unity_MatrixV[2].z / unity_MatrixV[1].z;
					    u_xlat1.xz = u_xlat3.xy * u_xlat1.xx + u_xlat2.xz;
					    u_xlat0.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.y = u_xlat0.x / u_xlat1.x;
					    u_xlat10.y = clamp(u_xlat10.y, 0.0, 1.0);
					    phase0_Output0_0.yz = u_xlat10.xy;
					    u_xlat0.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    phase0_Output0_0.x = u_xlat0.x * 1.20000005;
					    u_xlat2.w = _WaterLevel * 2.0 + (-u_xlat2.y);
					    u_xlat3 = u_xlat2.wwww * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat3;
					    u_xlat3 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat3;
					    gl_Position = u_xlat3 + unity_MatrixVP[3];
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat2.xyz = in_TANGENT0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat0.x = (-u_xlat3.w) * 0.5 + u_xlat2.w;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat1.x = u_xlat2.y * u_xlat10.x;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat1.x;
					    u_xlat6.z = u_xlat2.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat12.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat1.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat8.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.zzz * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat10.x * u_xlat12.y;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat10.x;
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat10.x;
					    u_xlat19.x = u_xlat12.z * u_xlat5.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat3.y = abs(_SunDir.y);
					    u_xlat3.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xyz = u_xlat3.xyz;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat1.xz = u_xlat2.xy * u_xlat3.xy;
					    u_xlat10.x = u_xlat1.z + u_xlat1.x;
					    u_xlat10.x = u_xlat3.z * u_xlat2.z + u_xlat10.x;
					    u_xlat10.x = u_xlat10.x * 0.600000024 + 0.200000003;
					    u_xlat1.x = u_xlat0.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat21 = max(u_xlat0.x, u_xlat1.x);
					    u_xlat21 = (-u_xlat21) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat2.yzx;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(u_xlat21) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat2.xyz = u_xlat10.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat3.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    u_xlat3.xyz = _CloudCoverage.yyy * u_xlat3.xyz + u_xlat0.xxx;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _LutLerp.www;
					    u_xlat3.xyz = _LutLerp.xyz * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat10.yyy * u_xlat5.xyz + u_xlat3.xyz;
					    vs_COLOR0.w = _FoamColor.w;
					    u_xlat3 = vec4(u_xlat30) * _MirrorColor2;
					    u_xlat0.x = dot(u_xlat3.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.w = u_xlat3.w;
					    u_xlat3.xyz = _MirrorColor2.xyz * vec3(u_xlat30) + (-u_xlat0.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat3.xyz + u_xlat0.xxx;
					    u_xlat3.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat3.xyz + u_xlat0.xyw;
					    u_xlat3.xyz = (-u_xlat0.xyw) + u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat10.yyy * u_xlat3.xyz + u_xlat0.xyw;
					    u_xlat3.xyz = (-u_xlat2.xyz) + u_xlat0.xyw;
					    u_xlat0.x = in_POSITION0.y + _WaterLevel;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.800000012;
					    u_xlat3.w = 0.0;
					    u_xlat2 = u_xlat0.xxxx * u_xlat3 + u_xlat2;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    vs_COLOR1.w = u_xlat2.w;
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					vs_TEXCOORD0 = phase0_Output0_0.xy;
					vs_TEXCOORD1 = phase0_Output0_0.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_VOXELAO_ON" }
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
						vec4 unused_0_0[44];
						vec4 _FoamColor;
						vec4 unused_0_2[2];
						vec4 _MirrorColor2;
						vec4 unused_0_4[8];
						float _WaterLevel;
						vec4 unused_0_6[7];
						vec4 _MainTex_ST;
						vec4 unused_0_8;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = in_TEXCOORD0.y * 0.5;
					    u_xlat3.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = max(u_xlat3.x, u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + _Time.y;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    vs_TEXCOORD0.y = u_xlat0.x;
					    u_xlat3.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    vs_TEXCOORD0.x = u_xlat3.x * 1.20000005;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat0.x = u_xlat0.x * 13.3392019 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat3.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat3.x = inversesqrt(u_xlat3.x);
					    u_xlat3.xyz = u_xlat3.xxx * in_NORMAL0.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * in_COLOR0.xxx;
					    u_xlat0.x = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = (-u_xlat3.y) * 2.0 + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * 0.300000072 + 0.899999976;
					    vs_COLOR1 = vec4(u_xlat6) * _MirrorColor2;
					    u_xlat0.xyz = u_xlat0.xyw * in_COLOR0.xxx;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat0.xyz;
					    u_xlat2.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat2.xy = u_xlat1.xz + u_xlat2.xy;
					    u_xlat9 = cos(u_xlat2.x);
					    u_xlat2.x = sin(u_xlat2.y);
					    u_xlat9 = u_xlat9 + u_xlat2.x;
					    u_xlat9 = u_xlat9 * in_COLOR0.x;
					    u_xlat9 = u_xlat9 * 0.0199999996;
					    u_xlat0.xyz = vec3(u_xlat9) * in_TANGENT0.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat2 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat2;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat2;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = _FoamColor * vec4(1.10000002, 1.10000002, 1.10000002, 1.10000002);
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_VOXELAO_ON" }
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
						vec4 unused_0_0[44];
						vec4 _FoamColor;
						vec4 unused_0_2[2];
						vec4 _MirrorColor2;
						vec4 unused_0_4[8];
						float _WaterLevel;
						vec4 unused_0_6[7];
						vec4 _MainTex_ST;
						vec4 unused_0_8;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = in_TEXCOORD0.y * 0.5;
					    u_xlat3.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = max(u_xlat3.x, u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + _Time.y;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    vs_TEXCOORD0.y = u_xlat0.x;
					    u_xlat3.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    vs_TEXCOORD0.x = u_xlat3.x * 1.20000005;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat0.x = u_xlat0.x * 13.3392019 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat3.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat3.x = inversesqrt(u_xlat3.x);
					    u_xlat3.xyz = u_xlat3.xxx * in_NORMAL0.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * in_COLOR0.xxx;
					    u_xlat0.x = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = (-u_xlat3.y) * 2.0 + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * 0.300000072 + 0.899999976;
					    vs_COLOR1 = vec4(u_xlat6) * _MirrorColor2;
					    u_xlat0.xyz = u_xlat0.xyw * in_COLOR0.xxx;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat0.xyz;
					    u_xlat2.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat2.xy = u_xlat1.xz + u_xlat2.xy;
					    u_xlat9 = cos(u_xlat2.x);
					    u_xlat2.x = sin(u_xlat2.y);
					    u_xlat9 = u_xlat9 + u_xlat2.x;
					    u_xlat9 = u_xlat9 * in_COLOR0.x;
					    u_xlat9 = u_xlat9 * 0.0199999996;
					    u_xlat0.xyz = vec3(u_xlat9) * in_TANGENT0.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat2 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat2;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat2;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = _FoamColor * vec4(1.10000002, 1.10000002, 1.10000002, 1.10000002);
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_VOXELAO_ON" "_SELECTED_ON" }
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
						vec4 unused_0_0[44];
						vec4 _FoamColor;
						vec4 unused_0_2[2];
						vec4 _MirrorColor2;
						vec4 unused_0_4[8];
						float _WaterLevel;
						vec4 unused_0_6[7];
						vec4 _MainTex_ST;
						vec4 unused_0_8;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = in_TEXCOORD0.y * 0.5;
					    u_xlat3.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = max(u_xlat3.x, u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + _Time.y;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    vs_TEXCOORD0.y = u_xlat0.x;
					    u_xlat3.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    vs_TEXCOORD0.x = u_xlat3.x * 1.20000005;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat0.x = u_xlat0.x * 13.3392019 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat3.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat3.x = inversesqrt(u_xlat3.x);
					    u_xlat3.xyz = u_xlat3.xxx * in_NORMAL0.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * in_COLOR0.xxx;
					    u_xlat0.x = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = (-u_xlat3.y) * 2.0 + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * 0.300000072 + 0.899999976;
					    vs_COLOR1 = vec4(u_xlat6) * _MirrorColor2;
					    u_xlat0.xyz = u_xlat0.xyw * in_COLOR0.xxx;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat0.xyz;
					    u_xlat2.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat2.xy = u_xlat1.xz + u_xlat2.xy;
					    u_xlat9 = cos(u_xlat2.x);
					    u_xlat2.x = sin(u_xlat2.y);
					    u_xlat9 = u_xlat9 + u_xlat2.x;
					    u_xlat9 = u_xlat9 * in_COLOR0.x;
					    u_xlat9 = u_xlat9 * 0.0199999996;
					    u_xlat0.xyz = vec3(u_xlat9) * in_TANGENT0.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat2 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat2;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat2;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = _FoamColor * vec4(1.10000002, 1.10000002, 1.10000002, 1.10000002);
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_VOXELAO_ON" }
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
						vec4 unused_0_0[44];
						vec4 _FoamColor;
						vec4 unused_0_2[2];
						vec4 _MirrorColor2;
						vec4 unused_0_4[8];
						float _WaterLevel;
						vec4 unused_0_6[7];
						vec4 _MainTex_ST;
						vec4 unused_0_8;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat5;
					float u_xlat6;
					void main()
					{
					    u_xlat0.x = in_TEXCOORD0.y * 0.5;
					    u_xlat3.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = max(u_xlat3.x, u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + _Time.y;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    vs_TEXCOORD0.y = u_xlat0.x;
					    u_xlat3.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    vs_TEXCOORD0.x = u_xlat3.x * 1.20000005;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat0.x = u_xlat0.x * 13.3392019 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat3.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat3.x = inversesqrt(u_xlat3.x);
					    u_xlat3.xyz = u_xlat3.xxx * in_NORMAL0.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * in_COLOR0.xxx;
					    u_xlat0.x = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = (-u_xlat3.y) * 2.0 + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * 0.300000072 + 0.899999976;
					    u_xlat0.xyw = u_xlat0.xyw * in_COLOR0.xxx;
					    u_xlat0.xyw = u_xlat0.xyw * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat0.xyw = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat0.xyw;
					    u_xlat2.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat2.xy = u_xlat1.xz + u_xlat2.xy;
					    u_xlat2.x = cos(u_xlat2.x);
					    u_xlat5 = sin(u_xlat2.y);
					    u_xlat2.x = u_xlat5 + u_xlat2.x;
					    u_xlat2.x = u_xlat2.x * in_COLOR0.x;
					    u_xlat2.x = u_xlat2.x * 0.0199999996;
					    u_xlat0.xyw = u_xlat2.xxx * in_TANGENT0.xyz + u_xlat0.xyw;
					    u_xlat0.xyw = u_xlat0.xyw + u_xlat1.xyz;
					    u_xlat2 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat0.wwww + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    vs_COLOR0.w = _FoamColor.w * 1.10000002;
					    u_xlat0.x = in_TANGENT0.y * 0.100000001 + 1.0;
					    u_xlat0.x = u_xlat0.x * 0.0800000057;
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat0.xxx;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    u_xlat2.w = u_xlat6 * _MirrorColor2.w;
					    u_xlat0 = _MirrorColor2 * vec4(u_xlat6) + (-u_xlat2);
					    u_xlat1.x = in_POSITION0.y + _WaterLevel;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat1.x = u_xlat1.x * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.800000012;
					    vs_COLOR1 = u_xlat1.xxxx * u_xlat0 + u_xlat2;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_CINEMATIC_ON" "_VOXELAO_ON" }
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
						vec4 unused_0_0[44];
						vec4 _FoamColor;
						vec4 unused_0_2[2];
						vec4 _MirrorColor2;
						vec4 unused_0_4[8];
						float _WaterLevel;
						vec4 unused_0_6[7];
						vec4 _MainTex_ST;
						vec4 unused_0_8;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat5;
					float u_xlat6;
					void main()
					{
					    u_xlat0.x = in_TEXCOORD0.y * 0.5;
					    u_xlat3.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = max(u_xlat3.x, u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + _Time.y;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    vs_TEXCOORD0.y = u_xlat0.x;
					    u_xlat3.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    vs_TEXCOORD0.x = u_xlat3.x * 1.20000005;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat0.x = u_xlat0.x * 13.3392019 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat3.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat3.x = inversesqrt(u_xlat3.x);
					    u_xlat3.xyz = u_xlat3.xxx * in_NORMAL0.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * in_COLOR0.xxx;
					    u_xlat0.x = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = (-u_xlat3.y) * 2.0 + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * 0.300000072 + 0.899999976;
					    u_xlat0.xyw = u_xlat0.xyw * in_COLOR0.xxx;
					    u_xlat0.xyw = u_xlat0.xyw * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat0.xyw = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat0.xyw;
					    u_xlat2.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat2.xy = u_xlat1.xz + u_xlat2.xy;
					    u_xlat2.x = cos(u_xlat2.x);
					    u_xlat5 = sin(u_xlat2.y);
					    u_xlat2.x = u_xlat5 + u_xlat2.x;
					    u_xlat2.x = u_xlat2.x * in_COLOR0.x;
					    u_xlat2.x = u_xlat2.x * 0.0199999996;
					    u_xlat0.xyw = u_xlat2.xxx * in_TANGENT0.xyz + u_xlat0.xyw;
					    u_xlat0.xyw = u_xlat0.xyw + u_xlat1.xyz;
					    u_xlat2 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat0.wwww + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    vs_COLOR0.w = _FoamColor.w * 1.10000002;
					    u_xlat0.x = in_TANGENT0.y * 0.100000001 + 1.0;
					    u_xlat0.x = u_xlat0.x * 0.0800000057;
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat0.xxx;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    u_xlat2.w = u_xlat6 * _MirrorColor2.w;
					    u_xlat0 = _MirrorColor2 * vec4(u_xlat6) + (-u_xlat2);
					    u_xlat1.x = in_POSITION0.y + _WaterLevel;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat1.x = u_xlat1.x * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.800000012;
					    vs_COLOR1 = u_xlat1.xxxx * u_xlat0 + u_xlat2;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_VOXELAO_ON" "_SELECTED_ON" }
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
						vec4 unused_0_0[44];
						vec4 _FoamColor;
						vec4 unused_0_2[2];
						vec4 _MirrorColor2;
						vec4 unused_0_4[8];
						float _WaterLevel;
						vec4 unused_0_6[7];
						vec4 _MainTex_ST;
						vec4 unused_0_8;
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
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat5;
					float u_xlat6;
					void main()
					{
					    u_xlat0.x = in_TEXCOORD0.y * 0.5;
					    u_xlat3.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = max(u_xlat3.x, u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + _Time.y;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    vs_TEXCOORD0.y = u_xlat0.x;
					    u_xlat3.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    vs_TEXCOORD0.x = u_xlat3.x * 1.20000005;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat3.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat3.xyz;
					    u_xlat0.x = u_xlat0.x * 13.3392019 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat3.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat3.x = inversesqrt(u_xlat3.x);
					    u_xlat3.xyz = u_xlat3.xxx * in_NORMAL0.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * in_COLOR0.xxx;
					    u_xlat0.x = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = (-u_xlat3.y) * 2.0 + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * 0.300000072 + 0.899999976;
					    u_xlat0.xyw = u_xlat0.xyw * in_COLOR0.xxx;
					    u_xlat0.xyw = u_xlat0.xyw * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat0.xyw = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat0.xyw;
					    u_xlat2.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat2.xy = u_xlat1.xz + u_xlat2.xy;
					    u_xlat2.x = cos(u_xlat2.x);
					    u_xlat5 = sin(u_xlat2.y);
					    u_xlat2.x = u_xlat5 + u_xlat2.x;
					    u_xlat2.x = u_xlat2.x * in_COLOR0.x;
					    u_xlat2.x = u_xlat2.x * 0.0199999996;
					    u_xlat0.xyw = u_xlat2.xxx * in_TANGENT0.xyz + u_xlat0.xyw;
					    u_xlat0.xyw = u_xlat0.xyw + u_xlat1.xyz;
					    u_xlat2 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat0.wwww + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    vs_COLOR0.w = _FoamColor.w * 1.10000002;
					    u_xlat0.x = in_TANGENT0.y * 0.100000001 + 1.0;
					    u_xlat0.x = u_xlat0.x * 0.0800000057;
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat0.xxx;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    u_xlat2.w = u_xlat6 * _MirrorColor2.w;
					    u_xlat0 = _MirrorColor2 * vec4(u_xlat6) + (-u_xlat2);
					    u_xlat1.x = in_POSITION0.y + _WaterLevel;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat1.x = u_xlat1.x * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.800000012;
					    vs_COLOR1 = u_xlat1.xxxx * u_xlat0 + u_xlat2;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_VOXELAO_ON" }
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[23];
						vec3 _SunDir;
						vec4 unused_0_7[4];
						vec4 _SideSunColor;
						vec4 unused_0_9[2];
						float _Year;
						vec4 unused_0_11[2];
						vec4 _FoamColor;
						vec4 unused_0_13[2];
						vec4 _MirrorColor2;
						vec4 unused_0_15;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_19[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_30[2];
						vec4 _MainTex_ST;
						vec4 unused_0_32;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					 vec4 phase0_Output0_0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat21;
					float u_xlat30;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.x = in_TEXCOORD0.y * 0.5;
					    u_xlat20 = u_xlat10.x * u_xlat10.x;
					    u_xlat10.x = max(u_xlat20, u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + _Time.y;
					    u_xlat10.x = u_xlat10.x + (-in_TANGENT0.w);
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat30 = u_xlat10.x * 13.3392019 + u_xlat1.x;
					    u_xlat30 = u_xlat1.z + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat2.xyz = u_xlat2.xxx * in_NORMAL0.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * in_COLOR0.xxx;
					    u_xlat30 = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat30 = (-u_xlat1.z) + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.xzw = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat30 = (-u_xlat2.y) * 2.0 + 2.0;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = u_xlat30 * 0.300000072 + 0.899999976;
					    u_xlat2.xyz = u_xlat2.xzw * in_COLOR0.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat2.xyz;
					    u_xlat3.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat3.xy = u_xlat1.xz + u_xlat3.xy;
					    u_xlat32 = cos(u_xlat3.x);
					    u_xlat3.x = sin(u_xlat3.y);
					    u_xlat32 = u_xlat32 + u_xlat3.x;
					    u_xlat32 = u_xlat32 * in_COLOR0.x;
					    u_xlat32 = u_xlat32 * 0.0199999996;
					    u_xlat2.xyz = vec3(u_xlat32) * in_TANGENT0.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    phase0_Output0_0.w = u_xlat0.x + 0.5;
					    u_xlat0.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    phase0_Output0_0.x = u_xlat0.x * 1.20000005;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.y = u_xlat0.x / u_xlat1.x;
					    u_xlat10.y = clamp(u_xlat10.y, 0.0, 1.0);
					    phase0_Output0_0.yz = u_xlat10.xy;
					    u_xlat3 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat3;
					    u_xlat3 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat3;
					    gl_Position = u_xlat3 + unity_MatrixVP[3];
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat2.xz;
					    u_xlat2.xzw = u_xlat2.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat2.xzw = max(u_xlat2.xzw, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xzw = min(u_xlat2.xzw, u_xlat3.xyz);
					    u_xlat0.x = (-u_xlat3.w) * 0.5 + u_xlat2.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat2.xyz = u_xlat2.xzw + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat1.x = u_xlat2.y * u_xlat10.x;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat1.x;
					    u_xlat6.z = u_xlat2.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat12.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat1.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat8.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.zzz * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat10.x * u_xlat12.y;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat10.x;
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat10.x;
					    u_xlat19.x = u_xlat12.z * u_xlat5.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat3.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xy = abs(_SunDir.yx);
					    u_xlat4.xyz = u_xlat3.zxw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat10.x = u_xlat3.x + u_xlat3.y;
					    u_xlat1.xz = u_xlat2.xy * u_xlat4.xy;
					    u_xlat1.x = u_xlat1.z + u_xlat1.x;
					    u_xlat10.x = u_xlat1.z / u_xlat10.x;
					    u_xlat1.x = u_xlat4.z * u_xlat2.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.600000024 + 0.400000006;
					    u_xlat21 = u_xlat0.x;
					    u_xlat21 = clamp(u_xlat21, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat0.x, u_xlat21);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat12.xyz = vec3(u_xlat21) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat12.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat2.xyz = u_xlat1.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat10.xxx + u_xlat2.xyz;
					    u_xlat0.x = _FlashDir.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat10.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat10.x + 1.0;
					    u_xlat3.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _FoamColor.xyz;
					    u_xlat0.x = dot(u_xlat3.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = _FoamColor.xyz * u_xlat2.xyz + (-u_xlat0.xxx);
					    u_xlat2.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _LutLerp.www;
					    u_xlat2.xyz = _LutLerp.xyz * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat4.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat10.yyy * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    u_xlat2.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat4.xyz = (-u_xlat2.xyz) + _LutLerp.www;
					    u_xlat2.xyz = _LutLerp.xyz * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat4.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat10.yyy * u_xlat4.xyz + u_xlat2.xyz;
					    vs_COLOR0.w = _FoamColor.w;
					    u_xlat2 = vec4(u_xlat30) * _MirrorColor2;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    vs_COLOR1.w = u_xlat2.w;
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(u_xlat30) + (-u_xlat0.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat3.xyz;
					    u_xlat0.xyw = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat1.x = dot(u_xlat0.xyw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyw = u_xlat0.xyw + (-u_xlat1.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat0.xyw + u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					vs_TEXCOORD0 = phase0_Output0_0.xy;
					vs_TEXCOORD1 = phase0_Output0_0.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_VOXELAO_ON" }
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[23];
						vec3 _SunDir;
						vec4 unused_0_7[4];
						vec4 _SideSunColor;
						vec4 unused_0_9[2];
						float _Year;
						vec4 unused_0_11[2];
						vec4 _FoamColor;
						vec4 unused_0_13[2];
						vec4 _MirrorColor2;
						vec4 unused_0_15;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_19[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_30[2];
						vec4 _MainTex_ST;
						vec4 unused_0_32;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					 vec4 phase0_Output0_0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat21;
					float u_xlat30;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.x = in_TEXCOORD0.y * 0.5;
					    u_xlat20 = u_xlat10.x * u_xlat10.x;
					    u_xlat10.x = max(u_xlat20, u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + _Time.y;
					    u_xlat10.x = u_xlat10.x + (-in_TANGENT0.w);
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat30 = u_xlat10.x * 13.3392019 + u_xlat1.x;
					    u_xlat30 = u_xlat1.z + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat2.xyz = u_xlat2.xxx * in_NORMAL0.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * in_COLOR0.xxx;
					    u_xlat30 = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat30 = (-u_xlat1.z) + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.xzw = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat30 = (-u_xlat2.y) * 2.0 + 2.0;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = u_xlat30 * 0.300000072 + 0.899999976;
					    u_xlat2.xyz = u_xlat2.xzw * in_COLOR0.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat2.xyz;
					    u_xlat3.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat3.xy = u_xlat1.xz + u_xlat3.xy;
					    u_xlat32 = cos(u_xlat3.x);
					    u_xlat3.x = sin(u_xlat3.y);
					    u_xlat32 = u_xlat32 + u_xlat3.x;
					    u_xlat32 = u_xlat32 * in_COLOR0.x;
					    u_xlat32 = u_xlat32 * 0.0199999996;
					    u_xlat2.xyz = vec3(u_xlat32) * in_TANGENT0.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    phase0_Output0_0.w = u_xlat0.x + 0.5;
					    u_xlat0.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    phase0_Output0_0.x = u_xlat0.x * 1.20000005;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.y = u_xlat0.x / u_xlat1.x;
					    u_xlat10.y = clamp(u_xlat10.y, 0.0, 1.0);
					    phase0_Output0_0.yz = u_xlat10.xy;
					    u_xlat3 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat3;
					    u_xlat3 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat3;
					    gl_Position = u_xlat3 + unity_MatrixVP[3];
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat2.xz;
					    u_xlat2.xzw = u_xlat2.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat2.xzw = max(u_xlat2.xzw, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xzw = min(u_xlat2.xzw, u_xlat3.xyz);
					    u_xlat0.x = (-u_xlat3.w) * 0.5 + u_xlat2.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat2.xyz = u_xlat2.xzw + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat1.x = u_xlat2.y * u_xlat10.x;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat1.x;
					    u_xlat6.z = u_xlat2.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat12.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat1.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat8.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.zzz * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat10.x * u_xlat12.y;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat10.x;
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat10.x;
					    u_xlat19.x = u_xlat12.z * u_xlat5.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat3.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xy = abs(_SunDir.yx);
					    u_xlat4.xyz = u_xlat3.zxw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat10.x = u_xlat3.x + u_xlat3.y;
					    u_xlat1.xz = u_xlat2.xy * u_xlat4.xy;
					    u_xlat1.x = u_xlat1.z + u_xlat1.x;
					    u_xlat10.x = u_xlat1.z / u_xlat10.x;
					    u_xlat1.x = u_xlat4.z * u_xlat2.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.600000024 + 0.400000006;
					    u_xlat21 = u_xlat0.x;
					    u_xlat21 = clamp(u_xlat21, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat0.x, u_xlat21);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat12.xyz = vec3(u_xlat21) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat12.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat2.xyz = u_xlat1.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat10.xxx + u_xlat2.xyz;
					    u_xlat0.x = _FlashDir.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat10.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat10.x + 1.0;
					    u_xlat3.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _FoamColor.xyz;
					    u_xlat0.x = dot(u_xlat3.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = _FoamColor.xyz * u_xlat2.xyz + (-u_xlat0.xxx);
					    u_xlat2.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _LutLerp.www;
					    u_xlat2.xyz = _LutLerp.xyz * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat4.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat10.yyy * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    u_xlat2.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat4.xyz = (-u_xlat2.xyz) + _LutLerp.www;
					    u_xlat2.xyz = _LutLerp.xyz * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat4.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat10.yyy * u_xlat4.xyz + u_xlat2.xyz;
					    vs_COLOR0.w = _FoamColor.w;
					    u_xlat2 = vec4(u_xlat30) * _MirrorColor2;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    vs_COLOR1.w = u_xlat2.w;
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(u_xlat30) + (-u_xlat0.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat3.xyz;
					    u_xlat0.xyw = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat1.x = dot(u_xlat0.xyw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyw = u_xlat0.xyw + (-u_xlat1.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat0.xyw + u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					vs_TEXCOORD0 = phase0_Output0_0.xy;
					vs_TEXCOORD1 = phase0_Output0_0.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_VOXELAO_ON" "_SELECTED_ON" }
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[23];
						vec3 _SunDir;
						vec4 unused_0_7[4];
						vec4 _SideSunColor;
						vec4 unused_0_9[2];
						float _Year;
						vec4 unused_0_11[2];
						vec4 _FoamColor;
						vec4 unused_0_13[2];
						vec4 _MirrorColor2;
						vec4 unused_0_15;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_19[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_30[2];
						vec4 _MainTex_ST;
						vec4 unused_0_32;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					 vec4 phase0_Output0_0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat21;
					float u_xlat30;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.x = in_TEXCOORD0.y * 0.5;
					    u_xlat20 = u_xlat10.x * u_xlat10.x;
					    u_xlat10.x = max(u_xlat20, u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + _Time.y;
					    u_xlat10.x = u_xlat10.x + (-in_TANGENT0.w);
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat30 = u_xlat10.x * 13.3392019 + u_xlat1.x;
					    u_xlat30 = u_xlat1.z + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat2.xyz = u_xlat2.xxx * in_NORMAL0.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * in_COLOR0.xxx;
					    u_xlat30 = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat30 = (-u_xlat1.z) + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.xzw = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat30 = (-u_xlat2.y) * 2.0 + 2.0;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = u_xlat30 * 0.300000072 + 0.899999976;
					    u_xlat2.xyz = u_xlat2.xzw * in_COLOR0.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat2.xyz;
					    u_xlat3.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat3.xy = u_xlat1.xz + u_xlat3.xy;
					    u_xlat32 = cos(u_xlat3.x);
					    u_xlat3.x = sin(u_xlat3.y);
					    u_xlat32 = u_xlat32 + u_xlat3.x;
					    u_xlat32 = u_xlat32 * in_COLOR0.x;
					    u_xlat32 = u_xlat32 * 0.0199999996;
					    u_xlat2.xyz = vec3(u_xlat32) * in_TANGENT0.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    phase0_Output0_0.w = u_xlat0.x + 0.5;
					    u_xlat0.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    phase0_Output0_0.x = u_xlat0.x * 1.20000005;
					    u_xlat0.x = dot(u_xlat2.xz, u_xlat2.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.y = u_xlat0.x / u_xlat1.x;
					    u_xlat10.y = clamp(u_xlat10.y, 0.0, 1.0);
					    phase0_Output0_0.yz = u_xlat10.xy;
					    u_xlat3 = u_xlat2.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat3;
					    u_xlat3 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat3;
					    gl_Position = u_xlat3 + unity_MatrixVP[3];
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat2.xz;
					    u_xlat2.xzw = u_xlat2.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat2.xzw = max(u_xlat2.xzw, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xzw = min(u_xlat2.xzw, u_xlat3.xyz);
					    u_xlat0.x = (-u_xlat3.w) * 0.5 + u_xlat2.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat2.xyz = u_xlat2.xzw + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat1.x = u_xlat2.y * u_xlat10.x;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat1.x;
					    u_xlat6.z = u_xlat2.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat12.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat1.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat8.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.zzz * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat10.x * u_xlat12.y;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat10.x;
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat10.x;
					    u_xlat19.x = u_xlat12.z * u_xlat5.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat3.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xy = abs(_SunDir.yx);
					    u_xlat4.xyz = u_xlat3.zxw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat10.x = u_xlat3.x + u_xlat3.y;
					    u_xlat1.xz = u_xlat2.xy * u_xlat4.xy;
					    u_xlat1.x = u_xlat1.z + u_xlat1.x;
					    u_xlat10.x = u_xlat1.z / u_xlat10.x;
					    u_xlat1.x = u_xlat4.z * u_xlat2.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.600000024 + 0.400000006;
					    u_xlat21 = u_xlat0.x;
					    u_xlat21 = clamp(u_xlat21, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat0.x, u_xlat21);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat12.xyz = vec3(u_xlat21) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat12.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat2.xyz = u_xlat1.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat10.xxx + u_xlat2.xyz;
					    u_xlat0.x = _FlashDir.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat10.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat10.x + 1.0;
					    u_xlat3.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _FoamColor.xyz;
					    u_xlat0.x = dot(u_xlat3.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = _FoamColor.xyz * u_xlat2.xyz + (-u_xlat0.xxx);
					    u_xlat2.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _LutLerp.www;
					    u_xlat2.xyz = _LutLerp.xyz * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat4.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat10.yyy * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    u_xlat2.xyz = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat4.xyz = (-u_xlat2.xyz) + _LutLerp.www;
					    u_xlat2.xyz = _LutLerp.xyz * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat4.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat10.yyy * u_xlat4.xyz + u_xlat2.xyz;
					    vs_COLOR0.w = _FoamColor.w;
					    u_xlat2 = vec4(u_xlat30) * _MirrorColor2;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    vs_COLOR1.w = u_xlat2.w;
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(u_xlat30) + (-u_xlat0.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat3.xyz;
					    u_xlat0.xyw = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat1.x = dot(u_xlat0.xyw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyw = u_xlat0.xyw + (-u_xlat1.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat0.xyw + u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					vs_TEXCOORD0 = phase0_Output0_0.xy;
					vs_TEXCOORD1 = phase0_Output0_0.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_VOXELAO_ON" }
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[23];
						vec3 _SunDir;
						vec4 unused_0_7[7];
						float _Year;
						vec4 unused_0_9[2];
						vec4 _FoamColor;
						vec4 unused_0_11[2];
						vec4 _MirrorColor2;
						vec4 unused_0_13;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_17[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_27[3];
						vec4 _MainTex_ST;
						vec4 unused_0_29;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					 vec4 phase0_Output0_0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat21;
					float u_xlat30;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.x = in_TEXCOORD0.y * 0.5;
					    u_xlat20 = u_xlat10.x * u_xlat10.x;
					    u_xlat10.x = max(u_xlat20, u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + _Time.y;
					    u_xlat10.x = u_xlat10.x + (-in_TANGENT0.w);
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat30 = u_xlat10.x * 13.3392019 + u_xlat1.x;
					    u_xlat30 = u_xlat1.z + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat2.xyz = u_xlat2.xxx * in_NORMAL0.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * in_COLOR0.xxx;
					    u_xlat30 = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat30 = (-u_xlat1.z) + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.xzw = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat30 = (-u_xlat2.y) * 2.0 + 2.0;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = u_xlat30 * 0.300000072 + 0.899999976;
					    u_xlat2.xyz = u_xlat2.xzw * in_COLOR0.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat2.xyz;
					    u_xlat3.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat3.xy = u_xlat1.xz + u_xlat3.xy;
					    u_xlat32 = cos(u_xlat3.x);
					    u_xlat3.x = sin(u_xlat3.y);
					    u_xlat32 = u_xlat32 + u_xlat3.x;
					    u_xlat32 = u_xlat32 * in_COLOR0.x;
					    u_xlat32 = u_xlat32 * 0.0199999996;
					    u_xlat2.xyz = vec3(u_xlat32) * in_TANGENT0.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    phase0_Output0_0.w = u_xlat0.x + 0.5;
					    u_xlat3.x = unity_MatrixV[0].z / unity_MatrixV[1].z;
					    u_xlat3.y = unity_MatrixV[2].z / unity_MatrixV[1].z;
					    u_xlat1.xz = u_xlat3.xy * u_xlat1.xx + u_xlat2.xz;
					    u_xlat0.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.y = u_xlat0.x / u_xlat1.x;
					    u_xlat10.y = clamp(u_xlat10.y, 0.0, 1.0);
					    phase0_Output0_0.yz = u_xlat10.xy;
					    u_xlat0.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    phase0_Output0_0.x = u_xlat0.x * 1.20000005;
					    u_xlat2.w = _WaterLevel * 2.0 + (-u_xlat2.y);
					    u_xlat3 = u_xlat2.wwww * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat3;
					    u_xlat3 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat3;
					    gl_Position = u_xlat3 + unity_MatrixVP[3];
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat2.xyz = in_TANGENT0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat0.x = (-u_xlat3.w) * 0.5 + u_xlat2.w;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat1.x = u_xlat2.y * u_xlat10.x;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat1.x;
					    u_xlat6.z = u_xlat2.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat12.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat1.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat8.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.zzz * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat10.x * u_xlat12.y;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat10.x;
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat10.x;
					    u_xlat19.x = u_xlat12.z * u_xlat5.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat3.y = abs(_SunDir.y);
					    u_xlat3.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xyz = u_xlat3.xyz;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat1.xz = u_xlat2.xy * u_xlat3.xy;
					    u_xlat10.x = u_xlat1.z + u_xlat1.x;
					    u_xlat10.x = u_xlat3.z * u_xlat2.z + u_xlat10.x;
					    u_xlat10.x = u_xlat10.x * 0.600000024 + 0.200000003;
					    u_xlat1.x = u_xlat0.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat21 = max(u_xlat0.x, u_xlat1.x);
					    u_xlat21 = (-u_xlat21) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat2.yzx;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(u_xlat21) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat2.xyz = u_xlat10.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat3.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    u_xlat3.xyz = _CloudCoverage.yyy * u_xlat3.xyz + u_xlat0.xxx;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _LutLerp.www;
					    u_xlat3.xyz = _LutLerp.xyz * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat10.yyy * u_xlat5.xyz + u_xlat3.xyz;
					    vs_COLOR0.w = _FoamColor.w;
					    u_xlat3 = vec4(u_xlat30) * _MirrorColor2;
					    u_xlat0.x = dot(u_xlat3.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.w = u_xlat3.w;
					    u_xlat3.xyz = _MirrorColor2.xyz * vec3(u_xlat30) + (-u_xlat0.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat3.xyz + u_xlat0.xxx;
					    u_xlat3.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat3.xyz + u_xlat0.xyw;
					    u_xlat3.xyz = (-u_xlat0.xyw) + u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat10.yyy * u_xlat3.xyz + u_xlat0.xyw;
					    u_xlat3.xyz = (-u_xlat2.xyz) + u_xlat0.xyw;
					    u_xlat0.x = in_POSITION0.y + _WaterLevel;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.800000012;
					    u_xlat3.w = 0.0;
					    u_xlat2 = u_xlat0.xxxx * u_xlat3 + u_xlat2;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    vs_COLOR1.w = u_xlat2.w;
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					vs_TEXCOORD0 = phase0_Output0_0.xy;
					vs_TEXCOORD1 = phase0_Output0_0.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_CINEMATIC_ON" "_VOXELAO_ON" }
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[23];
						vec3 _SunDir;
						vec4 unused_0_7[7];
						float _Year;
						vec4 unused_0_9[2];
						vec4 _FoamColor;
						vec4 unused_0_11[2];
						vec4 _MirrorColor2;
						vec4 unused_0_13;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_17[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_27[3];
						vec4 _MainTex_ST;
						vec4 unused_0_29;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					 vec4 phase0_Output0_0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat21;
					float u_xlat30;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.x = in_TEXCOORD0.y * 0.5;
					    u_xlat20 = u_xlat10.x * u_xlat10.x;
					    u_xlat10.x = max(u_xlat20, u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + _Time.y;
					    u_xlat10.x = u_xlat10.x + (-in_TANGENT0.w);
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat30 = u_xlat10.x * 13.3392019 + u_xlat1.x;
					    u_xlat30 = u_xlat1.z + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat2.xyz = u_xlat2.xxx * in_NORMAL0.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * in_COLOR0.xxx;
					    u_xlat30 = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat30 = (-u_xlat1.z) + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.xzw = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat30 = (-u_xlat2.y) * 2.0 + 2.0;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = u_xlat30 * 0.300000072 + 0.899999976;
					    u_xlat2.xyz = u_xlat2.xzw * in_COLOR0.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat2.xyz;
					    u_xlat3.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat3.xy = u_xlat1.xz + u_xlat3.xy;
					    u_xlat32 = cos(u_xlat3.x);
					    u_xlat3.x = sin(u_xlat3.y);
					    u_xlat32 = u_xlat32 + u_xlat3.x;
					    u_xlat32 = u_xlat32 * in_COLOR0.x;
					    u_xlat32 = u_xlat32 * 0.0199999996;
					    u_xlat2.xyz = vec3(u_xlat32) * in_TANGENT0.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    phase0_Output0_0.w = u_xlat0.x + 0.5;
					    u_xlat3.x = unity_MatrixV[0].z / unity_MatrixV[1].z;
					    u_xlat3.y = unity_MatrixV[2].z / unity_MatrixV[1].z;
					    u_xlat1.xz = u_xlat3.xy * u_xlat1.xx + u_xlat2.xz;
					    u_xlat0.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.y = u_xlat0.x / u_xlat1.x;
					    u_xlat10.y = clamp(u_xlat10.y, 0.0, 1.0);
					    phase0_Output0_0.yz = u_xlat10.xy;
					    u_xlat0.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    phase0_Output0_0.x = u_xlat0.x * 1.20000005;
					    u_xlat2.w = _WaterLevel * 2.0 + (-u_xlat2.y);
					    u_xlat3 = u_xlat2.wwww * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat3;
					    u_xlat3 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat3;
					    gl_Position = u_xlat3 + unity_MatrixVP[3];
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat2.xyz = in_TANGENT0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat0.x = (-u_xlat3.w) * 0.5 + u_xlat2.w;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat1.x = u_xlat2.y * u_xlat10.x;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat1.x;
					    u_xlat6.z = u_xlat2.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat12.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat1.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat8.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.zzz * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat10.x * u_xlat12.y;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat10.x;
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat10.x;
					    u_xlat19.x = u_xlat12.z * u_xlat5.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat3.y = abs(_SunDir.y);
					    u_xlat3.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xyz = u_xlat3.xyz;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat1.xz = u_xlat2.xy * u_xlat3.xy;
					    u_xlat10.x = u_xlat1.z + u_xlat1.x;
					    u_xlat10.x = u_xlat3.z * u_xlat2.z + u_xlat10.x;
					    u_xlat10.x = u_xlat10.x * 0.600000024 + 0.200000003;
					    u_xlat1.x = u_xlat0.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat21 = max(u_xlat0.x, u_xlat1.x);
					    u_xlat21 = (-u_xlat21) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat2.yzx;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(u_xlat21) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat2.xyz = u_xlat10.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat3.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    u_xlat3.xyz = _CloudCoverage.yyy * u_xlat3.xyz + u_xlat0.xxx;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _LutLerp.www;
					    u_xlat3.xyz = _LutLerp.xyz * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat10.yyy * u_xlat5.xyz + u_xlat3.xyz;
					    vs_COLOR0.w = _FoamColor.w;
					    u_xlat3 = vec4(u_xlat30) * _MirrorColor2;
					    u_xlat0.x = dot(u_xlat3.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.w = u_xlat3.w;
					    u_xlat3.xyz = _MirrorColor2.xyz * vec3(u_xlat30) + (-u_xlat0.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat3.xyz + u_xlat0.xxx;
					    u_xlat3.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat3.xyz + u_xlat0.xyw;
					    u_xlat3.xyz = (-u_xlat0.xyw) + u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat10.yyy * u_xlat3.xyz + u_xlat0.xyw;
					    u_xlat3.xyz = (-u_xlat2.xyz) + u_xlat0.xyw;
					    u_xlat0.x = in_POSITION0.y + _WaterLevel;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.800000012;
					    u_xlat3.w = 0.0;
					    u_xlat2 = u_xlat0.xxxx * u_xlat3 + u_xlat2;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    vs_COLOR1.w = u_xlat2.w;
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					vs_TEXCOORD0 = phase0_Output0_0.xy;
					vs_TEXCOORD1 = phase0_Output0_0.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_VOXELAO_ON" "_SELECTED_ON" }
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[23];
						vec3 _SunDir;
						vec4 unused_0_7[7];
						float _Year;
						vec4 unused_0_9[2];
						vec4 _FoamColor;
						vec4 unused_0_11[2];
						vec4 _MirrorColor2;
						vec4 unused_0_13;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_17[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_27[3];
						vec4 _MainTex_ST;
						vec4 unused_0_29;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					 vec4 phase0_Output0_0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat21;
					float u_xlat30;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.x = in_TEXCOORD0.y * 0.5;
					    u_xlat20 = u_xlat10.x * u_xlat10.x;
					    u_xlat10.x = max(u_xlat20, u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + _Time.y;
					    u_xlat10.x = u_xlat10.x + (-in_TANGENT0.w);
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat30 = u_xlat10.x * 13.3392019 + u_xlat1.x;
					    u_xlat30 = u_xlat1.z + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat2.xyz = u_xlat2.xxx * in_NORMAL0.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * in_COLOR0.xxx;
					    u_xlat30 = u_xlat1.y * 23.6247768 + u_xlat1.x;
					    u_xlat30 = (-u_xlat1.z) + u_xlat30;
					    u_xlat30 = sin(u_xlat30);
					    u_xlat2.xzw = vec3(u_xlat30) * u_xlat2.xyz;
					    u_xlat30 = (-u_xlat2.y) * 2.0 + 2.0;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = u_xlat30 * 0.300000072 + 0.899999976;
					    u_xlat2.xyz = u_xlat2.xzw * in_COLOR0.xxx;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.00999999978, 0.00999999978, 0.00999999978) + u_xlat2.xyz;
					    u_xlat3.xy = _Time.yy * vec2(6.12349987, 5.43100023) + u_xlat1.yy;
					    u_xlat3.xy = u_xlat1.xz + u_xlat3.xy;
					    u_xlat32 = cos(u_xlat3.x);
					    u_xlat3.x = sin(u_xlat3.y);
					    u_xlat32 = u_xlat32 + u_xlat3.x;
					    u_xlat32 = u_xlat32 * in_COLOR0.x;
					    u_xlat32 = u_xlat32 * 0.0199999996;
					    u_xlat2.xyz = vec3(u_xlat32) * in_TANGENT0.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat2.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    phase0_Output0_0.w = u_xlat0.x + 0.5;
					    u_xlat3.x = unity_MatrixV[0].z / unity_MatrixV[1].z;
					    u_xlat3.y = unity_MatrixV[2].z / unity_MatrixV[1].z;
					    u_xlat1.xz = u_xlat3.xy * u_xlat1.xx + u_xlat2.xz;
					    u_xlat0.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.y = u_xlat0.x / u_xlat1.x;
					    u_xlat10.y = clamp(u_xlat10.y, 0.0, 1.0);
					    phase0_Output0_0.yz = u_xlat10.xy;
					    u_xlat0.x = in_TEXCOORD0.x * _MainTex_ST.x + _MainTex_ST.z;
					    phase0_Output0_0.x = u_xlat0.x * 1.20000005;
					    u_xlat2.w = _WaterLevel * 2.0 + (-u_xlat2.y);
					    u_xlat3 = u_xlat2.wwww * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat2.xxxx + u_xlat3;
					    u_xlat3 = unity_MatrixVP[2] * u_xlat2.zzzz + u_xlat3;
					    gl_Position = u_xlat3 + unity_MatrixVP[3];
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat2.xwz + u_xlat3.xyz;
					    u_xlat2.xyz = in_TANGENT0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat0.x = (-u_xlat3.w) * 0.5 + u_xlat2.w;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat1.x = u_xlat2.y * u_xlat10.x;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat1.x;
					    u_xlat6.z = u_xlat2.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat12.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat1.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat8.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat4.zzz * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat10.x * u_xlat12.y;
					    u_xlat6.y = u_xlat5.x * u_xlat2.x + u_xlat10.x;
					    u_xlat6.x = u_xlat5.x * u_xlat12.x + u_xlat10.x;
					    u_xlat19.x = u_xlat12.z * u_xlat5.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat2.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat3.y = abs(_SunDir.y);
					    u_xlat3.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xyz = u_xlat3.xyz;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat1.xz = u_xlat2.xy * u_xlat3.xy;
					    u_xlat10.x = u_xlat1.z + u_xlat1.x;
					    u_xlat10.x = u_xlat3.z * u_xlat2.z + u_xlat10.x;
					    u_xlat10.x = u_xlat10.x * 0.600000024 + 0.200000003;
					    u_xlat1.x = u_xlat0.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat21 = max(u_xlat0.x, u_xlat1.x);
					    u_xlat21 = (-u_xlat21) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat2.yzx;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(u_xlat21) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat2.xyz = u_xlat10.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat3.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    u_xlat3.xyz = _CloudCoverage.yyy * u_xlat3.xyz + u_xlat0.xxx;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _LutLerp.www;
					    u_xlat3.xyz = _LutLerp.xyz * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat10.yyy * u_xlat5.xyz + u_xlat3.xyz;
					    vs_COLOR0.w = _FoamColor.w;
					    u_xlat3 = vec4(u_xlat30) * _MirrorColor2;
					    u_xlat0.x = dot(u_xlat3.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.w = u_xlat3.w;
					    u_xlat3.xyz = _MirrorColor2.xyz * vec3(u_xlat30) + (-u_xlat0.xxx);
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat3.xyz + u_xlat0.xxx;
					    u_xlat3.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat3.xyz + u_xlat0.xyw;
					    u_xlat3.xyz = (-u_xlat0.xyw) + u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat10.yyy * u_xlat3.xyz + u_xlat0.xyw;
					    u_xlat3.xyz = (-u_xlat2.xyz) + u_xlat0.xyw;
					    u_xlat0.x = in_POSITION0.y + _WaterLevel;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.800000012;
					    u_xlat3.w = 0.0;
					    u_xlat2 = u_xlat0.xxxx * u_xlat3 + u_xlat2;
					    u_xlat0.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.xyz = (-u_xlat0.xxx) + u_xlat2.xyz;
					    vs_COLOR1.w = u_xlat2.w;
					    u_xlat0.xyw = _CloudCoverage.yyy * u_xlat2.xyz + u_xlat0.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyw) + _LutLerp.www;
					    u_xlat0.xyw = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat2.xyz = (-u_xlat0.xyw) + u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat0.xyw;
					    u_xlat0.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD2.xy = u_xlat1.yw;
					vs_TEXCOORD0 = phase0_Output0_0.xy;
					vs_TEXCOORD1 = phase0_Output0_0.zw;
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
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = vs_TEXCOORD2.y * vs_TEXCOORD2.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 5.0 + -4.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz + (-vs_COLOR1.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat3 = vs_TEXCOORD2.x * 0.100000001 + 1.0;
					    SV_Target0.xyz = vec3(u_xlat3) * u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = vs_TEXCOORD2.y * vs_TEXCOORD2.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 5.0 + -4.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz + (-vs_COLOR1.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat3 = vs_TEXCOORD2.x * 0.100000001 + 1.0;
					    SV_Target0.xyz = vec3(u_xlat3) * u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_SELECTED_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = vs_TEXCOORD2.y * vs_TEXCOORD2.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 5.0 + -4.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz + (-vs_COLOR1.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat3 = vs_TEXCOORD2.x * 0.100000001 + 1.0;
					    SV_Target0.xyz = vec3(u_xlat3) * u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					bool u_xlatb0;
					void main()
					{
					    u_xlatb0 = vs_TEXCOORD1.y<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_CINEMATIC_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					bool u_xlatb0;
					void main()
					{
					    u_xlatb0 = vs_TEXCOORD1.y<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_SELECTED_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					bool u_xlatb0;
					void main()
					{
					    u_xlatb0 = vs_TEXCOORD1.y<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = vs_TEXCOORD2.y * vs_TEXCOORD2.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 5.0 + -4.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz + (-vs_COLOR1.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat3 = vs_TEXCOORD2.x * 0.100000001 + 1.0;
					    SV_Target0.xyz = vec3(u_xlat3) * u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = vs_TEXCOORD2.y * vs_TEXCOORD2.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 5.0 + -4.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz + (-vs_COLOR1.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat3 = vs_TEXCOORD2.x * 0.100000001 + 1.0;
					    SV_Target0.xyz = vec3(u_xlat3) * u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_SELECTED_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = vs_TEXCOORD2.y * vs_TEXCOORD2.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 5.0 + -4.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz + (-vs_COLOR1.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat3 = vs_TEXCOORD2.x * 0.100000001 + 1.0;
					    SV_Target0.xyz = vec3(u_xlat3) * u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					bool u_xlatb0;
					void main()
					{
					    u_xlatb0 = vs_TEXCOORD1.y<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_CINEMATIC_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					bool u_xlatb0;
					void main()
					{
					    u_xlatb0 = vs_TEXCOORD1.y<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_SELECTED_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					bool u_xlatb0;
					void main()
					{
					    u_xlatb0 = vs_TEXCOORD1.y<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_VOXELAO_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = vs_TEXCOORD2.y * vs_TEXCOORD2.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 5.0 + -4.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz + (-vs_COLOR1.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat3 = vs_TEXCOORD2.x * 0.100000001 + 1.0;
					    SV_Target0.xyz = vec3(u_xlat3) * u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_VOXELAO_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = vs_TEXCOORD2.y * vs_TEXCOORD2.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 5.0 + -4.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz + (-vs_COLOR1.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat3 = vs_TEXCOORD2.x * 0.100000001 + 1.0;
					    SV_Target0.xyz = vec3(u_xlat3) * u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_VOXELAO_ON" "_SELECTED_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = vs_TEXCOORD2.y * vs_TEXCOORD2.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 5.0 + -4.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz + (-vs_COLOR1.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat3 = vs_TEXCOORD2.x * 0.100000001 + 1.0;
					    SV_Target0.xyz = vec3(u_xlat3) * u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_VOXELAO_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					bool u_xlatb0;
					void main()
					{
					    u_xlatb0 = vs_TEXCOORD1.y<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_CINEMATIC_ON" "_VOXELAO_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					bool u_xlatb0;
					void main()
					{
					    u_xlatb0 = vs_TEXCOORD1.y<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_VOXELAO_ON" "_SELECTED_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					bool u_xlatb0;
					void main()
					{
					    u_xlatb0 = vs_TEXCOORD1.y<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_VOXELAO_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = vs_TEXCOORD2.y * vs_TEXCOORD2.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 5.0 + -4.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz + (-vs_COLOR1.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat3 = vs_TEXCOORD2.x * 0.100000001 + 1.0;
					    SV_Target0.xyz = vec3(u_xlat3) * u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_VOXELAO_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = vs_TEXCOORD2.y * vs_TEXCOORD2.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 5.0 + -4.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz + (-vs_COLOR1.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat3 = vs_TEXCOORD2.x * 0.100000001 + 1.0;
					    SV_Target0.xyz = vec3(u_xlat3) * u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_VOXELAO_ON" "_SELECTED_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = vs_TEXCOORD2.y * vs_TEXCOORD2.y + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 5.0 + -4.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz + (-vs_COLOR1.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat3 = vs_TEXCOORD2.x * 0.100000001 + 1.0;
					    SV_Target0.xyz = vec3(u_xlat3) * u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_VOXELAO_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					bool u_xlatb0;
					void main()
					{
					    u_xlatb0 = vs_TEXCOORD1.y<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_CINEMATIC_ON" "_VOXELAO_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					bool u_xlatb0;
					void main()
					{
					    u_xlatb0 = vs_TEXCOORD1.y<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_VOXELAO_ON" "_SELECTED_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					bool u_xlatb0;
					void main()
					{
					    u_xlatb0 = vs_TEXCOORD1.y<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
			}
		}
	}
}