Shader "Unlit/PathNode" {
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
			GpuProgramID 36055
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
						vec4 unused_0_8[14];
						float _NavDist;
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
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec4 vs_COLOR0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
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
					    u_xlat9 = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat0.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * vec3(0.699999988, 0.699999988, 0.699999988);
					    u_xlat0.x = (-in_TEXCOORD1.x) + _NavDist;
					    u_xlat0.x = u_xlat0.x + -0.200000003;
					    vs_COLOR0.w = u_xlat0.x + u_xlat0.x;
					    vs_COLOR0.w = clamp(vs_COLOR0.w, 0.0, 1.0);
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[37];
						float _Year;
						vec4 unused_0_4[7];
						vec4 _SnowColor;
						vec4 unused_0_6;
						float _SnowAmount;
						vec4 unused_0_8[14];
						float _NavDist;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[10];
					};
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec4 vs_COLOR0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    gl_Position = vec4(0.0, 0.0, 0.0, 0.0);
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
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.zxy + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _SnowColor.xyz;
					    u_xlat0.xyz = vec3(_SnowAmount) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat9 = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat0.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * vec3(0.699999988, 0.699999988, 0.699999988);
					    u_xlat0.x = (-in_TEXCOORD1.x) + _NavDist;
					    u_xlat0.x = u_xlat0.x + -0.200000003;
					    vs_COLOR0.w = u_xlat0.x + u_xlat0.x;
					    vs_COLOR0.w = clamp(vs_COLOR0.w, 0.0, 1.0);
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
						vec4 unused_0_16;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _LineWidth;
						vec4 unused_0_21[2];
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_25[4];
						float _NavDist;
						float _Radius;
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
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
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
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat20;
					vec3 u_xlat21;
					float u_xlat22;
					float u_xlat33;
					float u_xlat34;
					float u_xlat35;
					float u_xlat36;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat11.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat11.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth + 0.00999999978;
					    u_xlat11.x = _Radius * 2.0 + 6.0;
					    u_xlat0.x = u_xlat0.x * u_xlat11.x;
					    u_xlat0.xy = u_xlat0.xx * in_TEXCOORD0.xy;
					    u_xlat22 = (-in_TEXCOORD1.x) + _NavDist;
					    u_xlat22 = u_xlat22 + -0.200000003;
					    u_xlat22 = u_xlat22 + u_xlat22;
					    u_xlat22 = clamp(u_xlat22, 0.0, 1.0);
					    u_xlat33 = u_xlat22 * 0.5 + 0.5;
					    vs_COLOR0.w = u_xlat22;
					    u_xlat0.xy = vec2(u_xlat33) * u_xlat0.xy;
					    u_xlat22 = dot(unity_ObjectToWorld[1].xyz, unity_ObjectToWorld[1].xyz);
					    u_xlat22 = inversesqrt(u_xlat22);
					    u_xlat1.xyz = vec3(u_xlat22) * unity_ObjectToWorld[1].xyz;
					    u_xlat11.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat2.z = unity_MatrixV[2].z;
					    u_xlat3.xyz = u_xlat1.yzx * (-u_xlat2.zxy);
					    u_xlat1.xyz = (-u_xlat2.yzx) * u_xlat1.zxy + (-u_xlat3.xyz);
					    u_xlat34 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat34 = inversesqrt(u_xlat34);
					    u_xlat1.xyz = vec3(u_xlat34) * u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat11.xyz;
					    u_xlat0.w = u_xlat0.y * _CameraUpScale;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) * vec3(-0.0600000024, -0.0600000024, -0.0600000024) + u_xlat1.xyz;
					    u_xlat0.xyw = u_xlat0.xzw + u_xlat1.xzy;
					    u_xlat1 = u_xlat0.wwww * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.yyyy + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat1.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xwy + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat34 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat35 = u_xlat1.y * u_xlat34;
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat1.x + u_xlat35;
					    u_xlat6.z = u_xlat1.z * u_xlat5.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7 = u_xlat4.xxxx * u_xlat7;
					    u_xlat7 = u_xlat4.yyyy * u_xlat7;
					    u_xlat8.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat35;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat21.yz = u_xlat6.yx;
					    u_xlat9 = u_xlat3.xxxx * u_xlat9;
					    u_xlat9 = u_xlat4.yyyy * u_xlat9;
					    u_xlat9 = u_xlat4.zzzz * u_xlat9;
					    u_xlat7 = u_xlat7 * u_xlat4.zzzz + u_xlat9;
					    u_xlat12.x = u_xlat34 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat1.x + u_xlat12.x;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat12.x;
					    u_xlat21.x = u_xlat5.y * u_xlat8.z;
					    u_xlat1 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat5 = u_xlat3.xxxx * u_xlat5;
					    u_xlat5 = u_xlat3.yyyy * u_xlat5;
					    u_xlat1 = u_xlat4.xxxx * u_xlat1;
					    u_xlat1 = u_xlat3.yyyy * u_xlat1;
					    u_xlat1 = u_xlat1 * u_xlat4.zzzz + u_xlat7;
					    u_xlat1 = u_xlat5 * u_xlat4.zzzz + u_xlat1;
					    u_xlat5 = textureLod(_NormalTex, u_xlat21.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat21.zx, 0.0);
					    u_xlat6.w = u_xlat21.x;
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat7 = u_xlat4.yyyy * u_xlat7;
					    u_xlat5 = u_xlat4.xxxx * u_xlat5;
					    u_xlat5 = u_xlat4.yyyy * u_xlat5;
					    u_xlat1 = u_xlat5 * u_xlat3.zzzz + u_xlat1;
					    u_xlat1 = u_xlat7 * u_xlat3.zzzz + u_xlat1;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat6 = u_xlat3.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat4 = u_xlat4.xxxx * u_xlat5;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat1 = u_xlat4 * u_xlat3.zzzz + u_xlat1;
					    u_xlat1 = u_xlat6 * u_xlat3.zzzz + u_xlat1;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat34 = u_xlat1.w * 0.400000006;
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.yxyz + vec4(-2.0, -0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.yzw);
					    u_xlat33 = (-u_xlat2.x) * 0.5 + u_xlat0.w;
					    u_xlat33 = u_xlat33 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat35 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat36 = u_xlat0.y * u_xlat35;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat36;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat36;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat20.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat11.x = u_xlat35 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat11.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat11.x;
					    u_xlat20.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat20.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat20.zx, 0.0);
					    u_xlat5.w = u_xlat20.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat14.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat2.yyy * u_xlat14.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat13.xyz = u_xlat0.xyz * u_xlat3.xyz;
					    u_xlat0.x = u_xlat13.y + u_xlat13.x;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + u_xlat34;
					    u_xlat11.x = u_xlat33;
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat33 = (-u_xlat33);
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat22 = max(u_xlat33, u_xlat11.x);
					    u_xlat22 = (-u_xlat22) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat11.xxx * u_xlat3.yzx;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(u_xlat22) + u_xlat4.xyz;
					    u_xlat11.xyz = vec3(u_xlat33) * u_xlat3.zxy + u_xlat4.xyz;
					    u_xlat3.xyz = (-u_xlat11.xyz) + _SnowColor.xyz;
					    u_xlat11.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat11.xyz;
					    u_xlat3.xyz = u_xlat11.xyz * _MinAmbientColor.xyz;
					    u_xlat4.xyz = (-_MinAmbientColor.xyz) * u_xlat11.xyz + _MaxAmbientColor.xyz;
					    u_xlat3.xyz = u_xlat0.xxx * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat0.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat0.x = inversesqrt(u_xlat0.x);
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat1.w = (-u_xlat1.x);
					    u_xlat4.xyz = u_xlat1.xyw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat1.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat1.xy = u_xlat13.xy * u_xlat4.xy;
					    u_xlat1.x = u_xlat1.y + u_xlat1.x;
					    u_xlat1.x = u_xlat13.z * u_xlat4.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat2.x;
					    u_xlat12.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat1.xyz = u_xlat12.xyz * u_xlat1.xxx + u_xlat3.xyz;
					    u_xlat34 = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat34 + 1.0;
					    u_xlat2.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat11.xyz * u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * vec3(0.699999988, 0.699999988, 0.699999988);
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
						vec4 unused_0_16;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _LineWidth;
						vec4 unused_0_21[2];
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_25[4];
						float _NavDist;
						float _Radius;
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
						vec4 unused_3_2[10];
					};
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _NormalTex;
					uniform  sampler2D _AoTex;
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
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
					vec3 u_xlat12;
					vec3 u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat20;
					vec3 u_xlat21;
					float u_xlat22;
					float u_xlat33;
					float u_xlat34;
					float u_xlat35;
					float u_xlat36;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    gl_Position = vec4(0.0, 0.0, 0.0, 0.0);
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat11.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat11.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth + 0.00999999978;
					    u_xlat11.x = _Radius * 2.0 + 6.0;
					    u_xlat0.x = u_xlat0.x * u_xlat11.x;
					    u_xlat0.xy = u_xlat0.xx * in_TEXCOORD0.xy;
					    u_xlat22 = (-in_TEXCOORD1.x) + _NavDist;
					    u_xlat22 = u_xlat22 + -0.200000003;
					    u_xlat22 = u_xlat22 + u_xlat22;
					    u_xlat22 = clamp(u_xlat22, 0.0, 1.0);
					    u_xlat33 = u_xlat22 * 0.5 + 0.5;
					    vs_COLOR0.w = u_xlat22;
					    u_xlat0.xy = vec2(u_xlat33) * u_xlat0.xy;
					    u_xlat22 = dot(unity_ObjectToWorld[1].xyz, unity_ObjectToWorld[1].xyz);
					    u_xlat22 = inversesqrt(u_xlat22);
					    u_xlat1.xyz = vec3(u_xlat22) * unity_ObjectToWorld[1].xyz;
					    u_xlat11.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat2.x = unity_MatrixV[0].z;
					    u_xlat2.y = unity_MatrixV[1].z;
					    u_xlat2.z = unity_MatrixV[2].z;
					    u_xlat3.xyz = u_xlat1.yzx * (-u_xlat2.zxy);
					    u_xlat1.xyz = (-u_xlat2.yzx) * u_xlat1.zxy + (-u_xlat3.xyz);
					    u_xlat34 = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat34 = inversesqrt(u_xlat34);
					    u_xlat1.xyz = vec3(u_xlat34) * u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat11.xyz;
					    u_xlat0.w = u_xlat0.y * _CameraUpScale;
					    u_xlat1.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) * vec3(-0.0600000024, -0.0600000024, -0.0600000024) + u_xlat1.xyz;
					    u_xlat0.xyw = u_xlat0.xzw + u_xlat1.xzy;
					    u_xlat1.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat1.y = 1.0;
					    u_xlat0.xyz = u_xlat0.xwy + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat3.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat34 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat35 = u_xlat1.y * u_xlat34;
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.y = u_xlat5.x * u_xlat1.x + u_xlat35;
					    u_xlat6.z = u_xlat1.z * u_xlat5.y;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7 = u_xlat4.xxxx * u_xlat7;
					    u_xlat7 = u_xlat4.yyyy * u_xlat7;
					    u_xlat8.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat35;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat21.yz = u_xlat6.yx;
					    u_xlat9 = u_xlat3.xxxx * u_xlat9;
					    u_xlat9 = u_xlat4.yyyy * u_xlat9;
					    u_xlat9 = u_xlat4.zzzz * u_xlat9;
					    u_xlat7 = u_xlat7 * u_xlat4.zzzz + u_xlat9;
					    u_xlat12.x = u_xlat34 * u_xlat8.y;
					    u_xlat6.y = u_xlat5.x * u_xlat1.x + u_xlat12.x;
					    u_xlat6.x = u_xlat5.x * u_xlat8.x + u_xlat12.x;
					    u_xlat21.x = u_xlat5.y * u_xlat8.z;
					    u_xlat1 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat5 = u_xlat3.xxxx * u_xlat5;
					    u_xlat5 = u_xlat3.yyyy * u_xlat5;
					    u_xlat1 = u_xlat4.xxxx * u_xlat1;
					    u_xlat1 = u_xlat3.yyyy * u_xlat1;
					    u_xlat1 = u_xlat1 * u_xlat4.zzzz + u_xlat7;
					    u_xlat1 = u_xlat5 * u_xlat4.zzzz + u_xlat1;
					    u_xlat5 = textureLod(_NormalTex, u_xlat21.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat21.zx, 0.0);
					    u_xlat6.w = u_xlat21.x;
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat7 = u_xlat4.yyyy * u_xlat7;
					    u_xlat5 = u_xlat4.xxxx * u_xlat5;
					    u_xlat5 = u_xlat4.yyyy * u_xlat5;
					    u_xlat1 = u_xlat5 * u_xlat3.zzzz + u_xlat1;
					    u_xlat1 = u_xlat7 * u_xlat3.zzzz + u_xlat1;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat6 = u_xlat3.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat4 = u_xlat4.xxxx * u_xlat5;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat1 = u_xlat4 * u_xlat3.zzzz + u_xlat1;
					    u_xlat1 = u_xlat6 * u_xlat3.zzzz + u_xlat1;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat34 = u_xlat1.w * 0.400000006;
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat0.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.yxyz + vec4(-2.0, -0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.yzw);
					    u_xlat33 = (-u_xlat2.x) * 0.5 + u_xlat0.w;
					    u_xlat33 = u_xlat33 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat0.xyz);
					    u_xlat0.xyz = floor(u_xlat0.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat35 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat36 = u_xlat0.y * u_xlat35;
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat36;
					    u_xlat5.z = u_xlat0.z * u_xlat4.y;
					    u_xlat6 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat6.xyz = u_xlat3.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat0.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat36;
					    u_xlat8 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat20.yz = u_xlat5.yx;
					    u_xlat8.xyz = u_xlat2.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.yyy * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat3.zzz * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat3.zzz + u_xlat8.xyz;
					    u_xlat11.x = u_xlat35 * u_xlat7.y;
					    u_xlat5.y = u_xlat4.x * u_xlat0.x + u_xlat11.x;
					    u_xlat5.x = u_xlat4.x * u_xlat7.x + u_xlat11.x;
					    u_xlat20.x = u_xlat4.y * u_xlat7.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat5.xz, 0.0);
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat7.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat0.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat3.zzz + u_xlat6.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat20.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat20.zx, 0.0);
					    u_xlat5.w = u_xlat20.x;
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat3.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat14.xyz = u_xlat3.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat6.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat5.xw, 0.0);
					    u_xlat14.xyz = u_xlat2.xxx * u_xlat5.xyz;
					    u_xlat14.xyz = u_xlat2.yyy * u_xlat14.xyz;
					    u_xlat4.xyz = u_xlat3.xxx * u_xlat4.xyz;
					    u_xlat2.xyw = u_xlat2.yyy * u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat2.xyw * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat14.xyz * u_xlat2.zzz + u_xlat0.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat13.xyz = u_xlat0.xyz * u_xlat3.xyz;
					    u_xlat0.x = u_xlat13.y + u_xlat13.x;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + u_xlat34;
					    u_xlat11.x = u_xlat33;
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat33 = (-u_xlat33);
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat22 = max(u_xlat33, u_xlat11.x);
					    u_xlat22 = (-u_xlat22) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = u_xlat11.xxx * u_xlat3.yzx;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(u_xlat22) + u_xlat4.xyz;
					    u_xlat11.xyz = vec3(u_xlat33) * u_xlat3.zxy + u_xlat4.xyz;
					    u_xlat3.xyz = (-u_xlat11.xyz) + _SnowColor.xyz;
					    u_xlat11.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat11.xyz;
					    u_xlat3.xyz = u_xlat11.xyz * _MinAmbientColor.xyz;
					    u_xlat4.xyz = (-_MinAmbientColor.xyz) * u_xlat11.xyz + _MaxAmbientColor.xyz;
					    u_xlat3.xyz = u_xlat0.xxx * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat0.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat0.x = inversesqrt(u_xlat0.x);
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat1.w = (-u_xlat1.x);
					    u_xlat4.xyz = u_xlat1.xyw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat0.x = dot(u_xlat1.xyz, _FlashDir.xyz);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat1.xy = u_xlat13.xy * u_xlat4.xy;
					    u_xlat1.x = u_xlat1.y + u_xlat1.x;
					    u_xlat1.x = u_xlat13.z * u_xlat4.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.x / u_xlat2.x;
					    u_xlat12.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat1.xyz = u_xlat12.xyz * u_xlat1.xxx + u_xlat3.xyz;
					    u_xlat34 = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat34 + 1.0;
					    u_xlat2.xyz = u_xlat0.xxx * _FlashColor.xyz;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat11.xyz * u_xlat1.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * vec3(0.699999988, 0.699999988, 0.699999988);
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
						vec4 unused_0_2;
					};
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					float u_xlat3;
					vec2 u_xlat6;
					void main()
					{
					    u_xlat0.x = dFdx(vs_TEXCOORD0.y);
					    u_xlat3 = dFdy(vs_TEXCOORD0.y);
					    u_xlat0.x = abs(u_xlat3) + abs(u_xlat0.x);
					    u_xlat0.y = u_xlat0.x * 2.0 + vs_TEXCOORD0.y;
					    u_xlat0.x = vs_TEXCOORD0.x;
					    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
					    u_xlat0.y = sqrt(u_xlat0.x);
					    u_xlat6.x = dot(vs_TEXCOORD0.xy, vs_TEXCOORD0.xy);
					    u_xlat0.x = sqrt(u_xlat6.x);
					    u_xlat0.xy = (-u_xlat0.xy) + vec2(0.400000006, 0.400000006);
					    u_xlat6.xy = dFdx(u_xlat0.xy);
					    u_xlat1.xy = dFdy(u_xlat0.xy);
					    u_xlat6.xy = abs(u_xlat6.xy) + abs(u_xlat1.xy);
					    u_xlat0.xy = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xy = u_xlat0.xy + vec2(-1.0, -1.0);
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat2 = u_xlat0.yyyy * u_xlat1;
					    u_xlat1 = (-u_xlat0.yyyy) * u_xlat1 + _Color;
					    u_xlat0 = u_xlat0.xxxx * u_xlat1 + u_xlat2;
					    u_xlat1.x = u_xlat0.w * vs_COLOR0.w + -0.00999999978;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = max(u_xlat0.w, 9.99999975e-06);
					    SV_Target0.xyz = u_xlat0.xyz / u_xlat1.xxx;
					    u_xlat0.x = u_xlat0.w * vs_COLOR0.w;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
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
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					float u_xlat3;
					vec2 u_xlat6;
					void main()
					{
					    u_xlat0.x = dFdx(vs_TEXCOORD0.y);
					    u_xlat3 = dFdy(vs_TEXCOORD0.y);
					    u_xlat0.x = abs(u_xlat3) + abs(u_xlat0.x);
					    u_xlat0.y = u_xlat0.x * 2.0 + vs_TEXCOORD0.y;
					    u_xlat0.x = vs_TEXCOORD0.x;
					    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
					    u_xlat0.y = sqrt(u_xlat0.x);
					    u_xlat6.x = dot(vs_TEXCOORD0.xy, vs_TEXCOORD0.xy);
					    u_xlat0.x = sqrt(u_xlat6.x);
					    u_xlat0.xy = (-u_xlat0.xy) + vec2(0.400000006, 0.400000006);
					    u_xlat6.xy = dFdx(u_xlat0.xy);
					    u_xlat1.xy = dFdy(u_xlat0.xy);
					    u_xlat6.xy = abs(u_xlat6.xy) + abs(u_xlat1.xy);
					    u_xlat0.xy = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xy = u_xlat0.xy + vec2(-1.0, -1.0);
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat2 = u_xlat0.yyyy * u_xlat1;
					    u_xlat1 = (-u_xlat0.yyyy) * u_xlat1 + _Color;
					    u_xlat0 = u_xlat0.xxxx * u_xlat1 + u_xlat2;
					    u_xlat1.x = u_xlat0.w * vs_COLOR0.w + -0.00999999978;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = max(u_xlat0.w, 9.99999975e-06);
					    SV_Target0.xyz = u_xlat0.xyz / u_xlat1.xxx;
					    u_xlat0.x = u_xlat0.w * vs_COLOR0.w;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
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
						vec4 unused_0_0[65];
						vec4 _Color;
						vec4 unused_0_2;
					};
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					float u_xlat3;
					vec2 u_xlat6;
					void main()
					{
					    u_xlat0.x = dFdx(vs_TEXCOORD0.y);
					    u_xlat3 = dFdy(vs_TEXCOORD0.y);
					    u_xlat0.x = abs(u_xlat3) + abs(u_xlat0.x);
					    u_xlat0.y = u_xlat0.x * 2.0 + vs_TEXCOORD0.y;
					    u_xlat0.x = vs_TEXCOORD0.x;
					    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
					    u_xlat0.y = sqrt(u_xlat0.x);
					    u_xlat6.x = dot(vs_TEXCOORD0.xy, vs_TEXCOORD0.xy);
					    u_xlat0.x = sqrt(u_xlat6.x);
					    u_xlat0.xy = (-u_xlat0.xy) + vec2(0.400000006, 0.400000006);
					    u_xlat6.xy = dFdx(u_xlat0.xy);
					    u_xlat1.xy = dFdy(u_xlat0.xy);
					    u_xlat6.xy = abs(u_xlat6.xy) + abs(u_xlat1.xy);
					    u_xlat0.xy = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xy = u_xlat0.xy + vec2(-1.0, -1.0);
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat2 = u_xlat0.yyyy * u_xlat1;
					    u_xlat1 = (-u_xlat0.yyyy) * u_xlat1 + _Color;
					    u_xlat0 = u_xlat0.xxxx * u_xlat1 + u_xlat2;
					    u_xlat1.x = u_xlat0.w * vs_COLOR0.w + -0.00999999978;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = max(u_xlat0.w, 9.99999975e-06);
					    SV_Target0.xyz = u_xlat0.xyz / u_xlat1.xxx;
					    u_xlat0.x = u_xlat0.w * vs_COLOR0.w;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
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
						vec4 unused_0_0[65];
						vec4 _Color;
						vec4 unused_0_2;
					};
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					float u_xlat3;
					vec2 u_xlat6;
					void main()
					{
					    u_xlat0.x = dFdx(vs_TEXCOORD0.y);
					    u_xlat3 = dFdy(vs_TEXCOORD0.y);
					    u_xlat0.x = abs(u_xlat3) + abs(u_xlat0.x);
					    u_xlat0.y = u_xlat0.x * 2.0 + vs_TEXCOORD0.y;
					    u_xlat0.x = vs_TEXCOORD0.x;
					    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
					    u_xlat0.y = sqrt(u_xlat0.x);
					    u_xlat6.x = dot(vs_TEXCOORD0.xy, vs_TEXCOORD0.xy);
					    u_xlat0.x = sqrt(u_xlat6.x);
					    u_xlat0.xy = (-u_xlat0.xy) + vec2(0.400000006, 0.400000006);
					    u_xlat6.xy = dFdx(u_xlat0.xy);
					    u_xlat1.xy = dFdy(u_xlat0.xy);
					    u_xlat6.xy = abs(u_xlat6.xy) + abs(u_xlat1.xy);
					    u_xlat0.xy = u_xlat0.xy / u_xlat6.xy;
					    u_xlat0.xy = u_xlat0.xy + vec2(-1.0, -1.0);
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat1.xyz = vs_COLOR0.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat2 = u_xlat0.yyyy * u_xlat1;
					    u_xlat1 = (-u_xlat0.yyyy) * u_xlat1 + _Color;
					    u_xlat0 = u_xlat0.xxxx * u_xlat1 + u_xlat2;
					    u_xlat1.x = u_xlat0.w * vs_COLOR0.w + -0.00999999978;
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = max(u_xlat0.w, 9.99999975e-06);
					    SV_Target0.xyz = u_xlat0.xyz / u_xlat1.xxx;
					    u_xlat0.x = u_xlat0.w * vs_COLOR0.w;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    return;
					}"
				}
			}
		}
	}
}