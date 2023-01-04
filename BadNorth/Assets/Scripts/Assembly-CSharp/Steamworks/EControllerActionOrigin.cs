using System;

namespace Steamworks
{
	// Token: 0x020002D1 RID: 721
	public enum EControllerActionOrigin
	{
		// Token: 0x0400082B RID: 2091
		k_EControllerActionOrigin_None,
		// Token: 0x0400082C RID: 2092
		k_EControllerActionOrigin_A,
		// Token: 0x0400082D RID: 2093
		k_EControllerActionOrigin_B,
		// Token: 0x0400082E RID: 2094
		k_EControllerActionOrigin_X,
		// Token: 0x0400082F RID: 2095
		k_EControllerActionOrigin_Y,
		// Token: 0x04000830 RID: 2096
		k_EControllerActionOrigin_LeftBumper,
		// Token: 0x04000831 RID: 2097
		k_EControllerActionOrigin_RightBumper,
		// Token: 0x04000832 RID: 2098
		k_EControllerActionOrigin_LeftGrip,
		// Token: 0x04000833 RID: 2099
		k_EControllerActionOrigin_RightGrip,
		// Token: 0x04000834 RID: 2100
		k_EControllerActionOrigin_Start,
		// Token: 0x04000835 RID: 2101
		k_EControllerActionOrigin_Back,
		// Token: 0x04000836 RID: 2102
		k_EControllerActionOrigin_LeftPad_Touch,
		// Token: 0x04000837 RID: 2103
		k_EControllerActionOrigin_LeftPad_Swipe,
		// Token: 0x04000838 RID: 2104
		k_EControllerActionOrigin_LeftPad_Click,
		// Token: 0x04000839 RID: 2105
		k_EControllerActionOrigin_LeftPad_DPadNorth,
		// Token: 0x0400083A RID: 2106
		k_EControllerActionOrigin_LeftPad_DPadSouth,
		// Token: 0x0400083B RID: 2107
		k_EControllerActionOrigin_LeftPad_DPadWest,
		// Token: 0x0400083C RID: 2108
		k_EControllerActionOrigin_LeftPad_DPadEast,
		// Token: 0x0400083D RID: 2109
		k_EControllerActionOrigin_RightPad_Touch,
		// Token: 0x0400083E RID: 2110
		k_EControllerActionOrigin_RightPad_Swipe,
		// Token: 0x0400083F RID: 2111
		k_EControllerActionOrigin_RightPad_Click,
		// Token: 0x04000840 RID: 2112
		k_EControllerActionOrigin_RightPad_DPadNorth,
		// Token: 0x04000841 RID: 2113
		k_EControllerActionOrigin_RightPad_DPadSouth,
		// Token: 0x04000842 RID: 2114
		k_EControllerActionOrigin_RightPad_DPadWest,
		// Token: 0x04000843 RID: 2115
		k_EControllerActionOrigin_RightPad_DPadEast,
		// Token: 0x04000844 RID: 2116
		k_EControllerActionOrigin_LeftTrigger_Pull,
		// Token: 0x04000845 RID: 2117
		k_EControllerActionOrigin_LeftTrigger_Click,
		// Token: 0x04000846 RID: 2118
		k_EControllerActionOrigin_RightTrigger_Pull,
		// Token: 0x04000847 RID: 2119
		k_EControllerActionOrigin_RightTrigger_Click,
		// Token: 0x04000848 RID: 2120
		k_EControllerActionOrigin_LeftStick_Move,
		// Token: 0x04000849 RID: 2121
		k_EControllerActionOrigin_LeftStick_Click,
		// Token: 0x0400084A RID: 2122
		k_EControllerActionOrigin_LeftStick_DPadNorth,
		// Token: 0x0400084B RID: 2123
		k_EControllerActionOrigin_LeftStick_DPadSouth,
		// Token: 0x0400084C RID: 2124
		k_EControllerActionOrigin_LeftStick_DPadWest,
		// Token: 0x0400084D RID: 2125
		k_EControllerActionOrigin_LeftStick_DPadEast,
		// Token: 0x0400084E RID: 2126
		k_EControllerActionOrigin_Gyro_Move,
		// Token: 0x0400084F RID: 2127
		k_EControllerActionOrigin_Gyro_Pitch,
		// Token: 0x04000850 RID: 2128
		k_EControllerActionOrigin_Gyro_Yaw,
		// Token: 0x04000851 RID: 2129
		k_EControllerActionOrigin_Gyro_Roll,
		// Token: 0x04000852 RID: 2130
		k_EControllerActionOrigin_PS4_X,
		// Token: 0x04000853 RID: 2131
		k_EControllerActionOrigin_PS4_Circle,
		// Token: 0x04000854 RID: 2132
		k_EControllerActionOrigin_PS4_Triangle,
		// Token: 0x04000855 RID: 2133
		k_EControllerActionOrigin_PS4_Square,
		// Token: 0x04000856 RID: 2134
		k_EControllerActionOrigin_PS4_LeftBumper,
		// Token: 0x04000857 RID: 2135
		k_EControllerActionOrigin_PS4_RightBumper,
		// Token: 0x04000858 RID: 2136
		k_EControllerActionOrigin_PS4_Options,
		// Token: 0x04000859 RID: 2137
		k_EControllerActionOrigin_PS4_Share,
		// Token: 0x0400085A RID: 2138
		k_EControllerActionOrigin_PS4_LeftPad_Touch,
		// Token: 0x0400085B RID: 2139
		k_EControllerActionOrigin_PS4_LeftPad_Swipe,
		// Token: 0x0400085C RID: 2140
		k_EControllerActionOrigin_PS4_LeftPad_Click,
		// Token: 0x0400085D RID: 2141
		k_EControllerActionOrigin_PS4_LeftPad_DPadNorth,
		// Token: 0x0400085E RID: 2142
		k_EControllerActionOrigin_PS4_LeftPad_DPadSouth,
		// Token: 0x0400085F RID: 2143
		k_EControllerActionOrigin_PS4_LeftPad_DPadWest,
		// Token: 0x04000860 RID: 2144
		k_EControllerActionOrigin_PS4_LeftPad_DPadEast,
		// Token: 0x04000861 RID: 2145
		k_EControllerActionOrigin_PS4_RightPad_Touch,
		// Token: 0x04000862 RID: 2146
		k_EControllerActionOrigin_PS4_RightPad_Swipe,
		// Token: 0x04000863 RID: 2147
		k_EControllerActionOrigin_PS4_RightPad_Click,
		// Token: 0x04000864 RID: 2148
		k_EControllerActionOrigin_PS4_RightPad_DPadNorth,
		// Token: 0x04000865 RID: 2149
		k_EControllerActionOrigin_PS4_RightPad_DPadSouth,
		// Token: 0x04000866 RID: 2150
		k_EControllerActionOrigin_PS4_RightPad_DPadWest,
		// Token: 0x04000867 RID: 2151
		k_EControllerActionOrigin_PS4_RightPad_DPadEast,
		// Token: 0x04000868 RID: 2152
		k_EControllerActionOrigin_PS4_CenterPad_Touch,
		// Token: 0x04000869 RID: 2153
		k_EControllerActionOrigin_PS4_CenterPad_Swipe,
		// Token: 0x0400086A RID: 2154
		k_EControllerActionOrigin_PS4_CenterPad_Click,
		// Token: 0x0400086B RID: 2155
		k_EControllerActionOrigin_PS4_CenterPad_DPadNorth,
		// Token: 0x0400086C RID: 2156
		k_EControllerActionOrigin_PS4_CenterPad_DPadSouth,
		// Token: 0x0400086D RID: 2157
		k_EControllerActionOrigin_PS4_CenterPad_DPadWest,
		// Token: 0x0400086E RID: 2158
		k_EControllerActionOrigin_PS4_CenterPad_DPadEast,
		// Token: 0x0400086F RID: 2159
		k_EControllerActionOrigin_PS4_LeftTrigger_Pull,
		// Token: 0x04000870 RID: 2160
		k_EControllerActionOrigin_PS4_LeftTrigger_Click,
		// Token: 0x04000871 RID: 2161
		k_EControllerActionOrigin_PS4_RightTrigger_Pull,
		// Token: 0x04000872 RID: 2162
		k_EControllerActionOrigin_PS4_RightTrigger_Click,
		// Token: 0x04000873 RID: 2163
		k_EControllerActionOrigin_PS4_LeftStick_Move,
		// Token: 0x04000874 RID: 2164
		k_EControllerActionOrigin_PS4_LeftStick_Click,
		// Token: 0x04000875 RID: 2165
		k_EControllerActionOrigin_PS4_LeftStick_DPadNorth,
		// Token: 0x04000876 RID: 2166
		k_EControllerActionOrigin_PS4_LeftStick_DPadSouth,
		// Token: 0x04000877 RID: 2167
		k_EControllerActionOrigin_PS4_LeftStick_DPadWest,
		// Token: 0x04000878 RID: 2168
		k_EControllerActionOrigin_PS4_LeftStick_DPadEast,
		// Token: 0x04000879 RID: 2169
		k_EControllerActionOrigin_PS4_RightStick_Move,
		// Token: 0x0400087A RID: 2170
		k_EControllerActionOrigin_PS4_RightStick_Click,
		// Token: 0x0400087B RID: 2171
		k_EControllerActionOrigin_PS4_RightStick_DPadNorth,
		// Token: 0x0400087C RID: 2172
		k_EControllerActionOrigin_PS4_RightStick_DPadSouth,
		// Token: 0x0400087D RID: 2173
		k_EControllerActionOrigin_PS4_RightStick_DPadWest,
		// Token: 0x0400087E RID: 2174
		k_EControllerActionOrigin_PS4_RightStick_DPadEast,
		// Token: 0x0400087F RID: 2175
		k_EControllerActionOrigin_PS4_DPad_North,
		// Token: 0x04000880 RID: 2176
		k_EControllerActionOrigin_PS4_DPad_South,
		// Token: 0x04000881 RID: 2177
		k_EControllerActionOrigin_PS4_DPad_West,
		// Token: 0x04000882 RID: 2178
		k_EControllerActionOrigin_PS4_DPad_East,
		// Token: 0x04000883 RID: 2179
		k_EControllerActionOrigin_PS4_Gyro_Move,
		// Token: 0x04000884 RID: 2180
		k_EControllerActionOrigin_PS4_Gyro_Pitch,
		// Token: 0x04000885 RID: 2181
		k_EControllerActionOrigin_PS4_Gyro_Yaw,
		// Token: 0x04000886 RID: 2182
		k_EControllerActionOrigin_PS4_Gyro_Roll,
		// Token: 0x04000887 RID: 2183
		k_EControllerActionOrigin_XBoxOne_A,
		// Token: 0x04000888 RID: 2184
		k_EControllerActionOrigin_XBoxOne_B,
		// Token: 0x04000889 RID: 2185
		k_EControllerActionOrigin_XBoxOne_X,
		// Token: 0x0400088A RID: 2186
		k_EControllerActionOrigin_XBoxOne_Y,
		// Token: 0x0400088B RID: 2187
		k_EControllerActionOrigin_XBoxOne_LeftBumper,
		// Token: 0x0400088C RID: 2188
		k_EControllerActionOrigin_XBoxOne_RightBumper,
		// Token: 0x0400088D RID: 2189
		k_EControllerActionOrigin_XBoxOne_Menu,
		// Token: 0x0400088E RID: 2190
		k_EControllerActionOrigin_XBoxOne_View,
		// Token: 0x0400088F RID: 2191
		k_EControllerActionOrigin_XBoxOne_LeftTrigger_Pull,
		// Token: 0x04000890 RID: 2192
		k_EControllerActionOrigin_XBoxOne_LeftTrigger_Click,
		// Token: 0x04000891 RID: 2193
		k_EControllerActionOrigin_XBoxOne_RightTrigger_Pull,
		// Token: 0x04000892 RID: 2194
		k_EControllerActionOrigin_XBoxOne_RightTrigger_Click,
		// Token: 0x04000893 RID: 2195
		k_EControllerActionOrigin_XBoxOne_LeftStick_Move,
		// Token: 0x04000894 RID: 2196
		k_EControllerActionOrigin_XBoxOne_LeftStick_Click,
		// Token: 0x04000895 RID: 2197
		k_EControllerActionOrigin_XBoxOne_LeftStick_DPadNorth,
		// Token: 0x04000896 RID: 2198
		k_EControllerActionOrigin_XBoxOne_LeftStick_DPadSouth,
		// Token: 0x04000897 RID: 2199
		k_EControllerActionOrigin_XBoxOne_LeftStick_DPadWest,
		// Token: 0x04000898 RID: 2200
		k_EControllerActionOrigin_XBoxOne_LeftStick_DPadEast,
		// Token: 0x04000899 RID: 2201
		k_EControllerActionOrigin_XBoxOne_RightStick_Move,
		// Token: 0x0400089A RID: 2202
		k_EControllerActionOrigin_XBoxOne_RightStick_Click,
		// Token: 0x0400089B RID: 2203
		k_EControllerActionOrigin_XBoxOne_RightStick_DPadNorth,
		// Token: 0x0400089C RID: 2204
		k_EControllerActionOrigin_XBoxOne_RightStick_DPadSouth,
		// Token: 0x0400089D RID: 2205
		k_EControllerActionOrigin_XBoxOne_RightStick_DPadWest,
		// Token: 0x0400089E RID: 2206
		k_EControllerActionOrigin_XBoxOne_RightStick_DPadEast,
		// Token: 0x0400089F RID: 2207
		k_EControllerActionOrigin_XBoxOne_DPad_North,
		// Token: 0x040008A0 RID: 2208
		k_EControllerActionOrigin_XBoxOne_DPad_South,
		// Token: 0x040008A1 RID: 2209
		k_EControllerActionOrigin_XBoxOne_DPad_West,
		// Token: 0x040008A2 RID: 2210
		k_EControllerActionOrigin_XBoxOne_DPad_East,
		// Token: 0x040008A3 RID: 2211
		k_EControllerActionOrigin_XBox360_A,
		// Token: 0x040008A4 RID: 2212
		k_EControllerActionOrigin_XBox360_B,
		// Token: 0x040008A5 RID: 2213
		k_EControllerActionOrigin_XBox360_X,
		// Token: 0x040008A6 RID: 2214
		k_EControllerActionOrigin_XBox360_Y,
		// Token: 0x040008A7 RID: 2215
		k_EControllerActionOrigin_XBox360_LeftBumper,
		// Token: 0x040008A8 RID: 2216
		k_EControllerActionOrigin_XBox360_RightBumper,
		// Token: 0x040008A9 RID: 2217
		k_EControllerActionOrigin_XBox360_Start,
		// Token: 0x040008AA RID: 2218
		k_EControllerActionOrigin_XBox360_Back,
		// Token: 0x040008AB RID: 2219
		k_EControllerActionOrigin_XBox360_LeftTrigger_Pull,
		// Token: 0x040008AC RID: 2220
		k_EControllerActionOrigin_XBox360_LeftTrigger_Click,
		// Token: 0x040008AD RID: 2221
		k_EControllerActionOrigin_XBox360_RightTrigger_Pull,
		// Token: 0x040008AE RID: 2222
		k_EControllerActionOrigin_XBox360_RightTrigger_Click,
		// Token: 0x040008AF RID: 2223
		k_EControllerActionOrigin_XBox360_LeftStick_Move,
		// Token: 0x040008B0 RID: 2224
		k_EControllerActionOrigin_XBox360_LeftStick_Click,
		// Token: 0x040008B1 RID: 2225
		k_EControllerActionOrigin_XBox360_LeftStick_DPadNorth,
		// Token: 0x040008B2 RID: 2226
		k_EControllerActionOrigin_XBox360_LeftStick_DPadSouth,
		// Token: 0x040008B3 RID: 2227
		k_EControllerActionOrigin_XBox360_LeftStick_DPadWest,
		// Token: 0x040008B4 RID: 2228
		k_EControllerActionOrigin_XBox360_LeftStick_DPadEast,
		// Token: 0x040008B5 RID: 2229
		k_EControllerActionOrigin_XBox360_RightStick_Move,
		// Token: 0x040008B6 RID: 2230
		k_EControllerActionOrigin_XBox360_RightStick_Click,
		// Token: 0x040008B7 RID: 2231
		k_EControllerActionOrigin_XBox360_RightStick_DPadNorth,
		// Token: 0x040008B8 RID: 2232
		k_EControllerActionOrigin_XBox360_RightStick_DPadSouth,
		// Token: 0x040008B9 RID: 2233
		k_EControllerActionOrigin_XBox360_RightStick_DPadWest,
		// Token: 0x040008BA RID: 2234
		k_EControllerActionOrigin_XBox360_RightStick_DPadEast,
		// Token: 0x040008BB RID: 2235
		k_EControllerActionOrigin_XBox360_DPad_North,
		// Token: 0x040008BC RID: 2236
		k_EControllerActionOrigin_XBox360_DPad_South,
		// Token: 0x040008BD RID: 2237
		k_EControllerActionOrigin_XBox360_DPad_West,
		// Token: 0x040008BE RID: 2238
		k_EControllerActionOrigin_XBox360_DPad_East,
		// Token: 0x040008BF RID: 2239
		k_EControllerActionOrigin_SteamV2_A,
		// Token: 0x040008C0 RID: 2240
		k_EControllerActionOrigin_SteamV2_B,
		// Token: 0x040008C1 RID: 2241
		k_EControllerActionOrigin_SteamV2_X,
		// Token: 0x040008C2 RID: 2242
		k_EControllerActionOrigin_SteamV2_Y,
		// Token: 0x040008C3 RID: 2243
		k_EControllerActionOrigin_SteamV2_LeftBumper,
		// Token: 0x040008C4 RID: 2244
		k_EControllerActionOrigin_SteamV2_RightBumper,
		// Token: 0x040008C5 RID: 2245
		k_EControllerActionOrigin_SteamV2_LeftGrip,
		// Token: 0x040008C6 RID: 2246
		k_EControllerActionOrigin_SteamV2_RightGrip,
		// Token: 0x040008C7 RID: 2247
		k_EControllerActionOrigin_SteamV2_LeftGrip_Upper,
		// Token: 0x040008C8 RID: 2248
		k_EControllerActionOrigin_SteamV2_RightGrip_Upper,
		// Token: 0x040008C9 RID: 2249
		k_EControllerActionOrigin_SteamV2_LeftBumper_Pressure,
		// Token: 0x040008CA RID: 2250
		k_EControllerActionOrigin_SteamV2_RightBumper_Pressure,
		// Token: 0x040008CB RID: 2251
		k_EControllerActionOrigin_SteamV2_LeftGrip_Pressure,
		// Token: 0x040008CC RID: 2252
		k_EControllerActionOrigin_SteamV2_RightGrip_Pressure,
		// Token: 0x040008CD RID: 2253
		k_EControllerActionOrigin_SteamV2_LeftGrip_Upper_Pressure,
		// Token: 0x040008CE RID: 2254
		k_EControllerActionOrigin_SteamV2_RightGrip_Upper_Pressure,
		// Token: 0x040008CF RID: 2255
		k_EControllerActionOrigin_SteamV2_Start,
		// Token: 0x040008D0 RID: 2256
		k_EControllerActionOrigin_SteamV2_Back,
		// Token: 0x040008D1 RID: 2257
		k_EControllerActionOrigin_SteamV2_LeftPad_Touch,
		// Token: 0x040008D2 RID: 2258
		k_EControllerActionOrigin_SteamV2_LeftPad_Swipe,
		// Token: 0x040008D3 RID: 2259
		k_EControllerActionOrigin_SteamV2_LeftPad_Click,
		// Token: 0x040008D4 RID: 2260
		k_EControllerActionOrigin_SteamV2_LeftPad_Pressure,
		// Token: 0x040008D5 RID: 2261
		k_EControllerActionOrigin_SteamV2_LeftPad_DPadNorth,
		// Token: 0x040008D6 RID: 2262
		k_EControllerActionOrigin_SteamV2_LeftPad_DPadSouth,
		// Token: 0x040008D7 RID: 2263
		k_EControllerActionOrigin_SteamV2_LeftPad_DPadWest,
		// Token: 0x040008D8 RID: 2264
		k_EControllerActionOrigin_SteamV2_LeftPad_DPadEast,
		// Token: 0x040008D9 RID: 2265
		k_EControllerActionOrigin_SteamV2_RightPad_Touch,
		// Token: 0x040008DA RID: 2266
		k_EControllerActionOrigin_SteamV2_RightPad_Swipe,
		// Token: 0x040008DB RID: 2267
		k_EControllerActionOrigin_SteamV2_RightPad_Click,
		// Token: 0x040008DC RID: 2268
		k_EControllerActionOrigin_SteamV2_RightPad_Pressure,
		// Token: 0x040008DD RID: 2269
		k_EControllerActionOrigin_SteamV2_RightPad_DPadNorth,
		// Token: 0x040008DE RID: 2270
		k_EControllerActionOrigin_SteamV2_RightPad_DPadSouth,
		// Token: 0x040008DF RID: 2271
		k_EControllerActionOrigin_SteamV2_RightPad_DPadWest,
		// Token: 0x040008E0 RID: 2272
		k_EControllerActionOrigin_SteamV2_RightPad_DPadEast,
		// Token: 0x040008E1 RID: 2273
		k_EControllerActionOrigin_SteamV2_LeftTrigger_Pull,
		// Token: 0x040008E2 RID: 2274
		k_EControllerActionOrigin_SteamV2_LeftTrigger_Click,
		// Token: 0x040008E3 RID: 2275
		k_EControllerActionOrigin_SteamV2_RightTrigger_Pull,
		// Token: 0x040008E4 RID: 2276
		k_EControllerActionOrigin_SteamV2_RightTrigger_Click,
		// Token: 0x040008E5 RID: 2277
		k_EControllerActionOrigin_SteamV2_LeftStick_Move,
		// Token: 0x040008E6 RID: 2278
		k_EControllerActionOrigin_SteamV2_LeftStick_Click,
		// Token: 0x040008E7 RID: 2279
		k_EControllerActionOrigin_SteamV2_LeftStick_DPadNorth,
		// Token: 0x040008E8 RID: 2280
		k_EControllerActionOrigin_SteamV2_LeftStick_DPadSouth,
		// Token: 0x040008E9 RID: 2281
		k_EControllerActionOrigin_SteamV2_LeftStick_DPadWest,
		// Token: 0x040008EA RID: 2282
		k_EControllerActionOrigin_SteamV2_LeftStick_DPadEast,
		// Token: 0x040008EB RID: 2283
		k_EControllerActionOrigin_SteamV2_Gyro_Move,
		// Token: 0x040008EC RID: 2284
		k_EControllerActionOrigin_SteamV2_Gyro_Pitch,
		// Token: 0x040008ED RID: 2285
		k_EControllerActionOrigin_SteamV2_Gyro_Yaw,
		// Token: 0x040008EE RID: 2286
		k_EControllerActionOrigin_SteamV2_Gyro_Roll,
		// Token: 0x040008EF RID: 2287
		k_EControllerActionOrigin_Count
	}
}
