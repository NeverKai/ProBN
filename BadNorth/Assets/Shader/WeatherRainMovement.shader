Shader "Weather/RainMovement" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex ("Noise Texture", 2D) = "white" {}
		_Gravity ("Gravity", Float) = 1
		_Noise ("Noise", Float) = 0.1
		_Spin ("Spin", Float) = 0.1
		_DeltaTime ("DeltaTime", Float) = 0.1
		_Amount ("Amount", Range(0, 1)) = 0.1
		[Toggle] _Wrap ("Wrap", Float) = 0
	}
	SubShader {
		Pass {
			ZTest Always
			ZWrite Off
			Cull Off
			GpuProgramID 59750
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
				SubProgram "d3d11 " {
					Keywords { "_WRAP_ON" }
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[10];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_5;
						mat4x4 _Tex2World;
						mat4x4 _World2Tex;
						vec4 unused_0_8[31];
						float _WaterLevel;
						vec4 unused_0_10[6];
						float _DeltaTime;
						float _Amount;
						vec4 _Random;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _TopdownTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					bool u_xlatb2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					bool u_xlatb5;
					vec2 u_xlat6;
					bool u_xlatb6;
					float u_xlat11;
					bool u_xlatb11;
					float u_xlat15;
					void main()
					{
					    u_xlat0.xz = _WindDir.xz * vec2(18.0, 18.0);
					    u_xlat0.y = 8.0;
					    u_xlat0.xyz = vec3(_WindInterpolator) * u_xlat0.xyz + vec3(0.0, -9.0, 0.0);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(vec3(_DeltaTime, _DeltaTime, _DeltaTime));
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat15 = (-u_xlat1.w) + 1.0;
					    u_xlat15 = u_xlat15 * vs_TEXCOORD0.x;
					    u_xlat15 = u_xlat15 * 6.0 + 1.0;
					    u_xlat2 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat2 = _Tex2World[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = _Tex2World[2] * u_xlat1.zzzz + u_xlat2;
					    u_xlat2 = u_xlat2 + _Tex2World[3];
					    u_xlat1.xy = u_xlat0.xz * vec2(u_xlat15) + u_xlat2.xz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(u_xlat15) + u_xlat2.xyz;
					    u_xlat1.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat1.xy;
					    u_xlat1.xy = u_xlat1.xy / _AoTexVolume.xz;
					    u_xlat3 = textureLod(_TopdownTex, u_xlat1.xy, 0.0);
					    u_xlat1.x = u_xlat3.w * 8.0 + _WaterLevel;
					    u_xlat6.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat2.xz;
					    u_xlat6.xy = u_xlat6.xy / _AoTexVolume.xz;
					    u_xlat3 = textureLod(_TopdownTex, u_xlat6.xy, 0.0);
					    u_xlat6.x = u_xlat3.w * 8.0 + _WaterLevel;
					    u_xlat6.x = max(u_xlat1.x, u_xlat6.x);
					    u_xlat1.x = u_xlat1.x + -1.0;
					    u_xlatb1 = u_xlat2.y<u_xlat1.x;
					    u_xlatb6 = u_xlat6.x<u_xlat2.y;
					    u_xlat11 = (-_DeltaTime) * 0.5 + u_xlat1.w;
					    u_xlat3.w = (u_xlatb6) ? u_xlat1.w : u_xlat11;
					    u_xlatb11 = u_xlat11<0.0;
					    u_xlat4.w = uintBitsToFloat((uint(u_xlatb1) * 0xffffffffu) | (uint(u_xlatb11) * 0xffffffffu));
					    u_xlat0.w = 0.0;
					    u_xlat4.xyz = u_xlat2.xyz;
					    u_xlat0 = (bool(u_xlatb6)) ? u_xlat0 : u_xlat4;
					    u_xlat1.xyz = u_xlat0.yyy * _World2Tex[1].xyz;
					    u_xlat1.xyz = _World2Tex[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = _World2Tex[2].xyz * u_xlat0.zzz + u_xlat1.xyz;
					    u_xlat3.xyz = _World2Tex[3].xyz * u_xlat2.www + u_xlat0.xyz;
					    u_xlat0.xy = u_xlat3.zx * vec2(7.0999999, 7.0999999) + _Random.xy;
					    u_xlat1.xz = fract(u_xlat0.xy);
					    u_xlat0.x = vs_TEXCOORD0.x * 12.3100004 + _Random.z;
					    u_xlat0.x = fract(u_xlat0.x);
					    u_xlat1.y = u_xlat0.x * 2.0 + 1.0;
					    u_xlatb0 = _Amount>=vs_TEXCOORD0.x;
					    u_xlat1.w = u_xlatb0 ? 1.0 : float(0.0);
					    u_xlat0 = (floatBitsToInt(u_xlat0.w) != 0) ? u_xlat1 : u_xlat3;
					    u_xlatb6 = u_xlat0.x<0.0;
					    u_xlatb2 = 1.0<u_xlat0.x;
					    u_xlatb6 = u_xlatb6 || u_xlatb2;
					    u_xlat2.x = u_xlat0.z * 7.0999999 + _Random.y;
					    u_xlat1.z = fract(u_xlat2.x);
					    u_xlat1.x = fract(u_xlat0.x);
					    u_xlat0.xzw = (bool(u_xlatb6)) ? u_xlat1.xzw : u_xlat0.xzw;
					    SV_Target0.y = u_xlat0.y;
					    u_xlatb5 = u_xlat0.z<0.0;
					    u_xlatb6 = 1.0<u_xlat0.z;
					    u_xlatb5 = u_xlatb5 || u_xlatb6;
					    u_xlat6.x = u_xlat0.x * 7.30000019 + _Random.z;
					    u_xlat1.x = fract(u_xlat6.x);
					    u_xlat1.z = fract(u_xlat0.z);
					    SV_Target0.xzw = (bool(u_xlatb5)) ? u_xlat1.xzw : u_xlat0.xzw;
					    return;
					}"
				}
				SubProgram "d3d11 " {
					Keywords { "_WRAP_ON" }
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
						vec4 unused_0_0[3];
						vec3 _AoTexVolume;
						vec4 unused_0_2[10];
						float _WindInterpolator;
						vec3 _WindDir;
						vec4 unused_0_5;
						mat4x4 _Tex2World;
						mat4x4 _World2Tex;
						vec4 unused_0_8[31];
						float _WaterLevel;
						vec4 unused_0_10[6];
						float _DeltaTime;
						float _Amount;
						vec4 _Random;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _TopdownTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					bool u_xlatb2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					bool u_xlatb5;
					vec2 u_xlat6;
					bool u_xlatb6;
					float u_xlat11;
					bool u_xlatb11;
					float u_xlat15;
					void main()
					{
					    u_xlat0.xz = _WindDir.xz * vec2(18.0, 18.0);
					    u_xlat0.y = 8.0;
					    u_xlat0.xyz = vec3(_WindInterpolator) * u_xlat0.xyz + vec3(0.0, -9.0, 0.0);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(vec3(_DeltaTime, _DeltaTime, _DeltaTime));
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat15 = (-u_xlat1.w) + 1.0;
					    u_xlat15 = u_xlat15 * vs_TEXCOORD0.x;
					    u_xlat15 = u_xlat15 * 6.0 + 1.0;
					    u_xlat2 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat2 = _Tex2World[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat2 = _Tex2World[2] * u_xlat1.zzzz + u_xlat2;
					    u_xlat2 = u_xlat2 + _Tex2World[3];
					    u_xlat1.xy = u_xlat0.xz * vec2(u_xlat15) + u_xlat2.xz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(u_xlat15) + u_xlat2.xyz;
					    u_xlat1.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat1.xy;
					    u_xlat1.xy = u_xlat1.xy / _AoTexVolume.xz;
					    u_xlat3 = textureLod(_TopdownTex, u_xlat1.xy, 0.0);
					    u_xlat1.x = u_xlat3.w * 8.0 + _WaterLevel;
					    u_xlat6.xy = _AoTexVolume.xz * vec2(0.5, 0.5) + u_xlat2.xz;
					    u_xlat6.xy = u_xlat6.xy / _AoTexVolume.xz;
					    u_xlat3 = textureLod(_TopdownTex, u_xlat6.xy, 0.0);
					    u_xlat6.x = u_xlat3.w * 8.0 + _WaterLevel;
					    u_xlat6.x = max(u_xlat1.x, u_xlat6.x);
					    u_xlat1.x = u_xlat1.x + -1.0;
					    u_xlatb1 = u_xlat2.y<u_xlat1.x;
					    u_xlatb6 = u_xlat6.x<u_xlat2.y;
					    u_xlat11 = (-_DeltaTime) * 0.5 + u_xlat1.w;
					    u_xlat3.w = (u_xlatb6) ? u_xlat1.w : u_xlat11;
					    u_xlatb11 = u_xlat11<0.0;
					    u_xlat4.w = uintBitsToFloat((uint(u_xlatb1) * 0xffffffffu) | (uint(u_xlatb11) * 0xffffffffu));
					    u_xlat0.w = 0.0;
					    u_xlat4.xyz = u_xlat2.xyz;
					    u_xlat0 = (bool(u_xlatb6)) ? u_xlat0 : u_xlat4;
					    u_xlat1.xyz = u_xlat0.yyy * _World2Tex[1].xyz;
					    u_xlat1.xyz = _World2Tex[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = _World2Tex[2].xyz * u_xlat0.zzz + u_xlat1.xyz;
					    u_xlat3.xyz = _World2Tex[3].xyz * u_xlat2.www + u_xlat0.xyz;
					    u_xlat0.xy = u_xlat3.zx * vec2(7.0999999, 7.0999999) + _Random.xy;
					    u_xlat1.xz = fract(u_xlat0.xy);
					    u_xlat0.x = vs_TEXCOORD0.x * 12.3100004 + _Random.z;
					    u_xlat0.x = fract(u_xlat0.x);
					    u_xlat1.y = u_xlat0.x * 2.0 + 1.0;
					    u_xlatb0 = _Amount>=vs_TEXCOORD0.x;
					    u_xlat1.w = u_xlatb0 ? 1.0 : float(0.0);
					    u_xlat0 = (floatBitsToInt(u_xlat0.w) != 0) ? u_xlat1 : u_xlat3;
					    u_xlatb6 = u_xlat0.x<0.0;
					    u_xlatb2 = 1.0<u_xlat0.x;
					    u_xlatb6 = u_xlatb6 || u_xlatb2;
					    u_xlat2.x = u_xlat0.z * 7.0999999 + _Random.y;
					    u_xlat1.z = fract(u_xlat2.x);
					    u_xlat1.x = fract(u_xlat0.x);
					    u_xlat0.xzw = (bool(u_xlatb6)) ? u_xlat1.xzw : u_xlat0.xzw;
					    SV_Target0.y = u_xlat0.y;
					    u_xlatb5 = u_xlat0.z<0.0;
					    u_xlatb6 = 1.0<u_xlat0.z;
					    u_xlatb5 = u_xlatb5 || u_xlatb6;
					    u_xlat6.x = u_xlat0.x * 7.30000019 + _Random.z;
					    u_xlat1.x = fract(u_xlat6.x);
					    u_xlat1.z = fract(u_xlat0.z);
					    SV_Target0.xzw = (bool(u_xlatb5)) ? u_xlat1.xzw : u_xlat0.xzw;
					    return;
					}"
				}
			}
		}
	}
}