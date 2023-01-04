Shader "Unlit/ColoredCharacter" {
	Properties {
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_PartTex ("PartTex", 2D) = "white" {}
		_BloodTex ("BloodTex", 2D) = "white" {}
		_PartTexTiling ("PartTexTiling", Vector) = (4,1,1,1)
		[Toggle] _Mirror ("Mirror", Float) = 0
		[Toggle] _Selected ("Selected", Float) = 0
	}
	SubShader {
		LOD 100
		Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "RenderType" = "Opaque" }
		Pass {
			LOD 100
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "RenderType" = "Opaque" }
			AlphaToMask On
			Cull Off
			GpuProgramID 33771
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
						vec4 unused_0_3[37];
						float _Year;
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[37];
						float _Year;
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_LOWEND_ON" }
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
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" }
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
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_LOWEND_ON" }
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
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
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
						vec4 unused_0_17[3];
						vec4 _LongshipColor;
						vec4 unused_0_19[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_23;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_31;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_35;
						float _Hover;
						vec4 unused_0_37[2];
						vec4 _MainTex_ST;
						vec4 unused_0_39[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat16;
					vec3 u_xlat18;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					bool u_xlatb24;
					float u_xlat25;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat36 = (-u_xlat5.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat14.x = u_xlat4.y * u_xlat37;
					    u_xlat7.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat14.x;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat14.x;
					    u_xlat10 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat37;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat37;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat6.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat5.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat4.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat5.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat5.xy = abs(_SunDir.yx);
					    u_xlat6.xyz = u_xlat5.zxw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat5.x + u_xlat5.y;
					    u_xlat4.xyw = u_xlat4.xyz * u_xlat6.xyz;
					    u_xlat14.x = u_xlat4.y + u_xlat4.x;
					    u_xlat14.x = u_xlat6.z * u_xlat4.z + u_xlat14.x;
					    u_xlat24.x = u_xlat14.x * 0.600000024 + u_xlat24.x;
					    u_xlat14.x = u_xlat36;
					    u_xlat14.x = clamp(u_xlat14.x, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat38 = max(u_xlat36, u_xlat14.x);
					    u_xlat38 = (-u_xlat38) + 1.0;
					    u_xlat5.x = _Year;
					    u_xlat5.y = 0.0;
					    u_xlat5 = textureLod(_GrassTex, u_xlat5.xy, 0.0);
					    u_xlat6.xyz = u_xlat14.xxx * u_xlat5.yzx;
					    u_xlat6.xyz = u_xlat5.xyz * vec3(u_xlat38) + u_xlat6.xyz;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.zxy + u_xlat6.xyz;
					    u_xlat6.xyz = (-u_xlat5.xyz) + _SnowColor.xyz;
					    u_xlat5.xyz = vec3(_SnowAmount) * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat5.xyz * _MinAmbientColor.xyz;
					    u_xlat7.xyz = (-_MinAmbientColor.xyz) * u_xlat5.xyz + _MaxAmbientColor.xyz;
					    u_xlat5.xyz = u_xlat5.xyz + (-_LongshipColor.xyz);
					    u_xlat6.xyz = u_xlat24.xxx * u_xlat7.xyz + u_xlat6.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat7.xyz = u_xlat3.xyw;
					    u_xlat7.xyz = clamp(u_xlat7.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat14.xz = u_xlat4.xy * u_xlat7.xy;
					    u_xlat36 = u_xlat14.z + u_xlat14.x;
					    u_xlat36 = u_xlat4.w * u_xlat7.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat36) + u_xlat6.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat4.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat3.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat4.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat4.xz, _LevelRect.xy);
					    u_xlat4.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat6.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat14.xz = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat14.z;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat37 = u_xlat7.x * u_xlat9.y;
					    u_xlat37 = u_xlat7.y * u_xlat37;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat36;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat22.yz = u_xlat8.yx;
					    u_xlat36 = u_xlat6.x * u_xlat9.y;
					    u_xlat36 = u_xlat7.y * u_xlat36;
					    u_xlat36 = u_xlat7.z * u_xlat36;
					    u_xlat36 = u_xlat37 * u_xlat7.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat24.x;
					    u_xlat22.x = u_xlat14.z * u_xlat16.z;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.x = u_xlat6.x * u_xlat9.y;
					    u_xlat24.x = u_xlat6.y * u_xlat24.x;
					    u_xlat37 = u_xlat7.x * u_xlat4.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat36 = u_xlat37 * u_xlat7.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat7.z + u_xlat36;
					    u_xlat4 = textureLod(_PaintTex, u_xlat22.yx, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat22.zx, 0.0);
					    u_xlat8.w = u_xlat22.x;
					    u_xlat36 = u_xlat6.x * u_xlat9.y;
					    u_xlat36 = u_xlat7.y * u_xlat36;
					    u_xlat37 = u_xlat7.x * u_xlat4.y;
					    u_xlat37 = u_xlat7.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat6.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat6.z + u_xlat24.x;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat36 = u_xlat6.x * u_xlat8.y;
					    u_xlat36 = u_xlat6.y * u_xlat36;
					    u_xlat37 = u_xlat7.x * u_xlat4.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat6.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat6.z + u_xlat24.x;
					    u_xlat36 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat36 = u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat4.xyz = vec3(u_xlat36) * u_xlat5.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat4.xyz = u_xlat24.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat24.x = u_xlat1.y + 1.5;
					    u_xlat24.x = floor(u_xlat24.x);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat36 = u_xlat2.z * u_xlat2.x;
					    u_xlat24.x = u_xlat24.x * _AoTexVolume.x + u_xlat14.x;
					    u_xlat2.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat2.x = u_xlat24.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = vec4(u_xlat36) * u_xlat2;
					    u_xlatb24 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb24)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat4.xyz) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
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
						vec4 unused_0_17[3];
						vec4 _LongshipColor;
						vec4 unused_0_19[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_23;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_31;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_35;
						float _Hover;
						vec4 unused_0_37[2];
						vec4 _MainTex_ST;
						vec4 unused_0_39[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat16;
					vec3 u_xlat18;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					bool u_xlatb24;
					float u_xlat25;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat36 = (-u_xlat5.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat14.x = u_xlat4.y * u_xlat37;
					    u_xlat7.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat14.x;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat14.x;
					    u_xlat10 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat37;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat37;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat6.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat5.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat4.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat5.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat5.xy = abs(_SunDir.yx);
					    u_xlat6.xyz = u_xlat5.zxw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat5.x + u_xlat5.y;
					    u_xlat4.xyw = u_xlat4.xyz * u_xlat6.xyz;
					    u_xlat14.x = u_xlat4.y + u_xlat4.x;
					    u_xlat14.x = u_xlat6.z * u_xlat4.z + u_xlat14.x;
					    u_xlat24.x = u_xlat14.x * 0.600000024 + u_xlat24.x;
					    u_xlat14.x = u_xlat36;
					    u_xlat14.x = clamp(u_xlat14.x, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat38 = max(u_xlat36, u_xlat14.x);
					    u_xlat38 = (-u_xlat38) + 1.0;
					    u_xlat5.x = _Year;
					    u_xlat5.y = 0.0;
					    u_xlat5 = textureLod(_GrassTex, u_xlat5.xy, 0.0);
					    u_xlat6.xyz = u_xlat14.xxx * u_xlat5.yzx;
					    u_xlat6.xyz = u_xlat5.xyz * vec3(u_xlat38) + u_xlat6.xyz;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.zxy + u_xlat6.xyz;
					    u_xlat6.xyz = (-u_xlat5.xyz) + _SnowColor.xyz;
					    u_xlat5.xyz = vec3(_SnowAmount) * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat5.xyz * _MinAmbientColor.xyz;
					    u_xlat7.xyz = (-_MinAmbientColor.xyz) * u_xlat5.xyz + _MaxAmbientColor.xyz;
					    u_xlat5.xyz = u_xlat5.xyz + (-_LongshipColor.xyz);
					    u_xlat6.xyz = u_xlat24.xxx * u_xlat7.xyz + u_xlat6.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat7.xyz = u_xlat3.xyw;
					    u_xlat7.xyz = clamp(u_xlat7.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat14.xz = u_xlat4.xy * u_xlat7.xy;
					    u_xlat36 = u_xlat14.z + u_xlat14.x;
					    u_xlat36 = u_xlat4.w * u_xlat7.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat36) + u_xlat6.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat4.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat3.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat4.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat4.xz, _LevelRect.xy);
					    u_xlat4.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat6.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat14.xz = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat14.z;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat37 = u_xlat7.x * u_xlat9.y;
					    u_xlat37 = u_xlat7.y * u_xlat37;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat36;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat22.yz = u_xlat8.yx;
					    u_xlat36 = u_xlat6.x * u_xlat9.y;
					    u_xlat36 = u_xlat7.y * u_xlat36;
					    u_xlat36 = u_xlat7.z * u_xlat36;
					    u_xlat36 = u_xlat37 * u_xlat7.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat24.x;
					    u_xlat22.x = u_xlat14.z * u_xlat16.z;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.x = u_xlat6.x * u_xlat9.y;
					    u_xlat24.x = u_xlat6.y * u_xlat24.x;
					    u_xlat37 = u_xlat7.x * u_xlat4.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat36 = u_xlat37 * u_xlat7.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat7.z + u_xlat36;
					    u_xlat4 = textureLod(_PaintTex, u_xlat22.yx, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat22.zx, 0.0);
					    u_xlat8.w = u_xlat22.x;
					    u_xlat36 = u_xlat6.x * u_xlat9.y;
					    u_xlat36 = u_xlat7.y * u_xlat36;
					    u_xlat37 = u_xlat7.x * u_xlat4.y;
					    u_xlat37 = u_xlat7.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat6.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat6.z + u_xlat24.x;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat36 = u_xlat6.x * u_xlat8.y;
					    u_xlat36 = u_xlat6.y * u_xlat36;
					    u_xlat37 = u_xlat7.x * u_xlat4.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat6.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat6.z + u_xlat24.x;
					    u_xlat36 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat36 = u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat4.xyz = vec3(u_xlat36) * u_xlat5.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat4.xyz = u_xlat24.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat24.x = u_xlat1.y + 1.5;
					    u_xlat24.x = floor(u_xlat24.x);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat36 = u_xlat2.z * u_xlat2.x;
					    u_xlat24.x = u_xlat24.x * _AoTexVolume.x + u_xlat14.x;
					    u_xlat2.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat2.x = u_xlat24.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = vec4(u_xlat36) * u_xlat2;
					    u_xlatb24 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb24)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat4.xyz) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_LOWEND_ON" }
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
						vec4 unused_0_17[3];
						vec4 _LongshipColor;
						vec4 unused_0_19[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_23;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_31;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_35;
						float _Hover;
						vec4 unused_0_37[2];
						vec4 _MainTex_ST;
						vec4 unused_0_39[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat16;
					vec3 u_xlat18;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					bool u_xlatb24;
					float u_xlat25;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat36 = (-u_xlat5.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat14.x = u_xlat4.y * u_xlat37;
					    u_xlat7.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat14.x;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat14.x;
					    u_xlat10 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat37;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat37;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat6.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat5.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat4.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat5.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat5.xy = abs(_SunDir.yx);
					    u_xlat6.xyz = u_xlat5.zxw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat5.x + u_xlat5.y;
					    u_xlat4.xyw = u_xlat4.xyz * u_xlat6.xyz;
					    u_xlat14.x = u_xlat4.y + u_xlat4.x;
					    u_xlat14.x = u_xlat6.z * u_xlat4.z + u_xlat14.x;
					    u_xlat24.x = u_xlat14.x * 0.600000024 + u_xlat24.x;
					    u_xlat14.x = u_xlat36;
					    u_xlat14.x = clamp(u_xlat14.x, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat38 = max(u_xlat36, u_xlat14.x);
					    u_xlat38 = (-u_xlat38) + 1.0;
					    u_xlat5.x = _Year;
					    u_xlat5.y = 0.0;
					    u_xlat5 = textureLod(_GrassTex, u_xlat5.xy, 0.0);
					    u_xlat6.xyz = u_xlat14.xxx * u_xlat5.yzx;
					    u_xlat6.xyz = u_xlat5.xyz * vec3(u_xlat38) + u_xlat6.xyz;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.zxy + u_xlat6.xyz;
					    u_xlat6.xyz = (-u_xlat5.xyz) + _SnowColor.xyz;
					    u_xlat5.xyz = vec3(_SnowAmount) * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat5.xyz * _MinAmbientColor.xyz;
					    u_xlat7.xyz = (-_MinAmbientColor.xyz) * u_xlat5.xyz + _MaxAmbientColor.xyz;
					    u_xlat5.xyz = u_xlat5.xyz + (-_LongshipColor.xyz);
					    u_xlat6.xyz = u_xlat24.xxx * u_xlat7.xyz + u_xlat6.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat7.xyz = u_xlat3.xyw;
					    u_xlat7.xyz = clamp(u_xlat7.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat14.xz = u_xlat4.xy * u_xlat7.xy;
					    u_xlat36 = u_xlat14.z + u_xlat14.x;
					    u_xlat36 = u_xlat4.w * u_xlat7.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat36) + u_xlat6.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat4.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat3.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat4.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat4.xz, _LevelRect.xy);
					    u_xlat4.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat6.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat14.xz = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat14.z;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat37 = u_xlat7.x * u_xlat9.y;
					    u_xlat37 = u_xlat7.y * u_xlat37;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat36;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat22.yz = u_xlat8.yx;
					    u_xlat36 = u_xlat6.x * u_xlat9.y;
					    u_xlat36 = u_xlat7.y * u_xlat36;
					    u_xlat36 = u_xlat7.z * u_xlat36;
					    u_xlat36 = u_xlat37 * u_xlat7.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat24.x;
					    u_xlat22.x = u_xlat14.z * u_xlat16.z;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.x = u_xlat6.x * u_xlat9.y;
					    u_xlat24.x = u_xlat6.y * u_xlat24.x;
					    u_xlat37 = u_xlat7.x * u_xlat4.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat36 = u_xlat37 * u_xlat7.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat7.z + u_xlat36;
					    u_xlat4 = textureLod(_PaintTex, u_xlat22.yx, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat22.zx, 0.0);
					    u_xlat8.w = u_xlat22.x;
					    u_xlat36 = u_xlat6.x * u_xlat9.y;
					    u_xlat36 = u_xlat7.y * u_xlat36;
					    u_xlat37 = u_xlat7.x * u_xlat4.y;
					    u_xlat37 = u_xlat7.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat6.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat6.z + u_xlat24.x;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat36 = u_xlat6.x * u_xlat8.y;
					    u_xlat36 = u_xlat6.y * u_xlat36;
					    u_xlat37 = u_xlat7.x * u_xlat4.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat6.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat6.z + u_xlat24.x;
					    u_xlat36 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat36 = u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat4.xyz = vec3(u_xlat36) * u_xlat5.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat4.xyz = u_xlat24.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat24.x = u_xlat1.y + 1.5;
					    u_xlat24.x = floor(u_xlat24.x);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat36 = u_xlat2.z * u_xlat2.x;
					    u_xlat24.x = u_xlat24.x * _AoTexVolume.x + u_xlat14.x;
					    u_xlat2.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat2.x = u_xlat24.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = vec4(u_xlat36) * u_xlat2;
					    u_xlatb24 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb24)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat4.xyz) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_17[3];
						vec4 _LongshipColor;
						vec4 unused_0_19[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_23;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_31;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_35;
						float _Hover;
						vec4 unused_0_37[2];
						vec4 _MainTex_ST;
						vec4 unused_0_39[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat16;
					vec3 u_xlat18;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					bool u_xlatb24;
					float u_xlat25;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat36 = (-u_xlat5.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat14.x = u_xlat4.y * u_xlat37;
					    u_xlat7.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat14.x;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat14.x;
					    u_xlat10 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat37;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat37;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat6.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat5.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat4.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat5.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat5.xy = abs(_SunDir.yx);
					    u_xlat6.xyz = u_xlat5.zxw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat5.x + u_xlat5.y;
					    u_xlat4.xyw = u_xlat4.xyz * u_xlat6.xyz;
					    u_xlat14.x = u_xlat4.y + u_xlat4.x;
					    u_xlat14.x = u_xlat6.z * u_xlat4.z + u_xlat14.x;
					    u_xlat24.x = u_xlat14.x * 0.600000024 + u_xlat24.x;
					    u_xlat14.x = u_xlat36;
					    u_xlat14.x = clamp(u_xlat14.x, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat38 = max(u_xlat36, u_xlat14.x);
					    u_xlat38 = (-u_xlat38) + 1.0;
					    u_xlat5.x = _Year;
					    u_xlat5.y = 0.0;
					    u_xlat5 = textureLod(_GrassTex, u_xlat5.xy, 0.0);
					    u_xlat6.xyz = u_xlat14.xxx * u_xlat5.yzx;
					    u_xlat6.xyz = u_xlat5.xyz * vec3(u_xlat38) + u_xlat6.xyz;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.zxy + u_xlat6.xyz;
					    u_xlat6.xyz = (-u_xlat5.xyz) + _SnowColor.xyz;
					    u_xlat5.xyz = vec3(_SnowAmount) * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat5.xyz * _MinAmbientColor.xyz;
					    u_xlat7.xyz = (-_MinAmbientColor.xyz) * u_xlat5.xyz + _MaxAmbientColor.xyz;
					    u_xlat5.xyz = u_xlat5.xyz + (-_LongshipColor.xyz);
					    u_xlat6.xyz = u_xlat24.xxx * u_xlat7.xyz + u_xlat6.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat7.xyz = u_xlat3.xyw;
					    u_xlat7.xyz = clamp(u_xlat7.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat14.xz = u_xlat4.xy * u_xlat7.xy;
					    u_xlat36 = u_xlat14.z + u_xlat14.x;
					    u_xlat36 = u_xlat4.w * u_xlat7.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat36) + u_xlat6.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat4.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat3.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat4.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat4.xz, _LevelRect.xy);
					    u_xlat4.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat6.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat14.xz = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat14.z;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat37 = u_xlat7.x * u_xlat9.y;
					    u_xlat37 = u_xlat7.y * u_xlat37;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat36;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat22.yz = u_xlat8.yx;
					    u_xlat36 = u_xlat6.x * u_xlat9.y;
					    u_xlat36 = u_xlat7.y * u_xlat36;
					    u_xlat36 = u_xlat7.z * u_xlat36;
					    u_xlat36 = u_xlat37 * u_xlat7.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat24.x;
					    u_xlat22.x = u_xlat14.z * u_xlat16.z;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.x = u_xlat6.x * u_xlat9.y;
					    u_xlat24.x = u_xlat6.y * u_xlat24.x;
					    u_xlat37 = u_xlat7.x * u_xlat4.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat36 = u_xlat37 * u_xlat7.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat7.z + u_xlat36;
					    u_xlat4 = textureLod(_PaintTex, u_xlat22.yx, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat22.zx, 0.0);
					    u_xlat8.w = u_xlat22.x;
					    u_xlat36 = u_xlat6.x * u_xlat9.y;
					    u_xlat36 = u_xlat7.y * u_xlat36;
					    u_xlat37 = u_xlat7.x * u_xlat4.y;
					    u_xlat37 = u_xlat7.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat6.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat6.z + u_xlat24.x;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat36 = u_xlat6.x * u_xlat8.y;
					    u_xlat36 = u_xlat6.y * u_xlat36;
					    u_xlat37 = u_xlat7.x * u_xlat4.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat6.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat6.z + u_xlat24.x;
					    u_xlat36 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat36 = u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat4.xyz = vec3(u_xlat36) * u_xlat5.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat4.xyz = u_xlat24.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat24.x = u_xlat1.y + 1.5;
					    u_xlat24.x = floor(u_xlat24.x);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat36 = u_xlat2.z * u_xlat2.x;
					    u_xlat24.x = u_xlat24.x * _AoTexVolume.x + u_xlat14.x;
					    u_xlat2.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat2.x = u_xlat24.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = vec4(u_xlat36) * u_xlat2;
					    u_xlatb24 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb24)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat4.xyz) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" }
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
						vec4 unused_0_19;
						vec4 _LongshipColor;
						vec4 unused_0_21[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_25;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_33;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_37;
						float _Hover;
						vec4 unused_0_39[2];
						vec4 _MainTex_ST;
						vec4 unused_0_41[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat16;
					vec3 u_xlat18;
					vec3 u_xlat23;
					vec2 u_xlat24;
					bool u_xlatb24;
					float u_xlat25;
					vec2 u_xlat33;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat36 = (-u_xlat5.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat14.x = u_xlat4.y * u_xlat37;
					    u_xlat7.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat14.x;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat14.x;
					    u_xlat10 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat37;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat37;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat6.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat5.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat4.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat5.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat5.xy = abs(_SunDir.yx);
					    u_xlat6.xyz = u_xlat5.zxw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat5.x + u_xlat5.y;
					    u_xlat4.xyw = u_xlat4.xyz * u_xlat6.xyz;
					    u_xlat14.x = u_xlat4.y + u_xlat4.x;
					    u_xlat14.x = u_xlat6.z * u_xlat4.z + u_xlat14.x;
					    u_xlat24.x = u_xlat14.x * 0.600000024 + u_xlat24.x;
					    u_xlat14.x = u_xlat36;
					    u_xlat14.x = clamp(u_xlat14.x, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat38 = max(u_xlat36, u_xlat14.x);
					    u_xlat38 = (-u_xlat38) + 1.0;
					    u_xlat5.x = _Year;
					    u_xlat5.y = 0.0;
					    u_xlat5 = textureLod(_GrassTex, u_xlat5.xy, 0.0);
					    u_xlat6.xyz = u_xlat14.xxx * u_xlat5.yzx;
					    u_xlat6.xyz = u_xlat5.xyz * vec3(u_xlat38) + u_xlat6.xyz;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.zxy + u_xlat6.xyz;
					    u_xlat6.xyz = (-u_xlat5.xyz) + _SnowColor.xyz;
					    u_xlat5.xyz = vec3(_SnowAmount) * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat5.xyz * _MinAmbientColor.xyz;
					    u_xlat7.xyz = (-_MinAmbientColor.xyz) * u_xlat5.xyz + _MaxAmbientColor.xyz;
					    u_xlat5.xyz = u_xlat5.xyz + (-_LongshipColor.xyz);
					    u_xlat6.xyz = u_xlat24.xxx * u_xlat7.xyz + u_xlat6.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat7.xyz = u_xlat3.xyw;
					    u_xlat7.xyz = clamp(u_xlat7.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat14.xz = u_xlat4.xy * u_xlat7.xy;
					    u_xlat36 = u_xlat14.z + u_xlat14.x;
					    u_xlat36 = u_xlat4.w * u_xlat7.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat36) + u_xlat6.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat4.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat3.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat4.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat4.xz, _LevelRect.xy);
					    u_xlat4.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat6.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat14.xz = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat14.z;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat9.xy = u_xlat7.xx * u_xlat9.xy;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat9.zw = u_xlat6.xx * u_xlat10.xy;
					    u_xlat9 = u_xlat7.yyyy * u_xlat9;
					    u_xlat33.xy = u_xlat7.zz * u_xlat9.zw;
					    u_xlat9.xy = u_xlat9.xy * u_xlat7.zz + u_xlat33.xy;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat14.z * u_xlat16.z;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat10 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.xy = u_xlat6.xx * u_xlat10.xy;
					    u_xlat24.xy = u_xlat6.yy * u_xlat24.xy;
					    u_xlat14.xz = u_xlat7.xx * u_xlat4.xy;
					    u_xlat14.xz = u_xlat6.yy * u_xlat14.xz;
					    u_xlat14.xz = u_xlat14.xz * u_xlat7.zz + u_xlat9.xy;
					    u_xlat24.xy = u_xlat24.xy * u_xlat7.zz + u_xlat14.xz;
					    u_xlat4 = textureLod(_PaintTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat14.xz = u_xlat6.xx * u_xlat9.xy;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat4.xy = u_xlat7.xx * u_xlat4.xy;
					    u_xlat4.xy = u_xlat7.yy * u_xlat4.xy;
					    u_xlat24.xy = u_xlat4.xy * u_xlat6.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat14.xz * u_xlat6.zz + u_xlat24.xy;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat14.xz = u_xlat6.xx * u_xlat8.xy;
					    u_xlat14.xz = u_xlat6.yy * u_xlat14.xz;
					    u_xlat4.xy = u_xlat7.xx * u_xlat4.xy;
					    u_xlat4.xy = u_xlat6.yy * u_xlat4.xy;
					    u_xlat24.xy = u_xlat4.xy * u_xlat6.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat14.xz * u_xlat6.zz + u_xlat24.xy;
					    u_xlat37 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat37 = u_xlat37;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat4.xyz = vec3(u_xlat37) * u_xlat5.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat4.xyz = u_xlat24.yyy * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat24.x = u_xlat24.x * 1.20000005 + -0.200000003;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat5.xyz = u_xlat4.xyz * _BloodColor.xyz + (-u_xlat4.xyz);
					    u_xlat4.xyz = u_xlat24.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat24.x = u_xlat1.y + 1.5;
					    u_xlat24.x = floor(u_xlat24.x);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat36 = u_xlat2.z * u_xlat2.x;
					    u_xlat24.x = u_xlat24.x * _AoTexVolume.x + u_xlat14.x;
					    u_xlat2.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat2.x = u_xlat24.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = vec4(u_xlat36) * u_xlat2;
					    u_xlatb24 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb24)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat4.xyz) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_19;
						vec4 _LongshipColor;
						vec4 unused_0_21[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_25;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_33;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_37;
						float _Hover;
						vec4 unused_0_39[2];
						vec4 _MainTex_ST;
						vec4 unused_0_41[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat16;
					vec3 u_xlat18;
					vec3 u_xlat23;
					vec2 u_xlat24;
					bool u_xlatb24;
					float u_xlat25;
					vec2 u_xlat33;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat36 = (-u_xlat5.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat14.x = u_xlat4.y * u_xlat37;
					    u_xlat7.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat14.x;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat14.x;
					    u_xlat10 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat37;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat37;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat6.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat5.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat4.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat5.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat5.xy = abs(_SunDir.yx);
					    u_xlat6.xyz = u_xlat5.zxw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat5.x + u_xlat5.y;
					    u_xlat4.xyw = u_xlat4.xyz * u_xlat6.xyz;
					    u_xlat14.x = u_xlat4.y + u_xlat4.x;
					    u_xlat14.x = u_xlat6.z * u_xlat4.z + u_xlat14.x;
					    u_xlat24.x = u_xlat14.x * 0.600000024 + u_xlat24.x;
					    u_xlat14.x = u_xlat36;
					    u_xlat14.x = clamp(u_xlat14.x, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat38 = max(u_xlat36, u_xlat14.x);
					    u_xlat38 = (-u_xlat38) + 1.0;
					    u_xlat5.x = _Year;
					    u_xlat5.y = 0.0;
					    u_xlat5 = textureLod(_GrassTex, u_xlat5.xy, 0.0);
					    u_xlat6.xyz = u_xlat14.xxx * u_xlat5.yzx;
					    u_xlat6.xyz = u_xlat5.xyz * vec3(u_xlat38) + u_xlat6.xyz;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.zxy + u_xlat6.xyz;
					    u_xlat6.xyz = (-u_xlat5.xyz) + _SnowColor.xyz;
					    u_xlat5.xyz = vec3(_SnowAmount) * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat5.xyz * _MinAmbientColor.xyz;
					    u_xlat7.xyz = (-_MinAmbientColor.xyz) * u_xlat5.xyz + _MaxAmbientColor.xyz;
					    u_xlat5.xyz = u_xlat5.xyz + (-_LongshipColor.xyz);
					    u_xlat6.xyz = u_xlat24.xxx * u_xlat7.xyz + u_xlat6.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat7.xyz = u_xlat3.xyw;
					    u_xlat7.xyz = clamp(u_xlat7.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat14.xz = u_xlat4.xy * u_xlat7.xy;
					    u_xlat36 = u_xlat14.z + u_xlat14.x;
					    u_xlat36 = u_xlat4.w * u_xlat7.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat36) + u_xlat6.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat4.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat3.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat4.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat4.xz, _LevelRect.xy);
					    u_xlat4.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat6.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat14.xz = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat14.z;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat9.xy = u_xlat7.xx * u_xlat9.xy;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat9.zw = u_xlat6.xx * u_xlat10.xy;
					    u_xlat9 = u_xlat7.yyyy * u_xlat9;
					    u_xlat33.xy = u_xlat7.zz * u_xlat9.zw;
					    u_xlat9.xy = u_xlat9.xy * u_xlat7.zz + u_xlat33.xy;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat14.z * u_xlat16.z;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat10 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.xy = u_xlat6.xx * u_xlat10.xy;
					    u_xlat24.xy = u_xlat6.yy * u_xlat24.xy;
					    u_xlat14.xz = u_xlat7.xx * u_xlat4.xy;
					    u_xlat14.xz = u_xlat6.yy * u_xlat14.xz;
					    u_xlat14.xz = u_xlat14.xz * u_xlat7.zz + u_xlat9.xy;
					    u_xlat24.xy = u_xlat24.xy * u_xlat7.zz + u_xlat14.xz;
					    u_xlat4 = textureLod(_PaintTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat14.xz = u_xlat6.xx * u_xlat9.xy;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat4.xy = u_xlat7.xx * u_xlat4.xy;
					    u_xlat4.xy = u_xlat7.yy * u_xlat4.xy;
					    u_xlat24.xy = u_xlat4.xy * u_xlat6.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat14.xz * u_xlat6.zz + u_xlat24.xy;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat14.xz = u_xlat6.xx * u_xlat8.xy;
					    u_xlat14.xz = u_xlat6.yy * u_xlat14.xz;
					    u_xlat4.xy = u_xlat7.xx * u_xlat4.xy;
					    u_xlat4.xy = u_xlat6.yy * u_xlat4.xy;
					    u_xlat24.xy = u_xlat4.xy * u_xlat6.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat14.xz * u_xlat6.zz + u_xlat24.xy;
					    u_xlat37 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat37 = u_xlat37;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat4.xyz = vec3(u_xlat37) * u_xlat5.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat4.xyz = u_xlat24.yyy * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat24.x = u_xlat24.x * 1.20000005 + -0.200000003;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat5.xyz = u_xlat4.xyz * _BloodColor.xyz + (-u_xlat4.xyz);
					    u_xlat4.xyz = u_xlat24.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat24.x = u_xlat1.y + 1.5;
					    u_xlat24.x = floor(u_xlat24.x);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat36 = u_xlat2.z * u_xlat2.x;
					    u_xlat24.x = u_xlat24.x * _AoTexVolume.x + u_xlat14.x;
					    u_xlat2.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat2.x = u_xlat24.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = vec4(u_xlat36) * u_xlat2;
					    u_xlatb24 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb24)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat4.xyz) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_LOWEND_ON" }
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
						vec4 unused_0_19;
						vec4 _LongshipColor;
						vec4 unused_0_21[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_25;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_33;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_37;
						float _Hover;
						vec4 unused_0_39[2];
						vec4 _MainTex_ST;
						vec4 unused_0_41[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat16;
					vec3 u_xlat18;
					vec3 u_xlat23;
					vec2 u_xlat24;
					bool u_xlatb24;
					float u_xlat25;
					vec2 u_xlat33;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat36 = (-u_xlat5.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat14.x = u_xlat4.y * u_xlat37;
					    u_xlat7.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat14.x;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat14.x;
					    u_xlat10 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat37;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat37;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat6.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat5.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat4.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat5.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat5.xy = abs(_SunDir.yx);
					    u_xlat6.xyz = u_xlat5.zxw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat5.x + u_xlat5.y;
					    u_xlat4.xyw = u_xlat4.xyz * u_xlat6.xyz;
					    u_xlat14.x = u_xlat4.y + u_xlat4.x;
					    u_xlat14.x = u_xlat6.z * u_xlat4.z + u_xlat14.x;
					    u_xlat24.x = u_xlat14.x * 0.600000024 + u_xlat24.x;
					    u_xlat14.x = u_xlat36;
					    u_xlat14.x = clamp(u_xlat14.x, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat38 = max(u_xlat36, u_xlat14.x);
					    u_xlat38 = (-u_xlat38) + 1.0;
					    u_xlat5.x = _Year;
					    u_xlat5.y = 0.0;
					    u_xlat5 = textureLod(_GrassTex, u_xlat5.xy, 0.0);
					    u_xlat6.xyz = u_xlat14.xxx * u_xlat5.yzx;
					    u_xlat6.xyz = u_xlat5.xyz * vec3(u_xlat38) + u_xlat6.xyz;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.zxy + u_xlat6.xyz;
					    u_xlat6.xyz = (-u_xlat5.xyz) + _SnowColor.xyz;
					    u_xlat5.xyz = vec3(_SnowAmount) * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat5.xyz * _MinAmbientColor.xyz;
					    u_xlat7.xyz = (-_MinAmbientColor.xyz) * u_xlat5.xyz + _MaxAmbientColor.xyz;
					    u_xlat5.xyz = u_xlat5.xyz + (-_LongshipColor.xyz);
					    u_xlat6.xyz = u_xlat24.xxx * u_xlat7.xyz + u_xlat6.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat7.xyz = u_xlat3.xyw;
					    u_xlat7.xyz = clamp(u_xlat7.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat14.xz = u_xlat4.xy * u_xlat7.xy;
					    u_xlat36 = u_xlat14.z + u_xlat14.x;
					    u_xlat36 = u_xlat4.w * u_xlat7.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat36) + u_xlat6.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat4.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat3.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat4.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat4.xz, _LevelRect.xy);
					    u_xlat4.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat6.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat14.xz = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat14.z;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat9.xy = u_xlat7.xx * u_xlat9.xy;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat9.zw = u_xlat6.xx * u_xlat10.xy;
					    u_xlat9 = u_xlat7.yyyy * u_xlat9;
					    u_xlat33.xy = u_xlat7.zz * u_xlat9.zw;
					    u_xlat9.xy = u_xlat9.xy * u_xlat7.zz + u_xlat33.xy;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat14.z * u_xlat16.z;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat10 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.xy = u_xlat6.xx * u_xlat10.xy;
					    u_xlat24.xy = u_xlat6.yy * u_xlat24.xy;
					    u_xlat14.xz = u_xlat7.xx * u_xlat4.xy;
					    u_xlat14.xz = u_xlat6.yy * u_xlat14.xz;
					    u_xlat14.xz = u_xlat14.xz * u_xlat7.zz + u_xlat9.xy;
					    u_xlat24.xy = u_xlat24.xy * u_xlat7.zz + u_xlat14.xz;
					    u_xlat4 = textureLod(_PaintTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat14.xz = u_xlat6.xx * u_xlat9.xy;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat4.xy = u_xlat7.xx * u_xlat4.xy;
					    u_xlat4.xy = u_xlat7.yy * u_xlat4.xy;
					    u_xlat24.xy = u_xlat4.xy * u_xlat6.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat14.xz * u_xlat6.zz + u_xlat24.xy;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat14.xz = u_xlat6.xx * u_xlat8.xy;
					    u_xlat14.xz = u_xlat6.yy * u_xlat14.xz;
					    u_xlat4.xy = u_xlat7.xx * u_xlat4.xy;
					    u_xlat4.xy = u_xlat6.yy * u_xlat4.xy;
					    u_xlat24.xy = u_xlat4.xy * u_xlat6.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat14.xz * u_xlat6.zz + u_xlat24.xy;
					    u_xlat37 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat37 = u_xlat37;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat4.xyz = vec3(u_xlat37) * u_xlat5.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat4.xyz = u_xlat24.yyy * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat24.x = u_xlat24.x * 1.20000005 + -0.200000003;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat5.xyz = u_xlat4.xyz * _BloodColor.xyz + (-u_xlat4.xyz);
					    u_xlat4.xyz = u_xlat24.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat24.x = u_xlat1.y + 1.5;
					    u_xlat24.x = floor(u_xlat24.x);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat36 = u_xlat2.z * u_xlat2.x;
					    u_xlat24.x = u_xlat24.x * _AoTexVolume.x + u_xlat14.x;
					    u_xlat2.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat2.x = u_xlat24.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = vec4(u_xlat36) * u_xlat2;
					    u_xlatb24 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb24)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat4.xyz) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_19;
						vec4 _LongshipColor;
						vec4 unused_0_21[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_25;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_33;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_37;
						float _Hover;
						vec4 unused_0_39[2];
						vec4 _MainTex_ST;
						vec4 unused_0_41[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat16;
					vec3 u_xlat18;
					vec3 u_xlat23;
					vec2 u_xlat24;
					bool u_xlatb24;
					float u_xlat25;
					vec2 u_xlat33;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat36 = (-u_xlat5.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat14.x = u_xlat4.y * u_xlat37;
					    u_xlat7.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat14.x;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat14.x;
					    u_xlat10 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat37;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat37;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_AoTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat6.xxx * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat5.yyy * u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat4.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat4.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat7 = textureLod(_AoTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat4.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat4.xyz;
					    u_xlat5.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat5.xy = abs(_SunDir.yx);
					    u_xlat6.xyz = u_xlat5.zxw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat5.x + u_xlat5.y;
					    u_xlat4.xyw = u_xlat4.xyz * u_xlat6.xyz;
					    u_xlat14.x = u_xlat4.y + u_xlat4.x;
					    u_xlat14.x = u_xlat6.z * u_xlat4.z + u_xlat14.x;
					    u_xlat24.x = u_xlat14.x * 0.600000024 + u_xlat24.x;
					    u_xlat14.x = u_xlat36;
					    u_xlat14.x = clamp(u_xlat14.x, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat38 = max(u_xlat36, u_xlat14.x);
					    u_xlat38 = (-u_xlat38) + 1.0;
					    u_xlat5.x = _Year;
					    u_xlat5.y = 0.0;
					    u_xlat5 = textureLod(_GrassTex, u_xlat5.xy, 0.0);
					    u_xlat6.xyz = u_xlat14.xxx * u_xlat5.yzx;
					    u_xlat6.xyz = u_xlat5.xyz * vec3(u_xlat38) + u_xlat6.xyz;
					    u_xlat5.xyz = vec3(u_xlat36) * u_xlat5.zxy + u_xlat6.xyz;
					    u_xlat6.xyz = (-u_xlat5.xyz) + _SnowColor.xyz;
					    u_xlat5.xyz = vec3(_SnowAmount) * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat6.xyz = u_xlat5.xyz * _MinAmbientColor.xyz;
					    u_xlat7.xyz = (-_MinAmbientColor.xyz) * u_xlat5.xyz + _MaxAmbientColor.xyz;
					    u_xlat5.xyz = u_xlat5.xyz + (-_LongshipColor.xyz);
					    u_xlat6.xyz = u_xlat24.xxx * u_xlat7.xyz + u_xlat6.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat7.xyz = u_xlat3.xyw;
					    u_xlat7.xyz = clamp(u_xlat7.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat14.xz = u_xlat4.xy * u_xlat7.xy;
					    u_xlat36 = u_xlat14.z + u_xlat14.x;
					    u_xlat36 = u_xlat4.w * u_xlat7.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat3.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(u_xlat36) + u_xlat6.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat4.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat3.xyz;
					    vs_COLOR0.xyz = u_xlat3.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat4.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat4.xz, _LevelRect.xy);
					    u_xlat4.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat6.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat14.xz = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat14.z;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat9.xy = u_xlat7.xx * u_xlat9.xy;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat9.zw = u_xlat6.xx * u_xlat10.xy;
					    u_xlat9 = u_xlat7.yyyy * u_xlat9;
					    u_xlat33.xy = u_xlat7.zz * u_xlat9.zw;
					    u_xlat9.xy = u_xlat9.xy * u_xlat7.zz + u_xlat33.xy;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat14.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat14.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat14.z * u_xlat16.z;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat10 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.xy = u_xlat6.xx * u_xlat10.xy;
					    u_xlat24.xy = u_xlat6.yy * u_xlat24.xy;
					    u_xlat14.xz = u_xlat7.xx * u_xlat4.xy;
					    u_xlat14.xz = u_xlat6.yy * u_xlat14.xz;
					    u_xlat14.xz = u_xlat14.xz * u_xlat7.zz + u_xlat9.xy;
					    u_xlat24.xy = u_xlat24.xy * u_xlat7.zz + u_xlat14.xz;
					    u_xlat4 = textureLod(_PaintTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat14.xz = u_xlat6.xx * u_xlat9.xy;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat4.xy = u_xlat7.xx * u_xlat4.xy;
					    u_xlat4.xy = u_xlat7.yy * u_xlat4.xy;
					    u_xlat24.xy = u_xlat4.xy * u_xlat6.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat14.xz * u_xlat6.zz + u_xlat24.xy;
					    u_xlat4 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat14.xz = u_xlat6.xx * u_xlat8.xy;
					    u_xlat14.xz = u_xlat6.yy * u_xlat14.xz;
					    u_xlat4.xy = u_xlat7.xx * u_xlat4.xy;
					    u_xlat4.xy = u_xlat6.yy * u_xlat4.xy;
					    u_xlat24.xy = u_xlat4.xy * u_xlat6.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat14.xz * u_xlat6.zz + u_xlat24.xy;
					    u_xlat37 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat37 = u_xlat37;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat4.xyz = vec3(u_xlat37) * u_xlat5.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat4.xyz = u_xlat24.yyy * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat24.x = u_xlat24.x * 1.20000005 + -0.200000003;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat5.xyz = u_xlat4.xyz * _BloodColor.xyz + (-u_xlat4.xyz);
					    u_xlat4.xyz = u_xlat24.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat24.x = u_xlat1.y + 1.5;
					    u_xlat24.x = floor(u_xlat24.x);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat36 = u_xlat2.z * u_xlat2.x;
					    u_xlat24.x = u_xlat24.x * _AoTexVolume.x + u_xlat14.x;
					    u_xlat2.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat2.x = u_xlat24.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = vec4(u_xlat36) * u_xlat2;
					    u_xlatb24 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb24)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat4.xyz) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat4.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[37];
						float _Year;
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(-0.0300000012, -0.0300000012, -0.0300000012, -0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[37];
						float _Year;
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(-0.0300000012, -0.0300000012, -0.0300000012, -0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_LOWEND_ON" }
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
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(-0.0300000012, -0.0300000012, -0.0300000012, -0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(-0.0300000012, -0.0300000012, -0.0300000012, -0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" }
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
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(-0.0300000012, -0.0300000012, -0.0300000012, -0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(-0.0300000012, -0.0300000012, -0.0300000012, -0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_LOWEND_ON" }
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
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(-0.0300000012, -0.0300000012, -0.0300000012, -0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_5[3];
						vec4 _LongshipColor;
						vec4 unused_0_7[3];
						vec4 _SnowColor;
						vec4 unused_0_9;
						float _SnowAmount;
						vec4 unused_0_11[11];
						float _Hover;
						vec4 unused_0_13[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_16;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(-0.0300000012, -0.0300000012, -0.0300000012, -0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat15 = u_xlat0.y + 1.5;
					    u_xlat15 = floor(u_xlat15);
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat2.xy = floor(u_xlat0.xz);
					    u_xlat0.xz = fract(u_xlat0.xz);
					    u_xlat0.xz = u_xlat0.xz + vec2(-0.5, -0.5);
					    u_xlat0.xz = -abs(u_xlat0.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat0.xz = u_xlat0.xz * vec2(3.0, 3.0);
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat0.x = u_xlat0.z * u_xlat0.x;
					    u_xlat10.x = u_xlat15 * _AoTexVolume.x + u_xlat2.x;
					    u_xlat2.y = u_xlat2.y / _AoTexSize.y;
					    u_xlat2.x = u_xlat10.x / _AoTexSize.x;
					    u_xlat2 = textureLod(_HighlightTex, u_xlat2.xy, 0.0);
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlatb0 = 0.0<u_xlat2.w;
					    u_xlat3.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat0.xzw = (bool(u_xlatb0)) ? u_xlat3.xyz : u_xlat2.xyz;
					    u_xlat16 = _AoTexVolume.y + -2.0;
					    u_xlat5 = (-u_xlat16) * 0.5 + u_xlat0.y;
					    u_xlat5 = u_xlat5 * 0.25;
					    u_xlat16 = u_xlat5;
					    u_xlat16 = clamp(u_xlat16, 0.0, 1.0);
					    u_xlat5 = (-u_xlat5);
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.x = max(u_xlat5, u_xlat16);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat3.x = _Year;
					    u_xlat3.y = 0.0;
					    u_xlat3 = textureLod(_GrassTex, u_xlat3.xy, 0.0);
					    u_xlat4.xyz = vec3(u_xlat16) * u_xlat3.yzx;
					    u_xlat2.xyz = u_xlat3.xyz * u_xlat2.xxx + u_xlat4.xyz;
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat3.zxy + u_xlat2.xyz;
					    u_xlat3.xyz = (-u_xlat2.xyz) + _SnowColor.xyz;
					    u_xlat2.xyz = vec3(_SnowAmount) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat2.xyz + (-_LongshipColor.xyz);
					    u_xlat5 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat5 = u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat5) * u_xlat2.xyz + _LongshipColor.xyz;
					    u_xlat0.xyz = (-u_xlat2.xyz) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = u_xlat1.xyz * u_xlat2.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat0.xyz + u_xlat1.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat10.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat10.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
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
						vec4 unused_0_6[2];
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec3 _PaintTexOffset;
						vec4 _LevelRect;
						vec4 unused_0_11[19];
						vec3 _SunDir;
						vec4 unused_0_13[7];
						float _Year;
						vec4 unused_0_15[3];
						vec4 _LongshipColor;
						vec4 unused_0_17[3];
						vec4 _SnowColor;
						vec4 unused_0_19;
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
						vec4 unused_0_31[3];
						float _Hover;
						vec4 unused_0_33[2];
						vec4 _MainTex_ST;
						vec4 unused_0_35[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat17;
					vec3 u_xlat18;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat36;
					float u_xlat37;
					bool u_xlatb37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.z = unity_MatrixV[1].z;
					    u_xlat4.x = unity_MatrixV[0].z;
					    u_xlat4.z = unity_MatrixV[2].z;
					    u_xlat3.xy = (-u_xlat4.zx);
					    u_xlat5.xyz = u_xlat1.yzx * u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat3.zxy * u_xlat1.zxy + (-u_xlat5.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat4.y = unity_MatrixV[1].z;
					    u_xlat2.xyz = (-u_xlat4.xyz) * vec3(-0.0150000006, 0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat24.x = _WaterLevel * 2.0 + (-u_xlat1.y);
					    u_xlat2 = u_xlat24.xxxx * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat23.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat6.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat5.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat3.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat5.xyz = (-u_xlat4.xyz) * vec3(-1.0, 1.0, -1.0) + (-u_xlat3.xyz);
					    u_xlat3.xyz = u_xlat5.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat24.xy = u_xlat4.xz / unity_MatrixV[1].zz;
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat4.xyz);
					    u_xlat37 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat37 = u_xlat37 * 0.25;
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat14.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat3.y * u_xlat14.x;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat38;
					    u_xlat7.z = u_xlat3.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat38;
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat22.yz = u_xlat7.yx;
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.zzz * u_xlat9.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat9.xyz;
					    u_xlat14.x = u_xlat14.x * u_xlat15.y;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat14.x;
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat14.x;
					    u_xlat22.x = u_xlat15.z * u_xlat6.y;
					    u_xlat3 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat6.xyz = u_xlat4.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat5.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat4.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat22.yx, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat22.zx, 0.0);
					    u_xlat7.w = u_xlat22.x;
					    u_xlat8.xyz = u_xlat4.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat8.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat4.y = abs(_SunDir.y);
					    u_xlat4.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xyz = u_xlat4.xyz;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat14.xz = u_xlat3.xy * u_xlat4.xy;
					    u_xlat14.x = u_xlat14.z + u_xlat14.x;
					    u_xlat14.x = u_xlat4.z * u_xlat3.z + u_xlat14.x;
					    u_xlat14.x = u_xlat14.x * 0.600000024 + 0.200000003;
					    u_xlat38 = u_xlat37;
					    u_xlat38 = clamp(u_xlat38, 0.0, 1.0);
					    u_xlat37 = (-u_xlat37);
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.x = max(u_xlat37, u_xlat38);
					    u_xlat3.x = (-u_xlat3.x) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat15.xyz = vec3(u_xlat38) * u_xlat4.yzx;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xxx + u_xlat15.xyz;
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat4.zxy + u_xlat3.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _SnowColor.xyz;
					    u_xlat3.xyz = vec3(_SnowAmount) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * _MinAmbientColor.xyz;
					    u_xlat5.xyz = (-_MinAmbientColor.xyz) * u_xlat3.xyz + _MaxAmbientColor.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + (-_LongshipColor.xyz);
					    u_xlat4.xyz = u_xlat14.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat4.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat5.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat14.xz = max(u_xlat5.xz, _LevelRect.xy);
					    u_xlat5.xz = min(u_xlat14.xz, _LevelRect.zw);
					    u_xlat5.xyz = max(u_xlat5.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = min(u_xlat5.xyz, u_xlat6.xyz);
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat5.xyz);
					    u_xlat5.xyz = floor(u_xlat5.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat14.x = u_xlat5.y * u_xlat37;
					    u_xlat8.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat14.x;
					    u_xlat9.z = u_xlat5.z * u_xlat8.y;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat14.z = u_xlat7.x * u_xlat10.y;
					    u_xlat17.xyz = u_xlat5.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat14.x;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat23.yz = u_xlat9.yx;
					    u_xlat14.x = u_xlat6.x * u_xlat10.y;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat14.x = u_xlat7.z * u_xlat14.x;
					    u_xlat14.x = u_xlat14.z * u_xlat7.z + u_xlat14.x;
					    u_xlat37 = u_xlat37 * u_xlat17.y;
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat37;
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat37;
					    u_xlat23.x = u_xlat17.z * u_xlat8.y;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat37 = u_xlat6.x * u_xlat8.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat38 = u_xlat7.x * u_xlat5.y;
					    u_xlat38 = u_xlat6.y * u_xlat38;
					    u_xlat14.x = u_xlat38 * u_xlat7.z + u_xlat14.x;
					    u_xlat37 = u_xlat37 * u_xlat7.z + u_xlat14.x;
					    u_xlat5 = textureLod(_PaintTex, u_xlat23.yx, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat23.zx, 0.0);
					    u_xlat9.w = u_xlat23.x;
					    u_xlat14.x = u_xlat6.x * u_xlat8.y;
					    u_xlat14.z = u_xlat7.x * u_xlat5.y;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat37 = u_xlat14.z * u_xlat6.z + u_xlat37;
					    u_xlat37 = u_xlat14.x * u_xlat6.z + u_xlat37;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xw, 0.0);
					    u_xlat14.x = u_xlat6.x * u_xlat8.y;
					    u_xlat14.z = u_xlat7.x * u_xlat5.y;
					    u_xlat14.xz = u_xlat6.yy * u_xlat14.xz;
					    u_xlat37 = u_xlat14.z * u_xlat6.z + u_xlat37;
					    u_xlat37 = u_xlat14.x * u_xlat6.z + u_xlat37;
					    u_xlat14.x = in_COLOR0.w + in_COLOR0.w;
					    u_xlat14.x = u_xlat14.x;
					    u_xlat14.x = clamp(u_xlat14.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat14.xxx * u_xlat3.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat37 = u_xlat1.y + 1.5;
					    u_xlat37 = floor(u_xlat37);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat2.x = u_xlat2.z * u_xlat2.x;
					    u_xlat37 = u_xlat37 * _AoTexVolume.x + u_xlat14.x;
					    u_xlat5.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat5.x = u_xlat37 / _AoTexSize.x;
					    u_xlat5 = textureLod(_HighlightTex, u_xlat5.xy, 0.0);
					    u_xlat2 = u_xlat2.xxxx * u_xlat5;
					    u_xlatb37 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb37)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat37 = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat37;
					    u_xlat13 = u_xlat1.y + (-_WaterLevel);
					    u_xlat24.xy = u_xlat24.xy * vec2(u_xlat13) + u_xlat1.xz;
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + (-_FogMinRad);
					    u_xlat36 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat24.x / u_xlat36;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat24.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat36 = _AAFactor + 1.0;
					    u_xlat24.x = u_xlat36 * u_xlat24.x;
					    u_xlat24.x = u_xlat24.x * _LineWidth;
					    u_xlat24.x = u_xlat13 / u_xlat24.x;
					    u_xlat24.x = u_xlat24.x + 0.5;
					    u_xlat36 = (-u_xlat24.x) + 2.0;
					    vs_TEXCOORD3.y = u_xlat37 * u_xlat36 + u_xlat24.x;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
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
						vec4 unused_0_6[2];
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec3 _PaintTexOffset;
						vec4 _LevelRect;
						vec4 unused_0_11[19];
						vec3 _SunDir;
						vec4 unused_0_13[7];
						float _Year;
						vec4 unused_0_15[3];
						vec4 _LongshipColor;
						vec4 unused_0_17[3];
						vec4 _SnowColor;
						vec4 unused_0_19;
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
						vec4 unused_0_31[3];
						float _Hover;
						vec4 unused_0_33[2];
						vec4 _MainTex_ST;
						vec4 unused_0_35[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat17;
					vec3 u_xlat18;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat36;
					float u_xlat37;
					bool u_xlatb37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.z = unity_MatrixV[1].z;
					    u_xlat4.x = unity_MatrixV[0].z;
					    u_xlat4.z = unity_MatrixV[2].z;
					    u_xlat3.xy = (-u_xlat4.zx);
					    u_xlat5.xyz = u_xlat1.yzx * u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat3.zxy * u_xlat1.zxy + (-u_xlat5.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat4.y = unity_MatrixV[1].z;
					    u_xlat2.xyz = (-u_xlat4.xyz) * vec3(-0.0150000006, 0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat24.x = _WaterLevel * 2.0 + (-u_xlat1.y);
					    u_xlat2 = u_xlat24.xxxx * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat23.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat6.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat5.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat3.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat5.xyz = (-u_xlat4.xyz) * vec3(-1.0, 1.0, -1.0) + (-u_xlat3.xyz);
					    u_xlat3.xyz = u_xlat5.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat24.xy = u_xlat4.xz / unity_MatrixV[1].zz;
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat4.xyz);
					    u_xlat37 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat37 = u_xlat37 * 0.25;
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat14.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat3.y * u_xlat14.x;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat38;
					    u_xlat7.z = u_xlat3.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat38;
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat22.yz = u_xlat7.yx;
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.zzz * u_xlat9.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat9.xyz;
					    u_xlat14.x = u_xlat14.x * u_xlat15.y;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat14.x;
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat14.x;
					    u_xlat22.x = u_xlat15.z * u_xlat6.y;
					    u_xlat3 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat6.xyz = u_xlat4.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat5.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat4.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat22.yx, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat22.zx, 0.0);
					    u_xlat7.w = u_xlat22.x;
					    u_xlat8.xyz = u_xlat4.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat8.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat4.y = abs(_SunDir.y);
					    u_xlat4.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xyz = u_xlat4.xyz;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat14.xz = u_xlat3.xy * u_xlat4.xy;
					    u_xlat14.x = u_xlat14.z + u_xlat14.x;
					    u_xlat14.x = u_xlat4.z * u_xlat3.z + u_xlat14.x;
					    u_xlat14.x = u_xlat14.x * 0.600000024 + 0.200000003;
					    u_xlat38 = u_xlat37;
					    u_xlat38 = clamp(u_xlat38, 0.0, 1.0);
					    u_xlat37 = (-u_xlat37);
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.x = max(u_xlat37, u_xlat38);
					    u_xlat3.x = (-u_xlat3.x) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat15.xyz = vec3(u_xlat38) * u_xlat4.yzx;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xxx + u_xlat15.xyz;
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat4.zxy + u_xlat3.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _SnowColor.xyz;
					    u_xlat3.xyz = vec3(_SnowAmount) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * _MinAmbientColor.xyz;
					    u_xlat5.xyz = (-_MinAmbientColor.xyz) * u_xlat3.xyz + _MaxAmbientColor.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + (-_LongshipColor.xyz);
					    u_xlat4.xyz = u_xlat14.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat4.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat5.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat14.xz = max(u_xlat5.xz, _LevelRect.xy);
					    u_xlat5.xz = min(u_xlat14.xz, _LevelRect.zw);
					    u_xlat5.xyz = max(u_xlat5.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = min(u_xlat5.xyz, u_xlat6.xyz);
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat5.xyz);
					    u_xlat5.xyz = floor(u_xlat5.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat14.x = u_xlat5.y * u_xlat37;
					    u_xlat8.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat14.x;
					    u_xlat9.z = u_xlat5.z * u_xlat8.y;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat14.z = u_xlat7.x * u_xlat10.y;
					    u_xlat17.xyz = u_xlat5.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat14.x;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat23.yz = u_xlat9.yx;
					    u_xlat14.x = u_xlat6.x * u_xlat10.y;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat14.x = u_xlat7.z * u_xlat14.x;
					    u_xlat14.x = u_xlat14.z * u_xlat7.z + u_xlat14.x;
					    u_xlat37 = u_xlat37 * u_xlat17.y;
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat37;
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat37;
					    u_xlat23.x = u_xlat17.z * u_xlat8.y;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat37 = u_xlat6.x * u_xlat8.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat38 = u_xlat7.x * u_xlat5.y;
					    u_xlat38 = u_xlat6.y * u_xlat38;
					    u_xlat14.x = u_xlat38 * u_xlat7.z + u_xlat14.x;
					    u_xlat37 = u_xlat37 * u_xlat7.z + u_xlat14.x;
					    u_xlat5 = textureLod(_PaintTex, u_xlat23.yx, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat23.zx, 0.0);
					    u_xlat9.w = u_xlat23.x;
					    u_xlat14.x = u_xlat6.x * u_xlat8.y;
					    u_xlat14.z = u_xlat7.x * u_xlat5.y;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat37 = u_xlat14.z * u_xlat6.z + u_xlat37;
					    u_xlat37 = u_xlat14.x * u_xlat6.z + u_xlat37;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xw, 0.0);
					    u_xlat14.x = u_xlat6.x * u_xlat8.y;
					    u_xlat14.z = u_xlat7.x * u_xlat5.y;
					    u_xlat14.xz = u_xlat6.yy * u_xlat14.xz;
					    u_xlat37 = u_xlat14.z * u_xlat6.z + u_xlat37;
					    u_xlat37 = u_xlat14.x * u_xlat6.z + u_xlat37;
					    u_xlat14.x = in_COLOR0.w + in_COLOR0.w;
					    u_xlat14.x = u_xlat14.x;
					    u_xlat14.x = clamp(u_xlat14.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat14.xxx * u_xlat3.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat37 = u_xlat1.y + 1.5;
					    u_xlat37 = floor(u_xlat37);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat2.x = u_xlat2.z * u_xlat2.x;
					    u_xlat37 = u_xlat37 * _AoTexVolume.x + u_xlat14.x;
					    u_xlat5.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat5.x = u_xlat37 / _AoTexSize.x;
					    u_xlat5 = textureLod(_HighlightTex, u_xlat5.xy, 0.0);
					    u_xlat2 = u_xlat2.xxxx * u_xlat5;
					    u_xlatb37 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb37)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat37 = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat37;
					    u_xlat13 = u_xlat1.y + (-_WaterLevel);
					    u_xlat24.xy = u_xlat24.xy * vec2(u_xlat13) + u_xlat1.xz;
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + (-_FogMinRad);
					    u_xlat36 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat24.x / u_xlat36;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat24.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat36 = _AAFactor + 1.0;
					    u_xlat24.x = u_xlat36 * u_xlat24.x;
					    u_xlat24.x = u_xlat24.x * _LineWidth;
					    u_xlat24.x = u_xlat13 / u_xlat24.x;
					    u_xlat24.x = u_xlat24.x + 0.5;
					    u_xlat36 = (-u_xlat24.x) + 2.0;
					    vs_TEXCOORD3.y = u_xlat37 * u_xlat36 + u_xlat24.x;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_LOWEND_ON" }
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
						vec4 unused_0_13[7];
						float _Year;
						vec4 unused_0_15[3];
						vec4 _LongshipColor;
						vec4 unused_0_17[3];
						vec4 _SnowColor;
						vec4 unused_0_19;
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
						vec4 unused_0_31[3];
						float _Hover;
						vec4 unused_0_33[2];
						vec4 _MainTex_ST;
						vec4 unused_0_35[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat17;
					vec3 u_xlat18;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat36;
					float u_xlat37;
					bool u_xlatb37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.z = unity_MatrixV[1].z;
					    u_xlat4.x = unity_MatrixV[0].z;
					    u_xlat4.z = unity_MatrixV[2].z;
					    u_xlat3.xy = (-u_xlat4.zx);
					    u_xlat5.xyz = u_xlat1.yzx * u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat3.zxy * u_xlat1.zxy + (-u_xlat5.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat4.y = unity_MatrixV[1].z;
					    u_xlat2.xyz = (-u_xlat4.xyz) * vec3(-0.0150000006, 0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat24.x = _WaterLevel * 2.0 + (-u_xlat1.y);
					    u_xlat2 = u_xlat24.xxxx * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat23.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat6.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat5.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat3.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat5.xyz = (-u_xlat4.xyz) * vec3(-1.0, 1.0, -1.0) + (-u_xlat3.xyz);
					    u_xlat3.xyz = u_xlat5.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat24.xy = u_xlat4.xz / unity_MatrixV[1].zz;
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat4.xyz);
					    u_xlat37 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat37 = u_xlat37 * 0.25;
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat14.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat3.y * u_xlat14.x;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat38;
					    u_xlat7.z = u_xlat3.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat38;
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat22.yz = u_xlat7.yx;
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.zzz * u_xlat9.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat9.xyz;
					    u_xlat14.x = u_xlat14.x * u_xlat15.y;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat14.x;
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat14.x;
					    u_xlat22.x = u_xlat15.z * u_xlat6.y;
					    u_xlat3 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat6.xyz = u_xlat4.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat5.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat4.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat22.yx, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat22.zx, 0.0);
					    u_xlat7.w = u_xlat22.x;
					    u_xlat8.xyz = u_xlat4.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat8.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat4.y = abs(_SunDir.y);
					    u_xlat4.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xyz = u_xlat4.xyz;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat14.xz = u_xlat3.xy * u_xlat4.xy;
					    u_xlat14.x = u_xlat14.z + u_xlat14.x;
					    u_xlat14.x = u_xlat4.z * u_xlat3.z + u_xlat14.x;
					    u_xlat14.x = u_xlat14.x * 0.600000024 + 0.200000003;
					    u_xlat38 = u_xlat37;
					    u_xlat38 = clamp(u_xlat38, 0.0, 1.0);
					    u_xlat37 = (-u_xlat37);
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.x = max(u_xlat37, u_xlat38);
					    u_xlat3.x = (-u_xlat3.x) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat15.xyz = vec3(u_xlat38) * u_xlat4.yzx;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xxx + u_xlat15.xyz;
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat4.zxy + u_xlat3.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _SnowColor.xyz;
					    u_xlat3.xyz = vec3(_SnowAmount) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * _MinAmbientColor.xyz;
					    u_xlat5.xyz = (-_MinAmbientColor.xyz) * u_xlat3.xyz + _MaxAmbientColor.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + (-_LongshipColor.xyz);
					    u_xlat4.xyz = u_xlat14.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat4.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat5.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat14.xz = max(u_xlat5.xz, _LevelRect.xy);
					    u_xlat5.xz = min(u_xlat14.xz, _LevelRect.zw);
					    u_xlat5.xyz = max(u_xlat5.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = min(u_xlat5.xyz, u_xlat6.xyz);
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat5.xyz);
					    u_xlat5.xyz = floor(u_xlat5.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat14.x = u_xlat5.y * u_xlat37;
					    u_xlat8.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat14.x;
					    u_xlat9.z = u_xlat5.z * u_xlat8.y;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat14.z = u_xlat7.x * u_xlat10.y;
					    u_xlat17.xyz = u_xlat5.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat14.x;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat23.yz = u_xlat9.yx;
					    u_xlat14.x = u_xlat6.x * u_xlat10.y;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat14.x = u_xlat7.z * u_xlat14.x;
					    u_xlat14.x = u_xlat14.z * u_xlat7.z + u_xlat14.x;
					    u_xlat37 = u_xlat37 * u_xlat17.y;
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat37;
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat37;
					    u_xlat23.x = u_xlat17.z * u_xlat8.y;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat37 = u_xlat6.x * u_xlat8.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat38 = u_xlat7.x * u_xlat5.y;
					    u_xlat38 = u_xlat6.y * u_xlat38;
					    u_xlat14.x = u_xlat38 * u_xlat7.z + u_xlat14.x;
					    u_xlat37 = u_xlat37 * u_xlat7.z + u_xlat14.x;
					    u_xlat5 = textureLod(_PaintTex, u_xlat23.yx, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat23.zx, 0.0);
					    u_xlat9.w = u_xlat23.x;
					    u_xlat14.x = u_xlat6.x * u_xlat8.y;
					    u_xlat14.z = u_xlat7.x * u_xlat5.y;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat37 = u_xlat14.z * u_xlat6.z + u_xlat37;
					    u_xlat37 = u_xlat14.x * u_xlat6.z + u_xlat37;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xw, 0.0);
					    u_xlat14.x = u_xlat6.x * u_xlat8.y;
					    u_xlat14.z = u_xlat7.x * u_xlat5.y;
					    u_xlat14.xz = u_xlat6.yy * u_xlat14.xz;
					    u_xlat37 = u_xlat14.z * u_xlat6.z + u_xlat37;
					    u_xlat37 = u_xlat14.x * u_xlat6.z + u_xlat37;
					    u_xlat14.x = in_COLOR0.w + in_COLOR0.w;
					    u_xlat14.x = u_xlat14.x;
					    u_xlat14.x = clamp(u_xlat14.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat14.xxx * u_xlat3.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat37 = u_xlat1.y + 1.5;
					    u_xlat37 = floor(u_xlat37);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat2.x = u_xlat2.z * u_xlat2.x;
					    u_xlat37 = u_xlat37 * _AoTexVolume.x + u_xlat14.x;
					    u_xlat5.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat5.x = u_xlat37 / _AoTexSize.x;
					    u_xlat5 = textureLod(_HighlightTex, u_xlat5.xy, 0.0);
					    u_xlat2 = u_xlat2.xxxx * u_xlat5;
					    u_xlatb37 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb37)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat37 = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat37;
					    u_xlat13 = u_xlat1.y + (-_WaterLevel);
					    u_xlat24.xy = u_xlat24.xy * vec2(u_xlat13) + u_xlat1.xz;
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + (-_FogMinRad);
					    u_xlat36 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat24.x / u_xlat36;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat24.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat36 = _AAFactor + 1.0;
					    u_xlat24.x = u_xlat36 * u_xlat24.x;
					    u_xlat24.x = u_xlat24.x * _LineWidth;
					    u_xlat24.x = u_xlat13 / u_xlat24.x;
					    u_xlat24.x = u_xlat24.x + 0.5;
					    u_xlat36 = (-u_xlat24.x) + 2.0;
					    vs_TEXCOORD3.y = u_xlat37 * u_xlat36 + u_xlat24.x;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_13[7];
						float _Year;
						vec4 unused_0_15[3];
						vec4 _LongshipColor;
						vec4 unused_0_17[3];
						vec4 _SnowColor;
						vec4 unused_0_19;
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
						vec4 unused_0_31[3];
						float _Hover;
						vec4 unused_0_33[2];
						vec4 _MainTex_ST;
						vec4 unused_0_35[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat17;
					vec3 u_xlat18;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat36;
					float u_xlat37;
					bool u_xlatb37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.z = unity_MatrixV[1].z;
					    u_xlat4.x = unity_MatrixV[0].z;
					    u_xlat4.z = unity_MatrixV[2].z;
					    u_xlat3.xy = (-u_xlat4.zx);
					    u_xlat5.xyz = u_xlat1.yzx * u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat3.zxy * u_xlat1.zxy + (-u_xlat5.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat4.y = unity_MatrixV[1].z;
					    u_xlat2.xyz = (-u_xlat4.xyz) * vec3(-0.0150000006, 0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat24.x = _WaterLevel * 2.0 + (-u_xlat1.y);
					    u_xlat2 = u_xlat24.xxxx * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat23.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat6.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat5.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat3.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat5.xyz = (-u_xlat4.xyz) * vec3(-1.0, 1.0, -1.0) + (-u_xlat3.xyz);
					    u_xlat3.xyz = u_xlat5.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat24.xy = u_xlat4.xz / unity_MatrixV[1].zz;
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat4.xyz);
					    u_xlat37 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat37 = u_xlat37 * 0.25;
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat14.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat3.y * u_xlat14.x;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat38;
					    u_xlat7.z = u_xlat3.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat38;
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat22.yz = u_xlat7.yx;
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.zzz * u_xlat9.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat9.xyz;
					    u_xlat14.x = u_xlat14.x * u_xlat15.y;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat14.x;
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat14.x;
					    u_xlat22.x = u_xlat15.z * u_xlat6.y;
					    u_xlat3 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat6.xyz = u_xlat4.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat5.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat4.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat22.yx, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat22.zx, 0.0);
					    u_xlat7.w = u_xlat22.x;
					    u_xlat8.xyz = u_xlat4.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat8.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat4.y = abs(_SunDir.y);
					    u_xlat4.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xyz = u_xlat4.xyz;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat14.xz = u_xlat3.xy * u_xlat4.xy;
					    u_xlat14.x = u_xlat14.z + u_xlat14.x;
					    u_xlat14.x = u_xlat4.z * u_xlat3.z + u_xlat14.x;
					    u_xlat14.x = u_xlat14.x * 0.600000024 + 0.200000003;
					    u_xlat38 = u_xlat37;
					    u_xlat38 = clamp(u_xlat38, 0.0, 1.0);
					    u_xlat37 = (-u_xlat37);
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.x = max(u_xlat37, u_xlat38);
					    u_xlat3.x = (-u_xlat3.x) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat15.xyz = vec3(u_xlat38) * u_xlat4.yzx;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xxx + u_xlat15.xyz;
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat4.zxy + u_xlat3.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _SnowColor.xyz;
					    u_xlat3.xyz = vec3(_SnowAmount) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * _MinAmbientColor.xyz;
					    u_xlat5.xyz = (-_MinAmbientColor.xyz) * u_xlat3.xyz + _MaxAmbientColor.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + (-_LongshipColor.xyz);
					    u_xlat4.xyz = u_xlat14.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat4.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat5.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat14.xz = max(u_xlat5.xz, _LevelRect.xy);
					    u_xlat5.xz = min(u_xlat14.xz, _LevelRect.zw);
					    u_xlat5.xyz = max(u_xlat5.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = min(u_xlat5.xyz, u_xlat6.xyz);
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat5.xyz);
					    u_xlat5.xyz = floor(u_xlat5.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat14.x = u_xlat5.y * u_xlat37;
					    u_xlat8.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat14.x;
					    u_xlat9.z = u_xlat5.z * u_xlat8.y;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat14.z = u_xlat7.x * u_xlat10.y;
					    u_xlat17.xyz = u_xlat5.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat14.x;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat23.yz = u_xlat9.yx;
					    u_xlat14.x = u_xlat6.x * u_xlat10.y;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat14.x = u_xlat7.z * u_xlat14.x;
					    u_xlat14.x = u_xlat14.z * u_xlat7.z + u_xlat14.x;
					    u_xlat37 = u_xlat37 * u_xlat17.y;
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat37;
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat37;
					    u_xlat23.x = u_xlat17.z * u_xlat8.y;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat37 = u_xlat6.x * u_xlat8.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat38 = u_xlat7.x * u_xlat5.y;
					    u_xlat38 = u_xlat6.y * u_xlat38;
					    u_xlat14.x = u_xlat38 * u_xlat7.z + u_xlat14.x;
					    u_xlat37 = u_xlat37 * u_xlat7.z + u_xlat14.x;
					    u_xlat5 = textureLod(_PaintTex, u_xlat23.yx, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat23.zx, 0.0);
					    u_xlat9.w = u_xlat23.x;
					    u_xlat14.x = u_xlat6.x * u_xlat8.y;
					    u_xlat14.z = u_xlat7.x * u_xlat5.y;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat37 = u_xlat14.z * u_xlat6.z + u_xlat37;
					    u_xlat37 = u_xlat14.x * u_xlat6.z + u_xlat37;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xw, 0.0);
					    u_xlat14.x = u_xlat6.x * u_xlat8.y;
					    u_xlat14.z = u_xlat7.x * u_xlat5.y;
					    u_xlat14.xz = u_xlat6.yy * u_xlat14.xz;
					    u_xlat37 = u_xlat14.z * u_xlat6.z + u_xlat37;
					    u_xlat37 = u_xlat14.x * u_xlat6.z + u_xlat37;
					    u_xlat14.x = in_COLOR0.w + in_COLOR0.w;
					    u_xlat14.x = u_xlat14.x;
					    u_xlat14.x = clamp(u_xlat14.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat14.xxx * u_xlat3.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat37 = u_xlat1.y + 1.5;
					    u_xlat37 = floor(u_xlat37);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat2.x = u_xlat2.z * u_xlat2.x;
					    u_xlat37 = u_xlat37 * _AoTexVolume.x + u_xlat14.x;
					    u_xlat5.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat5.x = u_xlat37 / _AoTexSize.x;
					    u_xlat5 = textureLod(_HighlightTex, u_xlat5.xy, 0.0);
					    u_xlat2 = u_xlat2.xxxx * u_xlat5;
					    u_xlatb37 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb37)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat37 = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat37;
					    u_xlat13 = u_xlat1.y + (-_WaterLevel);
					    u_xlat24.xy = u_xlat24.xy * vec2(u_xlat13) + u_xlat1.xz;
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + (-_FogMinRad);
					    u_xlat36 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat24.x / u_xlat36;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat24.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat36 = _AAFactor + 1.0;
					    u_xlat24.x = u_xlat36 * u_xlat24.x;
					    u_xlat24.x = u_xlat24.x * _LineWidth;
					    u_xlat24.x = u_xlat13 / u_xlat24.x;
					    u_xlat24.x = u_xlat24.x + 0.5;
					    u_xlat36 = (-u_xlat24.x) + 2.0;
					    vs_TEXCOORD3.y = u_xlat37 * u_xlat36 + u_xlat24.x;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_GAME_ON" }
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
						vec4 unused_0_13[7];
						float _Year;
						vec4 unused_0_15;
						vec4 _BloodColor;
						vec4 unused_0_17;
						vec4 _LongshipColor;
						vec4 unused_0_19[3];
						vec4 _SnowColor;
						vec4 unused_0_21;
						float _SnowAmount;
						vec4 unused_0_23;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_31;
						float _AAFactor;
						vec4 unused_0_33[3];
						float _Hover;
						vec4 unused_0_35[2];
						vec4 _MainTex_ST;
						vec4 unused_0_37[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat17;
					vec3 u_xlat18;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					vec2 u_xlat32;
					float u_xlat36;
					float u_xlat37;
					bool u_xlatb37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.z = unity_MatrixV[1].z;
					    u_xlat4.x = unity_MatrixV[0].z;
					    u_xlat4.z = unity_MatrixV[2].z;
					    u_xlat3.xy = (-u_xlat4.zx);
					    u_xlat5.xyz = u_xlat1.yzx * u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat3.zxy * u_xlat1.zxy + (-u_xlat5.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat4.y = unity_MatrixV[1].z;
					    u_xlat2.xyz = (-u_xlat4.xyz) * vec3(-0.0150000006, 0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat24.x = _WaterLevel * 2.0 + (-u_xlat1.y);
					    u_xlat2 = u_xlat24.xxxx * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat23.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat6.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat5.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat3.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat5.xyz = (-u_xlat4.xyz) * vec3(-1.0, 1.0, -1.0) + (-u_xlat3.xyz);
					    u_xlat3.xyz = u_xlat5.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat24.xy = u_xlat4.xz / unity_MatrixV[1].zz;
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat4.xyz);
					    u_xlat37 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat37 = u_xlat37 * 0.25;
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat14.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat3.y * u_xlat14.x;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat38;
					    u_xlat7.z = u_xlat3.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat38;
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat22.yz = u_xlat7.yx;
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.zzz * u_xlat9.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat9.xyz;
					    u_xlat14.x = u_xlat14.x * u_xlat15.y;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat14.x;
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat14.x;
					    u_xlat22.x = u_xlat15.z * u_xlat6.y;
					    u_xlat3 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat6.xyz = u_xlat4.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat5.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat4.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat22.yx, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat22.zx, 0.0);
					    u_xlat7.w = u_xlat22.x;
					    u_xlat8.xyz = u_xlat4.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat8.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat4.y = abs(_SunDir.y);
					    u_xlat4.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xyz = u_xlat4.xyz;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat14.xz = u_xlat3.xy * u_xlat4.xy;
					    u_xlat14.x = u_xlat14.z + u_xlat14.x;
					    u_xlat14.x = u_xlat4.z * u_xlat3.z + u_xlat14.x;
					    u_xlat14.x = u_xlat14.x * 0.600000024 + 0.200000003;
					    u_xlat38 = u_xlat37;
					    u_xlat38 = clamp(u_xlat38, 0.0, 1.0);
					    u_xlat37 = (-u_xlat37);
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.x = max(u_xlat37, u_xlat38);
					    u_xlat3.x = (-u_xlat3.x) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat15.xyz = vec3(u_xlat38) * u_xlat4.yzx;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xxx + u_xlat15.xyz;
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat4.zxy + u_xlat3.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _SnowColor.xyz;
					    u_xlat3.xyz = vec3(_SnowAmount) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * _MinAmbientColor.xyz;
					    u_xlat5.xyz = (-_MinAmbientColor.xyz) * u_xlat3.xyz + _MaxAmbientColor.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + (-_LongshipColor.xyz);
					    u_xlat4.xyz = u_xlat14.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat4.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat5.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat14.xz = max(u_xlat5.xz, _LevelRect.xy);
					    u_xlat5.xz = min(u_xlat14.xz, _LevelRect.zw);
					    u_xlat5.xyz = max(u_xlat5.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = min(u_xlat5.xyz, u_xlat6.xyz);
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat5.xyz);
					    u_xlat5.xyz = floor(u_xlat5.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat14.x = u_xlat5.y * u_xlat37;
					    u_xlat8.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat14.x;
					    u_xlat9.z = u_xlat5.z * u_xlat8.y;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat32.xy = u_xlat7.xx * u_xlat10.xy;
					    u_xlat32.xy = u_xlat7.yy * u_xlat32.xy;
					    u_xlat17.xyz = u_xlat5.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat14.x;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat23.yz = u_xlat9.yx;
					    u_xlat14.xz = u_xlat6.xx * u_xlat10.xy;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat14.xz = u_xlat7.zz * u_xlat14.xz;
					    u_xlat14.xz = u_xlat32.xy * u_xlat7.zz + u_xlat14.xz;
					    u_xlat37 = u_xlat37 * u_xlat17.y;
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat37;
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat37;
					    u_xlat23.x = u_xlat17.z * u_xlat8.y;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat5.zw = u_xlat6.xx * u_xlat8.xy;
					    u_xlat5.xy = u_xlat7.xx * u_xlat5.xy;
					    u_xlat5 = u_xlat6.yyyy * u_xlat5;
					    u_xlat14.xz = u_xlat5.xy * u_xlat7.zz + u_xlat14.xz;
					    u_xlat14.xz = u_xlat5.zw * u_xlat7.zz + u_xlat14.xz;
					    u_xlat5 = textureLod(_PaintTex, u_xlat23.yx, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat23.zx, 0.0);
					    u_xlat9.w = u_xlat23.x;
					    u_xlat5.zw = u_xlat6.xx * u_xlat8.xy;
					    u_xlat5 = u_xlat7.xxyy * u_xlat5;
					    u_xlat5.xy = u_xlat7.yy * u_xlat5.xy;
					    u_xlat14.xz = u_xlat5.xy * u_xlat6.zz + u_xlat14.xz;
					    u_xlat14.xz = u_xlat5.zw * u_xlat6.zz + u_xlat14.xz;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xw, 0.0);
					    u_xlat5.zw = u_xlat6.xx * u_xlat8.xy;
					    u_xlat5.xy = u_xlat7.xx * u_xlat5.xy;
					    u_xlat5 = u_xlat6.yyyy * u_xlat5;
					    u_xlat14.xz = u_xlat5.xy * u_xlat6.zz + u_xlat14.xz;
					    u_xlat14.xz = u_xlat5.zw * u_xlat6.zz + u_xlat14.xz;
					    u_xlat37 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat37 = u_xlat37;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat3.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = u_xlat14.zzz * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat37 = u_xlat14.x * 1.20000005 + -0.200000003;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat5.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat37 = u_xlat1.y + 1.5;
					    u_xlat37 = floor(u_xlat37);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat2.x = u_xlat2.z * u_xlat2.x;
					    u_xlat37 = u_xlat37 * _AoTexVolume.x + u_xlat14.x;
					    u_xlat5.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat5.x = u_xlat37 / _AoTexSize.x;
					    u_xlat5 = textureLod(_HighlightTex, u_xlat5.xy, 0.0);
					    u_xlat2 = u_xlat2.xxxx * u_xlat5;
					    u_xlatb37 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb37)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat37 = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat37;
					    u_xlat13 = u_xlat1.y + (-_WaterLevel);
					    u_xlat24.xy = u_xlat24.xy * vec2(u_xlat13) + u_xlat1.xz;
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + (-_FogMinRad);
					    u_xlat36 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat24.x / u_xlat36;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat24.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat36 = _AAFactor + 1.0;
					    u_xlat24.x = u_xlat36 * u_xlat24.x;
					    u_xlat24.x = u_xlat24.x * _LineWidth;
					    u_xlat24.x = u_xlat13 / u_xlat24.x;
					    u_xlat24.x = u_xlat24.x + 0.5;
					    u_xlat36 = (-u_xlat24.x) + 2.0;
					    vs_TEXCOORD3.y = u_xlat37 * u_xlat36 + u_xlat24.x;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_13[7];
						float _Year;
						vec4 unused_0_15;
						vec4 _BloodColor;
						vec4 unused_0_17;
						vec4 _LongshipColor;
						vec4 unused_0_19[3];
						vec4 _SnowColor;
						vec4 unused_0_21;
						float _SnowAmount;
						vec4 unused_0_23;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_31;
						float _AAFactor;
						vec4 unused_0_33[3];
						float _Hover;
						vec4 unused_0_35[2];
						vec4 _MainTex_ST;
						vec4 unused_0_37[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat17;
					vec3 u_xlat18;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					vec2 u_xlat32;
					float u_xlat36;
					float u_xlat37;
					bool u_xlatb37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.z = unity_MatrixV[1].z;
					    u_xlat4.x = unity_MatrixV[0].z;
					    u_xlat4.z = unity_MatrixV[2].z;
					    u_xlat3.xy = (-u_xlat4.zx);
					    u_xlat5.xyz = u_xlat1.yzx * u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat3.zxy * u_xlat1.zxy + (-u_xlat5.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat4.y = unity_MatrixV[1].z;
					    u_xlat2.xyz = (-u_xlat4.xyz) * vec3(-0.0150000006, 0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat24.x = _WaterLevel * 2.0 + (-u_xlat1.y);
					    u_xlat2 = u_xlat24.xxxx * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat23.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat6.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat5.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat3.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat5.xyz = (-u_xlat4.xyz) * vec3(-1.0, 1.0, -1.0) + (-u_xlat3.xyz);
					    u_xlat3.xyz = u_xlat5.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat24.xy = u_xlat4.xz / unity_MatrixV[1].zz;
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat4.xyz);
					    u_xlat37 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat37 = u_xlat37 * 0.25;
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat14.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat3.y * u_xlat14.x;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat38;
					    u_xlat7.z = u_xlat3.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat38;
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat22.yz = u_xlat7.yx;
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.zzz * u_xlat9.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat9.xyz;
					    u_xlat14.x = u_xlat14.x * u_xlat15.y;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat14.x;
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat14.x;
					    u_xlat22.x = u_xlat15.z * u_xlat6.y;
					    u_xlat3 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat6.xyz = u_xlat4.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat5.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat4.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat22.yx, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat22.zx, 0.0);
					    u_xlat7.w = u_xlat22.x;
					    u_xlat8.xyz = u_xlat4.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat8.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat4.y = abs(_SunDir.y);
					    u_xlat4.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xyz = u_xlat4.xyz;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat14.xz = u_xlat3.xy * u_xlat4.xy;
					    u_xlat14.x = u_xlat14.z + u_xlat14.x;
					    u_xlat14.x = u_xlat4.z * u_xlat3.z + u_xlat14.x;
					    u_xlat14.x = u_xlat14.x * 0.600000024 + 0.200000003;
					    u_xlat38 = u_xlat37;
					    u_xlat38 = clamp(u_xlat38, 0.0, 1.0);
					    u_xlat37 = (-u_xlat37);
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.x = max(u_xlat37, u_xlat38);
					    u_xlat3.x = (-u_xlat3.x) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat15.xyz = vec3(u_xlat38) * u_xlat4.yzx;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xxx + u_xlat15.xyz;
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat4.zxy + u_xlat3.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _SnowColor.xyz;
					    u_xlat3.xyz = vec3(_SnowAmount) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * _MinAmbientColor.xyz;
					    u_xlat5.xyz = (-_MinAmbientColor.xyz) * u_xlat3.xyz + _MaxAmbientColor.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + (-_LongshipColor.xyz);
					    u_xlat4.xyz = u_xlat14.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat4.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat5.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat14.xz = max(u_xlat5.xz, _LevelRect.xy);
					    u_xlat5.xz = min(u_xlat14.xz, _LevelRect.zw);
					    u_xlat5.xyz = max(u_xlat5.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = min(u_xlat5.xyz, u_xlat6.xyz);
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat5.xyz);
					    u_xlat5.xyz = floor(u_xlat5.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat14.x = u_xlat5.y * u_xlat37;
					    u_xlat8.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat14.x;
					    u_xlat9.z = u_xlat5.z * u_xlat8.y;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat32.xy = u_xlat7.xx * u_xlat10.xy;
					    u_xlat32.xy = u_xlat7.yy * u_xlat32.xy;
					    u_xlat17.xyz = u_xlat5.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat14.x;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat23.yz = u_xlat9.yx;
					    u_xlat14.xz = u_xlat6.xx * u_xlat10.xy;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat14.xz = u_xlat7.zz * u_xlat14.xz;
					    u_xlat14.xz = u_xlat32.xy * u_xlat7.zz + u_xlat14.xz;
					    u_xlat37 = u_xlat37 * u_xlat17.y;
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat37;
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat37;
					    u_xlat23.x = u_xlat17.z * u_xlat8.y;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat5.zw = u_xlat6.xx * u_xlat8.xy;
					    u_xlat5.xy = u_xlat7.xx * u_xlat5.xy;
					    u_xlat5 = u_xlat6.yyyy * u_xlat5;
					    u_xlat14.xz = u_xlat5.xy * u_xlat7.zz + u_xlat14.xz;
					    u_xlat14.xz = u_xlat5.zw * u_xlat7.zz + u_xlat14.xz;
					    u_xlat5 = textureLod(_PaintTex, u_xlat23.yx, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat23.zx, 0.0);
					    u_xlat9.w = u_xlat23.x;
					    u_xlat5.zw = u_xlat6.xx * u_xlat8.xy;
					    u_xlat5 = u_xlat7.xxyy * u_xlat5;
					    u_xlat5.xy = u_xlat7.yy * u_xlat5.xy;
					    u_xlat14.xz = u_xlat5.xy * u_xlat6.zz + u_xlat14.xz;
					    u_xlat14.xz = u_xlat5.zw * u_xlat6.zz + u_xlat14.xz;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xw, 0.0);
					    u_xlat5.zw = u_xlat6.xx * u_xlat8.xy;
					    u_xlat5.xy = u_xlat7.xx * u_xlat5.xy;
					    u_xlat5 = u_xlat6.yyyy * u_xlat5;
					    u_xlat14.xz = u_xlat5.xy * u_xlat6.zz + u_xlat14.xz;
					    u_xlat14.xz = u_xlat5.zw * u_xlat6.zz + u_xlat14.xz;
					    u_xlat37 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat37 = u_xlat37;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat3.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = u_xlat14.zzz * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat37 = u_xlat14.x * 1.20000005 + -0.200000003;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat5.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat37 = u_xlat1.y + 1.5;
					    u_xlat37 = floor(u_xlat37);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat2.x = u_xlat2.z * u_xlat2.x;
					    u_xlat37 = u_xlat37 * _AoTexVolume.x + u_xlat14.x;
					    u_xlat5.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat5.x = u_xlat37 / _AoTexSize.x;
					    u_xlat5 = textureLod(_HighlightTex, u_xlat5.xy, 0.0);
					    u_xlat2 = u_xlat2.xxxx * u_xlat5;
					    u_xlatb37 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb37)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat37 = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat37;
					    u_xlat13 = u_xlat1.y + (-_WaterLevel);
					    u_xlat24.xy = u_xlat24.xy * vec2(u_xlat13) + u_xlat1.xz;
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + (-_FogMinRad);
					    u_xlat36 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat24.x / u_xlat36;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat24.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat36 = _AAFactor + 1.0;
					    u_xlat24.x = u_xlat36 * u_xlat24.x;
					    u_xlat24.x = u_xlat24.x * _LineWidth;
					    u_xlat24.x = u_xlat13 / u_xlat24.x;
					    u_xlat24.x = u_xlat24.x + 0.5;
					    u_xlat36 = (-u_xlat24.x) + 2.0;
					    vs_TEXCOORD3.y = u_xlat37 * u_xlat36 + u_xlat24.x;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_GAME_ON" "_LOWEND_ON" }
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
						vec4 unused_0_13[7];
						float _Year;
						vec4 unused_0_15;
						vec4 _BloodColor;
						vec4 unused_0_17;
						vec4 _LongshipColor;
						vec4 unused_0_19[3];
						vec4 _SnowColor;
						vec4 unused_0_21;
						float _SnowAmount;
						vec4 unused_0_23;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_31;
						float _AAFactor;
						vec4 unused_0_33[3];
						float _Hover;
						vec4 unused_0_35[2];
						vec4 _MainTex_ST;
						vec4 unused_0_37[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat17;
					vec3 u_xlat18;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					vec2 u_xlat32;
					float u_xlat36;
					float u_xlat37;
					bool u_xlatb37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.z = unity_MatrixV[1].z;
					    u_xlat4.x = unity_MatrixV[0].z;
					    u_xlat4.z = unity_MatrixV[2].z;
					    u_xlat3.xy = (-u_xlat4.zx);
					    u_xlat5.xyz = u_xlat1.yzx * u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat3.zxy * u_xlat1.zxy + (-u_xlat5.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat4.y = unity_MatrixV[1].z;
					    u_xlat2.xyz = (-u_xlat4.xyz) * vec3(-0.0150000006, 0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat24.x = _WaterLevel * 2.0 + (-u_xlat1.y);
					    u_xlat2 = u_xlat24.xxxx * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat23.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat6.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat5.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat3.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat5.xyz = (-u_xlat4.xyz) * vec3(-1.0, 1.0, -1.0) + (-u_xlat3.xyz);
					    u_xlat3.xyz = u_xlat5.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat24.xy = u_xlat4.xz / unity_MatrixV[1].zz;
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat4.xyz);
					    u_xlat37 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat37 = u_xlat37 * 0.25;
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat14.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat3.y * u_xlat14.x;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat38;
					    u_xlat7.z = u_xlat3.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat38;
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat22.yz = u_xlat7.yx;
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.zzz * u_xlat9.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat9.xyz;
					    u_xlat14.x = u_xlat14.x * u_xlat15.y;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat14.x;
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat14.x;
					    u_xlat22.x = u_xlat15.z * u_xlat6.y;
					    u_xlat3 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat6.xyz = u_xlat4.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat5.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat4.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat22.yx, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat22.zx, 0.0);
					    u_xlat7.w = u_xlat22.x;
					    u_xlat8.xyz = u_xlat4.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat8.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat4.y = abs(_SunDir.y);
					    u_xlat4.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xyz = u_xlat4.xyz;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat14.xz = u_xlat3.xy * u_xlat4.xy;
					    u_xlat14.x = u_xlat14.z + u_xlat14.x;
					    u_xlat14.x = u_xlat4.z * u_xlat3.z + u_xlat14.x;
					    u_xlat14.x = u_xlat14.x * 0.600000024 + 0.200000003;
					    u_xlat38 = u_xlat37;
					    u_xlat38 = clamp(u_xlat38, 0.0, 1.0);
					    u_xlat37 = (-u_xlat37);
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.x = max(u_xlat37, u_xlat38);
					    u_xlat3.x = (-u_xlat3.x) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat15.xyz = vec3(u_xlat38) * u_xlat4.yzx;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xxx + u_xlat15.xyz;
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat4.zxy + u_xlat3.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _SnowColor.xyz;
					    u_xlat3.xyz = vec3(_SnowAmount) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * _MinAmbientColor.xyz;
					    u_xlat5.xyz = (-_MinAmbientColor.xyz) * u_xlat3.xyz + _MaxAmbientColor.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + (-_LongshipColor.xyz);
					    u_xlat4.xyz = u_xlat14.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat4.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat5.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat14.xz = max(u_xlat5.xz, _LevelRect.xy);
					    u_xlat5.xz = min(u_xlat14.xz, _LevelRect.zw);
					    u_xlat5.xyz = max(u_xlat5.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = min(u_xlat5.xyz, u_xlat6.xyz);
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat5.xyz);
					    u_xlat5.xyz = floor(u_xlat5.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat14.x = u_xlat5.y * u_xlat37;
					    u_xlat8.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat14.x;
					    u_xlat9.z = u_xlat5.z * u_xlat8.y;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat32.xy = u_xlat7.xx * u_xlat10.xy;
					    u_xlat32.xy = u_xlat7.yy * u_xlat32.xy;
					    u_xlat17.xyz = u_xlat5.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat14.x;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat23.yz = u_xlat9.yx;
					    u_xlat14.xz = u_xlat6.xx * u_xlat10.xy;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat14.xz = u_xlat7.zz * u_xlat14.xz;
					    u_xlat14.xz = u_xlat32.xy * u_xlat7.zz + u_xlat14.xz;
					    u_xlat37 = u_xlat37 * u_xlat17.y;
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat37;
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat37;
					    u_xlat23.x = u_xlat17.z * u_xlat8.y;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat5.zw = u_xlat6.xx * u_xlat8.xy;
					    u_xlat5.xy = u_xlat7.xx * u_xlat5.xy;
					    u_xlat5 = u_xlat6.yyyy * u_xlat5;
					    u_xlat14.xz = u_xlat5.xy * u_xlat7.zz + u_xlat14.xz;
					    u_xlat14.xz = u_xlat5.zw * u_xlat7.zz + u_xlat14.xz;
					    u_xlat5 = textureLod(_PaintTex, u_xlat23.yx, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat23.zx, 0.0);
					    u_xlat9.w = u_xlat23.x;
					    u_xlat5.zw = u_xlat6.xx * u_xlat8.xy;
					    u_xlat5 = u_xlat7.xxyy * u_xlat5;
					    u_xlat5.xy = u_xlat7.yy * u_xlat5.xy;
					    u_xlat14.xz = u_xlat5.xy * u_xlat6.zz + u_xlat14.xz;
					    u_xlat14.xz = u_xlat5.zw * u_xlat6.zz + u_xlat14.xz;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xw, 0.0);
					    u_xlat5.zw = u_xlat6.xx * u_xlat8.xy;
					    u_xlat5.xy = u_xlat7.xx * u_xlat5.xy;
					    u_xlat5 = u_xlat6.yyyy * u_xlat5;
					    u_xlat14.xz = u_xlat5.xy * u_xlat6.zz + u_xlat14.xz;
					    u_xlat14.xz = u_xlat5.zw * u_xlat6.zz + u_xlat14.xz;
					    u_xlat37 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat37 = u_xlat37;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat3.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = u_xlat14.zzz * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat37 = u_xlat14.x * 1.20000005 + -0.200000003;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat5.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat37 = u_xlat1.y + 1.5;
					    u_xlat37 = floor(u_xlat37);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat2.x = u_xlat2.z * u_xlat2.x;
					    u_xlat37 = u_xlat37 * _AoTexVolume.x + u_xlat14.x;
					    u_xlat5.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat5.x = u_xlat37 / _AoTexSize.x;
					    u_xlat5 = textureLod(_HighlightTex, u_xlat5.xy, 0.0);
					    u_xlat2 = u_xlat2.xxxx * u_xlat5;
					    u_xlatb37 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb37)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat37 = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat37;
					    u_xlat13 = u_xlat1.y + (-_WaterLevel);
					    u_xlat24.xy = u_xlat24.xy * vec2(u_xlat13) + u_xlat1.xz;
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + (-_FogMinRad);
					    u_xlat36 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat24.x / u_xlat36;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat24.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat36 = _AAFactor + 1.0;
					    u_xlat24.x = u_xlat36 * u_xlat24.x;
					    u_xlat24.x = u_xlat24.x * _LineWidth;
					    u_xlat24.x = u_xlat13 / u_xlat24.x;
					    u_xlat24.x = u_xlat24.x + 0.5;
					    u_xlat36 = (-u_xlat24.x) + 2.0;
					    vs_TEXCOORD3.y = u_xlat37 * u_xlat36 + u_xlat24.x;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_13[7];
						float _Year;
						vec4 unused_0_15;
						vec4 _BloodColor;
						vec4 unused_0_17;
						vec4 _LongshipColor;
						vec4 unused_0_19[3];
						vec4 _SnowColor;
						vec4 unused_0_21;
						float _SnowAmount;
						vec4 unused_0_23;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_31;
						float _AAFactor;
						vec4 unused_0_33[3];
						float _Hover;
						vec4 unused_0_35[2];
						vec4 _MainTex_ST;
						vec4 unused_0_37[2];
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
					uniform  sampler2D _HighlightTex;
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
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
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat17;
					vec3 u_xlat18;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					vec2 u_xlat32;
					float u_xlat36;
					float u_xlat37;
					bool u_xlatb37;
					float u_xlat38;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.z = unity_MatrixV[1].z;
					    u_xlat4.x = unity_MatrixV[0].z;
					    u_xlat4.z = unity_MatrixV[2].z;
					    u_xlat3.xy = (-u_xlat4.zx);
					    u_xlat5.xyz = u_xlat1.yzx * u_xlat3.xyz;
					    u_xlat1.xyz = u_xlat3.zxy * u_xlat1.zxy + (-u_xlat5.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat4.y = unity_MatrixV[1].z;
					    u_xlat2.xyz = (-u_xlat4.xyz) * vec3(-0.0150000006, 0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat24.x = _WaterLevel * 2.0 + (-u_xlat1.y);
					    u_xlat2 = u_xlat24.xxxx * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat3.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat3.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9.xyz = u_xlat6.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10.xyz = u_xlat5.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat6.zzz * u_xlat10.xyz;
					    u_xlat9.xyz = u_xlat9.xyz * u_xlat6.zzz + u_xlat10.xyz;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat23.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7.xyz = u_xlat5.xxx * u_xlat7.xyz;
					    u_xlat7.xyz = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat6.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat5.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat6.zzz + u_xlat9.xyz;
					    u_xlat3.xyz = u_xlat7.xyz * u_xlat6.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9.xyz = u_xlat5.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat6.yyy * u_xlat9.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat18.xyz = u_xlat6.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat18.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat18.xyz = u_xlat5.yyy * u_xlat18.xyz;
					    u_xlat7.xyz = u_xlat6.xxx * u_xlat7.xyz;
					    u_xlat5.xyw = u_xlat5.yyy * u_xlat7.xyz;
					    u_xlat3.xyz = u_xlat5.xyw * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat18.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat5.xyz = (-u_xlat4.xyz) * vec3(-1.0, 1.0, -1.0) + (-u_xlat3.xyz);
					    u_xlat3.xyz = u_xlat5.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat24.xy = u_xlat4.xz / unity_MatrixV[1].zz;
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat4.xyz);
					    u_xlat37 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat37 = u_xlat37 * 0.25;
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat14.x = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat3.y * u_xlat14.x;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat38;
					    u_xlat7.z = u_xlat3.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat38;
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat22.yz = u_xlat7.yx;
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.yyy * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat5.zzz * u_xlat9.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat9.xyz;
					    u_xlat14.x = u_xlat14.x * u_xlat15.y;
					    u_xlat7.y = u_xlat6.x * u_xlat3.x + u_xlat14.x;
					    u_xlat7.x = u_xlat6.x * u_xlat15.x + u_xlat14.x;
					    u_xlat22.x = u_xlat15.z * u_xlat6.y;
					    u_xlat3 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat6 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat6.xyz = u_xlat4.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat5.xxx * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat4.yyy * u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat3.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat3.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat22.yx, 0.0);
					    u_xlat8 = textureLod(_AoTex, u_xlat22.zx, 0.0);
					    u_xlat7.w = u_xlat22.x;
					    u_xlat8.xyz = u_xlat4.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat8.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat3.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat3.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat3.xyz;
					    u_xlat4.y = abs(_SunDir.y);
					    u_xlat4.xz = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xyz = u_xlat4.xyz;
					    u_xlat4.xyz = clamp(u_xlat4.xyz, 0.0, 1.0);
					    u_xlat14.xz = u_xlat3.xy * u_xlat4.xy;
					    u_xlat14.x = u_xlat14.z + u_xlat14.x;
					    u_xlat14.x = u_xlat4.z * u_xlat3.z + u_xlat14.x;
					    u_xlat14.x = u_xlat14.x * 0.600000024 + 0.200000003;
					    u_xlat38 = u_xlat37;
					    u_xlat38 = clamp(u_xlat38, 0.0, 1.0);
					    u_xlat37 = (-u_xlat37);
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.x = max(u_xlat37, u_xlat38);
					    u_xlat3.x = (-u_xlat3.x) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat15.xyz = vec3(u_xlat38) * u_xlat4.yzx;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xxx + u_xlat15.xyz;
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat4.zxy + u_xlat3.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + _SnowColor.xyz;
					    u_xlat3.xyz = vec3(_SnowAmount) * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat4.xyz = u_xlat3.xyz * _MinAmbientColor.xyz;
					    u_xlat5.xyz = (-_MinAmbientColor.xyz) * u_xlat3.xyz + _MaxAmbientColor.xyz;
					    u_xlat3.xyz = u_xlat3.xyz + (-_LongshipColor.xyz);
					    u_xlat4.xyz = u_xlat14.xxx * u_xlat5.xyz + u_xlat4.xyz;
					    vs_COLOR0.xyz = u_xlat4.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat5.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat14.xz = max(u_xlat5.xz, _LevelRect.xy);
					    u_xlat5.xz = min(u_xlat14.xz, _LevelRect.zw);
					    u_xlat5.xyz = max(u_xlat5.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat6.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = min(u_xlat5.xyz, u_xlat6.xyz);
					    u_xlat5.xyz = u_xlat5.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat6.xyz = fract(u_xlat5.xyz);
					    u_xlat5.xyz = floor(u_xlat5.xyz);
					    u_xlat7.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat14.x = u_xlat5.y * u_xlat37;
					    u_xlat8.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat14.x;
					    u_xlat9.z = u_xlat5.z * u_xlat8.y;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat32.xy = u_xlat7.xx * u_xlat10.xy;
					    u_xlat32.xy = u_xlat7.yy * u_xlat32.xy;
					    u_xlat17.xyz = u_xlat5.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat14.x;
					    u_xlat10 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat23.yz = u_xlat9.yx;
					    u_xlat14.xz = u_xlat6.xx * u_xlat10.xy;
					    u_xlat14.xz = u_xlat7.yy * u_xlat14.xz;
					    u_xlat14.xz = u_xlat7.zz * u_xlat14.xz;
					    u_xlat14.xz = u_xlat32.xy * u_xlat7.zz + u_xlat14.xz;
					    u_xlat37 = u_xlat37 * u_xlat17.y;
					    u_xlat9.y = u_xlat8.x * u_xlat5.x + u_xlat37;
					    u_xlat9.x = u_xlat8.x * u_xlat17.x + u_xlat37;
					    u_xlat23.x = u_xlat17.z * u_xlat8.y;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yz, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xz, 0.0);
					    u_xlat5.zw = u_xlat6.xx * u_xlat8.xy;
					    u_xlat5.xy = u_xlat7.xx * u_xlat5.xy;
					    u_xlat5 = u_xlat6.yyyy * u_xlat5;
					    u_xlat14.xz = u_xlat5.xy * u_xlat7.zz + u_xlat14.xz;
					    u_xlat14.xz = u_xlat5.zw * u_xlat7.zz + u_xlat14.xz;
					    u_xlat5 = textureLod(_PaintTex, u_xlat23.yx, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat23.zx, 0.0);
					    u_xlat9.w = u_xlat23.x;
					    u_xlat5.zw = u_xlat6.xx * u_xlat8.xy;
					    u_xlat5 = u_xlat7.xxyy * u_xlat5;
					    u_xlat5.xy = u_xlat7.yy * u_xlat5.xy;
					    u_xlat14.xz = u_xlat5.xy * u_xlat6.zz + u_xlat14.xz;
					    u_xlat14.xz = u_xlat5.zw * u_xlat6.zz + u_xlat14.xz;
					    u_xlat5 = textureLod(_PaintTex, u_xlat9.yw, 0.0);
					    u_xlat8 = textureLod(_PaintTex, u_xlat9.xw, 0.0);
					    u_xlat5.zw = u_xlat6.xx * u_xlat8.xy;
					    u_xlat5.xy = u_xlat7.xx * u_xlat5.xy;
					    u_xlat5 = u_xlat6.yyyy * u_xlat5;
					    u_xlat14.xz = u_xlat5.xy * u_xlat6.zz + u_xlat14.xz;
					    u_xlat14.xz = u_xlat5.zw * u_xlat6.zz + u_xlat14.xz;
					    u_xlat37 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat37 = u_xlat37;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat3.xyz + _LongshipColor.xyz;
					    u_xlat5.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = u_xlat14.zzz * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat37 = u_xlat14.x * 1.20000005 + -0.200000003;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat5.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat5.xyz + u_xlat3.xyz;
					    u_xlat37 = u_xlat1.y + 1.5;
					    u_xlat37 = floor(u_xlat37);
					    u_xlat14.xz = floor(u_xlat2.xz);
					    u_xlat2.xz = fract(u_xlat2.xz);
					    u_xlat2.xz = u_xlat2.xz + vec2(-0.5, -0.5);
					    u_xlat2.xz = -abs(u_xlat2.xz) * vec2(2.0, 2.0) + vec2(1.0, 1.0);
					    u_xlat2.xz = u_xlat2.xz * vec2(3.0, 3.0);
					    u_xlat2.xz = min(u_xlat2.xz, vec2(1.0, 1.0));
					    u_xlat2.x = u_xlat2.z * u_xlat2.x;
					    u_xlat37 = u_xlat37 * _AoTexVolume.x + u_xlat14.x;
					    u_xlat5.y = u_xlat14.z / _AoTexSize.y;
					    u_xlat5.x = u_xlat37 / _AoTexSize.x;
					    u_xlat5 = textureLod(_HighlightTex, u_xlat5.xy, 0.0);
					    u_xlat2 = u_xlat2.xxxx * u_xlat5;
					    u_xlatb37 = 0.0<u_xlat2.w;
					    u_xlat5.xyz = u_xlat2.xyz / u_xlat2.www;
					    u_xlat2.xyz = (bool(u_xlatb37)) ? u_xlat5.xyz : u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * u_xlat4.xyz + u_xlat2.xyz;
					    u_xlat3.xyz = u_xlat4.xyz * u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.www * u_xlat2.xyz + u_xlat3.xyz;
					    u_xlat37 = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat37;
					    u_xlat13 = u_xlat1.y + (-_WaterLevel);
					    u_xlat24.xy = u_xlat24.xy * vec2(u_xlat13) + u_xlat1.xz;
					    u_xlat24.x = dot(u_xlat24.xy, u_xlat24.xy);
					    u_xlat24.x = sqrt(u_xlat24.x);
					    u_xlat24.x = u_xlat24.x + (-_FogMinRad);
					    u_xlat36 = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat24.x / u_xlat36;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat24.x = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat24.x = u_xlat24.x + u_xlat24.x;
					    u_xlat36 = _AAFactor + 1.0;
					    u_xlat24.x = u_xlat36 * u_xlat24.x;
					    u_xlat24.x = u_xlat24.x * _LineWidth;
					    u_xlat24.x = u_xlat13 / u_xlat24.x;
					    u_xlat24.x = u_xlat24.x + 0.5;
					    u_xlat36 = (-u_xlat24.x) + 2.0;
					    vs_TEXCOORD3.y = u_xlat37 * u_xlat36 + u_xlat24.x;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
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
						vec4 unused_0_4[3];
						vec4 _LongshipColor;
						vec4 unused_0_6[3];
						vec4 _SnowColor;
						vec4 unused_0_8;
						float _SnowAmount;
						vec4 unused_0_10[11];
						float _Hover;
						vec4 unused_0_12[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_15;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec2 u_xlat8;
					float u_xlat12;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat0.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat0.x) * 0.5 + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat4.x = u_xlat0.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8.x = max(u_xlat0.x, u_xlat4.x);
					    u_xlat8.x = (-u_xlat8.x) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat4.xxx * u_xlat2.yzx;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat8.xxx + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat4.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + _SnowColor.xyz;
					    u_xlat0.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + (-_LongshipColor.xyz);
					    u_xlat12 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat12 = u_xlat12;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz + _LongshipColor.xyz;
					    vs_COLOR1.xyz = u_xlat1.xyz * u_xlat0.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat8.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat8.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_4[3];
						vec4 _LongshipColor;
						vec4 unused_0_6[3];
						vec4 _SnowColor;
						vec4 unused_0_8;
						float _SnowAmount;
						vec4 unused_0_10[11];
						float _Hover;
						vec4 unused_0_12[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_15;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec2 u_xlat8;
					float u_xlat12;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat0.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat0.x) * 0.5 + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat4.x = u_xlat0.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8.x = max(u_xlat0.x, u_xlat4.x);
					    u_xlat8.x = (-u_xlat8.x) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat4.xxx * u_xlat2.yzx;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat8.xxx + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat4.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + _SnowColor.xyz;
					    u_xlat0.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + (-_LongshipColor.xyz);
					    u_xlat12 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat12 = u_xlat12;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz + _LongshipColor.xyz;
					    vs_COLOR1.xyz = u_xlat1.xyz * u_xlat0.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat8.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat8.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_LOWEND_ON" }
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
						vec4 unused_0_4[3];
						vec4 _LongshipColor;
						vec4 unused_0_6[3];
						vec4 _SnowColor;
						vec4 unused_0_8;
						float _SnowAmount;
						vec4 unused_0_10[11];
						float _Hover;
						vec4 unused_0_12[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_15;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec2 u_xlat8;
					float u_xlat12;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat0.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat0.x) * 0.5 + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat4.x = u_xlat0.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8.x = max(u_xlat0.x, u_xlat4.x);
					    u_xlat8.x = (-u_xlat8.x) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat4.xxx * u_xlat2.yzx;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat8.xxx + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat4.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + _SnowColor.xyz;
					    u_xlat0.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + (-_LongshipColor.xyz);
					    u_xlat12 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat12 = u_xlat12;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz + _LongshipColor.xyz;
					    vs_COLOR1.xyz = u_xlat1.xyz * u_xlat0.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat8.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat8.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_4[3];
						vec4 _LongshipColor;
						vec4 unused_0_6[3];
						vec4 _SnowColor;
						vec4 unused_0_8;
						float _SnowAmount;
						vec4 unused_0_10[11];
						float _Hover;
						vec4 unused_0_12[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_15;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec2 u_xlat8;
					float u_xlat12;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat0.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat0.x) * 0.5 + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat4.x = u_xlat0.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8.x = max(u_xlat0.x, u_xlat4.x);
					    u_xlat8.x = (-u_xlat8.x) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat4.xxx * u_xlat2.yzx;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat8.xxx + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat4.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + _SnowColor.xyz;
					    u_xlat0.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + (-_LongshipColor.xyz);
					    u_xlat12 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat12 = u_xlat12;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz + _LongshipColor.xyz;
					    vs_COLOR1.xyz = u_xlat1.xyz * u_xlat0.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat8.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat8.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_4[3];
						vec4 _LongshipColor;
						vec4 unused_0_6[3];
						vec4 _SnowColor;
						vec4 unused_0_8;
						float _SnowAmount;
						vec4 unused_0_10[11];
						float _Hover;
						vec4 unused_0_12[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_15;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec2 u_xlat8;
					float u_xlat12;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat0.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat0.x) * 0.5 + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat4.x = u_xlat0.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8.x = max(u_xlat0.x, u_xlat4.x);
					    u_xlat8.x = (-u_xlat8.x) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat4.xxx * u_xlat2.yzx;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat8.xxx + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat4.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + _SnowColor.xyz;
					    u_xlat0.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + (-_LongshipColor.xyz);
					    u_xlat12 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat12 = u_xlat12;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz + _LongshipColor.xyz;
					    vs_COLOR1.xyz = u_xlat1.xyz * u_xlat0.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat8.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat8.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_4[3];
						vec4 _LongshipColor;
						vec4 unused_0_6[3];
						vec4 _SnowColor;
						vec4 unused_0_8;
						float _SnowAmount;
						vec4 unused_0_10[11];
						float _Hover;
						vec4 unused_0_12[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_15;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec2 u_xlat8;
					float u_xlat12;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat0.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat0.x) * 0.5 + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat4.x = u_xlat0.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8.x = max(u_xlat0.x, u_xlat4.x);
					    u_xlat8.x = (-u_xlat8.x) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat4.xxx * u_xlat2.yzx;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat8.xxx + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat4.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + _SnowColor.xyz;
					    u_xlat0.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + (-_LongshipColor.xyz);
					    u_xlat12 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat12 = u_xlat12;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz + _LongshipColor.xyz;
					    vs_COLOR1.xyz = u_xlat1.xyz * u_xlat0.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat8.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat8.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_CINEMATIC_ON" "_LOWEND_ON" }
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
						vec4 unused_0_4[3];
						vec4 _LongshipColor;
						vec4 unused_0_6[3];
						vec4 _SnowColor;
						vec4 unused_0_8;
						float _SnowAmount;
						vec4 unused_0_10[11];
						float _Hover;
						vec4 unused_0_12[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_15;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec2 u_xlat8;
					float u_xlat12;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat0.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat0.x) * 0.5 + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat4.x = u_xlat0.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8.x = max(u_xlat0.x, u_xlat4.x);
					    u_xlat8.x = (-u_xlat8.x) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat4.xxx * u_xlat2.yzx;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat8.xxx + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat4.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + _SnowColor.xyz;
					    u_xlat0.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + (-_LongshipColor.xyz);
					    u_xlat12 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat12 = u_xlat12;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz + _LongshipColor.xyz;
					    vs_COLOR1.xyz = u_xlat1.xyz * u_xlat0.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat8.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat8.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_4[3];
						vec4 _LongshipColor;
						vec4 unused_0_6[3];
						vec4 _SnowColor;
						vec4 unused_0_8;
						float _SnowAmount;
						vec4 unused_0_10[11];
						float _Hover;
						vec4 unused_0_12[2];
						vec4 _MainTex_ST;
						vec3 _UV;
						vec4 unused_0_15;
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
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					 vec4 phase0_Output0_4;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					vec2 u_xlat8;
					float u_xlat12;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0.xyz = in_COLOR0.xyz + _UV.xyz;
					    vs_TEXCOORD1.xy = u_xlat0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.x = sqrt(u_xlat0.z);
					    phase0_Output0_4.y = u_xlat0.x * 2.0 + -2.0;
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    u_xlat1 = unity_MatrixV[1].zzzz * vec4(0.0300000012, 0.0300000012, 0.0300000012, 0.0300000012) + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_COLOR0 = u_xlat1;
					    u_xlat0.x = _AoTexVolume.y + -2.0;
					    u_xlat0.x = (-u_xlat0.x) * 0.5 + u_xlat0.y;
					    u_xlat0.x = u_xlat0.x * 0.25;
					    u_xlat4.x = u_xlat0.x;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x);
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat8.x = max(u_xlat0.x, u_xlat4.x);
					    u_xlat8.x = (-u_xlat8.x) + 1.0;
					    u_xlat2.x = _Year;
					    u_xlat2.y = 0.0;
					    u_xlat2 = textureLod(_GrassTex, u_xlat2.xy, 0.0);
					    u_xlat3.xyz = u_xlat4.xxx * u_xlat2.yzx;
					    u_xlat4.xyz = u_xlat2.xyz * u_xlat8.xxx + u_xlat3.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.zxy + u_xlat4.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + _SnowColor.xyz;
					    u_xlat0.xyz = vec3(_SnowAmount) * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat0.xyz = u_xlat0.xyz + (-_LongshipColor.xyz);
					    u_xlat12 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat12 = u_xlat12;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz + _LongshipColor.xyz;
					    vs_COLOR1.xyz = u_xlat1.xyz * u_xlat0.xyz;
					    vs_COLOR1.w = (-in_COLOR0.w) * 2.0 + 2.0;
					    vs_COLOR1.w = clamp(vs_COLOR1.w, 0.0, 1.0);
					    phase0_Output0_4.xzw = vec3(1.0, 0.0, 2.0);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat8.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    vs_TEXCOORD4.xy = u_xlat8.xy * u_xlat0.xy;
					vs_TEXCOORD2 = phase0_Output0_4.xy;
					vs_TEXCOORD3 = phase0_Output0_4.zw;
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
						vec4 unused_0_17[3];
						vec4 _LongshipColor;
						vec4 unused_0_19[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_23;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_31;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_35;
						float _Hover;
						vec4 unused_0_37[2];
						vec4 _MainTex_ST;
						vec4 unused_0_39[2];
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
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec4 u_xlat10;
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat17;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat25;
					float u_xlat26;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat39;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat4.xyz);
					    u_xlat36 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat2.y * u_xlat37;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat38;
					    u_xlat7.z = u_xlat2.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat9.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat38;
					    u_xlat10 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat14.yz = u_xlat7.yx;
					    u_xlat10.xyz = u_xlat4.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.zzz * u_xlat10.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat9.y;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat37;
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat37;
					    u_xlat14.x = u_xlat6.y * u_xlat9.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat6.xyz;
					    u_xlat8 = textureLod(_AoTex, u_xlat14.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat14.zx, 0.0);
					    u_xlat7.w = u_xlat14.x;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat2.xyz = u_xlat5.yyy * u_xlat2.xyz;
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat17.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat4.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xy = abs(_SunDir.yx);
					    u_xlat5.xyz = u_xlat4.zxw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat4.x + u_xlat4.y;
					    u_xlat2.xyw = u_xlat2.xyz * u_xlat5.xyz;
					    u_xlat39 = u_xlat2.y + u_xlat2.x;
					    u_xlat26 = u_xlat5.z * u_xlat2.z + u_xlat39;
					    u_xlat24.x = u_xlat26 * 0.600000024 + u_xlat24.x;
					    u_xlat26 = u_xlat36;
					    u_xlat26 = clamp(u_xlat26, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat39 = max(u_xlat36, u_xlat26);
					    u_xlat39 = (-u_xlat39) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat26) * u_xlat4.yzx;
					    u_xlat5.xyz = u_xlat4.xyz * vec3(u_xlat39) + u_xlat5.xyz;
					    u_xlat4.xyz = vec3(u_xlat36) * u_xlat4.zxy + u_xlat5.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + _SnowColor.xyz;
					    u_xlat4.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat4.xyz * _MinAmbientColor.xyz;
					    u_xlat6.xyz = (-_MinAmbientColor.xyz) * u_xlat4.xyz + _MaxAmbientColor.xyz;
					    u_xlat4.xyz = u_xlat4.xyz + (-_LongshipColor.xyz);
					    u_xlat5.xyz = u_xlat24.xxx * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat6.xyz = u_xlat3.xyw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat2.xy = u_xlat2.xy * u_xlat6.xy;
					    u_xlat36 = u_xlat2.y + u_xlat2.x;
					    u_xlat36 = u_xlat2.w * u_xlat6.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(u_xlat36) + u_xlat5.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat3.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat3.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat3.xz, _LevelRect.xy);
					    u_xlat3.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat37 = u_xlat6.x * u_xlat9.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat22.yz = u_xlat8.yx;
					    u_xlat36 = u_xlat5.x * u_xlat9.y;
					    u_xlat36 = u_xlat6.y * u_xlat36;
					    u_xlat36 = u_xlat6.z * u_xlat36;
					    u_xlat36 = u_xlat37 * u_xlat6.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat22.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.x = u_xlat5.x * u_xlat7.y;
					    u_xlat24.x = u_xlat5.y * u_xlat24.x;
					    u_xlat37 = u_xlat6.x * u_xlat3.y;
					    u_xlat37 = u_xlat5.y * u_xlat37;
					    u_xlat36 = u_xlat37 * u_xlat6.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat6.z + u_xlat36;
					    u_xlat3 = textureLod(_PaintTex, u_xlat22.yx, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat22.zx, 0.0);
					    u_xlat8.w = u_xlat22.x;
					    u_xlat36 = u_xlat5.x * u_xlat7.y;
					    u_xlat36 = u_xlat6.y * u_xlat36;
					    u_xlat37 = u_xlat6.x * u_xlat3.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat5.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat5.z + u_xlat24.x;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat36 = u_xlat5.x * u_xlat7.y;
					    u_xlat36 = u_xlat5.y * u_xlat36;
					    u_xlat37 = u_xlat6.x * u_xlat3.y;
					    u_xlat37 = u_xlat5.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat5.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat5.z + u_xlat24.x;
					    u_xlat36 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat36 = u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat36) * u_xlat4.xyz + _LongshipColor.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat4.xyz + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.xyz * u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_17[3];
						vec4 _LongshipColor;
						vec4 unused_0_19[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_23;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_31;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_35;
						float _Hover;
						vec4 unused_0_37[2];
						vec4 _MainTex_ST;
						vec4 unused_0_39[2];
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
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec4 u_xlat10;
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat17;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat25;
					float u_xlat26;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat39;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat4.xyz);
					    u_xlat36 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat2.y * u_xlat37;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat38;
					    u_xlat7.z = u_xlat2.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat9.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat38;
					    u_xlat10 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat14.yz = u_xlat7.yx;
					    u_xlat10.xyz = u_xlat4.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.zzz * u_xlat10.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat9.y;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat37;
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat37;
					    u_xlat14.x = u_xlat6.y * u_xlat9.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat6.xyz;
					    u_xlat8 = textureLod(_AoTex, u_xlat14.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat14.zx, 0.0);
					    u_xlat7.w = u_xlat14.x;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat2.xyz = u_xlat5.yyy * u_xlat2.xyz;
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat17.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat4.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xy = abs(_SunDir.yx);
					    u_xlat5.xyz = u_xlat4.zxw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat4.x + u_xlat4.y;
					    u_xlat2.xyw = u_xlat2.xyz * u_xlat5.xyz;
					    u_xlat39 = u_xlat2.y + u_xlat2.x;
					    u_xlat26 = u_xlat5.z * u_xlat2.z + u_xlat39;
					    u_xlat24.x = u_xlat26 * 0.600000024 + u_xlat24.x;
					    u_xlat26 = u_xlat36;
					    u_xlat26 = clamp(u_xlat26, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat39 = max(u_xlat36, u_xlat26);
					    u_xlat39 = (-u_xlat39) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat26) * u_xlat4.yzx;
					    u_xlat5.xyz = u_xlat4.xyz * vec3(u_xlat39) + u_xlat5.xyz;
					    u_xlat4.xyz = vec3(u_xlat36) * u_xlat4.zxy + u_xlat5.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + _SnowColor.xyz;
					    u_xlat4.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat4.xyz * _MinAmbientColor.xyz;
					    u_xlat6.xyz = (-_MinAmbientColor.xyz) * u_xlat4.xyz + _MaxAmbientColor.xyz;
					    u_xlat4.xyz = u_xlat4.xyz + (-_LongshipColor.xyz);
					    u_xlat5.xyz = u_xlat24.xxx * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat6.xyz = u_xlat3.xyw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat2.xy = u_xlat2.xy * u_xlat6.xy;
					    u_xlat36 = u_xlat2.y + u_xlat2.x;
					    u_xlat36 = u_xlat2.w * u_xlat6.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(u_xlat36) + u_xlat5.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat3.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat3.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat3.xz, _LevelRect.xy);
					    u_xlat3.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat37 = u_xlat6.x * u_xlat9.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat22.yz = u_xlat8.yx;
					    u_xlat36 = u_xlat5.x * u_xlat9.y;
					    u_xlat36 = u_xlat6.y * u_xlat36;
					    u_xlat36 = u_xlat6.z * u_xlat36;
					    u_xlat36 = u_xlat37 * u_xlat6.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat22.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.x = u_xlat5.x * u_xlat7.y;
					    u_xlat24.x = u_xlat5.y * u_xlat24.x;
					    u_xlat37 = u_xlat6.x * u_xlat3.y;
					    u_xlat37 = u_xlat5.y * u_xlat37;
					    u_xlat36 = u_xlat37 * u_xlat6.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat6.z + u_xlat36;
					    u_xlat3 = textureLod(_PaintTex, u_xlat22.yx, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat22.zx, 0.0);
					    u_xlat8.w = u_xlat22.x;
					    u_xlat36 = u_xlat5.x * u_xlat7.y;
					    u_xlat36 = u_xlat6.y * u_xlat36;
					    u_xlat37 = u_xlat6.x * u_xlat3.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat5.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat5.z + u_xlat24.x;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat36 = u_xlat5.x * u_xlat7.y;
					    u_xlat36 = u_xlat5.y * u_xlat36;
					    u_xlat37 = u_xlat6.x * u_xlat3.y;
					    u_xlat37 = u_xlat5.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat5.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat5.z + u_xlat24.x;
					    u_xlat36 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat36 = u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat36) * u_xlat4.xyz + _LongshipColor.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat4.xyz + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.xyz * u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_LOWEND_ON" }
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
						vec4 unused_0_17[3];
						vec4 _LongshipColor;
						vec4 unused_0_19[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_23;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_31;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_35;
						float _Hover;
						vec4 unused_0_37[2];
						vec4 _MainTex_ST;
						vec4 unused_0_39[2];
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
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec4 u_xlat10;
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat17;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat25;
					float u_xlat26;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat39;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat4.xyz);
					    u_xlat36 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat2.y * u_xlat37;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat38;
					    u_xlat7.z = u_xlat2.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat9.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat38;
					    u_xlat10 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat14.yz = u_xlat7.yx;
					    u_xlat10.xyz = u_xlat4.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.zzz * u_xlat10.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat9.y;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat37;
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat37;
					    u_xlat14.x = u_xlat6.y * u_xlat9.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat6.xyz;
					    u_xlat8 = textureLod(_AoTex, u_xlat14.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat14.zx, 0.0);
					    u_xlat7.w = u_xlat14.x;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat2.xyz = u_xlat5.yyy * u_xlat2.xyz;
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat17.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat4.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xy = abs(_SunDir.yx);
					    u_xlat5.xyz = u_xlat4.zxw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat4.x + u_xlat4.y;
					    u_xlat2.xyw = u_xlat2.xyz * u_xlat5.xyz;
					    u_xlat39 = u_xlat2.y + u_xlat2.x;
					    u_xlat26 = u_xlat5.z * u_xlat2.z + u_xlat39;
					    u_xlat24.x = u_xlat26 * 0.600000024 + u_xlat24.x;
					    u_xlat26 = u_xlat36;
					    u_xlat26 = clamp(u_xlat26, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat39 = max(u_xlat36, u_xlat26);
					    u_xlat39 = (-u_xlat39) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat26) * u_xlat4.yzx;
					    u_xlat5.xyz = u_xlat4.xyz * vec3(u_xlat39) + u_xlat5.xyz;
					    u_xlat4.xyz = vec3(u_xlat36) * u_xlat4.zxy + u_xlat5.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + _SnowColor.xyz;
					    u_xlat4.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat4.xyz * _MinAmbientColor.xyz;
					    u_xlat6.xyz = (-_MinAmbientColor.xyz) * u_xlat4.xyz + _MaxAmbientColor.xyz;
					    u_xlat4.xyz = u_xlat4.xyz + (-_LongshipColor.xyz);
					    u_xlat5.xyz = u_xlat24.xxx * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat6.xyz = u_xlat3.xyw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat2.xy = u_xlat2.xy * u_xlat6.xy;
					    u_xlat36 = u_xlat2.y + u_xlat2.x;
					    u_xlat36 = u_xlat2.w * u_xlat6.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(u_xlat36) + u_xlat5.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat3.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat3.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat3.xz, _LevelRect.xy);
					    u_xlat3.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat37 = u_xlat6.x * u_xlat9.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat22.yz = u_xlat8.yx;
					    u_xlat36 = u_xlat5.x * u_xlat9.y;
					    u_xlat36 = u_xlat6.y * u_xlat36;
					    u_xlat36 = u_xlat6.z * u_xlat36;
					    u_xlat36 = u_xlat37 * u_xlat6.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat22.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.x = u_xlat5.x * u_xlat7.y;
					    u_xlat24.x = u_xlat5.y * u_xlat24.x;
					    u_xlat37 = u_xlat6.x * u_xlat3.y;
					    u_xlat37 = u_xlat5.y * u_xlat37;
					    u_xlat36 = u_xlat37 * u_xlat6.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat6.z + u_xlat36;
					    u_xlat3 = textureLod(_PaintTex, u_xlat22.yx, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat22.zx, 0.0);
					    u_xlat8.w = u_xlat22.x;
					    u_xlat36 = u_xlat5.x * u_xlat7.y;
					    u_xlat36 = u_xlat6.y * u_xlat36;
					    u_xlat37 = u_xlat6.x * u_xlat3.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat5.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat5.z + u_xlat24.x;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat36 = u_xlat5.x * u_xlat7.y;
					    u_xlat36 = u_xlat5.y * u_xlat36;
					    u_xlat37 = u_xlat6.x * u_xlat3.y;
					    u_xlat37 = u_xlat5.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat5.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat5.z + u_xlat24.x;
					    u_xlat36 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat36 = u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat36) * u_xlat4.xyz + _LongshipColor.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat4.xyz + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.xyz * u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_17[3];
						vec4 _LongshipColor;
						vec4 unused_0_19[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_23;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_31;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_35;
						float _Hover;
						vec4 unused_0_37[2];
						vec4 _MainTex_ST;
						vec4 unused_0_39[2];
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
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec4 u_xlat10;
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat17;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat25;
					float u_xlat26;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat39;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat4.xyz);
					    u_xlat36 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat2.y * u_xlat37;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat38;
					    u_xlat7.z = u_xlat2.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat9.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat38;
					    u_xlat10 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat14.yz = u_xlat7.yx;
					    u_xlat10.xyz = u_xlat4.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.zzz * u_xlat10.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat9.y;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat37;
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat37;
					    u_xlat14.x = u_xlat6.y * u_xlat9.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat6.xyz;
					    u_xlat8 = textureLod(_AoTex, u_xlat14.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat14.zx, 0.0);
					    u_xlat7.w = u_xlat14.x;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat2.xyz = u_xlat5.yyy * u_xlat2.xyz;
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat17.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat4.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xy = abs(_SunDir.yx);
					    u_xlat5.xyz = u_xlat4.zxw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat4.x + u_xlat4.y;
					    u_xlat2.xyw = u_xlat2.xyz * u_xlat5.xyz;
					    u_xlat39 = u_xlat2.y + u_xlat2.x;
					    u_xlat26 = u_xlat5.z * u_xlat2.z + u_xlat39;
					    u_xlat24.x = u_xlat26 * 0.600000024 + u_xlat24.x;
					    u_xlat26 = u_xlat36;
					    u_xlat26 = clamp(u_xlat26, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat39 = max(u_xlat36, u_xlat26);
					    u_xlat39 = (-u_xlat39) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat26) * u_xlat4.yzx;
					    u_xlat5.xyz = u_xlat4.xyz * vec3(u_xlat39) + u_xlat5.xyz;
					    u_xlat4.xyz = vec3(u_xlat36) * u_xlat4.zxy + u_xlat5.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + _SnowColor.xyz;
					    u_xlat4.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat4.xyz * _MinAmbientColor.xyz;
					    u_xlat6.xyz = (-_MinAmbientColor.xyz) * u_xlat4.xyz + _MaxAmbientColor.xyz;
					    u_xlat4.xyz = u_xlat4.xyz + (-_LongshipColor.xyz);
					    u_xlat5.xyz = u_xlat24.xxx * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat6.xyz = u_xlat3.xyw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat2.xy = u_xlat2.xy * u_xlat6.xy;
					    u_xlat36 = u_xlat2.y + u_xlat2.x;
					    u_xlat36 = u_xlat2.w * u_xlat6.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(u_xlat36) + u_xlat5.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat3.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat3.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat3.xz, _LevelRect.xy);
					    u_xlat3.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat37 = u_xlat6.x * u_xlat9.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat22.yz = u_xlat8.yx;
					    u_xlat36 = u_xlat5.x * u_xlat9.y;
					    u_xlat36 = u_xlat6.y * u_xlat36;
					    u_xlat36 = u_xlat6.z * u_xlat36;
					    u_xlat36 = u_xlat37 * u_xlat6.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat22.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.x = u_xlat5.x * u_xlat7.y;
					    u_xlat24.x = u_xlat5.y * u_xlat24.x;
					    u_xlat37 = u_xlat6.x * u_xlat3.y;
					    u_xlat37 = u_xlat5.y * u_xlat37;
					    u_xlat36 = u_xlat37 * u_xlat6.z + u_xlat36;
					    u_xlat24.x = u_xlat24.x * u_xlat6.z + u_xlat36;
					    u_xlat3 = textureLod(_PaintTex, u_xlat22.yx, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat22.zx, 0.0);
					    u_xlat8.w = u_xlat22.x;
					    u_xlat36 = u_xlat5.x * u_xlat7.y;
					    u_xlat36 = u_xlat6.y * u_xlat36;
					    u_xlat37 = u_xlat6.x * u_xlat3.y;
					    u_xlat37 = u_xlat6.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat5.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat5.z + u_xlat24.x;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat36 = u_xlat5.x * u_xlat7.y;
					    u_xlat36 = u_xlat5.y * u_xlat36;
					    u_xlat37 = u_xlat6.x * u_xlat3.y;
					    u_xlat37 = u_xlat5.y * u_xlat37;
					    u_xlat24.x = u_xlat37 * u_xlat5.z + u_xlat24.x;
					    u_xlat24.x = u_xlat36 * u_xlat5.z + u_xlat24.x;
					    u_xlat36 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat36 = u_xlat36;
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat36) * u_xlat4.xyz + _LongshipColor.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat4.xyz + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.xyz * u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_19;
						vec4 _LongshipColor;
						vec4 unused_0_21[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_25;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_33;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_37;
						float _Hover;
						vec4 unused_0_39[2];
						vec4 _MainTex_ST;
						vec4 unused_0_41[2];
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
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec4 u_xlat10;
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat17;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat25;
					float u_xlat26;
					vec2 u_xlat31;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat39;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat4.xyz);
					    u_xlat36 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat2.y * u_xlat37;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat38;
					    u_xlat7.z = u_xlat2.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat9.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat38;
					    u_xlat10 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat14.yz = u_xlat7.yx;
					    u_xlat10.xyz = u_xlat4.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.zzz * u_xlat10.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat9.y;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat37;
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat37;
					    u_xlat14.x = u_xlat6.y * u_xlat9.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat6.xyz;
					    u_xlat8 = textureLod(_AoTex, u_xlat14.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat14.zx, 0.0);
					    u_xlat7.w = u_xlat14.x;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat2.xyz = u_xlat5.yyy * u_xlat2.xyz;
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat17.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat4.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xy = abs(_SunDir.yx);
					    u_xlat5.xyz = u_xlat4.zxw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat4.x + u_xlat4.y;
					    u_xlat2.xyw = u_xlat2.xyz * u_xlat5.xyz;
					    u_xlat39 = u_xlat2.y + u_xlat2.x;
					    u_xlat26 = u_xlat5.z * u_xlat2.z + u_xlat39;
					    u_xlat24.x = u_xlat26 * 0.600000024 + u_xlat24.x;
					    u_xlat26 = u_xlat36;
					    u_xlat26 = clamp(u_xlat26, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat39 = max(u_xlat36, u_xlat26);
					    u_xlat39 = (-u_xlat39) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat26) * u_xlat4.yzx;
					    u_xlat5.xyz = u_xlat4.xyz * vec3(u_xlat39) + u_xlat5.xyz;
					    u_xlat4.xyz = vec3(u_xlat36) * u_xlat4.zxy + u_xlat5.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + _SnowColor.xyz;
					    u_xlat4.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat4.xyz * _MinAmbientColor.xyz;
					    u_xlat6.xyz = (-_MinAmbientColor.xyz) * u_xlat4.xyz + _MaxAmbientColor.xyz;
					    u_xlat4.xyz = u_xlat4.xyz + (-_LongshipColor.xyz);
					    u_xlat5.xyz = u_xlat24.xxx * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat6.xyz = u_xlat3.xyw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat2.xy = u_xlat2.xy * u_xlat6.xy;
					    u_xlat36 = u_xlat2.y + u_xlat2.x;
					    u_xlat36 = u_xlat2.w * u_xlat6.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(u_xlat36) + u_xlat5.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat3.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat3.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat3.xz, _LevelRect.xy);
					    u_xlat3.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat31.xy = u_xlat6.xx * u_xlat9.xy;
					    u_xlat31.xy = u_xlat6.yy * u_xlat31.xy;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat22.yz = u_xlat8.yx;
					    u_xlat9.xy = u_xlat5.xx * u_xlat9.xy;
					    u_xlat9.xy = u_xlat6.yy * u_xlat9.xy;
					    u_xlat9.xy = u_xlat6.zz * u_xlat9.xy;
					    u_xlat31.xy = u_xlat31.xy * u_xlat6.zz + u_xlat9.xy;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat22.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.xy = u_xlat5.xx * u_xlat9.xy;
					    u_xlat24.xy = u_xlat5.yy * u_xlat24.xy;
					    u_xlat3.xy = u_xlat6.xx * u_xlat3.xy;
					    u_xlat3.xy = u_xlat5.yy * u_xlat3.xy;
					    u_xlat3.xy = u_xlat3.xy * u_xlat6.zz + u_xlat31.xy;
					    u_xlat24.xy = u_xlat24.xy * u_xlat6.zz + u_xlat3.xy;
					    u_xlat3 = textureLod(_PaintTex, u_xlat22.yx, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat22.zx, 0.0);
					    u_xlat8.w = u_xlat22.x;
					    u_xlat3.zw = u_xlat5.xx * u_xlat7.xy;
					    u_xlat3 = u_xlat6.xxyy * u_xlat3;
					    u_xlat3.xy = u_xlat6.yy * u_xlat3.xy;
					    u_xlat24.xy = u_xlat3.xy * u_xlat5.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat3.zw * u_xlat5.zz + u_xlat24.xy;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat3.zw = u_xlat5.xx * u_xlat7.xy;
					    u_xlat3.xy = u_xlat6.xx * u_xlat3.xy;
					    u_xlat3 = u_xlat5.yyyy * u_xlat3;
					    u_xlat24.xy = u_xlat3.xy * u_xlat5.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat3.zw * u_xlat5.zz + u_xlat24.xy;
					    u_xlat37 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat37 = u_xlat37;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat4.xyz + _LongshipColor.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = u_xlat24.yyy * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat24.x = u_xlat24.x * 1.20000005 + -0.200000003;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat4.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat4.xyz + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.xyz * u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_19;
						vec4 _LongshipColor;
						vec4 unused_0_21[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_25;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_33;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_37;
						float _Hover;
						vec4 unused_0_39[2];
						vec4 _MainTex_ST;
						vec4 unused_0_41[2];
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
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec4 u_xlat10;
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat17;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat25;
					float u_xlat26;
					vec2 u_xlat31;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat39;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat4.xyz);
					    u_xlat36 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat2.y * u_xlat37;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat38;
					    u_xlat7.z = u_xlat2.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat9.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat38;
					    u_xlat10 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat14.yz = u_xlat7.yx;
					    u_xlat10.xyz = u_xlat4.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.zzz * u_xlat10.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat9.y;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat37;
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat37;
					    u_xlat14.x = u_xlat6.y * u_xlat9.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat6.xyz;
					    u_xlat8 = textureLod(_AoTex, u_xlat14.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat14.zx, 0.0);
					    u_xlat7.w = u_xlat14.x;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat2.xyz = u_xlat5.yyy * u_xlat2.xyz;
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat17.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat4.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xy = abs(_SunDir.yx);
					    u_xlat5.xyz = u_xlat4.zxw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat4.x + u_xlat4.y;
					    u_xlat2.xyw = u_xlat2.xyz * u_xlat5.xyz;
					    u_xlat39 = u_xlat2.y + u_xlat2.x;
					    u_xlat26 = u_xlat5.z * u_xlat2.z + u_xlat39;
					    u_xlat24.x = u_xlat26 * 0.600000024 + u_xlat24.x;
					    u_xlat26 = u_xlat36;
					    u_xlat26 = clamp(u_xlat26, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat39 = max(u_xlat36, u_xlat26);
					    u_xlat39 = (-u_xlat39) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat26) * u_xlat4.yzx;
					    u_xlat5.xyz = u_xlat4.xyz * vec3(u_xlat39) + u_xlat5.xyz;
					    u_xlat4.xyz = vec3(u_xlat36) * u_xlat4.zxy + u_xlat5.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + _SnowColor.xyz;
					    u_xlat4.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat4.xyz * _MinAmbientColor.xyz;
					    u_xlat6.xyz = (-_MinAmbientColor.xyz) * u_xlat4.xyz + _MaxAmbientColor.xyz;
					    u_xlat4.xyz = u_xlat4.xyz + (-_LongshipColor.xyz);
					    u_xlat5.xyz = u_xlat24.xxx * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat6.xyz = u_xlat3.xyw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat2.xy = u_xlat2.xy * u_xlat6.xy;
					    u_xlat36 = u_xlat2.y + u_xlat2.x;
					    u_xlat36 = u_xlat2.w * u_xlat6.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(u_xlat36) + u_xlat5.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat3.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat3.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat3.xz, _LevelRect.xy);
					    u_xlat3.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat31.xy = u_xlat6.xx * u_xlat9.xy;
					    u_xlat31.xy = u_xlat6.yy * u_xlat31.xy;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat22.yz = u_xlat8.yx;
					    u_xlat9.xy = u_xlat5.xx * u_xlat9.xy;
					    u_xlat9.xy = u_xlat6.yy * u_xlat9.xy;
					    u_xlat9.xy = u_xlat6.zz * u_xlat9.xy;
					    u_xlat31.xy = u_xlat31.xy * u_xlat6.zz + u_xlat9.xy;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat22.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.xy = u_xlat5.xx * u_xlat9.xy;
					    u_xlat24.xy = u_xlat5.yy * u_xlat24.xy;
					    u_xlat3.xy = u_xlat6.xx * u_xlat3.xy;
					    u_xlat3.xy = u_xlat5.yy * u_xlat3.xy;
					    u_xlat3.xy = u_xlat3.xy * u_xlat6.zz + u_xlat31.xy;
					    u_xlat24.xy = u_xlat24.xy * u_xlat6.zz + u_xlat3.xy;
					    u_xlat3 = textureLod(_PaintTex, u_xlat22.yx, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat22.zx, 0.0);
					    u_xlat8.w = u_xlat22.x;
					    u_xlat3.zw = u_xlat5.xx * u_xlat7.xy;
					    u_xlat3 = u_xlat6.xxyy * u_xlat3;
					    u_xlat3.xy = u_xlat6.yy * u_xlat3.xy;
					    u_xlat24.xy = u_xlat3.xy * u_xlat5.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat3.zw * u_xlat5.zz + u_xlat24.xy;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat3.zw = u_xlat5.xx * u_xlat7.xy;
					    u_xlat3.xy = u_xlat6.xx * u_xlat3.xy;
					    u_xlat3 = u_xlat5.yyyy * u_xlat3;
					    u_xlat24.xy = u_xlat3.xy * u_xlat5.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat3.zw * u_xlat5.zz + u_xlat24.xy;
					    u_xlat37 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat37 = u_xlat37;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat4.xyz + _LongshipColor.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = u_xlat24.yyy * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat24.x = u_xlat24.x * 1.20000005 + -0.200000003;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat4.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat4.xyz + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.xyz * u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_CINEMATIC_ON" "_LOWEND_ON" }
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
						vec4 unused_0_19;
						vec4 _LongshipColor;
						vec4 unused_0_21[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_25;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_33;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_37;
						float _Hover;
						vec4 unused_0_39[2];
						vec4 _MainTex_ST;
						vec4 unused_0_41[2];
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
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec4 u_xlat10;
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat17;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat25;
					float u_xlat26;
					vec2 u_xlat31;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat39;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat4.xyz);
					    u_xlat36 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat2.y * u_xlat37;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat38;
					    u_xlat7.z = u_xlat2.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat9.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat38;
					    u_xlat10 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat14.yz = u_xlat7.yx;
					    u_xlat10.xyz = u_xlat4.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.zzz * u_xlat10.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat9.y;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat37;
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat37;
					    u_xlat14.x = u_xlat6.y * u_xlat9.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat6.xyz;
					    u_xlat8 = textureLod(_AoTex, u_xlat14.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat14.zx, 0.0);
					    u_xlat7.w = u_xlat14.x;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat2.xyz = u_xlat5.yyy * u_xlat2.xyz;
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat17.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat4.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xy = abs(_SunDir.yx);
					    u_xlat5.xyz = u_xlat4.zxw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat4.x + u_xlat4.y;
					    u_xlat2.xyw = u_xlat2.xyz * u_xlat5.xyz;
					    u_xlat39 = u_xlat2.y + u_xlat2.x;
					    u_xlat26 = u_xlat5.z * u_xlat2.z + u_xlat39;
					    u_xlat24.x = u_xlat26 * 0.600000024 + u_xlat24.x;
					    u_xlat26 = u_xlat36;
					    u_xlat26 = clamp(u_xlat26, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat39 = max(u_xlat36, u_xlat26);
					    u_xlat39 = (-u_xlat39) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat26) * u_xlat4.yzx;
					    u_xlat5.xyz = u_xlat4.xyz * vec3(u_xlat39) + u_xlat5.xyz;
					    u_xlat4.xyz = vec3(u_xlat36) * u_xlat4.zxy + u_xlat5.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + _SnowColor.xyz;
					    u_xlat4.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat4.xyz * _MinAmbientColor.xyz;
					    u_xlat6.xyz = (-_MinAmbientColor.xyz) * u_xlat4.xyz + _MaxAmbientColor.xyz;
					    u_xlat4.xyz = u_xlat4.xyz + (-_LongshipColor.xyz);
					    u_xlat5.xyz = u_xlat24.xxx * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat6.xyz = u_xlat3.xyw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat2.xy = u_xlat2.xy * u_xlat6.xy;
					    u_xlat36 = u_xlat2.y + u_xlat2.x;
					    u_xlat36 = u_xlat2.w * u_xlat6.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(u_xlat36) + u_xlat5.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat3.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat3.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat3.xz, _LevelRect.xy);
					    u_xlat3.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat31.xy = u_xlat6.xx * u_xlat9.xy;
					    u_xlat31.xy = u_xlat6.yy * u_xlat31.xy;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat22.yz = u_xlat8.yx;
					    u_xlat9.xy = u_xlat5.xx * u_xlat9.xy;
					    u_xlat9.xy = u_xlat6.yy * u_xlat9.xy;
					    u_xlat9.xy = u_xlat6.zz * u_xlat9.xy;
					    u_xlat31.xy = u_xlat31.xy * u_xlat6.zz + u_xlat9.xy;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat22.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.xy = u_xlat5.xx * u_xlat9.xy;
					    u_xlat24.xy = u_xlat5.yy * u_xlat24.xy;
					    u_xlat3.xy = u_xlat6.xx * u_xlat3.xy;
					    u_xlat3.xy = u_xlat5.yy * u_xlat3.xy;
					    u_xlat3.xy = u_xlat3.xy * u_xlat6.zz + u_xlat31.xy;
					    u_xlat24.xy = u_xlat24.xy * u_xlat6.zz + u_xlat3.xy;
					    u_xlat3 = textureLod(_PaintTex, u_xlat22.yx, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat22.zx, 0.0);
					    u_xlat8.w = u_xlat22.x;
					    u_xlat3.zw = u_xlat5.xx * u_xlat7.xy;
					    u_xlat3 = u_xlat6.xxyy * u_xlat3;
					    u_xlat3.xy = u_xlat6.yy * u_xlat3.xy;
					    u_xlat24.xy = u_xlat3.xy * u_xlat5.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat3.zw * u_xlat5.zz + u_xlat24.xy;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat3.zw = u_xlat5.xx * u_xlat7.xy;
					    u_xlat3.xy = u_xlat6.xx * u_xlat3.xy;
					    u_xlat3 = u_xlat5.yyyy * u_xlat3;
					    u_xlat24.xy = u_xlat3.xy * u_xlat5.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat3.zw * u_xlat5.zz + u_xlat24.xy;
					    u_xlat37 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat37 = u_xlat37;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat4.xyz + _LongshipColor.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = u_xlat24.yyy * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat24.x = u_xlat24.x * 1.20000005 + -0.200000003;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat4.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat4.xyz + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.xyz * u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_19;
						vec4 _LongshipColor;
						vec4 unused_0_21[3];
						vec4 _SnowColor;
						vec4 _CloudCoverage;
						float _SnowAmount;
						vec4 unused_0_25;
						float _CameraUpScale;
						vec4 _MinAmbientColor;
						vec4 _MaxAmbientColor;
						float _WaterLevel;
						float _LineWidth;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_33;
						float _AAFactor;
						vec4 _FlashColor;
						vec3 _FlashDir;
						vec4 unused_0_37;
						float _Hover;
						vec4 unused_0_39[2];
						vec4 _MainTex_ST;
						vec4 unused_0_41[2];
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
					uniform  sampler2D _PaintTex;
					in  vec4 in_POSITION0;
					in  vec3 in_NORMAL0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_COLOR1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec4 u_xlat10;
					float u_xlat13;
					vec3 u_xlat14;
					vec3 u_xlat15;
					vec3 u_xlat16;
					vec3 u_xlat17;
					vec3 u_xlat22;
					vec3 u_xlat23;
					vec2 u_xlat24;
					float u_xlat25;
					float u_xlat26;
					vec2 u_xlat31;
					float u_xlat36;
					float u_xlat37;
					float u_xlat38;
					float u_xlat39;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_COLOR0.yx * vec2(0.99609375, 0.99609375);
					    u_xlat0.xy = in_NORMAL0.yy * unity_ObjectToWorld[1].xz;
					    u_xlat0.xy = unity_ObjectToWorld[0].xz * in_NORMAL0.xx + u_xlat0.xy;
					    u_xlat0.xy = unity_ObjectToWorld[2].xz * in_NORMAL0.zz + u_xlat0.xy;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat1.x = unity_MatrixV[0].x;
					    u_xlat1.y = unity_MatrixV[2].x;
					    u_xlat0.x = dot(u_xlat0.zx, u_xlat1.xy);
					    u_xlatb0 = u_xlat0.x<0.0;
					    u_xlat0.x = (u_xlatb0) ? (-in_TEXCOORD1.x) : in_TEXCOORD1.x;
					    u_xlat24.xy = vec2(_Hover) * vec2(0.0500000007, 0.0299999993) + vec2(1.0, 1.0);
					    u_xlat0.y = in_TEXCOORD1.y;
					    u_xlat0.xy = u_xlat24.xy * u_xlat0.xy;
					    u_xlat1.xyz = in_TANGENT0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[0].xyz * in_TANGENT0.xxx + u_xlat1.xyz;
					    u_xlat1.xyz = unity_ObjectToWorld[2].xyz * in_TANGENT0.zzz + u_xlat1.xyz;
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat2.xyz = u_xlat0.yyy * u_xlat1.xyz;
					    u_xlat3.x = unity_MatrixV[0].z;
					    u_xlat3.y = unity_MatrixV[1].z;
					    u_xlat3.z = unity_MatrixV[2].z;
					    u_xlat4.xyz = u_xlat1.yzx * (-u_xlat3.zxy);
					    u_xlat1.xyz = (-u_xlat3.yzx) * u_xlat1.zxy + (-u_xlat4.xyz);
					    u_xlat24.x = dot(u_xlat1.xyz, u_xlat1.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat1.xyz = u_xlat24.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.xxx * (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat1.w = u_xlat1.y * _CameraUpScale;
					    u_xlat2.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat3.xyz) * vec3(-0.0150000006, -0.0150000006, -0.0150000006) + u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xwz + u_xlat2.xyz;
					    u_xlat2 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat2 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
					    gl_Position = u_xlat2 + unity_MatrixVP[3];
					    u_xlat2.w = u_xlat1.y;
					    u_xlat4.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat4.y = 1.0;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat4.xyz = u_xlat2.xwz + vec3(-0.5, 0.5, -0.5);
					    u_xlat4.xyz = max(u_xlat4.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _NormalTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = min(u_xlat4.xyz, u_xlat5.xyz);
					    u_xlat4.xyz = u_xlat4.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat4.xyz);
					    u_xlat4.xyz = floor(u_xlat4.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _NormalTexVolume.x / _NormalTexSize.x;
					    u_xlat36 = u_xlat4.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / _NormalTexSize.xy;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat36;
					    u_xlat8.z = u_xlat4.z * u_xlat7.y;
					    u_xlat9 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat9 = u_xlat6.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat16.xyz = u_xlat4.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat36;
					    u_xlat10 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat23.yz = u_xlat8.yx;
					    u_xlat10 = u_xlat5.xxxx * u_xlat10;
					    u_xlat10 = u_xlat6.yyyy * u_xlat10;
					    u_xlat10 = u_xlat6.zzzz * u_xlat10;
					    u_xlat9 = u_xlat9 * u_xlat6.zzzz + u_xlat10;
					    u_xlat24.x = u_xlat24.x * u_xlat16.y;
					    u_xlat8.y = u_xlat7.x * u_xlat4.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat16.x + u_xlat24.x;
					    u_xlat23.x = u_xlat16.z * u_xlat7.y;
					    u_xlat4 = textureLod(_NormalTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.xz, 0.0);
					    u_xlat7 = u_xlat5.xxxx * u_xlat7;
					    u_xlat7 = u_xlat5.yyyy * u_xlat7;
					    u_xlat4 = u_xlat6.xxxx * u_xlat4;
					    u_xlat4 = u_xlat5.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat6.zzzz + u_xlat9;
					    u_xlat4 = u_xlat7 * u_xlat6.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat23.yx, 0.0);
					    u_xlat9 = textureLod(_NormalTex, u_xlat23.zx, 0.0);
					    u_xlat8.w = u_xlat23.x;
					    u_xlat9 = u_xlat5.xxxx * u_xlat9;
					    u_xlat9 = u_xlat6.yyyy * u_xlat9;
					    u_xlat7 = u_xlat6.xxxx * u_xlat7;
					    u_xlat7 = u_xlat6.yyyy * u_xlat7;
					    u_xlat4 = u_xlat7 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat9 * u_xlat5.zzzz + u_xlat4;
					    u_xlat7 = textureLod(_NormalTex, u_xlat8.yw, 0.0);
					    u_xlat8 = textureLod(_NormalTex, u_xlat8.xw, 0.0);
					    u_xlat8 = u_xlat5.xxxx * u_xlat8;
					    u_xlat8 = u_xlat5.yyyy * u_xlat8;
					    u_xlat6 = u_xlat6.xxxx * u_xlat7;
					    u_xlat6 = u_xlat5.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4 = u_xlat8 * u_xlat5.zzzz + u_xlat4;
					    u_xlat4.xyz = u_xlat4.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
					    u_xlat24.x = u_xlat4.w * 0.400000006;
					    u_xlat3.xyz = u_xlat3.xyz + (-u_xlat4.xyz);
					    u_xlat3.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat4.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.400000006, 0.400000006, 0.400000006) + u_xlat2.xyz;
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat4 = _AoTexVolume.xyzy + vec4(-0.5, -0.5, -0.5, -2.0);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat4.xyz);
					    u_xlat36 = (-u_xlat4.w) * 0.5 + u_xlat1.y;
					    u_xlat36 = u_xlat36 * 0.25;
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat4.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat5.xyz = (-u_xlat4.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat37 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat38 = u_xlat2.y * u_xlat37;
					    u_xlat6.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat38;
					    u_xlat7.z = u_xlat2.z * u_xlat6.y;
					    u_xlat8 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat8.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat9.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat38;
					    u_xlat10 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat14.yz = u_xlat7.yx;
					    u_xlat10.xyz = u_xlat4.xxx * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.yyy * u_xlat10.xyz;
					    u_xlat10.xyz = u_xlat5.zzz * u_xlat10.xyz;
					    u_xlat8.xyz = u_xlat8.xyz * u_xlat5.zzz + u_xlat10.xyz;
					    u_xlat37 = u_xlat37 * u_xlat9.y;
					    u_xlat7.y = u_xlat6.x * u_xlat2.x + u_xlat37;
					    u_xlat7.x = u_xlat6.x * u_xlat9.x + u_xlat37;
					    u_xlat14.x = u_xlat6.y * u_xlat9.z;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yz, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat7.xz, 0.0);
					    u_xlat9.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat9.xyz = u_xlat4.yyy * u_xlat9.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * u_xlat5.zzz + u_xlat8.xyz;
					    u_xlat6.xyz = u_xlat9.xyz * u_xlat5.zzz + u_xlat6.xyz;
					    u_xlat8 = textureLod(_AoTex, u_xlat14.yx, 0.0);
					    u_xlat9 = textureLod(_AoTex, u_xlat14.zx, 0.0);
					    u_xlat7.w = u_xlat14.x;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat9.xyz;
					    u_xlat2.xyz = u_xlat5.yyy * u_xlat2.xyz;
					    u_xlat8.xyz = u_xlat5.xxx * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat5.yyy * u_xlat8.xyz;
					    u_xlat17.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat4.zzz + u_xlat17.xyz;
					    u_xlat6 = textureLod(_AoTex, u_xlat7.yw, 0.0);
					    u_xlat7 = textureLod(_AoTex, u_xlat7.xw, 0.0);
					    u_xlat17.xyz = u_xlat4.xxx * u_xlat7.xyz;
					    u_xlat17.xyz = u_xlat4.yyy * u_xlat17.xyz;
					    u_xlat6.xyz = u_xlat5.xxx * u_xlat6.xyz;
					    u_xlat4.xyw = u_xlat4.yyy * u_xlat6.xyz;
					    u_xlat2.xyz = u_xlat4.xyw * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat2.xyz = u_xlat17.xyz * u_xlat4.zzz + u_xlat2.xyz;
					    u_xlat4.zw = _SunDir.xx * vec2(1.0, -1.0);
					    u_xlat4.xy = abs(_SunDir.yx);
					    u_xlat5.xyz = u_xlat4.zxw;
					    u_xlat5.xyz = clamp(u_xlat5.xyz, 0.0, 1.0);
					    u_xlat37 = u_xlat4.x + u_xlat4.y;
					    u_xlat2.xyw = u_xlat2.xyz * u_xlat5.xyz;
					    u_xlat39 = u_xlat2.y + u_xlat2.x;
					    u_xlat26 = u_xlat5.z * u_xlat2.z + u_xlat39;
					    u_xlat24.x = u_xlat26 * 0.600000024 + u_xlat24.x;
					    u_xlat26 = u_xlat36;
					    u_xlat26 = clamp(u_xlat26, 0.0, 1.0);
					    u_xlat36 = (-u_xlat36);
					    u_xlat36 = clamp(u_xlat36, 0.0, 1.0);
					    u_xlat39 = max(u_xlat36, u_xlat26);
					    u_xlat39 = (-u_xlat39) + 1.0;
					    u_xlat4.x = _Year;
					    u_xlat4.y = 0.0;
					    u_xlat4 = textureLod(_GrassTex, u_xlat4.xy, 0.0);
					    u_xlat5.xyz = vec3(u_xlat26) * u_xlat4.yzx;
					    u_xlat5.xyz = u_xlat4.xyz * vec3(u_xlat39) + u_xlat5.xyz;
					    u_xlat4.xyz = vec3(u_xlat36) * u_xlat4.zxy + u_xlat5.xyz;
					    u_xlat5.xyz = (-u_xlat4.xyz) + _SnowColor.xyz;
					    u_xlat4.xyz = vec3(_SnowAmount) * u_xlat5.xyz + u_xlat4.xyz;
					    u_xlat5.xyz = u_xlat4.xyz * _MinAmbientColor.xyz;
					    u_xlat6.xyz = (-_MinAmbientColor.xyz) * u_xlat4.xyz + _MaxAmbientColor.xyz;
					    u_xlat4.xyz = u_xlat4.xyz + (-_LongshipColor.xyz);
					    u_xlat5.xyz = u_xlat24.xxx * u_xlat6.xyz + u_xlat5.xyz;
					    u_xlat24.x = dot(u_xlat3.xyz, u_xlat3.xyz);
					    u_xlat24.x = inversesqrt(u_xlat24.x);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat3.xyz;
					    u_xlat3.w = (-u_xlat3.x);
					    u_xlat6.xyz = u_xlat3.xyw;
					    u_xlat6.xyz = clamp(u_xlat6.xyz, 0.0, 1.0);
					    u_xlat24.x = dot(u_xlat3.xyz, _FlashDir.xyz);
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat24.x = (-u_xlat24.x) + 1.0;
					    u_xlat2.xy = u_xlat2.xy * u_xlat6.xy;
					    u_xlat36 = u_xlat2.y + u_xlat2.x;
					    u_xlat36 = u_xlat2.w * u_xlat6.z + u_xlat36;
					    u_xlat36 = u_xlat36 / u_xlat37;
					    u_xlat2.xyz = _SideSunColor.xyz * _CloudCoverage.zzz;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(u_xlat36) + u_xlat5.xyz;
					    u_xlat36 = u_xlat24.x * u_xlat24.x;
					    u_xlat24.x = (-u_xlat24.x) * u_xlat36 + 1.0;
					    u_xlat3.xyz = u_xlat24.xxx * _FlashColor.xyz;
					    u_xlat2.xyz = u_xlat3.xyz * vec3(0.600000024, 0.600000024, 0.600000024) + u_xlat2.xyz;
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    vs_COLOR0.w = 1.0;
					    u_xlat3.xyz = u_xlat1.xyz + _PaintTexOffset.xyz;
					    u_xlat24.xy = max(u_xlat3.xz, _LevelRect.xy);
					    u_xlat3.xz = min(u_xlat24.xy, _LevelRect.zw);
					    u_xlat3.xyz = max(u_xlat3.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat3.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
					    u_xlat3.xyz = u_xlat3.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat3.xyz);
					    u_xlat3.xyz = floor(u_xlat3.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat24.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat36 = u_xlat3.y * u_xlat24.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat36;
					    u_xlat8.z = u_xlat3.z * u_xlat7.y;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat31.xy = u_xlat6.xx * u_xlat9.xy;
					    u_xlat31.xy = u_xlat6.yy * u_xlat31.xy;
					    u_xlat15.xyz = u_xlat3.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat36;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat22.yz = u_xlat8.yx;
					    u_xlat9.xy = u_xlat5.xx * u_xlat9.xy;
					    u_xlat9.xy = u_xlat6.yy * u_xlat9.xy;
					    u_xlat9.xy = u_xlat6.zz * u_xlat9.xy;
					    u_xlat31.xy = u_xlat31.xy * u_xlat6.zz + u_xlat9.xy;
					    u_xlat24.x = u_xlat24.x * u_xlat15.y;
					    u_xlat8.y = u_xlat7.x * u_xlat3.x + u_xlat24.x;
					    u_xlat8.x = u_xlat7.x * u_xlat15.x + u_xlat24.x;
					    u_xlat22.x = u_xlat15.z * u_xlat7.y;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat24.xy = u_xlat5.xx * u_xlat9.xy;
					    u_xlat24.xy = u_xlat5.yy * u_xlat24.xy;
					    u_xlat3.xy = u_xlat6.xx * u_xlat3.xy;
					    u_xlat3.xy = u_xlat5.yy * u_xlat3.xy;
					    u_xlat3.xy = u_xlat3.xy * u_xlat6.zz + u_xlat31.xy;
					    u_xlat24.xy = u_xlat24.xy * u_xlat6.zz + u_xlat3.xy;
					    u_xlat3 = textureLod(_PaintTex, u_xlat22.yx, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat22.zx, 0.0);
					    u_xlat8.w = u_xlat22.x;
					    u_xlat3.zw = u_xlat5.xx * u_xlat7.xy;
					    u_xlat3 = u_xlat6.xxyy * u_xlat3;
					    u_xlat3.xy = u_xlat6.yy * u_xlat3.xy;
					    u_xlat24.xy = u_xlat3.xy * u_xlat5.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat3.zw * u_xlat5.zz + u_xlat24.xy;
					    u_xlat3 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat3.zw = u_xlat5.xx * u_xlat7.xy;
					    u_xlat3.xy = u_xlat6.xx * u_xlat3.xy;
					    u_xlat3 = u_xlat5.yyyy * u_xlat3;
					    u_xlat24.xy = u_xlat3.xy * u_xlat5.zz + u_xlat24.xy;
					    u_xlat24.xy = u_xlat3.zw * u_xlat5.zz + u_xlat24.xy;
					    u_xlat37 = in_COLOR0.w + in_COLOR0.w;
					    u_xlat37 = u_xlat37;
					    u_xlat37 = clamp(u_xlat37, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat37) * u_xlat4.xyz + _LongshipColor.xyz;
					    u_xlat4.xyz = (-u_xlat3.xyz) + vec3(0.400000006, 0.400000006, 0.400000006);
					    u_xlat3.xyz = u_xlat24.yyy * u_xlat4.xyz + u_xlat3.xyz;
					    u_xlat24.x = u_xlat24.x * 1.20000005 + -0.200000003;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    u_xlat4.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat3.xyz = u_xlat24.xxx * u_xlat4.xyz + u_xlat3.xyz;
					    vs_COLOR1.xyz = u_xlat2.xyz * u_xlat3.xyz;
					    u_xlat24.x = (-in_COLOR0.w) * 2.0 + 2.0;
					    u_xlat24.x = clamp(u_xlat24.x, 0.0, 1.0);
					    vs_COLOR1.w = u_xlat24.x;
					    u_xlat36 = u_xlat1.y + (-_WaterLevel);
					    u_xlat1.x = dot(u_xlat1.xz, u_xlat1.xz);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + (-_FogMinRad);
					    u_xlat13 = unity_OrthoParams.x / _ScreenParams.x;
					    u_xlat13 = u_xlat13 + u_xlat13;
					    u_xlat25 = _AAFactor + 1.0;
					    u_xlat13 = u_xlat25 * u_xlat13;
					    u_xlat13 = u_xlat13 * _LineWidth;
					    u_xlat36 = u_xlat36 / u_xlat13;
					    u_xlat36 = u_xlat36 + 0.5;
					    u_xlat13 = (-u_xlat36) + 2.0;
					    vs_TEXCOORD3.y = u_xlat24.x * u_xlat13 + u_xlat36;
					    vs_TEXCOORD2.x = u_xlat0.y;
					    vs_TEXCOORD4.xy = u_xlat0.xy;
					    u_xlat0.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD3.x = u_xlat1.x / u_xlat0.x;
					    vs_TEXCOORD3.x = clamp(vs_TEXCOORD3.x, 0.0, 1.0);
					    u_xlat0.x = sqrt(in_COLOR0.z);
					    vs_TEXCOORD2.y = u_xlat0.x * 2.0 + -2.0;
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[2];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_9[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec2 u_xlat3;
					vec3 u_xlat4;
					vec3 u_xlat5;
					float u_xlat6;
					float u_xlat8;
					vec2 u_xlat11;
					float u_xlat12;
					void main()
					{
					    u_xlat0.x = _Hover * 0.200000003 + 0.5;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat1.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = u_xlat2.xy * u_xlat1.xy + vs_TEXCOORD1.xy;
					    u_xlat12 = u_xlat1.x / _PartTex_TexelSize.x;
					    u_xlat1 = texture(_PartTex, u_xlat1.xy);
					    u_xlat2.x = dFdx(u_xlat12);
					    u_xlat12 = dFdy(u_xlat12);
					    u_xlat4.z = abs(u_xlat12) + abs(u_xlat2.x);
					    u_xlat4.xyz = u_xlat4.xxz * vec3(-0.5, 0.5, 0.100000001);
					    u_xlat2.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat6 = dFdx(u_xlat2.x);
					    u_xlat2.x = dFdy(u_xlat2.x);
					    u_xlat2.x = abs(u_xlat2.x) + abs(u_xlat6);
					    u_xlat2.xy = u_xlat2.xx * vec2(0.100000001, 0.00100000005);
					    u_xlat12 = max(u_xlat4.z, u_xlat2.y);
					    u_xlat3.xy = vec2(u_xlat12) * u_xlat4.xy;
					    u_xlat4.xy = u_xlat4.xy * u_xlat2.xx;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat3.xy = u_xlat3.xy * vec2(vec2(_LineWidth, _LineWidth)) + u_xlat0.xx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat11.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat12) * u_xlat11.xy + u_xlat3.xy;
					    u_xlat0.xw = (-vec2(u_xlat12)) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xw = max(u_xlat0.xw, vec2(0.0, 0.0));
					    u_xlat3.xy = min(u_xlat11.xy, vec2(1.0, 1.0));
					    u_xlat3.xy = (-u_xlat0.xw) + u_xlat3.xy;
					    u_xlat0.xw = (-u_xlat0.xw) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xw / u_xlat3.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat3.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat3.xy = u_xlat2.xx * u_xlat3.xy + u_xlat4.xy;
					    u_xlat4.xy = (-u_xlat2.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat4.xy = max(u_xlat4.xy, vec2(0.0, 0.0));
					    u_xlat2.xy = min(u_xlat3.xy, vec2(1.0, 1.0));
					    u_xlat2.xy = (-u_xlat4.xy) + u_xlat2.xy;
					    u_xlat4.xy = (-u_xlat4.xy) + u_xlat2.ww;
					    u_xlat4.xy = u_xlat4.xy / u_xlat2.xy;
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = (-u_xlat4.y) * u_xlat0.w + 1.0;
					    u_xlat8 = vs_TEXCOORD3.y;
					    u_xlat8 = clamp(u_xlat8, 0.0, 1.0);
					    u_xlat12 = u_xlat0.x * u_xlat8 + -0.00999999978;
					    u_xlat0.x = u_xlat8 * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat12<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-u_xlat2.z) + 1.0;
					    u_xlat8 = u_xlat2.z + 1.0;
					    u_xlat1.xyz = vec3(u_xlat8) * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat4.x = (-u_xlat4.x) + 1.0;
					    u_xlat8 = (-u_xlat4.x) * vs_COLOR1.w + 1.0;
					    u_xlat4.x = u_xlat4.x * vs_COLOR1.w;
					    u_xlat0.x = u_xlat8 * u_xlat0.x;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat4.xyz = u_xlat4.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat5.xyz = u_xlat4.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat4.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat5.xyz;
					    u_xlat1.xyz = (-u_xlat4.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[2];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_9[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec2 u_xlat3;
					vec3 u_xlat4;
					vec3 u_xlat5;
					float u_xlat6;
					float u_xlat8;
					vec2 u_xlat11;
					float u_xlat12;
					void main()
					{
					    u_xlat0.x = _Hover * 0.200000003 + 0.5;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat1.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = u_xlat2.xy * u_xlat1.xy + vs_TEXCOORD1.xy;
					    u_xlat12 = u_xlat1.x / _PartTex_TexelSize.x;
					    u_xlat1 = texture(_PartTex, u_xlat1.xy);
					    u_xlat2.x = dFdx(u_xlat12);
					    u_xlat12 = dFdy(u_xlat12);
					    u_xlat4.z = abs(u_xlat12) + abs(u_xlat2.x);
					    u_xlat4.xyz = u_xlat4.xxz * vec3(-0.5, 0.5, 0.100000001);
					    u_xlat2.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat6 = dFdx(u_xlat2.x);
					    u_xlat2.x = dFdy(u_xlat2.x);
					    u_xlat2.x = abs(u_xlat2.x) + abs(u_xlat6);
					    u_xlat2.xy = u_xlat2.xx * vec2(0.100000001, 0.00100000005);
					    u_xlat12 = max(u_xlat4.z, u_xlat2.y);
					    u_xlat3.xy = vec2(u_xlat12) * u_xlat4.xy;
					    u_xlat4.xy = u_xlat4.xy * u_xlat2.xx;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat3.xy = u_xlat3.xy * vec2(vec2(_LineWidth, _LineWidth)) + u_xlat0.xx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat11.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat12) * u_xlat11.xy + u_xlat3.xy;
					    u_xlat0.xw = (-vec2(u_xlat12)) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xw = max(u_xlat0.xw, vec2(0.0, 0.0));
					    u_xlat3.xy = min(u_xlat11.xy, vec2(1.0, 1.0));
					    u_xlat3.xy = (-u_xlat0.xw) + u_xlat3.xy;
					    u_xlat0.xw = (-u_xlat0.xw) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xw / u_xlat3.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat3.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat3.xy = u_xlat2.xx * u_xlat3.xy + u_xlat4.xy;
					    u_xlat4.xy = (-u_xlat2.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat4.xy = max(u_xlat4.xy, vec2(0.0, 0.0));
					    u_xlat2.xy = min(u_xlat3.xy, vec2(1.0, 1.0));
					    u_xlat2.xy = (-u_xlat4.xy) + u_xlat2.xy;
					    u_xlat4.xy = (-u_xlat4.xy) + u_xlat2.ww;
					    u_xlat4.xy = u_xlat4.xy / u_xlat2.xy;
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = (-u_xlat4.y) * u_xlat0.w + 1.0;
					    u_xlat8 = vs_TEXCOORD3.y;
					    u_xlat8 = clamp(u_xlat8, 0.0, 1.0);
					    u_xlat12 = u_xlat0.x * u_xlat8 + -0.00999999978;
					    u_xlat0.x = u_xlat8 * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat12<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-u_xlat2.z) + 1.0;
					    u_xlat8 = u_xlat2.z + 1.0;
					    u_xlat1.xyz = vec3(u_xlat8) * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat4.x = (-u_xlat4.x) + 1.0;
					    u_xlat8 = (-u_xlat4.x) * vs_COLOR1.w + 1.0;
					    u_xlat4.x = u_xlat4.x * vs_COLOR1.w;
					    u_xlat0.x = u_xlat8 * u_xlat0.x;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat4.xyz = u_xlat4.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat5.xyz = u_xlat4.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat4.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat5.xyz;
					    u_xlat1.xyz = (-u_xlat4.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_LOWEND_ON" }
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
						vec4 unused_0_0[62];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_4[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD3.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat3.x = u_xlat1.w * u_xlat2.w;
					    u_xlat6 = u_xlat3.x * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat3.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat6<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-u_xlat1.w) * u_xlat2.w + 1.0;
					    u_xlat3.x = (-u_xlat1.z) + 1.0;
					    u_xlat6 = u_xlat1.z + 1.0;
					    u_xlat1.xyz = vec3(u_xlat6) * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat3.x = (-u_xlat3.x) + 1.0;
					    u_xlat6 = (-u_xlat3.x) * vs_COLOR1.w + 1.0;
					    u_xlat3.x = u_xlat3.x * vs_COLOR1.w;
					    u_xlat0.x = u_xlat6 * u_xlat0.x;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat4.xyz = u_xlat3.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat3.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat3.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat9 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat9 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat9) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_0[62];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_4[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD3.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat3.x = u_xlat1.w * u_xlat2.w;
					    u_xlat6 = u_xlat3.x * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat3.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat6<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-u_xlat1.w) * u_xlat2.w + 1.0;
					    u_xlat3.x = (-u_xlat1.z) + 1.0;
					    u_xlat6 = u_xlat1.z + 1.0;
					    u_xlat1.xyz = vec3(u_xlat6) * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat3.x = (-u_xlat3.x) + 1.0;
					    u_xlat6 = (-u_xlat3.x) * vs_COLOR1.w + 1.0;
					    u_xlat3.x = u_xlat3.x * vs_COLOR1.w;
					    u_xlat0.x = u_xlat6 * u_xlat0.x;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat4.xyz = u_xlat3.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat3.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat3.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat9 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat9 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat9) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" }
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
						vec4 unused_0_0[43];
						vec4 _BloodColor;
						vec4 unused_0_2[12];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[2];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_11[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec2 u_xlat4;
					vec3 u_xlat5;
					vec2 u_xlat8;
					vec2 u_xlat9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat4.x);
					    u_xlat0.z = _AAFactor + 1.0;
					    u_xlat0 = u_xlat0.xxzz * vec4(0.100000001, 0.00100000005, -0.5, 0.5);
					    u_xlat1.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat1.xy = u_xlat1.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = (-u_xlat1.xy) + vec2(1.0, 1.0);
					    u_xlat9.xy = u_xlat0.xx * u_xlat9.xy + u_xlat1.xy;
					    u_xlat1.xy = (-u_xlat0.xx) * u_xlat1.xy + u_xlat1.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat9.xy = min(u_xlat9.xy, vec2(1.0, 1.0));
					    u_xlat9.xy = (-u_xlat1.xy) + u_xlat9.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = (-u_xlat1.xy) + u_xlat2.ww;
					    u_xlat1.xy = u_xlat1.xy / u_xlat9.xy;
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat9.xy = u_xlat2.xy * u_xlat9.xy + vs_TEXCOORD1.xy;
					    u_xlat0.x = u_xlat9.x / _PartTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat2.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat0.x = max(u_xlat0.x, u_xlat0.y);
					    u_xlat4.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat12 = _Hover * 0.200000003 + 0.5;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(u_xlat12);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat0.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat8.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat8.xy = (-u_xlat0.xy) + u_xlat8.xy;
					    u_xlat3 = texture(_PartTex, u_xlat9.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat3.ww;
					    u_xlat0.xy = u_xlat0.xy / u_xlat8.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    u_xlat0.y = (-u_xlat1.y) * u_xlat0.y + 1.0;
					    u_xlat8.x = vs_TEXCOORD3.y;
					    u_xlat8.x = clamp(u_xlat8.x, 0.0, 1.0);
					    u_xlat12 = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat12<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat8.x = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat8.x) + abs(u_xlat0.x);
					    u_xlat8.x = dFdx(u_xlat9.x);
					    u_xlat12 = dFdy(u_xlat9.x);
					    u_xlat1.xy = u_xlat9.xy + (-vs_TEXCOORD4.xy);
					    u_xlat8.x = abs(u_xlat12) + abs(u_xlat8.x);
					    u_xlat8.x = u_xlat8.x * 10.0;
					    u_xlat0.x = u_xlat8.x / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + vs_TEXCOORD4.xy;
					    u_xlat1.z = u_xlat3.y * 0.200000003 + u_xlat1.y;
					    u_xlat0.xz = u_xlat1.xz * vec2(4.0, 4.0);
					    u_xlat1 = texture(_BloodTex, u_xlat0.xz);
					    u_xlat0.x = u_xlat1.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat2.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat1.x = u_xlat2.z + 1.0;
					    u_xlat1.y = (-u_xlat2.z) + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.xyxx;
					    u_xlat0.xzw = u_xlat0.xzw * vs_COLOR0.xyz;
					    u_xlat1.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xzw);
					    u_xlat13 = vs_TEXCOORD2.x * 5.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat13 = (-u_xlat13) + 1.0;
					    u_xlat2.x = u_xlat13 * vs_COLOR1.w;
					    u_xlat13 = (-u_xlat13) * vs_COLOR1.w + 1.0;
					    u_xlat4.x = u_xlat0.y * u_xlat13;
					    u_xlat0.xzw = u_xlat2.xxx * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat5.xyz = u_xlat0.xzw * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xzw) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat5.xyz;
					    u_xlat1.xyz = (-u_xlat0.xzw) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat4.xxx * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_0[43];
						vec4 _BloodColor;
						vec4 unused_0_2[12];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[2];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_11[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec2 u_xlat4;
					vec3 u_xlat5;
					vec2 u_xlat8;
					vec2 u_xlat9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat4.x);
					    u_xlat0.z = _AAFactor + 1.0;
					    u_xlat0 = u_xlat0.xxzz * vec4(0.100000001, 0.00100000005, -0.5, 0.5);
					    u_xlat1.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat1.xy = u_xlat1.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = (-u_xlat1.xy) + vec2(1.0, 1.0);
					    u_xlat9.xy = u_xlat0.xx * u_xlat9.xy + u_xlat1.xy;
					    u_xlat1.xy = (-u_xlat0.xx) * u_xlat1.xy + u_xlat1.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat9.xy = min(u_xlat9.xy, vec2(1.0, 1.0));
					    u_xlat9.xy = (-u_xlat1.xy) + u_xlat9.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = (-u_xlat1.xy) + u_xlat2.ww;
					    u_xlat1.xy = u_xlat1.xy / u_xlat9.xy;
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat9.xy = u_xlat2.xy * u_xlat9.xy + vs_TEXCOORD1.xy;
					    u_xlat0.x = u_xlat9.x / _PartTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat2.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat0.x = max(u_xlat0.x, u_xlat0.y);
					    u_xlat4.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat12 = _Hover * 0.200000003 + 0.5;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(u_xlat12);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat0.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat8.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat8.xy = (-u_xlat0.xy) + u_xlat8.xy;
					    u_xlat3 = texture(_PartTex, u_xlat9.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat3.ww;
					    u_xlat0.xy = u_xlat0.xy / u_xlat8.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    u_xlat0.y = (-u_xlat1.y) * u_xlat0.y + 1.0;
					    u_xlat8.x = vs_TEXCOORD3.y;
					    u_xlat8.x = clamp(u_xlat8.x, 0.0, 1.0);
					    u_xlat12 = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat12<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat8.x = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat8.x) + abs(u_xlat0.x);
					    u_xlat8.x = dFdx(u_xlat9.x);
					    u_xlat12 = dFdy(u_xlat9.x);
					    u_xlat1.xy = u_xlat9.xy + (-vs_TEXCOORD4.xy);
					    u_xlat8.x = abs(u_xlat12) + abs(u_xlat8.x);
					    u_xlat8.x = u_xlat8.x * 10.0;
					    u_xlat0.x = u_xlat8.x / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + vs_TEXCOORD4.xy;
					    u_xlat1.z = u_xlat3.y * 0.200000003 + u_xlat1.y;
					    u_xlat0.xz = u_xlat1.xz * vec2(4.0, 4.0);
					    u_xlat1 = texture(_BloodTex, u_xlat0.xz);
					    u_xlat0.x = u_xlat1.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat2.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat1.x = u_xlat2.z + 1.0;
					    u_xlat1.y = (-u_xlat2.z) + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.xyxx;
					    u_xlat0.xzw = u_xlat0.xzw * vs_COLOR0.xyz;
					    u_xlat1.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xzw);
					    u_xlat13 = vs_TEXCOORD2.x * 5.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat13 = (-u_xlat13) + 1.0;
					    u_xlat2.x = u_xlat13 * vs_COLOR1.w;
					    u_xlat13 = (-u_xlat13) * vs_COLOR1.w + 1.0;
					    u_xlat4.x = u_xlat0.y * u_xlat13;
					    u_xlat0.xzw = u_xlat2.xxx * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat5.xyz = u_xlat0.xzw * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xzw) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat5.xyz;
					    u_xlat1.xyz = (-u_xlat0.xzw) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat4.xxx * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_LOWEND_ON" }
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
						vec4 unused_0_0[43];
						vec4 _BloodColor;
						vec4 unused_0_2[18];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_6[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat9;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD3.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat9 = u_xlat1.w * u_xlat2.w;
					    u_xlat1.x = u_xlat9 * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat9;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat1.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat9 = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat9) + abs(u_xlat0.x);
					    u_xlat9 = dFdx(u_xlat3.x);
					    u_xlat1.x = dFdy(u_xlat3.x);
					    u_xlat3.xy = u_xlat3.xy + (-vs_TEXCOORD4.xy);
					    u_xlat9 = abs(u_xlat9) + abs(u_xlat1.x);
					    u_xlat9 = u_xlat9 * 10.0;
					    u_xlat0.x = u_xlat9 / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat0.xy = u_xlat0.xx * u_xlat3.xy + vs_TEXCOORD4.xy;
					    u_xlat0.z = u_xlat2.y * 0.200000003 + u_xlat0.y;
					    u_xlat0.xy = u_xlat0.xz * vec2(4.0, 4.0);
					    u_xlat0 = texture(_BloodTex, u_xlat0.xy);
					    u_xlat0.x = u_xlat0.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat2.xyz * _BloodColor.xyz + (-u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.w = (-u_xlat1.w) * u_xlat2.w + 1.0;
					    u_xlat1.x = u_xlat1.z + 1.0;
					    u_xlat1.y = (-u_xlat1.z) + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.xxxy;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat1.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xyz);
					    u_xlat10 = vs_TEXCOORD2.x * 5.0;
					    u_xlat10 = clamp(u_xlat10, 0.0, 1.0);
					    u_xlat10 = (-u_xlat10) + 1.0;
					    u_xlat2.x = u_xlat10 * vs_COLOR1.w;
					    u_xlat10 = (-u_xlat10) * vs_COLOR1.w + 1.0;
					    u_xlat9 = u_xlat0.w * u_xlat10;
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat4.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat9 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat9 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat9) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_0[43];
						vec4 _BloodColor;
						vec4 unused_0_2[18];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_6[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat9;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD3.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat9 = u_xlat1.w * u_xlat2.w;
					    u_xlat1.x = u_xlat9 * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat9;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat1.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat9 = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat9) + abs(u_xlat0.x);
					    u_xlat9 = dFdx(u_xlat3.x);
					    u_xlat1.x = dFdy(u_xlat3.x);
					    u_xlat3.xy = u_xlat3.xy + (-vs_TEXCOORD4.xy);
					    u_xlat9 = abs(u_xlat9) + abs(u_xlat1.x);
					    u_xlat9 = u_xlat9 * 10.0;
					    u_xlat0.x = u_xlat9 / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat0.xy = u_xlat0.xx * u_xlat3.xy + vs_TEXCOORD4.xy;
					    u_xlat0.z = u_xlat2.y * 0.200000003 + u_xlat0.y;
					    u_xlat0.xy = u_xlat0.xz * vec2(4.0, 4.0);
					    u_xlat0 = texture(_BloodTex, u_xlat0.xy);
					    u_xlat0.x = u_xlat0.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat2.xyz * _BloodColor.xyz + (-u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.w = (-u_xlat1.w) * u_xlat2.w + 1.0;
					    u_xlat1.x = u_xlat1.z + 1.0;
					    u_xlat1.y = (-u_xlat1.z) + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.xxxy;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat1.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xyz);
					    u_xlat10 = vs_TEXCOORD2.x * 5.0;
					    u_xlat10 = clamp(u_xlat10, 0.0, 1.0);
					    u_xlat10 = (-u_xlat10) + 1.0;
					    u_xlat2.x = u_xlat10 * vs_COLOR1.w;
					    u_xlat10 = (-u_xlat10) * vs_COLOR1.w + 1.0;
					    u_xlat9 = u_xlat0.w * u_xlat10;
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat4.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat9 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat9 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat9) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
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
						vec4 unused_0_4[5];
						float _LineWidth;
						vec4 unused_0_6;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_10;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_15[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat6;
					vec2 u_xlat8;
					vec2 u_xlat11;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.x = _Hover * 0.200000003 + 0.5;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat1.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = u_xlat2.xy * u_xlat1.xy + vs_TEXCOORD1.xy;
					    u_xlat12 = u_xlat1.x / _PartTex_TexelSize.x;
					    u_xlat1 = texture(_PartTex, u_xlat1.xy);
					    u_xlat2.x = dFdx(u_xlat12);
					    u_xlat12 = dFdy(u_xlat12);
					    u_xlat4.z = abs(u_xlat12) + abs(u_xlat2.x);
					    u_xlat4.xyz = u_xlat4.xxz * vec3(-0.5, 0.5, 0.100000001);
					    u_xlat2.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat6 = dFdx(u_xlat2.x);
					    u_xlat2.x = dFdy(u_xlat2.x);
					    u_xlat2.x = abs(u_xlat2.x) + abs(u_xlat6);
					    u_xlat2.xy = u_xlat2.xx * vec2(0.100000001, 0.00100000005);
					    u_xlat12 = max(u_xlat4.z, u_xlat2.y);
					    u_xlat3.xy = vec2(u_xlat12) * u_xlat4.xy;
					    u_xlat4.xy = u_xlat4.xy * u_xlat2.xx;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat3.xy = u_xlat3.xy * vec2(vec2(_LineWidth, _LineWidth)) + u_xlat0.xx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat11.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat12) * u_xlat11.xy + u_xlat3.xy;
					    u_xlat0.xw = (-vec2(u_xlat12)) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xw = max(u_xlat0.xw, vec2(0.0, 0.0));
					    u_xlat3.xy = min(u_xlat11.xy, vec2(1.0, 1.0));
					    u_xlat3.xy = (-u_xlat0.xw) + u_xlat3.xy;
					    u_xlat0.xw = (-u_xlat0.xw) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xw / u_xlat3.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat3.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat3.xy = u_xlat2.xx * u_xlat3.xy + u_xlat4.xy;
					    u_xlat4.xy = (-u_xlat2.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat4.xy = max(u_xlat4.xy, vec2(0.0, 0.0));
					    u_xlat2.xy = min(u_xlat3.xy, vec2(1.0, 1.0));
					    u_xlat2.xy = (-u_xlat4.xy) + u_xlat2.xy;
					    u_xlat4.xy = (-u_xlat4.xy) + u_xlat2.ww;
					    u_xlat4.xy = u_xlat4.xy / u_xlat2.xy;
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = (-u_xlat4.y) * u_xlat0.w + 1.0;
					    u_xlat8.xy = vs_TEXCOORD3.yx;
					    u_xlat8.xy = clamp(u_xlat8.xy, 0.0, 1.0);
					    u_xlat13 = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat13<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-u_xlat2.z) + 1.0;
					    u_xlat8.x = u_xlat2.z + 1.0;
					    u_xlat1.xyz = u_xlat8.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat4.x = (-u_xlat4.x) + 1.0;
					    u_xlat8.x = (-u_xlat4.x) * vs_COLOR1.w + 1.0;
					    u_xlat4.x = u_xlat4.x * vs_COLOR1.w;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat1.xyz = u_xlat4.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat4.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * u_xlat4.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat13 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat13) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat8.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
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
						vec4 unused_0_4[5];
						float _LineWidth;
						vec4 unused_0_6;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_10;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_15[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat6;
					vec2 u_xlat8;
					vec2 u_xlat11;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.x = _Hover * 0.200000003 + 0.5;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat1.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = u_xlat2.xy * u_xlat1.xy + vs_TEXCOORD1.xy;
					    u_xlat12 = u_xlat1.x / _PartTex_TexelSize.x;
					    u_xlat1 = texture(_PartTex, u_xlat1.xy);
					    u_xlat2.x = dFdx(u_xlat12);
					    u_xlat12 = dFdy(u_xlat12);
					    u_xlat4.z = abs(u_xlat12) + abs(u_xlat2.x);
					    u_xlat4.xyz = u_xlat4.xxz * vec3(-0.5, 0.5, 0.100000001);
					    u_xlat2.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat6 = dFdx(u_xlat2.x);
					    u_xlat2.x = dFdy(u_xlat2.x);
					    u_xlat2.x = abs(u_xlat2.x) + abs(u_xlat6);
					    u_xlat2.xy = u_xlat2.xx * vec2(0.100000001, 0.00100000005);
					    u_xlat12 = max(u_xlat4.z, u_xlat2.y);
					    u_xlat3.xy = vec2(u_xlat12) * u_xlat4.xy;
					    u_xlat4.xy = u_xlat4.xy * u_xlat2.xx;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat3.xy = u_xlat3.xy * vec2(vec2(_LineWidth, _LineWidth)) + u_xlat0.xx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat11.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat12) * u_xlat11.xy + u_xlat3.xy;
					    u_xlat0.xw = (-vec2(u_xlat12)) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xw = max(u_xlat0.xw, vec2(0.0, 0.0));
					    u_xlat3.xy = min(u_xlat11.xy, vec2(1.0, 1.0));
					    u_xlat3.xy = (-u_xlat0.xw) + u_xlat3.xy;
					    u_xlat0.xw = (-u_xlat0.xw) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xw / u_xlat3.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat3.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat3.xy = u_xlat2.xx * u_xlat3.xy + u_xlat4.xy;
					    u_xlat4.xy = (-u_xlat2.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat4.xy = max(u_xlat4.xy, vec2(0.0, 0.0));
					    u_xlat2.xy = min(u_xlat3.xy, vec2(1.0, 1.0));
					    u_xlat2.xy = (-u_xlat4.xy) + u_xlat2.xy;
					    u_xlat4.xy = (-u_xlat4.xy) + u_xlat2.ww;
					    u_xlat4.xy = u_xlat4.xy / u_xlat2.xy;
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = (-u_xlat4.y) * u_xlat0.w + 1.0;
					    u_xlat8.xy = vs_TEXCOORD3.yx;
					    u_xlat8.xy = clamp(u_xlat8.xy, 0.0, 1.0);
					    u_xlat13 = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat13<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-u_xlat2.z) + 1.0;
					    u_xlat8.x = u_xlat2.z + 1.0;
					    u_xlat1.xyz = u_xlat8.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat4.x = (-u_xlat4.x) + 1.0;
					    u_xlat8.x = (-u_xlat4.x) * vs_COLOR1.w + 1.0;
					    u_xlat4.x = u_xlat4.x * vs_COLOR1.w;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat1.xyz = u_xlat4.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat4.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * u_xlat4.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat13 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat13) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat8.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_LOWEND_ON" }
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
						vec4 unused_0_8;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_12[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec3 u_xlat4;
					float u_xlat9;
					void main()
					{
					    u_xlat0.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat1.xy * u_xlat0.xy + vs_TEXCOORD1.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    u_xlat1.x = u_xlat0.w * u_xlat1.w;
					    u_xlat2.xy = vs_TEXCOORD3.yx;
					    u_xlat2.xy = clamp(u_xlat2.xy, 0.0, 1.0);
					    u_xlat4.x = u_xlat1.x * u_xlat2.x + -0.00999999978;
					    u_xlat1.x = u_xlat1.x * u_xlat2.x;
					    SV_Target0.w = u_xlat1.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb1 = u_xlat4.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.w = (-u_xlat1.w) * u_xlat0.w + 1.0;
					    u_xlat1.x = (-u_xlat1.z) + 1.0;
					    u_xlat1.y = u_xlat1.z + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.yyyx;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat1.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat4.x = (-u_xlat1.x) * vs_COLOR1.w + 1.0;
					    u_xlat1.x = u_xlat1.x * vs_COLOR1.w;
					    u_xlat9 = u_xlat0.w * u_xlat4.x;
					    u_xlat4.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xyz);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat4.xyz + u_xlat0.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat4.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat2.xzw = (-u_xlat0.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xzw + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat9 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat9 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat9)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat9 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat9) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_8;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_12[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec3 u_xlat4;
					float u_xlat9;
					void main()
					{
					    u_xlat0.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat1.xy * u_xlat0.xy + vs_TEXCOORD1.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    u_xlat1.x = u_xlat0.w * u_xlat1.w;
					    u_xlat2.xy = vs_TEXCOORD3.yx;
					    u_xlat2.xy = clamp(u_xlat2.xy, 0.0, 1.0);
					    u_xlat4.x = u_xlat1.x * u_xlat2.x + -0.00999999978;
					    u_xlat1.x = u_xlat1.x * u_xlat2.x;
					    SV_Target0.w = u_xlat1.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb1 = u_xlat4.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.w = (-u_xlat1.w) * u_xlat0.w + 1.0;
					    u_xlat1.x = (-u_xlat1.z) + 1.0;
					    u_xlat1.y = u_xlat1.z + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.yyyx;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat1.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat4.x = (-u_xlat1.x) * vs_COLOR1.w + 1.0;
					    u_xlat1.x = u_xlat1.x * vs_COLOR1.w;
					    u_xlat9 = u_xlat0.w * u_xlat4.x;
					    u_xlat4.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xyz);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat4.xyz + u_xlat0.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat4.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat2.xzw = (-u_xlat0.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xzw + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat9 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat9 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat9)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat9);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat9 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat9) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" }
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
						vec4 unused_0_2[33];
						vec4 _BloodColor;
						vec4 unused_0_4[6];
						vec4 _CloudCoverage;
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_17[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec2 u_xlat4;
					vec2 u_xlat5;
					vec2 u_xlat8;
					vec2 u_xlat9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat4.x);
					    u_xlat0.z = _AAFactor + 1.0;
					    u_xlat0 = u_xlat0.xxzz * vec4(0.100000001, 0.00100000005, -0.5, 0.5);
					    u_xlat1.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat1.xy = u_xlat1.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = (-u_xlat1.xy) + vec2(1.0, 1.0);
					    u_xlat9.xy = u_xlat0.xx * u_xlat9.xy + u_xlat1.xy;
					    u_xlat1.xy = (-u_xlat0.xx) * u_xlat1.xy + u_xlat1.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat9.xy = min(u_xlat9.xy, vec2(1.0, 1.0));
					    u_xlat9.xy = (-u_xlat1.xy) + u_xlat9.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = (-u_xlat1.xy) + u_xlat2.ww;
					    u_xlat1.xy = u_xlat1.xy / u_xlat9.xy;
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat9.xy = u_xlat2.xy * u_xlat9.xy + vs_TEXCOORD1.xy;
					    u_xlat0.x = u_xlat9.x / _PartTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat2.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat0.x = max(u_xlat0.x, u_xlat0.y);
					    u_xlat4.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat12 = _Hover * 0.200000003 + 0.5;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(u_xlat12);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat0.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat8.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat8.xy = (-u_xlat0.xy) + u_xlat8.xy;
					    u_xlat3 = texture(_PartTex, u_xlat9.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat3.ww;
					    u_xlat0.xy = u_xlat0.xy / u_xlat8.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    u_xlat4.x = (-u_xlat1.y) * u_xlat0.y + 1.0;
					    u_xlat8.xy = vs_TEXCOORD3.yx;
					    u_xlat8.xy = clamp(u_xlat8.xy, 0.0, 1.0);
					    u_xlat1.x = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat1.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat8.x = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat8.x) + abs(u_xlat0.x);
					    u_xlat8.x = dFdx(u_xlat9.x);
					    u_xlat1.x = dFdy(u_xlat9.x);
					    u_xlat5.xy = u_xlat9.xy + (-vs_TEXCOORD4.xy);
					    u_xlat8.x = abs(u_xlat8.x) + abs(u_xlat1.x);
					    u_xlat8.x = u_xlat8.x * 10.0;
					    u_xlat0.x = u_xlat8.x / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat5.xy + vs_TEXCOORD4.xy;
					    u_xlat1.z = u_xlat3.y * 0.200000003 + u_xlat1.y;
					    u_xlat0.xz = u_xlat1.xz * vec2(4.0, 4.0);
					    u_xlat1 = texture(_BloodTex, u_xlat0.xz);
					    u_xlat0.x = u_xlat1.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat2.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat0.x = u_xlat2.z + 1.0;
					    u_xlat8.x = (-u_xlat2.z) + 1.0;
					    u_xlat4.x = u_xlat8.x * u_xlat4.x;
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat0.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat8.x = u_xlat0.x * vs_COLOR1.w;
					    u_xlat0.x = (-u_xlat0.x) * vs_COLOR1.w + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat1.xyz = u_xlat8.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat4.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * u_xlat4.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat13 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat13) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat8.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_2[33];
						vec4 _BloodColor;
						vec4 unused_0_4[6];
						vec4 _CloudCoverage;
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_17[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec2 u_xlat4;
					vec2 u_xlat5;
					vec2 u_xlat8;
					vec2 u_xlat9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat4.x);
					    u_xlat0.z = _AAFactor + 1.0;
					    u_xlat0 = u_xlat0.xxzz * vec4(0.100000001, 0.00100000005, -0.5, 0.5);
					    u_xlat1.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat1.xy = u_xlat1.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = (-u_xlat1.xy) + vec2(1.0, 1.0);
					    u_xlat9.xy = u_xlat0.xx * u_xlat9.xy + u_xlat1.xy;
					    u_xlat1.xy = (-u_xlat0.xx) * u_xlat1.xy + u_xlat1.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat9.xy = min(u_xlat9.xy, vec2(1.0, 1.0));
					    u_xlat9.xy = (-u_xlat1.xy) + u_xlat9.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = (-u_xlat1.xy) + u_xlat2.ww;
					    u_xlat1.xy = u_xlat1.xy / u_xlat9.xy;
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat9.xy = u_xlat2.xy * u_xlat9.xy + vs_TEXCOORD1.xy;
					    u_xlat0.x = u_xlat9.x / _PartTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat2.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat0.x = max(u_xlat0.x, u_xlat0.y);
					    u_xlat4.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat12 = _Hover * 0.200000003 + 0.5;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(u_xlat12);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat0.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat8.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat8.xy = (-u_xlat0.xy) + u_xlat8.xy;
					    u_xlat3 = texture(_PartTex, u_xlat9.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat3.ww;
					    u_xlat0.xy = u_xlat0.xy / u_xlat8.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    u_xlat4.x = (-u_xlat1.y) * u_xlat0.y + 1.0;
					    u_xlat8.xy = vs_TEXCOORD3.yx;
					    u_xlat8.xy = clamp(u_xlat8.xy, 0.0, 1.0);
					    u_xlat1.x = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat1.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat8.x = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat8.x) + abs(u_xlat0.x);
					    u_xlat8.x = dFdx(u_xlat9.x);
					    u_xlat1.x = dFdy(u_xlat9.x);
					    u_xlat5.xy = u_xlat9.xy + (-vs_TEXCOORD4.xy);
					    u_xlat8.x = abs(u_xlat8.x) + abs(u_xlat1.x);
					    u_xlat8.x = u_xlat8.x * 10.0;
					    u_xlat0.x = u_xlat8.x / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat5.xy + vs_TEXCOORD4.xy;
					    u_xlat1.z = u_xlat3.y * 0.200000003 + u_xlat1.y;
					    u_xlat0.xz = u_xlat1.xz * vec2(4.0, 4.0);
					    u_xlat1 = texture(_BloodTex, u_xlat0.xz);
					    u_xlat0.x = u_xlat1.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat2.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat0.x = u_xlat2.z + 1.0;
					    u_xlat8.x = (-u_xlat2.z) + 1.0;
					    u_xlat4.x = u_xlat8.x * u_xlat4.x;
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat0.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat8.x = u_xlat0.x * vs_COLOR1.w;
					    u_xlat0.x = (-u_xlat0.x) * vs_COLOR1.w + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat1.xyz = u_xlat8.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat4.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * u_xlat4.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat13 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat13) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat8.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_LOWEND_ON" }
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
						vec4 unused_0_2[33];
						vec4 _BloodColor;
						vec4 unused_0_4[6];
						vec4 _CloudCoverage;
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_14[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat5;
					float u_xlat6;
					vec2 u_xlat8;
					float u_xlat9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD3.yx;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat8.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat8.xy = u_xlat1.xy * u_xlat8.xy + vs_TEXCOORD1.xy;
					    u_xlat2 = texture(_PartTex, u_xlat8.xy);
					    u_xlat1.x = u_xlat1.w * u_xlat2.w;
					    u_xlat5.x = u_xlat1.x * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat5.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat1.x = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat1.x);
					    u_xlat1.x = dFdx(u_xlat8.x);
					    u_xlat5.x = dFdy(u_xlat8.x);
					    u_xlat8.xy = u_xlat8.xy + (-vs_TEXCOORD4.xy);
					    u_xlat1.x = abs(u_xlat5.x) + abs(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x * 10.0;
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat3.xy = u_xlat0.xx * u_xlat8.xy + vs_TEXCOORD4.xy;
					    u_xlat3.z = u_xlat2.y * 0.200000003 + u_xlat3.y;
					    u_xlat0.xz = u_xlat3.xz * vec2(4.0, 4.0);
					    u_xlat3 = texture(_BloodTex, u_xlat0.xz);
					    u_xlat0.x = u_xlat3.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat2.xyz * _BloodColor.xyz + (-u_xlat2.xyz);
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat1.x = (-u_xlat1.w) * u_xlat2.w + 1.0;
					    u_xlat5.x = u_xlat1.z + 1.0;
					    u_xlat9 = (-u_xlat1.z) + 1.0;
					    u_xlat1.x = u_xlat9 * u_xlat1.x;
					    u_xlat0.xzw = u_xlat0.xzw * u_xlat5.xxx;
					    u_xlat0.xzw = u_xlat0.xzw * vs_COLOR0.xyz;
					    u_xlat5.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xzw);
					    u_xlat2.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat6 = u_xlat2.x * vs_COLOR1.w;
					    u_xlat2.x = (-u_xlat2.x) * vs_COLOR1.w + 1.0;
					    u_xlat1.x = u_xlat1.x * u_xlat2.x;
					    u_xlat0.xzw = vec3(u_xlat6) * u_xlat5.xyz + u_xlat0.xzw;
					    u_xlat5.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat2.xyz = u_xlat0.xzw * u_xlat5.xxx;
					    u_xlat5.xyz = (-u_xlat0.xzw) * u_xlat5.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat5.xyz = vec3(_Hover) * u_xlat5.xyz + u_xlat2.xyz;
					    u_xlat5.xyz = (-u_xlat0.xzw) + u_xlat5.xyz;
					    u_xlat0.xzw = u_xlat1.xxx * u_xlat5.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = (-u_xlat0.xzw) + vs_COLOR0.xyz;
					    u_xlat13 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat0.xzw = vec3(u_xlat13) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.x = dot(u_xlat0.xzw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xzw = u_xlat0.xzw + (-u_xlat1.xxx);
					    u_xlat0.xzw = _CloudCoverage.yyy * u_xlat0.xzw + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xzw) + _LutLerp.www;
					    u_xlat0.xzw = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xzw) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.yyy * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_2[33];
						vec4 _BloodColor;
						vec4 unused_0_4[6];
						vec4 _CloudCoverage;
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_14[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat5;
					float u_xlat6;
					vec2 u_xlat8;
					float u_xlat9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD3.yx;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat8.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat8.xy = u_xlat1.xy * u_xlat8.xy + vs_TEXCOORD1.xy;
					    u_xlat2 = texture(_PartTex, u_xlat8.xy);
					    u_xlat1.x = u_xlat1.w * u_xlat2.w;
					    u_xlat5.x = u_xlat1.x * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat5.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat1.x = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat1.x);
					    u_xlat1.x = dFdx(u_xlat8.x);
					    u_xlat5.x = dFdy(u_xlat8.x);
					    u_xlat8.xy = u_xlat8.xy + (-vs_TEXCOORD4.xy);
					    u_xlat1.x = abs(u_xlat5.x) + abs(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x * 10.0;
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat3.xy = u_xlat0.xx * u_xlat8.xy + vs_TEXCOORD4.xy;
					    u_xlat3.z = u_xlat2.y * 0.200000003 + u_xlat3.y;
					    u_xlat0.xz = u_xlat3.xz * vec2(4.0, 4.0);
					    u_xlat3 = texture(_BloodTex, u_xlat0.xz);
					    u_xlat0.x = u_xlat3.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat2.xyz * _BloodColor.xyz + (-u_xlat2.xyz);
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat1.x = (-u_xlat1.w) * u_xlat2.w + 1.0;
					    u_xlat5.x = u_xlat1.z + 1.0;
					    u_xlat9 = (-u_xlat1.z) + 1.0;
					    u_xlat1.x = u_xlat9 * u_xlat1.x;
					    u_xlat0.xzw = u_xlat0.xzw * u_xlat5.xxx;
					    u_xlat0.xzw = u_xlat0.xzw * vs_COLOR0.xyz;
					    u_xlat5.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xzw);
					    u_xlat2.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat6 = u_xlat2.x * vs_COLOR1.w;
					    u_xlat2.x = (-u_xlat2.x) * vs_COLOR1.w + 1.0;
					    u_xlat1.x = u_xlat1.x * u_xlat2.x;
					    u_xlat0.xzw = vec3(u_xlat6) * u_xlat5.xyz + u_xlat0.xzw;
					    u_xlat5.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat2.xyz = u_xlat0.xzw * u_xlat5.xxx;
					    u_xlat5.xyz = (-u_xlat0.xzw) * u_xlat5.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat5.xyz = vec3(_Hover) * u_xlat5.xyz + u_xlat2.xyz;
					    u_xlat5.xyz = (-u_xlat0.xzw) + u_xlat5.xyz;
					    u_xlat0.xzw = u_xlat1.xxx * u_xlat5.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = (-u_xlat0.xzw) + vs_COLOR0.xyz;
					    u_xlat13 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat0.xzw = vec3(u_xlat13) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.x = dot(u_xlat0.xzw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xzw = u_xlat0.xzw + (-u_xlat1.xxx);
					    u_xlat0.xzw = _CloudCoverage.yyy * u_xlat0.xzw + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xzw) + _LutLerp.www;
					    u_xlat0.xzw = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xzw) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.yyy * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat12 = u_xlat0.y + 0.5;
					    u_xlat1.xyz = _SelectionColor.xyz * vec3(u_xlat12) + (-u_xlat0.xyz);
					    u_xlat0.xyz = _SelectionColor.www * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Hover) * vec3(0.200000003, 0.200000003, 0.200000003) + u_xlat0.xyz;
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
						vec4 unused_0_2[8];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[3];
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_10[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					float u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0 = _Hover * 0.200000003 + 0.5;
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat9 = u_xlat3.x / _PartTex_TexelSize.x;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat3.x = dFdx(u_xlat9);
					    u_xlat6 = dFdy(u_xlat9);
					    u_xlat3.x = abs(u_xlat6) + abs(u_xlat3.x);
					    u_xlat6 = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat9 = dFdx(u_xlat6);
					    u_xlat6 = dFdy(u_xlat6);
					    u_xlat3.y = abs(u_xlat6) + abs(u_xlat9);
					    u_xlat3.xyz = u_xlat3.xyy * vec3(0.100000001, 0.100000001, 0.00100000005);
					    u_xlat3.x = max(u_xlat3.x, u_xlat3.z);
					    u_xlat9 = _AAFactor + 1.0;
					    u_xlat9 = u_xlat9 * -0.5;
					    u_xlat1.x = u_xlat3.x * u_xlat9;
					    u_xlat9 = u_xlat3.y * u_xlat9;
					    u_xlat9 = u_xlat9 * _LineWidth + 0.5;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0 = u_xlat1.x * _LineWidth + u_xlat0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat0) + 1.0;
					    u_xlat1.x = u_xlat3.x * u_xlat1.x + u_xlat0;
					    u_xlat0 = (-u_xlat3.x) * u_xlat0 + u_xlat0;
					    u_xlat0 = max(u_xlat0, 0.0);
					    u_xlat3.x = min(u_xlat1.x, 1.0);
					    u_xlat3.x = (-u_xlat0) + u_xlat3.x;
					    u_xlat0 = (-u_xlat0) + u_xlat2.w;
					    u_xlat0 = u_xlat0 / u_xlat3.x;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat3.x = (-u_xlat9) + 1.0;
					    u_xlat3.x = u_xlat3.y * u_xlat3.x + u_xlat9;
					    u_xlat6 = (-u_xlat3.y) * u_xlat9 + u_xlat9;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat3.x = min(u_xlat3.x, 1.0);
					    u_xlat3.x = (-u_xlat6) + u_xlat3.x;
					    u_xlat6 = (-u_xlat6) + u_xlat1.w;
					    u_xlat3.x = u_xlat6 / u_xlat3.x;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat0 = u_xlat0 * u_xlat3.x;
					    u_xlat3.x = vs_TEXCOORD3.y;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat6 = u_xlat0 * u_xlat3.x + -0.00999999978;
					    u_xlat0 = u_xlat3.x * u_xlat0;
					    SV_Target0.w = u_xlat0;
					    u_xlatb0 = u_xlat6<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat3.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat3.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat3.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = vec3(u_xlat0) * u_xlat1.xyz + u_xlat3.xyz;
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
						vec4 unused_0_2[8];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[3];
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_10[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					float u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0 = _Hover * 0.200000003 + 0.5;
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat9 = u_xlat3.x / _PartTex_TexelSize.x;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat3.x = dFdx(u_xlat9);
					    u_xlat6 = dFdy(u_xlat9);
					    u_xlat3.x = abs(u_xlat6) + abs(u_xlat3.x);
					    u_xlat6 = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat9 = dFdx(u_xlat6);
					    u_xlat6 = dFdy(u_xlat6);
					    u_xlat3.y = abs(u_xlat6) + abs(u_xlat9);
					    u_xlat3.xyz = u_xlat3.xyy * vec3(0.100000001, 0.100000001, 0.00100000005);
					    u_xlat3.x = max(u_xlat3.x, u_xlat3.z);
					    u_xlat9 = _AAFactor + 1.0;
					    u_xlat9 = u_xlat9 * -0.5;
					    u_xlat1.x = u_xlat3.x * u_xlat9;
					    u_xlat9 = u_xlat3.y * u_xlat9;
					    u_xlat9 = u_xlat9 * _LineWidth + 0.5;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0 = u_xlat1.x * _LineWidth + u_xlat0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat0) + 1.0;
					    u_xlat1.x = u_xlat3.x * u_xlat1.x + u_xlat0;
					    u_xlat0 = (-u_xlat3.x) * u_xlat0 + u_xlat0;
					    u_xlat0 = max(u_xlat0, 0.0);
					    u_xlat3.x = min(u_xlat1.x, 1.0);
					    u_xlat3.x = (-u_xlat0) + u_xlat3.x;
					    u_xlat0 = (-u_xlat0) + u_xlat2.w;
					    u_xlat0 = u_xlat0 / u_xlat3.x;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat3.x = (-u_xlat9) + 1.0;
					    u_xlat3.x = u_xlat3.y * u_xlat3.x + u_xlat9;
					    u_xlat6 = (-u_xlat3.y) * u_xlat9 + u_xlat9;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat3.x = min(u_xlat3.x, 1.0);
					    u_xlat3.x = (-u_xlat6) + u_xlat3.x;
					    u_xlat6 = (-u_xlat6) + u_xlat1.w;
					    u_xlat3.x = u_xlat6 / u_xlat3.x;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat0 = u_xlat0 * u_xlat3.x;
					    u_xlat3.x = vs_TEXCOORD3.y;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat6 = u_xlat0 * u_xlat3.x + -0.00999999978;
					    u_xlat0 = u_xlat3.x * u_xlat0;
					    SV_Target0.w = u_xlat0;
					    u_xlatb0 = u_xlat6<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat3.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat3.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat3.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = vec3(u_xlat0) * u_xlat1.xyz + u_xlat3.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_LOWEND_ON" }
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
						vec4 unused_0_2[21];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_2[21];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" }
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
						vec4 unused_0_2[8];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[3];
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_10[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					float u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0 = _Hover * 0.200000003 + 0.5;
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat9 = u_xlat3.x / _PartTex_TexelSize.x;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat3.x = dFdx(u_xlat9);
					    u_xlat6 = dFdy(u_xlat9);
					    u_xlat3.x = abs(u_xlat6) + abs(u_xlat3.x);
					    u_xlat6 = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat9 = dFdx(u_xlat6);
					    u_xlat6 = dFdy(u_xlat6);
					    u_xlat3.y = abs(u_xlat6) + abs(u_xlat9);
					    u_xlat3.xyz = u_xlat3.xyy * vec3(0.100000001, 0.100000001, 0.00100000005);
					    u_xlat3.x = max(u_xlat3.x, u_xlat3.z);
					    u_xlat9 = _AAFactor + 1.0;
					    u_xlat9 = u_xlat9 * -0.5;
					    u_xlat1.x = u_xlat3.x * u_xlat9;
					    u_xlat9 = u_xlat3.y * u_xlat9;
					    u_xlat9 = u_xlat9 * _LineWidth + 0.5;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0 = u_xlat1.x * _LineWidth + u_xlat0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat0) + 1.0;
					    u_xlat1.x = u_xlat3.x * u_xlat1.x + u_xlat0;
					    u_xlat0 = (-u_xlat3.x) * u_xlat0 + u_xlat0;
					    u_xlat0 = max(u_xlat0, 0.0);
					    u_xlat3.x = min(u_xlat1.x, 1.0);
					    u_xlat3.x = (-u_xlat0) + u_xlat3.x;
					    u_xlat0 = (-u_xlat0) + u_xlat2.w;
					    u_xlat0 = u_xlat0 / u_xlat3.x;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat3.x = (-u_xlat9) + 1.0;
					    u_xlat3.x = u_xlat3.y * u_xlat3.x + u_xlat9;
					    u_xlat6 = (-u_xlat3.y) * u_xlat9 + u_xlat9;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat3.x = min(u_xlat3.x, 1.0);
					    u_xlat3.x = (-u_xlat6) + u_xlat3.x;
					    u_xlat6 = (-u_xlat6) + u_xlat1.w;
					    u_xlat3.x = u_xlat6 / u_xlat3.x;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat0 = u_xlat0 * u_xlat3.x;
					    u_xlat3.x = vs_TEXCOORD3.y;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat6 = u_xlat0 * u_xlat3.x + -0.00999999978;
					    u_xlat0 = u_xlat3.x * u_xlat0;
					    SV_Target0.w = u_xlat0;
					    u_xlatb0 = u_xlat6<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat3.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat3.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat3.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = vec3(u_xlat0) * u_xlat1.xyz + u_xlat3.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_2[8];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[3];
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_10[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					float u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0 = _Hover * 0.200000003 + 0.5;
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat9 = u_xlat3.x / _PartTex_TexelSize.x;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat3.x = dFdx(u_xlat9);
					    u_xlat6 = dFdy(u_xlat9);
					    u_xlat3.x = abs(u_xlat6) + abs(u_xlat3.x);
					    u_xlat6 = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat9 = dFdx(u_xlat6);
					    u_xlat6 = dFdy(u_xlat6);
					    u_xlat3.y = abs(u_xlat6) + abs(u_xlat9);
					    u_xlat3.xyz = u_xlat3.xyy * vec3(0.100000001, 0.100000001, 0.00100000005);
					    u_xlat3.x = max(u_xlat3.x, u_xlat3.z);
					    u_xlat9 = _AAFactor + 1.0;
					    u_xlat9 = u_xlat9 * -0.5;
					    u_xlat1.x = u_xlat3.x * u_xlat9;
					    u_xlat9 = u_xlat3.y * u_xlat9;
					    u_xlat9 = u_xlat9 * _LineWidth + 0.5;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0 = u_xlat1.x * _LineWidth + u_xlat0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat0) + 1.0;
					    u_xlat1.x = u_xlat3.x * u_xlat1.x + u_xlat0;
					    u_xlat0 = (-u_xlat3.x) * u_xlat0 + u_xlat0;
					    u_xlat0 = max(u_xlat0, 0.0);
					    u_xlat3.x = min(u_xlat1.x, 1.0);
					    u_xlat3.x = (-u_xlat0) + u_xlat3.x;
					    u_xlat0 = (-u_xlat0) + u_xlat2.w;
					    u_xlat0 = u_xlat0 / u_xlat3.x;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat3.x = (-u_xlat9) + 1.0;
					    u_xlat3.x = u_xlat3.y * u_xlat3.x + u_xlat9;
					    u_xlat6 = (-u_xlat3.y) * u_xlat9 + u_xlat9;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat3.x = min(u_xlat3.x, 1.0);
					    u_xlat3.x = (-u_xlat6) + u_xlat3.x;
					    u_xlat6 = (-u_xlat6) + u_xlat1.w;
					    u_xlat3.x = u_xlat6 / u_xlat3.x;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat0 = u_xlat0 * u_xlat3.x;
					    u_xlat3.x = vs_TEXCOORD3.y;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat6 = u_xlat0 * u_xlat3.x + -0.00999999978;
					    u_xlat0 = u_xlat3.x * u_xlat0;
					    SV_Target0.w = u_xlat0;
					    u_xlatb0 = u_xlat6<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
					    u_xlat3.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat3.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat3.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = vec3(u_xlat0) * u_xlat1.xyz + u_xlat3.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_LOWEND_ON" }
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
						vec4 unused_0_2[21];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_2[21];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    SV_Target0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    SV_Target0.w = 0.0;
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
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12[2];
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_16[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = _Hover * 0.200000003 + 0.5;
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat9 = u_xlat3.x / _PartTex_TexelSize.x;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat3.x = dFdx(u_xlat9);
					    u_xlat6 = dFdy(u_xlat9);
					    u_xlat3.x = abs(u_xlat6) + abs(u_xlat3.x);
					    u_xlat6 = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat9 = dFdx(u_xlat6);
					    u_xlat6 = dFdy(u_xlat6);
					    u_xlat3.y = abs(u_xlat6) + abs(u_xlat9);
					    u_xlat3.xyz = u_xlat3.xyy * vec3(0.100000001, 0.100000001, 0.00100000005);
					    u_xlat3.x = max(u_xlat3.x, u_xlat3.z);
					    u_xlat9 = _AAFactor + 1.0;
					    u_xlat9 = u_xlat9 * -0.5;
					    u_xlat1.x = u_xlat3.x * u_xlat9;
					    u_xlat9 = u_xlat3.y * u_xlat9;
					    u_xlat9 = u_xlat9 * _LineWidth + 0.5;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.x = u_xlat1.x * _LineWidth + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat0.x) + 1.0;
					    u_xlat1.x = u_xlat3.x * u_xlat1.x + u_xlat0.x;
					    u_xlat0.x = (-u_xlat3.x) * u_xlat0.x + u_xlat0.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat1.x, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat2.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.x = (-u_xlat9) + 1.0;
					    u_xlat3.x = u_xlat3.y * u_xlat3.x + u_xlat9;
					    u_xlat6 = (-u_xlat3.y) * u_xlat9 + u_xlat9;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat3.x = min(u_xlat3.x, 1.0);
					    u_xlat3.x = (-u_xlat6) + u_xlat3.x;
					    u_xlat6 = (-u_xlat6) + u_xlat1.w;
					    u_xlat3.x = u_xlat6 / u_xlat3.x;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat3.x;
					    u_xlat3.xy = vs_TEXCOORD3.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD3.y) + 2.0;
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
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12[2];
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_16[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = _Hover * 0.200000003 + 0.5;
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat9 = u_xlat3.x / _PartTex_TexelSize.x;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat3.x = dFdx(u_xlat9);
					    u_xlat6 = dFdy(u_xlat9);
					    u_xlat3.x = abs(u_xlat6) + abs(u_xlat3.x);
					    u_xlat6 = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat9 = dFdx(u_xlat6);
					    u_xlat6 = dFdy(u_xlat6);
					    u_xlat3.y = abs(u_xlat6) + abs(u_xlat9);
					    u_xlat3.xyz = u_xlat3.xyy * vec3(0.100000001, 0.100000001, 0.00100000005);
					    u_xlat3.x = max(u_xlat3.x, u_xlat3.z);
					    u_xlat9 = _AAFactor + 1.0;
					    u_xlat9 = u_xlat9 * -0.5;
					    u_xlat1.x = u_xlat3.x * u_xlat9;
					    u_xlat9 = u_xlat3.y * u_xlat9;
					    u_xlat9 = u_xlat9 * _LineWidth + 0.5;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.x = u_xlat1.x * _LineWidth + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat0.x) + 1.0;
					    u_xlat1.x = u_xlat3.x * u_xlat1.x + u_xlat0.x;
					    u_xlat0.x = (-u_xlat3.x) * u_xlat0.x + u_xlat0.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat1.x, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat2.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.x = (-u_xlat9) + 1.0;
					    u_xlat3.x = u_xlat3.y * u_xlat3.x + u_xlat9;
					    u_xlat6 = (-u_xlat3.y) * u_xlat9 + u_xlat9;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat3.x = min(u_xlat3.x, 1.0);
					    u_xlat3.x = (-u_xlat6) + u_xlat3.x;
					    u_xlat6 = (-u_xlat6) + u_xlat1.w;
					    u_xlat3.x = u_xlat6 / u_xlat3.x;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat3.x;
					    u_xlat3.xy = vs_TEXCOORD3.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD3.y) + 2.0;
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
					Keywords { "_MIRROR_ON" "_GAME_ON" "_LOWEND_ON" }
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
						vec4 unused_0_10[8];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD3.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_10[8];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD3.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_GAME_ON" }
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
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12[2];
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_16[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = _Hover * 0.200000003 + 0.5;
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat9 = u_xlat3.x / _PartTex_TexelSize.x;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat3.x = dFdx(u_xlat9);
					    u_xlat6 = dFdy(u_xlat9);
					    u_xlat3.x = abs(u_xlat6) + abs(u_xlat3.x);
					    u_xlat6 = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat9 = dFdx(u_xlat6);
					    u_xlat6 = dFdy(u_xlat6);
					    u_xlat3.y = abs(u_xlat6) + abs(u_xlat9);
					    u_xlat3.xyz = u_xlat3.xyy * vec3(0.100000001, 0.100000001, 0.00100000005);
					    u_xlat3.x = max(u_xlat3.x, u_xlat3.z);
					    u_xlat9 = _AAFactor + 1.0;
					    u_xlat9 = u_xlat9 * -0.5;
					    u_xlat1.x = u_xlat3.x * u_xlat9;
					    u_xlat9 = u_xlat3.y * u_xlat9;
					    u_xlat9 = u_xlat9 * _LineWidth + 0.5;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.x = u_xlat1.x * _LineWidth + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat0.x) + 1.0;
					    u_xlat1.x = u_xlat3.x * u_xlat1.x + u_xlat0.x;
					    u_xlat0.x = (-u_xlat3.x) * u_xlat0.x + u_xlat0.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat1.x, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat2.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.x = (-u_xlat9) + 1.0;
					    u_xlat3.x = u_xlat3.y * u_xlat3.x + u_xlat9;
					    u_xlat6 = (-u_xlat3.y) * u_xlat9 + u_xlat9;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat3.x = min(u_xlat3.x, 1.0);
					    u_xlat3.x = (-u_xlat6) + u_xlat3.x;
					    u_xlat6 = (-u_xlat6) + u_xlat1.w;
					    u_xlat3.x = u_xlat6 / u_xlat3.x;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat3.x;
					    u_xlat3.xy = vs_TEXCOORD3.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD3.y) + 2.0;
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
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12[2];
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_16[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = _Hover * 0.200000003 + 0.5;
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat9 = u_xlat3.x / _PartTex_TexelSize.x;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat3.x = dFdx(u_xlat9);
					    u_xlat6 = dFdy(u_xlat9);
					    u_xlat3.x = abs(u_xlat6) + abs(u_xlat3.x);
					    u_xlat6 = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat9 = dFdx(u_xlat6);
					    u_xlat6 = dFdy(u_xlat6);
					    u_xlat3.y = abs(u_xlat6) + abs(u_xlat9);
					    u_xlat3.xyz = u_xlat3.xyy * vec3(0.100000001, 0.100000001, 0.00100000005);
					    u_xlat3.x = max(u_xlat3.x, u_xlat3.z);
					    u_xlat9 = _AAFactor + 1.0;
					    u_xlat9 = u_xlat9 * -0.5;
					    u_xlat1.x = u_xlat3.x * u_xlat9;
					    u_xlat9 = u_xlat3.y * u_xlat9;
					    u_xlat9 = u_xlat9 * _LineWidth + 0.5;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.x = u_xlat1.x * _LineWidth + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat0.x) + 1.0;
					    u_xlat1.x = u_xlat3.x * u_xlat1.x + u_xlat0.x;
					    u_xlat0.x = (-u_xlat3.x) * u_xlat0.x + u_xlat0.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat3.x = min(u_xlat1.x, 1.0);
					    u_xlat3.x = (-u_xlat0.x) + u_xlat3.x;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat2.w;
					    u_xlat0.x = u_xlat0.x / u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.x = (-u_xlat9) + 1.0;
					    u_xlat3.x = u_xlat3.y * u_xlat3.x + u_xlat9;
					    u_xlat6 = (-u_xlat3.y) * u_xlat9 + u_xlat9;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat3.x = min(u_xlat3.x, 1.0);
					    u_xlat3.x = (-u_xlat6) + u_xlat3.x;
					    u_xlat6 = (-u_xlat6) + u_xlat1.w;
					    u_xlat3.x = u_xlat6 / u_xlat3.x;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat3.x;
					    u_xlat3.xy = vs_TEXCOORD3.yx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat9 = u_xlat0.x * u_xlat3.x + -0.00999999978;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat9<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD3.y) + 2.0;
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
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_GAME_ON" "_LOWEND_ON" }
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
						vec4 unused_0_10[8];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD3.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_MIRROR_ON" "_GAME_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_10[8];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec3 u_xlat1;
					vec3 u_xlat2;
					float u_xlat6;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.w + -0.5;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat2.xyz = vs_COLOR0.xyz * vec3(0.0800000057, 0.0800000057, 0.0800000057);
					    u_xlat2.xyz = _MirrorColor2.xyz * vec3(0.699999988, 0.699999988, 0.699999988) + u_xlat2.xyz;
					    u_xlat1.xyz = (-u_xlat2.xyz) + vs_COLOR0.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat6 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat6)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat6);
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat6 = vs_TEXCOORD3.x;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat6) * u_xlat1.xyz + u_xlat0.xyz;
					    SV_Target0.w = 0.0;
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[2];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_9[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec2 u_xlat3;
					vec3 u_xlat4;
					vec3 u_xlat5;
					float u_xlat6;
					float u_xlat8;
					vec2 u_xlat11;
					float u_xlat12;
					void main()
					{
					    u_xlat0.x = _Hover * 0.200000003 + 0.5;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat1.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = u_xlat2.xy * u_xlat1.xy + vs_TEXCOORD1.xy;
					    u_xlat12 = u_xlat1.x / _PartTex_TexelSize.x;
					    u_xlat1 = texture(_PartTex, u_xlat1.xy);
					    u_xlat2.x = dFdx(u_xlat12);
					    u_xlat12 = dFdy(u_xlat12);
					    u_xlat4.z = abs(u_xlat12) + abs(u_xlat2.x);
					    u_xlat4.xyz = u_xlat4.xxz * vec3(-0.5, 0.5, 0.100000001);
					    u_xlat2.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat6 = dFdx(u_xlat2.x);
					    u_xlat2.x = dFdy(u_xlat2.x);
					    u_xlat2.x = abs(u_xlat2.x) + abs(u_xlat6);
					    u_xlat2.xy = u_xlat2.xx * vec2(0.100000001, 0.00100000005);
					    u_xlat12 = max(u_xlat4.z, u_xlat2.y);
					    u_xlat3.xy = vec2(u_xlat12) * u_xlat4.xy;
					    u_xlat4.xy = u_xlat4.xy * u_xlat2.xx;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat3.xy = u_xlat3.xy * vec2(vec2(_LineWidth, _LineWidth)) + u_xlat0.xx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat11.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat12) * u_xlat11.xy + u_xlat3.xy;
					    u_xlat0.xw = (-vec2(u_xlat12)) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xw = max(u_xlat0.xw, vec2(0.0, 0.0));
					    u_xlat3.xy = min(u_xlat11.xy, vec2(1.0, 1.0));
					    u_xlat3.xy = (-u_xlat0.xw) + u_xlat3.xy;
					    u_xlat0.xw = (-u_xlat0.xw) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xw / u_xlat3.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat3.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat3.xy = u_xlat2.xx * u_xlat3.xy + u_xlat4.xy;
					    u_xlat4.xy = (-u_xlat2.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat4.xy = max(u_xlat4.xy, vec2(0.0, 0.0));
					    u_xlat2.xy = min(u_xlat3.xy, vec2(1.0, 1.0));
					    u_xlat2.xy = (-u_xlat4.xy) + u_xlat2.xy;
					    u_xlat4.xy = (-u_xlat4.xy) + u_xlat2.ww;
					    u_xlat4.xy = u_xlat4.xy / u_xlat2.xy;
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = (-u_xlat4.y) * u_xlat0.w + 1.0;
					    u_xlat8 = vs_TEXCOORD3.y;
					    u_xlat8 = clamp(u_xlat8, 0.0, 1.0);
					    u_xlat12 = u_xlat0.x * u_xlat8 + -0.00999999978;
					    u_xlat0.x = u_xlat8 * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat12<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-u_xlat2.z) + 1.0;
					    u_xlat8 = u_xlat2.z + 1.0;
					    u_xlat1.xyz = vec3(u_xlat8) * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat4.x = (-u_xlat4.x) + 1.0;
					    u_xlat8 = (-u_xlat4.x) * vs_COLOR1.w + 1.0;
					    u_xlat4.x = u_xlat4.x * vs_COLOR1.w;
					    u_xlat0.x = u_xlat8 * u_xlat0.x;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat4.xyz = u_xlat4.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat5.xyz = u_xlat4.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat4.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat5.xyz;
					    u_xlat1.xyz = (-u_xlat4.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[2];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_9[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec2 u_xlat3;
					vec3 u_xlat4;
					vec3 u_xlat5;
					float u_xlat6;
					float u_xlat8;
					vec2 u_xlat11;
					float u_xlat12;
					void main()
					{
					    u_xlat0.x = _Hover * 0.200000003 + 0.5;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat1.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = u_xlat2.xy * u_xlat1.xy + vs_TEXCOORD1.xy;
					    u_xlat12 = u_xlat1.x / _PartTex_TexelSize.x;
					    u_xlat1 = texture(_PartTex, u_xlat1.xy);
					    u_xlat2.x = dFdx(u_xlat12);
					    u_xlat12 = dFdy(u_xlat12);
					    u_xlat4.z = abs(u_xlat12) + abs(u_xlat2.x);
					    u_xlat4.xyz = u_xlat4.xxz * vec3(-0.5, 0.5, 0.100000001);
					    u_xlat2.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat6 = dFdx(u_xlat2.x);
					    u_xlat2.x = dFdy(u_xlat2.x);
					    u_xlat2.x = abs(u_xlat2.x) + abs(u_xlat6);
					    u_xlat2.xy = u_xlat2.xx * vec2(0.100000001, 0.00100000005);
					    u_xlat12 = max(u_xlat4.z, u_xlat2.y);
					    u_xlat3.xy = vec2(u_xlat12) * u_xlat4.xy;
					    u_xlat4.xy = u_xlat4.xy * u_xlat2.xx;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat3.xy = u_xlat3.xy * vec2(vec2(_LineWidth, _LineWidth)) + u_xlat0.xx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat11.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat12) * u_xlat11.xy + u_xlat3.xy;
					    u_xlat0.xw = (-vec2(u_xlat12)) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xw = max(u_xlat0.xw, vec2(0.0, 0.0));
					    u_xlat3.xy = min(u_xlat11.xy, vec2(1.0, 1.0));
					    u_xlat3.xy = (-u_xlat0.xw) + u_xlat3.xy;
					    u_xlat0.xw = (-u_xlat0.xw) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xw / u_xlat3.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat3.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat3.xy = u_xlat2.xx * u_xlat3.xy + u_xlat4.xy;
					    u_xlat4.xy = (-u_xlat2.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat4.xy = max(u_xlat4.xy, vec2(0.0, 0.0));
					    u_xlat2.xy = min(u_xlat3.xy, vec2(1.0, 1.0));
					    u_xlat2.xy = (-u_xlat4.xy) + u_xlat2.xy;
					    u_xlat4.xy = (-u_xlat4.xy) + u_xlat2.ww;
					    u_xlat4.xy = u_xlat4.xy / u_xlat2.xy;
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = (-u_xlat4.y) * u_xlat0.w + 1.0;
					    u_xlat8 = vs_TEXCOORD3.y;
					    u_xlat8 = clamp(u_xlat8, 0.0, 1.0);
					    u_xlat12 = u_xlat0.x * u_xlat8 + -0.00999999978;
					    u_xlat0.x = u_xlat8 * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat12<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-u_xlat2.z) + 1.0;
					    u_xlat8 = u_xlat2.z + 1.0;
					    u_xlat1.xyz = vec3(u_xlat8) * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat4.x = (-u_xlat4.x) + 1.0;
					    u_xlat8 = (-u_xlat4.x) * vs_COLOR1.w + 1.0;
					    u_xlat4.x = u_xlat4.x * vs_COLOR1.w;
					    u_xlat0.x = u_xlat8 * u_xlat0.x;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat4.xyz = u_xlat4.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat5.xyz = u_xlat4.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat4.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat5.xyz;
					    u_xlat1.xyz = (-u_xlat4.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_LOWEND_ON" }
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
						vec4 unused_0_0[62];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_4[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD3.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat3.x = u_xlat1.w * u_xlat2.w;
					    u_xlat6 = u_xlat3.x * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat3.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat6<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-u_xlat1.w) * u_xlat2.w + 1.0;
					    u_xlat3.x = (-u_xlat1.z) + 1.0;
					    u_xlat6 = u_xlat1.z + 1.0;
					    u_xlat1.xyz = vec3(u_xlat6) * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat3.x = (-u_xlat3.x) + 1.0;
					    u_xlat6 = (-u_xlat3.x) * vs_COLOR1.w + 1.0;
					    u_xlat3.x = u_xlat3.x * vs_COLOR1.w;
					    u_xlat0.x = u_xlat6 * u_xlat0.x;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat4.xyz = u_xlat3.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat3.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat3.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat9 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_0[62];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_4[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat6;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD3.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat3.x = u_xlat1.w * u_xlat2.w;
					    u_xlat6 = u_xlat3.x * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat3.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat6<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-u_xlat1.w) * u_xlat2.w + 1.0;
					    u_xlat3.x = (-u_xlat1.z) + 1.0;
					    u_xlat6 = u_xlat1.z + 1.0;
					    u_xlat1.xyz = vec3(u_xlat6) * u_xlat2.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat3.x * u_xlat0.x;
					    u_xlat3.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat3.x = (-u_xlat3.x) + 1.0;
					    u_xlat6 = (-u_xlat3.x) * vs_COLOR1.w + 1.0;
					    u_xlat3.x = u_xlat3.x * vs_COLOR1.w;
					    u_xlat0.x = u_xlat6 * u_xlat0.x;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat3.xyz = u_xlat3.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat4.xyz = u_xlat3.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat3.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat3.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat9 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_0[43];
						vec4 _BloodColor;
						vec4 unused_0_2[12];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[2];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_11[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec2 u_xlat4;
					vec3 u_xlat5;
					vec2 u_xlat8;
					vec2 u_xlat9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat4.x);
					    u_xlat0.z = _AAFactor + 1.0;
					    u_xlat0 = u_xlat0.xxzz * vec4(0.100000001, 0.00100000005, -0.5, 0.5);
					    u_xlat1.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat1.xy = u_xlat1.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = (-u_xlat1.xy) + vec2(1.0, 1.0);
					    u_xlat9.xy = u_xlat0.xx * u_xlat9.xy + u_xlat1.xy;
					    u_xlat1.xy = (-u_xlat0.xx) * u_xlat1.xy + u_xlat1.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat9.xy = min(u_xlat9.xy, vec2(1.0, 1.0));
					    u_xlat9.xy = (-u_xlat1.xy) + u_xlat9.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = (-u_xlat1.xy) + u_xlat2.ww;
					    u_xlat1.xy = u_xlat1.xy / u_xlat9.xy;
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat9.xy = u_xlat2.xy * u_xlat9.xy + vs_TEXCOORD1.xy;
					    u_xlat0.x = u_xlat9.x / _PartTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat2.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat0.x = max(u_xlat0.x, u_xlat0.y);
					    u_xlat4.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat12 = _Hover * 0.200000003 + 0.5;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(u_xlat12);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat0.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat8.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat8.xy = (-u_xlat0.xy) + u_xlat8.xy;
					    u_xlat3 = texture(_PartTex, u_xlat9.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat3.ww;
					    u_xlat0.xy = u_xlat0.xy / u_xlat8.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    u_xlat0.y = (-u_xlat1.y) * u_xlat0.y + 1.0;
					    u_xlat8.x = vs_TEXCOORD3.y;
					    u_xlat8.x = clamp(u_xlat8.x, 0.0, 1.0);
					    u_xlat12 = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat12<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat8.x = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat8.x) + abs(u_xlat0.x);
					    u_xlat8.x = dFdx(u_xlat9.x);
					    u_xlat12 = dFdy(u_xlat9.x);
					    u_xlat1.xy = u_xlat9.xy + (-vs_TEXCOORD4.xy);
					    u_xlat8.x = abs(u_xlat12) + abs(u_xlat8.x);
					    u_xlat8.x = u_xlat8.x * 10.0;
					    u_xlat0.x = u_xlat8.x / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + vs_TEXCOORD4.xy;
					    u_xlat1.z = u_xlat3.y * 0.200000003 + u_xlat1.y;
					    u_xlat0.xz = u_xlat1.xz * vec2(4.0, 4.0);
					    u_xlat1 = texture(_BloodTex, u_xlat0.xz);
					    u_xlat0.x = u_xlat1.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat2.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat1.x = u_xlat2.z + 1.0;
					    u_xlat1.y = (-u_xlat2.z) + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.xyxx;
					    u_xlat0.xzw = u_xlat0.xzw * vs_COLOR0.xyz;
					    u_xlat1.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xzw);
					    u_xlat13 = vs_TEXCOORD2.x * 5.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat13 = (-u_xlat13) + 1.0;
					    u_xlat2.x = u_xlat13 * vs_COLOR1.w;
					    u_xlat13 = (-u_xlat13) * vs_COLOR1.w + 1.0;
					    u_xlat4.x = u_xlat0.y * u_xlat13;
					    u_xlat0.xzw = u_xlat2.xxx * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat5.xyz = u_xlat0.xzw * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xzw) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat5.xyz;
					    u_xlat1.xyz = (-u_xlat0.xzw) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat4.xxx * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_0[43];
						vec4 _BloodColor;
						vec4 unused_0_2[12];
						float _LineWidth;
						vec4 unused_0_4[2];
						float _AAFactor;
						vec4 unused_0_6[2];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_11[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec2 u_xlat4;
					vec3 u_xlat5;
					vec2 u_xlat8;
					vec2 u_xlat9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat4.x);
					    u_xlat0.z = _AAFactor + 1.0;
					    u_xlat0 = u_xlat0.xxzz * vec4(0.100000001, 0.00100000005, -0.5, 0.5);
					    u_xlat1.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat1.xy = u_xlat1.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = (-u_xlat1.xy) + vec2(1.0, 1.0);
					    u_xlat9.xy = u_xlat0.xx * u_xlat9.xy + u_xlat1.xy;
					    u_xlat1.xy = (-u_xlat0.xx) * u_xlat1.xy + u_xlat1.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat9.xy = min(u_xlat9.xy, vec2(1.0, 1.0));
					    u_xlat9.xy = (-u_xlat1.xy) + u_xlat9.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = (-u_xlat1.xy) + u_xlat2.ww;
					    u_xlat1.xy = u_xlat1.xy / u_xlat9.xy;
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat9.xy = u_xlat2.xy * u_xlat9.xy + vs_TEXCOORD1.xy;
					    u_xlat0.x = u_xlat9.x / _PartTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat2.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat0.x = max(u_xlat0.x, u_xlat0.y);
					    u_xlat4.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat12 = _Hover * 0.200000003 + 0.5;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(u_xlat12);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat0.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat8.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat8.xy = (-u_xlat0.xy) + u_xlat8.xy;
					    u_xlat3 = texture(_PartTex, u_xlat9.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat3.ww;
					    u_xlat0.xy = u_xlat0.xy / u_xlat8.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    u_xlat0.y = (-u_xlat1.y) * u_xlat0.y + 1.0;
					    u_xlat8.x = vs_TEXCOORD3.y;
					    u_xlat8.x = clamp(u_xlat8.x, 0.0, 1.0);
					    u_xlat12 = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat12<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat8.x = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat8.x) + abs(u_xlat0.x);
					    u_xlat8.x = dFdx(u_xlat9.x);
					    u_xlat12 = dFdy(u_xlat9.x);
					    u_xlat1.xy = u_xlat9.xy + (-vs_TEXCOORD4.xy);
					    u_xlat8.x = abs(u_xlat12) + abs(u_xlat8.x);
					    u_xlat8.x = u_xlat8.x * 10.0;
					    u_xlat0.x = u_xlat8.x / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat1.xy + vs_TEXCOORD4.xy;
					    u_xlat1.z = u_xlat3.y * 0.200000003 + u_xlat1.y;
					    u_xlat0.xz = u_xlat1.xz * vec2(4.0, 4.0);
					    u_xlat1 = texture(_BloodTex, u_xlat0.xz);
					    u_xlat0.x = u_xlat1.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat2.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat1.x = u_xlat2.z + 1.0;
					    u_xlat1.y = (-u_xlat2.z) + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.xyxx;
					    u_xlat0.xzw = u_xlat0.xzw * vs_COLOR0.xyz;
					    u_xlat1.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xzw);
					    u_xlat13 = vs_TEXCOORD2.x * 5.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat13 = (-u_xlat13) + 1.0;
					    u_xlat2.x = u_xlat13 * vs_COLOR1.w;
					    u_xlat13 = (-u_xlat13) * vs_COLOR1.w + 1.0;
					    u_xlat4.x = u_xlat0.y * u_xlat13;
					    u_xlat0.xzw = u_xlat2.xxx * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat5.xyz = u_xlat0.xzw * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xzw) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat5.xyz;
					    u_xlat1.xyz = (-u_xlat0.xzw) + u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat4.xxx * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat12 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_CINEMATIC_ON" "_LOWEND_ON" }
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
						vec4 unused_0_0[43];
						vec4 _BloodColor;
						vec4 unused_0_2[18];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_6[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat9;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD3.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat9 = u_xlat1.w * u_xlat2.w;
					    u_xlat1.x = u_xlat9 * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat9;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat1.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat9 = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat9) + abs(u_xlat0.x);
					    u_xlat9 = dFdx(u_xlat3.x);
					    u_xlat1.x = dFdy(u_xlat3.x);
					    u_xlat3.xy = u_xlat3.xy + (-vs_TEXCOORD4.xy);
					    u_xlat9 = abs(u_xlat9) + abs(u_xlat1.x);
					    u_xlat9 = u_xlat9 * 10.0;
					    u_xlat0.x = u_xlat9 / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat0.xy = u_xlat0.xx * u_xlat3.xy + vs_TEXCOORD4.xy;
					    u_xlat0.z = u_xlat2.y * 0.200000003 + u_xlat0.y;
					    u_xlat0.xy = u_xlat0.xz * vec2(4.0, 4.0);
					    u_xlat0 = texture(_BloodTex, u_xlat0.xy);
					    u_xlat0.x = u_xlat0.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat2.xyz * _BloodColor.xyz + (-u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.w = (-u_xlat1.w) * u_xlat2.w + 1.0;
					    u_xlat1.x = u_xlat1.z + 1.0;
					    u_xlat1.y = (-u_xlat1.z) + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.xxxy;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat1.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xyz);
					    u_xlat10 = vs_TEXCOORD2.x * 5.0;
					    u_xlat10 = clamp(u_xlat10, 0.0, 1.0);
					    u_xlat10 = (-u_xlat10) + 1.0;
					    u_xlat2.x = u_xlat10 * vs_COLOR1.w;
					    u_xlat10 = (-u_xlat10) * vs_COLOR1.w + 1.0;
					    u_xlat9 = u_xlat0.w * u_xlat10;
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat4.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat9 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_0[43];
						vec4 _BloodColor;
						vec4 unused_0_2[18];
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_6[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat9;
					float u_xlat10;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD3.y;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xy = u_xlat1.xy * u_xlat3.xy + vs_TEXCOORD1.xy;
					    u_xlat2 = texture(_PartTex, u_xlat3.xy);
					    u_xlat9 = u_xlat1.w * u_xlat2.w;
					    u_xlat1.x = u_xlat9 * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat9;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat1.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat9 = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat9) + abs(u_xlat0.x);
					    u_xlat9 = dFdx(u_xlat3.x);
					    u_xlat1.x = dFdy(u_xlat3.x);
					    u_xlat3.xy = u_xlat3.xy + (-vs_TEXCOORD4.xy);
					    u_xlat9 = abs(u_xlat9) + abs(u_xlat1.x);
					    u_xlat9 = u_xlat9 * 10.0;
					    u_xlat0.x = u_xlat9 / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat0.xy = u_xlat0.xx * u_xlat3.xy + vs_TEXCOORD4.xy;
					    u_xlat0.z = u_xlat2.y * 0.200000003 + u_xlat0.y;
					    u_xlat0.xy = u_xlat0.xz * vec2(4.0, 4.0);
					    u_xlat0 = texture(_BloodTex, u_xlat0.xy);
					    u_xlat0.x = u_xlat0.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat2.xyz * _BloodColor.xyz + (-u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat0.w = (-u_xlat1.w) * u_xlat2.w + 1.0;
					    u_xlat1.x = u_xlat1.z + 1.0;
					    u_xlat1.y = (-u_xlat1.z) + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.xxxy;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat1.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xyz);
					    u_xlat10 = vs_TEXCOORD2.x * 5.0;
					    u_xlat10 = clamp(u_xlat10, 0.0, 1.0);
					    u_xlat10 = (-u_xlat10) + 1.0;
					    u_xlat2.x = u_xlat10 * vs_COLOR1.w;
					    u_xlat10 = (-u_xlat10) * vs_COLOR1.w + 1.0;
					    u_xlat9 = u_xlat0.w * u_xlat10;
					    u_xlat0.xyz = u_xlat2.xxx * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat4.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat2.xyz = (-u_xlat0.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xyz + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat9 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    SV_Target0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
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
						vec4 unused_0_0[9];
						vec4 _LutLerp;
						vec4 unused_0_2[40];
						vec4 _CloudCoverage;
						vec4 unused_0_4[5];
						float _LineWidth;
						vec4 unused_0_6;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_10;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_15[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat6;
					vec2 u_xlat8;
					vec2 u_xlat11;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.x = _Hover * 0.200000003 + 0.5;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat1.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = u_xlat2.xy * u_xlat1.xy + vs_TEXCOORD1.xy;
					    u_xlat12 = u_xlat1.x / _PartTex_TexelSize.x;
					    u_xlat1 = texture(_PartTex, u_xlat1.xy);
					    u_xlat2.x = dFdx(u_xlat12);
					    u_xlat12 = dFdy(u_xlat12);
					    u_xlat4.z = abs(u_xlat12) + abs(u_xlat2.x);
					    u_xlat4.xyz = u_xlat4.xxz * vec3(-0.5, 0.5, 0.100000001);
					    u_xlat2.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat6 = dFdx(u_xlat2.x);
					    u_xlat2.x = dFdy(u_xlat2.x);
					    u_xlat2.x = abs(u_xlat2.x) + abs(u_xlat6);
					    u_xlat2.xy = u_xlat2.xx * vec2(0.100000001, 0.00100000005);
					    u_xlat12 = max(u_xlat4.z, u_xlat2.y);
					    u_xlat3.xy = vec2(u_xlat12) * u_xlat4.xy;
					    u_xlat4.xy = u_xlat4.xy * u_xlat2.xx;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat3.xy = u_xlat3.xy * vec2(vec2(_LineWidth, _LineWidth)) + u_xlat0.xx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat11.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat12) * u_xlat11.xy + u_xlat3.xy;
					    u_xlat0.xw = (-vec2(u_xlat12)) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xw = max(u_xlat0.xw, vec2(0.0, 0.0));
					    u_xlat3.xy = min(u_xlat11.xy, vec2(1.0, 1.0));
					    u_xlat3.xy = (-u_xlat0.xw) + u_xlat3.xy;
					    u_xlat0.xw = (-u_xlat0.xw) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xw / u_xlat3.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat3.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat3.xy = u_xlat2.xx * u_xlat3.xy + u_xlat4.xy;
					    u_xlat4.xy = (-u_xlat2.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat4.xy = max(u_xlat4.xy, vec2(0.0, 0.0));
					    u_xlat2.xy = min(u_xlat3.xy, vec2(1.0, 1.0));
					    u_xlat2.xy = (-u_xlat4.xy) + u_xlat2.xy;
					    u_xlat4.xy = (-u_xlat4.xy) + u_xlat2.ww;
					    u_xlat4.xy = u_xlat4.xy / u_xlat2.xy;
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = (-u_xlat4.y) * u_xlat0.w + 1.0;
					    u_xlat8.xy = vs_TEXCOORD3.yx;
					    u_xlat8.xy = clamp(u_xlat8.xy, 0.0, 1.0);
					    u_xlat13 = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat13<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-u_xlat2.z) + 1.0;
					    u_xlat8.x = u_xlat2.z + 1.0;
					    u_xlat1.xyz = u_xlat8.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat4.x = (-u_xlat4.x) + 1.0;
					    u_xlat8.x = (-u_xlat4.x) * vs_COLOR1.w + 1.0;
					    u_xlat4.x = u_xlat4.x * vs_COLOR1.w;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat1.xyz = u_xlat4.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat4.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * u_xlat4.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat13 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat13) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat8.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_4[5];
						float _LineWidth;
						vec4 unused_0_6;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_10;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_15[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec3 u_xlat4;
					float u_xlat6;
					vec2 u_xlat8;
					vec2 u_xlat11;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.x = _Hover * 0.200000003 + 0.5;
					    u_xlat4.x = _AAFactor + 1.0;
					    u_xlat1.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = u_xlat2.xy * u_xlat1.xy + vs_TEXCOORD1.xy;
					    u_xlat12 = u_xlat1.x / _PartTex_TexelSize.x;
					    u_xlat1 = texture(_PartTex, u_xlat1.xy);
					    u_xlat2.x = dFdx(u_xlat12);
					    u_xlat12 = dFdy(u_xlat12);
					    u_xlat4.z = abs(u_xlat12) + abs(u_xlat2.x);
					    u_xlat4.xyz = u_xlat4.xxz * vec3(-0.5, 0.5, 0.100000001);
					    u_xlat2.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat6 = dFdx(u_xlat2.x);
					    u_xlat2.x = dFdy(u_xlat2.x);
					    u_xlat2.x = abs(u_xlat2.x) + abs(u_xlat6);
					    u_xlat2.xy = u_xlat2.xx * vec2(0.100000001, 0.00100000005);
					    u_xlat12 = max(u_xlat4.z, u_xlat2.y);
					    u_xlat3.xy = vec2(u_xlat12) * u_xlat4.xy;
					    u_xlat4.xy = u_xlat4.xy * u_xlat2.xx;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat3.xy = u_xlat3.xy * vec2(vec2(_LineWidth, _LineWidth)) + u_xlat0.xx;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat11.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat11.xy = vec2(u_xlat12) * u_xlat11.xy + u_xlat3.xy;
					    u_xlat0.xw = (-vec2(u_xlat12)) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat0.xw = max(u_xlat0.xw, vec2(0.0, 0.0));
					    u_xlat3.xy = min(u_xlat11.xy, vec2(1.0, 1.0));
					    u_xlat3.xy = (-u_xlat0.xw) + u_xlat3.xy;
					    u_xlat0.xw = (-u_xlat0.xw) + u_xlat1.ww;
					    u_xlat0.xw = u_xlat0.xw / u_xlat3.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat3.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat3.xy = u_xlat2.xx * u_xlat3.xy + u_xlat4.xy;
					    u_xlat4.xy = (-u_xlat2.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat4.xy = max(u_xlat4.xy, vec2(0.0, 0.0));
					    u_xlat2.xy = min(u_xlat3.xy, vec2(1.0, 1.0));
					    u_xlat2.xy = (-u_xlat4.xy) + u_xlat2.xy;
					    u_xlat4.xy = (-u_xlat4.xy) + u_xlat2.ww;
					    u_xlat4.xy = u_xlat4.xy / u_xlat2.xy;
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = (-u_xlat4.y) * u_xlat0.w + 1.0;
					    u_xlat8.xy = vs_TEXCOORD3.yx;
					    u_xlat8.xy = clamp(u_xlat8.xy, 0.0, 1.0);
					    u_xlat13 = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat13<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = (-u_xlat2.z) + 1.0;
					    u_xlat8.x = u_xlat2.z + 1.0;
					    u_xlat1.xyz = u_xlat8.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat4.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    u_xlat4.x = (-u_xlat4.x) + 1.0;
					    u_xlat8.x = (-u_xlat4.x) * vs_COLOR1.w + 1.0;
					    u_xlat4.x = u_xlat4.x * vs_COLOR1.w;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat1.xyz = u_xlat4.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat4.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * u_xlat4.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat13 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat13) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat8.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_LOWEND_ON" }
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
						vec4 unused_0_8;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_12[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec3 u_xlat4;
					float u_xlat9;
					void main()
					{
					    u_xlat0.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat1.xy * u_xlat0.xy + vs_TEXCOORD1.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    u_xlat1.x = u_xlat0.w * u_xlat1.w;
					    u_xlat2.xy = vs_TEXCOORD3.yx;
					    u_xlat2.xy = clamp(u_xlat2.xy, 0.0, 1.0);
					    u_xlat4.x = u_xlat1.x * u_xlat2.x + -0.00999999978;
					    u_xlat1.x = u_xlat1.x * u_xlat2.x;
					    SV_Target0.w = u_xlat1.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb1 = u_xlat4.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.w = (-u_xlat1.w) * u_xlat0.w + 1.0;
					    u_xlat1.x = (-u_xlat1.z) + 1.0;
					    u_xlat1.y = u_xlat1.z + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.yyyx;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat1.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat4.x = (-u_xlat1.x) * vs_COLOR1.w + 1.0;
					    u_xlat1.x = u_xlat1.x * vs_COLOR1.w;
					    u_xlat9 = u_xlat0.w * u_xlat4.x;
					    u_xlat4.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xyz);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat4.xyz + u_xlat0.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat4.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat2.xzw = (-u_xlat0.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xzw + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat9 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
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
					Keywords { "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_8;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_12[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec3 u_xlat4;
					float u_xlat9;
					void main()
					{
					    u_xlat0.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xy = u_xlat1.xy * u_xlat0.xy + vs_TEXCOORD1.xy;
					    u_xlat0 = texture(_PartTex, u_xlat0.xy);
					    u_xlat1.x = u_xlat0.w * u_xlat1.w;
					    u_xlat2.xy = vs_TEXCOORD3.yx;
					    u_xlat2.xy = clamp(u_xlat2.xy, 0.0, 1.0);
					    u_xlat4.x = u_xlat1.x * u_xlat2.x + -0.00999999978;
					    u_xlat1.x = u_xlat1.x * u_xlat2.x;
					    SV_Target0.w = u_xlat1.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb1 = u_xlat4.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.w = (-u_xlat1.w) * u_xlat0.w + 1.0;
					    u_xlat1.x = (-u_xlat1.z) + 1.0;
					    u_xlat1.y = u_xlat1.z + 1.0;
					    u_xlat0 = u_xlat0 * u_xlat1.yyyx;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    u_xlat1.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlat4.x = (-u_xlat1.x) * vs_COLOR1.w + 1.0;
					    u_xlat1.x = u_xlat1.x * vs_COLOR1.w;
					    u_xlat9 = u_xlat0.w * u_xlat4.x;
					    u_xlat4.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xyz);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat4.xyz + u_xlat0.xyz;
					    u_xlat1.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat4.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat2.xzw = (-u_xlat0.xyz) * u_xlat1.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = vec3(_Hover) * u_xlat2.xzw + u_xlat4.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat9 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat9) * u_xlat1.xyz + u_xlat0.xyz;
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
					Keywords { "_BLOOD_ON" "_GAME_ON" "_CINEMATIC_ON" }
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
						vec4 unused_0_2[33];
						vec4 _BloodColor;
						vec4 unused_0_4[6];
						vec4 _CloudCoverage;
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_17[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec2 u_xlat4;
					vec2 u_xlat5;
					vec2 u_xlat8;
					vec2 u_xlat9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat4.x);
					    u_xlat0.z = _AAFactor + 1.0;
					    u_xlat0 = u_xlat0.xxzz * vec4(0.100000001, 0.00100000005, -0.5, 0.5);
					    u_xlat1.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat1.xy = u_xlat1.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = (-u_xlat1.xy) + vec2(1.0, 1.0);
					    u_xlat9.xy = u_xlat0.xx * u_xlat9.xy + u_xlat1.xy;
					    u_xlat1.xy = (-u_xlat0.xx) * u_xlat1.xy + u_xlat1.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat9.xy = min(u_xlat9.xy, vec2(1.0, 1.0));
					    u_xlat9.xy = (-u_xlat1.xy) + u_xlat9.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = (-u_xlat1.xy) + u_xlat2.ww;
					    u_xlat1.xy = u_xlat1.xy / u_xlat9.xy;
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat9.xy = u_xlat2.xy * u_xlat9.xy + vs_TEXCOORD1.xy;
					    u_xlat0.x = u_xlat9.x / _PartTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat2.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat0.x = max(u_xlat0.x, u_xlat0.y);
					    u_xlat4.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat12 = _Hover * 0.200000003 + 0.5;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(u_xlat12);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat0.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat8.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat8.xy = (-u_xlat0.xy) + u_xlat8.xy;
					    u_xlat3 = texture(_PartTex, u_xlat9.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat3.ww;
					    u_xlat0.xy = u_xlat0.xy / u_xlat8.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    u_xlat4.x = (-u_xlat1.y) * u_xlat0.y + 1.0;
					    u_xlat8.xy = vs_TEXCOORD3.yx;
					    u_xlat8.xy = clamp(u_xlat8.xy, 0.0, 1.0);
					    u_xlat1.x = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat1.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat8.x = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat8.x) + abs(u_xlat0.x);
					    u_xlat8.x = dFdx(u_xlat9.x);
					    u_xlat1.x = dFdy(u_xlat9.x);
					    u_xlat5.xy = u_xlat9.xy + (-vs_TEXCOORD4.xy);
					    u_xlat8.x = abs(u_xlat8.x) + abs(u_xlat1.x);
					    u_xlat8.x = u_xlat8.x * 10.0;
					    u_xlat0.x = u_xlat8.x / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat5.xy + vs_TEXCOORD4.xy;
					    u_xlat1.z = u_xlat3.y * 0.200000003 + u_xlat1.y;
					    u_xlat0.xz = u_xlat1.xz * vec2(4.0, 4.0);
					    u_xlat1 = texture(_BloodTex, u_xlat0.xz);
					    u_xlat0.x = u_xlat1.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat2.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat0.x = u_xlat2.z + 1.0;
					    u_xlat8.x = (-u_xlat2.z) + 1.0;
					    u_xlat4.x = u_xlat8.x * u_xlat4.x;
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat0.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat8.x = u_xlat0.x * vs_COLOR1.w;
					    u_xlat0.x = (-u_xlat0.x) * vs_COLOR1.w + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat1.xyz = u_xlat8.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat4.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * u_xlat4.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat13 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat13) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat8.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" }
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
						vec4 unused_0_2[33];
						vec4 _BloodColor;
						vec4 unused_0_4[6];
						vec4 _CloudCoverage;
						vec4 unused_0_6[5];
						float _LineWidth;
						vec4 unused_0_8;
						vec4 _FogColor;
						float _AAFactor;
						vec4 _FlashColor;
						vec4 unused_0_12;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_17[2];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec2 u_xlat4;
					vec2 u_xlat5;
					vec2 u_xlat8;
					vec2 u_xlat9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat4.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat4.x);
					    u_xlat0.z = _AAFactor + 1.0;
					    u_xlat0 = u_xlat0.xxzz * vec4(0.100000001, 0.00100000005, -0.5, 0.5);
					    u_xlat1.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat1.xy = u_xlat1.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.5, 0.5);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = (-u_xlat1.xy) + vec2(1.0, 1.0);
					    u_xlat9.xy = u_xlat0.xx * u_xlat9.xy + u_xlat1.xy;
					    u_xlat1.xy = (-u_xlat0.xx) * u_xlat1.xy + u_xlat1.xy;
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat9.xy = min(u_xlat9.xy, vec2(1.0, 1.0));
					    u_xlat9.xy = (-u_xlat1.xy) + u_xlat9.xy;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = (-u_xlat1.xy) + u_xlat2.ww;
					    u_xlat1.xy = u_xlat1.xy / u_xlat9.xy;
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat9.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat9.xy = u_xlat2.xy * u_xlat9.xy + vs_TEXCOORD1.xy;
					    u_xlat0.x = u_xlat9.x / _PartTex_TexelSize.x;
					    u_xlat2.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat2.x);
					    u_xlat0.x = u_xlat0.x * 0.100000001;
					    u_xlat0.x = max(u_xlat0.x, u_xlat0.y);
					    u_xlat4.xy = u_xlat0.xx * u_xlat0.zw;
					    u_xlat12 = _Hover * 0.200000003 + 0.5;
					    u_xlat4.xy = u_xlat4.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(u_xlat12);
					    u_xlat4.xy = clamp(u_xlat4.xy, 0.0, 1.0);
					    u_xlat2.xy = (-u_xlat4.xy) + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat0.xx * u_xlat2.xy + u_xlat4.xy;
					    u_xlat0.xy = (-u_xlat0.xx) * u_xlat4.xy + u_xlat4.xy;
					    u_xlat0.xy = max(u_xlat0.xy, vec2(0.0, 0.0));
					    u_xlat8.xy = min(u_xlat2.xy, vec2(1.0, 1.0));
					    u_xlat8.xy = (-u_xlat0.xy) + u_xlat8.xy;
					    u_xlat3 = texture(_PartTex, u_xlat9.xy);
					    u_xlat0.xy = (-u_xlat0.xy) + u_xlat3.ww;
					    u_xlat0.xy = u_xlat0.xy / u_xlat8.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    u_xlat4.x = (-u_xlat1.y) * u_xlat0.y + 1.0;
					    u_xlat8.xy = vs_TEXCOORD3.yx;
					    u_xlat8.xy = clamp(u_xlat8.xy, 0.0, 1.0);
					    u_xlat1.x = u_xlat0.x * u_xlat8.x + -0.00999999978;
					    u_xlat0.x = u_xlat8.x * u_xlat0.x;
					    SV_Target0.w = u_xlat0.x;
					    u_xlatb0 = u_xlat1.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat8.x = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat8.x) + abs(u_xlat0.x);
					    u_xlat8.x = dFdx(u_xlat9.x);
					    u_xlat1.x = dFdy(u_xlat9.x);
					    u_xlat5.xy = u_xlat9.xy + (-vs_TEXCOORD4.xy);
					    u_xlat8.x = abs(u_xlat8.x) + abs(u_xlat1.x);
					    u_xlat8.x = u_xlat8.x * 10.0;
					    u_xlat0.x = u_xlat8.x / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat1.xy = u_xlat0.xx * u_xlat5.xy + vs_TEXCOORD4.xy;
					    u_xlat1.z = u_xlat3.y * 0.200000003 + u_xlat1.y;
					    u_xlat0.xz = u_xlat1.xz * vec2(4.0, 4.0);
					    u_xlat1 = texture(_BloodTex, u_xlat0.xz);
					    u_xlat0.x = u_xlat1.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat2.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.xyz = u_xlat3.xyz * _BloodColor.xyz + (-u_xlat3.xyz);
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat0.x = u_xlat2.z + 1.0;
					    u_xlat8.x = (-u_xlat2.z) + 1.0;
					    u_xlat4.x = u_xlat8.x * u_xlat4.x;
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat2.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat1.xyz);
					    u_xlat0.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = (-u_xlat0.x) + 1.0;
					    u_xlat8.x = u_xlat0.x * vs_COLOR1.w;
					    u_xlat0.x = (-u_xlat0.x) * vs_COLOR1.w + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x;
					    u_xlat1.xyz = u_xlat8.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat4.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat2.xyz = u_xlat4.xxx * u_xlat1.xyz;
					    u_xlat3.xyz = (-u_xlat1.xyz) * u_xlat4.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat2.xyz = vec3(_Hover) * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + vs_COLOR0.xyz;
					    u_xlat13 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat0.xyz = vec3(u_xlat13) * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.x = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = u_xlat0.xyz + (-u_xlat1.xxx);
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xyz) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat8.yyy * u_xlat1.xyz + u_xlat0.xyz;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_CINEMATIC_ON" "_LOWEND_ON" }
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
						vec4 unused_0_2[33];
						vec4 _BloodColor;
						vec4 unused_0_4[6];
						vec4 _CloudCoverage;
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_14[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat5;
					float u_xlat6;
					vec2 u_xlat8;
					float u_xlat9;
					float u_xlat13;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD3.yx;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat8.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat8.xy = u_xlat1.xy * u_xlat8.xy + vs_TEXCOORD1.xy;
					    u_xlat2 = texture(_PartTex, u_xlat8.xy);
					    u_xlat1.x = u_xlat1.w * u_xlat2.w;
					    u_xlat5.x = u_xlat1.x * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat5.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat1.x = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat1.x);
					    u_xlat1.x = dFdx(u_xlat8.x);
					    u_xlat5.x = dFdy(u_xlat8.x);
					    u_xlat8.xy = u_xlat8.xy + (-vs_TEXCOORD4.xy);
					    u_xlat1.x = abs(u_xlat5.x) + abs(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x * 10.0;
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat3.xy = u_xlat0.xx * u_xlat8.xy + vs_TEXCOORD4.xy;
					    u_xlat3.z = u_xlat2.y * 0.200000003 + u_xlat3.y;
					    u_xlat0.xz = u_xlat3.xz * vec2(4.0, 4.0);
					    u_xlat3 = texture(_BloodTex, u_xlat0.xz);
					    u_xlat0.x = u_xlat3.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat2.xyz * _BloodColor.xyz + (-u_xlat2.xyz);
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat1.x = (-u_xlat1.w) * u_xlat2.w + 1.0;
					    u_xlat5.x = u_xlat1.z + 1.0;
					    u_xlat9 = (-u_xlat1.z) + 1.0;
					    u_xlat1.x = u_xlat9 * u_xlat1.x;
					    u_xlat0.xzw = u_xlat0.xzw * u_xlat5.xxx;
					    u_xlat0.xzw = u_xlat0.xzw * vs_COLOR0.xyz;
					    u_xlat5.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xzw);
					    u_xlat2.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat6 = u_xlat2.x * vs_COLOR1.w;
					    u_xlat2.x = (-u_xlat2.x) * vs_COLOR1.w + 1.0;
					    u_xlat1.x = u_xlat1.x * u_xlat2.x;
					    u_xlat0.xzw = vec3(u_xlat6) * u_xlat5.xyz + u_xlat0.xzw;
					    u_xlat5.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat2.xyz = u_xlat0.xzw * u_xlat5.xxx;
					    u_xlat5.xyz = (-u_xlat0.xzw) * u_xlat5.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat5.xyz = vec3(_Hover) * u_xlat5.xyz + u_xlat2.xyz;
					    u_xlat5.xyz = (-u_xlat0.xzw) + u_xlat5.xyz;
					    u_xlat0.xzw = u_xlat1.xxx * u_xlat5.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = (-u_xlat0.xzw) + vs_COLOR0.xyz;
					    u_xlat13 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat0.xzw = vec3(u_xlat13) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.x = dot(u_xlat0.xzw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xzw = u_xlat0.xzw + (-u_xlat1.xxx);
					    u_xlat0.xzw = _CloudCoverage.yyy * u_xlat0.xzw + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xzw) + _LutLerp.www;
					    u_xlat0.xzw = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xzw) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat0.yyy * u_xlat1.xyz + u_xlat0.xzw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "_GAME_ON" "_CINEMATIC_ON" "_MOBILE_PLATFORM" "_LOWEND_ON" }
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
						vec4 unused_0_2[33];
						vec4 _BloodColor;
						vec4 unused_0_4[6];
						vec4 _CloudCoverage;
						vec4 unused_0_6[7];
						vec4 _FogColor;
						vec4 unused_0_8;
						vec4 _FlashColor;
						vec4 unused_0_10;
						vec4 _SelectionColor;
						float _Hover;
						vec4 _PartTex_TexelSize;
						vec4 unused_0_14[3];
						vec4 _PartTexTiling;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PartTex;
					uniform  sampler2D _BloodTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					in  vec4 vs_COLOR1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat5;
					float u_xlat6;
					vec2 u_xlat8;
					float u_xlat9;
					float u_xlat13;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD3.yx;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat8.xy = _PartTex_TexelSize.xy * _PartTexTiling.xy;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat8.xy = u_xlat1.xy * u_xlat8.xy + vs_TEXCOORD1.xy;
					    u_xlat2 = texture(_PartTex, u_xlat8.xy);
					    u_xlat1.x = u_xlat1.w * u_xlat2.w;
					    u_xlat5.x = u_xlat1.x * u_xlat0.x + -0.00999999978;
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    SV_Target0.w = u_xlat0.x;
					    SV_Target0.w = clamp(SV_Target0.w, 0.0, 1.0);
					    u_xlatb0 = u_xlat5.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = dFdx(vs_TEXCOORD4.x);
					    u_xlat1.x = dFdy(vs_TEXCOORD4.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat1.x);
					    u_xlat1.x = dFdx(u_xlat8.x);
					    u_xlat5.x = dFdy(u_xlat8.x);
					    u_xlat8.xy = u_xlat8.xy + (-vs_TEXCOORD4.xy);
					    u_xlat1.x = abs(u_xlat5.x) + abs(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x * 10.0;
					    u_xlat0.x = u_xlat1.x / u_xlat0.x;
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat3.xy = u_xlat0.xx * u_xlat8.xy + vs_TEXCOORD4.xy;
					    u_xlat3.z = u_xlat2.y * 0.200000003 + u_xlat3.y;
					    u_xlat0.xz = u_xlat3.xz * vec2(4.0, 4.0);
					    u_xlat3 = texture(_BloodTex, u_xlat0.xz);
					    u_xlat0.x = u_xlat3.y * 2.0 + vs_TEXCOORD2.y;
					    u_xlat0.x = (-u_xlat1.z) + u_xlat0.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3.xyz = u_xlat2.xyz * _BloodColor.xyz + (-u_xlat2.xyz);
					    u_xlat0.xzw = u_xlat0.xxx * u_xlat3.xyz + u_xlat2.xyz;
					    u_xlat1.x = (-u_xlat1.w) * u_xlat2.w + 1.0;
					    u_xlat5.x = u_xlat1.z + 1.0;
					    u_xlat9 = (-u_xlat1.z) + 1.0;
					    u_xlat1.x = u_xlat9 * u_xlat1.x;
					    u_xlat0.xzw = u_xlat0.xzw * u_xlat5.xxx;
					    u_xlat0.xzw = u_xlat0.xzw * vs_COLOR0.xyz;
					    u_xlat5.xyz = vs_COLOR1.xyz * vec3(0.800000012, 0.800000012, 0.800000012) + (-u_xlat0.xzw);
					    u_xlat2.x = vs_TEXCOORD2.x * 5.0;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2.x = (-u_xlat2.x) + 1.0;
					    u_xlat6 = u_xlat2.x * vs_COLOR1.w;
					    u_xlat2.x = (-u_xlat2.x) * vs_COLOR1.w + 1.0;
					    u_xlat1.x = u_xlat1.x * u_xlat2.x;
					    u_xlat0.xzw = vec3(u_xlat6) * u_xlat5.xyz + u_xlat0.xzw;
					    u_xlat5.x = _SelectionColor.w * -0.400000006 + 0.400000006;
					    u_xlat2.xyz = u_xlat0.xzw * u_xlat5.xxx;
					    u_xlat5.xyz = (-u_xlat0.xzw) * u_xlat5.xxx + vec3(1.0, 1.0, 1.0);
					    u_xlat5.xyz = vec3(_Hover) * u_xlat5.xyz + u_xlat2.xyz;
					    u_xlat5.xyz = (-u_xlat0.xzw) + u_xlat5.xyz;
					    u_xlat0.xzw = u_xlat1.xxx * u_xlat5.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = (-u_xlat0.xzw) + vs_COLOR0.xyz;
					    u_xlat13 = (-vs_TEXCOORD3.y) + 2.0;
					    u_xlat13 = clamp(u_xlat13, 0.0, 1.0);
					    u_xlat0.xzw = vec3(u_xlat13) * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.x = dot(u_xlat0.xzw, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xzw = u_xlat0.xzw + (-u_xlat1.xxx);
					    u_xlat0.xzw = _CloudCoverage.yyy * u_xlat0.xzw + u_xlat1.xxx;
					    u_xlat1.xyz = (-u_xlat0.xzw) + _LutLerp.www;
					    u_xlat0.xzw = _LutLerp.xyz * u_xlat1.xyz + u_xlat0.xzw;
					    u_xlat1.xyz = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyz = (-u_xlat0.xzw) + u_xlat1.xyz;
					    SV_Target0.xyz = u_xlat0.yyy * u_xlat1.xyz + u_xlat0.xzw;
					    return;
					}"
				}
			}
		}
	}
}