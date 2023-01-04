Shader "Blit/AgentBlit" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader {
		Pass {
			ZTest Always
			ZWrite Off
			Cull Off
			GpuProgramID 35097
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
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec2 u_xlat1;
					vec3 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat6;
					vec2 u_xlat9;
					float u_xlat12;
					float u_xlat13;
					float u_xlat14;
					bool u_xlatb14;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xy = dFdx(vs_TEXCOORD0.xy);
					    u_xlat9.xy = dFdy(vs_TEXCOORD0.xy);
					    u_xlat1.xy = abs(u_xlat9.xy) + abs(u_xlat1.xy);
					    u_xlat9.x = 0.0;
					    u_xlat13 = u_xlat0.w;
					    u_xlat2.x = float(2.0);
					    u_xlat2.y = float(0.0);
					    u_xlat2.z = float(0.0);
					    while(true){
					        u_xlatb14 = floatBitsToInt(u_xlat2.z)>=4;
					        if(u_xlatb14){break;}
					        u_xlat3.xy = u_xlat2.xy * u_xlat1.xy + vs_TEXCOORD0.xy;
					        u_xlat3 = texture(_MainTex, u_xlat3.xy);
					        u_xlat3.xyz = (-u_xlat0.xyz) + u_xlat3.xyz;
					        u_xlat3.xyz = clamp(u_xlat3.xyz, 0.0, 1.0);
					        u_xlat14 = u_xlat9.x + u_xlat3.x;
					        u_xlat14 = u_xlat3.y + u_xlat14;
					        u_xlat9.x = u_xlat3.z + u_xlat14;
					        u_xlat13 = max(u_xlat13, u_xlat3.w);
					        u_xlat6.y = intBitsToFloat(floatBitsToInt(u_xlat2.z) + 1);
					        u_xlat6.z = u_xlat2.y;
					        u_xlat6.x = (-u_xlat2.x);
					        u_xlat2.xyz = u_xlat6.zxy;
					    }
					    SV_Target0.w = u_xlat13;
					    u_xlat12 = u_xlat9.x * 1.60000002 + 1.0;
					    u_xlat0.xyz = log2(u_xlat0.xyz);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(u_xlat12);
					    u_xlat0.xyz = exp2(u_xlat0.xyz);
					    SV_Target0.xyz = u_xlat0.xyz * vec3(1.20000005, 1.20000005, 1.20000005);
					    return;
					}"
				}
			}
		}
	}
}