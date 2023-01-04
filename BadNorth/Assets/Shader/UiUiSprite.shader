Shader "Ui/UiSprite" {
	Properties {
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (1,1,1,1)
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
			Blend One OneMinusSrcAlpha, One OneMinusSrcAlpha
			ColorMask 0 -1
			ZWrite Off
			Cull Off
			Stencil {
				ReadMask 0
				WriteMask 0
				Comp Disabled
				Pass Keep
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 62365
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
					in  vec2 in_TEXCOORD2;
					in  vec4 in_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
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
					    u_xlat0.x = in_TEXCOORD1.x + 0.5;
					    vs_TEXCOORD1.x = (-u_xlat0.x) + 1.0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    vs_TEXCOORD1.y = in_TEXCOORD1.y + 1.0;
					    vs_TEXCOORD2.xy = in_TEXCOORD2.xy + vec2(1.0, 0.0);
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
						vec4 unused_0_2[2];
						float _AAFactor;
						vec4 unused_0_4[7];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					float u_xlat3;
					float u_xlat4;
					vec3 u_xlat6;
					float u_xlat8;
					bool u_xlatb8;
					vec2 u_xlat9;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat8 = min(u_xlat0.w, vs_COLOR0.w);
					    u_xlat8 = u_xlat8 + -0.00999999978;
					    u_xlatb8 = u_xlat8<0.0;
					    if(((int(u_xlatb8) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.xyz = dFdx(u_xlat0.wyx);
					    u_xlat2.xyz = dFdy(u_xlat0.wyx);
					    u_xlat6.xyz = abs(u_xlat1.yzz) + abs(u_xlat2.yzz);
					    u_xlat1.w = u_xlat2.x;
					    u_xlat8 = dot(u_xlat1.xw, u_xlat1.xw);
					    u_xlat8 = sqrt(u_xlat8);
					    u_xlat1.xy = (-u_xlat6.xy) * vec2(0.5, 0.25) + vec2(0.5, 0.25);
					    u_xlat9.xy = u_xlat6.xz * vec2(0.5, 0.75) + vec2(0.5, 0.25);
					    u_xlat9.xy = min(u_xlat9.xy, vec2(1.0, 1.0));
					    u_xlat1.xy = max(u_xlat1.xy, vec2(0.0, 0.0));
					    u_xlat0.xy = u_xlat0.yx + (-u_xlat1.xy);
					    u_xlat1.xy = (-u_xlat1.xy) + u_xlat9.xy;
					    u_xlat0.xy = u_xlat0.xy / u_xlat1.xy;
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * vec2(0.600000024, 0.300000012);
					    u_xlat1.xyz = (-vs_COLOR0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz + vs_COLOR0.xyz;
					    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat1.xyz = u_xlat0.yyy * u_xlat2.xyz + u_xlat1.xyz;
					    u_xlat0.x = vs_TEXCOORD2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.w = 1.0;
					    u_xlat2 = u_xlat0.xxxx * u_xlat1;
					    u_xlat1 = (-u_xlat0.xxxx) * u_xlat1 + vec4(0.0, 0.0, 0.0, 1.0);
					    u_xlat0.x = max(vs_TEXCOORD1.y, 1.0);
					    u_xlat0.x = u_xlat0.x * 0.5;
					    u_xlat4 = _AAFactor + 1.0;
					    u_xlat3 = u_xlat4 * u_xlat0.x;
					    u_xlat0.x = u_xlat4 * (-u_xlat0.x);
					    u_xlat0.x = u_xlat8 * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _LineWidth + vs_TEXCOORD1.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat4 = u_xlat8 * u_xlat3;
					    u_xlat4 = u_xlat4 * _LineWidth + vs_TEXCOORD1.x;
					    u_xlat4 = clamp(u_xlat4, 0.0, 1.0);
					    u_xlat3 = (-u_xlat4) + 1.0;
					    u_xlat3 = u_xlat8 * u_xlat3 + u_xlat4;
					    u_xlat4 = (-u_xlat8) * u_xlat4 + u_xlat4;
					    u_xlat4 = max(u_xlat4, 0.0);
					    u_xlat3 = min(u_xlat3, 1.0);
					    u_xlat3 = (-u_xlat4) + u_xlat3;
					    u_xlat4 = (-u_xlat4) + u_xlat0.w;
					    u_xlat4 = u_xlat4 / u_xlat3;
					    u_xlat4 = clamp(u_xlat4, 0.0, 1.0);
					    u_xlat4 = (-u_xlat4) + 1.0;
					    u_xlat3 = vs_TEXCOORD1.y;
					    u_xlat3 = clamp(u_xlat3, 0.0, 1.0);
					    u_xlat4 = u_xlat4 * u_xlat3;
					    u_xlat1 = vec4(u_xlat4) * u_xlat1 + u_xlat2;
					    u_xlat4 = (-u_xlat0.x) + 1.0;
					    u_xlat4 = u_xlat8 * u_xlat4 + u_xlat0.x;
					    u_xlat0.x = (-u_xlat8) * u_xlat0.x + u_xlat0.x;
					    u_xlat0.x = max(u_xlat0.x, 0.0);
					    u_xlat4 = min(u_xlat4, 1.0);
					    u_xlat4 = (-u_xlat0.x) + u_xlat4;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat0.w;
					    u_xlat0.x = u_xlat0.x / u_xlat4;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0 = u_xlat1 * u_xlat0.xxxx;
					    SV_Target0 = u_xlat0 * vs_COLOR0.wwww;
					    return;
					}"
				}
			}
		}
	}
}