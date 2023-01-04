Shader "Unlit/ShipShade" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		[Toggle] _Paint ("Paint", Float) = 0
		[Toggle] _Selected ("Selected", Float) = 0
	}
	SubShader {
		LOD 100
		Tags { "RenderType" = "Opaque" }
		Pass {
			LOD 100
			Tags { "RenderType" = "Opaque" }
			Blend One One, One One
			BlendOp Min, Min
			ZWrite Off
			Cull Off
			Stencil {
				Ref 2
				Comp Equal
				Pass Keep
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 34450
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[37];
						float _Year;
						vec4 unused_0_4[7];
						vec4 _SnowColor;
						vec4 unused_0_6;
						float _SnowAmount;
						vec4 unused_0_8[4];
						float _WaterLevel;
						vec4 unused_0_10[7];
						vec4 _MainTex_ST;
						vec4 _Color;
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
					uniform  sampler2D _TopdownTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out float vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1 = 0.0;
					    u_xlat0.xy = in_POSITION0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_POSITION0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_POSITION0.zz + u_xlat0.xy;
					    u_xlat0.xz = unity_ObjectToWorld[3].xz * in_POSITION0.ww + u_xlat0.xy;
					    u_xlat1.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat1.xy = u_xlat1.xy / _AoTexVolume.xz;
					    u_xlat1 = textureLod(_TopdownTex, u_xlat1.xy, 0.0);
					    u_xlat9 = u_xlat1.w * 8.0 + _WaterLevel;
					    u_xlat0.y = min(u_xlat9, 0.0);
					    u_xlat1.x = unity_MatrixV[0].z * -0.200000003;
					    u_xlat1.y = unity_MatrixV[1].z * -0.200000003;
					    u_xlat1.z = unity_MatrixV[2].z * -0.200000003;
					    u_xlat0.xzw = u_xlat0.xyz + (-u_xlat1.xyz);
					    u_xlat1 = u_xlat0.zzzz * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat0.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat0.x) * 0.5 + u_xlat0.y;
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
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.zxy + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _SnowColor.xyz;
					    u_xlat0.xyz = vec3(_SnowAmount) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.529999971, 0.529999971, 0.529999971);
					    u_xlat0.w = 1.0;
					    vs_COLOR0 = u_xlat0 * _Color;
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[37];
						float _Year;
						vec4 unused_0_4[7];
						vec4 _SnowColor;
						vec4 unused_0_6;
						float _SnowAmount;
						vec4 unused_0_8[4];
						float _WaterLevel;
						vec4 unused_0_10[7];
						vec4 _MainTex_ST;
						vec4 _Color;
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
					uniform  sampler2D _TopdownTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out float vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1 = 0.0;
					    u_xlat0.xy = in_POSITION0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_POSITION0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_POSITION0.zz + u_xlat0.xy;
					    u_xlat0.xz = unity_ObjectToWorld[3].xz * in_POSITION0.ww + u_xlat0.xy;
					    u_xlat1.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat1.xy = u_xlat1.xy / _AoTexVolume.xz;
					    u_xlat1 = textureLod(_TopdownTex, u_xlat1.xy, 0.0);
					    u_xlat9 = u_xlat1.w * 8.0 + _WaterLevel;
					    u_xlat0.y = min(u_xlat9, 0.0);
					    u_xlat1.x = unity_MatrixV[0].z * -0.200000003;
					    u_xlat1.y = unity_MatrixV[1].z * -0.200000003;
					    u_xlat1.z = unity_MatrixV[2].z * -0.200000003;
					    u_xlat0.xzw = u_xlat0.xyz + (-u_xlat1.xyz);
					    u_xlat1 = u_xlat0.zzzz * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat0.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat0.x) * 0.5 + u_xlat0.y;
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
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.zxy + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _SnowColor.xyz;
					    u_xlat0.xyz = vec3(_SnowAmount) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.529999971, 0.529999971, 0.529999971);
					    u_xlat0.w = 1.0;
					    vs_COLOR0 = u_xlat0 * _Color;
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
						vec4 unused_0_6;
						vec4 _LutLerp;
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec3 _PaintTexOffset;
						vec4 _LevelRect;
						vec4 unused_0_12[19];
						vec3 _SunDir;
						vec4 unused_0_14[4];
						vec4 _SideSunColor;
						vec4 unused_0_16[2];
						float _Year;
						vec4 unused_0_18[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_22[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_29;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_32[2];
						vec4 _MainTex_ST;
						vec4 _Color;
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
					uniform  sampler2D _TopdownTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out float vs_TEXCOORD1;
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
					vec4 u_xlat10;
					vec3 u_xlat11;
					float u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat16;
					vec3 u_xlat20;
					vec3 u_xlat21;
					vec2 u_xlat22;
					float u_xlat23;
					float u_xlat24;
					float u_xlat33;
					float u_xlat34;
					float u_xlat35;
					float u_xlat36;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0 = _FogMaxRad + (-_FogMinRad);
					    u_xlat11.xy = in_POSITION0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat11.xy = unity_ObjectToWorld[0].xz * in_POSITION0.xx + u_xlat11.xy;
					    u_xlat11.xy = unity_ObjectToWorld[2].xz * in_POSITION0.zz + u_xlat11.xy;
					    u_xlat1.xz = unity_ObjectToWorld[3].xz * in_POSITION0.ww + u_xlat11.xy;
					    u_xlat11.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat11.x = sqrt(u_xlat11.x);
					    u_xlat11.x = u_xlat11.x + (-_FogMinRad);
					    u_xlat0 = u_xlat11.x / u_xlat0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    vs_TEXCOORD1 = u_xlat0;
					    u_xlat2.x = unity_MatrixV[0].z * -0.200000003;
					    u_xlat2.y = unity_MatrixV[1].z * -0.200000003;
					    u_xlat2.z = unity_MatrixV[2].z * -0.200000003;
					    u_xlat11.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat1.xz;
					    u_xlat11.xy = u_xlat11.xy / _AoTexVolume.xz;
					    u_xlat3 = textureLod(_TopdownTex, u_xlat11.xy, 0.0);
					    u_xlat11.x = u_xlat3.w * 8.0 + _WaterLevel;
					    u_xlat1.y = min(u_xlat11.x, 0.0);
					    u_xlat11.xyz = u_xlat1.xyz + (-u_xlat2.xyz);
					    u_xlat2 = u_xlat11.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat11.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat11.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat2.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat11.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat2.w = u_xlat1.y;
					    u_xlat1.xzw = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xzw = max(u_xlat1.xzw, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xzw = min(u_xlat1.xzw, u_xlat3.xyz);
					    u_xlat1.xzw = u_xlat1.xzw + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xzw);
					    u_xlat1.xzw = fract(u_xlat1.xzw);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat35 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat3.y * u_xlat35;
					    u_xlat6.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat36;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat36;
					    u_xlat7 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat14.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat7 = u_xlat1.xxxx * u_xlat7;
					    u_xlat9.xyz = (-u_xlat1.xzw) + vec3(1.0, 1.0, 1.0);
					    u_xlat7 = u_xlat7 * u_xlat9.yyyy;
					    u_xlat7 = u_xlat9.zzzz * u_xlat7;
					    u_xlat8 = u_xlat8 * u_xlat9.xxxx;
					    u_xlat8 = u_xlat9.yyyy * u_xlat8;
					    u_xlat7 = u_xlat8 * u_xlat9.zzzz + u_xlat7;
					    u_xlat35 = u_xlat35 * u_xlat6.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat35;
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat35;
					    u_xlat14.x = u_xlat4.y * u_xlat6.z;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat6 = u_xlat1.zzzz * u_xlat6;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.zzzz * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat9.zzzz + u_xlat7;
					    u_xlat4 = u_xlat6 * u_xlat9.zzzz + u_xlat4;
					    u_xlat6 = textureLod(_NormalTex, u_xlat14.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat14.zx, 0.0);
					    u_xlat5.w = u_xlat14.x;
					    u_xlat3 = u_xlat1.xxxx * u_xlat7;
					    u_xlat3 = u_xlat9.yyyy * u_xlat3;
					    u_xlat6 = u_xlat9.xxxx * u_xlat6;
					    u_xlat6 = u_xlat9.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat1.wwww + u_xlat4;
					    u_xlat3 = u_xlat3 * u_xlat1.wwww + u_xlat4;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat1.xxxx * u_xlat5;
					    u_xlat5 = u_xlat1.zzzz * u_xlat5;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.zzzz * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.wwww + u_xlat3;
					    u_xlat3 = u_xlat5 * u_xlat1.wwww + u_xlat3;
					    u_xlat1.x = u_xlat3.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -2.0, -1.0);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + vec3(0.0, 1.0, 0.0);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.yxyz + vec4(-2.0, -0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat4.yzw);
					    u_xlat12 = (-u_xlat4.x) * 0.5 + u_xlat1.y;
					    u_xlat12 = u_xlat12 * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat23 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat34 = u_xlat2.y * u_xlat23;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat34;
					    u_xlat7.z = u_xlat2.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat13.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat13.x + u_xlat34;
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat21.yz = u_xlat7.yx;
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.zzz * u_xlat9.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat9.xyz;
					    u_xlat23 = u_xlat23 * u_xlat13.y;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat23;
					    u_xlat7.x = u_xlat6.x * u_xlat13.x + u_xlat23;
					    u_xlat21.x = u_xlat13.z * u_xlat6.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat6.xyz = u_xlat4.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat5.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat4.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat2.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat2.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat21.yx, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat21.zx, 0.0);
					    u_xlat7.w = u_xlat21.x;
					    u_xlat8.xyz = u_xlat4.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat16.xyz = u_xlat5.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat16.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat8.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat16.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat16.xyz = u_xlat4.yyy * u_xlat16.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat16.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat4.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xy = abs(_SunDir.yx);
					    u_xlat5.xyz = u_xlat4.zxw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat23 = u_xlat4.x + u_xlat4.y;
					    u_xlat2.xyw = u_xlat2.xyz * u_xlat5.xyz;
					    u_xlat34 = u_xlat2.y + u_xlat2.x;
					    u_xlat34 = u_xlat5.z * u_xlat2.z + u_xlat34;
					    u_xlat1.x = u_xlat34 * 0.600000024 + u_xlat1.x;
					    u_xlat34 = u_xlat12;
					    u_xlat34 = clamp(u_xlat34, 0.0, 1.0);
					    u_xlat12 = (-u_xlat12);
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat24 = max(u_xlat12, u_xlat34);
					    u_xlat24 = (-u_xlat24) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat34) * u_xlat4.yzx;
					    u_xlat5.xyz = u_xlat4.xyz * vec3(u_xlat24) + u_xlat5.xyz;
					    u_xlat4.xyz = vec3(u_xlat12) * u_xlat4.zxy + u_xlat5.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + _SnowColor.xyz;
					    u_xlat4.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat4.xyz * _MinAmbientColor.xyz;
					    u_xlat6.xyz = (-_MinAmbientColor.xyz) * u_xlat4.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyw = u_xlat1.xxx * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat24 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24 = inversesqrt(u_xlat24);
					    u_xlat3.xyz = vec3(u_xlat24) * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat5.xyz = u_xlat3.xyw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat24 = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24 = clamp(u_xlat24, 0.0, 1.0);
					    u_xlat24 = (-u_xlat24) + 1.0;
					    u_xlat2.xy = u_xlat2.xy * u_xlat5.xy;
					    u_xlat2.x = u_xlat2.y + u_xlat2.x;
					    u_xlat2.x = u_xlat2.w * u_xlat5.z + u_xlat2.x;
					    u_xlat23 = u_xlat2.x / u_xlat23;
					    u_xlat2.xyw = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat1.xyz = u_xlat2.xyw * vec3(u_xlat23) + u_xlat1.xyw;
					    u_xlat34 = u_xlat24 * u_xlat24;
					    u_xlat34 = (-u_xlat24) * u_xlat34 + 1.0;
					    u_xlat2.xyz = vec3(u_xlat34) * _FlashColor.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat4.xyz) * u_xlat1.xyz + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xy = max(u_xlat11.xz, _LevelRect.xy);
					    u_xlat11.xz = min(u_xlat3.xy, _LevelRect.zw);
					    u_xlat11.xyz = max(u_xlat11.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat11.xyz = min(u_xlat11.xyz, u_xlat3.xyz);
					    u_xlat11.xyz = u_xlat11.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat11.xyz);
					    u_xlat11.xyz = floor(u_xlat11.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat5.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat6.z = u_xlat11.z * u_xlat5.y;
					    u_xlat34 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat35 = u_xlat11.y * u_xlat34;
					    u_xlat7.xyz = u_xlat11.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat7.x + u_xlat35;
					    u_xlat6.y = u_xlat5.x * u_xlat11.x + u_xlat35;
					    u_xlat8 = textureLod(_PaintTex, u_xlat6.xz, 0.0);
					    u_xlat20.yz = u_xlat6.yx;
					    u_xlat10 = textureLod(_PaintTex, u_xlat6.yz, 0.0);
					    u_xlat22.x = u_xlat4.x * u_xlat10.y;
					    u_xlat22.y = u_xlat3.x * u_xlat8.y;
					    u_xlat22.xy = u_xlat4.yy * u_xlat22.xy;
					    u_xlat33 = u_xlat4.z * u_xlat22.y;
					    u_xlat22.x = u_xlat22.x * u_xlat4.z + u_xlat33;
					    u_xlat33 = u_xlat34 * u_xlat7.y;
					    u_xlat6.y = u_xlat5.x * u_xlat11.x + u_xlat33;
					    u_xlat6.x = u_xlat5.x * u_xlat7.x + u_xlat33;
					    u_xlat20.x = u_xlat5.y * u_xlat7.z;
					    u_xlat5 = textureLod(_PaintTex, u_xlat6.yz, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat6.xz, 0.0);
					    u_xlat11.x = u_xlat3.x * u_xlat7.y;
					    u_xlat11.z = u_xlat4.x * u_xlat5.y;
					    u_xlat11.xz = u_xlat3.yy * u_xlat11.xz;
					    u_xlat22.x = u_xlat11.z * u_xlat4.z + u_xlat22.x;
					    u_xlat11.x = u_xlat11.x * u_xlat4.z + u_xlat22.x;
					    u_xlat5 = textureLod(_PaintTex, u_xlat20.yx, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat20.zx, 0.0);
					    u_xlat6.w = u_xlat20.x;
					    u_xlat22.x = u_xlat3.x * u_xlat7.y;
					    u_xlat22.y = u_xlat4.x * u_xlat5.y;
					    u_xlat22.xy = u_xlat4.yy * u_xlat22.xy;
					    u_xlat11.x = u_xlat22.y * u_xlat3.z + u_xlat11.x;
					    u_xlat11.x = u_xlat22.x * u_xlat3.z + u_xlat11.x;
					    u_xlat5 = textureLod(_PaintTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_PaintTex, u_xlat6.xw, 0.0);
					    u_xlat22.x = u_xlat3.x * u_xlat6.y;
					    u_xlat22.y = u_xlat4.x * u_xlat5.y;
					    u_xlat22.xy = u_xlat3.yy * u_xlat22.xy;
					    u_xlat11.x = u_xlat22.y * u_xlat3.z + u_xlat11.x;
					    u_xlat11.x = u_xlat22.x * u_xlat3.z + u_xlat11.x;
					    u_xlat11.xyz = u_xlat11.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat11.xyz * _Color.xyz;
					    u_xlat1.x = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat11.xyz = u_xlat11.xyz * _Color.xyz + (-u_xlat1.xxx);
					    u_xlat11.xyz = _CloudCoverage.yyy * u_xlat11.xyz + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat11.xyz) + _LutLerp.www;
					    u_xlat11.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat11.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat11.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat0) * u_xlat1.xyz + u_xlat11.xyz;
					    vs_COLOR0.w = _Color.w;
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
						vec4 unused_0_6;
						vec4 _LutLerp;
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec3 _PaintTexOffset;
						vec4 _LevelRect;
						vec4 unused_0_12[19];
						vec3 _SunDir;
						vec4 unused_0_14[4];
						vec4 _SideSunColor;
						vec4 unused_0_16[2];
						float _Year;
						vec4 unused_0_18[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_22[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_29;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_32[2];
						vec4 _MainTex_ST;
						vec4 _Color;
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
					uniform  sampler2D _TopdownTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out float vs_TEXCOORD1;
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
					vec4 u_xlat10;
					vec3 u_xlat11;
					float u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat16;
					vec3 u_xlat20;
					vec3 u_xlat21;
					vec2 u_xlat22;
					float u_xlat23;
					float u_xlat24;
					float u_xlat33;
					float u_xlat34;
					float u_xlat35;
					float u_xlat36;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0 = _FogMaxRad + (-_FogMinRad);
					    u_xlat11.xy = in_POSITION0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat11.xy = unity_ObjectToWorld[0].xz * in_POSITION0.xx + u_xlat11.xy;
					    u_xlat11.xy = unity_ObjectToWorld[2].xz * in_POSITION0.zz + u_xlat11.xy;
					    u_xlat1.xz = unity_ObjectToWorld[3].xz * in_POSITION0.ww + u_xlat11.xy;
					    u_xlat11.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat11.x = sqrt(u_xlat11.x);
					    u_xlat11.x = u_xlat11.x + (-_FogMinRad);
					    u_xlat0 = u_xlat11.x / u_xlat0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    vs_TEXCOORD1 = u_xlat0;
					    u_xlat2.x = unity_MatrixV[0].z * -0.200000003;
					    u_xlat2.y = unity_MatrixV[1].z * -0.200000003;
					    u_xlat2.z = unity_MatrixV[2].z * -0.200000003;
					    u_xlat11.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat1.xz;
					    u_xlat11.xy = u_xlat11.xy / _AoTexVolume.xz;
					    u_xlat3 = textureLod(_TopdownTex, u_xlat11.xy, 0.0);
					    u_xlat11.x = u_xlat3.w * 8.0 + _WaterLevel;
					    u_xlat1.y = min(u_xlat11.x, 0.0);
					    u_xlat11.xyz = u_xlat1.xyz + (-u_xlat2.xyz);
					    u_xlat2 = u_xlat11.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat11.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat11.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat2.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat11.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat2.w = u_xlat1.y;
					    u_xlat1.xzw = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xzw = max(u_xlat1.xzw, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xzw = min(u_xlat1.xzw, u_xlat3.xyz);
					    u_xlat1.xzw = u_xlat1.xzw + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xzw);
					    u_xlat1.xzw = fract(u_xlat1.xzw);
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.z = u_xlat3.z * u_xlat4.y;
					    u_xlat35 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat3.y * u_xlat35;
					    u_xlat6.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat36;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat36;
					    u_xlat7 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat14.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat7 = u_xlat1.xxxx * u_xlat7;
					    u_xlat9.xyz = (-u_xlat1.xzw) + vec3(1.0, 1.0, 1.0);
					    u_xlat7 = u_xlat7 * u_xlat9.yyyy;
					    u_xlat7 = u_xlat9.zzzz * u_xlat7;
					    u_xlat8 = u_xlat8 * u_xlat9.xxxx;
					    u_xlat8 = u_xlat9.yyyy * u_xlat8;
					    u_xlat7 = u_xlat8 * u_xlat9.zzzz + u_xlat7;
					    u_xlat35 = u_xlat35 * u_xlat6.y;
					    u_xlat5.y = u_xlat4.x * u_xlat3.x + u_xlat35;
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat35;
					    u_xlat14.x = u_xlat4.y * u_xlat6.z;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat6 = u_xlat1.xxxx * u_xlat6;
					    u_xlat6 = u_xlat1.zzzz * u_xlat6;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.zzzz * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat9.zzzz + u_xlat7;
					    u_xlat4 = u_xlat6 * u_xlat9.zzzz + u_xlat4;
					    u_xlat6 = textureLod(_NormalTex, u_xlat14.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat14.zx, 0.0);
					    u_xlat5.w = u_xlat14.x;
					    u_xlat3 = u_xlat1.xxxx * u_xlat7;
					    u_xlat3 = u_xlat9.yyyy * u_xlat3;
					    u_xlat6 = u_xlat9.xxxx * u_xlat6;
					    u_xlat6 = u_xlat9.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat1.wwww + u_xlat4;
					    u_xlat3 = u_xlat3 * u_xlat1.wwww + u_xlat4;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat1.xxxx * u_xlat5;
					    u_xlat5 = u_xlat1.zzzz * u_xlat5;
					    u_xlat4 = u_xlat9.xxxx * u_xlat4;
					    u_xlat4 = u_xlat1.zzzz * u_xlat4;
					    u_xlat3 = u_xlat4 * u_xlat1.wwww + u_xlat3;
					    u_xlat3 = u_xlat5 * u_xlat1.wwww + u_xlat3;
					    u_xlat1.x = u_xlat3.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -2.0, -1.0);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + vec3(0.0, 1.0, 0.0);
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.yxyz + vec4(-2.0, -0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat4.yzw);
					    u_xlat12 = (-u_xlat4.x) * 0.5 + u_xlat1.y;
					    u_xlat12 = u_xlat12 * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat23 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat34 = u_xlat2.y * u_xlat23;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat34;
					    u_xlat7.z = u_xlat2.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat13.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat13.x + u_xlat34;
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat21.yz = u_xlat7.yx;
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.zzz * u_xlat9.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat9.xyz;
					    u_xlat23 = u_xlat23 * u_xlat13.y;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat23;
					    u_xlat7.x = u_xlat6.x * u_xlat13.x + u_xlat23;
					    u_xlat21.x = u_xlat13.z * u_xlat6.y;
					    u_xlat2 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat6.xyz = u_xlat4.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat5.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat4.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat2.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat2.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat21.yx, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat21.zx, 0.0);
					    u_xlat7.w = u_xlat21.x;
					    u_xlat8.xyz = u_xlat4.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat16.xyz = u_xlat5.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat16.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat8.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat16.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat16.xyz = u_xlat4.yyy * u_xlat16.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat16.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat4.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xy = abs(_SunDir.yx);
					    u_xlat5.xyz = u_xlat4.zxw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat23 = u_xlat4.x + u_xlat4.y;
					    u_xlat2.xyw = u_xlat2.xyz * u_xlat5.xyz;
					    u_xlat34 = u_xlat2.y + u_xlat2.x;
					    u_xlat34 = u_xlat5.z * u_xlat2.z + u_xlat34;
					    u_xlat1.x = u_xlat34 * 0.600000024 + u_xlat1.x;
					    u_xlat34 = u_xlat12;
					    u_xlat34 = clamp(u_xlat34, 0.0, 1.0);
					    u_xlat12 = (-u_xlat12);
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat24 = max(u_xlat12, u_xlat34);
					    u_xlat24 = (-u_xlat24) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat34) * u_xlat4.yzx;
					    u_xlat5.xyz = u_xlat4.xyz * vec3(u_xlat24) + u_xlat5.xyz;
					    u_xlat4.xyz = vec3(u_xlat12) * u_xlat4.zxy + u_xlat5.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + _SnowColor.xyz;
					    u_xlat4.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat4.xyz * _MinAmbientColor.xyz;
					    u_xlat6.xyz = (-_MinAmbientColor.xyz) * u_xlat4.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyw = u_xlat1.xxx * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat24 = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24 = inversesqrt(u_xlat24);
					    u_xlat3.xyz = vec3(u_xlat24) * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat5.xyz = u_xlat3.xyw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat24 = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24 = clamp(u_xlat24, 0.0, 1.0);
					    u_xlat24 = (-u_xlat24) + 1.0;
					    u_xlat2.xy = u_xlat2.xy * u_xlat5.xy;
					    u_xlat2.x = u_xlat2.y + u_xlat2.x;
					    u_xlat2.x = u_xlat2.w * u_xlat5.z + u_xlat2.x;
					    u_xlat23 = u_xlat2.x / u_xlat23;
					    u_xlat2.xyw = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat1.xyz = u_xlat2.xyw * vec3(u_xlat23) + u_xlat1.xyw;
					    u_xlat34 = u_xlat24 * u_xlat24;
					    u_xlat34 = (-u_xlat24) * u_xlat34 + 1.0;
					    u_xlat2.xyz = vec3(u_xlat34) * _FlashColor.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat4.xyz) * u_xlat1.xyz + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xy = max(u_xlat11.xz, _LevelRect.xy);
					    u_xlat11.xz = min(u_xlat3.xy, _LevelRect.zw);
					    u_xlat11.xyz = max(u_xlat11.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat11.xyz = min(u_xlat11.xyz, u_xlat3.xyz);
					    u_xlat11.xyz = u_xlat11.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat11.xyz);
					    u_xlat11.xyz = floor(u_xlat11.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat5.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat6.z = u_xlat11.z * u_xlat5.y;
					    u_xlat34 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat35 = u_xlat11.y * u_xlat34;
					    u_xlat7.xyz = u_xlat11.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat7.x + u_xlat35;
					    u_xlat6.y = u_xlat5.x * u_xlat11.x + u_xlat35;
					    u_xlat8 = textureLod(_PaintTex, u_xlat6.xz, 0.0);
					    u_xlat20.yz = u_xlat6.yx;
					    u_xlat10 = textureLod(_PaintTex, u_xlat6.yz, 0.0);
					    u_xlat22.x = u_xlat4.x * u_xlat10.y;
					    u_xlat22.y = u_xlat3.x * u_xlat8.y;
					    u_xlat22.xy = u_xlat4.yy * u_xlat22.xy;
					    u_xlat33 = u_xlat4.z * u_xlat22.y;
					    u_xlat22.x = u_xlat22.x * u_xlat4.z + u_xlat33;
					    u_xlat33 = u_xlat34 * u_xlat7.y;
					    u_xlat6.y = u_xlat5.x * u_xlat11.x + u_xlat33;
					    u_xlat6.x = u_xlat5.x * u_xlat7.x + u_xlat33;
					    u_xlat20.x = u_xlat5.y * u_xlat7.z;
					    u_xlat5 = textureLod(_PaintTex, u_xlat6.yz, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat6.xz, 0.0);
					    u_xlat11.x = u_xlat3.x * u_xlat7.y;
					    u_xlat11.z = u_xlat4.x * u_xlat5.y;
					    u_xlat11.xz = u_xlat3.yy * u_xlat11.xz;
					    u_xlat22.x = u_xlat11.z * u_xlat4.z + u_xlat22.x;
					    u_xlat11.x = u_xlat11.x * u_xlat4.z + u_xlat22.x;
					    u_xlat5 = textureLod(_PaintTex, u_xlat20.yx, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat20.zx, 0.0);
					    u_xlat6.w = u_xlat20.x;
					    u_xlat22.x = u_xlat3.x * u_xlat7.y;
					    u_xlat22.y = u_xlat4.x * u_xlat5.y;
					    u_xlat22.xy = u_xlat4.yy * u_xlat22.xy;
					    u_xlat11.x = u_xlat22.y * u_xlat3.z + u_xlat11.x;
					    u_xlat11.x = u_xlat22.x * u_xlat3.z + u_xlat11.x;
					    u_xlat5 = textureLod(_PaintTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_PaintTex, u_xlat6.xw, 0.0);
					    u_xlat22.x = u_xlat3.x * u_xlat6.y;
					    u_xlat22.y = u_xlat4.x * u_xlat5.y;
					    u_xlat22.xy = u_xlat3.yy * u_xlat22.xy;
					    u_xlat11.x = u_xlat22.y * u_xlat3.z + u_xlat11.x;
					    u_xlat11.x = u_xlat22.x * u_xlat3.z + u_xlat11.x;
					    u_xlat11.xyz = u_xlat11.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat11.xyz * _Color.xyz;
					    u_xlat1.x = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat11.xyz = u_xlat11.xyz * _Color.xyz + (-u_xlat1.xxx);
					    u_xlat11.xyz = _CloudCoverage.yyy * u_xlat11.xyz + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat11.xyz) + _LutLerp.www;
					    u_xlat11.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat11.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat11.xyz) + u_xlat1.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat0) * u_xlat1.xyz + u_xlat11.xyz;
					    vs_COLOR0.w = _Color.w;
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
					
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0 = vs_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0 = vs_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0 = vs_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_PAINT_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0 = vs_COLOR0;
					    return;
					}"
				}
			}
		}
	}
}