Shader "Effects/Explosion" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_FireColor ("Fire Color", Vector) = (0.5,0.5,0.5,1)
		_SmokeColor ("Smoke Color", Vector) = (0.5,0.5,0.5,1)
		_Multiplier ("Multiplier", Float) = 1
		[KeywordEnum(None, Up, Cliff, Tree)] _COLOR ("Color Mode", Float) = 0
		_Test ("test", Range(0, 1)) = 1
	}
	SubShader {
		LOD 100
		Tags { "RenderType" = "Transparent" }
		Pass {
			LOD 100
			Tags { "RenderType" = "Transparent" }
			Blend One OneMinusSrcAlpha, One OneMinusSrcAlpha
			ZWrite Off
			GpuProgramID 32885
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
						vec4 unused_0_2[7];
						vec4 _MainTex_ST;
						vec4 unused_0_4[3];
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
					in  vec4 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD2;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_TEXCOORD0.zw;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat2 = max(u_xlat0.y, _WaterLevel);
					    u_xlat1 = vec4(u_xlat2) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0;
					    vs_COLOR1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_TEXCOORD2 = 0.0;
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
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_22[2];
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_25[2];
						vec4 _MainTex_ST;
						vec4 unused_0_27[3];
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
					in  vec4 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD2;
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
					vec3 u_xlat10;
					vec3 u_xlat11;
					float u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat17;
					float u_xlat20;
					float u_xlat21;
					float u_xlat23;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_TEXCOORD0.zw;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat0.w = max(u_xlat0.y, _WaterLevel);
					    u_xlat1 = u_xlat0.wwww * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0;
					    u_xlat1.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat1.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xwz + u_xlat1.xyz;
					    u_xlat0.x = dot(u_xlat0.xz, u_xlat0.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.w = u_xlat0.w;
					    u_xlat10.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat10.xyz = max(u_xlat10.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat10.xyz = min(u_xlat10.xyz, u_xlat2.xyz);
					    u_xlat10.xyz = u_xlat10.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat10.xyz);
					    u_xlat10.xyz = fract(u_xlat10.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat32 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat23 = u_xlat2.y * u_xlat32;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat23;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat23;
					    u_xlat6 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat17.yz = u_xlat4.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat6 = u_xlat10.xxxx * u_xlat6;
					    u_xlat9.xyz = (-u_xlat10.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat9.yyyy;
					    u_xlat6 = u_xlat9.zzzz * u_xlat6;
					    u_xlat8 = u_xlat8 * u_xlat9.xxxx;
					    u_xlat8 = u_xlat9.yyyy * u_xlat8;
					    u_xlat6 = u_xlat8 * u_xlat9.zzzz + u_xlat6;
					    u_xlat12 = u_xlat32 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat12;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat12;
					    u_xlat17.x = u_xlat3.y * u_xlat5.z;
					    u_xlat2 = textureLod(_NormalTex, u_xlat4.yz, 0.0);
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.xz, 0.0);
					    u_xlat3 = u_xlat10.xxxx * u_xlat3;
					    u_xlat3 = u_xlat10.yyyy * u_xlat3;
					    u_xlat2 = u_xlat9.xxxx * u_xlat2;
					    u_xlat2 = u_xlat10.yyyy * u_xlat2;
					    u_xlat2 = u_xlat2 * u_xlat9.zzzz + u_xlat6;
					    u_xlat2 = u_xlat3 * u_xlat9.zzzz + u_xlat2;
					    u_xlat3 = textureLod(_NormalTex, u_xlat17.yx, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat17.zx, 0.0);
					    u_xlat4.w = u_xlat17.x;
					    u_xlat5 = u_xlat10.xxxx * u_xlat5;
					    u_xlat5 = u_xlat9.yyyy * u_xlat5;
					    u_xlat3 = u_xlat9.xxxx * u_xlat3;
					    u_xlat3 = u_xlat9.yyyy * u_xlat3;
					    u_xlat2 = u_xlat3 * u_xlat10.zzzz + u_xlat2;
					    u_xlat2 = u_xlat5 * u_xlat10.zzzz + u_xlat2;
					    u_xlat3 = textureLod(_NormalTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat4.xw, 0.0);
					    u_xlat4 = u_xlat10.xxxx * u_xlat4;
					    u_xlat4 = u_xlat10.yyyy * u_xlat4;
					    u_xlat3 = u_xlat9.xxxx * u_xlat3;
					    u_xlat3 = u_xlat10.yyyy * u_xlat3;
					    u_xlat2 = u_xlat3 * u_xlat10.zzzz + u_xlat2;
					    u_xlat2 = u_xlat4 * u_xlat10.zzzz + u_xlat2;
					    u_xlat10.x = u_xlat2.w * 0.400000006;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat3.x = (-u_xlat2.x) + unity_MatrixV[0].z;
					    u_xlat3.y = (-u_xlat2.y) + unity_MatrixV[1].z;
					    u_xlat3.z = (-u_xlat2.z) + unity_MatrixV[2].z;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat20 = (-u_xlat3.w) * 0.5 + u_xlat1.w;
					    u_xlat20 = u_xlat20 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat30 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat31 = u_xlat1.y * u_xlat30;
					    u_xlat5.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat1.x + u_xlat31;
					    u_xlat6.z = u_xlat1.z * u_xlat5.y;
					    u_xlat7 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat8.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat31;
					    u_xlat9 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat11.yz = u_xlat6.yx;
					    u_xlat9.xyz = u_xlat3.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.zzz * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat4.zzz + u_xlat9.xyz;
					    u_xlat30 = u_xlat30 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat1.x + u_xlat30;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat30;
					    u_xlat11.x = u_xlat5.y * u_xlat8.z;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yz, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat6.xz, 0.0);
					    u_xlat8.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat4.zzz + u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat8.xyz * u_xlat4.zzz + u_xlat5.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat11.yx, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat11.zx, 0.0);
					    u_xlat6.w = u_xlat11.x;
					    u_xlat1.xyz = u_xlat3.xxx * u_xlat8.xyz;
					    u_xlat1.xyz = u_xlat4.yyy * u_xlat1.xyz;
					    u_xlat7.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat14.xyz = u_xlat4.yyy * u_xlat7.xyz;
					    u_xlat14.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat5.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat14.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat6.xw, 0.0);
					    u_xlat14.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat4.xxx * u_xlat5.xyz;
					    u_xlat3.xyw = u_xlat3.yyy * u_xlat5.xyz;
					    u_xlat1.xyz = u_xlat3.xyw * u_xlat3.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat14.xyz * u_xlat3.zzz + u_xlat1.xyz;
					    u_xlat3.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat3.xy = abs(_SunDir.yx);
					    u_xlat4.xyz = u_xlat3.zxw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat30 = u_xlat3.x + u_xlat3.y;
					    u_xlat1.xyw = u_xlat1.xyz * u_xlat4.xyz;
					    u_xlat32 = u_xlat1.y + u_xlat1.x;
					    u_xlat21 = u_xlat4.z * u_xlat1.z + u_xlat32;
					    u_xlat10.x = u_xlat21 * 0.600000024 + u_xlat10.x;
					    u_xlat21 = u_xlat20;
					    u_xlat21 = clamp(u_xlat21, 0.0, 1.0);
					    u_xlat20 = (-u_xlat20);
					    u_xlat20 = clamp(u_xlat20, 0.0, 1.0);
					    u_xlat32 = max(u_xlat20, u_xlat21);
					    u_xlat32 = (-u_xlat32) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat21) * u_xlat3.yzx;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(u_xlat32) + u_xlat4.xyz;
					    u_xlat3.xyz = vec3(u_xlat20) * u_xlat3.zxy + u_xlat4.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _SnowColor.xyz;
					    u_xlat3.xyz = vec3(_SnowAmount) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * _MinAmbientColor.xyz;
					    u_xlat3.xyz = (-_MinAmbientColor.xyz) * u_xlat3.xyz + _MaxAmbientColor.xyz;
					    u_xlat3.xyz = u_xlat10.xxx * u_xlat3.xyz + u_xlat4.xyz;
					    u_xlat10.x = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat10.x = inversesqrt(u_xlat10.x);
					    u_xlat2.xyz = u_xlat10.xxx * u_xlat2.xyz;
					    u_xlat2.w = (-u_xlat2.x);
					    u_xlat4.xyz = u_xlat2.xyw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat10.x = dot(u_xlat2.xyz, _FlashDir.xyz);
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat10.x = (-u_xlat10.x) + 1.0;
					    u_xlat1.xy = u_xlat1.xy * u_xlat4.xy;
					    u_xlat20 = u_xlat1.y + u_xlat1.x;
					    u_xlat20 = u_xlat1.w * u_xlat4.z + u_xlat20;
					    u_xlat20 = u_xlat20 / u_xlat30;
					    u_xlat1.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat20) + u_xlat3.xyz;
					    u_xlat20 = u_xlat10.x * u_xlat10.x;
					    u_xlat10.x = (-u_xlat10.x) * u_xlat20 + 1.0;
					    u_xlat10.xyz = u_xlat10.xxx * _FlashColor.xyz;
					    vs_COLOR1.xyz = u_xlat10.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat1.xyz;
					    vs_COLOR1.w = 1.0;
					    u_xlat10.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD2 = u_xlat0.x / u_xlat10.x;
					    vs_TEXCOORD2 = clamp(vs_TEXCOORD2, 0.0, 1.0);
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
						vec4 unused_0_0[5];
						vec4 _CliffParams;
						vec4 unused_0_2[59];
						vec4 _FireColor;
						vec4 _SmokeColor;
						float _Multiplier;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					float u_xlat3;
					float u_xlat5;
					float u_xlat7;
					float u_xlat10;
					void main()
					{
					    u_xlat0 = vs_TEXCOORD1.xxyy * vec4(2.0, 2.0, 2.0, 2.0) + vec4(-1.0, -2.0, -1.0, -2.0);
					    u_xlat1.xy = u_xlat0.xz * u_xlat0.xz;
					    u_xlat0 = u_xlat0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) * u_xlat1.x + 1.0;
					    u_xlat7 = (-u_xlat1.x) + 1.0;
					    u_xlat7 = clamp(u_xlat7, 0.0, 1.0);
					    u_xlat10 = (-u_xlat7) + 1.0;
					    u_xlat10 = u_xlat10 * 0.00999999978 + u_xlat7;
					    u_xlat10 = min(u_xlat10, 1.0);
					    u_xlat10 = (-u_xlat7) * 0.99000001 + u_xlat10;
					    u_xlat2 = textureLod(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat5 = (-u_xlat2.x) + u_xlat2.y;
					    u_xlat0.xz = u_xlat0.xz * vec2(u_xlat5) + u_xlat2.xx;
					    u_xlat2.xy = (-u_xlat0.xz) + u_xlat2.zz;
					    u_xlat0.xy = u_xlat0.yw * u_xlat2.xy + u_xlat0.xz;
					    u_xlat0.x = (-u_xlat7) * 0.99000001 + u_xlat0.x;
					    u_xlat3 = (-u_xlat1.y) * u_xlat1.y + u_xlat0.y;
					    u_xlat3 = clamp(u_xlat3, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x / u_xlat10;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat1.x * u_xlat0.x;
					    u_xlat1.xyz = _FireColor.xyz * vec3(_Multiplier);
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = _CliffParams.www + (-_SmokeColor.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + _SmokeColor.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR1.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat1 = vec4(u_xlat3) * u_xlat1;
					    SV_Target0.xyz = u_xlat0.xzw * vs_COLOR0.xyz + u_xlat1.xyz;
					    SV_Target0.w = u_xlat1.w;
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
						vec4 unused_0_0[5];
						vec4 _CliffParams;
						vec4 unused_0_2[3];
						vec4 _LutLerp;
						vec4 unused_0_4[40];
						vec4 _CloudCoverage;
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[4];
						vec4 _FireColor;
						vec4 _SmokeColor;
						float _Multiplier;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  float vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					float u_xlat3;
					float u_xlat5;
					float u_xlat7;
					float u_xlat10;
					void main()
					{
					    u_xlat0 = vs_TEXCOORD1.xxyy * vec4(2.0, 2.0, 2.0, 2.0) + vec4(-1.0, -2.0, -1.0, -2.0);
					    u_xlat1.xy = u_xlat0.xz * u_xlat0.xz;
					    u_xlat0 = u_xlat0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat1.xy = (-u_xlat1.xy) * u_xlat1.xy + vec2(1.0, 1.0);
					    u_xlat7 = (-u_xlat1.x) + 1.0;
					    u_xlat7 = clamp(u_xlat7, 0.0, 1.0);
					    u_xlat10 = (-u_xlat7) + 1.0;
					    u_xlat10 = u_xlat10 * 0.00999999978 + u_xlat7;
					    u_xlat10 = min(u_xlat10, 1.0);
					    u_xlat10 = (-u_xlat7) * 0.99000001 + u_xlat10;
					    u_xlat2 = textureLod(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat5 = (-u_xlat2.x) + u_xlat2.y;
					    u_xlat0.xz = u_xlat0.xz * vec2(u_xlat5) + u_xlat2.xx;
					    u_xlat2.xy = (-u_xlat0.xz) + u_xlat2.zz;
					    u_xlat0.xy = u_xlat0.yw * u_xlat2.xy + u_xlat0.xz;
					    u_xlat0.x = (-u_xlat7) * 0.99000001 + u_xlat0.x;
					    u_xlat3 = u_xlat1.y + u_xlat0.y;
					    u_xlat3 = u_xlat3 + -1.0;
					    u_xlat3 = clamp(u_xlat3, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x / u_xlat10;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat1.x * u_xlat0.x;
					    u_xlat1.xyz = _FireColor.xyz * vec3(_Multiplier);
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = _CliffParams.www + (-_SmokeColor.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + _SmokeColor.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * vs_COLOR1.xyz;
					    u_xlat10 = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR1.xyz + (-vec3(u_xlat10));
					    u_xlat1.xyz = _CloudCoverage.yyy * u_xlat1.xyz + vec3(u_xlat10);
					    u_xlat2.xyz = (-u_xlat1.xyz) + _LutLerp.www;
					    u_xlat1.xyz = _LutLerp.xyz * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat2.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat10 = vs_TEXCOORD2;
					    u_xlat10 = clamp(u_xlat10, 0.0, 1.0);
					    u_xlat1.xyz = vec3(u_xlat10) * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat1 = vec4(u_xlat3) * u_xlat1;
					    SV_Target0.xyz = u_xlat0.xzw * vs_COLOR0.xyz + u_xlat1.xyz;
					    SV_Target0.w = u_xlat1.w;
					    return;
					}"
				}
			}
		}
	}
}