Shader "Unlit/WindFire" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_PosTex ("Position Texture", 2D) = "white" {}
		_Color0 ("Color0", Vector) = (0.5,0.5,0.5,1)
		_Color1 ("Color1", Vector) = (0.5,0.5,0.5,1)
		_Fire ("Fire", Range(0, 1)) = 1
		[KeywordEnum(None, Up, Cliff, Tree)] _COLOR ("Color Mode", Float) = 0
	}
	SubShader {
		LOD 100
		Tags { "RenderType" = "Transparent" }
		Pass {
			LOD 100
			Tags { "RenderType" = "Transparent" }
			BlendOp Max, Max
			ZWrite Off
			Cull Off
			GpuProgramID 41285
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
						vec4 unused_0_0[14];
						float _WindInterpolator;
						vec3 _WindDir;
						float _SqrtWindTime;
						vec4 unused_0_4[38];
						vec4 _MaxAmbientColor;
						float _FogMaxRad;
						float _FogMinRad;
						vec4 unused_0_8[7];
						vec4 _PosTex_TexelSize;
						vec4 unused_0_10[2];
						vec4 _Bounds_Min;
						vec4 _Bounds_Max;
						float _TotalDistance;
						float _Fire;
						float _Random;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[9];
						mat4x4 unity_MatrixV;
						vec4 unused_2_2[4];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_4[2];
					};
					uniform  sampler2D _PosTex;
					in  vec2 in_TEXCOORD0;
					in  vec2 in_TEXCOORD1;
					in  vec2 in_TEXCOORD2;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec3 u_xlat3;
					vec4 u_xlat4;
					vec3 u_xlat5;
					vec3 u_xlat6;
					vec2 u_xlat7;
					float u_xlat9;
					vec3 u_xlat11;
					float u_xlat16;
					float u_xlat24;
					float u_xlat26;
					void main()
					{
					    u_xlat0.y = (-_WindInterpolator) + 1.0;
					    u_xlat16 = _Time.y / _TotalDistance;
					    u_xlat16 = u_xlat16 + in_TEXCOORD0.x;
					    u_xlat16 = u_xlat16 + _Random;
					    u_xlat16 = fract(u_xlat16);
					    u_xlat24 = (-_Fire) + 1.0;
					    u_xlat1.xy = vec2(u_xlat24) * vec2(-0.699999988, -0.800000012) + vec2(1.0, 1.0);
					    u_xlat0.x = u_xlat16 * u_xlat1.x + _PosTex_TexelSize.x;
					    u_xlat2 = textureLod(_PosTex, u_xlat0.xy, 0.0);
					    u_xlat0.xyw = (-_Bounds_Min.xyz) + _Bounds_Max.xyz;
					    u_xlat2.xyz = u_xlat2.xyz * u_xlat0.xyw + _Bounds_Min.xyz;
					    u_xlat1.x = u_xlat16 * u_xlat1.x;
					    u_xlat3.y = _PosTex_TexelSize.x * 0.300000012 + u_xlat1.x;
					    u_xlat3.xz = (-vec2(_WindInterpolator)) + vec2(1.0, 1.0);
					    u_xlat4 = textureLod(_PosTex, u_xlat3.yz, 0.0);
					    u_xlat0.xyw = u_xlat4.xyz * u_xlat0.xyw + _Bounds_Min.xyz;
					    u_xlat1.xzw = (-u_xlat0.xyw) + u_xlat2.xyz;
					    u_xlat2.x = dot(u_xlat1.xzw, u_xlat1.xzw);
					    u_xlat2.x = sqrt(u_xlat2.x);
					    u_xlat1.xzw = u_xlat1.xzw / u_xlat2.xxx;
					    u_xlat2.xyz = (-u_xlat1.zwx) + _WindDir.yzx;
					    u_xlat2.xyz = vec3(_WindInterpolator) * u_xlat2.xyz + u_xlat1.zwx;
					    u_xlat26 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat26 = inversesqrt(u_xlat26);
					    u_xlat2.xyz = vec3(u_xlat26) * u_xlat2.xyz;
					    u_xlat4.x = unity_MatrixV[0].z;
					    u_xlat4.y = unity_MatrixV[1].z;
					    u_xlat4.z = unity_MatrixV[2].z;
					    u_xlat11.xyz = u_xlat2.xyz * (-u_xlat4.zxy);
					    u_xlat2.xyz = (-u_xlat4.yzx) * u_xlat2.yzx + (-u_xlat11.xyz);
					    u_xlat26 = dot(u_xlat2.xyz, u_xlat2.xyz);
					    u_xlat26 = inversesqrt(u_xlat26);
					    u_xlat2.xyz = vec3(u_xlat26) * u_xlat2.xyz;
					    u_xlat11.xyz = (-u_xlat4.yzx) * u_xlat2.zxy;
					    u_xlat11.xyz = u_xlat2.yzx * (-u_xlat4.zxy) + (-u_xlat11.xyz);
					    u_xlat5.xyz = u_xlat4.zxy * vec3(-1.0, 0.0, 0.0);
					    u_xlat5.xyz = u_xlat4.yzx * vec3(0.0, 0.0, -1.0) + (-u_xlat5.xyz);
					    u_xlat26 = dot(u_xlat5.xz, u_xlat5.xz);
					    u_xlat26 = inversesqrt(u_xlat26);
					    u_xlat5.xyz = vec3(u_xlat26) * u_xlat5.xyz;
					    u_xlat6.xyz = (-u_xlat4.zxy) * u_xlat5.yzx;
					    u_xlat6.xyz = (-u_xlat4.yzx) * u_xlat5.zxy + (-u_xlat6.xyz);
					    u_xlat5.xyz = u_xlat5.xyz * in_TEXCOORD1.xxx;
					    u_xlat26 = dot(u_xlat6.xyz, u_xlat6.xyz);
					    u_xlat26 = inversesqrt(u_xlat26);
					    u_xlat6.xyz = vec3(u_xlat26) * u_xlat6.xyz;
					    u_xlat6.xyz = u_xlat6.xyz * in_TEXCOORD1.yyy;
					    u_xlat26 = (-u_xlat16) + 1.0;
					    u_xlat26 = u_xlat26 * u_xlat26;
					    u_xlat26 = u_xlat26 * u_xlat26;
					    u_xlat26 = u_xlat26 * u_xlat26;
					    u_xlat26 = u_xlat26 * u_xlat26;
					    u_xlat26 = (-u_xlat26) * u_xlat26 + 1.0;
					    u_xlat26 = u_xlat26 * 0.600000024;
					    u_xlat7.xy = in_TEXCOORD0.yy * vec2(0.400000036, 0.399999976) + vec2(0.800000012, 0.600000024);
					    u_xlat26 = u_xlat26 * u_xlat7.x;
					    u_xlat9 = u_xlat1.y * u_xlat26;
					    u_xlat6.xyz = vec3(u_xlat9) * u_xlat6.xyz;
					    u_xlat5.xyz = u_xlat5.xyz * vec3(u_xlat9) + u_xlat6.xyz;
					    u_xlat9 = dot(u_xlat11.xyz, u_xlat5.xyz);
					    u_xlat9 = in_TEXCOORD0.y * 53.1230011 + u_xlat9;
					    u_xlat9 = u_xlat4.w * _TotalDistance + u_xlat9;
					    u_xlat26 = u_xlat4.w * _TotalDistance;
					    vs_TEXCOORD0.x = (-_SqrtWindTime) * u_xlat7.y + u_xlat9;
					    u_xlat9 = u_xlat26 * 2.0 + in_TEXCOORD0.y;
					    vs_TEXCOORD3.y = u_xlat26;
					    u_xlat9 = sin(u_xlat9);
					    u_xlat9 = u_xlat3.x * u_xlat9;
					    u_xlat2.x = dot(u_xlat2.xyz, u_xlat5.xyz);
					    u_xlat2.x = in_TEXCOORD2.y * 12.54 + u_xlat2.x;
					    vs_TEXCOORD0.y = u_xlat9 * 0.300000012 + u_xlat2.x;
					    vs_TEXCOORD1.y = in_TEXCOORD2.x;
					    u_xlat0.xyw = u_xlat0.xyw + u_xlat5.xyz;
					    u_xlat1.x = dot(u_xlat1.xzw, u_xlat5.xyz);
					    u_xlat1.x = u_xlat1.x / _TotalDistance;
					    vs_TEXCOORD3.x = u_xlat16 + u_xlat1.x;
					    u_xlat0.xyz = (-u_xlat4.xyz) * vec3(-0.200000003, -0.200000003, -0.200000003) + u_xlat0.xyw;
					    u_xlat24 = dot(u_xlat0.xz, u_xlat0.xz);
					    u_xlat24 = sqrt(u_xlat24);
					    u_xlat24 = u_xlat24 + (-_FogMinRad);
					    u_xlat1.x = _FogMaxRad + (-_FogMinRad);
					    vs_TEXCOORD1.x = u_xlat24 / u_xlat1.x;
					    vs_TEXCOORD1.x = clamp(vs_TEXCOORD1.x, 0.0, 1.0);
					    vs_TEXCOORD2.xy = in_TEXCOORD1.xy;
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = u_xlat0 + unity_MatrixVP[3];
					    vs_COLOR0 = _MaxAmbientColor.yyyy * vec4(0.100000001, 0.100000001, 0.100000001, 0.100000001) + vec4(1.0, 1.0, 1.0, 1.0);
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
						vec4 unused_0_0[66];
						vec4 _Color0;
						vec4 _Color1;
						vec4 unused_0_3[2];
						float _Fire;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec3 u_xlat1;
					vec4 u_xlat2;
					float u_xlat3;
					float u_xlat6;
					vec2 u_xlat7;
					float u_xlat9;
					void main()
					{
					    u_xlat0.x = _Fire * 0.300000012 + 0.699999988;
					    u_xlat3 = dot(vs_TEXCOORD2.xy, vs_TEXCOORD2.xy);
					    u_xlat3 = sqrt(u_xlat3);
					    u_xlat0.x = u_xlat0.x * u_xlat3;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat3 = min(u_xlat3, 1.0);
					    u_xlat0.y = (-u_xlat3) * u_xlat3 + 1.0;
					    u_xlat6 = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = (-u_xlat6) * u_xlat0.x + 1.0;
					    u_xlat1.y = vs_TEXCOORD0.y;
					    u_xlat6 = vs_TEXCOORD2.x * vs_TEXCOORD2.x;
					    u_xlat9 = min(vs_TEXCOORD3.x, 1.0);
					    u_xlat7.xy = (-vec2(u_xlat9)) + vec2(1.0, 2.0);
					    u_xlat6 = u_xlat6 * u_xlat7.x;
					    u_xlat1.x = u_xlat6 * 0.300000012 + vs_TEXCOORD0.x;
					    u_xlat1.xy = u_xlat1.xy * vec2(0.300000012, 0.300000012);
					    u_xlat2 = texture(_MainTex, u_xlat1.xy);
					    u_xlat6 = (-u_xlat2.y) + u_xlat2.x;
					    u_xlat6 = u_xlat9 * u_xlat6 + u_xlat2.y;
					    u_xlat9 = (-u_xlat9) * u_xlat9 + u_xlat6;
					    u_xlat6 = u_xlat6 * 0.100000001 + 1.0;
					    u_xlat9 = u_xlat9 + 1.0;
					    u_xlat0.x = u_xlat0.x * 2.0 + u_xlat9;
					    u_xlat0.x = u_xlat0.x + -3.0;
					    u_xlat0.x = u_xlat0.x * 4.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat1.x = _Fire * 10.0;
					    u_xlat1.x = u_xlat1.x;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat0.x = u_xlat0.x * u_xlat1.x;
					    u_xlat0.xy = u_xlat0.xy * u_xlat7.yx;
					    u_xlat3 = u_xlat6 * u_xlat0.y;
					    u_xlat0.xzw = u_xlat0.xxx * _Color0.xyz;
					    u_xlat0.xzw = max(u_xlat0.xzw, vec3(0.0, 0.0, 0.0));
					    u_xlat1.x = (-_Fire) + 1.0;
					    u_xlat1.x = u_xlat1.x * u_xlat1.x;
					    u_xlat1.x = (-u_xlat1.x) * u_xlat1.x + 1.0;
					    u_xlat3 = u_xlat3 * u_xlat1.x;
					    u_xlat3 = u_xlat3 * 1.5;
					    u_xlat1.xyz = vec3(u_xlat3) * _Color1.xyz;
					    u_xlat0.xyz = max(u_xlat0.xzw, u_xlat1.xyz);
					    SV_Target0.xyz = u_xlat0.xyz * vs_COLOR0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}"
				}
			}
		}
	}
}