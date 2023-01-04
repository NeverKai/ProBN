Shader "Unlit/ColoredCorpse" {
	Properties {
		_UvTex ("Texture", 2D) = "white" {}
		_PartTex ("PartTex", 2D) = "white" {}
		_PartTexTiling ("PartTexTiling", Vector) = (64,64,1,1)
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
		[Toggle] _Paint ("Paint", Float) = 0
	}
	SubShader {
		LOD 100
		Tags { "RenderType" = "Opaque" }
		Pass {
			LOD 100
			Tags { "RenderType" = "Opaque" }
			GpuProgramID 44115
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
						vec4 unused_0_3[39];
						vec4 _BloodColor;
						vec4 unused_0_5;
						vec4 _LongshipColor;
						vec4 unused_0_7[18];
						vec4 _UvTex_ST;
						vec4 unused_0_9[3];
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
					uniform  sampler2D _HighlightTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					float u_xlat4;
					vec3 u_xlat5;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _UvTex_ST.xy + _UvTex_ST.zw;
					    vs_TEXCOORD2.xy = in_COLOR0.yx;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat4 = u_xlat0.y + 1.5;
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat4 = floor(u_xlat4);
					    u_xlat1.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat4 = u_xlat4 * _AoTexVolume.x + u_xlat1.x;
					    u_xlat1.y = u_xlat1.y / _AoTexSize.y;
					    u_xlat1.x = u_xlat4 / _AoTexSize.x;
					    u_xlat1 = textureLod(_HighlightTex, u_xlat1.xy, 0.0);
					    u_xlat0 = u_xlat0.xxxx * u_xlat1;
					    u_xlatb1 = 0.0<u_xlat0.w;
					    u_xlat5.xyz = u_xlat0.xyz / u_xlat0.www;
					    u_xlat0.xyz = (bool(u_xlatb1)) ? u_xlat5.xyz : u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * in_COLOR0.zzzz;
					    vs_COLOR0 = u_xlat0.wwww * vec4(-0.349999994, -0.349999994, -0.349999994, 0.349999994) + vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat1.x = u_xlat0.w * 0.699999988;
					    u_xlat5.xyz = _LongshipColor.xyz * _BloodColor.xyz + (-_LongshipColor.xyz);
					    u_xlat5.xyz = u_xlat5.xyz * vec3(0.76000005, 0.76000005, 0.76000005) + _LongshipColor.xyz;
					    u_xlat2.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat3.xyz = u_xlat5.xyz * u_xlat2.xxx;
					    u_xlat3.w = u_xlat2.x * _LongshipColor.w;
					    u_xlat2 = u_xlat3 * vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat0 = u_xlat0 * vec4(1.0, 1.0, 1.0, 0.699999988) + (-u_xlat2);
					    vs_COLOR1 = u_xlat1.xxxx * u_xlat0 + u_xlat2;
					    vs_TEXCOORD1 = 0.0;
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
						vec4 unused_0_12;
						vec4 _BloodColor;
						vec4 unused_0_14;
						vec4 _LongshipColor;
						vec4 unused_0_16[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_20[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_25[2];
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_28[2];
						vec4 _UvTex_ST;
						vec4 unused_0_30[3];
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
					uniform  sampler2D _HighlightTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD1;
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
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat19;
					float u_xlat22;
					float u_xlat23;
					float u_xlat33;
					bool u_xlatb33;
					float u_xlat34;
					float u_xlat36;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _UvTex_ST.xy + _UvTex_ST.zw;
					    vs_TEXCOORD2.xy = in_COLOR0.yx;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat33 = u_xlat0.y + 1.5;
					    u_xlat33 = floor(u_xlat33);
					    u_xlat1.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat1.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat2.xy = floor(u_xlat1.xz);
					    u_xlat33 = u_xlat33 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat33 / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat3.xy = fract(u_xlat1.xz);
					    u_xlat3.xy = u_xlat3.xy + vec2(-0.5, -0.5);
					    u_xlat3.xy = -abs(u_xlat3.xy) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat3.xy = u_xlat3.xy * vec2(3.0, 3.0);
					    u_xlat3.xy = min(u_xlat3.xy, vec2(1.0, 1.0));
					    u_xlat33 = u_xlat3.y * u_xlat3.x;
					    u_xlat2 = vec4(u_xlat33) * u_xlat2;
					    u_xlatb33 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb33)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat2 = u_xlat2 * in_COLOR0.zzzz;
					    vs_COLOR0 = u_xlat2.wwww * vec4(-0.349999994, -0.349999994, -0.349999994, 0.349999994) + vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat1.w = u_xlat0.y;
					    u_xlat3.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = floor(u_xlat3.xyz);
					    u_xlat3.xyz = fract(u_xlat3.xyz);
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.z = u_xlat4.z * u_xlat5.y;
					    u_xlat33 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat34 = u_xlat4.y * u_xlat33;
					    u_xlat15.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat15.x + u_xlat34;
					    u_xlat6.y = u_xlat5.x * u_xlat4.x + u_xlat34;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat10.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat7 = u_xlat7 * u_xlat10.yyyy;
					    u_xlat7 = u_xlat10.zzzz * u_xlat7;
					    u_xlat9 = u_xlat9 * u_xlat10.xxxx;
					    u_xlat9 = u_xlat10.yyyy * u_xlat9;
					    u_xlat7 = u_xlat9 * u_xlat10.zzzz + u_xlat7;
					    u_xlat33 = u_xlat33 * u_xlat15.y;
					    u_xlat6.y = u_xlat5.x * u_xlat4.x + u_xlat33;
					    u_xlat6.x = u_xlat5.x * u_xlat15.x + u_xlat33;
					    u_xlat19.x = u_xlat15.z * u_xlat5.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat5 = u_xlat3.xxxx * u_xlat5;
					    u_xlat5 = u_xlat3.yyyy * u_xlat5;
					    u_xlat4 = u_xlat10.xxxx * u_xlat4;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat10.zzzz + u_xlat7;
					    u_xlat4 = u_xlat5 * u_xlat10.zzzz + u_xlat4;
					    u_xlat5 = textureLod(_NormalTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat7 = u_xlat10.yyyy * u_xlat7;
					    u_xlat5 = u_xlat10.xxxx * u_xlat5;
					    u_xlat5 = u_xlat10.yyyy * u_xlat5;
					    u_xlat4 = u_xlat5 * u_xlat3.zzzz + u_xlat4;
					    u_xlat4 = u_xlat7 * u_xlat3.zzzz + u_xlat4;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat6 = u_xlat3.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat5 = u_xlat10.xxxx * u_xlat5;
					    u_xlat5 = u_xlat3.yyyy * u_xlat5;
					    u_xlat4 = u_xlat5 * u_xlat3.zzzz + u_xlat4;
					    u_xlat3 = u_xlat6 * u_xlat3.zzzz + u_xlat4;
					    u_xlat33 = u_xlat3.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat4.x = (-u_xlat3.x) + unity_MatrixV[0].z;
					    u_xlat4.y = (-u_xlat3.y) + unity_MatrixV[1].z;
					    u_xlat4.z = (-u_xlat3.z) + unity_MatrixV[2].z;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat4.xyz);
					    u_xlat11.x = (-u_xlat4.w) * 0.5 + u_xlat0.y;
					    u_xlat0.x = dot(u_xlat0.xz, u_xlat0.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat11.x = u_xlat11.x * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat22 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat34 = u_xlat1.y * u_xlat22;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat1.x + u_xlat34;
					    u_xlat7.z = u_xlat1.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat9.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat34;
					    u_xlat10 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat12.yz = u_xlat7.yx;
					    u_xlat10.xyz = u_xlat4.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.zzz * u_xlat10.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat10.xyz;
					    u_xlat22 = u_xlat22 * u_xlat9.y;
					    u_xlat7.y = u_xlat6.x * u_xlat1.x + u_xlat22;
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat22;
					    u_xlat12.x = u_xlat6.y * u_xlat9.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat6.xyz;
					    u_xlat8 = textureLod(_AoTex, u_xlat12.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat12.zx, 0.0);
					    u_xlat7.w = u_xlat12.x;
					    u_xlat1.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat1.xyz = u_xlat5.yyy * u_xlat1.xyz;
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat16.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat16.xyz = u_xlat16.xyz * u_xlat4.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat4.zzz + u_xlat16.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat16.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat16.xyz = u_xlat4.yyy * u_xlat16.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat16.xyz * u_xlat4.zzz + u_xlat1.xyz;
					    u_xlat4.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xy = abs(_SunDir.yx);
					    u_xlat5.xyz = u_xlat4.zxw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat22 = u_xlat4.x + u_xlat4.y;
					    u_xlat1.xyw = u_xlat1.xyz * u_xlat5.xyz;
					    u_xlat36 = u_xlat1.y + u_xlat1.x;
					    u_xlat23 = u_xlat5.z * u_xlat1.z + u_xlat36;
					    u_xlat33 = u_xlat23 * 0.600000024 + u_xlat33;
					    u_xlat23 = u_xlat11.x;
					    u_xlat23 = clamp(u_xlat23, 0.0, 1.0);
					    u_xlat11.x = (-u_xlat11.x);
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat36 = max(u_xlat11.x, u_xlat23);
					    u_xlat36 = (-u_xlat36) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat23) * u_xlat4.yzx;
					    u_xlat5.xyz = u_xlat4.xyz * vec3(u_xlat36) + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat11.xxx * u_xlat4.zxy + u_xlat5.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + _SnowColor.xyz;
					    u_xlat4.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat4.xyz * _MinAmbientColor.xyz;
					    u_xlat4.xyz = (-_MinAmbientColor.xyz) * u_xlat4.xyz + _MaxAmbientColor.xyz;
					    u_xlat4.xyz = vec3(u_xlat33) * u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat11.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat11.x = inversesqrt(u_xlat11.x);
					    u_xlat3.xyz = u_xlat11.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat5.xyz = u_xlat3.xyw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat11.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat11.x = (-u_xlat11.x) + 1.0;
					    u_xlat1.xy = u_xlat1.xy * u_xlat5.xy;
					    u_xlat33 = u_xlat1.y + u_xlat1.x;
					    u_xlat33 = u_xlat1.w * u_xlat5.z + u_xlat33;
					    u_xlat22 = u_xlat33 / u_xlat22;
					    u_xlat1.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat22) + u_xlat4.xyz;
					    u_xlat22 = u_xlat11.x * u_xlat11.x;
					    u_xlat11.x = (-u_xlat11.x) * u_xlat22 + 1.0;
					    u_xlat11.xyz = u_xlat11.xxx * _FlashColor.xyz;
					    u_xlat11.xyz = u_xlat11.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat1.xyz;
					    u_xlat1.xyz = _LongshipColor.xyz * _BloodColor.xyz + (-_LongshipColor.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.76000005, 0.76000005, 0.76000005) + _LongshipColor.xyz;
					    u_xlat11.xyz = u_xlat11.xyz * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat11.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat1.w = _LongshipColor.w * 0.5;
					    u_xlat3 = u_xlat2 * vec4(1.0, 1.0, 1.0, 0.699999988) + (-u_xlat1);
					    u_xlat11.x = u_xlat2.w * 0.699999988;
					    vs_COLOR1 = u_xlat11.xxxx * u_xlat3 + u_xlat1;
					    u_xlat11.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1 = u_xlat0.x / u_xlat11.x;
					    vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
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
						vec4 unused_0_0[43];
						vec4 _BloodColor;
						vec4 unused_0_2;
						vec4 _LongshipColor;
						vec4 unused_0_4[18];
						vec4 _UvTex_ST;
						vec4 unused_0_6[3];
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
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _UvTex_ST.xy + _UvTex_ST.zw;
					    vs_TEXCOORD2.xy = in_COLOR0.yx;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    vs_COLOR0 = vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat0.xyz = _LongshipColor.xyz * _BloodColor.xyz + (-_LongshipColor.xyz);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.76000005, 0.76000005, 0.76000005) + _LongshipColor.xyz;
					    u_xlat6 = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat1.xyz = vec3(u_xlat6) * u_xlat0.xyz;
					    u_xlat1.w = u_xlat6 * _LongshipColor.w;
					    vs_COLOR1 = u_xlat1 * vec4(0.5, 0.5, 0.5, 0.5);
					    vs_TEXCOORD1 = 0.0;
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
						vec4 unused_0_3[39];
						vec4 _BloodColor;
						vec4 unused_0_5;
						vec4 _LongshipColor;
						vec4 unused_0_7[18];
						vec4 _UvTex_ST;
						vec4 unused_0_9[3];
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
					uniform  sampler2D _HighlightTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					float u_xlat4;
					vec3 u_xlat5;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _UvTex_ST.xy + _UvTex_ST.zw;
					    vs_TEXCOORD2.xy = in_COLOR0.yx;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat4 = u_xlat0.y + 1.5;
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat4 = floor(u_xlat4);
					    u_xlat1.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat4 = u_xlat4 * _AoTexVolume.x + u_xlat1.x;
					    u_xlat1.y = u_xlat1.y / _AoTexSize.y;
					    u_xlat1.x = u_xlat4 / _AoTexSize.x;
					    u_xlat1 = textureLod(_HighlightTex, u_xlat1.xy, 0.0);
					    u_xlat0 = u_xlat0.xxxx * u_xlat1;
					    u_xlatb1 = 0.0<u_xlat0.w;
					    u_xlat5.xyz = u_xlat0.xyz / u_xlat0.www;
					    u_xlat0.xyz = (bool(u_xlatb1)) ? u_xlat5.xyz : u_xlat0.xyz;
					    u_xlat0 = u_xlat0 * in_COLOR0.zzzz;
					    vs_COLOR0 = u_xlat0.wwww * vec4(-0.349999994, -0.349999994, -0.349999994, 0.349999994) + vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat1.x = u_xlat0.w * 0.699999988;
					    u_xlat5.xyz = _LongshipColor.xyz * _BloodColor.xyz + (-_LongshipColor.xyz);
					    u_xlat5.xyz = u_xlat5.xyz * vec3(0.76000005, 0.76000005, 0.76000005) + _LongshipColor.xyz;
					    u_xlat2.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat3.xyz = u_xlat5.xyz * u_xlat2.xxx;
					    u_xlat3.w = u_xlat2.x * _LongshipColor.w;
					    u_xlat2 = u_xlat3 * vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat0 = u_xlat0 * vec4(1.0, 1.0, 1.0, 0.699999988) + (-u_xlat2);
					    vs_COLOR1 = u_xlat1.xxxx * u_xlat0 + u_xlat2;
					    vs_TEXCOORD1 = 0.0;
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
						vec4 unused_0_6[25];
						vec3 _SunDir;
						vec4 unused_0_8[4];
						vec4 _SideSunColor;
						vec4 unused_0_10[2];
						float _Year;
						vec4 unused_0_12;
						vec4 _BloodColor;
						vec4 unused_0_14;
						vec4 _LongshipColor;
						vec4 unused_0_16[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_20[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_25[2];
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_28[2];
						vec4 _UvTex_ST;
						vec4 unused_0_30[3];
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
					uniform  sampler2D _HighlightTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD1;
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
					vec3 u_xlat11;
					vec3 u_xlat12;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat19;
					float u_xlat22;
					float u_xlat23;
					float u_xlat33;
					bool u_xlatb33;
					float u_xlat34;
					float u_xlat36;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _UvTex_ST.xy + _UvTex_ST.zw;
					    vs_TEXCOORD2.xy = in_COLOR0.yx;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat33 = u_xlat0.y + 1.5;
					    u_xlat33 = floor(u_xlat33);
					    u_xlat1.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat1.y = 1.0;
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat2.xy = floor(u_xlat1.xz);
					    u_xlat33 = u_xlat33 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat33 / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat3.xy = fract(u_xlat1.xz);
					    u_xlat3.xy = u_xlat3.xy + vec2(-0.5, -0.5);
					    u_xlat3.xy = -abs(u_xlat3.xy) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat3.xy = u_xlat3.xy * vec2(3.0, 3.0);
					    u_xlat3.xy = min(u_xlat3.xy, vec2(1.0, 1.0));
					    u_xlat33 = u_xlat3.y * u_xlat3.x;
					    u_xlat2 = vec4(u_xlat33) * u_xlat2;
					    u_xlatb33 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb33)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat2 = u_xlat2 * in_COLOR0.zzzz;
					    vs_COLOR0 = u_xlat2.wwww * vec4(-0.349999994, -0.349999994, -0.349999994, 0.349999994) + vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat1.w = u_xlat0.y;
					    u_xlat3.xyz = u_xlat1.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = floor(u_xlat3.xyz);
					    u_xlat3.xyz = fract(u_xlat3.xyz);
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.z = u_xlat4.z * u_xlat5.y;
					    u_xlat33 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat34 = u_xlat4.y * u_xlat33;
					    u_xlat15.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat15.x + u_xlat34;
					    u_xlat6.y = u_xlat5.x * u_xlat4.x + u_xlat34;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat19.yz = u_xlat6.yx;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat10.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat7 = u_xlat7 * u_xlat10.yyyy;
					    u_xlat7 = u_xlat10.zzzz * u_xlat7;
					    u_xlat9 = u_xlat9 * u_xlat10.xxxx;
					    u_xlat9 = u_xlat10.yyyy * u_xlat9;
					    u_xlat7 = u_xlat9 * u_xlat10.zzzz + u_xlat7;
					    u_xlat33 = u_xlat33 * u_xlat15.y;
					    u_xlat6.y = u_xlat5.x * u_xlat4.x + u_xlat33;
					    u_xlat6.x = u_xlat5.x * u_xlat15.x + u_xlat33;
					    u_xlat19.x = u_xlat15.z * u_xlat5.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat5 = u_xlat3.xxxx * u_xlat5;
					    u_xlat5 = u_xlat3.yyyy * u_xlat5;
					    u_xlat4 = u_xlat10.xxxx * u_xlat4;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat10.zzzz + u_xlat7;
					    u_xlat4 = u_xlat5 * u_xlat10.zzzz + u_xlat4;
					    u_xlat5 = textureLod(_NormalTex, u_xlat19.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat19.zx, 0.0);
					    u_xlat6.w = u_xlat19.x;
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat7 = u_xlat10.yyyy * u_xlat7;
					    u_xlat5 = u_xlat10.xxxx * u_xlat5;
					    u_xlat5 = u_xlat10.yyyy * u_xlat5;
					    u_xlat4 = u_xlat5 * u_xlat3.zzzz + u_xlat4;
					    u_xlat4 = u_xlat7 * u_xlat3.zzzz + u_xlat4;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat6 = u_xlat3.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat5 = u_xlat10.xxxx * u_xlat5;
					    u_xlat5 = u_xlat3.yyyy * u_xlat5;
					    u_xlat4 = u_xlat5 * u_xlat3.zzzz + u_xlat4;
					    u_xlat3 = u_xlat6 * u_xlat3.zzzz + u_xlat4;
					    u_xlat33 = u_xlat3.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat4.x = (-u_xlat3.x) + unity_MatrixV[0].z;
					    u_xlat4.y = (-u_xlat3.y) + unity_MatrixV[1].z;
					    u_xlat4.z = (-u_xlat3.z) + unity_MatrixV[2].z;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat1.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat1.xyz = min(u_xlat1.xyz, u_xlat4.xyz);
					    u_xlat11.x = (-u_xlat4.w) * 0.5 + u_xlat0.y;
					    u_xlat0.x = dot(u_xlat0.xz, u_xlat0.xz);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat11.x = u_xlat11.x * 0.25;
					    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat1.xyz);
					    u_xlat1.xyz = floor(u_xlat1.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat22 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat34 = u_xlat1.y * u_xlat22;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat1.x + u_xlat34;
					    u_xlat7.z = u_xlat1.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat9.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat34;
					    u_xlat10 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat12.yz = u_xlat7.yx;
					    u_xlat10.xyz = u_xlat4.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.zzz * u_xlat10.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat10.xyz;
					    u_xlat22 = u_xlat22 * u_xlat9.y;
					    u_xlat7.y = u_xlat6.x * u_xlat1.x + u_xlat22;
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat22;
					    u_xlat12.x = u_xlat6.y * u_xlat9.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat6.xyz;
					    u_xlat8 = textureLod(_AoTex, u_xlat12.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat12.zx, 0.0);
					    u_xlat7.w = u_xlat12.x;
					    u_xlat1.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat1.xyz = u_xlat5.yyy * u_xlat1.xyz;
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat16.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat16.xyz = u_xlat16.xyz * u_xlat4.zzz + u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat4.zzz + u_xlat16.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat16.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat16.xyz = u_xlat4.yyy * u_xlat16.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat1.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat16.xyz * u_xlat4.zzz + u_xlat1.xyz;
					    u_xlat4.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xy = abs(_SunDir.yx);
					    u_xlat5.xyz = u_xlat4.zxw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat22 = u_xlat4.x + u_xlat4.y;
					    u_xlat1.xyw = u_xlat1.xyz * u_xlat5.xyz;
					    u_xlat36 = u_xlat1.y + u_xlat1.x;
					    u_xlat23 = u_xlat5.z * u_xlat1.z + u_xlat36;
					    u_xlat33 = u_xlat23 * 0.600000024 + u_xlat33;
					    u_xlat23 = u_xlat11.x;
					    u_xlat23 = clamp(u_xlat23, 0.0, 1.0);
					    u_xlat11.x = (-u_xlat11.x);
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat36 = max(u_xlat11.x, u_xlat23);
					    u_xlat36 = (-u_xlat36) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat23) * u_xlat4.yzx;
					    u_xlat5.xyz = u_xlat4.xyz * vec3(u_xlat36) + u_xlat5.xyz;
					    u_xlat4.xyz = u_xlat11.xxx * u_xlat4.zxy + u_xlat5.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + _SnowColor.xyz;
					    u_xlat4.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat4.xyz * _MinAmbientColor.xyz;
					    u_xlat4.xyz = (-_MinAmbientColor.xyz) * u_xlat4.xyz + _MaxAmbientColor.xyz;
					    u_xlat4.xyz = vec3(u_xlat33) * u_xlat4.xyz + u_xlat5.xyz;
					    u_xlat11.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat11.x = inversesqrt(u_xlat11.x);
					    u_xlat3.xyz = u_xlat11.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat5.xyz = u_xlat3.xyw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat11.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat11.x = (-u_xlat11.x) + 1.0;
					    u_xlat1.xy = u_xlat1.xy * u_xlat5.xy;
					    u_xlat33 = u_xlat1.y + u_xlat1.x;
					    u_xlat33 = u_xlat1.w * u_xlat5.z + u_xlat33;
					    u_xlat22 = u_xlat33 / u_xlat22;
					    u_xlat1.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat22) + u_xlat4.xyz;
					    u_xlat22 = u_xlat11.x * u_xlat11.x;
					    u_xlat11.x = (-u_xlat11.x) * u_xlat22 + 1.0;
					    u_xlat11.xyz = u_xlat11.xxx * _FlashColor.xyz;
					    u_xlat11.xyz = u_xlat11.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat1.xyz;
					    u_xlat1.xyz = _LongshipColor.xyz * _BloodColor.xyz + (-_LongshipColor.xyz);
					    u_xlat1.xyz = u_xlat1.xyz * vec3(0.76000005, 0.76000005, 0.76000005) + _LongshipColor.xyz;
					    u_xlat11.xyz = u_xlat11.xyz * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat11.xyz * vec3(0.5, 0.5, 0.5);
					    u_xlat1.w = _LongshipColor.w * 0.5;
					    u_xlat3 = u_xlat2 * vec4(1.0, 1.0, 1.0, 0.699999988) + (-u_xlat1);
					    u_xlat11.x = u_xlat2.w * 0.699999988;
					    vs_COLOR1 = u_xlat11.xxxx * u_xlat3 + u_xlat1;
					    u_xlat11.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1 = u_xlat0.x / u_xlat11.x;
					    vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[43];
						vec4 _BloodColor;
						vec4 unused_0_2;
						vec4 _LongshipColor;
						vec4 unused_0_4[18];
						vec4 _UvTex_ST;
						vec4 unused_0_6[3];
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
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _UvTex_ST.xy + _UvTex_ST.zw;
					    vs_TEXCOORD2.xy = in_COLOR0.yx;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    vs_COLOR0 = vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat0.xyz = _LongshipColor.xyz * _BloodColor.xyz + (-_LongshipColor.xyz);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.76000005, 0.76000005, 0.76000005) + _LongshipColor.xyz;
					    u_xlat6 = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat1.xyz = vec3(u_xlat6) * u_xlat0.xyz;
					    u_xlat1.w = u_xlat6 * _LongshipColor.w;
					    vs_COLOR1 = u_xlat1 * vec4(0.5, 0.5, 0.5, 0.5);
					    vs_TEXCOORD1 = 0.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_ISLAND_ON" }
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
						vec4 unused_0_3[37];
						float _Year;
						vec4 unused_0_5[7];
						vec4 _SnowColor;
						vec4 unused_0_7;
						float _SnowAmount;
						vec4 unused_0_9[12];
						vec4 _UvTex_ST;
						vec4 unused_0_11[3];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD1;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat8;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _UvTex_ST.xy + _UvTex_ST.zw;
					    vs_TEXCOORD2.xy = in_COLOR0.yx;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat0.xzw = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xzw = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xzw;
					    u_xlat0.xzw = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xzw;
					    u_xlat0.xzw = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xzw;
					    u_xlat8 = u_xlat0.z + 1.5;
					    u_xlat0.xw = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xw;
					    u_xlat8 = floor(u_xlat8);
					    u_xlat1.xy = floor(u_xlat0.xw);
					    u_xlat0.xw = fract(u_xlat0.xw);
					    u_xlat0.xw = u_xlat0.xw + vec2(-0.5, -0.5);
					    u_xlat0.xw = -abs(u_xlat0.xw) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xw = u_xlat0.xw * vec2(3.0, 3.0);
					    u_xlat0.xw = min(u_xlat0.xw, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.w * u_xlat0.x;
					    u_xlat8 = u_xlat8 * _AoTexVolume.x + u_xlat1.x;
					    u_xlat1.y = u_xlat1.y / _AoTexSize.y;
					    u_xlat1.x = u_xlat8 / _AoTexSize.x;
					    u_xlat1 = textureLod(_HighlightTex, u_xlat1.xy, 0.0);
					    u_xlat1 = u_xlat0.xxxx * u_xlat1;
					    u_xlatb0 = 0.0<u_xlat1.w;
					    u_xlat2.xyz = u_xlat1.xyz / u_xlat1.www;
					    u_xlat1.xyz = (bool(u_xlatb0)) ? u_xlat2.xyz : u_xlat1.xyz;
					    u_xlat1 = u_xlat1 * in_COLOR0.zzzz;
					    vs_COLOR0 = u_xlat1.wwww * vec4(-0.349999994, -0.349999994, -0.349999994, 0.349999994) + vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat0.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat0.x) * 0.5 + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat4.x = u_xlat0.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8 = max(u_xlat0.x, u_xlat4.x);
					    u_xlat8 = (-u_xlat8) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat4.xxx * u_xlat2.yzx;
					    u_xlat4.xyz = u_xlat2.xyz * vec3(u_xlat8) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat4.xyz;
					    u_xlat0 = (-u_xlat2) + _SnowColor;
					    u_xlat0 = vec4(_SnowAmount) * u_xlat0 + u_xlat2;
					    u_xlat2.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat2.xxxx;
					    u_xlat0 = u_xlat0 * vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat2 = u_xlat1 * vec4(1.0, 1.0, 1.0, 0.699999988) + (-u_xlat0);
					    u_xlat1.x = u_xlat1.w * 0.699999988;
					    vs_COLOR1 = u_xlat1.xxxx * u_xlat2 + u_xlat0;
					    vs_TEXCOORD1 = 0.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_ISLAND_ON" }
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
						vec4 unused_0_6[2];
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec3 _PaintTexOffset;
						vec4 _LevelRect;
						vec4 unused_0_11[19];
						vec3 _SunDir;
						vec4 unused_0_13[4];
						vec4 _SideSunColor;
						vec4 unused_0_15[2];
						float _Year;
						vec4 unused_0_17;
						vec4 _BloodColor;
						vec4 unused_0_19[5];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_23[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_28[2];
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_31[2];
						vec4 _UvTex_ST;
						vec4 unused_0_33[3];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					bool u_xlatb3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec4 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat17;
					vec3 u_xlat20;
					vec3 u_xlat21;
					vec3 u_xlat23;
					float u_xlat26;
					vec2 u_xlat29;
					vec2 u_xlat30;
					float u_xlat38;
					float u_xlat39;
					float u_xlat40;
					float u_xlat41;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _UvTex_ST.xy + _UvTex_ST.zw;
					    vs_TEXCOORD2.xy = in_COLOR0.yx;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat0.xzw = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xzw = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xzw;
					    u_xlat0.xzw = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xzw;
					    u_xlat0.xzw = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xzw;
					    u_xlat1.x = u_xlat0.z + 1.5;
					    u_xlat1.x = floor(u_xlat1.x);
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat2.y = 1.0;
					    u_xlat2.xyz = u_xlat0.xzw + u_xlat2.xyz;
					    u_xlat1.yz = floor(u_xlat2.xz);
					    u_xlat1.x = u_xlat1.x * _AoTexVolume.x + u_xlat1.y;
					    u_xlat3.xy = u_xlat1.xz / _AoTexSize.xy;
					    u_xlat1 = textureLod(_HighlightTex, u_xlat3.xy, 0.0);
					    u_xlat3.xy = fract(u_xlat2.xz);
					    u_xlat3.xy = u_xlat3.xy + vec2(-0.5, -0.5);
					    u_xlat3.xy = -abs(u_xlat3.xy) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat3.xy = u_xlat3.xy * vec2(3.0, 3.0);
					    u_xlat3.xy = min(u_xlat3.xy, vec2(1.0, 1.0));
					    u_xlat3.x = u_xlat3.y * u_xlat3.x;
					    u_xlat1 = u_xlat1 * u_xlat3.xxxx;
					    u_xlatb3 = 0.0<u_xlat1.w;
					    u_xlat15.xyz = u_xlat1.xyz / u_xlat1.www;
					    u_xlat1.xyz = (bool(u_xlatb3)) ? u_xlat15.xyz : u_xlat1.xyz;
					    u_xlat1 = u_xlat1 * in_COLOR0.zzzz;
					    vs_COLOR0 = u_xlat1.wwww * vec4(-0.349999994, -0.349999994, -0.349999994, 0.349999994) + vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat2.w = u_xlat0.z;
					    u_xlat3.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = floor(u_xlat3.xyz);
					    u_xlat3.xyz = fract(u_xlat3.xyz);
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.z = u_xlat4.z * u_xlat5.y;
					    u_xlat38 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat39 = u_xlat4.y * u_xlat38;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat16.x + u_xlat39;
					    u_xlat6.y = u_xlat5.x * u_xlat4.x + u_xlat39;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat20.yz = u_xlat6.yx;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat10.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat7 = u_xlat7 * u_xlat10.yyyy;
					    u_xlat7 = u_xlat10.zzzz * u_xlat7;
					    u_xlat9 = u_xlat9 * u_xlat10.xxxx;
					    u_xlat9 = u_xlat10.yyyy * u_xlat9;
					    u_xlat7 = u_xlat9 * u_xlat10.zzzz + u_xlat7;
					    u_xlat38 = u_xlat38 * u_xlat16.y;
					    u_xlat6.y = u_xlat5.x * u_xlat4.x + u_xlat38;
					    u_xlat6.x = u_xlat5.x * u_xlat16.x + u_xlat38;
					    u_xlat20.x = u_xlat16.z * u_xlat5.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat5 = u_xlat3.xxxx * u_xlat5;
					    u_xlat5 = u_xlat3.yyyy * u_xlat5;
					    u_xlat4 = u_xlat10.xxxx * u_xlat4;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat10.zzzz + u_xlat7;
					    u_xlat4 = u_xlat5 * u_xlat10.zzzz + u_xlat4;
					    u_xlat5 = textureLod(_NormalTex, u_xlat20.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat20.zx, 0.0);
					    u_xlat6.w = u_xlat20.x;
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat7 = u_xlat10.yyyy * u_xlat7;
					    u_xlat5 = u_xlat10.xxxx * u_xlat5;
					    u_xlat5 = u_xlat10.yyyy * u_xlat5;
					    u_xlat4 = u_xlat5 * u_xlat3.zzzz + u_xlat4;
					    u_xlat4 = u_xlat7 * u_xlat3.zzzz + u_xlat4;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat6 = u_xlat3.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat5 = u_xlat10.xxxx * u_xlat5;
					    u_xlat5 = u_xlat3.yyyy * u_xlat5;
					    u_xlat4 = u_xlat5 * u_xlat3.zzzz + u_xlat4;
					    u_xlat3 = u_xlat6 * u_xlat3.zzzz + u_xlat4;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat38 = u_xlat3.w * 0.400000006;
					    u_xlat4.x = (-u_xlat3.x) + unity_MatrixV[0].z;
					    u_xlat4.y = (-u_xlat3.y) + unity_MatrixV[1].z;
					    u_xlat4.z = (-u_xlat3.z) + unity_MatrixV[2].z;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat4.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat39 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat41 = u_xlat2.y * u_xlat39;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat41;
					    u_xlat7.z = u_xlat2.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat9.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat41;
					    u_xlat10 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat23.yz = u_xlat7.yx;
					    u_xlat10.xyz = u_xlat4.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.zzz * u_xlat10.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat10.xyz;
					    u_xlat14.x = u_xlat39 * u_xlat9.y;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat14.x;
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat14.x;
					    u_xlat23.x = u_xlat6.y * u_xlat9.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat2.xyz = u_xlat4.yyy * u_xlat2.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat5.zzz + u_xlat6.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat23.yx, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat23.zx, 0.0);
					    u_xlat7.w = u_xlat23.x;
					    u_xlat8.xyz = u_xlat4.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat8.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat6.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat5.xy = abs(_SunDir.yx);
					    u_xlat4.xyz = u_xlat5.zxw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat39 = u_xlat5.x + u_xlat5.y;
					    u_xlat5.xyz = u_xlat2.xyz * u_xlat4.xyz;
					    u_xlat2.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat6.w = (-u_xlat6.x);
					    u_xlat3.xyz = u_xlat6.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = dot(u_xlat6.xyz, _FlashDir.xyz);
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.xy = u_xlat3.xy * u_xlat5.xy;
					    u_xlat14.x = u_xlat3.y + u_xlat3.x;
					    u_xlat14.x = u_xlat5.z * u_xlat3.z + u_xlat14.x;
					    u_xlat3.x = u_xlat5.y + u_xlat5.x;
					    u_xlat26 = u_xlat4.z * u_xlat2.z + u_xlat3.x;
					    u_xlat26 = u_xlat26 * 0.600000024 + u_xlat38;
					    u_xlat14.x = u_xlat14.x / u_xlat39;
					    u_xlat38 = (-u_xlat4.w) * 0.5 + u_xlat0.z;
					    u_xlat12.x = (-u_xlat4.w) * 0.5 + u_xlat0.y;
					    u_xlat12.x = u_xlat12.x * 0.25;
					    u_xlat38 = u_xlat38 * 0.25;
					    u_xlat3.x = (-u_xlat38);
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat38 = u_xlat38;
					    u_xlat38 = clamp(u_xlat38, 0.0, 1.0);
					    u_xlat15.x = max(u_xlat3.x, u_xlat38);
					    u_xlat15.x = (-u_xlat15.x) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat38) * u_xlat4.yzx;
					    u_xlat15.xyz = u_xlat4.xyz * u_xlat15.xxx + u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat4.zxy + u_xlat15.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + _SnowColor.xyz;
					    u_xlat3.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat5.xyz = u_xlat3.xyz * _MinAmbientColor.xyz;
					    u_xlat3.xyz = (-_MinAmbientColor.xyz) * u_xlat3.xyz + _MaxAmbientColor.xyz;
					    u_xlat3.xyz = vec3(u_xlat26) * u_xlat3.xyz + u_xlat5.xyz;
					    u_xlat5.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat14.xyz = u_xlat5.xyz * u_xlat14.xxx + u_xlat3.xyz;
					    u_xlat3.x = u_xlat2.x * u_xlat2.x;
					    u_xlat2.x = (-u_xlat2.x) * u_xlat3.x + 1.0;
					    u_xlat3.xyz = u_xlat2.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat14.xyz;
					    u_xlat38 = u_xlat12.x;
					    u_xlat38 = clamp(u_xlat38, 0.0, 1.0);
					    u_xlat12.x = (-u_xlat12.x);
					    u_xlat12.x = clamp(u_xlat12.x, 0.0, 1.0);
					    u_xlat3.x = max(u_xlat12.x, u_xlat38);
					    u_xlat15.xyz = u_xlat4.yzx * vec3(u_xlat38);
					    u_xlat38 = (-u_xlat3.x) + 1.0;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(u_xlat38) + u_xlat15.xyz;
					    u_xlat4.xyz = u_xlat12.xxx * u_xlat4.zxy + u_xlat3.xyz;
					    u_xlat3 = (-u_xlat4) + _SnowColor;
					    u_xlat3 = vec4(_SnowAmount) * u_xlat3 + u_xlat4;
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat5.xyz = u_xlat0.xzw + _PaintTexOffset.xyz;
					    u_xlat0.x = dot(u_xlat0.xw, u_xlat0.xw);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat12.xy = max(u_xlat5.xz, _LevelRect.xy);
					    u_xlat5.xz = min(u_xlat12.xy, _LevelRect.zw);
					    u_xlat12.xyz = max(u_xlat5.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat12.xyz = min(u_xlat12.xyz, u_xlat5.xyz);
					    u_xlat12.xyz = u_xlat12.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = floor(u_xlat12.xyz);
					    u_xlat12.xyz = fract(u_xlat12.xyz);
					    u_xlat6.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat7.z = u_xlat5.z * u_xlat6.y;
					    u_xlat38 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat40 = u_xlat5.y * u_xlat38;
					    u_xlat17.xyz = u_xlat5.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat17.x + u_xlat40;
					    u_xlat7.y = u_xlat6.x * u_xlat5.x + u_xlat40;
					    u_xlat8 = textureLod(_PaintTex, u_xlat7.xz, 0.0);
					    u_xlat21.yz = u_xlat7.yx;
					    u_xlat10 = textureLod(_PaintTex, u_xlat7.yz, 0.0);
					    u_xlat30.xy = u_xlat12.xx * u_xlat8.xy;
					    u_xlat8.xyz = (-u_xlat12.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat30.xy = u_xlat30.xy * u_xlat8.yy;
					    u_xlat30.xy = u_xlat8.zz * u_xlat30.xy;
					    u_xlat10.xy = u_xlat8.xx * u_xlat10.xy;
					    u_xlat10.xy = u_xlat8.yy * u_xlat10.xy;
					    u_xlat30.xy = u_xlat10.xy * u_xlat8.zz + u_xlat30.xy;
					    u_xlat38 = u_xlat38 * u_xlat17.y;
					    u_xlat7.y = u_xlat6.x * u_xlat5.x + u_xlat38;
					    u_xlat7.x = u_xlat6.x * u_xlat17.x + u_xlat38;
					    u_xlat21.x = u_xlat17.z * u_xlat6.y;
					    u_xlat5 = textureLod(_PaintTex, u_xlat7.yz, 0.0);
					    u_xlat10 = textureLod(_PaintTex, u_xlat7.xz, 0.0);
					    u_xlat5.zw = u_xlat12.xx * u_xlat10.xy;
					    u_xlat5.xy = u_xlat8.xx * u_xlat5.xy;
					    u_xlat5 = u_xlat12.yyyy * u_xlat5;
					    u_xlat5.xy = u_xlat5.xy * u_xlat8.zz + u_xlat30.xy;
					    u_xlat5.xy = u_xlat5.zw * u_xlat8.zz + u_xlat5.xy;
					    u_xlat6 = textureLod(_PaintTex, u_xlat21.yx, 0.0);
					    u_xlat10 = textureLod(_PaintTex, u_xlat21.zx, 0.0);
					    u_xlat7.w = u_xlat21.x;
					    u_xlat29.xy = u_xlat12.xx * u_xlat10.xy;
					    u_xlat29.xy = u_xlat8.yy * u_xlat29.xy;
					    u_xlat6.xy = u_xlat8.xx * u_xlat6.xy;
					    u_xlat6.xy = u_xlat8.yy * u_xlat6.xy;
					    u_xlat5.xy = u_xlat6.xy * u_xlat12.zz + u_xlat5.xy;
					    u_xlat5.xy = u_xlat29.xy * u_xlat12.zz + u_xlat5.xy;
					    u_xlat6 = textureLod(_PaintTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat7.xw, 0.0);
					    u_xlat29.xy = u_xlat12.xx * u_xlat7.xy;
					    u_xlat29.xy = u_xlat12.yy * u_xlat29.xy;
					    u_xlat6.xy = u_xlat8.xx * u_xlat6.xy;
					    u_xlat12.xy = u_xlat12.yy * u_xlat6.xy;
					    u_xlat12.xy = u_xlat12.xy * u_xlat12.zz + u_xlat5.xy;
					    u_xlat12.xy = u_xlat29.xy * u_xlat12.zz + u_xlat12.xy;
					    u_xlat4.xyz = u_xlat12.yyy * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat12.x = u_xlat12.x * 1.20000005 + -0.200000003;
					    u_xlat12.x = clamp(u_xlat12.x, 0.0, 1.0);
					    u_xlat5.xyz = u_xlat4.xyz * _BloodColor.xyz + (-u_xlat4.xyz);
					    u_xlat12.xyz = u_xlat12.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * u_xlat12.xyz;
					    u_xlat2 = u_xlat3 * vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat3 = u_xlat1 * vec4(1.0, 1.0, 1.0, 0.699999988) + (-u_xlat2);
					    u_xlat12.x = u_xlat1.w * 0.699999988;
					    vs_COLOR1 = u_xlat12.xxxx * u_xlat3 + u_xlat2;
					    u_xlat12.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1 = u_xlat0.x / u_xlat12.x;
					    vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_ISLAND_ON" }
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
						vec4 _UvTex_ST;
						vec4 unused_0_10[3];
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
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _UvTex_ST.xy + _UvTex_ST.zw;
					    vs_TEXCOORD2.xy = in_COLOR0.yx;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    vs_COLOR0 = vec4(0.5, 0.5, 0.5, 0.5);
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
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.zxy + u_xlat3.xyz;
					    u_xlat0 = (-u_xlat1) + _SnowColor;
					    u_xlat0 = vec4(_SnowAmount) * u_xlat0 + u_xlat1;
					    u_xlat1.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.xxxx;
					    vs_COLOR1 = u_xlat0 * vec4(0.5, 0.5, 0.5, 0.5);
					    vs_TEXCOORD1 = 0.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" "_ISLAND_ON" }
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
						vec4 unused_0_3[37];
						float _Year;
						vec4 unused_0_5[7];
						vec4 _SnowColor;
						vec4 unused_0_7;
						float _SnowAmount;
						vec4 unused_0_9[12];
						vec4 _UvTex_ST;
						vec4 unused_0_11[3];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _GrassTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD1;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat8;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _UvTex_ST.xy + _UvTex_ST.zw;
					    vs_TEXCOORD2.xy = in_COLOR0.yx;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat0.xzw = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xzw = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xzw;
					    u_xlat0.xzw = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xzw;
					    u_xlat0.xzw = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xzw;
					    u_xlat8 = u_xlat0.z + 1.5;
					    u_xlat0.xw = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xw;
					    u_xlat8 = floor(u_xlat8);
					    u_xlat1.xy = floor(u_xlat0.xw);
					    u_xlat0.xw = fract(u_xlat0.xw);
					    u_xlat0.xw = u_xlat0.xw + vec2(-0.5, -0.5);
					    u_xlat0.xw = -abs(u_xlat0.xw) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xw = u_xlat0.xw * vec2(3.0, 3.0);
					    u_xlat0.xw = min(u_xlat0.xw, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.w * u_xlat0.x;
					    u_xlat8 = u_xlat8 * _AoTexVolume.x + u_xlat1.x;
					    u_xlat1.y = u_xlat1.y / _AoTexSize.y;
					    u_xlat1.x = u_xlat8 / _AoTexSize.x;
					    u_xlat1 = textureLod(_HighlightTex, u_xlat1.xy, 0.0);
					    u_xlat1 = u_xlat0.xxxx * u_xlat1;
					    u_xlatb0 = 0.0<u_xlat1.w;
					    u_xlat2.xyz = u_xlat1.xyz / u_xlat1.www;
					    u_xlat1.xyz = (bool(u_xlatb0)) ? u_xlat2.xyz : u_xlat1.xyz;
					    u_xlat1 = u_xlat1 * in_COLOR0.zzzz;
					    vs_COLOR0 = u_xlat1.wwww * vec4(-0.349999994, -0.349999994, -0.349999994, 0.349999994) + vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat0.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat0.x) * 0.5 + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat4.x = u_xlat0.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8 = max(u_xlat0.x, u_xlat4.x);
					    u_xlat8 = (-u_xlat8) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat4.xxx * u_xlat2.yzx;
					    u_xlat4.xyz = u_xlat2.xyz * vec3(u_xlat8) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat4.xyz;
					    u_xlat0 = (-u_xlat2) + _SnowColor;
					    u_xlat0 = vec4(_SnowAmount) * u_xlat0 + u_xlat2;
					    u_xlat2.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat2.xxxx;
					    u_xlat0 = u_xlat0 * vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat2 = u_xlat1 * vec4(1.0, 1.0, 1.0, 0.699999988) + (-u_xlat0);
					    u_xlat1.x = u_xlat1.w * 0.699999988;
					    vs_COLOR1 = u_xlat1.xxxx * u_xlat2 + u_xlat0;
					    vs_TEXCOORD1 = 0.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_PAINT_ON" "_ISLAND_ON" }
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
						vec4 unused_0_6[2];
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec3 _PaintTexOffset;
						vec4 _LevelRect;
						vec4 unused_0_11[19];
						vec3 _SunDir;
						vec4 unused_0_13[4];
						vec4 _SideSunColor;
						vec4 unused_0_15[2];
						float _Year;
						vec4 unused_0_17;
						vec4 _BloodColor;
						vec4 unused_0_19[5];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_23[2];
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_28[2];
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_31[2];
						vec4 _UvTex_ST;
						vec4 unused_0_33[3];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					bool u_xlatb3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec4 u_xlat10;
					vec3 u_xlat12;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat17;
					vec3 u_xlat20;
					vec3 u_xlat21;
					vec3 u_xlat23;
					float u_xlat26;
					vec2 u_xlat29;
					vec2 u_xlat30;
					float u_xlat38;
					float u_xlat39;
					float u_xlat40;
					float u_xlat41;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _UvTex_ST.xy + _UvTex_ST.zw;
					    vs_TEXCOORD2.xy = in_COLOR0.yx;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat0.xzw = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xzw = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xzw;
					    u_xlat0.xzw = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xzw;
					    u_xlat0.xzw = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xzw;
					    u_xlat1.x = u_xlat0.z + 1.5;
					    u_xlat1.x = floor(u_xlat1.x);
					    u_xlat2.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat2.y = 1.0;
					    u_xlat2.xyz = u_xlat0.xzw + u_xlat2.xyz;
					    u_xlat1.yz = floor(u_xlat2.xz);
					    u_xlat1.x = u_xlat1.x * _AoTexVolume.x + u_xlat1.y;
					    u_xlat3.xy = u_xlat1.xz / _AoTexSize.xy;
					    u_xlat1 = textureLod(_HighlightTex, u_xlat3.xy, 0.0);
					    u_xlat3.xy = fract(u_xlat2.xz);
					    u_xlat3.xy = u_xlat3.xy + vec2(-0.5, -0.5);
					    u_xlat3.xy = -abs(u_xlat3.xy) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat3.xy = u_xlat3.xy * vec2(3.0, 3.0);
					    u_xlat3.xy = min(u_xlat3.xy, vec2(1.0, 1.0));
					    u_xlat3.x = u_xlat3.y * u_xlat3.x;
					    u_xlat1 = u_xlat1 * u_xlat3.xxxx;
					    u_xlatb3 = 0.0<u_xlat1.w;
					    u_xlat15.xyz = u_xlat1.xyz / u_xlat1.www;
					    u_xlat1.xyz = (bool(u_xlatb3)) ? u_xlat15.xyz : u_xlat1.xyz;
					    u_xlat1 = u_xlat1 * in_COLOR0.zzzz;
					    vs_COLOR0 = u_xlat1.wwww * vec4(-0.349999994, -0.349999994, -0.349999994, 0.349999994) + vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat2.w = u_xlat0.z;
					    u_xlat3.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = floor(u_xlat3.xyz);
					    u_xlat3.xyz = fract(u_xlat3.xyz);
					    u_xlat5.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat6.z = u_xlat4.z * u_xlat5.y;
					    u_xlat38 = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat39 = u_xlat4.y * u_xlat38;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat6.x = u_xlat5.x * u_xlat16.x + u_xlat39;
					    u_xlat6.y = u_xlat5.x * u_xlat4.x + u_xlat39;
					    u_xlat7 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat20.yz = u_xlat6.yx;
					    u_xlat9 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat10.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat7 = u_xlat7 * u_xlat10.yyyy;
					    u_xlat7 = u_xlat10.zzzz * u_xlat7;
					    u_xlat9 = u_xlat9 * u_xlat10.xxxx;
					    u_xlat9 = u_xlat10.yyyy * u_xlat9;
					    u_xlat7 = u_xlat9 * u_xlat10.zzzz + u_xlat7;
					    u_xlat38 = u_xlat38 * u_xlat16.y;
					    u_xlat6.y = u_xlat5.x * u_xlat4.x + u_xlat38;
					    u_xlat6.x = u_xlat5.x * u_xlat16.x + u_xlat38;
					    u_xlat20.x = u_xlat16.z * u_xlat5.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat6.yz, 0.0);
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.xz, 0.0);
					    u_xlat5 = u_xlat3.xxxx * u_xlat5;
					    u_xlat5 = u_xlat3.yyyy * u_xlat5;
					    u_xlat4 = u_xlat10.xxxx * u_xlat4;
					    u_xlat4 = u_xlat3.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat10.zzzz + u_xlat7;
					    u_xlat4 = u_xlat5 * u_xlat10.zzzz + u_xlat4;
					    u_xlat5 = textureLod(_NormalTex, u_xlat20.yx, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat20.zx, 0.0);
					    u_xlat6.w = u_xlat20.x;
					    u_xlat7 = u_xlat3.xxxx * u_xlat7;
					    u_xlat7 = u_xlat10.yyyy * u_xlat7;
					    u_xlat5 = u_xlat10.xxxx * u_xlat5;
					    u_xlat5 = u_xlat10.yyyy * u_xlat5;
					    u_xlat4 = u_xlat5 * u_xlat3.zzzz + u_xlat4;
					    u_xlat4 = u_xlat7 * u_xlat3.zzzz + u_xlat4;
					    u_xlat5 = textureLod(_NormalTex, u_xlat6.yw, 0.0);
					    u_xlat6 = textureLod(_NormalTex, u_xlat6.xw, 0.0);
					    u_xlat6 = u_xlat3.xxxx * u_xlat6;
					    u_xlat6 = u_xlat3.yyyy * u_xlat6;
					    u_xlat5 = u_xlat10.xxxx * u_xlat5;
					    u_xlat5 = u_xlat3.yyyy * u_xlat5;
					    u_xlat4 = u_xlat5 * u_xlat3.zzzz + u_xlat4;
					    u_xlat3 = u_xlat6 * u_xlat3.zzzz + u_xlat4;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat38 = u_xlat3.w * 0.400000006;
					    u_xlat4.x = (-u_xlat3.x) + unity_MatrixV[0].z;
					    u_xlat4.y = (-u_xlat3.y) + unity_MatrixV[1].z;
					    u_xlat4.z = (-u_xlat3.z) + unity_MatrixV[2].z;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat4.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat39 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat41 = u_xlat2.y * u_xlat39;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat41;
					    u_xlat7.z = u_xlat2.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat9.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat41;
					    u_xlat10 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat23.yz = u_xlat7.yx;
					    u_xlat10.xyz = u_xlat4.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.zzz * u_xlat10.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat10.xyz;
					    u_xlat14.x = u_xlat39 * u_xlat9.y;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat14.x;
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat14.x;
					    u_xlat23.x = u_xlat6.y * u_xlat9.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat2.xyz = u_xlat4.yyy * u_xlat2.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat5.zzz + u_xlat6.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat23.yx, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat23.zx, 0.0);
					    u_xlat7.w = u_xlat23.x;
					    u_xlat8.xyz = u_xlat4.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat8.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat6.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat5.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat5.xy = abs(_SunDir.yx);
					    u_xlat4.xyz = u_xlat5.zxw;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat39 = u_xlat5.x + u_xlat5.y;
					    u_xlat5.xyz = u_xlat2.xyz * u_xlat4.xyz;
					    u_xlat2.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat2.x = inversesqrt(u_xlat2.x);
					    u_xlat6.xyz = u_xlat2.xxx * u_xlat3.xyz;
					    u_xlat6.w = (-u_xlat6.x);
					    u_xlat3.xyz = u_xlat6.xyw;
					    u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					    u_xlat2.x = dot(u_xlat6.xyz, _FlashDir.xyz);
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.xy = u_xlat3.xy * u_xlat5.xy;
					    u_xlat14.x = u_xlat3.y + u_xlat3.x;
					    u_xlat14.x = u_xlat5.z * u_xlat3.z + u_xlat14.x;
					    u_xlat3.x = u_xlat5.y + u_xlat5.x;
					    u_xlat26 = u_xlat4.z * u_xlat2.z + u_xlat3.x;
					    u_xlat26 = u_xlat26 * 0.600000024 + u_xlat38;
					    u_xlat14.x = u_xlat14.x / u_xlat39;
					    u_xlat38 = (-u_xlat4.w) * 0.5 + u_xlat0.z;
					    u_xlat12.x = (-u_xlat4.w) * 0.5 + u_xlat0.y;
					    u_xlat12.x = u_xlat12.x * 0.25;
					    u_xlat38 = u_xlat38 * 0.25;
					    u_xlat3.x = (-u_xlat38);
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat38 = u_xlat38;
					    u_xlat38 = clamp(u_xlat38, 0.0, 1.0);
					    u_xlat15.x = max(u_xlat3.x, u_xlat38);
					    u_xlat15.x = (-u_xlat15.x) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat38) * u_xlat4.yzx;
					    u_xlat15.xyz = u_xlat4.xyz * u_xlat15.xxx + u_xlat5.xyz;
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat4.zxy + u_xlat15.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + _SnowColor.xyz;
					    u_xlat3.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat5.xyz = u_xlat3.xyz * _MinAmbientColor.xyz;
					    u_xlat3.xyz = (-_MinAmbientColor.xyz) * u_xlat3.xyz + _MaxAmbientColor.xyz;
					    u_xlat3.xyz = vec3(u_xlat26) * u_xlat3.xyz + u_xlat5.xyz;
					    u_xlat5.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat14.xyz = u_xlat5.xyz * u_xlat14.xxx + u_xlat3.xyz;
					    u_xlat3.x = u_xlat2.x * u_xlat2.x;
					    u_xlat2.x = (-u_xlat2.x) * u_xlat3.x + 1.0;
					    u_xlat3.xyz = u_xlat2.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat14.xyz;
					    u_xlat38 = u_xlat12.x;
					    u_xlat38 = clamp(u_xlat38, 0.0, 1.0);
					    u_xlat12.x = (-u_xlat12.x);
					    u_xlat12.x = clamp(u_xlat12.x, 0.0, 1.0);
					    u_xlat3.x = max(u_xlat12.x, u_xlat38);
					    u_xlat15.xyz = u_xlat4.yzx * vec3(u_xlat38);
					    u_xlat38 = (-u_xlat3.x) + 1.0;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(u_xlat38) + u_xlat15.xyz;
					    u_xlat4.xyz = u_xlat12.xxx * u_xlat4.zxy + u_xlat3.xyz;
					    u_xlat3 = (-u_xlat4) + _SnowColor;
					    u_xlat3 = vec4(_SnowAmount) * u_xlat3 + u_xlat4;
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat5.xyz = u_xlat0.xzw + _PaintTexOffset.xyz;
					    u_xlat0.x = dot(u_xlat0.xw, u_xlat0.xw);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x + (-_FogMinRad);
					    u_xlat12.xy = max(u_xlat5.xz, _LevelRect.xy);
					    u_xlat5.xz = min(u_xlat12.xy, _LevelRect.zw);
					    u_xlat12.xyz = max(u_xlat5.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat12.xyz = min(u_xlat12.xyz, u_xlat5.xyz);
					    u_xlat12.xyz = u_xlat12.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = floor(u_xlat12.xyz);
					    u_xlat12.xyz = fract(u_xlat12.xyz);
					    u_xlat6.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat7.z = u_xlat5.z * u_xlat6.y;
					    u_xlat38 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat40 = u_xlat5.y * u_xlat38;
					    u_xlat17.xyz = u_xlat5.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat17.x + u_xlat40;
					    u_xlat7.y = u_xlat6.x * u_xlat5.x + u_xlat40;
					    u_xlat8 = textureLod(_PaintTex, u_xlat7.xz, 0.0);
					    u_xlat21.yz = u_xlat7.yx;
					    u_xlat10 = textureLod(_PaintTex, u_xlat7.yz, 0.0);
					    u_xlat30.xy = u_xlat12.xx * u_xlat8.xy;
					    u_xlat8.xyz = (-u_xlat12.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat30.xy = u_xlat30.xy * u_xlat8.yy;
					    u_xlat30.xy = u_xlat8.zz * u_xlat30.xy;
					    u_xlat10.xy = u_xlat8.xx * u_xlat10.xy;
					    u_xlat10.xy = u_xlat8.yy * u_xlat10.xy;
					    u_xlat30.xy = u_xlat10.xy * u_xlat8.zz + u_xlat30.xy;
					    u_xlat38 = u_xlat38 * u_xlat17.y;
					    u_xlat7.y = u_xlat6.x * u_xlat5.x + u_xlat38;
					    u_xlat7.x = u_xlat6.x * u_xlat17.x + u_xlat38;
					    u_xlat21.x = u_xlat17.z * u_xlat6.y;
					    u_xlat5 = textureLod(_PaintTex, u_xlat7.yz, 0.0);
					    u_xlat10 = textureLod(_PaintTex, u_xlat7.xz, 0.0);
					    u_xlat5.zw = u_xlat12.xx * u_xlat10.xy;
					    u_xlat5.xy = u_xlat8.xx * u_xlat5.xy;
					    u_xlat5 = u_xlat12.yyyy * u_xlat5;
					    u_xlat5.xy = u_xlat5.xy * u_xlat8.zz + u_xlat30.xy;
					    u_xlat5.xy = u_xlat5.zw * u_xlat8.zz + u_xlat5.xy;
					    u_xlat6 = textureLod(_PaintTex, u_xlat21.yx, 0.0);
					    u_xlat10 = textureLod(_PaintTex, u_xlat21.zx, 0.0);
					    u_xlat7.w = u_xlat21.x;
					    u_xlat29.xy = u_xlat12.xx * u_xlat10.xy;
					    u_xlat29.xy = u_xlat8.yy * u_xlat29.xy;
					    u_xlat6.xy = u_xlat8.xx * u_xlat6.xy;
					    u_xlat6.xy = u_xlat8.yy * u_xlat6.xy;
					    u_xlat5.xy = u_xlat6.xy * u_xlat12.zz + u_xlat5.xy;
					    u_xlat5.xy = u_xlat29.xy * u_xlat12.zz + u_xlat5.xy;
					    u_xlat6 = textureLod(_PaintTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat7.xw, 0.0);
					    u_xlat29.xy = u_xlat12.xx * u_xlat7.xy;
					    u_xlat29.xy = u_xlat12.yy * u_xlat29.xy;
					    u_xlat6.xy = u_xlat8.xx * u_xlat6.xy;
					    u_xlat12.xy = u_xlat12.yy * u_xlat6.xy;
					    u_xlat12.xy = u_xlat12.xy * u_xlat12.zz + u_xlat5.xy;
					    u_xlat12.xy = u_xlat29.xy * u_xlat12.zz + u_xlat12.xy;
					    u_xlat4.xyz = u_xlat12.yyy * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat12.x = u_xlat12.x * 1.20000005 + -0.200000003;
					    u_xlat12.x = clamp(u_xlat12.x, 0.0, 1.0);
					    u_xlat5.xyz = u_xlat4.xyz * _BloodColor.xyz + (-u_xlat4.xyz);
					    u_xlat12.xyz = u_xlat12.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat3.xyz = u_xlat2.xyz * u_xlat12.xyz;
					    u_xlat2 = u_xlat3 * vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat3 = u_xlat1 * vec4(1.0, 1.0, 1.0, 0.699999988) + (-u_xlat2);
					    u_xlat12.x = u_xlat1.w * 0.699999988;
					    vs_COLOR1 = u_xlat12.xxxx * u_xlat3 + u_xlat2;
					    u_xlat12.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1 = u_xlat0.x / u_xlat12.x;
					    vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" "_CINEMATIC_ON" "_ISLAND_ON" }
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
						vec4 _UvTex_ST;
						vec4 unused_0_10[3];
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
					out vec2 vs_TEXCOORD2;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out float vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _UvTex_ST.xy + _UvTex_ST.zw;
					    vs_TEXCOORD2.xy = in_COLOR0.yx;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    vs_COLOR0 = vec4(0.5, 0.5, 0.5, 0.5);
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
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.zxy + u_xlat3.xyz;
					    u_xlat0 = (-u_xlat1) + _SnowColor;
					    u_xlat0 = vec4(_SnowAmount) * u_xlat0 + u_xlat1;
					    u_xlat1.x = unity_MatrixV[1].z * 0.0300000012 + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.xxxx;
					    vs_COLOR1 = u_xlat0 * vec4(0.5, 0.5, 0.5, 0.5);
					    vs_TEXCOORD1 = 0.0;
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
						vec4 _PartTex_TexelSize;
						vec4 _PartTexTiling;
						vec4 unused_0_3;
					};
					uniform  sampler2D _UvTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = texture(_UvTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat0.xy * _PartTex_TexelSize.xy;
					    u_xlat0.xy = u_xlat0.xy * _PartTexTiling.xy + vs_TEXCOORD2.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    SV_Target0 = u_xlat0 * vs_COLOR0 + vs_COLOR1;
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
						vec4 _PartTex_TexelSize;
						vec4 _PartTexTiling;
						vec4 unused_0_11;
					};
					uniform  sampler2D _UvTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  float vs_TEXCOORD1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_UvTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat0.xy * _PartTex_TexelSize.xy;
					    u_xlat0.xy = u_xlat0.xy * _PartTexTiling.xy + vs_TEXCOORD2.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    u_xlat0 = u_xlat0 * vs_COLOR0 + vs_COLOR1;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    SV_Target0.w = u_xlat0.w;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
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
						vec4 _PartTex_TexelSize;
						vec4 _PartTexTiling;
						vec4 unused_0_3;
					};
					uniform  sampler2D _UvTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = texture(_UvTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat0.xy * _PartTex_TexelSize.xy;
					    u_xlat0.xy = u_xlat0.xy * _PartTexTiling.xy + vs_TEXCOORD2.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    SV_Target0 = u_xlat0 * vs_COLOR0 + vs_COLOR1;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" }
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
						vec4 _PartTex_TexelSize;
						vec4 _PartTexTiling;
						vec4 unused_0_3;
					};
					uniform  sampler2D _UvTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = texture(_UvTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat0.xy * _PartTex_TexelSize.xy;
					    u_xlat0.xy = u_xlat0.xy * _PartTexTiling.xy + vs_TEXCOORD2.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    SV_Target0 = u_xlat0 * vs_COLOR0 + vs_COLOR1;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_PAINT_ON" }
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
						vec4 _PartTex_TexelSize;
						vec4 _PartTexTiling;
						vec4 unused_0_11;
					};
					uniform  sampler2D _UvTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  float vs_TEXCOORD1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_UvTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat0.xy * _PartTex_TexelSize.xy;
					    u_xlat0.xy = u_xlat0.xy * _PartTexTiling.xy + vs_TEXCOORD2.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    u_xlat0 = u_xlat0 * vs_COLOR0 + vs_COLOR1;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    SV_Target0.w = u_xlat0.w;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
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
					Keywords { "_PAINT_ON" "_CINEMATIC_ON" }
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
						vec4 _PartTex_TexelSize;
						vec4 _PartTexTiling;
						vec4 unused_0_3;
					};
					uniform  sampler2D _UvTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = texture(_UvTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat0.xy * _PartTex_TexelSize.xy;
					    u_xlat0.xy = u_xlat0.xy * _PartTexTiling.xy + vs_TEXCOORD2.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    SV_Target0 = u_xlat0 * vs_COLOR0 + vs_COLOR1;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_ISLAND_ON" }
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
						vec4 _PartTex_TexelSize;
						vec4 _PartTexTiling;
						vec4 unused_0_3;
					};
					uniform  sampler2D _UvTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = texture(_UvTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat0.xy * _PartTex_TexelSize.xy;
					    u_xlat0.xy = u_xlat0.xy * _PartTexTiling.xy + vs_TEXCOORD2.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    SV_Target0 = u_xlat0 * vs_COLOR0 + vs_COLOR1;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_ISLAND_ON" }
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
						vec4 _PartTex_TexelSize;
						vec4 _PartTexTiling;
						vec4 unused_0_11;
					};
					uniform  sampler2D _UvTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  float vs_TEXCOORD1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_UvTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat0.xy * _PartTex_TexelSize.xy;
					    u_xlat0.xy = u_xlat0.xy * _PartTexTiling.xy + vs_TEXCOORD2.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    u_xlat0 = u_xlat0 * vs_COLOR0 + vs_COLOR1;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    SV_Target0.w = u_xlat0.w;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
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
					Keywords { "_CINEMATIC_ON" "_ISLAND_ON" }
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
						vec4 _PartTex_TexelSize;
						vec4 _PartTexTiling;
						vec4 unused_0_3;
					};
					uniform  sampler2D _UvTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = texture(_UvTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat0.xy * _PartTex_TexelSize.xy;
					    u_xlat0.xy = u_xlat0.xy * _PartTexTiling.xy + vs_TEXCOORD2.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    SV_Target0 = u_xlat0 * vs_COLOR0 + vs_COLOR1;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_PAINT_ON" "_ISLAND_ON" }
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
						vec4 _PartTex_TexelSize;
						vec4 _PartTexTiling;
						vec4 unused_0_3;
					};
					uniform  sampler2D _UvTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = texture(_UvTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat0.xy * _PartTex_TexelSize.xy;
					    u_xlat0.xy = u_xlat0.xy * _PartTexTiling.xy + vs_TEXCOORD2.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    SV_Target0 = u_xlat0 * vs_COLOR0 + vs_COLOR1;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_PAINT_ON" "_ISLAND_ON" }
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
						vec4 _PartTex_TexelSize;
						vec4 _PartTexTiling;
						vec4 unused_0_11;
					};
					uniform  sampler2D _UvTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  float vs_TEXCOORD1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_UvTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat0.xy * _PartTex_TexelSize.xy;
					    u_xlat0.xy = u_xlat0.xy * _PartTexTiling.xy + vs_TEXCOORD2.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    u_xlat0 = u_xlat0 * vs_COLOR0 + vs_COLOR1;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    SV_Target0.w = u_xlat0.w;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
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
					Keywords { "_PAINT_ON" "_CINEMATIC_ON" "_ISLAND_ON" }
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
						vec4 _PartTex_TexelSize;
						vec4 _PartTexTiling;
						vec4 unused_0_3;
					};
					uniform  sampler2D _UvTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = texture(_UvTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat0.xy * _PartTex_TexelSize.xy;
					    u_xlat0.xy = u_xlat0.xy * _PartTexTiling.xy + vs_TEXCOORD2.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    SV_Target0 = u_xlat0 * vs_COLOR0 + vs_COLOR1;
					    return;
					}"
				}
			}
		}
	}
}