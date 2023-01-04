using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000AF RID: 175
	public class ActivityManager
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0001BEB4 File Offset: 0x0001A2B4
		internal ActivityManager(IntPtr ptr, IntPtr eventsPtr, ref ActivityManager.FFIEvents events)
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

		// Token: 0x06000663 RID: 1635 RVA: 0x0001BF09 File Offset: 0x0001A309
		public void RegisterCommand()
		{
			this.RegisterCommand(null);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001BF12 File Offset: 0x0001A312
		private ActivityManager.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(ActivityManager.FFIMethods));
				}
				return (ActivityManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000665 RID: 1637 RVA: 0x0001BF48 File Offset: 0x0001A348
		// (remove) Token: 0x06000666 RID: 1638 RVA: 0x0001BF80 File Offset: 0x0001A380
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ActivityManager.ActivityJoinHandler OnActivityJoin;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000667 RID: 1639 RVA: 0x0001BFB8 File Offset: 0x0001A3B8
		// (remove) Token: 0x06000668 RID: 1640 RVA: 0x0001BFF0 File Offset: 0x0001A3F0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ActivityManager.ActivitySpectateHandler OnActivitySpectate;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000669 RID: 1641 RVA: 0x0001C028 File Offset: 0x0001A428
		// (remove) Token: 0x0600066A RID: 1642 RVA: 0x0001C060 File Offset: 0x0001A460
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ActivityManager.ActivityJoinRequestHandler OnActivityJoinRequest;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x0600066B RID: 1643 RVA: 0x0001C098 File Offset: 0x0001A498
		// (remove) Token: 0x0600066C RID: 1644 RVA: 0x0001C0D0 File Offset: 0x0001A4D0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ActivityManager.ActivityInviteHandler OnActivityInvite;

		// Token: 0x0600066D RID: 1645 RVA: 0x0001C108 File Offset: 0x0001A508
		private void InitEvents(IntPtr eventsPtr, ref ActivityManager.FFIEvents events)
		{
			events.OnActivityJoin = delegate(IntPtr ptr, string secret)
			{
				if (this.OnActivityJoin != null)
				{
					this.OnActivityJoin(secret);
				}
			};
			events.OnActivitySpectate = delegate(IntPtr ptr, string secret)
			{
				if (this.OnActivitySpectate != null)
				{
					this.OnActivitySpectate(secret);
				}
			};
			events.OnActivityJoinRequest = delegate(IntPtr ptr, ref User user)
			{
				if (this.OnActivityJoinRequest != null)
				{
					this.OnActivityJoinRequest(ref user);
				}
			};
			events.OnActivityInvite = delegate(IntPtr ptr, ActivityActionType type, ref User user, ref Activity activity)
			{
				if (this.OnActivityInvite != null)
				{
					this.OnActivityInvite(type, ref user, ref activity);
				}
			};
			Marshal.StructureToPtr(events, eventsPtr, false);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001C170 File Offset: 0x0001A570
		public void RegisterCommand(string command)
		{
			Result result = this.Methods.RegisterCommand(this.MethodsPtr, command);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001C1A8 File Offset: 0x0001A5A8
		public void RegisterSteam(uint steamId)
		{
			Result result = this.Methods.RegisterSteam(this.MethodsPtr, steamId);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001C1E0 File Offset: 0x0001A5E0
		public void UpdateActivity(Activity activity, ActivityManager.UpdateActivityHandler callback)
		{
			ActivityManager.FFIMethods.UpdateActivityCallback updateActivityCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.UpdateActivity(this.MethodsPtr, ref activity, Utility.Retain<ActivityManager.FFIMethods.UpdateActivityCallback>(updateActivityCallback), updateActivityCallback);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001C22C File Offset: 0x0001A62C
		public void ClearActivity(ActivityManager.ClearActivityHandler callback)
		{
			ActivityManager.FFIMethods.ClearActivityCallback clearActivityCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.ClearActivity(this.MethodsPtr, Utility.Retain<ActivityManager.FFIMethods.ClearActivityCallback>(clearActivityCallback), clearActivityCallback);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001C274 File Offset: 0x0001A674
		public void SendRequestReply(long userId, ActivityJoinRequestReply reply, ActivityManager.SendRequestReplyHandler callback)
		{
			ActivityManager.FFIMethods.SendRequestReplyCallback sendRequestReplyCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.SendRequestReply(this.MethodsPtr, userId, reply, Utility.Retain<ActivityManager.FFIMethods.SendRequestReplyCallback>(sendRequestReplyCallback), sendRequestReplyCallback);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001C2C0 File Offset: 0x0001A6C0
		public void SendInvite(long userId, ActivityActionType type, string content, ActivityManager.SendInviteHandler callback)
		{
			ActivityManager.FFIMethods.SendInviteCallback sendInviteCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.SendInvite(this.MethodsPtr, userId, type, content, Utility.Retain<ActivityManager.FFIMethods.SendInviteCallback>(sendInviteCallback), sendInviteCallback);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001C30C File Offset: 0x0001A70C
		public void AcceptInvite(long userId, ActivityManager.AcceptInviteHandler callback)
		{
			ActivityManager.FFIMethods.AcceptInviteCallback acceptInviteCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.AcceptInvite(this.MethodsPtr, userId, Utility.Retain<ActivityManager.FFIMethods.AcceptInviteCallback>(acceptInviteCallback), acceptInviteCallback);
		}

		// Token: 0x04000319 RID: 793
		private IntPtr MethodsPtr;

		// Token: 0x0400031A RID: 794
		private object MethodsStructure;

		// Token: 0x02000120 RID: 288
		internal struct FFIEvents
		{
			// Token: 0x04000457 RID: 1111
			internal ActivityManager.FFIEvents.ActivityJoinHandler OnActivityJoin;

			// Token: 0x04000458 RID: 1112
			internal ActivityManager.FFIEvents.ActivitySpectateHandler OnActivitySpectate;

			// Token: 0x04000459 RID: 1113
			internal ActivityManager.FFIEvents.ActivityJoinRequestHandler OnActivityJoinRequest;

			// Token: 0x0400045A RID: 1114
			internal ActivityManager.FFIEvents.ActivityInviteHandler OnActivityInvite;

			// Token: 0x02000121 RID: 289
			// (Invoke) Token: 0x06000791 RID: 1937
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ActivityJoinHandler(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)] string secret);

			// Token: 0x02000122 RID: 290
			// (Invoke) Token: 0x06000795 RID: 1941
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ActivitySpectateHandler(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)] string secret);

			// Token: 0x02000123 RID: 291
			// (Invoke) Token: 0x06000799 RID: 1945
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ActivityJoinRequestHandler(IntPtr ptr, ref User user);

			// Token: 0x02000124 RID: 292
			// (Invoke) Token: 0x0600079D RID: 1949
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ActivityInviteHandler(IntPtr ptr, ActivityActionType type, ref User user, ref Activity activity);
		}

		// Token: 0x02000125 RID: 293
		internal struct FFIMethods
		{
			// Token: 0x0400045B RID: 1115
			internal ActivityManager.FFIMethods.RegisterCommandMethod RegisterCommand;

			// Token: 0x0400045C RID: 1116
			internal ActivityManager.FFIMethods.RegisterSteamMethod RegisterSteam;

			// Token: 0x0400045D RID: 1117
			internal ActivityManager.FFIMethods.UpdateActivityMethod UpdateActivity;

			// Token: 0x0400045E RID: 1118
			internal ActivityManager.FFIMethods.ClearActivityMethod ClearActivity;

			// Token: 0x0400045F RID: 1119
			internal ActivityManager.FFIMethods.SendRequestReplyMethod SendRequestReply;

			// Token: 0x04000460 RID: 1120
			internal ActivityManager.FFIMethods.SendInviteMethod SendInvite;

			// Token: 0x04000461 RID: 1121
			internal ActivityManager.FFIMethods.AcceptInviteMethod AcceptInvite;

			// Token: 0x02000126 RID: 294
			// (Invoke) Token: 0x060007A1 RID: 1953
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result RegisterCommandMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string command);

			// Token: 0x02000127 RID: 295
			// (Invoke) Token: 0x060007A5 RID: 1957
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result RegisterSteamMethod(IntPtr methodsPtr, uint steamId);

			// Token: 0x02000128 RID: 296
			// (Invoke) Token: 0x060007A9 RID: 1961
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void UpdateActivityCallback(IntPtr ptr, Result result);

			// Token: 0x02000129 RID: 297
			// (Invoke) Token: 0x060007AD RID: 1965
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void UpdateActivityMethod(IntPtr methodsPtr, ref Activity activity, IntPtr callbackData, ActivityManager.FFIMethods.UpdateActivityCallback callback);

			// Token: 0x0200012A RID: 298
			// (Invoke) Token: 0x060007B1 RID: 1969
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ClearActivityCallback(IntPtr ptr, Result result);

			// Token: 0x0200012B RID: 299
			// (Invoke) Token: 0x060007B5 RID: 1973
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ClearActivityMethod(IntPtr methodsPtr, IntPtr callbackData, ActivityManager.FFIMethods.ClearActivityCallback callback);

			// Token: 0x0200012C RID: 300
			// (Invoke) Token: 0x060007B9 RID: 1977
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SendRequestReplyCallback(IntPtr ptr, Result result);

			// Token: 0x0200012D RID: 301
			// (Invoke) Token: 0x060007BD RID: 1981
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SendRequestReplyMethod(IntPtr methodsPtr, long userId, ActivityJoinRequestReply reply, IntPtr callbackData, ActivityManager.FFIMethods.SendRequestReplyCallback callback);

			// Token: 0x0200012E RID: 302
			// (Invoke) Token: 0x060007C1 RID: 1985
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SendInviteCallback(IntPtr ptr, Result result);

			// Token: 0x0200012F RID: 303
			// (Invoke) Token: 0x060007C5 RID: 1989
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SendInviteMethod(IntPtr methodsPtr, long userId, ActivityActionType type, [MarshalAs(UnmanagedType.LPStr)] string content, IntPtr callbackData, ActivityManager.FFIMethods.SendInviteCallback callback);

			// Token: 0x02000130 RID: 304
			// (Invoke) Token: 0x060007C9 RID: 1993
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void AcceptInviteCallback(IntPtr ptr, Result result);

			// Token: 0x02000131 RID: 305
			// (Invoke) Token: 0x060007CD RID: 1997
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void AcceptInviteMethod(IntPtr methodsPtr, long userId, IntPtr callbackData, ActivityManager.FFIMethods.AcceptInviteCallback callback);
		}

		// Token: 0x02000132 RID: 306
		// (Invoke) Token: 0x060007D1 RID: 2001
		public delegate void UpdateActivityHandler(Result result);

		// Token: 0x02000133 RID: 307
		// (Invoke) Token: 0x060007D5 RID: 2005
		public delegate void ClearActivityHandler(Result result);

		// Token: 0x02000134 RID: 308
		// (Invoke) Token: 0x060007D9 RID: 2009
		public delegate void SendRequestReplyHandler(Result result);

		// Token: 0x02000135 RID: 309
		// (Invoke) Token: 0x060007DD RID: 2013
		public delegate void SendInviteHandler(Result result);

		// Token: 0x02000136 RID: 310
		// (Invoke) Token: 0x060007E1 RID: 2017
		public delegate void AcceptInviteHandler(Result result);

		// Token: 0x02000137 RID: 311
		// (Invoke) Token: 0x060007E5 RID: 2021
		public delegate void ActivityJoinHandler(string secret);

		// Token: 0x02000138 RID: 312
		// (Invoke) Token: 0x060007E9 RID: 2025
		public delegate void ActivitySpectateHandler(string secret);

		// Token: 0x02000139 RID: 313
		// (Invoke) Token: 0x060007ED RID: 2029
		public delegate void ActivityJoinRequestHandler(ref User user);

		// Token: 0x0200013A RID: 314
		// (Invoke) Token: 0x060007F1 RID: 2033
		public delegate void ActivityInviteHandler(ActivityActionType type, ref User user, ref Activity activity);
	}
}
