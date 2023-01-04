Shader "Unlit/TransparentVoxel" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
	}
	SubShader {
		LOD 100
		Tags { "RenderType" = "Opaque" }
		Pass {
			LOD 100
			Tags { "RenderType" = "Opaque" }
			AlphaToMask On
			GpuProgramID 43684
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
						vec4 unused_0_0[15];
						vec3 _WindDir;
						float _MaxWindTime;
						vec4 unused_0_3[28];
						vec4 _FoamColor;
						vec4 unused_0_5[2];
						vec4 _MirrorColor2;
						vec4 unused_0_7[16];
						vec4 _MainTex_ST;
						vec4 unused_0_9;
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
					uniform  sampler2D _MainTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD2.x = 0.0;
					    vs_TEXCOORD2.y = (-in_TEXCOORD0.y) + 0.949999988;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = in_TEXCOORD0.y + 1.0;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    u_xlat0.x = u_xlat0.x * 0.5;
					    u_xlat0.x = u_xlat0.x * u_xlat0.x;
					    u_xlat2 = in_NORMAL0.y * 0.100000001 + 1.0;
					    u_xlat1 = _FoamColor * vec4(u_xlat2) + (-_MirrorColor2);
					    vs_COLOR0 = u_xlat0.xxxx * u_xlat1 + _MirrorColor2;
					    u_xlat0.xy = _WindDir.xz * vec2(vec2(_MaxWindTime, _MaxWindTime));
					    u_xlat0.xy = (-u_xlat0.xy) * vec2(0.200000003, 0.200000003) + in_POSITION0.xz;
					    u_xlat0.xy = u_xlat0.xy * vec2(0.200000003, 0.200000003);
					    vs_COLOR1 = textureLod(_MainTex, u_xlat0.xy, 0.0);
					    vs_TEXCOORD3.x = in_TEXCOORD0.y * -0.600000024 + 0.5;
					    vs_TEXCOORD3.y = 0.0;
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
						vec4 unused_0_0[15];
						vec3 _WindDir;
						float _MaxWindTime;
						vec4 unused_0_3[28];
						vec4 _FoamColor;
						vec4 unused_0_5[2];
						vec4 _MirrorColor2;
						vec4 unused_0_7[16];
						vec4 _MainTex_ST;
						vec4 unused_0_9;
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
					uniform  sampler2D _MainTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD2.x = 0.0;
					    vs_TEXCOORD2.y = (-in_TEXCOORD0.y) + 0.949999988;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = in_TEXCOORD0.y + 1.0;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    u_xlat0.x = u_xlat0.x * 0.5;
					    u_xlat0.x = u_xlat0.x * u_xlat0.x;
					    u_xlat2 = in_NORMAL0.y * 0.100000001 + 1.0;
					    u_xlat1 = _FoamColor * vec4(u_xlat2) + (-_MirrorColor2);
					    vs_COLOR0 = u_xlat0.xxxx * u_xlat1 + _MirrorColor2;
					    u_xlat0.xy = _WindDir.xz * vec2(vec2(_MaxWindTime, _MaxWindTime));
					    u_xlat0.xy = (-u_xlat0.xy) * vec2(0.200000003, 0.200000003) + in_POSITION0.xz;
					    u_xlat0.xy = u_xlat0.xy * vec2(0.200000003, 0.200000003);
					    vs_COLOR1 = textureLod(_MainTex, u_xlat0.xy, 0.0);
					    vs_TEXCOORD3.x = in_TEXCOORD0.y * -0.600000024 + 0.5;
					    vs_TEXCOORD3.y = 0.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_SELECTION_ON" }
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
						vec4 unused_0_0[15];
						vec3 _WindDir;
						float _MaxWindTime;
						vec4 unused_0_3[28];
						vec4 _FoamColor;
						vec4 unused_0_5[2];
						vec4 _MirrorColor2;
						vec4 unused_0_7[16];
						vec4 _MainTex_ST;
						vec4 unused_0_9;
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
					uniform  sampler2D _MainTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD2.x = 0.0;
					    vs_TEXCOORD2.y = (-in_TEXCOORD0.y) + 0.949999988;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = in_TEXCOORD0.y + 1.0;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    u_xlat0.x = u_xlat0.x * 0.5;
					    u_xlat0.x = u_xlat0.x * u_xlat0.x;
					    u_xlat2 = in_NORMAL0.y * 0.100000001 + 1.0;
					    u_xlat1 = _FoamColor * vec4(u_xlat2) + (-_MirrorColor2);
					    vs_COLOR0 = u_xlat0.xxxx * u_xlat1 + _MirrorColor2;
					    u_xlat0.xy = _WindDir.xz * vec2(vec2(_MaxWindTime, _MaxWindTime));
					    u_xlat0.xy = (-u_xlat0.xy) * vec2(0.200000003, 0.200000003) + in_POSITION0.xz;
					    u_xlat0.xy = u_xlat0.xy * vec2(0.200000003, 0.200000003);
					    vs_COLOR1 = textureLod(_MainTex, u_xlat0.xy, 0.0);
					    vs_TEXCOORD3.x = in_TEXCOORD0.y * -0.600000024 + 0.5;
					    vs_TEXCOORD3.y = 0.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_SELECTION_ON" }
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
						vec4 unused_0_0[15];
						vec3 _WindDir;
						float _MaxWindTime;
						vec4 unused_0_3[28];
						vec4 _FoamColor;
						vec4 unused_0_5[2];
						vec4 _MirrorColor2;
						vec4 unused_0_7[16];
						vec4 _MainTex_ST;
						vec4 unused_0_9;
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
					uniform  sampler2D _MainTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD2.x = 0.0;
					    vs_TEXCOORD2.y = (-in_TEXCOORD0.y) + 0.949999988;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = in_TEXCOORD0.y + 1.0;
					    u_xlat0.x = u_xlat0.x + (-in_TANGENT0.w);
					    u_xlat0.x = u_xlat0.x * 0.5;
					    u_xlat0.x = u_xlat0.x * u_xlat0.x;
					    u_xlat2 = in_NORMAL0.y * 0.100000001 + 1.0;
					    u_xlat1 = _FoamColor * vec4(u_xlat2) + (-_MirrorColor2);
					    vs_COLOR0 = u_xlat0.xxxx * u_xlat1 + _MirrorColor2;
					    u_xlat0.xy = _WindDir.xz * vec2(vec2(_MaxWindTime, _MaxWindTime));
					    u_xlat0.xy = (-u_xlat0.xy) * vec2(0.200000003, 0.200000003) + in_POSITION0.xz;
					    u_xlat0.xy = u_xlat0.xy * vec2(0.200000003, 0.200000003);
					    vs_COLOR1 = textureLod(_MainTex, u_xlat0.xy, 0.0);
					    vs_TEXCOORD3.x = in_TEXCOORD0.y * -0.600000024 + 0.5;
					    vs_TEXCOORD3.y = 0.0;
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
						vec4 unused_0_5[4];
						float _WindInterpolator;
						vec3 _WindDir;
						float _MaxWindTime;
						float _WindTime;
						float _SqrtWindTime;
						vec4 unused_0_11[16];
						vec3 _SunDir;
						vec4 unused_0_13[4];
						vec4 _SideSunColor;
						vec4 unused_0_15[2];
						float _Year;
						vec4 unused_0_17[2];
						vec4 _FoamColor;
						vec4 unused_0_19[2];
						vec4 _MirrorColor2;
						vec4 unused_0_21;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_25[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_31;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_34[2];
						vec4 _MainTex_ST;
						vec4 unused_0_36;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _WindTex;
					uniform  sampler2D _MainTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					float u_xlat10;
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat17;
					float u_xlat20;
					vec2 u_xlat21;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + in_POSITION0.xyz;
					    u_xlat1.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat30 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat4.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat13.xz = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.x = u_xlat13.x * u_xlat4.x + u_xlat31;
					    u_xlat5.y = u_xlat13.x * u_xlat3.x + u_xlat31;
					    u_xlat5.z = u_xlat3.z * u_xlat13.z;
					    u_xlat6 = textureLod(_WindTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_WindTex, u_xlat5.yz, 0.0);
					    u_xlat6.xy = u_xlat1.xx * u_xlat6.xz;
					    u_xlat9.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xy = u_xlat6.xy * u_xlat9.yy;
					    u_xlat6.zw = u_xlat8.xz * u_xlat9.xx;
					    u_xlat6 = u_xlat9.zzyy * u_xlat6;
					    u_xlat6.xy = u_xlat6.zw * u_xlat9.zz + u_xlat6.xy;
					    u_xlat31 = u_xlat30 * u_xlat4.y;
					    u_xlat5.y = u_xlat13.x * u_xlat3.x + u_xlat31;
					    u_xlat5.x = u_xlat13.x * u_xlat4.x + u_xlat31;
					    u_xlat17.x = u_xlat4.z * u_xlat13.z;
					    u_xlat4 = textureLod(_WindTex, u_xlat5.yz, 0.0);
					    u_xlat8 = textureLod(_WindTex, u_xlat5.xz, 0.0);
					    u_xlat3.xz = u_xlat1.xx * u_xlat8.xz;
					    u_xlat3.xz = u_xlat1.yy * u_xlat3.xz;
					    u_xlat4.xy = u_xlat9.xx * u_xlat4.xz;
					    u_xlat4.xy = u_xlat1.yy * u_xlat4.xy;
					    u_xlat4.xy = u_xlat4.xy * u_xlat9.zz + u_xlat6.xy;
					    u_xlat3.xz = u_xlat3.xz * u_xlat9.zz + u_xlat4.xy;
					    u_xlat4 = textureLod(_WindTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_WindTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat4.yw = u_xlat1.xx * u_xlat6.xz;
					    u_xlat4 = u_xlat9.xyxy * u_xlat4;
					    u_xlat4.xz = u_xlat9.yy * u_xlat4.xz;
					    u_xlat3.xz = u_xlat4.xz * u_xlat1.zz + u_xlat3.xz;
					    u_xlat3.xz = u_xlat4.yw * u_xlat1.zz + u_xlat3.xz;
					    u_xlat4 = textureLod(_WindTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_WindTex, u_xlat5.xw, 0.0);
					    u_xlat1.xw = u_xlat1.xx * u_xlat5.xz;
					    u_xlat1.xw = u_xlat1.yy * u_xlat1.xw;
					    u_xlat4.xy = u_xlat9.xx * u_xlat4.xz;
					    u_xlat4.xy = u_xlat1.yy * u_xlat4.xy;
					    u_xlat3.xz = u_xlat4.xy * u_xlat1.zz + u_xlat3.xz;
					    u_xlat1.xy = u_xlat1.xw * u_xlat1.zz + u_xlat3.xz;
					    u_xlat1.xy = u_xlat1.xy * vec2(4.0, 4.0) + vec2(-2.0, -2.0);
					    u_xlat4 = u_xlat1.xyxy * vec4(_WindInterpolator);
					    u_xlat4 = u_xlat4 * vec4(0.300000012, 0.300000012, 0.300000012, 0.300000012);
					    u_xlat4 = in_TANGENT0.xzxz * vec4(0.5, 0.5, 0.5, 0.5) + u_xlat4;
					    u_xlat21.xy = (-in_TEXCOORD0.yy) + vec2(0.949999988, 1.0);
					    u_xlat5 = u_xlat21.yyyy * u_xlat4;
					    u_xlat3.xz = u_xlat4.zw * u_xlat21.yy + in_POSITION0.xz;
					    u_xlat31 = dot(in_TANGENT0.xzxz, u_xlat5);
					    u_xlat31 = (-u_xlat31) + in_TEXCOORD0.y;
					    u_xlat4.x = dot(in_POSITION0.xz, (-u_xlat1.xy));
					    u_xlat1.x = dot(u_xlat1.xy, in_TANGENT0.xz);
					    u_xlat1.x = u_xlat1.x * in_TEXCOORD0.y;
					    u_xlat1.x = dot(u_xlat1.xx, vec2(_WindInterpolator));
					    u_xlat11.x = _SqrtWindTime * 0.600000024 + 10.0;
					    u_xlat11.x = _WindTime * 0.200000003 + u_xlat11.x;
					    u_xlat11.x = (-u_xlat4.x) * _WindInterpolator + u_xlat11.x;
					    u_xlat4.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat4.x = sqrt(u_xlat4.x);
					    u_xlat11.x = (-u_xlat4.x) * 0.699999988 + u_xlat11.x;
					    u_xlat4.x = u_xlat4.x + (-_FogMinRad);
					    vs_TEXCOORD2.x = u_xlat31 * 1.29999995 + u_xlat11.x;
					    u_xlat11.x = u_xlat11.x * 6.28318548;
					    u_xlat11.x = sin(u_xlat11.x);
					    vs_TEXCOORD2.y = u_xlat11.x * 0.0199999996 + u_xlat21.x;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat5 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat5 = unity_ObjectToWorld[0] * u_xlat3.xxxx + u_xlat5;
					    u_xlat5 = unity_ObjectToWorld[2] * u_xlat3.zzzz + u_xlat5;
					    u_xlat5 = u_xlat5 + unity_ObjectToWorld[3];
					    u_xlat6 = u_xlat5.yyyy * unity_MatrixVP[1];
					    u_xlat6 = unity_MatrixVP[0] * u_xlat5.xxxx + u_xlat6;
					    u_xlat6 = unity_MatrixVP[2] * u_xlat5.zzzz + u_xlat6;
					    gl_Position = unity_MatrixVP[3] * u_xlat5.wwww + u_xlat6;
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat11.x = (-u_xlat2.w) * 0.5 + in_POSITION0.y;
					    u_xlat11.x = u_xlat11.x * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat21.x = u_xlat30 * u_xlat2.y;
					    u_xlat14.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat13.x * u_xlat14.x + u_xlat21.x;
					    u_xlat5.y = u_xlat13.x * u_xlat2.x + u_xlat21.x;
					    u_xlat5.z = u_xlat2.z * u_xlat13.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat12.yz = u_xlat5.yx;
					    u_xlat7 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat8.yyy;
					    u_xlat6.xyz = u_xlat8.zzz * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat8.xxx;
					    u_xlat7.xyz = u_xlat8.yyy * u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat30 = u_xlat30 * u_xlat14.y;
					    u_xlat5.y = u_xlat13.x * u_xlat2.x + u_xlat30;
					    u_xlat5.x = u_xlat13.x * u_xlat14.x + u_xlat30;
					    u_xlat12.x = u_xlat13.z * u_xlat14.z;
					    u_xlat7 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat14.xyz = u_xlat0.xxx * u_xlat9.xyz;
					    u_xlat14.xyz = u_xlat0.yyy * u_xlat14.xyz;
					    u_xlat7.xyz = u_xlat8.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat0.yyy * u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat14.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat12.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat12.zx, 0.0);
					    u_xlat5.w = u_xlat12.x;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat8.yyy * u_xlat2.xyz;
					    u_xlat6.xyz = u_xlat8.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat8.yyy * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat6.xyz * u_xlat0.zzz + u_xlat14.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat0.zzz + u_xlat14.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat14.xyz = u_xlat0.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat0.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat8.xxx * u_xlat6.xyz;
					    u_xlat0.xyw = u_xlat0.yyy * u_xlat5.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat14.xyz = u_xlat2.zxw;
					    u_xlat14.xyz = clamp(u_xlat14.xyz, 0.0, 1.0);
					    u_xlat30 = u_xlat2.x + u_xlat2.y;
					    u_xlat2.xyz = u_xlat0.xyz * u_xlat14.xyz;
					    u_xlat0.x = u_xlat2.y + u_xlat2.x;
					    u_xlat0.x = u_xlat14.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.400000006;
					    u_xlat10 = u_xlat11.x;
					    u_xlat10 = clamp(u_xlat10, 0.0, 1.0);
					    u_xlat11.x = (-u_xlat11.x);
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat20 = max(u_xlat10, u_xlat11.x);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat5.x = _Year;
					    u_xlat5.y = 0.0;
					    u_xlat5 = textureLod(_GrassTex, u_xlat5.xy, 0.0);
					    u_xlat14.xyz = vec3(u_xlat10) * u_xlat5.yzx;
					    u_xlat14.xyz = u_xlat5.xyz * vec3(u_xlat20) + u_xlat14.xyz;
					    u_xlat11.xyz = u_xlat11.xxx * u_xlat5.zxy + u_xlat14.xyz;
					    u_xlat14.xyz = (-u_xlat11.xyz) + _SnowColor.xyz;
					    u_xlat11.xyz = vec3(_SnowAmount) * u_xlat14.xyz + u_xlat11.xyz;
					    u_xlat14.xyz = u_xlat11.xyz * _MinAmbientColor.xyz;
					    u_xlat11.xyz = (-_MinAmbientColor.xyz) * u_xlat11.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat11.xyz + u_xlat14.xyz;
					    u_xlat11.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat11.x = inversesqrt(u_xlat11.x);
					    u_xlat5.xyz = u_xlat11.xxx * in_NORMAL0.xyz;
					    u_xlat5.w = (-u_xlat5.x);
					    u_xlat11.xyz = u_xlat5.xyw;
					    u_xlat11.xyz = clamp(u_xlat11.xyz, 0.0, 1.0);
					    u_xlat32 = dot(u_xlat5.xyz, _FlashDir.xyz);
					    u_xlat32 = clamp(u_xlat32, 0.0, 1.0);
					    u_xlat32 = (-u_xlat32) + 1.0;
					    u_xlat11.xy = u_xlat11.xy * u_xlat2.xy;
					    u_xlat11.x = u_xlat11.y + u_xlat11.x;
					    u_xlat11.x = u_xlat2.z * u_xlat11.z + u_xlat11.x;
					    u_xlat30 = u_xlat11.x / u_xlat30;
					    u_xlat11.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat11.xyz * vec3(u_xlat30) + u_xlat0.xyz;
					    u_xlat30 = u_xlat32 * u_xlat32;
					    u_xlat30 = (-u_xlat32) * u_xlat30 + 1.0;
					    u_xlat11.xyz = vec3(u_xlat30) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat11.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = _FoamColor * u_xlat0 + (-_MirrorColor2);
					    u_xlat11.x = in_TEXCOORD0.y + 1.0;
					    u_xlat11.x = u_xlat11.x + (-in_TANGENT0.w);
					    u_xlat11.x = u_xlat11.x * 0.5;
					    u_xlat11.x = u_xlat11.x * u_xlat11.x;
					    u_xlat0 = u_xlat11.xxxx * u_xlat0 + _MirrorColor2;
					    vs_COLOR0.w = u_xlat0.w;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat30)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat30);
					    u_xlat11.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat11.xyz + u_xlat0.xyz;
					    u_xlat11.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat11.xyz = (-u_xlat0.xyz) + u_xlat11.xyz;
					    u_xlat30 = dot(in_POSITION0.xz, in_POSITION0.xz);
					    u_xlat30 = sqrt(u_xlat30);
					    u_xlat30 = u_xlat30 + (-_FogMinRad);
					    u_xlat2.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat30 = u_xlat30 / u_xlat2.x;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    vs_TEXCOORD3.y = u_xlat4.x / u_xlat2.x;
					    vs_TEXCOORD3.y = clamp(vs_TEXCOORD3.y, 0.0, 1.0);
					    vs_COLOR0.xyz = vec3(u_xlat30) * u_xlat11.xyz + u_xlat0.xyz;
					    u_xlat0.xy = _WindDir.xz * vec2(vec2(_MaxWindTime, _MaxWindTime));
					    u_xlat0.xy = (-u_xlat0.xy) * vec2(0.200000003, 0.200000003) + u_xlat3.xz;
					    u_xlat0.xy = u_xlat0.xy * vec2(0.200000003, 0.200000003);
					    vs_COLOR1 = textureLod(_MainTex, u_xlat0.xy, 0.0);
					    u_xlat0.x = in_TEXCOORD0.y * -0.600000024 + 0.5;
					    vs_TEXCOORD3.x = u_xlat1.x + u_xlat0.x;
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
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[4];
						float _WindInterpolator;
						vec3 _WindDir;
						float _MaxWindTime;
						float _WindTime;
						float _SqrtWindTime;
						vec4 unused_0_11[16];
						vec3 _SunDir;
						vec4 unused_0_13[4];
						vec4 _SideSunColor;
						vec4 unused_0_15[2];
						float _Year;
						vec4 unused_0_17[2];
						vec4 _FoamColor;
						vec4 unused_0_19[2];
						vec4 _MirrorColor2;
						vec4 unused_0_21;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_25[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_31;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_34[2];
						vec4 _MainTex_ST;
						vec4 unused_0_36;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _WindTex;
					uniform  sampler2D _MainTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					float u_xlat10;
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat17;
					float u_xlat20;
					vec2 u_xlat21;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + in_POSITION0.xyz;
					    u_xlat1.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat30 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat4.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat13.xz = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.x = u_xlat13.x * u_xlat4.x + u_xlat31;
					    u_xlat5.y = u_xlat13.x * u_xlat3.x + u_xlat31;
					    u_xlat5.z = u_xlat3.z * u_xlat13.z;
					    u_xlat6 = textureLod(_WindTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_WindTex, u_xlat5.yz, 0.0);
					    u_xlat6.xy = u_xlat1.xx * u_xlat6.xz;
					    u_xlat9.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xy = u_xlat6.xy * u_xlat9.yy;
					    u_xlat6.zw = u_xlat8.xz * u_xlat9.xx;
					    u_xlat6 = u_xlat9.zzyy * u_xlat6;
					    u_xlat6.xy = u_xlat6.zw * u_xlat9.zz + u_xlat6.xy;
					    u_xlat31 = u_xlat30 * u_xlat4.y;
					    u_xlat5.y = u_xlat13.x * u_xlat3.x + u_xlat31;
					    u_xlat5.x = u_xlat13.x * u_xlat4.x + u_xlat31;
					    u_xlat17.x = u_xlat4.z * u_xlat13.z;
					    u_xlat4 = textureLod(_WindTex, u_xlat5.yz, 0.0);
					    u_xlat8 = textureLod(_WindTex, u_xlat5.xz, 0.0);
					    u_xlat3.xz = u_xlat1.xx * u_xlat8.xz;
					    u_xlat3.xz = u_xlat1.yy * u_xlat3.xz;
					    u_xlat4.xy = u_xlat9.xx * u_xlat4.xz;
					    u_xlat4.xy = u_xlat1.yy * u_xlat4.xy;
					    u_xlat4.xy = u_xlat4.xy * u_xlat9.zz + u_xlat6.xy;
					    u_xlat3.xz = u_xlat3.xz * u_xlat9.zz + u_xlat4.xy;
					    u_xlat4 = textureLod(_WindTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_WindTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat4.yw = u_xlat1.xx * u_xlat6.xz;
					    u_xlat4 = u_xlat9.xyxy * u_xlat4;
					    u_xlat4.xz = u_xlat9.yy * u_xlat4.xz;
					    u_xlat3.xz = u_xlat4.xz * u_xlat1.zz + u_xlat3.xz;
					    u_xlat3.xz = u_xlat4.yw * u_xlat1.zz + u_xlat3.xz;
					    u_xlat4 = textureLod(_WindTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_WindTex, u_xlat5.xw, 0.0);
					    u_xlat1.xw = u_xlat1.xx * u_xlat5.xz;
					    u_xlat1.xw = u_xlat1.yy * u_xlat1.xw;
					    u_xlat4.xy = u_xlat9.xx * u_xlat4.xz;
					    u_xlat4.xy = u_xlat1.yy * u_xlat4.xy;
					    u_xlat3.xz = u_xlat4.xy * u_xlat1.zz + u_xlat3.xz;
					    u_xlat1.xy = u_xlat1.xw * u_xlat1.zz + u_xlat3.xz;
					    u_xlat1.xy = u_xlat1.xy * vec2(4.0, 4.0) + vec2(-2.0, -2.0);
					    u_xlat4 = u_xlat1.xyxy * vec4(_WindInterpolator);
					    u_xlat4 = u_xlat4 * vec4(0.300000012, 0.300000012, 0.300000012, 0.300000012);
					    u_xlat4 = in_TANGENT0.xzxz * vec4(0.5, 0.5, 0.5, 0.5) + u_xlat4;
					    u_xlat21.xy = (-in_TEXCOORD0.yy) + vec2(0.949999988, 1.0);
					    u_xlat5 = u_xlat21.yyyy * u_xlat4;
					    u_xlat3.xz = u_xlat4.zw * u_xlat21.yy + in_POSITION0.xz;
					    u_xlat31 = dot(in_TANGENT0.xzxz, u_xlat5);
					    u_xlat31 = (-u_xlat31) + in_TEXCOORD0.y;
					    u_xlat4.x = dot(in_POSITION0.xz, (-u_xlat1.xy));
					    u_xlat1.x = dot(u_xlat1.xy, in_TANGENT0.xz);
					    u_xlat1.x = u_xlat1.x * in_TEXCOORD0.y;
					    u_xlat1.x = dot(u_xlat1.xx, vec2(_WindInterpolator));
					    u_xlat11.x = _SqrtWindTime * 0.600000024 + 10.0;
					    u_xlat11.x = _WindTime * 0.200000003 + u_xlat11.x;
					    u_xlat11.x = (-u_xlat4.x) * _WindInterpolator + u_xlat11.x;
					    u_xlat4.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat4.x = sqrt(u_xlat4.x);
					    u_xlat11.x = (-u_xlat4.x) * 0.699999988 + u_xlat11.x;
					    u_xlat4.x = u_xlat4.x + (-_FogMinRad);
					    vs_TEXCOORD2.x = u_xlat31 * 1.29999995 + u_xlat11.x;
					    u_xlat11.x = u_xlat11.x * 6.28318548;
					    u_xlat11.x = sin(u_xlat11.x);
					    vs_TEXCOORD2.y = u_xlat11.x * 0.0199999996 + u_xlat21.x;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat5 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat5 = unity_ObjectToWorld[0] * u_xlat3.xxxx + u_xlat5;
					    u_xlat5 = unity_ObjectToWorld[2] * u_xlat3.zzzz + u_xlat5;
					    u_xlat5 = u_xlat5 + unity_ObjectToWorld[3];
					    u_xlat6 = u_xlat5.yyyy * unity_MatrixVP[1];
					    u_xlat6 = unity_MatrixVP[0] * u_xlat5.xxxx + u_xlat6;
					    u_xlat6 = unity_MatrixVP[2] * u_xlat5.zzzz + u_xlat6;
					    gl_Position = unity_MatrixVP[3] * u_xlat5.wwww + u_xlat6;
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat11.x = (-u_xlat2.w) * 0.5 + in_POSITION0.y;
					    u_xlat11.x = u_xlat11.x * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat21.x = u_xlat30 * u_xlat2.y;
					    u_xlat14.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat13.x * u_xlat14.x + u_xlat21.x;
					    u_xlat5.y = u_xlat13.x * u_xlat2.x + u_xlat21.x;
					    u_xlat5.z = u_xlat2.z * u_xlat13.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat12.yz = u_xlat5.yx;
					    u_xlat7 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat8.yyy;
					    u_xlat6.xyz = u_xlat8.zzz * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat8.xxx;
					    u_xlat7.xyz = u_xlat8.yyy * u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat30 = u_xlat30 * u_xlat14.y;
					    u_xlat5.y = u_xlat13.x * u_xlat2.x + u_xlat30;
					    u_xlat5.x = u_xlat13.x * u_xlat14.x + u_xlat30;
					    u_xlat12.x = u_xlat13.z * u_xlat14.z;
					    u_xlat7 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat14.xyz = u_xlat0.xxx * u_xlat9.xyz;
					    u_xlat14.xyz = u_xlat0.yyy * u_xlat14.xyz;
					    u_xlat7.xyz = u_xlat8.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat0.yyy * u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat14.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat12.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat12.zx, 0.0);
					    u_xlat5.w = u_xlat12.x;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat8.yyy * u_xlat2.xyz;
					    u_xlat6.xyz = u_xlat8.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat8.yyy * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat6.xyz * u_xlat0.zzz + u_xlat14.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat0.zzz + u_xlat14.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat14.xyz = u_xlat0.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat0.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat8.xxx * u_xlat6.xyz;
					    u_xlat0.xyw = u_xlat0.yyy * u_xlat5.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat14.xyz = u_xlat2.zxw;
					    u_xlat14.xyz = clamp(u_xlat14.xyz, 0.0, 1.0);
					    u_xlat30 = u_xlat2.x + u_xlat2.y;
					    u_xlat2.xyz = u_xlat0.xyz * u_xlat14.xyz;
					    u_xlat0.x = u_xlat2.y + u_xlat2.x;
					    u_xlat0.x = u_xlat14.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.400000006;
					    u_xlat10 = u_xlat11.x;
					    u_xlat10 = clamp(u_xlat10, 0.0, 1.0);
					    u_xlat11.x = (-u_xlat11.x);
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat20 = max(u_xlat10, u_xlat11.x);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat5.x = _Year;
					    u_xlat5.y = 0.0;
					    u_xlat5 = textureLod(_GrassTex, u_xlat5.xy, 0.0);
					    u_xlat14.xyz = vec3(u_xlat10) * u_xlat5.yzx;
					    u_xlat14.xyz = u_xlat5.xyz * vec3(u_xlat20) + u_xlat14.xyz;
					    u_xlat11.xyz = u_xlat11.xxx * u_xlat5.zxy + u_xlat14.xyz;
					    u_xlat14.xyz = (-u_xlat11.xyz) + _SnowColor.xyz;
					    u_xlat11.xyz = vec3(_SnowAmount) * u_xlat14.xyz + u_xlat11.xyz;
					    u_xlat14.xyz = u_xlat11.xyz * _MinAmbientColor.xyz;
					    u_xlat11.xyz = (-_MinAmbientColor.xyz) * u_xlat11.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat11.xyz + u_xlat14.xyz;
					    u_xlat11.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat11.x = inversesqrt(u_xlat11.x);
					    u_xlat5.xyz = u_xlat11.xxx * in_NORMAL0.xyz;
					    u_xlat5.w = (-u_xlat5.x);
					    u_xlat11.xyz = u_xlat5.xyw;
					    u_xlat11.xyz = clamp(u_xlat11.xyz, 0.0, 1.0);
					    u_xlat32 = dot(u_xlat5.xyz, _FlashDir.xyz);
					    u_xlat32 = clamp(u_xlat32, 0.0, 1.0);
					    u_xlat32 = (-u_xlat32) + 1.0;
					    u_xlat11.xy = u_xlat11.xy * u_xlat2.xy;
					    u_xlat11.x = u_xlat11.y + u_xlat11.x;
					    u_xlat11.x = u_xlat2.z * u_xlat11.z + u_xlat11.x;
					    u_xlat30 = u_xlat11.x / u_xlat30;
					    u_xlat11.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat11.xyz * vec3(u_xlat30) + u_xlat0.xyz;
					    u_xlat30 = u_xlat32 * u_xlat32;
					    u_xlat30 = (-u_xlat32) * u_xlat30 + 1.0;
					    u_xlat11.xyz = vec3(u_xlat30) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat11.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = _FoamColor * u_xlat0 + (-_MirrorColor2);
					    u_xlat11.x = in_TEXCOORD0.y + 1.0;
					    u_xlat11.x = u_xlat11.x + (-in_TANGENT0.w);
					    u_xlat11.x = u_xlat11.x * 0.5;
					    u_xlat11.x = u_xlat11.x * u_xlat11.x;
					    u_xlat0 = u_xlat11.xxxx * u_xlat0 + _MirrorColor2;
					    vs_COLOR0.w = u_xlat0.w;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat30)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat30);
					    u_xlat11.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat11.xyz + u_xlat0.xyz;
					    u_xlat11.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat11.xyz = (-u_xlat0.xyz) + u_xlat11.xyz;
					    u_xlat30 = dot(in_POSITION0.xz, in_POSITION0.xz);
					    u_xlat30 = sqrt(u_xlat30);
					    u_xlat30 = u_xlat30 + (-_FogMinRad);
					    u_xlat2.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat30 = u_xlat30 / u_xlat2.x;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    vs_TEXCOORD3.y = u_xlat4.x / u_xlat2.x;
					    vs_TEXCOORD3.y = clamp(vs_TEXCOORD3.y, 0.0, 1.0);
					    vs_COLOR0.xyz = vec3(u_xlat30) * u_xlat11.xyz + u_xlat0.xyz;
					    u_xlat0.xy = _WindDir.xz * vec2(vec2(_MaxWindTime, _MaxWindTime));
					    u_xlat0.xy = (-u_xlat0.xy) * vec2(0.200000003, 0.200000003) + u_xlat3.xz;
					    u_xlat0.xy = u_xlat0.xy * vec2(0.200000003, 0.200000003);
					    vs_COLOR1 = textureLod(_MainTex, u_xlat0.xy, 0.0);
					    u_xlat0.x = in_TEXCOORD0.y * -0.600000024 + 0.5;
					    vs_TEXCOORD3.x = u_xlat1.x + u_xlat0.x;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_SELECTION_ON" }
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
						vec4 unused_0_5[4];
						float _WindInterpolator;
						vec3 _WindDir;
						float _MaxWindTime;
						float _WindTime;
						float _SqrtWindTime;
						vec4 unused_0_11[16];
						vec3 _SunDir;
						vec4 unused_0_13[4];
						vec4 _SideSunColor;
						vec4 unused_0_15[2];
						float _Year;
						vec4 unused_0_17[2];
						vec4 _FoamColor;
						vec4 unused_0_19[2];
						vec4 _MirrorColor2;
						vec4 unused_0_21;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_25[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_31;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_34[2];
						vec4 _MainTex_ST;
						vec4 unused_0_36;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _WindTex;
					uniform  sampler2D _MainTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					float u_xlat10;
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat17;
					float u_xlat20;
					vec2 u_xlat21;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + in_POSITION0.xyz;
					    u_xlat1.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat30 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat4.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat13.xz = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.x = u_xlat13.x * u_xlat4.x + u_xlat31;
					    u_xlat5.y = u_xlat13.x * u_xlat3.x + u_xlat31;
					    u_xlat5.z = u_xlat3.z * u_xlat13.z;
					    u_xlat6 = textureLod(_WindTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_WindTex, u_xlat5.yz, 0.0);
					    u_xlat6.xy = u_xlat1.xx * u_xlat6.xz;
					    u_xlat9.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xy = u_xlat6.xy * u_xlat9.yy;
					    u_xlat6.zw = u_xlat8.xz * u_xlat9.xx;
					    u_xlat6 = u_xlat9.zzyy * u_xlat6;
					    u_xlat6.xy = u_xlat6.zw * u_xlat9.zz + u_xlat6.xy;
					    u_xlat31 = u_xlat30 * u_xlat4.y;
					    u_xlat5.y = u_xlat13.x * u_xlat3.x + u_xlat31;
					    u_xlat5.x = u_xlat13.x * u_xlat4.x + u_xlat31;
					    u_xlat17.x = u_xlat4.z * u_xlat13.z;
					    u_xlat4 = textureLod(_WindTex, u_xlat5.yz, 0.0);
					    u_xlat8 = textureLod(_WindTex, u_xlat5.xz, 0.0);
					    u_xlat3.xz = u_xlat1.xx * u_xlat8.xz;
					    u_xlat3.xz = u_xlat1.yy * u_xlat3.xz;
					    u_xlat4.xy = u_xlat9.xx * u_xlat4.xz;
					    u_xlat4.xy = u_xlat1.yy * u_xlat4.xy;
					    u_xlat4.xy = u_xlat4.xy * u_xlat9.zz + u_xlat6.xy;
					    u_xlat3.xz = u_xlat3.xz * u_xlat9.zz + u_xlat4.xy;
					    u_xlat4 = textureLod(_WindTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_WindTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat4.yw = u_xlat1.xx * u_xlat6.xz;
					    u_xlat4 = u_xlat9.xyxy * u_xlat4;
					    u_xlat4.xz = u_xlat9.yy * u_xlat4.xz;
					    u_xlat3.xz = u_xlat4.xz * u_xlat1.zz + u_xlat3.xz;
					    u_xlat3.xz = u_xlat4.yw * u_xlat1.zz + u_xlat3.xz;
					    u_xlat4 = textureLod(_WindTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_WindTex, u_xlat5.xw, 0.0);
					    u_xlat1.xw = u_xlat1.xx * u_xlat5.xz;
					    u_xlat1.xw = u_xlat1.yy * u_xlat1.xw;
					    u_xlat4.xy = u_xlat9.xx * u_xlat4.xz;
					    u_xlat4.xy = u_xlat1.yy * u_xlat4.xy;
					    u_xlat3.xz = u_xlat4.xy * u_xlat1.zz + u_xlat3.xz;
					    u_xlat1.xy = u_xlat1.xw * u_xlat1.zz + u_xlat3.xz;
					    u_xlat1.xy = u_xlat1.xy * vec2(4.0, 4.0) + vec2(-2.0, -2.0);
					    u_xlat4 = u_xlat1.xyxy * vec4(_WindInterpolator);
					    u_xlat4 = u_xlat4 * vec4(0.300000012, 0.300000012, 0.300000012, 0.300000012);
					    u_xlat4 = in_TANGENT0.xzxz * vec4(0.5, 0.5, 0.5, 0.5) + u_xlat4;
					    u_xlat21.xy = (-in_TEXCOORD0.yy) + vec2(0.949999988, 1.0);
					    u_xlat5 = u_xlat21.yyyy * u_xlat4;
					    u_xlat3.xz = u_xlat4.zw * u_xlat21.yy + in_POSITION0.xz;
					    u_xlat31 = dot(in_TANGENT0.xzxz, u_xlat5);
					    u_xlat31 = (-u_xlat31) + in_TEXCOORD0.y;
					    u_xlat4.x = dot(in_POSITION0.xz, (-u_xlat1.xy));
					    u_xlat1.x = dot(u_xlat1.xy, in_TANGENT0.xz);
					    u_xlat1.x = u_xlat1.x * in_TEXCOORD0.y;
					    u_xlat1.x = dot(u_xlat1.xx, vec2(_WindInterpolator));
					    u_xlat11.x = _SqrtWindTime * 0.600000024 + 10.0;
					    u_xlat11.x = _WindTime * 0.200000003 + u_xlat11.x;
					    u_xlat11.x = (-u_xlat4.x) * _WindInterpolator + u_xlat11.x;
					    u_xlat4.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat4.x = sqrt(u_xlat4.x);
					    u_xlat11.x = (-u_xlat4.x) * 0.699999988 + u_xlat11.x;
					    u_xlat4.x = u_xlat4.x + (-_FogMinRad);
					    vs_TEXCOORD2.x = u_xlat31 * 1.29999995 + u_xlat11.x;
					    u_xlat11.x = u_xlat11.x * 6.28318548;
					    u_xlat11.x = sin(u_xlat11.x);
					    vs_TEXCOORD2.y = u_xlat11.x * 0.0199999996 + u_xlat21.x;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat5 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat5 = unity_ObjectToWorld[0] * u_xlat3.xxxx + u_xlat5;
					    u_xlat5 = unity_ObjectToWorld[2] * u_xlat3.zzzz + u_xlat5;
					    u_xlat5 = u_xlat5 + unity_ObjectToWorld[3];
					    u_xlat6 = u_xlat5.yyyy * unity_MatrixVP[1];
					    u_xlat6 = unity_MatrixVP[0] * u_xlat5.xxxx + u_xlat6;
					    u_xlat6 = unity_MatrixVP[2] * u_xlat5.zzzz + u_xlat6;
					    gl_Position = unity_MatrixVP[3] * u_xlat5.wwww + u_xlat6;
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat11.x = (-u_xlat2.w) * 0.5 + in_POSITION0.y;
					    u_xlat11.x = u_xlat11.x * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat21.x = u_xlat30 * u_xlat2.y;
					    u_xlat14.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat13.x * u_xlat14.x + u_xlat21.x;
					    u_xlat5.y = u_xlat13.x * u_xlat2.x + u_xlat21.x;
					    u_xlat5.z = u_xlat2.z * u_xlat13.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat12.yz = u_xlat5.yx;
					    u_xlat7 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat8.yyy;
					    u_xlat6.xyz = u_xlat8.zzz * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat8.xxx;
					    u_xlat7.xyz = u_xlat8.yyy * u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat30 = u_xlat30 * u_xlat14.y;
					    u_xlat5.y = u_xlat13.x * u_xlat2.x + u_xlat30;
					    u_xlat5.x = u_xlat13.x * u_xlat14.x + u_xlat30;
					    u_xlat12.x = u_xlat13.z * u_xlat14.z;
					    u_xlat7 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat14.xyz = u_xlat0.xxx * u_xlat9.xyz;
					    u_xlat14.xyz = u_xlat0.yyy * u_xlat14.xyz;
					    u_xlat7.xyz = u_xlat8.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat0.yyy * u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat14.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat12.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat12.zx, 0.0);
					    u_xlat5.w = u_xlat12.x;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat8.yyy * u_xlat2.xyz;
					    u_xlat6.xyz = u_xlat8.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat8.yyy * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat6.xyz * u_xlat0.zzz + u_xlat14.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat0.zzz + u_xlat14.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat14.xyz = u_xlat0.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat0.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat8.xxx * u_xlat6.xyz;
					    u_xlat0.xyw = u_xlat0.yyy * u_xlat5.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat14.xyz = u_xlat2.zxw;
					    u_xlat14.xyz = clamp(u_xlat14.xyz, 0.0, 1.0);
					    u_xlat30 = u_xlat2.x + u_xlat2.y;
					    u_xlat2.xyz = u_xlat0.xyz * u_xlat14.xyz;
					    u_xlat0.x = u_xlat2.y + u_xlat2.x;
					    u_xlat0.x = u_xlat14.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.400000006;
					    u_xlat10 = u_xlat11.x;
					    u_xlat10 = clamp(u_xlat10, 0.0, 1.0);
					    u_xlat11.x = (-u_xlat11.x);
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat20 = max(u_xlat10, u_xlat11.x);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat5.x = _Year;
					    u_xlat5.y = 0.0;
					    u_xlat5 = textureLod(_GrassTex, u_xlat5.xy, 0.0);
					    u_xlat14.xyz = vec3(u_xlat10) * u_xlat5.yzx;
					    u_xlat14.xyz = u_xlat5.xyz * vec3(u_xlat20) + u_xlat14.xyz;
					    u_xlat11.xyz = u_xlat11.xxx * u_xlat5.zxy + u_xlat14.xyz;
					    u_xlat14.xyz = (-u_xlat11.xyz) + _SnowColor.xyz;
					    u_xlat11.xyz = vec3(_SnowAmount) * u_xlat14.xyz + u_xlat11.xyz;
					    u_xlat14.xyz = u_xlat11.xyz * _MinAmbientColor.xyz;
					    u_xlat11.xyz = (-_MinAmbientColor.xyz) * u_xlat11.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat11.xyz + u_xlat14.xyz;
					    u_xlat11.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat11.x = inversesqrt(u_xlat11.x);
					    u_xlat5.xyz = u_xlat11.xxx * in_NORMAL0.xyz;
					    u_xlat5.w = (-u_xlat5.x);
					    u_xlat11.xyz = u_xlat5.xyw;
					    u_xlat11.xyz = clamp(u_xlat11.xyz, 0.0, 1.0);
					    u_xlat32 = dot(u_xlat5.xyz, _FlashDir.xyz);
					    u_xlat32 = clamp(u_xlat32, 0.0, 1.0);
					    u_xlat32 = (-u_xlat32) + 1.0;
					    u_xlat11.xy = u_xlat11.xy * u_xlat2.xy;
					    u_xlat11.x = u_xlat11.y + u_xlat11.x;
					    u_xlat11.x = u_xlat2.z * u_xlat11.z + u_xlat11.x;
					    u_xlat30 = u_xlat11.x / u_xlat30;
					    u_xlat11.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat11.xyz * vec3(u_xlat30) + u_xlat0.xyz;
					    u_xlat30 = u_xlat32 * u_xlat32;
					    u_xlat30 = (-u_xlat32) * u_xlat30 + 1.0;
					    u_xlat11.xyz = vec3(u_xlat30) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat11.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = _FoamColor * u_xlat0 + (-_MirrorColor2);
					    u_xlat11.x = in_TEXCOORD0.y + 1.0;
					    u_xlat11.x = u_xlat11.x + (-in_TANGENT0.w);
					    u_xlat11.x = u_xlat11.x * 0.5;
					    u_xlat11.x = u_xlat11.x * u_xlat11.x;
					    u_xlat0 = u_xlat11.xxxx * u_xlat0 + _MirrorColor2;
					    vs_COLOR0.w = u_xlat0.w;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat30)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat30);
					    u_xlat11.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat11.xyz + u_xlat0.xyz;
					    u_xlat11.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat11.xyz = (-u_xlat0.xyz) + u_xlat11.xyz;
					    u_xlat30 = dot(in_POSITION0.xz, in_POSITION0.xz);
					    u_xlat30 = sqrt(u_xlat30);
					    u_xlat30 = u_xlat30 + (-_FogMinRad);
					    u_xlat2.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat30 = u_xlat30 / u_xlat2.x;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    vs_TEXCOORD3.y = u_xlat4.x / u_xlat2.x;
					    vs_TEXCOORD3.y = clamp(vs_TEXCOORD3.y, 0.0, 1.0);
					    vs_COLOR0.xyz = vec3(u_xlat30) * u_xlat11.xyz + u_xlat0.xyz;
					    u_xlat0.xy = _WindDir.xz * vec2(vec2(_MaxWindTime, _MaxWindTime));
					    u_xlat0.xy = (-u_xlat0.xy) * vec2(0.200000003, 0.200000003) + u_xlat3.xz;
					    u_xlat0.xy = u_xlat0.xy * vec2(0.200000003, 0.200000003);
					    vs_COLOR1 = textureLod(_MainTex, u_xlat0.xy, 0.0);
					    u_xlat0.x = in_TEXCOORD0.y * -0.600000024 + 0.5;
					    vs_TEXCOORD3.x = u_xlat1.x + u_xlat0.x;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_SELECTION_ON" }
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
						vec4 unused_0_5[4];
						float _WindInterpolator;
						vec3 _WindDir;
						float _MaxWindTime;
						float _WindTime;
						float _SqrtWindTime;
						vec4 unused_0_11[16];
						vec3 _SunDir;
						vec4 unused_0_13[4];
						vec4 _SideSunColor;
						vec4 unused_0_15[2];
						float _Year;
						vec4 unused_0_17[2];
						vec4 _FoamColor;
						vec4 unused_0_19[2];
						vec4 _MirrorColor2;
						vec4 unused_0_21;
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_25[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_31;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_34[2];
						vec4 _MainTex_ST;
						vec4 unused_0_36;
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
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _WindTex;
					uniform  sampler2D _MainTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					float u_xlat10;
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat17;
					float u_xlat20;
					vec2 u_xlat21;
					float u_xlat30;
					float u_xlat31;
					float u_xlat32;
					void main()
					{
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xyz + in_POSITION0.xyz;
					    u_xlat1.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat0.xyz = in_NORMAL0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat30 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat31 = u_xlat3.y * u_xlat30;
					    u_xlat4.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat13.xz = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.x = u_xlat13.x * u_xlat4.x + u_xlat31;
					    u_xlat5.y = u_xlat13.x * u_xlat3.x + u_xlat31;
					    u_xlat5.z = u_xlat3.z * u_xlat13.z;
					    u_xlat6 = textureLod(_WindTex, u_xlat5.xz, 0.0);
					    u_xlat17.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_WindTex, u_xlat5.yz, 0.0);
					    u_xlat6.xy = u_xlat1.xx * u_xlat6.xz;
					    u_xlat9.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xy = u_xlat6.xy * u_xlat9.yy;
					    u_xlat6.zw = u_xlat8.xz * u_xlat9.xx;
					    u_xlat6 = u_xlat9.zzyy * u_xlat6;
					    u_xlat6.xy = u_xlat6.zw * u_xlat9.zz + u_xlat6.xy;
					    u_xlat31 = u_xlat30 * u_xlat4.y;
					    u_xlat5.y = u_xlat13.x * u_xlat3.x + u_xlat31;
					    u_xlat5.x = u_xlat13.x * u_xlat4.x + u_xlat31;
					    u_xlat17.x = u_xlat4.z * u_xlat13.z;
					    u_xlat4 = textureLod(_WindTex, u_xlat5.yz, 0.0);
					    u_xlat8 = textureLod(_WindTex, u_xlat5.xz, 0.0);
					    u_xlat3.xz = u_xlat1.xx * u_xlat8.xz;
					    u_xlat3.xz = u_xlat1.yy * u_xlat3.xz;
					    u_xlat4.xy = u_xlat9.xx * u_xlat4.xz;
					    u_xlat4.xy = u_xlat1.yy * u_xlat4.xy;
					    u_xlat4.xy = u_xlat4.xy * u_xlat9.zz + u_xlat6.xy;
					    u_xlat3.xz = u_xlat3.xz * u_xlat9.zz + u_xlat4.xy;
					    u_xlat4 = textureLod(_WindTex, u_xlat17.yx, 0.0);
					    u_xlat6 = textureLod(_WindTex, u_xlat17.zx, 0.0);
					    u_xlat5.w = u_xlat17.x;
					    u_xlat4.yw = u_xlat1.xx * u_xlat6.xz;
					    u_xlat4 = u_xlat9.xyxy * u_xlat4;
					    u_xlat4.xz = u_xlat9.yy * u_xlat4.xz;
					    u_xlat3.xz = u_xlat4.xz * u_xlat1.zz + u_xlat3.xz;
					    u_xlat3.xz = u_xlat4.yw * u_xlat1.zz + u_xlat3.xz;
					    u_xlat4 = textureLod(_WindTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_WindTex, u_xlat5.xw, 0.0);
					    u_xlat1.xw = u_xlat1.xx * u_xlat5.xz;
					    u_xlat1.xw = u_xlat1.yy * u_xlat1.xw;
					    u_xlat4.xy = u_xlat9.xx * u_xlat4.xz;
					    u_xlat4.xy = u_xlat1.yy * u_xlat4.xy;
					    u_xlat3.xz = u_xlat4.xy * u_xlat1.zz + u_xlat3.xz;
					    u_xlat1.xy = u_xlat1.xw * u_xlat1.zz + u_xlat3.xz;
					    u_xlat1.xy = u_xlat1.xy * vec2(4.0, 4.0) + vec2(-2.0, -2.0);
					    u_xlat4 = u_xlat1.xyxy * vec4(_WindInterpolator);
					    u_xlat4 = u_xlat4 * vec4(0.300000012, 0.300000012, 0.300000012, 0.300000012);
					    u_xlat4 = in_TANGENT0.xzxz * vec4(0.5, 0.5, 0.5, 0.5) + u_xlat4;
					    u_xlat21.xy = (-in_TEXCOORD0.yy) + vec2(0.949999988, 1.0);
					    u_xlat5 = u_xlat21.yyyy * u_xlat4;
					    u_xlat3.xz = u_xlat4.zw * u_xlat21.yy + in_POSITION0.xz;
					    u_xlat31 = dot(in_TANGENT0.xzxz, u_xlat5);
					    u_xlat31 = (-u_xlat31) + in_TEXCOORD0.y;
					    u_xlat4.x = dot(in_POSITION0.xz, (-u_xlat1.xy));
					    u_xlat1.x = dot(u_xlat1.xy, in_TANGENT0.xz);
					    u_xlat1.x = u_xlat1.x * in_TEXCOORD0.y;
					    u_xlat1.x = dot(u_xlat1.xx, vec2(_WindInterpolator));
					    u_xlat11.x = _SqrtWindTime * 0.600000024 + 10.0;
					    u_xlat11.x = _WindTime * 0.200000003 + u_xlat11.x;
					    u_xlat11.x = (-u_xlat4.x) * _WindInterpolator + u_xlat11.x;
					    u_xlat4.x = dot(u_xlat3.xz, u_xlat3.xz);
					    u_xlat4.x = sqrt(u_xlat4.x);
					    u_xlat11.x = (-u_xlat4.x) * 0.699999988 + u_xlat11.x;
					    u_xlat4.x = u_xlat4.x + (-_FogMinRad);
					    vs_TEXCOORD2.x = u_xlat31 * 1.29999995 + u_xlat11.x;
					    u_xlat11.x = u_xlat11.x * 6.28318548;
					    u_xlat11.x = sin(u_xlat11.x);
					    vs_TEXCOORD2.y = u_xlat11.x * 0.0199999996 + u_xlat21.x;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat5 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat5 = unity_ObjectToWorld[0] * u_xlat3.xxxx + u_xlat5;
					    u_xlat5 = unity_ObjectToWorld[2] * u_xlat3.zzzz + u_xlat5;
					    u_xlat5 = u_xlat5 + unity_ObjectToWorld[3];
					    u_xlat6 = u_xlat5.yyyy * unity_MatrixVP[1];
					    u_xlat6 = unity_MatrixVP[0] * u_xlat5.xxxx + u_xlat6;
					    u_xlat6 = unity_MatrixVP[2] * u_xlat5.zzzz + u_xlat6;
					    gl_Position = unity_MatrixVP[3] * u_xlat5.wwww + u_xlat6;
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat11.x = (-u_xlat2.w) * 0.5 + in_POSITION0.y;
					    u_xlat11.x = u_xlat11.x * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat21.x = u_xlat30 * u_xlat2.y;
					    u_xlat14.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat13.x * u_xlat14.x + u_xlat21.x;
					    u_xlat5.y = u_xlat13.x * u_xlat2.x + u_xlat21.x;
					    u_xlat5.z = u_xlat2.z * u_xlat13.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat12.yz = u_xlat5.yx;
					    u_xlat7 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat8.yyy;
					    u_xlat6.xyz = u_xlat8.zzz * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat8.xxx;
					    u_xlat7.xyz = u_xlat8.yyy * u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat30 = u_xlat30 * u_xlat14.y;
					    u_xlat5.y = u_xlat13.x * u_xlat2.x + u_xlat30;
					    u_xlat5.x = u_xlat13.x * u_xlat14.x + u_xlat30;
					    u_xlat12.x = u_xlat13.z * u_xlat14.z;
					    u_xlat7 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat14.xyz = u_xlat0.xxx * u_xlat9.xyz;
					    u_xlat14.xyz = u_xlat0.yyy * u_xlat14.xyz;
					    u_xlat7.xyz = u_xlat8.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat0.yyy * u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat14.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat12.yx, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat12.zx, 0.0);
					    u_xlat5.w = u_xlat12.x;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat7.xyz;
					    u_xlat2.xyz = u_xlat8.yyy * u_xlat2.xyz;
					    u_xlat6.xyz = u_xlat8.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat8.yyy * u_xlat6.xyz;
					    u_xlat14.xyz = u_xlat6.xyz * u_xlat0.zzz + u_xlat14.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat0.zzz + u_xlat14.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat14.xyz = u_xlat0.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat0.yyy * u_xlat14.xyz;
					    u_xlat5.xyz = u_xlat8.xxx * u_xlat6.xyz;
					    u_xlat0.xyw = u_xlat0.yyy * u_xlat5.xyz;
					    u_xlat0.xyw = u_xlat0.xyw * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat0.zzz + u_xlat0.xyw;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat14.xyz = u_xlat2.zxw;
					    u_xlat14.xyz = clamp(u_xlat14.xyz, 0.0, 1.0);
					    u_xlat30 = u_xlat2.x + u_xlat2.y;
					    u_xlat2.xyz = u_xlat0.xyz * u_xlat14.xyz;
					    u_xlat0.x = u_xlat2.y + u_xlat2.x;
					    u_xlat0.x = u_xlat14.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.400000006;
					    u_xlat10 = u_xlat11.x;
					    u_xlat10 = clamp(u_xlat10, 0.0, 1.0);
					    u_xlat11.x = (-u_xlat11.x);
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat20 = max(u_xlat10, u_xlat11.x);
					    u_xlat20 = (-u_xlat20) + 1.0;
					    u_xlat5.x = _Year;
					    u_xlat5.y = 0.0;
					    u_xlat5 = textureLod(_GrassTex, u_xlat5.xy, 0.0);
					    u_xlat14.xyz = vec3(u_xlat10) * u_xlat5.yzx;
					    u_xlat14.xyz = u_xlat5.xyz * vec3(u_xlat20) + u_xlat14.xyz;
					    u_xlat11.xyz = u_xlat11.xxx * u_xlat5.zxy + u_xlat14.xyz;
					    u_xlat14.xyz = (-u_xlat11.xyz) + _SnowColor.xyz;
					    u_xlat11.xyz = vec3(_SnowAmount) * u_xlat14.xyz + u_xlat11.xyz;
					    u_xlat14.xyz = u_xlat11.xyz * _MinAmbientColor.xyz;
					    u_xlat11.xyz = (-_MinAmbientColor.xyz) * u_xlat11.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat11.xyz + u_xlat14.xyz;
					    u_xlat11.x = dot(in_NORMAL0.xyz, in_NORMAL0.xyz);
					    u_xlat11.x = inversesqrt(u_xlat11.x);
					    u_xlat5.xyz = u_xlat11.xxx * in_NORMAL0.xyz;
					    u_xlat5.w = (-u_xlat5.x);
					    u_xlat11.xyz = u_xlat5.xyw;
					    u_xlat11.xyz = clamp(u_xlat11.xyz, 0.0, 1.0);
					    u_xlat32 = dot(u_xlat5.xyz, _FlashDir.xyz);
					    u_xlat32 = clamp(u_xlat32, 0.0, 1.0);
					    u_xlat32 = (-u_xlat32) + 1.0;
					    u_xlat11.xy = u_xlat11.xy * u_xlat2.xy;
					    u_xlat11.x = u_xlat11.y + u_xlat11.x;
					    u_xlat11.x = u_xlat2.z * u_xlat11.z + u_xlat11.x;
					    u_xlat30 = u_xlat11.x / u_xlat30;
					    u_xlat11.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat11.xyz * vec3(u_xlat30) + u_xlat0.xyz;
					    u_xlat30 = u_xlat32 * u_xlat32;
					    u_xlat30 = (-u_xlat32) * u_xlat30 + 1.0;
					    u_xlat11.xyz = vec3(u_xlat30) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat11.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = _FoamColor * u_xlat0 + (-_MirrorColor2);
					    u_xlat11.x = in_TEXCOORD0.y + 1.0;
					    u_xlat11.x = u_xlat11.x + (-in_TANGENT0.w);
					    u_xlat11.x = u_xlat11.x * 0.5;
					    u_xlat11.x = u_xlat11.x * u_xlat11.x;
					    u_xlat0 = u_xlat11.xxxx * u_xlat0 + _MirrorColor2;
					    vs_COLOR0.w = u_xlat0.w;
					    u_xlat30 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat30)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat30);
					    u_xlat11.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat11.xyz + u_xlat0.xyz;
					    u_xlat11.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat11.xyz = (-u_xlat0.xyz) + u_xlat11.xyz;
					    u_xlat30 = dot(in_POSITION0.xz, in_POSITION0.xz);
					    u_xlat30 = sqrt(u_xlat30);
					    u_xlat30 = u_xlat30 + (-_FogMinRad);
					    u_xlat2.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat30 = u_xlat30 / u_xlat2.x;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    vs_TEXCOORD3.y = u_xlat4.x / u_xlat2.x;
					    vs_TEXCOORD3.y = clamp(vs_TEXCOORD3.y, 0.0, 1.0);
					    vs_COLOR0.xyz = vec3(u_xlat30) * u_xlat11.xyz + u_xlat0.xyz;
					    u_xlat0.xy = _WindDir.xz * vec2(vec2(_MaxWindTime, _MaxWindTime));
					    u_xlat0.xy = (-u_xlat0.xy) * vec2(0.200000003, 0.200000003) + u_xlat3.xz;
					    u_xlat0.xy = u_xlat0.xy * vec2(0.200000003, 0.200000003);
					    vs_COLOR1 = textureLod(_MainTex, u_xlat0.xy, 0.0);
					    u_xlat0.x = in_TEXCOORD0.y * -0.600000024 + 0.5;
					    vs_TEXCOORD3.x = u_xlat1.x + u_xlat0.x;
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
					
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec2 u_xlat1;
					bvec3 u_xlatb1;
					vec3 u_xlat2;
					bvec3 u_xlatb2;
					void main()
					{
					    u_xlat0 = vs_TEXCOORD2.xxxx + vec4(0.5, -0.0, -1.0, -2.0);
					    u_xlat2.xyz = u_xlat0.yzw * vec3(0.333333343, 0.333333343, 0.333333343);
					    u_xlat0.x = fract(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * 2.0 + -1.0;
					    u_xlatb1.xyz = greaterThanEqual(u_xlat2.xyzx, (-u_xlat2.xyzx)).xyz;
					    u_xlat2.xyz = fract(abs(u_xlat2.xyz));
					    {
					        vec3 hlslcc_movcTemp = u_xlat2;
					        hlslcc_movcTemp.x = (u_xlatb1.x) ? u_xlat2.x : (-u_xlat2.x);
					        hlslcc_movcTemp.y = (u_xlatb1.y) ? u_xlat2.y : (-u_xlat2.y);
					        hlslcc_movcTemp.z = (u_xlatb1.z) ? u_xlat2.z : (-u_xlat2.z);
					        u_xlat2 = hlslcc_movcTemp;
					    }
					    u_xlatb2.xyz = greaterThanEqual(vec4(0.333333343, 0.333333343, 0.333333343, 0.333333343), u_xlat2.xyzz).xyz;
					    u_xlat2.x = u_xlatb2.x ? float(1.0) : 0.0;
					    u_xlat2.y = u_xlatb2.y ? float(1.0) : 0.0;
					    u_xlat2.z = u_xlatb2.z ? float(1.0) : 0.0;
					;
					    u_xlat2.xy = u_xlat2.xy * vs_COLOR1.xy;
					    u_xlat2.x = u_xlat2.y + u_xlat2.x;
					    u_xlat2.x = u_xlat2.z * vs_COLOR1.z + u_xlat2.x;
					    u_xlat0.x = abs(u_xlat0.x) + u_xlat2.x;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat0.x = u_xlat0.x + (-vs_TEXCOORD3.x);
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat2.xy = dFdx(vs_TEXCOORD2.xy);
					    u_xlat1.xy = dFdy(vs_TEXCOORD2.xy);
					    u_xlat2.xy = abs(u_xlat2.xy) + abs(u_xlat1.xy);
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = vs_TEXCOORD2.y / u_xlat2.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat0.x = max(u_xlat2.x, u_xlat0.x);
					    u_xlat2.x = u_xlat0.x + -0.00999999978;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat2.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec2 u_xlat1;
					bvec3 u_xlatb1;
					vec3 u_xlat2;
					bvec3 u_xlatb2;
					void main()
					{
					    u_xlat0 = vs_TEXCOORD2.xxxx + vec4(0.5, -0.0, -1.0, -2.0);
					    u_xlat2.xyz = u_xlat0.yzw * vec3(0.333333343, 0.333333343, 0.333333343);
					    u_xlat0.x = fract(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * 2.0 + -1.0;
					    u_xlatb1.xyz = greaterThanEqual(u_xlat2.xyzx, (-u_xlat2.xyzx)).xyz;
					    u_xlat2.xyz = fract(abs(u_xlat2.xyz));
					    {
					        vec3 hlslcc_movcTemp = u_xlat2;
					        hlslcc_movcTemp.x = (u_xlatb1.x) ? u_xlat2.x : (-u_xlat2.x);
					        hlslcc_movcTemp.y = (u_xlatb1.y) ? u_xlat2.y : (-u_xlat2.y);
					        hlslcc_movcTemp.z = (u_xlatb1.z) ? u_xlat2.z : (-u_xlat2.z);
					        u_xlat2 = hlslcc_movcTemp;
					    }
					    u_xlatb2.xyz = greaterThanEqual(vec4(0.333333343, 0.333333343, 0.333333343, 0.333333343), u_xlat2.xyzz).xyz;
					    u_xlat2.x = u_xlatb2.x ? float(1.0) : 0.0;
					    u_xlat2.y = u_xlatb2.y ? float(1.0) : 0.0;
					    u_xlat2.z = u_xlatb2.z ? float(1.0) : 0.0;
					;
					    u_xlat2.xy = u_xlat2.xy * vs_COLOR1.xy;
					    u_xlat2.x = u_xlat2.y + u_xlat2.x;
					    u_xlat2.x = u_xlat2.z * vs_COLOR1.z + u_xlat2.x;
					    u_xlat0.x = abs(u_xlat0.x) + u_xlat2.x;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat0.x = u_xlat0.x + (-vs_TEXCOORD3.x);
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat2.xy = dFdx(vs_TEXCOORD2.xy);
					    u_xlat1.xy = dFdy(vs_TEXCOORD2.xy);
					    u_xlat2.xy = abs(u_xlat2.xy) + abs(u_xlat1.xy);
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = vs_TEXCOORD2.y / u_xlat2.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat0.x = max(u_xlat2.x, u_xlat0.x);
					    u_xlat2.x = u_xlat0.x + -0.00999999978;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat2.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_SELECTION_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec2 u_xlat1;
					bvec3 u_xlatb1;
					vec3 u_xlat2;
					bvec3 u_xlatb2;
					void main()
					{
					    u_xlat0 = vs_TEXCOORD2.xxxx + vec4(0.5, -0.0, -1.0, -2.0);
					    u_xlat2.xyz = u_xlat0.yzw * vec3(0.333333343, 0.333333343, 0.333333343);
					    u_xlat0.x = fract(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * 2.0 + -1.0;
					    u_xlatb1.xyz = greaterThanEqual(u_xlat2.xyzx, (-u_xlat2.xyzx)).xyz;
					    u_xlat2.xyz = fract(abs(u_xlat2.xyz));
					    {
					        vec3 hlslcc_movcTemp = u_xlat2;
					        hlslcc_movcTemp.x = (u_xlatb1.x) ? u_xlat2.x : (-u_xlat2.x);
					        hlslcc_movcTemp.y = (u_xlatb1.y) ? u_xlat2.y : (-u_xlat2.y);
					        hlslcc_movcTemp.z = (u_xlatb1.z) ? u_xlat2.z : (-u_xlat2.z);
					        u_xlat2 = hlslcc_movcTemp;
					    }
					    u_xlatb2.xyz = greaterThanEqual(vec4(0.333333343, 0.333333343, 0.333333343, 0.333333343), u_xlat2.xyzz).xyz;
					    u_xlat2.x = u_xlatb2.x ? float(1.0) : 0.0;
					    u_xlat2.y = u_xlatb2.y ? float(1.0) : 0.0;
					    u_xlat2.z = u_xlatb2.z ? float(1.0) : 0.0;
					;
					    u_xlat2.xy = u_xlat2.xy * vs_COLOR1.xy;
					    u_xlat2.x = u_xlat2.y + u_xlat2.x;
					    u_xlat2.x = u_xlat2.z * vs_COLOR1.z + u_xlat2.x;
					    u_xlat0.x = abs(u_xlat0.x) + u_xlat2.x;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat0.x = u_xlat0.x + (-vs_TEXCOORD3.x);
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat2.xy = dFdx(vs_TEXCOORD2.xy);
					    u_xlat1.xy = dFdy(vs_TEXCOORD2.xy);
					    u_xlat2.xy = abs(u_xlat2.xy) + abs(u_xlat1.xy);
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = vs_TEXCOORD2.y / u_xlat2.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat0.x = max(u_xlat2.x, u_xlat0.x);
					    u_xlat2.x = u_xlat0.x + -0.00999999978;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat2.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_SELECTION_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec2 u_xlat1;
					bvec3 u_xlatb1;
					vec3 u_xlat2;
					bvec3 u_xlatb2;
					void main()
					{
					    u_xlat0 = vs_TEXCOORD2.xxxx + vec4(0.5, -0.0, -1.0, -2.0);
					    u_xlat2.xyz = u_xlat0.yzw * vec3(0.333333343, 0.333333343, 0.333333343);
					    u_xlat0.x = fract(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * 2.0 + -1.0;
					    u_xlatb1.xyz = greaterThanEqual(u_xlat2.xyzx, (-u_xlat2.xyzx)).xyz;
					    u_xlat2.xyz = fract(abs(u_xlat2.xyz));
					    {
					        vec3 hlslcc_movcTemp = u_xlat2;
					        hlslcc_movcTemp.x = (u_xlatb1.x) ? u_xlat2.x : (-u_xlat2.x);
					        hlslcc_movcTemp.y = (u_xlatb1.y) ? u_xlat2.y : (-u_xlat2.y);
					        hlslcc_movcTemp.z = (u_xlatb1.z) ? u_xlat2.z : (-u_xlat2.z);
					        u_xlat2 = hlslcc_movcTemp;
					    }
					    u_xlatb2.xyz = greaterThanEqual(vec4(0.333333343, 0.333333343, 0.333333343, 0.333333343), u_xlat2.xyzz).xyz;
					    u_xlat2.x = u_xlatb2.x ? float(1.0) : 0.0;
					    u_xlat2.y = u_xlatb2.y ? float(1.0) : 0.0;
					    u_xlat2.z = u_xlatb2.z ? float(1.0) : 0.0;
					;
					    u_xlat2.xy = u_xlat2.xy * vs_COLOR1.xy;
					    u_xlat2.x = u_xlat2.y + u_xlat2.x;
					    u_xlat2.x = u_xlat2.z * vs_COLOR1.z + u_xlat2.x;
					    u_xlat0.x = abs(u_xlat0.x) + u_xlat2.x;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat0.x = u_xlat0.x + (-vs_TEXCOORD3.x);
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat2.xy = dFdx(vs_TEXCOORD2.xy);
					    u_xlat1.xy = dFdy(vs_TEXCOORD2.xy);
					    u_xlat2.xy = abs(u_xlat2.xy) + abs(u_xlat1.xy);
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = vs_TEXCOORD2.y / u_xlat2.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat0.x = max(u_xlat2.x, u_xlat0.x);
					    u_xlat2.x = u_xlat0.x + -0.00999999978;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat2.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec2 u_xlat1;
					bvec3 u_xlatb1;
					vec3 u_xlat2;
					bvec3 u_xlatb2;
					void main()
					{
					    u_xlat0 = vs_TEXCOORD2.xxxx + vec4(0.5, -0.0, -1.0, -2.0);
					    u_xlat2.xyz = u_xlat0.yzw * vec3(0.333333343, 0.333333343, 0.333333343);
					    u_xlat0.x = fract(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * 2.0 + -1.0;
					    u_xlatb1.xyz = greaterThanEqual(u_xlat2.xyzx, (-u_xlat2.xyzx)).xyz;
					    u_xlat2.xyz = fract(abs(u_xlat2.xyz));
					    {
					        vec3 hlslcc_movcTemp = u_xlat2;
					        hlslcc_movcTemp.x = (u_xlatb1.x) ? u_xlat2.x : (-u_xlat2.x);
					        hlslcc_movcTemp.y = (u_xlatb1.y) ? u_xlat2.y : (-u_xlat2.y);
					        hlslcc_movcTemp.z = (u_xlatb1.z) ? u_xlat2.z : (-u_xlat2.z);
					        u_xlat2 = hlslcc_movcTemp;
					    }
					    u_xlatb2.xyz = greaterThanEqual(vec4(0.333333343, 0.333333343, 0.333333343, 0.333333343), u_xlat2.xyzz).xyz;
					    u_xlat2.x = u_xlatb2.x ? float(1.0) : 0.0;
					    u_xlat2.y = u_xlatb2.y ? float(1.0) : 0.0;
					    u_xlat2.z = u_xlatb2.z ? float(1.0) : 0.0;
					;
					    u_xlat2.xy = u_xlat2.xy * vs_COLOR1.xy;
					    u_xlat2.x = u_xlat2.y + u_xlat2.x;
					    u_xlat2.x = u_xlat2.z * vs_COLOR1.z + u_xlat2.x;
					    u_xlat0.x = abs(u_xlat0.x) + u_xlat2.x;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat0.x = u_xlat0.x + (-vs_TEXCOORD3.x);
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat2.xy = dFdx(vs_TEXCOORD2.xy);
					    u_xlat1.xy = dFdy(vs_TEXCOORD2.xy);
					    u_xlat2.xy = abs(u_xlat2.xy) + abs(u_xlat1.xy);
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = vs_TEXCOORD2.y / u_xlat2.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat0.x = max(u_xlat2.x, u_xlat0.x);
					    u_xlat2.x = u_xlat0.x + -0.00999999978;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat2.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec2 u_xlat1;
					bvec3 u_xlatb1;
					vec3 u_xlat2;
					bvec3 u_xlatb2;
					void main()
					{
					    u_xlat0 = vs_TEXCOORD2.xxxx + vec4(0.5, -0.0, -1.0, -2.0);
					    u_xlat2.xyz = u_xlat0.yzw * vec3(0.333333343, 0.333333343, 0.333333343);
					    u_xlat0.x = fract(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * 2.0 + -1.0;
					    u_xlatb1.xyz = greaterThanEqual(u_xlat2.xyzx, (-u_xlat2.xyzx)).xyz;
					    u_xlat2.xyz = fract(abs(u_xlat2.xyz));
					    {
					        vec3 hlslcc_movcTemp = u_xlat2;
					        hlslcc_movcTemp.x = (u_xlatb1.x) ? u_xlat2.x : (-u_xlat2.x);
					        hlslcc_movcTemp.y = (u_xlatb1.y) ? u_xlat2.y : (-u_xlat2.y);
					        hlslcc_movcTemp.z = (u_xlatb1.z) ? u_xlat2.z : (-u_xlat2.z);
					        u_xlat2 = hlslcc_movcTemp;
					    }
					    u_xlatb2.xyz = greaterThanEqual(vec4(0.333333343, 0.333333343, 0.333333343, 0.333333343), u_xlat2.xyzz).xyz;
					    u_xlat2.x = u_xlatb2.x ? float(1.0) : 0.0;
					    u_xlat2.y = u_xlatb2.y ? float(1.0) : 0.0;
					    u_xlat2.z = u_xlatb2.z ? float(1.0) : 0.0;
					;
					    u_xlat2.xy = u_xlat2.xy * vs_COLOR1.xy;
					    u_xlat2.x = u_xlat2.y + u_xlat2.x;
					    u_xlat2.x = u_xlat2.z * vs_COLOR1.z + u_xlat2.x;
					    u_xlat0.x = abs(u_xlat0.x) + u_xlat2.x;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat0.x = u_xlat0.x + (-vs_TEXCOORD3.x);
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat2.xy = dFdx(vs_TEXCOORD2.xy);
					    u_xlat1.xy = dFdy(vs_TEXCOORD2.xy);
					    u_xlat2.xy = abs(u_xlat2.xy) + abs(u_xlat1.xy);
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = vs_TEXCOORD2.y / u_xlat2.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat0.x = max(u_xlat2.x, u_xlat0.x);
					    u_xlat2.x = u_xlat0.x + -0.00999999978;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat2.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_SELECTION_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec2 u_xlat1;
					bvec3 u_xlatb1;
					vec3 u_xlat2;
					bvec3 u_xlatb2;
					void main()
					{
					    u_xlat0 = vs_TEXCOORD2.xxxx + vec4(0.5, -0.0, -1.0, -2.0);
					    u_xlat2.xyz = u_xlat0.yzw * vec3(0.333333343, 0.333333343, 0.333333343);
					    u_xlat0.x = fract(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * 2.0 + -1.0;
					    u_xlatb1.xyz = greaterThanEqual(u_xlat2.xyzx, (-u_xlat2.xyzx)).xyz;
					    u_xlat2.xyz = fract(abs(u_xlat2.xyz));
					    {
					        vec3 hlslcc_movcTemp = u_xlat2;
					        hlslcc_movcTemp.x = (u_xlatb1.x) ? u_xlat2.x : (-u_xlat2.x);
					        hlslcc_movcTemp.y = (u_xlatb1.y) ? u_xlat2.y : (-u_xlat2.y);
					        hlslcc_movcTemp.z = (u_xlatb1.z) ? u_xlat2.z : (-u_xlat2.z);
					        u_xlat2 = hlslcc_movcTemp;
					    }
					    u_xlatb2.xyz = greaterThanEqual(vec4(0.333333343, 0.333333343, 0.333333343, 0.333333343), u_xlat2.xyzz).xyz;
					    u_xlat2.x = u_xlatb2.x ? float(1.0) : 0.0;
					    u_xlat2.y = u_xlatb2.y ? float(1.0) : 0.0;
					    u_xlat2.z = u_xlatb2.z ? float(1.0) : 0.0;
					;
					    u_xlat2.xy = u_xlat2.xy * vs_COLOR1.xy;
					    u_xlat2.x = u_xlat2.y + u_xlat2.x;
					    u_xlat2.x = u_xlat2.z * vs_COLOR1.z + u_xlat2.x;
					    u_xlat0.x = abs(u_xlat0.x) + u_xlat2.x;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat0.x = u_xlat0.x + (-vs_TEXCOORD3.x);
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat2.xy = dFdx(vs_TEXCOORD2.xy);
					    u_xlat1.xy = dFdy(vs_TEXCOORD2.xy);
					    u_xlat2.xy = abs(u_xlat2.xy) + abs(u_xlat1.xy);
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = vs_TEXCOORD2.y / u_xlat2.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat0.x = max(u_xlat2.x, u_xlat0.x);
					    u_xlat2.x = u_xlat0.x + -0.00999999978;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat2.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_SELECTION_ON" }
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec2 u_xlat1;
					bvec3 u_xlatb1;
					vec3 u_xlat2;
					bvec3 u_xlatb2;
					void main()
					{
					    u_xlat0 = vs_TEXCOORD2.xxxx + vec4(0.5, -0.0, -1.0, -2.0);
					    u_xlat2.xyz = u_xlat0.yzw * vec3(0.333333343, 0.333333343, 0.333333343);
					    u_xlat0.x = fract(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * 2.0 + -1.0;
					    u_xlatb1.xyz = greaterThanEqual(u_xlat2.xyzx, (-u_xlat2.xyzx)).xyz;
					    u_xlat2.xyz = fract(abs(u_xlat2.xyz));
					    {
					        vec3 hlslcc_movcTemp = u_xlat2;
					        hlslcc_movcTemp.x = (u_xlatb1.x) ? u_xlat2.x : (-u_xlat2.x);
					        hlslcc_movcTemp.y = (u_xlatb1.y) ? u_xlat2.y : (-u_xlat2.y);
					        hlslcc_movcTemp.z = (u_xlatb1.z) ? u_xlat2.z : (-u_xlat2.z);
					        u_xlat2 = hlslcc_movcTemp;
					    }
					    u_xlatb2.xyz = greaterThanEqual(vec4(0.333333343, 0.333333343, 0.333333343, 0.333333343), u_xlat2.xyzz).xyz;
					    u_xlat2.x = u_xlatb2.x ? float(1.0) : 0.0;
					    u_xlat2.y = u_xlatb2.y ? float(1.0) : 0.0;
					    u_xlat2.z = u_xlatb2.z ? float(1.0) : 0.0;
					;
					    u_xlat2.xy = u_xlat2.xy * vs_COLOR1.xy;
					    u_xlat2.x = u_xlat2.y + u_xlat2.x;
					    u_xlat2.x = u_xlat2.z * vs_COLOR1.z + u_xlat2.x;
					    u_xlat0.x = abs(u_xlat0.x) + u_xlat2.x;
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat0.x = u_xlat0.x + (-vs_TEXCOORD3.x);
					    u_xlat0.x = u_xlat0.x + -0.5;
					    u_xlat2.xy = dFdx(vs_TEXCOORD2.xy);
					    u_xlat1.xy = dFdy(vs_TEXCOORD2.xy);
					    u_xlat2.xy = abs(u_xlat2.xy) + abs(u_xlat1.xy);
					    u_xlat0.x = u_xlat0.x / u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.x = vs_TEXCOORD2.y / u_xlat2.y;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat0.x = max(u_xlat2.x, u_xlat0.x);
					    u_xlat2.x = u_xlat0.x + -0.00999999978;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat2.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
			}
		}
	}
}