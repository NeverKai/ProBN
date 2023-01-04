Shader "Unlit/Wake" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_FoamTex ("Foam Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		_Emissive ("Emissive", Range(0, 1)) = 0
		[Toggle] _Paint ("Paint", Float) = 0
	}
	SubShader {
		LOD 100
		Tags { "RenderType" = "Opaque" }
		Pass {
			LOD 100
			Tags { "RenderType" = "Opaque" }
			Stencil {
				Ref 16
				Comp LEqual
				Pass Keep
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 58043
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[5];
						vec3 _WindDir;
						float _SqrtWindTime;
						vec4 unused_0_8[16];
						vec3 _SunDir;
						vec4 unused_0_10[4];
						vec4 _SideSunColor;
						vec4 unused_0_12[2];
						float _Year;
						vec4 unused_0_14[2];
						vec4 _FoamColor;
						vec4 unused_0_16[4];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_20[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_27;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_30[3];
						vec4 _FoamTex_ST;
						vec4 unused_0_32[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat7;
					vec3 u_xlat8;
					vec3 u_xlat9;
					vec3 u_xlat10;
					vec3 u_xlat15;
					float u_xlat18;
					bool u_xlatb18;
					float u_xlat19;
					float u_xlat20;
					float u_xlat27;
					bool u_xlatb27;
					float u_xlat28;
					float u_xlat29;
					void main()
					{
					    u_xlat0.x = _SqrtWindTime * 0.0500000007;
					    u_xlat9.x = (-in_TEXCOORD0.y) + _Time.y;
					    u_xlatb18 = 40.0>=u_xlat9.x;
					    u_xlatb27 = u_xlat9.x>=0.0;
					    u_xlatb18 = u_xlatb27 && u_xlatb18;
					    u_xlat27 = u_xlat9.x * 0.0250000004;
					    vs_TEXCOORD0.x = (-u_xlat9.x) * 0.0250000004 + 1.0;
					    u_xlat27 = u_xlat27;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat9.x = u_xlat27 * 0.200000003 + in_TEXCOORD1.x;
					    u_xlat9.xz = in_TEXCOORD3.xy * u_xlat9.xx + in_TEXCOORD2.xy;
					    u_xlat9.xy = (bool(u_xlatb18)) ? u_xlat9.xz : in_TEXCOORD2.xy;
					    u_xlat1.xy = u_xlat9.xy * _FoamTex_ST.xy + _FoamTex_ST.zw;
					    vs_TEXCOORD2.xy = (-u_xlat0.xx) * _WindDir.xz + u_xlat1.xy;
					    vs_TEXCOORD0.y = in_TEXCOORD0.x;
					    u_xlat1 = vec4(_WaterLevel) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat9.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat9.yyyy + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat9.xy;
					    u_xlat0.x = dot(u_xlat9.xy, u_xlat9.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.y = _WaterLevel;
					    u_xlat9.xyz = u_xlat1.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat9.xyz = max(u_xlat9.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat1 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat9.xyz = min(u_xlat9.xyz, u_xlat1.xyz);
					    u_xlat1.x = (-u_xlat1.w) * 0.5 + _WaterLevel;
					    u_xlat1.x = u_xlat1.x * 0.25;
					    u_xlat9.xyz = u_xlat9.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat10.xyz = floor(u_xlat9.xyz);
					    u_xlat9.xyz = fract(u_xlat9.xyz);
					    u_xlat2.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat3.z = u_xlat10.z * u_xlat2.y;
					    u_xlat20 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat29 = u_xlat10.y * u_xlat20;
					    u_xlat4.xyz = u_xlat10.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat3.x = u_xlat2.x * u_xlat4.x + u_xlat29;
					    u_xlat3.y = u_xlat2.x * u_xlat10.x + u_xlat29;
					    u_xlat5 = textureLod(_AoTex, u_xlat3.xz, 0.0);
					    u_xlat15.yz = u_xlat3.yx;
					    u_xlat7 = textureLod(_AoTex, u_xlat3.yz, 0.0);
					    u_xlat5.xyz = u_xlat9.xxx * u_xlat5.xyz;
					    u_xlat8.xyz = (-u_xlat9.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat8.yyy;
					    u_xlat5.xyz = u_xlat8.zzz * u_xlat5.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat8.xxx;
					    u_xlat7.xyz = u_xlat8.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat5.xyz;
					    u_xlat19 = u_xlat20 * u_xlat4.y;
					    u_xlat3.y = u_xlat2.x * u_xlat10.x + u_xlat19;
					    u_xlat3.x = u_xlat2.x * u_xlat4.x + u_xlat19;
					    u_xlat15.x = u_xlat2.y * u_xlat4.z;
					    u_xlat2 = textureLod(_AoTex, u_xlat3.yz, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat3.xz, 0.0);
					    u_xlat10.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat10.xyz = u_xlat9.yyy * u_xlat10.xyz;
					    u_xlat2.xyz = u_xlat8.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat9.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat8.zzz + u_xlat5.xyz;
					    u_xlat10.xyz = u_xlat10.xyz * u_xlat8.zzz + u_xlat2.xyz;
					    u_xlat2 = textureLod(_AoTex, u_xlat15.yx, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat15.zx, 0.0);
					    u_xlat3.w = u_xlat15.x;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat8.yyy * u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat8.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat8.yyy * u_xlat2.xyz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat2 = textureLod(_AoTex, u_xlat3.yw, 0.0);
					    u_xlat3 = textureLod(_AoTex, u_xlat3.xw, 0.0);
					    u_xlat3.xyz = u_xlat9.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat9.yyy * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat8.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat9.yyy * u_xlat2.xyz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat3.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat10.xyz = u_xlat2.zxw;
					    u_xlat10.xyz = clamp(u_xlat10.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat9.xy = u_xlat9.xy * u_xlat10.xy;
					    u_xlat9.x = u_xlat9.y + u_xlat9.x;
					    u_xlat18 = u_xlat9.y / u_xlat2.x;
					    u_xlat9.x = u_xlat10.z * u_xlat9.z + u_xlat9.x;
					    u_xlat9.x = u_xlat9.x * 0.600000024 + 0.400000006;
					    u_xlat27 = u_xlat1.x;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x);
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat10.x = max(u_xlat27, u_xlat1.x);
					    u_xlat10.x = (-u_xlat10.x) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = vec3(u_xlat27) * u_xlat2.yzx;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat10.xxx + u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat1.xxx * u_xlat2.zxy + u_xlat10.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = u_xlat9.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat9.xyz = u_xlat2.xyz * vec3(u_xlat18) + u_xlat1.xyz;
					    u_xlat1.x = _FlashDir.y;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat10.x = u_xlat1.x * u_xlat1.x;
					    u_xlat1.x = (-u_xlat1.x) * u_xlat10.x + 1.0;
					    u_xlat1.xyz = u_xlat1.xxx * _FlashColor.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat9.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat2 = u_xlat1 * _FoamColor;
					    u_xlat9.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    vs_COLOR0.w = u_xlat2.w * in_COLOR0.w;
					    u_xlat1.xyz = _FoamColor.xyz * u_xlat1.xyz + (-u_xlat9.xxx);
					    u_xlat9.xyz = _CloudCoverage.yyy * u_xlat1.xyz + u_xlat9.xxx;
					    u_xlat1.xyz = (-u_xlat9.xyz) + _LutLerp.www;
					    u_xlat9.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat9.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat9.xyz) + u_xlat1.xyz;
					    u_xlat28 = _FogMaxRad + (-_FogMinRad);
					    u_xlat0.x = u_xlat0.x / u_xlat28;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat9.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * in_COLOR0.xyz;
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[5];
						vec3 _WindDir;
						float _SqrtWindTime;
						vec4 unused_0_8[16];
						vec3 _SunDir;
						vec4 unused_0_10[4];
						vec4 _SideSunColor;
						vec4 unused_0_12[2];
						float _Year;
						vec4 unused_0_14[2];
						vec4 _FoamColor;
						vec4 unused_0_16[4];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_20[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_27;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_30[3];
						vec4 _FoamTex_ST;
						vec4 unused_0_32[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat7;
					vec3 u_xlat8;
					vec3 u_xlat9;
					vec3 u_xlat10;
					vec3 u_xlat15;
					float u_xlat18;
					bool u_xlatb18;
					float u_xlat19;
					float u_xlat20;
					float u_xlat27;
					bool u_xlatb27;
					float u_xlat28;
					float u_xlat29;
					void main()
					{
					    u_xlat0.x = _SqrtWindTime * 0.0500000007;
					    u_xlat9.x = (-in_TEXCOORD0.y) + _Time.y;
					    u_xlatb18 = 40.0>=u_xlat9.x;
					    u_xlatb27 = u_xlat9.x>=0.0;
					    u_xlatb18 = u_xlatb27 && u_xlatb18;
					    u_xlat27 = u_xlat9.x * 0.0250000004;
					    vs_TEXCOORD0.x = (-u_xlat9.x) * 0.0250000004 + 1.0;
					    u_xlat27 = u_xlat27;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat9.x = u_xlat27 * 0.200000003 + in_TEXCOORD1.x;
					    u_xlat9.xz = in_TEXCOORD3.xy * u_xlat9.xx + in_TEXCOORD2.xy;
					    u_xlat9.xy = (bool(u_xlatb18)) ? u_xlat9.xz : in_TEXCOORD2.xy;
					    u_xlat1.xy = u_xlat9.xy * _FoamTex_ST.xy + _FoamTex_ST.zw;
					    vs_TEXCOORD2.xy = (-u_xlat0.xx) * _WindDir.xz + u_xlat1.xy;
					    vs_TEXCOORD0.y = in_TEXCOORD0.x;
					    u_xlat1 = vec4(_WaterLevel) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat9.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat9.yyyy + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat9.xy;
					    u_xlat0.x = dot(u_xlat9.xy, u_xlat9.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.y = _WaterLevel;
					    u_xlat9.xyz = u_xlat1.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat9.xyz = max(u_xlat9.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat1 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat9.xyz = min(u_xlat9.xyz, u_xlat1.xyz);
					    u_xlat1.x = (-u_xlat1.w) * 0.5 + _WaterLevel;
					    u_xlat1.x = u_xlat1.x * 0.25;
					    u_xlat9.xyz = u_xlat9.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat10.xyz = floor(u_xlat9.xyz);
					    u_xlat9.xyz = fract(u_xlat9.xyz);
					    u_xlat2.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat3.z = u_xlat10.z * u_xlat2.y;
					    u_xlat20 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat29 = u_xlat10.y * u_xlat20;
					    u_xlat4.xyz = u_xlat10.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat3.x = u_xlat2.x * u_xlat4.x + u_xlat29;
					    u_xlat3.y = u_xlat2.x * u_xlat10.x + u_xlat29;
					    u_xlat5 = textureLod(_AoTex, u_xlat3.xz, 0.0);
					    u_xlat15.yz = u_xlat3.yx;
					    u_xlat7 = textureLod(_AoTex, u_xlat3.yz, 0.0);
					    u_xlat5.xyz = u_xlat9.xxx * u_xlat5.xyz;
					    u_xlat8.xyz = (-u_xlat9.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat8.yyy;
					    u_xlat5.xyz = u_xlat8.zzz * u_xlat5.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat8.xxx;
					    u_xlat7.xyz = u_xlat8.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat5.xyz;
					    u_xlat19 = u_xlat20 * u_xlat4.y;
					    u_xlat3.y = u_xlat2.x * u_xlat10.x + u_xlat19;
					    u_xlat3.x = u_xlat2.x * u_xlat4.x + u_xlat19;
					    u_xlat15.x = u_xlat2.y * u_xlat4.z;
					    u_xlat2 = textureLod(_AoTex, u_xlat3.yz, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat3.xz, 0.0);
					    u_xlat10.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat10.xyz = u_xlat9.yyy * u_xlat10.xyz;
					    u_xlat2.xyz = u_xlat8.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat9.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat8.zzz + u_xlat5.xyz;
					    u_xlat10.xyz = u_xlat10.xyz * u_xlat8.zzz + u_xlat2.xyz;
					    u_xlat2 = textureLod(_AoTex, u_xlat15.yx, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat15.zx, 0.0);
					    u_xlat3.w = u_xlat15.x;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat8.yyy * u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat8.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat8.yyy * u_xlat2.xyz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat2 = textureLod(_AoTex, u_xlat3.yw, 0.0);
					    u_xlat3 = textureLod(_AoTex, u_xlat3.xw, 0.0);
					    u_xlat3.xyz = u_xlat9.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat9.yyy * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat8.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat9.yyy * u_xlat2.xyz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat3.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat10.xyz = u_xlat2.zxw;
					    u_xlat10.xyz = clamp(u_xlat10.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat9.xy = u_xlat9.xy * u_xlat10.xy;
					    u_xlat9.x = u_xlat9.y + u_xlat9.x;
					    u_xlat18 = u_xlat9.y / u_xlat2.x;
					    u_xlat9.x = u_xlat10.z * u_xlat9.z + u_xlat9.x;
					    u_xlat9.x = u_xlat9.x * 0.600000024 + 0.400000006;
					    u_xlat27 = u_xlat1.x;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x);
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat10.x = max(u_xlat27, u_xlat1.x);
					    u_xlat10.x = (-u_xlat10.x) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = vec3(u_xlat27) * u_xlat2.yzx;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat10.xxx + u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat1.xxx * u_xlat2.zxy + u_xlat10.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = u_xlat9.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat9.xyz = u_xlat2.xyz * vec3(u_xlat18) + u_xlat1.xyz;
					    u_xlat1.x = _FlashDir.y;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat10.x = u_xlat1.x * u_xlat1.x;
					    u_xlat1.x = (-u_xlat1.x) * u_xlat10.x + 1.0;
					    u_xlat1.xyz = u_xlat1.xxx * _FlashColor.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat9.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat2 = u_xlat1 * _FoamColor;
					    u_xlat9.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    vs_COLOR0.w = u_xlat2.w * in_COLOR0.w;
					    u_xlat1.xyz = _FoamColor.xyz * u_xlat1.xyz + (-u_xlat9.xxx);
					    u_xlat9.xyz = _CloudCoverage.yyy * u_xlat1.xyz + u_xlat9.xxx;
					    u_xlat1.xyz = (-u_xlat9.xyz) + _LutLerp.www;
					    u_xlat9.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat9.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat9.xyz) + u_xlat1.xyz;
					    u_xlat28 = _FogMaxRad + (-_FogMinRad);
					    u_xlat0.x = u_xlat0.x / u_xlat28;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat9.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * in_COLOR0.xyz;
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec3 _PaintTexOffset;
						vec4 _LevelRect;
						vec4 unused_0_9;
						vec3 _WindDir;
						float _SqrtWindTime;
						vec4 unused_0_12[16];
						vec3 _SunDir;
						vec4 unused_0_14[4];
						vec4 _SideSunColor;
						vec4 unused_0_16[2];
						float _Year;
						vec4 unused_0_18[2];
						vec4 _FoamColor;
						vec4 unused_0_20[4];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_24[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_31;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_34[3];
						vec4 _FoamTex_ST;
						vec4 unused_0_36[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
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
					vec3 u_xlat9;
					vec3 u_xlat10;
					vec3 u_xlat11;
					float u_xlat18;
					bool u_xlatb18;
					float u_xlat22;
					float u_xlat27;
					bool u_xlatb27;
					float u_xlat28;
					float u_xlat29;
					float u_xlat30;
					void main()
					{
					    vs_TEXCOORD0.y = in_TEXCOORD0.x;
					    u_xlat0.x = _SqrtWindTime * 0.0500000007;
					    u_xlat9.x = (-in_TEXCOORD0.y) + _Time.y;
					    u_xlatb18 = 40.0>=u_xlat9.x;
					    u_xlatb27 = u_xlat9.x>=0.0;
					    u_xlatb18 = u_xlatb27 && u_xlatb18;
					    u_xlat27 = u_xlat9.x * 0.0250000004;
					    vs_TEXCOORD0.x = (-u_xlat9.x) * 0.0250000004 + 1.0;
					    u_xlat27 = u_xlat27;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat9.x = u_xlat27 * 0.200000003 + in_TEXCOORD1.x;
					    u_xlat9.xz = in_TEXCOORD3.xy * u_xlat9.xx + in_TEXCOORD2.xy;
					    u_xlat1.xz = (bool(u_xlatb18)) ? u_xlat9.xz : in_TEXCOORD2.xy;
					    u_xlat9.xy = u_xlat1.xz * _FoamTex_ST.xy + _FoamTex_ST.zw;
					    vs_TEXCOORD2.xy = (-u_xlat0.xx) * _WindDir.xz + u_xlat9.xy;
					    u_xlat0 = vec4(_WaterLevel) * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat1.xz;
					    u_xlat0.y = _WaterLevel;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat27 = (-u_xlat2.w) * 0.5 + _WaterLevel;
					    u_xlat27 = u_xlat27 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat28 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat29 = u_xlat2.y * u_xlat28;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat29;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat29;
					    u_xlat6 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat11.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat8.yyy;
					    u_xlat6.xyz = u_xlat8.zzz * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat8.xxx;
					    u_xlat7.xyz = u_xlat8.yyy * u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat28 = u_xlat28 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat28;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat28;
					    u_xlat11.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat5.xyz = u_xlat0.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat0.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat8.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat0.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat5.xyz * u_xlat8.zzz + u_xlat3.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat11.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat11.zx, 0.0);
					    u_xlat4.w = u_xlat11.x;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat8.yyy * u_xlat2.xyz;
					    u_xlat5.xyz = u_xlat8.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat8.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat5.xyz * u_xlat0.zzz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat0.zzz + u_xlat3.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat4.xyz = u_xlat0.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat8.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat0.yyy * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat4.xyz * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat28 = u_xlat2.x + u_xlat2.y;
					    u_xlat0.xy = u_xlat0.xy * u_xlat3.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat9.x = u_xlat0.y / u_xlat28;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.400000006;
					    u_xlat18 = u_xlat27;
					    u_xlat18 = clamp(u_xlat18, 0.0, 1.0);
					    u_xlat27 = (-u_xlat27);
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat28 = max(u_xlat27, u_xlat18);
					    u_xlat28 = (-u_xlat28) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = vec3(u_xlat18) * u_xlat2.yzx;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(u_xlat28) + u_xlat3.xyz;
					    u_xlat2.xyz = vec3(u_xlat27) * u_xlat2.zxy + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat2.xyz * u_xlat9.xxx + u_xlat0.xzw;
					    u_xlat27 = _FlashDir.y;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat27 = (-u_xlat27) + 1.0;
					    u_xlat28 = u_xlat27 * u_xlat27;
					    u_xlat27 = (-u_xlat27) * u_xlat28 + 1.0;
					    u_xlat2.xyz = vec3(u_xlat27) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat2 = u_xlat0 * _FoamColor;
					    u_xlat27 = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = _FoamColor.xyz * u_xlat0.xyz + (-vec3(u_xlat27));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat27);
					    u_xlat2.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat2.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + u_xlat2.xyz;
					    u_xlat27 = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat27 = sqrt(u_xlat27);
					    u_xlat27 = u_xlat27 + (-_FogMinRad);
					    u_xlat28 = _FogMaxRad + (-_FogMinRad);
					    u_xlat27 = u_xlat27 / u_xlat28;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat27) * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat1.y = _WaterLevel;
					    u_xlat1.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat2.xy = max(u_xlat1.xz, _LevelRect.xy);
					    u_xlat1.xz = min(u_xlat2.xy, _LevelRect.zw);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat4.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat27 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat28 = u_xlat1.y * u_xlat27;
					    u_xlat6.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat28;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat28;
					    u_xlat7 = textureLod(_PaintTex, u_xlat5.xz, 0.0);
					    u_xlat10.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_PaintTex, u_xlat5.yz, 0.0);
					    u_xlat30 = u_xlat3.x * u_xlat8.y;
					    u_xlat30 = u_xlat3.y * u_xlat30;
					    u_xlat22 = u_xlat2.x * u_xlat7.y;
					    u_xlat22 = u_xlat3.y * u_xlat22;
					    u_xlat22 = u_xlat3.z * u_xlat22;
					    u_xlat30 = u_xlat30 * u_xlat3.z + u_xlat22;
					    u_xlat27 = u_xlat27 * u_xlat6.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat27;
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat27;
					    u_xlat10.x = u_xlat4.y * u_xlat6.z;
					    u_xlat4 = textureLod(_PaintTex, u_xlat5.yz, 0.0);
					    u_xlat6 = textureLod(_PaintTex, u_xlat5.xz, 0.0);
					    u_xlat27 = u_xlat2.x * u_xlat6.y;
					    u_xlat27 = u_xlat2.y * u_xlat27;
					    u_xlat1.x = u_xlat3.x * u_xlat4.y;
					    u_xlat1.x = u_xlat2.y * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * u_xlat3.z + u_xlat30;
					    u_xlat27 = u_xlat27 * u_xlat3.z + u_xlat1.x;
					    u_xlat4 = textureLod(_PaintTex, u_xlat10.yx, 0.0);
					    u_xlat6 = textureLod(_PaintTex, u_xlat10.zx, 0.0);
					    u_xlat5.w = u_xlat10.x;
					    u_xlat1.x = u_xlat2.x * u_xlat6.y;
					    u_xlat1.y = u_xlat3.x * u_xlat4.y;
					    u_xlat1.xy = u_xlat3.yy * u_xlat1.xy;
					    u_xlat27 = u_xlat1.y * u_xlat2.z + u_xlat27;
					    u_xlat27 = u_xlat1.x * u_xlat2.z + u_xlat27;
					    u_xlat1 = textureLod(_PaintTex, u_xlat5.yw, 0.0);
					    u_xlat4 = textureLod(_PaintTex, u_xlat5.xw, 0.0);
					    u_xlat1.x = u_xlat2.x * u_xlat4.y;
					    u_xlat1.y = u_xlat3.x * u_xlat1.y;
					    u_xlat1.xy = u_xlat2.yy * u_xlat1.xy;
					    u_xlat27 = u_xlat1.y * u_xlat2.z + u_xlat27;
					    u_xlat27 = u_xlat1.x * u_xlat2.z + u_xlat27;
					    u_xlat1.xyz = (-in_COLOR0.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat1.xyz = vec3(u_xlat27) * u_xlat1.xyz + in_COLOR0.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * u_xlat1.xyz;
					    u_xlat0.x = in_COLOR0.w;
					    vs_COLOR0.w = u_xlat0.x * u_xlat2.w;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" "_SELECTION_ON" }
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
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec3 _PaintTexOffset;
						vec4 _LevelRect;
						vec4 unused_0_9;
						vec3 _WindDir;
						float _SqrtWindTime;
						vec4 unused_0_12[16];
						vec3 _SunDir;
						vec4 unused_0_14[4];
						vec4 _SideSunColor;
						vec4 unused_0_16[2];
						float _Year;
						vec4 unused_0_18[2];
						vec4 _FoamColor;
						vec4 unused_0_20[4];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_24[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_31;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_34[3];
						vec4 _FoamTex_ST;
						vec4 unused_0_36[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
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
					vec3 u_xlat9;
					vec3 u_xlat10;
					vec3 u_xlat11;
					float u_xlat18;
					bool u_xlatb18;
					float u_xlat22;
					float u_xlat27;
					bool u_xlatb27;
					float u_xlat28;
					float u_xlat29;
					float u_xlat30;
					void main()
					{
					    vs_TEXCOORD0.y = in_TEXCOORD0.x;
					    u_xlat0.x = _SqrtWindTime * 0.0500000007;
					    u_xlat9.x = (-in_TEXCOORD0.y) + _Time.y;
					    u_xlatb18 = 40.0>=u_xlat9.x;
					    u_xlatb27 = u_xlat9.x>=0.0;
					    u_xlatb18 = u_xlatb27 && u_xlatb18;
					    u_xlat27 = u_xlat9.x * 0.0250000004;
					    vs_TEXCOORD0.x = (-u_xlat9.x) * 0.0250000004 + 1.0;
					    u_xlat27 = u_xlat27;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat9.x = u_xlat27 * 0.200000003 + in_TEXCOORD1.x;
					    u_xlat9.xz = in_TEXCOORD3.xy * u_xlat9.xx + in_TEXCOORD2.xy;
					    u_xlat1.xz = (bool(u_xlatb18)) ? u_xlat9.xz : in_TEXCOORD2.xy;
					    u_xlat9.xy = u_xlat1.xz * _FoamTex_ST.xy + _FoamTex_ST.zw;
					    vs_TEXCOORD2.xy = (-u_xlat0.xx) * _WindDir.xz + u_xlat9.xy;
					    u_xlat0 = vec4(_WaterLevel) * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat1.xz;
					    u_xlat0.y = _WaterLevel;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat27 = (-u_xlat2.w) * 0.5 + _WaterLevel;
					    u_xlat27 = u_xlat27 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat28 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat29 = u_xlat2.y * u_xlat28;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat29;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat29;
					    u_xlat6 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat11.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat8.yyy;
					    u_xlat6.xyz = u_xlat8.zzz * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat8.xxx;
					    u_xlat7.xyz = u_xlat8.yyy * u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat28 = u_xlat28 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat28;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat28;
					    u_xlat11.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat5.xyz = u_xlat0.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat0.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat8.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat0.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat5.xyz * u_xlat8.zzz + u_xlat3.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat11.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat11.zx, 0.0);
					    u_xlat4.w = u_xlat11.x;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat8.yyy * u_xlat2.xyz;
					    u_xlat5.xyz = u_xlat8.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat8.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat5.xyz * u_xlat0.zzz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat0.zzz + u_xlat3.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat4.xyz = u_xlat0.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat8.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat0.yyy * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat4.xyz * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat28 = u_xlat2.x + u_xlat2.y;
					    u_xlat0.xy = u_xlat0.xy * u_xlat3.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat9.x = u_xlat0.y / u_xlat28;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.400000006;
					    u_xlat18 = u_xlat27;
					    u_xlat18 = clamp(u_xlat18, 0.0, 1.0);
					    u_xlat27 = (-u_xlat27);
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat28 = max(u_xlat27, u_xlat18);
					    u_xlat28 = (-u_xlat28) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = vec3(u_xlat18) * u_xlat2.yzx;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(u_xlat28) + u_xlat3.xyz;
					    u_xlat2.xyz = vec3(u_xlat27) * u_xlat2.zxy + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat2.xyz * u_xlat9.xxx + u_xlat0.xzw;
					    u_xlat27 = _FlashDir.y;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat27 = (-u_xlat27) + 1.0;
					    u_xlat28 = u_xlat27 * u_xlat27;
					    u_xlat27 = (-u_xlat27) * u_xlat28 + 1.0;
					    u_xlat2.xyz = vec3(u_xlat27) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat2 = u_xlat0 * _FoamColor;
					    u_xlat27 = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = _FoamColor.xyz * u_xlat0.xyz + (-vec3(u_xlat27));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat27);
					    u_xlat2.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat2.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + u_xlat2.xyz;
					    u_xlat27 = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat27 = sqrt(u_xlat27);
					    u_xlat27 = u_xlat27 + (-_FogMinRad);
					    u_xlat28 = _FogMaxRad + (-_FogMinRad);
					    u_xlat27 = u_xlat27 / u_xlat28;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat27) * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat1.y = _WaterLevel;
					    u_xlat1.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat2.xy = max(u_xlat1.xz, _LevelRect.xy);
					    u_xlat1.xz = min(u_xlat2.xy, _LevelRect.zw);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat4.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat27 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat28 = u_xlat1.y * u_xlat27;
					    u_xlat6.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat28;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat28;
					    u_xlat7 = textureLod(_PaintTex, u_xlat5.xz, 0.0);
					    u_xlat10.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_PaintTex, u_xlat5.yz, 0.0);
					    u_xlat30 = u_xlat3.x * u_xlat8.y;
					    u_xlat30 = u_xlat3.y * u_xlat30;
					    u_xlat22 = u_xlat2.x * u_xlat7.y;
					    u_xlat22 = u_xlat3.y * u_xlat22;
					    u_xlat22 = u_xlat3.z * u_xlat22;
					    u_xlat30 = u_xlat30 * u_xlat3.z + u_xlat22;
					    u_xlat27 = u_xlat27 * u_xlat6.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat27;
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat27;
					    u_xlat10.x = u_xlat4.y * u_xlat6.z;
					    u_xlat4 = textureLod(_PaintTex, u_xlat5.yz, 0.0);
					    u_xlat6 = textureLod(_PaintTex, u_xlat5.xz, 0.0);
					    u_xlat27 = u_xlat2.x * u_xlat6.y;
					    u_xlat27 = u_xlat2.y * u_xlat27;
					    u_xlat1.x = u_xlat3.x * u_xlat4.y;
					    u_xlat1.x = u_xlat2.y * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * u_xlat3.z + u_xlat30;
					    u_xlat27 = u_xlat27 * u_xlat3.z + u_xlat1.x;
					    u_xlat4 = textureLod(_PaintTex, u_xlat10.yx, 0.0);
					    u_xlat6 = textureLod(_PaintTex, u_xlat10.zx, 0.0);
					    u_xlat5.w = u_xlat10.x;
					    u_xlat1.x = u_xlat2.x * u_xlat6.y;
					    u_xlat1.y = u_xlat3.x * u_xlat4.y;
					    u_xlat1.xy = u_xlat3.yy * u_xlat1.xy;
					    u_xlat27 = u_xlat1.y * u_xlat2.z + u_xlat27;
					    u_xlat27 = u_xlat1.x * u_xlat2.z + u_xlat27;
					    u_xlat1 = textureLod(_PaintTex, u_xlat5.yw, 0.0);
					    u_xlat4 = textureLod(_PaintTex, u_xlat5.xw, 0.0);
					    u_xlat1.x = u_xlat2.x * u_xlat4.y;
					    u_xlat1.y = u_xlat3.x * u_xlat1.y;
					    u_xlat1.xy = u_xlat2.yy * u_xlat1.xy;
					    u_xlat27 = u_xlat1.y * u_xlat2.z + u_xlat27;
					    u_xlat27 = u_xlat1.x * u_xlat2.z + u_xlat27;
					    u_xlat1.xyz = (-in_COLOR0.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat1.xyz = vec3(u_xlat27) * u_xlat1.xyz + in_COLOR0.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * u_xlat1.xyz;
					    u_xlat0.x = in_COLOR0.w;
					    vs_COLOR0.w = u_xlat0.x * u_xlat2.w;
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[5];
						vec4 _LutLerp;
						vec4 unused_0_5[5];
						vec3 _WindDir;
						float _SqrtWindTime;
						vec4 unused_0_8[16];
						vec3 _SunDir;
						vec4 unused_0_10[4];
						vec4 _SideSunColor;
						vec4 unused_0_12[2];
						float _Year;
						vec4 unused_0_14[2];
						vec4 _FoamColor;
						vec4 unused_0_16[4];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_20[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_27;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_30[3];
						vec4 _FoamTex_ST;
						vec4 unused_0_32[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat7;
					vec3 u_xlat8;
					vec3 u_xlat9;
					vec3 u_xlat10;
					vec3 u_xlat15;
					float u_xlat18;
					bool u_xlatb18;
					float u_xlat19;
					float u_xlat20;
					float u_xlat27;
					bool u_xlatb27;
					float u_xlat28;
					float u_xlat29;
					void main()
					{
					    u_xlat0.x = _SqrtWindTime * 0.0500000007;
					    u_xlat9.x = (-in_TEXCOORD0.y) + _Time.y;
					    u_xlatb18 = 40.0>=u_xlat9.x;
					    u_xlatb27 = u_xlat9.x>=0.0;
					    u_xlatb18 = u_xlatb27 && u_xlatb18;
					    u_xlat27 = u_xlat9.x * 0.0250000004;
					    vs_TEXCOORD0.x = (-u_xlat9.x) * 0.0250000004 + 1.0;
					    u_xlat27 = u_xlat27;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat9.x = u_xlat27 * 0.200000003 + in_TEXCOORD1.x;
					    u_xlat9.xz = in_TEXCOORD3.xy * u_xlat9.xx + in_TEXCOORD2.xy;
					    u_xlat9.xy = (bool(u_xlatb18)) ? u_xlat9.xz : in_TEXCOORD2.xy;
					    u_xlat1.xy = u_xlat9.xy * _FoamTex_ST.xy + _FoamTex_ST.zw;
					    vs_TEXCOORD2.xy = (-u_xlat0.xx) * _WindDir.xz + u_xlat1.xy;
					    vs_TEXCOORD0.y = in_TEXCOORD0.x;
					    u_xlat1 = vec4(_WaterLevel) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat9.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat9.yyyy + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat9.xy;
					    u_xlat0.x = dot(u_xlat9.xy, u_xlat9.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.y = _WaterLevel;
					    u_xlat9.xyz = u_xlat1.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat9.xyz = max(u_xlat9.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat1 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat9.xyz = min(u_xlat9.xyz, u_xlat1.xyz);
					    u_xlat1.x = (-u_xlat1.w) * 0.5 + _WaterLevel;
					    u_xlat1.x = u_xlat1.x * 0.25;
					    u_xlat9.xyz = u_xlat9.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat10.xyz = floor(u_xlat9.xyz);
					    u_xlat9.xyz = fract(u_xlat9.xyz);
					    u_xlat2.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat3.z = u_xlat10.z * u_xlat2.y;
					    u_xlat20 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat29 = u_xlat10.y * u_xlat20;
					    u_xlat4.xyz = u_xlat10.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat3.x = u_xlat2.x * u_xlat4.x + u_xlat29;
					    u_xlat3.y = u_xlat2.x * u_xlat10.x + u_xlat29;
					    u_xlat5 = textureLod(_AoTex, u_xlat3.xz, 0.0);
					    u_xlat15.yz = u_xlat3.yx;
					    u_xlat7 = textureLod(_AoTex, u_xlat3.yz, 0.0);
					    u_xlat5.xyz = u_xlat9.xxx * u_xlat5.xyz;
					    u_xlat8.xyz = (-u_xlat9.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat8.yyy;
					    u_xlat5.xyz = u_xlat8.zzz * u_xlat5.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat8.xxx;
					    u_xlat7.xyz = u_xlat8.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat5.xyz;
					    u_xlat19 = u_xlat20 * u_xlat4.y;
					    u_xlat3.y = u_xlat2.x * u_xlat10.x + u_xlat19;
					    u_xlat3.x = u_xlat2.x * u_xlat4.x + u_xlat19;
					    u_xlat15.x = u_xlat2.y * u_xlat4.z;
					    u_xlat2 = textureLod(_AoTex, u_xlat3.yz, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat3.xz, 0.0);
					    u_xlat10.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat10.xyz = u_xlat9.yyy * u_xlat10.xyz;
					    u_xlat2.xyz = u_xlat8.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat9.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat8.zzz + u_xlat5.xyz;
					    u_xlat10.xyz = u_xlat10.xyz * u_xlat8.zzz + u_xlat2.xyz;
					    u_xlat2 = textureLod(_AoTex, u_xlat15.yx, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat15.zx, 0.0);
					    u_xlat3.w = u_xlat15.x;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat8.yyy * u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat8.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat8.yyy * u_xlat2.xyz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat2 = textureLod(_AoTex, u_xlat3.yw, 0.0);
					    u_xlat3 = textureLod(_AoTex, u_xlat3.xw, 0.0);
					    u_xlat3.xyz = u_xlat9.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat9.yyy * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat8.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat9.yyy * u_xlat2.xyz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat3.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat10.xyz = u_xlat2.zxw;
					    u_xlat10.xyz = clamp(u_xlat10.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat9.xy = u_xlat9.xy * u_xlat10.xy;
					    u_xlat9.x = u_xlat9.y + u_xlat9.x;
					    u_xlat18 = u_xlat9.y / u_xlat2.x;
					    u_xlat9.x = u_xlat10.z * u_xlat9.z + u_xlat9.x;
					    u_xlat9.x = u_xlat9.x * 0.600000024 + 0.400000006;
					    u_xlat27 = u_xlat1.x;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x);
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat10.x = max(u_xlat27, u_xlat1.x);
					    u_xlat10.x = (-u_xlat10.x) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = vec3(u_xlat27) * u_xlat2.yzx;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat10.xxx + u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat1.xxx * u_xlat2.zxy + u_xlat10.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = u_xlat9.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat9.xyz = u_xlat2.xyz * vec3(u_xlat18) + u_xlat1.xyz;
					    u_xlat1.x = _FlashDir.y;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat10.x = u_xlat1.x * u_xlat1.x;
					    u_xlat1.x = (-u_xlat1.x) * u_xlat10.x + 1.0;
					    u_xlat1.xyz = u_xlat1.xxx * _FlashColor.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat9.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat2 = u_xlat1 * _FoamColor;
					    u_xlat9.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    vs_COLOR0.w = u_xlat2.w * in_COLOR0.w;
					    u_xlat1.xyz = _FoamColor.xyz * u_xlat1.xyz + (-u_xlat9.xxx);
					    u_xlat9.xyz = _CloudCoverage.yyy * u_xlat1.xyz + u_xlat9.xxx;
					    u_xlat1.xyz = (-u_xlat9.xyz) + _LutLerp.www;
					    u_xlat9.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat9.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat9.xyz) + u_xlat1.xyz;
					    u_xlat28 = _FogMaxRad + (-_FogMinRad);
					    u_xlat0.x = u_xlat0.x / u_xlat28;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat9.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * in_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_VOXELAO_ON" "_SELECTION_ON" }
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
						vec4 unused_0_5[5];
						vec3 _WindDir;
						float _SqrtWindTime;
						vec4 unused_0_8[16];
						vec3 _SunDir;
						vec4 unused_0_10[4];
						vec4 _SideSunColor;
						vec4 unused_0_12[2];
						float _Year;
						vec4 unused_0_14[2];
						vec4 _FoamColor;
						vec4 unused_0_16[4];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_20[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_27;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_30[3];
						vec4 _FoamTex_ST;
						vec4 unused_0_32[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat7;
					vec3 u_xlat8;
					vec3 u_xlat9;
					vec3 u_xlat10;
					vec3 u_xlat15;
					float u_xlat18;
					bool u_xlatb18;
					float u_xlat19;
					float u_xlat20;
					float u_xlat27;
					bool u_xlatb27;
					float u_xlat28;
					float u_xlat29;
					void main()
					{
					    u_xlat0.x = _SqrtWindTime * 0.0500000007;
					    u_xlat9.x = (-in_TEXCOORD0.y) + _Time.y;
					    u_xlatb18 = 40.0>=u_xlat9.x;
					    u_xlatb27 = u_xlat9.x>=0.0;
					    u_xlatb18 = u_xlatb27 && u_xlatb18;
					    u_xlat27 = u_xlat9.x * 0.0250000004;
					    vs_TEXCOORD0.x = (-u_xlat9.x) * 0.0250000004 + 1.0;
					    u_xlat27 = u_xlat27;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat9.x = u_xlat27 * 0.200000003 + in_TEXCOORD1.x;
					    u_xlat9.xz = in_TEXCOORD3.xy * u_xlat9.xx + in_TEXCOORD2.xy;
					    u_xlat9.xy = (bool(u_xlatb18)) ? u_xlat9.xz : in_TEXCOORD2.xy;
					    u_xlat1.xy = u_xlat9.xy * _FoamTex_ST.xy + _FoamTex_ST.zw;
					    vs_TEXCOORD2.xy = (-u_xlat0.xx) * _WindDir.xz + u_xlat1.xy;
					    vs_TEXCOORD0.y = in_TEXCOORD0.x;
					    u_xlat1 = vec4(_WaterLevel) * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat9.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat9.yyyy + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat9.xy;
					    u_xlat0.x = dot(u_xlat9.xy, u_xlat9.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat1.y = _WaterLevel;
					    u_xlat9.xyz = u_xlat1.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat9.xyz = max(u_xlat9.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat1 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat9.xyz = min(u_xlat9.xyz, u_xlat1.xyz);
					    u_xlat1.x = (-u_xlat1.w) * 0.5 + _WaterLevel;
					    u_xlat1.x = u_xlat1.x * 0.25;
					    u_xlat9.xyz = u_xlat9.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat10.xyz = floor(u_xlat9.xyz);
					    u_xlat9.xyz = fract(u_xlat9.xyz);
					    u_xlat2.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat3.z = u_xlat10.z * u_xlat2.y;
					    u_xlat20 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat29 = u_xlat10.y * u_xlat20;
					    u_xlat4.xyz = u_xlat10.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat3.x = u_xlat2.x * u_xlat4.x + u_xlat29;
					    u_xlat3.y = u_xlat2.x * u_xlat10.x + u_xlat29;
					    u_xlat5 = textureLod(_AoTex, u_xlat3.xz, 0.0);
					    u_xlat15.yz = u_xlat3.yx;
					    u_xlat7 = textureLod(_AoTex, u_xlat3.yz, 0.0);
					    u_xlat5.xyz = u_xlat9.xxx * u_xlat5.xyz;
					    u_xlat8.xyz = (-u_xlat9.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat5.xyz = u_xlat5.xyz * u_xlat8.yyy;
					    u_xlat5.xyz = u_xlat8.zzz * u_xlat5.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat8.xxx;
					    u_xlat7.xyz = u_xlat8.yyy * u_xlat7.xyz;
					    u_xlat5.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat5.xyz;
					    u_xlat19 = u_xlat20 * u_xlat4.y;
					    u_xlat3.y = u_xlat2.x * u_xlat10.x + u_xlat19;
					    u_xlat3.x = u_xlat2.x * u_xlat4.x + u_xlat19;
					    u_xlat15.x = u_xlat2.y * u_xlat4.z;
					    u_xlat2 = textureLod(_AoTex, u_xlat3.yz, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat3.xz, 0.0);
					    u_xlat10.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat10.xyz = u_xlat9.yyy * u_xlat10.xyz;
					    u_xlat2.xyz = u_xlat8.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat9.yyy * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat8.zzz + u_xlat5.xyz;
					    u_xlat10.xyz = u_xlat10.xyz * u_xlat8.zzz + u_xlat2.xyz;
					    u_xlat2 = textureLod(_AoTex, u_xlat15.yx, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat15.zx, 0.0);
					    u_xlat3.w = u_xlat15.x;
					    u_xlat4.xyz = u_xlat9.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat8.yyy * u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat8.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat8.yyy * u_xlat2.xyz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat4.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat2 = textureLod(_AoTex, u_xlat3.yw, 0.0);
					    u_xlat3 = textureLod(_AoTex, u_xlat3.xw, 0.0);
					    u_xlat3.xyz = u_xlat9.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat9.yyy * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat8.xxx * u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat9.yyy * u_xlat2.xyz;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat3.xyz * u_xlat9.zzz + u_xlat10.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat10.xyz = u_xlat2.zxw;
					    u_xlat10.xyz = clamp(u_xlat10.xyz, 0.0, 1.0);
					    u_xlat2.x = u_xlat2.x + u_xlat2.y;
					    u_xlat9.xy = u_xlat9.xy * u_xlat10.xy;
					    u_xlat9.x = u_xlat9.y + u_xlat9.x;
					    u_xlat18 = u_xlat9.y / u_xlat2.x;
					    u_xlat9.x = u_xlat10.z * u_xlat9.z + u_xlat9.x;
					    u_xlat9.x = u_xlat9.x * 0.600000024 + 0.400000006;
					    u_xlat27 = u_xlat1.x;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x);
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat10.x = max(u_xlat27, u_xlat1.x);
					    u_xlat10.x = (-u_xlat10.x) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = vec3(u_xlat27) * u_xlat2.yzx;
					    u_xlat10.xyz = u_xlat2.xyz * u_xlat10.xxx + u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat1.xxx * u_xlat2.zxy + u_xlat10.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + _SnowColor.xyz;
					    u_xlat1.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat1.xyz * _MinAmbientColor.xyz;
					    u_xlat1.xyz = (-_MinAmbientColor.xyz) * u_xlat1.xyz + _MaxAmbientColor.xyz;
					    u_xlat1.xyz = u_xlat9.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat9.xyz = u_xlat2.xyz * vec3(u_xlat18) + u_xlat1.xyz;
					    u_xlat1.x = _FlashDir.y;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat10.x = u_xlat1.x * u_xlat1.x;
					    u_xlat1.x = (-u_xlat1.x) * u_xlat10.x + 1.0;
					    u_xlat1.xyz = u_xlat1.xxx * _FlashColor.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat9.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat2 = u_xlat1 * _FoamColor;
					    u_xlat9.x = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    vs_COLOR0.w = u_xlat2.w * in_COLOR0.w;
					    u_xlat1.xyz = _FoamColor.xyz * u_xlat1.xyz + (-u_xlat9.xxx);
					    u_xlat9.xyz = _CloudCoverage.yyy * u_xlat1.xyz + u_xlat9.xxx;
					    u_xlat1.xyz = (-u_xlat9.xyz) + _LutLerp.www;
					    u_xlat9.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat9.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat9.xyz) + u_xlat1.xyz;
					    u_xlat28 = _FogMaxRad + (-_FogMinRad);
					    u_xlat0.x = u_xlat0.x / u_xlat28;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat9.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * in_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" "_VOXELAO_ON" }
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
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec3 _PaintTexOffset;
						vec4 _LevelRect;
						vec4 unused_0_9;
						vec3 _WindDir;
						float _SqrtWindTime;
						vec4 unused_0_12[16];
						vec3 _SunDir;
						vec4 unused_0_14[4];
						vec4 _SideSunColor;
						vec4 unused_0_16[2];
						float _Year;
						vec4 unused_0_18[2];
						vec4 _FoamColor;
						vec4 unused_0_20[4];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_24[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_31;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_34[3];
						vec4 _FoamTex_ST;
						vec4 unused_0_36[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
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
					vec3 u_xlat9;
					vec3 u_xlat10;
					vec3 u_xlat11;
					float u_xlat18;
					bool u_xlatb18;
					float u_xlat22;
					float u_xlat27;
					bool u_xlatb27;
					float u_xlat28;
					float u_xlat29;
					float u_xlat30;
					void main()
					{
					    vs_TEXCOORD0.y = in_TEXCOORD0.x;
					    u_xlat0.x = _SqrtWindTime * 0.0500000007;
					    u_xlat9.x = (-in_TEXCOORD0.y) + _Time.y;
					    u_xlatb18 = 40.0>=u_xlat9.x;
					    u_xlatb27 = u_xlat9.x>=0.0;
					    u_xlatb18 = u_xlatb27 && u_xlatb18;
					    u_xlat27 = u_xlat9.x * 0.0250000004;
					    vs_TEXCOORD0.x = (-u_xlat9.x) * 0.0250000004 + 1.0;
					    u_xlat27 = u_xlat27;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat9.x = u_xlat27 * 0.200000003 + in_TEXCOORD1.x;
					    u_xlat9.xz = in_TEXCOORD3.xy * u_xlat9.xx + in_TEXCOORD2.xy;
					    u_xlat1.xz = (bool(u_xlatb18)) ? u_xlat9.xz : in_TEXCOORD2.xy;
					    u_xlat9.xy = u_xlat1.xz * _FoamTex_ST.xy + _FoamTex_ST.zw;
					    vs_TEXCOORD2.xy = (-u_xlat0.xx) * _WindDir.xz + u_xlat9.xy;
					    u_xlat0 = vec4(_WaterLevel) * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat1.xz;
					    u_xlat0.y = _WaterLevel;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat27 = (-u_xlat2.w) * 0.5 + _WaterLevel;
					    u_xlat27 = u_xlat27 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat28 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat29 = u_xlat2.y * u_xlat28;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat29;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat29;
					    u_xlat6 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat11.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat8.yyy;
					    u_xlat6.xyz = u_xlat8.zzz * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat8.xxx;
					    u_xlat7.xyz = u_xlat8.yyy * u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat28 = u_xlat28 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat28;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat28;
					    u_xlat11.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat5.xyz = u_xlat0.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat0.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat8.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat0.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat5.xyz * u_xlat8.zzz + u_xlat3.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat11.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat11.zx, 0.0);
					    u_xlat4.w = u_xlat11.x;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat8.yyy * u_xlat2.xyz;
					    u_xlat5.xyz = u_xlat8.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat8.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat5.xyz * u_xlat0.zzz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat0.zzz + u_xlat3.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat4.xyz = u_xlat0.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat8.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat0.yyy * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat4.xyz * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat28 = u_xlat2.x + u_xlat2.y;
					    u_xlat0.xy = u_xlat0.xy * u_xlat3.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat9.x = u_xlat0.y / u_xlat28;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.400000006;
					    u_xlat18 = u_xlat27;
					    u_xlat18 = clamp(u_xlat18, 0.0, 1.0);
					    u_xlat27 = (-u_xlat27);
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat28 = max(u_xlat27, u_xlat18);
					    u_xlat28 = (-u_xlat28) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = vec3(u_xlat18) * u_xlat2.yzx;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(u_xlat28) + u_xlat3.xyz;
					    u_xlat2.xyz = vec3(u_xlat27) * u_xlat2.zxy + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat2.xyz * u_xlat9.xxx + u_xlat0.xzw;
					    u_xlat27 = _FlashDir.y;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat27 = (-u_xlat27) + 1.0;
					    u_xlat28 = u_xlat27 * u_xlat27;
					    u_xlat27 = (-u_xlat27) * u_xlat28 + 1.0;
					    u_xlat2.xyz = vec3(u_xlat27) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat2 = u_xlat0 * _FoamColor;
					    u_xlat27 = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = _FoamColor.xyz * u_xlat0.xyz + (-vec3(u_xlat27));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat27);
					    u_xlat2.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat2.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + u_xlat2.xyz;
					    u_xlat27 = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat27 = sqrt(u_xlat27);
					    u_xlat27 = u_xlat27 + (-_FogMinRad);
					    u_xlat28 = _FogMaxRad + (-_FogMinRad);
					    u_xlat27 = u_xlat27 / u_xlat28;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat27) * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat1.y = _WaterLevel;
					    u_xlat1.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat2.xy = max(u_xlat1.xz, _LevelRect.xy);
					    u_xlat1.xz = min(u_xlat2.xy, _LevelRect.zw);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat4.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat27 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat28 = u_xlat1.y * u_xlat27;
					    u_xlat6.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat28;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat28;
					    u_xlat7 = textureLod(_PaintTex, u_xlat5.xz, 0.0);
					    u_xlat10.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_PaintTex, u_xlat5.yz, 0.0);
					    u_xlat30 = u_xlat3.x * u_xlat8.y;
					    u_xlat30 = u_xlat3.y * u_xlat30;
					    u_xlat22 = u_xlat2.x * u_xlat7.y;
					    u_xlat22 = u_xlat3.y * u_xlat22;
					    u_xlat22 = u_xlat3.z * u_xlat22;
					    u_xlat30 = u_xlat30 * u_xlat3.z + u_xlat22;
					    u_xlat27 = u_xlat27 * u_xlat6.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat27;
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat27;
					    u_xlat10.x = u_xlat4.y * u_xlat6.z;
					    u_xlat4 = textureLod(_PaintTex, u_xlat5.yz, 0.0);
					    u_xlat6 = textureLod(_PaintTex, u_xlat5.xz, 0.0);
					    u_xlat27 = u_xlat2.x * u_xlat6.y;
					    u_xlat27 = u_xlat2.y * u_xlat27;
					    u_xlat1.x = u_xlat3.x * u_xlat4.y;
					    u_xlat1.x = u_xlat2.y * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * u_xlat3.z + u_xlat30;
					    u_xlat27 = u_xlat27 * u_xlat3.z + u_xlat1.x;
					    u_xlat4 = textureLod(_PaintTex, u_xlat10.yx, 0.0);
					    u_xlat6 = textureLod(_PaintTex, u_xlat10.zx, 0.0);
					    u_xlat5.w = u_xlat10.x;
					    u_xlat1.x = u_xlat2.x * u_xlat6.y;
					    u_xlat1.y = u_xlat3.x * u_xlat4.y;
					    u_xlat1.xy = u_xlat3.yy * u_xlat1.xy;
					    u_xlat27 = u_xlat1.y * u_xlat2.z + u_xlat27;
					    u_xlat27 = u_xlat1.x * u_xlat2.z + u_xlat27;
					    u_xlat1 = textureLod(_PaintTex, u_xlat5.yw, 0.0);
					    u_xlat4 = textureLod(_PaintTex, u_xlat5.xw, 0.0);
					    u_xlat1.x = u_xlat2.x * u_xlat4.y;
					    u_xlat1.y = u_xlat3.x * u_xlat1.y;
					    u_xlat1.xy = u_xlat2.yy * u_xlat1.xy;
					    u_xlat27 = u_xlat1.y * u_xlat2.z + u_xlat27;
					    u_xlat27 = u_xlat1.x * u_xlat2.z + u_xlat27;
					    u_xlat1.xyz = (-in_COLOR0.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat1.xyz = vec3(u_xlat27) * u_xlat1.xyz + in_COLOR0.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * u_xlat1.xyz;
					    u_xlat0.x = in_COLOR0.w;
					    vs_COLOR0.w = u_xlat0.x * u_xlat2.w;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" "_VOXELAO_ON" "_SELECTION_ON" }
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
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec3 _PaintTexOffset;
						vec4 _LevelRect;
						vec4 unused_0_9;
						vec3 _WindDir;
						float _SqrtWindTime;
						vec4 unused_0_12[16];
						vec3 _SunDir;
						vec4 unused_0_14[4];
						vec4 _SideSunColor;
						vec4 unused_0_16[2];
						float _Year;
						vec4 unused_0_18[2];
						vec4 _FoamColor;
						vec4 unused_0_20[4];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_24[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_31;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_34[3];
						vec4 _FoamTex_ST;
						vec4 unused_0_36[2];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					uniform  sampler2D _AoTex;
					uniform  sampler2D _GrassTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					in  vec2 in_TEXCOORD3;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
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
					vec3 u_xlat9;
					vec3 u_xlat10;
					vec3 u_xlat11;
					float u_xlat18;
					bool u_xlatb18;
					float u_xlat22;
					float u_xlat27;
					bool u_xlatb27;
					float u_xlat28;
					float u_xlat29;
					float u_xlat30;
					void main()
					{
					    vs_TEXCOORD0.y = in_TEXCOORD0.x;
					    u_xlat0.x = _SqrtWindTime * 0.0500000007;
					    u_xlat9.x = (-in_TEXCOORD0.y) + _Time.y;
					    u_xlatb18 = 40.0>=u_xlat9.x;
					    u_xlatb27 = u_xlat9.x>=0.0;
					    u_xlatb18 = u_xlatb27 && u_xlatb18;
					    u_xlat27 = u_xlat9.x * 0.0250000004;
					    vs_TEXCOORD0.x = (-u_xlat9.x) * 0.0250000004 + 1.0;
					    u_xlat27 = u_xlat27;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat9.x = u_xlat27 * 0.200000003 + in_TEXCOORD1.x;
					    u_xlat9.xz = in_TEXCOORD3.xy * u_xlat9.xx + in_TEXCOORD2.xy;
					    u_xlat1.xz = (bool(u_xlatb18)) ? u_xlat9.xz : in_TEXCOORD2.xy;
					    u_xlat9.xy = u_xlat1.xz * _FoamTex_ST.xy + _FoamTex_ST.zw;
					    vs_TEXCOORD2.xy = (-u_xlat0.xx) * _WindDir.xz + u_xlat9.xy;
					    u_xlat0 = vec4(_WaterLevel) * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat1.xz;
					    u_xlat0.y = _WaterLevel;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(0.0, 1.39999998, 0.0);
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat27 = (-u_xlat2.w) * 0.5 + _WaterLevel;
					    u_xlat27 = u_xlat27 * 0.25;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat28 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat29 = u_xlat2.y * u_xlat28;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat29;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat29;
					    u_xlat6 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat11.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat6.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat8.yyy;
					    u_xlat6.xyz = u_xlat8.zzz * u_xlat6.xyz;
					    u_xlat7.xyz = u_xlat7.xyz * u_xlat8.xxx;
					    u_xlat7.xyz = u_xlat8.yyy * u_xlat7.xyz;
					    u_xlat6.xyz = u_xlat7.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat28 = u_xlat28 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat28;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat28;
					    u_xlat11.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_AoTex, u_xlat4.xz, 0.0);
					    u_xlat5.xyz = u_xlat0.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat0.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat8.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat0.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat8.zzz + u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat5.xyz * u_xlat8.zzz + u_xlat3.xyz;
					    u_xlat5 = textureLod(_AoTex, u_xlat11.yx, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat11.zx, 0.0);
					    u_xlat4.w = u_xlat11.x;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat8.yyy * u_xlat2.xyz;
					    u_xlat5.xyz = u_xlat8.xxx * u_xlat5.xyz;
					    u_xlat5.xyz = u_xlat8.yyy * u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat5.xyz * u_xlat0.zzz + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat0.zzz + u_xlat3.xyz;
					    u_xlat3 = textureLod(_AoTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_AoTex, u_xlat4.xw, 0.0);
					    u_xlat4.xyz = u_xlat0.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat0.yyy * u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat8.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat0.yyy * u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat4.xyz * u_xlat0.zzz + u_xlat2.xyz;
					    u_xlat2.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat2.xy = abs(_SunDir.yx);
					    u_xlat3.xyz = u_xlat2.zxw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat28 = u_xlat2.x + u_xlat2.y;
					    u_xlat0.xy = u_xlat0.xy * u_xlat3.xy;
					    u_xlat0.x = u_xlat0.y + u_xlat0.x;
					    u_xlat9.x = u_xlat0.y / u_xlat28;
					    u_xlat0.x = u_xlat3.z * u_xlat0.z + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.600000024 + 0.400000006;
					    u_xlat18 = u_xlat27;
					    u_xlat18 = clamp(u_xlat18, 0.0, 1.0);
					    u_xlat27 = (-u_xlat27);
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat28 = max(u_xlat27, u_xlat18);
					    u_xlat28 = (-u_xlat28) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = vec3(u_xlat18) * u_xlat2.yzx;
					    u_xlat3.xyz = u_xlat2.xyz * vec3(u_xlat28) + u_xlat3.xyz;
					    u_xlat2.xyz = vec3(u_xlat27) * u_xlat2.zxy + u_xlat3.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * _MinAmbientColor.xyz;
					    u_xlat2.xyz = (-_MinAmbientColor.xyz) * u_xlat2.xyz + _MaxAmbientColor.xyz;
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat0.xyz = u_xlat2.xyz * u_xlat9.xxx + u_xlat0.xzw;
					    u_xlat27 = _FlashDir.y;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat27 = (-u_xlat27) + 1.0;
					    u_xlat28 = u_xlat27 * u_xlat27;
					    u_xlat27 = (-u_xlat27) * u_xlat28 + 1.0;
					    u_xlat2.xyz = vec3(u_xlat27) * _FlashColor.xyz;
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat0.xyz;
					    u_xlat0.w = 1.0;
					    u_xlat2 = u_xlat0 * _FoamColor;
					    u_xlat27 = dot(u_xlat2.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = _FoamColor.xyz * u_xlat0.xyz + (-vec3(u_xlat27));
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat27);
					    u_xlat2.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat2.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + u_xlat2.xyz;
					    u_xlat27 = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat27 = sqrt(u_xlat27);
					    u_xlat27 = u_xlat27 + (-_FogMinRad);
					    u_xlat28 = _FogMaxRad + (-_FogMinRad);
					    u_xlat27 = u_xlat27 / u_xlat28;
					    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat27) * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat1.y = _WaterLevel;
					    u_xlat1.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat2.xy = max(u_xlat1.xz, _LevelRect.xy);
					    u_xlat1.xz = min(u_xlat2.xy, _LevelRect.zw);
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat2.xyz);
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat3.xyz = (-u_xlat2.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat4.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat27 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat28 = u_xlat1.y * u_xlat27;
					    u_xlat6.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat28;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat28;
					    u_xlat7 = textureLod(_PaintTex, u_xlat5.xz, 0.0);
					    u_xlat10.yz = u_xlat5.yx;
					    u_xlat8 = textureLod(_PaintTex, u_xlat5.yz, 0.0);
					    u_xlat30 = u_xlat3.x * u_xlat8.y;
					    u_xlat30 = u_xlat3.y * u_xlat30;
					    u_xlat22 = u_xlat2.x * u_xlat7.y;
					    u_xlat22 = u_xlat3.y * u_xlat22;
					    u_xlat22 = u_xlat3.z * u_xlat22;
					    u_xlat30 = u_xlat30 * u_xlat3.z + u_xlat22;
					    u_xlat27 = u_xlat27 * u_xlat6.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat27;
					    u_xlat5.x = u_xlat4.x * u_xlat6.x + u_xlat27;
					    u_xlat10.x = u_xlat4.y * u_xlat6.z;
					    u_xlat4 = textureLod(_PaintTex, u_xlat5.yz, 0.0);
					    u_xlat6 = textureLod(_PaintTex, u_xlat5.xz, 0.0);
					    u_xlat27 = u_xlat2.x * u_xlat6.y;
					    u_xlat27 = u_xlat2.y * u_xlat27;
					    u_xlat1.x = u_xlat3.x * u_xlat4.y;
					    u_xlat1.x = u_xlat2.y * u_xlat1.x;
					    u_xlat1.x = u_xlat1.x * u_xlat3.z + u_xlat30;
					    u_xlat27 = u_xlat27 * u_xlat3.z + u_xlat1.x;
					    u_xlat4 = textureLod(_PaintTex, u_xlat10.yx, 0.0);
					    u_xlat6 = textureLod(_PaintTex, u_xlat10.zx, 0.0);
					    u_xlat5.w = u_xlat10.x;
					    u_xlat1.x = u_xlat2.x * u_xlat6.y;
					    u_xlat1.y = u_xlat3.x * u_xlat4.y;
					    u_xlat1.xy = u_xlat3.yy * u_xlat1.xy;
					    u_xlat27 = u_xlat1.y * u_xlat2.z + u_xlat27;
					    u_xlat27 = u_xlat1.x * u_xlat2.z + u_xlat27;
					    u_xlat1 = textureLod(_PaintTex, u_xlat5.yw, 0.0);
					    u_xlat4 = textureLod(_PaintTex, u_xlat5.xw, 0.0);
					    u_xlat1.x = u_xlat2.x * u_xlat4.y;
					    u_xlat1.y = u_xlat3.x * u_xlat1.y;
					    u_xlat1.xy = u_xlat2.yy * u_xlat1.xy;
					    u_xlat27 = u_xlat1.y * u_xlat2.z + u_xlat27;
					    u_xlat27 = u_xlat1.x * u_xlat2.z + u_xlat27;
					    u_xlat1.xyz = (-in_COLOR0.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat1.xyz = vec3(u_xlat27) * u_xlat1.xyz + in_COLOR0.xyz;
					    vs_COLOR0.xyz = u_xlat0.xyz * u_xlat1.xyz;
					    u_xlat0.x = in_COLOR0.w;
					    vs_COLOR0.w = u_xlat0.x * u_xlat2.w;
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
					uniform  sampler2D _FoamTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					bool u_xlatb2;
					float u_xlat4;
					void main()
					{
					    u_xlat0.xy = abs(vs_TEXCOORD0.xy);
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat1 = texture(_FoamTex, vs_TEXCOORD2.xy);
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-1.10000002, -1.10000002, -1.10000002);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat0.x = max(u_xlat0.z, u_xlat0.x);
					    u_xlat2 = u_xlat0.x * vs_COLOR0.w;
					    u_xlat0.x = vs_COLOR0.w * u_xlat0.x + -0.5;
					    u_xlat4 = dFdx(u_xlat2);
					    u_xlat2 = dFdy(u_xlat2);
					    u_xlat2 = abs(u_xlat2) + abs(u_xlat4);
					    u_xlat2 = max(u_xlat2, 0.00100000005);
					    u_xlat2 = min(u_xlat2, 1.0);
					    u_xlat0.x = u_xlat0.x / u_xlat2;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb2 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb2) * int(0xffffffffu)))!=0){discard;}
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
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					uniform  sampler2D _FoamTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					bool u_xlatb2;
					float u_xlat4;
					void main()
					{
					    u_xlat0.xy = abs(vs_TEXCOORD0.xy);
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat1 = texture(_FoamTex, vs_TEXCOORD2.xy);
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-1.10000002, -1.10000002, -1.10000002);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat0.x = max(u_xlat0.z, u_xlat0.x);
					    u_xlat2 = u_xlat0.x * vs_COLOR0.w;
					    u_xlat0.x = vs_COLOR0.w * u_xlat0.x + -0.5;
					    u_xlat4 = dFdx(u_xlat2);
					    u_xlat2 = dFdy(u_xlat2);
					    u_xlat2 = abs(u_xlat2) + abs(u_xlat4);
					    u_xlat2 = max(u_xlat2, 0.00100000005);
					    u_xlat2 = min(u_xlat2, 1.0);
					    u_xlat0.x = u_xlat0.x / u_xlat2;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb2 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb2) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" }
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
					uniform  sampler2D _FoamTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					bool u_xlatb2;
					float u_xlat4;
					void main()
					{
					    u_xlat0.xy = abs(vs_TEXCOORD0.xy);
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat1 = texture(_FoamTex, vs_TEXCOORD2.xy);
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-1.10000002, -1.10000002, -1.10000002);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat0.x = max(u_xlat0.z, u_xlat0.x);
					    u_xlat2 = u_xlat0.x * vs_COLOR0.w;
					    u_xlat0.x = vs_COLOR0.w * u_xlat0.x + -0.5;
					    u_xlat4 = dFdx(u_xlat2);
					    u_xlat2 = dFdy(u_xlat2);
					    u_xlat2 = abs(u_xlat2) + abs(u_xlat4);
					    u_xlat2 = max(u_xlat2, 0.00100000005);
					    u_xlat2 = min(u_xlat2, 1.0);
					    u_xlat0.x = u_xlat0.x / u_xlat2;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb2 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb2) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" "_SELECTION_ON" }
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
					uniform  sampler2D _FoamTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					bool u_xlatb2;
					float u_xlat4;
					void main()
					{
					    u_xlat0.xy = abs(vs_TEXCOORD0.xy);
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat1 = texture(_FoamTex, vs_TEXCOORD2.xy);
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-1.10000002, -1.10000002, -1.10000002);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat0.x = max(u_xlat0.z, u_xlat0.x);
					    u_xlat2 = u_xlat0.x * vs_COLOR0.w;
					    u_xlat0.x = vs_COLOR0.w * u_xlat0.x + -0.5;
					    u_xlat4 = dFdx(u_xlat2);
					    u_xlat2 = dFdy(u_xlat2);
					    u_xlat2 = abs(u_xlat2) + abs(u_xlat4);
					    u_xlat2 = max(u_xlat2, 0.00100000005);
					    u_xlat2 = min(u_xlat2, 1.0);
					    u_xlat0.x = u_xlat0.x / u_xlat2;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb2 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb2) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_VOXELAO_ON" }
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
					uniform  sampler2D _FoamTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					bool u_xlatb2;
					float u_xlat4;
					void main()
					{
					    u_xlat0.xy = abs(vs_TEXCOORD0.xy);
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat1 = texture(_FoamTex, vs_TEXCOORD2.xy);
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-1.10000002, -1.10000002, -1.10000002);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat0.x = max(u_xlat0.z, u_xlat0.x);
					    u_xlat2 = u_xlat0.x * vs_COLOR0.w;
					    u_xlat0.x = vs_COLOR0.w * u_xlat0.x + -0.5;
					    u_xlat4 = dFdx(u_xlat2);
					    u_xlat2 = dFdy(u_xlat2);
					    u_xlat2 = abs(u_xlat2) + abs(u_xlat4);
					    u_xlat2 = max(u_xlat2, 0.00100000005);
					    u_xlat2 = min(u_xlat2, 1.0);
					    u_xlat0.x = u_xlat0.x / u_xlat2;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb2 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb2) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_VOXELAO_ON" "_SELECTION_ON" }
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
					uniform  sampler2D _FoamTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					bool u_xlatb2;
					float u_xlat4;
					void main()
					{
					    u_xlat0.xy = abs(vs_TEXCOORD0.xy);
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat1 = texture(_FoamTex, vs_TEXCOORD2.xy);
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-1.10000002, -1.10000002, -1.10000002);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat0.x = max(u_xlat0.z, u_xlat0.x);
					    u_xlat2 = u_xlat0.x * vs_COLOR0.w;
					    u_xlat0.x = vs_COLOR0.w * u_xlat0.x + -0.5;
					    u_xlat4 = dFdx(u_xlat2);
					    u_xlat2 = dFdy(u_xlat2);
					    u_xlat2 = abs(u_xlat2) + abs(u_xlat4);
					    u_xlat2 = max(u_xlat2, 0.00100000005);
					    u_xlat2 = min(u_xlat2, 1.0);
					    u_xlat0.x = u_xlat0.x / u_xlat2;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb2 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb2) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" "_VOXELAO_ON" }
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
					uniform  sampler2D _FoamTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					bool u_xlatb2;
					float u_xlat4;
					void main()
					{
					    u_xlat0.xy = abs(vs_TEXCOORD0.xy);
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat1 = texture(_FoamTex, vs_TEXCOORD2.xy);
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-1.10000002, -1.10000002, -1.10000002);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat0.x = max(u_xlat0.z, u_xlat0.x);
					    u_xlat2 = u_xlat0.x * vs_COLOR0.w;
					    u_xlat0.x = vs_COLOR0.w * u_xlat0.x + -0.5;
					    u_xlat4 = dFdx(u_xlat2);
					    u_xlat2 = dFdy(u_xlat2);
					    u_xlat2 = abs(u_xlat2) + abs(u_xlat4);
					    u_xlat2 = max(u_xlat2, 0.00100000005);
					    u_xlat2 = min(u_xlat2, 1.0);
					    u_xlat0.x = u_xlat0.x / u_xlat2;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb2 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb2) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" "_VOXELAO_ON" "_SELECTION_ON" }
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
					uniform  sampler2D _FoamTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					bool u_xlatb2;
					float u_xlat4;
					void main()
					{
					    u_xlat0.xy = abs(vs_TEXCOORD0.xy);
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat1 = texture(_FoamTex, vs_TEXCOORD2.xy);
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-1.10000002, -1.10000002, -1.10000002);
					    u_xlat0.x = max(u_xlat0.y, u_xlat0.x);
					    u_xlat0.x = max(u_xlat0.z, u_xlat0.x);
					    u_xlat2 = u_xlat0.x * vs_COLOR0.w;
					    u_xlat0.x = vs_COLOR0.w * u_xlat0.x + -0.5;
					    u_xlat4 = dFdx(u_xlat2);
					    u_xlat2 = dFdy(u_xlat2);
					    u_xlat2 = abs(u_xlat2) + abs(u_xlat4);
					    u_xlat2 = max(u_xlat2, 0.00100000005);
					    u_xlat2 = min(u_xlat2, 1.0);
					    u_xlat0.x = u_xlat0.x / u_xlat2;
					    u_xlat0.xy = u_xlat0.xx + vec2(0.5, 0.49000001);
					    u_xlatb2 = u_xlat0.y<0.0;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    if(((int(u_xlatb2) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
			}
		}
	}
}