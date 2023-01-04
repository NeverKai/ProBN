Shader "Unlit/NavSpot" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (1,1,1,1)
		_Fill ("Fill", Float) = 0.2
		_Outline ("Outline", Float) = 1
		_OutlineWidth ("Outline Width", Float) = 2
		_Shade ("Shade", Float) = 0.2
		_ShadeOffset ("Shade Offset", Float) = 4
	}
	SubShader {
		LOD 100
		Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			LOD 100
			Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			Stencil {
				Ref 2
				Comp LEqual
				Pass Keep
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 3398
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[3];
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
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat0.x = unity_MatrixV[0].z * -0.0799999982;
					    u_xlat0.y = unity_MatrixV[1].z * -0.0799999982;
					    u_xlat0.z = unity_MatrixV[2].z * -0.0799999982;
					    u_xlat0.xyz = (-u_xlat0.xyz) + in_POSITION0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_ObjectToWorld[1];
					    u_xlat1 = unity_ObjectToWorld[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_ObjectToWorld[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[3];
					};
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					void main()
					{
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    gl_Position = vec4(0.0, 0.0, 0.0, 0.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_LOWEND_ON" }
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
						vec4 unused_0_2[3];
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
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat0.x = unity_MatrixV[0].z * -0.0799999982;
					    u_xlat0.y = unity_MatrixV[1].z * -0.0799999982;
					    u_xlat0.z = unity_MatrixV[2].z * -0.0799999982;
					    u_xlat0.xyz = (-u_xlat0.xyz) + in_POSITION0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_ObjectToWorld[1];
					    u_xlat1 = unity_ObjectToWorld[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_ObjectToWorld[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_LOWEND_ON" }
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
						vec4 unused_0_2[3];
					};
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					void main()
					{
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    gl_Position = vec4(0.0, 0.0, 0.0, 0.0);
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[3];
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
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat0.x = unity_MatrixV[0].z * -0.0799999982;
					    u_xlat0.y = unity_MatrixV[1].z * -0.0799999982;
					    u_xlat0.z = unity_MatrixV[2].z * -0.0799999982;
					    u_xlat0.xyz = (-u_xlat0.xyz) + in_POSITION0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_ObjectToWorld[1];
					    u_xlat1 = unity_ObjectToWorld[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_ObjectToWorld[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[3];
					};
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					void main()
					{
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    gl_Position = vec4(0.0, 0.0, 0.0, 0.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_LOWEND_ON" "_VOXELAO_ON" }
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
						vec4 unused_0_2[3];
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
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat0.x = unity_MatrixV[0].z * -0.0799999982;
					    u_xlat0.y = unity_MatrixV[1].z * -0.0799999982;
					    u_xlat0.z = unity_MatrixV[2].z * -0.0799999982;
					    u_xlat0.xyz = (-u_xlat0.xyz) + in_POSITION0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_ObjectToWorld[1];
					    u_xlat1 = unity_ObjectToWorld[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_ObjectToWorld[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_LOWEND_ON" "_VOXELAO_ON" }
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
						vec4 unused_0_2[3];
					};
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					void main()
					{
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    gl_Position = vec4(0.0, 0.0, 0.0, 0.0);
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
						vec4 unused_0_4[5];
						vec4 _Color;
						float _Shade;
						float _ShadeOffset;
						float _Fill;
						float _Outline;
						float _OutlineWidth;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[5];
						vec4 _ProjectionParams;
						vec4 unused_1_2[3];
					};
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					int u_xlati0;
					vec2 u_xlat1;
					vec2 u_xlat2;
					int u_xlati2;
					float u_xlat3;
					float u_xlat4;
					float u_xlat6;
					void main()
					{
					    u_xlati0 = int((0.0<_ProjectionParams.x) ? 0xFFFFFFFFu : uint(0));
					    u_xlati2 = int((_ProjectionParams.x<0.0) ? 0xFFFFFFFFu : uint(0));
					    u_xlati0 = u_xlati0 + (-u_xlati2);
					    u_xlat0.x = float(u_xlati0);
					    u_xlat2.xy = (-vs_TEXCOORD0.yy) + vec2(0.920000017, 1.0);
					    u_xlat6 = dFdx(u_xlat2.x);
					    u_xlat1.x = dFdy(u_xlat2.x);
					    u_xlat6 = abs(u_xlat6) + abs(u_xlat1.x);
					    u_xlat1.x = _AAFactor + 1.0;
					    u_xlat6 = u_xlat6 * u_xlat1.x;
					    u_xlat0.yz = u_xlat2.yx / vec2(u_xlat6);
					    u_xlat0.x = u_xlat0.x * u_xlat0.z;
					    u_xlat1.y = dFdy(u_xlat0.x);
					    u_xlat1.x = dFdx(u_xlat0.z);
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = inversesqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * u_xlat1.y;
					    u_xlat6 = (-u_xlat0.x) * _ShadeOffset;
					    u_xlat0.xy = u_xlat0.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * _LineWidth + (-u_xlat0.z);
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = (-u_xlat6) + 1.0;
					    u_xlat1.x = u_xlat6 * _Shade;
					    u_xlat6 = max(u_xlat6, 0.00100000005);
					    u_xlat3 = max(_Fill, _Outline);
					    u_xlat3 = (-u_xlat1.x) * u_xlat0.x + u_xlat3;
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    u_xlat1.x = (-u_xlat0.z);
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat0.x = u_xlat1.x * u_xlat3 + u_xlat0.x;
					    u_xlat6 = u_xlat1.x / u_xlat6;
					    u_xlat6 = min(u_xlat6, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * _Color.xyz;
					    u_xlat6 = (-u_xlat0.x) + _Fill;
					    u_xlat4 = _OutlineWidth * _LineWidth + (-u_xlat0.z);
					    u_xlat4 = clamp(u_xlat4, 0.0, 1.0);
					    u_xlat4 = (-u_xlat4) + 1.0;
					    u_xlat0.x = u_xlat4 * u_xlat6 + u_xlat0.x;
					    u_xlat0.x = u_xlat0.y * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x * _Color.w;
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[5];
						vec4 _Color;
						float _Shade;
						float _ShadeOffset;
						float _Fill;
						float _Outline;
						float _OutlineWidth;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[5];
						vec4 _ProjectionParams;
						vec4 unused_1_2[3];
					};
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					int u_xlati0;
					vec2 u_xlat1;
					vec2 u_xlat2;
					int u_xlati2;
					float u_xlat3;
					float u_xlat4;
					float u_xlat6;
					void main()
					{
					    u_xlati0 = int((0.0<_ProjectionParams.x) ? 0xFFFFFFFFu : uint(0));
					    u_xlati2 = int((_ProjectionParams.x<0.0) ? 0xFFFFFFFFu : uint(0));
					    u_xlati0 = u_xlati0 + (-u_xlati2);
					    u_xlat0.x = float(u_xlati0);
					    u_xlat2.xy = (-vs_TEXCOORD0.yy) + vec2(0.920000017, 1.0);
					    u_xlat6 = dFdx(u_xlat2.x);
					    u_xlat1.x = dFdy(u_xlat2.x);
					    u_xlat6 = abs(u_xlat6) + abs(u_xlat1.x);
					    u_xlat1.x = _AAFactor + 1.0;
					    u_xlat6 = u_xlat6 * u_xlat1.x;
					    u_xlat0.yz = u_xlat2.yx / vec2(u_xlat6);
					    u_xlat0.x = u_xlat0.x * u_xlat0.z;
					    u_xlat1.y = dFdy(u_xlat0.x);
					    u_xlat1.x = dFdx(u_xlat0.z);
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = inversesqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * u_xlat1.y;
					    u_xlat6 = (-u_xlat0.x) * _ShadeOffset;
					    u_xlat0.xy = u_xlat0.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * _LineWidth + (-u_xlat0.z);
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = (-u_xlat6) + 1.0;
					    u_xlat1.x = u_xlat6 * _Shade;
					    u_xlat6 = max(u_xlat6, 0.00100000005);
					    u_xlat3 = max(_Fill, _Outline);
					    u_xlat3 = (-u_xlat1.x) * u_xlat0.x + u_xlat3;
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    u_xlat1.x = (-u_xlat0.z);
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat0.x = u_xlat1.x * u_xlat3 + u_xlat0.x;
					    u_xlat6 = u_xlat1.x / u_xlat6;
					    u_xlat6 = min(u_xlat6, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * _Color.xyz;
					    u_xlat6 = (-u_xlat0.x) + _Fill;
					    u_xlat4 = _OutlineWidth * _LineWidth + (-u_xlat0.z);
					    u_xlat4 = clamp(u_xlat4, 0.0, 1.0);
					    u_xlat4 = (-u_xlat4) + 1.0;
					    u_xlat0.x = u_xlat4 * u_xlat6 + u_xlat0.x;
					    u_xlat0.x = u_xlat0.y * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x * _Color.w;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_LOWEND_ON" }
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
						vec4 unused_0_2[2];
					};
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0 = _Color * vec4(1.0, 1.0, 1.0, 0.699999988);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_LOWEND_ON" }
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
						vec4 unused_0_2[2];
					};
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0 = _Color * vec4(1.0, 1.0, 1.0, 0.699999988);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_VOXELAO_ON" }
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
						vec4 _Color;
						float _Shade;
						float _ShadeOffset;
						float _Fill;
						float _Outline;
						float _OutlineWidth;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[5];
						vec4 _ProjectionParams;
						vec4 unused_1_2[3];
					};
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					int u_xlati0;
					vec2 u_xlat1;
					vec2 u_xlat2;
					int u_xlati2;
					float u_xlat3;
					float u_xlat4;
					float u_xlat6;
					void main()
					{
					    u_xlati0 = int((0.0<_ProjectionParams.x) ? 0xFFFFFFFFu : uint(0));
					    u_xlati2 = int((_ProjectionParams.x<0.0) ? 0xFFFFFFFFu : uint(0));
					    u_xlati0 = u_xlati0 + (-u_xlati2);
					    u_xlat0.x = float(u_xlati0);
					    u_xlat2.xy = (-vs_TEXCOORD0.yy) + vec2(0.920000017, 1.0);
					    u_xlat6 = dFdx(u_xlat2.x);
					    u_xlat1.x = dFdy(u_xlat2.x);
					    u_xlat6 = abs(u_xlat6) + abs(u_xlat1.x);
					    u_xlat1.x = _AAFactor + 1.0;
					    u_xlat6 = u_xlat6 * u_xlat1.x;
					    u_xlat0.yz = u_xlat2.yx / vec2(u_xlat6);
					    u_xlat0.x = u_xlat0.x * u_xlat0.z;
					    u_xlat1.y = dFdy(u_xlat0.x);
					    u_xlat1.x = dFdx(u_xlat0.z);
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = inversesqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * u_xlat1.y;
					    u_xlat6 = (-u_xlat0.x) * _ShadeOffset;
					    u_xlat0.xy = u_xlat0.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * _LineWidth + (-u_xlat0.z);
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = (-u_xlat6) + 1.0;
					    u_xlat1.x = u_xlat6 * _Shade;
					    u_xlat6 = max(u_xlat6, 0.00100000005);
					    u_xlat3 = max(_Fill, _Outline);
					    u_xlat3 = (-u_xlat1.x) * u_xlat0.x + u_xlat3;
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    u_xlat1.x = (-u_xlat0.z);
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat0.x = u_xlat1.x * u_xlat3 + u_xlat0.x;
					    u_xlat6 = u_xlat1.x / u_xlat6;
					    u_xlat6 = min(u_xlat6, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * _Color.xyz;
					    u_xlat6 = (-u_xlat0.x) + _Fill;
					    u_xlat4 = _OutlineWidth * _LineWidth + (-u_xlat0.z);
					    u_xlat4 = clamp(u_xlat4, 0.0, 1.0);
					    u_xlat4 = (-u_xlat4) + 1.0;
					    u_xlat0.x = u_xlat4 * u_xlat6 + u_xlat0.x;
					    u_xlat0.x = u_xlat0.y * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x * _Color.w;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_VOXELAO_ON" }
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
						vec4 _Color;
						float _Shade;
						float _ShadeOffset;
						float _Fill;
						float _Outline;
						float _OutlineWidth;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[5];
						vec4 _ProjectionParams;
						vec4 unused_1_2[3];
					};
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					int u_xlati0;
					vec2 u_xlat1;
					vec2 u_xlat2;
					int u_xlati2;
					float u_xlat3;
					float u_xlat4;
					float u_xlat6;
					void main()
					{
					    u_xlati0 = int((0.0<_ProjectionParams.x) ? 0xFFFFFFFFu : uint(0));
					    u_xlati2 = int((_ProjectionParams.x<0.0) ? 0xFFFFFFFFu : uint(0));
					    u_xlati0 = u_xlati0 + (-u_xlati2);
					    u_xlat0.x = float(u_xlati0);
					    u_xlat2.xy = (-vs_TEXCOORD0.yy) + vec2(0.920000017, 1.0);
					    u_xlat6 = dFdx(u_xlat2.x);
					    u_xlat1.x = dFdy(u_xlat2.x);
					    u_xlat6 = abs(u_xlat6) + abs(u_xlat1.x);
					    u_xlat1.x = _AAFactor + 1.0;
					    u_xlat6 = u_xlat6 * u_xlat1.x;
					    u_xlat0.yz = u_xlat2.yx / vec2(u_xlat6);
					    u_xlat0.x = u_xlat0.x * u_xlat0.z;
					    u_xlat1.y = dFdy(u_xlat0.x);
					    u_xlat1.x = dFdx(u_xlat0.z);
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = inversesqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * u_xlat1.y;
					    u_xlat6 = (-u_xlat0.x) * _ShadeOffset;
					    u_xlat0.xy = u_xlat0.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat6 = u_xlat6 * _LineWidth + (-u_xlat0.z);
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = (-u_xlat6) + 1.0;
					    u_xlat1.x = u_xlat6 * _Shade;
					    u_xlat6 = max(u_xlat6, 0.00100000005);
					    u_xlat3 = max(_Fill, _Outline);
					    u_xlat3 = (-u_xlat1.x) * u_xlat0.x + u_xlat3;
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    u_xlat1.x = (-u_xlat0.z);
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat0.x = u_xlat1.x * u_xlat3 + u_xlat0.x;
					    u_xlat6 = u_xlat1.x / u_xlat6;
					    u_xlat6 = min(u_xlat6, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * _Color.xyz;
					    u_xlat6 = (-u_xlat0.x) + _Fill;
					    u_xlat4 = _OutlineWidth * _LineWidth + (-u_xlat0.z);
					    u_xlat4 = clamp(u_xlat4, 0.0, 1.0);
					    u_xlat4 = (-u_xlat4) + 1.0;
					    u_xlat0.x = u_xlat4 * u_xlat6 + u_xlat0.x;
					    u_xlat0.x = u_xlat0.y * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x * _Color.w;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_LOWEND_ON" "_VOXELAO_ON" }
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
						vec4 unused_0_2[2];
					};
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0 = _Color * vec4(1.0, 1.0, 1.0, 0.699999988);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_LOWEND_ON" "_VOXELAO_ON" }
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
						vec4 unused_0_2[2];
					};
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0 = _Color * vec4(1.0, 1.0, 1.0, 0.699999988);
					    return;
					}"
				}
			}
		}
	}
}