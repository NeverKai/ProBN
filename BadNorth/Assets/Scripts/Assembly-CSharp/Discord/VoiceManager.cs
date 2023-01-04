using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020001DF RID: 479
	public class VoiceManager
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001FBDC File Offset: 0x0001DFDC
		internal VoiceManager(IntPtr ptr, IntPtr eventsPtr, ref VoiceManager.FFIEvents events)
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

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0001FC31 File Offset: 0x0001E031
		private VoiceManager.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(VoiceManager.FFIMethods));
				}
				return (VoiceManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000ACA RID: 2762 RVA: 0x0001FC64 File Offset: 0x0001E064
		// (remove) Token: 0x06000ACB RID: 2763 RVA: 0x0001FC9C File Offset: 0x0001E09C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VoiceManager.SettingsUpdateHandler OnSettingsUpdate;

		// Token: 0x06000ACC RID: 2764 RVA: 0x0001FCD2 File Offset: 0x0001E0D2
		private void InitEvents(IntPtr eventsPtr, ref VoiceManager.FFIEvents events)
		{
			events.OnSettingsUpdate = delegate(IntPtr ptr)
			{
				if (this.OnSettingsUpdate != null)
				{
					this.OnSettingsUpdate();
				}
			};
			Marshal.StructureToPtr(events, eventsPtr, false);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001FCF8 File Offset: 0x0001E0F8
		public InputMode GetInputMode()
		{
			InputMode result = default(InputMode);
			Result result2 = this.Methods.GetInputMode(this.MethodsPtr, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0001FD38 File Offset: 0x0001E138
		public void SetInputMode(InputMode inputMode, VoiceManager.SetInputModeHandler callback)
		{
			VoiceManager.FFIMethods.SetInputModeCallback setInputModeCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.SetInputMode(this.MethodsPtr, inputMode, Utility.Retain<VoiceManager.FFIMethods.SetInputModeCallback>(setInputModeCallback), setInputModeCallback);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0001FD80 File Offset: 0x0001E180
		public bool IsSelfMute()
		{
			bool result = false;
			Result result2 = this.Methods.IsSelfMute(this.MethodsPtr, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0001FDBC File Offset: 0x0001E1BC
		public void SetSelfMute(bool mute)
		{
			Result result = this.Methods.SetSelfMute(this.MethodsPtr, mute);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0001FDF4 File Offset: 0x0001E1F4
		public bool IsSelfDeaf()
		{
			bool result = false;
			Result result2 = this.Methods.IsSelfDeaf(this.MethodsPtr, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0001FE30 File Offset: 0x0001E230
		public void SetSelfDeaf(bool deaf)
		{
			Result result = this.Methods.SetSelfDeaf(this.MethodsPtr, deaf);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001FE68 File Offset: 0x0001E268
		public bool IsLocalMute(long userId)
		{
			bool result = false;
			Result result2 = this.Methods.IsLocalMute(this.MethodsPtr, userId, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001FEA4 File Offset: 0x0001E2A4
		public void SetLocalMute(long userId, bool mute)
		{
			Result result = this.Methods.SetLocalMute(this.MethodsPtr, userId, mute);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001FEDC File Offset: 0x0001E2DC
		public byte GetLocalVolume(long userId)
		{
			byte result = 0;
			Result result2 = this.Methods.GetLocalVolume(this.MethodsPtr, userId, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0001FF18 File Offset: 0x0001E318
		public void SetLocalVolume(long userId, byte volume)
		{
			Result result = this.Methods.SetLocalVolume(this.MethodsPtr, userId, volume);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x040004D4 RID: 1236
		private IntPtr MethodsPtr;

		// Token: 0x040004D5 RID: 1237
		private object MethodsStructure;

		// Token: 0x020001E0 RID: 480
		internal struct FFIEvents
		{
			// Token: 0x040004D7 RID: 1239
			internal VoiceManager.FFIEvents.SettingsUpdateHandler OnSettingsUpdate;

			// Token: 0x020001E1 RID: 481
			// (Invoke) Token: 0x06000AD9 RID: 2777
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SettingsUpdateHandler(IntPtr ptr);
		}

		// Token: 0x020001E2 RID: 482
		internal struct FFIMethods
		{
			// Token: 0x040004D8 RID: 1240
			internal VoiceManager.FFIMethods.GetInputModeMethod GetInputMode;

			// Token: 0x040004D9 RID: 1241
			internal VoiceManager.FFIMethods.SetInputModeMethod SetInputMode;

			// Token: 0x040004DA RID: 1242
			internal VoiceManager.FFIMethods.IsSelfMuteMethod IsSelfMute;

			// Token: 0x040004DB RID: 1243
			internal VoiceManager.FFIMethods.SetSelfMuteMethod SetSelfMute;

			// Token: 0x040004DC RID: 1244
			internal VoiceManager.FFIMethods.IsSelfDeafMethod IsSelfDeaf;

			// Token: 0x040004DD RID: 1245
			internal VoiceManager.FFIMethods.SetSelfDeafMethod SetSelfDeaf;

			// Token: 0x040004DE RID: 1246
			internal VoiceManager.FFIMethods.IsLocalMuteMethod IsLocalMute;

			// Token: 0x040004DF RID: 1247
			internal VoiceManager.FFIMethods.SetLocalMuteMethod SetLocalMute;

			// Token: 0x040004E0 RID: 1248
			internal VoiceManager.FFIMethods.GetLocalVolumeMethod GetLocalVolume;

			// Token: 0x040004E1 RID: 1249
			internal VoiceManager.FFIMethods.SetLocalVolumeMethod SetLocalVolume;

			// Token: 0x020001E3 RID: 483
			// (Invoke) Token: 0x06000ADD RID: 2781
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetInputModeMethod(IntPtr methodsPtr, ref InputMode inputMode);

			// Token: 0x020001E4 RID: 484
			// (Invoke) Token: 0x06000AE1 RID: 2785
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SetInputModeCallback(IntPtr ptr, Result result);

			// Token: 0x020001E5 RID: 485
			// (Invoke) Token: 0x06000AE5 RID: 2789
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SetInputModeMethod(IntPtr methodsPtr, InputMode inputMode, IntPtr callbackData, VoiceManager.FFIMethods.SetInputModeCallback callback);

			// Token: 0x020001E6 RID: 486
			// (Invoke) Token: 0x06000AE9 RID: 2793
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result IsSelfMuteMethod(IntPtr methodsPtr, ref bool mute);

			// Token: 0x020001E7 RID: 487
			// (Invoke) Token: 0x06000AED RID: 2797
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result SetSelfMuteMethod(IntPtr methodsPtr, bool mute);

			// Token: 0x020001E8 RID: 488
			// (Invoke) Token: 0x06000AF1 RID: 2801
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result IsSelfDeafMethod(IntPtr methodsPtr, ref bool deaf);

			// Token: 0x020001E9 RID: 489
			// (Invoke) Token: 0x06000AF5 RID: 2805
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result SetSelfDeafMethod(IntPtr methodsPtr, bool deaf);

			// Token: 0x020001EA RID: 490
			// (Invoke) Token: 0x06000AF9 RID: 2809
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result IsLocalMuteMethod(IntPtr methodsPtr, long userId, ref bool mute);

			// Token: 0x020001EB RID: 491
			// (Invoke) Token: 0x06000AFD RID: 2813
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result SetLocalMuteMethod(IntPtr methodsPtr, long userId, bool mute);

			// Token: 0x020001EC RID: 492
			// (Invoke) Token: 0x06000B01 RID: 2817
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetLocalVolumeMethod(IntPtr methodsPtr, long userId, ref byte volume);

			// Token: 0x020001ED RID: 493
			// (Invoke) Token: 0x06000B05 RID: 2821
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result SetLocalVolumeMethod(IntPtr methodsPtr, long userId, byte volume);
		}

		// Token: 0x020001EE RID: 494
		// (Invoke) Token: 0x06000B09 RID: 2825
		public delegate void SetInputModeHandler(Result result);

		// Token: 0x020001EF RID: 495
		// (Invoke) Token: 0x06000B0D RID: 2829
		public delegate void SettingsUpdateHandler();
	}
}
