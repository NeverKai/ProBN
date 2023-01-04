Shader "Unlit/Flag" {
	Properties {
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
	}
	SubShader {
		LOD 100
		Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "RenderType" = "Opaque" }
		Pass {
			LOD 100
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "RenderType" = "Opaque" }
			AlphaToMask On
			Cull Off
			GpuProgramID 2038
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
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * -0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" }
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
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * -0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
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
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * -0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * -0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * 0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" }
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
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * 0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_LOWEND_ON" }
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
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * 0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * 0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * -0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * -0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
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
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * -0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * -0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
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
						vec4 unused_0_0[64];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * 0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * 0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_CINEMATIC_ON" "_LOWEND_ON" }
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
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * 0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat2.y = unity_MatrixV[1].z * 0.0199999996;
					    u_xlat2.x = unity_MatrixV[0].z * -0.0199999996;
					    u_xlat2.z = unity_MatrixV[2].z * -0.0199999996;
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat0.xyz = (-u_xlat2.xyz) + u_xlat1.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat2.x = dot(u_xlat0.xxx, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat2.x = u_xlat0.x / u_xlat2.x;
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    vs_COLOR0.xyz = u_xlat2.xxx * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xxx;
					    vs_COLOR0.w = 1.0;
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
						vec4 unused_0_27[2];
						vec4 _MainTex_ST;
						vec4 unused_0_29[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat19;
					float u_xlat20;
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
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat2.z = unity_MatrixV[2].z;
					    u_xlat10.xyz = (-u_xlat2.xyz) * vec3(-0.0199999996, -0.0199999996, -0.0199999996) + u_xlat1.xyz;
					    u_xlat31 = u_xlat10.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat31 = dot(u_xlat10.xz, u_xlat10.xz);
					    u_xlat31 = sqrt(u_xlat31);
					    u_xlat31 = u_xlat31 + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat3 = u_xlat10.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat10.xxxx + u_xlat3;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat10.zzzz + u_xlat3;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat1.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat9.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat9.yyyy;
					    u_xlat6 = u_xlat9.zzzz * u_xlat6;
					    u_xlat8 = u_xlat8 * u_xlat9.xxxx;
					    u_xlat8 = u_xlat9.yyyy * u_xlat8;
					    u_xlat6 = u_xlat8 * u_xlat9.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat1.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat9.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat9.zzzz + u_xlat6;
					    u_xlat3 = u_xlat4 * u_xlat9.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat6 = u_xlat9.yyyy * u_xlat6;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat9.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat3 = u_xlat6 * u_xlat1.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat1.xxxx * u_xlat5;
					    u_xlat5 = u_xlat1.yyyy * u_xlat5;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat1 = u_xlat5 * u_xlat1.zzzz + u_xlat3;
					    u_xlat30 = u_xlat1.w * 0.400000006;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + in_TEXCOORD1.y;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat0.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat33;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat10.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat10.x;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat0.xyz * u_xlat3.xyz;
					    u_xlat0.x = u_xlat12.y + u_xlat12.x;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + u_xlat30;
					    u_xlat10.x = u_xlat31;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat20 = max(u_xlat10.x, u_xlat31);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat10.xxx * u_xlat3.yzx;
					    u_xlat10.xyz = u_xlat3.xyz * vec3(u_xlat20) + u_xlat4.xyz;
					    u_xlat10.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat10.xyz;
					    u_xlat3.xyz = (-u_xlat10.xyz) + _SnowColor.xyz;
					    u_xlat10.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat10.xyz;
					    u_xlat3.xyz = u_xlat10.xyz * _MinAmbientColor.xyz;
					    u_xlat10.xyz = (-_MinAmbientColor.xyz) * u_xlat10.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz;
					    u_xlat1.w = (-u_xlat1.x);
					    u_xlat3.xyz = u_xlat1.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat30 = dot(u_xlat1.xyz, _FlashDir.xyz);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30) + 1.0;
					    u_xlat1.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat1.x = u_xlat1.y + u_xlat1.x;
					    u_xlat1.x = u_xlat12.z * u_xlat3.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat2.x;
					    u_xlat11.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat11.xyz * u_xlat1.xxx + u_xlat0.xyz;
					    u_xlat1.x = u_xlat30 * u_xlat30;
					    u_xlat30 = (-u_xlat30) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = vec3(u_xlat30) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_27[2];
						vec4 _MainTex_ST;
						vec4 unused_0_29[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat19;
					float u_xlat20;
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
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat2.z = unity_MatrixV[2].z;
					    u_xlat10.xyz = (-u_xlat2.xyz) * vec3(-0.0199999996, -0.0199999996, -0.0199999996) + u_xlat1.xyz;
					    u_xlat31 = u_xlat10.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat31 = dot(u_xlat10.xz, u_xlat10.xz);
					    u_xlat31 = sqrt(u_xlat31);
					    u_xlat31 = u_xlat31 + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat3 = u_xlat10.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat10.xxxx + u_xlat3;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat10.zzzz + u_xlat3;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat1.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat9.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat9.yyyy;
					    u_xlat6 = u_xlat9.zzzz * u_xlat6;
					    u_xlat8 = u_xlat8 * u_xlat9.xxxx;
					    u_xlat8 = u_xlat9.yyyy * u_xlat8;
					    u_xlat6 = u_xlat8 * u_xlat9.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat1.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat9.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat9.zzzz + u_xlat6;
					    u_xlat3 = u_xlat4 * u_xlat9.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat6 = u_xlat9.yyyy * u_xlat6;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat9.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat3 = u_xlat6 * u_xlat1.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat1.xxxx * u_xlat5;
					    u_xlat5 = u_xlat1.yyyy * u_xlat5;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat1 = u_xlat5 * u_xlat1.zzzz + u_xlat3;
					    u_xlat30 = u_xlat1.w * 0.400000006;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + in_TEXCOORD1.y;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat0.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat33;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat10.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat10.x;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat0.xyz * u_xlat3.xyz;
					    u_xlat0.x = u_xlat12.y + u_xlat12.x;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + u_xlat30;
					    u_xlat10.x = u_xlat31;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat20 = max(u_xlat10.x, u_xlat31);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat10.xxx * u_xlat3.yzx;
					    u_xlat10.xyz = u_xlat3.xyz * vec3(u_xlat20) + u_xlat4.xyz;
					    u_xlat10.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat10.xyz;
					    u_xlat3.xyz = (-u_xlat10.xyz) + _SnowColor.xyz;
					    u_xlat10.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat10.xyz;
					    u_xlat3.xyz = u_xlat10.xyz * _MinAmbientColor.xyz;
					    u_xlat10.xyz = (-_MinAmbientColor.xyz) * u_xlat10.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz;
					    u_xlat1.w = (-u_xlat1.x);
					    u_xlat3.xyz = u_xlat1.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat30 = dot(u_xlat1.xyz, _FlashDir.xyz);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30) + 1.0;
					    u_xlat1.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat1.x = u_xlat1.y + u_xlat1.x;
					    u_xlat1.x = u_xlat12.z * u_xlat3.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat2.x;
					    u_xlat11.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat11.xyz * u_xlat1.xxx + u_xlat0.xyz;
					    u_xlat1.x = u_xlat30 * u_xlat30;
					    u_xlat30 = (-u_xlat30) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = vec3(u_xlat30) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_LOWEND_ON" }
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
						vec4 unused_0_27[2];
						vec4 _MainTex_ST;
						vec4 unused_0_29[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat19;
					float u_xlat20;
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
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat2.z = unity_MatrixV[2].z;
					    u_xlat10.xyz = (-u_xlat2.xyz) * vec3(-0.0199999996, -0.0199999996, -0.0199999996) + u_xlat1.xyz;
					    u_xlat31 = u_xlat10.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat31 = dot(u_xlat10.xz, u_xlat10.xz);
					    u_xlat31 = sqrt(u_xlat31);
					    u_xlat31 = u_xlat31 + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat3 = u_xlat10.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat10.xxxx + u_xlat3;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat10.zzzz + u_xlat3;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat1.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat9.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat9.yyyy;
					    u_xlat6 = u_xlat9.zzzz * u_xlat6;
					    u_xlat8 = u_xlat8 * u_xlat9.xxxx;
					    u_xlat8 = u_xlat9.yyyy * u_xlat8;
					    u_xlat6 = u_xlat8 * u_xlat9.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat1.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat9.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat9.zzzz + u_xlat6;
					    u_xlat3 = u_xlat4 * u_xlat9.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat6 = u_xlat9.yyyy * u_xlat6;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat9.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat3 = u_xlat6 * u_xlat1.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat1.xxxx * u_xlat5;
					    u_xlat5 = u_xlat1.yyyy * u_xlat5;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat1 = u_xlat5 * u_xlat1.zzzz + u_xlat3;
					    u_xlat30 = u_xlat1.w * 0.400000006;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + in_TEXCOORD1.y;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat0.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat33;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat10.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat10.x;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat0.xyz * u_xlat3.xyz;
					    u_xlat0.x = u_xlat12.y + u_xlat12.x;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + u_xlat30;
					    u_xlat10.x = u_xlat31;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat20 = max(u_xlat10.x, u_xlat31);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat10.xxx * u_xlat3.yzx;
					    u_xlat10.xyz = u_xlat3.xyz * vec3(u_xlat20) + u_xlat4.xyz;
					    u_xlat10.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat10.xyz;
					    u_xlat3.xyz = (-u_xlat10.xyz) + _SnowColor.xyz;
					    u_xlat10.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat10.xyz;
					    u_xlat3.xyz = u_xlat10.xyz * _MinAmbientColor.xyz;
					    u_xlat10.xyz = (-_MinAmbientColor.xyz) * u_xlat10.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz;
					    u_xlat1.w = (-u_xlat1.x);
					    u_xlat3.xyz = u_xlat1.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat30 = dot(u_xlat1.xyz, _FlashDir.xyz);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30) + 1.0;
					    u_xlat1.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat1.x = u_xlat1.y + u_xlat1.x;
					    u_xlat1.x = u_xlat12.z * u_xlat3.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat2.x;
					    u_xlat11.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat11.xyz * u_xlat1.xxx + u_xlat0.xyz;
					    u_xlat1.x = u_xlat30 * u_xlat30;
					    u_xlat30 = (-u_xlat30) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = vec3(u_xlat30) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_27[2];
						vec4 _MainTex_ST;
						vec4 unused_0_29[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat19;
					float u_xlat20;
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
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat2.z = unity_MatrixV[2].z;
					    u_xlat10.xyz = (-u_xlat2.xyz) * vec3(-0.0199999996, -0.0199999996, -0.0199999996) + u_xlat1.xyz;
					    u_xlat31 = u_xlat10.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat31 = dot(u_xlat10.xz, u_xlat10.xz);
					    u_xlat31 = sqrt(u_xlat31);
					    u_xlat31 = u_xlat31 + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat3 = u_xlat10.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat10.xxxx + u_xlat3;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat10.zzzz + u_xlat3;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat1.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat9.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat9.yyyy;
					    u_xlat6 = u_xlat9.zzzz * u_xlat6;
					    u_xlat8 = u_xlat8 * u_xlat9.xxxx;
					    u_xlat8 = u_xlat9.yyyy * u_xlat8;
					    u_xlat6 = u_xlat8 * u_xlat9.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat1.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat9.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat9.zzzz + u_xlat6;
					    u_xlat3 = u_xlat4 * u_xlat9.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat6 = u_xlat9.yyyy * u_xlat6;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat9.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat3 = u_xlat6 * u_xlat1.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat1.xxxx * u_xlat5;
					    u_xlat5 = u_xlat1.yyyy * u_xlat5;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat1 = u_xlat5 * u_xlat1.zzzz + u_xlat3;
					    u_xlat30 = u_xlat1.w * 0.400000006;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + in_TEXCOORD1.y;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat0.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat33;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat10.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat10.x;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat0.xyz * u_xlat3.xyz;
					    u_xlat0.x = u_xlat12.y + u_xlat12.x;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + u_xlat30;
					    u_xlat10.x = u_xlat31;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat20 = max(u_xlat10.x, u_xlat31);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat10.xxx * u_xlat3.yzx;
					    u_xlat10.xyz = u_xlat3.xyz * vec3(u_xlat20) + u_xlat4.xyz;
					    u_xlat10.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat10.xyz;
					    u_xlat3.xyz = (-u_xlat10.xyz) + _SnowColor.xyz;
					    u_xlat10.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat10.xyz;
					    u_xlat3.xyz = u_xlat10.xyz * _MinAmbientColor.xyz;
					    u_xlat10.xyz = (-_MinAmbientColor.xyz) * u_xlat10.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz;
					    u_xlat1.w = (-u_xlat1.x);
					    u_xlat3.xyz = u_xlat1.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat30 = dot(u_xlat1.xyz, _FlashDir.xyz);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30) + 1.0;
					    u_xlat1.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat1.x = u_xlat1.y + u_xlat1.x;
					    u_xlat1.x = u_xlat12.z * u_xlat3.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat2.x;
					    u_xlat11.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat11.xyz * u_xlat1.xxx + u_xlat0.xyz;
					    u_xlat1.x = u_xlat30 * u_xlat30;
					    u_xlat30 = (-u_xlat30) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = vec3(u_xlat30) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
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
						vec4 unused_0_23[4];
						vec4 _MainTex_ST;
						vec4 unused_0_25[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat18;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.z = unity_MatrixV[2].z;
					    u_xlat10.xy = u_xlat1.xz / unity_MatrixV[1].zz;
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat2.xy = in_TEXCOORD1.xy;
					    u_xlat2.z = in_TEXCOORD2.x;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(-0.0199999996, 0.0199999996, -0.0199999996) + u_xlat2.xyz;
					    u_xlat1.xz = (-u_xlat1.xz);
					    u_xlat30 = u_xlat3.y + (-_WaterLevel);
					    u_xlat10.xy = u_xlat10.xy * vec2(u_xlat30) + u_xlat3.xz;
					    u_xlat10.x = dot(u_xlat10.xy, u_xlat10.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat0.x = u_xlat30 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _WaterLevel * 2.0 + (-u_xlat3.y);
					    u_xlat0 = u_xlat0.xxxx * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat2.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat2.xyz);
					    u_xlat2.xyz = fract(u_xlat2.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat9.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat9.yyy;
					    u_xlat6.xyz = u_xlat9.zzz * u_xlat6.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat9.xxx;
					    u_xlat8.xyz = u_xlat9.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat8.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4.xyz = u_xlat2.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat9.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat9.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.xyw * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat1.xyz = (-u_xlat1.xyz) + (-u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat1 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat1.xyz);
					    u_xlat30 = (-u_xlat1.w) * 0.5 + in_TEXCOORD1.y;
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
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat1.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_23[4];
						vec4 _MainTex_ST;
						vec4 unused_0_25[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat18;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.z = unity_MatrixV[2].z;
					    u_xlat10.xy = u_xlat1.xz / unity_MatrixV[1].zz;
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat2.xy = in_TEXCOORD1.xy;
					    u_xlat2.z = in_TEXCOORD2.x;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(-0.0199999996, 0.0199999996, -0.0199999996) + u_xlat2.xyz;
					    u_xlat1.xz = (-u_xlat1.xz);
					    u_xlat30 = u_xlat3.y + (-_WaterLevel);
					    u_xlat10.xy = u_xlat10.xy * vec2(u_xlat30) + u_xlat3.xz;
					    u_xlat10.x = dot(u_xlat10.xy, u_xlat10.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat0.x = u_xlat30 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _WaterLevel * 2.0 + (-u_xlat3.y);
					    u_xlat0 = u_xlat0.xxxx * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat2.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat2.xyz);
					    u_xlat2.xyz = fract(u_xlat2.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat9.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat9.yyy;
					    u_xlat6.xyz = u_xlat9.zzz * u_xlat6.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat9.xxx;
					    u_xlat8.xyz = u_xlat9.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat8.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4.xyz = u_xlat2.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat9.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat9.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.xyw * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat1.xyz = (-u_xlat1.xyz) + (-u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat1 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat1.xyz);
					    u_xlat30 = (-u_xlat1.w) * 0.5 + in_TEXCOORD1.y;
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
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat1.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_LOWEND_ON" }
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
						vec4 unused_0_23[4];
						vec4 _MainTex_ST;
						vec4 unused_0_25[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat18;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.z = unity_MatrixV[2].z;
					    u_xlat10.xy = u_xlat1.xz / unity_MatrixV[1].zz;
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat2.xy = in_TEXCOORD1.xy;
					    u_xlat2.z = in_TEXCOORD2.x;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(-0.0199999996, 0.0199999996, -0.0199999996) + u_xlat2.xyz;
					    u_xlat1.xz = (-u_xlat1.xz);
					    u_xlat30 = u_xlat3.y + (-_WaterLevel);
					    u_xlat10.xy = u_xlat10.xy * vec2(u_xlat30) + u_xlat3.xz;
					    u_xlat10.x = dot(u_xlat10.xy, u_xlat10.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat0.x = u_xlat30 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _WaterLevel * 2.0 + (-u_xlat3.y);
					    u_xlat0 = u_xlat0.xxxx * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat2.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat2.xyz);
					    u_xlat2.xyz = fract(u_xlat2.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat9.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat9.yyy;
					    u_xlat6.xyz = u_xlat9.zzz * u_xlat6.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat9.xxx;
					    u_xlat8.xyz = u_xlat9.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat8.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4.xyz = u_xlat2.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat9.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat9.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.xyw * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat1.xyz = (-u_xlat1.xyz) + (-u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat1 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat1.xyz);
					    u_xlat30 = (-u_xlat1.w) * 0.5 + in_TEXCOORD1.y;
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
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat1.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_23[4];
						vec4 _MainTex_ST;
						vec4 unused_0_25[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat18;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.z = unity_MatrixV[2].z;
					    u_xlat10.xy = u_xlat1.xz / unity_MatrixV[1].zz;
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat2.xy = in_TEXCOORD1.xy;
					    u_xlat2.z = in_TEXCOORD2.x;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(-0.0199999996, 0.0199999996, -0.0199999996) + u_xlat2.xyz;
					    u_xlat1.xz = (-u_xlat1.xz);
					    u_xlat30 = u_xlat3.y + (-_WaterLevel);
					    u_xlat10.xy = u_xlat10.xy * vec2(u_xlat30) + u_xlat3.xz;
					    u_xlat10.x = dot(u_xlat10.xy, u_xlat10.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat0.x = u_xlat30 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _WaterLevel * 2.0 + (-u_xlat3.y);
					    u_xlat0 = u_xlat0.xxxx * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat2.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat2.xyz);
					    u_xlat2.xyz = fract(u_xlat2.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat9.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat9.yyy;
					    u_xlat6.xyz = u_xlat9.zzz * u_xlat6.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat9.xxx;
					    u_xlat8.xyz = u_xlat9.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat8.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4.xyz = u_xlat2.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat9.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat9.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.xyw * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat1.xyz = (-u_xlat1.xyz) + (-u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat1 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat1.xyz);
					    u_xlat30 = (-u_xlat1.w) * 0.5 + in_TEXCOORD1.y;
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
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat1.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
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
						vec4 unused_0_27[2];
						vec4 _MainTex_ST;
						vec4 unused_0_29[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat19;
					float u_xlat20;
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
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat2.z = unity_MatrixV[2].z;
					    u_xlat10.xyz = (-u_xlat2.xyz) * vec3(-0.0199999996, -0.0199999996, -0.0199999996) + u_xlat1.xyz;
					    u_xlat31 = u_xlat10.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat31 = dot(u_xlat10.xz, u_xlat10.xz);
					    u_xlat31 = sqrt(u_xlat31);
					    u_xlat31 = u_xlat31 + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat3 = u_xlat10.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat10.xxxx + u_xlat3;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat10.zzzz + u_xlat3;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat1.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat9.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat9.yyyy;
					    u_xlat6 = u_xlat9.zzzz * u_xlat6;
					    u_xlat8 = u_xlat8 * u_xlat9.xxxx;
					    u_xlat8 = u_xlat9.yyyy * u_xlat8;
					    u_xlat6 = u_xlat8 * u_xlat9.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat1.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat9.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat9.zzzz + u_xlat6;
					    u_xlat3 = u_xlat4 * u_xlat9.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat6 = u_xlat9.yyyy * u_xlat6;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat9.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat3 = u_xlat6 * u_xlat1.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat1.xxxx * u_xlat5;
					    u_xlat5 = u_xlat1.yyyy * u_xlat5;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat1 = u_xlat5 * u_xlat1.zzzz + u_xlat3;
					    u_xlat30 = u_xlat1.w * 0.400000006;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + in_TEXCOORD1.y;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat0.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat33;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat10.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat10.x;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat0.xyz * u_xlat3.xyz;
					    u_xlat0.x = u_xlat12.y + u_xlat12.x;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + u_xlat30;
					    u_xlat10.x = u_xlat31;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat20 = max(u_xlat10.x, u_xlat31);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat10.xxx * u_xlat3.yzx;
					    u_xlat10.xyz = u_xlat3.xyz * vec3(u_xlat20) + u_xlat4.xyz;
					    u_xlat10.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat10.xyz;
					    u_xlat3.xyz = (-u_xlat10.xyz) + _SnowColor.xyz;
					    u_xlat10.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat10.xyz;
					    u_xlat3.xyz = u_xlat10.xyz * _MinAmbientColor.xyz;
					    u_xlat10.xyz = (-_MinAmbientColor.xyz) * u_xlat10.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz;
					    u_xlat1.w = (-u_xlat1.x);
					    u_xlat3.xyz = u_xlat1.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat30 = dot(u_xlat1.xyz, _FlashDir.xyz);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30) + 1.0;
					    u_xlat1.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat1.x = u_xlat1.y + u_xlat1.x;
					    u_xlat1.x = u_xlat12.z * u_xlat3.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat2.x;
					    u_xlat11.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat11.xyz * u_xlat1.xxx + u_xlat0.xyz;
					    u_xlat1.x = u_xlat30 * u_xlat30;
					    u_xlat30 = (-u_xlat30) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = vec3(u_xlat30) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_27[2];
						vec4 _MainTex_ST;
						vec4 unused_0_29[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat19;
					float u_xlat20;
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
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat2.z = unity_MatrixV[2].z;
					    u_xlat10.xyz = (-u_xlat2.xyz) * vec3(-0.0199999996, -0.0199999996, -0.0199999996) + u_xlat1.xyz;
					    u_xlat31 = u_xlat10.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat31 = dot(u_xlat10.xz, u_xlat10.xz);
					    u_xlat31 = sqrt(u_xlat31);
					    u_xlat31 = u_xlat31 + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat3 = u_xlat10.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat10.xxxx + u_xlat3;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat10.zzzz + u_xlat3;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat1.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat9.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat9.yyyy;
					    u_xlat6 = u_xlat9.zzzz * u_xlat6;
					    u_xlat8 = u_xlat8 * u_xlat9.xxxx;
					    u_xlat8 = u_xlat9.yyyy * u_xlat8;
					    u_xlat6 = u_xlat8 * u_xlat9.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat1.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat9.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat9.zzzz + u_xlat6;
					    u_xlat3 = u_xlat4 * u_xlat9.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat6 = u_xlat9.yyyy * u_xlat6;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat9.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat3 = u_xlat6 * u_xlat1.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat1.xxxx * u_xlat5;
					    u_xlat5 = u_xlat1.yyyy * u_xlat5;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat1 = u_xlat5 * u_xlat1.zzzz + u_xlat3;
					    u_xlat30 = u_xlat1.w * 0.400000006;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + in_TEXCOORD1.y;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat0.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat33;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat10.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat10.x;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat0.xyz * u_xlat3.xyz;
					    u_xlat0.x = u_xlat12.y + u_xlat12.x;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + u_xlat30;
					    u_xlat10.x = u_xlat31;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat20 = max(u_xlat10.x, u_xlat31);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat10.xxx * u_xlat3.yzx;
					    u_xlat10.xyz = u_xlat3.xyz * vec3(u_xlat20) + u_xlat4.xyz;
					    u_xlat10.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat10.xyz;
					    u_xlat3.xyz = (-u_xlat10.xyz) + _SnowColor.xyz;
					    u_xlat10.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat10.xyz;
					    u_xlat3.xyz = u_xlat10.xyz * _MinAmbientColor.xyz;
					    u_xlat10.xyz = (-_MinAmbientColor.xyz) * u_xlat10.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz;
					    u_xlat1.w = (-u_xlat1.x);
					    u_xlat3.xyz = u_xlat1.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat30 = dot(u_xlat1.xyz, _FlashDir.xyz);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30) + 1.0;
					    u_xlat1.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat1.x = u_xlat1.y + u_xlat1.x;
					    u_xlat1.x = u_xlat12.z * u_xlat3.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat2.x;
					    u_xlat11.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat11.xyz * u_xlat1.xxx + u_xlat0.xyz;
					    u_xlat1.x = u_xlat30 * u_xlat30;
					    u_xlat30 = (-u_xlat30) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = vec3(u_xlat30) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_LOWEND_ON" }
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
						vec4 unused_0_27[2];
						vec4 _MainTex_ST;
						vec4 unused_0_29[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat19;
					float u_xlat20;
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
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat2.z = unity_MatrixV[2].z;
					    u_xlat10.xyz = (-u_xlat2.xyz) * vec3(-0.0199999996, -0.0199999996, -0.0199999996) + u_xlat1.xyz;
					    u_xlat31 = u_xlat10.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat31 = dot(u_xlat10.xz, u_xlat10.xz);
					    u_xlat31 = sqrt(u_xlat31);
					    u_xlat31 = u_xlat31 + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat3 = u_xlat10.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat10.xxxx + u_xlat3;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat10.zzzz + u_xlat3;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat1.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat9.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat9.yyyy;
					    u_xlat6 = u_xlat9.zzzz * u_xlat6;
					    u_xlat8 = u_xlat8 * u_xlat9.xxxx;
					    u_xlat8 = u_xlat9.yyyy * u_xlat8;
					    u_xlat6 = u_xlat8 * u_xlat9.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat1.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat9.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat9.zzzz + u_xlat6;
					    u_xlat3 = u_xlat4 * u_xlat9.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat6 = u_xlat9.yyyy * u_xlat6;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat9.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat3 = u_xlat6 * u_xlat1.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat1.xxxx * u_xlat5;
					    u_xlat5 = u_xlat1.yyyy * u_xlat5;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat1 = u_xlat5 * u_xlat1.zzzz + u_xlat3;
					    u_xlat30 = u_xlat1.w * 0.400000006;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + in_TEXCOORD1.y;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat0.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat33;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat10.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat10.x;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat0.xyz * u_xlat3.xyz;
					    u_xlat0.x = u_xlat12.y + u_xlat12.x;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + u_xlat30;
					    u_xlat10.x = u_xlat31;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat20 = max(u_xlat10.x, u_xlat31);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat10.xxx * u_xlat3.yzx;
					    u_xlat10.xyz = u_xlat3.xyz * vec3(u_xlat20) + u_xlat4.xyz;
					    u_xlat10.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat10.xyz;
					    u_xlat3.xyz = (-u_xlat10.xyz) + _SnowColor.xyz;
					    u_xlat10.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat10.xyz;
					    u_xlat3.xyz = u_xlat10.xyz * _MinAmbientColor.xyz;
					    u_xlat10.xyz = (-_MinAmbientColor.xyz) * u_xlat10.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz;
					    u_xlat1.w = (-u_xlat1.x);
					    u_xlat3.xyz = u_xlat1.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat30 = dot(u_xlat1.xyz, _FlashDir.xyz);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30) + 1.0;
					    u_xlat1.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat1.x = u_xlat1.y + u_xlat1.x;
					    u_xlat1.x = u_xlat12.z * u_xlat3.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat2.x;
					    u_xlat11.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat11.xyz * u_xlat1.xxx + u_xlat0.xyz;
					    u_xlat1.x = u_xlat30 * u_xlat30;
					    u_xlat30 = (-u_xlat30) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = vec3(u_xlat30) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_27[2];
						vec4 _MainTex_ST;
						vec4 unused_0_29[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat19;
					float u_xlat20;
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
					    u_xlat1.xy = in_TEXCOORD1.xy;
					    u_xlat1.z = in_TEXCOORD2.x;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat2.z = unity_MatrixV[2].z;
					    u_xlat10.xyz = (-u_xlat2.xyz) * vec3(-0.0199999996, -0.0199999996, -0.0199999996) + u_xlat1.xyz;
					    u_xlat31 = u_xlat10.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat31 = dot(u_xlat10.xz, u_xlat10.xz);
					    u_xlat31 = sqrt(u_xlat31);
					    u_xlat31 = u_xlat31 + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat31 / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat3 = u_xlat10.yyyy * unity_MatrixVP[1];
					    u_xlat3 = unity_MatrixVP[0] * u_xlat10.xxxx + u_xlat3;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat10.zzzz + u_xlat3;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat1.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat9.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat9.yyyy;
					    u_xlat6 = u_xlat9.zzzz * u_xlat6;
					    u_xlat8 = u_xlat8 * u_xlat9.xxxx;
					    u_xlat8 = u_xlat9.yyyy * u_xlat8;
					    u_xlat6 = u_xlat8 * u_xlat9.zzzz + u_xlat6;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat1.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat9.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat9.zzzz + u_xlat6;
					    u_xlat3 = u_xlat4 * u_xlat9.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat6 = u_xlat9.yyyy * u_xlat6;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat9.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat3 = u_xlat6 * u_xlat1.zzzz + u_xlat3;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat1.xxxx * u_xlat5;
					    u_xlat5 = u_xlat1.yyyy * u_xlat5;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.zzzz + u_xlat3;
					    u_xlat1 = u_xlat5 * u_xlat1.zzzz + u_xlat3;
					    u_xlat30 = u_xlat1.w * 0.400000006;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat31 = (-u_xlat2.w) * 0.5 + in_TEXCOORD1.y;
					    u_xlat31 = u_xlat31 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat32 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat33 = u_xlat0.y * u_xlat32;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat33;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat33;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat10.x = u_xlat32 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat10.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat10.x;
					    u_xlat19.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat12.xyz = u_xlat0.xyz * u_xlat3.xyz;
					    u_xlat0.x = u_xlat12.y + u_xlat12.x;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + u_xlat30;
					    u_xlat10.x = u_xlat31;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat31 = (-u_xlat31);
					    u_xlat31 = clamp(u_xlat31, 0.0, 1.0);
					    u_xlat20 = max(u_xlat10.x, u_xlat31);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat10.xxx * u_xlat3.yzx;
					    u_xlat10.xyz = u_xlat3.xyz * vec3(u_xlat20) + u_xlat4.xyz;
					    u_xlat10.xyz = vec3(u_xlat31) * u_xlat3.zxy + u_xlat10.xyz;
					    u_xlat3.xyz = (-u_xlat10.xyz) + _SnowColor.xyz;
					    u_xlat10.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat10.xyz;
					    u_xlat3.xyz = u_xlat10.xyz * _MinAmbientColor.xyz;
					    u_xlat10.xyz = (-_MinAmbientColor.xyz) * u_xlat10.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat3.xyz;
					    u_xlat30 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz;
					    u_xlat1.w = (-u_xlat1.x);
					    u_xlat3.xyz = u_xlat1.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat30 = dot(u_xlat1.xyz, _FlashDir.xyz);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30) + 1.0;
					    u_xlat1.xy = u_xlat12.xy * u_xlat3.xy;
					    u_xlat1.x = u_xlat1.y + u_xlat1.x;
					    u_xlat1.x = u_xlat12.z * u_xlat3.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat2.x;
					    u_xlat11.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat11.xyz * u_xlat1.xxx + u_xlat0.xyz;
					    u_xlat1.x = u_xlat30 * u_xlat30;
					    u_xlat30 = (-u_xlat30) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = vec3(u_xlat30) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
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
						vec4 unused_0_23[4];
						vec4 _MainTex_ST;
						vec4 unused_0_25[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat18;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.z = unity_MatrixV[2].z;
					    u_xlat10.xy = u_xlat1.xz / unity_MatrixV[1].zz;
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat2.xy = in_TEXCOORD1.xy;
					    u_xlat2.z = in_TEXCOORD2.x;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(-0.0199999996, 0.0199999996, -0.0199999996) + u_xlat2.xyz;
					    u_xlat1.xz = (-u_xlat1.xz);
					    u_xlat30 = u_xlat3.y + (-_WaterLevel);
					    u_xlat10.xy = u_xlat10.xy * vec2(u_xlat30) + u_xlat3.xz;
					    u_xlat10.x = dot(u_xlat10.xy, u_xlat10.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat0.x = u_xlat30 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _WaterLevel * 2.0 + (-u_xlat3.y);
					    u_xlat0 = u_xlat0.xxxx * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat2.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat2.xyz);
					    u_xlat2.xyz = fract(u_xlat2.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat9.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat9.yyy;
					    u_xlat6.xyz = u_xlat9.zzz * u_xlat6.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat9.xxx;
					    u_xlat8.xyz = u_xlat9.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat8.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4.xyz = u_xlat2.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat9.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat9.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.xyw * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat1.xyz = (-u_xlat1.xyz) + (-u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat1 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat1.xyz);
					    u_xlat30 = (-u_xlat1.w) * 0.5 + in_TEXCOORD1.y;
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
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat1.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_23[4];
						vec4 _MainTex_ST;
						vec4 unused_0_25[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat18;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.z = unity_MatrixV[2].z;
					    u_xlat10.xy = u_xlat1.xz / unity_MatrixV[1].zz;
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat2.xy = in_TEXCOORD1.xy;
					    u_xlat2.z = in_TEXCOORD2.x;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(-0.0199999996, 0.0199999996, -0.0199999996) + u_xlat2.xyz;
					    u_xlat1.xz = (-u_xlat1.xz);
					    u_xlat30 = u_xlat3.y + (-_WaterLevel);
					    u_xlat10.xy = u_xlat10.xy * vec2(u_xlat30) + u_xlat3.xz;
					    u_xlat10.x = dot(u_xlat10.xy, u_xlat10.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat0.x = u_xlat30 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _WaterLevel * 2.0 + (-u_xlat3.y);
					    u_xlat0 = u_xlat0.xxxx * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat2.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat2.xyz);
					    u_xlat2.xyz = fract(u_xlat2.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat9.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat9.yyy;
					    u_xlat6.xyz = u_xlat9.zzz * u_xlat6.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat9.xxx;
					    u_xlat8.xyz = u_xlat9.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat8.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4.xyz = u_xlat2.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat9.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat9.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.xyw * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat1.xyz = (-u_xlat1.xyz) + (-u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat1 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat1.xyz);
					    u_xlat30 = (-u_xlat1.w) * 0.5 + in_TEXCOORD1.y;
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
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat1.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_CINEMATIC_ON" "_LOWEND_ON" }
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
						vec4 unused_0_23[4];
						vec4 _MainTex_ST;
						vec4 unused_0_25[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat18;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.z = unity_MatrixV[2].z;
					    u_xlat10.xy = u_xlat1.xz / unity_MatrixV[1].zz;
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat2.xy = in_TEXCOORD1.xy;
					    u_xlat2.z = in_TEXCOORD2.x;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(-0.0199999996, 0.0199999996, -0.0199999996) + u_xlat2.xyz;
					    u_xlat1.xz = (-u_xlat1.xz);
					    u_xlat30 = u_xlat3.y + (-_WaterLevel);
					    u_xlat10.xy = u_xlat10.xy * vec2(u_xlat30) + u_xlat3.xz;
					    u_xlat10.x = dot(u_xlat10.xy, u_xlat10.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat0.x = u_xlat30 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _WaterLevel * 2.0 + (-u_xlat3.y);
					    u_xlat0 = u_xlat0.xxxx * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat2.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat2.xyz);
					    u_xlat2.xyz = fract(u_xlat2.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat9.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat9.yyy;
					    u_xlat6.xyz = u_xlat9.zzz * u_xlat6.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat9.xxx;
					    u_xlat8.xyz = u_xlat9.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat8.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4.xyz = u_xlat2.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat9.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat9.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.xyw * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat1.xyz = (-u_xlat1.xyz) + (-u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat1 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat1.xyz);
					    u_xlat30 = (-u_xlat1.w) * 0.5 + in_TEXCOORD1.y;
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
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat1.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    vs_COLOR0.w = 1.0;
					    vs_COLOR1 = in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_23[4];
						vec4 _MainTex_ST;
						vec4 unused_0_25[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[6];
						vec4 _ScreenParams;
						vec4 unused_1_2;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
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
					vec3 u_xlat13;
					vec3 u_xlat17;
					vec3 u_xlat18;
					float u_xlat20;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.z = unity_MatrixV[2].z;
					    u_xlat10.xy = u_xlat1.xz / unity_MatrixV[1].zz;
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat2.xy = in_TEXCOORD1.xy;
					    u_xlat2.z = in_TEXCOORD2.x;
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(-0.0199999996, 0.0199999996, -0.0199999996) + u_xlat2.xyz;
					    u_xlat1.xz = (-u_xlat1.xz);
					    u_xlat30 = u_xlat3.y + (-_WaterLevel);
					    u_xlat10.xy = u_xlat10.xy * vec2(u_xlat30) + u_xlat3.xz;
					    u_xlat10.x = dot(u_xlat10.xy, u_xlat10.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat0.x = u_xlat30 / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = _WaterLevel * 2.0 + (-u_xlat3.y);
					    u_xlat0 = u_xlat0.xxxx * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat0.w = in_TEXCOORD1.y;
					    u_xlat2.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat3.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat2.xyz);
					    u_xlat2.xyz = fract(u_xlat2.xyz);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat30 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat13.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat31;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat31;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat9.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat9.yyy;
					    u_xlat6.xyz = u_xlat9.zzz * u_xlat6.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat9.xxx;
					    u_xlat8.xyz = u_xlat9.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat8.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat30 = u_xlat30 * u_xlat13.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat30;
					    u_xlat5.x = u_xlat4.x * u_xlat13.x + u_xlat30;
					    u_xlat17.x = u_xlat13.z * u_xlat4.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4.xyz = u_xlat2.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat9.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat9.zzz + u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat9.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.xyw * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat1.y = unity_MatrixV[1].z;
					    u_xlat1.xyz = (-u_xlat1.xyz) + (-u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat1 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat1.xyz);
					    u_xlat30 = (-u_xlat1.w) * 0.5 + in_TEXCOORD1.y;
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
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat1.xyz;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat0.xyz / vec3(u_xlat30);
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[2];
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_7;
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					vec2 u_xlat4;
					vec2 u_xlat8;
					float u_xlat12;
					void main()
					{
					    u_xlat0.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat0.xy = vs_TEXCOORD0.xy / u_xlat0.xy;
					    u_xlat8.xy = dFdx(u_xlat0.xy);
					    u_xlat0.xy = dFdy(u_xlat0.xy);
					    u_xlat0.xy = abs(u_xlat0.xy) + abs(u_xlat8.xy);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat0.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat8.x = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat4.xy = u_xlat4.xx * vec2(-0.5, 0.5) + u_xlat8.xx;
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat4.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat8.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat8.xy = (-u_xlat0.xy) + u_xlat8.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat1.xy = u_xlat1.yx * vec2(0.600000024, 0.400000006);
					    u_xlat0.xy = u_xlat0.xy / u_xlat8.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat8.x = vs_TEXCOORD1.y;
					    u_xlat8.x = clamp(u_xlat8.x, 0.0, 1.0);
					    u_xlat12 = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    u_xlat4.x = (-u_xlat0.y) + 1.0;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat12<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xzw = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xzw = u_xlat1.xxx * u_xlat0.xzw + vs_COLOR1.xyz;
					    u_xlat1.xzw = (-u_xlat0.xzw) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xzw = u_xlat1.yyy * u_xlat1.xzw + u_xlat0.xzw;
					    u_xlat0.xzw = u_xlat0.xzw * vs_COLOR0.xyz;
					    u_xlat1.xyz = u_xlat0.xzw * _Color.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.xzw = (-u_xlat0.xzw) * _Color.xyz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat4.xxx * u_xlat0.xzw + u_xlat1.xyz;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" }
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
						vec4 unused_0_4[2];
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_7;
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					bool u_xlatb1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					vec2 u_xlat5;
					float u_xlat8;
					bool u_xlatb8;
					float u_xlat9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat8 = u_xlat0.w + -0.5;
					    u_xlatb8 = u_xlat8<0.0;
					    if(((int(u_xlatb8) * int(0xffffffffu)))!=0){discard;}
					    u_xlat8 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat8 * -0.5;
					    u_xlat5.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat5.xy = vs_TEXCOORD0.xy / u_xlat5.xy;
					    u_xlat2.xy = dFdx(u_xlat5.xy);
					    u_xlat5.xy = dFdy(u_xlat5.xy);
					    u_xlat5.xy = abs(u_xlat5.xy) + abs(u_xlat2.xy);
					    u_xlat5.x = max(u_xlat5.y, u_xlat5.x);
					    u_xlat1.x = u_xlat5.x * u_xlat1.x;
					    u_xlat9 = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat1.x = u_xlat1.x * _LineWidth + u_xlat9;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat13 = (-u_xlat1.x) + 1.0;
					    u_xlat13 = u_xlat5.x * u_xlat13 + u_xlat1.x;
					    u_xlat1.x = (-u_xlat5.x) * u_xlat1.x + u_xlat1.x;
					    u_xlat1.x = max(u_xlat1.x, 0.0);
					    u_xlat13 = min(u_xlat13, 1.0);
					    u_xlat13 = (-u_xlat1.x) + u_xlat13;
					    u_xlat1.x = u_xlat0.w + (-u_xlat1.x);
					    u_xlat1.x = u_xlat1.x / u_xlat13;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat13 = vs_TEXCOORD1.y;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.x * u_xlat13 + -0.00999999978;
					    u_xlat1.x = u_xlat13 * u_xlat1.x;
					    SV_Target0.w = u_xlat1.x;
					    u_xlatb1 = u_xlat2.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat8 = u_xlat8 * u_xlat5.x;
					    u_xlat8 = u_xlat8 * _LineWidth;
					    u_xlat8 = u_xlat8 * 0.5 + u_xlat9;
					    u_xlat8 = clamp(u_xlat8, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat8) + 1.0;
					    u_xlat1.x = u_xlat5.x * u_xlat1.x + u_xlat8;
					    u_xlat8 = (-u_xlat5.x) * u_xlat8 + u_xlat8;
					    u_xlat8 = max(u_xlat8, 0.0);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat1.x = (-u_xlat8) + u_xlat1.x;
					    u_xlat8 = (-u_xlat8) + u_xlat0.w;
					    u_xlat0.xy = u_xlat0.yx * vec2(0.600000024, 0.400000006);
					    u_xlat8 = u_xlat8 / u_xlat1.x;
					    u_xlat8 = clamp(u_xlat8, 0.0, 1.0);
					    u_xlat8 = (-u_xlat8) + 1.0;
					    u_xlat1.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xyw = u_xlat0.yyy * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * vs_COLOR0.xyz;
					    u_xlat1.xyz = u_xlat0.xyw * _Color.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.xyw = (-u_xlat0.xyw) * _Color.xyz + u_xlat2.xyz;
					    u_xlat0.xyz = vec3(u_xlat8) * u_xlat0.xyw + u_xlat1.xyz;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_0[62];
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_3[2];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat4;
					float u_xlat12;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD1.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat4 = u_xlat1.w * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat1.w;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat4<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xy = u_xlat1.yx * vec2(0.600000024, 0.400000006);
					    u_xlat12 = (-u_xlat1.w) + 1.0;
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat0.xyz + vs_COLOR1.xyz;
					    u_xlat1.xzw = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xyz = u_xlat1.yyy * u_xlat1.xzw + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + u_xlat2.xyz;
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_3[2];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat8;
					bool u_xlatb8;
					float u_xlat12;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat8 = u_xlat0.w + -0.5;
					    u_xlatb8 = u_xlat8<0.0;
					    if(((int(u_xlatb8) * int(0xffffffffu)))!=0){discard;}
					    u_xlat8 = vs_TEXCOORD1.y;
					    u_xlat8 = clamp(u_xlat8, 0.0, 1.0);
					    u_xlat1.x = u_xlat0.w * u_xlat8 + -0.00999999978;
					    u_xlat8 = u_xlat8 * u_xlat0.w;
					    SV_Target0.w = u_xlat8;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb8 = u_xlat1.x<0.0;
					    if(((int(u_xlatb8) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xy = u_xlat0.yx * vec2(0.600000024, 0.400000006);
					    u_xlat8 = (-u_xlat0.w) + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xyw = u_xlat0.yyy * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * vs_COLOR0.xyz;
					    u_xlat1.xyz = u_xlat0.xyw * _Color.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.xyw = (-u_xlat0.xyw) * _Color.xyz + u_xlat2.xyz;
					    u_xlat0.xyz = vec3(u_xlat8) * u_xlat0.xyw + u_xlat1.xyz;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_2[8];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[3];
						float _Hover;
						vec4 unused_0_8;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_10;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec2 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec2 u_xlat4;
					void main()
					{
					    u_xlat0.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat0.xy = vs_TEXCOORD0.xy / u_xlat0.xy;
					    u_xlat4.xy = dFdx(u_xlat0.xy);
					    u_xlat0.xy = dFdy(u_xlat0.xy);
					    u_xlat0.xy = abs(u_xlat0.xy) + abs(u_xlat4.xy);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat2.x = u_xlat2.x * u_xlat0.x;
					    u_xlat2.x = u_xlat2.x * _LineWidth;
					    u_xlat4.x = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat2.x = u_xlat2.x * -0.5 + u_xlat4.x;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4.x = (-u_xlat2.x) + 1.0;
					    u_xlat4.x = u_xlat0.x * u_xlat4.x + u_xlat2.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat2.x + u_xlat2.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat2.x = min(u_xlat4.x, 1.0);
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = (-u_xlat0.x) + u_xlat1.w;
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = vs_TEXCOORD1.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4.x = u_xlat0.x * u_xlat2.x + -0.00999999978;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat4.x<0.0;
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
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_6[3];
						float _Hover;
						vec4 unused_0_8;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_10;
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
					    u_xlat0.x = u_xlat0.w + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat0.x * -0.5;
					    u_xlat2.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat2.xy = vs_TEXCOORD0.xy / u_xlat2.xy;
					    u_xlat1.xy = dFdx(u_xlat2.xy);
					    u_xlat2.xy = dFdy(u_xlat2.xy);
					    u_xlat2.xy = abs(u_xlat2.xy) + abs(u_xlat1.xy);
					    u_xlat2.x = max(u_xlat2.y, u_xlat2.x);
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    u_xlat4 = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat0.x = u_xlat0.x * _LineWidth + u_xlat4;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat4 = (-u_xlat0.x) + 1.0;
					    u_xlat4 = u_xlat2.x * u_xlat4 + u_xlat0.x;
					    u_xlat0.x = (-u_xlat2.x) * u_xlat0.x + u_xlat0.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat2.x = min(u_xlat4, 1.0);
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat0.w;
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = vs_TEXCOORD1.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = u_xlat0.x * u_xlat2.x + -0.00999999978;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
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
					Keywords { "_MIRROR_ON" "_LOWEND_ON" }
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
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_4[3];
						float _Hover;
						vec4 unused_0_6;
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					vec2 u_xlat4;
					vec2 u_xlat8;
					float u_xlat12;
					void main()
					{
					    u_xlat0.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat0.xy = vs_TEXCOORD0.xy / u_xlat0.xy;
					    u_xlat8.xy = dFdx(u_xlat0.xy);
					    u_xlat0.xy = dFdy(u_xlat0.xy);
					    u_xlat0.xy = abs(u_xlat0.xy) + abs(u_xlat8.xy);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat4.x = u_xlat4.x * u_xlat0.x;
					    u_xlat4.x = u_xlat4.x * _LineWidth;
					    u_xlat8.x = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat4.xy = u_xlat4.xx * vec2(-0.5, 0.5) + u_xlat8.xx;
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat4.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat8.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat8.xy = (-u_xlat0.xy) + u_xlat8.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat1.xy = u_xlat1.yx * vec2(0.600000024, 0.400000006);
					    u_xlat0.xy = u_xlat0.xy / u_xlat8.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat8.x = vs_TEXCOORD1.y;
					    u_xlat8.x = clamp(u_xlat8.x, 0.0, 1.0);
					    u_xlat12 = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    u_xlat4.x = (-u_xlat0.y) + 1.0;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat12<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xzw = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xzw = u_xlat1.xxx * u_xlat0.xzw + vs_COLOR1.xyz;
					    u_xlat1.xzw = (-u_xlat0.xzw) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xzw = u_xlat1.yyy * u_xlat1.xzw + u_xlat0.xzw;
					    u_xlat0.xzw = u_xlat0.xzw * vs_COLOR0.xyz;
					    u_xlat1.xyz = u_xlat0.xzw * _Color.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.xzw = (-u_xlat0.xzw) * _Color.xyz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat4.xxx * u_xlat0.xzw + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_4[3];
						float _Hover;
						vec4 unused_0_6;
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					bool u_xlatb1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					vec2 u_xlat5;
					float u_xlat8;
					bool u_xlatb8;
					float u_xlat9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat8 = u_xlat0.w + -0.5;
					    u_xlatb8 = u_xlat8<0.0;
					    if(((int(u_xlatb8) * int(0xffffffffu)))!=0){discard;}
					    u_xlat8 = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat8 * -0.5;
					    u_xlat5.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat5.xy = vs_TEXCOORD0.xy / u_xlat5.xy;
					    u_xlat2.xy = dFdx(u_xlat5.xy);
					    u_xlat5.xy = dFdy(u_xlat5.xy);
					    u_xlat5.xy = abs(u_xlat5.xy) + abs(u_xlat2.xy);
					    u_xlat5.x = max(u_xlat5.y, u_xlat5.x);
					    u_xlat1.x = u_xlat5.x * u_xlat1.x;
					    u_xlat9 = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat1.x = u_xlat1.x * _LineWidth + u_xlat9;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat13 = (-u_xlat1.x) + 1.0;
					    u_xlat13 = u_xlat5.x * u_xlat13 + u_xlat1.x;
					    u_xlat1.x = (-u_xlat5.x) * u_xlat1.x + u_xlat1.x;
					    u_xlat1.x = max(u_xlat1.x, 0.0);
					    u_xlat13 = min(u_xlat13, 1.0);
					    u_xlat13 = (-u_xlat1.x) + u_xlat13;
					    u_xlat1.x = u_xlat0.w + (-u_xlat1.x);
					    u_xlat1.x = u_xlat1.x / u_xlat13;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat13 = vs_TEXCOORD1.y;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat2.x = u_xlat1.x * u_xlat13 + -0.00999999978;
					    u_xlat1.x = u_xlat13 * u_xlat1.x;
					    SV_Target0.w = u_xlat1.x;
					    u_xlatb1 = u_xlat2.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat8 = u_xlat8 * u_xlat5.x;
					    u_xlat8 = u_xlat8 * _LineWidth;
					    u_xlat8 = u_xlat8 * 0.5 + u_xlat9;
					    u_xlat8 = clamp(u_xlat8, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat8) + 1.0;
					    u_xlat1.x = u_xlat5.x * u_xlat1.x + u_xlat8;
					    u_xlat8 = (-u_xlat5.x) * u_xlat8 + u_xlat8;
					    u_xlat8 = max(u_xlat8, 0.0);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat1.x = (-u_xlat8) + u_xlat1.x;
					    u_xlat8 = (-u_xlat8) + u_xlat0.w;
					    u_xlat0.xy = u_xlat0.yx * vec2(0.600000024, 0.400000006);
					    u_xlat8 = u_xlat8 / u_xlat1.x;
					    u_xlat8 = clamp(u_xlat8, 0.0, 1.0);
					    u_xlat8 = (-u_xlat8) + 1.0;
					    u_xlat1.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xyw = u_xlat0.yyy * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * vs_COLOR0.xyz;
					    u_xlat1.xyz = u_xlat0.xyw * _Color.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.xyw = (-u_xlat0.xyw) * _Color.xyz + u_xlat2.xyz;
					    u_xlat0.xyz = vec3(u_xlat8) * u_xlat0.xyw + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 unused_0_2[2];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat4;
					float u_xlat12;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD1.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat4 = u_xlat1.w * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat1.w;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat4<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xy = u_xlat1.yx * vec2(0.600000024, 0.400000006);
					    u_xlat12 = (-u_xlat1.w) + 1.0;
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat0.xyz + vs_COLOR1.xyz;
					    u_xlat1.xzw = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xyz = u_xlat1.yyy * u_xlat1.xzw + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + u_xlat2.xyz;
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_0[63];
						float _Hover;
						vec4 unused_0_2[2];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat8;
					bool u_xlatb8;
					float u_xlat12;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat8 = u_xlat0.w + -0.5;
					    u_xlatb8 = u_xlat8<0.0;
					    if(((int(u_xlatb8) * int(0xffffffffu)))!=0){discard;}
					    u_xlat8 = vs_TEXCOORD1.y;
					    u_xlat8 = clamp(u_xlat8, 0.0, 1.0);
					    u_xlat1.x = u_xlat0.w * u_xlat8 + -0.00999999978;
					    u_xlat8 = u_xlat8 * u_xlat0.w;
					    SV_Target0.w = u_xlat8;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb8 = u_xlat1.x<0.0;
					    if(((int(u_xlatb8) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xy = u_xlat0.yx * vec2(0.600000024, 0.400000006);
					    u_xlat8 = (-u_xlat0.w) + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xyw = u_xlat0.yyy * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * vs_COLOR0.xyz;
					    u_xlat1.xyz = u_xlat0.xyw * _Color.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat3.xyz = (-u_xlat1.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.xyw = (-u_xlat0.xyw) * _Color.xyz + u_xlat2.xyz;
					    u_xlat0.xyz = vec3(u_xlat8) * u_xlat0.xyw + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_2[8];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[3];
						float _Hover;
						vec4 unused_0_8;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_10;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec2 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec2 u_xlat4;
					void main()
					{
					    u_xlat0.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat0.xy = vs_TEXCOORD0.xy / u_xlat0.xy;
					    u_xlat4.xy = dFdx(u_xlat0.xy);
					    u_xlat0.xy = dFdy(u_xlat0.xy);
					    u_xlat0.xy = abs(u_xlat0.xy) + abs(u_xlat4.xy);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat2.x = _AAFactor + 1.0;
					    u_xlat2.x = u_xlat2.x * u_xlat0.x;
					    u_xlat2.x = u_xlat2.x * _LineWidth;
					    u_xlat4.x = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat2.x = u_xlat2.x * -0.5 + u_xlat4.x;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4.x = (-u_xlat2.x) + 1.0;
					    u_xlat4.x = u_xlat0.x * u_xlat4.x + u_xlat2.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat2.x + u_xlat2.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat2.x = min(u_xlat4.x, 1.0);
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = (-u_xlat0.x) + u_xlat1.w;
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = vs_TEXCOORD1.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4.x = u_xlat0.x * u_xlat2.x + -0.00999999978;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat4.x<0.0;
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
					Keywords { "_MIRROR_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_6[3];
						float _Hover;
						vec4 unused_0_8;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_10;
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
					    u_xlat0.x = u_xlat0.w + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat0.x * -0.5;
					    u_xlat2.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat2.xy = vs_TEXCOORD0.xy / u_xlat2.xy;
					    u_xlat1.xy = dFdx(u_xlat2.xy);
					    u_xlat2.xy = dFdy(u_xlat2.xy);
					    u_xlat2.xy = abs(u_xlat2.xy) + abs(u_xlat1.xy);
					    u_xlat2.x = max(u_xlat2.y, u_xlat2.x);
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    u_xlat4 = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat0.x = u_xlat0.x * _LineWidth + u_xlat4;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat4 = (-u_xlat0.x) + 1.0;
					    u_xlat4 = u_xlat2.x * u_xlat4 + u_xlat0.x;
					    u_xlat0.x = (-u_xlat2.x) * u_xlat0.x + u_xlat0.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat2.x = min(u_xlat4, 1.0);
					    u_xlat2.x = (-u_xlat0.x) + u_xlat2.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat0.w;
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = vs_TEXCOORD1.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat4 = u_xlat0.x * u_xlat2.x + -0.00999999978;
					    u_xlat0.x = u_xlat2.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
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
					Keywords { "_MIRROR_ON" "_CINEMATIC_ON" "_LOWEND_ON" }
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
					Keywords { "_MIRROR_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_4[5];
						float _LineWidth;
						vec4 unused_0_6;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_10;
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_13;
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec2 u_xlat5;
					vec2 u_xlat10;
					float u_xlat11;
					bool u_xlatb11;
					float u_xlat16;
					void main()
					{
					    u_xlat0.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat0.xy = vs_TEXCOORD0.xy / u_xlat0.xy;
					    u_xlat10.xy = dFdx(u_xlat0.xy);
					    u_xlat0.xy = dFdy(u_xlat0.xy);
					    u_xlat0.xy = abs(u_xlat0.xy) + abs(u_xlat10.xy);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat5.x = _AAFactor + 1.0;
					    u_xlat5.x = u_xlat5.x * u_xlat0.x;
					    u_xlat5.x = u_xlat5.x * _LineWidth;
					    u_xlat10.x = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat5.xy = u_xlat5.xx * vec2(-0.5, 0.5) + u_xlat10.xx;
					    u_xlat5.xy = clamp(u_xlat5.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat5.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat5.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat5.xy + u_xlat5.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat10.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat10.xy = (-u_xlat0.xy) + u_xlat10.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat1.xy = u_xlat1.yx * vec2(0.600000024, 0.400000006);
					    u_xlat0.xy = u_xlat0.xy / u_xlat10.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat10.xy = vs_TEXCOORD1.yx;
					    u_xlat10.xy = clamp(u_xlat10.xy, 0.0, 1.0);
					    u_xlat11 = u_xlat0.x * u_xlat10.x + -0.00999999978;
					    u_xlatb11 = u_xlat11<0.0;
					    if(((int(u_xlatb11) * int(0xffffffffu)))!=0){discard;}
					    u_xlat2.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xzw = u_xlat1.xxx * u_xlat2.xyz + vs_COLOR1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xzw) + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = u_xlat1.yyy * u_xlat2.xyz + u_xlat1.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * _Color.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat4.xyz = (-u_xlat2.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat3.xyz = vec3(_Hover) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat1.xyz) * _Color.xyz + u_xlat3.xyz;
					    u_xlat5.x = (-u_xlat0.y) + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlat0.xyz = u_xlat5.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * u_xlat1.xxx + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat16 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat16) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat10.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_4[5];
						float _LineWidth;
						vec4 unused_0_6;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_10;
						vec4 _SelectionColor;
						float _Hover;
						vec4 unused_0_13;
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec3 u_xlat5;
					float u_xlat6;
					float u_xlat7;
					vec2 u_xlat12;
					bool u_xlatb12;
					vec2 u_xlat13;
					bool u_xlatb13;
					float u_xlat18;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat12.x = u_xlat0.w + -0.5;
					    u_xlatb12 = u_xlat12.x<0.0;
					    if(((int(u_xlatb12) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat1.xy = vs_TEXCOORD0.xy / u_xlat1.xy;
					    u_xlat13.xy = dFdx(u_xlat1.xy);
					    u_xlat1.xy = dFdy(u_xlat1.xy);
					    u_xlat1.xy = abs(u_xlat1.xy) + abs(u_xlat13.xy);
					    u_xlat12.x = max(u_xlat1.y, u_xlat1.x);
					    u_xlat1.x = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat12.x * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat7 = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat1.xy = u_xlat1.xx * vec2(-0.5, 0.5) + vec2(u_xlat7);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat13.xy = (-u_xlat1.xy) + vec2(1.0, 1.0);
					    u_xlat13.xy = u_xlat12.xx * u_xlat13.xy + u_xlat1.xy;
					    u_xlat1.xy = (-u_xlat12.xx) * u_xlat1.xy + u_xlat1.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat13.xy = min(u_xlat13.xy, vec2(1.0, 1.0));
					    u_xlat13.xy = (-u_xlat1.xy) + u_xlat13.xy;
					    u_xlat12.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat0.xy = u_xlat0.yx * vec2(0.600000024, 0.400000006);
					    u_xlat12.xy = u_xlat12.xy / u_xlat13.xy;
					    u_xlat12.xy = clamp(u_xlat12.xy, 0.0, 1.0);
					    u_xlat1.xy = vs_TEXCOORD1.yx;
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat13.x = u_xlat12.x * u_xlat1.x + -0.00999999978;
					    u_xlatb13 = u_xlat13.x<0.0;
					    if(((int(u_xlatb13) * int(0xffffffffu)))!=0){discard;}
					    u_xlat2.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.xyz + vs_COLOR1.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * vs_COLOR0.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _Color.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat5.xyz = (-u_xlat3.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat4.xyz = vec3(_Hover) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat2.xyz = (-u_xlat2.xyz) * _Color.xyz + u_xlat4.xyz;
					    u_xlat0.x = (-u_xlat12.y) + 1.0;
					    u_xlat6 = u_xlat12.x * u_xlat1.x;
					    SV_Target0.w = u_xlat6;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat18 = u_xlat0.y + 0.5;
					    u_xlat1.xzw = _SelectionColor.xyz * vec3(u_xlat18) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xzw + u_xlat0.xyz;
					    u_xlat0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    u_xlat1.xzw = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat18 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat18 = clamp(u_xlat18, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat18) * u_xlat1.xzw + u_xlat0.xyz;
					    u_xlat18 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat18)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat18);
					    u_xlat1.xzw = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xzw + u_xlat0.xyz;
					    u_xlat1.xzw = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xzw = (-u_xlat0.xyz) + u_xlat1.xzw;
					    SV_Target0.xyz = u_xlat1.yyy * u_xlat1.xzw + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_LOWEND_ON" }
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
						vec4 unused_0_11[2];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec2 u_xlat10;
					bool u_xlatb10;
					float u_xlat16;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD1.yx;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat10.x = u_xlat1.w * u_xlat0.x + -0.00999999978;
					    u_xlatb10 = u_xlat10.x<0.0;
					    if(((int(u_xlatb10) * int(0xffffffffu)))!=0){discard;}
					    u_xlat2.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.xy = u_xlat1.yx * vec2(0.600000024, 0.400000006);
					    u_xlat1.xyz = u_xlat10.xxx * u_xlat2.xyz + vs_COLOR1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * _Color.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat4.xyz = (-u_xlat2.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat3.xyz = vec3(_Hover) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat1.xyz) * _Color.xyz + u_xlat3.xyz;
					    u_xlat10.x = (-u_xlat1.w) + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat1.w;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlat0.xzw = u_xlat10.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.x = u_xlat0.z + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * u_xlat1.xxx + (-u_xlat0.xzw);
					    u_xlat0.xzw = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat0.xzw = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xzw;
					    u_xlat1.xyz = (-u_xlat0.xzw) + vs_COLOR0.xyz;
					    u_xlat16 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat0.xzw = vec3(u_xlat16) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.x = dot(u_xlat0.xzw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xzw = u_xlat0.xzw + (-u_xlat1.xxx);
					    u_xlat0.xzw = _CloudCoverage.yyy * u_xlat0.xzw + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xzw) + _LutLerp.www;
					    u_xlat0.xzw = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xzw) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat0.yyy * u_xlat1.xyz + u_xlat0.xzw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_11[2];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat10;
					bool u_xlatb10;
					float u_xlat11;
					float u_xlat15;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat10 = u_xlat0.w + -0.5;
					    u_xlatb10 = u_xlat10<0.0;
					    if(((int(u_xlatb10) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xy = vs_TEXCOORD1.yx;
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat10 = u_xlat0.w * u_xlat1.x + -0.00999999978;
					    u_xlatb10 = u_xlat10<0.0;
					    if(((int(u_xlatb10) * int(0xffffffffu)))!=0){discard;}
					    u_xlat2.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xy = u_xlat0.yx * vec2(0.600000024, 0.400000006);
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.xyz + vs_COLOR1.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xyz = u_xlat0.yyy * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat2.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat4.xyz = (-u_xlat2.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat3.xyz = vec3(_Hover) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + u_xlat3.xyz;
					    u_xlat11 = (-u_xlat0.w) + 1.0;
					    u_xlat15 = u_xlat0.w * u_xlat1.x;
					    SV_Target0.w = u_xlat15;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat11) * u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat15 = u_xlat0.y + 0.5;
					    u_xlat1.xzw = _SelectionColor.xyz * vec3(u_xlat15) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xzw + u_xlat0.xyz;
					    u_xlat0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    u_xlat1.xzw = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat15 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat15) * u_xlat1.xzw + u_xlat0.xyz;
					    u_xlat15 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat15)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat15);
					    u_xlat1.xzw = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xzw + u_xlat0.xyz;
					    u_xlat1.xzw = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xzw = (-u_xlat0.xyz) + u_xlat1.xzw;
					    SV_Target0.xyz = u_xlat1.yyy * u_xlat1.xzw + u_xlat0.xyz;
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
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12[2];
						float _Hover;
						vec4 unused_0_14;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_16;
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
					vec2 u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat0.xy = vs_TEXCOORD0.xy / u_xlat0.xy;
					    u_xlat6.xy = dFdx(u_xlat0.xy);
					    u_xlat0.xy = dFdy(u_xlat0.xy);
					    u_xlat0.xy = abs(u_xlat0.xy) + abs(u_xlat6.xy);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat6.x = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat3.x = u_xlat3.x * -0.5 + u_xlat6.x;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat6.x = (-u_xlat3.x) + 1.0;
					    u_xlat6.x = u_xlat0.x * u_xlat6.x + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat3.x + u_xlat3.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat6.x, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = (-u_xlat0.x) + u_xlat1.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xy = vs_TEXCOORD1.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
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
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_12[2];
						float _Hover;
						vec4 unused_0_14;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_16;
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
					    u_xlat0.x = u_xlat0.w + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat0.x * -0.5;
					    u_xlat3.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat3.xy = vs_TEXCOORD0.xy / u_xlat3.xy;
					    u_xlat1.xy = dFdx(u_xlat3.xy);
					    u_xlat3.xy = dFdy(u_xlat3.xy);
					    u_xlat3.xy = abs(u_xlat3.xy) + abs(u_xlat1.xy);
					    u_xlat3.x = max(u_xlat3.y, u_xlat3.x);
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat6 = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat0.x = u_xlat0.x * _LineWidth + u_xlat6;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat6 = (-u_xlat0.x) + 1.0;
					    u_xlat6 = u_xlat3.x * u_xlat6 + u_xlat0.x;
					    u_xlat0.x = (-u_xlat3.x) * u_xlat0.x + u_xlat0.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat6, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat0.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xy = vs_TEXCOORD1.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
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
					Keywords { "_MIRROR_ON" "_GAME_ON" "_LOWEND_ON" }
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
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_4[5];
						float _LineWidth;
						vec4 unused_0_6;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_10[2];
						float _Hover;
						vec4 unused_0_12;
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec2 u_xlat5;
					vec2 u_xlat10;
					float u_xlat11;
					bool u_xlatb11;
					float u_xlat16;
					void main()
					{
					    u_xlat0.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat0.xy = vs_TEXCOORD0.xy / u_xlat0.xy;
					    u_xlat10.xy = dFdx(u_xlat0.xy);
					    u_xlat0.xy = dFdy(u_xlat0.xy);
					    u_xlat0.xy = abs(u_xlat0.xy) + abs(u_xlat10.xy);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat5.x = _AAFactor + 1.0;
					    u_xlat5.x = u_xlat5.x * u_xlat0.x;
					    u_xlat5.x = u_xlat5.x * _LineWidth;
					    u_xlat10.x = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat5.xy = u_xlat5.xx * vec2(-0.5, 0.5) + u_xlat10.xx;
					    u_xlat5.xy = clamp(u_xlat5.xy, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat5.xy) + vec2(1.0, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + u_xlat5.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat5.xy + u_xlat5.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat10.xy = min(u_xlat1.xy, vec2(1.0, 1.0));
					    u_xlat10.xy = (-u_xlat0.xy) + u_xlat10.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat1.ww;
					    u_xlat1.xy = u_xlat1.yx * vec2(0.600000024, 0.400000006);
					    u_xlat0.xy = u_xlat0.xy / u_xlat10.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat10.xy = vs_TEXCOORD1.yx;
					    u_xlat10.xy = clamp(u_xlat10.xy, 0.0, 1.0);
					    u_xlat11 = u_xlat0.x * u_xlat10.x + -0.00999999978;
					    u_xlatb11 = u_xlat11<0.0;
					    if(((int(u_xlatb11) * int(0xffffffffu)))!=0){discard;}
					    u_xlat2.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xzw = u_xlat1.xxx * u_xlat2.xyz + vs_COLOR1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xzw) + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = u_xlat1.yyy * u_xlat2.xyz + u_xlat1.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * _Color.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat4.xyz = (-u_xlat2.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat3.xyz = vec3(_Hover) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat1.xyz) * _Color.xyz + u_xlat3.xyz;
					    u_xlat5.x = (-u_xlat0.y) + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlat0.xyz = u_xlat5.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat16 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat16) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat10.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_4[5];
						float _LineWidth;
						vec4 unused_0_6;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_10[2];
						float _Hover;
						vec4 unused_0_12;
						vec4 _MainTex_TexelSize;
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec3 u_xlat5;
					float u_xlat6;
					float u_xlat7;
					vec2 u_xlat12;
					bool u_xlatb12;
					vec2 u_xlat13;
					bool u_xlatb13;
					float u_xlat18;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat12.x = u_xlat0.w + -0.5;
					    u_xlatb12 = u_xlat12.x<0.0;
					    if(((int(u_xlatb12) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat1.xy = vs_TEXCOORD0.xy / u_xlat1.xy;
					    u_xlat13.xy = dFdx(u_xlat1.xy);
					    u_xlat1.xy = dFdy(u_xlat1.xy);
					    u_xlat1.xy = abs(u_xlat1.xy) + abs(u_xlat13.xy);
					    u_xlat12.x = max(u_xlat1.y, u_xlat1.x);
					    u_xlat1.x = _AAFactor + 1.0;
					    u_xlat1.x = u_xlat12.x * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * _LineWidth;
					    u_xlat7 = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat1.xy = u_xlat1.xx * vec2(-0.5, 0.5) + vec2(u_xlat7);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat13.xy = (-u_xlat1.xy) + vec2(1.0, 1.0);
					    u_xlat13.xy = u_xlat12.xx * u_xlat13.xy + u_xlat1.xy;
					    u_xlat1.xy = (-u_xlat12.xx) * u_xlat1.xy + u_xlat1.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat13.xy = min(u_xlat13.xy, vec2(1.0, 1.0));
					    u_xlat13.xy = (-u_xlat1.xy) + u_xlat13.xy;
					    u_xlat12.xy = u_xlat0.ww + (-u_xlat1.xy);
					    u_xlat0.xy = u_xlat0.yx * vec2(0.600000024, 0.400000006);
					    u_xlat12.xy = u_xlat12.xy / u_xlat13.xy;
					    u_xlat12.xy = clamp(u_xlat12.xy, 0.0, 1.0);
					    u_xlat1.xy = vs_TEXCOORD1.yx;
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat13.x = u_xlat12.x * u_xlat1.x + -0.00999999978;
					    u_xlatb13 = u_xlat13.x<0.0;
					    if(((int(u_xlatb13) * int(0xffffffffu)))!=0){discard;}
					    u_xlat2.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.xyz + vs_COLOR1.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * vs_COLOR0.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _Color.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat5.xyz = (-u_xlat3.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat4.xyz = vec3(_Hover) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat2.xyz = (-u_xlat2.xyz) * _Color.xyz + u_xlat4.xyz;
					    u_xlat0.x = (-u_xlat12.y) + 1.0;
					    u_xlat6 = u_xlat12.x * u_xlat1.x;
					    SV_Target0.w = u_xlat6;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat1.xzw = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat18 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat18 = clamp(u_xlat18, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat18) * u_xlat1.xzw + u_xlat0.xyz;
					    u_xlat18 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat18)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat18);
					    u_xlat1.xzw = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xzw + u_xlat0.xyz;
					    u_xlat1.xzw = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xzw = (-u_xlat0.xyz) + u_xlat1.xzw;
					    SV_Target0.xyz = u_xlat1.yyy * u_xlat1.xzw + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_LOWEND_ON" }
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
						vec4 unused_0_8[2];
						float _Hover;
						vec4 unused_0_10[2];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec2 u_xlat10;
					bool u_xlatb10;
					float u_xlat16;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD1.yx;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat10.x = u_xlat1.w * u_xlat0.x + -0.00999999978;
					    u_xlatb10 = u_xlat10.x<0.0;
					    if(((int(u_xlatb10) * int(0xffffffffu)))!=0){discard;}
					    u_xlat2.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat10.xy = u_xlat1.yx * vec2(0.600000024, 0.400000006);
					    u_xlat1.xyz = u_xlat10.xxx * u_xlat2.xyz + vs_COLOR1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = u_xlat10.yyy * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * _Color.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat4.xyz = (-u_xlat2.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat3.xyz = vec3(_Hover) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat1.xyz) * _Color.xyz + u_xlat3.xyz;
					    u_xlat10.x = (-u_xlat1.w) + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat1.w;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlat0.xzw = u_xlat10.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat0.xzw) + vs_COLOR0.xyz;
					    u_xlat16 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat0.xzw = vec3(u_xlat16) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.x = dot(u_xlat0.xzw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xzw = u_xlat0.xzw + (-u_xlat1.xxx);
					    u_xlat0.xzw = _CloudCoverage.yyy * u_xlat0.xzw + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xzw) + _LutLerp.www;
					    u_xlat0.xzw = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xzw) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat0.yyy * u_xlat1.xyz + u_xlat0.xzw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_8[2];
						float _Hover;
						vec4 unused_0_10[2];
						vec4 _Color;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat10;
					bool u_xlatb10;
					float u_xlat11;
					float u_xlat15;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat10 = u_xlat0.w + -0.5;
					    u_xlatb10 = u_xlat10<0.0;
					    if(((int(u_xlatb10) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xy = vs_TEXCOORD1.yx;
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat10 = u_xlat0.w * u_xlat1.x + -0.00999999978;
					    u_xlatb10 = u_xlat10<0.0;
					    if(((int(u_xlatb10) * int(0xffffffffu)))!=0){discard;}
					    u_xlat2.xyz = (-vs_COLOR1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xy = u_xlat0.yx * vec2(0.600000024, 0.400000006);
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.xyz + vs_COLOR1.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat0.xyz = u_xlat0.yyy * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat2.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					    u_xlat4.xyz = (-u_xlat2.xyz) * vec3(0.300000012, 0.300000012, 0.300000012) + vec3(1.0, 1.0, 1.0);
					    u_xlat3.xyz = vec3(_Hover) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat0.xyz = (-u_xlat0.xyz) * _Color.xyz + u_xlat3.xyz;
					    u_xlat11 = (-u_xlat0.w) + 1.0;
					    u_xlat15 = u_xlat0.w * u_xlat1.x;
					    SV_Target0.w = u_xlat15;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat11) * u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat1.xzw = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat15 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat15) * u_xlat1.xzw + u_xlat0.xyz;
					    u_xlat15 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat15)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat15);
					    u_xlat1.xzw = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xzw + u_xlat0.xyz;
					    u_xlat1.xzw = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xzw = (-u_xlat0.xyz) + u_xlat1.xzw;
					    SV_Target0.xyz = u_xlat1.yyy * u_xlat1.xzw + u_xlat0.xyz;
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
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12[2];
						float _Hover;
						vec4 unused_0_14;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_16;
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
					vec2 u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat0.xy = vs_TEXCOORD0.xy / u_xlat0.xy;
					    u_xlat6.xy = dFdx(u_xlat0.xy);
					    u_xlat0.xy = dFdy(u_xlat0.xy);
					    u_xlat0.xy = abs(u_xlat0.xy) + abs(u_xlat6.xy);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat3.x = _AAFactor + 1.0;
					    u_xlat3.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = u_xlat3.x * _LineWidth;
					    u_xlat6.x = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat3.x = u_xlat3.x * -0.5 + u_xlat6.x;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat6.x = (-u_xlat3.x) + 1.0;
					    u_xlat6.x = u_xlat0.x * u_xlat6.x + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat3.x + u_xlat3.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat6.x, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = (-u_xlat0.x) + u_xlat1.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xy = vs_TEXCOORD1.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
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
					Keywords { "_MIRROR_ON" "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_12[2];
						float _Hover;
						vec4 unused_0_14;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_16;
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
					    u_xlat0.x = u_xlat0.w + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat0.x * -0.5;
					    u_xlat3.xy = _MainTex_TexelSize.xy * vec2(10.0, 10.0);
					    u_xlat3.xy = vs_TEXCOORD0.xy / u_xlat3.xy;
					    u_xlat1.xy = dFdx(u_xlat3.xy);
					    u_xlat3.xy = dFdy(u_xlat3.xy);
					    u_xlat3.xy = abs(u_xlat3.xy) + abs(u_xlat1.xy);
					    u_xlat3.x = max(u_xlat3.y, u_xlat3.x);
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat6 = (-_Hover) * 0.300000012 + 0.5;
					    u_xlat0.x = u_xlat0.x * _LineWidth + u_xlat6;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat6 = (-u_xlat0.x) + 1.0;
					    u_xlat6 = u_xlat3.x * u_xlat6 + u_xlat0.x;
					    u_xlat0.x = (-u_xlat3.x) * u_xlat0.x + u_xlat0.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat6, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat0.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xy = vs_TEXCOORD1.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
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
					Keywords { "_MIRROR_ON" "_GAME_ON" "_CINEMATIC_ON" "_LOWEND_ON" }
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
					Keywords { "_MIRROR_ON" "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
			}
		}
	}
}