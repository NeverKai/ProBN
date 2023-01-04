Shader "Unlit/Fog" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader {
		LOD 100
		Tags { "RenderType" = "Opaque" }
		Pass {
			LOD 100
			Tags { "RenderType" = "Opaque" }
			ZWrite Off
			Stencil {
				Ref 16
				Comp Always
				Pass Replace
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 47058
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
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_POSITION0;
					out vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					void main()
					{
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1.x = (-unity_MatrixV[0].z);
					    u_xlat1.y = (-unity_MatrixV[1].z);
					    u_xlat1.z = (-unity_MatrixV[2].z);
					    u_xlat1.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    vs_TEXCOORD0.xy = u_xlat0.xz;
					    u_xlat0 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
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
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_0_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_1_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_4[2];
					};
					in  vec4 in_POSITION0;
					out vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					float u_xlat6;
					void main()
					{
					    u_xlat0.xyz = in_POSITION0.yyy * unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = unity_ObjectToWorld[3].xyz * in_POSITION0.www + u_xlat0.xyz;
					    u_xlat1.x = (-unity_MatrixV[0].z);
					    u_xlat1.y = (-unity_MatrixV[1].z);
					    u_xlat1.z = (-unity_MatrixV[2].z);
					    u_xlat6 = dot(u_xlat1.xyz, u_xlat0.xyz);
					    u_xlat6 = u_xlat6 + -20.0;
					    u_xlat1.xyz = (-vec3(u_xlat6)) * u_xlat1.xyz + u_xlat0.xyz;
					    vs_TEXCOORD0.xy = u_xlat0.xz;
					    u_xlat0 = u_xlat1.yyyy * unity_MatrixVP[1];
					    u_xlat0 = unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat0;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
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
						vec4 unused_0_0[14];
						float _WindInterpolator;
						vec3 _WindDir;
						float _WindTime;
						vec4 unused_0_4[29];
						vec4 _MirrorColor;
						vec4 unused_0_6;
						vec4 _CloudColor;
						vec4 unused_0_8;
						vec4 _CloudCoverage;
						vec4 unused_0_10[7];
						vec4 _FogColor;
						vec4 unused_0_12;
						vec4 _FlashColor;
						vec4 unused_0_14[3];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[7];
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[10];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec2 u_xlat8;
					float u_xlat12;
					void main()
					{
					    u_xlat0.x = unity_OrthoParams.x + 2.0;
					    u_xlat0.xy = vs_TEXCOORD0.xy / u_xlat0.xx;
					    u_xlat8.xy = _WindDir.xz * vec2(_WindTime);
					    u_xlat0.xy = (-u_xlat8.xy) * vec2(0.00999999978, 0.00999999978) + u_xlat0.xy;
					    u_xlat1.x = unity_MatrixV[0].z;
					    u_xlat1.y = unity_MatrixV[2].z;
					    u_xlat0.xy = u_xlat0.xy * vec2(0.5, 0.5) + (-u_xlat1.xy);
					    u_xlat1.xy = (-u_xlat1.xy) * vec2(-0.200000003, -0.200000003) + vs_TEXCOORD0.xy;
					    u_xlat8.x = _Time.y * 0.200000003 + u_xlat0.x;
					    u_xlat2 = textureLod(_MainTex, u_xlat0.xy, 2.5);
					    u_xlat0.x = sin(u_xlat8.x);
					    u_xlat4.x = (-u_xlat2.x) + u_xlat2.y;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x + u_xlat2.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = _CloudCoverage.x * 2.0 + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x + -1.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat4.xyz = _FlashColor.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + _CloudColor.xyz;
					    u_xlat4.xyz = u_xlat4.xyz + (-_MirrorColor.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz + _MirrorColor.xyz;
					    u_xlat2.xyz = u_xlat0.xyz * vec3(-0.199999988, -0.199999988, -0.199999988);
					    u_xlat1.zw = vs_TEXCOORD0.xy;
					    u_xlat3 = u_xlat1 * _WindDir.xzxz;
					    u_xlat1 = (-vec4(_WindTime)) * _WindDir.xzxz + u_xlat1;
					    u_xlat3 = u_xlat3 * vec4(_WindInterpolator);
					    u_xlat1 = u_xlat3 * vec4(0.400000006, 0.400000006, 0.400000006, 0.400000006) + u_xlat1;
					    u_xlat1 = u_xlat1 * vec4(0.0500000007, 0.0500000007, 0.0500000007, 0.0500000007);
					    u_xlat3 = textureLod(_MainTex, u_xlat1.xy, 0.0).zxwy;
					    u_xlat1 = textureLod(_MainTex, u_xlat1.zw, 0.5);
					    u_xlat3.xz = u_xlat1.xy;
					    u_xlat1.xy = (-u_xlat3.xy) + u_xlat3.zw;
					    u_xlat12 = sin(_Time.y);
					    u_xlat12 = u_xlat12 + 1.0;
					    u_xlat12 = u_xlat12 * 0.5;
					    u_xlat1.xy = vec2(u_xlat12) * u_xlat1.xy + u_xlat3.xy;
					    u_xlat1.xy = u_xlat1.xy * vec2(0.400000006, 0.400000006) + vec2(-0.300000012, -0.300000012);
					    u_xlat1.xz = u_xlat1.xy;
					    u_xlat1.xz = clamp(u_xlat1.xz, 0.0, 1.0);
					    u_xlat12 = u_xlat1.y * 0.100000001 + 1.0;
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat1.xyw = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat1.xyw = u_xlat1.xyw + (-_MirrorColor.xyz);
					    u_xlat1.xyw = vec3(u_xlat12) * u_xlat1.xyw + _MirrorColor.xyz;
					    u_xlat1.xyw = (-u_xlat0.xyz) + u_xlat1.xyw;
					    SV_Target0.xyz = u_xlat1.zzz * u_xlat1.xyw + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
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
						vec4 unused_0_2[4];
						float _WindInterpolator;
						vec3 _WindDir;
						float _WindTime;
						vec4 unused_0_6[29];
						vec4 _MirrorColor;
						vec4 unused_0_8;
						vec4 _CloudColor;
						vec4 unused_0_10;
						vec4 _CloudCoverage;
						vec4 unused_0_12[5];
						float _FogMaxRad;
						float _FogMinRad;
						vec4 _FogColor;
						vec4 unused_0_16;
						vec4 _FlashColor;
						vec4 unused_0_18[3];
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[7];
						vec4 unity_OrthoParams;
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[10];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					vec2 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					vec3 u_xlat5;
					vec2 u_xlat8;
					vec2 u_xlat9;
					float u_xlat12;
					float u_xlat13;
					void main()
					{
					    u_xlat0.x = (-unity_MatrixV[0].z);
					    u_xlat0.y = (-unity_MatrixV[2].z);
					    u_xlat8.x = dot(vs_TEXCOORD0.xy, vs_TEXCOORD0.xy);
					    u_xlat8.x = sqrt(u_xlat8.x);
					    u_xlat8.x = u_xlat8.x + (-_FogMinRad);
					    u_xlat12 = _FogMaxRad + (-_FogMinRad);
					    u_xlat1.y = u_xlat8.x / u_xlat12;
					    u_xlat8.x = u_xlat1.y * 1.5999999 + 0.200000003;
					    u_xlat2.xy = (-u_xlat0.xy) * u_xlat8.xx + vs_TEXCOORD0.xy;
					    u_xlat8.x = dot(u_xlat2.xy, u_xlat2.xy);
					    u_xlat8.x = sqrt(u_xlat8.x);
					    u_xlat8.x = u_xlat8.x + (-_FogMinRad);
					    u_xlat1.x = u_xlat8.x / u_xlat12;
					    u_xlat3 = u_xlat1.xxyy * vec4(0.600000024, 0.600000024, 0.600000024, 0.600000024) + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat2.zw = vs_TEXCOORD0.xy;
					    u_xlat8.xy = _WindDir.xz * vec2(_WindTime);
					    u_xlat3 = u_xlat2 * u_xlat3 + (-u_xlat8.xyxy);
					    u_xlat2 = u_xlat2 * _WindDir.xzxz;
					    u_xlat2 = u_xlat2 * vec4(_WindInterpolator);
					    u_xlat2 = u_xlat2 * vec4(0.400000006, 0.400000006, 0.400000006, 0.400000006) + u_xlat3;
					    u_xlat2 = u_xlat2 * vec4(0.0500000007, 0.0500000007, 0.0500000007, 0.0500000007);
					    u_xlat9.xy = u_xlat1.xy * vec2(13.0, 13.0) + vec2(-9.0, -9.0);
					    u_xlat9.xy = max(u_xlat9.xy, vec2(0.0, 0.0));
					    u_xlat13 = u_xlat9.y + 0.5;
					    u_xlat3 = textureLod(_MainTex, u_xlat2.xy, u_xlat9.x).zxwy;
					    u_xlat2 = textureLod(_MainTex, u_xlat2.zw, u_xlat13);
					    u_xlat3.xz = u_xlat2.xy;
					    u_xlat9.xy = (-u_xlat3.xy) + u_xlat3.zw;
					    u_xlat2.xy = u_xlat1.yx + _Time.yy;
					    u_xlat2.xy = sin(u_xlat2.xy);
					    u_xlat2.xy = u_xlat2.xy + vec2(1.0, 1.0);
					    u_xlat2.xy = u_xlat2.xy * vec2(0.5, 0.5);
					    u_xlat9.xy = u_xlat2.xy * u_xlat9.xy + u_xlat3.xy;
					    u_xlat2.xy = u_xlat1.yx * vec2(0.5, 0.5);
					    u_xlat5.x = (-u_xlat1.y) + 1.0;
					    u_xlat5.x = clamp(u_xlat5.x, 0.0, 1.0);
					    u_xlat1.x = u_xlat1.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat9.xy = u_xlat9.xy * vec2(0.400000006, 0.400000006) + u_xlat2.xy;
					    u_xlat9.xy = u_xlat9.xy + vec2(-0.300000012, -0.300000012);
					    u_xlat2.xy = u_xlat9.xy;
					    u_xlat2.xy = clamp(u_xlat2.xy, 0.0, 1.0);
					    u_xlat9.x = u_xlat9.y * 0.100000001 + 1.0;
					    u_xlat5.x = u_xlat5.x * u_xlat2.x;
					    u_xlat13 = unity_OrthoParams.x + 2.0;
					    u_xlat2.xz = vs_TEXCOORD0.xy / vec2(u_xlat13);
					    u_xlat8.xy = (-u_xlat8.xy) * vec2(0.00999999978, 0.00999999978) + u_xlat2.xz;
					    u_xlat0.xy = u_xlat8.xy * vec2(0.5, 0.5) + u_xlat0.xy;
					    u_xlat8.x = _Time.y * 0.200000003 + u_xlat0.x;
					    u_xlat3 = textureLod(_MainTex, u_xlat0.xy, 2.5);
					    u_xlat0.x = sin(u_xlat8.x);
					    u_xlat4.x = (-u_xlat3.x) + u_xlat3.y;
					    u_xlat0.x = u_xlat0.x * u_xlat4.x + u_xlat3.x;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat0.x = _CloudCoverage.x * 2.0 + u_xlat0.x;
					    u_xlat0.x = u_xlat0.x + -1.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat4.xyz = _FlashColor.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + _CloudColor.xyz;
					    u_xlat4.xyz = u_xlat4.xyz + (-_MirrorColor.xyz);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz + _MirrorColor.xyz;
					    u_xlat2.xzw = u_xlat0.xyz * vec3(-0.199999988, -0.199999988, -0.199999988);
					    u_xlat0.xyz = u_xlat5.xxx * u_xlat2.xzw + u_xlat0.xyz;
					    u_xlat12 = dot(u_xlat0.xyz, vec3(0.200000003, 0.699999988, 0.100000001));
					    u_xlat0.xyz = (-vec3(u_xlat12)) + u_xlat0.xyz;
					    u_xlat0.xyz = _CloudCoverage.yyy * u_xlat0.xyz + vec3(u_xlat12);
					    u_xlat2.xzw = (-u_xlat0.xyz) + _LutLerp.www;
					    u_xlat0.xyz = _LutLerp.xyz * u_xlat2.xzw + u_xlat0.xyz;
					    u_xlat2.xzw = _FlashColor.xyz * vec3(0.200000003, 0.200000003, 0.200000003) + _FogColor.xyz;
					    u_xlat2.xzw = u_xlat2.xzw + (-_MirrorColor.xyz);
					    u_xlat5.xyz = u_xlat9.xxx * u_xlat2.xzw + _MirrorColor.xyz;
					    u_xlat2.xzw = (-u_xlat0.xyz) + u_xlat5.xyz;
					    u_xlat0.xyz = u_xlat2.yyy * u_xlat2.xzw + u_xlat0.xyz;
					    u_xlat5.xyz = (-u_xlat0.xyz) + u_xlat5.xyz;
					    SV_Target0.xyz = u_xlat1.xxx * u_xlat5.xyz + u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
				}
			}
		}
	}
}