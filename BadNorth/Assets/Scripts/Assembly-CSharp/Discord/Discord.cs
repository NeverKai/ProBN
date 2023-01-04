using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000EA RID: 234
	public class Discord : IDisposable
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x0001C900 File Offset: 0x0001AD00
		public Discord(long clientId, ulong flags)
		{
			Discord.FFICreateParams fficreateParams;
			fficreateParams.ClientId = clientId;
			fficreateParams.Flags = flags;
			this.Events = default(Discord.FFIEvents);
			this.EventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.Events));
			fficreateParams.Events = this.EventsPtr;
			fficreateParams.EventData = (IntPtr)0;
			this.ApplicationEvents = default(ApplicationManager.FFIEvents);
			this.ApplicationEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.ApplicationEvents));
			fficreateParams.ApplicationEvents = this.ApplicationEventsPtr;
			fficreateParams.ApplicationVersion = 1U;
			this.UserEvents = default(UserManager.FFIEvents);
			this.UserEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.UserEvents));
			fficreateParams.UserEvents = this.UserEventsPtr;
			fficreateParams.UserVersion = 1U;
			this.ImageEvents = default(ImageManager.FFIEvents);
			this.ImageEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.ImageEvents));
			fficreateParams.ImageEvents = this.ImageEventsPtr;
			fficreateParams.ImageVersion = 1U;
			this.ActivityEvents = default(ActivityManager.FFIEvents);
			this.ActivityEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.ActivityEvents));
			fficreateParams.ActivityEvents = this.ActivityEventsPtr;
			fficreateParams.ActivityVersion = 1U;
			this.RelationshipEvents = default(RelationshipManager.FFIEvents);
			this.RelationshipEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.RelationshipEvents));
			fficreateParams.RelationshipEvents = this.RelationshipEventsPtr;
			fficreateParams.RelationshipVersion = 1U;
			this.LobbyEvents = default(LobbyManager.FFIEvents);
			this.LobbyEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.LobbyEvents));
			fficreateParams.LobbyEvents = this.LobbyEventsPtr;
			fficreateParams.LobbyVersion = 1U;
			this.NetworkEvents = default(NetworkManager.FFIEvents);
			this.NetworkEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.NetworkEvents));
			fficreateParams.NetworkEvents = this.NetworkEventsPtr;
			fficreateParams.NetworkVersion = 1U;
			this.OverlayEvents = default(OverlayManager.FFIEvents);
			this.OverlayEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.OverlayEvents));
			fficreateParams.OverlayEvents = this.OverlayEventsPtr;
			fficreateParams.OverlayVersion = 1U;
			this.StorageEvents = default(StorageManager.FFIEvents);
			this.StorageEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.StorageEvents));
			fficreateParams.StorageEvents = this.StorageEventsPtr;
			fficreateParams.StorageVersion = 1U;
			this.StoreEvents = default(StoreManager.FFIEvents);
			this.StoreEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.StoreEvents));
			fficreateParams.StoreEvents = this.StoreEventsPtr;
			fficreateParams.StoreVersion = 1U;
			this.VoiceEvents = default(VoiceManager.FFIEvents);
			this.VoiceEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.VoiceEvents));
			fficreateParams.VoiceEvents = this.VoiceEventsPtr;
			fficreateParams.VoiceVersion = 1U;
			this.AchievementEvents = default(AchievementManager.FFIEvents);
			this.AchievementEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.AchievementEvents));
			fficreateParams.AchievementEvents = this.AchievementEventsPtr;
			fficreateParams.AchievementVersion = 1U;
			this.InitEvents(this.EventsPtr, ref this.Events);
			Result result = Discord.DiscordCreate(2U, ref fficreateParams, out this.MethodsPtr);
			if (result != Result.Ok)
			{
				this.Dispose();
				throw new ResultException(result);
			}
		}

		// Token: 0x060006BF RID: 1727
		[DllImport("discord_game_sdk", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		private static extern Result DiscordCreate(uint version, ref Discord.FFICreateParams createParams, out IntPtr manager);

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001CC9C File Offset: 0x0001B09C
		private Discord.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(Discord.FFIMethods));
				}
				return (Discord.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001CCCF File Offset: 0x0001B0CF
		private void InitEvents(IntPtr eventsPtr, ref Discord.FFIEvents events)
		{
			Marshal.StructureToPtr(events, eventsPtr, false);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001CCE4 File Offset: 0x0001B0E4
		public void Dispose()
		{
			if (this.MethodsPtr != IntPtr.Zero)
			{
				this.Methods.Destroy(this.MethodsPtr);
			}
			Marshal.FreeHGlobal(this.EventsPtr);
			Marshal.FreeHGlobal(this.ApplicationEventsPtr);
			Marshal.FreeHGlobal(this.UserEventsPtr);
			Marshal.FreeHGlobal(this.ImageEventsPtr);
			Marshal.FreeHGlobal(this.ActivityEventsPtr);
			Marshal.FreeHGlobal(this.RelationshipEventsPtr);
			Marshal.FreeHGlobal(this.LobbyEventsPtr);
			Marshal.FreeHGlobal(this.NetworkEventsPtr);
			Marshal.FreeHGlobal(this.OverlayEventsPtr);
			Marshal.FreeHGlobal(this.StorageEventsPtr);
			Marshal.FreeHGlobal(this.StoreEventsPtr);
			Marshal.FreeHGlobal(this.VoiceEventsPtr);
			Marshal.FreeHGlobal(this.AchievementEventsPtr);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001CDB0 File Offset: 0x0001B1B0
		public void RunCallbacks()
		{
			Result result = this.Methods.RunCallbacks(this.MethodsPtr);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001CDE4 File Offset: 0x0001B1E4
		public void SetLogHook(LogLevel minLevel, Discord.SetLogHookHandler callback)
		{
			this.setLogHook = delegate(IntPtr ptr, LogLevel level, string message)
			{
				callback(level, message);
			};
			this.Methods.SetLogHook(this.MethodsPtr, minLevel, IntPtr.Zero, this.setLogHook);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001CE38 File Offset: 0x0001B238
		public ApplicationManager GetApplicationManager()
		{
			if (this.ApplicationManagerInstance == null)
			{
				this.ApplicationManagerInstance = new ApplicationManager(this.Methods.GetApplicationManager(this.MethodsPtr), this.ApplicationEventsPtr, ref this.ApplicationEvents);
			}
			return this.ApplicationManagerInstance;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001CE88 File Offset: 0x0001B288
		public UserManager GetUserManager()
		{
			if (this.UserManagerInstance == null)
			{
				this.UserManagerInstance = new UserManager(this.Methods.GetUserManager(this.MethodsPtr), this.UserEventsPtr, ref this.UserEvents);
			}
			return this.UserManagerInstance;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001CED8 File Offset: 0x0001B2D8
		public ImageManager GetImageManager()
		{
			if (this.ImageManagerInstance == null)
			{
				this.ImageManagerInstance = new ImageManager(this.Methods.GetImageManager(this.MethodsPtr), this.ImageEventsPtr, ref this.ImageEvents);
			}
			return this.ImageManagerInstance;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001CF28 File Offset: 0x0001B328
		public ActivityManager GetActivityManager()
		{
			if (this.ActivityManagerInstance == null)
			{
				this.ActivityManagerInstance = new ActivityManager(this.Methods.GetActivityManager(this.MethodsPtr), this.ActivityEventsPtr, ref this.ActivityEvents);
			}
			return this.ActivityManagerInstance;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001CF78 File Offset: 0x0001B378
		public RelationshipManager GetRelationshipManager()
		{
			if (this.RelationshipManagerInstance == null)
			{
				this.RelationshipManagerInstance = new RelationshipManager(this.Methods.GetRelationshipManager(this.MethodsPtr), this.RelationshipEventsPtr, ref this.RelationshipEvents);
			}
			return this.RelationshipManagerInstance;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001CFC8 File Offset: 0x0001B3C8
		public LobbyManager GetLobbyManager()
		{
			if (this.LobbyManagerInstance == null)
			{
				this.LobbyManagerInstance = new LobbyManager(this.Methods.GetLobbyManager(this.MethodsPtr), this.LobbyEventsPtr, ref this.LobbyEvents);
			}
			return this.LobbyManagerInstance;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001D018 File Offset: 0x0001B418
		public NetworkManager GetNetworkManager()
		{
			if (this.NetworkManagerInstance == null)
			{
				this.NetworkManagerInstance = new NetworkManager(this.Methods.GetNetworkManager(this.MethodsPtr), this.NetworkEventsPtr, ref this.NetworkEvents);
			}
			return this.NetworkManagerInstance;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001D068 File Offset: 0x0001B468
		public OverlayManager GetOverlayManager()
		{
			if (this.OverlayManagerInstance == null)
			{
				this.OverlayManagerInstance = new OverlayManager(this.Methods.GetOverlayManager(this.MethodsPtr), this.OverlayEventsPtr, ref this.OverlayEvents);
			}
			return this.OverlayManagerInstance;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001D0B8 File Offset: 0x0001B4B8
		public StorageManager GetStorageManager()
		{
			if (this.StorageManagerInstance == null)
			{
				this.StorageManagerInstance = new StorageManager(this.Methods.GetStorageManager(this.MethodsPtr), this.StorageEventsPtr, ref this.StorageEvents);
			}
			return this.StorageManagerInstance;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001D108 File Offset: 0x0001B508
		public StoreManager GetStoreManager()
		{
			if (this.StoreManagerInstance == null)
			{
				this.StoreManagerInstance = new StoreManager(this.Methods.GetStoreManager(this.MethodsPtr), this.StoreEventsPtr, ref this.StoreEvents);
			}
			return this.StoreManagerInstance;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001D158 File Offset: 0x0001B558
		public VoiceManager GetVoiceManager()
		{
			if (this.VoiceManagerInstance == null)
			{
				this.VoiceManagerInstance = new VoiceManager(this.Methods.GetVoiceManager(this.MethodsPtr), this.VoiceEventsPtr, ref this.VoiceEvents);
			}
			return this.VoiceManagerInstance;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001D1A8 File Offset: 0x0001B5A8
		public AchievementManager GetAchievementManager()
		{
			if (this.AchievementManagerInstance == null)
			{
				this.AchievementManagerInstance = new AchievementManager(this.Methods.GetAchievementManager(this.MethodsPtr), this.AchievementEventsPtr, ref this.AchievementEvents);
			}
			return this.AchievementManagerInstance;
		}

		// Token: 0x040003EF RID: 1007
		private IntPtr EventsPtr;

		// Token: 0x040003F0 RID: 1008
		private Discord.FFIEvents Events;

		// Token: 0x040003F1 RID: 1009
		private IntPtr ApplicationEventsPtr;

		// Token: 0x040003F2 RID: 1010
		private ApplicationManager.FFIEvents ApplicationEvents;

		// Token: 0x040003F3 RID: 1011
		private ApplicationManager ApplicationManagerInstance;

		// Token: 0x040003F4 RID: 1012
		private IntPtr UserEventsPtr;

		// Token: 0x040003F5 RID: 1013
		private UserManager.FFIEvents UserEvents;

		// Token: 0x040003F6 RID: 1014
		private UserManager UserManagerInstance;

		// Token: 0x040003F7 RID: 1015
		private IntPtr ImageEventsPtr;

		// Token: 0x040003F8 RID: 1016
		private ImageManager.FFIEvents ImageEvents;

		// Token: 0x040003F9 RID: 1017
		private ImageManager ImageManagerInstance;

		// Token: 0x040003FA RID: 1018
		private IntPtr ActivityEventsPtr;

		// Token: 0x040003FB RID: 1019
		private ActivityManager.FFIEvents ActivityEvents;

		// Token: 0x040003FC RID: 1020
		private ActivityManager ActivityManagerInstance;

		// Token: 0x040003FD RID: 1021
		private IntPtr RelationshipEventsPtr;

		// Token: 0x040003FE RID: 1022
		private RelationshipManager.FFIEvents RelationshipEvents;

		// Token: 0x040003FF RID: 1023
		private RelationshipManager RelationshipManagerInstance;

		// Token: 0x04000400 RID: 1024
		private IntPtr LobbyEventsPtr;

		// Token: 0x04000401 RID: 1025
		private LobbyManager.FFIEvents LobbyEvents;

		// Token: 0x04000402 RID: 1026
		private LobbyManager LobbyManagerInstance;

		// Token: 0x04000403 RID: 1027
		private IntPtr NetworkEventsPtr;

		// Token: 0x04000404 RID: 1028
		private NetworkManager.FFIEvents NetworkEvents;

		// Token: 0x04000405 RID: 1029
		private NetworkManager NetworkManagerInstance;

		// Token: 0x04000406 RID: 1030
		private IntPtr OverlayEventsPtr;

		// Token: 0x04000407 RID: 1031
		private OverlayManager.FFIEvents OverlayEvents;

		// Token: 0x04000408 RID: 1032
		private OverlayManager OverlayManagerInstance;

		// Token: 0x04000409 RID: 1033
		private IntPtr StorageEventsPtr;

		// Token: 0x0400040A RID: 1034
		private StorageManager.FFIEvents StorageEvents;

		// Token: 0x0400040B RID: 1035
		private StorageManager StorageManagerInstance;

		// Token: 0x0400040C RID: 1036
		private IntPtr StoreEventsPtr;

		// Token: 0x0400040D RID: 1037
		private StoreManager.FFIEvents StoreEvents;

		// Token: 0x0400040E RID: 1038
		private StoreManager StoreManagerInstance;

		// Token: 0x0400040F RID: 1039
		private IntPtr VoiceEventsPtr;

		// Token: 0x04000410 RID: 1040
		private VoiceManager.FFIEvents VoiceEvents;

		// Token: 0x04000411 RID: 1041
		private VoiceManager VoiceManagerInstance;

		// Token: 0x04000412 RID: 1042
		private IntPtr AchievementEventsPtr;

		// Token: 0x04000413 RID: 1043
		private AchievementManager.FFIEvents AchievementEvents;

		// Token: 0x04000414 RID: 1044
		private AchievementManager AchievementManagerInstance;

		// Token: 0x04000415 RID: 1045
		private IntPtr MethodsPtr;

		// Token: 0x04000416 RID: 1046
		private object MethodsStructure;

		// Token: 0x04000417 RID: 1047
		private Discord.FFIMethods.SetLogHookCallback setLogHook;

		// Token: 0x020000EB RID: 235
		internal struct FFIEvents
		{
		}

		// Token: 0x020000EC RID: 236
		internal struct FFIMethods
		{
			// Token: 0x04000418 RID: 1048
			internal Discord.FFIMethods.DestroyHandler Destroy;

			// Token: 0x04000419 RID: 1049
			internal Discord.FFIMethods.RunCallbacksMethod RunCallbacks;

			// Token: 0x0400041A RID: 1050
			internal Discord.FFIMethods.SetLogHookMethod SetLogHook;

			// Token: 0x0400041B RID: 1051
			internal Discord.FFIMethods.GetApplicationManagerMethod GetApplicationManager;

			// Token: 0x0400041C RID: 1052
			internal Discord.FFIMethods.GetUserManagerMethod GetUserManager;

			// Token: 0x0400041D RID: 1053
			internal Discord.FFIMethods.GetImageManagerMethod GetImageManager;

			// Token: 0x0400041E RID: 1054
			internal Discord.FFIMethods.GetActivityManagerMethod GetActivityManager;

			// Token: 0x0400041F RID: 1055
			internal Discord.FFIMethods.GetRelationshipManagerMethod GetRelationshipManager;

			// Token: 0x04000420 RID: 1056
			internal Discord.FFIMethods.GetLobbyManagerMethod GetLobbyManager;

			// Token: 0x04000421 RID: 1057
			internal Discord.FFIMethods.GetNetworkManagerMethod GetNetworkManager;

			// Token: 0x04000422 RID: 1058
			internal Discord.FFIMethods.GetOverlayManagerMethod GetOverlayManager;

			// Token: 0x04000423 RID: 1059
			internal Discord.FFIMethods.GetStorageManagerMethod GetStorageManager;

			// Token: 0x04000424 RID: 1060
			internal Discord.FFIMethods.GetStoreManagerMethod GetStoreManager;

			// Token: 0x04000425 RID: 1061
			internal Discord.FFIMethods.GetVoiceManagerMethod GetVoiceManager;

			// Token: 0x04000426 RID: 1062
			internal Discord.FFIMethods.GetAchievementManagerMethod GetAchievementManager;

			// Token: 0x020000ED RID: 237
			// (Invoke) Token: 0x060006D2 RID: 1746
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void DestroyHandler(IntPtr MethodsPtr);

			// Token: 0x020000EE RID: 238
			// (Invoke) Token: 0x060006D6 RID: 1750
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result RunCallbacksMethod(IntPtr methodsPtr);

			// Token: 0x020000EF RID: 239
			// (Invoke) Token: 0x060006DA RID: 1754
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SetLogHookCallback(IntPtr ptr, LogLevel level, [MarshalAs(UnmanagedType.LPStr)] string message);

			// Token: 0x020000F0 RID: 240
			// (Invoke) Token: 0x060006DE RID: 1758
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SetLogHookMethod(IntPtr methodsPtr, LogLevel minLevel, IntPtr callbackData, Discord.FFIMethods.SetLogHookCallback callback);

			// Token: 0x020000F1 RID: 241
			// (Invoke) Token: 0x060006E2 RID: 1762
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate IntPtr GetApplicationManagerMethod(IntPtr discordPtr);

			// Token: 0x020000F2 RID: 242
			// (Invoke) Token: 0x060006E6 RID: 1766
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate IntPtr GetUserManagerMethod(IntPtr discordPtr);

			// Token: 0x020000F3 RID: 243
			// (Invoke) Token: 0x060006EA RID: 1770
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate IntPtr GetImageManagerMethod(IntPtr discordPtr);

			// Token: 0x020000F4 RID: 244
			// (Invoke) Token: 0x060006EE RID: 1774
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate IntPtr GetActivityManagerMethod(IntPtr discordPtr);

			// Token: 0x020000F5 RID: 245
			// (Invoke) Token: 0x060006F2 RID: 1778
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate IntPtr GetRelationshipManagerMethod(IntPtr discordPtr);

			// Token: 0x020000F6 RID: 246
			// (Invoke) Token: 0x060006F6 RID: 1782
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate IntPtr GetLobbyManagerMethod(IntPtr discordPtr);

			// Token: 0x020000F7 RID: 247
			// (Invoke) Token: 0x060006FA RID: 1786
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate IntPtr GetNetworkManagerMethod(IntPtr discordPtr);

			// Token: 0x020000F8 RID: 248
			// (Invoke) Token: 0x060006FE RID: 1790
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate IntPtr GetOverlayManagerMethod(IntPtr discordPtr);

			// Token: 0x020000F9 RID: 249
			// (Invoke) Token: 0x06000702 RID: 1794
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate IntPtr GetStorageManagerMethod(IntPtr discordPtr);

			// Token: 0x020000FA RID: 250
			// (Invoke) Token: 0x06000706 RID: 1798
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate IntPtr GetStoreManagerMethod(IntPtr discordPtr);

			// Token: 0x020000FB RID: 251
			// (Invoke) Token: 0x0600070A RID: 1802
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate IntPtr GetVoiceManagerMethod(IntPtr discordPtr);

			// Token: 0x020000FC RID: 252
			// (Invoke) Token: 0x0600070E RID: 1806
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate IntPtr GetAchievementManagerMethod(IntPtr discordPtr);
		}

		// Token: 0x020000FD RID: 253
		internal struct FFICreateParams
		{
			// Token: 0x04000427 RID: 1063
			internal long ClientId;

			// Token: 0x04000428 RID: 1064
			internal ulong Flags;

			// Token: 0x04000429 RID: 1065
			internal IntPtr Events;

			// Token: 0x0400042A RID: 1066
			internal IntPtr EventData;

			// Token: 0x0400042B RID: 1067
			internal IntPtr ApplicationEvents;

			// Token: 0x0400042C RID: 1068
			internal uint ApplicationVersion;

			// Token: 0x0400042D RID: 1069
			internal IntPtr UserEvents;

			// Token: 0x0400042E RID: 1070
			internal uint UserVersion;

			// Token: 0x0400042F RID: 1071
			internal IntPtr ImageEvents;

			// Token: 0x04000430 RID: 1072
			internal uint ImageVersion;

			// Token: 0x04000431 RID: 1073
			internal IntPtr ActivityEvents;

			// Token: 0x04000432 RID: 1074
			internal uint ActivityVersion;

			// Token: 0x04000433 RID: 1075
			internal IntPtr RelationshipEvents;

			// Token: 0x04000434 RID: 1076
			internal uint RelationshipVersion;

			// Token: 0x04000435 RID: 1077
			internal IntPtr LobbyEvents;

			// Token: 0x04000436 RID: 1078
			internal uint LobbyVersion;

			// Token: 0x04000437 RID: 1079
			internal IntPtr NetworkEvents;

			// Token: 0x04000438 RID: 1080
			internal uint NetworkVersion;

			// Token: 0x04000439 RID: 1081
			internal IntPtr OverlayEvents;

			// Token: 0x0400043A RID: 1082
			internal uint OverlayVersion;

			// Token: 0x0400043B RID: 1083
			internal IntPtr StorageEvents;

			// Token: 0x0400043C RID: 1084
			internal uint StorageVersion;

			// Token: 0x0400043D RID: 1085
			internal IntPtr StoreEvents;

			// Token: 0x0400043E RID: 1086
			internal uint StoreVersion;

			// Token: 0x0400043F RID: 1087
			internal IntPtr VoiceEvents;

			// Token: 0x04000440 RID: 1088
			internal uint VoiceVersion;

			// Token: 0x04000441 RID: 1089
			internal IntPtr AchievementEvents;

			// Token: 0x04000442 RID: 1090
			internal uint AchievementVersion;
		}

		// Token: 0x020000FE RID: 254
		// (Invoke) Token: 0x06000712 RID: 1810
		public delegate void SetLogHookHandler(LogLevel level, string message);
	}
}
