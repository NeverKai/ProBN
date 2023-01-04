Shader "Ui/UiFlair" {
	Properties {
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (1,1,1,1)
		_Progress ("Progress", Range(0, 1)) = 1
		_Fade ("Fade", Range(0, 1)) = 0
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_ColorMask ("Color Mask", Float) = 15
		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
	}
	SubShader {
		Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ColorMask 0 -1
			Cull Off
			Stencil {
				ReadMask 0
				WriteMask 0
				Comp Disabled
				Pass Keep
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 60246
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
						vec4 _Color;
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
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec4 in_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
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
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy;
					    vs_COLOR0 = in_COLOR0 * _Color;
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
						vec4 unused_0_2[8];
						vec4 _MainTex_TexelSize;
						vec4 unused_0_4[2];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec2 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec2 u_xlat3;
					float u_xlat4;
					float u_xlat8;
					bool u_xlatb9;
					float u_xlat12;
					bool u_xlatb12;
					void main()
					{
					    u_xlat0.x = dFdx(vs_TEXCOORD0.x);
					    u_xlat4 = dFdy(vs_TEXCOORD0.x);
					    u_xlat0.x = abs(u_xlat4) + abs(u_xlat0.x);
					    u_xlat0.x = _MainTex_TexelSize.x / u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * 32.0;
					    u_xlat4 = vs_TEXCOORD1.y * -0.5 + 1.0;
					    u_xlat8 = vs_TEXCOORD1.x * vs_TEXCOORD1.x;
					    u_xlat8 = (-u_xlat8) * vs_TEXCOORD1.x + 1.0;
					    u_xlat1.xy = (-vs_TEXCOORD1.xy) + vec2(2.0, 1.0);
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat12 = u_xlat1.x + (-u_xlat2.x);
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat8 = u_xlat8 * u_xlat12;
					    u_xlat12 = u_xlat8 * 0.25999999 + 0.540000021;
					    u_xlat4 = u_xlat12 * u_xlat4 + u_xlat2.y;
					    u_xlat4 = u_xlat4 + -1.0;
					    u_xlat12 = _LineWidth * 0.5;
					    u_xlat3.x = u_xlat4 * u_xlat0.x + u_xlat12;
					    u_xlat3.y = u_xlat4 * u_xlat0.x + (-u_xlat12);
					    u_xlat0.xy = u_xlat3.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat12 = u_xlat3.x + 2.0;
					    u_xlatb12 = u_xlat12<0.0;
					    if(((int(u_xlatb12) * int(0xffffffffu)))!=0){discard;}
					    u_xlat12 = vs_TEXCOORD1.x * 1.10000002 + u_xlat2.x;
					    u_xlat1.x = (-u_xlat2.x) + 1.0;
					    u_xlat1.x = u_xlat1.x + (-vs_TEXCOORD1.y);
					    u_xlat1.x = u_xlat1.x * 100.0 + 2.0;
					    u_xlat12 = u_xlat12 + -1.0;
					    u_xlatb9 = u_xlat12<0.0;
					    u_xlat12 = u_xlat12 * 100.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    u_xlatb9 = u_xlat1.x<0.0;
					    u_xlat1.x = u_xlat1.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat12 = u_xlat12 * u_xlat1.x;
					    u_xlat0.x = u_xlat0.x * u_xlat12;
					    u_xlat1.xyw = u_xlat0.yyy * u_xlat1.yyy + vs_TEXCOORD1.yyy;
					    SV_Target0.xyz = u_xlat1.xyw * vs_COLOR0.xyz + vec3(u_xlat8);
					    u_xlat0.x = u_xlat0.x * vs_COLOR0.w;
					    SV_Target0.w = u_xlat0.x;
					    if(((int(u_xlatb9) * int(0xffffffffu)))!=0){discard;}
					    return;
					}"
				}
			}
		}
	}
}