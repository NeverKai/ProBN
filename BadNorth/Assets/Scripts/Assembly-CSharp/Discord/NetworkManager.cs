using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x02000192 RID: 402
	public class NetworkManager
	{
		// Token: 0x06000983 RID: 2435 RVA: 0x0001EBE0 File Offset: 0x0001CFE0
		internal NetworkManager(IntPtr ptr, IntPtr eventsPtr, ref NetworkManager.FFIEvents events)
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0001EC35 File Offset: 0x0001D035
		private NetworkManager.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(NetworkManager.FFIMethods));
				}
				return (NetworkManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000985 RID: 2437 RVA: 0x0001EC68 File Offset: 0x0001D068
		// (remove) Token: 0x06000986 RID: 2438 RVA: 0x0001ECA0 File Offset: 0x0001D0A0
		public event NetworkManager.MessageHandler OnMessage;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06000987 RID: 2439 RVA: 0x0001ECD8 File Offset: 0x0001D0D8
		// (remove) Token: 0x06000988 RID: 2440 RVA: 0x0001ED10 File Offset: 0x0001D110
		public event NetworkManager.RouteUpdateHandler OnRouteUpdate;

		// Token: 0x06000989 RID: 2441 RVA: 0x0001ED46 File Offset: 0x0001D146
		private void InitEvents(IntPtr eventsPtr, ref NetworkManager.FFIEvents events)
		{
			events.OnMessage = delegate(IntPtr ptr, ulong peerId, byte channelId, IntPtr dataPtr, int dataLen)
			{
				if (this.OnMessage != null)
				{
					byte[] array = new byte[dataLen];
					Marshal.Copy(dataPtr, array, 0, dataLen);
					this.OnMessage(peerId, channelId, array);
				}
			};
			events.OnRouteUpdate = delegate(IntPtr ptr, string routeData)
			{
				if (this.OnRouteUpdate != null)
				{
					this.OnRouteUpdate(routeData);
				}
			};
			Marshal.StructureToPtr(events, eventsPtr, false);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001ED80 File Offset: 0x0001D180
		public ulong GetPeerId()
		{
			ulong result = 0UL;
			this.Methods.GetPeerId(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001EDAC File Offset: 0x0001D1AC
		public void Flush()
		{
			Result result = this.Methods.Flush(this.MethodsPtr);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001EDE0 File Offset: 0x0001D1E0
		public void OpenPeer(ulong peerId, string routeData)
		{
			Result result = this.Methods.OpenPeer(this.MethodsPtr, peerId, routeData);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001EE18 File Offset: 0x0001D218
		public void UpdatePeer(ulong peerId, string routeData)
		{
			Result result = this.Methods.UpdatePeer(this.MethodsPtr, peerId, routeData);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001EE50 File Offset: 0x0001D250
		public void ClosePeer(ulong peerId)
		{
			Result result = this.Methods.ClosePeer(this.MethodsPtr, peerId);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001EE88 File Offset: 0x0001D288
		public void OpenChannel(ulong peerId, byte channelId, bool reliable)
		{
			Result result = this.Methods.OpenChannel(this.MethodsPtr, peerId, channelId, reliable);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001EEC0 File Offset: 0x0001D2C0
		public void CloseChannel(ulong peerId, byte channelId)
		{
			Result result = this.Methods.CloseChannel(this.MethodsPtr, peerId, channelId);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001EEF8 File Offset: 0x0001D2F8
		public void SendMessage(ulong peerId, byte channelId, byte[] data)
		{
			Result result = this.Methods.SendMessage(this.MethodsPtr, peerId, channelId, data, data.Length);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x0400049F RID: 1183
		private IntPtr MethodsPtr;

		// Token: 0x040004A0 RID: 1184
		private object MethodsStructure;

		// Token: 0x02000193 RID: 403
		internal struct FFIEvents
		{
			// Token: 0x040004A3 RID: 1187
			internal NetworkManager.FFIEvents.MessageHandler OnMessage;

			// Token: 0x040004A4 RID: 1188
			internal NetworkManager.FFIEvents.RouteUpdateHandler OnRouteUpdate;

			// Token: 0x02000194 RID: 404
			// (Invoke) Token: 0x06000995 RID: 2453
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void MessageHandler(IntPtr ptr, ulong peerId, byte channelId, IntPtr dataPtr, int dataLen);

			// Token: 0x02000195 RID: 405
			// (Invoke) Token: 0x06000999 RID: 2457
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void RouteUpdateHandler(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)] string routeData);
		}

		// Token: 0x02000196 RID: 406
		internal struct FFIMethods
		{
			// Token: 0x040004A5 RID: 1189
			internal NetworkManager.FFIMethods.GetPeerIdMethod GetPeerId;

			// Token: 0x040004A6 RID: 1190
			internal NetworkManager.FFIMethods.FlushMethod Flush;

			// Token: 0x040004A7 RID: 1191
			internal NetworkManager.FFIMethods.OpenPeerMethod OpenPeer;

			// Token: 0x040004A8 RID: 1192
			internal NetworkManager.FFIMethods.UpdatePeerMethod UpdatePeer;

			// Token: 0x040004A9 RID: 1193
			internal NetworkManager.FFIMethods.ClosePeerMethod ClosePeer;

			// Token: 0x040004AA RID: 1194
			internal NetworkManager.FFIMethods.OpenChannelMethod OpenChannel;

			// Token: 0x040004AB RID: 1195
			internal NetworkManager.FFIMethods.CloseChannelMethod CloseChannel;

			// Token: 0x040004AC RID: 1196
			internal NetworkManager.FFIMethods.SendMessageMethod SendMessage;

			// Token: 0x02000197 RID: 407
			// (Invoke) Token: 0x0600099D RID: 2461
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void GetPeerIdMethod(IntPtr methodsPtr, ref ulong peerId);

			// Token: 0x02000198 RID: 408
			// (Invoke) Token: 0x060009A1 RID: 2465
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result FlushMethod(IntPtr methodsPtr);

			// Token: 0x02000199 RID: 409
			// (Invoke) Token: 0x060009A5 RID: 2469
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result OpenPeerMethod(IntPtr methodsPtr, ulong peerId, [MarshalAs(UnmanagedType.LPStr)] string routeData);

			// Token: 0x0200019A RID: 410
			// (Invoke) Token: 0x060009A9 RID: 2473
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result UpdatePeerMethod(IntPtr methodsPtr, ulong peerId, [MarshalAs(UnmanagedType.LPStr)] string routeData);

			// Token: 0x0200019B RID: 411
			// (Invoke) Token: 0x060009AD RID: 2477
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result ClosePeerMethod(IntPtr methodsPtr, ulong peerId);

			// Token: 0x0200019C RID: 412
			// (Invoke) Token: 0x060009B1 RID: 2481
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result OpenChannelMethod(IntPtr methodsPtr, ulong peerId, byte channelId, bool reliable);

			// Token: 0x0200019D RID: 413
			// (Invoke) Token: 0x060009B5 RID: 2485
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result CloseChannelMethod(IntPtr methodsPtr, ulong peerId, byte channelId);

			// Token: 0x0200019E RID: 414
			// (Invoke) Token: 0x060009B9 RID: 2489
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result SendMessageMethod(IntPtr methodsPtr, ulong peerId, byte channelId, byte[] data, int dataLen);
		}

		// Token: 0x0200019F RID: 415
		// (Invoke) Token: 0x060009BD RID: 2493
		public delegate void MessageHandler(ulong peerId, byte channelId, byte[] data);

		// Token: 0x020001A0 RID: 416
		// (Invoke) Token: 0x060009C1 RID: 2497
		public delegate void RouteUpdateHandler(string routeData);
	}
}
