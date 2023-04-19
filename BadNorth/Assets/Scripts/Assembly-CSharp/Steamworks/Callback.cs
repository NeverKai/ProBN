using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000326 RID: 806
	public sealed class Callback<T> : IDisposable
	{
		// Token: 0x060011F0 RID: 4592 RVA: 0x000268F2 File Offset: 0x00024CF2
		public Callback(Callback<T>.DispatchDelegate func, bool bGameServer = false)
		{
			this.m_bGameServer = bGameServer;
			this.BuildCCallbackBase();
			this.Register(func);
		}

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060011F1 RID: 4593 RVA: 0x00026930 File Offset: 0x00024D30
		// (remove) Token: 0x060011F2 RID: 4594 RVA: 0x00026968 File Offset: 0x00024D68
		
		private event Callback<T>.DispatchDelegate m_Func;

		// Token: 0x060011F3 RID: 4595 RVA: 0x0002699E File Offset: 0x00024D9E
		public static Callback<T> Create(Callback<T>.DispatchDelegate func)
		{
			return new Callback<T>(func, false);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x000269A7 File Offset: 0x00024DA7
		public static Callback<T> CreateGameServer(Callback<T>.DispatchDelegate func)
		{
			return new Callback<T>(func, true);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x000269B0 File Offset: 0x00024DB0
		~Callback()
		{
			this.Dispose();
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x000269E0 File Offset: 0x00024DE0
		public void Dispose()
		{
			if (this.m_bDisposed)
			{
				return;
			}
			GC.SuppressFinalize(this);
			this.Unregister();
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

		// Token: 0x060011F7 RID: 4599 RVA: 0x00026A48 File Offset: 0x00024E48
		public void Register(Callback<T>.DispatchDelegate func)
		{
			if (func == null)
			{
				throw new Exception("Callback function must not be null.");
			}
			if ((this.m_CCallbackBase.m_nCallbackFlags & 1) == 1)
			{
				this.Unregister();
			}
			if (this.m_bGameServer)
			{
				this.SetGameserverFlag();
			}
			this.m_Func = func;
			NativeMethods.SteamAPI_RegisterCallback(this.m_pCCallbackBase.AddrOfPinnedObject(), CallbackIdentities.GetCallbackIdentity(typeof(T)));
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00026AB6 File Offset: 0x00024EB6
		public void Unregister()
		{
			NativeMethods.SteamAPI_UnregisterCallback(this.m_pCCallbackBase.AddrOfPinnedObject());
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00026AC8 File Offset: 0x00024EC8
		public void SetGameserverFlag()
		{
			CCallbackBase ccallbackBase = this.m_CCallbackBase;
			ccallbackBase.m_nCallbackFlags |= 2;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00026AE0 File Offset: 0x00024EE0
		private void OnRunCallback(IntPtr pvParam)
		{
			try
			{
				this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))));
			}
			catch (Exception e)
			{
				CallbackDispatcher.ExceptionHandler(e);
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00026B30 File Offset: 0x00024F30
		private void OnRunCallResult(IntPtr pvParam, bool bFailed, ulong hSteamAPICall)
		{
			try
			{
				this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))));
			}
			catch (Exception e)
			{
				CallbackDispatcher.ExceptionHandler(e);
			}
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00026B80 File Offset: 0x00024F80
		private int OnGetCallbackSizeBytes()
		{
			return this.m_size;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00026B88 File Offset: 0x00024F88
		private void BuildCCallbackBase()
		{
			this.VTable = new CCallbackBaseVTable
			{
				m_RunCallResult = new CCallbackBaseVTable.RunCRDel(this.OnRunCallResult),
				m_RunCallback = new CCallbackBaseVTable.RunCBDel(this.OnRunCallback),
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

		// Token: 0x04000C29 RID: 3113
		private CCallbackBaseVTable VTable;

		// Token: 0x04000C2A RID: 3114
		private IntPtr m_pVTable = IntPtr.Zero;

		// Token: 0x04000C2B RID: 3115
		private CCallbackBase m_CCallbackBase;

		// Token: 0x04000C2C RID: 3116
		private GCHandle m_pCCallbackBase;

		// Token: 0x04000C2E RID: 3118
		private bool m_bGameServer;

		// Token: 0x04000C2F RID: 3119
		private readonly int m_size = Marshal.SizeOf(typeof(T));

		// Token: 0x04000C30 RID: 3120
		private bool m_bDisposed;

		// Token: 0x02000327 RID: 807
		// (Invoke) Token: 0x060011FF RID: 4607
		public delegate void DispatchDelegate(T param);
	}
}
