Shader "Campaign/CampaignFog" {
	Properties {
		_MainTex ("Main Texture", 2D) = "white" {}
		_FogOffset ("Fog Offset", Float) = 1
		_Color ("Color", Vector) = (0.5,0.5,0.5,1)
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
			ZTest Less
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
			GpuProgramID 5400
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
						vec4 _FrontierParams;
						vec4 unused_0_2[2];
						float _CloudOffset;
						vec4 unused_0_4[64];
						vec4 _Color;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
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
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_TEXCOORD3;
					out vec4 vs_TEXCOORD4;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * _Color;
					    vs_TEXCOORD2.xy = u_xlat0.xy;
					    u_xlat0 = u_xlat0.yxyx + vec4(-0.699999988, -0.300000012, 0.699999988, 0.300000012);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat1 = u_xlat0.yxwz * _FrontierParams.zwzw + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_TEXCOORD3 = u_xlat1 * vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat1.xy = u_xlat0.yw + vec2(_CloudOffset);
					    u_xlat0.yw = (-_Time.yy) * vec2(0.300000012, 0.300000012) + u_xlat1.xy;
					    u_xlat1 = u_xlat0 * vec4(-0.00400000019, 0.00400000019, -0.00400000019, 0.00400000019);
					    vs_TEXCOORD4 = u_xlat0.yxwz * vec4(0.0199999996, 0.0199999996, 0.0199999996, 0.0199999996) + u_xlat1;
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
						vec4 _FrontierParams;
						vec4 unused_0_2[2];
						float _CloudOffset;
						vec4 unused_0_4[64];
						vec4 _Color;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
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
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_TEXCOORD3;
					out vec4 vs_TEXCOORD4;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * _Color;
					    vs_TEXCOORD2.xy = u_xlat0.xy;
					    u_xlat0 = u_xlat0.yxyx + vec4(-0.699999988, -0.300000012, 0.699999988, 0.300000012);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat1 = u_xlat0.yxwz * _FrontierParams.zwzw + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_TEXCOORD3 = u_xlat1 * vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat1.xy = u_xlat0.yw + vec2(_CloudOffset);
					    u_xlat0.yw = (-_Time.yy) * vec2(0.300000012, 0.300000012) + u_xlat1.xy;
					    u_xlat1 = u_xlat0 * vec4(-0.00400000019, 0.00400000019, -0.00400000019, 0.00400000019);
					    vs_TEXCOORD4 = u_xlat0.yxwz * vec4(0.0199999996, 0.0199999996, 0.0199999996, 0.0199999996) + u_xlat1;
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
					layout(std140) uniform VGlobals {
						vec4 unused_0_0[2];
						vec4 _FrontierParams;
						vec4 unused_0_2[2];
						float _CloudOffset;
						vec4 unused_0_4[64];
						vec4 _Color;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
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
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_TEXCOORD3;
					out vec4 vs_TEXCOORD4;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * _Color;
					    vs_TEXCOORD2.xy = u_xlat0.xy;
					    u_xlat0 = u_xlat0.yxyx + vec4(-0.699999988, -0.300000012, 0.699999988, 0.300000012);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat1 = u_xlat0.yxwz * _FrontierParams.zwzw + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_TEXCOORD3 = u_xlat1 * vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat1.xy = u_xlat0.yw + vec2(_CloudOffset);
					    u_xlat0.yw = (-_Time.yy) * vec2(0.300000012, 0.300000012) + u_xlat1.xy;
					    u_xlat1 = u_xlat0 * vec4(-0.00400000019, 0.00400000019, -0.00400000019, 0.00400000019);
					    vs_TEXCOORD4 = u_xlat0.yxwz * vec4(0.0199999996, 0.0199999996, 0.0199999996, 0.0199999996) + u_xlat1;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "UNITY_UI_ALPHACLIP" }
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
						vec4 _FrontierParams;
						vec4 unused_0_2[2];
						float _CloudOffset;
						vec4 unused_0_4[64];
						vec4 _Color;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
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
					in  vec4 in_POSITION0;
					in  vec4 in_COLOR0;
					in  vec2 in_TEXCOORD0;
					out vec4 vs_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD2;
					out vec4 vs_TEXCOORD3;
					out vec4 vs_TEXCOORD4;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat1 + unity_MatrixVP[3];
					    vs_COLOR0 = in_COLOR0 * _Color;
					    vs_TEXCOORD2.xy = u_xlat0.xy;
					    u_xlat0 = u_xlat0.yxyx + vec4(-0.699999988, -0.300000012, 0.699999988, 0.300000012);
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat1 = u_xlat0.yxwz * _FrontierParams.zwzw + vec4(1.0, 1.0, 1.0, 1.0);
					    vs_TEXCOORD3 = u_xlat1 * vec4(0.5, 0.5, 0.5, 0.5);
					    u_xlat1.xy = u_xlat0.yw + vec2(_CloudOffset);
					    u_xlat0.yw = (-_Time.yy) * vec2(0.300000012, 0.300000012) + u_xlat1.xy;
					    u_xlat1 = u_xlat0 * vec4(-0.00400000019, 0.00400000019, -0.00400000019, 0.00400000019);
					    vs_TEXCOORD4 = u_xlat0.yxwz * vec4(0.0199999996, 0.0199999996, 0.0199999996, 0.0199999996) + u_xlat1;
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
						vec4 unused_0_0[4];
						vec4 _FrontierTex_TexelSize;
						vec4 unused_0_2[55];
						float _LineWidth;
						vec4 unused_0_4[9];
						vec4 _Color;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _FrontierTex;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_TEXCOORD3;
					in  vec4 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					float u_xlat5;
					float u_xlat6;
					vec2 u_xlat10;
					float u_xlat11;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD4.y + _Time.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat5 = u_xlat0.x + 1.0;
					    u_xlat0.x = abs(u_xlat0.x) * 2.0 + 2.0;
					    u_xlat5 = u_xlat5 * 0.5;
					    u_xlat1 = textureLod(_MainTex, vs_TEXCOORD4.zw, u_xlat0.x);
					    u_xlat2 = textureLod(_MainTex, vs_TEXCOORD4.xy, u_xlat0.x);
					    u_xlat0.x = (-u_xlat1.x) + u_xlat1.y;
					    u_xlat1.x = u_xlat5 * u_xlat0.x + u_xlat1.x;
					    u_xlat3 = (-_FrontierTex_TexelSize.xyxy) + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat4 = max(vs_TEXCOORD3, _FrontierTex_TexelSize.xyxy);
					    u_xlat3 = min(u_xlat3, u_xlat4);
					    u_xlat4 = texture(_FrontierTex, u_xlat3.xy).yxzw;
					    u_xlat3 = texture(_FrontierTex, u_xlat3.zw);
					    u_xlat4.x = u_xlat3.x;
					    u_xlat0.x = (-u_xlat2.x) + u_xlat2.y;
					    u_xlat1.y = u_xlat5 * u_xlat0.x + u_xlat2.x;
					    u_xlat0.xy = u_xlat4.xy * vec2(2.0, 2.0) + u_xlat1.xy;
					    u_xlat0.xy = u_xlat0.xy + vec2(-1.0, -1.0);
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat10.xy = dFdx(u_xlat0.xy);
					    u_xlat1.xz = dFdy(u_xlat0.xy);
					    u_xlat10.xy = abs(u_xlat10.xy) + abs(u_xlat1.xz);
					    u_xlat1.xz = u_xlat0.xy + vec2(-0.5, -0.5);
					    u_xlat0.xz = u_xlat1.xz / u_xlat10.xy;
					    u_xlat15 = u_xlat10.y * 8.0;
					    u_xlat1.xz = u_xlat0.zx + vec2(3.0, 0.5);
					    u_xlat1.x = max(u_xlat1.z, u_xlat1.x);
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = u_xlat0.y * 8.0 + u_xlat1.y;
					    u_xlat5 = (-u_xlat0.y) * 2.0 + 2.0;
					    u_xlat5 = min(u_xlat5, 1.0);
					    u_xlat1.x = fract(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + -0.5;
					    u_xlat15 = abs(u_xlat1.x) / u_xlat15;
					    u_xlat1.x = _LineWidth + -1.0;
					    u_xlat15 = (-u_xlat1.x) * 0.5 + u_xlat15;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat6 = u_xlat5 * u_xlat5;
					    u_xlat6 = dot(vec2(u_xlat6), vec2(u_xlat5));
					    u_xlat5 = u_xlat5 + u_xlat5;
					    u_xlat5 = min(u_xlat5, 1.0);
					    u_xlat6 = u_xlat6 + vs_TEXCOORD2.y;
					    u_xlat11 = dFdx(u_xlat6);
					    u_xlat16 = dFdy(u_xlat6);
					    u_xlat6 = fract(u_xlat6);
					    u_xlat6 = u_xlat6 + -0.5;
					    u_xlat11 = abs(u_xlat16) + abs(u_xlat11);
					    u_xlat6 = abs(u_xlat6) / u_xlat11;
					    u_xlat6 = (-u_xlat1.x) * 0.5 + u_xlat6;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = (-u_xlat6) + 1.0;
					    u_xlat15 = u_xlat15 * u_xlat6;
					    u_xlat15 = u_xlat15 * 0.5;
					    u_xlat1.y = (-u_xlat1.x) * 0.5 + (-u_xlat0.z);
					    u_xlat1.x = (-u_xlat1.x) * 0.5 + u_xlat0.z;
					    u_xlat0.xz = u_xlat0.xz;
					    u_xlat0.xz = clamp(u_xlat0.xz, 0.0, 1.0);
					    u_xlat1.xy = u_xlat1.xy + vec2(-0.300000012, -0.300000012);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * 0.119999997 + -1.0;
					    u_xlat0.x = u_xlat1.y * u_xlat0.x + 1.0;
					    u_xlat5 = u_xlat15 * u_xlat5 + (-u_xlat0.x);
					    u_xlat0.x = u_xlat1.x * u_xlat5 + u_xlat0.x;
					    u_xlat1 = u_xlat0.zzzz * _Color;
					    u_xlat2 = (-u_xlat0.zzzz) * _Color + vec4(0.0, 0.0, 0.0, 1.0);
					    SV_Target0 = u_xlat0.xxxx * u_xlat2 + u_xlat1;
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
						vec4 unused_0_0[4];
						vec4 _FrontierTex_TexelSize;
						vec4 unused_0_2[55];
						float _LineWidth;
						vec4 unused_0_4[9];
						vec4 _Color;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _FrontierTex;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_TEXCOORD3;
					in  vec4 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					float u_xlat5;
					float u_xlat6;
					vec2 u_xlat10;
					float u_xlat11;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD4.y + _Time.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat5 = u_xlat0.x + 1.0;
					    u_xlat0.x = abs(u_xlat0.x) * 2.0 + 2.0;
					    u_xlat5 = u_xlat5 * 0.5;
					    u_xlat1 = textureLod(_MainTex, vs_TEXCOORD4.zw, u_xlat0.x);
					    u_xlat2 = textureLod(_MainTex, vs_TEXCOORD4.xy, u_xlat0.x);
					    u_xlat0.x = (-u_xlat1.x) + u_xlat1.y;
					    u_xlat1.x = u_xlat5 * u_xlat0.x + u_xlat1.x;
					    u_xlat3 = (-_FrontierTex_TexelSize.xyxy) + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat4 = max(vs_TEXCOORD3, _FrontierTex_TexelSize.xyxy);
					    u_xlat3 = min(u_xlat3, u_xlat4);
					    u_xlat4 = texture(_FrontierTex, u_xlat3.xy).yxzw;
					    u_xlat3 = texture(_FrontierTex, u_xlat3.zw);
					    u_xlat4.x = u_xlat3.x;
					    u_xlat0.x = (-u_xlat2.x) + u_xlat2.y;
					    u_xlat1.y = u_xlat5 * u_xlat0.x + u_xlat2.x;
					    u_xlat0.xy = u_xlat4.xy * vec2(2.0, 2.0) + u_xlat1.xy;
					    u_xlat0.xy = u_xlat0.xy + vec2(-1.0, -1.0);
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat10.xy = dFdx(u_xlat0.xy);
					    u_xlat1.xz = dFdy(u_xlat0.xy);
					    u_xlat10.xy = abs(u_xlat10.xy) + abs(u_xlat1.xz);
					    u_xlat1.xz = u_xlat0.xy + vec2(-0.5, -0.5);
					    u_xlat0.xz = u_xlat1.xz / u_xlat10.xy;
					    u_xlat15 = u_xlat10.y * 8.0;
					    u_xlat1.xz = u_xlat0.zx + vec2(3.0, 0.5);
					    u_xlat1.x = max(u_xlat1.z, u_xlat1.x);
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = u_xlat0.y * 8.0 + u_xlat1.y;
					    u_xlat5 = (-u_xlat0.y) * 2.0 + 2.0;
					    u_xlat5 = min(u_xlat5, 1.0);
					    u_xlat1.x = fract(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + -0.5;
					    u_xlat15 = abs(u_xlat1.x) / u_xlat15;
					    u_xlat1.x = _LineWidth + -1.0;
					    u_xlat15 = (-u_xlat1.x) * 0.5 + u_xlat15;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat6 = u_xlat5 * u_xlat5;
					    u_xlat6 = dot(vec2(u_xlat6), vec2(u_xlat5));
					    u_xlat5 = u_xlat5 + u_xlat5;
					    u_xlat5 = min(u_xlat5, 1.0);
					    u_xlat6 = u_xlat6 + vs_TEXCOORD2.y;
					    u_xlat11 = dFdx(u_xlat6);
					    u_xlat16 = dFdy(u_xlat6);
					    u_xlat6 = fract(u_xlat6);
					    u_xlat6 = u_xlat6 + -0.5;
					    u_xlat11 = abs(u_xlat16) + abs(u_xlat11);
					    u_xlat6 = abs(u_xlat6) / u_xlat11;
					    u_xlat6 = (-u_xlat1.x) * 0.5 + u_xlat6;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = (-u_xlat6) + 1.0;
					    u_xlat15 = u_xlat15 * u_xlat6;
					    u_xlat15 = u_xlat15 * 0.5;
					    u_xlat1.y = (-u_xlat1.x) * 0.5 + (-u_xlat0.z);
					    u_xlat1.x = (-u_xlat1.x) * 0.5 + u_xlat0.z;
					    u_xlat0.xz = u_xlat0.xz;
					    u_xlat0.xz = clamp(u_xlat0.xz, 0.0, 1.0);
					    u_xlat1.xy = u_xlat1.xy + vec2(-0.300000012, -0.300000012);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * 0.119999997 + -1.0;
					    u_xlat0.x = u_xlat1.y * u_xlat0.x + 1.0;
					    u_xlat5 = u_xlat15 * u_xlat5 + (-u_xlat0.x);
					    u_xlat0.x = u_xlat1.x * u_xlat5 + u_xlat0.x;
					    u_xlat1 = u_xlat0.zzzz * _Color;
					    u_xlat2 = (-u_xlat0.zzzz) * _Color + vec4(0.0, 0.0, 0.0, 1.0);
					    SV_Target0 = u_xlat0.xxxx * u_xlat2 + u_xlat1;
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
						vec4 unused_0_0[4];
						vec4 _FrontierTex_TexelSize;
						vec4 unused_0_2[55];
						float _LineWidth;
						vec4 unused_0_4[9];
						vec4 _Color;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _FrontierTex;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_TEXCOORD3;
					in  vec4 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					float u_xlat5;
					float u_xlat6;
					vec2 u_xlat10;
					float u_xlat11;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD4.y + _Time.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat5 = u_xlat0.x + 1.0;
					    u_xlat0.x = abs(u_xlat0.x) * 2.0 + 2.0;
					    u_xlat5 = u_xlat5 * 0.5;
					    u_xlat1 = textureLod(_MainTex, vs_TEXCOORD4.zw, u_xlat0.x);
					    u_xlat2 = textureLod(_MainTex, vs_TEXCOORD4.xy, u_xlat0.x);
					    u_xlat0.x = (-u_xlat1.x) + u_xlat1.y;
					    u_xlat1.x = u_xlat5 * u_xlat0.x + u_xlat1.x;
					    u_xlat3 = (-_FrontierTex_TexelSize.xyxy) + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat4 = max(vs_TEXCOORD3, _FrontierTex_TexelSize.xyxy);
					    u_xlat3 = min(u_xlat3, u_xlat4);
					    u_xlat4 = texture(_FrontierTex, u_xlat3.xy).yxzw;
					    u_xlat3 = texture(_FrontierTex, u_xlat3.zw);
					    u_xlat4.x = u_xlat3.x;
					    u_xlat0.x = (-u_xlat2.x) + u_xlat2.y;
					    u_xlat1.y = u_xlat5 * u_xlat0.x + u_xlat2.x;
					    u_xlat0.xy = u_xlat4.xy * vec2(2.0, 2.0) + u_xlat1.xy;
					    u_xlat0.xy = u_xlat0.xy + vec2(-1.0, -1.0);
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat10.xy = dFdx(u_xlat0.xy);
					    u_xlat1.xz = dFdy(u_xlat0.xy);
					    u_xlat10.xy = abs(u_xlat10.xy) + abs(u_xlat1.xz);
					    u_xlat1.xz = u_xlat0.xy + vec2(-0.5, -0.5);
					    u_xlat0.xz = u_xlat1.xz / u_xlat10.xy;
					    u_xlat15 = u_xlat10.y * 8.0;
					    u_xlat1.xz = u_xlat0.zx + vec2(3.0, 0.5);
					    u_xlat1.x = max(u_xlat1.z, u_xlat1.x);
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = u_xlat0.y * 8.0 + u_xlat1.y;
					    u_xlat5 = (-u_xlat0.y) * 2.0 + 2.0;
					    u_xlat5 = min(u_xlat5, 1.0);
					    u_xlat1.x = fract(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + -0.5;
					    u_xlat15 = abs(u_xlat1.x) / u_xlat15;
					    u_xlat1.x = _LineWidth + -1.0;
					    u_xlat15 = (-u_xlat1.x) * 0.5 + u_xlat15;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat6 = u_xlat5 * u_xlat5;
					    u_xlat6 = dot(vec2(u_xlat6), vec2(u_xlat5));
					    u_xlat5 = u_xlat5 + u_xlat5;
					    u_xlat5 = min(u_xlat5, 1.0);
					    u_xlat6 = u_xlat6 + vs_TEXCOORD2.y;
					    u_xlat11 = dFdx(u_xlat6);
					    u_xlat16 = dFdy(u_xlat6);
					    u_xlat6 = fract(u_xlat6);
					    u_xlat6 = u_xlat6 + -0.5;
					    u_xlat11 = abs(u_xlat16) + abs(u_xlat11);
					    u_xlat6 = abs(u_xlat6) / u_xlat11;
					    u_xlat6 = (-u_xlat1.x) * 0.5 + u_xlat6;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = (-u_xlat6) + 1.0;
					    u_xlat15 = u_xlat15 * u_xlat6;
					    u_xlat15 = u_xlat15 * 0.5;
					    u_xlat1.y = (-u_xlat1.x) * 0.5 + (-u_xlat0.z);
					    u_xlat1.x = (-u_xlat1.x) * 0.5 + u_xlat0.z;
					    u_xlat0.xz = u_xlat0.xz;
					    u_xlat0.xz = clamp(u_xlat0.xz, 0.0, 1.0);
					    u_xlat1.xy = u_xlat1.xy + vec2(-0.300000012, -0.300000012);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * 0.119999997 + -1.0;
					    u_xlat0.x = u_xlat1.y * u_xlat0.x + 1.0;
					    u_xlat5 = u_xlat15 * u_xlat5 + (-u_xlat0.x);
					    u_xlat0.x = u_xlat1.x * u_xlat5 + u_xlat0.x;
					    u_xlat1 = u_xlat0.zzzz * _Color;
					    u_xlat2 = (-u_xlat0.zzzz) * _Color + vec4(0.0, 0.0, 0.0, 1.0);
					    SV_Target0 = u_xlat0.xxxx * u_xlat2 + u_xlat1;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_GAME_ON" "UNITY_UI_ALPHACLIP" }
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
						vec4 unused_0_0[4];
						vec4 _FrontierTex_TexelSize;
						vec4 unused_0_2[55];
						float _LineWidth;
						vec4 unused_0_4[9];
						vec4 _Color;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _FrontierTex;
					in  vec2 vs_TEXCOORD2;
					in  vec4 vs_TEXCOORD3;
					in  vec4 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					float u_xlat5;
					float u_xlat6;
					vec2 u_xlat10;
					float u_xlat11;
					float u_xlat15;
					float u_xlat16;
					void main()
					{
					    u_xlat0.x = vs_TEXCOORD4.y + _Time.x;
					    u_xlat0.x = sin(u_xlat0.x);
					    u_xlat5 = u_xlat0.x + 1.0;
					    u_xlat0.x = abs(u_xlat0.x) * 2.0 + 2.0;
					    u_xlat5 = u_xlat5 * 0.5;
					    u_xlat1 = textureLod(_MainTex, vs_TEXCOORD4.zw, u_xlat0.x);
					    u_xlat2 = textureLod(_MainTex, vs_TEXCOORD4.xy, u_xlat0.x);
					    u_xlat0.x = (-u_xlat1.x) + u_xlat1.y;
					    u_xlat1.x = u_xlat5 * u_xlat0.x + u_xlat1.x;
					    u_xlat3 = (-_FrontierTex_TexelSize.xyxy) + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat4 = max(vs_TEXCOORD3, _FrontierTex_TexelSize.xyxy);
					    u_xlat3 = min(u_xlat3, u_xlat4);
					    u_xlat4 = texture(_FrontierTex, u_xlat3.xy).yxzw;
					    u_xlat3 = texture(_FrontierTex, u_xlat3.zw);
					    u_xlat4.x = u_xlat3.x;
					    u_xlat0.x = (-u_xlat2.x) + u_xlat2.y;
					    u_xlat1.y = u_xlat5 * u_xlat0.x + u_xlat2.x;
					    u_xlat0.xy = u_xlat4.xy * vec2(2.0, 2.0) + u_xlat1.xy;
					    u_xlat0.xy = u_xlat0.xy + vec2(-1.0, -1.0);
					    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
					    u_xlat10.xy = dFdx(u_xlat0.xy);
					    u_xlat1.xz = dFdy(u_xlat0.xy);
					    u_xlat10.xy = abs(u_xlat10.xy) + abs(u_xlat1.xz);
					    u_xlat1.xz = u_xlat0.xy + vec2(-0.5, -0.5);
					    u_xlat0.xz = u_xlat1.xz / u_xlat10.xy;
					    u_xlat15 = u_xlat10.y * 8.0;
					    u_xlat1.xz = u_xlat0.zx + vec2(3.0, 0.5);
					    u_xlat1.x = max(u_xlat1.z, u_xlat1.x);
					    u_xlatb1 = u_xlat1.x<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    u_xlat1.x = u_xlat0.y * 8.0 + u_xlat1.y;
					    u_xlat5 = (-u_xlat0.y) * 2.0 + 2.0;
					    u_xlat5 = min(u_xlat5, 1.0);
					    u_xlat1.x = fract(u_xlat1.x);
					    u_xlat1.x = u_xlat1.x + -0.5;
					    u_xlat15 = abs(u_xlat1.x) / u_xlat15;
					    u_xlat1.x = _LineWidth + -1.0;
					    u_xlat15 = (-u_xlat1.x) * 0.5 + u_xlat15;
					    u_xlat15 = clamp(u_xlat15, 0.0, 1.0);
					    u_xlat6 = u_xlat5 * u_xlat5;
					    u_xlat6 = dot(vec2(u_xlat6), vec2(u_xlat5));
					    u_xlat5 = u_xlat5 + u_xlat5;
					    u_xlat5 = min(u_xlat5, 1.0);
					    u_xlat6 = u_xlat6 + vs_TEXCOORD2.y;
					    u_xlat11 = dFdx(u_xlat6);
					    u_xlat16 = dFdy(u_xlat6);
					    u_xlat6 = fract(u_xlat6);
					    u_xlat6 = u_xlat6 + -0.5;
					    u_xlat11 = abs(u_xlat16) + abs(u_xlat11);
					    u_xlat6 = abs(u_xlat6) / u_xlat11;
					    u_xlat6 = (-u_xlat1.x) * 0.5 + u_xlat6;
					    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
					    u_xlat6 = (-u_xlat6) + 1.0;
					    u_xlat15 = u_xlat15 * u_xlat6;
					    u_xlat15 = u_xlat15 * 0.5;
					    u_xlat1.y = (-u_xlat1.x) * 0.5 + (-u_xlat0.z);
					    u_xlat1.x = (-u_xlat1.x) * 0.5 + u_xlat0.z;
					    u_xlat0.xz = u_xlat0.xz;
					    u_xlat0.xz = clamp(u_xlat0.xz, 0.0, 1.0);
					    u_xlat1.xy = u_xlat1.xy + vec2(-0.300000012, -0.300000012);
					    u_xlat1.xy = clamp(u_xlat1.xy, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * 0.119999997 + -1.0;
					    u_xlat0.x = u_xlat1.y * u_xlat0.x + 1.0;
					    u_xlat5 = u_xlat15 * u_xlat5 + (-u_xlat0.x);
					    u_xlat0.x = u_xlat1.x * u_xlat5 + u_xlat0.x;
					    u_xlat1 = u_xlat0.zzzz * _Color;
					    u_xlat2 = (-u_xlat0.zzzz) * _Color + vec4(0.0, 0.0, 0.0, 1.0);
					    SV_Target0 = u_xlat0.xxxx * u_xlat2 + u_xlat1;
					    return;
					}"
				}
			}
		}
	}
}