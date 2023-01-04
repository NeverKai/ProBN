Shader "Ui/DistanceField" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (1,1,1,1)
	}
	SubShader {
		LOD 100
		Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "RenderType" = "Opaque" }
		Pass {
			LOD 100
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "RenderType" = "Opaque" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			Cull Off
			GpuProgramID 45352
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
					in  vec4 in_COLOR0;
					out vec2 vs_TEXCOORD0;
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
						vec4 unused_0_2[9];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec2 u_xlat3;
					vec4 u_xlat4;
					float u_xlat16;
					bool u_xlatb16;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat0 + vec4(-0.5, -0.5, -0.5, -0.100000001);
					    u_xlatb16 = u_xlat1.w<0.0;
					    if(((int(u_xlatb16) * int(0xffffffffu)))!=0){discard;}
					    u_xlat16 = vs_COLOR0.w + -0.00999999978;
					    u_xlatb16 = u_xlat16<0.0;
					    if(((int(u_xlatb16) * int(0xffffffffu)))!=0){discard;}
					    u_xlat2.xyz = dFdx(u_xlat0.xyz);
					    u_xlat3.x = u_xlat2.z;
					    u_xlat4.xyz = dFdy(u_xlat0.xyz);
					    u_xlat3.y = u_xlat4.z;
					    u_xlat0.x = dot(u_xlat3.xy, u_xlat3.xy);
					    u_xlat0.z = sqrt(u_xlat0.x);
					    u_xlat2.w = u_xlat4.x;
					    u_xlat16 = dot(u_xlat2.xw, u_xlat2.xw);
					    u_xlat4.w = u_xlat2.y;
					    u_xlat2.x = dot(u_xlat4.yw, u_xlat4.yw);
					    u_xlat0.y = sqrt(u_xlat2.x);
					    u_xlat0.x = sqrt(u_xlat16);
					    u_xlat0.xyz = u_xlat1.xyz / u_xlat0.xyz;
					    u_xlat0.xyz = vec3(vec3(_LineWidth, _LineWidth, _LineWidth)) * vec3(0.5, 0.5, 0.5) + u_xlat0.xyz;
					    u_xlat0.xyz = clamp(u_xlat0.xyz, 0.0, 1.0);
					    u_xlat0.x = min(u_xlat0.y, u_xlat0.x);
					    u_xlat0.x = min(u_xlat0.z, u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * vs_COLOR0.w;
					    SV_Target0.w = u_xlat0.w * u_xlat0.x;
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    return;
					}"
				}
			}
		}
	}
}