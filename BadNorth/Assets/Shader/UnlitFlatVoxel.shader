Shader "Unlit/FlatVoxel" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		_OutlineMultiplier ("Outline Multiplier", Float) = 0.5
		[Toggle] _Paint ("Paint", Float) = 0
		[Toggle] _Selected ("Selected", Float) = 0
	}
	SubShader {
		LOD 100
		Tags { "RenderType" = "Opaque" }
		Pass {
			LOD 100
			Tags { "RenderType" = "Opaque" }
			GpuProgramID 27231
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_3[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR1 = in_COLOR0;
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_3[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR1 = in_COLOR0;
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_3[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = unity_MatrixV[1].zzzz * vec4(-0.0300000012, -0.0300000012, -0.0300000012, -0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR1 = in_COLOR0;
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_3[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = unity_MatrixV[1].zzzz * vec4(-0.0300000012, -0.0300000012, -0.0300000012, -0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_OUTLINE_ON" }
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
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_5[2];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_8;
						float _OutlineMultiplier;
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
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat9;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat1.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat2.xyz = u_xlat1.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[0].xyz * u_xlat1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat9 = _FlashColor.x + 1.0;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(u_xlat9) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    vs_COLOR0.xyz = u_xlat0.xxx * vec3(_OutlineMultiplier);
					    vs_COLOR0.w = u_xlat0.x;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_OUTLINE_ON" "_CINEMATIC_ON" }
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
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_5[2];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_8;
						float _OutlineMultiplier;
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
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat9;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat1.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat2.xyz = u_xlat1.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[0].xyz * u_xlat1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat9 = _FlashColor.x + 1.0;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(u_xlat9) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    vs_COLOR0.xyz = u_xlat0.xxx * vec3(_OutlineMultiplier);
					    vs_COLOR0.w = u_xlat0.x;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" }
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_3[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_3[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" }
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_3[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_3[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" }
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_3[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = unity_MatrixV[1].zzzz * vec4(-0.0300000012, -0.0300000012, -0.0300000012, -0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_3[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = unity_MatrixV[1].zzzz * vec4(-0.0300000012, -0.0300000012, -0.0300000012, -0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_OUTLINE_ON" }
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
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_5[2];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_8;
						float _OutlineMultiplier;
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
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat9;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat1.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat2.xyz = u_xlat1.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[0].xyz * u_xlat1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat9 = _FlashColor.x + 1.0;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(u_xlat9) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    vs_COLOR0.xyz = u_xlat0.xxx * vec3(_OutlineMultiplier);
					    vs_COLOR0.w = u_xlat0.x;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_OUTLINE_ON" "_CINEMATIC_ON" }
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
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_5[2];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_8;
						float _OutlineMultiplier;
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
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat9;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat3.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat3.xyz;
					    u_xlat3.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat3.xyz;
					    u_xlat1.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat2.xyz = u_xlat1.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[0].xyz * u_xlat1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat9 = _FlashColor.x + 1.0;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(u_xlat9) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    vs_COLOR0.xyz = u_xlat0.xxx * vec3(_OutlineMultiplier);
					    vs_COLOR0.w = u_xlat0.x;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_PAINT_ON" }
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_3[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_PAINT_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_3[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR1 = in_COLOR0;
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[4];
						vec4 _SideSunColor;
						vec4 unused_0_10[2];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_16[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_23;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_27;
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_30[2];
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat10;
					float u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat19;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat10.xyz = u_xlat10.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat10.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * u_xlat10.zzz + u_xlat1.xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[3].xzy * in_POSITION0.www + u_xlat10.xzy;
					    u_xlat10.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwy;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat2.y * u_xlat30;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat32;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat32;
					    u_xlat6 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat12.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat6 = u_xlat0.xxxx * u_xlat6;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat8.yyyy;
					    u_xlat6 = u_xlat8.zzzz * u_xlat6;
					    u_xlat7 = u_xlat7 * u_xlat8.xxxx;
					    u_xlat7 = u_xlat8.yyyy * u_xlat7;
					    u_xlat6 = u_xlat7 * u_xlat8.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat30;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat30;
					    u_xlat12.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat5 = u_xlat0.xxxx * u_xlat5;
					    u_xlat5 = u_xlat0.yyyy * u_xlat5;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat8.zzzz + u_xlat6;
					    u_xlat3 = u_xlat5 * u_xlat8.zzzz + u_xlat3;
					    u_xlat5 = textureLod(_NormalTex, u_xlat12.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat12.zx, 0.0);
					    u_xlat4.w = u_xlat12.x;
					    u_xlat2 = u_xlat0.xxxx * u_xlat6;
					    u_xlat2 = u_xlat8.yyyy * u_xlat2;
					    u_xlat5 = u_xlat8.xxxx * u_xlat5;
					    u_xlat5 = u_xlat8.yyyy * u_xlat5;
					    u_xlat3 = u_xlat5 * u_xlat0.zzzz + u_xlat3;
					    u_xlat2 = u_xlat2 * u_xlat0.zzzz + u_xlat3;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat4.xw, 0.0);
					    u_xlat4 = u_xlat0.xxxx * u_xlat4;
					    u_xlat4 = u_xlat0.yyyy * u_xlat4;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat2 = u_xlat3 * u_xlat0.zzzz + u_xlat2;
					    u_xlat0 = u_xlat4 * u_xlat0.zzzz + u_xlat2;
					    u_xlat30 = u_xlat0.w * 0.400000006;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = (-u_xlat0.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat0.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat0.z) + unity_MatrixV[2].z;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat1.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat33;
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat11 = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat11;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat11;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat12.y + u_xlat12.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat30 = u_xlat1.x * 0.600000024 + u_xlat30;
					    u_xlat1.x = u_xlat31;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat11 = max(u_xlat31, u_xlat1.x);
					    u_xlat11 = (-u_xlat11) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat1.xxx * u_xlat3.yzx;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat11) + u_xlat4.xyz;
					    u_xlat1.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat0.xyz = vec3(u_xlat30) * u_xlat0.xyz;
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat3.xyz = u_xlat0.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat0.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat10.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat10.x = u_xlat10.y + u_xlat10.x;
					    u_xlat10.x = u_xlat12.z * u_xlat3.z + u_xlat10.x;
					    u_xlat10.x = u_xlat10.x / u_xlat2.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat10.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[4];
						vec4 _SideSunColor;
						vec4 unused_0_10[2];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_16[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_23;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_27;
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_30[2];
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat10;
					float u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat19;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat10.xyz = u_xlat10.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat10.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * u_xlat10.zzz + u_xlat1.xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[3].xzy * in_POSITION0.www + u_xlat10.xzy;
					    u_xlat10.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwy;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat2.y * u_xlat30;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat32;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat32;
					    u_xlat6 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat12.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat6 = u_xlat0.xxxx * u_xlat6;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat8.yyyy;
					    u_xlat6 = u_xlat8.zzzz * u_xlat6;
					    u_xlat7 = u_xlat7 * u_xlat8.xxxx;
					    u_xlat7 = u_xlat8.yyyy * u_xlat7;
					    u_xlat6 = u_xlat7 * u_xlat8.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat30;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat30;
					    u_xlat12.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat5 = u_xlat0.xxxx * u_xlat5;
					    u_xlat5 = u_xlat0.yyyy * u_xlat5;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat8.zzzz + u_xlat6;
					    u_xlat3 = u_xlat5 * u_xlat8.zzzz + u_xlat3;
					    u_xlat5 = textureLod(_NormalTex, u_xlat12.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat12.zx, 0.0);
					    u_xlat4.w = u_xlat12.x;
					    u_xlat2 = u_xlat0.xxxx * u_xlat6;
					    u_xlat2 = u_xlat8.yyyy * u_xlat2;
					    u_xlat5 = u_xlat8.xxxx * u_xlat5;
					    u_xlat5 = u_xlat8.yyyy * u_xlat5;
					    u_xlat3 = u_xlat5 * u_xlat0.zzzz + u_xlat3;
					    u_xlat2 = u_xlat2 * u_xlat0.zzzz + u_xlat3;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat4.xw, 0.0);
					    u_xlat4 = u_xlat0.xxxx * u_xlat4;
					    u_xlat4 = u_xlat0.yyyy * u_xlat4;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat2 = u_xlat3 * u_xlat0.zzzz + u_xlat2;
					    u_xlat0 = u_xlat4 * u_xlat0.zzzz + u_xlat2;
					    u_xlat30 = u_xlat0.w * 0.400000006;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = (-u_xlat0.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat0.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat0.z) + unity_MatrixV[2].z;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat1.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat33;
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat11 = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat11;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat11;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat12.y + u_xlat12.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat30 = u_xlat1.x * 0.600000024 + u_xlat30;
					    u_xlat1.x = u_xlat31;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat11 = max(u_xlat31, u_xlat1.x);
					    u_xlat11 = (-u_xlat11) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat1.xxx * u_xlat3.yzx;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat11) + u_xlat4.xyz;
					    u_xlat1.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat0.xyz = vec3(u_xlat30) * u_xlat0.xyz;
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat3.xyz = u_xlat0.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat0.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat10.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat10.x = u_xlat10.y + u_xlat10.x;
					    u_xlat10.x = u_xlat12.z * u_xlat3.z + u_xlat10.x;
					    u_xlat10.x = u_xlat10.x / u_xlat2.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat10.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[7];
						float _Year;
						vec4 unused_0_10[7];
						vec4 _SnowColor;
						vec4 unused_0_12;
						float _SnowAmount;
						vec4 unused_0_14[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_21;
						float _AAFactor;
						vec4 unused_0_23[3];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_26[2];
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat9;
					vec3 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat18;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat10.xyz = u_xlat10.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat10.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * u_xlat10.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat10.xyz;
					    u_xlat10.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = unity_MatrixV[0].z;
					    u_xlat0.z = unity_MatrixV[2].z;
					    u_xlat2.xy = u_xlat0.xz / unity_MatrixV[1].zz;
					    u_xlat3.xz = (-u_xlat0.xz);
					    u_xlat0.xy = u_xlat2.xy * u_xlat10.xx + u_xlat1.xz;
					    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat10.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat10.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1.w = _WaterLevel * 2.0 + (-u_xlat1.y);
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwz;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat2.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat2.y * u_xlat30;
					    u_xlat6.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat32;
					    u_xlat5.y = u_xlat4.x * u_xlat2.x + u_xlat32;
					    u_xlat7 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat12.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat7.xyz = u_xlat0.xxx * u_xlat7.xyz;
					    u_xlat9.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat9.yyy;
					    u_xlat7.xyz = u_xlat9.zzz * u_xlat7.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat9.xxx;
					    u_xlat8.xyz = u_xlat9.yyy * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat8.xyz * u_xlat9.zzz + u_xlat7.xyz;
					    u_xlat30 = u_xlat30 * u_xlat6.y;
					    u_xlat5.y = u_xlat4.x * u_xlat2.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat30;
					    u_xlat12.x = u_xlat4.y * u_xlat6.z;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat0.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat6.xyz * u_xlat9.zzz + u_xlat4.xyz;
					    u_xlat6 = textureLod(_NormalTex, u_xlat12.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat12.zx, 0.0);
					    u_xlat5.w = u_xlat12.x;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat9.yyy * u_xlat2.xyz;
					    u_xlat6.xyz = u_xlat9.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat9.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat6.xyz * u_xlat0.zzz + u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat0.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat0.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat0.yyy * u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat0.yyy * u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat2.xyz = (-u_xlat0.xyz) + (-u_xlat3.xyz);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat30 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat30 = u_xlat30 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat31 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat32 = u_xlat0.y * u_xlat31;
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat32;
					    u_xlat4.z = u_xlat0.z * u_xlat3.y;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat32;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat18.yz = u_xlat4.yx;
					    u_xlat7.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.yyy * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.zzz * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat7.xyz;
					    u_xlat10.x = u_xlat31 * u_xlat6.y;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat10.x;
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat10.x;
					    u_xlat18.x = u_xlat3.y * u_xlat6.z;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat1.yyy * u_xlat0.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat2.zzz + u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat18.yx, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat18.zx, 0.0);
					    u_xlat4.w = u_xlat18.x;
					    u_xlat5.xyz = u_xlat1.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat12.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat12.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat12.xyz = u_xlat1.xxx * u_xlat4.xyz;
					    u_xlat12.xyz = u_xlat1.yyy * u_xlat12.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat1.xyw = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat1.xyw * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat12.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat1.y = abs(_SunDir.y);
					    u_xlat1.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat1.xyz = u_xlat1.xyz;
					    u_xlat1.xyz = clamp(u_xlat1.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat1.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat1.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.200000003;
					    u_xlat10.x = u_xlat30;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat20 = max(u_xlat30, u_xlat10.x);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat1.x = _Year;
					    u_xlat1.y = 0.0;
					    u_xlat1 = textureLod(_GrassTex, u_xlat1.xy, 0.0);
					    u_xlat2.xyz = u_xlat10.xxx * u_xlat1.yzx;
					    u_xlat2.xyz = u_xlat1.xyz * vec3(u_xlat20) + u_xlat2.xyz;
					    u_xlat10.xyz = vec3(u_xlat30) * u_xlat1.zxy + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat10.xyz) + _SnowColor.xyz;
					    u_xlat10.xyz = vec3(_SnowAmount) * u_xlat1.xyz + u_xlat10.xyz;
					    u_xlat1.xyz = u_xlat10.xyz * _MinAmbientColor.xyz;
					    u_xlat10.xyz = (-_MinAmbientColor.xyz) * u_xlat10.xyz + _MaxAmbientColor.xyz;
					    vs_COLOR0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat1.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[7];
						float _Year;
						vec4 unused_0_10[7];
						vec4 _SnowColor;
						vec4 unused_0_12;
						float _SnowAmount;
						vec4 unused_0_14[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_21;
						float _AAFactor;
						vec4 unused_0_23[3];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_26[2];
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat9;
					vec3 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat18;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat10.xyz = u_xlat10.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat10.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * u_xlat10.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat10.xyz;
					    u_xlat10.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = unity_MatrixV[0].z;
					    u_xlat0.z = unity_MatrixV[2].z;
					    u_xlat2.xy = u_xlat0.xz / unity_MatrixV[1].zz;
					    u_xlat3.xz = (-u_xlat0.xz);
					    u_xlat0.xy = u_xlat2.xy * u_xlat10.xx + u_xlat1.xz;
					    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat10.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat10.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1.w = _WaterLevel * 2.0 + (-u_xlat1.y);
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwz;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat2.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat2.y * u_xlat30;
					    u_xlat6.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat32;
					    u_xlat5.y = u_xlat4.x * u_xlat2.x + u_xlat32;
					    u_xlat7 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat12.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat7.xyz = u_xlat0.xxx * u_xlat7.xyz;
					    u_xlat9.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat9.yyy;
					    u_xlat7.xyz = u_xlat9.zzz * u_xlat7.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat9.xxx;
					    u_xlat8.xyz = u_xlat9.yyy * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat8.xyz * u_xlat9.zzz + u_xlat7.xyz;
					    u_xlat30 = u_xlat30 * u_xlat6.y;
					    u_xlat5.y = u_xlat4.x * u_xlat2.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat30;
					    u_xlat12.x = u_xlat4.y * u_xlat6.z;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat0.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat6.xyz * u_xlat9.zzz + u_xlat4.xyz;
					    u_xlat6 = textureLod(_NormalTex, u_xlat12.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat12.zx, 0.0);
					    u_xlat5.w = u_xlat12.x;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat9.yyy * u_xlat2.xyz;
					    u_xlat6.xyz = u_xlat9.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat9.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat6.xyz * u_xlat0.zzz + u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat0.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat0.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat0.yyy * u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat0.yyy * u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat2.xyz = (-u_xlat0.xyz) + (-u_xlat3.xyz);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat30 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat30 = u_xlat30 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat31 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat32 = u_xlat0.y * u_xlat31;
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat32;
					    u_xlat4.z = u_xlat0.z * u_xlat3.y;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat32;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat18.yz = u_xlat4.yx;
					    u_xlat7.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.yyy * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.zzz * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat7.xyz;
					    u_xlat10.x = u_xlat31 * u_xlat6.y;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat10.x;
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat10.x;
					    u_xlat18.x = u_xlat3.y * u_xlat6.z;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat1.yyy * u_xlat0.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat2.zzz + u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat18.yx, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat18.zx, 0.0);
					    u_xlat4.w = u_xlat18.x;
					    u_xlat5.xyz = u_xlat1.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat12.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat12.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat12.xyz = u_xlat1.xxx * u_xlat4.xyz;
					    u_xlat12.xyz = u_xlat1.yyy * u_xlat12.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat1.xyw = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat1.xyw * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat12.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat1.y = abs(_SunDir.y);
					    u_xlat1.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat1.xyz = u_xlat1.xyz;
					    u_xlat1.xyz = clamp(u_xlat1.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat1.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat1.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.200000003;
					    u_xlat10.x = u_xlat30;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat20 = max(u_xlat30, u_xlat10.x);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat1.x = _Year;
					    u_xlat1.y = 0.0;
					    u_xlat1 = textureLod(_GrassTex, u_xlat1.xy, 0.0);
					    u_xlat2.xyz = u_xlat10.xxx * u_xlat1.yzx;
					    u_xlat2.xyz = u_xlat1.xyz * vec3(u_xlat20) + u_xlat2.xyz;
					    u_xlat10.xyz = vec3(u_xlat30) * u_xlat1.zxy + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat10.xyz) + _SnowColor.xyz;
					    u_xlat10.xyz = vec3(_SnowAmount) * u_xlat1.xyz + u_xlat10.xyz;
					    u_xlat1.xyz = u_xlat10.xyz * _MinAmbientColor.xyz;
					    u_xlat10.xyz = (-_MinAmbientColor.xyz) * u_xlat10.xyz + _MaxAmbientColor.xyz;
					    vs_COLOR0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat1.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_OUTLINE_ON" }
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[4];
						vec4 _SideSunColor;
						vec4 unused_0_10[2];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_16[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_23;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_26[2];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_29;
						float _OutlineMultiplier;
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat10;
					float u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat10.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat10.xyz + u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat0.xxx * u_xlat10.xyz;
					    u_xlat1.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat2.xyz = u_xlat1.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[0].xyz * u_xlat1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat31 = _FlashColor.x + 1.0;
					    u_xlat1.xyw = u_xlat10.xzy * vec3(u_xlat31) + u_xlat1.xzy;
					    u_xlat10.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat10.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat10.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwy;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat0.y * u_xlat30;
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat32;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat3.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat32;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8 = u_xlat2.xxxx * u_xlat8;
					    u_xlat8 = u_xlat3.yyyy * u_xlat8;
					    u_xlat8 = u_xlat3.zzzz * u_xlat8;
					    u_xlat6 = u_xlat6 * u_xlat3.zzzz + u_xlat8;
					    u_xlat10.x = u_xlat30 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat10.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat10.x;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat2.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat0 = u_xlat3.xxxx * u_xlat0;
					    u_xlat0 = u_xlat2.yyyy * u_xlat0;
					    u_xlat0 = u_xlat0 * u_xlat3.zzzz + u_xlat6;
					    u_xlat0 = u_xlat4 * u_xlat3.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6 = u_xlat2.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat4 = u_xlat3.xxxx * u_xlat4;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat6 * u_xlat2.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat2.xxxx * u_xlat5;
					    u_xlat5 = u_xlat2.yyyy * u_xlat5;
					    u_xlat3 = u_xlat3.xxxx * u_xlat4;
					    u_xlat3 = u_xlat2.yyyy * u_xlat3;
					    u_xlat0 = u_xlat3 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat5 * u_xlat2.zzzz + u_xlat0;
					    u_xlat30 = u_xlat0.w * 0.400000006;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = (-u_xlat0.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat0.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat0.z) + unity_MatrixV[2].z;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat1.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat33;
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat11 = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat11;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat11;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat12.y + u_xlat12.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat30 = u_xlat1.x * 0.600000024 + u_xlat30;
					    u_xlat1.x = u_xlat31;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat11 = max(u_xlat31, u_xlat1.x);
					    u_xlat11 = (-u_xlat11) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat1.xxx * u_xlat3.yzx;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat11) + u_xlat4.xyz;
					    u_xlat1.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat20 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat20 = inversesqrt(u_xlat20);
					    u_xlat0.xy = vec2(u_xlat20) * u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.x);
					    u_xlat0.xyz = u_xlat0.xyz;
					    u_xlat0.xyz = clamp(u_xlat0.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat12.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat12.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat10.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat10.xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = _FlashColor.xyz * vec3(4.0, 4.0, 4.0) + u_xlat0.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * vec3(_OutlineMultiplier);
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_OUTLINE_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[4];
						vec4 _SideSunColor;
						vec4 unused_0_10[2];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_16[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_23;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_26[2];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_29;
						float _OutlineMultiplier;
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat10;
					float u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat10.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat10.xyz + u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat0.xxx * u_xlat10.xyz;
					    u_xlat1.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat2.xyz = u_xlat1.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[0].xyz * u_xlat1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat31 = _FlashColor.x + 1.0;
					    u_xlat1.xyw = u_xlat10.xzy * vec3(u_xlat31) + u_xlat1.xzy;
					    u_xlat10.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat10.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat10.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwy;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat0.y * u_xlat30;
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat32;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat3.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat32;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8 = u_xlat2.xxxx * u_xlat8;
					    u_xlat8 = u_xlat3.yyyy * u_xlat8;
					    u_xlat8 = u_xlat3.zzzz * u_xlat8;
					    u_xlat6 = u_xlat6 * u_xlat3.zzzz + u_xlat8;
					    u_xlat10.x = u_xlat30 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat10.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat10.x;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat2.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat0 = u_xlat3.xxxx * u_xlat0;
					    u_xlat0 = u_xlat2.yyyy * u_xlat0;
					    u_xlat0 = u_xlat0 * u_xlat3.zzzz + u_xlat6;
					    u_xlat0 = u_xlat4 * u_xlat3.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6 = u_xlat2.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat4 = u_xlat3.xxxx * u_xlat4;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat6 * u_xlat2.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat2.xxxx * u_xlat5;
					    u_xlat5 = u_xlat2.yyyy * u_xlat5;
					    u_xlat3 = u_xlat3.xxxx * u_xlat4;
					    u_xlat3 = u_xlat2.yyyy * u_xlat3;
					    u_xlat0 = u_xlat3 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat5 * u_xlat2.zzzz + u_xlat0;
					    u_xlat30 = u_xlat0.w * 0.400000006;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = (-u_xlat0.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat0.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat0.z) + unity_MatrixV[2].z;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat1.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat33;
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat11 = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat11;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat11;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat12.y + u_xlat12.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat30 = u_xlat1.x * 0.600000024 + u_xlat30;
					    u_xlat1.x = u_xlat31;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat11 = max(u_xlat31, u_xlat1.x);
					    u_xlat11 = (-u_xlat11) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat1.xxx * u_xlat3.yzx;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat11) + u_xlat4.xyz;
					    u_xlat1.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat20 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat20 = inversesqrt(u_xlat20);
					    u_xlat0.xy = vec2(u_xlat20) * u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.x);
					    u_xlat0.xyz = u_xlat0.xyz;
					    u_xlat0.xyz = clamp(u_xlat0.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat12.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat12.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat10.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat10.xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = _FlashColor.xyz * vec3(4.0, 4.0, 4.0) + u_xlat0.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * vec3(_OutlineMultiplier);
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_PAINT_ON" }
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[4];
						vec4 _SideSunColor;
						vec4 unused_0_10[2];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_16[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_23;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_27;
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_30[2];
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat10;
					float u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat19;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat10.xyz = u_xlat10.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat10.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * u_xlat10.zzz + u_xlat1.xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[3].xzy * in_POSITION0.www + u_xlat10.xzy;
					    u_xlat10.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwy;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat2.y * u_xlat30;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat32;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat32;
					    u_xlat6 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat12.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat6 = u_xlat0.xxxx * u_xlat6;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat8.yyyy;
					    u_xlat6 = u_xlat8.zzzz * u_xlat6;
					    u_xlat7 = u_xlat7 * u_xlat8.xxxx;
					    u_xlat7 = u_xlat8.yyyy * u_xlat7;
					    u_xlat6 = u_xlat7 * u_xlat8.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat30;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat30;
					    u_xlat12.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat5 = u_xlat0.xxxx * u_xlat5;
					    u_xlat5 = u_xlat0.yyyy * u_xlat5;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat8.zzzz + u_xlat6;
					    u_xlat3 = u_xlat5 * u_xlat8.zzzz + u_xlat3;
					    u_xlat5 = textureLod(_NormalTex, u_xlat12.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat12.zx, 0.0);
					    u_xlat4.w = u_xlat12.x;
					    u_xlat2 = u_xlat0.xxxx * u_xlat6;
					    u_xlat2 = u_xlat8.yyyy * u_xlat2;
					    u_xlat5 = u_xlat8.xxxx * u_xlat5;
					    u_xlat5 = u_xlat8.yyyy * u_xlat5;
					    u_xlat3 = u_xlat5 * u_xlat0.zzzz + u_xlat3;
					    u_xlat2 = u_xlat2 * u_xlat0.zzzz + u_xlat3;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat4.xw, 0.0);
					    u_xlat4 = u_xlat0.xxxx * u_xlat4;
					    u_xlat4 = u_xlat0.yyyy * u_xlat4;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat2 = u_xlat3 * u_xlat0.zzzz + u_xlat2;
					    u_xlat0 = u_xlat4 * u_xlat0.zzzz + u_xlat2;
					    u_xlat30 = u_xlat0.w * 0.400000006;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = (-u_xlat0.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat0.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat0.z) + unity_MatrixV[2].z;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat1.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat33;
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat11 = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat11;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat11;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat12.y + u_xlat12.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat30 = u_xlat1.x * 0.600000024 + u_xlat30;
					    u_xlat1.x = u_xlat31;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat11 = max(u_xlat31, u_xlat1.x);
					    u_xlat11 = (-u_xlat11) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat1.xxx * u_xlat3.yzx;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat11) + u_xlat4.xyz;
					    u_xlat1.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat0.xyz = vec3(u_xlat30) * u_xlat0.xyz;
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat3.xyz = u_xlat0.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat0.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat10.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat10.x = u_xlat10.y + u_xlat10.x;
					    u_xlat10.x = u_xlat12.z * u_xlat3.z + u_xlat10.x;
					    u_xlat10.x = u_xlat10.x / u_xlat2.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat10.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_PAINT_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[4];
						vec4 _SideSunColor;
						vec4 unused_0_10[2];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_16[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_23;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_27;
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_30[2];
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat10;
					float u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat19;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat10.xyz = u_xlat10.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat10.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * u_xlat10.zzz + u_xlat1.xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[3].xzy * in_POSITION0.www + u_xlat10.xzy;
					    u_xlat10.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwy;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat2.y * u_xlat30;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat32;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat32;
					    u_xlat6 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat12.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat6 = u_xlat0.xxxx * u_xlat6;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat8.yyyy;
					    u_xlat6 = u_xlat8.zzzz * u_xlat6;
					    u_xlat7 = u_xlat7 * u_xlat8.xxxx;
					    u_xlat7 = u_xlat8.yyyy * u_xlat7;
					    u_xlat6 = u_xlat7 * u_xlat8.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat30;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat30;
					    u_xlat12.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat5 = u_xlat0.xxxx * u_xlat5;
					    u_xlat5 = u_xlat0.yyyy * u_xlat5;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat8.zzzz + u_xlat6;
					    u_xlat3 = u_xlat5 * u_xlat8.zzzz + u_xlat3;
					    u_xlat5 = textureLod(_NormalTex, u_xlat12.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat12.zx, 0.0);
					    u_xlat4.w = u_xlat12.x;
					    u_xlat2 = u_xlat0.xxxx * u_xlat6;
					    u_xlat2 = u_xlat8.yyyy * u_xlat2;
					    u_xlat5 = u_xlat8.xxxx * u_xlat5;
					    u_xlat5 = u_xlat8.yyyy * u_xlat5;
					    u_xlat3 = u_xlat5 * u_xlat0.zzzz + u_xlat3;
					    u_xlat2 = u_xlat2 * u_xlat0.zzzz + u_xlat3;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat4.xw, 0.0);
					    u_xlat4 = u_xlat0.xxxx * u_xlat4;
					    u_xlat4 = u_xlat0.yyyy * u_xlat4;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat2 = u_xlat3 * u_xlat0.zzzz + u_xlat2;
					    u_xlat0 = u_xlat4 * u_xlat0.zzzz + u_xlat2;
					    u_xlat30 = u_xlat0.w * 0.400000006;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = (-u_xlat0.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat0.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat0.z) + unity_MatrixV[2].z;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat1.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat33;
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat11 = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat11;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat11;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat12.y + u_xlat12.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat30 = u_xlat1.x * 0.600000024 + u_xlat30;
					    u_xlat1.x = u_xlat31;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat11 = max(u_xlat31, u_xlat1.x);
					    u_xlat11 = (-u_xlat11) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat1.xxx * u_xlat3.yzx;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat11) + u_xlat4.xyz;
					    u_xlat1.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat0.xyz = vec3(u_xlat30) * u_xlat0.xyz;
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat3.xyz = u_xlat0.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat0.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat10.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat10.x = u_xlat10.y + u_xlat10.x;
					    u_xlat10.x = u_xlat12.z * u_xlat3.z + u_xlat10.x;
					    u_xlat10.x = u_xlat10.x / u_xlat2.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat10.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" }
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[4];
						vec4 _SideSunColor;
						vec4 unused_0_10[2];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_16[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_23;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_27;
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_30[2];
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat10;
					float u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat19;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat10.xyz = u_xlat10.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat10.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * u_xlat10.zzz + u_xlat1.xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[3].xzy * in_POSITION0.www + u_xlat10.xzy;
					    u_xlat10.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwy;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat2.y * u_xlat30;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat32;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat32;
					    u_xlat6 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat12.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat6 = u_xlat0.xxxx * u_xlat6;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat8.yyyy;
					    u_xlat6 = u_xlat8.zzzz * u_xlat6;
					    u_xlat7 = u_xlat7 * u_xlat8.xxxx;
					    u_xlat7 = u_xlat8.yyyy * u_xlat7;
					    u_xlat6 = u_xlat7 * u_xlat8.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat30;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat30;
					    u_xlat12.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat5 = u_xlat0.xxxx * u_xlat5;
					    u_xlat5 = u_xlat0.yyyy * u_xlat5;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat8.zzzz + u_xlat6;
					    u_xlat3 = u_xlat5 * u_xlat8.zzzz + u_xlat3;
					    u_xlat5 = textureLod(_NormalTex, u_xlat12.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat12.zx, 0.0);
					    u_xlat4.w = u_xlat12.x;
					    u_xlat2 = u_xlat0.xxxx * u_xlat6;
					    u_xlat2 = u_xlat8.yyyy * u_xlat2;
					    u_xlat5 = u_xlat8.xxxx * u_xlat5;
					    u_xlat5 = u_xlat8.yyyy * u_xlat5;
					    u_xlat3 = u_xlat5 * u_xlat0.zzzz + u_xlat3;
					    u_xlat2 = u_xlat2 * u_xlat0.zzzz + u_xlat3;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat4.xw, 0.0);
					    u_xlat4 = u_xlat0.xxxx * u_xlat4;
					    u_xlat4 = u_xlat0.yyyy * u_xlat4;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat2 = u_xlat3 * u_xlat0.zzzz + u_xlat2;
					    u_xlat0 = u_xlat4 * u_xlat0.zzzz + u_xlat2;
					    u_xlat30 = u_xlat0.w * 0.400000006;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = (-u_xlat0.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat0.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat0.z) + unity_MatrixV[2].z;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat1.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat33;
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat11 = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat11;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat11;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat12.y + u_xlat12.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat30 = u_xlat1.x * 0.600000024 + u_xlat30;
					    u_xlat1.x = u_xlat31;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat11 = max(u_xlat31, u_xlat1.x);
					    u_xlat11 = (-u_xlat11) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat1.xxx * u_xlat3.yzx;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat11) + u_xlat4.xyz;
					    u_xlat1.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat0.xyz = vec3(u_xlat30) * u_xlat0.xyz;
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat3.xyz = u_xlat0.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat0.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat10.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat10.x = u_xlat10.y + u_xlat10.x;
					    u_xlat10.x = u_xlat12.z * u_xlat3.z + u_xlat10.x;
					    u_xlat10.x = u_xlat10.x / u_xlat2.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat10.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[4];
						vec4 _SideSunColor;
						vec4 unused_0_10[2];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_16[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_23;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_27;
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_30[2];
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat10;
					float u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat19;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat10.xyz = u_xlat10.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat10.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * u_xlat10.zzz + u_xlat1.xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[3].xzy * in_POSITION0.www + u_xlat10.xzy;
					    u_xlat10.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat10.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwy;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat2.y * u_xlat30;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat32;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat32;
					    u_xlat6 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat12.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat6 = u_xlat0.xxxx * u_xlat6;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat8.yyyy;
					    u_xlat6 = u_xlat8.zzzz * u_xlat6;
					    u_xlat7 = u_xlat7 * u_xlat8.xxxx;
					    u_xlat7 = u_xlat8.yyyy * u_xlat7;
					    u_xlat6 = u_xlat7 * u_xlat8.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat30;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat30;
					    u_xlat12.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat5 = u_xlat0.xxxx * u_xlat5;
					    u_xlat5 = u_xlat0.yyyy * u_xlat5;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat8.zzzz + u_xlat6;
					    u_xlat3 = u_xlat5 * u_xlat8.zzzz + u_xlat3;
					    u_xlat5 = textureLod(_NormalTex, u_xlat12.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat12.zx, 0.0);
					    u_xlat4.w = u_xlat12.x;
					    u_xlat2 = u_xlat0.xxxx * u_xlat6;
					    u_xlat2 = u_xlat8.yyyy * u_xlat2;
					    u_xlat5 = u_xlat8.xxxx * u_xlat5;
					    u_xlat5 = u_xlat8.yyyy * u_xlat5;
					    u_xlat3 = u_xlat5 * u_xlat0.zzzz + u_xlat3;
					    u_xlat2 = u_xlat2 * u_xlat0.zzzz + u_xlat3;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat4.xw, 0.0);
					    u_xlat4 = u_xlat0.xxxx * u_xlat4;
					    u_xlat4 = u_xlat0.yyyy * u_xlat4;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat2 = u_xlat3 * u_xlat0.zzzz + u_xlat2;
					    u_xlat0 = u_xlat4 * u_xlat0.zzzz + u_xlat2;
					    u_xlat30 = u_xlat0.w * 0.400000006;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = (-u_xlat0.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat0.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat0.z) + unity_MatrixV[2].z;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat1.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat33;
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat11 = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat11;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat11;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat12.y + u_xlat12.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat30 = u_xlat1.x * 0.600000024 + u_xlat30;
					    u_xlat1.x = u_xlat31;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat11 = max(u_xlat31, u_xlat1.x);
					    u_xlat11 = (-u_xlat11) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat1.xxx * u_xlat3.yzx;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat11) + u_xlat4.xyz;
					    u_xlat1.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat0.xyz = vec3(u_xlat30) * u_xlat0.xyz;
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat3.xyz = u_xlat0.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat0.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat10.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat10.x = u_xlat10.y + u_xlat10.x;
					    u_xlat10.x = u_xlat12.z * u_xlat3.z + u_xlat10.x;
					    u_xlat10.x = u_xlat10.x / u_xlat2.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat10.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_GAME_ON" }
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[7];
						float _Year;
						vec4 unused_0_10[7];
						vec4 _SnowColor;
						vec4 unused_0_12;
						float _SnowAmount;
						vec4 unused_0_14[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_21;
						float _AAFactor;
						vec4 unused_0_23[3];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_26[2];
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat9;
					vec3 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat18;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat10.xyz = u_xlat10.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat10.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * u_xlat10.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat10.xyz;
					    u_xlat10.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = unity_MatrixV[0].z;
					    u_xlat0.z = unity_MatrixV[2].z;
					    u_xlat2.xy = u_xlat0.xz / unity_MatrixV[1].zz;
					    u_xlat3.xz = (-u_xlat0.xz);
					    u_xlat0.xy = u_xlat2.xy * u_xlat10.xx + u_xlat1.xz;
					    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat10.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat10.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1.w = _WaterLevel * 2.0 + (-u_xlat1.y);
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwz;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat2.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat2.y * u_xlat30;
					    u_xlat6.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat32;
					    u_xlat5.y = u_xlat4.x * u_xlat2.x + u_xlat32;
					    u_xlat7 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat12.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat7.xyz = u_xlat0.xxx * u_xlat7.xyz;
					    u_xlat9.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat9.yyy;
					    u_xlat7.xyz = u_xlat9.zzz * u_xlat7.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat9.xxx;
					    u_xlat8.xyz = u_xlat9.yyy * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat8.xyz * u_xlat9.zzz + u_xlat7.xyz;
					    u_xlat30 = u_xlat30 * u_xlat6.y;
					    u_xlat5.y = u_xlat4.x * u_xlat2.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat30;
					    u_xlat12.x = u_xlat4.y * u_xlat6.z;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat0.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat6.xyz * u_xlat9.zzz + u_xlat4.xyz;
					    u_xlat6 = textureLod(_NormalTex, u_xlat12.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat12.zx, 0.0);
					    u_xlat5.w = u_xlat12.x;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat9.yyy * u_xlat2.xyz;
					    u_xlat6.xyz = u_xlat9.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat9.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat6.xyz * u_xlat0.zzz + u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat0.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat0.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat0.yyy * u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat0.yyy * u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat2.xyz = (-u_xlat0.xyz) + (-u_xlat3.xyz);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat30 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat30 = u_xlat30 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat31 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat32 = u_xlat0.y * u_xlat31;
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat32;
					    u_xlat4.z = u_xlat0.z * u_xlat3.y;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat32;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat18.yz = u_xlat4.yx;
					    u_xlat7.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.yyy * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.zzz * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat7.xyz;
					    u_xlat10.x = u_xlat31 * u_xlat6.y;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat10.x;
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat10.x;
					    u_xlat18.x = u_xlat3.y * u_xlat6.z;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat1.yyy * u_xlat0.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat2.zzz + u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat18.yx, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat18.zx, 0.0);
					    u_xlat4.w = u_xlat18.x;
					    u_xlat5.xyz = u_xlat1.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat12.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat12.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat12.xyz = u_xlat1.xxx * u_xlat4.xyz;
					    u_xlat12.xyz = u_xlat1.yyy * u_xlat12.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat1.xyw = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat1.xyw * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat12.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat1.y = abs(_SunDir.y);
					    u_xlat1.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat1.xyz = u_xlat1.xyz;
					    u_xlat1.xyz = clamp(u_xlat1.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat1.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat1.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.200000003;
					    u_xlat10.x = u_xlat30;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat20 = max(u_xlat30, u_xlat10.x);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat1.x = _Year;
					    u_xlat1.y = 0.0;
					    u_xlat1 = textureLod(_GrassTex, u_xlat1.xy, 0.0);
					    u_xlat2.xyz = u_xlat10.xxx * u_xlat1.yzx;
					    u_xlat2.xyz = u_xlat1.xyz * vec3(u_xlat20) + u_xlat2.xyz;
					    u_xlat10.xyz = vec3(u_xlat30) * u_xlat1.zxy + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat10.xyz) + _SnowColor.xyz;
					    u_xlat10.xyz = vec3(_SnowAmount) * u_xlat1.xyz + u_xlat10.xyz;
					    u_xlat1.xyz = u_xlat10.xyz * _MinAmbientColor.xyz;
					    u_xlat10.xyz = (-_MinAmbientColor.xyz) * u_xlat10.xyz + _MaxAmbientColor.xyz;
					    vs_COLOR0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat1.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_GAME_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[7];
						float _Year;
						vec4 unused_0_10[7];
						vec4 _SnowColor;
						vec4 unused_0_12;
						float _SnowAmount;
						vec4 unused_0_14[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_21;
						float _AAFactor;
						vec4 unused_0_23[3];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_26[2];
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat9;
					vec3 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat18;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat10.xyz = u_xlat10.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat10.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * u_xlat10.xxx + u_xlat1.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * u_xlat10.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat10.xyz;
					    u_xlat10.x = u_xlat1.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = unity_MatrixV[0].z;
					    u_xlat0.z = unity_MatrixV[2].z;
					    u_xlat2.xy = u_xlat0.xz / unity_MatrixV[1].zz;
					    u_xlat3.xz = (-u_xlat0.xz);
					    u_xlat0.xy = u_xlat2.xy * u_xlat10.xx + u_xlat1.xz;
					    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat10.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat10.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1.w = _WaterLevel * 2.0 + (-u_xlat1.y);
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwz;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat2.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat2.y * u_xlat30;
					    u_xlat6.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat32;
					    u_xlat5.y = u_xlat4.x * u_xlat2.x + u_xlat32;
					    u_xlat7 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat12.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat7.xyz = u_xlat0.xxx * u_xlat7.xyz;
					    u_xlat9.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat9.yyy;
					    u_xlat7.xyz = u_xlat9.zzz * u_xlat7.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat9.xxx;
					    u_xlat8.xyz = u_xlat9.yyy * u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat8.xyz * u_xlat9.zzz + u_xlat7.xyz;
					    u_xlat30 = u_xlat30 * u_xlat6.y;
					    u_xlat5.y = u_xlat4.x * u_xlat2.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat30;
					    u_xlat12.x = u_xlat4.y * u_xlat6.z;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat0.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat6.xyz * u_xlat9.zzz + u_xlat4.xyz;
					    u_xlat6 = textureLod(_NormalTex, u_xlat12.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat12.zx, 0.0);
					    u_xlat5.w = u_xlat12.x;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat9.yyy * u_xlat2.xyz;
					    u_xlat6.xyz = u_xlat9.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat9.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat6.xyz * u_xlat0.zzz + u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat0.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat0.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat0.yyy * u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat0.yyy * u_xlat4.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat2.xyz = (-u_xlat0.xyz) + (-u_xlat3.xyz);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat30 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat30 = u_xlat30 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat31 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat32 = u_xlat0.y * u_xlat31;
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat32;
					    u_xlat4.z = u_xlat0.z * u_xlat3.y;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat32;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat18.yz = u_xlat4.yx;
					    u_xlat7.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.yyy * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.zzz * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat7.xyz;
					    u_xlat10.x = u_xlat31 * u_xlat6.y;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat10.x;
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat10.x;
					    u_xlat18.x = u_xlat3.y * u_xlat6.z;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat1.yyy * u_xlat0.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat2.zzz + u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat18.yx, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat18.zx, 0.0);
					    u_xlat4.w = u_xlat18.x;
					    u_xlat5.xyz = u_xlat1.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat12.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat12.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat12.xyz = u_xlat1.xxx * u_xlat4.xyz;
					    u_xlat12.xyz = u_xlat1.yyy * u_xlat12.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat1.xyw = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat1.xyw * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat12.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat1.y = abs(_SunDir.y);
					    u_xlat1.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat1.xyz = u_xlat1.xyz;
					    u_xlat1.xyz = clamp(u_xlat1.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat1.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat1.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.200000003;
					    u_xlat10.x = u_xlat30;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat20 = max(u_xlat30, u_xlat10.x);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat1.x = _Year;
					    u_xlat1.y = 0.0;
					    u_xlat1 = textureLod(_GrassTex, u_xlat1.xy, 0.0);
					    u_xlat2.xyz = u_xlat10.xxx * u_xlat1.yzx;
					    u_xlat2.xyz = u_xlat1.xyz * vec3(u_xlat20) + u_xlat2.xyz;
					    u_xlat10.xyz = vec3(u_xlat30) * u_xlat1.zxy + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat10.xyz) + _SnowColor.xyz;
					    u_xlat10.xyz = vec3(_SnowAmount) * u_xlat1.xyz + u_xlat10.xyz;
					    u_xlat1.xyz = u_xlat10.xyz * _MinAmbientColor.xyz;
					    u_xlat10.xyz = (-_MinAmbientColor.xyz) * u_xlat10.xyz + _MaxAmbientColor.xyz;
					    vs_COLOR0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat1.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_OUTLINE_ON" }
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[4];
						vec4 _SideSunColor;
						vec4 unused_0_10[2];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_16[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_23;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_26[2];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_29;
						float _OutlineMultiplier;
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat10;
					float u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat10.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat10.xyz + u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat0.xxx * u_xlat10.xyz;
					    u_xlat1.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat2.xyz = u_xlat1.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[0].xyz * u_xlat1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat31 = _FlashColor.x + 1.0;
					    u_xlat1.xyw = u_xlat10.xzy * vec3(u_xlat31) + u_xlat1.xzy;
					    u_xlat10.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat10.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat10.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwy;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat0.y * u_xlat30;
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat32;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat3.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat32;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8 = u_xlat2.xxxx * u_xlat8;
					    u_xlat8 = u_xlat3.yyyy * u_xlat8;
					    u_xlat8 = u_xlat3.zzzz * u_xlat8;
					    u_xlat6 = u_xlat6 * u_xlat3.zzzz + u_xlat8;
					    u_xlat10.x = u_xlat30 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat10.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat10.x;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat2.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat0 = u_xlat3.xxxx * u_xlat0;
					    u_xlat0 = u_xlat2.yyyy * u_xlat0;
					    u_xlat0 = u_xlat0 * u_xlat3.zzzz + u_xlat6;
					    u_xlat0 = u_xlat4 * u_xlat3.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6 = u_xlat2.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat4 = u_xlat3.xxxx * u_xlat4;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat6 * u_xlat2.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat2.xxxx * u_xlat5;
					    u_xlat5 = u_xlat2.yyyy * u_xlat5;
					    u_xlat3 = u_xlat3.xxxx * u_xlat4;
					    u_xlat3 = u_xlat2.yyyy * u_xlat3;
					    u_xlat0 = u_xlat3 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat5 * u_xlat2.zzzz + u_xlat0;
					    u_xlat30 = u_xlat0.w * 0.400000006;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = (-u_xlat0.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat0.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat0.z) + unity_MatrixV[2].z;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat1.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat33;
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat11 = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat11;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat11;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat12.y + u_xlat12.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat30 = u_xlat1.x * 0.600000024 + u_xlat30;
					    u_xlat1.x = u_xlat31;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat11 = max(u_xlat31, u_xlat1.x);
					    u_xlat11 = (-u_xlat11) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat1.xxx * u_xlat3.yzx;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat11) + u_xlat4.xyz;
					    u_xlat1.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat20 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat20 = inversesqrt(u_xlat20);
					    u_xlat0.xy = vec2(u_xlat20) * u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.x);
					    u_xlat0.xyz = u_xlat0.xyz;
					    u_xlat0.xyz = clamp(u_xlat0.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat12.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat12.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat10.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat10.xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = _FlashColor.xyz * vec3(4.0, 4.0, 4.0) + u_xlat0.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * vec3(_OutlineMultiplier);
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_OUTLINE_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[4];
						vec4 _SideSunColor;
						vec4 unused_0_10[2];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_16[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_23;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_26[2];
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_29;
						float _OutlineMultiplier;
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec3 u_xlat10;
					float u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat19;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat10.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat10.xyz + u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat0.xxx * u_xlat10.xyz;
					    u_xlat1.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat2.xyz = u_xlat1.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[0].xyz * u_xlat1.xxx + u_xlat2.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat31 = _FlashColor.x + 1.0;
					    u_xlat1.xyw = u_xlat10.xzy * vec3(u_xlat31) + u_xlat1.xzy;
					    u_xlat10.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat10.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat10.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xwy;
					    u_xlat0.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat0.y * u_xlat30;
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat32;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat3.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat32;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8 = u_xlat2.xxxx * u_xlat8;
					    u_xlat8 = u_xlat3.yyyy * u_xlat8;
					    u_xlat8 = u_xlat3.zzzz * u_xlat8;
					    u_xlat6 = u_xlat6 * u_xlat3.zzzz + u_xlat8;
					    u_xlat10.x = u_xlat30 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat10.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat10.x;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat2.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat0 = u_xlat3.xxxx * u_xlat0;
					    u_xlat0 = u_xlat2.yyyy * u_xlat0;
					    u_xlat0 = u_xlat0 * u_xlat3.zzzz + u_xlat6;
					    u_xlat0 = u_xlat4 * u_xlat3.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6 = u_xlat2.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat4 = u_xlat3.xxxx * u_xlat4;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat6 * u_xlat2.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat2.xxxx * u_xlat5;
					    u_xlat5 = u_xlat2.yyyy * u_xlat5;
					    u_xlat3 = u_xlat3.xxxx * u_xlat4;
					    u_xlat3 = u_xlat2.yyyy * u_xlat3;
					    u_xlat0 = u_xlat3 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat5 * u_xlat2.zzzz + u_xlat0;
					    u_xlat30 = u_xlat0.w * 0.400000006;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = (-u_xlat0.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat0.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat0.z) + unity_MatrixV[2].z;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat1.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat33;
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat11 = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat11;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat11;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat12.y + u_xlat12.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat30 = u_xlat1.x * 0.600000024 + u_xlat30;
					    u_xlat1.x = u_xlat31;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat11 = max(u_xlat31, u_xlat1.x);
					    u_xlat11 = (-u_xlat11) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat1.xxx * u_xlat3.yzx;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat11) + u_xlat4.xyz;
					    u_xlat1.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat20 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat20 = inversesqrt(u_xlat20);
					    u_xlat0.xy = vec2(u_xlat20) * u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.x);
					    u_xlat0.xyz = u_xlat0.xyz;
					    u_xlat0.xyz = clamp(u_xlat0.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat12.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat12.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat10.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat10.xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = _FlashColor.xyz * vec3(4.0, 4.0, 4.0) + u_xlat0.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * vec3(_OutlineMultiplier);
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_PAINT_ON" }
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[2];
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec3 _PaintTexOffset;
						vec4 _LevelRect;
						vec4 unused_0_11[19];
						vec3 _SunDir;
						vec4 unused_0_13[4];
						vec4 _SideSunColor;
						vec4 unused_0_15[2];
						float _Year;
						vec4 unused_0_17;
						vec4 _BloodColor;
						vec4 unused_0_19[5];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_23[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_30;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_34;
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_37[2];
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec3 u_xlat11;
					float u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat17;
					vec3 u_xlat18;
					vec3 u_xlat21;
					float u_xlat25;
					float u_xlat33;
					float u_xlat34;
					float u_xlat35;
					float u_xlat36;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat11.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat11.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat11.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat11.xyz = u_xlat11.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat11.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * u_xlat11.xxx + u_xlat1.xyz;
					    u_xlat11.xyz = unity_ObjectToWorld[2].xyz * u_xlat11.zzz + u_xlat1.xyz;
					    u_xlat11.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat11.xyz;
					    u_xlat1.x = u_xlat11.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat1.x = dot(u_xlat11.xz, u_xlat11.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1 = u_xlat11.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat11.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat11.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1.w = u_xlat11.y;
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat2.y = 1.0;
					    u_xlat1.xyz = u_xlat11.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat2.xyz);
					    u_xlat2.xyz = fract(u_xlat2.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat0.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat34 = u_xlat3.y * u_xlat0.x;
					    u_xlat14.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat14.x + u_xlat34;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat34;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat18.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat2.xxxx * u_xlat6;
					    u_xlat9.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat9.yyyy;
					    u_xlat6 = u_xlat9.zzzz * u_xlat6;
					    u_xlat8 = u_xlat8 * u_xlat9.xxxx;
					    u_xlat8 = u_xlat9.yyyy * u_xlat8;
					    u_xlat6 = u_xlat8 * u_xlat9.zzzz + u_xlat6;
					    u_xlat0.x = u_xlat0.x * u_xlat14.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat0.x;
					    u_xlat5.x = u_xlat4.x * u_xlat14.x + u_xlat0.x;
					    u_xlat18.x = u_xlat14.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat2.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat3 = u_xlat9.xxxx * u_xlat3;
					    u_xlat3 = u_xlat2.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat9.zzzz + u_xlat6;
					    u_xlat3 = u_xlat4 * u_xlat9.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat18.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat18.zx, 0.0);
					    u_xlat5.w = u_xlat18.x;
					    u_xlat6 = u_xlat2.xxxx * u_xlat6;
					    u_xlat6 = u_xlat9.yyyy * u_xlat6;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat9.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat2.zzzz + u_xlat3;
					    u_xlat3 = u_xlat6 * u_xlat2.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat2.xxxx * u_xlat5;
					    u_xlat5 = u_xlat2.yyyy * u_xlat5;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat2.zzzz + u_xlat3;
					    u_xlat2 = u_xlat5 * u_xlat2.zzzz + u_xlat3;
					    u_xlat0.x = u_xlat2.w * 0.400000006;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat3.x = (-u_xlat2.x) + unity_MatrixV[0].z;
					    u_xlat3.y = (-u_xlat2.y) + unity_MatrixV[1].z;
					    u_xlat3.z = (-u_xlat2.z) + unity_MatrixV[2].z;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat34 = (-u_xlat3.w) * 0.5 + u_xlat11.y;
					    u_xlat11.xyz = u_xlat11.xyz + _PaintTexOffset.xyz;
					    u_xlat34 = u_xlat34 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat35 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat36 = u_xlat1.y * u_xlat35;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat1.x + u_xlat36;
					    u_xlat6.z = u_xlat1.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat8.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat36;
					    u_xlat9 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat21.yz = u_xlat6.yx;
					    u_xlat9.xyz = u_xlat3.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.zzz * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat9.xyz;
					    u_xlat12 = u_xlat35 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat1.x + u_xlat12;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat12;
					    u_xlat21.x = u_xlat5.y * u_xlat8.z;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat1.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat1.xyz = u_xlat3.yyy * u_xlat1.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat4.zzz + u_xlat5.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat21.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat21.zx, 0.0);
					    u_xlat6.w = u_xlat21.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat15.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat1.xyz = u_xlat15.xyz * u_xlat3.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat1.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat15.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat15.xyz = u_xlat3.yyy * u_xlat15.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat1.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat15.xyz * u_xlat3.zzz + u_xlat1.xyz;
					    u_xlat3.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xy = abs(_SunDir.yx);
					    u_xlat4.xyz = u_xlat3.zxw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat35 = u_xlat3.x + u_xlat3.y;
					    u_xlat3.xyz = u_xlat1.xyz * u_xlat4.xyz;
					    u_xlat1.x = u_xlat3.y + u_xlat3.x;
					    u_xlat1.x = u_xlat4.z * u_xlat1.z + u_xlat1.x;
					    u_xlat0.x = u_xlat1.x * 0.600000024 + u_xlat0.x;
					    u_xlat1.x = u_xlat34;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat34 = (-u_xlat34);
					    u_xlat34 = clamp(u_xlat34, 0.0, 1.0);
					    u_xlat12 = max(u_xlat34, u_xlat1.x);
					    u_xlat12 = (-u_xlat12) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = u_xlat1.xxx * u_xlat4.yzx;
					    u_xlat1.xyz = u_xlat4.xyz * vec3(u_xlat12) + u_xlat5.xyz;
					    u_xlat1.xyz = vec3(u_xlat34) * u_xlat4.zxy + u_xlat1.xyz;
					    u_xlat4.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat4.xyz + u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat0.x = inversesqrt(u_xlat0.x);
					    u_xlat4.xyz = u_xlat0.xxx * u_xlat2.xyz;
					    u_xlat4.w = (-u_xlat4.x);
					    u_xlat2.xyz = u_xlat4.xyw;
					    u_xlat2.xyz = clamp(u_xlat2.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat4.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat2.xy = u_xlat2.xy * u_xlat3.xy;
					    u_xlat34 = u_xlat2.y + u_xlat2.x;
					    u_xlat34 = u_xlat3.z * u_xlat2.z + u_xlat34;
					    u_xlat34 = u_xlat34 / u_xlat35;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(u_xlat34) + u_xlat1.xyz;
					    u_xlat34 = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat34 + 1.0;
					    u_xlat2.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat1.xyz;
					    u_xlat2.xy = max(u_xlat11.xz, _LevelRect.xy);
					    u_xlat11.xz = min(u_xlat2.xy, _LevelRect.zw);
					    u_xlat0.xyz = max(u_xlat11.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat33 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat34 = u_xlat2.y * u_xlat33;
					    u_xlat13.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat13.x + u_xlat34;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat34;
					    u_xlat5 = textureLod(_PaintTex, u_xlat4.xz, 0.0);
					    u_xlat16.yz = u_xlat4.yx;
					    u_xlat6 = textureLod(_PaintTex, u_xlat4.yz, 0.0);
					    u_xlat34 = u_xlat0.x * u_xlat5.x;
					    u_xlat17.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat34 = u_xlat34 * u_xlat17.y;
					    u_xlat34 = u_xlat17.z * u_xlat34;
					    u_xlat25 = u_xlat17.x * u_xlat6.x;
					    u_xlat25 = u_xlat17.y * u_xlat25;
					    u_xlat34 = u_xlat25 * u_xlat17.z + u_xlat34;
					    u_xlat33 = u_xlat33 * u_xlat13.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat33;
					    u_xlat4.x = u_xlat3.x * u_xlat13.x + u_xlat33;
					    u_xlat16.x = u_xlat13.z * u_xlat3.y;
					    u_xlat2 = textureLod(_PaintTex, u_xlat4.yz, 0.0);
					    u_xlat3 = textureLod(_PaintTex, u_xlat4.xz, 0.0);
					    u_xlat33 = u_xlat0.x * u_xlat3.x;
					    u_xlat33 = u_xlat0.y * u_xlat33;
					    u_xlat2.x = u_xlat17.x * u_xlat2.x;
					    u_xlat2.x = u_xlat0.y * u_xlat2.x;
					    u_xlat34 = u_xlat2.x * u_xlat17.z + u_xlat34;
					    u_xlat33 = u_xlat33 * u_xlat17.z + u_xlat34;
					    u_xlat2 = textureLod(_PaintTex, u_xlat16.yx, 0.0);
					    u_xlat3 = textureLod(_PaintTex, u_xlat16.zx, 0.0);
					    u_xlat4.w = u_xlat16.x;
					    u_xlat34 = u_xlat0.x * u_xlat3.x;
					    u_xlat34 = u_xlat17.y * u_xlat34;
					    u_xlat2.x = u_xlat17.x * u_xlat2.x;
					    u_xlat2.x = u_xlat17.y * u_xlat2.x;
					    u_xlat33 = u_xlat2.x * u_xlat0.z + u_xlat33;
					    u_xlat33 = u_xlat34 * u_xlat0.z + u_xlat33;
					    u_xlat2 = textureLod(_PaintTex, u_xlat4.yw, 0.0);
					    u_xlat3 = textureLod(_PaintTex, u_xlat4.xw, 0.0);
					    u_xlat0.x = u_xlat0.x * u_xlat3.x;
					    u_xlat0.x = u_xlat0.y * u_xlat0.x;
					    u_xlat34 = u_xlat17.x * u_xlat2.x;
					    u_xlat11.x = u_xlat0.y * u_xlat34;
					    u_xlat11.x = u_xlat11.x * u_xlat0.z + u_xlat33;
					    u_xlat0.x = u_xlat0.x * u_xlat0.z + u_xlat11.x;
					    u_xlat0.x = u_xlat0.x * 1.29999995 + -0.300000012;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat11.xyz = _BloodColor.xyz + vec3(-1.0, -1.0, -1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat11.xyz + vec3(1.0, 1.0, 1.0);
					    vs_COLOR0.xyz = u_xlat0.xyz * u_xlat1.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_PAINT_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_3[2];
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_6[2];
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec3 _PaintTexOffset;
						vec4 _LevelRect;
						vec4 unused_0_11[19];
						vec3 _SunDir;
						vec4 unused_0_13[4];
						vec4 _SideSunColor;
						vec4 unused_0_15[2];
						float _Year;
						vec4 unused_0_17;
						vec4 _BloodColor;
						vec4 unused_0_19[5];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_23[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_30;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_34;
						float _Hover;
						vec4 _MainTex_ST;
						vec4 unused_0_37[2];
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec3 u_xlat11;
					float u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat17;
					vec3 u_xlat18;
					vec3 u_xlat21;
					float u_xlat25;
					float u_xlat33;
					float u_xlat34;
					float u_xlat35;
					float u_xlat36;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat11.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat11.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat11.xyz = in_NORMAL0.xyz * vec3(_Hover);
					    u_xlat11.xyz = u_xlat11.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996) + in_POSITION0.xyz;
					    u_xlat1.xyz = u_xlat11.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * u_xlat11.xxx + u_xlat1.xyz;
					    u_xlat11.xyz = unity_ObjectToWorld[2].xyz * u_xlat11.zzz + u_xlat1.xyz;
					    u_xlat11.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat11.xyz;
					    u_xlat1.x = u_xlat11.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat1.x = dot(u_xlat11.xz, u_xlat11.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat1 = u_xlat11.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat11.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat11.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1.w = u_xlat11.y;
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat2.y = 1.0;
					    u_xlat1.xyz = u_xlat11.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat2.xyz);
					    u_xlat2.xyz = fract(u_xlat2.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat0.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat34 = u_xlat3.y * u_xlat0.x;
					    u_xlat14.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat14.x + u_xlat34;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat34;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat18.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat2.xxxx * u_xlat6;
					    u_xlat9.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat9.yyyy;
					    u_xlat6 = u_xlat9.zzzz * u_xlat6;
					    u_xlat8 = u_xlat8 * u_xlat9.xxxx;
					    u_xlat8 = u_xlat9.yyyy * u_xlat8;
					    u_xlat6 = u_xlat8 * u_xlat9.zzzz + u_xlat6;
					    u_xlat0.x = u_xlat0.x * u_xlat14.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat0.x;
					    u_xlat5.x = u_xlat4.x * u_xlat14.x + u_xlat0.x;
					    u_xlat18.x = u_xlat14.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat2.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat3 = u_xlat9.xxxx * u_xlat3;
					    u_xlat3 = u_xlat2.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat9.zzzz + u_xlat6;
					    u_xlat3 = u_xlat4 * u_xlat9.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat18.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat18.zx, 0.0);
					    u_xlat5.w = u_xlat18.x;
					    u_xlat6 = u_xlat2.xxxx * u_xlat6;
					    u_xlat6 = u_xlat9.yyyy * u_xlat6;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat9.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat2.zzzz + u_xlat3;
					    u_xlat3 = u_xlat6 * u_xlat2.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat2.xxxx * u_xlat5;
					    u_xlat5 = u_xlat2.yyyy * u_xlat5;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat2.zzzz + u_xlat3;
					    u_xlat2 = u_xlat5 * u_xlat2.zzzz + u_xlat3;
					    u_xlat0.x = u_xlat2.w * 0.400000006;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat3.x = (-u_xlat2.x) + unity_MatrixV[0].z;
					    u_xlat3.y = (-u_xlat2.y) + unity_MatrixV[1].z;
					    u_xlat3.z = (-u_xlat2.z) + unity_MatrixV[2].z;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat34 = (-u_xlat3.w) * 0.5 + u_xlat11.y;
					    u_xlat11.xyz = u_xlat11.xyz + _PaintTexOffset.xyz;
					    u_xlat34 = u_xlat34 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat35 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat36 = u_xlat1.y * u_xlat35;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat1.x + u_xlat36;
					    u_xlat6.z = u_xlat1.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat8.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat36;
					    u_xlat9 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat21.yz = u_xlat6.yx;
					    u_xlat9.xyz = u_xlat3.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.zzz * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat9.xyz;
					    u_xlat12 = u_xlat35 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat1.x + u_xlat12;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat12;
					    u_xlat21.x = u_xlat5.y * u_xlat8.z;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat1.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat1.xyz = u_xlat3.yyy * u_xlat1.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat4.zzz + u_xlat5.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat21.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat21.zx, 0.0);
					    u_xlat6.w = u_xlat21.x;
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat15.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat1.xyz = u_xlat15.xyz * u_xlat3.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat1.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat15.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat15.xyz = u_xlat3.yyy * u_xlat15.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat1.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat15.xyz * u_xlat3.zzz + u_xlat1.xyz;
					    u_xlat3.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xy = abs(_SunDir.yx);
					    u_xlat4.xyz = u_xlat3.zxw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat35 = u_xlat3.x + u_xlat3.y;
					    u_xlat3.xyz = u_xlat1.xyz * u_xlat4.xyz;
					    u_xlat1.x = u_xlat3.y + u_xlat3.x;
					    u_xlat1.x = u_xlat4.z * u_xlat1.z + u_xlat1.x;
					    u_xlat0.x = u_xlat1.x * 0.600000024 + u_xlat0.x;
					    u_xlat1.x = u_xlat34;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat34 = (-u_xlat34);
					    u_xlat34 = clamp(u_xlat34, 0.0, 1.0);
					    u_xlat12 = max(u_xlat34, u_xlat1.x);
					    u_xlat12 = (-u_xlat12) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = u_xlat1.xxx * u_xlat4.yzx;
					    u_xlat1.xyz = u_xlat4.xyz * vec3(u_xlat12) + u_xlat5.xyz;
					    u_xlat1.xyz = vec3(u_xlat34) * u_xlat4.zxy + u_xlat1.xyz;
					    u_xlat4.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat4.xyz + u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat0.x = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat0.x = inversesqrt(u_xlat0.x);
					    u_xlat4.xyz = u_xlat0.xxx * u_xlat2.xyz;
					    u_xlat4.w = (-u_xlat4.x);
					    u_xlat2.xyz = u_xlat4.xyw;
					    u_xlat2.xyz = clamp(u_xlat2.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat4.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat2.xy = u_xlat2.xy * u_xlat3.xy;
					    u_xlat34 = u_xlat2.y + u_xlat2.x;
					    u_xlat34 = u_xlat3.z * u_xlat2.z + u_xlat34;
					    u_xlat34 = u_xlat34 / u_xlat35;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(u_xlat34) + u_xlat1.xyz;
					    u_xlat34 = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat34 + 1.0;
					    u_xlat2.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat1.xyz;
					    u_xlat2.xy = max(u_xlat11.xz, _LevelRect.xy);
					    u_xlat11.xz = min(u_xlat2.xy, _LevelRect.zw);
					    u_xlat0.xyz = max(u_xlat11.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat33 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat34 = u_xlat2.y * u_xlat33;
					    u_xlat13.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat13.x + u_xlat34;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat34;
					    u_xlat5 = textureLod(_PaintTex, u_xlat4.xz, 0.0);
					    u_xlat16.yz = u_xlat4.yx;
					    u_xlat6 = textureLod(_PaintTex, u_xlat4.yz, 0.0);
					    u_xlat34 = u_xlat0.x * u_xlat5.x;
					    u_xlat17.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat34 = u_xlat34 * u_xlat17.y;
					    u_xlat34 = u_xlat17.z * u_xlat34;
					    u_xlat25 = u_xlat17.x * u_xlat6.x;
					    u_xlat25 = u_xlat17.y * u_xlat25;
					    u_xlat34 = u_xlat25 * u_xlat17.z + u_xlat34;
					    u_xlat33 = u_xlat33 * u_xlat13.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat33;
					    u_xlat4.x = u_xlat3.x * u_xlat13.x + u_xlat33;
					    u_xlat16.x = u_xlat13.z * u_xlat3.y;
					    u_xlat2 = textureLod(_PaintTex, u_xlat4.yz, 0.0);
					    u_xlat3 = textureLod(_PaintTex, u_xlat4.xz, 0.0);
					    u_xlat33 = u_xlat0.x * u_xlat3.x;
					    u_xlat33 = u_xlat0.y * u_xlat33;
					    u_xlat2.x = u_xlat17.x * u_xlat2.x;
					    u_xlat2.x = u_xlat0.y * u_xlat2.x;
					    u_xlat34 = u_xlat2.x * u_xlat17.z + u_xlat34;
					    u_xlat33 = u_xlat33 * u_xlat17.z + u_xlat34;
					    u_xlat2 = textureLod(_PaintTex, u_xlat16.yx, 0.0);
					    u_xlat3 = textureLod(_PaintTex, u_xlat16.zx, 0.0);
					    u_xlat4.w = u_xlat16.x;
					    u_xlat34 = u_xlat0.x * u_xlat3.x;
					    u_xlat34 = u_xlat17.y * u_xlat34;
					    u_xlat2.x = u_xlat17.x * u_xlat2.x;
					    u_xlat2.x = u_xlat17.y * u_xlat2.x;
					    u_xlat33 = u_xlat2.x * u_xlat0.z + u_xlat33;
					    u_xlat33 = u_xlat34 * u_xlat0.z + u_xlat33;
					    u_xlat2 = textureLod(_PaintTex, u_xlat4.yw, 0.0);
					    u_xlat3 = textureLod(_PaintTex, u_xlat4.xw, 0.0);
					    u_xlat0.x = u_xlat0.x * u_xlat3.x;
					    u_xlat0.x = u_xlat0.y * u_xlat0.x;
					    u_xlat34 = u_xlat17.x * u_xlat2.x;
					    u_xlat11.x = u_xlat0.y * u_xlat34;
					    u_xlat11.x = u_xlat11.x * u_xlat0.z + u_xlat33;
					    u_xlat0.x = u_xlat0.x * u_xlat0.z + u_xlat11.x;
					    u_xlat0.x = u_xlat0.x * 1.29999995 + -0.300000012;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat11.xyz = _BloodColor.xyz + vec3(-1.0, -1.0, -1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat11.xyz + vec3(1.0, 1.0, 1.0);
					    vs_COLOR0.xyz = u_xlat0.xyz * u_xlat1.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
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
						vec4 unused_0_0[62];
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_3;
						vec4 _Color;
						vec4 unused_0_5;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat6) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
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
						vec4 unused_0_0[65];
						vec4 _Color;
						vec4 unused_0_2;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
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
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat0.x = u_xlat0.w + (-vs_COLOR1.w);
					    u_xlat0.x = u_xlat0.w * u_xlat0.x + vs_COLOR1.w;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
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
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat0.x = u_xlat0.w + (-vs_COLOR1.w);
					    u_xlat0.x = u_xlat0.w * u_xlat0.x + vs_COLOR1.w;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_OUTLINE_ON" }
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
						vec4 unused_0_0[62];
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_3;
						vec4 _Color;
						vec4 unused_0_5;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat6) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_OUTLINE_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[65];
						vec4 _Color;
						vec4 unused_0_2;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" }
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
						vec4 unused_0_0[62];
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_3;
						vec4 _Color;
						vec4 unused_0_5;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat6) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[65];
						vec4 _Color;
						vec4 unused_0_2;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" }
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
						vec4 unused_0_0[62];
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_3;
						vec4 _Color;
						vec4 unused_0_5;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat6) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[65];
						vec4 _Color;
						vec4 unused_0_2;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" }
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
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat0.x = u_xlat0.w + (-vs_COLOR1.w);
					    u_xlat0.x = u_xlat0.w * u_xlat0.x + vs_COLOR1.w;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_CINEMATIC_ON" }
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
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat0.x = u_xlat0.w + (-vs_COLOR1.w);
					    u_xlat0.x = u_xlat0.w * u_xlat0.x + vs_COLOR1.w;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_OUTLINE_ON" }
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
						vec4 unused_0_0[62];
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_3;
						vec4 _Color;
						vec4 unused_0_5;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat6) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_OUTLINE_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[65];
						vec4 _Color;
						vec4 unused_0_2;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_PAINT_ON" }
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
						vec4 unused_0_0[62];
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_3;
						vec4 _Color;
						vec4 unused_0_5;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat6) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_PAINT_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[65];
						vec4 _Color;
						vec4 unused_0_2;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
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
						vec4 unused_0_6;
						vec4 _FlashColor;
						vec4 unused_0_8;
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_11;
						vec4 _Color;
						vec4 unused_0_13;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat6 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat6) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
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
						vec4 unused_0_6;
						vec4 _FlashColor;
						vec4 unused_0_8[4];
						vec4 _Color;
						vec4 unused_0_10;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[6];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat0.x = u_xlat0.w + (-vs_COLOR1.w);
					    u_xlat0.x = u_xlat0.w * u_xlat0.x + vs_COLOR1.w;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[6];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat0.x = u_xlat0.w + (-vs_COLOR1.w);
					    u_xlat0.x = u_xlat0.w * u_xlat0.x + vs_COLOR1.w;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_OUTLINE_ON" }
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
						vec4 unused_0_6;
						vec4 _FlashColor;
						vec4 unused_0_8;
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_11;
						vec4 _Color;
						vec4 unused_0_13;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat6 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat6) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_OUTLINE_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_6;
						vec4 _FlashColor;
						vec4 unused_0_8[4];
						vec4 _Color;
						vec4 unused_0_10;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_PAINT_ON" }
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
						vec4 unused_0_6;
						vec4 _FlashColor;
						vec4 unused_0_8;
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_11;
						vec4 _Color;
						vec4 unused_0_13;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat6 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat6) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_PAINT_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_6;
						vec4 _FlashColor;
						vec4 unused_0_8[4];
						vec4 _Color;
						vec4 unused_0_10;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" }
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
						vec4 unused_0_6;
						vec4 _FlashColor;
						vec4 unused_0_8;
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_11;
						vec4 _Color;
						vec4 unused_0_13;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat6 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat6) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_6;
						vec4 _FlashColor;
						vec4 unused_0_8[4];
						vec4 _Color;
						vec4 unused_0_10;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_GAME_ON" }
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
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[6];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat0.x = u_xlat0.w + (-vs_COLOR1.w);
					    u_xlat0.x = u_xlat0.w * u_xlat0.x + vs_COLOR1.w;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_GAME_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[6];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat0.x = u_xlat0.w + (-vs_COLOR1.w);
					    u_xlat0.x = u_xlat0.w * u_xlat0.x + vs_COLOR1.w;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_OUTLINE_ON" }
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
						vec4 unused_0_6;
						vec4 _FlashColor;
						vec4 unused_0_8;
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_11;
						vec4 _Color;
						vec4 unused_0_13;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat6 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat6) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_OUTLINE_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_6;
						vec4 _FlashColor;
						vec4 unused_0_8[4];
						vec4 _Color;
						vec4 unused_0_10;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_PAINT_ON" }
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
						vec4 unused_0_6;
						vec4 _FlashColor;
						vec4 unused_0_8;
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_11;
						vec4 _Color;
						vec4 unused_0_13;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat6 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat6) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_PAINT_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_6;
						vec4 _FlashColor;
						vec4 unused_0_8[4];
						vec4 _Color;
						vec4 unused_0_10;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat1 = u_xlat0 + (-vs_COLOR1);
					    u_xlat0 = u_xlat0.wwww * u_xlat1 + vs_COLOR1;
					    u_xlat6 = u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlatb6 = u_xlat6<0.0;
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + vs_COLOR0.xyz;
					    u_xlat6 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat6) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
			}
		}
	}
}