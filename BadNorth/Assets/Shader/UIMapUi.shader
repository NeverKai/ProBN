Shader "UI/MapUi" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_BloodTex ("Blood Texture", 2D) = "white" {}
		_WaterColor ("Water Color", Vector) = (0.5,0.5,0.5,1)
		_LowColor ("Low Color", Vector) = (0.5,0.5,0.5,1)
		_HighColor ("High Color", Vector) = (0.5,0.5,0.5,1)
		_HighlightColor ("Highlight Color", Vector) = (1,1,0.2,1)
		_BloodColor2 ("Blood Color", Vector) = (1,0,0,1)
		_ForestColor2 ("Forest Color", Vector) = (0,0,0,1)
		_ShadowColor ("Shadow Color", Vector) = (0.5,0.5,0.5,1)
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_ColorMask ("Color Mask", Float) = 15
		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
	}
	SubShader {
		Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "RenderType" = "Opaque" }
		Pass {
			Name "Default"
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "RenderType" = "Opaque" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			Cull Off
			Stencil {
				ReadMask 0
				WriteMask 0
				Comp Disabled
				Pass Keep
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 12258
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
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_0_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec4 vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    vs_COLOR0 = in_COLOR0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    vs_TEXCOORD1 = in_POSITION0;
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
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_0_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec4 vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    vs_COLOR0 = in_COLOR0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    vs_TEXCOORD1 = in_POSITION0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "UNITY_UI_ALPHACLIP" }
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
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_0_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec4 vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    vs_COLOR0 = in_COLOR0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    vs_TEXCOORD1 = in_POSITION0;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "UNITY_UI_ALPHACLIP" }
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
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_0_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec4 vs_TEXCOORD1;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    vs_COLOR0 = in_COLOR0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    vs_TEXCOORD1 = in_POSITION0;
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
						vec4 unused_0_2[9];
						vec2 _MainTex_TexelSize;
						vec4 unused_0_4[4];
						vec4 _BloodColor2;
						vec4 unused_0_6[2];
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _BloodTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					bool u_xlatb2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec2 u_xlat5;
					bool u_xlatb5;
					vec2 u_xlat7;
					vec2 u_xlat10;
					vec2 u_xlat12;
					vec2 u_xlat14;
					float u_xlat15;
					bool u_xlatb15;
					float u_xlat17;
					void main()
					{
					    u_xlat0.xy = dFdx(vs_TEXCOORD0.xy);
					    u_xlat10.xy = dFdy(vs_TEXCOORD0.xy);
					    u_xlat0.xy = abs(u_xlat10.xy) + abs(u_xlat0.xy);
					    u_xlat0.xy = u_xlat0.xy * vec2(vec2(_LineWidth, _LineWidth));
					    u_xlat0.xy = u_xlat0.xy * vec2(0.5, 0.5);
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat0.z = 0.0;
					    u_xlat1.xy = u_xlat0.wz + vs_TEXCOORD0.xy;
					    u_xlat1 = texture(_MainTex, u_xlat1.xy);
					    u_xlat2 = u_xlat0.xzzy + vs_TEXCOORD0.xyxy;
					    u_xlat0.xy = u_xlat0.zy * vec2(1.0, -1.0) + vs_TEXCOORD0.xy;
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat3 = texture(_MainTex, u_xlat2.xy);
					    u_xlat2 = texture(_MainTex, u_xlat2.zw);
					    u_xlat4.xy = max(u_xlat1.zw, u_xlat3.zw);
					    u_xlat14.xy = max(u_xlat0.zw, u_xlat2.zw);
					    u_xlat4.xy = max(u_xlat14.xy, u_xlat4.xy);
					    u_xlat15 = u_xlat4.y + -0.00100000005;
					    u_xlatb15 = u_xlat15<0.0;
					    if(((int(u_xlatb15) * int(0xffffffffu)))!=0){discard;}
					    u_xlat15 = min(u_xlat1.z, u_xlat3.z);
					    u_xlat17 = min(u_xlat0.z, u_xlat2.z);
					    u_xlat15 = min(u_xlat15, u_xlat17);
					    u_xlat17 = (-u_xlat15) + u_xlat4.x;
					    u_xlat15 = u_xlat17 * 0.5 + u_xlat15;
					    u_xlat15 = u_xlat15 + -0.5;
					    u_xlat4.x = (-u_xlat1.z) + u_xlat3.z;
					    u_xlat4.y = (-u_xlat0.z) + u_xlat2.z;
					    u_xlat10.x = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat15 / u_xlat10.x;
					    u_xlat15 = u_xlat10.x + 0.5;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat12.xy = vs_TEXCOORD0.xy / _MainTex_TexelSize.xy;
					    u_xlat12.x = u_xlat12.y + u_xlat12.x;
					    u_xlat17 = u_xlat1.x + u_xlat3.x;
					    u_xlat1.x = u_xlat3.y;
					    u_xlat2.x = u_xlat2.x + u_xlat17;
					    u_xlat1.z = u_xlat2.y;
					    u_xlat0.x = u_xlat0.x + u_xlat2.x;
					    u_xlat1.w = u_xlat0.y;
					    u_xlat3 = u_xlat1 * vec4(10.0, 10.0, 10.0, 10.0) + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat3.y + u_xlat3.x;
					    u_xlat5.x = u_xlat3.z + u_xlat5.x;
					    u_xlat5.x = u_xlat3.w + u_xlat5.x;
					    u_xlat2.x = u_xlat5.x * 0.25;
					    u_xlatb5 = u_xlat5.x>=6.0;
					    u_xlat5.x = u_xlatb5 ? 1.0 : float(0.0);
					    u_xlat2.x = roundEven(u_xlat2.x);
					    u_xlat2.x = u_xlat2.x * 0.333333343;
					    u_xlat2.x = u_xlat12.x * 0.333333343 + u_xlat2.x;
					    u_xlat7.x = dFdx(u_xlat2.x);
					    u_xlat12.x = dFdy(u_xlat2.x);
					    u_xlat2.x = fract(u_xlat2.x);
					    u_xlat7.x = abs(u_xlat12.x) + abs(u_xlat7.x);
					    u_xlat7.x = u_xlat7.x * _LineWidth;
					    u_xlatb2 = u_xlat7.x>=u_xlat2.x;
					    u_xlat2.x = u_xlatb2 ? 0.5 : float(0.0);
					    u_xlat15 = u_xlat15 * u_xlat2.x;
					    u_xlat2.x = u_xlat10.x;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat10.x = -abs(u_xlat10.x) + 1.0;
					    u_xlat10.x = max(u_xlat10.x, 0.0);
					    u_xlat2.x = u_xlat2.x * 0.5;
					    u_xlat2.x = u_xlat2.x * vs_COLOR0.y;
					    u_xlat7.x = (-vs_COLOR0.x) + 1.0;
					    u_xlat2.x = u_xlat7.x * u_xlat2.x;
					    u_xlat7.x = u_xlat7.x * vs_COLOR0.y;
					    u_xlat3 = u_xlat7.xxxx * vec4(1.0, 1.0, 1.0, 0.5) + vec4(0.0, 0.0, 0.0, 0.200000003);
					    u_xlat7.xy = vs_TEXCOORD0.xy * vec2(10.0, 10.0);
					    u_xlat4 = texture(_BloodTex, u_xlat7.xy);
					    u_xlat7.x = u_xlat4.y * 0.200000003 + -0.200000003;
					    u_xlat7.x = u_xlat7.x * 10.0;
					    u_xlat7.x = clamp(u_xlat7.x, 0.0, 1.0);
					    u_xlat7.x = u_xlat7.x * vs_COLOR0.w;
					    u_xlat7.x = clamp(u_xlat7.x, 0.0, 1.0);
					    u_xlat4 = (-u_xlat3.zzzw) + _BloodColor2;
					    u_xlat3 = u_xlat7.xxxx * u_xlat4 + u_xlat3;
					    u_xlat4 = (-u_xlat3) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = u_xlat2.xxxx * u_xlat4 + u_xlat3;
					    u_xlat3 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = vec4(u_xlat15) * u_xlat3 + u_xlat2;
					    u_xlat3 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = u_xlat10.xxxx * u_xlat3 + u_xlat2;
					    u_xlat10.x = u_xlat2.w * -0.300000012;
					    u_xlat3.w = vs_COLOR0.z * u_xlat10.x + u_xlat2.w;
					    u_xlat10.xy = vs_COLOR0.zx * vec2(0.100000001, -0.300000012) + vec2(1.0, 1.0);
					    u_xlat3.xyz = u_xlat10.xxx * u_xlat2.xyz;
					    u_xlat2 = u_xlat5.xxxx * u_xlat3;
					    u_xlat3 = (-u_xlat3) * u_xlat5.xxxx + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat4 = u_xlat1 * vec4(10.0, 10.0, 10.0, 10.0);
					    u_xlat1 = u_xlat1 * vec4(20.0, 20.0, 20.0, 20.0) + vec4(-1.0, -1.0, -1.0, -1.0);
					    u_xlat1 = max(u_xlat1, u_xlat4);
					    u_xlat1 = u_xlat1 + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat1.y + u_xlat1.x;
					    u_xlat5.x = u_xlat1.z + u_xlat5.x;
					    u_xlat0.y = u_xlat1.w + u_xlat5.x;
					    u_xlat1.xy = (-u_xlat1.yw) + u_xlat1.xz;
					    u_xlat10.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat1.x = u_xlat0.y * 0.25 + -1.5;
					    u_xlat0.xy = u_xlat0.xy * vec2(0.075000003, 0.25);
					    u_xlat5.x = fract(u_xlat0.y);
					    u_xlat5.x = u_xlat5.x * 2.0 + -1.0;
					    u_xlat5.x = abs(u_xlat5.x) / u_xlat10.x;
					    u_xlat5.x = u_xlat5.x * 0.5;
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat1.x);
					    u_xlat10.x = u_xlat1.x / u_xlat10.x;
					    u_xlat5.y = u_xlat10.x * 0.5;
					    u_xlat5.xy = min(u_xlat5.xy, vec2(1.0, 1.0));
					    u_xlat1.x = (-u_xlat5.x) + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat1.x + u_xlat5.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat5.y + 1.0;
					    u_xlat1 = u_xlat0.xxxx * u_xlat3.wxyz + u_xlat2.wxyz;
					    u_xlat1.x = u_xlat1.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    SV_Target0.xyz = u_xlat1.yzw;
					    SV_Target0.xyz = clamp(SV_Target0.xyz, 0.0, 1.0);
					    SV_Target0.w = u_xlat10.y * u_xlat1.x;
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
						vec4 unused_0_0[10];
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec4 unused_0_3[44];
						float _LineWidth;
						vec4 unused_0_5[9];
						vec2 _MainTex_TexelSize;
						vec2 _PaintTex_TexelSize;
						vec4 unused_0_8[4];
						vec4 _BloodColor2;
						vec4 unused_0_10[2];
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PaintTex;
					uniform  sampler2D _BloodTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec3 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec4 u_xlat10;
					float u_xlat11;
					float u_xlat12;
					vec3 u_xlat21;
					vec2 u_xlat22;
					vec2 u_xlat23;
					vec2 u_xlat26;
					vec2 u_xlat27;
					float u_xlat33;
					bool u_xlatb33;
					float u_xlat34;
					float u_xlat35;
					void main()
					{
					    u_xlat0.xy = dFdx(vs_TEXCOORD0.xy);
					    u_xlat22.xy = dFdy(vs_TEXCOORD0.xy);
					    u_xlat0.xy = abs(u_xlat22.xy) + abs(u_xlat0.xy);
					    u_xlat0.xy = u_xlat0.xy * vec2(vec2(_LineWidth, _LineWidth));
					    u_xlat0.xy = u_xlat0.xy * vec2(0.5, 0.5);
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat0.z = 0.0;
					    u_xlat1.xy = u_xlat0.wz + vs_TEXCOORD0.xy;
					    u_xlat1 = texture(_MainTex, u_xlat1.xy);
					    u_xlat2 = u_xlat0.xzzy + vs_TEXCOORD0.xyxy;
					    u_xlat0.xy = u_xlat0.zy * vec2(1.0, -1.0) + vs_TEXCOORD0.xy;
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat3 = texture(_MainTex, u_xlat2.xy).yxzw;
					    u_xlat2 = texture(_MainTex, u_xlat2.zw);
					    u_xlat4.xy = max(u_xlat1.zw, u_xlat3.zw);
					    u_xlat26.xy = max(u_xlat0.zw, u_xlat2.zw);
					    u_xlat4.xy = max(u_xlat26.xy, u_xlat4.xy);
					    u_xlat33 = u_xlat4.y + -0.00100000005;
					    u_xlatb33 = u_xlat33<0.0;
					    if(((int(u_xlatb33) * int(0xffffffffu)))!=0){discard;}
					    u_xlat33 = min(u_xlat1.z, u_xlat3.z);
					    u_xlat34 = min(u_xlat0.z, u_xlat2.z);
					    u_xlat33 = min(u_xlat33, u_xlat34);
					    u_xlat34 = (-u_xlat33) + u_xlat4.x;
					    u_xlat33 = u_xlat34 * 0.5 + u_xlat33;
					    u_xlat33 = u_xlat33 + -0.5;
					    u_xlat4.x = (-u_xlat1.z) + u_xlat3.z;
					    u_xlat4.y = (-u_xlat0.z) + u_xlat2.z;
					    u_xlat22.x = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat22.x = sqrt(u_xlat22.x);
					    u_xlat22.x = u_xlat33 / u_xlat22.x;
					    u_xlat33 = u_xlat22.x;
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat33 = u_xlat33 * 0.5;
					    u_xlat33 = u_xlat33 * vs_COLOR0.y;
					    u_xlat23.x = (-vs_COLOR0.x) + 1.0;
					    u_xlat33 = u_xlat33 * u_xlat23.x;
					    u_xlat23.x = u_xlat23.x * vs_COLOR0.y;
					    u_xlat4 = u_xlat23.xxxx * vec4(1.0, 1.0, 1.0, 0.5) + vec4(0.0, 0.0, 0.0, 0.200000003);
					    u_xlat1.xz = u_xlat1.xy + u_xlat3.yx;
					    u_xlat3.y = u_xlat1.y;
					    u_xlat1.xy = u_xlat2.xy + u_xlat1.xz;
					    u_xlat3.z = u_xlat2.y;
					    u_xlat1.xy = u_xlat0.xy + u_xlat1.xy;
					    u_xlat3.w = u_xlat0.y;
					    u_xlat0.x = u_xlat1.y * 2.5 + 1.0;
					    u_xlat11 = u_xlat1.x * 0.075000003;
					    u_xlat1 = u_xlat3 * vec4(10.0, 10.0, 10.0, 10.0) + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat1.x = u_xlat1.y + u_xlat1.x;
					    u_xlat1.x = u_xlat1.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.w + u_xlat1.x;
					    u_xlat12 = u_xlat1.x * 0.25;
					    u_xlatb1 = u_xlat1.x>=6.0;
					    u_xlat1.x = u_xlatb1 ? 1.0 : float(0.0);
					    u_xlat23.x = max(u_xlat12, 0.0);
					    u_xlat12 = roundEven(u_xlat12);
					    u_xlat12 = u_xlat12 * 0.333333343;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat23.x;
					    u_xlat2.y = u_xlat23.x + -0.5;
					    u_xlat0.x = max(u_xlat0.x, -1.0);
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat5 = u_xlat3 * vec4(10.0, 10.0, 10.0, 10.0);
					    u_xlat3 = u_xlat3 * vec4(20.0, 20.0, 20.0, 20.0) + vec4(-1.0, -1.0, -1.0, -1.0);
					    u_xlat3 = max(u_xlat3, u_xlat5);
					    u_xlat3 = u_xlat3 + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat23.xy = (-u_xlat3.yw) + u_xlat3.xz;
					    u_xlat35 = _PaintTex_TexelSize.xxxy.w / _MainTex_TexelSize.y;
					    u_xlat5.xy = vs_TEXCOORD0.xy / _MainTex_TexelSize.xy;
					    u_xlat27.xy = u_xlat5.xy / vec2(u_xlat35);
					    u_xlat35 = u_xlat5.y + u_xlat5.x;
					    u_xlat12 = u_xlat35 * 0.333333343 + u_xlat12;
					    u_xlat2.xz = u_xlat23.xy * u_xlat0.xx + u_xlat27.xy;
					    u_xlat0.x = dot(u_xlat23.xy, u_xlat23.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat5.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat23.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat34 = u_xlat2.y * u_xlat23.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat7.x * u_xlat2.x + u_xlat34;
					    u_xlat8.z = u_xlat2.z * u_xlat7.y;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat35 = u_xlat6.x * u_xlat9.x;
					    u_xlat35 = u_xlat6.y * u_xlat35;
					    u_xlat9.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat9.x + u_xlat34;
					    u_xlat10 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat21.yz = u_xlat8.yx;
					    u_xlat34 = u_xlat5.x * u_xlat10.x;
					    u_xlat34 = u_xlat6.y * u_xlat34;
					    u_xlat34 = u_xlat6.z * u_xlat34;
					    u_xlat34 = u_xlat35 * u_xlat6.z + u_xlat34;
					    u_xlat23.x = u_xlat23.x * u_xlat9.y;
					    u_xlat8.y = u_xlat7.x * u_xlat2.x + u_xlat23.x;
					    u_xlat8.x = u_xlat7.x * u_xlat9.x + u_xlat23.x;
					    u_xlat21.x = u_xlat7.y * u_xlat9.z;
					    u_xlat2 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat23.x = u_xlat5.x * u_xlat7.x;
					    u_xlat23.x = u_xlat5.y * u_xlat23.x;
					    u_xlat2.x = u_xlat6.x * u_xlat2.x;
					    u_xlat2.x = u_xlat5.y * u_xlat2.x;
					    u_xlat34 = u_xlat2.x * u_xlat6.z + u_xlat34;
					    u_xlat23.x = u_xlat23.x * u_xlat6.z + u_xlat34;
					    u_xlat2 = textureLod(_PaintTex, u_xlat21.yx, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat21.zx, 0.0);
					    u_xlat8.w = u_xlat21.x;
					    u_xlat34 = u_xlat5.x * u_xlat7.x;
					    u_xlat34 = u_xlat6.y * u_xlat34;
					    u_xlat2.x = u_xlat6.x * u_xlat2.x;
					    u_xlat2.x = u_xlat6.y * u_xlat2.x;
					    u_xlat23.x = u_xlat2.x * u_xlat5.z + u_xlat23.x;
					    u_xlat23.x = u_xlat34 * u_xlat5.z + u_xlat23.x;
					    u_xlat2 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat34 = u_xlat5.x * u_xlat7.x;
					    u_xlat34 = u_xlat5.y * u_xlat34;
					    u_xlat2.x = u_xlat6.x * u_xlat2.x;
					    u_xlat2.x = u_xlat5.y * u_xlat2.x;
					    u_xlat23.x = u_xlat2.x * u_xlat5.z + u_xlat23.x;
					    u_xlat23.x = u_xlat34 * u_xlat5.z + u_xlat23.x;
					    u_xlat23.x = u_xlat23.x + u_xlat23.x;
					    u_xlat2.xy = vs_TEXCOORD0.xy * vec2(10.0, 10.0);
					    u_xlat2 = texture(_BloodTex, u_xlat2.xy);
					    u_xlat23.x = u_xlat2.y * 0.200000003 + u_xlat23.x;
					    u_xlat23.x = u_xlat23.x + -0.200000003;
					    u_xlat23.x = u_xlat23.x * 10.0;
					    u_xlat23.x = clamp(u_xlat23.x, 0.0, 1.0);
					    u_xlat23.x = u_xlat23.x * vs_COLOR0.w;
					    u_xlat23.x = clamp(u_xlat23.x, 0.0, 1.0);
					    u_xlat2 = (-u_xlat4.zzzw) + _BloodColor2;
					    u_xlat2 = u_xlat23.xxxx * u_xlat2 + u_xlat4;
					    u_xlat4 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = vec4(u_xlat33) * u_xlat4 + u_xlat2;
					    u_xlat4 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat33 = dFdx(u_xlat12);
					    u_xlat23.x = dFdy(u_xlat12);
					    u_xlat12 = fract(u_xlat12);
					    u_xlat33 = abs(u_xlat33) + abs(u_xlat23.x);
					    u_xlat33 = u_xlat33 * _LineWidth;
					    u_xlatb33 = u_xlat33>=u_xlat12;
					    u_xlat33 = u_xlatb33 ? 0.5 : float(0.0);
					    u_xlat12 = u_xlat22.x + 0.5;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat22.x = -abs(u_xlat22.x) + 1.0;
					    u_xlat22.x = max(u_xlat22.x, 0.0);
					    u_xlat33 = u_xlat33 * u_xlat12;
					    u_xlat2 = vec4(u_xlat33) * u_xlat4 + u_xlat2;
					    u_xlat4 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = u_xlat22.xxxx * u_xlat4 + u_xlat2;
					    u_xlat22.x = u_xlat2.w * -0.300000012;
					    u_xlat4.w = vs_COLOR0.z * u_xlat22.x + u_xlat2.w;
					    u_xlat22.xy = vs_COLOR0.zx * vec2(0.100000001, -0.300000012) + vec2(1.0, 1.0);
					    u_xlat4.xyz = u_xlat22.xxx * u_xlat2.xyz;
					    u_xlat2 = u_xlat1.xxxx * u_xlat4;
					    u_xlat1 = (-u_xlat4) * u_xlat1.xxxx + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat22.x = u_xlat3.y + u_xlat3.x;
					    u_xlat22.x = u_xlat3.z + u_xlat22.x;
					    u_xlat22.x = u_xlat3.w + u_xlat22.x;
					    u_xlat3.x = u_xlat22.x * 0.25;
					    u_xlat22.x = u_xlat22.x * 0.25 + -1.5;
					    u_xlat22.x = abs(u_xlat22.x) + abs(u_xlat22.x);
					    u_xlat22.x = u_xlat22.x / u_xlat0.x;
					    u_xlat0.z = u_xlat22.x * 0.5;
					    u_xlat3.x = fract(u_xlat3.x);
					    u_xlat3.x = u_xlat3.x * 2.0 + -1.0;
					    u_xlat0.x = abs(u_xlat3.x) / u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.5;
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat3.x = (-u_xlat0.x) + 1.0;
					    u_xlat0.x = u_xlat11 * u_xlat3.x + u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat0.z + 1.0;
					    u_xlat1 = u_xlat0.xxxx * u_xlat1.wxyz + u_xlat2.wxyz;
					    u_xlat1.x = u_xlat1.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    SV_Target0.xyz = u_xlat1.yzw;
					    SV_Target0.xyz = clamp(SV_Target0.xyz, 0.0, 1.0);
					    SV_Target0.w = u_xlat22.y * u_xlat1.x;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "UNITY_UI_ALPHACLIP" }
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
						vec4 unused_0_2[9];
						vec2 _MainTex_TexelSize;
						vec4 unused_0_4[4];
						vec4 _BloodColor2;
						vec4 unused_0_6[2];
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _BloodTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					bool u_xlatb2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec2 u_xlat5;
					bool u_xlatb5;
					vec2 u_xlat7;
					vec2 u_xlat10;
					vec2 u_xlat12;
					vec2 u_xlat14;
					float u_xlat15;
					bool u_xlatb15;
					float u_xlat17;
					void main()
					{
					    u_xlat0.xy = dFdx(vs_TEXCOORD0.xy);
					    u_xlat10.xy = dFdy(vs_TEXCOORD0.xy);
					    u_xlat0.xy = abs(u_xlat10.xy) + abs(u_xlat0.xy);
					    u_xlat0.xy = u_xlat0.xy * vec2(vec2(_LineWidth, _LineWidth));
					    u_xlat0.xy = u_xlat0.xy * vec2(0.5, 0.5);
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat0.z = 0.0;
					    u_xlat1.xy = u_xlat0.wz + vs_TEXCOORD0.xy;
					    u_xlat1 = texture(_MainTex, u_xlat1.xy);
					    u_xlat2 = u_xlat0.xzzy + vs_TEXCOORD0.xyxy;
					    u_xlat0.xy = u_xlat0.zy * vec2(1.0, -1.0) + vs_TEXCOORD0.xy;
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat3 = texture(_MainTex, u_xlat2.xy);
					    u_xlat2 = texture(_MainTex, u_xlat2.zw);
					    u_xlat4.xy = max(u_xlat1.zw, u_xlat3.zw);
					    u_xlat14.xy = max(u_xlat0.zw, u_xlat2.zw);
					    u_xlat4.xy = max(u_xlat14.xy, u_xlat4.xy);
					    u_xlat15 = u_xlat4.y + -0.00100000005;
					    u_xlatb15 = u_xlat15<0.0;
					    if(((int(u_xlatb15) * int(0xffffffffu)))!=0){discard;}
					    u_xlat15 = min(u_xlat1.z, u_xlat3.z);
					    u_xlat17 = min(u_xlat0.z, u_xlat2.z);
					    u_xlat15 = min(u_xlat15, u_xlat17);
					    u_xlat17 = (-u_xlat15) + u_xlat4.x;
					    u_xlat15 = u_xlat17 * 0.5 + u_xlat15;
					    u_xlat15 = u_xlat15 + -0.5;
					    u_xlat4.x = (-u_xlat1.z) + u_xlat3.z;
					    u_xlat4.y = (-u_xlat0.z) + u_xlat2.z;
					    u_xlat10.x = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat10.x = u_xlat15 / u_xlat10.x;
					    u_xlat15 = u_xlat10.x + 0.5;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat12.xy = vs_TEXCOORD0.xy / _MainTex_TexelSize.xy;
					    u_xlat12.x = u_xlat12.y + u_xlat12.x;
					    u_xlat17 = u_xlat1.x + u_xlat3.x;
					    u_xlat1.x = u_xlat3.y;
					    u_xlat2.x = u_xlat2.x + u_xlat17;
					    u_xlat1.z = u_xlat2.y;
					    u_xlat0.x = u_xlat0.x + u_xlat2.x;
					    u_xlat1.w = u_xlat0.y;
					    u_xlat3 = u_xlat1 * vec4(10.0, 10.0, 10.0, 10.0) + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat3.y + u_xlat3.x;
					    u_xlat5.x = u_xlat3.z + u_xlat5.x;
					    u_xlat5.x = u_xlat3.w + u_xlat5.x;
					    u_xlat2.x = u_xlat5.x * 0.25;
					    u_xlatb5 = u_xlat5.x>=6.0;
					    u_xlat5.x = u_xlatb5 ? 1.0 : float(0.0);
					    u_xlat2.x = roundEven(u_xlat2.x);
					    u_xlat2.x = u_xlat2.x * 0.333333343;
					    u_xlat2.x = u_xlat12.x * 0.333333343 + u_xlat2.x;
					    u_xlat7.x = dFdx(u_xlat2.x);
					    u_xlat12.x = dFdy(u_xlat2.x);
					    u_xlat2.x = fract(u_xlat2.x);
					    u_xlat7.x = abs(u_xlat12.x) + abs(u_xlat7.x);
					    u_xlat7.x = u_xlat7.x * _LineWidth;
					    u_xlatb2 = u_xlat7.x>=u_xlat2.x;
					    u_xlat2.x = u_xlatb2 ? 0.5 : float(0.0);
					    u_xlat15 = u_xlat15 * u_xlat2.x;
					    u_xlat2.x = u_xlat10.x;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat10.x = -abs(u_xlat10.x) + 1.0;
					    u_xlat10.x = max(u_xlat10.x, 0.0);
					    u_xlat2.x = u_xlat2.x * 0.5;
					    u_xlat2.x = u_xlat2.x * vs_COLOR0.y;
					    u_xlat7.x = (-vs_COLOR0.x) + 1.0;
					    u_xlat2.x = u_xlat7.x * u_xlat2.x;
					    u_xlat7.x = u_xlat7.x * vs_COLOR0.y;
					    u_xlat3 = u_xlat7.xxxx * vec4(1.0, 1.0, 1.0, 0.5) + vec4(0.0, 0.0, 0.0, 0.200000003);
					    u_xlat7.xy = vs_TEXCOORD0.xy * vec2(10.0, 10.0);
					    u_xlat4 = texture(_BloodTex, u_xlat7.xy);
					    u_xlat7.x = u_xlat4.y * 0.200000003 + -0.200000003;
					    u_xlat7.x = u_xlat7.x * 10.0;
					    u_xlat7.x = clamp(u_xlat7.x, 0.0, 1.0);
					    u_xlat7.x = u_xlat7.x * vs_COLOR0.w;
					    u_xlat7.x = clamp(u_xlat7.x, 0.0, 1.0);
					    u_xlat4 = (-u_xlat3.zzzw) + _BloodColor2;
					    u_xlat3 = u_xlat7.xxxx * u_xlat4 + u_xlat3;
					    u_xlat4 = (-u_xlat3) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = u_xlat2.xxxx * u_xlat4 + u_xlat3;
					    u_xlat3 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = vec4(u_xlat15) * u_xlat3 + u_xlat2;
					    u_xlat3 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = u_xlat10.xxxx * u_xlat3 + u_xlat2;
					    u_xlat10.x = u_xlat2.w * -0.300000012;
					    u_xlat3.w = vs_COLOR0.z * u_xlat10.x + u_xlat2.w;
					    u_xlat10.xy = vs_COLOR0.zx * vec2(0.100000001, -0.300000012) + vec2(1.0, 1.0);
					    u_xlat3.xyz = u_xlat10.xxx * u_xlat2.xyz;
					    u_xlat2 = u_xlat5.xxxx * u_xlat3;
					    u_xlat3 = (-u_xlat3) * u_xlat5.xxxx + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat4 = u_xlat1 * vec4(10.0, 10.0, 10.0, 10.0);
					    u_xlat1 = u_xlat1 * vec4(20.0, 20.0, 20.0, 20.0) + vec4(-1.0, -1.0, -1.0, -1.0);
					    u_xlat1 = max(u_xlat1, u_xlat4);
					    u_xlat1 = u_xlat1 + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat5.x = u_xlat1.y + u_xlat1.x;
					    u_xlat5.x = u_xlat1.z + u_xlat5.x;
					    u_xlat0.y = u_xlat1.w + u_xlat5.x;
					    u_xlat1.xy = (-u_xlat1.yw) + u_xlat1.xz;
					    u_xlat10.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat10.x = sqrt(u_xlat10.x);
					    u_xlat1.x = u_xlat0.y * 0.25 + -1.5;
					    u_xlat0.xy = u_xlat0.xy * vec2(0.075000003, 0.25);
					    u_xlat5.x = fract(u_xlat0.y);
					    u_xlat5.x = u_xlat5.x * 2.0 + -1.0;
					    u_xlat5.x = abs(u_xlat5.x) / u_xlat10.x;
					    u_xlat5.x = u_xlat5.x * 0.5;
					    u_xlat1.x = abs(u_xlat1.x) + abs(u_xlat1.x);
					    u_xlat10.x = u_xlat1.x / u_xlat10.x;
					    u_xlat5.y = u_xlat10.x * 0.5;
					    u_xlat5.xy = min(u_xlat5.xy, vec2(1.0, 1.0));
					    u_xlat1.x = (-u_xlat5.x) + 1.0;
					    u_xlat0.x = u_xlat0.x * u_xlat1.x + u_xlat5.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat5.y + 1.0;
					    u_xlat1 = u_xlat0.xxxx * u_xlat3.wxyz + u_xlat2.wxyz;
					    u_xlat1.x = u_xlat1.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    SV_Target0.xyz = u_xlat1.yzw;
					    SV_Target0.xyz = clamp(SV_Target0.xyz, 0.0, 1.0);
					    SV_Target0.w = u_xlat10.y * u_xlat1.x;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_BLOOD_ON" "UNITY_UI_ALPHACLIP" }
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
						vec4 unused_0_0[10];
						vec2 _PaintTexSize;
						vec3 _PaintTexVolume;
						vec4 unused_0_3[44];
						float _LineWidth;
						vec4 unused_0_5[9];
						vec2 _MainTex_TexelSize;
						vec2 _PaintTex_TexelSize;
						vec4 unused_0_8[4];
						vec4 _BloodColor2;
						vec4 unused_0_10[2];
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _PaintTex;
					uniform  sampler2D _BloodTex;
					in  vec4 vs_COLOR0;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec3 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec4 u_xlat10;
					float u_xlat11;
					float u_xlat12;
					vec3 u_xlat21;
					vec2 u_xlat22;
					vec2 u_xlat23;
					vec2 u_xlat26;
					vec2 u_xlat27;
					float u_xlat33;
					bool u_xlatb33;
					float u_xlat34;
					float u_xlat35;
					void main()
					{
					    u_xlat0.xy = dFdx(vs_TEXCOORD0.xy);
					    u_xlat22.xy = dFdy(vs_TEXCOORD0.xy);
					    u_xlat0.xy = abs(u_xlat22.xy) + abs(u_xlat0.xy);
					    u_xlat0.xy = u_xlat0.xy * vec2(vec2(_LineWidth, _LineWidth));
					    u_xlat0.xy = u_xlat0.xy * vec2(0.5, 0.5);
					    u_xlat0.w = (-u_xlat0.x);
					    u_xlat0.z = 0.0;
					    u_xlat1.xy = u_xlat0.wz + vs_TEXCOORD0.xy;
					    u_xlat1 = texture(_MainTex, u_xlat1.xy);
					    u_xlat2 = u_xlat0.xzzy + vs_TEXCOORD0.xyxy;
					    u_xlat0.xy = u_xlat0.zy * vec2(1.0, -1.0) + vs_TEXCOORD0.xy;
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat3 = texture(_MainTex, u_xlat2.xy).yxzw;
					    u_xlat2 = texture(_MainTex, u_xlat2.zw);
					    u_xlat4.xy = max(u_xlat1.zw, u_xlat3.zw);
					    u_xlat26.xy = max(u_xlat0.zw, u_xlat2.zw);
					    u_xlat4.xy = max(u_xlat26.xy, u_xlat4.xy);
					    u_xlat33 = u_xlat4.y + -0.00100000005;
					    u_xlatb33 = u_xlat33<0.0;
					    if(((int(u_xlatb33) * int(0xffffffffu)))!=0){discard;}
					    u_xlat33 = min(u_xlat1.z, u_xlat3.z);
					    u_xlat34 = min(u_xlat0.z, u_xlat2.z);
					    u_xlat33 = min(u_xlat33, u_xlat34);
					    u_xlat34 = (-u_xlat33) + u_xlat4.x;
					    u_xlat33 = u_xlat34 * 0.5 + u_xlat33;
					    u_xlat33 = u_xlat33 + -0.5;
					    u_xlat4.x = (-u_xlat1.z) + u_xlat3.z;
					    u_xlat4.y = (-u_xlat0.z) + u_xlat2.z;
					    u_xlat22.x = dot(u_xlat4.xy, u_xlat4.xy);
					    u_xlat22.x = sqrt(u_xlat22.x);
					    u_xlat22.x = u_xlat33 / u_xlat22.x;
					    u_xlat33 = u_xlat22.x;
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat33 = u_xlat33 * 0.5;
					    u_xlat33 = u_xlat33 * vs_COLOR0.y;
					    u_xlat23.x = (-vs_COLOR0.x) + 1.0;
					    u_xlat33 = u_xlat33 * u_xlat23.x;
					    u_xlat23.x = u_xlat23.x * vs_COLOR0.y;
					    u_xlat4 = u_xlat23.xxxx * vec4(1.0, 1.0, 1.0, 0.5) + vec4(0.0, 0.0, 0.0, 0.200000003);
					    u_xlat1.xz = u_xlat1.xy + u_xlat3.yx;
					    u_xlat3.y = u_xlat1.y;
					    u_xlat1.xy = u_xlat2.xy + u_xlat1.xz;
					    u_xlat3.z = u_xlat2.y;
					    u_xlat1.xy = u_xlat0.xy + u_xlat1.xy;
					    u_xlat3.w = u_xlat0.y;
					    u_xlat0.x = u_xlat1.y * 2.5 + 1.0;
					    u_xlat11 = u_xlat1.x * 0.075000003;
					    u_xlat1 = u_xlat3 * vec4(10.0, 10.0, 10.0, 10.0) + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat1.x = u_xlat1.y + u_xlat1.x;
					    u_xlat1.x = u_xlat1.z + u_xlat1.x;
					    u_xlat1.x = u_xlat1.w + u_xlat1.x;
					    u_xlat12 = u_xlat1.x * 0.25;
					    u_xlatb1 = u_xlat1.x>=6.0;
					    u_xlat1.x = u_xlatb1 ? 1.0 : float(0.0);
					    u_xlat23.x = max(u_xlat12, 0.0);
					    u_xlat12 = roundEven(u_xlat12);
					    u_xlat12 = u_xlat12 * 0.333333343;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat23.x;
					    u_xlat2.y = u_xlat23.x + -0.5;
					    u_xlat0.x = max(u_xlat0.x, -1.0);
					    u_xlat0.x = min(u_xlat0.x, 1.0);
					    u_xlat5 = u_xlat3 * vec4(10.0, 10.0, 10.0, 10.0);
					    u_xlat3 = u_xlat3 * vec4(20.0, 20.0, 20.0, 20.0) + vec4(-1.0, -1.0, -1.0, -1.0);
					    u_xlat3 = max(u_xlat3, u_xlat5);
					    u_xlat3 = u_xlat3 + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat23.xy = (-u_xlat3.yw) + u_xlat3.xz;
					    u_xlat35 = _PaintTex_TexelSize.xxxy.w / _MainTex_TexelSize.y;
					    u_xlat5.xy = vs_TEXCOORD0.xy / _MainTex_TexelSize.xy;
					    u_xlat27.xy = u_xlat5.xy / vec2(u_xlat35);
					    u_xlat35 = u_xlat5.y + u_xlat5.x;
					    u_xlat12 = u_xlat35 * 0.333333343 + u_xlat12;
					    u_xlat2.xz = u_xlat23.xy * u_xlat0.xx + u_xlat27.xy;
					    u_xlat0.x = dot(u_xlat23.xy, u_xlat23.xy);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat2.xyz = max(u_xlat2.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat5.xyz = _PaintTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = min(u_xlat2.xyz, u_xlat5.xyz);
					    u_xlat2.xyz = u_xlat2.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat5.xyz = fract(u_xlat2.xyz);
					    u_xlat2.xyz = floor(u_xlat2.xyz);
					    u_xlat6.xyz = (-u_xlat5.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat23.x = _PaintTexVolume.x / _PaintTexSize.xxxy.z;
					    u_xlat34 = u_xlat2.y * u_xlat23.x;
					    u_xlat7.xy = vec2(1.0, 1.0) / vec2(_PaintTexSize.x, _PaintTexSize.y);
					    u_xlat8.y = u_xlat7.x * u_xlat2.x + u_xlat34;
					    u_xlat8.z = u_xlat2.z * u_xlat7.y;
					    u_xlat9 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat35 = u_xlat6.x * u_xlat9.x;
					    u_xlat35 = u_xlat6.y * u_xlat35;
					    u_xlat9.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat8.x = u_xlat7.x * u_xlat9.x + u_xlat34;
					    u_xlat10 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat21.yz = u_xlat8.yx;
					    u_xlat34 = u_xlat5.x * u_xlat10.x;
					    u_xlat34 = u_xlat6.y * u_xlat34;
					    u_xlat34 = u_xlat6.z * u_xlat34;
					    u_xlat34 = u_xlat35 * u_xlat6.z + u_xlat34;
					    u_xlat23.x = u_xlat23.x * u_xlat9.y;
					    u_xlat8.y = u_xlat7.x * u_xlat2.x + u_xlat23.x;
					    u_xlat8.x = u_xlat7.x * u_xlat9.x + u_xlat23.x;
					    u_xlat21.x = u_xlat7.y * u_xlat9.z;
					    u_xlat2 = textureLod(_PaintTex, u_xlat8.yz, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xz, 0.0);
					    u_xlat23.x = u_xlat5.x * u_xlat7.x;
					    u_xlat23.x = u_xlat5.y * u_xlat23.x;
					    u_xlat2.x = u_xlat6.x * u_xlat2.x;
					    u_xlat2.x = u_xlat5.y * u_xlat2.x;
					    u_xlat34 = u_xlat2.x * u_xlat6.z + u_xlat34;
					    u_xlat23.x = u_xlat23.x * u_xlat6.z + u_xlat34;
					    u_xlat2 = textureLod(_PaintTex, u_xlat21.yx, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat21.zx, 0.0);
					    u_xlat8.w = u_xlat21.x;
					    u_xlat34 = u_xlat5.x * u_xlat7.x;
					    u_xlat34 = u_xlat6.y * u_xlat34;
					    u_xlat2.x = u_xlat6.x * u_xlat2.x;
					    u_xlat2.x = u_xlat6.y * u_xlat2.x;
					    u_xlat23.x = u_xlat2.x * u_xlat5.z + u_xlat23.x;
					    u_xlat23.x = u_xlat34 * u_xlat5.z + u_xlat23.x;
					    u_xlat2 = textureLod(_PaintTex, u_xlat8.yw, 0.0);
					    u_xlat7 = textureLod(_PaintTex, u_xlat8.xw, 0.0);
					    u_xlat34 = u_xlat5.x * u_xlat7.x;
					    u_xlat34 = u_xlat5.y * u_xlat34;
					    u_xlat2.x = u_xlat6.x * u_xlat2.x;
					    u_xlat2.x = u_xlat5.y * u_xlat2.x;
					    u_xlat23.x = u_xlat2.x * u_xlat5.z + u_xlat23.x;
					    u_xlat23.x = u_xlat34 * u_xlat5.z + u_xlat23.x;
					    u_xlat23.x = u_xlat23.x + u_xlat23.x;
					    u_xlat2.xy = vs_TEXCOORD0.xy * vec2(10.0, 10.0);
					    u_xlat2 = texture(_BloodTex, u_xlat2.xy);
					    u_xlat23.x = u_xlat2.y * 0.200000003 + u_xlat23.x;
					    u_xlat23.x = u_xlat23.x + -0.200000003;
					    u_xlat23.x = u_xlat23.x * 10.0;
					    u_xlat23.x = clamp(u_xlat23.x, 0.0, 1.0);
					    u_xlat23.x = u_xlat23.x * vs_COLOR0.w;
					    u_xlat23.x = clamp(u_xlat23.x, 0.0, 1.0);
					    u_xlat2 = (-u_xlat4.zzzw) + _BloodColor2;
					    u_xlat2 = u_xlat23.xxxx * u_xlat2 + u_xlat4;
					    u_xlat4 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = vec4(u_xlat33) * u_xlat4 + u_xlat2;
					    u_xlat4 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat33 = dFdx(u_xlat12);
					    u_xlat23.x = dFdy(u_xlat12);
					    u_xlat12 = fract(u_xlat12);
					    u_xlat33 = abs(u_xlat33) + abs(u_xlat23.x);
					    u_xlat33 = u_xlat33 * _LineWidth;
					    u_xlatb33 = u_xlat33>=u_xlat12;
					    u_xlat33 = u_xlatb33 ? 0.5 : float(0.0);
					    u_xlat12 = u_xlat22.x + 0.5;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat22.x = -abs(u_xlat22.x) + 1.0;
					    u_xlat22.x = max(u_xlat22.x, 0.0);
					    u_xlat33 = u_xlat33 * u_xlat12;
					    u_xlat2 = vec4(u_xlat33) * u_xlat4 + u_xlat2;
					    u_xlat4 = (-u_xlat2) + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat2 = u_xlat22.xxxx * u_xlat4 + u_xlat2;
					    u_xlat22.x = u_xlat2.w * -0.300000012;
					    u_xlat4.w = vs_COLOR0.z * u_xlat22.x + u_xlat2.w;
					    u_xlat22.xy = vs_COLOR0.zx * vec2(0.100000001, -0.300000012) + vec2(1.0, 1.0);
					    u_xlat4.xyz = u_xlat22.xxx * u_xlat2.xyz;
					    u_xlat2 = u_xlat1.xxxx * u_xlat4;
					    u_xlat1 = (-u_xlat4) * u_xlat1.xxxx + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat22.x = u_xlat3.y + u_xlat3.x;
					    u_xlat22.x = u_xlat3.z + u_xlat22.x;
					    u_xlat22.x = u_xlat3.w + u_xlat22.x;
					    u_xlat3.x = u_xlat22.x * 0.25;
					    u_xlat22.x = u_xlat22.x * 0.25 + -1.5;
					    u_xlat22.x = abs(u_xlat22.x) + abs(u_xlat22.x);
					    u_xlat22.x = u_xlat22.x / u_xlat0.x;
					    u_xlat0.z = u_xlat22.x * 0.5;
					    u_xlat3.x = fract(u_xlat3.x);
					    u_xlat3.x = u_xlat3.x * 2.0 + -1.0;
					    u_xlat0.x = abs(u_xlat3.x) / u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 0.5;
					    u_xlat0.xz = min(u_xlat0.xz, vec2(1.0, 1.0));
					    u_xlat3.x = (-u_xlat0.x) + 1.0;
					    u_xlat0.x = u_xlat11 * u_xlat3.x + u_xlat0.x;
					    u_xlat0.x = (-u_xlat0.x) * u_xlat0.z + 1.0;
					    u_xlat1 = u_xlat0.xxxx * u_xlat1.wxyz + u_xlat2.wxyz;
					    u_xlat1.x = u_xlat1.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    SV_Target0.xyz = u_xlat1.yzw;
					    SV_Target0.xyz = clamp(SV_Target0.xyz, 0.0, 1.0);
					    SV_Target0.w = u_xlat22.y * u_xlat1.x;
					    return;
					}"
				}
			}
		}
	}
}