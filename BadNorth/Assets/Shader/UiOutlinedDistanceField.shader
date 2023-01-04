Shader "Ui/OutlinedDistanceField" {
	Properties {
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (1,1,1,1)
		[Toggle] _Fwidth ("Fwidth", Float) = 1
		[Toggle] _AlphaAndColors ("Alpha and colors", Float) = 1
		_PixelWidth ("PixelWidth", Float) = 32
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_ColorMask ("Color Mask", Float) = 15
		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
	}
	SubShader {
		Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Name "Default"
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend One OneMinusSrcAlpha, One OneMinusSrcAlpha
			ColorMask 0 -1
			Cull Off
			Stencil {
				ReadMask 0
				WriteMask 0
				Comp Disabled
				Pass Keep
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 42021
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
						vec4 unused_0_0[65];
						vec4 _MainTex_ST;
						vec4 _Color;
						vec4 unused_0_3[3];
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
					in  vec3 in_NORMAL0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
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
					    u_xlat0 = in_COLOR0 * _Color;
					    vs_COLOR0.xyz = u_xlat0.xyz * abs(in_NORMAL0.zzz);
					    vs_COLOR0.w = u_xlat0.w;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy + vec2(0.5, 1.0);
					    vs_TEXCOORD2.xy = in_TEXCOORD2.xy + vec2(1.0, 0.0);
					    vs_TEXCOORD3.xy = in_POSITION0.xy;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_ALPHAANDCOLORS_ON" }
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
						vec4 unused_0_0[65];
						vec4 _MainTex_ST;
						vec4 _Color;
						vec4 unused_0_3[3];
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
					in  vec3 in_NORMAL0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
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
					    u_xlat0 = in_COLOR0 * _Color;
					    vs_COLOR0.xyz = u_xlat0.xyz * abs(in_NORMAL0.zzz);
					    vs_COLOR0.w = u_xlat0.w;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy + vec2(0.5, 1.0);
					    vs_TEXCOORD2.xy = in_TEXCOORD2.xy + vec2(1.0, 0.0);
					    vs_TEXCOORD3.xy = in_POSITION0.xy;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_FWIDTH_ON" }
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
						vec4 unused_0_0[65];
						vec4 _MainTex_ST;
						vec4 _Color;
						vec4 unused_0_3[3];
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
					in  vec3 in_NORMAL0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
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
					    u_xlat0 = in_COLOR0 * _Color;
					    vs_COLOR0.xyz = u_xlat0.xyz * abs(in_NORMAL0.zzz);
					    vs_COLOR0.w = u_xlat0.w;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy + vec2(0.5, 1.0);
					    vs_TEXCOORD2.xy = in_TEXCOORD2.xy + vec2(1.0, 0.0);
					    vs_TEXCOORD3.xy = in_POSITION0.xy;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_FWIDTH_ON" "_ALPHAANDCOLORS_ON" }
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
						vec4 unused_0_0[65];
						vec4 _MainTex_ST;
						vec4 _Color;
						vec4 unused_0_3[3];
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
					in  vec3 in_NORMAL0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
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
					    u_xlat0 = in_COLOR0 * _Color;
					    vs_COLOR0.xyz = u_xlat0.xyz * abs(in_NORMAL0.zzz);
					    vs_COLOR0.w = u_xlat0.w;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy + vec2(0.5, 1.0);
					    vs_TEXCOORD2.xy = in_TEXCOORD2.xy + vec2(1.0, 0.0);
					    vs_TEXCOORD3.xy = in_POSITION0.xy;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "UNITY_UI_ALPHACLIP" }
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
						vec4 unused_0_0[65];
						vec4 _MainTex_ST;
						vec4 _Color;
						vec4 unused_0_3[3];
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
					in  vec3 in_NORMAL0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
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
					    u_xlat0 = in_COLOR0 * _Color;
					    vs_COLOR0.xyz = u_xlat0.xyz * abs(in_NORMAL0.zzz);
					    vs_COLOR0.w = u_xlat0.w;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy + vec2(0.5, 1.0);
					    vs_TEXCOORD2.xy = in_TEXCOORD2.xy + vec2(1.0, 0.0);
					    vs_TEXCOORD3.xy = in_POSITION0.xy;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "UNITY_UI_ALPHACLIP" "_ALPHAANDCOLORS_ON" }
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
						vec4 unused_0_0[65];
						vec4 _MainTex_ST;
						vec4 _Color;
						vec4 unused_0_3[3];
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
					in  vec3 in_NORMAL0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
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
					    u_xlat0 = in_COLOR0 * _Color;
					    vs_COLOR0.xyz = u_xlat0.xyz * abs(in_NORMAL0.zzz);
					    vs_COLOR0.w = u_xlat0.w;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy + vec2(0.5, 1.0);
					    vs_TEXCOORD2.xy = in_TEXCOORD2.xy + vec2(1.0, 0.0);
					    vs_TEXCOORD3.xy = in_POSITION0.xy;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "UNITY_UI_ALPHACLIP" "_FWIDTH_ON" }
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
						vec4 unused_0_0[65];
						vec4 _MainTex_ST;
						vec4 _Color;
						vec4 unused_0_3[3];
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
					in  vec3 in_NORMAL0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
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
					    u_xlat0 = in_COLOR0 * _Color;
					    vs_COLOR0.xyz = u_xlat0.xyz * abs(in_NORMAL0.zzz);
					    vs_COLOR0.w = u_xlat0.w;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy + vec2(0.5, 1.0);
					    vs_TEXCOORD2.xy = in_TEXCOORD2.xy + vec2(1.0, 0.0);
					    vs_TEXCOORD3.xy = in_POSITION0.xy;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "UNITY_UI_ALPHACLIP" "_FWIDTH_ON" "_ALPHAANDCOLORS_ON" }
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
						vec4 unused_0_0[65];
						vec4 _MainTex_ST;
						vec4 _Color;
						vec4 unused_0_3[3];
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
					in  vec3 in_NORMAL0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
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
					    u_xlat0 = in_COLOR0 * _Color;
					    vs_COLOR0.xyz = u_xlat0.xyz * abs(in_NORMAL0.zzz);
					    vs_COLOR0.w = u_xlat0.w;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy + vec2(0.5, 1.0);
					    vs_TEXCOORD2.xy = in_TEXCOORD2.xy + vec2(1.0, 0.0);
					    vs_TEXCOORD3.xy = in_POSITION0.xy;
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[9];
						vec4 _ClipRect;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[5];
						vec4 _ProjectionParams;
						vec4 unused_1_2[3];
					};
					uniform  sampler2D _MainTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bvec4 u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					float u_xlat5;
					int u_xlati5;
					bool u_xlatb5;
					float u_xlat6;
					float u_xlat10;
					int u_xlati10;
					float u_xlat11;
					float u_xlat16;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat5 = min(u_xlat0.w, vs_COLOR0.w);
					    u_xlat5 = u_xlat5 + -0.00999999978;
					    u_xlatb5 = u_xlat5<0.0;
					    if(((int(u_xlatb5) * int(0xffffffffu)))!=0){discard;}
					    u_xlati5 = int((0.0<_ProjectionParams.x) ? 0xFFFFFFFFu : uint(0));
					    u_xlati10 = int((_ProjectionParams.x<0.0) ? 0xFFFFFFFFu : uint(0));
					    u_xlati5 = u_xlati5 + (-u_xlati10);
					    u_xlat5 = float(u_xlati5);
					    u_xlat5 = u_xlat5 * u_xlat0.x;
					    u_xlat1.y = dFdy(u_xlat5);
					    u_xlat5 = u_xlat1.y + u_xlat1.y;
					    u_xlat1.x = dFdx(u_xlat0.x);
					    u_xlat5 = u_xlat1.x * 2.0 + (-u_xlat5);
					    u_xlat10 = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat10 = sqrt(u_xlat10);
					    u_xlat1.x = fract(vs_TEXCOORD2.y);
					    u_xlat1.x = u_xlat1.x * 1.01010096;
					    u_xlat6 = (-vs_TEXCOORD1.x) + 1.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat5 = u_xlat5 * u_xlat1.x + u_xlat6;
					    u_xlat11 = max(vs_TEXCOORD1.y, 1.0);
					    u_xlat11 = u_xlat11 * 0.5;
					    u_xlat16 = _AAFactor + 1.0;
					    u_xlat2.x = u_xlat16 * (-u_xlat11);
					    u_xlat11 = u_xlat16 * u_xlat11;
					    u_xlat11 = u_xlat10 * u_xlat11;
					    u_xlat11 = u_xlat11 * _LineWidth + u_xlat6;
					    u_xlat11 = clamp(u_xlat11, 0.0, 1.0);
					    u_xlat16 = u_xlat10 * u_xlat2.x;
					    u_xlat5 = u_xlat16 * _LineWidth + u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat6 = u_xlat16 * _LineWidth + u_xlat6;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat16 = (-u_xlat5) + 1.0;
					    u_xlat1.w = u_xlat10 * u_xlat16 + u_xlat5;
					    u_xlat5 = (-u_xlat10) * u_xlat5 + u_xlat5;
					    u_xlat5 = max(u_xlat5, 0.0);
					    u_xlat1.xw = min(u_xlat1.xw, vec2(1.0, 1.0));
					    u_xlat16 = (-u_xlat5) + u_xlat1.w;
					    u_xlat5 = (-u_xlat5) + u_xlat0.x;
					    u_xlat5 = u_xlat5 / u_xlat16;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat5 = u_xlat1.x * u_xlat5;
					    u_xlat5 = u_xlat5 * 0.200000003;
					    u_xlat5 = u_xlat5 * vs_COLOR0.w;
					    u_xlat1.x = floor(vs_TEXCOORD2.y);
					    u_xlat1.x = u_xlat1.x + -1.0;
					    u_xlat1.x = max(u_xlat1.x, 0.0);
					    u_xlat16 = u_xlat0.x * u_xlat1.x;
					    u_xlat16 = fract(u_xlat16);
					    u_xlat16 = u_xlat16 + -0.5;
					    u_xlat1.x = abs(u_xlat16) / u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat10;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat1.x = u_xlat1.x * 0.300000012;
					    u_xlat2.xyz = vs_COLOR0.xyz;
					    u_xlat2.w = 1.0;
					    u_xlat3 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = u_xlat1.xxxx * u_xlat3 + u_xlat2;
					    u_xlat1.x = vs_TEXCOORD2.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat2.xyz;
					    u_xlat16 = vs_TEXCOORD1.y;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat16) * (-u_xlat3.xyz) + u_xlat3.xyz;
					    u_xlat3.w = 1.0;
					    u_xlat3 = (-vec4(u_xlat5)) * vec4(0.0, 0.0, 0.0, 1.0) + u_xlat3;
					    u_xlat4 = vec4(u_xlat5) * vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat5 = (-u_xlat6) + 1.0;
					    u_xlat5 = u_xlat10 * u_xlat5 + u_xlat6;
					    u_xlat6 = (-u_xlat10) * u_xlat6 + u_xlat6;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat5 = min(u_xlat5, 1.0);
					    u_xlat5 = (-u_xlat6) + u_xlat5;
					    u_xlat6 = u_xlat0.x + (-u_xlat6);
					    u_xlat5 = u_xlat6 / u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat3 = vec4(u_xlat5) * u_xlat3 + u_xlat4;
					    u_xlat2 = u_xlat2 * u_xlat1.xxxx + (-u_xlat3);
					    u_xlat5 = (-u_xlat11) + 1.0;
					    u_xlat5 = u_xlat10 * u_xlat5 + u_xlat11;
					    u_xlat10 = (-u_xlat10) * u_xlat11 + u_xlat11;
					    u_xlat10 = max(u_xlat10, 0.0);
					    u_xlat0.y = min(u_xlat5, 1.0);
					    u_xlat0.xy = (-vec2(u_xlat10)) + u_xlat0.xy;
					    u_xlat0.x = u_xlat0.x / u_xlat0.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1 = u_xlat0.xxxx * u_xlat2 + u_xlat3;
					    u_xlat0 = u_xlat0.wwww * u_xlat1;
					    u_xlat0 = u_xlat0 * vs_COLOR0.wwww;
					    u_xlatb1.xy = greaterThanEqual(vs_TEXCOORD3.xyxx, _ClipRect.xyxx).xy;
					    u_xlatb1.zw = greaterThanEqual(_ClipRect.zzzw, vs_TEXCOORD3.xxxy).zw;
					    u_xlat1.x = u_xlatb1.x ? float(1.0) : 0.0;
					    u_xlat1.y = u_xlatb1.y ? float(1.0) : 0.0;
					    u_xlat1.z = u_xlatb1.z ? float(1.0) : 0.0;
					    u_xlat1.w = u_xlatb1.w ? float(1.0) : 0.0;
					;
					    u_xlat1.xy = u_xlat1.zw * u_xlat1.xy;
					    u_xlat1.x = u_xlat1.y * u_xlat1.x;
					    SV_Target0 = u_xlat0 * u_xlat1.xxxx;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_ALPHAANDCOLORS_ON" }
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
						vec4 unused_0_4[9];
						vec4 _ClipRect;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[5];
						vec4 _ProjectionParams;
						vec4 unused_1_2[3];
					};
					uniform  sampler2D _MainTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					int u_xlati1;
					bvec4 u_xlatb1;
					vec3 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					float u_xlat6;
					float u_xlat7;
					int u_xlati7;
					vec3 u_xlat8;
					float u_xlat12;
					float u_xlat13;
					float u_xlat19;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = min(u_xlat0.w, vs_COLOR0.w);
					    u_xlat1.x = u_xlat1.x + -0.00999999978;
					    u_xlatb1.x = u_xlat1.x<0.0;
					    if(((int(u_xlatb1.x) * int(0xffffffffu)))!=0){discard;}
					    u_xlati1 = int((0.0<_ProjectionParams.x) ? 0xFFFFFFFFu : uint(0));
					    u_xlati7 = int((_ProjectionParams.x<0.0) ? 0xFFFFFFFFu : uint(0));
					    u_xlati1 = u_xlati1 + (-u_xlati7);
					    u_xlat1.x = float(u_xlati1);
					    u_xlat1.x = u_xlat0.w * u_xlat1.x;
					    u_xlat1.y = dFdy(u_xlat1.x);
					    u_xlat13 = u_xlat1.y + u_xlat1.y;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat13 = u_xlat1.x * 2.0 + (-u_xlat13);
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat7 = fract(vs_TEXCOORD2.y);
					    u_xlat7 = u_xlat7 * 1.01010096;
					    u_xlat19 = (-vs_TEXCOORD1.x) + 1.0;
					    u_xlat19 = clamp(u_xlat19, 0.0, 1.0);
					    u_xlat13 = u_xlat13 * u_xlat7 + u_xlat19;
					    u_xlat7 = min(u_xlat7, 1.0);
					    u_xlat2.x = max(vs_TEXCOORD1.y, 1.0);
					    u_xlat2.x = u_xlat2.x * 0.5;
					    u_xlat8.x = _AAFactor + 1.0;
					    u_xlat2.z = u_xlat8.x * (-u_xlat2.x);
					    u_xlat2.x = u_xlat8.x * u_xlat2.x;
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xz;
					    u_xlat2.x = u_xlat2.x * _LineWidth + u_xlat19;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat13 = u_xlat2.y * _LineWidth + u_xlat13;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat19 = u_xlat2.y * _LineWidth + u_xlat19;
					    u_xlat19 = clamp(u_xlat19, 0.0, 1.0);
					    u_xlat8.x = (-u_xlat13) + 1.0;
					    u_xlat8.x = u_xlat1.x * u_xlat8.x + u_xlat13;
					    u_xlat13 = (-u_xlat1.x) * u_xlat13 + u_xlat13;
					    u_xlat13 = max(u_xlat13, 0.0);
					    u_xlat8.x = min(u_xlat8.x, 1.0);
					    u_xlat8.x = (-u_xlat13) + u_xlat8.x;
					    u_xlat13 = u_xlat0.w + (-u_xlat13);
					    u_xlat13 = u_xlat13 / u_xlat8.x;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat7 = u_xlat7 * u_xlat13;
					    u_xlat7 = u_xlat7 * 0.200000003;
					    u_xlat7 = u_xlat7 * vs_COLOR0.w;
					    u_xlat13 = floor(vs_TEXCOORD2.y);
					    u_xlat13 = u_xlat13 + -1.0;
					    u_xlat13 = max(u_xlat13, 0.0);
					    u_xlat8.x = u_xlat0.w * u_xlat13;
					    u_xlat8.x = fract(u_xlat8.x);
					    u_xlat8.x = u_xlat8.x + -0.5;
					    u_xlat13 = abs(u_xlat8.x) / u_xlat13;
					    u_xlat13 = u_xlat13 / u_xlat1.x;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat13 = (-u_xlat13) + 1.0;
					    u_xlat13 = u_xlat13 * 0.300000012;
					    u_xlat8.xyz = vs_COLOR0.xyz;
					    u_xlat8.xyz = vec3(u_xlat13) * (-u_xlat8.xyz) + u_xlat8.xyz;
					    u_xlat3.xyz = u_xlat0.xyz * u_xlat8.xyz;
					    u_xlat0.x = vs_TEXCOORD2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = vs_TEXCOORD1.y;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat4.xyz = vec3(u_xlat6) * (-u_xlat8.xyz) + u_xlat8.xyz;
					    u_xlat4.w = 1.0;
					    u_xlat4 = (-vec4(u_xlat7)) * vec4(0.0, 0.0, 0.0, 1.0) + u_xlat4;
					    u_xlat5 = vec4(u_xlat7) * vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat6 = (-u_xlat19) + 1.0;
					    u_xlat6 = u_xlat1.x * u_xlat6 + u_xlat19;
					    u_xlat12 = (-u_xlat1.x) * u_xlat19 + u_xlat19;
					    u_xlat12 = max(u_xlat12, 0.0);
					    u_xlat6 = min(u_xlat6, 1.0);
					    u_xlat6 = (-u_xlat12) + u_xlat6;
					    u_xlat12 = (-u_xlat12) + u_xlat0.w;
					    u_xlat6 = u_xlat12 / u_xlat6;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat4 = vec4(u_xlat6) * u_xlat4 + u_xlat5;
					    u_xlat3.w = 1.0;
					    u_xlat3 = u_xlat3 * u_xlat0.xxxx + (-u_xlat4);
					    u_xlat0.x = (-u_xlat2.x) + 1.0;
					    u_xlat0.x = u_xlat1.x * u_xlat0.x + u_xlat2.x;
					    u_xlat6 = (-u_xlat1.x) * u_xlat2.x + u_xlat2.x;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat0.x = (-u_xlat6) + u_xlat0.x;
					    u_xlat6 = (-u_xlat6) + u_xlat0.w;
					    u_xlat0.x = u_xlat6 / u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0 = u_xlat0.xxxx * u_xlat3 + u_xlat4;
					    u_xlat0 = u_xlat0 * vs_COLOR0.wwww;
					    u_xlatb1.xy = greaterThanEqual(vs_TEXCOORD3.xyxx, _ClipRect.xyxx).xy;
					    u_xlatb1.zw = greaterThanEqual(_ClipRect.zzzw, vs_TEXCOORD3.xxxy).zw;
					    u_xlat1.x = u_xlatb1.x ? float(1.0) : 0.0;
					    u_xlat1.y = u_xlatb1.y ? float(1.0) : 0.0;
					    u_xlat1.z = u_xlatb1.z ? float(1.0) : 0.0;
					    u_xlat1.w = u_xlatb1.w ? float(1.0) : 0.0;
					;
					    u_xlat1.xy = u_xlat1.zw * u_xlat1.xy;
					    u_xlat1.x = u_xlat1.y * u_xlat1.x;
					    SV_Target0 = u_xlat0 * u_xlat1.xxxx;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_FWIDTH_ON" }
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
						vec4 unused_0_4[9];
						vec4 _ClipRect;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[5];
						vec4 _ProjectionParams;
						vec4 unused_1_2[3];
					};
					uniform  sampler2D _MainTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bvec4 u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					float u_xlat5;
					int u_xlati5;
					bool u_xlatb5;
					float u_xlat6;
					float u_xlat10;
					int u_xlati10;
					float u_xlat11;
					float u_xlat16;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat5 = min(u_xlat0.w, vs_COLOR0.w);
					    u_xlat5 = u_xlat5 + -0.00999999978;
					    u_xlatb5 = u_xlat5<0.0;
					    if(((int(u_xlatb5) * int(0xffffffffu)))!=0){discard;}
					    u_xlati5 = int((0.0<_ProjectionParams.x) ? 0xFFFFFFFFu : uint(0));
					    u_xlati10 = int((_ProjectionParams.x<0.0) ? 0xFFFFFFFFu : uint(0));
					    u_xlati5 = u_xlati5 + (-u_xlati10);
					    u_xlat5 = float(u_xlati5);
					    u_xlat5 = u_xlat5 * u_xlat0.x;
					    u_xlat1.y = dFdy(u_xlat5);
					    u_xlat5 = u_xlat1.y + u_xlat1.y;
					    u_xlat1.x = dFdx(u_xlat0.x);
					    u_xlat5 = u_xlat1.x * 2.0 + (-u_xlat5);
					    u_xlat10 = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat10 = sqrt(u_xlat10);
					    u_xlat1.x = fract(vs_TEXCOORD2.y);
					    u_xlat1.x = u_xlat1.x * 1.01010096;
					    u_xlat6 = (-vs_TEXCOORD1.x) + 1.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat5 = u_xlat5 * u_xlat1.x + u_xlat6;
					    u_xlat11 = max(vs_TEXCOORD1.y, 1.0);
					    u_xlat11 = u_xlat11 * 0.5;
					    u_xlat16 = _AAFactor + 1.0;
					    u_xlat2.x = u_xlat16 * (-u_xlat11);
					    u_xlat11 = u_xlat16 * u_xlat11;
					    u_xlat11 = u_xlat10 * u_xlat11;
					    u_xlat11 = u_xlat11 * _LineWidth + u_xlat6;
					    u_xlat11 = clamp(u_xlat11, 0.0, 1.0);
					    u_xlat16 = u_xlat10 * u_xlat2.x;
					    u_xlat5 = u_xlat16 * _LineWidth + u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat6 = u_xlat16 * _LineWidth + u_xlat6;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat16 = (-u_xlat5) + 1.0;
					    u_xlat1.w = u_xlat10 * u_xlat16 + u_xlat5;
					    u_xlat5 = (-u_xlat10) * u_xlat5 + u_xlat5;
					    u_xlat5 = max(u_xlat5, 0.0);
					    u_xlat1.xw = min(u_xlat1.xw, vec2(1.0, 1.0));
					    u_xlat16 = (-u_xlat5) + u_xlat1.w;
					    u_xlat5 = (-u_xlat5) + u_xlat0.x;
					    u_xlat5 = u_xlat5 / u_xlat16;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat5 = u_xlat1.x * u_xlat5;
					    u_xlat5 = u_xlat5 * 0.200000003;
					    u_xlat5 = u_xlat5 * vs_COLOR0.w;
					    u_xlat1.x = floor(vs_TEXCOORD2.y);
					    u_xlat1.x = u_xlat1.x + -1.0;
					    u_xlat1.x = max(u_xlat1.x, 0.0);
					    u_xlat16 = u_xlat0.x * u_xlat1.x;
					    u_xlat16 = fract(u_xlat16);
					    u_xlat16 = u_xlat16 + -0.5;
					    u_xlat1.x = abs(u_xlat16) / u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat10;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat1.x = u_xlat1.x * 0.300000012;
					    u_xlat2.xyz = vs_COLOR0.xyz;
					    u_xlat2.w = 1.0;
					    u_xlat3 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = u_xlat1.xxxx * u_xlat3 + u_xlat2;
					    u_xlat1.x = vs_TEXCOORD2.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat2.xyz;
					    u_xlat16 = vs_TEXCOORD1.y;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat16) * (-u_xlat3.xyz) + u_xlat3.xyz;
					    u_xlat3.w = 1.0;
					    u_xlat3 = (-vec4(u_xlat5)) * vec4(0.0, 0.0, 0.0, 1.0) + u_xlat3;
					    u_xlat4 = vec4(u_xlat5) * vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat5 = (-u_xlat6) + 1.0;
					    u_xlat5 = u_xlat10 * u_xlat5 + u_xlat6;
					    u_xlat6 = (-u_xlat10) * u_xlat6 + u_xlat6;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat5 = min(u_xlat5, 1.0);
					    u_xlat5 = (-u_xlat6) + u_xlat5;
					    u_xlat6 = u_xlat0.x + (-u_xlat6);
					    u_xlat5 = u_xlat6 / u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat3 = vec4(u_xlat5) * u_xlat3 + u_xlat4;
					    u_xlat2 = u_xlat2 * u_xlat1.xxxx + (-u_xlat3);
					    u_xlat5 = (-u_xlat11) + 1.0;
					    u_xlat5 = u_xlat10 * u_xlat5 + u_xlat11;
					    u_xlat10 = (-u_xlat10) * u_xlat11 + u_xlat11;
					    u_xlat10 = max(u_xlat10, 0.0);
					    u_xlat0.y = min(u_xlat5, 1.0);
					    u_xlat0.xy = (-vec2(u_xlat10)) + u_xlat0.xy;
					    u_xlat0.x = u_xlat0.x / u_xlat0.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1 = u_xlat0.xxxx * u_xlat2 + u_xlat3;
					    u_xlat0 = u_xlat0.wwww * u_xlat1;
					    u_xlat0 = u_xlat0 * vs_COLOR0.wwww;
					    u_xlatb1.xy = greaterThanEqual(vs_TEXCOORD3.xyxx, _ClipRect.xyxx).xy;
					    u_xlatb1.zw = greaterThanEqual(_ClipRect.zzzw, vs_TEXCOORD3.xxxy).zw;
					    u_xlat1.x = u_xlatb1.x ? float(1.0) : 0.0;
					    u_xlat1.y = u_xlatb1.y ? float(1.0) : 0.0;
					    u_xlat1.z = u_xlatb1.z ? float(1.0) : 0.0;
					    u_xlat1.w = u_xlatb1.w ? float(1.0) : 0.0;
					;
					    u_xlat1.xy = u_xlat1.zw * u_xlat1.xy;
					    u_xlat1.x = u_xlat1.y * u_xlat1.x;
					    SV_Target0 = u_xlat0 * u_xlat1.xxxx;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_FWIDTH_ON" "_ALPHAANDCOLORS_ON" }
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
						vec4 unused_0_4[9];
						vec4 _ClipRect;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[5];
						vec4 _ProjectionParams;
						vec4 unused_1_2[3];
					};
					uniform  sampler2D _MainTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					int u_xlati1;
					bvec4 u_xlatb1;
					vec3 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					float u_xlat6;
					float u_xlat7;
					int u_xlati7;
					vec3 u_xlat8;
					float u_xlat12;
					float u_xlat13;
					float u_xlat19;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = min(u_xlat0.w, vs_COLOR0.w);
					    u_xlat1.x = u_xlat1.x + -0.00999999978;
					    u_xlatb1.x = u_xlat1.x<0.0;
					    if(((int(u_xlatb1.x) * int(0xffffffffu)))!=0){discard;}
					    u_xlati1 = int((0.0<_ProjectionParams.x) ? 0xFFFFFFFFu : uint(0));
					    u_xlati7 = int((_ProjectionParams.x<0.0) ? 0xFFFFFFFFu : uint(0));
					    u_xlati1 = u_xlati1 + (-u_xlati7);
					    u_xlat1.x = float(u_xlati1);
					    u_xlat1.x = u_xlat0.w * u_xlat1.x;
					    u_xlat1.y = dFdy(u_xlat1.x);
					    u_xlat13 = u_xlat1.y + u_xlat1.y;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat13 = u_xlat1.x * 2.0 + (-u_xlat13);
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat7 = fract(vs_TEXCOORD2.y);
					    u_xlat7 = u_xlat7 * 1.01010096;
					    u_xlat19 = (-vs_TEXCOORD1.x) + 1.0;
					    u_xlat19 = clamp(u_xlat19, 0.0, 1.0);
					    u_xlat13 = u_xlat13 * u_xlat7 + u_xlat19;
					    u_xlat7 = min(u_xlat7, 1.0);
					    u_xlat2.x = max(vs_TEXCOORD1.y, 1.0);
					    u_xlat2.x = u_xlat2.x * 0.5;
					    u_xlat8.x = _AAFactor + 1.0;
					    u_xlat2.z = u_xlat8.x * (-u_xlat2.x);
					    u_xlat2.x = u_xlat8.x * u_xlat2.x;
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xz;
					    u_xlat2.x = u_xlat2.x * _LineWidth + u_xlat19;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat13 = u_xlat2.y * _LineWidth + u_xlat13;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat19 = u_xlat2.y * _LineWidth + u_xlat19;
					    u_xlat19 = clamp(u_xlat19, 0.0, 1.0);
					    u_xlat8.x = (-u_xlat13) + 1.0;
					    u_xlat8.x = u_xlat1.x * u_xlat8.x + u_xlat13;
					    u_xlat13 = (-u_xlat1.x) * u_xlat13 + u_xlat13;
					    u_xlat13 = max(u_xlat13, 0.0);
					    u_xlat8.x = min(u_xlat8.x, 1.0);
					    u_xlat8.x = (-u_xlat13) + u_xlat8.x;
					    u_xlat13 = u_xlat0.w + (-u_xlat13);
					    u_xlat13 = u_xlat13 / u_xlat8.x;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat7 = u_xlat7 * u_xlat13;
					    u_xlat7 = u_xlat7 * 0.200000003;
					    u_xlat7 = u_xlat7 * vs_COLOR0.w;
					    u_xlat13 = floor(vs_TEXCOORD2.y);
					    u_xlat13 = u_xlat13 + -1.0;
					    u_xlat13 = max(u_xlat13, 0.0);
					    u_xlat8.x = u_xlat0.w * u_xlat13;
					    u_xlat8.x = fract(u_xlat8.x);
					    u_xlat8.x = u_xlat8.x + -0.5;
					    u_xlat13 = abs(u_xlat8.x) / u_xlat13;
					    u_xlat13 = u_xlat13 / u_xlat1.x;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat13 = (-u_xlat13) + 1.0;
					    u_xlat13 = u_xlat13 * 0.300000012;
					    u_xlat8.xyz = vs_COLOR0.xyz;
					    u_xlat8.xyz = vec3(u_xlat13) * (-u_xlat8.xyz) + u_xlat8.xyz;
					    u_xlat3.xyz = u_xlat0.xyz * u_xlat8.xyz;
					    u_xlat0.x = vs_TEXCOORD2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = vs_TEXCOORD1.y;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat4.xyz = vec3(u_xlat6) * (-u_xlat8.xyz) + u_xlat8.xyz;
					    u_xlat4.w = 1.0;
					    u_xlat4 = (-vec4(u_xlat7)) * vec4(0.0, 0.0, 0.0, 1.0) + u_xlat4;
					    u_xlat5 = vec4(u_xlat7) * vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat6 = (-u_xlat19) + 1.0;
					    u_xlat6 = u_xlat1.x * u_xlat6 + u_xlat19;
					    u_xlat12 = (-u_xlat1.x) * u_xlat19 + u_xlat19;
					    u_xlat12 = max(u_xlat12, 0.0);
					    u_xlat6 = min(u_xlat6, 1.0);
					    u_xlat6 = (-u_xlat12) + u_xlat6;
					    u_xlat12 = (-u_xlat12) + u_xlat0.w;
					    u_xlat6 = u_xlat12 / u_xlat6;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat4 = vec4(u_xlat6) * u_xlat4 + u_xlat5;
					    u_xlat3.w = 1.0;
					    u_xlat3 = u_xlat3 * u_xlat0.xxxx + (-u_xlat4);
					    u_xlat0.x = (-u_xlat2.x) + 1.0;
					    u_xlat0.x = u_xlat1.x * u_xlat0.x + u_xlat2.x;
					    u_xlat6 = (-u_xlat1.x) * u_xlat2.x + u_xlat2.x;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat0.x = (-u_xlat6) + u_xlat0.x;
					    u_xlat6 = (-u_xlat6) + u_xlat0.w;
					    u_xlat0.x = u_xlat6 / u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0 = u_xlat0.xxxx * u_xlat3 + u_xlat4;
					    u_xlat0 = u_xlat0 * vs_COLOR0.wwww;
					    u_xlatb1.xy = greaterThanEqual(vs_TEXCOORD3.xyxx, _ClipRect.xyxx).xy;
					    u_xlatb1.zw = greaterThanEqual(_ClipRect.zzzw, vs_TEXCOORD3.xxxy).zw;
					    u_xlat1.x = u_xlatb1.x ? float(1.0) : 0.0;
					    u_xlat1.y = u_xlatb1.y ? float(1.0) : 0.0;
					    u_xlat1.z = u_xlatb1.z ? float(1.0) : 0.0;
					    u_xlat1.w = u_xlatb1.w ? float(1.0) : 0.0;
					;
					    u_xlat1.xy = u_xlat1.zw * u_xlat1.xy;
					    u_xlat1.x = u_xlat1.y * u_xlat1.x;
					    SV_Target0 = u_xlat0 * u_xlat1.xxxx;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "UNITY_UI_ALPHACLIP" }
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
						vec4 unused_0_4[9];
						vec4 _ClipRect;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[5];
						vec4 _ProjectionParams;
						vec4 unused_1_2[3];
					};
					uniform  sampler2D _MainTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					bvec4 u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					float u_xlat5;
					int u_xlati5;
					bool u_xlatb5;
					float u_xlat6;
					float u_xlat10;
					int u_xlati10;
					float u_xlat11;
					float u_xlat16;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat5 = min(u_xlat0.w, vs_COLOR0.w);
					    u_xlat5 = u_xlat5 + -0.00999999978;
					    u_xlatb5 = u_xlat5<0.0;
					    if(((int(u_xlatb5) * int(0xffffffffu)))!=0){discard;}
					    u_xlati5 = int((0.0<_ProjectionParams.x) ? 0xFFFFFFFFu : uint(0));
					    u_xlati10 = int((_ProjectionParams.x<0.0) ? 0xFFFFFFFFu : uint(0));
					    u_xlati5 = u_xlati5 + (-u_xlati10);
					    u_xlat5 = float(u_xlati5);
					    u_xlat5 = u_xlat5 * u_xlat0.x;
					    u_xlat1.y = dFdy(u_xlat5);
					    u_xlat5 = u_xlat1.y + u_xlat1.y;
					    u_xlat1.x = dFdx(u_xlat0.x);
					    u_xlat5 = u_xlat1.x * 2.0 + (-u_xlat5);
					    u_xlat10 = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat10 = sqrt(u_xlat10);
					    u_xlat1.x = fract(vs_TEXCOORD2.y);
					    u_xlat1.x = u_xlat1.x * 1.01010096;
					    u_xlat6 = (-vs_TEXCOORD1.x) + 1.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat5 = u_xlat5 * u_xlat1.x + u_xlat6;
					    u_xlat11 = max(vs_TEXCOORD1.y, 1.0);
					    u_xlat11 = u_xlat11 * 0.5;
					    u_xlat16 = _AAFactor + 1.0;
					    u_xlat2.x = u_xlat16 * (-u_xlat11);
					    u_xlat11 = u_xlat16 * u_xlat11;
					    u_xlat11 = u_xlat10 * u_xlat11;
					    u_xlat11 = u_xlat11 * _LineWidth + u_xlat6;
					    u_xlat11 = clamp(u_xlat11, 0.0, 1.0);
					    u_xlat16 = u_xlat10 * u_xlat2.x;
					    u_xlat5 = u_xlat16 * _LineWidth + u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat6 = u_xlat16 * _LineWidth + u_xlat6;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat16 = (-u_xlat5) + 1.0;
					    u_xlat1.w = u_xlat10 * u_xlat16 + u_xlat5;
					    u_xlat5 = (-u_xlat10) * u_xlat5 + u_xlat5;
					    u_xlat5 = max(u_xlat5, 0.0);
					    u_xlat1.xw = min(u_xlat1.xw, vec2(1.0, 1.0));
					    u_xlat16 = (-u_xlat5) + u_xlat1.w;
					    u_xlat5 = (-u_xlat5) + u_xlat0.x;
					    u_xlat5 = u_xlat5 / u_xlat16;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat5 = u_xlat1.x * u_xlat5;
					    u_xlat5 = u_xlat5 * 0.200000003;
					    u_xlat5 = u_xlat5 * vs_COLOR0.w;
					    u_xlat1.x = floor(vs_TEXCOORD2.y);
					    u_xlat1.x = u_xlat1.x + -1.0;
					    u_xlat1.x = max(u_xlat1.x, 0.0);
					    u_xlat16 = u_xlat0.x * u_xlat1.x;
					    u_xlat16 = fract(u_xlat16);
					    u_xlat16 = u_xlat16 + -0.5;
					    u_xlat1.x = abs(u_xlat16) / u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat10;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat1.x = u_xlat1.x * 0.300000012;
					    u_xlat2.xyz = vs_COLOR0.xyz;
					    u_xlat2.w = 1.0;
					    u_xlat3 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = u_xlat1.xxxx * u_xlat3 + u_xlat2;
					    u_xlat1.x = vs_TEXCOORD2.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat2.xyz;
					    u_xlat16 = vs_TEXCOORD1.y;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat16) * (-u_xlat3.xyz) + u_xlat3.xyz;
					    u_xlat3.w = 1.0;
					    u_xlat3 = (-vec4(u_xlat5)) * vec4(0.0, 0.0, 0.0, 1.0) + u_xlat3;
					    u_xlat4 = vec4(u_xlat5) * vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat5 = (-u_xlat6) + 1.0;
					    u_xlat5 = u_xlat10 * u_xlat5 + u_xlat6;
					    u_xlat6 = (-u_xlat10) * u_xlat6 + u_xlat6;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat5 = min(u_xlat5, 1.0);
					    u_xlat5 = (-u_xlat6) + u_xlat5;
					    u_xlat6 = u_xlat0.x + (-u_xlat6);
					    u_xlat5 = u_xlat6 / u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat3 = vec4(u_xlat5) * u_xlat3 + u_xlat4;
					    u_xlat2 = u_xlat2 * u_xlat1.xxxx + (-u_xlat3);
					    u_xlat5 = (-u_xlat11) + 1.0;
					    u_xlat5 = u_xlat10 * u_xlat5 + u_xlat11;
					    u_xlat10 = (-u_xlat10) * u_xlat11 + u_xlat11;
					    u_xlat10 = max(u_xlat10, 0.0);
					    u_xlat0.y = min(u_xlat5, 1.0);
					    u_xlat0.xy = (-vec2(u_xlat10)) + u_xlat0.xy;
					    u_xlat0.x = u_xlat0.x / u_xlat0.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1 = u_xlat0.xxxx * u_xlat2 + u_xlat3;
					    u_xlat0 = u_xlat0.wwww * u_xlat1;
					    u_xlat0 = u_xlat0 * vs_COLOR0.wwww;
					    u_xlatb1.xy = greaterThanEqual(vs_TEXCOORD3.xyxx, _ClipRect.xyxx).xy;
					    u_xlatb1.zw = greaterThanEqual(_ClipRect.zzzw, vs_TEXCOORD3.xxxy).zw;
					    u_xlat1.x = u_xlatb1.x ? float(1.0) : 0.0;
					    u_xlat1.y = u_xlatb1.y ? float(1.0) : 0.0;
					    u_xlat1.z = u_xlatb1.z ? float(1.0) : 0.0;
					    u_xlat1.w = u_xlatb1.w ? float(1.0) : 0.0;
					;
					    u_xlat1.xy = u_xlat1.zw * u_xlat1.xy;
					    u_xlat1.x = u_xlat1.y * u_xlat1.x;
					    u_xlat6 = u_xlat0.w * u_xlat1.x + -0.00100000005;
					    u_xlat0 = u_xlat0 * u_xlat1.xxxx;
					    SV_Target0 = u_xlat0;
					    u_xlatb0 = u_xlat6<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "UNITY_UI_ALPHACLIP" "_ALPHAANDCOLORS_ON" }
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
						vec4 unused_0_4[9];
						vec4 _ClipRect;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[5];
						vec4 _ProjectionParams;
						vec4 unused_1_2[3];
					};
					uniform  sampler2D _MainTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					int u_xlati1;
					bvec4 u_xlatb1;
					vec3 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					float u_xlat6;
					float u_xlat7;
					int u_xlati7;
					vec3 u_xlat8;
					float u_xlat12;
					float u_xlat13;
					float u_xlat19;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = min(u_xlat0.w, vs_COLOR0.w);
					    u_xlat1.x = u_xlat1.x + -0.00999999978;
					    u_xlatb1.x = u_xlat1.x<0.0;
					    if(((int(u_xlatb1.x) * int(0xffffffffu)))!=0){discard;}
					    u_xlati1 = int((0.0<_ProjectionParams.x) ? 0xFFFFFFFFu : uint(0));
					    u_xlati7 = int((_ProjectionParams.x<0.0) ? 0xFFFFFFFFu : uint(0));
					    u_xlati1 = u_xlati1 + (-u_xlati7);
					    u_xlat1.x = float(u_xlati1);
					    u_xlat1.x = u_xlat0.w * u_xlat1.x;
					    u_xlat1.y = dFdy(u_xlat1.x);
					    u_xlat13 = u_xlat1.y + u_xlat1.y;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat13 = u_xlat1.x * 2.0 + (-u_xlat13);
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat7 = fract(vs_TEXCOORD2.y);
					    u_xlat7 = u_xlat7 * 1.01010096;
					    u_xlat19 = (-vs_TEXCOORD1.x) + 1.0;
					    u_xlat19 = clamp(u_xlat19, 0.0, 1.0);
					    u_xlat13 = u_xlat13 * u_xlat7 + u_xlat19;
					    u_xlat7 = min(u_xlat7, 1.0);
					    u_xlat2.x = max(vs_TEXCOORD1.y, 1.0);
					    u_xlat2.x = u_xlat2.x * 0.5;
					    u_xlat8.x = _AAFactor + 1.0;
					    u_xlat2.z = u_xlat8.x * (-u_xlat2.x);
					    u_xlat2.x = u_xlat8.x * u_xlat2.x;
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xz;
					    u_xlat2.x = u_xlat2.x * _LineWidth + u_xlat19;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat13 = u_xlat2.y * _LineWidth + u_xlat13;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat19 = u_xlat2.y * _LineWidth + u_xlat19;
					    u_xlat19 = clamp(u_xlat19, 0.0, 1.0);
					    u_xlat8.x = (-u_xlat13) + 1.0;
					    u_xlat8.x = u_xlat1.x * u_xlat8.x + u_xlat13;
					    u_xlat13 = (-u_xlat1.x) * u_xlat13 + u_xlat13;
					    u_xlat13 = max(u_xlat13, 0.0);
					    u_xlat8.x = min(u_xlat8.x, 1.0);
					    u_xlat8.x = (-u_xlat13) + u_xlat8.x;
					    u_xlat13 = u_xlat0.w + (-u_xlat13);
					    u_xlat13 = u_xlat13 / u_xlat8.x;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat7 = u_xlat7 * u_xlat13;
					    u_xlat7 = u_xlat7 * 0.200000003;
					    u_xlat7 = u_xlat7 * vs_COLOR0.w;
					    u_xlat13 = floor(vs_TEXCOORD2.y);
					    u_xlat13 = u_xlat13 + -1.0;
					    u_xlat13 = max(u_xlat13, 0.0);
					    u_xlat8.x = u_xlat0.w * u_xlat13;
					    u_xlat8.x = fract(u_xlat8.x);
					    u_xlat8.x = u_xlat8.x + -0.5;
					    u_xlat13 = abs(u_xlat8.x) / u_xlat13;
					    u_xlat13 = u_xlat13 / u_xlat1.x;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat13 = (-u_xlat13) + 1.0;
					    u_xlat13 = u_xlat13 * 0.300000012;
					    u_xlat8.xyz = vs_COLOR0.xyz;
					    u_xlat8.xyz = vec3(u_xlat13) * (-u_xlat8.xyz) + u_xlat8.xyz;
					    u_xlat3.xyz = u_xlat0.xyz * u_xlat8.xyz;
					    u_xlat0.x = vs_TEXCOORD2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = vs_TEXCOORD1.y;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat4.xyz = vec3(u_xlat6) * (-u_xlat8.xyz) + u_xlat8.xyz;
					    u_xlat4.w = 1.0;
					    u_xlat4 = (-vec4(u_xlat7)) * vec4(0.0, 0.0, 0.0, 1.0) + u_xlat4;
					    u_xlat5 = vec4(u_xlat7) * vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat6 = (-u_xlat19) + 1.0;
					    u_xlat6 = u_xlat1.x * u_xlat6 + u_xlat19;
					    u_xlat12 = (-u_xlat1.x) * u_xlat19 + u_xlat19;
					    u_xlat12 = max(u_xlat12, 0.0);
					    u_xlat6 = min(u_xlat6, 1.0);
					    u_xlat6 = (-u_xlat12) + u_xlat6;
					    u_xlat12 = (-u_xlat12) + u_xlat0.w;
					    u_xlat6 = u_xlat12 / u_xlat6;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat4 = vec4(u_xlat6) * u_xlat4 + u_xlat5;
					    u_xlat3.w = 1.0;
					    u_xlat3 = u_xlat3 * u_xlat0.xxxx + (-u_xlat4);
					    u_xlat0.x = (-u_xlat2.x) + 1.0;
					    u_xlat0.x = u_xlat1.x * u_xlat0.x + u_xlat2.x;
					    u_xlat6 = (-u_xlat1.x) * u_xlat2.x + u_xlat2.x;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat0.x = (-u_xlat6) + u_xlat0.x;
					    u_xlat6 = (-u_xlat6) + u_xlat0.w;
					    u_xlat0.x = u_xlat6 / u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0 = u_xlat0.xxxx * u_xlat3 + u_xlat4;
					    u_xlat0 = u_xlat0 * vs_COLOR0.wwww;
					    u_xlatb1.xy = greaterThanEqual(vs_TEXCOORD3.xyxx, _ClipRect.xyxx).xy;
					    u_xlatb1.zw = greaterThanEqual(_ClipRect.zzzw, vs_TEXCOORD3.xxxy).zw;
					    u_xlat1.x = u_xlatb1.x ? float(1.0) : 0.0;
					    u_xlat1.y = u_xlatb1.y ? float(1.0) : 0.0;
					    u_xlat1.z = u_xlatb1.z ? float(1.0) : 0.0;
					    u_xlat1.w = u_xlatb1.w ? float(1.0) : 0.0;
					;
					    u_xlat1.xy = u_xlat1.zw * u_xlat1.xy;
					    u_xlat1.x = u_xlat1.y * u_xlat1.x;
					    u_xlat7 = u_xlat0.w * u_xlat1.x + -0.00100000005;
					    u_xlat0 = u_xlat0 * u_xlat1.xxxx;
					    SV_Target0 = u_xlat0;
					    u_xlatb0 = u_xlat7<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "UNITY_UI_ALPHACLIP" "_FWIDTH_ON" }
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
						vec4 unused_0_4[9];
						vec4 _ClipRect;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[5];
						vec4 _ProjectionParams;
						vec4 unused_1_2[3];
					};
					uniform  sampler2D _MainTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					bvec4 u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					float u_xlat5;
					int u_xlati5;
					bool u_xlatb5;
					float u_xlat6;
					float u_xlat10;
					int u_xlati10;
					float u_xlat11;
					float u_xlat16;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat5 = min(u_xlat0.w, vs_COLOR0.w);
					    u_xlat5 = u_xlat5 + -0.00999999978;
					    u_xlatb5 = u_xlat5<0.0;
					    if(((int(u_xlatb5) * int(0xffffffffu)))!=0){discard;}
					    u_xlati5 = int((0.0<_ProjectionParams.x) ? 0xFFFFFFFFu : uint(0));
					    u_xlati10 = int((_ProjectionParams.x<0.0) ? 0xFFFFFFFFu : uint(0));
					    u_xlati5 = u_xlati5 + (-u_xlati10);
					    u_xlat5 = float(u_xlati5);
					    u_xlat5 = u_xlat5 * u_xlat0.x;
					    u_xlat1.y = dFdy(u_xlat5);
					    u_xlat5 = u_xlat1.y + u_xlat1.y;
					    u_xlat1.x = dFdx(u_xlat0.x);
					    u_xlat5 = u_xlat1.x * 2.0 + (-u_xlat5);
					    u_xlat10 = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat10 = sqrt(u_xlat10);
					    u_xlat1.x = fract(vs_TEXCOORD2.y);
					    u_xlat1.x = u_xlat1.x * 1.01010096;
					    u_xlat6 = (-vs_TEXCOORD1.x) + 1.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat5 = u_xlat5 * u_xlat1.x + u_xlat6;
					    u_xlat11 = max(vs_TEXCOORD1.y, 1.0);
					    u_xlat11 = u_xlat11 * 0.5;
					    u_xlat16 = _AAFactor + 1.0;
					    u_xlat2.x = u_xlat16 * (-u_xlat11);
					    u_xlat11 = u_xlat16 * u_xlat11;
					    u_xlat11 = u_xlat10 * u_xlat11;
					    u_xlat11 = u_xlat11 * _LineWidth + u_xlat6;
					    u_xlat11 = clamp(u_xlat11, 0.0, 1.0);
					    u_xlat16 = u_xlat10 * u_xlat2.x;
					    u_xlat5 = u_xlat16 * _LineWidth + u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat6 = u_xlat16 * _LineWidth + u_xlat6;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat16 = (-u_xlat5) + 1.0;
					    u_xlat1.w = u_xlat10 * u_xlat16 + u_xlat5;
					    u_xlat5 = (-u_xlat10) * u_xlat5 + u_xlat5;
					    u_xlat5 = max(u_xlat5, 0.0);
					    u_xlat1.xw = min(u_xlat1.xw, vec2(1.0, 1.0));
					    u_xlat16 = (-u_xlat5) + u_xlat1.w;
					    u_xlat5 = (-u_xlat5) + u_xlat0.x;
					    u_xlat5 = u_xlat5 / u_xlat16;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat5 = u_xlat1.x * u_xlat5;
					    u_xlat5 = u_xlat5 * 0.200000003;
					    u_xlat5 = u_xlat5 * vs_COLOR0.w;
					    u_xlat1.x = floor(vs_TEXCOORD2.y);
					    u_xlat1.x = u_xlat1.x + -1.0;
					    u_xlat1.x = max(u_xlat1.x, 0.0);
					    u_xlat16 = u_xlat0.x * u_xlat1.x;
					    u_xlat16 = fract(u_xlat16);
					    u_xlat16 = u_xlat16 + -0.5;
					    u_xlat1.x = abs(u_xlat16) / u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat10;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat1.x = u_xlat1.x * 0.300000012;
					    u_xlat2.xyz = vs_COLOR0.xyz;
					    u_xlat2.w = 1.0;
					    u_xlat3 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = u_xlat1.xxxx * u_xlat3 + u_xlat2;
					    u_xlat1.x = vs_TEXCOORD2.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat2.xyz;
					    u_xlat16 = vs_TEXCOORD1.y;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat16) * (-u_xlat3.xyz) + u_xlat3.xyz;
					    u_xlat3.w = 1.0;
					    u_xlat3 = (-vec4(u_xlat5)) * vec4(0.0, 0.0, 0.0, 1.0) + u_xlat3;
					    u_xlat4 = vec4(u_xlat5) * vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat5 = (-u_xlat6) + 1.0;
					    u_xlat5 = u_xlat10 * u_xlat5 + u_xlat6;
					    u_xlat6 = (-u_xlat10) * u_xlat6 + u_xlat6;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat5 = min(u_xlat5, 1.0);
					    u_xlat5 = (-u_xlat6) + u_xlat5;
					    u_xlat6 = u_xlat0.x + (-u_xlat6);
					    u_xlat5 = u_xlat6 / u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat3 = vec4(u_xlat5) * u_xlat3 + u_xlat4;
					    u_xlat2 = u_xlat2 * u_xlat1.xxxx + (-u_xlat3);
					    u_xlat5 = (-u_xlat11) + 1.0;
					    u_xlat5 = u_xlat10 * u_xlat5 + u_xlat11;
					    u_xlat10 = (-u_xlat10) * u_xlat11 + u_xlat11;
					    u_xlat10 = max(u_xlat10, 0.0);
					    u_xlat0.y = min(u_xlat5, 1.0);
					    u_xlat0.xy = (-vec2(u_xlat10)) + u_xlat0.xy;
					    u_xlat0.x = u_xlat0.x / u_xlat0.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1 = u_xlat0.xxxx * u_xlat2 + u_xlat3;
					    u_xlat0 = u_xlat0.wwww * u_xlat1;
					    u_xlat0 = u_xlat0 * vs_COLOR0.wwww;
					    u_xlatb1.xy = greaterThanEqual(vs_TEXCOORD3.xyxx, _ClipRect.xyxx).xy;
					    u_xlatb1.zw = greaterThanEqual(_ClipRect.zzzw, vs_TEXCOORD3.xxxy).zw;
					    u_xlat1.x = u_xlatb1.x ? float(1.0) : 0.0;
					    u_xlat1.y = u_xlatb1.y ? float(1.0) : 0.0;
					    u_xlat1.z = u_xlatb1.z ? float(1.0) : 0.0;
					    u_xlat1.w = u_xlatb1.w ? float(1.0) : 0.0;
					;
					    u_xlat1.xy = u_xlat1.zw * u_xlat1.xy;
					    u_xlat1.x = u_xlat1.y * u_xlat1.x;
					    u_xlat6 = u_xlat0.w * u_xlat1.x + -0.00100000005;
					    u_xlat0 = u_xlat0 * u_xlat1.xxxx;
					    SV_Target0 = u_xlat0;
					    u_xlatb0 = u_xlat6<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "UNITY_UI_ALPHACLIP" "_FWIDTH_ON" "_ALPHAANDCOLORS_ON" }
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
						vec4 unused_0_4[9];
						vec4 _ClipRect;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[5];
						vec4 _ProjectionParams;
						vec4 unused_1_2[3];
					};
					uniform  sampler2D _MainTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					int u_xlati1;
					bvec4 u_xlatb1;
					vec3 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					float u_xlat6;
					float u_xlat7;
					int u_xlati7;
					vec3 u_xlat8;
					float u_xlat12;
					float u_xlat13;
					float u_xlat19;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = min(u_xlat0.w, vs_COLOR0.w);
					    u_xlat1.x = u_xlat1.x + -0.00999999978;
					    u_xlatb1.x = u_xlat1.x<0.0;
					    if(((int(u_xlatb1.x) * int(0xffffffffu)))!=0){discard;}
					    u_xlati1 = int((0.0<_ProjectionParams.x) ? 0xFFFFFFFFu : uint(0));
					    u_xlati7 = int((_ProjectionParams.x<0.0) ? 0xFFFFFFFFu : uint(0));
					    u_xlati1 = u_xlati1 + (-u_xlati7);
					    u_xlat1.x = float(u_xlati1);
					    u_xlat1.x = u_xlat0.w * u_xlat1.x;
					    u_xlat1.y = dFdy(u_xlat1.x);
					    u_xlat13 = u_xlat1.y + u_xlat1.y;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat13 = u_xlat1.x * 2.0 + (-u_xlat13);
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat7 = fract(vs_TEXCOORD2.y);
					    u_xlat7 = u_xlat7 * 1.01010096;
					    u_xlat19 = (-vs_TEXCOORD1.x) + 1.0;
					    u_xlat19 = clamp(u_xlat19, 0.0, 1.0);
					    u_xlat13 = u_xlat13 * u_xlat7 + u_xlat19;
					    u_xlat7 = min(u_xlat7, 1.0);
					    u_xlat2.x = max(vs_TEXCOORD1.y, 1.0);
					    u_xlat2.x = u_xlat2.x * 0.5;
					    u_xlat8.x = _AAFactor + 1.0;
					    u_xlat2.z = u_xlat8.x * (-u_xlat2.x);
					    u_xlat2.x = u_xlat8.x * u_xlat2.x;
					    u_xlat2.xy = u_xlat1.xx * u_xlat2.xz;
					    u_xlat2.x = u_xlat2.x * _LineWidth + u_xlat19;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat13 = u_xlat2.y * _LineWidth + u_xlat13;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat19 = u_xlat2.y * _LineWidth + u_xlat19;
					    u_xlat19 = clamp(u_xlat19, 0.0, 1.0);
					    u_xlat8.x = (-u_xlat13) + 1.0;
					    u_xlat8.x = u_xlat1.x * u_xlat8.x + u_xlat13;
					    u_xlat13 = (-u_xlat1.x) * u_xlat13 + u_xlat13;
					    u_xlat13 = max(u_xlat13, 0.0);
					    u_xlat8.x = min(u_xlat8.x, 1.0);
					    u_xlat8.x = (-u_xlat13) + u_xlat8.x;
					    u_xlat13 = u_xlat0.w + (-u_xlat13);
					    u_xlat13 = u_xlat13 / u_xlat8.x;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat7 = u_xlat7 * u_xlat13;
					    u_xlat7 = u_xlat7 * 0.200000003;
					    u_xlat7 = u_xlat7 * vs_COLOR0.w;
					    u_xlat13 = floor(vs_TEXCOORD2.y);
					    u_xlat13 = u_xlat13 + -1.0;
					    u_xlat13 = max(u_xlat13, 0.0);
					    u_xlat8.x = u_xlat0.w * u_xlat13;
					    u_xlat8.x = fract(u_xlat8.x);
					    u_xlat8.x = u_xlat8.x + -0.5;
					    u_xlat13 = abs(u_xlat8.x) / u_xlat13;
					    u_xlat13 = u_xlat13 / u_xlat1.x;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat13 = (-u_xlat13) + 1.0;
					    u_xlat13 = u_xlat13 * 0.300000012;
					    u_xlat8.xyz = vs_COLOR0.xyz;
					    u_xlat8.xyz = vec3(u_xlat13) * (-u_xlat8.xyz) + u_xlat8.xyz;
					    u_xlat3.xyz = u_xlat0.xyz * u_xlat8.xyz;
					    u_xlat0.x = vs_TEXCOORD2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat6 = vs_TEXCOORD1.y;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat4.xyz = vec3(u_xlat6) * (-u_xlat8.xyz) + u_xlat8.xyz;
					    u_xlat4.w = 1.0;
					    u_xlat4 = (-vec4(u_xlat7)) * vec4(0.0, 0.0, 0.0, 1.0) + u_xlat4;
					    u_xlat5 = vec4(u_xlat7) * vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat6 = (-u_xlat19) + 1.0;
					    u_xlat6 = u_xlat1.x * u_xlat6 + u_xlat19;
					    u_xlat12 = (-u_xlat1.x) * u_xlat19 + u_xlat19;
					    u_xlat12 = max(u_xlat12, 0.0);
					    u_xlat6 = min(u_xlat6, 1.0);
					    u_xlat6 = (-u_xlat12) + u_xlat6;
					    u_xlat12 = (-u_xlat12) + u_xlat0.w;
					    u_xlat6 = u_xlat12 / u_xlat6;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat4 = vec4(u_xlat6) * u_xlat4 + u_xlat5;
					    u_xlat3.w = 1.0;
					    u_xlat3 = u_xlat3 * u_xlat0.xxxx + (-u_xlat4);
					    u_xlat0.x = (-u_xlat2.x) + 1.0;
					    u_xlat0.x = u_xlat1.x * u_xlat0.x + u_xlat2.x;
					    u_xlat6 = (-u_xlat1.x) * u_xlat2.x + u_xlat2.x;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat0.x = (-u_xlat6) + u_xlat0.x;
					    u_xlat6 = (-u_xlat6) + u_xlat0.w;
					    u_xlat0.x = u_xlat6 / u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0 = u_xlat0.xxxx * u_xlat3 + u_xlat4;
					    u_xlat0 = u_xlat0 * vs_COLOR0.wwww;
					    u_xlatb1.xy = greaterThanEqual(vs_TEXCOORD3.xyxx, _ClipRect.xyxx).xy;
					    u_xlatb1.zw = greaterThanEqual(_ClipRect.zzzw, vs_TEXCOORD3.xxxy).zw;
					    u_xlat1.x = u_xlatb1.x ? float(1.0) : 0.0;
					    u_xlat1.y = u_xlatb1.y ? float(1.0) : 0.0;
					    u_xlat1.z = u_xlatb1.z ? float(1.0) : 0.0;
					    u_xlat1.w = u_xlatb1.w ? float(1.0) : 0.0;
					;
					    u_xlat1.xy = u_xlat1.zw * u_xlat1.xy;
					    u_xlat1.x = u_xlat1.y * u_xlat1.x;
					    u_xlat7 = u_xlat0.w * u_xlat1.x + -0.00100000005;
					    u_xlat0 = u_xlat0 * u_xlat1.xxxx;
					    SV_Target0 = u_xlat0;
					    u_xlatb0 = u_xlat7<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    return;
					}"
				}
			}
		}
	}
}