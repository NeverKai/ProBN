Shader "Weather/WindParticleMovement" {
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
			GpuProgramID 43443
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[10];
						float _WindInterpolator;
						float _MaxWindSpeed;
						vec3 _WindDir;
						float _WindTime;
						mat4x4 _Tex2World;
						mat4x4 _World2Tex;
						vec4 unused_0_10[38];
						float _DeltaTime;
						float _Gravity;
						vec4 unused_0_13;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _WindTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec3 u_xlat8;
					vec3 u_xlat11;
					float u_xlat21;
					bvec2 u_xlatb21;
					float u_xlat27;
					float u_xlat29;
					void main()
					{
					    u_xlat0.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat0.y = 1.0;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat2 = u_xlat1.yyyy * _Tex2World[1];
					    u_xlat2 = _Tex2World[0] * u_xlat1.xxxx + u_xlat2;
					    u_xlat1 = _Tex2World[2] * u_xlat1.zzzz + u_xlat2;
					    u_xlat1 = u_xlat1 + _Tex2World[3];
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat2.xyz = _AoTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat2.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat2.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat3.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat4.z = u_xlat2.z * u_xlat3.y;
					    u_xlat27 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat29 = u_xlat2.y * u_xlat27;
					    u_xlat5.xyz = u_xlat2.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat29;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat29;
					    u_xlat6 = textureLod(_WindTex, u_xlat4.xz, 0.0);
					    u_xlat11.yz = u_xlat4.yx;
					    u_xlat7 = textureLod(_WindTex, u_xlat4.yz, 0.0);
					    u_xlat6 = u_xlat0.xxxx * u_xlat6;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat8.yyyy;
					    u_xlat6 = u_xlat8.zzzz * u_xlat6;
					    u_xlat7 = u_xlat7 * u_xlat8.xxxx;
					    u_xlat7 = u_xlat8.yyyy * u_xlat7;
					    u_xlat6 = u_xlat7 * u_xlat8.zzzz + u_xlat6;
					    u_xlat27 = u_xlat27 * u_xlat5.y;
					    u_xlat4.y = u_xlat3.x * u_xlat2.x + u_xlat27;
					    u_xlat4.x = u_xlat3.x * u_xlat5.x + u_xlat27;
					    u_xlat11.x = u_xlat3.y * u_xlat5.z;
					    u_xlat3 = textureLod(_WindTex, u_xlat4.yz, 0.0);
					    u_xlat5 = textureLod(_WindTex, u_xlat4.xz, 0.0);
					    u_xlat5 = u_xlat0.xxxx * u_xlat5;
					    u_xlat5 = u_xlat0.yyyy * u_xlat5;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat3 = u_xlat3 * u_xlat8.zzzz + u_xlat6;
					    u_xlat3 = u_xlat5 * u_xlat8.zzzz + u_xlat3;
					    u_xlat5 = textureLod(_WindTex, u_xlat11.yx, 0.0);
					    u_xlat6 = textureLod(_WindTex, u_xlat11.zx, 0.0);
					    u_xlat4.w = u_xlat11.x;
					    u_xlat2 = u_xlat0.xxxx * u_xlat6;
					    u_xlat2 = u_xlat8.yyyy * u_xlat2;
					    u_xlat5 = u_xlat8.xxxx * u_xlat5;
					    u_xlat5 = u_xlat8.yyyy * u_xlat5;
					    u_xlat3 = u_xlat5 * u_xlat0.zzzz + u_xlat3;
					    u_xlat2 = u_xlat2 * u_xlat0.zzzz + u_xlat3;
					    u_xlat3 = textureLod(_WindTex, u_xlat4.yw, 0.0);
					    u_xlat4 = textureLod(_WindTex, u_xlat4.xw, 0.0);
					    u_xlat4 = u_xlat0.xxxx * u_xlat4;
					    u_xlat4 = u_xlat0.yyyy * u_xlat4;
					    u_xlat3 = u_xlat8.xxxx * u_xlat3;
					    u_xlat3 = u_xlat0.yyyy * u_xlat3;
					    u_xlat2 = u_xlat3 * u_xlat0.zzzz + u_xlat2;
					    u_xlat0 = u_xlat4 * u_xlat0.zzzz + u_xlat2;
					    u_xlat2.x = u_xlat0.w * 2.0 + -1.0;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat11.xyz = u_xlat0.xyz * vec3(4.0, 4.0, 4.0) + vec3(-2.0, -2.0, -2.0);
					    u_xlat0 = u_xlat0 * vec4(4.0, 4.0, 4.0, 1.0) + vec4(-2.0, -2.0, -2.0, 0.0);
					    u_xlat3.xy = (-_WindDir.zx) * vec2(_WindTime) + u_xlat1.zx;
					    u_xlat3.xy = u_xlat3.xy * vec2(0.159154937, 0.159154937);
					    u_xlatb21.xy = greaterThanEqual(u_xlat3.xyxy, (-u_xlat3.xyxy)).xy;
					    u_xlat3.xy = fract(abs(u_xlat3.xy));
					    {
					        vec4 hlslcc_movcTemp = u_xlat3;
					        hlslcc_movcTemp.x = (u_xlatb21.x) ? u_xlat3.x : (-u_xlat3.x);
					        hlslcc_movcTemp.y = (u_xlatb21.y) ? u_xlat3.y : (-u_xlat3.y);
					        u_xlat3 = hlslcc_movcTemp;
					    }
					    u_xlat3.xy = u_xlat3.xy * vec2(6.28318548, 6.28318548);
					    u_xlat3.xy = sin(u_xlat3.xy);
					    u_xlat21 = _MaxWindSpeed * _WindInterpolator;
					    u_xlat3.xy = (-u_xlat11.xz) * vec2(u_xlat21) + u_xlat3.xy;
					    u_xlat4.xz = u_xlat11.xz * vec2(u_xlat21);
					    u_xlat4.w = u_xlat11.y * u_xlat21 + (-_Gravity);
					    u_xlat2.xy = u_xlat2.xx * u_xlat3.xy + u_xlat4.xz;
					    u_xlat0.x = dot(u_xlat0, u_xlat0);
					    SV_Target0.w = u_xlat0.w;
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.xy = u_xlat0.xx * u_xlat2.xy;
					    u_xlat0.xz = u_xlat0.xy * vec2(0.200000003, 0.200000003);
					    u_xlat0.y = 0.0;
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat4.xwz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(vec3(_DeltaTime, _DeltaTime, _DeltaTime)) + u_xlat1.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * _World2Tex[1].xyz;
					    u_xlat0.xyw = _World2Tex[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    u_xlat0.xyz = _World2Tex[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    SV_Target0.xyz = _World2Tex[3].xyz * u_xlat1.www + u_xlat0.xyz;
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
						vec4 unused_0_0[2];
						vec2 _AoTexSize;
						vec3 _AoTexVolume;
						vec4 unused_0_3[10];
						float _WindInterpolator;
						float _MaxWindSpeed;
						vec3 _WindDir;
						float _WindTime;
						mat4x4 _Tex2World;
						mat4x4 _World2Tex;
						vec4 unused_0_10[38];
						float _DeltaTime;
						float _Gravity;
						float _Amount;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _WindTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					bvec3 u_xlatb4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec3 u_xlat8;
					vec2 u_xlat9;
					bool u_xlatb9;
					vec3 u_xlat10;
					bool u_xlatb18;
					float u_xlat27;
					float u_xlat28;
					void main()
					{
					    u_xlat0.xyz = _AoTexVolume.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xz = _AoTexVolume.xz * vec2(0.5, 0.5);
					    u_xlat1.y = 1.0;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3 = u_xlat2.yyyy * _Tex2World[1];
					    u_xlat3 = _Tex2World[0] * u_xlat2.xxxx + u_xlat3;
					    u_xlat3 = _Tex2World[2] * u_xlat2.zzzz + u_xlat3;
					    u_xlat3 = u_xlat3 + _Tex2World[3];
					    u_xlat1.xyz = u_xlat1.xyz + u_xlat3.xyz;
					    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.5, 0.5, 0.5));
					    u_xlat0.xyz = min(u_xlat0.xyz, u_xlat1.xyz);
					    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.5, -0.5, -0.5);
					    u_xlat1.xyz = floor(u_xlat0.xyz);
					    u_xlat0.xyz = fract(u_xlat0.xyz);
					    u_xlat27 = _AoTexVolume.x / _AoTexSize.x;
					    u_xlat28 = u_xlat1.y * u_xlat27;
					    u_xlat2.xyz = u_xlat1.xyz + vec3(1.0, 1.0, 1.0);
					    u_xlat4.xy = vec2(1.0, 1.0) / _AoTexSize.xy;
					    u_xlat5.x = u_xlat4.x * u_xlat2.x + u_xlat28;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat28;
					    u_xlat5.z = u_xlat1.z * u_xlat4.y;
					    u_xlat6 = textureLod(_WindTex, u_xlat5.xz, 0.0);
					    u_xlat10.yz = u_xlat5.yx;
					    u_xlat7 = textureLod(_WindTex, u_xlat5.yz, 0.0);
					    u_xlat6 = u_xlat0.xxxx * u_xlat6;
					    u_xlat8.xyz = (-u_xlat0.xyz) + vec3(1.0, 1.0, 1.0);
					    u_xlat6 = u_xlat6 * u_xlat8.yyyy;
					    u_xlat6 = u_xlat8.zzzz * u_xlat6;
					    u_xlat7 = u_xlat7 * u_xlat8.xxxx;
					    u_xlat7 = u_xlat8.yyyy * u_xlat7;
					    u_xlat6 = u_xlat7 * u_xlat8.zzzz + u_xlat6;
					    u_xlat27 = u_xlat27 * u_xlat2.y;
					    u_xlat5.y = u_xlat4.x * u_xlat1.x + u_xlat27;
					    u_xlat5.x = u_xlat4.x * u_xlat2.x + u_xlat27;
					    u_xlat10.x = u_xlat2.z * u_xlat4.y;
					    u_xlat4 = textureLod(_WindTex, u_xlat5.yz, 0.0);
					    u_xlat7 = textureLod(_WindTex, u_xlat5.xz, 0.0);
					    u_xlat7 = u_xlat0.xxxx * u_xlat7;
					    u_xlat7 = u_xlat0.yyyy * u_xlat7;
					    u_xlat4 = u_xlat8.xxxx * u_xlat4;
					    u_xlat4 = u_xlat0.yyyy * u_xlat4;
					    u_xlat4 = u_xlat4 * u_xlat8.zzzz + u_xlat6;
					    u_xlat4 = u_xlat7 * u_xlat8.zzzz + u_xlat4;
					    u_xlat6 = textureLod(_WindTex, u_xlat10.yx, 0.0);
					    u_xlat7 = textureLod(_WindTex, u_xlat10.zx, 0.0);
					    u_xlat5.w = u_xlat10.x;
					    u_xlat1 = u_xlat0.xxxx * u_xlat7;
					    u_xlat1 = u_xlat8.yyyy * u_xlat1;
					    u_xlat6 = u_xlat8.xxxx * u_xlat6;
					    u_xlat6 = u_xlat8.yyyy * u_xlat6;
					    u_xlat4 = u_xlat6 * u_xlat0.zzzz + u_xlat4;
					    u_xlat1 = u_xlat1 * u_xlat0.zzzz + u_xlat4;
					    u_xlat4 = textureLod(_WindTex, u_xlat5.yw, 0.0);
					    u_xlat5 = textureLod(_WindTex, u_xlat5.xw, 0.0);
					    u_xlat5 = u_xlat0.xxxx * u_xlat5;
					    u_xlat5 = u_xlat0.yyyy * u_xlat5;
					    u_xlat4 = u_xlat8.xxxx * u_xlat4;
					    u_xlat4 = u_xlat0.yyyy * u_xlat4;
					    u_xlat1 = u_xlat4 * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = u_xlat5 * u_xlat0.zzzz + u_xlat1;
					    u_xlat1.x = u_xlat0.w * 2.0 + -1.0;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat10.xyz = u_xlat0.xyz * vec3(4.0, 4.0, 4.0) + vec3(-2.0, -2.0, -2.0);
					    u_xlat2.xyz = (-_WindDir.xzx) * vec3(_WindTime) + u_xlat3.xzx;
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.159154937, 0.159154937, 0.159154937);
					    u_xlatb4.xyz = greaterThanEqual(u_xlat2.xyzx, (-u_xlat2.zyzz)).xyz;
					    u_xlat2.xyz = fract(abs(u_xlat2.zyz));
					    {
					        vec4 hlslcc_movcTemp = u_xlat2;
					        hlslcc_movcTemp.x = (u_xlatb4.x) ? u_xlat2.x : (-u_xlat2.z);
					        hlslcc_movcTemp.y = (u_xlatb4.y) ? u_xlat2.y : (-u_xlat2.y);
					        hlslcc_movcTemp.z = (u_xlatb4.z) ? u_xlat2.z : (-u_xlat2.z);
					        u_xlat2 = hlslcc_movcTemp;
					    }
					    u_xlat2.xyz = u_xlat2.xyz * vec3(6.28318548, 6.28318548, 6.28318548);
					    u_xlat2.xyz = sin(u_xlat2.xyz);
					    u_xlat4.x = _MaxWindSpeed * _WindInterpolator;
					    u_xlat2.xyz = (-u_xlat10.zxz) * u_xlat4.xxx + u_xlat2.xyz;
					    u_xlat5.xz = u_xlat10.xz * u_xlat4.xx;
					    u_xlat5.w = u_xlat10.y * u_xlat4.x + (-_Gravity);
					    u_xlat1.xyz = u_xlat1.xxx * u_xlat2.xyz + u_xlat5.zxz;
					    u_xlat4 = u_xlat0 * vec4(4.0, 4.0, 4.0, 1.0) + vec4(-2.0, -2.0, -2.0, 0.0);
					    u_xlat0.x = dot(u_xlat4, u_xlat4);
					    u_xlat0.x = sqrt(u_xlat0.x);
					    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
					    u_xlat1.yzw = u_xlat0.xyz * vec3(0.200000003, 0.200000003, 0.200000003);
					    u_xlat1.x = 0.0;
					    u_xlat1 = u_xlat1 + u_xlat5.wzxz;
					    u_xlat1 = u_xlat1 * vec4(vec4(_DeltaTime, _DeltaTime, _DeltaTime, _DeltaTime)) + u_xlat3.yzxz;
					    u_xlat0.xyz = u_xlat1.xxx * _World2Tex[1].xyz;
					    u_xlat0.xyz = _World2Tex[0].xyz * u_xlat1.zzz + u_xlat0.xyz;
					    u_xlat0.xyz = _World2Tex[2].xyz * u_xlat1.www + u_xlat0.xyz;
					    u_xlat3.xyz = _World2Tex[3].xyz * u_xlat3.www + u_xlat0.xyz;
					    u_xlatb0 = u_xlat3.x<0.0;
					    u_xlatb9 = 1.0<u_xlat3.x;
					    u_xlatb0 = u_xlatb9 || u_xlatb0;
					    u_xlat4.x = fract(u_xlat3.x);
					    u_xlat5 = u_xlat1 + _Time.xyxy;
					    u_xlat9.xy = u_xlat1.zx + _Time.yx;
					    u_xlat1.xy = fract(u_xlat9.xy);
					    u_xlat5 = fract(u_xlat5.zxwy);
					    u_xlat4.yz = u_xlat5.yw;
					    u_xlat3.w = min(u_xlat2.w, u_xlat0.w);
					    u_xlatb9 = u_xlat0.w<0.100000001;
					    u_xlatb18 = _Amount>=vs_TEXCOORD0.x;
					    u_xlat4.w = u_xlatb18 ? 1.0 : float(0.0);
					    u_xlat2 = (bool(u_xlatb0)) ? u_xlat4 : u_xlat3;
					    u_xlat5.w = u_xlat4.w;
					    u_xlatb0 = u_xlat2.y<0.0;
					    u_xlatb18 = 1.0<u_xlat2.y;
					    u_xlatb0 = u_xlatb18 || u_xlatb0;
					    u_xlat5.y = 1.0;
					    u_xlat2 = (bool(u_xlatb0)) ? u_xlat5 : u_xlat2;
					    u_xlatb0 = u_xlat2.z<0.0;
					    u_xlatb18 = 1.0<u_xlat2.z;
					    u_xlatb0 = u_xlatb18 || u_xlatb0;
					    u_xlat1.z = fract(u_xlat2.z);
					    u_xlat1.w = u_xlat5.w;
					    u_xlat1 = (bool(u_xlatb0)) ? u_xlat1 : u_xlat2;
					    SV_Target0.yw = (bool(u_xlatb9)) ? u_xlat5.yw : u_xlat1.yw;
					    SV_Target0.xz = u_xlat1.xz;
					    return;
					}"
				}
			}
		}
	}
}