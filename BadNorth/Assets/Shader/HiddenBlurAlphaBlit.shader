Shader "Hidden/BlurAlphaBlit" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader {
		Pass {
			ZTest Always
			ZWrite Off
			Cull Off
			GpuProgramID 34699
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
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
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
						vec4 unused_0_0[64];
						vec4 _MainTex_TexelSize;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec2 u_xlat2;
					vec2 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					bool u_xlatb5;
					vec4 u_xlat6;
					vec2 u_xlat7;
					vec2 u_xlat8;
					bool u_xlatb12;
					int u_xlati20;
					vec2 u_xlat21;
					float u_xlat25;
					bool u_xlatb27;
					float u_xlat28;
					int u_xlati29;
					bool u_xlatb29;
					bool u_xlatb33;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xyz = u_xlat0.xyz;
					    u_xlat1.w = 6.0;
					    u_xlat2.x = float(6.0);
					    u_xlat2.y = float(6.0);
					    for(int u_xlati_loop_1 = int(int(0xFFFFFFFBu)) ; u_xlati_loop_1<=5 ; u_xlati_loop_1++)
					    {
					        u_xlat3.x = float(u_xlati_loop_1);
					        u_xlat4 = u_xlat1;
					        u_xlat21.xy = u_xlat2.xy;
					        for(int u_xlati_loop_2 = int(0xFFFFFFFBu) ; u_xlati_loop_2<=5 ; u_xlati_loop_2++)
					        {
					            u_xlat3.y = float(u_xlati_loop_2);
					            u_xlat5.x = dot(u_xlat3.xy, u_xlat3.xy);
					            u_xlat5.w = sqrt(u_xlat5.x);
					            u_xlat6.xy = u_xlat3.xy * _MainTex_TexelSize.xy + vs_TEXCOORD0.xy;
					            u_xlat6 = texture(_MainTex, u_xlat6.xy);
					            u_xlatb12 = u_xlat6.w>=0.5;
					            u_xlat7.x = u_xlat5.w + (-u_xlat6.w);
					            u_xlat7.x = min(u_xlat21.x, u_xlat7.x);
					            u_xlat25 = u_xlat5.w + u_xlat6.w;
					            u_xlat8.y = min(u_xlat21.y, u_xlat25);
					            u_xlat7.y = u_xlat21.y;
					            u_xlat8.x = u_xlat21.x;
					            u_xlat21.xy = (bool(u_xlatb12)) ? u_xlat7.xy : u_xlat8.xy;
					            u_xlatb12 = 0.100000001<u_xlat6.w;
					            u_xlatb33 = u_xlat5.w<u_xlat4.w;
					            u_xlatb12 = u_xlatb12 && u_xlatb33;
					            u_xlat5.xyz = u_xlat6.xyz;
					            u_xlat4 = (bool(u_xlatb12)) ? u_xlat5 : u_xlat4;
					        }
					        u_xlat1 = u_xlat4;
					        u_xlat2.xy = u_xlat21.xy;
					    }
					    u_xlat2.xy = u_xlat2.xy * vec2(-0.100000001, 0.100000001) + vec2(0.5, 0.5);
					    u_xlat28 = (-u_xlat2.x) + u_xlat2.y;
					    SV_Target0.w = u_xlat0.w * u_xlat28 + u_xlat2.x;
					    u_xlatb27 = u_xlat0.w<0.100000001;
					    SV_Target0.xyz = (bool(u_xlatb27)) ? u_xlat1.xyz : u_xlat0.xyz;
					    return;
					}"
				}
			}
		}
	}
}