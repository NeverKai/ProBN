using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000328 RID: 808
	public sealed class CallResult<T> : IDisposable
	{
		// Token: 0x06001202 RID: 4610 RVA: 0x00026C4B File Offset: 0x0002504B
		public CallResult(CallResult<T>.APIDispatchDelegate func = null)
		{
			this.m_Func = func;
			this.BuildCCallbackBase();
		}

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06001203 RID: 4611 RVA: 0x00026C8C File Offset: 0x0002508C
		// (remove) Token: 0x06001204 RID: 4612 RVA: 0x00026CC4 File Offset: 0x000250C4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event CallResult<T>.APIDispatchDelegate m_Func;

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x00026CFA File Offset: 0x000250FA
		public SteamAPICall_t Handle
		{
			get
			{
				return this.m_hAPICall;
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00026D02 File Offset: 0x00025102
		public static CallResult<T> Create(CallResult<T>.APIDispatchDelegate func = null)
		{
			return new CallResult<T>(func);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00026D0C File Offset: 0x0002510C
		~CallResult()
		{
			this.Dispose();
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00026D3C File Offset: 0x0002513C
		public void Dispose()
		{
			if (this.m_bDisposed)
			{
				return;
			}
			GC.SuppressFinalize(this);
			this.Cancel();
			if (this.m_pVTable != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pVTable);
			}
			if (this.m_pCCallbackBase.IsAllocated)
			{
				this.m_pCCallbackBase.Free();
			}
			this.m_bDisposed = true;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00026DA4 File Offset: 0x000251A4
		public void Set(SteamAPICall_t hAPICall, CallResult<T>.APIDispatchDelegate func = null)
		{
			if (func != null)
			{
				this.m_Func = func;
			}
			if (this.m_Func == null)
			{
				throw new Exception("CallResult function was null, you must either set it in the CallResult Constructor or in Set()");
			}
			if (this.m_hAPICall != SteamAPICall_t.Invalid)
			{
				NativeMethods.SteamAPI_UnregisterCallResult(this.m_pCCallbackBase.AddrOfPinnedObject(), (ulong)this.m_hAPICall);
			}
			this.m_hAPICall = hAPICall;
			if (hAPICall != SteamAPICall_t.Invalid)
			{
				NativeMethods.SteamAPI_RegisterCallResult(this.m_pCCallbackBase.AddrOfPinnedObject(), (ulong)hAPICall);
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00026E31 File Offset: 0x00025231
		public bool IsActive()
		{
			return this.m_hAPICall != SteamAPICall_t.Invalid;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00026E43 File Offset: 0x00025243
		public void Cancel()
		{
			if (this.m_hAPICall != SteamAPICall_t.Invalid)
			{
				NativeMethods.SteamAPI_UnregisterCallResult(this.m_pCCallbackBase.AddrOfPinnedObject(), (ulong)this.m_hAPICall);
				this.m_hAPICall = SteamAPICall_t.Invalid;
			}
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00026E80 File Offset: 0x00025280
		public void SetGameserverFlag()
		{
			CCallbackBase ccallbackBase = this.m_CCallbackBase;
			ccallbackBase.m_nCallbackFlags |= 2;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00026E98 File Offset: 0x00025298
		private void OnRunCallback(IntPtr pvParam)
		{
			this.m_hAPICall = SteamAPICall_t.Invalid;
			try
			{
				this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))), false);
			}
			catch (Exception e)
			{
				CallbackDispatcher.ExceptionHandler(e);
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00026EF4 File Offset: 0x000252F4
		private void OnRunCallResult(IntPtr pvParam, bool bFailed, ulong hSteamAPICall_)
		{
			SteamAPICall_t x = (SteamAPICall_t)hSteamAPICall_;
			if (x == this.m_hAPICall)
			{
				this.m_hAPICall = SteamAPICall_t.Invalid;
				try
				{
					this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))), bFailed);
				}
				catch (Exception e)
				{
					CallbackDispatcher.ExceptionHandler(e);
				}
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00026F68 File Offset: 0x00025368
		private int OnGetCallbackSizeBytes()
		{
			return this.m_size;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00026F70 File Offset: 0x00025370
		private void BuildCCallbackBase()
		{
			this.VTable = new CCallbackBaseVTable
			{
				m_RunCallback = new CCallbackBaseVTable.RunCBDel(this.OnRunCallback),
				m_RunCallResult = new CCallbackBaseVTable.RunCRDel(this.OnRunCallResult),
				m_GetCallbackSizeBytes = new CCallbackBaseVTable.GetCallbackSizeBytesDel(this.OnGetCallbackSizeBytes)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CCallbackBaseVTable)));
			Marshal.StructureToPtr(this.VTable, this.m_pVTable, false);
			this.m_CCallbackBase = new CCallbackBase
			{
				m_vfptr = this.m_pVTable,
				m_nCallbackFlags = 0,
				m_iCallback = CallbackIdentities.GetCallbackIdentity(typeof(T))
			};
			this.m_pCCallbackBase = GCHandle.Alloc(this.m_CCallbackBase, GCHandleType.Pinned);
		}

		// Token: 0x04000C31 RID: 3121
		private CCallbackBaseVTable VTable;

		// Token: 0x04000C32 RID: 3122
		private IntPtr m_pVTable = IntPtr.Zero;

		// Token: 0x04000C33 RID: 3123
		private CCallbackBase m_CCallbackBase;

		// Token: 0x04000C34 RID: 3124
		private GCHandle m_pCCallbackBase;

		// Token: 0x04000C36 RID: 3126
		private SteamAPICall_t m_hAPICall = SteamAPICall_t.Invalid;

		// Token: 0x04000C37 RID: 3127
		private readonly int m_size = Marshal.SizeOf(typeof(T));

		// Token: 0x04000C38 RID: 3128
		private bool m_bDisposed;

		// Token: 0x02000329 RID: 809
		// (Invoke) Token: 0x06001212 RID: 4626
		public delegate void APIDispatchDelegate(T param, bool bIOFailure);
	}
}
