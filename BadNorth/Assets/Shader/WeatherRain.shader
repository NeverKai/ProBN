Shader "Weather/Rain" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_PosTex0 ("Position Texture 0", 2D) = "white" {}
		_PosTex1 ("Position Texture 1", 2D) = "white" {}
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
			GpuProgramID 47717
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
						vec4 unused_0_2[10];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_5;
						mat4x4 _Tex2World;
						vec4 unused_0_7[32];
						float _CameraUpScale;
						vec4 unused_0_9[2];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_12[2];
						float _AAFactor;
						vec4 unused_0_14[4];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_17;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_20;
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
					uniform  sampler2D _TopdownTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec3 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat9;
					vec3 u_xlat13;
					vec2 u_xlat16;
					vec2 u_xlat18;
					bool u_xlatb18;
					float u_xlat28;
					float u_xlat29;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat9.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat9.x = (-_Size.x) + _Size.y;
					    u_xlat9.x = in_TEXCOORD3.y * u_xlat9.x + _Size.x;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x + (-u_xlat9.x);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat9.x;
					    u_xlat9.x = _Interpolator + -1.0;
					    u_xlat9.x = -abs(u_xlat9.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat9.xxxx * u_xlat3 + u_xlat2;
					    u_xlat9.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat9.x = dot(u_xlat9.xy, u_xlat9.xy);
					    u_xlat9.x = sqrt(u_xlat9.x);
					    u_xlat9.x = u_xlat9.x + u_xlat9.x;
					    u_xlat9.x = min(u_xlat9.x, 1.0);
					    u_xlat9.x = (-u_xlat9.x) + 1.0;
					    u_xlat18.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat9.x = dot(u_xlat9.xx, u_xlat18.xx);
					    u_xlat9.x = clamp(u_xlat9.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xyz = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat9.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat9.xy = u_xlat9.xy / _AoTexVolume.xz;
					    u_xlat4 = textureLod(_TopdownTex, u_xlat9.xy, 0.0);
					    u_xlat9.x = u_xlat4.w * 8.0 + _WaterLevel;
					    u_xlatb18 = _WaterLevel>=u_xlat9.x;
					    u_xlat28 = u_xlatb18 ? 1.0 : float(0.0);
					    u_xlat18.x = (u_xlatb18) ? 0.0 : 1.0;
					    u_xlat4.xy = u_xlat18.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat28 = u_xlat28 * u_xlat4.x;
					    u_xlat29 = u_xlat4.y * u_xlat28 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat29 = u_xlat3.w * u_xlat3.w;
					    u_xlat29 = (-u_xlat29) * u_xlat29 + 1.0;
					    u_xlat29 = u_xlat18.x * u_xlat29;
					    u_xlat29 = u_xlat29 * 6.0 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat13.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat13.xyz = vec3(_WindInterpolator) * u_xlat13.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat13.xyz = u_xlat13.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat5.z = unity_MatrixV[0].z;
					    u_xlat5.x = unity_MatrixV[1].z;
					    u_xlat5.y = unity_MatrixV[2].z;
					    u_xlat29 = dot((-u_xlat5.yzx), u_xlat13.xyz);
					    u_xlat6.xyz = u_xlat5.zxy * vec3(u_xlat29) + u_xlat13.yzx;
					    u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
					    u_xlat29 = sqrt(u_xlat29);
					    u_xlat32 = u_xlat29 + 1.0;
					    u_xlat6.xyz = u_xlat13.xyz * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat13.zxy * u_xlat5.yzx + (-u_xlat6.xyz);
					    u_xlat6.x = dot(u_xlat13.xyz, u_xlat13.xyz);
					    u_xlat6.x = inversesqrt(u_xlat6.x);
					    u_xlat13.xyz = u_xlat13.xyz * u_xlat6.xxx;
					    u_xlat6.xyz = u_xlat13.xyz / vec3(u_xlat32);
					    u_xlat7.x = unity_MatrixV[0].x;
					    u_xlat7.y = unity_MatrixV[1].x;
					    u_xlat7.z = unity_MatrixV[2].x;
					    u_xlat8.xyz = vec3(u_xlat28) * u_xlat7.xyz;
					    u_xlat32 = (-u_xlat4.x) + 1.0;
					    u_xlat6.xyz = u_xlat6.xyz * vec3(u_xlat32) + u_xlat8.xyz;
					    u_xlat33 = u_xlat18.x * u_xlat4.x;
					    u_xlat6.xyz = u_xlat7.xyz * vec3(u_xlat33) + u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * in_TEXCOORD1.xxx;
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat7.x = u_xlat18.y * u_xlat18.y;
					    u_xlat7.x = u_xlat18.y * u_xlat7.x;
					    u_xlat16.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat7.xw = u_xlat7.xx * u_xlat16.xy;
					    u_xlat7.xw = u_xlat18.xx * u_xlat7.xw;
					    u_xlat18.x = u_xlat18.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat18.y * u_xlat33 + u_xlat32;
					    u_xlat18.xy = u_xlat18.xx * u_xlat16.xy;
					    u_xlat18.xy = u_xlat18.xy * vec2(u_xlat28) + u_xlat3.xz;
					    u_xlat7.xz = u_xlat7.xw * vec2(8.0, 8.0) + u_xlat18.xy;
					    u_xlat18.x = (-u_xlat3.y) + u_xlat9.x;
					    u_xlat7.y = u_xlat4.x * u_xlat18.x + u_xlat3.y;
					    u_xlat18.x = (-u_xlat9.x) + u_xlat3.y;
					    u_xlat9.x = u_xlat9.x + -0.100000001;
					    u_xlat9.x = max(u_xlat9.x, _WaterLevel);
					    u_xlat18.x = u_xlat18.x + u_xlat18.x;
					    u_xlat18.x = clamp(u_xlat18.x, 0.0, 1.0);
					    u_xlat18.x = u_xlat4.x * u_xlat18.x;
					    u_xlat18.x = (-u_xlat18.x) * _WindInterpolator + 1.0;
					    u_xlat8.w = u_xlat18.x * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat5.xyz * u_xlat13.zxy;
					    u_xlat4.xyz = u_xlat13.yzx * u_xlat5.yzx + (-u_xlat6.xyz);
					    u_xlat18.x = _WindInterpolator * 2.0 + 8.0;
					    u_xlat18.x = u_xlat29 * u_xlat18.x + 1.0;
					    u_xlat4.xyz = u_xlat18.xxx * u_xlat4.xyz;
					    u_xlat5.x = (-unity_MatrixV[2].x);
					    u_xlat5.y = 0.0;
					    u_xlat5.z = unity_MatrixV[0].x;
					    u_xlat5.xyz = vec3(u_xlat28) * u_xlat5.xyz;
					    vs_COLOR1.y = u_xlat28;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(u_xlat32) + u_xlat5.xyz;
					    u_xlat5.y = u_xlat33 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat33;
					    u_xlat5.x = float(0.0);
					    u_xlat5.z = float(0.0);
					    u_xlat4.xyz = u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * in_TEXCOORD1.yyy;
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.xzw * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat4.w = max(u_xlat9.x, u_xlat4.y);
					    u_xlat0 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat0 = _Tex2World[0] * u_xlat2.xxxx + u_xlat0;
					    u_xlat0 = _Tex2World[2] * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + _Tex2World[3];
					    u_xlat2 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat2 = _Tex2World[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat2;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat0 = u_xlat0 + (-u_xlat1);
					    u_xlat0.x = dot(u_xlat0, u_xlat0);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlatb0 = 2.0<u_xlat0.x;
					    u_xlat0.xyz = (bool(u_xlatb0)) ? vec3(0.0, 0.0, 0.0) : u_xlat4.xwz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat8.xyz = in_COLOR0.xyz;
					    u_xlat0 = u_xlat0.xxxx * u_xlat8;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[10];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_5;
						mat4x4 _Tex2World;
						vec4 unused_0_7[32];
						float _CameraUpScale;
						vec4 unused_0_9[2];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_12[2];
						float _AAFactor;
						vec4 unused_0_14[4];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_17;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_20;
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
					uniform  sampler2D _TopdownTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec3 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat9;
					vec3 u_xlat13;
					vec2 u_xlat16;
					vec2 u_xlat18;
					bool u_xlatb18;
					float u_xlat28;
					float u_xlat29;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat9.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat9.x = (-_Size.x) + _Size.y;
					    u_xlat9.x = in_TEXCOORD3.y * u_xlat9.x + _Size.x;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x + (-u_xlat9.x);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat9.x;
					    u_xlat9.x = _Interpolator + -1.0;
					    u_xlat9.x = -abs(u_xlat9.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat9.xxxx * u_xlat3 + u_xlat2;
					    u_xlat9.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat9.x = dot(u_xlat9.xy, u_xlat9.xy);
					    u_xlat9.x = sqrt(u_xlat9.x);
					    u_xlat9.x = u_xlat9.x + u_xlat9.x;
					    u_xlat9.x = min(u_xlat9.x, 1.0);
					    u_xlat9.x = (-u_xlat9.x) + 1.0;
					    u_xlat18.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat9.x = dot(u_xlat9.xx, u_xlat18.xx);
					    u_xlat9.x = clamp(u_xlat9.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xyz = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat9.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat9.xy = u_xlat9.xy / _AoTexVolume.xz;
					    u_xlat4 = textureLod(_TopdownTex, u_xlat9.xy, 0.0);
					    u_xlat9.x = u_xlat4.w * 8.0 + _WaterLevel;
					    u_xlatb18 = _WaterLevel>=u_xlat9.x;
					    u_xlat28 = u_xlatb18 ? 1.0 : float(0.0);
					    u_xlat18.x = (u_xlatb18) ? 0.0 : 1.0;
					    u_xlat4.xy = u_xlat18.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat28 = u_xlat28 * u_xlat4.x;
					    u_xlat29 = u_xlat4.y * u_xlat28 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat29 = u_xlat3.w * u_xlat3.w;
					    u_xlat29 = (-u_xlat29) * u_xlat29 + 1.0;
					    u_xlat29 = u_xlat18.x * u_xlat29;
					    u_xlat29 = u_xlat29 * 6.0 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat13.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat13.xyz = vec3(_WindInterpolator) * u_xlat13.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat13.xyz = u_xlat13.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat5.z = unity_MatrixV[0].z;
					    u_xlat5.x = unity_MatrixV[1].z;
					    u_xlat5.y = unity_MatrixV[2].z;
					    u_xlat29 = dot((-u_xlat5.yzx), u_xlat13.xyz);
					    u_xlat6.xyz = u_xlat5.zxy * vec3(u_xlat29) + u_xlat13.yzx;
					    u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
					    u_xlat29 = sqrt(u_xlat29);
					    u_xlat32 = u_xlat29 + 1.0;
					    u_xlat6.xyz = u_xlat13.xyz * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat13.zxy * u_xlat5.yzx + (-u_xlat6.xyz);
					    u_xlat6.x = dot(u_xlat13.xyz, u_xlat13.xyz);
					    u_xlat6.x = inversesqrt(u_xlat6.x);
					    u_xlat13.xyz = u_xlat13.xyz * u_xlat6.xxx;
					    u_xlat6.xyz = u_xlat13.xyz / vec3(u_xlat32);
					    u_xlat7.x = unity_MatrixV[0].x;
					    u_xlat7.y = unity_MatrixV[1].x;
					    u_xlat7.z = unity_MatrixV[2].x;
					    u_xlat8.xyz = vec3(u_xlat28) * u_xlat7.xyz;
					    u_xlat32 = (-u_xlat4.x) + 1.0;
					    u_xlat6.xyz = u_xlat6.xyz * vec3(u_xlat32) + u_xlat8.xyz;
					    u_xlat33 = u_xlat18.x * u_xlat4.x;
					    u_xlat6.xyz = u_xlat7.xyz * vec3(u_xlat33) + u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * in_TEXCOORD1.xxx;
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat7.x = u_xlat18.y * u_xlat18.y;
					    u_xlat7.x = u_xlat18.y * u_xlat7.x;
					    u_xlat16.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat7.xw = u_xlat7.xx * u_xlat16.xy;
					    u_xlat7.xw = u_xlat18.xx * u_xlat7.xw;
					    u_xlat18.x = u_xlat18.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat18.y * u_xlat33 + u_xlat32;
					    u_xlat18.xy = u_xlat18.xx * u_xlat16.xy;
					    u_xlat18.xy = u_xlat18.xy * vec2(u_xlat28) + u_xlat3.xz;
					    u_xlat7.xz = u_xlat7.xw * vec2(8.0, 8.0) + u_xlat18.xy;
					    u_xlat18.x = (-u_xlat3.y) + u_xlat9.x;
					    u_xlat7.y = u_xlat4.x * u_xlat18.x + u_xlat3.y;
					    u_xlat18.x = (-u_xlat9.x) + u_xlat3.y;
					    u_xlat9.x = u_xlat9.x + -0.100000001;
					    u_xlat9.x = max(u_xlat9.x, _WaterLevel);
					    u_xlat18.x = u_xlat18.x + u_xlat18.x;
					    u_xlat18.x = clamp(u_xlat18.x, 0.0, 1.0);
					    u_xlat18.x = u_xlat4.x * u_xlat18.x;
					    u_xlat18.x = (-u_xlat18.x) * _WindInterpolator + 1.0;
					    u_xlat8.w = u_xlat18.x * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat5.xyz * u_xlat13.zxy;
					    u_xlat4.xyz = u_xlat13.yzx * u_xlat5.yzx + (-u_xlat6.xyz);
					    u_xlat18.x = _WindInterpolator * 2.0 + 8.0;
					    u_xlat18.x = u_xlat29 * u_xlat18.x + 1.0;
					    u_xlat4.xyz = u_xlat18.xxx * u_xlat4.xyz;
					    u_xlat5.x = (-unity_MatrixV[2].x);
					    u_xlat5.y = 0.0;
					    u_xlat5.z = unity_MatrixV[0].x;
					    u_xlat5.xyz = vec3(u_xlat28) * u_xlat5.xyz;
					    vs_COLOR1.y = u_xlat28;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(u_xlat32) + u_xlat5.xyz;
					    u_xlat5.y = u_xlat33 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat33;
					    u_xlat5.x = float(0.0);
					    u_xlat5.z = float(0.0);
					    u_xlat4.xyz = u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * in_TEXCOORD1.yyy;
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.xzw * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat4.w = max(u_xlat9.x, u_xlat4.y);
					    u_xlat0 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat0 = _Tex2World[0] * u_xlat2.xxxx + u_xlat0;
					    u_xlat0 = _Tex2World[2] * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + _Tex2World[3];
					    u_xlat2 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat2 = _Tex2World[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat2;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat0 = u_xlat0 + (-u_xlat1);
					    u_xlat0.x = dot(u_xlat0, u_xlat0);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlatb0 = 2.0<u_xlat0.x;
					    u_xlat0.xyz = (bool(u_xlatb0)) ? vec3(0.0, 0.0, 0.0) : u_xlat4.xwz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat8.xyz = in_COLOR0.xyz;
					    u_xlat0 = u_xlat0.xxxx * u_xlat8;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[10];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_5;
						mat4x4 _Tex2World;
						vec4 unused_0_7[32];
						float _CameraUpScale;
						vec4 unused_0_9[2];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_12[2];
						float _AAFactor;
						vec4 unused_0_14[4];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_17;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_20;
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
					uniform  sampler2D _TopdownTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec3 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat9;
					vec3 u_xlat13;
					vec2 u_xlat16;
					vec2 u_xlat18;
					bool u_xlatb18;
					float u_xlat28;
					float u_xlat29;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat9.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat9.x = (-_Size.x) + _Size.y;
					    u_xlat9.x = in_TEXCOORD3.y * u_xlat9.x + _Size.x;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x + (-u_xlat9.x);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat9.x;
					    u_xlat9.x = _Interpolator + -1.0;
					    u_xlat9.x = -abs(u_xlat9.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat9.xxxx * u_xlat3 + u_xlat2;
					    u_xlat9.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat9.x = dot(u_xlat9.xy, u_xlat9.xy);
					    u_xlat9.x = sqrt(u_xlat9.x);
					    u_xlat9.x = u_xlat9.x + u_xlat9.x;
					    u_xlat9.x = min(u_xlat9.x, 1.0);
					    u_xlat9.x = (-u_xlat9.x) + 1.0;
					    u_xlat18.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat9.x = dot(u_xlat9.xx, u_xlat18.xx);
					    u_xlat9.x = clamp(u_xlat9.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xyz = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat9.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat9.xy = u_xlat9.xy / _AoTexVolume.xz;
					    u_xlat4 = textureLod(_TopdownTex, u_xlat9.xy, 0.0);
					    u_xlat9.x = u_xlat4.w * 8.0 + _WaterLevel;
					    u_xlatb18 = _WaterLevel>=u_xlat9.x;
					    u_xlat28 = u_xlatb18 ? 1.0 : float(0.0);
					    u_xlat18.x = (u_xlatb18) ? 0.0 : 1.0;
					    u_xlat4.xy = u_xlat18.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat28 = u_xlat28 * u_xlat4.x;
					    u_xlat29 = u_xlat4.y * u_xlat28 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat29 = u_xlat3.w * u_xlat3.w;
					    u_xlat29 = (-u_xlat29) * u_xlat29 + 1.0;
					    u_xlat29 = u_xlat18.x * u_xlat29;
					    u_xlat29 = u_xlat29 * 6.0 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat13.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat13.xyz = vec3(_WindInterpolator) * u_xlat13.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat13.xyz = u_xlat13.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat5.z = unity_MatrixV[0].z;
					    u_xlat5.x = unity_MatrixV[1].z;
					    u_xlat5.y = unity_MatrixV[2].z;
					    u_xlat29 = dot((-u_xlat5.yzx), u_xlat13.xyz);
					    u_xlat6.xyz = u_xlat5.zxy * vec3(u_xlat29) + u_xlat13.yzx;
					    u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
					    u_xlat29 = sqrt(u_xlat29);
					    u_xlat32 = u_xlat29 + 1.0;
					    u_xlat6.xyz = u_xlat13.xyz * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat13.zxy * u_xlat5.yzx + (-u_xlat6.xyz);
					    u_xlat6.x = dot(u_xlat13.xyz, u_xlat13.xyz);
					    u_xlat6.x = inversesqrt(u_xlat6.x);
					    u_xlat13.xyz = u_xlat13.xyz * u_xlat6.xxx;
					    u_xlat6.xyz = u_xlat13.xyz / vec3(u_xlat32);
					    u_xlat7.x = unity_MatrixV[0].x;
					    u_xlat7.y = unity_MatrixV[1].x;
					    u_xlat7.z = unity_MatrixV[2].x;
					    u_xlat8.xyz = vec3(u_xlat28) * u_xlat7.xyz;
					    u_xlat32 = (-u_xlat4.x) + 1.0;
					    u_xlat6.xyz = u_xlat6.xyz * vec3(u_xlat32) + u_xlat8.xyz;
					    u_xlat33 = u_xlat18.x * u_xlat4.x;
					    u_xlat6.xyz = u_xlat7.xyz * vec3(u_xlat33) + u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * in_TEXCOORD1.xxx;
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat7.x = u_xlat18.y * u_xlat18.y;
					    u_xlat7.x = u_xlat18.y * u_xlat7.x;
					    u_xlat16.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat7.xw = u_xlat7.xx * u_xlat16.xy;
					    u_xlat7.xw = u_xlat18.xx * u_xlat7.xw;
					    u_xlat18.x = u_xlat18.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat18.y * u_xlat33 + u_xlat32;
					    u_xlat18.xy = u_xlat18.xx * u_xlat16.xy;
					    u_xlat18.xy = u_xlat18.xy * vec2(u_xlat28) + u_xlat3.xz;
					    u_xlat7.xz = u_xlat7.xw * vec2(8.0, 8.0) + u_xlat18.xy;
					    u_xlat18.x = (-u_xlat3.y) + u_xlat9.x;
					    u_xlat7.y = u_xlat4.x * u_xlat18.x + u_xlat3.y;
					    u_xlat18.x = (-u_xlat9.x) + u_xlat3.y;
					    u_xlat9.x = u_xlat9.x + -0.100000001;
					    u_xlat9.x = max(u_xlat9.x, _WaterLevel);
					    u_xlat18.x = u_xlat18.x + u_xlat18.x;
					    u_xlat18.x = clamp(u_xlat18.x, 0.0, 1.0);
					    u_xlat18.x = u_xlat4.x * u_xlat18.x;
					    u_xlat18.x = (-u_xlat18.x) * _WindInterpolator + 1.0;
					    u_xlat8.w = u_xlat18.x * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat5.xyz * u_xlat13.zxy;
					    u_xlat4.xyz = u_xlat13.yzx * u_xlat5.yzx + (-u_xlat6.xyz);
					    u_xlat18.x = _WindInterpolator * 2.0 + 8.0;
					    u_xlat18.x = u_xlat29 * u_xlat18.x + 1.0;
					    u_xlat4.xyz = u_xlat18.xxx * u_xlat4.xyz;
					    u_xlat5.x = (-unity_MatrixV[2].x);
					    u_xlat5.y = 0.0;
					    u_xlat5.z = unity_MatrixV[0].x;
					    u_xlat5.xyz = vec3(u_xlat28) * u_xlat5.xyz;
					    vs_COLOR1.y = u_xlat28;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(u_xlat32) + u_xlat5.xyz;
					    u_xlat5.y = u_xlat33 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat33;
					    u_xlat5.x = float(0.0);
					    u_xlat5.z = float(0.0);
					    u_xlat4.xyz = u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * in_TEXCOORD1.yyy;
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.xzw * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat4.w = max(u_xlat9.x, u_xlat4.y);
					    u_xlat0 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat0 = _Tex2World[0] * u_xlat2.xxxx + u_xlat0;
					    u_xlat0 = _Tex2World[2] * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + _Tex2World[3];
					    u_xlat2 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat2 = _Tex2World[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat2;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat0 = u_xlat0 + (-u_xlat1);
					    u_xlat0.x = dot(u_xlat0, u_xlat0);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlatb0 = 2.0<u_xlat0.x;
					    u_xlat0.xyz = (bool(u_xlatb0)) ? vec3(0.0, 0.0, 0.0) : u_xlat4.xwz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat8.xyz = in_COLOR0.xyz;
					    u_xlat0 = u_xlat0.xxxx * u_xlat8;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[10];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_5;
						mat4x4 _Tex2World;
						vec4 unused_0_7[32];
						float _CameraUpScale;
						vec4 unused_0_9[2];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_12[2];
						float _AAFactor;
						vec4 unused_0_14[4];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_17;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_20;
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
					uniform  sampler2D _TopdownTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec3 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat9;
					vec3 u_xlat13;
					vec2 u_xlat16;
					vec2 u_xlat18;
					bool u_xlatb18;
					float u_xlat28;
					float u_xlat29;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat9.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat9.x = (-_Size.x) + _Size.y;
					    u_xlat9.x = in_TEXCOORD3.y * u_xlat9.x + _Size.x;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x + (-u_xlat9.x);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat9.x;
					    u_xlat9.x = _Interpolator + -1.0;
					    u_xlat9.x = -abs(u_xlat9.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat9.xxxx * u_xlat3 + u_xlat2;
					    u_xlat9.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat9.x = dot(u_xlat9.xy, u_xlat9.xy);
					    u_xlat9.x = sqrt(u_xlat9.x);
					    u_xlat9.x = u_xlat9.x + u_xlat9.x;
					    u_xlat9.x = min(u_xlat9.x, 1.0);
					    u_xlat9.x = (-u_xlat9.x) + 1.0;
					    u_xlat18.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat9.x = dot(u_xlat9.xx, u_xlat18.xx);
					    u_xlat9.x = clamp(u_xlat9.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xyz = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat9.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat9.xy = u_xlat9.xy / _AoTexVolume.xz;
					    u_xlat4 = textureLod(_TopdownTex, u_xlat9.xy, 0.0);
					    u_xlat9.x = u_xlat4.w * 8.0 + _WaterLevel;
					    u_xlatb18 = _WaterLevel>=u_xlat9.x;
					    u_xlat28 = u_xlatb18 ? 1.0 : float(0.0);
					    u_xlat18.x = (u_xlatb18) ? 0.0 : 1.0;
					    u_xlat4.xy = u_xlat18.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat28 = u_xlat28 * u_xlat4.x;
					    u_xlat29 = u_xlat4.y * u_xlat28 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat29 = u_xlat3.w * u_xlat3.w;
					    u_xlat29 = (-u_xlat29) * u_xlat29 + 1.0;
					    u_xlat29 = u_xlat18.x * u_xlat29;
					    u_xlat29 = u_xlat29 * 6.0 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat13.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat13.xyz = vec3(_WindInterpolator) * u_xlat13.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat13.xyz = u_xlat13.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat5.z = unity_MatrixV[0].z;
					    u_xlat5.x = unity_MatrixV[1].z;
					    u_xlat5.y = unity_MatrixV[2].z;
					    u_xlat29 = dot((-u_xlat5.yzx), u_xlat13.xyz);
					    u_xlat6.xyz = u_xlat5.zxy * vec3(u_xlat29) + u_xlat13.yzx;
					    u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
					    u_xlat29 = sqrt(u_xlat29);
					    u_xlat32 = u_xlat29 + 1.0;
					    u_xlat6.xyz = u_xlat13.xyz * u_xlat5.xyz;
					    u_xlat13.xyz = u_xlat13.zxy * u_xlat5.yzx + (-u_xlat6.xyz);
					    u_xlat6.x = dot(u_xlat13.xyz, u_xlat13.xyz);
					    u_xlat6.x = inversesqrt(u_xlat6.x);
					    u_xlat13.xyz = u_xlat13.xyz * u_xlat6.xxx;
					    u_xlat6.xyz = u_xlat13.xyz / vec3(u_xlat32);
					    u_xlat7.x = unity_MatrixV[0].x;
					    u_xlat7.y = unity_MatrixV[1].x;
					    u_xlat7.z = unity_MatrixV[2].x;
					    u_xlat8.xyz = vec3(u_xlat28) * u_xlat7.xyz;
					    u_xlat32 = (-u_xlat4.x) + 1.0;
					    u_xlat6.xyz = u_xlat6.xyz * vec3(u_xlat32) + u_xlat8.xyz;
					    u_xlat33 = u_xlat18.x * u_xlat4.x;
					    u_xlat6.xyz = u_xlat7.xyz * vec3(u_xlat33) + u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * in_TEXCOORD1.xxx;
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat7.x = u_xlat18.y * u_xlat18.y;
					    u_xlat7.x = u_xlat18.y * u_xlat7.x;
					    u_xlat16.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat7.xw = u_xlat7.xx * u_xlat16.xy;
					    u_xlat7.xw = u_xlat18.xx * u_xlat7.xw;
					    u_xlat18.x = u_xlat18.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat18.y * u_xlat33 + u_xlat32;
					    u_xlat18.xy = u_xlat18.xx * u_xlat16.xy;
					    u_xlat18.xy = u_xlat18.xy * vec2(u_xlat28) + u_xlat3.xz;
					    u_xlat7.xz = u_xlat7.xw * vec2(8.0, 8.0) + u_xlat18.xy;
					    u_xlat18.x = (-u_xlat3.y) + u_xlat9.x;
					    u_xlat7.y = u_xlat4.x * u_xlat18.x + u_xlat3.y;
					    u_xlat18.x = (-u_xlat9.x) + u_xlat3.y;
					    u_xlat9.x = u_xlat9.x + -0.100000001;
					    u_xlat9.x = max(u_xlat9.x, _WaterLevel);
					    u_xlat18.x = u_xlat18.x + u_xlat18.x;
					    u_xlat18.x = clamp(u_xlat18.x, 0.0, 1.0);
					    u_xlat18.x = u_xlat4.x * u_xlat18.x;
					    u_xlat18.x = (-u_xlat18.x) * _WindInterpolator + 1.0;
					    u_xlat8.w = u_xlat18.x * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat5.xyz * u_xlat13.zxy;
					    u_xlat4.xyz = u_xlat13.yzx * u_xlat5.yzx + (-u_xlat6.xyz);
					    u_xlat18.x = _WindInterpolator * 2.0 + 8.0;
					    u_xlat18.x = u_xlat29 * u_xlat18.x + 1.0;
					    u_xlat4.xyz = u_xlat18.xxx * u_xlat4.xyz;
					    u_xlat5.x = (-unity_MatrixV[2].x);
					    u_xlat5.y = 0.0;
					    u_xlat5.z = unity_MatrixV[0].x;
					    u_xlat5.xyz = vec3(u_xlat28) * u_xlat5.xyz;
					    vs_COLOR1.y = u_xlat28;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(u_xlat32) + u_xlat5.xyz;
					    u_xlat5.y = u_xlat33 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat33;
					    u_xlat5.x = float(0.0);
					    u_xlat5.z = float(0.0);
					    u_xlat4.xyz = u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * in_TEXCOORD1.yyy;
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.xzw * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat4.w = max(u_xlat9.x, u_xlat4.y);
					    u_xlat0 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat0 = _Tex2World[0] * u_xlat2.xxxx + u_xlat0;
					    u_xlat0 = _Tex2World[2] * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + _Tex2World[3];
					    u_xlat2 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat2 = _Tex2World[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat2;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat0 = u_xlat0 + (-u_xlat1);
					    u_xlat0.x = dot(u_xlat0, u_xlat0);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlatb0 = 2.0<u_xlat0.x;
					    u_xlat0.xyz = (bool(u_xlatb0)) ? vec3(0.0, 0.0, 0.0) : u_xlat4.xwz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat8.xyz = in_COLOR0.xyz;
					    u_xlat0 = u_xlat0.xxxx * u_xlat8;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_6[6];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_9;
						mat4x4 _Tex2World;
						vec4 unused_0_11[12];
						vec3 _SunDir;
						vec4 unused_0_13[4];
						vec4 _SideSunColor;
						vec4 unused_0_15[2];
						float _Year;
						vec4 unused_0_17[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_21;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_29;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_33[2];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_36;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_39;
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
					uniform  sampler2D _TopdownTex;
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
					vec4 u_xlat9;
					vec4 u_xlat10;
					vec3 u_xlat12;
					bool u_xlatb12;
					float u_xlat13;
					vec3 u_xlat14;
					vec2 u_xlat16;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat28;
					vec2 u_xlat32;
					float u_xlat36;
					bool u_xlatb36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat40;
					float u_xlat41;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat12.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat12.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat12.x = (-_Size.x) + _Size.y;
					    u_xlat12.x = in_TEXCOORD3.y * u_xlat12.x + _Size.x;
					    u_xlat24.x = u_xlat12.x * u_xlat0.x + (-u_xlat12.x);
					    u_xlat12.x = _ScreenSpaceSize * u_xlat24.x + u_xlat12.x;
					    u_xlat24.x = _Interpolator + -1.0;
					    u_xlat24.x = -abs(u_xlat24.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat24.xxxx * u_xlat3 + u_xlat2;
					    u_xlat24.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat24.x = min(u_xlat24.x, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat4.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat24.x = dot(u_xlat24.xx, u_xlat4.xx);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat12.x = u_xlat24.x * u_xlat12.x;
					    u_xlat4.xzw = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xzw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xzw;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xzw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat24.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat24.xy = u_xlat24.xy / _AoTexVolume.xz;
					    u_xlat5 = textureLod(_TopdownTex, u_xlat24.xy, 0.0);
					    u_xlat24.x = u_xlat5.w * 8.0 + _WaterLevel;
					    u_xlatb36 = _WaterLevel>=u_xlat24.x;
					    u_xlat37 = u_xlatb36 ? 1.0 : float(0.0);
					    u_xlat36 = (u_xlatb36) ? 0.0 : 1.0;
					    u_xlat4.xz = u_xlat4.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat37 = u_xlat37 * u_xlat4.x;
					    u_xlat38 = u_xlat4.z * u_xlat37 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat38 = u_xlat3.w * u_xlat3.w;
					    u_xlat38 = (-u_xlat38) * u_xlat38 + 1.0;
					    u_xlat38 = u_xlat36 * u_xlat38;
					    u_xlat38 = u_xlat38 * 6.0 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat5.xyz = vec3(_WindInterpolator) * u_xlat5.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat5.xyz = u_xlat5.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat6.x = unity_MatrixV[0].z;
					    u_xlat6.y = unity_MatrixV[1].z;
					    u_xlat6.z = unity_MatrixV[2].z;
					    u_xlat38 = dot((-u_xlat6.zxy), u_xlat5.xyz);
					    u_xlat7.xyz = u_xlat6.xyz * vec3(u_xlat38) + u_xlat5.yzx;
					    u_xlat38 = dot(u_xlat7.xyz, u_xlat7.xyz);
					    u_xlat38 = sqrt(u_xlat38);
					    u_xlat28 = u_xlat38 + 1.0;
					    u_xlat7.xyz = u_xlat5.xyz * u_xlat6.yzx;
					    u_xlat5.xyz = u_xlat5.zxy * u_xlat6.zxy + (-u_xlat7.xyz);
					    u_xlat40 = dot(u_xlat5.xyz, u_xlat5.xyz);
					    u_xlat40 = inversesqrt(u_xlat40);
					    u_xlat5.xyz = vec3(u_xlat40) * u_xlat5.xyz;
					    u_xlat7.xyz = u_xlat5.xyz / vec3(u_xlat28);
					    u_xlat8.x = unity_MatrixV[0].x;
					    u_xlat8.y = unity_MatrixV[1].x;
					    u_xlat8.z = unity_MatrixV[2].x;
					    u_xlat9.xyz = vec3(u_xlat37) * u_xlat8.xyz;
					    u_xlat28 = (-u_xlat4.x) + 1.0;
					    u_xlat7.xyz = u_xlat7.xyz * vec3(u_xlat28) + u_xlat9.xyz;
					    u_xlat40 = u_xlat36 * u_xlat4.x;
					    u_xlat7.xyz = u_xlat8.xyz * vec3(u_xlat40) + u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * in_TEXCOORD1.xxx;
					    u_xlat7.xyz = u_xlat12.xxx * u_xlat7.xyz;
					    u_xlat41 = u_xlat4.y * u_xlat4.y;
					    u_xlat41 = u_xlat4.y * u_xlat41;
					    u_xlat8.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat32.xy = vec2(u_xlat41) * u_xlat8.xy;
					    u_xlat32.xy = vec2(u_xlat36) * u_xlat32.xy;
					    u_xlat36 = u_xlat4.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat4.y * u_xlat40 + u_xlat28;
					    u_xlat8.xy = vec2(u_xlat36) * u_xlat8.xy;
					    u_xlat3.xz = u_xlat8.xy * vec2(u_xlat37) + u_xlat3.xz;
					    u_xlat8.xz = u_xlat32.xy * vec2(8.0, 8.0) + u_xlat3.xz;
					    u_xlat36 = (-u_xlat3.y) + u_xlat24.x;
					    u_xlat8.y = u_xlat4.x * u_xlat36 + u_xlat3.y;
					    u_xlat36 = (-u_xlat24.x) + u_xlat3.y;
					    u_xlat24.x = u_xlat24.x + -0.100000001;
					    u_xlat24.x = max(u_xlat24.x, _WaterLevel);
					    u_xlat36 = u_xlat36 + u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat36 = u_xlat4.x * u_xlat36;
					    u_xlat36 = (-u_xlat36) * _WindInterpolator + 1.0;
					    u_xlat9.w = u_xlat36 * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat7.xyz * vec3(2.0, 2.0, 2.0) + u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat6.yzx * u_xlat5.zxy;
					    u_xlat5.xyz = u_xlat5.yzx * u_xlat6.zxy + (-u_xlat7.xyz);
					    u_xlat36 = _WindInterpolator * 2.0 + 8.0;
					    u_xlat36 = u_xlat38 * u_xlat36 + 1.0;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.xyz;
					    u_xlat7.x = (-unity_MatrixV[2].x);
					    u_xlat7.y = 0.0;
					    u_xlat7.z = unity_MatrixV[0].x;
					    u_xlat7.xyz = vec3(u_xlat37) * u_xlat7.xyz;
					    vs_COLOR1.y = u_xlat37;
					    u_xlat4.xyz = u_xlat5.xyz * vec3(u_xlat28) + u_xlat7.xyz;
					    u_xlat5.y = u_xlat40 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat40;
					    u_xlat5.x = float(0.0);
					    u_xlat5.z = float(0.0);
					    u_xlat4.xyz = u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * in_TEXCOORD1.yyy;
					    u_xlat4.xyz = u_xlat12.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat4.w = max(u_xlat24.x, u_xlat4.y);
					    u_xlat5 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat5 = _Tex2World[0] * u_xlat2.xxxx + u_xlat5;
					    u_xlat2 = _Tex2World[2] * u_xlat2.zzzz + u_xlat5;
					    u_xlat2 = u_xlat2 + _Tex2World[3];
					    u_xlat5 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat5 = _Tex2World[0] * u_xlat1.xxxx + u_xlat5;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat5;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat1 = (-u_xlat1) + u_xlat2;
					    u_xlat12.x = dot(u_xlat1, u_xlat1);
					    u_xlat12.x = sqrt(u_xlat12.x);
					    u_xlatb12 = 2.0<u_xlat12.x;
					    u_xlat1.xyw = (bool(u_xlatb12)) ? vec3(0.0, 0.0, 0.0) : u_xlat4.xzw;
					    u_xlat12.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat12.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat12.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat12.x;
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
					    u_xlat36 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat38 = u_xlat0.y * u_xlat36;
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat38;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat7 = u_xlat3.yyyy * u_xlat7;
					    u_xlat8.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat38;
					    u_xlat10 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat23.yz = u_xlat5.yx;
					    u_xlat10 = u_xlat2.xxxx * u_xlat10;
					    u_xlat10 = u_xlat3.yyyy * u_xlat10;
					    u_xlat10 = u_xlat3.zzzz * u_xlat10;
					    u_xlat7 = u_xlat7 * u_xlat3.zzzz + u_xlat10;
					    u_xlat12.x = u_xlat36 * u_xlat8.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat12.x;
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat12.x;
					    u_xlat23.x = u_xlat4.y * u_xlat8.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat2.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat0 = u_xlat3.xxxx * u_xlat0;
					    u_xlat0 = u_xlat2.yyyy * u_xlat0;
					    u_xlat0 = u_xlat0 * u_xlat3.zzzz + u_xlat7;
					    u_xlat0 = u_xlat4 * u_xlat3.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat5.w = u_xlat23.x;
					    u_xlat7 = u_xlat2.xxxx * u_xlat7;
					    u_xlat7 = u_xlat3.yyyy * u_xlat7;
					    u_xlat4 = u_xlat3.xxxx * u_xlat4;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat7 * u_xlat2.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat2.xxxx * u_xlat5;
					    u_xlat5 = u_xlat2.yyyy * u_xlat5;
					    u_xlat4 = u_xlat3.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat5 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat36 = u_xlat0.w * 0.400000006;
					    u_xlat2.xyz = (-u_xlat0.xyz) + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat37 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat37 = u_xlat37 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat38 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat4.x = u_xlat1.y * u_xlat38;
					    u_xlat16.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat16.x * u_xlat1.x + u_xlat4.x;
					    u_xlat5.z = u_xlat1.z * u_xlat16.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat16.x * u_xlat7.x + u_xlat4.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat22.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat13 = u_xlat38 * u_xlat7.y;
					    u_xlat5.y = u_xlat16.x * u_xlat1.x + u_xlat13;
					    u_xlat5.x = u_xlat16.x * u_xlat7.x + u_xlat13;
					    u_xlat22.x = u_xlat16.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat22.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat22.zx, 0.0);
					    u_xlat5.w = u_xlat22.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat14.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat14.y + u_xlat14.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat36 = u_xlat1.x * 0.600000024 + u_xlat36;
					    u_xlat1.x = u_xlat37;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat37 = (-u_xlat37);
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat13 = max(u_xlat37, u_xlat1.x);
					    u_xlat13 = (-u_xlat13) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat4.yzx;
					    u_xlat1.xyz = u_xlat4.xyz * vec3(u_xlat13) + u_xlat3.xyz;
					    u_xlat1.xyz = vec3(u_xlat37) * u_xlat4.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat36) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat36 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat36 = inversesqrt(u_xlat36);
					    u_xlat0.xyz = vec3(u_xlat36) * u_xlat0.xyz;
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat3.xyz = u_xlat0.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat0.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat12.xy = u_xlat14.xy * u_xlat3.xy;
					    u_xlat12.x = u_xlat12.y + u_xlat12.x;
					    u_xlat12.x = u_xlat14.z * u_xlat3.z + u_xlat12.x;
					    u_xlat12.x = u_xlat12.x / u_xlat2.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat12.xyz = u_xlat2.xyz * u_xlat12.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat12.xyz;
					    u_xlat9.xyz = in_COLOR0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * u_xlat9;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_6[6];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_9;
						mat4x4 _Tex2World;
						vec4 unused_0_11[12];
						vec3 _SunDir;
						vec4 unused_0_13[4];
						vec4 _SideSunColor;
						vec4 unused_0_15[2];
						float _Year;
						vec4 unused_0_17[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_21;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_29;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_33[2];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_36;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_39;
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
					uniform  sampler2D _TopdownTex;
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
					vec4 u_xlat9;
					vec4 u_xlat10;
					vec3 u_xlat12;
					bool u_xlatb12;
					float u_xlat13;
					vec3 u_xlat14;
					vec2 u_xlat16;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat28;
					vec2 u_xlat32;
					float u_xlat36;
					bool u_xlatb36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat40;
					float u_xlat41;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat12.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat12.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat12.x = (-_Size.x) + _Size.y;
					    u_xlat12.x = in_TEXCOORD3.y * u_xlat12.x + _Size.x;
					    u_xlat24.x = u_xlat12.x * u_xlat0.x + (-u_xlat12.x);
					    u_xlat12.x = _ScreenSpaceSize * u_xlat24.x + u_xlat12.x;
					    u_xlat24.x = _Interpolator + -1.0;
					    u_xlat24.x = -abs(u_xlat24.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat24.xxxx * u_xlat3 + u_xlat2;
					    u_xlat24.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat24.x = min(u_xlat24.x, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat4.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat24.x = dot(u_xlat24.xx, u_xlat4.xx);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat12.x = u_xlat24.x * u_xlat12.x;
					    u_xlat4.xzw = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xzw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xzw;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xzw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat24.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat24.xy = u_xlat24.xy / _AoTexVolume.xz;
					    u_xlat5 = textureLod(_TopdownTex, u_xlat24.xy, 0.0);
					    u_xlat24.x = u_xlat5.w * 8.0 + _WaterLevel;
					    u_xlatb36 = _WaterLevel>=u_xlat24.x;
					    u_xlat37 = u_xlatb36 ? 1.0 : float(0.0);
					    u_xlat36 = (u_xlatb36) ? 0.0 : 1.0;
					    u_xlat4.xz = u_xlat4.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat37 = u_xlat37 * u_xlat4.x;
					    u_xlat38 = u_xlat4.z * u_xlat37 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat38 = u_xlat3.w * u_xlat3.w;
					    u_xlat38 = (-u_xlat38) * u_xlat38 + 1.0;
					    u_xlat38 = u_xlat36 * u_xlat38;
					    u_xlat38 = u_xlat38 * 6.0 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat5.xyz = vec3(_WindInterpolator) * u_xlat5.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat5.xyz = u_xlat5.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat6.x = unity_MatrixV[0].z;
					    u_xlat6.y = unity_MatrixV[1].z;
					    u_xlat6.z = unity_MatrixV[2].z;
					    u_xlat38 = dot((-u_xlat6.zxy), u_xlat5.xyz);
					    u_xlat7.xyz = u_xlat6.xyz * vec3(u_xlat38) + u_xlat5.yzx;
					    u_xlat38 = dot(u_xlat7.xyz, u_xlat7.xyz);
					    u_xlat38 = sqrt(u_xlat38);
					    u_xlat28 = u_xlat38 + 1.0;
					    u_xlat7.xyz = u_xlat5.xyz * u_xlat6.yzx;
					    u_xlat5.xyz = u_xlat5.zxy * u_xlat6.zxy + (-u_xlat7.xyz);
					    u_xlat40 = dot(u_xlat5.xyz, u_xlat5.xyz);
					    u_xlat40 = inversesqrt(u_xlat40);
					    u_xlat5.xyz = vec3(u_xlat40) * u_xlat5.xyz;
					    u_xlat7.xyz = u_xlat5.xyz / vec3(u_xlat28);
					    u_xlat8.x = unity_MatrixV[0].x;
					    u_xlat8.y = unity_MatrixV[1].x;
					    u_xlat8.z = unity_MatrixV[2].x;
					    u_xlat9.xyz = vec3(u_xlat37) * u_xlat8.xyz;
					    u_xlat28 = (-u_xlat4.x) + 1.0;
					    u_xlat7.xyz = u_xlat7.xyz * vec3(u_xlat28) + u_xlat9.xyz;
					    u_xlat40 = u_xlat36 * u_xlat4.x;
					    u_xlat7.xyz = u_xlat8.xyz * vec3(u_xlat40) + u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * in_TEXCOORD1.xxx;
					    u_xlat7.xyz = u_xlat12.xxx * u_xlat7.xyz;
					    u_xlat41 = u_xlat4.y * u_xlat4.y;
					    u_xlat41 = u_xlat4.y * u_xlat41;
					    u_xlat8.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat32.xy = vec2(u_xlat41) * u_xlat8.xy;
					    u_xlat32.xy = vec2(u_xlat36) * u_xlat32.xy;
					    u_xlat36 = u_xlat4.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat4.y * u_xlat40 + u_xlat28;
					    u_xlat8.xy = vec2(u_xlat36) * u_xlat8.xy;
					    u_xlat3.xz = u_xlat8.xy * vec2(u_xlat37) + u_xlat3.xz;
					    u_xlat8.xz = u_xlat32.xy * vec2(8.0, 8.0) + u_xlat3.xz;
					    u_xlat36 = (-u_xlat3.y) + u_xlat24.x;
					    u_xlat8.y = u_xlat4.x * u_xlat36 + u_xlat3.y;
					    u_xlat36 = (-u_xlat24.x) + u_xlat3.y;
					    u_xlat24.x = u_xlat24.x + -0.100000001;
					    u_xlat24.x = max(u_xlat24.x, _WaterLevel);
					    u_xlat36 = u_xlat36 + u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat36 = u_xlat4.x * u_xlat36;
					    u_xlat36 = (-u_xlat36) * _WindInterpolator + 1.0;
					    u_xlat9.w = u_xlat36 * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat7.xyz * vec3(2.0, 2.0, 2.0) + u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat6.yzx * u_xlat5.zxy;
					    u_xlat5.xyz = u_xlat5.yzx * u_xlat6.zxy + (-u_xlat7.xyz);
					    u_xlat36 = _WindInterpolator * 2.0 + 8.0;
					    u_xlat36 = u_xlat38 * u_xlat36 + 1.0;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.xyz;
					    u_xlat7.x = (-unity_MatrixV[2].x);
					    u_xlat7.y = 0.0;
					    u_xlat7.z = unity_MatrixV[0].x;
					    u_xlat7.xyz = vec3(u_xlat37) * u_xlat7.xyz;
					    vs_COLOR1.y = u_xlat37;
					    u_xlat4.xyz = u_xlat5.xyz * vec3(u_xlat28) + u_xlat7.xyz;
					    u_xlat5.y = u_xlat40 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat40;
					    u_xlat5.x = float(0.0);
					    u_xlat5.z = float(0.0);
					    u_xlat4.xyz = u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * in_TEXCOORD1.yyy;
					    u_xlat4.xyz = u_xlat12.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat4.w = max(u_xlat24.x, u_xlat4.y);
					    u_xlat5 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat5 = _Tex2World[0] * u_xlat2.xxxx + u_xlat5;
					    u_xlat2 = _Tex2World[2] * u_xlat2.zzzz + u_xlat5;
					    u_xlat2 = u_xlat2 + _Tex2World[3];
					    u_xlat5 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat5 = _Tex2World[0] * u_xlat1.xxxx + u_xlat5;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat5;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat1 = (-u_xlat1) + u_xlat2;
					    u_xlat12.x = dot(u_xlat1, u_xlat1);
					    u_xlat12.x = sqrt(u_xlat12.x);
					    u_xlatb12 = 2.0<u_xlat12.x;
					    u_xlat1.xyw = (bool(u_xlatb12)) ? vec3(0.0, 0.0, 0.0) : u_xlat4.xzw;
					    u_xlat12.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat12.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat12.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat12.x;
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
					    u_xlat36 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat38 = u_xlat0.y * u_xlat36;
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat38;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat7 = u_xlat3.yyyy * u_xlat7;
					    u_xlat8.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat38;
					    u_xlat10 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat23.yz = u_xlat5.yx;
					    u_xlat10 = u_xlat2.xxxx * u_xlat10;
					    u_xlat10 = u_xlat3.yyyy * u_xlat10;
					    u_xlat10 = u_xlat3.zzzz * u_xlat10;
					    u_xlat7 = u_xlat7 * u_xlat3.zzzz + u_xlat10;
					    u_xlat12.x = u_xlat36 * u_xlat8.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat12.x;
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat12.x;
					    u_xlat23.x = u_xlat4.y * u_xlat8.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat2.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat0 = u_xlat3.xxxx * u_xlat0;
					    u_xlat0 = u_xlat2.yyyy * u_xlat0;
					    u_xlat0 = u_xlat0 * u_xlat3.zzzz + u_xlat7;
					    u_xlat0 = u_xlat4 * u_xlat3.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat5.w = u_xlat23.x;
					    u_xlat7 = u_xlat2.xxxx * u_xlat7;
					    u_xlat7 = u_xlat3.yyyy * u_xlat7;
					    u_xlat4 = u_xlat3.xxxx * u_xlat4;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat7 * u_xlat2.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat2.xxxx * u_xlat5;
					    u_xlat5 = u_xlat2.yyyy * u_xlat5;
					    u_xlat4 = u_xlat3.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat5 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat36 = u_xlat0.w * 0.400000006;
					    u_xlat2.xyz = (-u_xlat0.xyz) + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat37 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat37 = u_xlat37 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat38 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat4.x = u_xlat1.y * u_xlat38;
					    u_xlat16.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat16.x * u_xlat1.x + u_xlat4.x;
					    u_xlat5.z = u_xlat1.z * u_xlat16.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat16.x * u_xlat7.x + u_xlat4.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat22.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat13 = u_xlat38 * u_xlat7.y;
					    u_xlat5.y = u_xlat16.x * u_xlat1.x + u_xlat13;
					    u_xlat5.x = u_xlat16.x * u_xlat7.x + u_xlat13;
					    u_xlat22.x = u_xlat16.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat22.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat22.zx, 0.0);
					    u_xlat5.w = u_xlat22.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat14.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat14.y + u_xlat14.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat36 = u_xlat1.x * 0.600000024 + u_xlat36;
					    u_xlat1.x = u_xlat37;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat37 = (-u_xlat37);
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat13 = max(u_xlat37, u_xlat1.x);
					    u_xlat13 = (-u_xlat13) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat4.yzx;
					    u_xlat1.xyz = u_xlat4.xyz * vec3(u_xlat13) + u_xlat3.xyz;
					    u_xlat1.xyz = vec3(u_xlat37) * u_xlat4.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat36) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat36 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat36 = inversesqrt(u_xlat36);
					    u_xlat0.xyz = vec3(u_xlat36) * u_xlat0.xyz;
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat3.xyz = u_xlat0.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat0.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat12.xy = u_xlat14.xy * u_xlat3.xy;
					    u_xlat12.x = u_xlat12.y + u_xlat12.x;
					    u_xlat12.x = u_xlat14.z * u_xlat3.z + u_xlat12.x;
					    u_xlat12.x = u_xlat12.x / u_xlat2.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat12.xyz = u_xlat2.xyz * u_xlat12.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat12.xyz;
					    u_xlat9.xyz = in_COLOR0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * u_xlat9;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_6[6];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_9;
						mat4x4 _Tex2World;
						vec4 unused_0_11[12];
						vec3 _SunDir;
						vec4 unused_0_13[4];
						vec4 _SideSunColor;
						vec4 unused_0_15[2];
						float _Year;
						vec4 unused_0_17[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_21;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_29;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_33[2];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_36;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_39;
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
					uniform  sampler2D _TopdownTex;
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
					vec4 u_xlat9;
					vec4 u_xlat10;
					vec3 u_xlat12;
					bool u_xlatb12;
					float u_xlat13;
					vec3 u_xlat14;
					vec2 u_xlat16;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat28;
					vec2 u_xlat32;
					float u_xlat36;
					bool u_xlatb36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat40;
					float u_xlat41;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat12.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat12.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat12.x = (-_Size.x) + _Size.y;
					    u_xlat12.x = in_TEXCOORD3.y * u_xlat12.x + _Size.x;
					    u_xlat24.x = u_xlat12.x * u_xlat0.x + (-u_xlat12.x);
					    u_xlat12.x = _ScreenSpaceSize * u_xlat24.x + u_xlat12.x;
					    u_xlat24.x = _Interpolator + -1.0;
					    u_xlat24.x = -abs(u_xlat24.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat24.xxxx * u_xlat3 + u_xlat2;
					    u_xlat24.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat24.x = min(u_xlat24.x, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat4.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat24.x = dot(u_xlat24.xx, u_xlat4.xx);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat12.x = u_xlat24.x * u_xlat12.x;
					    u_xlat4.xzw = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xzw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xzw;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xzw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat24.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat24.xy = u_xlat24.xy / _AoTexVolume.xz;
					    u_xlat5 = textureLod(_TopdownTex, u_xlat24.xy, 0.0);
					    u_xlat24.x = u_xlat5.w * 8.0 + _WaterLevel;
					    u_xlatb36 = _WaterLevel>=u_xlat24.x;
					    u_xlat37 = u_xlatb36 ? 1.0 : float(0.0);
					    u_xlat36 = (u_xlatb36) ? 0.0 : 1.0;
					    u_xlat4.xz = u_xlat4.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat37 = u_xlat37 * u_xlat4.x;
					    u_xlat38 = u_xlat4.z * u_xlat37 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat38 = u_xlat3.w * u_xlat3.w;
					    u_xlat38 = (-u_xlat38) * u_xlat38 + 1.0;
					    u_xlat38 = u_xlat36 * u_xlat38;
					    u_xlat38 = u_xlat38 * 6.0 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat5.xyz = vec3(_WindInterpolator) * u_xlat5.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat5.xyz = u_xlat5.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat6.x = unity_MatrixV[0].z;
					    u_xlat6.y = unity_MatrixV[1].z;
					    u_xlat6.z = unity_MatrixV[2].z;
					    u_xlat38 = dot((-u_xlat6.zxy), u_xlat5.xyz);
					    u_xlat7.xyz = u_xlat6.xyz * vec3(u_xlat38) + u_xlat5.yzx;
					    u_xlat38 = dot(u_xlat7.xyz, u_xlat7.xyz);
					    u_xlat38 = sqrt(u_xlat38);
					    u_xlat28 = u_xlat38 + 1.0;
					    u_xlat7.xyz = u_xlat5.xyz * u_xlat6.yzx;
					    u_xlat5.xyz = u_xlat5.zxy * u_xlat6.zxy + (-u_xlat7.xyz);
					    u_xlat40 = dot(u_xlat5.xyz, u_xlat5.xyz);
					    u_xlat40 = inversesqrt(u_xlat40);
					    u_xlat5.xyz = vec3(u_xlat40) * u_xlat5.xyz;
					    u_xlat7.xyz = u_xlat5.xyz / vec3(u_xlat28);
					    u_xlat8.x = unity_MatrixV[0].x;
					    u_xlat8.y = unity_MatrixV[1].x;
					    u_xlat8.z = unity_MatrixV[2].x;
					    u_xlat9.xyz = vec3(u_xlat37) * u_xlat8.xyz;
					    u_xlat28 = (-u_xlat4.x) + 1.0;
					    u_xlat7.xyz = u_xlat7.xyz * vec3(u_xlat28) + u_xlat9.xyz;
					    u_xlat40 = u_xlat36 * u_xlat4.x;
					    u_xlat7.xyz = u_xlat8.xyz * vec3(u_xlat40) + u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * in_TEXCOORD1.xxx;
					    u_xlat7.xyz = u_xlat12.xxx * u_xlat7.xyz;
					    u_xlat41 = u_xlat4.y * u_xlat4.y;
					    u_xlat41 = u_xlat4.y * u_xlat41;
					    u_xlat8.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat32.xy = vec2(u_xlat41) * u_xlat8.xy;
					    u_xlat32.xy = vec2(u_xlat36) * u_xlat32.xy;
					    u_xlat36 = u_xlat4.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat4.y * u_xlat40 + u_xlat28;
					    u_xlat8.xy = vec2(u_xlat36) * u_xlat8.xy;
					    u_xlat3.xz = u_xlat8.xy * vec2(u_xlat37) + u_xlat3.xz;
					    u_xlat8.xz = u_xlat32.xy * vec2(8.0, 8.0) + u_xlat3.xz;
					    u_xlat36 = (-u_xlat3.y) + u_xlat24.x;
					    u_xlat8.y = u_xlat4.x * u_xlat36 + u_xlat3.y;
					    u_xlat36 = (-u_xlat24.x) + u_xlat3.y;
					    u_xlat24.x = u_xlat24.x + -0.100000001;
					    u_xlat24.x = max(u_xlat24.x, _WaterLevel);
					    u_xlat36 = u_xlat36 + u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat36 = u_xlat4.x * u_xlat36;
					    u_xlat36 = (-u_xlat36) * _WindInterpolator + 1.0;
					    u_xlat9.w = u_xlat36 * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat7.xyz * vec3(2.0, 2.0, 2.0) + u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat6.yzx * u_xlat5.zxy;
					    u_xlat5.xyz = u_xlat5.yzx * u_xlat6.zxy + (-u_xlat7.xyz);
					    u_xlat36 = _WindInterpolator * 2.0 + 8.0;
					    u_xlat36 = u_xlat38 * u_xlat36 + 1.0;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.xyz;
					    u_xlat7.x = (-unity_MatrixV[2].x);
					    u_xlat7.y = 0.0;
					    u_xlat7.z = unity_MatrixV[0].x;
					    u_xlat7.xyz = vec3(u_xlat37) * u_xlat7.xyz;
					    vs_COLOR1.y = u_xlat37;
					    u_xlat4.xyz = u_xlat5.xyz * vec3(u_xlat28) + u_xlat7.xyz;
					    u_xlat5.y = u_xlat40 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat40;
					    u_xlat5.x = float(0.0);
					    u_xlat5.z = float(0.0);
					    u_xlat4.xyz = u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * in_TEXCOORD1.yyy;
					    u_xlat4.xyz = u_xlat12.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat4.w = max(u_xlat24.x, u_xlat4.y);
					    u_xlat5 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat5 = _Tex2World[0] * u_xlat2.xxxx + u_xlat5;
					    u_xlat2 = _Tex2World[2] * u_xlat2.zzzz + u_xlat5;
					    u_xlat2 = u_xlat2 + _Tex2World[3];
					    u_xlat5 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat5 = _Tex2World[0] * u_xlat1.xxxx + u_xlat5;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat5;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat1 = (-u_xlat1) + u_xlat2;
					    u_xlat12.x = dot(u_xlat1, u_xlat1);
					    u_xlat12.x = sqrt(u_xlat12.x);
					    u_xlatb12 = 2.0<u_xlat12.x;
					    u_xlat1.xyw = (bool(u_xlatb12)) ? vec3(0.0, 0.0, 0.0) : u_xlat4.xzw;
					    u_xlat12.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat12.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat12.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat12.x;
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
					    u_xlat36 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat38 = u_xlat0.y * u_xlat36;
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat38;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat7 = u_xlat3.yyyy * u_xlat7;
					    u_xlat8.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat38;
					    u_xlat10 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat23.yz = u_xlat5.yx;
					    u_xlat10 = u_xlat2.xxxx * u_xlat10;
					    u_xlat10 = u_xlat3.yyyy * u_xlat10;
					    u_xlat10 = u_xlat3.zzzz * u_xlat10;
					    u_xlat7 = u_xlat7 * u_xlat3.zzzz + u_xlat10;
					    u_xlat12.x = u_xlat36 * u_xlat8.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat12.x;
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat12.x;
					    u_xlat23.x = u_xlat4.y * u_xlat8.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat2.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat0 = u_xlat3.xxxx * u_xlat0;
					    u_xlat0 = u_xlat2.yyyy * u_xlat0;
					    u_xlat0 = u_xlat0 * u_xlat3.zzzz + u_xlat7;
					    u_xlat0 = u_xlat4 * u_xlat3.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat5.w = u_xlat23.x;
					    u_xlat7 = u_xlat2.xxxx * u_xlat7;
					    u_xlat7 = u_xlat3.yyyy * u_xlat7;
					    u_xlat4 = u_xlat3.xxxx * u_xlat4;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat7 * u_xlat2.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat2.xxxx * u_xlat5;
					    u_xlat5 = u_xlat2.yyyy * u_xlat5;
					    u_xlat4 = u_xlat3.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat5 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat36 = u_xlat0.w * 0.400000006;
					    u_xlat2.xyz = (-u_xlat0.xyz) + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat37 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat37 = u_xlat37 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat38 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat4.x = u_xlat1.y * u_xlat38;
					    u_xlat16.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat16.x * u_xlat1.x + u_xlat4.x;
					    u_xlat5.z = u_xlat1.z * u_xlat16.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat16.x * u_xlat7.x + u_xlat4.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat22.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat13 = u_xlat38 * u_xlat7.y;
					    u_xlat5.y = u_xlat16.x * u_xlat1.x + u_xlat13;
					    u_xlat5.x = u_xlat16.x * u_xlat7.x + u_xlat13;
					    u_xlat22.x = u_xlat16.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat22.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat22.zx, 0.0);
					    u_xlat5.w = u_xlat22.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat14.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat14.y + u_xlat14.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat36 = u_xlat1.x * 0.600000024 + u_xlat36;
					    u_xlat1.x = u_xlat37;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat37 = (-u_xlat37);
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat13 = max(u_xlat37, u_xlat1.x);
					    u_xlat13 = (-u_xlat13) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat4.yzx;
					    u_xlat1.xyz = u_xlat4.xyz * vec3(u_xlat13) + u_xlat3.xyz;
					    u_xlat1.xyz = vec3(u_xlat37) * u_xlat4.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat36) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat36 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat36 = inversesqrt(u_xlat36);
					    u_xlat0.xyz = vec3(u_xlat36) * u_xlat0.xyz;
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat3.xyz = u_xlat0.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat0.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat12.xy = u_xlat14.xy * u_xlat3.xy;
					    u_xlat12.x = u_xlat12.y + u_xlat12.x;
					    u_xlat12.x = u_xlat14.z * u_xlat3.z + u_xlat12.x;
					    u_xlat12.x = u_xlat12.x / u_xlat2.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat12.xyz = u_xlat2.xyz * u_xlat12.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat12.xyz;
					    u_xlat9.xyz = in_COLOR0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * u_xlat9;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_6[6];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_9;
						mat4x4 _Tex2World;
						vec4 unused_0_11[12];
						vec3 _SunDir;
						vec4 unused_0_13[4];
						vec4 _SideSunColor;
						vec4 unused_0_15[2];
						float _Year;
						vec4 unused_0_17[7];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_21;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_29;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_33[2];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_36;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_39;
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
					uniform  sampler2D _TopdownTex;
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
					vec4 u_xlat9;
					vec4 u_xlat10;
					vec3 u_xlat12;
					bool u_xlatb12;
					float u_xlat13;
					vec3 u_xlat14;
					vec2 u_xlat16;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat28;
					vec2 u_xlat32;
					float u_xlat36;
					bool u_xlatb36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat40;
					float u_xlat41;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat12.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat12.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat12.x = (-_Size.x) + _Size.y;
					    u_xlat12.x = in_TEXCOORD3.y * u_xlat12.x + _Size.x;
					    u_xlat24.x = u_xlat12.x * u_xlat0.x + (-u_xlat12.x);
					    u_xlat12.x = _ScreenSpaceSize * u_xlat24.x + u_xlat12.x;
					    u_xlat24.x = _Interpolator + -1.0;
					    u_xlat24.x = -abs(u_xlat24.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat24.xxxx * u_xlat3 + u_xlat2;
					    u_xlat24.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat24.x = min(u_xlat24.x, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat4.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat24.x = dot(u_xlat24.xx, u_xlat4.xx);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat12.x = u_xlat24.x * u_xlat12.x;
					    u_xlat4.xzw = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xzw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xzw;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xzw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat24.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat24.xy = u_xlat24.xy / _AoTexVolume.xz;
					    u_xlat5 = textureLod(_TopdownTex, u_xlat24.xy, 0.0);
					    u_xlat24.x = u_xlat5.w * 8.0 + _WaterLevel;
					    u_xlatb36 = _WaterLevel>=u_xlat24.x;
					    u_xlat37 = u_xlatb36 ? 1.0 : float(0.0);
					    u_xlat36 = (u_xlatb36) ? 0.0 : 1.0;
					    u_xlat4.xz = u_xlat4.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat37 = u_xlat37 * u_xlat4.x;
					    u_xlat38 = u_xlat4.z * u_xlat37 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat38 = u_xlat3.w * u_xlat3.w;
					    u_xlat38 = (-u_xlat38) * u_xlat38 + 1.0;
					    u_xlat38 = u_xlat36 * u_xlat38;
					    u_xlat38 = u_xlat38 * 6.0 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat5.xyz = vec3(_WindInterpolator) * u_xlat5.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat5.xyz = u_xlat5.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat6.x = unity_MatrixV[0].z;
					    u_xlat6.y = unity_MatrixV[1].z;
					    u_xlat6.z = unity_MatrixV[2].z;
					    u_xlat38 = dot((-u_xlat6.zxy), u_xlat5.xyz);
					    u_xlat7.xyz = u_xlat6.xyz * vec3(u_xlat38) + u_xlat5.yzx;
					    u_xlat38 = dot(u_xlat7.xyz, u_xlat7.xyz);
					    u_xlat38 = sqrt(u_xlat38);
					    u_xlat28 = u_xlat38 + 1.0;
					    u_xlat7.xyz = u_xlat5.xyz * u_xlat6.yzx;
					    u_xlat5.xyz = u_xlat5.zxy * u_xlat6.zxy + (-u_xlat7.xyz);
					    u_xlat40 = dot(u_xlat5.xyz, u_xlat5.xyz);
					    u_xlat40 = inversesqrt(u_xlat40);
					    u_xlat5.xyz = vec3(u_xlat40) * u_xlat5.xyz;
					    u_xlat7.xyz = u_xlat5.xyz / vec3(u_xlat28);
					    u_xlat8.x = unity_MatrixV[0].x;
					    u_xlat8.y = unity_MatrixV[1].x;
					    u_xlat8.z = unity_MatrixV[2].x;
					    u_xlat9.xyz = vec3(u_xlat37) * u_xlat8.xyz;
					    u_xlat28 = (-u_xlat4.x) + 1.0;
					    u_xlat7.xyz = u_xlat7.xyz * vec3(u_xlat28) + u_xlat9.xyz;
					    u_xlat40 = u_xlat36 * u_xlat4.x;
					    u_xlat7.xyz = u_xlat8.xyz * vec3(u_xlat40) + u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * in_TEXCOORD1.xxx;
					    u_xlat7.xyz = u_xlat12.xxx * u_xlat7.xyz;
					    u_xlat41 = u_xlat4.y * u_xlat4.y;
					    u_xlat41 = u_xlat4.y * u_xlat41;
					    u_xlat8.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat32.xy = vec2(u_xlat41) * u_xlat8.xy;
					    u_xlat32.xy = vec2(u_xlat36) * u_xlat32.xy;
					    u_xlat36 = u_xlat4.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat4.y * u_xlat40 + u_xlat28;
					    u_xlat8.xy = vec2(u_xlat36) * u_xlat8.xy;
					    u_xlat3.xz = u_xlat8.xy * vec2(u_xlat37) + u_xlat3.xz;
					    u_xlat8.xz = u_xlat32.xy * vec2(8.0, 8.0) + u_xlat3.xz;
					    u_xlat36 = (-u_xlat3.y) + u_xlat24.x;
					    u_xlat8.y = u_xlat4.x * u_xlat36 + u_xlat3.y;
					    u_xlat36 = (-u_xlat24.x) + u_xlat3.y;
					    u_xlat24.x = u_xlat24.x + -0.100000001;
					    u_xlat24.x = max(u_xlat24.x, _WaterLevel);
					    u_xlat36 = u_xlat36 + u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat36 = u_xlat4.x * u_xlat36;
					    u_xlat36 = (-u_xlat36) * _WindInterpolator + 1.0;
					    u_xlat9.w = u_xlat36 * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat7.xyz * vec3(2.0, 2.0, 2.0) + u_xlat8.xyz;
					    u_xlat7.xyz = u_xlat6.yzx * u_xlat5.zxy;
					    u_xlat5.xyz = u_xlat5.yzx * u_xlat6.zxy + (-u_xlat7.xyz);
					    u_xlat36 = _WindInterpolator * 2.0 + 8.0;
					    u_xlat36 = u_xlat38 * u_xlat36 + 1.0;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.xyz;
					    u_xlat7.x = (-unity_MatrixV[2].x);
					    u_xlat7.y = 0.0;
					    u_xlat7.z = unity_MatrixV[0].x;
					    u_xlat7.xyz = vec3(u_xlat37) * u_xlat7.xyz;
					    vs_COLOR1.y = u_xlat37;
					    u_xlat4.xyz = u_xlat5.xyz * vec3(u_xlat28) + u_xlat7.xyz;
					    u_xlat5.y = u_xlat40 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat40;
					    u_xlat5.x = float(0.0);
					    u_xlat5.z = float(0.0);
					    u_xlat4.xyz = u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * in_TEXCOORD1.yyy;
					    u_xlat4.xyz = u_xlat12.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat4.w = max(u_xlat24.x, u_xlat4.y);
					    u_xlat5 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat5 = _Tex2World[0] * u_xlat2.xxxx + u_xlat5;
					    u_xlat2 = _Tex2World[2] * u_xlat2.zzzz + u_xlat5;
					    u_xlat2 = u_xlat2 + _Tex2World[3];
					    u_xlat5 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat5 = _Tex2World[0] * u_xlat1.xxxx + u_xlat5;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat5;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat1 = (-u_xlat1) + u_xlat2;
					    u_xlat12.x = dot(u_xlat1, u_xlat1);
					    u_xlat12.x = sqrt(u_xlat12.x);
					    u_xlatb12 = 2.0<u_xlat12.x;
					    u_xlat1.xyw = (bool(u_xlatb12)) ? vec3(0.0, 0.0, 0.0) : u_xlat4.xzw;
					    u_xlat12.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat12.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat12.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat12.x;
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
					    u_xlat36 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat38 = u_xlat0.y * u_xlat36;
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat38;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat7 = u_xlat3.yyyy * u_xlat7;
					    u_xlat8.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat38;
					    u_xlat10 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat23.yz = u_xlat5.yx;
					    u_xlat10 = u_xlat2.xxxx * u_xlat10;
					    u_xlat10 = u_xlat3.yyyy * u_xlat10;
					    u_xlat10 = u_xlat3.zzzz * u_xlat10;
					    u_xlat7 = u_xlat7 * u_xlat3.zzzz + u_xlat10;
					    u_xlat12.x = u_xlat36 * u_xlat8.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat12.x;
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat12.x;
					    u_xlat23.x = u_xlat4.y * u_xlat8.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4 = u_xlat2.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat0 = u_xlat3.xxxx * u_xlat0;
					    u_xlat0 = u_xlat2.yyyy * u_xlat0;
					    u_xlat0 = u_xlat0 * u_xlat3.zzzz + u_xlat7;
					    u_xlat0 = u_xlat4 * u_xlat3.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat5.w = u_xlat23.x;
					    u_xlat7 = u_xlat2.xxxx * u_xlat7;
					    u_xlat7 = u_xlat3.yyyy * u_xlat7;
					    u_xlat4 = u_xlat3.xxxx * u_xlat4;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat7 * u_xlat2.zzzz + u_xlat0;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat2.xxxx * u_xlat5;
					    u_xlat5 = u_xlat2.yyyy * u_xlat5;
					    u_xlat4 = u_xlat3.xxxx * u_xlat4;
					    u_xlat4 = u_xlat2.yyyy * u_xlat4;
					    u_xlat0 = u_xlat4 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat5 * u_xlat2.zzzz + u_xlat0;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat36 = u_xlat0.w * 0.400000006;
					    u_xlat2.xyz = (-u_xlat0.xyz) + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat37 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat37 = u_xlat37 * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat38 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat4.x = u_xlat1.y * u_xlat38;
					    u_xlat16.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat16.x * u_xlat1.x + u_xlat4.x;
					    u_xlat5.z = u_xlat1.z * u_xlat16.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat16.x * u_xlat7.x + u_xlat4.x;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat22.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat13 = u_xlat38 * u_xlat7.y;
					    u_xlat5.y = u_xlat16.x * u_xlat1.x + u_xlat13;
					    u_xlat5.x = u_xlat16.x * u_xlat7.x + u_xlat13;
					    u_xlat22.x = u_xlat16.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat1.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat1.xyz = u_xlat2.yyy * u_xlat1.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat22.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat22.zx, 0.0);
					    u_xlat5.w = u_xlat22.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat1.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat1.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat14.xyz = u_xlat1.xyz * u_xlat3.xyz;
					    u_xlat1.x = u_xlat14.y + u_xlat14.x;
					    u_xlat1.x = u_xlat3.z * u_xlat1.z + u_xlat1.x;
					    u_xlat36 = u_xlat1.x * 0.600000024 + u_xlat36;
					    u_xlat1.x = u_xlat37;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat37 = (-u_xlat37);
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat13 = max(u_xlat37, u_xlat1.x);
					    u_xlat13 = (-u_xlat13) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat4.yzx;
					    u_xlat1.xyz = u_xlat4.xyz * vec3(u_xlat13) + u_xlat3.xyz;
					    u_xlat1.xyz = vec3(u_xlat37) * u_xlat4.zxy + u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat1.xyz;
					    u_xlat3.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat36) * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat36 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat36 = inversesqrt(u_xlat36);
					    u_xlat0.xyz = vec3(u_xlat36) * u_xlat0.xyz;
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat3.xyz = u_xlat0.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat0.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat12.xy = u_xlat14.xy * u_xlat3.xy;
					    u_xlat12.x = u_xlat12.y + u_xlat12.x;
					    u_xlat12.x = u_xlat14.z * u_xlat3.z + u_xlat12.x;
					    u_xlat12.x = u_xlat12.x / u_xlat2.x;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat12.xyz = u_xlat2.xyz * u_xlat12.xxx + u_xlat1.xyz;
					    u_xlat1.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat1.x + 1.0;
					    u_xlat1.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat12.xyz;
					    u_xlat9.xyz = in_COLOR0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * u_xlat9;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[10];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_5;
						mat4x4 _Tex2World;
						vec4 unused_0_7[32];
						float _CameraUpScale;
						vec4 unused_0_9[2];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_12[2];
						float _AAFactor;
						vec4 unused_0_14[4];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_17;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_20;
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
					uniform  sampler2D _TopdownTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec3 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat9;
					vec3 u_xlat13;
					vec2 u_xlat16;
					vec2 u_xlat18;
					bool u_xlatb18;
					float u_xlat28;
					float u_xlat29;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat9.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat9.x = (-_Size.x) + _Size.y;
					    u_xlat9.x = in_TEXCOORD3.y * u_xlat9.x + _Size.x;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x + (-u_xlat9.x);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat9.x;
					    u_xlat9.x = _Interpolator + -1.0;
					    u_xlat9.x = -abs(u_xlat9.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat9.xxxx * u_xlat3 + u_xlat2;
					    u_xlat9.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat9.x = dot(u_xlat9.xy, u_xlat9.xy);
					    u_xlat9.x = sqrt(u_xlat9.x);
					    u_xlat9.x = u_xlat9.x + u_xlat9.x;
					    u_xlat9.x = min(u_xlat9.x, 1.0);
					    u_xlat9.x = (-u_xlat9.x) + 1.0;
					    u_xlat18.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat9.x = dot(u_xlat9.xx, u_xlat18.xx);
					    u_xlat9.x = clamp(u_xlat9.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xyz = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat9.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat9.xy = u_xlat9.xy / _AoTexVolume.xz;
					    u_xlat4 = textureLod(_TopdownTex, u_xlat9.xy, 0.0);
					    u_xlat9.x = u_xlat4.w * 8.0 + _WaterLevel;
					    u_xlatb18 = _WaterLevel>=u_xlat9.x;
					    u_xlat28 = u_xlatb18 ? 1.0 : float(0.0);
					    u_xlat18.x = (u_xlatb18) ? 0.0 : 1.0;
					    u_xlat4.xy = u_xlat18.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat28 = u_xlat28 * u_xlat4.x;
					    u_xlat29 = u_xlat4.y * u_xlat28 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat29 = u_xlat3.w * u_xlat3.w;
					    u_xlat29 = (-u_xlat29) * u_xlat29 + 1.0;
					    u_xlat29 = u_xlat18.x * u_xlat29;
					    u_xlat29 = u_xlat29 * 6.0 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat13.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat13.xyz = vec3(_WindInterpolator) * u_xlat13.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat13.xyz = u_xlat13.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat5.z = (-unity_MatrixV[0].z);
					    u_xlat5.y = (-unity_MatrixV[2].z);
					    u_xlat5.x = unity_MatrixV[1].z;
					    u_xlat29 = dot(u_xlat5.yzx, u_xlat13.xyz);
					    u_xlat6.xyz = (-u_xlat5.zxy) * vec3(u_xlat29) + u_xlat13.yzx;
					    u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
					    u_xlat29 = sqrt(u_xlat29);
					    u_xlat32 = u_xlat29 + 1.0;
					    u_xlat6.xyz = u_xlat13.xyz * (-u_xlat5.xyz);
					    u_xlat13.xyz = u_xlat13.zxy * (-u_xlat5.yzx) + (-u_xlat6.xyz);
					    u_xlat6.x = dot(u_xlat13.xyz, u_xlat13.xyz);
					    u_xlat6.x = inversesqrt(u_xlat6.x);
					    u_xlat13.xyz = u_xlat13.xyz * u_xlat6.xxx;
					    u_xlat6.xyz = u_xlat13.xyz / vec3(u_xlat32);
					    u_xlat7.x = unity_MatrixV[0].x;
					    u_xlat7.y = unity_MatrixV[1].x;
					    u_xlat7.z = unity_MatrixV[2].x;
					    u_xlat8.xyz = vec3(u_xlat28) * u_xlat7.xyz;
					    u_xlat32 = (-u_xlat4.x) + 1.0;
					    u_xlat6.xyz = u_xlat6.xyz * vec3(u_xlat32) + u_xlat8.xyz;
					    u_xlat33 = u_xlat18.x * u_xlat4.x;
					    u_xlat6.xyz = u_xlat7.xyz * vec3(u_xlat33) + u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * in_TEXCOORD1.xxx;
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat7.x = u_xlat18.y * u_xlat18.y;
					    u_xlat7.x = u_xlat18.y * u_xlat7.x;
					    u_xlat16.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat7.xw = u_xlat7.xx * u_xlat16.xy;
					    u_xlat7.xw = u_xlat18.xx * u_xlat7.xw;
					    u_xlat18.x = u_xlat18.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat18.y * u_xlat33 + u_xlat32;
					    u_xlat18.xy = u_xlat18.xx * u_xlat16.xy;
					    u_xlat18.xy = u_xlat18.xy * vec2(u_xlat28) + u_xlat3.xz;
					    u_xlat7.xz = u_xlat7.xw * vec2(8.0, 8.0) + u_xlat18.xy;
					    u_xlat18.x = (-u_xlat3.y) + u_xlat9.x;
					    u_xlat7.y = u_xlat4.x * u_xlat18.x + u_xlat3.y;
					    u_xlat18.x = (-u_xlat9.x) + u_xlat3.y;
					    u_xlat9.x = u_xlat9.x + -0.100000001;
					    u_xlat9.x = max(u_xlat9.x, _WaterLevel);
					    u_xlat18.x = u_xlat18.x + u_xlat18.x;
					    u_xlat18.x = clamp(u_xlat18.x, 0.0, 1.0);
					    u_xlat18.x = u_xlat4.x * u_xlat18.x;
					    u_xlat18.x = (-u_xlat18.x) * _WindInterpolator + 1.0;
					    u_xlat8.w = u_xlat18.x * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + u_xlat7.xyz;
					    u_xlat6.xyz = (-u_xlat5.xyz) * u_xlat13.zxy;
					    u_xlat4.xyz = u_xlat13.yzx * (-u_xlat5.yzx) + (-u_xlat6.xyz);
					    u_xlat18.x = _WindInterpolator * 2.0 + 8.0;
					    u_xlat18.x = u_xlat29 * u_xlat18.x + 1.0;
					    u_xlat4.xyz = u_xlat18.xxx * u_xlat4.xyz;
					    u_xlat5.x = (-unity_MatrixV[2].x);
					    u_xlat5.y = 0.0;
					    u_xlat5.z = unity_MatrixV[0].x;
					    u_xlat5.xyz = vec3(u_xlat28) * u_xlat5.xyz;
					    vs_COLOR1.y = u_xlat28;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(u_xlat32) + u_xlat5.xyz;
					    u_xlat5.y = u_xlat33 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat33;
					    u_xlat5.x = float(0.0);
					    u_xlat5.z = float(0.0);
					    u_xlat4.xyz = u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * in_TEXCOORD1.yyy;
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.xzw * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat4.w = max(u_xlat9.x, u_xlat4.y);
					    u_xlat0 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat0 = _Tex2World[0] * u_xlat2.xxxx + u_xlat0;
					    u_xlat0 = _Tex2World[2] * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + _Tex2World[3];
					    u_xlat2 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat2 = _Tex2World[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat2;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat0 = u_xlat0 + (-u_xlat1);
					    u_xlat0.x = dot(u_xlat0, u_xlat0);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlatb0 = 2.0<u_xlat0.x;
					    u_xlat0.xyz = (bool(u_xlatb0)) ? vec3(0.0, 0.0, 0.0) : u_xlat4.xwz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat8.xyz = in_COLOR0.xyz;
					    u_xlat0 = u_xlat0.xxxx * u_xlat8;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[10];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_5;
						mat4x4 _Tex2World;
						vec4 unused_0_7[32];
						float _CameraUpScale;
						vec4 unused_0_9[2];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_12[2];
						float _AAFactor;
						vec4 unused_0_14[4];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_17;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_20;
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
					uniform  sampler2D _TopdownTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec3 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat9;
					vec3 u_xlat13;
					vec2 u_xlat16;
					vec2 u_xlat18;
					bool u_xlatb18;
					float u_xlat28;
					float u_xlat29;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat9.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat9.x = (-_Size.x) + _Size.y;
					    u_xlat9.x = in_TEXCOORD3.y * u_xlat9.x + _Size.x;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x + (-u_xlat9.x);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat9.x;
					    u_xlat9.x = _Interpolator + -1.0;
					    u_xlat9.x = -abs(u_xlat9.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat9.xxxx * u_xlat3 + u_xlat2;
					    u_xlat9.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat9.x = dot(u_xlat9.xy, u_xlat9.xy);
					    u_xlat9.x = sqrt(u_xlat9.x);
					    u_xlat9.x = u_xlat9.x + u_xlat9.x;
					    u_xlat9.x = min(u_xlat9.x, 1.0);
					    u_xlat9.x = (-u_xlat9.x) + 1.0;
					    u_xlat18.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat9.x = dot(u_xlat9.xx, u_xlat18.xx);
					    u_xlat9.x = clamp(u_xlat9.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xyz = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat9.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat9.xy = u_xlat9.xy / _AoTexVolume.xz;
					    u_xlat4 = textureLod(_TopdownTex, u_xlat9.xy, 0.0);
					    u_xlat9.x = u_xlat4.w * 8.0 + _WaterLevel;
					    u_xlatb18 = _WaterLevel>=u_xlat9.x;
					    u_xlat28 = u_xlatb18 ? 1.0 : float(0.0);
					    u_xlat18.x = (u_xlatb18) ? 0.0 : 1.0;
					    u_xlat4.xy = u_xlat18.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat28 = u_xlat28 * u_xlat4.x;
					    u_xlat29 = u_xlat4.y * u_xlat28 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat29 = u_xlat3.w * u_xlat3.w;
					    u_xlat29 = (-u_xlat29) * u_xlat29 + 1.0;
					    u_xlat29 = u_xlat18.x * u_xlat29;
					    u_xlat29 = u_xlat29 * 6.0 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat13.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat13.xyz = vec3(_WindInterpolator) * u_xlat13.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat13.xyz = u_xlat13.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat5.z = (-unity_MatrixV[0].z);
					    u_xlat5.y = (-unity_MatrixV[2].z);
					    u_xlat5.x = unity_MatrixV[1].z;
					    u_xlat29 = dot(u_xlat5.yzx, u_xlat13.xyz);
					    u_xlat6.xyz = (-u_xlat5.zxy) * vec3(u_xlat29) + u_xlat13.yzx;
					    u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
					    u_xlat29 = sqrt(u_xlat29);
					    u_xlat32 = u_xlat29 + 1.0;
					    u_xlat6.xyz = u_xlat13.xyz * (-u_xlat5.xyz);
					    u_xlat13.xyz = u_xlat13.zxy * (-u_xlat5.yzx) + (-u_xlat6.xyz);
					    u_xlat6.x = dot(u_xlat13.xyz, u_xlat13.xyz);
					    u_xlat6.x = inversesqrt(u_xlat6.x);
					    u_xlat13.xyz = u_xlat13.xyz * u_xlat6.xxx;
					    u_xlat6.xyz = u_xlat13.xyz / vec3(u_xlat32);
					    u_xlat7.x = unity_MatrixV[0].x;
					    u_xlat7.y = unity_MatrixV[1].x;
					    u_xlat7.z = unity_MatrixV[2].x;
					    u_xlat8.xyz = vec3(u_xlat28) * u_xlat7.xyz;
					    u_xlat32 = (-u_xlat4.x) + 1.0;
					    u_xlat6.xyz = u_xlat6.xyz * vec3(u_xlat32) + u_xlat8.xyz;
					    u_xlat33 = u_xlat18.x * u_xlat4.x;
					    u_xlat6.xyz = u_xlat7.xyz * vec3(u_xlat33) + u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * in_TEXCOORD1.xxx;
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat7.x = u_xlat18.y * u_xlat18.y;
					    u_xlat7.x = u_xlat18.y * u_xlat7.x;
					    u_xlat16.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat7.xw = u_xlat7.xx * u_xlat16.xy;
					    u_xlat7.xw = u_xlat18.xx * u_xlat7.xw;
					    u_xlat18.x = u_xlat18.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat18.y * u_xlat33 + u_xlat32;
					    u_xlat18.xy = u_xlat18.xx * u_xlat16.xy;
					    u_xlat18.xy = u_xlat18.xy * vec2(u_xlat28) + u_xlat3.xz;
					    u_xlat7.xz = u_xlat7.xw * vec2(8.0, 8.0) + u_xlat18.xy;
					    u_xlat18.x = (-u_xlat3.y) + u_xlat9.x;
					    u_xlat7.y = u_xlat4.x * u_xlat18.x + u_xlat3.y;
					    u_xlat18.x = (-u_xlat9.x) + u_xlat3.y;
					    u_xlat9.x = u_xlat9.x + -0.100000001;
					    u_xlat9.x = max(u_xlat9.x, _WaterLevel);
					    u_xlat18.x = u_xlat18.x + u_xlat18.x;
					    u_xlat18.x = clamp(u_xlat18.x, 0.0, 1.0);
					    u_xlat18.x = u_xlat4.x * u_xlat18.x;
					    u_xlat18.x = (-u_xlat18.x) * _WindInterpolator + 1.0;
					    u_xlat8.w = u_xlat18.x * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + u_xlat7.xyz;
					    u_xlat6.xyz = (-u_xlat5.xyz) * u_xlat13.zxy;
					    u_xlat4.xyz = u_xlat13.yzx * (-u_xlat5.yzx) + (-u_xlat6.xyz);
					    u_xlat18.x = _WindInterpolator * 2.0 + 8.0;
					    u_xlat18.x = u_xlat29 * u_xlat18.x + 1.0;
					    u_xlat4.xyz = u_xlat18.xxx * u_xlat4.xyz;
					    u_xlat5.x = (-unity_MatrixV[2].x);
					    u_xlat5.y = 0.0;
					    u_xlat5.z = unity_MatrixV[0].x;
					    u_xlat5.xyz = vec3(u_xlat28) * u_xlat5.xyz;
					    vs_COLOR1.y = u_xlat28;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(u_xlat32) + u_xlat5.xyz;
					    u_xlat5.y = u_xlat33 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat33;
					    u_xlat5.x = float(0.0);
					    u_xlat5.z = float(0.0);
					    u_xlat4.xyz = u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * in_TEXCOORD1.yyy;
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.xzw * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat4.w = max(u_xlat9.x, u_xlat4.y);
					    u_xlat0 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat0 = _Tex2World[0] * u_xlat2.xxxx + u_xlat0;
					    u_xlat0 = _Tex2World[2] * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + _Tex2World[3];
					    u_xlat2 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat2 = _Tex2World[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat2;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat0 = u_xlat0 + (-u_xlat1);
					    u_xlat0.x = dot(u_xlat0, u_xlat0);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlatb0 = 2.0<u_xlat0.x;
					    u_xlat0.xyz = (bool(u_xlatb0)) ? vec3(0.0, 0.0, 0.0) : u_xlat4.xwz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat8.xyz = in_COLOR0.xyz;
					    u_xlat0 = u_xlat0.xxxx * u_xlat8;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[10];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_5;
						mat4x4 _Tex2World;
						vec4 unused_0_7[32];
						float _CameraUpScale;
						vec4 unused_0_9[2];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_12[2];
						float _AAFactor;
						vec4 unused_0_14[4];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_17;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_20;
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
					uniform  sampler2D _TopdownTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec3 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat9;
					vec3 u_xlat13;
					vec2 u_xlat16;
					vec2 u_xlat18;
					bool u_xlatb18;
					float u_xlat28;
					float u_xlat29;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat9.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat9.x = (-_Size.x) + _Size.y;
					    u_xlat9.x = in_TEXCOORD3.y * u_xlat9.x + _Size.x;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x + (-u_xlat9.x);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat9.x;
					    u_xlat9.x = _Interpolator + -1.0;
					    u_xlat9.x = -abs(u_xlat9.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat9.xxxx * u_xlat3 + u_xlat2;
					    u_xlat9.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat9.x = dot(u_xlat9.xy, u_xlat9.xy);
					    u_xlat9.x = sqrt(u_xlat9.x);
					    u_xlat9.x = u_xlat9.x + u_xlat9.x;
					    u_xlat9.x = min(u_xlat9.x, 1.0);
					    u_xlat9.x = (-u_xlat9.x) + 1.0;
					    u_xlat18.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat9.x = dot(u_xlat9.xx, u_xlat18.xx);
					    u_xlat9.x = clamp(u_xlat9.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xyz = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat9.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat9.xy = u_xlat9.xy / _AoTexVolume.xz;
					    u_xlat4 = textureLod(_TopdownTex, u_xlat9.xy, 0.0);
					    u_xlat9.x = u_xlat4.w * 8.0 + _WaterLevel;
					    u_xlatb18 = _WaterLevel>=u_xlat9.x;
					    u_xlat28 = u_xlatb18 ? 1.0 : float(0.0);
					    u_xlat18.x = (u_xlatb18) ? 0.0 : 1.0;
					    u_xlat4.xy = u_xlat18.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat28 = u_xlat28 * u_xlat4.x;
					    u_xlat29 = u_xlat4.y * u_xlat28 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat29 = u_xlat3.w * u_xlat3.w;
					    u_xlat29 = (-u_xlat29) * u_xlat29 + 1.0;
					    u_xlat29 = u_xlat18.x * u_xlat29;
					    u_xlat29 = u_xlat29 * 6.0 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat13.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat13.xyz = vec3(_WindInterpolator) * u_xlat13.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat13.xyz = u_xlat13.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat5.z = (-unity_MatrixV[0].z);
					    u_xlat5.y = (-unity_MatrixV[2].z);
					    u_xlat5.x = unity_MatrixV[1].z;
					    u_xlat29 = dot(u_xlat5.yzx, u_xlat13.xyz);
					    u_xlat6.xyz = (-u_xlat5.zxy) * vec3(u_xlat29) + u_xlat13.yzx;
					    u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
					    u_xlat29 = sqrt(u_xlat29);
					    u_xlat32 = u_xlat29 + 1.0;
					    u_xlat6.xyz = u_xlat13.xyz * (-u_xlat5.xyz);
					    u_xlat13.xyz = u_xlat13.zxy * (-u_xlat5.yzx) + (-u_xlat6.xyz);
					    u_xlat6.x = dot(u_xlat13.xyz, u_xlat13.xyz);
					    u_xlat6.x = inversesqrt(u_xlat6.x);
					    u_xlat13.xyz = u_xlat13.xyz * u_xlat6.xxx;
					    u_xlat6.xyz = u_xlat13.xyz / vec3(u_xlat32);
					    u_xlat7.x = unity_MatrixV[0].x;
					    u_xlat7.y = unity_MatrixV[1].x;
					    u_xlat7.z = unity_MatrixV[2].x;
					    u_xlat8.xyz = vec3(u_xlat28) * u_xlat7.xyz;
					    u_xlat32 = (-u_xlat4.x) + 1.0;
					    u_xlat6.xyz = u_xlat6.xyz * vec3(u_xlat32) + u_xlat8.xyz;
					    u_xlat33 = u_xlat18.x * u_xlat4.x;
					    u_xlat6.xyz = u_xlat7.xyz * vec3(u_xlat33) + u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * in_TEXCOORD1.xxx;
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat7.x = u_xlat18.y * u_xlat18.y;
					    u_xlat7.x = u_xlat18.y * u_xlat7.x;
					    u_xlat16.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat7.xw = u_xlat7.xx * u_xlat16.xy;
					    u_xlat7.xw = u_xlat18.xx * u_xlat7.xw;
					    u_xlat18.x = u_xlat18.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat18.y * u_xlat33 + u_xlat32;
					    u_xlat18.xy = u_xlat18.xx * u_xlat16.xy;
					    u_xlat18.xy = u_xlat18.xy * vec2(u_xlat28) + u_xlat3.xz;
					    u_xlat7.xz = u_xlat7.xw * vec2(8.0, 8.0) + u_xlat18.xy;
					    u_xlat18.x = (-u_xlat3.y) + u_xlat9.x;
					    u_xlat7.y = u_xlat4.x * u_xlat18.x + u_xlat3.y;
					    u_xlat18.x = (-u_xlat9.x) + u_xlat3.y;
					    u_xlat9.x = u_xlat9.x + -0.100000001;
					    u_xlat9.x = max(u_xlat9.x, _WaterLevel);
					    u_xlat18.x = u_xlat18.x + u_xlat18.x;
					    u_xlat18.x = clamp(u_xlat18.x, 0.0, 1.0);
					    u_xlat18.x = u_xlat4.x * u_xlat18.x;
					    u_xlat18.x = (-u_xlat18.x) * _WindInterpolator + 1.0;
					    u_xlat8.w = u_xlat18.x * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + u_xlat7.xyz;
					    u_xlat6.xyz = (-u_xlat5.xyz) * u_xlat13.zxy;
					    u_xlat4.xyz = u_xlat13.yzx * (-u_xlat5.yzx) + (-u_xlat6.xyz);
					    u_xlat18.x = _WindInterpolator * 2.0 + 8.0;
					    u_xlat18.x = u_xlat29 * u_xlat18.x + 1.0;
					    u_xlat4.xyz = u_xlat18.xxx * u_xlat4.xyz;
					    u_xlat5.x = (-unity_MatrixV[2].x);
					    u_xlat5.y = 0.0;
					    u_xlat5.z = unity_MatrixV[0].x;
					    u_xlat5.xyz = vec3(u_xlat28) * u_xlat5.xyz;
					    vs_COLOR1.y = u_xlat28;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(u_xlat32) + u_xlat5.xyz;
					    u_xlat5.y = u_xlat33 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat33;
					    u_xlat5.x = float(0.0);
					    u_xlat5.z = float(0.0);
					    u_xlat4.xyz = u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * in_TEXCOORD1.yyy;
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.xzw * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat4.w = max(u_xlat9.x, u_xlat4.y);
					    u_xlat0 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat0 = _Tex2World[0] * u_xlat2.xxxx + u_xlat0;
					    u_xlat0 = _Tex2World[2] * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + _Tex2World[3];
					    u_xlat2 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat2 = _Tex2World[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat2;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat0 = u_xlat0 + (-u_xlat1);
					    u_xlat0.x = dot(u_xlat0, u_xlat0);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlatb0 = 2.0<u_xlat0.x;
					    u_xlat0.xyz = (bool(u_xlatb0)) ? vec3(0.0, 0.0, 0.0) : u_xlat4.xwz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat8.xyz = in_COLOR0.xyz;
					    u_xlat0 = u_xlat0.xxxx * u_xlat8;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[10];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_5;
						mat4x4 _Tex2World;
						vec4 unused_0_7[32];
						float _CameraUpScale;
						vec4 unused_0_9[2];
						float _WaterLevel;
						float _LineWidth;
						vec4 unused_0_12[2];
						float _AAFactor;
						vec4 unused_0_14[4];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_17;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_20;
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
					uniform  sampler2D _TopdownTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec3 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat9;
					vec3 u_xlat13;
					vec2 u_xlat16;
					vec2 u_xlat18;
					bool u_xlatb18;
					float u_xlat28;
					float u_xlat29;
					float u_xlat32;
					float u_xlat33;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat9.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat9.x = (-_Size.x) + _Size.y;
					    u_xlat9.x = in_TEXCOORD3.y * u_xlat9.x + _Size.x;
					    u_xlat0.x = u_xlat9.x * u_xlat0.x + (-u_xlat9.x);
					    u_xlat0.x = _ScreenSpaceSize * u_xlat0.x + u_xlat9.x;
					    u_xlat9.x = _Interpolator + -1.0;
					    u_xlat9.x = -abs(u_xlat9.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat9.xxxx * u_xlat3 + u_xlat2;
					    u_xlat9.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat9.x = dot(u_xlat9.xy, u_xlat9.xy);
					    u_xlat9.x = sqrt(u_xlat9.x);
					    u_xlat9.x = u_xlat9.x + u_xlat9.x;
					    u_xlat9.x = min(u_xlat9.x, 1.0);
					    u_xlat9.x = (-u_xlat9.x) + 1.0;
					    u_xlat18.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat9.x = dot(u_xlat9.xx, u_xlat18.xx);
					    u_xlat9.x = clamp(u_xlat9.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat9.x * u_xlat0.x;
					    u_xlat4.xyz = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xyz = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xyz;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat9.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat9.xy = u_xlat9.xy / _AoTexVolume.xz;
					    u_xlat4 = textureLod(_TopdownTex, u_xlat9.xy, 0.0);
					    u_xlat9.x = u_xlat4.w * 8.0 + _WaterLevel;
					    u_xlatb18 = _WaterLevel>=u_xlat9.x;
					    u_xlat28 = u_xlatb18 ? 1.0 : float(0.0);
					    u_xlat18.x = (u_xlatb18) ? 0.0 : 1.0;
					    u_xlat4.xy = u_xlat18.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat28 = u_xlat28 * u_xlat4.x;
					    u_xlat29 = u_xlat4.y * u_xlat28 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat29 = u_xlat3.w * u_xlat3.w;
					    u_xlat29 = (-u_xlat29) * u_xlat29 + 1.0;
					    u_xlat29 = u_xlat18.x * u_xlat29;
					    u_xlat29 = u_xlat29 * 6.0 + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat29;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat13.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat13.xyz = vec3(_WindInterpolator) * u_xlat13.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat13.xyz = u_xlat13.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat5.z = (-unity_MatrixV[0].z);
					    u_xlat5.y = (-unity_MatrixV[2].z);
					    u_xlat5.x = unity_MatrixV[1].z;
					    u_xlat29 = dot(u_xlat5.yzx, u_xlat13.xyz);
					    u_xlat6.xyz = (-u_xlat5.zxy) * vec3(u_xlat29) + u_xlat13.yzx;
					    u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
					    u_xlat29 = sqrt(u_xlat29);
					    u_xlat32 = u_xlat29 + 1.0;
					    u_xlat6.xyz = u_xlat13.xyz * (-u_xlat5.xyz);
					    u_xlat13.xyz = u_xlat13.zxy * (-u_xlat5.yzx) + (-u_xlat6.xyz);
					    u_xlat6.x = dot(u_xlat13.xyz, u_xlat13.xyz);
					    u_xlat6.x = inversesqrt(u_xlat6.x);
					    u_xlat13.xyz = u_xlat13.xyz * u_xlat6.xxx;
					    u_xlat6.xyz = u_xlat13.xyz / vec3(u_xlat32);
					    u_xlat7.x = unity_MatrixV[0].x;
					    u_xlat7.y = unity_MatrixV[1].x;
					    u_xlat7.z = unity_MatrixV[2].x;
					    u_xlat8.xyz = vec3(u_xlat28) * u_xlat7.xyz;
					    u_xlat32 = (-u_xlat4.x) + 1.0;
					    u_xlat6.xyz = u_xlat6.xyz * vec3(u_xlat32) + u_xlat8.xyz;
					    u_xlat33 = u_xlat18.x * u_xlat4.x;
					    u_xlat6.xyz = u_xlat7.xyz * vec3(u_xlat33) + u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * in_TEXCOORD1.xxx;
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat7.x = u_xlat18.y * u_xlat18.y;
					    u_xlat7.x = u_xlat18.y * u_xlat7.x;
					    u_xlat16.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat7.xw = u_xlat7.xx * u_xlat16.xy;
					    u_xlat7.xw = u_xlat18.xx * u_xlat7.xw;
					    u_xlat18.x = u_xlat18.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat18.y * u_xlat33 + u_xlat32;
					    u_xlat18.xy = u_xlat18.xx * u_xlat16.xy;
					    u_xlat18.xy = u_xlat18.xy * vec2(u_xlat28) + u_xlat3.xz;
					    u_xlat7.xz = u_xlat7.xw * vec2(8.0, 8.0) + u_xlat18.xy;
					    u_xlat18.x = (-u_xlat3.y) + u_xlat9.x;
					    u_xlat7.y = u_xlat4.x * u_xlat18.x + u_xlat3.y;
					    u_xlat18.x = (-u_xlat9.x) + u_xlat3.y;
					    u_xlat9.x = u_xlat9.x + -0.100000001;
					    u_xlat9.x = max(u_xlat9.x, _WaterLevel);
					    u_xlat18.x = u_xlat18.x + u_xlat18.x;
					    u_xlat18.x = clamp(u_xlat18.x, 0.0, 1.0);
					    u_xlat18.x = u_xlat4.x * u_xlat18.x;
					    u_xlat18.x = (-u_xlat18.x) * _WindInterpolator + 1.0;
					    u_xlat8.w = u_xlat18.x * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + u_xlat7.xyz;
					    u_xlat6.xyz = (-u_xlat5.xyz) * u_xlat13.zxy;
					    u_xlat4.xyz = u_xlat13.yzx * (-u_xlat5.yzx) + (-u_xlat6.xyz);
					    u_xlat18.x = _WindInterpolator * 2.0 + 8.0;
					    u_xlat18.x = u_xlat29 * u_xlat18.x + 1.0;
					    u_xlat4.xyz = u_xlat18.xxx * u_xlat4.xyz;
					    u_xlat5.x = (-unity_MatrixV[2].x);
					    u_xlat5.y = 0.0;
					    u_xlat5.z = unity_MatrixV[0].x;
					    u_xlat5.xyz = vec3(u_xlat28) * u_xlat5.xyz;
					    vs_COLOR1.y = u_xlat28;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(u_xlat32) + u_xlat5.xyz;
					    u_xlat5.y = u_xlat33 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat33;
					    u_xlat5.x = float(0.0);
					    u_xlat5.z = float(0.0);
					    u_xlat4.xyz = u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * in_TEXCOORD1.yyy;
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.xzw * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat4.w = max(u_xlat9.x, u_xlat4.y);
					    u_xlat0 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat0 = _Tex2World[0] * u_xlat2.xxxx + u_xlat0;
					    u_xlat0 = _Tex2World[2] * u_xlat2.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + _Tex2World[3];
					    u_xlat2 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat2 = _Tex2World[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat2;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat0 = u_xlat0 + (-u_xlat1);
					    u_xlat0.x = dot(u_xlat0, u_xlat0);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlatb0 = 2.0<u_xlat0.x;
					    u_xlat0.xyz = (bool(u_xlatb0)) ? vec3(0.0, 0.0, 0.0) : u_xlat4.xwz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.x = unity_MatrixV[1].z * -0.0300000012 + 1.0;
					    u_xlat8.xyz = in_COLOR0.xyz;
					    u_xlat0 = u_xlat0.xxxx * u_xlat8;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_6[6];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_9;
						mat4x4 _Tex2World;
						vec4 unused_0_11[12];
						vec3 _SunDir;
						vec4 unused_0_13[7];
						float _Year;
						vec4 unused_0_15[7];
						vec4 _SnowColor;
						vec4 unused_0_17;
						float _SnowAmount;
						vec4 unused_0_19;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_27;
						float _AAFactor;
						vec4 unused_0_29[4];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_32;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_35;
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
					uniform  sampler2D _TopdownTex;
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
					out vec4 vs_COLOR1;
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
					vec4 u_xlat10;
					vec3 u_xlat12;
					bool u_xlatb12;
					vec3 u_xlat14;
					vec3 u_xlat20;
					vec3 u_xlat23;
					vec2 u_xlat24;
					vec2 u_xlat28;
					vec2 u_xlat32;
					float u_xlat36;
					bool u_xlatb36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat41;
					float u_xlat42;
					float u_xlat43;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat12.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat12.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat12.x = (-_Size.x) + _Size.y;
					    u_xlat12.x = in_TEXCOORD3.y * u_xlat12.x + _Size.x;
					    u_xlat24.x = u_xlat12.x * u_xlat0.x + (-u_xlat12.x);
					    u_xlat12.x = _ScreenSpaceSize * u_xlat24.x + u_xlat12.x;
					    u_xlat24.x = _Interpolator + -1.0;
					    u_xlat24.x = -abs(u_xlat24.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat24.xxxx * u_xlat3 + u_xlat2;
					    u_xlat24.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat24.x = min(u_xlat24.x, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat4.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat24.x = dot(u_xlat24.xx, u_xlat4.xx);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat12.x = u_xlat24.x * u_xlat12.x;
					    u_xlat4.xzw = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xzw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xzw;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xzw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat24.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat24.xy = u_xlat24.xy / _AoTexVolume.xz;
					    u_xlat5 = textureLod(_TopdownTex, u_xlat24.xy, 0.0);
					    u_xlat24.x = u_xlat5.w * 8.0 + _WaterLevel;
					    u_xlatb36 = _WaterLevel>=u_xlat24.x;
					    u_xlat37 = u_xlatb36 ? 1.0 : float(0.0);
					    u_xlat36 = (u_xlatb36) ? 0.0 : 1.0;
					    u_xlat4.xz = u_xlat4.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat37 = u_xlat37 * u_xlat4.x;
					    u_xlat38 = u_xlat4.z * u_xlat37 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat38 = u_xlat3.w * u_xlat3.w;
					    u_xlat38 = (-u_xlat38) * u_xlat38 + 1.0;
					    u_xlat38 = u_xlat36 * u_xlat38;
					    u_xlat38 = u_xlat38 * 6.0 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat5.xyz = vec3(_WindInterpolator) * u_xlat5.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat5.xyz = u_xlat5.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat6.x = unity_MatrixV[0].z;
					    u_xlat6.y = unity_MatrixV[2].z;
					    u_xlat7.xz = (-u_xlat6.xy);
					    u_xlat28.xy = u_xlat6.xy / unity_MatrixV[1].zz;
					    u_xlat7.y = unity_MatrixV[1].z;
					    u_xlat38 = dot(u_xlat7.zxy, u_xlat5.xyz);
					    u_xlat6.xyz = (-u_xlat7.xyz) * vec3(u_xlat38) + u_xlat5.yzx;
					    u_xlat38 = dot(u_xlat6.xyz, u_xlat6.xyz);
					    u_xlat38 = sqrt(u_xlat38);
					    u_xlat41 = u_xlat38 + 1.0;
					    u_xlat6.xyz = u_xlat5.xyz * (-u_xlat7.yzx);
					    u_xlat5.xyz = u_xlat5.zxy * (-u_xlat7.zxy) + (-u_xlat6.xyz);
					    u_xlat6.x = dot(u_xlat5.xyz, u_xlat5.xyz);
					    u_xlat6.x = inversesqrt(u_xlat6.x);
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat6.xxx;
					    u_xlat6.xyz = u_xlat5.xyz / vec3(u_xlat41);
					    u_xlat8.x = unity_MatrixV[0].x;
					    u_xlat8.y = unity_MatrixV[1].x;
					    u_xlat8.z = unity_MatrixV[2].x;
					    u_xlat9.xyz = vec3(u_xlat37) * u_xlat8.xyz;
					    u_xlat41 = (-u_xlat4.x) + 1.0;
					    u_xlat6.xyz = u_xlat6.xyz * vec3(u_xlat41) + u_xlat9.xyz;
					    u_xlat42 = u_xlat36 * u_xlat4.x;
					    u_xlat6.xyz = u_xlat8.xyz * vec3(u_xlat42) + u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * in_TEXCOORD1.xxx;
					    u_xlat6.xyz = u_xlat12.xxx * u_xlat6.xyz;
					    u_xlat43 = u_xlat4.y * u_xlat4.y;
					    u_xlat43 = u_xlat4.y * u_xlat43;
					    u_xlat8.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat32.xy = vec2(u_xlat43) * u_xlat8.xy;
					    u_xlat32.xy = vec2(u_xlat36) * u_xlat32.xy;
					    u_xlat36 = u_xlat4.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat4.y * u_xlat42 + u_xlat41;
					    u_xlat8.xy = vec2(u_xlat36) * u_xlat8.xy;
					    u_xlat3.xz = u_xlat8.xy * vec2(u_xlat37) + u_xlat3.xz;
					    u_xlat8.xz = u_xlat32.xy * vec2(8.0, 8.0) + u_xlat3.xz;
					    u_xlat36 = (-u_xlat3.y) + u_xlat24.x;
					    u_xlat8.y = u_xlat4.x * u_xlat36 + u_xlat3.y;
					    u_xlat36 = (-u_xlat24.x) + u_xlat3.y;
					    u_xlat24.x = u_xlat24.x + -0.100000001;
					    u_xlat24.x = max(u_xlat24.x, _WaterLevel);
					    u_xlat36 = u_xlat36 + u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat36 = u_xlat4.x * u_xlat36;
					    u_xlat36 = (-u_xlat36) * _WindInterpolator + 1.0;
					    u_xlat9.w = u_xlat36 * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + u_xlat8.xyz;
					    u_xlat6.xyz = (-u_xlat7.yzx) * u_xlat5.zxy;
					    u_xlat5.xyz = u_xlat5.yzx * (-u_xlat7.zxy) + (-u_xlat6.xyz);
					    u_xlat36 = _WindInterpolator * 2.0 + 8.0;
					    u_xlat36 = u_xlat38 * u_xlat36 + 1.0;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.xyz;
					    u_xlat6.x = (-unity_MatrixV[2].x);
					    u_xlat6.y = 0.0;
					    u_xlat6.z = unity_MatrixV[0].x;
					    u_xlat6.xyz = vec3(u_xlat37) * u_xlat6.xyz;
					    vs_COLOR1.y = u_xlat37;
					    u_xlat5.xyz = u_xlat5.xyz * vec3(u_xlat41) + u_xlat6.xyz;
					    u_xlat6.y = u_xlat42 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat42;
					    u_xlat6.x = float(0.0);
					    u_xlat6.z = float(0.0);
					    u_xlat5.xyz = u_xlat5.xyz + u_xlat6.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * in_TEXCOORD1.yyy;
					    u_xlat5.xyz = u_xlat12.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat5.w = max(u_xlat24.x, u_xlat5.y);
					    u_xlat6 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat6 = _Tex2World[0] * u_xlat2.xxxx + u_xlat6;
					    u_xlat2 = _Tex2World[2] * u_xlat2.zzzz + u_xlat6;
					    u_xlat2 = u_xlat2 + _Tex2World[3];
					    u_xlat6 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat6 = _Tex2World[0] * u_xlat1.xxxx + u_xlat6;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat6;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat1 = (-u_xlat1) + u_xlat2;
					    u_xlat12.x = dot(u_xlat1, u_xlat1);
					    u_xlat12.x = sqrt(u_xlat12.x);
					    u_xlatb12 = 2.0<u_xlat12.x;
					    u_xlat1.xyw = (bool(u_xlatb12)) ? vec3(0.0, 0.0, 0.0) : u_xlat5.xzw;
					    u_xlat12.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat24.xy = u_xlat28.xy * u_xlat12.xx + u_xlat1.xy;
					    u_xlat0.x = u_xlat12.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat12.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat12.x;
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
					    u_xlat36 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat38 = u_xlat0.y * u_xlat36;
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat38;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat8.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat38;
					    u_xlat10 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat23.yz = u_xlat5.yx;
					    u_xlat10.xyz = u_xlat2.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat3.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat3.zzz * u_xlat10.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat10.xyz;
					    u_xlat12.x = u_xlat36 * u_xlat8.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat12.x;
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat12.x;
					    u_xlat23.x = u_xlat4.y * u_xlat8.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4.xyz = u_xlat2.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat3.xxx * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat5.w = u_xlat23.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat0.xyz) + (-u_xlat7.xyz);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat36 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat0.y * u_xlat37;
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat38;
					    u_xlat4.z = u_xlat0.z * u_xlat3.y;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat38;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat20.yz = u_xlat4.yx;
					    u_xlat7.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.yyy * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.zzz * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat7.xyz;
					    u_xlat12.x = u_xlat37 * u_xlat6.y;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat12.x;
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat12.x;
					    u_xlat20.x = u_xlat3.y * u_xlat6.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat1.yyy * u_xlat0.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat2.zzz + u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat20.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat20.zx, 0.0);
					    u_xlat4.w = u_xlat20.x;
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat3.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat14.xyz = u_xlat1.xxx * u_xlat4.xyz;
					    u_xlat14.xyz = u_xlat1.yyy * u_xlat14.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat1.xyw = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat1.xyw * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat1.y = abs(_SunDir.y);
					    u_xlat1.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat1.xyz = u_xlat1.xyz;
					    u_xlat1.xyz = clamp(u_xlat1.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat1.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat1.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.200000003;
					    u_xlat12.x = u_xlat36;
					    u_xlat12.x = clamp(u_xlat12.x, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat24.x = max(u_xlat36, u_xlat12.x);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat1.x = _Year;
					    u_xlat1.y = 0.0;
					    u_xlat1 = textureLod(_GrassTex, u_xlat1.xy, 0.0);
					    u_xlat2.xyz = u_xlat12.xxx * u_xlat1.yzx;
					    u_xlat2.xyz = u_xlat1.xyz * u_xlat24.xxx + u_xlat2.xyz;
					    u_xlat12.xyz = vec3(u_xlat36) * u_xlat1.zxy + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat12.xyz) + _SnowColor.xyz;
					    u_xlat12.xyz = vec3(_SnowAmount) * u_xlat1.xyz + u_xlat12.xyz;
					    u_xlat1.xyz = u_xlat12.xyz * _MinAmbientColor.xyz;
					    u_xlat12.xyz = (-_MinAmbientColor.xyz) * u_xlat12.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat12.xyz + u_xlat1.xyz;
					    u_xlat9.xyz = in_COLOR0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * u_xlat9;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_6[6];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_9;
						mat4x4 _Tex2World;
						vec4 unused_0_11[12];
						vec3 _SunDir;
						vec4 unused_0_13[7];
						float _Year;
						vec4 unused_0_15[7];
						vec4 _SnowColor;
						vec4 unused_0_17;
						float _SnowAmount;
						vec4 unused_0_19;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_27;
						float _AAFactor;
						vec4 unused_0_29[4];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_32;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_35;
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
					uniform  sampler2D _TopdownTex;
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
					out vec4 vs_COLOR1;
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
					vec4 u_xlat10;
					vec3 u_xlat12;
					bool u_xlatb12;
					vec3 u_xlat14;
					vec3 u_xlat20;
					vec3 u_xlat23;
					vec2 u_xlat24;
					vec2 u_xlat28;
					vec2 u_xlat32;
					float u_xlat36;
					bool u_xlatb36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat41;
					float u_xlat42;
					float u_xlat43;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat12.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat12.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat12.x = (-_Size.x) + _Size.y;
					    u_xlat12.x = in_TEXCOORD3.y * u_xlat12.x + _Size.x;
					    u_xlat24.x = u_xlat12.x * u_xlat0.x + (-u_xlat12.x);
					    u_xlat12.x = _ScreenSpaceSize * u_xlat24.x + u_xlat12.x;
					    u_xlat24.x = _Interpolator + -1.0;
					    u_xlat24.x = -abs(u_xlat24.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat24.xxxx * u_xlat3 + u_xlat2;
					    u_xlat24.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat24.x = min(u_xlat24.x, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat4.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat24.x = dot(u_xlat24.xx, u_xlat4.xx);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat12.x = u_xlat24.x * u_xlat12.x;
					    u_xlat4.xzw = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xzw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xzw;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xzw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat24.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat24.xy = u_xlat24.xy / _AoTexVolume.xz;
					    u_xlat5 = textureLod(_TopdownTex, u_xlat24.xy, 0.0);
					    u_xlat24.x = u_xlat5.w * 8.0 + _WaterLevel;
					    u_xlatb36 = _WaterLevel>=u_xlat24.x;
					    u_xlat37 = u_xlatb36 ? 1.0 : float(0.0);
					    u_xlat36 = (u_xlatb36) ? 0.0 : 1.0;
					    u_xlat4.xz = u_xlat4.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat37 = u_xlat37 * u_xlat4.x;
					    u_xlat38 = u_xlat4.z * u_xlat37 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat38 = u_xlat3.w * u_xlat3.w;
					    u_xlat38 = (-u_xlat38) * u_xlat38 + 1.0;
					    u_xlat38 = u_xlat36 * u_xlat38;
					    u_xlat38 = u_xlat38 * 6.0 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat5.xyz = vec3(_WindInterpolator) * u_xlat5.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat5.xyz = u_xlat5.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat6.x = unity_MatrixV[0].z;
					    u_xlat6.y = unity_MatrixV[2].z;
					    u_xlat7.xz = (-u_xlat6.xy);
					    u_xlat28.xy = u_xlat6.xy / unity_MatrixV[1].zz;
					    u_xlat7.y = unity_MatrixV[1].z;
					    u_xlat38 = dot(u_xlat7.zxy, u_xlat5.xyz);
					    u_xlat6.xyz = (-u_xlat7.xyz) * vec3(u_xlat38) + u_xlat5.yzx;
					    u_xlat38 = dot(u_xlat6.xyz, u_xlat6.xyz);
					    u_xlat38 = sqrt(u_xlat38);
					    u_xlat41 = u_xlat38 + 1.0;
					    u_xlat6.xyz = u_xlat5.xyz * (-u_xlat7.yzx);
					    u_xlat5.xyz = u_xlat5.zxy * (-u_xlat7.zxy) + (-u_xlat6.xyz);
					    u_xlat6.x = dot(u_xlat5.xyz, u_xlat5.xyz);
					    u_xlat6.x = inversesqrt(u_xlat6.x);
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat6.xxx;
					    u_xlat6.xyz = u_xlat5.xyz / vec3(u_xlat41);
					    u_xlat8.x = unity_MatrixV[0].x;
					    u_xlat8.y = unity_MatrixV[1].x;
					    u_xlat8.z = unity_MatrixV[2].x;
					    u_xlat9.xyz = vec3(u_xlat37) * u_xlat8.xyz;
					    u_xlat41 = (-u_xlat4.x) + 1.0;
					    u_xlat6.xyz = u_xlat6.xyz * vec3(u_xlat41) + u_xlat9.xyz;
					    u_xlat42 = u_xlat36 * u_xlat4.x;
					    u_xlat6.xyz = u_xlat8.xyz * vec3(u_xlat42) + u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * in_TEXCOORD1.xxx;
					    u_xlat6.xyz = u_xlat12.xxx * u_xlat6.xyz;
					    u_xlat43 = u_xlat4.y * u_xlat4.y;
					    u_xlat43 = u_xlat4.y * u_xlat43;
					    u_xlat8.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat32.xy = vec2(u_xlat43) * u_xlat8.xy;
					    u_xlat32.xy = vec2(u_xlat36) * u_xlat32.xy;
					    u_xlat36 = u_xlat4.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat4.y * u_xlat42 + u_xlat41;
					    u_xlat8.xy = vec2(u_xlat36) * u_xlat8.xy;
					    u_xlat3.xz = u_xlat8.xy * vec2(u_xlat37) + u_xlat3.xz;
					    u_xlat8.xz = u_xlat32.xy * vec2(8.0, 8.0) + u_xlat3.xz;
					    u_xlat36 = (-u_xlat3.y) + u_xlat24.x;
					    u_xlat8.y = u_xlat4.x * u_xlat36 + u_xlat3.y;
					    u_xlat36 = (-u_xlat24.x) + u_xlat3.y;
					    u_xlat24.x = u_xlat24.x + -0.100000001;
					    u_xlat24.x = max(u_xlat24.x, _WaterLevel);
					    u_xlat36 = u_xlat36 + u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat36 = u_xlat4.x * u_xlat36;
					    u_xlat36 = (-u_xlat36) * _WindInterpolator + 1.0;
					    u_xlat9.w = u_xlat36 * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + u_xlat8.xyz;
					    u_xlat6.xyz = (-u_xlat7.yzx) * u_xlat5.zxy;
					    u_xlat5.xyz = u_xlat5.yzx * (-u_xlat7.zxy) + (-u_xlat6.xyz);
					    u_xlat36 = _WindInterpolator * 2.0 + 8.0;
					    u_xlat36 = u_xlat38 * u_xlat36 + 1.0;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.xyz;
					    u_xlat6.x = (-unity_MatrixV[2].x);
					    u_xlat6.y = 0.0;
					    u_xlat6.z = unity_MatrixV[0].x;
					    u_xlat6.xyz = vec3(u_xlat37) * u_xlat6.xyz;
					    vs_COLOR1.y = u_xlat37;
					    u_xlat5.xyz = u_xlat5.xyz * vec3(u_xlat41) + u_xlat6.xyz;
					    u_xlat6.y = u_xlat42 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat42;
					    u_xlat6.x = float(0.0);
					    u_xlat6.z = float(0.0);
					    u_xlat5.xyz = u_xlat5.xyz + u_xlat6.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * in_TEXCOORD1.yyy;
					    u_xlat5.xyz = u_xlat12.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat5.w = max(u_xlat24.x, u_xlat5.y);
					    u_xlat6 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat6 = _Tex2World[0] * u_xlat2.xxxx + u_xlat6;
					    u_xlat2 = _Tex2World[2] * u_xlat2.zzzz + u_xlat6;
					    u_xlat2 = u_xlat2 + _Tex2World[3];
					    u_xlat6 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat6 = _Tex2World[0] * u_xlat1.xxxx + u_xlat6;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat6;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat1 = (-u_xlat1) + u_xlat2;
					    u_xlat12.x = dot(u_xlat1, u_xlat1);
					    u_xlat12.x = sqrt(u_xlat12.x);
					    u_xlatb12 = 2.0<u_xlat12.x;
					    u_xlat1.xyw = (bool(u_xlatb12)) ? vec3(0.0, 0.0, 0.0) : u_xlat5.xzw;
					    u_xlat12.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat24.xy = u_xlat28.xy * u_xlat12.xx + u_xlat1.xy;
					    u_xlat0.x = u_xlat12.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat12.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat12.x;
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
					    u_xlat36 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat38 = u_xlat0.y * u_xlat36;
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat38;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat8.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat38;
					    u_xlat10 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat23.yz = u_xlat5.yx;
					    u_xlat10.xyz = u_xlat2.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat3.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat3.zzz * u_xlat10.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat10.xyz;
					    u_xlat12.x = u_xlat36 * u_xlat8.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat12.x;
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat12.x;
					    u_xlat23.x = u_xlat4.y * u_xlat8.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4.xyz = u_xlat2.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat3.xxx * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat5.w = u_xlat23.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat0.xyz) + (-u_xlat7.xyz);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat36 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat0.y * u_xlat37;
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat38;
					    u_xlat4.z = u_xlat0.z * u_xlat3.y;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat38;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat20.yz = u_xlat4.yx;
					    u_xlat7.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.yyy * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.zzz * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat7.xyz;
					    u_xlat12.x = u_xlat37 * u_xlat6.y;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat12.x;
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat12.x;
					    u_xlat20.x = u_xlat3.y * u_xlat6.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat1.yyy * u_xlat0.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat2.zzz + u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat20.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat20.zx, 0.0);
					    u_xlat4.w = u_xlat20.x;
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat3.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat14.xyz = u_xlat1.xxx * u_xlat4.xyz;
					    u_xlat14.xyz = u_xlat1.yyy * u_xlat14.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat1.xyw = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat1.xyw * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat1.y = abs(_SunDir.y);
					    u_xlat1.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat1.xyz = u_xlat1.xyz;
					    u_xlat1.xyz = clamp(u_xlat1.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat1.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat1.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.200000003;
					    u_xlat12.x = u_xlat36;
					    u_xlat12.x = clamp(u_xlat12.x, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat24.x = max(u_xlat36, u_xlat12.x);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat1.x = _Year;
					    u_xlat1.y = 0.0;
					    u_xlat1 = textureLod(_GrassTex, u_xlat1.xy, 0.0);
					    u_xlat2.xyz = u_xlat12.xxx * u_xlat1.yzx;
					    u_xlat2.xyz = u_xlat1.xyz * u_xlat24.xxx + u_xlat2.xyz;
					    u_xlat12.xyz = vec3(u_xlat36) * u_xlat1.zxy + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat12.xyz) + _SnowColor.xyz;
					    u_xlat12.xyz = vec3(_SnowAmount) * u_xlat1.xyz + u_xlat12.xyz;
					    u_xlat1.xyz = u_xlat12.xyz * _MinAmbientColor.xyz;
					    u_xlat12.xyz = (-_MinAmbientColor.xyz) * u_xlat12.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat12.xyz + u_xlat1.xyz;
					    u_xlat9.xyz = in_COLOR0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * u_xlat9;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_6[6];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_9;
						mat4x4 _Tex2World;
						vec4 unused_0_11[12];
						vec3 _SunDir;
						vec4 unused_0_13[7];
						float _Year;
						vec4 unused_0_15[7];
						vec4 _SnowColor;
						vec4 unused_0_17;
						float _SnowAmount;
						vec4 unused_0_19;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_27;
						float _AAFactor;
						vec4 unused_0_29[4];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_32;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_35;
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
					uniform  sampler2D _TopdownTex;
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
					out vec4 vs_COLOR1;
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
					vec4 u_xlat10;
					vec3 u_xlat12;
					bool u_xlatb12;
					vec3 u_xlat14;
					vec3 u_xlat20;
					vec3 u_xlat23;
					vec2 u_xlat24;
					vec2 u_xlat28;
					vec2 u_xlat32;
					float u_xlat36;
					bool u_xlatb36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat41;
					float u_xlat42;
					float u_xlat43;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat12.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat12.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat12.x = (-_Size.x) + _Size.y;
					    u_xlat12.x = in_TEXCOORD3.y * u_xlat12.x + _Size.x;
					    u_xlat24.x = u_xlat12.x * u_xlat0.x + (-u_xlat12.x);
					    u_xlat12.x = _ScreenSpaceSize * u_xlat24.x + u_xlat12.x;
					    u_xlat24.x = _Interpolator + -1.0;
					    u_xlat24.x = -abs(u_xlat24.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat24.xxxx * u_xlat3 + u_xlat2;
					    u_xlat24.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat24.x = min(u_xlat24.x, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat4.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat24.x = dot(u_xlat24.xx, u_xlat4.xx);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat12.x = u_xlat24.x * u_xlat12.x;
					    u_xlat4.xzw = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xzw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xzw;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xzw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat24.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat24.xy = u_xlat24.xy / _AoTexVolume.xz;
					    u_xlat5 = textureLod(_TopdownTex, u_xlat24.xy, 0.0);
					    u_xlat24.x = u_xlat5.w * 8.0 + _WaterLevel;
					    u_xlatb36 = _WaterLevel>=u_xlat24.x;
					    u_xlat37 = u_xlatb36 ? 1.0 : float(0.0);
					    u_xlat36 = (u_xlatb36) ? 0.0 : 1.0;
					    u_xlat4.xz = u_xlat4.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat37 = u_xlat37 * u_xlat4.x;
					    u_xlat38 = u_xlat4.z * u_xlat37 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat38 = u_xlat3.w * u_xlat3.w;
					    u_xlat38 = (-u_xlat38) * u_xlat38 + 1.0;
					    u_xlat38 = u_xlat36 * u_xlat38;
					    u_xlat38 = u_xlat38 * 6.0 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat5.xyz = vec3(_WindInterpolator) * u_xlat5.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat5.xyz = u_xlat5.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat6.x = unity_MatrixV[0].z;
					    u_xlat6.y = unity_MatrixV[2].z;
					    u_xlat7.xz = (-u_xlat6.xy);
					    u_xlat28.xy = u_xlat6.xy / unity_MatrixV[1].zz;
					    u_xlat7.y = unity_MatrixV[1].z;
					    u_xlat38 = dot(u_xlat7.zxy, u_xlat5.xyz);
					    u_xlat6.xyz = (-u_xlat7.xyz) * vec3(u_xlat38) + u_xlat5.yzx;
					    u_xlat38 = dot(u_xlat6.xyz, u_xlat6.xyz);
					    u_xlat38 = sqrt(u_xlat38);
					    u_xlat41 = u_xlat38 + 1.0;
					    u_xlat6.xyz = u_xlat5.xyz * (-u_xlat7.yzx);
					    u_xlat5.xyz = u_xlat5.zxy * (-u_xlat7.zxy) + (-u_xlat6.xyz);
					    u_xlat6.x = dot(u_xlat5.xyz, u_xlat5.xyz);
					    u_xlat6.x = inversesqrt(u_xlat6.x);
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat6.xxx;
					    u_xlat6.xyz = u_xlat5.xyz / vec3(u_xlat41);
					    u_xlat8.x = unity_MatrixV[0].x;
					    u_xlat8.y = unity_MatrixV[1].x;
					    u_xlat8.z = unity_MatrixV[2].x;
					    u_xlat9.xyz = vec3(u_xlat37) * u_xlat8.xyz;
					    u_xlat41 = (-u_xlat4.x) + 1.0;
					    u_xlat6.xyz = u_xlat6.xyz * vec3(u_xlat41) + u_xlat9.xyz;
					    u_xlat42 = u_xlat36 * u_xlat4.x;
					    u_xlat6.xyz = u_xlat8.xyz * vec3(u_xlat42) + u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * in_TEXCOORD1.xxx;
					    u_xlat6.xyz = u_xlat12.xxx * u_xlat6.xyz;
					    u_xlat43 = u_xlat4.y * u_xlat4.y;
					    u_xlat43 = u_xlat4.y * u_xlat43;
					    u_xlat8.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat32.xy = vec2(u_xlat43) * u_xlat8.xy;
					    u_xlat32.xy = vec2(u_xlat36) * u_xlat32.xy;
					    u_xlat36 = u_xlat4.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat4.y * u_xlat42 + u_xlat41;
					    u_xlat8.xy = vec2(u_xlat36) * u_xlat8.xy;
					    u_xlat3.xz = u_xlat8.xy * vec2(u_xlat37) + u_xlat3.xz;
					    u_xlat8.xz = u_xlat32.xy * vec2(8.0, 8.0) + u_xlat3.xz;
					    u_xlat36 = (-u_xlat3.y) + u_xlat24.x;
					    u_xlat8.y = u_xlat4.x * u_xlat36 + u_xlat3.y;
					    u_xlat36 = (-u_xlat24.x) + u_xlat3.y;
					    u_xlat24.x = u_xlat24.x + -0.100000001;
					    u_xlat24.x = max(u_xlat24.x, _WaterLevel);
					    u_xlat36 = u_xlat36 + u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat36 = u_xlat4.x * u_xlat36;
					    u_xlat36 = (-u_xlat36) * _WindInterpolator + 1.0;
					    u_xlat9.w = u_xlat36 * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + u_xlat8.xyz;
					    u_xlat6.xyz = (-u_xlat7.yzx) * u_xlat5.zxy;
					    u_xlat5.xyz = u_xlat5.yzx * (-u_xlat7.zxy) + (-u_xlat6.xyz);
					    u_xlat36 = _WindInterpolator * 2.0 + 8.0;
					    u_xlat36 = u_xlat38 * u_xlat36 + 1.0;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.xyz;
					    u_xlat6.x = (-unity_MatrixV[2].x);
					    u_xlat6.y = 0.0;
					    u_xlat6.z = unity_MatrixV[0].x;
					    u_xlat6.xyz = vec3(u_xlat37) * u_xlat6.xyz;
					    vs_COLOR1.y = u_xlat37;
					    u_xlat5.xyz = u_xlat5.xyz * vec3(u_xlat41) + u_xlat6.xyz;
					    u_xlat6.y = u_xlat42 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat42;
					    u_xlat6.x = float(0.0);
					    u_xlat6.z = float(0.0);
					    u_xlat5.xyz = u_xlat5.xyz + u_xlat6.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * in_TEXCOORD1.yyy;
					    u_xlat5.xyz = u_xlat12.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat5.w = max(u_xlat24.x, u_xlat5.y);
					    u_xlat6 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat6 = _Tex2World[0] * u_xlat2.xxxx + u_xlat6;
					    u_xlat2 = _Tex2World[2] * u_xlat2.zzzz + u_xlat6;
					    u_xlat2 = u_xlat2 + _Tex2World[3];
					    u_xlat6 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat6 = _Tex2World[0] * u_xlat1.xxxx + u_xlat6;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat6;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat1 = (-u_xlat1) + u_xlat2;
					    u_xlat12.x = dot(u_xlat1, u_xlat1);
					    u_xlat12.x = sqrt(u_xlat12.x);
					    u_xlatb12 = 2.0<u_xlat12.x;
					    u_xlat1.xyw = (bool(u_xlatb12)) ? vec3(0.0, 0.0, 0.0) : u_xlat5.xzw;
					    u_xlat12.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat24.xy = u_xlat28.xy * u_xlat12.xx + u_xlat1.xy;
					    u_xlat0.x = u_xlat12.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat12.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat12.x;
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
					    u_xlat36 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat38 = u_xlat0.y * u_xlat36;
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat38;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat8.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat38;
					    u_xlat10 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat23.yz = u_xlat5.yx;
					    u_xlat10.xyz = u_xlat2.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat3.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat3.zzz * u_xlat10.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat10.xyz;
					    u_xlat12.x = u_xlat36 * u_xlat8.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat12.x;
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat12.x;
					    u_xlat23.x = u_xlat4.y * u_xlat8.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4.xyz = u_xlat2.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat3.xxx * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat5.w = u_xlat23.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat0.xyz) + (-u_xlat7.xyz);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat36 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat0.y * u_xlat37;
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat38;
					    u_xlat4.z = u_xlat0.z * u_xlat3.y;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat38;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat20.yz = u_xlat4.yx;
					    u_xlat7.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.yyy * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.zzz * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat7.xyz;
					    u_xlat12.x = u_xlat37 * u_xlat6.y;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat12.x;
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat12.x;
					    u_xlat20.x = u_xlat3.y * u_xlat6.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat1.yyy * u_xlat0.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat2.zzz + u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat20.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat20.zx, 0.0);
					    u_xlat4.w = u_xlat20.x;
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat3.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat14.xyz = u_xlat1.xxx * u_xlat4.xyz;
					    u_xlat14.xyz = u_xlat1.yyy * u_xlat14.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat1.xyw = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat1.xyw * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat1.y = abs(_SunDir.y);
					    u_xlat1.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat1.xyz = u_xlat1.xyz;
					    u_xlat1.xyz = clamp(u_xlat1.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat1.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat1.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.200000003;
					    u_xlat12.x = u_xlat36;
					    u_xlat12.x = clamp(u_xlat12.x, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat24.x = max(u_xlat36, u_xlat12.x);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat1.x = _Year;
					    u_xlat1.y = 0.0;
					    u_xlat1 = textureLod(_GrassTex, u_xlat1.xy, 0.0);
					    u_xlat2.xyz = u_xlat12.xxx * u_xlat1.yzx;
					    u_xlat2.xyz = u_xlat1.xyz * u_xlat24.xxx + u_xlat2.xyz;
					    u_xlat12.xyz = vec3(u_xlat36) * u_xlat1.zxy + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat12.xyz) + _SnowColor.xyz;
					    u_xlat12.xyz = vec3(_SnowAmount) * u_xlat1.xyz + u_xlat12.xyz;
					    u_xlat1.xyz = u_xlat12.xyz * _MinAmbientColor.xyz;
					    u_xlat12.xyz = (-_MinAmbientColor.xyz) * u_xlat12.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat12.xyz + u_xlat1.xyz;
					    u_xlat9.xyz = in_COLOR0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * u_xlat9;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_6[6];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_9;
						mat4x4 _Tex2World;
						vec4 unused_0_11[12];
						vec3 _SunDir;
						vec4 unused_0_13[7];
						float _Year;
						vec4 unused_0_15[7];
						vec4 _SnowColor;
						vec4 unused_0_17;
						float _SnowAmount;
						vec4 unused_0_19;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_27;
						float _AAFactor;
						vec4 unused_0_29[4];
						vec4 _MainTex_ST;
						float _Interpolator;
						vec4 unused_0_32;
						vec4 _Size;
						float _ScreenSpaceSize;
						vec4 unused_0_35;
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
					uniform  sampler2D _TopdownTex;
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
					out vec4 vs_COLOR1;
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
					vec4 u_xlat10;
					vec3 u_xlat12;
					bool u_xlatb12;
					vec3 u_xlat14;
					vec3 u_xlat20;
					vec3 u_xlat23;
					vec2 u_xlat24;
					vec2 u_xlat28;
					vec2 u_xlat32;
					float u_xlat36;
					bool u_xlatb36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat41;
					float u_xlat42;
					float u_xlat43;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat12.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat12.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat12.x = (-_Size.x) + _Size.y;
					    u_xlat12.x = in_TEXCOORD3.y * u_xlat12.x + _Size.x;
					    u_xlat24.x = u_xlat12.x * u_xlat0.x + (-u_xlat12.x);
					    u_xlat12.x = _ScreenSpaceSize * u_xlat24.x + u_xlat12.x;
					    u_xlat24.x = _Interpolator + -1.0;
					    u_xlat24.x = -abs(u_xlat24.x) + 1.0;
					    u_xlat1 = textureLod(_PosTex1, in_TEXCOORD2.xy, 0.0);
					    u_xlat2 = textureLod(_PosTex0, in_TEXCOORD2.xy, 0.0);
					    u_xlat3 = u_xlat1 + (-u_xlat2);
					    u_xlat3 = u_xlat24.xxxx * u_xlat3 + u_xlat2;
					    u_xlat24.xy = u_xlat3.xz + vec2(-0.5, -0.5);
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat24.x = min(u_xlat24.x, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat4.xy = (-u_xlat3.yw) + vec2(1.0, 1.0);
					    u_xlat24.x = dot(u_xlat24.xx, u_xlat4.xx);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat12.x = u_xlat24.x * u_xlat12.x;
					    u_xlat4.xzw = u_xlat3.yyy * _Tex2World[1].xyz;
					    u_xlat4.xzw = _Tex2World[0].xyz * u_xlat3.xxx + u_xlat4.xzw;
					    u_xlat3.xyz = _Tex2World[2].xyz * u_xlat3.zzz + u_xlat4.xzw;
					    u_xlat3.xyz = u_xlat3.xyz + _Tex2World[3].xyz;
					    u_xlat24.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat3.xz;
					    u_xlat24.xy = u_xlat24.xy / _AoTexVolume.xz;
					    u_xlat5 = textureLod(_TopdownTex, u_xlat24.xy, 0.0);
					    u_xlat24.x = u_xlat5.w * 8.0 + _WaterLevel;
					    u_xlatb36 = _WaterLevel>=u_xlat24.x;
					    u_xlat37 = u_xlatb36 ? 1.0 : float(0.0);
					    u_xlat36 = (u_xlatb36) ? 0.0 : 1.0;
					    u_xlat4.xz = u_xlat4.yy * vec2(20.0, 6.0);
					    u_xlat4.x = u_xlat4.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat37 = u_xlat37 * u_xlat4.x;
					    u_xlat38 = u_xlat4.z * u_xlat37 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat38 = u_xlat3.w * u_xlat3.w;
					    u_xlat38 = (-u_xlat38) * u_xlat38 + 1.0;
					    u_xlat38 = u_xlat36 * u_xlat38;
					    u_xlat38 = u_xlat38 * 6.0 + 1.0;
					    u_xlat12.x = u_xlat12.x * u_xlat38;
					    u_xlat5.xy = _WindDir.zx * vec2(18.0, 18.0);
					    u_xlat5.z = -1.0;
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.0, -0.0, 9.0);
					    u_xlat5.xyz = vec3(_WindInterpolator) * u_xlat5.xyz + vec3(0.0, 0.0, -9.0);
					    u_xlat5.xyz = u_xlat5.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat6.x = unity_MatrixV[0].z;
					    u_xlat6.y = unity_MatrixV[2].z;
					    u_xlat7.xz = (-u_xlat6.xy);
					    u_xlat28.xy = u_xlat6.xy / unity_MatrixV[1].zz;
					    u_xlat7.y = unity_MatrixV[1].z;
					    u_xlat38 = dot(u_xlat7.zxy, u_xlat5.xyz);
					    u_xlat6.xyz = (-u_xlat7.xyz) * vec3(u_xlat38) + u_xlat5.yzx;
					    u_xlat38 = dot(u_xlat6.xyz, u_xlat6.xyz);
					    u_xlat38 = sqrt(u_xlat38);
					    u_xlat41 = u_xlat38 + 1.0;
					    u_xlat6.xyz = u_xlat5.xyz * (-u_xlat7.yzx);
					    u_xlat5.xyz = u_xlat5.zxy * (-u_xlat7.zxy) + (-u_xlat6.xyz);
					    u_xlat6.x = dot(u_xlat5.xyz, u_xlat5.xyz);
					    u_xlat6.x = inversesqrt(u_xlat6.x);
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat6.xxx;
					    u_xlat6.xyz = u_xlat5.xyz / vec3(u_xlat41);
					    u_xlat8.x = unity_MatrixV[0].x;
					    u_xlat8.y = unity_MatrixV[1].x;
					    u_xlat8.z = unity_MatrixV[2].x;
					    u_xlat9.xyz = vec3(u_xlat37) * u_xlat8.xyz;
					    u_xlat41 = (-u_xlat4.x) + 1.0;
					    u_xlat6.xyz = u_xlat6.xyz * vec3(u_xlat41) + u_xlat9.xyz;
					    u_xlat42 = u_xlat36 * u_xlat4.x;
					    u_xlat6.xyz = u_xlat8.xyz * vec3(u_xlat42) + u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * in_TEXCOORD1.xxx;
					    u_xlat6.xyz = u_xlat12.xxx * u_xlat6.xyz;
					    u_xlat43 = u_xlat4.y * u_xlat4.y;
					    u_xlat43 = u_xlat4.y * u_xlat43;
					    u_xlat8.xy = vec2(_WindInterpolator) * _WindDir.xz;
					    u_xlat32.xy = vec2(u_xlat43) * u_xlat8.xy;
					    u_xlat32.xy = vec2(u_xlat36) * u_xlat32.xy;
					    u_xlat36 = u_xlat4.y * 0.200000003 + 0.300000012;
					    vs_COLOR1.z = u_xlat4.y * u_xlat42 + u_xlat41;
					    u_xlat8.xy = vec2(u_xlat36) * u_xlat8.xy;
					    u_xlat3.xz = u_xlat8.xy * vec2(u_xlat37) + u_xlat3.xz;
					    u_xlat8.xz = u_xlat32.xy * vec2(8.0, 8.0) + u_xlat3.xz;
					    u_xlat36 = (-u_xlat3.y) + u_xlat24.x;
					    u_xlat8.y = u_xlat4.x * u_xlat36 + u_xlat3.y;
					    u_xlat36 = (-u_xlat24.x) + u_xlat3.y;
					    u_xlat24.x = u_xlat24.x + -0.100000001;
					    u_xlat24.x = max(u_xlat24.x, _WaterLevel);
					    u_xlat36 = u_xlat36 + u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat36 = u_xlat4.x * u_xlat36;
					    u_xlat36 = (-u_xlat36) * _WindInterpolator + 1.0;
					    u_xlat9.w = u_xlat36 * in_COLOR0.w;
					    u_xlat3.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + u_xlat8.xyz;
					    u_xlat6.xyz = (-u_xlat7.yzx) * u_xlat5.zxy;
					    u_xlat5.xyz = u_xlat5.yzx * (-u_xlat7.zxy) + (-u_xlat6.xyz);
					    u_xlat36 = _WindInterpolator * 2.0 + 8.0;
					    u_xlat36 = u_xlat38 * u_xlat36 + 1.0;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.xyz;
					    u_xlat6.x = (-unity_MatrixV[2].x);
					    u_xlat6.y = 0.0;
					    u_xlat6.z = unity_MatrixV[0].x;
					    u_xlat6.xyz = vec3(u_xlat37) * u_xlat6.xyz;
					    vs_COLOR1.y = u_xlat37;
					    u_xlat5.xyz = u_xlat5.xyz * vec3(u_xlat41) + u_xlat6.xyz;
					    u_xlat6.y = u_xlat42 * _CameraUpScale;
					    vs_COLOR1.x = u_xlat3.w * u_xlat42;
					    u_xlat6.x = float(0.0);
					    u_xlat6.z = float(0.0);
					    u_xlat5.xyz = u_xlat5.xyz + u_xlat6.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * in_TEXCOORD1.yyy;
					    u_xlat5.xyz = u_xlat12.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * vec3(2.0, 2.0, 2.0) + u_xlat3.xyz;
					    u_xlat5.w = max(u_xlat24.x, u_xlat5.y);
					    u_xlat6 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat6 = _Tex2World[0] * u_xlat2.xxxx + u_xlat6;
					    u_xlat2 = _Tex2World[2] * u_xlat2.zzzz + u_xlat6;
					    u_xlat2 = u_xlat2 + _Tex2World[3];
					    u_xlat6 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat6 = _Tex2World[0] * u_xlat1.xxxx + u_xlat6;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat6;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat1 = (-u_xlat1) + u_xlat2;
					    u_xlat12.x = dot(u_xlat1, u_xlat1);
					    u_xlat12.x = sqrt(u_xlat12.x);
					    u_xlatb12 = 2.0<u_xlat12.x;
					    u_xlat1.xyw = (bool(u_xlatb12)) ? vec3(0.0, 0.0, 0.0) : u_xlat5.xzw;
					    u_xlat12.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat24.xy = u_xlat28.xy * u_xlat12.xx + u_xlat1.xy;
					    u_xlat0.x = u_xlat12.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    u_xlat0.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat12.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat12.x;
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
					    u_xlat36 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat38 = u_xlat0.y * u_xlat36;
					    u_xlat4.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat38;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat8.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat38;
					    u_xlat10 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat23.yz = u_xlat5.yx;
					    u_xlat10.xyz = u_xlat2.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat3.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat3.zzz * u_xlat10.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat10.xyz;
					    u_xlat12.x = u_xlat36 * u_xlat8.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat12.x;
					    u_xlat5.x = u_xlat4.x * u_xlat8.x + u_xlat12.x;
					    u_xlat23.x = u_xlat4.y * u_xlat8.z;
					    u_xlat0 = textureLod(_NormalTex, u_xlat5.yz, 0.0);
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.xz, 0.0);
					    u_xlat4.xyz = u_xlat2.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat3.xxx * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat5.w = u_xlat23.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat4.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_NormalTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat5.xw, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat2.xyz = (-u_xlat0.xyz) + (-u_xlat7.xyz);
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat36 = (-u_xlat2.w) * 0.5 + u_xlat1.w;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat0.y * u_xlat37;
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat38;
					    u_xlat4.z = u_xlat0.z * u_xlat3.y;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat38;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat20.yz = u_xlat4.yx;
					    u_xlat7.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.yyy * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat2.zzz * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat2.zzz + u_xlat7.xyz;
					    u_xlat12.x = u_xlat37 * u_xlat6.y;
					    u_xlat4.y = u_xlat3.x * u_xlat0.x + u_xlat12.x;
					    u_xlat4.x = u_xlat3.x * u_xlat6.x + u_xlat12.x;
					    u_xlat20.x = u_xlat3.y * u_xlat6.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat1.yyy * u_xlat0.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat2.zzz + u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat2.zzz + u_xlat3.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat20.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat20.zx, 0.0);
					    u_xlat4.w = u_xlat20.x;
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat2.yyy * u_xlat3.xyz;
					    u_xlat5.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat2.yyy * u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat3.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat14.xyz = u_xlat1.xxx * u_xlat4.xyz;
					    u_xlat14.xyz = u_xlat1.yyy * u_xlat14.xyz;
					    u_xlat3.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat1.xyw = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat1.xyw * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat1.y = abs(_SunDir.y);
					    u_xlat1.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat1.xyz = u_xlat1.xyz;
					    u_xlat1.xyz = clamp(u_xlat1.xyz, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * u_xlat1.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat0.x = u_xlat1.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.200000003;
					    u_xlat12.x = u_xlat36;
					    u_xlat12.x = clamp(u_xlat12.x, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat24.x = max(u_xlat36, u_xlat12.x);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat1.x = _Year;
					    u_xlat1.y = 0.0;
					    u_xlat1 = textureLod(_GrassTex, u_xlat1.xy, 0.0);
					    u_xlat2.xyz = u_xlat12.xxx * u_xlat1.yzx;
					    u_xlat2.xyz = u_xlat1.xyz * u_xlat24.xxx + u_xlat2.xyz;
					    u_xlat12.xyz = vec3(u_xlat36) * u_xlat1.zxy + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat12.xyz) + _SnowColor.xyz;
					    u_xlat12.xyz = vec3(_SnowAmount) * u_xlat1.xyz + u_xlat12.xyz;
					    u_xlat1.xyz = u_xlat12.xyz * _MinAmbientColor.xyz;
					    u_xlat12.xyz = (-_MinAmbientColor.xyz) * u_xlat12.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat12.xyz + u_xlat1.xyz;
					    u_xlat9.xyz = in_COLOR0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat0 = u_xlat0 * u_xlat9;
					    vs_COLOR0.w = u_xlat3.w * u_xlat0.w;
					    vs_COLOR0.xyz = u_xlat0.xyz;
					    vs_COLOR1.w = 0.0;
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
						vec4 unused_0_0[46];
						vec4 _MirrorColor;
						vec4 unused_0_2[11];
						vec4 _FogColor;
						vec4 unused_0_4[11];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					bool u_xlatb3;
					float u_xlat4;
					float u_xlat9;
					void main()
					{
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.www;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    SV_Target0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat9 / u_xlat1.x;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb3 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb3) * int(0xffffffffu)))!=0){discard;}
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
						vec4 unused_0_0[46];
						vec4 _MirrorColor;
						vec4 unused_0_2[11];
						vec4 _FogColor;
						vec4 unused_0_4[11];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					bool u_xlatb3;
					float u_xlat4;
					float u_xlat9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat9 = u_xlat0.w + -0.100000001;
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.www;
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    SV_Target0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat9 / u_xlat1.x;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb3 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb3) * int(0xffffffffu)))!=0){discard;}
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
						vec4 unused_0_0[46];
						vec4 _MirrorColor;
						vec4 unused_0_2[11];
						vec4 _FogColor;
						vec4 unused_0_4[11];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					bool u_xlatb3;
					float u_xlat4;
					float u_xlat9;
					void main()
					{
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.www;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    SV_Target0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat9 / u_xlat1.x;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb3 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb3) * int(0xffffffffu)))!=0){discard;}
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
						vec4 unused_0_0[46];
						vec4 _MirrorColor;
						vec4 unused_0_2[11];
						vec4 _FogColor;
						vec4 unused_0_4[11];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					bool u_xlatb3;
					float u_xlat4;
					float u_xlat9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat9 = u_xlat0.w + -0.100000001;
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.www;
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    SV_Target0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat9 / u_xlat1.x;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb3 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb3) * int(0xffffffffu)))!=0){discard;}
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
						vec4 unused_0_2[36];
						vec4 _MirrorColor;
						vec4 unused_0_4[3];
						vec4 _CloudCoverage;
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[9];
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
					float u_xlat4;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.www;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    u_xlat9 = u_xlat9 / u_xlat1.x;
					    u_xlat1.xy = vec2(u_xlat9) + vec2(0.5, 0.49000001);
					    u_xlatb9 = u_xlat1.y<0.0;
					    SV_Target0.w = u_xlat1.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat9 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat9));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat9 = vs_TEXCOORD1.x;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_2[36];
						vec4 _MirrorColor;
						vec4 unused_0_4[3];
						vec4 _CloudCoverage;
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[9];
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
					vec4 u_xlat2;
					float u_xlat4;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat9 = u_xlat0.w + -0.100000001;
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.www;
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    u_xlat9 = u_xlat9 / u_xlat1.x;
					    u_xlat1.xy = vec2(u_xlat9) + vec2(0.5, 0.49000001);
					    u_xlatb9 = u_xlat1.y<0.0;
					    SV_Target0.w = u_xlat1.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat9 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat9));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat9 = vs_TEXCOORD1.x;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_2[36];
						vec4 _MirrorColor;
						vec4 unused_0_4[3];
						vec4 _CloudCoverage;
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[9];
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
					float u_xlat4;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.www;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    u_xlat9 = u_xlat9 / u_xlat1.x;
					    u_xlat1.xy = vec2(u_xlat9) + vec2(0.5, 0.49000001);
					    u_xlatb9 = u_xlat1.y<0.0;
					    SV_Target0.w = u_xlat1.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat9 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat9));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat9 = vs_TEXCOORD1.x;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_2[36];
						vec4 _MirrorColor;
						vec4 unused_0_4[3];
						vec4 _CloudCoverage;
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[9];
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
					vec4 u_xlat2;
					float u_xlat4;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat9 = u_xlat0.w + -0.100000001;
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.www;
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    u_xlat9 = u_xlat9 / u_xlat1.x;
					    u_xlat1.xy = vec2(u_xlat9) + vec2(0.5, 0.49000001);
					    u_xlatb9 = u_xlat1.y<0.0;
					    SV_Target0.w = u_xlat1.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat9 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat9));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat9 = vs_TEXCOORD1.x;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_0[46];
						vec4 _MirrorColor;
						vec4 unused_0_2[11];
						vec4 _FogColor;
						vec4 unused_0_4[11];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					bool u_xlatb3;
					float u_xlat4;
					float u_xlat9;
					void main()
					{
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.www;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    SV_Target0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat9 / u_xlat1.x;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb3 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb3) * int(0xffffffffu)))!=0){discard;}
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
						vec4 unused_0_0[46];
						vec4 _MirrorColor;
						vec4 unused_0_2[11];
						vec4 _FogColor;
						vec4 unused_0_4[11];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					bool u_xlatb3;
					float u_xlat4;
					float u_xlat9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat9 = u_xlat0.w + -0.100000001;
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.www;
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    SV_Target0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat9 / u_xlat1.x;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb3 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb3) * int(0xffffffffu)))!=0){discard;}
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
						vec4 unused_0_0[46];
						vec4 _MirrorColor;
						vec4 unused_0_2[11];
						vec4 _FogColor;
						vec4 unused_0_4[11];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					bool u_xlatb3;
					float u_xlat4;
					float u_xlat9;
					void main()
					{
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.www;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    SV_Target0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat9 / u_xlat1.x;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb3 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb3) * int(0xffffffffu)))!=0){discard;}
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
						vec4 unused_0_0[46];
						vec4 _MirrorColor;
						vec4 unused_0_2[11];
						vec4 _FogColor;
						vec4 unused_0_4[11];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					bool u_xlatb3;
					float u_xlat4;
					float u_xlat9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat9 = u_xlat0.w + -0.100000001;
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.www;
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    SV_Target0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat9 / u_xlat1.x;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb3 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb3) * int(0xffffffffu)))!=0){discard;}
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
						vec4 unused_0_2[36];
						vec4 _MirrorColor;
						vec4 unused_0_4[3];
						vec4 _CloudCoverage;
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[9];
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
					float u_xlat4;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.www;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    u_xlat9 = u_xlat9 / u_xlat1.x;
					    u_xlat1.xy = vec2(u_xlat9) + vec2(0.5, 0.49000001);
					    u_xlatb9 = u_xlat1.y<0.0;
					    SV_Target0.w = u_xlat1.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat9 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat9));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat9 = vs_TEXCOORD1.x;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_2[36];
						vec4 _MirrorColor;
						vec4 unused_0_4[3];
						vec4 _CloudCoverage;
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[9];
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
					vec4 u_xlat2;
					float u_xlat4;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat9 = u_xlat0.w + -0.100000001;
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.www;
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    u_xlat9 = u_xlat9 / u_xlat1.x;
					    u_xlat1.xy = vec2(u_xlat9) + vec2(0.5, 0.49000001);
					    u_xlatb9 = u_xlat1.y<0.0;
					    SV_Target0.w = u_xlat1.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat9 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat9));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat9 = vs_TEXCOORD1.x;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_2[36];
						vec4 _MirrorColor;
						vec4 unused_0_4[3];
						vec4 _CloudCoverage;
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[9];
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
					float u_xlat4;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.www;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    u_xlat9 = u_xlat9 / u_xlat1.x;
					    u_xlat1.xy = vec2(u_xlat9) + vec2(0.5, 0.49000001);
					    u_xlatb9 = u_xlat1.y<0.0;
					    SV_Target0.w = u_xlat1.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat9 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat9));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat9 = vs_TEXCOORD1.x;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_2[36];
						vec4 _MirrorColor;
						vec4 unused_0_4[3];
						vec4 _CloudCoverage;
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10[9];
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
					vec4 u_xlat2;
					float u_xlat4;
					float u_xlat9;
					bool u_xlatb9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat9 = u_xlat0.w + -0.100000001;
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.www;
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.xyz = _MirrorColor.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat0.xyz = _FogColor.xyz * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat0.w = u_xlat1.z;
					    u_xlat0 = u_xlat0 * vs_COLOR1.zzzz;
					    u_xlat2.x = float(0.800000012);
					    u_xlat2.y = float(0.800000012);
					    u_xlat2.z = float(0.800000012);
					    u_xlat2.w = u_xlat1.y;
					    u_xlat0 = u_xlat2 * vs_COLOR1.yyyy + u_xlat0;
					    u_xlat1.w = 0.699999988;
					    u_xlat0 = u_xlat1.wwwx * vs_COLOR1.xxxx + u_xlat0;
					    u_xlat1.x = dFdx(u_xlat0.w);
					    u_xlat4 = dFdy(u_xlat0.w);
					    u_xlat1.x = abs(u_xlat4) + abs(u_xlat1.x);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat9 = u_xlat0.w + -0.5;
					    u_xlat9 = u_xlat9 / u_xlat1.x;
					    u_xlat1.xy = vec2(u_xlat9) + vec2(0.5, 0.49000001);
					    u_xlatb9 = u_xlat1.y<0.0;
					    SV_Target0.w = u_xlat1.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat9 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat9));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat9 = vs_TEXCOORD1.x;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
			}
		}
	}
}