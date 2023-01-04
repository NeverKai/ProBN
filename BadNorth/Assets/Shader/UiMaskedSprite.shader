Shader "Ui/MaskedSprite" {
	Properties {
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_MaskTex ("Border Texture", 2D) = "white" {}
		_DecorTex ("Decor Texture", 2D) = "white" {}
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
			ZWrite Off
			Stencil {
				ReadMask 0
				WriteMask 0
				Comp Disabled
				Pass Keep
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 53559
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[9];
						vec4 _MainTex_ST;
						vec4 unused_0_4;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						mat4x4 unity_WorldToObject;
						vec4 unused_1_2[3];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec3 in_NORMAL0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_TANGENT0;
					out vec3 vs_NORMAL0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec2 u_xlat3;
					float u_xlat12;
					bool u_xlatb12;
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
					    u_xlat0.x = in_COLOR0.x;
					    u_xlat12 = (-in_TEXCOORD0.x) + -1.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat1.x = u_xlat12 * -0.849999964 + 0.899999976;
					    u_xlat2.x = 0.899999976;
					    u_xlat2.yzw = in_COLOR0.xxw;
					    u_xlat1.yz = in_COLOR0.yz;
					    u_xlat3.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlatb12 = -0.5<u_xlat3.x;
					    u_xlat1.xyz = (bool(u_xlatb12)) ? u_xlat2.xyz : u_xlat1.xyz;
					    u_xlat0.yz = u_xlat1.yz;
					    u_xlat2.xyz = u_xlat0.xyz * in_TANGENT0.zzz;
					    u_xlat2 = u_xlat2 + u_xlat2;
					    vs_COLOR0.w = u_xlat2.w;
					    vs_COLOR0.w = clamp(vs_COLOR0.w, 0.0, 1.0);
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    u_xlat0.xy = in_COLOR0.yz * vec2(2.0, 10.0);
					    u_xlat2.x = 1.0;
					    u_xlat2.z = in_TEXCOORD0.y;
					    u_xlat0.xy = (bool(u_xlatb12)) ? u_xlat0.xy : u_xlat2.xz;
					    vs_TANGENT0.w = u_xlat0.y * _LineWidth;
					    vs_TANGENT0.xz = u_xlat0.xy;
					    vs_TANGENT0.y = in_TANGENT0.w;
					    u_xlatb0 = in_TEXCOORD0.x>=1.5;
					    u_xlat1.w = 0.0;
					    u_xlat0.zw = (bool(u_xlatb0)) ? vec2(0.600000024, 1.0) : u_xlat1.xw;
					    u_xlat0.x = dot(in_NORMAL0.xyz, unity_WorldToObject[0].xyz);
					    u_xlat0.y = dot(in_NORMAL0.xyz, unity_WorldToObject[1].xyz);
					    vs_NORMAL0.xyz = u_xlat0.xyz;
					    vs_TEXCOORD2.x = u_xlat0.w;
					    vs_TEXCOORD0.xy = u_xlat3.xy;
					    u_xlat0.xy = u_xlat3.xy * vec2(0.5, 0.5) + vec2(-0.5, -0.5);
					    u_xlat0.xy = fract(u_xlat0.xy);
					    u_xlat0.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
					    vs_TEXCOORD3.xy = abs(u_xlat0.xy);
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy;
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
						vec4 unused_0_0[56];
						float _LineWidth;
						vec4 unused_0_2[9];
						vec4 _MainTex_ST;
						vec4 unused_0_4;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						mat4x4 unity_WorldToObject;
						vec4 unused_1_2[3];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec4 in_TANGENT0;
					in  vec3 in_NORMAL0;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					out vec4 vs_COLOR0;
					out vec4 vs_TANGENT0;
					out vec3 vs_NORMAL0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec2 u_xlat3;
					float u_xlat12;
					bool u_xlatb12;
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
					    u_xlat0.x = in_COLOR0.x;
					    u_xlat12 = (-in_TEXCOORD0.x) + -1.0;
					    u_xlat12 = clamp(u_xlat12, 0.0, 1.0);
					    u_xlat1.x = u_xlat12 * -0.849999964 + 0.899999976;
					    u_xlat2.x = 0.899999976;
					    u_xlat2.yzw = in_COLOR0.xxw;
					    u_xlat1.yz = in_COLOR0.yz;
					    u_xlat3.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlatb12 = -0.5<u_xlat3.x;
					    u_xlat1.xyz = (bool(u_xlatb12)) ? u_xlat2.xyz : u_xlat1.xyz;
					    u_xlat0.yz = u_xlat1.yz;
					    u_xlat2.xyz = u_xlat0.xyz * in_TANGENT0.zzz;
					    u_xlat2 = u_xlat2 + u_xlat2;
					    vs_COLOR0.w = u_xlat2.w;
					    vs_COLOR0.w = clamp(vs_COLOR0.w, 0.0, 1.0);
					    vs_COLOR0.xyz = u_xlat2.xyz;
					    u_xlat0.xy = in_COLOR0.yz * vec2(2.0, 10.0);
					    u_xlat2.x = 1.0;
					    u_xlat2.z = in_TEXCOORD0.y;
					    u_xlat0.xy = (bool(u_xlatb12)) ? u_xlat0.xy : u_xlat2.xz;
					    vs_TANGENT0.w = u_xlat0.y * _LineWidth;
					    vs_TANGENT0.xz = u_xlat0.xy;
					    vs_TANGENT0.y = in_TANGENT0.w;
					    u_xlatb0 = in_TEXCOORD0.x>=1.5;
					    u_xlat1.w = 0.0;
					    u_xlat0.zw = (bool(u_xlatb0)) ? vec2(0.600000024, 1.0) : u_xlat1.xw;
					    u_xlat0.x = dot(in_NORMAL0.xyz, unity_WorldToObject[0].xyz);
					    u_xlat0.y = dot(in_NORMAL0.xyz, unity_WorldToObject[1].xyz);
					    vs_NORMAL0.xyz = u_xlat0.xyz;
					    vs_TEXCOORD2.x = u_xlat0.w;
					    vs_TEXCOORD0.xy = u_xlat3.xy;
					    u_xlat0.xy = u_xlat3.xy * vec2(0.5, 0.5) + vec2(-0.5, -0.5);
					    u_xlat0.xy = fract(u_xlat0.xy);
					    u_xlat0.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
					    vs_TEXCOORD3.xy = abs(u_xlat0.xy);
					    vs_TEXCOORD1.xy = in_TEXCOORD1.xy;
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
						vec4 unused_0_4[8];
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _DecorTex;
					uniform  sampler2D _MaskTex;
					in  vec4 vs_COLOR0;
					in  vec4 vs_TANGENT0;
					in  vec3 vs_NORMAL0;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					bool u_xlatb2;
					vec4 u_xlat3;
					vec3 u_xlat5;
					bool u_xlatb5;
					float u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					float u_xlat10;
					bool u_xlatb10;
					float u_xlat13;
					bool u_xlatb13;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD0.xy * vec2(0.5, 0.5) + vec2(-0.5, -0.5);
					    u_xlat0.xy = fract(u_xlat0.xy);
					    u_xlat0.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
					    u_xlat0.xy = abs(u_xlat0.xy);
					    u_xlat0 = texture(_DecorTex, u_xlat0.xy);
					    u_xlat1 = texture(_MaskTex, vs_TEXCOORD1.xy);
					    u_xlat1.x = u_xlat0.w + (-u_xlat1.y);
					    u_xlatb9 = vs_TEXCOORD2.x>=0.5;
					    u_xlat9 = u_xlatb9 ? 1.0 : float(0.0);
					    u_xlat1.x = u_xlat9 * u_xlat1.x + u_xlat1.y;
					    u_xlat5.x = (-u_xlat1.x) + 1.0;
					    u_xlatb13 = -1.5>=vs_TEXCOORD1.x;
					    u_xlat13 = u_xlatb13 ? 1.0 : float(0.0);
					    u_xlat1.x = u_xlat13 * u_xlat5.x + u_xlat1.x;
					    u_xlat2.x = dFdx(u_xlat1.x);
					    u_xlat2.y = dFdy(u_xlat1.x);
					    u_xlat5.x = dot(u_xlat2.xy, u_xlat2.xy);
					    u_xlat5.x = sqrt(u_xlat5.x);
					    u_xlat5.x = max(u_xlat5.x, 0.00100000005);
					    u_xlat2.x = max(vs_TANGENT0.z, 1.0);
					    u_xlat2.x = u_xlat2.x * 0.5;
					    u_xlat6 = _AAFactor + 1.0;
					    u_xlat2.z = u_xlat6 * (-u_xlat2.x);
					    u_xlat2.x = u_xlat6 * u_xlat2.x;
					    u_xlat2.xy = u_xlat5.xx * u_xlat2.xz;
					    u_xlat2.x = u_xlat2.x * _LineWidth + vs_NORMAL0.z;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat6 = u_xlat2.y * _LineWidth + vs_NORMAL0.z;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat10 = (-u_xlat6) + 1.0;
					    u_xlat10 = u_xlat5.x * u_xlat10 + u_xlat6;
					    u_xlat6 = (-u_xlat5.x) * u_xlat6 + u_xlat6;
					    u_xlat6 = max(u_xlat6, 0.0);
					    u_xlat10 = min(u_xlat10, 1.0);
					    u_xlat10 = (-u_xlat6) + u_xlat10;
					    u_xlat6 = u_xlat1.x + (-u_xlat6);
					    u_xlat6 = u_xlat6 / u_xlat10;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat3 = texture(_MainTex, vs_TEXCOORD0.xy, -1.0);
					    u_xlat3 = u_xlat3 + vec4(-1.0, -1.0, -1.0, -1.0);
					    u_xlatb10 = vs_TEXCOORD0.x>=-0.5;
					    u_xlat10 = u_xlatb10 ? 1.0 : float(0.0);
					    u_xlat3 = vec4(u_xlat10) * u_xlat3 + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat0 = u_xlat0 + (-u_xlat3);
					    u_xlat0 = vec4(u_xlat9) * u_xlat0 + u_xlat3;
					    u_xlat9 = u_xlat0.w + -1.0;
					    u_xlat0.w = u_xlat13 * u_xlat9 + 1.0;
					    u_xlat9 = vs_TANGENT0.z;
					    u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
					    u_xlat3.xyz = vec3(u_xlat9) * (-u_xlat0.xyz) + u_xlat0.xyz;
					    u_xlat3.w = 1.0;
					    u_xlat0 = (-vec4(u_xlat6)) * u_xlat3 + u_xlat0;
					    u_xlat3 = vec4(u_xlat6) * u_xlat3;
					    u_xlat9 = (-u_xlat2.x) + 1.0;
					    u_xlat9 = u_xlat5.x * u_xlat9 + u_xlat2.x;
					    u_xlat5.x = (-u_xlat5.x) * u_xlat2.x + u_xlat2.x;
					    u_xlat5.x = max(u_xlat5.x, 0.0);
					    u_xlat1.z = min(u_xlat9, 1.0);
					    u_xlat1.xz = (-u_xlat5.xx) + u_xlat1.xz;
					    u_xlat1.x = u_xlat1.x / u_xlat1.z;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat0 = u_xlat1.xxxx * u_xlat0 + u_xlat3;
					    u_xlat0 = u_xlat0 * vs_COLOR0;
					    u_xlat1.x = (-vs_TANGENT0.x) + 1.0;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = u_xlat10 * u_xlat1.x;
					    u_xlat5.x = max(abs(vs_NORMAL0.y), abs(vs_NORMAL0.x));
					    u_xlat5.x = float(1.0) / u_xlat5.x;
					    u_xlat9 = min(abs(vs_NORMAL0.y), abs(vs_NORMAL0.x));
					    u_xlat5.x = u_xlat5.x * u_xlat9;
					    u_xlat9 = u_xlat5.x * u_xlat5.x;
					    u_xlat13 = u_xlat9 * 0.0208350997 + -0.0851330012;
					    u_xlat13 = u_xlat9 * u_xlat13 + 0.180141002;
					    u_xlat13 = u_xlat9 * u_xlat13 + -0.330299497;
					    u_xlat9 = u_xlat9 * u_xlat13 + 0.999866009;
					    u_xlat13 = u_xlat9 * u_xlat5.x;
					    u_xlat13 = u_xlat13 * -2.0 + 1.57079637;
					    u_xlatb2 = abs(vs_NORMAL0.y)<abs(vs_NORMAL0.x);
					    u_xlat13 = u_xlatb2 ? u_xlat13 : float(0.0);
					    u_xlat5.x = u_xlat5.x * u_xlat9 + u_xlat13;
					    u_xlatb9 = (-vs_NORMAL0.y)<vs_NORMAL0.y;
					    u_xlat9 = u_xlatb9 ? -3.14159274 : float(0.0);
					    u_xlat5.x = u_xlat9 + u_xlat5.x;
					    u_xlat9 = min((-vs_NORMAL0.y), (-vs_NORMAL0.x));
					    u_xlatb9 = u_xlat9<(-u_xlat9);
					    u_xlat13 = max((-vs_NORMAL0.y), (-vs_NORMAL0.x));
					    u_xlatb13 = u_xlat13>=(-u_xlat13);
					    u_xlatb9 = u_xlatb13 && u_xlatb9;
					    u_xlat5.x = (u_xlatb9) ? (-u_xlat5.x) : u_xlat5.x;
					    u_xlat5.x = u_xlat5.x * 0.158999994 + 0.5;
					    u_xlatb5 = u_xlat5.x>=vs_TANGENT0.y;
					    u_xlat5.x = u_xlatb5 ? -0.699999988 : float(0.0);
					    u_xlat1.x = u_xlat1.x * u_xlat5.x + 1.0;
					    u_xlat5.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    u_xlat5.x = dot(u_xlat5.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat5.x = u_xlat5.x * 0.800000012;
					    u_xlat0.xyz = u_xlat0.xyz * u_xlat1.xxx + (-u_xlat5.xxx);
					    u_xlat0.xyz = vs_TANGENT0.xxx * u_xlat0.xyz + u_xlat5.xxx;
					    SV_Target0.xyz = u_xlat0.www * u_xlat0.xyz;
					    SV_Target0.w = u_xlat0.w;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_LOWEND_ON" }
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
					uniform  sampler2D _DecorTex;
					uniform  sampler2D _MaskTex;
					in  vec4 vs_COLOR0;
					in  vec4 vs_TANGENT0;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					float u_xlat4;
					float u_xlat8;
					bool u_xlatb8;
					float u_xlat12;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD0.xy * vec2(0.5, 0.5) + vec2(-0.5, -0.5);
					    u_xlat0.xy = fract(u_xlat0.xy);
					    u_xlat0.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
					    u_xlat0.xy = abs(u_xlat0.xy);
					    u_xlat0 = texture(_DecorTex, u_xlat0.xy);
					    u_xlatb1 = vs_TEXCOORD0.x>=-0.5;
					    u_xlat1.x = u_xlatb1 ? 1.0 : float(0.0);
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy, -1.0);
					    u_xlat2 = u_xlat2 + vec4(-1.0, -1.0, -1.0, -1.0);
					    u_xlat1 = u_xlat1.xxxx * u_xlat2 + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat2 = u_xlat0 + (-u_xlat1);
					    u_xlatb0 = vs_TEXCOORD2.x>=0.5;
					    u_xlat0.x = u_xlatb0 ? 1.0 : float(0.0);
					    u_xlat1 = u_xlat0.xxxx * u_xlat2 + u_xlat1;
					    u_xlat4 = u_xlat1.w + -1.0;
					    u_xlatb8 = -1.5>=vs_TEXCOORD1.x;
					    u_xlat8 = u_xlatb8 ? 1.0 : float(0.0);
					    u_xlat1.w = u_xlat8 * u_xlat4 + 1.0;
					    u_xlat4 = vs_TANGENT0.z;
					    u_xlat4 = clamp(u_xlat4, 0.0, 1.0);
					    u_xlat2.xyz = vec3(u_xlat4) * (-u_xlat1.xyz) + u_xlat1.xyz;
					    u_xlat3 = texture(_MaskTex, vs_TEXCOORD1.xy);
					    u_xlat4 = u_xlat0.w + (-u_xlat3.y);
					    u_xlat0.x = u_xlat0.x * u_xlat4 + u_xlat3.y;
					    u_xlat4 = (-u_xlat0.x) + 1.0;
					    u_xlat0.x = u_xlat8 * u_xlat4 + u_xlat0.x;
					    u_xlat2.w = 1.0;
					    u_xlat1 = (-u_xlat0.xxxx) * u_xlat2 + u_xlat1;
					    u_xlat2 = u_xlat0.xxxx * u_xlat2;
					    u_xlat0 = u_xlat0.xxxx * u_xlat1 + u_xlat2;
					    u_xlat1 = u_xlat0 * vs_COLOR0;
					    u_xlat12 = dot(u_xlat1.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat12 = u_xlat12 * 0.800000012;
					    u_xlat0.xyz = u_xlat0.xyz * vs_COLOR0.xyz + (-vec3(u_xlat12));
					    u_xlat0.xyz = vs_TANGENT0.xxx * u_xlat0.xyz + vec3(u_xlat12);
					    SV_Target0.xyz = u_xlat1.www * u_xlat0.xyz;
					    SV_Target0.w = u_xlat1.w;
					    return;
					}"
				}
			}
		}
	}
}