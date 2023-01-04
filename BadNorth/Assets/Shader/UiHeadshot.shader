Shader "Ui/Headshot" {
	Properties {
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_UvTex ("UvTex", 2D) = "white" {}
		_PartTexTiling ("PartTexTiling", Vector) = (4,1,1,1)
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_ColorMask ("Color Mask", Float) = 15
	}
	SubShader {
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ZTest Always
			Cull Off
			Stencil {
				ReadMask 0
				WriteMask 0
				Comp Disabled
				Pass Keep
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 1288
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
					in  vec2 in_TEXCOORD1;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    vs_COLOR0 = in_COLOR0;
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
						vec4 unused_0_4[5];
						vec4 _UvTex_TexelSize;
						vec4 _MainTex_TexelSize;
					};
					uniform  sampler2D _UvTex;
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec2 u_xlat2;
					vec2 u_xlat3;
					vec2 u_xlat5;
					vec2 u_xlat8;
					void main()
					{
					    u_xlat0 = texture(_UvTex, vs_TEXCOORD1.xy);
					    u_xlat0.x = (-u_xlat0.x) + 0.5;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy, -1.0);
					    u_xlat3.x = u_xlat1.w + -0.00100000005;
					    u_xlat0.x = max(u_xlat3.x, u_xlat0.x);
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = u_xlat0.w + -0.00100000005;
					    u_xlatb0 = u_xlat0.x<0.0;
					    if(((int(u_xlatb0) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0.x = vs_TEXCOORD1.x / _UvTex_TexelSize.x;
					    u_xlat3.x = dFdx(u_xlat0.x);
					    u_xlat0.x = dFdy(u_xlat0.x);
					    u_xlat0.x = abs(u_xlat0.x) + abs(u_xlat3.x);
					    u_xlat0.y = _AAFactor + 1.0;
					    u_xlat0.xyz = u_xlat0.xyy * vec3(0.125, -0.5, 0.5);
					    u_xlat2.xy = u_xlat0.xx * u_xlat0.yz;
					    u_xlat2.xy = u_xlat2.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.439999998, 0.439999998);
					    u_xlat2.xy = clamp(u_xlat2.xy, 0.0, 1.0);
					    u_xlat8.xy = (-u_xlat2.xy) + vec2(1.0, 1.0);
					    u_xlat8.xy = u_xlat0.xx * u_xlat8.xy + u_xlat2.xy;
					    u_xlat2.xy = (-u_xlat0.xx) * u_xlat2.xy + u_xlat2.xy;
					    u_xlat2.xy = max(u_xlat2.xy, vec2(0.0, 0.0));
					    u_xlat8.xy = min(u_xlat8.xy, vec2(1.0, 1.0));
					    u_xlat8.xy = (-u_xlat2.xy) + u_xlat8.xy;
					    u_xlat0.xw = u_xlat0.ww + (-u_xlat2.xy);
					    u_xlat0.xw = u_xlat0.xw / u_xlat8.xy;
					    u_xlat0.xw = clamp(u_xlat0.xw, 0.0, 1.0);
					    u_xlat2.x = vs_TEXCOORD0.x / _MainTex_TexelSize.x;
					    u_xlat5.x = dFdx(u_xlat2.x);
					    u_xlat2.x = dFdy(u_xlat2.x);
					    u_xlat2.x = abs(u_xlat2.x) + abs(u_xlat5.x);
					    u_xlat2.x = u_xlat2.x * 0.125;
					    u_xlat3.xy = u_xlat0.yz * u_xlat2.xx;
					    u_xlat3.xy = u_xlat3.xy * vec2(vec2(_LineWidth, _LineWidth)) + vec2(0.300000012, 0.300000012);
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat5.xy = (-u_xlat3.xy) + vec2(1.0, 1.0);
					    u_xlat5.xy = u_xlat2.xx * u_xlat5.xy + u_xlat3.xy;
					    u_xlat3.xy = (-u_xlat2.xx) * u_xlat3.xy + u_xlat3.xy;
					    u_xlat3.xy = max(u_xlat3.xy, vec2(0.0, 0.0));
					    u_xlat2.xy = min(u_xlat5.xy, vec2(1.0, 1.0));
					    u_xlat2.xy = (-u_xlat3.xy) + u_xlat2.xy;
					    u_xlat3.xy = (-u_xlat3.xy) + u_xlat1.ww;
					    u_xlat3.xy = u_xlat3.xy / u_xlat2.xy;
					    u_xlat3.xy = clamp(u_xlat3.xy, 0.0, 1.0);
					    u_xlat0.xw = u_xlat3.yx * u_xlat0.wx;
					    u_xlat1.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat0.xyz = u_xlat1.xyz * vs_COLOR0.xyz;
					    u_xlat1.x = 1.20000005;
					    u_xlat1.w = vs_COLOR0.w;
					    SV_Target0 = u_xlat0 * u_xlat1.xxxw;
					    return;
					}"
				}
			}
		}
	}
}