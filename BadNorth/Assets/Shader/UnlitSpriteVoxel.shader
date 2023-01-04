Shader "Unlit/SpriteVoxel" {
	Properties {
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		[Toggle] _Foam ("Foam", Float) = 0
	}
	SubShader {
		LOD 100
		Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "RenderType" = "Opaque" }
		Pass {
			LOD 100
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "RenderType" = "Opaque" }
			Cull Off
			GpuProgramID 46755
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
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    vs_COLOR0 = u_xlat0.xxxx * in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_FOAM_ON" }
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
						vec4 unused_0_0[44];
						vec4 _FoamColor;
						vec4 unused_0_2[19];
						vec4 _MainTex_ST;
						vec4 unused_0_4;
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
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = vec2(0.0, 2.0);
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0 = _FoamColor * vec4(1.10000002, 1.10000002, 1.10000002, 1.10000002);
					    vs_COLOR0 = u_xlat0 * in_COLOR0;
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
						vec4 unused_0_29;
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
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
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
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat10.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat10.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat10.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat10.xyz;
					    u_xlat10.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat10.xyz;
					    u_xlat1.xyw = unity_ObjectToWorld[3].xzy * in_POSITION0.www + u_xlat10.xzy;
					    u_xlat10.x = u_xlat1.w + (-_WaterLevel);
					    u_xlat0.x = u_xlat10.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat10.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat0.x / u_xlat10.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
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
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat10.xyz;
					    u_xlat0.w = 1.0;
					    vs_COLOR0 = u_xlat0 * in_COLOR0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_FOAM_ON" }
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
						vec4 unused_0_5[23];
						vec3 _SunDir;
						vec4 unused_0_7[4];
						vec4 _SideSunColor;
						vec4 unused_0_9[2];
						float _Year;
						vec4 unused_0_11[2];
						vec4 _FoamColor;
						vec4 unused_0_13[4];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_17[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_28[2];
						vec4 _MainTex_ST;
						vec4 unused_0_30;
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
						vec4 unused_3_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_3_2[2];
					};
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec3 u_xlat7;
					vec3 u_xlat8;
					float u_xlat9;
					vec3 u_xlat10;
					vec2 u_xlat16;
					float u_xlat24;
					float u_xlat25;
					float u_xlat26;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat0.x = u_xlat0.x + u_xlat0.x;
					    u_xlat8.x = _AAFactor + 1.0;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth;
					    u_xlat8.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat8.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat8.xyz;
					    u_xlat8.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat8.xyz;
					    u_xlat8.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat8.xyz;
					    u_xlat1.x = u_xlat8.y + (-_WaterLevel);
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD1.y = u_xlat0.x + 0.5;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.x = dot(u_xlat8.xz, u_xlat8.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    u_xlat0.x = u_xlat0.x / u_xlat1.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    vs_TEXCOORD1.x = u_xlat0.x;
					    u_xlat1 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat1 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat1;
					    u_xlat1 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat1;
					    u_xlat1 = u_xlat1 + unity_ObjectToWorld[3];
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = unity_MatrixVP[3] * u_xlat1.wwww + u_xlat2;
					    u_xlat8.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat8.xz;
					    u_xlat1.xyz = u_xlat8.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat8.x = (-u_xlat2.w) * 0.5 + u_xlat8.y;
					    u_xlat8.x = u_xlat8.x * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat1.xyz);
					    u_xlat1.xyz = fract(u_xlat1.xyz);
					    u_xlat16.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat3.z = u_xlat2.z * u_xlat16.y;
					    u_xlat25 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat26 = u_xlat2.y * u_xlat25;
					    u_xlat4.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat3.x = u_xlat16.x * u_xlat4.x + u_xlat26;
					    u_xlat3.y = u_xlat16.x * u_xlat2.x + u_xlat26;
					    u_xlat5 = textureLod(_AoTex, u_xlat3.xz, 0.0);
					    u_xlat10.yz = u_xlat3.yx;
					    u_xlat6 = textureLod(_AoTex, u_xlat3.yz, 0.0);
					    u_xlat5.xyz = u_xlat1.xxx * u_xlat5.xyz;
					    u_xlat7.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat7.yyy;
					    u_xlat5.xyz = u_xlat7.zzz * u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat7.xxx;
					    u_xlat6.xyz = u_xlat7.yyy * u_xlat6.xyz;
					    u_xlat5.xyz = u_xlat6.xyz * u_xlat7.zzz + u_xlat5.xyz;
					    u_xlat25 = u_xlat25 * u_xlat4.y;
					    u_xlat3.y = u_xlat16.x * u_xlat2.x + u_xlat25;
					    u_xlat3.x = u_xlat16.x * u_xlat4.x + u_xlat25;
					    u_xlat10.x = u_xlat16.y * u_xlat4.z;
					    u_xlat4 = textureLod(_AoTex, u_xlat3.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat3.xz, 0.0);
					    u_xlat6.xyz = u_xlat1.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat1.yyy * u_xlat6.xyz;
					    u_xlat4.xyz = u_xlat7.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat1.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat7.zzz + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat6.xyz * u_xlat7.zzz + u_xlat4.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat10.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat10.zx, 0.0);
					    u_xlat3.w = u_xlat10.x;
					    u_xlat2.xyz = u_xlat1.xxx * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat7.yyy * u_xlat2.xyz;
					    u_xlat5.xyz = u_xlat7.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat7.yyy * u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat5.xyz * u_xlat1.zzz + u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat1.zzz + u_xlat4.xyz;
					    u_xlat4 = textureLod(_AoTex, u_xlat3.yw, 0.0);
					    u_xlat3 = textureLod(_AoTex, u_xlat3.xw, 0.0);
					    u_xlat3.xyz = u_xlat1.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat1.yyy * u_xlat3.xyz;
					    u_xlat4.xyz = u_xlat7.xxx * u_xlat4.xyz;
					    u_xlat1.xyw = u_xlat1.yyy * u_xlat4.xyz;
					    u_xlat1.xyw = u_xlat1.xyw * u_xlat1.zzz + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat3.xyz * u_xlat1.zzz + u_xlat1.xyw;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat16.x = u_xlat2.x + u_xlat2.y;
					    u_xlat1.xy = u_xlat1.xy * u_xlat3.xy;
					    u_xlat24 = u_xlat1.y + u_xlat1.x;
					    u_xlat16.x = u_xlat1.y / u_xlat16.x;
					    u_xlat24 = u_xlat3.z * u_xlat1.z + u_xlat24;
					    u_xlat24 = u_xlat24 * 0.600000024 + 0.400000006;
					    u_xlat1.x = u_xlat8.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat8.x = (-u_xlat8.x);
					    u_xlat8.x = clamp(u_xlat8.x, 0.0, 1.0);
					    u_xlat9 = max(u_xlat8.x, u_xlat1.x);
					    u_xlat9 = (-u_xlat9) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat1.xzw = u_xlat1.xxx * u_xlat2.yzx;
					    u_xlat1.xyz = u_xlat2.xyz * vec3(u_xlat9) + u_xlat1.xzw;
					    u_xlat1.xyz = u_xlat8.xxx * u_xlat2.zxy + u_xlat1.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = vec3(u_xlat24) * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat8.xyz = u_xlat2.xyz * u_xlat16.xxx + u_xlat1.xyz;
					    u_xlat1.x = _FlashDir.y;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat9 = u_xlat1.x * u_xlat1.x;
					    u_xlat1.x = (-u_xlat1.x) * u_xlat9 + 1.0;
					    u_xlat1.xyz = u_xlat1.xxx * _FlashColor.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat8.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat2 = u_xlat1 * _FoamColor;
					    u_xlat8.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    vs_COLOR0.w = u_xlat2.w * in_COLOR0.w;
					    u_xlat1.xyz = _FoamColor.xyz * u_xlat1.xyz + (-u_xlat8.xxx);
					    u_xlat8.xyz = _CloudCoverage.yyy * u_xlat1.xyz + u_xlat8.xxx;
					    u_xlat1.xyz = (-u_xlat8.xyz) + _LutLerp.www;
					    u_xlat8.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat8.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat8.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat8.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * in_COLOR0.xyz;
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
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					float u_xlat1;
					bool u_xlatb2;
					float u_xlat3;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat0.w * vs_COLOR0.w;
					    u_xlat3 = dFdx(u_xlat1);
					    u_xlat1 = dFdy(u_xlat1);
					    u_xlat1 = abs(u_xlat1) + abs(u_xlat3);
					    u_xlat1 = max(u_xlat1, 0.00100000005);
					    u_xlat1 = min(u_xlat1, 1.0);
					    u_xlat6 = vs_COLOR0.w * u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * _Color.xyz;
					    SV_Target0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat6 / u_xlat1;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb2 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb2) * int(0xffffffffu)))!=0){discard;}
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_FOAM_ON" }
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
					float u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w * vs_COLOR0.w;
					    u_xlat1 = vs_COLOR0.w * u_xlat0.w + -0.5;
					    u_xlat2 = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat2);
					    u_xlat0.x = max(u_xlat0.x, 0.00100000005);
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat0.x = u_xlat1 / u_xlat0.x;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb1 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
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
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat3;
					float u_xlat6;
					bool u_xlatb6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = u_xlat0.w * vs_COLOR0.w;
					    u_xlat3 = dFdx(u_xlat1.x);
					    u_xlat1.x = dFdy(u_xlat1.x);
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat3);
					    u_xlat1.x = max(u_xlat1.x, 0.00100000005);
					    u_xlat1.x = min(u_xlat1.x, 1.0);
					    u_xlat6 = vs_COLOR0.w * u_xlat0.w + -0.5;
					    u_xlat0.xyz = u_xlat0.xyz * _Color.xyz;
					    u_xlat6 = u_xlat6 / u_xlat1.x;
					    u_xlat1.xy = vec2(u_xlat6) + vec2(0.5, 0.49000001);
					    u_xlatb6 = u_xlat1.y<0.0;
					    SV_Target0.w = u_xlat1.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb6) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat6 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat6));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD1.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_FOAM_ON" }
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
					float u_xlat1;
					bool u_xlatb1;
					float u_xlat2;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w * vs_COLOR0.w;
					    u_xlat1 = vs_COLOR0.w * u_xlat0.w + -0.5;
					    u_xlat2 = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat2);
					    u_xlat0.x = max(u_xlat0.x, 0.00100000005);
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat0.x = u_xlat1 / u_xlat0.x;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb1 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
			}
		}
	}
}