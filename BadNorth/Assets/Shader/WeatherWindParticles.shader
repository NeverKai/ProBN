Shader "Weather/WindParticles" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_PosTex0 ("Position Texture 0", 2D) = "white" {}
		_PosTex1 ("Position Texture 1", 2D) = "white" {}
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		_Size ("Size", Vector) = (0.1,0.2,0,0)
		_ScreenSpaceSize ("Screen Space Size", Range(0, 1)) = 0
		_Alpha ("Alpha", Vector) = (1,3,0,0)
		[Toggle] _Age ("Age", Float) = 0
		_Lifespan ("Lifespan", Vector) = (1,2,0,0)
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
			GpuProgramID 53859
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
						vec4 unused_0_0[17];
						mat4x4 _Tex2World;
						vec4 unused_0_2[35];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_5[2];
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9;
						float _Interpolator;
						vec4 unused_0_11;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
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
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					bool u_xlatb10;
					float u_xlat15;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat5 * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat5 = (-_Size.x) + _Size.y;
					    u_xlat5 = in_TEXCOORD3.y * u_xlat5 + _Size.x;
					    u_xlat0.x = u_xlat5 * u_xlat0.x + (-u_xlat5);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat5;
					    u_xlat0.xy = u_xlat0.xx * in_TEXCOORD1.xy;
					    u_xlat10.x = _Interpolator + -1.0;
					    u_xlat10.x = -abs(u_xlat10.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat10.xxxx * u_xlat3 + u_xlat2;
					    u_xlat10.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat10.x = dot(u_xlat10.xy, u_xlat10.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + u_xlat10.x;
					    u_xlat10.x = min(u_xlat10.x, 1.0);
					    u_xlat10.x = (-u_xlat10.x) + 1.0;
					    u_xlat15 = (-u_xlat3.y) + 1.0;
					    u_xlat10.x = u_xlat15 * u_xlat10.x;
					    u_xlat10.x = u_xlat10.x * 1.39999998;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat0.xy = u_xlat10.xx * u_xlat0.xy;
					    u_xlat0.xy = u_xlat3.ww * u_xlat0.xy;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].zxy;
					    u_xlat1.xyw = _Tex2World[0].zxy * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].zxy * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].zxy;
					    u_xlat1.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlatb10 = 1.0<_Interpolator;
					    u_xlat1.xyz = (bool(u_xlatb10)) ? (-u_xlat1.xyz) : u_xlat1.xyz;
					    u_xlat2.z = (-unity_MatrixV[0].z);
					    u_xlat2.x = (-unity_MatrixV[1].z);
					    u_xlat2.y = (-unity_MatrixV[2].z);
					    u_xlat10.x = dot(u_xlat2.yzx, u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat2.zxy) * u_xlat10.xxx + u_xlat1.yzx;
					    u_xlat10.x = dot(u_xlat4.xyz, u_xlat4.xyz);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat15 = u_xlat10.x + 1.0;
					    u_xlatb10 = 2.0<u_xlat10.x;
					    u_xlat0.x = u_xlat15 * u_xlat0.x;
					    u_xlat5 = u_xlat0.y / u_xlat15;
					    u_xlat4.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat15 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat15 = inversesqrt(u_xlat15);
					    u_xlat1.xyz = vec3(u_xlat15) * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat1.xyz = vec3(u_xlat5) * u_xlat1.xyz;
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat2.xyz = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].xyz;
					    u_xlat2.w = max(u_xlat2.y, _WaterLevel);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + u_xlat2.xwz;
					    u_xlat0.xyw = u_xlat0.xyw * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat0.xyz = (bool(u_xlatb10)) ? vec3(0.0, 0.0, 0.0) : u_xlat0.xyw;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = (-_Alpha.x) + _Alpha.y;
					    u_xlat0.x = in_TEXCOORD3.y * u_xlat0.x + _Alpha.x;
					    u_xlat5 = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat1 = vec4(u_xlat5) * in_COLOR0;
					    vs_COLOR0.w = u_xlat0.x * u_xlat1.w;
					    vs_COLOR0.xyz = u_xlat1.xyz;
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
						vec4 unused_0_0[17];
						mat4x4 _Tex2World;
						vec4 unused_0_2[35];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_5[2];
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9;
						float _Interpolator;
						vec4 unused_0_11;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
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
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					bool u_xlatb10;
					float u_xlat15;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat5 * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat5 = (-_Size.x) + _Size.y;
					    u_xlat5 = in_TEXCOORD3.y * u_xlat5 + _Size.x;
					    u_xlat0.x = u_xlat5 * u_xlat0.x + (-u_xlat5);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat5;
					    u_xlat0.xy = u_xlat0.xx * in_TEXCOORD1.xy;
					    u_xlat10.x = _Interpolator + -1.0;
					    u_xlat10.x = -abs(u_xlat10.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat10.xxxx * u_xlat3 + u_xlat2;
					    u_xlat10.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat10.x = dot(u_xlat10.xy, u_xlat10.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + u_xlat10.x;
					    u_xlat10.x = min(u_xlat10.x, 1.0);
					    u_xlat10.x = (-u_xlat10.x) + 1.0;
					    u_xlat15 = (-u_xlat3.y) + 1.0;
					    u_xlat10.x = u_xlat15 * u_xlat10.x;
					    u_xlat10.x = u_xlat10.x * 1.39999998;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat0.xy = u_xlat10.xx * u_xlat0.xy;
					    u_xlat0.xy = u_xlat3.ww * u_xlat0.xy;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].zxy;
					    u_xlat1.xyw = _Tex2World[0].zxy * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].zxy * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].zxy;
					    u_xlat1.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlatb10 = 1.0<_Interpolator;
					    u_xlat1.xyz = (bool(u_xlatb10)) ? (-u_xlat1.xyz) : u_xlat1.xyz;
					    u_xlat2.z = (-unity_MatrixV[0].z);
					    u_xlat2.x = (-unity_MatrixV[1].z);
					    u_xlat2.y = (-unity_MatrixV[2].z);
					    u_xlat10.x = dot(u_xlat2.yzx, u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat2.zxy) * u_xlat10.xxx + u_xlat1.yzx;
					    u_xlat10.x = dot(u_xlat4.xyz, u_xlat4.xyz);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat15 = u_xlat10.x + 1.0;
					    u_xlatb10 = 2.0<u_xlat10.x;
					    u_xlat0.x = u_xlat15 * u_xlat0.x;
					    u_xlat5 = u_xlat0.y / u_xlat15;
					    u_xlat4.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat15 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat15 = inversesqrt(u_xlat15);
					    u_xlat1.xyz = vec3(u_xlat15) * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat1.xyz = vec3(u_xlat5) * u_xlat1.xyz;
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat2.xyz = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].xyz;
					    u_xlat2.w = max(u_xlat2.y, _WaterLevel);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + u_xlat2.xwz;
					    u_xlat0.xyw = u_xlat0.xyw * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat0.xyz = (bool(u_xlatb10)) ? vec3(0.0, 0.0, 0.0) : u_xlat0.xyw;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = (-_Alpha.x) + _Alpha.y;
					    u_xlat0.x = in_TEXCOORD3.y * u_xlat0.x + _Alpha.x;
					    u_xlat5 = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat1 = vec4(u_xlat5) * in_COLOR0;
					    vs_COLOR0.w = u_xlat0.x * u_xlat1.w;
					    vs_COLOR0.xyz = u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_AGE_ON" }
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
						vec4 unused_0_0[17];
						mat4x4 _Tex2World;
						vec4 unused_0_2[35];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_5[2];
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9;
						float _Interpolator;
						vec4 _Lifespan;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					bool u_xlatb15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat5 * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat5 = (-_Lifespan.x) + _Lifespan.y;
					    u_xlat5 = in_TEXCOORD3.y * u_xlat5 + _Lifespan.x;
					    u_xlat10.x = (-in_TEXCOORD3.x) + _Time.y;
					    u_xlat5 = u_xlat10.x / u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat10.xy = (-_Size.xz) + _Size.yw;
					    u_xlat10.xy = in_TEXCOORD3.yy * u_xlat10.xy + _Size.xz;
					    u_xlat15 = (-u_xlat10.x) + u_xlat10.y;
					    u_xlat10.x = u_xlat5 * u_xlat15 + u_xlat10.x;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x + (-u_xlat10.x);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat10.x;
					    u_xlat0.xz = u_xlat0.xx * in_TEXCOORD1.xy;
					    u_xlat15 = _Interpolator + -1.0;
					    u_xlat15 = -abs(u_xlat15) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = vec4(u_xlat15) * u_xlat3 + u_xlat2;
					    u_xlat4.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat15 = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat15 = sqrt(u_xlat15);
					    u_xlat15 = u_xlat15 + u_xlat15;
					    u_xlat15 = min(u_xlat15, 1.0);
					    u_xlat15 = (-u_xlat15) + 1.0;
					    u_xlat16 = (-u_xlat3.y) + 1.0;
					    u_xlat15 = u_xlat15 * u_xlat16;
					    u_xlat15 = u_xlat15 * 1.39999998;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat0.xz = vec2(u_xlat15) * u_xlat0.xz;
					    u_xlat0.xz = u_xlat3.ww * u_xlat0.xz;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].zxy;
					    u_xlat1.xyw = _Tex2World[0].zxy * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].zxy * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].zxy;
					    u_xlat1.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlatb15 = 1.0<_Interpolator;
					    u_xlat1.xyz = (bool(u_xlatb15)) ? (-u_xlat1.xyz) : u_xlat1.xyz;
					    u_xlat2.z = (-unity_MatrixV[0].z);
					    u_xlat2.x = (-unity_MatrixV[1].z);
					    u_xlat2.y = (-unity_MatrixV[2].z);
					    u_xlat15 = dot(u_xlat2.yzx, u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat2.zxy) * vec3(u_xlat15) + u_xlat1.yzx;
					    u_xlat15 = dot(u_xlat4.xyz, u_xlat4.xyz);
					    u_xlat15 = sqrt(u_xlat15);
					    u_xlat16 = u_xlat15 + 1.0;
					    u_xlatb15 = 2.0<u_xlat15;
					    u_xlat0.x = u_xlat0.x * u_xlat16;
					    u_xlat10.x = u_xlat0.z / u_xlat16;
					    u_xlat4.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat16 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat16 = inversesqrt(u_xlat16);
					    u_xlat1.xyz = vec3(u_xlat16) * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat1.xyz = u_xlat10.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.xyz;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat3.xyw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat3.w = max(u_xlat3.y, _WaterLevel);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xwz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat0.xzw = (bool(u_xlatb15)) ? vec3(0.0, 0.0, 0.0) : u_xlat1.xyz;
					    u_xlat1 = u_xlat0.zzzz * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat0.xz = (-_Alpha.xz) + _Alpha.yw;
					    u_xlat0.xz = in_TEXCOORD3.yy * u_xlat0.xz + _Alpha.xz;
					    u_xlat10.x = (-u_xlat0.x) + u_xlat0.z;
					    u_xlat0.x = u_xlat5 * u_xlat10.x + u_xlat0.x;
					    u_xlat5 = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat1 = vec4(u_xlat5) * in_COLOR0;
					    vs_COLOR0.w = u_xlat0.x * u_xlat1.w;
					    vs_COLOR0.xyz = u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_AGE_ON" }
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
						vec4 unused_0_0[17];
						mat4x4 _Tex2World;
						vec4 unused_0_2[35];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_5[2];
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9;
						float _Interpolator;
						vec4 _Lifespan;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					bool u_xlatb15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat5 * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat5 = (-_Lifespan.x) + _Lifespan.y;
					    u_xlat5 = in_TEXCOORD3.y * u_xlat5 + _Lifespan.x;
					    u_xlat10.x = (-in_TEXCOORD3.x) + _Time.y;
					    u_xlat5 = u_xlat10.x / u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat10.xy = (-_Size.xz) + _Size.yw;
					    u_xlat10.xy = in_TEXCOORD3.yy * u_xlat10.xy + _Size.xz;
					    u_xlat15 = (-u_xlat10.x) + u_xlat10.y;
					    u_xlat10.x = u_xlat5 * u_xlat15 + u_xlat10.x;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x + (-u_xlat10.x);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat10.x;
					    u_xlat0.xz = u_xlat0.xx * in_TEXCOORD1.xy;
					    u_xlat15 = _Interpolator + -1.0;
					    u_xlat15 = -abs(u_xlat15) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = vec4(u_xlat15) * u_xlat3 + u_xlat2;
					    u_xlat4.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat15 = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat15 = sqrt(u_xlat15);
					    u_xlat15 = u_xlat15 + u_xlat15;
					    u_xlat15 = min(u_xlat15, 1.0);
					    u_xlat15 = (-u_xlat15) + 1.0;
					    u_xlat16 = (-u_xlat3.y) + 1.0;
					    u_xlat15 = u_xlat15 * u_xlat16;
					    u_xlat15 = u_xlat15 * 1.39999998;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat0.xz = vec2(u_xlat15) * u_xlat0.xz;
					    u_xlat0.xz = u_xlat3.ww * u_xlat0.xz;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].zxy;
					    u_xlat1.xyw = _Tex2World[0].zxy * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].zxy * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].zxy;
					    u_xlat1.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlatb15 = 1.0<_Interpolator;
					    u_xlat1.xyz = (bool(u_xlatb15)) ? (-u_xlat1.xyz) : u_xlat1.xyz;
					    u_xlat2.z = (-unity_MatrixV[0].z);
					    u_xlat2.x = (-unity_MatrixV[1].z);
					    u_xlat2.y = (-unity_MatrixV[2].z);
					    u_xlat15 = dot(u_xlat2.yzx, u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat2.zxy) * vec3(u_xlat15) + u_xlat1.yzx;
					    u_xlat15 = dot(u_xlat4.xyz, u_xlat4.xyz);
					    u_xlat15 = sqrt(u_xlat15);
					    u_xlat16 = u_xlat15 + 1.0;
					    u_xlatb15 = 2.0<u_xlat15;
					    u_xlat0.x = u_xlat0.x * u_xlat16;
					    u_xlat10.x = u_xlat0.z / u_xlat16;
					    u_xlat4.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat16 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat16 = inversesqrt(u_xlat16);
					    u_xlat1.xyz = vec3(u_xlat16) * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat1.xyz = u_xlat10.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.xyz;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat3.xyw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat3.w = max(u_xlat3.y, _WaterLevel);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xwz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat0.xzw = (bool(u_xlatb15)) ? vec3(0.0, 0.0, 0.0) : u_xlat1.xyz;
					    u_xlat1 = u_xlat0.zzzz * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat0.xz = (-_Alpha.xz) + _Alpha.yw;
					    u_xlat0.xz = in_TEXCOORD3.yy * u_xlat0.xz + _Alpha.xz;
					    u_xlat10.x = (-u_xlat0.x) + u_xlat0.z;
					    u_xlat0.x = u_xlat5 * u_xlat10.x + u_xlat0.x;
					    u_xlat5 = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat1 = vec4(u_xlat5) * in_COLOR0;
					    vs_COLOR0.w = u_xlat0.x * u_xlat1.w;
					    vs_COLOR0.xyz = u_xlat1.xyz;
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
						vec4 unused_0_6[9];
						mat4x4 _Tex2World;
						vec4 unused_0_8[12];
						vec3 _SunDir;
						vec4 unused_0_10[4];
						vec4 _SideSunColor;
						vec4 unused_0_12[2];
						float _Year;
						vec4 unused_0_14[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_18[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_25;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_29[2];
						vec4 _MainTex_ST;
						vec4 unused_0_31;
						float _Interpolator;
						vec4 unused_0_33;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
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
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
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
					vec3 u_xlat20;
					vec3 u_xlat21;
					float u_xlat22;
					float u_xlat33;
					bool u_xlatb33;
					float u_xlat34;
					float u_xlat35;
					float u_xlat36;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat11.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat11.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat11.x = (-_Size.x) + _Size.y;
					    u_xlat11.x = in_TEXCOORD3.y * u_xlat11.x + _Size.x;
					    u_xlat22 = u_xlat11.x * u_xlat0.x + (-u_xlat11.x);
					    u_xlat11.x = _ScreenSpaceSize * u_xlat22 + u_xlat11.x;
					    u_xlat11.xy = u_xlat11.xx * in_TEXCOORD1.xy;
					    u_xlat33 = _Interpolator + -1.0;
					    u_xlat33 = -abs(u_xlat33) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = vec4(u_xlat33) * u_xlat3 + u_xlat2;
					    u_xlat4.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat33 = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat33 = sqrt(u_xlat33);
					    u_xlat33 = u_xlat33 + u_xlat33;
					    u_xlat33 = min(u_xlat33, 1.0);
					    u_xlat33 = (-u_xlat33) + 1.0;
					    u_xlat34 = (-u_xlat3.y) + 1.0;
					    u_xlat33 = u_xlat33 * u_xlat34;
					    u_xlat33 = u_xlat33 * 1.39999998;
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat33) * u_xlat11.xy;
					    u_xlat11.xy = u_xlat3.ww * u_xlat11.xy;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].zxy;
					    u_xlat1.xyw = _Tex2World[0].zxy * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].zxy * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].zxy;
					    u_xlat1.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlatb33 = 1.0<_Interpolator;
					    u_xlat1.xyz = (bool(u_xlatb33)) ? (-u_xlat1.xyz) : u_xlat1.xyz;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat2.z = unity_MatrixV[2].z;
					    u_xlat33 = dot((-u_xlat2.zxy), u_xlat1.xyz);
					    u_xlat4.xyz = u_xlat2.xyz * vec3(u_xlat33) + u_xlat1.yzx;
					    u_xlat33 = dot(u_xlat4.xyz, u_xlat4.xyz);
					    u_xlat33 = sqrt(u_xlat33);
					    u_xlat34 = u_xlat33 + 1.0;
					    u_xlatb33 = 2.0<u_xlat33;
					    u_xlat11.x = u_xlat11.x * u_xlat34;
					    u_xlat22 = u_xlat11.y / u_xlat34;
					    u_xlat4.xyz = u_xlat1.xyz * (-u_xlat2.yzx);
					    u_xlat1.xyz = u_xlat1.zxy * (-u_xlat2.zxy) + (-u_xlat4.xyz);
					    u_xlat34 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat34 = inversesqrt(u_xlat34);
					    u_xlat1.xyz = vec3(u_xlat34) * u_xlat1.xyz;
					    u_xlat4.xyz = (-u_xlat2.yzx) * u_xlat1.zxy;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat2.zxy) + (-u_xlat4.xyz);
					    u_xlat1.xyz = vec3(u_xlat22) * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat11.xxx * u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat3.xyw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat5.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat3.w = max(u_xlat3.y, _WaterLevel);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xwz;
					    u_xlat1.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat1.xyw = (bool(u_xlatb33)) ? vec3(0.0, 0.0, 0.0) : u_xlat1.xzy;
					    u_xlat11.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat11.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat11.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat11.x;
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
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat3.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat33 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat35 = u_xlat0.y * u_xlat33;
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat0.x + u_xlat35;
					    u_xlat6.z = u_xlat0.z * u_xlat5.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7 = u_xlat4.xxxx * u_xlat7;
					    u_xlat7 = u_xlat4.yyyy * u_xlat7;
					    u_xlat8.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat35;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat21.yz = u_xlat6.yx;
					    u_xlat9 = u_xlat3.xxxx * u_xlat9;
					    u_xlat9 = u_xlat4.yyyy * u_xlat9;
					    u_xlat9 = u_xlat4.zzzz * u_xlat9;
					    u_xlat7 = u_xlat7 * u_xlat4.zzzz + u_xlat9;
					    u_xlat11.x = u_xlat33 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat0.x + u_xlat11.x;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat11.x;
					    u_xlat21.x = u_xlat5.y * u_xlat8.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat5 = u_xlat3.xxxx * u_xlat5;
					    u_xlat5 = u_xlat3.yyyy * u_xlat5;
					    u_xlat0 = u_xlat4.xxxx * u_xlat0;
					    u_xlat0 = u_xlat3.yyyy * u_xlat0;
					    u_xlat0 = u_xlat0 * u_xlat4.zzzz + u_xlat7;
					    u_xlat0 = u_xlat5 * u_xlat4.zzzz + u_xlat0;
					    u_xlat5 = textureLod(_NormalTex, u_xlat21.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat21.zx, 0.0);
					    u_xlat6.w = u_xlat21.x;
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat7 = u_xlat4.yyyy * u_xlat7;
					    u_xlat5 = u_xlat4.xxxx * u_xlat5;
					    u_xlat5 = u_xlat4.yyyy * u_xlat5;
					    u_xlat0 = u_xlat5 * u_xlat3.zzzz + u_xlat0;
					    u_xlat0 = u_xlat7 * u_xlat3.zzzz + u_xlat0;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat6 = u_xlat3.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat4 = u_xlat4.xxxx * u_xlat5;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat3.zzzz + u_xlat0;
					    u_xlat0 = u_xlat6 * u_xlat3.zzzz + u_xlat0;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat33 = u_xlat0.w * 0.400000006;
					    u_xlat2.xyz = (-u_xlat0.xyz) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat34 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat34 = u_xlat34 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat35 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat36 = u_xlat1.y * u_xlat35;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat36;
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat36;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat20.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat12 = u_xlat35 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat12;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat12;
					    u_xlat20.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat20.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat20.zx, 0.0);
					    u_xlat5.w = u_xlat20.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat14.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat2.yyy * u_xlat14.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat13.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat13.y + u_xlat13.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat33 = u_xlat1.x * 0.600000024 + u_xlat33;
					    u_xlat1.x = u_xlat34;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat34 = (-u_xlat34);
					    u_xlat34 = clamp(u_xlat34, 0.0, 1.0);
					    u_xlat12 = max(u_xlat34, u_xlat1.x);
					    u_xlat12 = (-u_xlat12) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat1.xxx * u_xlat3.yzx;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat12) + u_xlat4.xyz;
					    u_xlat1.xyz = vec3(u_xlat34) * u_xlat3.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat33) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat33 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat33 = inversesqrt(u_xlat33);
					    u_xlat0.xyz = vec3(u_xlat33) * u_xlat0.xyz;
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat3.xyz = u_xlat0.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat0.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat11.xy = u_xlat13.xy * u_xlat3.xy;
					    u_xlat11.x = u_xlat11.y + u_xlat11.x;
					    u_xlat11.x = u_xlat13.z * u_xlat3.z + u_xlat11.x;
					    u_xlat11.x = u_xlat11.x / u_xlat2.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat11.xyz = u_xlat2.xyz * u_xlat11.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat11.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * in_COLOR0;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    u_xlat0.x = (-_Alpha.x) + _Alpha.y;
					    u_xlat0.x = in_TEXCOORD3.y * u_xlat0.x + _Alpha.x;
					    vs_COLOR0.w = u_xlat0.x * u_xlat0.w;
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
						vec4 unused_0_6[9];
						mat4x4 _Tex2World;
						vec4 unused_0_8[12];
						vec3 _SunDir;
						vec4 unused_0_10[4];
						vec4 _SideSunColor;
						vec4 unused_0_12[2];
						float _Year;
						vec4 unused_0_14[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_18[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_25;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_29[2];
						vec4 _MainTex_ST;
						vec4 unused_0_31;
						float _Interpolator;
						vec4 unused_0_33;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
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
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
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
					vec3 u_xlat20;
					vec3 u_xlat21;
					float u_xlat22;
					float u_xlat33;
					bool u_xlatb33;
					float u_xlat34;
					float u_xlat35;
					float u_xlat36;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat11.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat11.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat11.x = (-_Size.x) + _Size.y;
					    u_xlat11.x = in_TEXCOORD3.y * u_xlat11.x + _Size.x;
					    u_xlat22 = u_xlat11.x * u_xlat0.x + (-u_xlat11.x);
					    u_xlat11.x = _ScreenSpaceSize * u_xlat22 + u_xlat11.x;
					    u_xlat11.xy = u_xlat11.xx * in_TEXCOORD1.xy;
					    u_xlat33 = _Interpolator + -1.0;
					    u_xlat33 = -abs(u_xlat33) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = vec4(u_xlat33) * u_xlat3 + u_xlat2;
					    u_xlat4.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat33 = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat33 = sqrt(u_xlat33);
					    u_xlat33 = u_xlat33 + u_xlat33;
					    u_xlat33 = min(u_xlat33, 1.0);
					    u_xlat33 = (-u_xlat33) + 1.0;
					    u_xlat34 = (-u_xlat3.y) + 1.0;
					    u_xlat33 = u_xlat33 * u_xlat34;
					    u_xlat33 = u_xlat33 * 1.39999998;
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat33) * u_xlat11.xy;
					    u_xlat11.xy = u_xlat3.ww * u_xlat11.xy;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].zxy;
					    u_xlat1.xyw = _Tex2World[0].zxy * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].zxy * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].zxy;
					    u_xlat1.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlatb33 = 1.0<_Interpolator;
					    u_xlat1.xyz = (bool(u_xlatb33)) ? (-u_xlat1.xyz) : u_xlat1.xyz;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat2.z = unity_MatrixV[2].z;
					    u_xlat33 = dot((-u_xlat2.zxy), u_xlat1.xyz);
					    u_xlat4.xyz = u_xlat2.xyz * vec3(u_xlat33) + u_xlat1.yzx;
					    u_xlat33 = dot(u_xlat4.xyz, u_xlat4.xyz);
					    u_xlat33 = sqrt(u_xlat33);
					    u_xlat34 = u_xlat33 + 1.0;
					    u_xlatb33 = 2.0<u_xlat33;
					    u_xlat11.x = u_xlat11.x * u_xlat34;
					    u_xlat22 = u_xlat11.y / u_xlat34;
					    u_xlat4.xyz = u_xlat1.xyz * (-u_xlat2.yzx);
					    u_xlat1.xyz = u_xlat1.zxy * (-u_xlat2.zxy) + (-u_xlat4.xyz);
					    u_xlat34 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat34 = inversesqrt(u_xlat34);
					    u_xlat1.xyz = vec3(u_xlat34) * u_xlat1.xyz;
					    u_xlat4.xyz = (-u_xlat2.yzx) * u_xlat1.zxy;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat2.zxy) + (-u_xlat4.xyz);
					    u_xlat1.xyz = vec3(u_xlat22) * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat11.xxx * u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat3.xyw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat5.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat3.w = max(u_xlat3.y, _WaterLevel);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xwz;
					    u_xlat1.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat1.xyw = (bool(u_xlatb33)) ? vec3(0.0, 0.0, 0.0) : u_xlat1.xzy;
					    u_xlat11.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat11.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat11.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat11.x;
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
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat3.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat33 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat35 = u_xlat0.y * u_xlat33;
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat0.x + u_xlat35;
					    u_xlat6.z = u_xlat0.z * u_xlat5.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7 = u_xlat4.xxxx * u_xlat7;
					    u_xlat7 = u_xlat4.yyyy * u_xlat7;
					    u_xlat8.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat35;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat21.yz = u_xlat6.yx;
					    u_xlat9 = u_xlat3.xxxx * u_xlat9;
					    u_xlat9 = u_xlat4.yyyy * u_xlat9;
					    u_xlat9 = u_xlat4.zzzz * u_xlat9;
					    u_xlat7 = u_xlat7 * u_xlat4.zzzz + u_xlat9;
					    u_xlat11.x = u_xlat33 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat0.x + u_xlat11.x;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat11.x;
					    u_xlat21.x = u_xlat5.y * u_xlat8.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat5 = u_xlat3.xxxx * u_xlat5;
					    u_xlat5 = u_xlat3.yyyy * u_xlat5;
					    u_xlat0 = u_xlat4.xxxx * u_xlat0;
					    u_xlat0 = u_xlat3.yyyy * u_xlat0;
					    u_xlat0 = u_xlat0 * u_xlat4.zzzz + u_xlat7;
					    u_xlat0 = u_xlat5 * u_xlat4.zzzz + u_xlat0;
					    u_xlat5 = textureLod(_NormalTex, u_xlat21.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat21.zx, 0.0);
					    u_xlat6.w = u_xlat21.x;
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat7 = u_xlat4.yyyy * u_xlat7;
					    u_xlat5 = u_xlat4.xxxx * u_xlat5;
					    u_xlat5 = u_xlat4.yyyy * u_xlat5;
					    u_xlat0 = u_xlat5 * u_xlat3.zzzz + u_xlat0;
					    u_xlat0 = u_xlat7 * u_xlat3.zzzz + u_xlat0;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat6 = u_xlat3.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat4 = u_xlat4.xxxx * u_xlat5;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat3.zzzz + u_xlat0;
					    u_xlat0 = u_xlat6 * u_xlat3.zzzz + u_xlat0;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat33 = u_xlat0.w * 0.400000006;
					    u_xlat2.xyz = (-u_xlat0.xyz) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat34 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat34 = u_xlat34 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat35 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat36 = u_xlat1.y * u_xlat35;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat36;
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat36;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat20.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat12 = u_xlat35 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat12;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat12;
					    u_xlat20.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat20.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat20.zx, 0.0);
					    u_xlat5.w = u_xlat20.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat14.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat2.yyy * u_xlat14.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat13.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat13.y + u_xlat13.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat33 = u_xlat1.x * 0.600000024 + u_xlat33;
					    u_xlat1.x = u_xlat34;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat34 = (-u_xlat34);
					    u_xlat34 = clamp(u_xlat34, 0.0, 1.0);
					    u_xlat12 = max(u_xlat34, u_xlat1.x);
					    u_xlat12 = (-u_xlat12) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat1.xxx * u_xlat3.yzx;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat12) + u_xlat4.xyz;
					    u_xlat1.xyz = vec3(u_xlat34) * u_xlat3.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat33) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat33 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat33 = inversesqrt(u_xlat33);
					    u_xlat0.xyz = vec3(u_xlat33) * u_xlat0.xyz;
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat3.xyz = u_xlat0.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat0.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat11.xy = u_xlat13.xy * u_xlat3.xy;
					    u_xlat11.x = u_xlat11.y + u_xlat11.x;
					    u_xlat11.x = u_xlat13.z * u_xlat3.z + u_xlat11.x;
					    u_xlat11.x = u_xlat11.x / u_xlat2.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat11.xyz = u_xlat2.xyz * u_xlat11.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat11.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * in_COLOR0;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    u_xlat0.x = (-_Alpha.x) + _Alpha.y;
					    u_xlat0.x = in_TEXCOORD3.y * u_xlat0.x + _Alpha.x;
					    vs_COLOR0.w = u_xlat0.x * u_xlat0.w;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AGE_ON" }
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
						vec4 unused_0_6[9];
						mat4x4 _Tex2World;
						vec4 unused_0_8[12];
						vec3 _SunDir;
						vec4 unused_0_10[4];
						vec4 _SideSunColor;
						vec4 unused_0_12[2];
						float _Year;
						vec4 unused_0_14[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_18[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_25;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_29[2];
						vec4 _MainTex_ST;
						vec4 unused_0_31;
						float _Interpolator;
						vec4 _Lifespan;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					float u_xlat0;
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
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec2 u_xlat15;
					vec3 u_xlat20;
					vec3 u_xlat21;
					float u_xlat22;
					float u_xlat23;
					float u_xlat33;
					float u_xlat34;
					bool u_xlatb34;
					float u_xlat35;
					float u_xlat36;
					void main()
					{
					    u_xlat0 = (-_Lifespan.x) + _Lifespan.y;
					    u_xlat0 = in_TEXCOORD3.y * u_xlat0 + _Lifespan.x;
					    u_xlat11.x = (-in_TEXCOORD3.x) + _Time.y;
					    u_xlat0 = u_xlat11.x / u_xlat0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat11.xy = (-_Size.xz) + _Size.yw;
					    u_xlat11.xy = in_TEXCOORD3.yy * u_xlat11.xy + _Size.xz;
					    u_xlat22 = (-u_xlat11.x) + u_xlat11.y;
					    u_xlat11.x = u_xlat0 * u_xlat22 + u_xlat11.x;
					    u_xlat22 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat22 = u_xlat22 + u_xlat22;
					    u_xlat33 = _AAFactor + 1.0;
					    u_xlat22 = u_xlat33 * u_xlat22;
					    u_xlat22 = u_xlat22 * _LineWidth;
					    u_xlat33 = u_xlat11.x * u_xlat22 + (-u_xlat11.x);
					    u_xlat11.x = _ScreenSpaceSize * u_xlat33 + u_xlat11.x;
					    u_xlat11.xz = u_xlat11.xx * in_TEXCOORD1.xy;
					    u_xlat1.x = _Interpolator + -1.0;
					    u_xlat1.x = -abs(u_xlat1.x) + 1.0;
					    u_xlat2 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat4 = u_xlat2 + (-u_xlat3);
					    u_xlat1 = u_xlat1.xxxx * u_xlat4 + u_xlat3;
					    u_xlat4.xy = u_xlat1.xz + vec2(-0.5, -0.5);
					    u_xlat35 = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat35 = sqrt(u_xlat35);
					    u_xlat35 = u_xlat35 + u_xlat35;
					    u_xlat35 = min(u_xlat35, 1.0);
					    u_xlat35 = (-u_xlat35) + 1.0;
					    u_xlat36 = (-u_xlat1.y) + 1.0;
					    u_xlat35 = u_xlat35 * u_xlat36;
					    u_xlat35 = u_xlat35 * 1.39999998;
					    u_xlat35 = clamp(u_xlat35, 0.0, 1.0);
					    u_xlat11.xz = u_xlat11.xz * vec2(u_xlat35);
					    u_xlat11.xz = u_xlat1.ww * u_xlat11.xz;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].zxy;
					    u_xlat3.xyw = _Tex2World[0].zxy * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].zxy * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat2.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlatb34 = 1.0<_Interpolator;
					    u_xlat2.xyz = (bool(u_xlatb34)) ? (-u_xlat2.xyz) : u_xlat2.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat34 = dot((-u_xlat3.zxy), u_xlat2.xyz);
					    u_xlat4.xyz = u_xlat3.xyz * vec3(u_xlat34) + u_xlat2.yzx;
					    u_xlat34 = dot(u_xlat4.xyz, u_xlat4.xyz);
					    u_xlat34 = sqrt(u_xlat34);
					    u_xlat35 = u_xlat34 + 1.0;
					    u_xlatb34 = 2.0<u_xlat34;
					    u_xlat11.x = u_xlat11.x * u_xlat35;
					    u_xlat33 = u_xlat11.z / u_xlat35;
					    u_xlat4.xyz = u_xlat2.xyz * (-u_xlat3.yzx);
					    u_xlat2.xyz = u_xlat2.zxy * (-u_xlat3.zxy) + (-u_xlat4.xyz);
					    u_xlat35 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat35 = inversesqrt(u_xlat35);
					    u_xlat2.xyz = vec3(u_xlat35) * u_xlat2.xyz;
					    u_xlat4.xyz = (-u_xlat3.yzx) * u_xlat2.zxy;
					    u_xlat4.xyz = u_xlat2.yzx * (-u_xlat3.zxy) + (-u_xlat4.xyz);
					    u_xlat2.xyz = vec3(u_xlat33) * u_xlat2.xyz;
					    u_xlat4.xyz = u_xlat11.xxx * u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat1.yyy * _Tex2World[1].xyz;
					    u_xlat5.xyz = _Tex2World[0].xyz * u_xlat1.xxx + u_xlat5.xyz;
					    u_xlat1.xyz = _Tex2World[2].xyz * u_xlat1.zzz + u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat1.xyz + _Tex2World[3].xyz;
					    u_xlat5.w = max(u_xlat5.y, _WaterLevel);
					    u_xlat1.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + u_xlat5.xwz;
					    u_xlat1.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat1.xyw = (bool(u_xlatb34)) ? vec3(0.0, 0.0, 0.0) : u_xlat1.xzy;
					    u_xlat11.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat11.x = u_xlat11.x / u_xlat22;
					    vs_TEXCOORD1.y = u_xlat11.x + 0.5;
					    u_xlat11.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat11.x = sqrt(u_xlat11.x);
					    u_xlat11.x = u_xlat11.x + (-_FogMinRad);
					    u_xlat22 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat11.x / u_xlat22;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat2 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat2.y = 1.0;
					    u_xlat1.xyz = u_xlat1.xwy + u_xlat2.xyz;
					    u_xlat11.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat11.xyz = max(u_xlat11.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat11.xyz = min(u_xlat11.xyz, u_xlat2.xyz);
					    u_xlat11.xyz = u_xlat11.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat11.xyz);
					    u_xlat11.xyz = floor(u_xlat11.xyz);
					    u_xlat4.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat35 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat11.y * u_xlat35;
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat11.x + u_xlat36;
					    u_xlat6.z = u_xlat11.z * u_xlat5.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7 = u_xlat4.xxxx * u_xlat7;
					    u_xlat7 = u_xlat4.yyyy * u_xlat7;
					    u_xlat8.xyz = u_xlat11.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat36;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat21.yz = u_xlat6.yx;
					    u_xlat9 = u_xlat2.xxxx * u_xlat9;
					    u_xlat9 = u_xlat4.yyyy * u_xlat9;
					    u_xlat9 = u_xlat4.zzzz * u_xlat9;
					    u_xlat7 = u_xlat7 * u_xlat4.zzzz + u_xlat9;
					    u_xlat22 = u_xlat35 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat11.x + u_xlat22;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat22;
					    u_xlat21.x = u_xlat5.y * u_xlat8.z;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat8 = u_xlat2.xxxx * u_xlat8;
					    u_xlat8 = u_xlat2.yyyy * u_xlat8;
					    u_xlat5 = u_xlat4.xxxx * u_xlat5;
					    u_xlat5 = u_xlat2.yyyy * u_xlat5;
					    u_xlat5 = u_xlat5 * u_xlat4.zzzz + u_xlat7;
					    u_xlat5 = u_xlat8 * u_xlat4.zzzz + u_xlat5;
					    u_xlat7 = textureLod(_NormalTex, u_xlat21.yx, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat21.zx, 0.0);
					    u_xlat6.w = u_xlat21.x;
					    u_xlat8 = u_xlat2.xxxx * u_xlat8;
					    u_xlat8 = u_xlat4.yyyy * u_xlat8;
					    u_xlat7 = u_xlat4.xxxx * u_xlat7;
					    u_xlat7 = u_xlat4.yyyy * u_xlat7;
					    u_xlat5 = u_xlat7 * u_xlat2.zzzz + u_xlat5;
					    u_xlat5 = u_xlat8 * u_xlat2.zzzz + u_xlat5;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat6 = u_xlat2.xxxx * u_xlat6;
					    u_xlat6 = u_xlat2.yyyy * u_xlat6;
					    u_xlat4 = u_xlat4.xxxx * u_xlat7;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat2.zzzz + u_xlat5;
					    u_xlat2 = u_xlat6 * u_xlat2.zzzz + u_xlat4;
					    u_xlat11.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = u_xlat2.w * 0.400000006;
					    u_xlat13.xyz = (-u_xlat11.xyz) + u_xlat3.xyz;
					    u_xlat11.xyz = u_xlat13.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat11.xyz;
					    u_xlat1.xyz = u_xlat11.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat34 = (-u_xlat3.w) * 0.5 + u_xlat1.w;
					    u_xlat34 = u_xlat34 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat13.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat13.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat36 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat4.x = u_xlat1.y * u_xlat36;
					    u_xlat15.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat15.x * u_xlat1.x + u_xlat4.x;
					    u_xlat5.z = u_xlat1.z * u_xlat15.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat15.x * u_xlat7.x + u_xlat4.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat20.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat13.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat12.x = u_xlat36 * u_xlat7.y;
					    u_xlat5.y = u_xlat15.x * u_xlat1.x + u_xlat12.x;
					    u_xlat5.x = u_xlat15.x * u_xlat7.x + u_xlat12.x;
					    u_xlat20.x = u_xlat15.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat13.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat13.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat13.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat20.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat20.zx, 0.0);
					    u_xlat5.w = u_xlat20.x;
					    u_xlat6.xyz = u_xlat13.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat14.xyz * u_xlat13.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat13.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat14.xyz = u_xlat13.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat13.yyy * u_xlat14.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat13.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat4.xyz * u_xlat13.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat14.xyz * u_xlat13.zzz + u_xlat1.xyz;
					    u_xlat3.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xy = abs(_SunDir.yx);
					    u_xlat13.xyz = u_xlat3.zxw;
					    u_xlat13.xyz = clamp(u_xlat13.xyz, 0.0, 1.0);
					    u_xlat3.x = u_xlat3.x + u_xlat3.y;
					    u_xlat14.xyz = u_xlat1.xyz * u_xlat13.xyz;
					    u_xlat1.x = u_xlat14.y + u_xlat14.x;
					    u_xlat1.x = u_xlat13.z * u_xlat1.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.600000024 + u_xlat2.x;
					    u_xlat12.x = u_xlat34;
					    u_xlat12.x = clamp(u_xlat12.x, 0.0, 1.0);
					    u_xlat34 = (-u_xlat34);
					    u_xlat34 = clamp(u_xlat34, 0.0, 1.0);
					    u_xlat23 = max(u_xlat34, u_xlat12.x);
					    u_xlat23 = (-u_xlat23) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat4.xyz = u_xlat12.xxx * u_xlat2.yzx;
					    u_xlat4.xyz = u_xlat2.xyz * vec3(u_xlat23) + u_xlat4.xyz;
					    u_xlat12.xyz = vec3(u_xlat34) * u_xlat2.zxy + u_xlat4.xyz;
					    u_xlat2.xyz = (-u_xlat12.xyz) + _SnowColor.xyz;
					    u_xlat12.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat12.xyz;
					    u_xlat2.xyz = u_xlat12.xyz * _MinAmbientColor.xyz;
					    u_xlat12.xyz = (-_MinAmbientColor.xyz) * u_xlat12.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = u_xlat1.xxx * u_xlat12.xyz + u_xlat2.xyz;
					    u_xlat34 = dot(u_xlat11.xyz, u_xlat11.xyz);
					    u_xlat34 = inversesqrt(u_xlat34);
					    u_xlat2.xyz = u_xlat11.xyz * vec3(u_xlat34);
					    u_xlat2.w = (-u_xlat2.x);
					    u_xlat11.xyz = u_xlat2.xyw;
					    u_xlat11.xyz = clamp(u_xlat11.xyz, 0.0, 1.0);
					    u_xlat34 = dot(u_xlat2.xyz, _FlashDir.xyz);
					    u_xlat34 = clamp(u_xlat34, 0.0, 1.0);
					    u_xlat34 = (-u_xlat34) + 1.0;
					    u_xlat11.xy = u_xlat11.xy * u_xlat14.xy;
					    u_xlat11.x = u_xlat11.y + u_xlat11.x;
					    u_xlat11.x = u_xlat14.z * u_xlat11.z + u_xlat11.x;
					    u_xlat11.x = u_xlat11.x / u_xlat3.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat11.xyz = u_xlat2.xyz * u_xlat11.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat34 * u_xlat34;
					    u_xlat1.x = (-u_xlat34) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat1.xxx * _FlashColor.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat11.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat1 = u_xlat1 * in_COLOR0;
					    u_xlat11.xy = (-_Alpha.xz) + _Alpha.yw;
					    u_xlat11.xy = in_TEXCOORD3.yy * u_xlat11.xy + _Alpha.xz;
					    u_xlat22 = (-u_xlat11.x) + u_xlat11.y;
					    u_xlat0 = u_xlat0 * u_xlat22 + u_xlat11.x;
					    vs_COLOR0.w = u_xlat0 * u_xlat1.w;
					    vs_COLOR0.xyz = u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AGE_ON" }
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
						vec4 unused_0_6[9];
						mat4x4 _Tex2World;
						vec4 unused_0_8[12];
						vec3 _SunDir;
						vec4 unused_0_10[4];
						vec4 _SideSunColor;
						vec4 unused_0_12[2];
						float _Year;
						vec4 unused_0_14[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_18[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_25;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_29[2];
						vec4 _MainTex_ST;
						vec4 unused_0_31;
						float _Interpolator;
						vec4 _Lifespan;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					float u_xlat0;
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
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec2 u_xlat15;
					vec3 u_xlat20;
					vec3 u_xlat21;
					float u_xlat22;
					float u_xlat23;
					float u_xlat33;
					float u_xlat34;
					bool u_xlatb34;
					float u_xlat35;
					float u_xlat36;
					void main()
					{
					    u_xlat0 = (-_Lifespan.x) + _Lifespan.y;
					    u_xlat0 = in_TEXCOORD3.y * u_xlat0 + _Lifespan.x;
					    u_xlat11.x = (-in_TEXCOORD3.x) + _Time.y;
					    u_xlat0 = u_xlat11.x / u_xlat0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat11.xy = (-_Size.xz) + _Size.yw;
					    u_xlat11.xy = in_TEXCOORD3.yy * u_xlat11.xy + _Size.xz;
					    u_xlat22 = (-u_xlat11.x) + u_xlat11.y;
					    u_xlat11.x = u_xlat0 * u_xlat22 + u_xlat11.x;
					    u_xlat22 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat22 = u_xlat22 + u_xlat22;
					    u_xlat33 = _AAFactor + 1.0;
					    u_xlat22 = u_xlat33 * u_xlat22;
					    u_xlat22 = u_xlat22 * _LineWidth;
					    u_xlat33 = u_xlat11.x * u_xlat22 + (-u_xlat11.x);
					    u_xlat11.x = _ScreenSpaceSize * u_xlat33 + u_xlat11.x;
					    u_xlat11.xz = u_xlat11.xx * in_TEXCOORD1.xy;
					    u_xlat1.x = _Interpolator + -1.0;
					    u_xlat1.x = -abs(u_xlat1.x) + 1.0;
					    u_xlat2 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat4 = u_xlat2 + (-u_xlat3);
					    u_xlat1 = u_xlat1.xxxx * u_xlat4 + u_xlat3;
					    u_xlat4.xy = u_xlat1.xz + vec2(-0.5, -0.5);
					    u_xlat35 = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat35 = sqrt(u_xlat35);
					    u_xlat35 = u_xlat35 + u_xlat35;
					    u_xlat35 = min(u_xlat35, 1.0);
					    u_xlat35 = (-u_xlat35) + 1.0;
					    u_xlat36 = (-u_xlat1.y) + 1.0;
					    u_xlat35 = u_xlat35 * u_xlat36;
					    u_xlat35 = u_xlat35 * 1.39999998;
					    u_xlat35 = clamp(u_xlat35, 0.0, 1.0);
					    u_xlat11.xz = u_xlat11.xz * vec2(u_xlat35);
					    u_xlat11.xz = u_xlat1.ww * u_xlat11.xz;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].zxy;
					    u_xlat3.xyw = _Tex2World[0].zxy * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].zxy * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat2.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlatb34 = 1.0<_Interpolator;
					    u_xlat2.xyz = (bool(u_xlatb34)) ? (-u_xlat2.xyz) : u_xlat2.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat34 = dot((-u_xlat3.zxy), u_xlat2.xyz);
					    u_xlat4.xyz = u_xlat3.xyz * vec3(u_xlat34) + u_xlat2.yzx;
					    u_xlat34 = dot(u_xlat4.xyz, u_xlat4.xyz);
					    u_xlat34 = sqrt(u_xlat34);
					    u_xlat35 = u_xlat34 + 1.0;
					    u_xlatb34 = 2.0<u_xlat34;
					    u_xlat11.x = u_xlat11.x * u_xlat35;
					    u_xlat33 = u_xlat11.z / u_xlat35;
					    u_xlat4.xyz = u_xlat2.xyz * (-u_xlat3.yzx);
					    u_xlat2.xyz = u_xlat2.zxy * (-u_xlat3.zxy) + (-u_xlat4.xyz);
					    u_xlat35 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat35 = inversesqrt(u_xlat35);
					    u_xlat2.xyz = vec3(u_xlat35) * u_xlat2.xyz;
					    u_xlat4.xyz = (-u_xlat3.yzx) * u_xlat2.zxy;
					    u_xlat4.xyz = u_xlat2.yzx * (-u_xlat3.zxy) + (-u_xlat4.xyz);
					    u_xlat2.xyz = vec3(u_xlat33) * u_xlat2.xyz;
					    u_xlat4.xyz = u_xlat11.xxx * u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat1.yyy * _Tex2World[1].xyz;
					    u_xlat5.xyz = _Tex2World[0].xyz * u_xlat1.xxx + u_xlat5.xyz;
					    u_xlat1.xyz = _Tex2World[2].xyz * u_xlat1.zzz + u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat1.xyz + _Tex2World[3].xyz;
					    u_xlat5.w = max(u_xlat5.y, _WaterLevel);
					    u_xlat1.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + u_xlat5.xwz;
					    u_xlat1.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat1.xyw = (bool(u_xlatb34)) ? vec3(0.0, 0.0, 0.0) : u_xlat1.xzy;
					    u_xlat11.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat11.x = u_xlat11.x / u_xlat22;
					    vs_TEXCOORD1.y = u_xlat11.x + 0.5;
					    u_xlat11.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat11.x = sqrt(u_xlat11.x);
					    u_xlat11.x = u_xlat11.x + (-_FogMinRad);
					    u_xlat22 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat11.x / u_xlat22;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat2 = u_xlat1.wwww * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat2.y = 1.0;
					    u_xlat1.xyz = u_xlat1.xwy + u_xlat2.xyz;
					    u_xlat11.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat11.xyz = max(u_xlat11.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat11.xyz = min(u_xlat11.xyz, u_xlat2.xyz);
					    u_xlat11.xyz = u_xlat11.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat11.xyz);
					    u_xlat11.xyz = floor(u_xlat11.xyz);
					    u_xlat4.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat35 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat11.y * u_xlat35;
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat11.x + u_xlat36;
					    u_xlat6.z = u_xlat11.z * u_xlat5.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7 = u_xlat4.xxxx * u_xlat7;
					    u_xlat7 = u_xlat4.yyyy * u_xlat7;
					    u_xlat8.xyz = u_xlat11.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat36;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat21.yz = u_xlat6.yx;
					    u_xlat9 = u_xlat2.xxxx * u_xlat9;
					    u_xlat9 = u_xlat4.yyyy * u_xlat9;
					    u_xlat9 = u_xlat4.zzzz * u_xlat9;
					    u_xlat7 = u_xlat7 * u_xlat4.zzzz + u_xlat9;
					    u_xlat22 = u_xlat35 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat11.x + u_xlat22;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat22;
					    u_xlat21.x = u_xlat5.y * u_xlat8.z;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat8 = u_xlat2.xxxx * u_xlat8;
					    u_xlat8 = u_xlat2.yyyy * u_xlat8;
					    u_xlat5 = u_xlat4.xxxx * u_xlat5;
					    u_xlat5 = u_xlat2.yyyy * u_xlat5;
					    u_xlat5 = u_xlat5 * u_xlat4.zzzz + u_xlat7;
					    u_xlat5 = u_xlat8 * u_xlat4.zzzz + u_xlat5;
					    u_xlat7 = textureLod(_NormalTex, u_xlat21.yx, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat21.zx, 0.0);
					    u_xlat6.w = u_xlat21.x;
					    u_xlat8 = u_xlat2.xxxx * u_xlat8;
					    u_xlat8 = u_xlat4.yyyy * u_xlat8;
					    u_xlat7 = u_xlat4.xxxx * u_xlat7;
					    u_xlat7 = u_xlat4.yyyy * u_xlat7;
					    u_xlat5 = u_xlat7 * u_xlat2.zzzz + u_xlat5;
					    u_xlat5 = u_xlat8 * u_xlat2.zzzz + u_xlat5;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat6 = u_xlat2.xxxx * u_xlat6;
					    u_xlat6 = u_xlat2.yyyy * u_xlat6;
					    u_xlat4 = u_xlat4.xxxx * u_xlat7;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat2.zzzz + u_xlat5;
					    u_xlat2 = u_xlat6 * u_xlat2.zzzz + u_xlat4;
					    u_xlat11.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.x = u_xlat2.w * 0.400000006;
					    u_xlat13.xyz = (-u_xlat11.xyz) + u_xlat3.xyz;
					    u_xlat11.xyz = u_xlat13.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat11.xyz;
					    u_xlat1.xyz = u_xlat11.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat34 = (-u_xlat3.w) * 0.5 + u_xlat1.w;
					    u_xlat34 = u_xlat34 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat13.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat13.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat36 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat4.x = u_xlat1.y * u_xlat36;
					    u_xlat15.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat15.x * u_xlat1.x + u_xlat4.x;
					    u_xlat5.z = u_xlat1.z * u_xlat15.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat15.x * u_xlat7.x + u_xlat4.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat20.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat13.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat12.x = u_xlat36 * u_xlat7.y;
					    u_xlat5.y = u_xlat15.x * u_xlat1.x + u_xlat12.x;
					    u_xlat5.x = u_xlat15.x * u_xlat7.x + u_xlat12.x;
					    u_xlat20.x = u_xlat15.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat13.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat13.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat13.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat20.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat20.zx, 0.0);
					    u_xlat5.w = u_xlat20.x;
					    u_xlat6.xyz = u_xlat13.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat14.xyz * u_xlat13.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat13.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat14.xyz = u_xlat13.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat13.yyy * u_xlat14.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat13.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat4.xyz * u_xlat13.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat14.xyz * u_xlat13.zzz + u_xlat1.xyz;
					    u_xlat3.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xy = abs(_SunDir.yx);
					    u_xlat13.xyz = u_xlat3.zxw;
					    u_xlat13.xyz = clamp(u_xlat13.xyz, 0.0, 1.0);
					    u_xlat3.x = u_xlat3.x + u_xlat3.y;
					    u_xlat14.xyz = u_xlat1.xyz * u_xlat13.xyz;
					    u_xlat1.x = u_xlat14.y + u_xlat14.x;
					    u_xlat1.x = u_xlat13.z * u_xlat1.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * 0.600000024 + u_xlat2.x;
					    u_xlat12.x = u_xlat34;
					    u_xlat12.x = clamp(u_xlat12.x, 0.0, 1.0);
					    u_xlat34 = (-u_xlat34);
					    u_xlat34 = clamp(u_xlat34, 0.0, 1.0);
					    u_xlat23 = max(u_xlat34, u_xlat12.x);
					    u_xlat23 = (-u_xlat23) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat4.xyz = u_xlat12.xxx * u_xlat2.yzx;
					    u_xlat4.xyz = u_xlat2.xyz * vec3(u_xlat23) + u_xlat4.xyz;
					    u_xlat12.xyz = vec3(u_xlat34) * u_xlat2.zxy + u_xlat4.xyz;
					    u_xlat2.xyz = (-u_xlat12.xyz) + _SnowColor.xyz;
					    u_xlat12.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat12.xyz;
					    u_xlat2.xyz = u_xlat12.xyz * _MinAmbientColor.xyz;
					    u_xlat12.xyz = (-_MinAmbientColor.xyz) * u_xlat12.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = u_xlat1.xxx * u_xlat12.xyz + u_xlat2.xyz;
					    u_xlat34 = dot(u_xlat11.xyz, u_xlat11.xyz);
					    u_xlat34 = inversesqrt(u_xlat34);
					    u_xlat2.xyz = u_xlat11.xyz * vec3(u_xlat34);
					    u_xlat2.w = (-u_xlat2.x);
					    u_xlat11.xyz = u_xlat2.xyw;
					    u_xlat11.xyz = clamp(u_xlat11.xyz, 0.0, 1.0);
					    u_xlat34 = dot(u_xlat2.xyz, _FlashDir.xyz);
					    u_xlat34 = clamp(u_xlat34, 0.0, 1.0);
					    u_xlat34 = (-u_xlat34) + 1.0;
					    u_xlat11.xy = u_xlat11.xy * u_xlat14.xy;
					    u_xlat11.x = u_xlat11.y + u_xlat11.x;
					    u_xlat11.x = u_xlat14.z * u_xlat11.z + u_xlat11.x;
					    u_xlat11.x = u_xlat11.x / u_xlat3.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat11.xyz = u_xlat2.xyz * u_xlat11.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat34 * u_xlat34;
					    u_xlat1.x = (-u_xlat34) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat1.xxx * _FlashColor.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat11.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat1 = u_xlat1 * in_COLOR0;
					    u_xlat11.xy = (-_Alpha.xz) + _Alpha.yw;
					    u_xlat11.xy = in_TEXCOORD3.yy * u_xlat11.xy + _Alpha.xz;
					    u_xlat22 = (-u_xlat11.x) + u_xlat11.y;
					    u_xlat0 = u_xlat0 * u_xlat22 + u_xlat11.x;
					    vs_COLOR0.w = u_xlat0 * u_xlat1.w;
					    vs_COLOR0.xyz = u_xlat1.xyz;
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
						vec4 unused_0_0[17];
						mat4x4 _Tex2World;
						vec4 unused_0_2[35];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_5[2];
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9;
						float _Interpolator;
						vec4 unused_0_11;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
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
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					bool u_xlatb10;
					float u_xlat15;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat5 * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat5 = (-_Size.x) + _Size.y;
					    u_xlat5 = in_TEXCOORD3.y * u_xlat5 + _Size.x;
					    u_xlat0.x = u_xlat5 * u_xlat0.x + (-u_xlat5);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat5;
					    u_xlat0.xy = u_xlat0.xx * in_TEXCOORD1.xy;
					    u_xlat10.x = _Interpolator + -1.0;
					    u_xlat10.x = -abs(u_xlat10.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat10.xxxx * u_xlat3 + u_xlat2;
					    u_xlat10.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat10.x = dot(u_xlat10.xy, u_xlat10.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + u_xlat10.x;
					    u_xlat10.x = min(u_xlat10.x, 1.0);
					    u_xlat10.x = (-u_xlat10.x) + 1.0;
					    u_xlat15 = (-u_xlat3.y) + 1.0;
					    u_xlat10.x = u_xlat15 * u_xlat10.x;
					    u_xlat10.x = u_xlat10.x * 1.39999998;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat0.xy = u_xlat10.xx * u_xlat0.xy;
					    u_xlat0.xy = u_xlat3.ww * u_xlat0.xy;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat3.xyw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat3.w = max(u_xlat3.y, _WaterLevel);
					    u_xlat10.x = (-u_xlat3.w) * 0.5 + 1.0;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat0.xy = u_xlat10.xx * u_xlat0.xy;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].zxy;
					    u_xlat1.xyw = _Tex2World[0].zxy * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].zxy * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].zxy;
					    u_xlat1.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlatb10 = 1.0<_Interpolator;
					    u_xlat1.xyz = (bool(u_xlatb10)) ? (-u_xlat1.xyz) : u_xlat1.xyz;
					    u_xlat2.z = (-unity_MatrixV[0].z);
					    u_xlat2.y = (-unity_MatrixV[2].z);
					    u_xlat2.x = unity_MatrixV[1].z;
					    u_xlat10.x = dot(u_xlat2.yzx, u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat2.zxy) * u_xlat10.xxx + u_xlat1.yzx;
					    u_xlat10.x = dot(u_xlat4.xyz, u_xlat4.xyz);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat15 = u_xlat10.x + 1.0;
					    u_xlatb10 = 2.0<u_xlat10.x;
					    u_xlat0.x = u_xlat15 * u_xlat0.x;
					    u_xlat5 = u_xlat0.y / u_xlat15;
					    u_xlat4.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat15 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat15 = inversesqrt(u_xlat15);
					    u_xlat1.xyz = vec3(u_xlat15) * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat1.xyz = vec3(u_xlat5) * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xwz;
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat2.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat0.xyz = (bool(u_xlatb10)) ? vec3(0.0, 0.0, 0.0) : u_xlat0.xyw;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = (-_Alpha.x) + _Alpha.y;
					    u_xlat0.x = in_TEXCOORD3.y * u_xlat0.x + _Alpha.x;
					    u_xlat5 = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat1 = vec4(u_xlat5) * in_COLOR0;
					    vs_COLOR0.w = u_xlat0.x * u_xlat1.w;
					    vs_COLOR0.xyz = u_xlat1.xyz;
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
						vec4 unused_0_0[17];
						mat4x4 _Tex2World;
						vec4 unused_0_2[35];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_5[2];
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9;
						float _Interpolator;
						vec4 unused_0_11;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
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
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					bool u_xlatb10;
					float u_xlat15;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat5 * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat5 = (-_Size.x) + _Size.y;
					    u_xlat5 = in_TEXCOORD3.y * u_xlat5 + _Size.x;
					    u_xlat0.x = u_xlat5 * u_xlat0.x + (-u_xlat5);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat5;
					    u_xlat0.xy = u_xlat0.xx * in_TEXCOORD1.xy;
					    u_xlat10.x = _Interpolator + -1.0;
					    u_xlat10.x = -abs(u_xlat10.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat10.xxxx * u_xlat3 + u_xlat2;
					    u_xlat10.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat10.x = dot(u_xlat10.xy, u_xlat10.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat10.x + u_xlat10.x;
					    u_xlat10.x = min(u_xlat10.x, 1.0);
					    u_xlat10.x = (-u_xlat10.x) + 1.0;
					    u_xlat15 = (-u_xlat3.y) + 1.0;
					    u_xlat10.x = u_xlat15 * u_xlat10.x;
					    u_xlat10.x = u_xlat10.x * 1.39999998;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat0.xy = u_xlat10.xx * u_xlat0.xy;
					    u_xlat0.xy = u_xlat3.ww * u_xlat0.xy;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat3.xyw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat3.w = max(u_xlat3.y, _WaterLevel);
					    u_xlat10.x = (-u_xlat3.w) * 0.5 + 1.0;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat0.xy = u_xlat10.xx * u_xlat0.xy;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].zxy;
					    u_xlat1.xyw = _Tex2World[0].zxy * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].zxy * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].zxy;
					    u_xlat1.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlatb10 = 1.0<_Interpolator;
					    u_xlat1.xyz = (bool(u_xlatb10)) ? (-u_xlat1.xyz) : u_xlat1.xyz;
					    u_xlat2.z = (-unity_MatrixV[0].z);
					    u_xlat2.y = (-unity_MatrixV[2].z);
					    u_xlat2.x = unity_MatrixV[1].z;
					    u_xlat10.x = dot(u_xlat2.yzx, u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat2.zxy) * u_xlat10.xxx + u_xlat1.yzx;
					    u_xlat10.x = dot(u_xlat4.xyz, u_xlat4.xyz);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat15 = u_xlat10.x + 1.0;
					    u_xlatb10 = 2.0<u_xlat10.x;
					    u_xlat0.x = u_xlat15 * u_xlat0.x;
					    u_xlat5 = u_xlat0.y / u_xlat15;
					    u_xlat4.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat15 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat15 = inversesqrt(u_xlat15);
					    u_xlat1.xyz = vec3(u_xlat15) * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat1.xyz = vec3(u_xlat5) * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xwz;
					    u_xlat0.xyw = u_xlat0.xxx * u_xlat2.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat0.xyz = (bool(u_xlatb10)) ? vec3(0.0, 0.0, 0.0) : u_xlat0.xyw;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = (-_Alpha.x) + _Alpha.y;
					    u_xlat0.x = in_TEXCOORD3.y * u_xlat0.x + _Alpha.x;
					    u_xlat5 = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat1 = vec4(u_xlat5) * in_COLOR0;
					    vs_COLOR0.w = u_xlat0.x * u_xlat1.w;
					    vs_COLOR0.xyz = u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_AGE_ON" }
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
						vec4 unused_0_0[17];
						mat4x4 _Tex2World;
						vec4 unused_0_2[35];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_5[2];
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9;
						float _Interpolator;
						vec4 _Lifespan;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					bool u_xlatb15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat5 * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat5 = (-_Lifespan.x) + _Lifespan.y;
					    u_xlat5 = in_TEXCOORD3.y * u_xlat5 + _Lifespan.x;
					    u_xlat10.x = (-in_TEXCOORD3.x) + _Time.y;
					    u_xlat5 = u_xlat10.x / u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat10.xy = (-_Size.xz) + _Size.yw;
					    u_xlat10.xy = in_TEXCOORD3.yy * u_xlat10.xy + _Size.xz;
					    u_xlat15 = (-u_xlat10.x) + u_xlat10.y;
					    u_xlat10.x = u_xlat5 * u_xlat15 + u_xlat10.x;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x + (-u_xlat10.x);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat10.x;
					    u_xlat0.xz = u_xlat0.xx * in_TEXCOORD1.xy;
					    u_xlat15 = _Interpolator + -1.0;
					    u_xlat15 = -abs(u_xlat15) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = vec4(u_xlat15) * u_xlat3 + u_xlat2;
					    u_xlat4.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat15 = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat15 = sqrt(u_xlat15);
					    u_xlat15 = u_xlat15 + u_xlat15;
					    u_xlat15 = min(u_xlat15, 1.0);
					    u_xlat15 = (-u_xlat15) + 1.0;
					    u_xlat16 = (-u_xlat3.y) + 1.0;
					    u_xlat15 = u_xlat15 * u_xlat16;
					    u_xlat15 = u_xlat15 * 1.39999998;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat0.xz = vec2(u_xlat15) * u_xlat0.xz;
					    u_xlat0.xz = u_xlat3.ww * u_xlat0.xz;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat3.xyw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat3.w = max(u_xlat3.y, _WaterLevel);
					    u_xlat15 = (-u_xlat3.w) * 0.5 + 1.0;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat0.xz = vec2(u_xlat15) * u_xlat0.xz;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].zxy;
					    u_xlat1.xyw = _Tex2World[0].zxy * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].zxy * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].zxy;
					    u_xlat1.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlatb15 = 1.0<_Interpolator;
					    u_xlat1.xyz = (bool(u_xlatb15)) ? (-u_xlat1.xyz) : u_xlat1.xyz;
					    u_xlat2.z = (-unity_MatrixV[0].z);
					    u_xlat2.y = (-unity_MatrixV[2].z);
					    u_xlat2.x = unity_MatrixV[1].z;
					    u_xlat15 = dot(u_xlat2.yzx, u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat2.zxy) * vec3(u_xlat15) + u_xlat1.yzx;
					    u_xlat15 = dot(u_xlat4.xyz, u_xlat4.xyz);
					    u_xlat15 = sqrt(u_xlat15);
					    u_xlat16 = u_xlat15 + 1.0;
					    u_xlatb15 = 2.0<u_xlat15;
					    u_xlat0.x = u_xlat0.x * u_xlat16;
					    u_xlat10.x = u_xlat0.z / u_xlat16;
					    u_xlat4.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat16 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat16 = inversesqrt(u_xlat16);
					    u_xlat1.xyz = vec3(u_xlat16) * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat1.xyz = u_xlat10.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xwz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat0.xzw = (bool(u_xlatb15)) ? vec3(0.0, 0.0, 0.0) : u_xlat1.xyz;
					    u_xlat1 = u_xlat0.zzzz * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat0.xz = (-_Alpha.xz) + _Alpha.yw;
					    u_xlat0.xz = in_TEXCOORD3.yy * u_xlat0.xz + _Alpha.xz;
					    u_xlat10.x = (-u_xlat0.x) + u_xlat0.z;
					    u_xlat0.x = u_xlat5 * u_xlat10.x + u_xlat0.x;
					    u_xlat5 = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat1 = vec4(u_xlat5) * in_COLOR0;
					    vs_COLOR0.w = u_xlat0.x * u_xlat1.w;
					    vs_COLOR0.xyz = u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_AGE_ON" }
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
						vec4 unused_0_0[17];
						mat4x4 _Tex2World;
						vec4 unused_0_2[35];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_5[2];
						float _AAFactor;
						vec4 unused_0_7[4];
						vec4 _MainTex_ST;
						vec4 unused_0_9;
						float _Interpolator;
						vec4 _Lifespan;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					bool u_xlatb15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat5 = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat5 * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat5 = (-_Lifespan.x) + _Lifespan.y;
					    u_xlat5 = in_TEXCOORD3.y * u_xlat5 + _Lifespan.x;
					    u_xlat10.x = (-in_TEXCOORD3.x) + _Time.y;
					    u_xlat5 = u_xlat10.x / u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat10.xy = (-_Size.xz) + _Size.yw;
					    u_xlat10.xy = in_TEXCOORD3.yy * u_xlat10.xy + _Size.xz;
					    u_xlat15 = (-u_xlat10.x) + u_xlat10.y;
					    u_xlat10.x = u_xlat5 * u_xlat15 + u_xlat10.x;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x + (-u_xlat10.x);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat10.x;
					    u_xlat0.xz = u_xlat0.xx * in_TEXCOORD1.xy;
					    u_xlat15 = _Interpolator + -1.0;
					    u_xlat15 = -abs(u_xlat15) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = vec4(u_xlat15) * u_xlat3 + u_xlat2;
					    u_xlat4.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat15 = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat15 = sqrt(u_xlat15);
					    u_xlat15 = u_xlat15 + u_xlat15;
					    u_xlat15 = min(u_xlat15, 1.0);
					    u_xlat15 = (-u_xlat15) + 1.0;
					    u_xlat16 = (-u_xlat3.y) + 1.0;
					    u_xlat15 = u_xlat15 * u_xlat16;
					    u_xlat15 = u_xlat15 * 1.39999998;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat0.xz = vec2(u_xlat15) * u_xlat0.xz;
					    u_xlat0.xz = u_xlat3.ww * u_xlat0.xz;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat3.xyw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat3.w = max(u_xlat3.y, _WaterLevel);
					    u_xlat15 = (-u_xlat3.w) * 0.5 + 1.0;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat0.xz = vec2(u_xlat15) * u_xlat0.xz;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].zxy;
					    u_xlat1.xyw = _Tex2World[0].zxy * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].zxy * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].zxy;
					    u_xlat1.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlatb15 = 1.0<_Interpolator;
					    u_xlat1.xyz = (bool(u_xlatb15)) ? (-u_xlat1.xyz) : u_xlat1.xyz;
					    u_xlat2.z = (-unity_MatrixV[0].z);
					    u_xlat2.y = (-unity_MatrixV[2].z);
					    u_xlat2.x = unity_MatrixV[1].z;
					    u_xlat15 = dot(u_xlat2.yzx, u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat2.zxy) * vec3(u_xlat15) + u_xlat1.yzx;
					    u_xlat15 = dot(u_xlat4.xyz, u_xlat4.xyz);
					    u_xlat15 = sqrt(u_xlat15);
					    u_xlat16 = u_xlat15 + 1.0;
					    u_xlatb15 = 2.0<u_xlat15;
					    u_xlat0.x = u_xlat0.x * u_xlat16;
					    u_xlat10.x = u_xlat0.z / u_xlat16;
					    u_xlat4.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat16 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat16 = inversesqrt(u_xlat16);
					    u_xlat1.xyz = vec3(u_xlat16) * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat1.zxy;
					    u_xlat2.xyz = u_xlat1.yzx * u_xlat2.yzx + (-u_xlat4.xyz);
					    u_xlat1.xyz = u_xlat10.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xwz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat0.xzw = (bool(u_xlatb15)) ? vec3(0.0, 0.0, 0.0) : u_xlat1.xyz;
					    u_xlat1 = u_xlat0.zzzz * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat0.xz = (-_Alpha.xz) + _Alpha.yw;
					    u_xlat0.xz = in_TEXCOORD3.yy * u_xlat0.xz + _Alpha.xz;
					    u_xlat10.x = (-u_xlat0.x) + u_xlat0.z;
					    u_xlat0.x = u_xlat5 * u_xlat10.x + u_xlat0.x;
					    u_xlat5 = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat1 = vec4(u_xlat5) * in_COLOR0;
					    vs_COLOR0.w = u_xlat0.x * u_xlat1.w;
					    vs_COLOR0.xyz = u_xlat1.xyz;
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
						vec4 unused_0_6[9];
						mat4x4 _Tex2World;
						vec4 unused_0_8[12];
						vec3 _SunDir;
						vec4 unused_0_10[7];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 unused_0_14;
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
						vec4 unused_0_25[4];
						vec4 _MainTex_ST;
						vec4 unused_0_27;
						float _Interpolator;
						vec4 unused_0_29;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
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
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec3 u_xlat8;
					vec4 u_xlat9;
					vec3 u_xlat11;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat19;
					vec3 u_xlat21;
					vec2 u_xlat22;
					float u_xlat33;
					bool u_xlatb33;
					float u_xlat34;
					float u_xlat35;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat11.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat11.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat11.x = (-_Size.x) + _Size.y;
					    u_xlat11.x = in_TEXCOORD3.y * u_xlat11.x + _Size.x;
					    u_xlat22.x = u_xlat11.x * u_xlat0.x + (-u_xlat11.x);
					    u_xlat11.x = _ScreenSpaceSize * u_xlat22.x + u_xlat11.x;
					    u_xlat11.xy = u_xlat11.xx * in_TEXCOORD1.xy;
					    u_xlat33 = _Interpolator + -1.0;
					    u_xlat33 = -abs(u_xlat33) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = vec4(u_xlat33) * u_xlat3 + u_xlat2;
					    u_xlat4.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat33 = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat33 = sqrt(u_xlat33);
					    u_xlat33 = u_xlat33 + u_xlat33;
					    u_xlat33 = min(u_xlat33, 1.0);
					    u_xlat33 = (-u_xlat33) + 1.0;
					    u_xlat34 = (-u_xlat3.y) + 1.0;
					    u_xlat33 = u_xlat33 * u_xlat34;
					    u_xlat33 = u_xlat33 * 1.39999998;
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat33) * u_xlat11.xy;
					    u_xlat11.xy = u_xlat3.ww * u_xlat11.xy;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat3.xyw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat3.w = max(u_xlat3.y, _WaterLevel);
					    u_xlat33 = (-u_xlat3.w) * 0.5 + 1.0;
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat33) * u_xlat11.xy;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].zxy;
					    u_xlat1.xyw = _Tex2World[0].zxy * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].zxy * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].zxy;
					    u_xlat1.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlatb33 = 1.0<_Interpolator;
					    u_xlat1.xyz = (bool(u_xlatb33)) ? (-u_xlat1.xyz) : u_xlat1.xyz;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[2].z;
					    u_xlat4.xz = (-u_xlat2.xy);
					    u_xlat2.xy = u_xlat2.xy / unity_MatrixV[1].zz;
					    u_xlat4.y = unity_MatrixV[1].z;
					    u_xlat33 = dot(u_xlat4.zxy, u_xlat1.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) * vec3(u_xlat33) + u_xlat1.yzx;
					    u_xlat33 = dot(u_xlat5.xyz, u_xlat5.xyz);
					    u_xlat33 = sqrt(u_xlat33);
					    u_xlat34 = u_xlat33 + 1.0;
					    u_xlatb33 = 2.0<u_xlat33;
					    u_xlat11.x = u_xlat11.x * u_xlat34;
					    u_xlat22.x = u_xlat11.y / u_xlat34;
					    u_xlat5.xyz = u_xlat1.xyz * u_xlat4.yzx;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat4.zxy + (-u_xlat5.xyz);
					    u_xlat34 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat34 = inversesqrt(u_xlat34);
					    u_xlat1.xyz = vec3(u_xlat34) * u_xlat1.xyz;
					    u_xlat5.xyz = u_xlat4.yzx * u_xlat1.zxy;
					    u_xlat5.xyz = u_xlat1.yzx * u_xlat4.zxy + (-u_xlat5.xyz);
					    u_xlat1.xyz = u_xlat22.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xwz;
					    u_xlat3.xyz = u_xlat11.xxx * u_xlat5.xyz;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat1.xyw = (bool(u_xlatb33)) ? vec3(0.0, 0.0, 0.0) : u_xlat1.xzy;
					    u_xlat11.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat22.xy = u_xlat2.xy * u_xlat11.xx + u_xlat1.xy;
					    u_xlat0.x = u_xlat11.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat22.xy, u_xlat22.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat11.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat11.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = _WaterLevel * 2.0 + (-u_xlat1.w);
					    u_xlat0 = u_xlat0.xxxx * unity_MatrixVP[1];
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
					    u_xlat33 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat35 = u_xlat0.y * u_xlat33;
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat0.x + u_xlat35;
					    u_xlat6.z = u_xlat0.z * u_xlat5.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat3.yyy * u_xlat7.xyz;
					    u_xlat8.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat35;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat21.yz = u_xlat6.yx;
					    u_xlat9.xyz = u_xlat2.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat3.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat3.zzz * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat9.xyz;
					    u_xlat11.x = u_xlat33 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat0.x + u_xlat11.x;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat11.x;
					    u_xlat21.x = u_xlat5.y * u_xlat8.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat3.xxx * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat3.zzz + u_xlat0.xyz;
					    u_xlat5 = textureLod(_NormalTex, u_xlat21.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat21.zx, 0.0);
					    u_xlat6.w = u_xlat21.x;
					    u_xlat7.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat3.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat7.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat2.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat0.xyz) + (-u_xlat4.xyz);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat33 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat33 = u_xlat33 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat34 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat35 = u_xlat0.y * u_xlat34;
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat35;
					    u_xlat4.z = u_xlat0.z * u_xlat3.y;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat35;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat19.yz = u_xlat4.yx;
					    u_xlat7.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.yyy * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.zzz * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat7.xyz;
					    u_xlat11.x = u_xlat34 * u_xlat6.y;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat11.x;
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat11.x;
					    u_xlat19.x = u_xlat3.y * u_xlat6.z;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat1.yyy * u_xlat0.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat2.zzz + u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat4.w = u_xlat19.x;
					    u_xlat5.xyz = u_xlat1.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat13.xyz = u_xlat1.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat1.yyy * u_xlat13.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat1.xyw = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat1.xyw * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat1.y = abs(_SunDir.y);
					    u_xlat1.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat1.xyz = u_xlat1.xyz;
					    u_xlat1.xyz = clamp(u_xlat1.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat1.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat1.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.200000003;
					    u_xlat11.x = u_xlat33;
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat33 = (-u_xlat33);
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat22.x = max(u_xlat33, u_xlat11.x);
					    u_xlat22.x = (-u_xlat22.x) + 1.0;
					    u_xlat1.x = _Year;
					    u_xlat1.y = 0.0;
					    u_xlat1 = textureLod(_GrassTex, u_xlat1.xy, 0.0);
					    u_xlat2.xyz = u_xlat11.xxx * u_xlat1.yzx;
					    u_xlat2.xyz = u_xlat1.xyz * u_xlat22.xxx + u_xlat2.xyz;
					    u_xlat11.xyz = vec3(u_xlat33) * u_xlat1.zxy + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat11.xyz) + _SnowColor.xyz;
					    u_xlat11.xyz = vec3(_SnowAmount) * u_xlat1.xyz + u_xlat11.xyz;
					    u_xlat1.xyz = u_xlat11.xyz * _MinAmbientColor.xyz;
					    u_xlat11.xyz = (-_MinAmbientColor.xyz) * u_xlat11.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat11.xyz + u_xlat1.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * in_COLOR0;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    u_xlat0.x = (-_Alpha.x) + _Alpha.y;
					    u_xlat0.x = in_TEXCOORD3.y * u_xlat0.x + _Alpha.x;
					    vs_COLOR0.w = u_xlat0.x * u_xlat0.w;
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
						vec4 unused_0_6[9];
						mat4x4 _Tex2World;
						vec4 unused_0_8[12];
						vec3 _SunDir;
						vec4 unused_0_10[7];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 unused_0_14;
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
						vec4 unused_0_25[4];
						vec4 _MainTex_ST;
						vec4 unused_0_27;
						float _Interpolator;
						vec4 unused_0_29;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
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
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec3 u_xlat8;
					vec4 u_xlat9;
					vec3 u_xlat11;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat19;
					vec3 u_xlat21;
					vec2 u_xlat22;
					float u_xlat33;
					bool u_xlatb33;
					float u_xlat34;
					float u_xlat35;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat11.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat11.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat11.x = (-_Size.x) + _Size.y;
					    u_xlat11.x = in_TEXCOORD3.y * u_xlat11.x + _Size.x;
					    u_xlat22.x = u_xlat11.x * u_xlat0.x + (-u_xlat11.x);
					    u_xlat11.x = _ScreenSpaceSize * u_xlat22.x + u_xlat11.x;
					    u_xlat11.xy = u_xlat11.xx * in_TEXCOORD1.xy;
					    u_xlat33 = _Interpolator + -1.0;
					    u_xlat33 = -abs(u_xlat33) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = vec4(u_xlat33) * u_xlat3 + u_xlat2;
					    u_xlat4.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat33 = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat33 = sqrt(u_xlat33);
					    u_xlat33 = u_xlat33 + u_xlat33;
					    u_xlat33 = min(u_xlat33, 1.0);
					    u_xlat33 = (-u_xlat33) + 1.0;
					    u_xlat34 = (-u_xlat3.y) + 1.0;
					    u_xlat33 = u_xlat33 * u_xlat34;
					    u_xlat33 = u_xlat33 * 1.39999998;
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat33) * u_xlat11.xy;
					    u_xlat11.xy = u_xlat3.ww * u_xlat11.xy;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat3.xyw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat3.w = max(u_xlat3.y, _WaterLevel);
					    u_xlat33 = (-u_xlat3.w) * 0.5 + 1.0;
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat33) * u_xlat11.xy;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].zxy;
					    u_xlat1.xyw = _Tex2World[0].zxy * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].zxy * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].zxy;
					    u_xlat1.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlatb33 = 1.0<_Interpolator;
					    u_xlat1.xyz = (bool(u_xlatb33)) ? (-u_xlat1.xyz) : u_xlat1.xyz;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[2].z;
					    u_xlat4.xz = (-u_xlat2.xy);
					    u_xlat2.xy = u_xlat2.xy / unity_MatrixV[1].zz;
					    u_xlat4.y = unity_MatrixV[1].z;
					    u_xlat33 = dot(u_xlat4.zxy, u_xlat1.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) * vec3(u_xlat33) + u_xlat1.yzx;
					    u_xlat33 = dot(u_xlat5.xyz, u_xlat5.xyz);
					    u_xlat33 = sqrt(u_xlat33);
					    u_xlat34 = u_xlat33 + 1.0;
					    u_xlatb33 = 2.0<u_xlat33;
					    u_xlat11.x = u_xlat11.x * u_xlat34;
					    u_xlat22.x = u_xlat11.y / u_xlat34;
					    u_xlat5.xyz = u_xlat1.xyz * u_xlat4.yzx;
					    u_xlat1.xyz = u_xlat1.zxy * u_xlat4.zxy + (-u_xlat5.xyz);
					    u_xlat34 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat34 = inversesqrt(u_xlat34);
					    u_xlat1.xyz = vec3(u_xlat34) * u_xlat1.xyz;
					    u_xlat5.xyz = u_xlat4.yzx * u_xlat1.zxy;
					    u_xlat5.xyz = u_xlat1.yzx * u_xlat4.zxy + (-u_xlat5.xyz);
					    u_xlat1.xyz = u_xlat22.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xwz;
					    u_xlat3.xyz = u_xlat11.xxx * u_xlat5.xyz;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xyz;
					    u_xlat1.xyw = (bool(u_xlatb33)) ? vec3(0.0, 0.0, 0.0) : u_xlat1.xzy;
					    u_xlat11.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat22.xy = u_xlat2.xy * u_xlat11.xx + u_xlat1.xy;
					    u_xlat0.x = u_xlat11.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat22.xy, u_xlat22.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat11.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat11.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = _WaterLevel * 2.0 + (-u_xlat1.w);
					    u_xlat0 = u_xlat0.xxxx * unity_MatrixVP[1];
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
					    u_xlat33 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat35 = u_xlat0.y * u_xlat33;
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat0.x + u_xlat35;
					    u_xlat6.z = u_xlat0.z * u_xlat5.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat3.yyy * u_xlat7.xyz;
					    u_xlat8.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat35;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat21.yz = u_xlat6.yx;
					    u_xlat9.xyz = u_xlat2.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat3.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat3.zzz * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat9.xyz;
					    u_xlat11.x = u_xlat33 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat0.x + u_xlat11.x;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat11.x;
					    u_xlat21.x = u_xlat5.y * u_xlat8.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat3.xxx * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat3.zzz + u_xlat0.xyz;
					    u_xlat5 = textureLod(_NormalTex, u_xlat21.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat21.zx, 0.0);
					    u_xlat6.w = u_xlat21.x;
					    u_xlat7.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat3.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat7.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat2.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat0.xyz) + (-u_xlat4.xyz);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat33 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat33 = u_xlat33 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat34 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat35 = u_xlat0.y * u_xlat34;
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat35;
					    u_xlat4.z = u_xlat0.z * u_xlat3.y;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat35;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat19.yz = u_xlat4.yx;
					    u_xlat7.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.yyy * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.zzz * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat7.xyz;
					    u_xlat11.x = u_xlat34 * u_xlat6.y;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat11.x;
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat11.x;
					    u_xlat19.x = u_xlat3.y * u_xlat6.z;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat1.yyy * u_xlat0.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat2.zzz + u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat4.w = u_xlat19.x;
					    u_xlat5.xyz = u_xlat1.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat13.xyz = u_xlat1.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat1.yyy * u_xlat13.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat1.xyw = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat1.xyw * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat13.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat1.y = abs(_SunDir.y);
					    u_xlat1.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat1.xyz = u_xlat1.xyz;
					    u_xlat1.xyz = clamp(u_xlat1.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat1.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat1.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.200000003;
					    u_xlat11.x = u_xlat33;
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat33 = (-u_xlat33);
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat22.x = max(u_xlat33, u_xlat11.x);
					    u_xlat22.x = (-u_xlat22.x) + 1.0;
					    u_xlat1.x = _Year;
					    u_xlat1.y = 0.0;
					    u_xlat1 = textureLod(_GrassTex, u_xlat1.xy, 0.0);
					    u_xlat2.xyz = u_xlat11.xxx * u_xlat1.yzx;
					    u_xlat2.xyz = u_xlat1.xyz * u_xlat22.xxx + u_xlat2.xyz;
					    u_xlat11.xyz = vec3(u_xlat33) * u_xlat1.zxy + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat11.xyz) + _SnowColor.xyz;
					    u_xlat11.xyz = vec3(_SnowAmount) * u_xlat1.xyz + u_xlat11.xyz;
					    u_xlat1.xyz = u_xlat11.xyz * _MinAmbientColor.xyz;
					    u_xlat11.xyz = (-_MinAmbientColor.xyz) * u_xlat11.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat11.xyz + u_xlat1.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * in_COLOR0;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    u_xlat0.x = (-_Alpha.x) + _Alpha.y;
					    u_xlat0.x = in_TEXCOORD3.y * u_xlat0.x + _Alpha.x;
					    vs_COLOR0.w = u_xlat0.x * u_xlat0.w;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_AGE_ON" }
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
						vec4 unused_0_6[9];
						mat4x4 _Tex2World;
						vec4 unused_0_8[12];
						vec3 _SunDir;
						vec4 unused_0_10[7];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 unused_0_14;
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
						vec4 unused_0_25[4];
						vec4 _MainTex_ST;
						vec4 unused_0_27;
						float _Interpolator;
						vec4 _Lifespan;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					float u_xlat0;
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
					vec3 u_xlat12;
					bool u_xlatb12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat19;
					vec3 u_xlat21;
					float u_xlat22;
					float u_xlat33;
					float u_xlat35;
					float u_xlat36;
					void main()
					{
					    u_xlat0 = (-_Lifespan.x) + _Lifespan.y;
					    u_xlat0 = in_TEXCOORD3.y * u_xlat0 + _Lifespan.x;
					    u_xlat11.x = (-in_TEXCOORD3.x) + _Time.y;
					    u_xlat0 = u_xlat11.x / u_xlat0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat11.xy = (-_Size.xz) + _Size.yw;
					    u_xlat11.xy = in_TEXCOORD3.yy * u_xlat11.xy + _Size.xz;
					    u_xlat22 = (-u_xlat11.x) + u_xlat11.y;
					    u_xlat11.x = u_xlat0 * u_xlat22 + u_xlat11.x;
					    u_xlat22 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat22 = u_xlat22 + u_xlat22;
					    u_xlat33 = _AAFactor + 1.0;
					    u_xlat22 = u_xlat33 * u_xlat22;
					    u_xlat22 = u_xlat22 * _LineWidth;
					    u_xlat33 = u_xlat11.x * u_xlat22 + (-u_xlat11.x);
					    u_xlat11.x = _ScreenSpaceSize * u_xlat33 + u_xlat11.x;
					    u_xlat11.xz = u_xlat11.xx * in_TEXCOORD1.xy;
					    u_xlat1.x = _Interpolator + -1.0;
					    u_xlat1.x = -abs(u_xlat1.x) + 1.0;
					    u_xlat2 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat4 = u_xlat2 + (-u_xlat3);
					    u_xlat1 = u_xlat1.xxxx * u_xlat4 + u_xlat3;
					    u_xlat4.xy = u_xlat1.xz + vec2(-0.5, -0.5);
					    u_xlat35 = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat35 = sqrt(u_xlat35);
					    u_xlat35 = u_xlat35 + u_xlat35;
					    u_xlat35 = min(u_xlat35, 1.0);
					    u_xlat35 = (-u_xlat35) + 1.0;
					    u_xlat36 = (-u_xlat1.y) + 1.0;
					    u_xlat35 = u_xlat35 * u_xlat36;
					    u_xlat35 = u_xlat35 * 1.39999998;
					    u_xlat35 = clamp(u_xlat35, 0.0, 1.0);
					    u_xlat11.xz = u_xlat11.xz * vec2(u_xlat35);
					    u_xlat11.xz = u_xlat1.ww * u_xlat11.xz;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].xyz;
					    u_xlat1.xyw = _Tex2World[0].xyz * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].xyz * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].xyz;
					    u_xlat1.w = max(u_xlat1.y, _WaterLevel);
					    u_xlat12.x = (-u_xlat1.w) * 0.5 + 1.0;
					    u_xlat12.x = clamp(u_xlat12.x, 0.0, 1.0);
					    u_xlat11.xz = u_xlat11.xz * u_xlat12.xx;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].zxy;
					    u_xlat3.xyw = _Tex2World[0].zxy * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].zxy * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat2.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlatb12 = 1.0<_Interpolator;
					    u_xlat2.xyz = (bool(u_xlatb12)) ? (-u_xlat2.xyz) : u_xlat2.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[2].z;
					    u_xlat4.xz = (-u_xlat3.xy);
					    u_xlat3.xy = u_xlat3.xy / unity_MatrixV[1].zz;
					    u_xlat4.y = unity_MatrixV[1].z;
					    u_xlat12.x = dot(u_xlat4.zxy, u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) * u_xlat12.xxx + u_xlat2.yzx;
					    u_xlat12.x = dot(u_xlat5.xyz, u_xlat5.xyz);
					    u_xlat12.x = sqrt(u_xlat12.x);
					    u_xlat35 = u_xlat12.x + 1.0;
					    u_xlatb12 = 2.0<u_xlat12.x;
					    u_xlat11.x = u_xlat11.x * u_xlat35;
					    u_xlat33 = u_xlat11.z / u_xlat35;
					    u_xlat5.xyz = u_xlat2.xyz * u_xlat4.yzx;
					    u_xlat2.xyz = u_xlat2.zxy * u_xlat4.zxy + (-u_xlat5.xyz);
					    u_xlat35 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat35 = inversesqrt(u_xlat35);
					    u_xlat2.xyz = vec3(u_xlat35) * u_xlat2.xyz;
					    u_xlat5.xyz = u_xlat4.yzx * u_xlat2.zxy;
					    u_xlat5.xyz = u_xlat2.yzx * u_xlat4.zxy + (-u_xlat5.xyz);
					    u_xlat2.xyz = vec3(u_xlat33) * u_xlat2.xyz;
					    u_xlat1.xzw = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xwz;
					    u_xlat2.xyz = u_xlat11.xxx * u_xlat5.xyz;
					    u_xlat1.xzw = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xzw;
					    u_xlat1.xyw = (bool(u_xlatb12)) ? vec3(0.0, 0.0, 0.0) : u_xlat1.xwz;
					    u_xlat11.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat2.xy = u_xlat3.xy * u_xlat11.xx + u_xlat1.xy;
					    u_xlat11.x = u_xlat11.x / u_xlat22;
					    vs_TEXCOORD1.y = u_xlat11.x + 0.5;
					    u_xlat11.x = dot(u_xlat2.xy, u_xlat2.xy);
					    u_xlat11.x = sqrt(u_xlat11.x);
					    u_xlat11.x = u_xlat11.x + (-_FogMinRad);
					    u_xlat22 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat11.x / u_xlat22;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat11.x = _WaterLevel * 2.0 + (-u_xlat1.w);
					    u_xlat2 = u_xlat11.xxxx * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat2.y = 1.0;
					    u_xlat1.xyz = u_xlat1.xwy + u_xlat2.xyz;
					    u_xlat11.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat11.xyz = max(u_xlat11.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat11.xyz = min(u_xlat11.xyz, u_xlat2.xyz);
					    u_xlat11.xyz = u_xlat11.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat11.xyz);
					    u_xlat11.xyz = floor(u_xlat11.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat35 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat11.y * u_xlat35;
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat11.x + u_xlat36;
					    u_xlat6.z = u_xlat11.z * u_xlat5.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat3.yyy * u_xlat7.xyz;
					    u_xlat8.xyz = u_xlat11.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat36;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat21.yz = u_xlat6.yx;
					    u_xlat9.xyz = u_xlat2.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat3.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat3.zzz * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat9.xyz;
					    u_xlat22 = u_xlat35 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat11.x + u_xlat22;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat22;
					    u_xlat21.x = u_xlat5.y * u_xlat8.z;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat11.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat11.xyz = u_xlat2.yyy * u_xlat11.xyz;
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat3.zzz + u_xlat7.xyz;
					    u_xlat11.xyz = u_xlat11.xyz * u_xlat3.zzz + u_xlat5.xyz;
					    u_xlat5 = textureLod(_NormalTex, u_xlat21.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat21.zx, 0.0);
					    u_xlat6.w = u_xlat21.x;
					    u_xlat7.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat3.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat11.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat11.xyz;
					    u_xlat11.xyz = u_xlat7.xyz * u_xlat2.zzz + u_xlat11.xyz;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat2.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat11.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat11.xyz;
					    u_xlat11.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat11.xyz;
					    u_xlat11.xyz = u_xlat11.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat11.xyz) + (-u_xlat4.xyz);
					    u_xlat11.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat11.xyz;
					    u_xlat11.xyz = u_xlat11.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat11.xyz = max(u_xlat11.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat11.xyz = min(u_xlat11.xyz, u_xlat2.xyz);
					    u_xlat1.x = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat1.x = u_xlat1.x * 0.25;
					    u_xlat11.xyz = u_xlat11.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat12.xyz = fract(u_xlat11.xyz);
					    u_xlat11.xyz = floor(u_xlat11.xyz);
					    u_xlat2.xyz = (-u_xlat12.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat35 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat3.x = u_xlat11.y * u_xlat35;
					    u_xlat14.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.y = u_xlat14.x * u_xlat11.x + u_xlat3.x;
					    u_xlat4.z = u_xlat11.z * u_xlat14.y;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat11.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat14.x * u_xlat6.x + u_xlat3.x;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat19.yz = u_xlat4.yx;
					    u_xlat7.xyz = u_xlat12.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.yyy * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.zzz * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat7.xyz;
					    u_xlat22 = u_xlat35 * u_xlat6.y;
					    u_xlat4.y = u_xlat14.x * u_xlat11.x + u_xlat22;
					    u_xlat4.x = u_xlat14.x * u_xlat6.x + u_xlat22;
					    u_xlat19.x = u_xlat14.y * u_xlat6.z;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat11.xyz = u_xlat12.xxx * u_xlat6.xyz;
					    u_xlat11.xyz = u_xlat12.yyy * u_xlat11.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat12.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat2.zzz + u_xlat5.xyz;
					    u_xlat11.xyz = u_xlat11.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat4.w = u_xlat19.x;
					    u_xlat5.xyz = u_xlat12.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat11.xyz = u_xlat13.xyz * u_xlat12.zzz + u_xlat11.xyz;
					    u_xlat11.xyz = u_xlat5.xyz * u_xlat12.zzz + u_xlat11.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat13.xyz = u_xlat12.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat12.yyy * u_xlat13.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat12.yyy * u_xlat3.xyz;
					    u_xlat11.xyz = u_xlat3.xyz * u_xlat12.zzz + u_xlat11.xyz;
					    u_xlat11.xyz = u_xlat13.xyz * u_xlat12.zzz + u_xlat11.xyz;
					    u_xlat2.y = abs(_SunDir.y);
					    u_xlat2.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xyz = u_xlat2.xyz;
					    u_xlat2.xyz = clamp(u_xlat2.xyz, 0.0, 1.0);
					    u_xlat11.xy = u_xlat11.xy * u_xlat2.xy;
					    u_xlat11.x = u_xlat11.y + u_xlat11.x;
					    u_xlat11.x = u_xlat2.z * u_xlat11.z + u_xlat11.x;
					    u_xlat11.x = u_xlat11.x * 0.600000024 + 0.200000003;
					    u_xlat22 = u_xlat1.x;
					    u_xlat22 = clamp(u_xlat22, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x);
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat33 = max(u_xlat22, u_xlat1.x);
					    u_xlat33 = (-u_xlat33) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat12.xyz = vec3(u_xlat22) * u_xlat2.yzx;
					    u_xlat12.xyz = u_xlat2.xyz * vec3(u_xlat33) + u_xlat12.xyz;
					    u_xlat1.xyz = u_xlat1.xxx * u_xlat2.zxy + u_xlat12.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = u_xlat11.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat1 = u_xlat1 * in_COLOR0;
					    u_xlat11.xy = (-_Alpha.xz) + _Alpha.yw;
					    u_xlat11.xy = in_TEXCOORD3.yy * u_xlat11.xy + _Alpha.xz;
					    u_xlat22 = (-u_xlat11.x) + u_xlat11.y;
					    u_xlat0 = u_xlat0 * u_xlat22 + u_xlat11.x;
					    vs_COLOR0.w = u_xlat0 * u_xlat1.w;
					    vs_COLOR0.xyz = u_xlat1.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_AGE_ON" }
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
						vec4 unused_0_6[9];
						mat4x4 _Tex2World;
						vec4 unused_0_8[12];
						vec3 _SunDir;
						vec4 unused_0_10[7];
						float _Year;
						vec4 unused_0_12[7];
						vec4 _SnowColor;
						vec4 unused_0_14;
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
						vec4 unused_0_25[4];
						vec4 _MainTex_ST;
						vec4 unused_0_27;
						float _Interpolator;
						vec4 _Lifespan;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 _Alpha;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[5];
						vec4 _ScreenParams;
						vec4 unused_1_3;
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _PosTex0;
					uniform  sampler2D _PosTex1;
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					float u_xlat0;
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
					vec3 u_xlat12;
					bool u_xlatb12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat19;
					vec3 u_xlat21;
					float u_xlat22;
					float u_xlat33;
					float u_xlat35;
					float u_xlat36;
					void main()
					{
					    u_xlat0 = (-_Lifespan.x) + _Lifespan.y;
					    u_xlat0 = in_TEXCOORD3.y * u_xlat0 + _Lifespan.x;
					    u_xlat11.x = (-in_TEXCOORD3.x) + _Time.y;
					    u_xlat0 = u_xlat11.x / u_xlat0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat11.xy = (-_Size.xz) + _Size.yw;
					    u_xlat11.xy = in_TEXCOORD3.yy * u_xlat11.xy + _Size.xz;
					    u_xlat22 = (-u_xlat11.x) + u_xlat11.y;
					    u_xlat11.x = u_xlat0 * u_xlat22 + u_xlat11.x;
					    u_xlat22 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat22 = u_xlat22 + u_xlat22;
					    u_xlat33 = _AAFactor + 1.0;
					    u_xlat22 = u_xlat33 * u_xlat22;
					    u_xlat22 = u_xlat22 * _LineWidth;
					    u_xlat33 = u_xlat11.x * u_xlat22 + (-u_xlat11.x);
					    u_xlat11.x = _ScreenSpaceSize * u_xlat33 + u_xlat11.x;
					    u_xlat11.xz = u_xlat11.xx * in_TEXCOORD1.xy;
					    u_xlat1.x = _Interpolator + -1.0;
					    u_xlat1.x = -abs(u_xlat1.x) + 1.0;
					    u_xlat2 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat4 = u_xlat2 + (-u_xlat3);
					    u_xlat1 = u_xlat1.xxxx * u_xlat4 + u_xlat3;
					    u_xlat4.xy = u_xlat1.xz + vec2(-0.5, -0.5);
					    u_xlat35 = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat35 = sqrt(u_xlat35);
					    u_xlat35 = u_xlat35 + u_xlat35;
					    u_xlat35 = min(u_xlat35, 1.0);
					    u_xlat35 = (-u_xlat35) + 1.0;
					    u_xlat36 = (-u_xlat1.y) + 1.0;
					    u_xlat35 = u_xlat35 * u_xlat36;
					    u_xlat35 = u_xlat35 * 1.39999998;
					    u_xlat35 = clamp(u_xlat35, 0.0, 1.0);
					    u_xlat11.xz = u_xlat11.xz * vec2(u_xlat35);
					    u_xlat11.xz = u_xlat1.ww * u_xlat11.xz;
					    u_xlat4.xyz = u_xlat1.yyy * _Tex2World[1].xyz;
					    u_xlat1.xyw = _Tex2World[0].xyz * u_xlat1.xxx + u_xlat4.xyz;
					    u_xlat1.xyz = _Tex2World[2].xyz * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat1.xyz = u_xlat1.xyz + _Tex2World[3].xyz;
					    u_xlat1.w = max(u_xlat1.y, _WaterLevel);
					    u_xlat12.x = (-u_xlat1.w) * 0.5 + 1.0;
					    u_xlat12.x = clamp(u_xlat12.x, 0.0, 1.0);
					    u_xlat11.xz = u_xlat11.xz * u_xlat12.xx;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].zxy;
					    u_xlat3.xyw = _Tex2World[0].zxy * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].zxy * u_xlat3.zzz + u_xlat3.xyw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].zxy;
					    u_xlat4.xyz = u_xlat2.yyy * _Tex2World[1].zxy;
					    u_xlat2.xyw = _Tex2World[0].zxy * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = _Tex2World[2].zxy * u_xlat2.zzz + u_xlat2.xyw;
					    u_xlat2.xyz = u_xlat2.xyz + _Tex2World[3].zxy;
					    u_xlat2.xyz = (-u_xlat2.xyz) + u_xlat3.xyz;
					    u_xlatb12 = 1.0<_Interpolator;
					    u_xlat2.xyz = (bool(u_xlatb12)) ? (-u_xlat2.xyz) : u_xlat2.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[2].z;
					    u_xlat4.xz = (-u_xlat3.xy);
					    u_xlat3.xy = u_xlat3.xy / unity_MatrixV[1].zz;
					    u_xlat4.y = unity_MatrixV[1].z;
					    u_xlat12.x = dot(u_xlat4.zxy, u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) * u_xlat12.xxx + u_xlat2.yzx;
					    u_xlat12.x = dot(u_xlat5.xyz, u_xlat5.xyz);
					    u_xlat12.x = sqrt(u_xlat12.x);
					    u_xlat35 = u_xlat12.x + 1.0;
					    u_xlatb12 = 2.0<u_xlat12.x;
					    u_xlat11.x = u_xlat11.x * u_xlat35;
					    u_xlat33 = u_xlat11.z / u_xlat35;
					    u_xlat5.xyz = u_xlat2.xyz * u_xlat4.yzx;
					    u_xlat2.xyz = u_xlat2.zxy * u_xlat4.zxy + (-u_xlat5.xyz);
					    u_xlat35 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat35 = inversesqrt(u_xlat35);
					    u_xlat2.xyz = vec3(u_xlat35) * u_xlat2.xyz;
					    u_xlat5.xyz = u_xlat4.yzx * u_xlat2.zxy;
					    u_xlat5.xyz = u_xlat2.yzx * u_xlat4.zxy + (-u_xlat5.xyz);
					    u_xlat2.xyz = vec3(u_xlat33) * u_xlat2.xyz;
					    u_xlat1.xzw = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xwz;
					    u_xlat2.xyz = u_xlat11.xxx * u_xlat5.xyz;
					    u_xlat1.xzw = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + u_xlat1.xzw;
					    u_xlat1.xyw = (bool(u_xlatb12)) ? vec3(0.0, 0.0, 0.0) : u_xlat1.xwz;
					    u_xlat11.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat2.xy = u_xlat3.xy * u_xlat11.xx + u_xlat1.xy;
					    u_xlat11.x = u_xlat11.x / u_xlat22;
					    vs_TEXCOORD1.y = u_xlat11.x + 0.5;
					    u_xlat11.x = dot(u_xlat2.xy, u_xlat2.xy);
					    u_xlat11.x = sqrt(u_xlat11.x);
					    u_xlat11.x = u_xlat11.x + (-_FogMinRad);
					    u_xlat22 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat11.x / u_xlat22;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat11.x = _WaterLevel * 2.0 + (-u_xlat1.w);
					    u_xlat2 = u_xlat11.xxxx * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.yyyy + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat2.y = 1.0;
					    u_xlat1.xyz = u_xlat1.xwy + u_xlat2.xyz;
					    u_xlat11.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat11.xyz = max(u_xlat11.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat11.xyz = min(u_xlat11.xyz, u_xlat2.xyz);
					    u_xlat11.xyz = u_xlat11.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat11.xyz);
					    u_xlat11.xyz = floor(u_xlat11.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat35 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat11.y * u_xlat35;
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat11.x + u_xlat36;
					    u_xlat6.z = u_xlat11.z * u_xlat5.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat3.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat3.yyy * u_xlat7.xyz;
					    u_xlat8.xyz = u_xlat11.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat36;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat21.yz = u_xlat6.yx;
					    u_xlat9.xyz = u_xlat2.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat3.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat3.zzz * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat3.zzz + u_xlat9.xyz;
					    u_xlat22 = u_xlat35 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat11.x + u_xlat22;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat22;
					    u_xlat21.x = u_xlat5.y * u_xlat8.z;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat11.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat11.xyz = u_xlat2.yyy * u_xlat11.xyz;
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat3.zzz + u_xlat7.xyz;
					    u_xlat11.xyz = u_xlat11.xyz * u_xlat3.zzz + u_xlat5.xyz;
					    u_xlat5 = textureLod(_NormalTex, u_xlat21.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat21.zx, 0.0);
					    u_xlat6.w = u_xlat21.x;
					    u_xlat7.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat3.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat11.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat11.xyz;
					    u_xlat11.xyz = u_xlat7.xyz * u_xlat2.zzz + u_xlat11.xyz;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat2.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat3.xxx * u_xlat5.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat11.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat11.xyz;
					    u_xlat11.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat11.xyz;
					    u_xlat11.xyz = u_xlat11.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat11.xyz) + (-u_xlat4.xyz);
					    u_xlat11.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat11.xyz;
					    u_xlat11.xyz = u_xlat11.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat11.xyz = max(u_xlat11.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat11.xyz = min(u_xlat11.xyz, u_xlat2.xyz);
					    u_xlat1.x = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat1.x = u_xlat1.x * 0.25;
					    u_xlat11.xyz = u_xlat11.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat12.xyz = fract(u_xlat11.xyz);
					    u_xlat11.xyz = floor(u_xlat11.xyz);
					    u_xlat2.xyz = (-u_xlat12.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat35 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat3.x = u_xlat11.y * u_xlat35;
					    u_xlat14.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.y = u_xlat14.x * u_xlat11.x + u_xlat3.x;
					    u_xlat4.z = u_xlat11.z * u_xlat14.y;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat11.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat14.x * u_xlat6.x + u_xlat3.x;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat19.yz = u_xlat4.yx;
					    u_xlat7.xyz = u_xlat12.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.yyy * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.zzz * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat7.xyz;
					    u_xlat22 = u_xlat35 * u_xlat6.y;
					    u_xlat4.y = u_xlat14.x * u_xlat11.x + u_xlat22;
					    u_xlat4.x = u_xlat14.x * u_xlat6.x + u_xlat22;
					    u_xlat19.x = u_xlat14.y * u_xlat6.z;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat11.xyz = u_xlat12.xxx * u_xlat6.xyz;
					    u_xlat11.xyz = u_xlat12.yyy * u_xlat11.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat12.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat2.zzz + u_xlat5.xyz;
					    u_xlat11.xyz = u_xlat11.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat19.yx, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat19.zx, 0.0);
					    u_xlat4.w = u_xlat19.x;
					    u_xlat5.xyz = u_xlat12.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat13.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat11.xyz = u_xlat13.xyz * u_xlat12.zzz + u_xlat11.xyz;
					    u_xlat11.xyz = u_xlat5.xyz * u_xlat12.zzz + u_xlat11.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat13.xyz = u_xlat12.xxx * u_xlat4.xyz;
					    u_xlat13.xyz = u_xlat12.yyy * u_xlat13.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat12.yyy * u_xlat3.xyz;
					    u_xlat11.xyz = u_xlat3.xyz * u_xlat12.zzz + u_xlat11.xyz;
					    u_xlat11.xyz = u_xlat13.xyz * u_xlat12.zzz + u_xlat11.xyz;
					    u_xlat2.y = abs(_SunDir.y);
					    u_xlat2.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xyz = u_xlat2.xyz;
					    u_xlat2.xyz = clamp(u_xlat2.xyz, 0.0, 1.0);
					    u_xlat11.xy = u_xlat11.xy * u_xlat2.xy;
					    u_xlat11.x = u_xlat11.y + u_xlat11.x;
					    u_xlat11.x = u_xlat2.z * u_xlat11.z + u_xlat11.x;
					    u_xlat11.x = u_xlat11.x * 0.600000024 + 0.200000003;
					    u_xlat22 = u_xlat1.x;
					    u_xlat22 = clamp(u_xlat22, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x);
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat33 = max(u_xlat22, u_xlat1.x);
					    u_xlat33 = (-u_xlat33) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat12.xyz = vec3(u_xlat22) * u_xlat2.yzx;
					    u_xlat12.xyz = u_xlat2.xyz * vec3(u_xlat33) + u_xlat12.xyz;
					    u_xlat1.xyz = u_xlat1.xxx * u_xlat2.zxy + u_xlat12.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = u_xlat11.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat1 = u_xlat1 * in_COLOR0;
					    u_xlat11.xy = (-_Alpha.xz) + _Alpha.yw;
					    u_xlat11.xy = in_TEXCOORD3.yy * u_xlat11.xy + _Alpha.xz;
					    u_xlat22 = (-u_xlat11.x) + u_xlat11.y;
					    u_xlat0 = u_xlat0 * u_xlat22 + u_xlat11.x;
					    vs_COLOR0.w = u_xlat0 * u_xlat1.w;
					    vs_COLOR0.xyz = u_xlat1.xyz;
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
						vec4 unused_0_0[65];
						vec4 _Color;
						vec4 unused_0_2[5];
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
						vec4 unused_0_0[65];
						vec4 _Color;
						vec4 unused_0_2[5];
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
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
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
					Keywords { "_AGE_ON" }
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
						vec4 unused_0_2[5];
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
					Keywords { "_MOBILE_PLATFORM" "_AGE_ON" }
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
						vec4 unused_0_2[5];
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
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
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
						vec4 unused_0_8[4];
						vec4 _Color;
						vec4 unused_0_10[5];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec2 u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat2.xy = vs_TEXCOORD1.yx;
					    u_xlat2.xy = clamp(u_xlat2.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat1.w * u_xlat2.x + -0.00999999978;
					    u_xlatb9 = u_xlat9<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat9 = u_xlat1.w * u_xlat2.x;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlat9 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat9)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat2.yyy * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_4[7];
						vec4 _FogColor;
						vec4 unused_0_6;
						vec4 _FlashColor;
						vec4 unused_0_8[4];
						vec4 _Color;
						vec4 unused_0_10[5];
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
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat2.xy = vs_TEXCOORD1.yx;
					    u_xlat2.xy = clamp(u_xlat2.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat1.w * u_xlat2.x + -0.00999999978;
					    u_xlatb9 = u_xlat9<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat9 = u_xlat1.w * u_xlat2.x;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlat9 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat9)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat2.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_AGE_ON" }
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
						vec4 unused_0_10[5];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec2 u_xlat2;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat2.xy = vs_TEXCOORD1.yx;
					    u_xlat2.xy = clamp(u_xlat2.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat1.w * u_xlat2.x + -0.00999999978;
					    u_xlatb9 = u_xlat9<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat9 = u_xlat1.w * u_xlat2.x;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlat9 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat9)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat2.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_AGE_ON" }
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
						vec4 unused_0_10[5];
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
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w + -0.100000001;
					    u_xlat0 = u_xlat0 * _Color;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat0.xyz = (-u_xlat0.xyz) * vs_COLOR0.xyz + vs_COLOR0.xyz;
					    u_xlat2.xy = vs_TEXCOORD1.yx;
					    u_xlat2.xy = clamp(u_xlat2.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat1.w * u_xlat2.x + -0.00999999978;
					    u_xlatb9 = u_xlat9<0.0;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat9 = (-vs_TEXCOORD1.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat9 = u_xlat1.w * u_xlat2.x;
					    SV_Target0.w = u_xlat9;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlat9 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat9)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat2.yyy * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_2[17];
						vec4 _Color;
						vec4 unused_0_4[5];
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
					    u_xlat0.x = u_xlat0.w * _Color.w;
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
						vec4 unused_0_2[17];
						vec4 _Color;
						vec4 unused_0_4[5];
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
					    u_xlat2.x = u_xlat0.w * _Color.w;
					    u_xlat2.x = u_xlat2.x * vs_COLOR0.w;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = vs_TEXCOORD1.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat4 = u_xlat2.x * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat2.x;
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
					Keywords { "_MIRROR_ON" "_AGE_ON" }
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
						vec4 unused_0_2[17];
						vec4 _Color;
						vec4 unused_0_4[5];
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
					    u_xlat0.x = u_xlat0.w * _Color.w;
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
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_AGE_ON" }
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
						vec4 unused_0_2[17];
						vec4 _Color;
						vec4 unused_0_4[5];
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
					    u_xlat2.x = u_xlat0.w * _Color.w;
					    u_xlat2.x = u_xlat2.x * vs_COLOR0.w;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = vs_TEXCOORD1.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat4 = u_xlat2.x * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat2.x;
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
						vec4 unused_0_10[4];
						vec4 _Color;
						vec4 unused_0_12[5];
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
					float u_xlat9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w * _Color.w;
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
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[4];
						vec4 _Color;
						vec4 unused_0_12[5];
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
					float u_xlat3;
					float u_xlat9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlat3 = u_xlat0.w * _Color.w;
					    u_xlat3 = u_xlat3 * vs_COLOR0.w;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xz = vs_TEXCOORD1.yx;
					    u_xlat0.xz = clamp(u_xlat0.xz, 0.0, 1.0);
					    u_xlat9 = u_xlat3 * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat3;
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
					    SV_Target0.xyz = u_xlat0.zzz * u_xlat1.xyz + u_xlat0.xyw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_AGE_ON" }
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
						vec4 unused_0_10[4];
						vec4 _Color;
						vec4 unused_0_12[5];
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
					float u_xlat9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w * _Color.w;
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
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_AGE_ON" }
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
						vec4 unused_0_10[4];
						vec4 _Color;
						vec4 unused_0_12[5];
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
					float u_xlat3;
					float u_xlat9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.100000001;
					    u_xlat3 = u_xlat0.w * _Color.w;
					    u_xlat3 = u_xlat3 * vs_COLOR0.w;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xz = vs_TEXCOORD1.yx;
					    u_xlat0.xz = clamp(u_xlat0.xz, 0.0, 1.0);
					    u_xlat9 = u_xlat3 * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat3;
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
					    SV_Target0.xyz = u_xlat0.zzz * u_xlat1.xyz + u_xlat0.xyw;
					    return;
					}"
				}
			}
		}
	}
}