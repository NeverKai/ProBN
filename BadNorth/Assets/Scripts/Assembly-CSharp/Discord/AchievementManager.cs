using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020001F0 RID: 496
	public class AchievementManager
	{
		// Token: 0x06000B10 RID: 2832 RVA: 0x0001FF84 File Offset: 0x0001E384
		internal AchievementManager(IntPtr ptr, IntPtr eventsPtr, ref AchievementManager.FFIEvents events)
		{
			if (eventsPtr == IntPtr.Zero)
			{
				throw new ResultException(Result.InternalError);
			}
			this.InitEvents(eventsPtr, ref events);
			this.MethodsPtr = ptr;
			if (this.MethodsPtr == IntPtr.Zero)
			{
				throw new ResultException(Result.InternalError);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0001FFD9 File Offset: 0x0001E3D9
		private AchievementManager.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(AchievementManager.FFIMethods));
				}
				return (AchievementManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000B12 RID: 2834 RVA: 0x0002000C File Offset: 0x0001E40C
		// (remove) Token: 0x06000B13 RID: 2835 RVA: 0x00020044 File Offset: 0x0001E444
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AchievementManager.UserAchievementUpdateHandler OnUserAchievementUpdate;

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002007A File Offset: 0x0001E47A
		private void InitEvents(IntPtr eventsPtr, ref AchievementManager.FFIEvents events)
		{
			events.OnUserAchievementUpdate = delegate(IntPtr ptr, ref UserAchievement userAchievement)
			{
				if (this.OnUserAchievementUpdate != null)
				{
					this.OnUserAchievementUpdate(ref userAchievement);
				}
			};
			Marshal.StructureToPtr(events, eventsPtr, false);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000200A0 File Offset: 0x0001E4A0
		public void SetUserAchievement(long achievementId, long percentComplete, AchievementManager.SetUserAchievementHandler callback)
		{
			AchievementManager.FFIMethods.SetUserAchievementCallback setUserAchievementCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.SetUserAchievement(this.MethodsPtr, achievementId, percentComplete, Utility.Retain<AchievementManager.FFIMethods.SetUserAchievementCallback>(setUserAchievementCallback), setUserAchievementCallback);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000200EC File Offset: 0x0001E4EC
		public void FetchUserAchievements(AchievementManager.FetchUserAchievementsHandler callback)
		{
			AchievementManager.FFIMethods.FetchUserAchievementsCallback fetchUserAchievementsCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.FetchUserAchievements(this.MethodsPtr, Utility.Retain<AchievementManager.FFIMethods.FetchUserAchievementsCallback>(fetchUserAchievementsCallback), fetchUserAchievementsCallback);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00020134 File Offset: 0x0001E534
		public int CountUserAchievements()
		{
			int result = 0;
			this.Methods.CountUserAchievements(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00020160 File Offset: 0x0001E560
		public UserAchievement GetUserAchievement(long userAchievementId)
		{
			UserAchievement result = default(UserAchievement);
			Result result2 = this.Methods.GetUserAchievement(this.MethodsPtr, userAchievementId, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000201A0 File Offset: 0x0001E5A0
		public UserAchievement GetUserAchievementAt(int index)
		{
			UserAchievement result = default(UserAchievement);
			Result result2 = this.Methods.GetUserAchievementAt(this.MethodsPtr, index, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x040004E2 RID: 1250
		private IntPtr MethodsPtr;

		// Token: 0x040004E3 RID: 1251
		private object MethodsStructure;

		// Token: 0x020001F1 RID: 497
		internal struct FFIEvents
		{
			// Token: 0x040004E5 RID: 1253
			internal AchievementManager.FFIEvents.UserAchievementUpdateHandler OnUserAchievementUpdate;

			// Token: 0x020001F2 RID: 498
			// (Invoke) Token: 0x06000B1C RID: 2844
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void UserAchievementUpdateHandler(IntPtr ptr, ref UserAchievement userAchievement);
		}

		// Token: 0x020001F3 RID: 499
		internal struct FFIMethods
		{
			// Token: 0x040004E6 RID: 1254
			internal AchievementManager.FFIMethods.SetUserAchievementMethod SetUserAchievement;

			// Token: 0x040004E7 RID: 1255
			internal AchievementManager.FFIMethods.FetchUserAchievementsMethod FetchUserAchievements;

			// Token: 0x040004E8 RID: 1256
			internal AchievementManager.FFIMethods.CountUserAchievementsMethod CountUserAchievements;

			// Token: 0x040004E9 RID: 1257
			internal AchievementManager.FFIMethods.GetUserAchievementMethod GetUserAchievement;

			// Token: 0x040004EA RID: 1258
			internal AchievementManager.FFIMethods.GetUserAchievementAtMethod GetUserAchievementAt;

			// Token: 0x020001F4 RID: 500
			// (Invoke) Token: 0x06000B20 RID: 2848
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SetUserAchievementCallback(IntPtr ptr, Result result);

			// Token: 0x020001F5 RID: 501
			// (Invoke) Token: 0x06000B24 RID: 2852
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SetUserAchievementMethod(IntPtr methodsPtr, long achievementId, long percentComplete, IntPtr callbackData, AchievementManager.FFIMethods.SetUserAchievementCallback callback);

			// Token: 0x020001F6 RID: 502
			// (Invoke) Token: 0x06000B28 RID: 2856
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void FetchUserAchievementsCallback(IntPtr ptr, Result result);

			// Token: 0x020001F7 RID: 503
			// (Invoke) Token: 0x06000B2C RID: 2860
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void FetchUserAchievementsMethod(IntPtr methodsPtr, IntPtr callbackData, AchievementManager.FFIMethods.FetchUserAchievementsCallback callback);

			// Token: 0x020001F8 RID: 504
			// (Invoke) Token: 0x06000B30 RID: 2864
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void CountUserAchievementsMethod(IntPtr methodsPtr, ref int count);

			// Token: 0x020001F9 RID: 505
			// (Invoke) Token: 0x06000B34 RID: 2868
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetUserAchievementMethod(IntPtr methodsPtr, long userAchievementId, ref UserAchievement userAchievement);

			// Token: 0x020001FA RID: 506
			// (Invoke) Token: 0x06000B38 RID: 2872
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetUserAchievementAtMethod(IntPtr methodsPtr, int index, ref UserAchievement userAchievement);
		}

		// Token: 0x020001FB RID: 507
		// (Invoke) Token: 0x06000B3C RID: 2876
		public delegate void SetUserAchievementHandler(Result result);

		// Token: 0x020001FC RID: 508
		// (Invoke) Token: 0x06000B40 RID: 2880
		public delegate void FetchUserAchievementsHandler(Result result);

		// Token: 0x020001FD RID: 509
		// (Invoke) Token: 0x06000B44 RID: 2884
		public delegate void UserAchievementUpdateHandler(ref UserAchievement userAchievement);
	}
}
