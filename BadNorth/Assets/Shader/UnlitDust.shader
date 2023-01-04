Shader "Unlit/Dust" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		_Multiplier ("Multiplier", Float) = 1
		[KeywordEnum(None, Up, Cliff, Tree)] _COLOR ("Color Mode", Float) = 0
	}
	SubShader {
		LOD 100
		Tags { "RenderType" = "Transparent" }
		Pass {
			LOD 100
			Tags { "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			GpuProgramID 16166
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
						vec4 unused_0_2;
						float _Multiplier;
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
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out float vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1 = 0.0;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat0.xyz = u_xlat0.xxx * in_COLOR0.xyz;
					    u_xlat0.w = in_COLOR0.w;
					    vs_COLOR0 = u_xlat0 * vec4(_Multiplier);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_COLOR_UP" }
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[37];
						float _Year;
						vec4 unused_0_4[7];
						vec4 _SnowColor;
						vec4 unused_0_6;
						float _SnowAmount;
						vec4 unused_0_8[12];
						vec4 _MainTex_ST;
						vec4 unused_0_10;
						float _Multiplier;
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
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out float vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1 = 0.0;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = in_POSITION0.y * unity_ObjectToWorld[1].y;
					    u_xlat0.x = unity_ObjectToWorld[0].y * in_POSITION0.x + u_xlat0.x;
					    u_xlat0.x = unity_ObjectToWorld[2].y * in_POSITION0.z + u_xlat0.x;
					    u_xlat0.x = unity_ObjectToWorld[3].y * in_POSITION0.w + u_xlat0.x;
					    u_xlat3.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat3.x) * 0.5 + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat3.x = u_xlat0.x;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat6 = max(u_xlat0.x, u_xlat3.x);
					    u_xlat6 = (-u_xlat6) + 1.0;
					    u_xlat1.x = _Year;
					    u_xlat1.y = 0.0;
					    u_xlat1 = textureLod(_GrassTex, u_xlat1.xy, 0.0);
					    u_xlat2.xyz = u_xlat3.xxx * u_xlat1.yzx;
					    u_xlat3.xyz = u_xlat1.xyz * vec3(u_xlat6) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.zxy + u_xlat3.xyz;
					    u_xlat0 = (-u_xlat1) + _SnowColor;
					    u_xlat0 = vec4(_SnowAmount) * u_xlat0 + u_xlat1;
					    u_xlat1.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat1.xyz = u_xlat1.xxx * in_COLOR0.xyz;
					    u_xlat1.w = in_COLOR0.w;
					    u_xlat0 = u_xlat0 * u_xlat1;
					    vs_COLOR0 = u_xlat0 * vec4(_Multiplier);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_COLOR_CLIFF" }
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
						vec4 unused_0_0[5];
						vec4 _CliffParams;
						vec4 unused_0_2[58];
						vec4 _MainTex_ST;
						vec4 unused_0_4;
						float _Multiplier;
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
					uniform  sampler2D _CliffTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out float vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1 = 0.0;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = in_POSITION0.y * unity_ObjectToWorld[1].y;
					    u_xlat0.x = unity_ObjectToWorld[0].y * in_POSITION0.x + u_xlat0.x;
					    u_xlat0.x = unity_ObjectToWorld[2].y * in_POSITION0.z + u_xlat0.x;
					    u_xlat0.x = unity_ObjectToWorld[3].y * in_POSITION0.w + u_xlat0.x;
					    u_xlat0.y = u_xlat0.x * 0.100000001;
					    u_xlat0.x = 0.0;
					    u_xlat0 = textureLod(_CliffTex, u_xlat0.xy, 0.0);
					    u_xlat2 = (-_CliffParams.x) + _CliffParams.y;
					    u_xlat0.xyz = u_xlat0.xxx * vec3(u_xlat2) + _CliffParams.xxx;
					    u_xlat1.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat1.xyz = u_xlat1.xxx * in_COLOR0.xyz;
					    u_xlat1.w = in_COLOR0.w;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1;
					    vs_COLOR0 = u_xlat0 * vec4(_Multiplier);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_COLOR_TREE" }
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[37];
						float _Year;
						vec4 unused_0_4[22];
						vec4 _MainTex_ST;
						vec4 unused_0_6;
						float _Multiplier;
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
					uniform  sampler2D _TreeTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out float vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1 = 0.0;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = in_POSITION0.y * unity_ObjectToWorld[1].y;
					    u_xlat0.x = unity_ObjectToWorld[0].y * in_POSITION0.x + u_xlat0.x;
					    u_xlat0.x = unity_ObjectToWorld[2].y * in_POSITION0.z + u_xlat0.x;
					    u_xlat0.x = unity_ObjectToWorld[3].y * in_POSITION0.w + u_xlat0.x;
					    u_xlat3.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat3.x) * 0.5 + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat3.x = u_xlat0.x;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat6 = max(u_xlat0.x, u_xlat3.x);
					    u_xlat6 = (-u_xlat6) + 1.0;
					    u_xlat1.x = _Year + 0.0500000007;
					    u_xlat1.y = 0.0;
					    u_xlat1 = textureLod(_TreeTex, u_xlat1.xy, 0.0);
					    u_xlat2.xyz = u_xlat3.xxx * u_xlat1.yzx;
					    u_xlat3.xyz = u_xlat1.xyz * vec3(u_xlat6) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.zxy + u_xlat3.xyz;
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat0.xyz = u_xlat0.xxx * in_COLOR0.xyz;
					    u_xlat0.w = in_COLOR0.w;
					    u_xlat0 = u_xlat1 * u_xlat0;
					    vs_COLOR0 = u_xlat0 * vec4(_Multiplier);
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
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_21[2];
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_24[2];
						vec4 _MainTex_ST;
						vec4 unused_0_26;
						float _Multiplier;
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out float vs_TEXCOORD1;
					out vec4 vs_COLOR0;
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
					vec3 u_xlat11;
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
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[3].xzy * in_POSITION0.www + u_xlat0.xzy;
					    u_xlat1.x = dot(u_xlat0.xy, u_xlat0.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat11.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1 = u_xlat1.x / u_xlat11.x;
					    vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat1 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat1;
					    u_xlat1 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat1;
					    u_xlat1 = u_xlat1 + unity_ObjectToWorld[3];
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = unity_MatrixVP[3] * u_xlat1.wwww + u_xlat2;
					    u_xlat1.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat1.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xwy + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat31 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat2.y * u_xlat31;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat32;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat32;
					    u_xlat6 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat12.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat8.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat8.yyyy;
					    u_xlat6 = u_xlat8.zzzz * u_xlat6;
					    u_xlat7 = u_xlat7 * u_xlat8.xxxx;
					    u_xlat7 = u_xlat8.yyyy * u_xlat7;
					    u_xlat6 = u_xlat7 * u_xlat8.zzzz + u_xlat6;
					    u_xlat31 = u_xlat31 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat31;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat31;
					    u_xlat12.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat5 = u_xlat1.xxxx * u_xlat5;
					    u_xlat5 = u_xlat1.yyyy * u_xlat5;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat8.zzzz + u_xlat6;
					    u_xlat3 = u_xlat5 * u_xlat8.zzzz + u_xlat3;
					    u_xlat5 = textureLod(_NormalTex, u_xlat12.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat12.zx, 0.0);
					    u_xlat4.w = u_xlat12.x;
					    u_xlat2 = u_xlat1.xxxx * u_xlat6;
					    u_xlat2 = u_xlat8.yyyy * u_xlat2;
					    u_xlat5 = u_xlat8.xxxx * u_xlat5;
					    u_xlat5 = u_xlat8.yyyy * u_xlat5;
					    u_xlat3 = u_xlat5 * u_xlat1.zzzz + u_xlat3;
					    u_xlat2 = u_xlat2 * u_xlat1.zzzz + u_xlat3;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat4.xw, 0.0);
					    u_xlat4 = u_xlat1.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat2 = u_xlat3 * u_xlat1.zzzz + u_xlat2;
					    u_xlat1 = u_xlat4 * u_xlat1.zzzz + u_xlat2;
					    u_xlat31 = u_xlat1.w * 0.400000006;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = (-u_xlat1.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat1.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat1.z) + unity_MatrixV[2].z;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat30 = (-u_xlat2.w) * 0.5 + u_xlat0.w;
					    u_xlat30 = u_xlat30 * 0.25;
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
					    u_xlat0.x = u_xlat0.x * 0.600000024 + u_xlat31;
					    u_xlat10.x = u_xlat30;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat20 = max(u_xlat30, u_xlat10.x);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat10.xxx * u_xlat3.yzx;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(u_xlat20) + u_xlat4.xyz;
					    u_xlat10.xyz = vec3(u_xlat30) * u_xlat3.zxy + u_xlat4.xyz;
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
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * in_COLOR0;
					    vs_COLOR0 = u_xlat0 * vec4(_Multiplier);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_COLOR_UP" }
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
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_21[2];
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_24[2];
						vec4 _MainTex_ST;
						vec4 unused_0_26;
						float _Multiplier;
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out float vs_TEXCOORD1;
					out vec4 vs_COLOR0;
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
					vec3 u_xlat11;
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
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[3].xzy * in_POSITION0.www + u_xlat0.xzy;
					    u_xlat1.x = dot(u_xlat0.xy, u_xlat0.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat11.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1 = u_xlat1.x / u_xlat11.x;
					    vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat1 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat1;
					    u_xlat1 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat1;
					    u_xlat1 = u_xlat1 + unity_ObjectToWorld[3];
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = unity_MatrixVP[3] * u_xlat1.wwww + u_xlat2;
					    u_xlat1.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat1.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xwy + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat31 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat2.y * u_xlat31;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat32;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat32;
					    u_xlat6 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat12.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat8.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat8.yyyy;
					    u_xlat6 = u_xlat8.zzzz * u_xlat6;
					    u_xlat7 = u_xlat7 * u_xlat8.xxxx;
					    u_xlat7 = u_xlat8.yyyy * u_xlat7;
					    u_xlat6 = u_xlat7 * u_xlat8.zzzz + u_xlat6;
					    u_xlat31 = u_xlat31 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat31;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat31;
					    u_xlat12.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat5 = u_xlat1.xxxx * u_xlat5;
					    u_xlat5 = u_xlat1.yyyy * u_xlat5;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat8.zzzz + u_xlat6;
					    u_xlat3 = u_xlat5 * u_xlat8.zzzz + u_xlat3;
					    u_xlat5 = textureLod(_NormalTex, u_xlat12.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat12.zx, 0.0);
					    u_xlat4.w = u_xlat12.x;
					    u_xlat2 = u_xlat1.xxxx * u_xlat6;
					    u_xlat2 = u_xlat8.yyyy * u_xlat2;
					    u_xlat5 = u_xlat8.xxxx * u_xlat5;
					    u_xlat5 = u_xlat8.yyyy * u_xlat5;
					    u_xlat3 = u_xlat5 * u_xlat1.zzzz + u_xlat3;
					    u_xlat2 = u_xlat2 * u_xlat1.zzzz + u_xlat3;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat4.xw, 0.0);
					    u_xlat4 = u_xlat1.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat2 = u_xlat3 * u_xlat1.zzzz + u_xlat2;
					    u_xlat1 = u_xlat4 * u_xlat1.zzzz + u_xlat2;
					    u_xlat31 = u_xlat1.w * 0.400000006;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = (-u_xlat1.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat1.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat1.z) + unity_MatrixV[2].z;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat30 = (-u_xlat2.w) * 0.5 + u_xlat0.w;
					    u_xlat30 = u_xlat30 * 0.25;
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
					    u_xlat0.x = u_xlat0.x * 0.600000024 + u_xlat31;
					    u_xlat10.x = u_xlat30;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat20 = max(u_xlat30, u_xlat10.x);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat10.xxx * u_xlat3.yzx;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(u_xlat20) + u_xlat4.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat3.zxy + u_xlat4.xyz;
					    u_xlat10.xyz = (-u_xlat3.xyz) + _SnowColor.xyz;
					    u_xlat10.xyz = vec3(_SnowAmount) * u_xlat10.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = u_xlat10.xyz * _MinAmbientColor.xyz;
					    u_xlat10.xyz = (-_MinAmbientColor.xyz) * u_xlat10.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat10.xyz + u_xlat4.xyz;
					    u_xlat30 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat30 = inversesqrt(u_xlat30);
					    u_xlat1.xyz = vec3(u_xlat30) * u_xlat1.xyz;
					    u_xlat1.w = (-u_xlat1.x);
					    u_xlat4.xyz = u_xlat1.xyw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat30 = dot(u_xlat1.xyz, _FlashDir.xyz);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30) + 1.0;
					    u_xlat1.xy = u_xlat12.xy * u_xlat4.xy;
					    u_xlat1.x = u_xlat1.y + u_xlat1.x;
					    u_xlat1.x = u_xlat12.z * u_xlat4.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat2.x;
					    u_xlat11.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat11.xyz * u_xlat1.xxx + u_xlat0.xyz;
					    u_xlat1.x = u_xlat30 * u_xlat30;
					    u_xlat30 = (-u_xlat30) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = vec3(u_xlat30) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * in_COLOR0;
					    u_xlat1 = (-u_xlat3) + _SnowColor;
					    u_xlat1 = vec4(_SnowAmount) * u_xlat1 + u_xlat3;
					    u_xlat0 = u_xlat0 * u_xlat1;
					    vs_COLOR0 = u_xlat0 * vec4(_Multiplier);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_COLOR_CLIFF" }
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
						vec4 unused_0_3;
						vec4 _CliffParams;
						vec2 _NormalTexSize;
						vec3 _NormalTexVolume;
						vec4 unused_0_7[25];
						vec3 _SunDir;
						vec4 unused_0_9[4];
						vec4 _SideSunColor;
						vec4 unused_0_11[2];
						float _Year;
						vec4 unused_0_13[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_17[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_22[2];
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_25[2];
						vec4 _MainTex_ST;
						vec4 unused_0_27;
						float _Multiplier;
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _CliffTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out float vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
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
					vec3 u_xlat21;
					vec2 u_xlat25;
					float u_xlat33;
					float u_xlat34;
					float u_xlat35;
					float u_xlat37;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat11.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat11.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat11.xyz;
					    u_xlat11.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat11.xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[3].xzy * in_POSITION0.www + u_xlat11.xzy;
					    u_xlat11.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat11.x = sqrt(u_xlat11.x);
					    u_xlat11.x = u_xlat11.x + (-_FogMinRad);
					    vs_TEXCOORD1 = u_xlat11.x / u_xlat0.x;
					    vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat2 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat2;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat2;
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
					    u_xlat33 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat35 = u_xlat2.y * u_xlat33;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat35;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat35;
					    u_xlat6 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat13.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat6 = u_xlat0.xxxx * u_xlat6;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat8.yyyy;
					    u_xlat6 = u_xlat8.zzzz * u_xlat6;
					    u_xlat7 = u_xlat7 * u_xlat8.xxxx;
					    u_xlat7 = u_xlat8.yyyy * u_xlat7;
					    u_xlat6 = u_xlat7 * u_xlat8.zzzz + u_xlat6;
					    u_xlat33 = u_xlat33 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat33;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat33;
					    u_xlat13.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat5 = u_xlat0.xxxx * u_xlat5;
					    u_xlat5 = u_xlat0.yyyy * u_xlat5;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat8.zzzz + u_xlat6;
					    u_xlat3 = u_xlat5 * u_xlat8.zzzz + u_xlat3;
					    u_xlat5 = textureLod(_NormalTex, u_xlat13.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat13.zx, 0.0);
					    u_xlat4.w = u_xlat13.x;
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
					    u_xlat33 = u_xlat0.w * 0.400000006;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = (-u_xlat0.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat0.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat0.z) + unity_MatrixV[2].z;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat2.x = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat25.y = u_xlat1.w * 0.100000001;
					    u_xlat34 = u_xlat2.x * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat35 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat37 = u_xlat1.y * u_xlat35;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat1.x + u_xlat37;
					    u_xlat6.z = u_xlat1.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat8.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat37;
					    u_xlat9 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat21.yz = u_xlat6.yx;
					    u_xlat9.xyz = u_xlat2.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.zzz * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat9.xyz;
					    u_xlat12 = u_xlat35 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat1.x + u_xlat12;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat12;
					    u_xlat21.x = u_xlat5.y * u_xlat8.z;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat4.zzz + u_xlat5.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat21.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat21.zx, 0.0);
					    u_xlat6.w = u_xlat21.x;
					    u_xlat7.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat15.xyz = u_xlat4.yyy * u_xlat5.xyz;
					    u_xlat1.xyz = u_xlat15.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat7.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat15.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat15.xyz = u_xlat2.yyy * u_xlat15.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat15.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat4.xyz = u_xlat2.zxw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat13.xyz = u_xlat1.xyz * u_xlat4.xyz;
					    u_xlat1.x = u_xlat13.y + u_xlat13.x;
					    u_xlat1.x = u_xlat4.z * u_xlat1.z + u_xlat1.x;
					    u_xlat33 = u_xlat1.x * 0.600000024 + u_xlat33;
					    u_xlat1.x = u_xlat34;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat34 = (-u_xlat34);
					    u_xlat34 = clamp(u_xlat34, 0.0, 1.0);
					    u_xlat12 = max(u_xlat34, u_xlat1.x);
					    u_xlat12 = (-u_xlat12) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = float(0.0);
					    u_xlat25.x = float(0.0);
					    u_xlat4 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat3 = textureLod(_CliffTex, u_xlat25.xy, 0.0);
					    u_xlat14.xyz = u_xlat1.xxx * u_xlat4.yzx;
					    u_xlat1.xyz = u_xlat4.xyz * vec3(u_xlat12) + u_xlat14.xyz;
					    u_xlat1.xyz = vec3(u_xlat34) * u_xlat4.zxy + u_xlat1.xyz;
					    u_xlat14.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat14.xyz + u_xlat1.xyz;
					    u_xlat14.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat33) * u_xlat1.xyz + u_xlat14.xyz;
					    u_xlat33 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat33 = inversesqrt(u_xlat33);
					    u_xlat0.xyz = vec3(u_xlat33) * u_xlat0.xyz;
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat14.xyz = u_xlat0.xyw;
					    u_xlat14.xyz = clamp(u_xlat14.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat0.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat11.xy = u_xlat13.xy * u_xlat14.xy;
					    u_xlat11.x = u_xlat11.y + u_xlat11.x;
					    u_xlat11.x = u_xlat13.z * u_xlat14.z + u_xlat11.x;
					    u_xlat11.x = u_xlat11.x / u_xlat2.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat11.xyz = u_xlat2.xyz * u_xlat11.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat11.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * in_COLOR0.xyz;
					    u_xlat1.x = (-_CliffParams.x) + _CliffParams.y;
					    u_xlat1.xyz = u_xlat3.xxx * u_xlat1.xxx + _CliffParams.xxx;
					    u_xlat0.w = in_COLOR0.w;
					    u_xlat1.w = 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1;
					    vs_COLOR0 = u_xlat0 * vec4(_Multiplier);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_COLOR_TREE" }
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
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_21[2];
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_24[2];
						vec4 _MainTex_ST;
						vec4 unused_0_26;
						float _Multiplier;
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
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _TreeTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out float vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					float u_xlat10;
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec2 u_xlat14;
					vec3 u_xlat19;
					float u_xlat20;
					vec2 u_xlat23;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyw = unity_ObjectToWorld[3].xzy * in_POSITION0.www + u_xlat0.xzy;
					    u_xlat1.x = dot(u_xlat0.xy, u_xlat0.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat11.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1 = u_xlat1.x / u_xlat11.x;
					    vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat1 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat1 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat1;
					    u_xlat1 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat1;
					    u_xlat1 = u_xlat1 + unity_ObjectToWorld[3];
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = unity_MatrixVP[3] * u_xlat1.wwww + u_xlat2;
					    u_xlat1.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat1.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xwy + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat31 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat32 = u_xlat2.y * u_xlat31;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat32;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat32;
					    u_xlat6 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat12.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat8.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat8.yyyy;
					    u_xlat6 = u_xlat8.zzzz * u_xlat6;
					    u_xlat7 = u_xlat7 * u_xlat8.xxxx;
					    u_xlat7 = u_xlat8.yyyy * u_xlat7;
					    u_xlat6 = u_xlat7 * u_xlat8.zzzz + u_xlat6;
					    u_xlat31 = u_xlat31 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat31;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat31;
					    u_xlat12.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat5 = u_xlat1.xxxx * u_xlat5;
					    u_xlat5 = u_xlat1.yyyy * u_xlat5;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat8.zzzz + u_xlat6;
					    u_xlat3 = u_xlat5 * u_xlat8.zzzz + u_xlat3;
					    u_xlat5 = textureLod(_NormalTex, u_xlat12.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat12.zx, 0.0);
					    u_xlat4.w = u_xlat12.x;
					    u_xlat2 = u_xlat1.xxxx * u_xlat6;
					    u_xlat2 = u_xlat8.yyyy * u_xlat2;
					    u_xlat5 = u_xlat8.xxxx * u_xlat5;
					    u_xlat5 = u_xlat8.yyyy * u_xlat5;
					    u_xlat3 = u_xlat5 * u_xlat1.zzzz + u_xlat3;
					    u_xlat2 = u_xlat2 * u_xlat1.zzzz + u_xlat3;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat4.xw, 0.0);
					    u_xlat4 = u_xlat1.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.yyyy * u_xlat4;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat1.yyyy * u_xlat3;
					    u_xlat2 = u_xlat3 * u_xlat1.zzzz + u_xlat2;
					    u_xlat1 = u_xlat4 * u_xlat1.zzzz + u_xlat2;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat31 = u_xlat1.w * 0.400000006;
					    u_xlat2.x = (-u_xlat1.x) + unity_MatrixV[0].z;
					    u_xlat2.y = (-u_xlat1.y) + unity_MatrixV[1].z;
					    u_xlat2.z = (-u_xlat1.z) + unity_MatrixV[2].z;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat1.xyz;
					    u_xlat2.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat2.xyz = u_xlat1.xyz * u_xlat2.xxx;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.w = (-u_xlat2.x);
					    u_xlat1.xyz = u_xlat2.xyw;
					    u_xlat1.xyz = clamp(u_xlat1.xyz, 0.0, 1.0);
					    u_xlat2.x = dot(u_xlat2.xyz, _FlashDir.xyz);
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat3.xyz);
					    u_xlat30 = (-u_xlat3.w) * 0.5 + u_xlat0.w;
					    u_xlat30 = u_xlat30 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat12.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat12.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat33 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat4.x = u_xlat0.y * u_xlat33;
					    u_xlat14.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat14.x * u_xlat0.x + u_xlat4.x;
					    u_xlat5.z = u_xlat0.z * u_xlat14.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat14.x * u_xlat7.x + u_xlat4.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat19.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat12.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat10 = u_xlat33 * u_xlat7.y;
					    u_xlat5.y = u_xlat14.x * u_xlat0.x + u_xlat10;
					    u_xlat5.x = u_xlat14.x * u_xlat7.x + u_xlat10;
					    u_xlat19.x = u_xlat14.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat0.xyz = u_xlat12.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat12.yyy * u_xlat0.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat12.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat5.w = u_xlat19.x;
					    u_xlat6.xyz = u_xlat12.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat12.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat12.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat13.xyz = u_xlat12.xxx * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat12.yyy * u_xlat13.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat12.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat4.xyz * u_xlat12.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat12.zzz + u_xlat0.xyz;
					    u_xlat3.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xy = abs(_SunDir.yx);
					    u_xlat12.xyz = u_xlat3.zxw;
					    u_xlat12.xyz = clamp(u_xlat12.xyz, 0.0, 1.0);
					    u_xlat3.x = u_xlat3.x + u_xlat3.y;
					    u_xlat13.xyz = u_xlat0.xyz * u_xlat12.xyz;
					    u_xlat0.xy = u_xlat1.xy * u_xlat13.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat13.z * u_xlat1.z + u_xlat0.x;
					    u_xlat10 = u_xlat13.y + u_xlat13.x;
					    u_xlat10 = u_xlat12.z * u_xlat0.z + u_xlat10;
					    u_xlat10 = u_xlat10 * 0.600000024 + u_xlat31;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat20 = u_xlat30;
					    u_xlat20 = clamp(u_xlat20, 0.0, 1.0);
					    u_xlat30 = (-u_xlat30);
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat1.x = max(u_xlat30, u_xlat20);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = float(0.0);
					    u_xlat23.y = float(0.0);
					    u_xlat4 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat11.xyz = vec3(u_xlat20) * u_xlat4.yzx;
					    u_xlat11.xyz = u_xlat4.xyz * u_xlat1.xxx + u_xlat11.xyz;
					    u_xlat11.xyz = vec3(u_xlat30) * u_xlat4.zxy + u_xlat11.xyz;
					    u_xlat12.xyz = (-u_xlat11.xyz) + _SnowColor.xyz;
					    u_xlat11.xyz = vec3(_SnowAmount) * u_xlat12.xyz + u_xlat11.xyz;
					    u_xlat12.xyz = u_xlat11.xyz * _MinAmbientColor.xyz;
					    u_xlat11.xyz = (-_MinAmbientColor.xyz) * u_xlat11.xyz + _MaxAmbientColor.xyz;
					    u_xlat11.xyz = vec3(u_xlat10) * u_xlat11.xyz + u_xlat12.xyz;
					    u_xlat12.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat11.xyz = u_xlat12.xyz * u_xlat0.xxx + u_xlat11.xyz;
					    u_xlat0.x = u_xlat2.x * u_xlat2.x;
					    u_xlat0.x = (-u_xlat2.x) * u_xlat0.x + 1.0;
					    u_xlat2.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat11.xyz;
					    u_xlat2.w = 1.0;
					    u_xlat2 = u_xlat2 * in_COLOR0;
					    u_xlat23.x = _Year + 0.0500000007;
					    u_xlat3 = textureLod(_TreeTex, u_xlat23.xy, 0.0);
					    u_xlat0.xyz = vec3(u_xlat20) * u_xlat3.yzx;
					    u_xlat0.xyz = u_xlat3.xyz * u_xlat1.xxx + u_xlat0.xyz;
					    u_xlat3.xyz = vec3(u_xlat30) * u_xlat3.zxy + u_xlat0.xyz;
					    u_xlat0 = u_xlat2 * u_xlat3;
					    vs_COLOR0 = u_xlat0 * vec4(_Multiplier);
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
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    SV_Target0 = u_xlat0 * vs_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_COLOR_UP" }
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
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    SV_Target0 = u_xlat0 * vs_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_COLOR_CLIFF" }
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
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    SV_Target0 = u_xlat0 * vs_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_COLOR_TREE" }
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
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    SV_Target0 = u_xlat0 * vs_COLOR0;
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
						vec4 unused_0_8[6];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  float vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat6 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    SV_Target0.w = u_xlat1.w;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat6));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_COLOR_UP" }
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
						vec4 unused_0_8[6];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  float vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat6 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    SV_Target0.w = u_xlat1.w;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat6));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_COLOR_CLIFF" }
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
						vec4 unused_0_8[6];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  float vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat6 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    SV_Target0.w = u_xlat1.w;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat6));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_COLOR_TREE" }
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
						vec4 unused_0_8[6];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  float vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat6 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    SV_Target0.w = u_xlat1.w;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat6));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
			}
		}
	}
}