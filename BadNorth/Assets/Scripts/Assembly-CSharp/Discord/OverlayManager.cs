using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020001A1 RID: 417
	public class OverlayManager
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x0001EF88 File Offset: 0x0001D388
		internal OverlayManager(IntPtr ptr, IntPtr eventsPtr, ref OverlayManager.FFIEvents events)
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0001EFDD File Offset: 0x0001D3DD
		private OverlayManager.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(OverlayManager.FFIMethods));
				}
				return (OverlayManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x060009C6 RID: 2502 RVA: 0x0001F010 File Offset: 0x0001D410
		// (remove) Token: 0x060009C7 RID: 2503 RVA: 0x0001F048 File Offset: 0x0001D448
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event OverlayManager.ToggleHandler OnToggle;

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001F07E File Offset: 0x0001D47E
		private void InitEvents(IntPtr eventsPtr, ref OverlayManager.FFIEvents events)
		{
			events.OnToggle = delegate(IntPtr ptr, bool locked)
			{
				if (this.OnToggle != null)
				{
					this.OnToggle(locked);
				}
			};
			Marshal.StructureToPtr(events, eventsPtr, false);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0001F0A4 File Offset: 0x0001D4A4
		public bool IsEnabled()
		{
			bool result = false;
			this.Methods.IsEnabled(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0001F0D0 File Offset: 0x0001D4D0
		public bool IsLocked()
		{
			bool result = false;
			this.Methods.IsLocked(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0001F0FC File Offset: 0x0001D4FC
		public void SetLocked(bool locked, OverlayManager.SetLockedHandler callback)
		{
			OverlayManager.FFIMethods.SetLockedCallback setLockedCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.SetLocked(this.MethodsPtr, locked, Utility.Retain<OverlayManager.FFIMethods.SetLockedCallback>(setLockedCallback), setLockedCallback);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0001F144 File Offset: 0x0001D544
		public void OpenActivityInvite(ActivityActionType type, OverlayManager.OpenActivityInviteHandler callback)
		{
			OverlayManager.FFIMethods.OpenActivityInviteCallback openActivityInviteCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.OpenActivityInvite(this.MethodsPtr, type, Utility.Retain<OverlayManager.FFIMethods.OpenActivityInviteCallback>(openActivityInviteCallback), openActivityInviteCallback);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001F18C File Offset: 0x0001D58C
		public void OpenGuildInvite(string code, OverlayManager.OpenGuildInviteHandler callback)
		{
			OverlayManager.FFIMethods.OpenGuildInviteCallback openGuildInviteCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.OpenGuildInvite(this.MethodsPtr, code, Utility.Retain<OverlayManager.FFIMethods.OpenGuildInviteCallback>(openGuildInviteCallback), openGuildInviteCallback);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0001F1D4 File Offset: 0x0001D5D4
		public void OpenVoiceSettings(OverlayManager.OpenVoiceSettingsHandler callback)
		{
			OverlayManager.FFIMethods.OpenVoiceSettingsCallback openVoiceSettingsCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.OpenVoiceSettings(this.MethodsPtr, Utility.Retain<OverlayManager.FFIMethods.OpenVoiceSettingsCallback>(openVoiceSettingsCallback), openVoiceSettingsCallback);
		}

		// Token: 0x040004AD RID: 1197
		private IntPtr MethodsPtr;

		// Token: 0x040004AE RID: 1198
		private object MethodsStructure;

		// Token: 0x020001A2 RID: 418
		internal struct FFIEvents
		{
			// Token: 0x040004B0 RID: 1200
			internal OverlayManager.FFIEvents.ToggleHandler OnToggle;

			// Token: 0x020001A3 RID: 419
			// (Invoke) Token: 0x060009D1 RID: 2513
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ToggleHandler(IntPtr ptr, bool locked);
		}

		// Token: 0x020001A4 RID: 420
		internal struct FFIMethods
		{
			// Token: 0x040004B1 RID: 1201
			internal OverlayManager.FFIMethods.IsEnabledMethod IsEnabled;

			// Token: 0x040004B2 RID: 1202
			internal OverlayManager.FFIMethods.IsLockedMethod IsLocked;

			// Token: 0x040004B3 RID: 1203
			internal OverlayManager.FFIMethods.SetLockedMethod SetLocked;

			// Token: 0x040004B4 RID: 1204
			internal OverlayManager.FFIMethods.OpenActivityInviteMethod OpenActivityInvite;

			// Token: 0x040004B5 RID: 1205
			internal OverlayManager.FFIMethods.OpenGuildInviteMethod OpenGuildInvite;

			// Token: 0x040004B6 RID: 1206
			internal OverlayManager.FFIMethods.OpenVoiceSettingsMethod OpenVoiceSettings;

			// Token: 0x020001A5 RID: 421
			// (Invoke) Token: 0x060009D5 RID: 2517
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void IsEnabledMethod(IntPtr methodsPtr, ref bool enabled);

			// Token: 0x020001A6 RID: 422
			// (Invoke) Token: 0x060009D9 RID: 2521
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void IsLockedMethod(IntPtr methodsPtr, ref bool locked);

			// Token: 0x020001A7 RID: 423
			// (Invoke) Token: 0x060009DD RID: 2525
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SetLockedCallback(IntPtr ptr, Result result);

			// Token: 0x020001A8 RID: 424
			// (Invoke) Token: 0x060009E1 RID: 2529
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SetLockedMethod(IntPtr methodsPtr, bool locked, IntPtr callbackData, OverlayManager.FFIMethods.SetLockedCallback callback);

			// Token: 0x020001A9 RID: 425
			// (Invoke) Token: 0x060009E5 RID: 2533
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void OpenActivityInviteCallback(IntPtr ptr, Result result);

			// Token: 0x020001AA RID: 426
			// (Invoke) Token: 0x060009E9 RID: 2537
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void OpenActivityInviteMethod(IntPtr methodsPtr, ActivityActionType type, IntPtr callbackData, OverlayManager.FFIMethods.OpenActivityInviteCallback callback);

			// Token: 0x020001AB RID: 427
			// (Invoke) Token: 0x060009ED RID: 2541
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void OpenGuildInviteCallback(IntPtr ptr, Result result);

			// Token: 0x020001AC RID: 428
			// (Invoke) Token: 0x060009F1 RID: 2545
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void OpenGuildInviteMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string code, IntPtr callbackData, OverlayManager.FFIMethods.OpenGuildInviteCallback callback);

			// Token: 0x020001AD RID: 429
			// (Invoke) Token: 0x060009F5 RID: 2549
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void OpenVoiceSettingsCallback(IntPtr ptr, Result result);

			// Token: 0x020001AE RID: 430
			// (Invoke) Token: 0x060009F9 RID: 2553
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void OpenVoiceSettingsMethod(IntPtr methodsPtr, IntPtr callbackData, OverlayManager.FFIMethods.OpenVoiceSettingsCallback callback);
		}

		// Token: 0x020001AF RID: 431
		// (Invoke) Token: 0x060009FD RID: 2557
		public delegate void SetLockedHandler(Result result);

		// Token: 0x020001B0 RID: 432
		// (Invoke) Token: 0x06000A01 RID: 2561
		public delegate void OpenActivityInviteHandler(Result result);

		// Token: 0x020001B1 RID: 433
		// (Invoke) Token: 0x06000A05 RID: 2565
		public delegate void OpenGuildInviteHandler(Result result);

		// Token: 0x020001B2 RID: 434
		// (Invoke) Token: 0x06000A09 RID: 2569
		public delegate void OpenVoiceSettingsHandler(Result result);

		// Token: 0x020001B3 RID: 435
		// (Invoke) Token: 0x06000A0D RID: 2573
		public delegate void ToggleHandler(bool locked);
	}
}
