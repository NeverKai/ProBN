using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Discord
{
	// Token: 0x020001B4 RID: 436
	public class StorageManager
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x0001F2A4 File Offset: 0x0001D6A4
		internal StorageManager(IntPtr ptr, IntPtr eventsPtr, ref StorageManager.FFIEvents events)
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

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0001F2F9 File Offset: 0x0001D6F9
		private StorageManager.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(StorageManager.FFIMethods));
				}
				return (StorageManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001F32C File Offset: 0x0001D72C
		private void InitEvents(IntPtr eventsPtr, ref StorageManager.FFIEvents events)
		{
			Marshal.StructureToPtr(events, eventsPtr, false);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0001F340 File Offset: 0x0001D740
		public uint Read(string name, byte[] data)
		{
			uint result = 0U;
			Result result2 = this.Methods.Read(this.MethodsPtr, name, data, data.Length, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001F380 File Offset: 0x0001D780
		public void ReadAsync(string name, StorageManager.ReadAsyncHandler callback)
		{
			StorageManager.FFIMethods.ReadAsyncCallback readAsyncCallback = delegate(IntPtr ptr, Result result, IntPtr dataPtr, int dataLen)
			{
				Utility.Release(ptr);
				byte[] array = new byte[dataLen];
				Marshal.Copy(dataPtr, array, 0, dataLen);
				callback(result, array);
			};
			this.Methods.ReadAsync(this.MethodsPtr, name, Utility.Retain<StorageManager.FFIMethods.ReadAsyncCallback>(readAsyncCallback), readAsyncCallback);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001F3C8 File Offset: 0x0001D7C8
		public void ReadAsyncPartial(string name, ulong offset, ulong length, StorageManager.ReadAsyncPartialHandler callback)
		{
			StorageManager.FFIMethods.ReadAsyncPartialCallback readAsyncPartialCallback = delegate(IntPtr ptr, Result result, IntPtr dataPtr, int dataLen)
			{
				Utility.Release(ptr);
				byte[] array = new byte[dataLen];
				Marshal.Copy(dataPtr, array, 0, dataLen);
				callback(result, array);
			};
			this.Methods.ReadAsyncPartial(this.MethodsPtr, name, offset, length, Utility.Retain<StorageManager.FFIMethods.ReadAsyncPartialCallback>(readAsyncPartialCallback), readAsyncPartialCallback);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0001F414 File Offset: 0x0001D814
		public void Write(string name, byte[] data)
		{
			Result result = this.Methods.Write(this.MethodsPtr, name, data, data.Length);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001F450 File Offset: 0x0001D850
		public void WriteAsync(string name, byte[] data, StorageManager.WriteAsyncHandler callback)
		{
			StorageManager.FFIMethods.WriteAsyncCallback writeAsyncCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.WriteAsync(this.MethodsPtr, name, data, data.Length, Utility.Retain<StorageManager.FFIMethods.WriteAsyncCallback>(writeAsyncCallback), writeAsyncCallback);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001F49C File Offset: 0x0001D89C
		public void Delete(string name)
		{
			Result result = this.Methods.Delete(this.MethodsPtr, name);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001F4D4 File Offset: 0x0001D8D4
		public bool Exists(string name)
		{
			bool result = false;
			Result result2 = this.Methods.Exists(this.MethodsPtr, name, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001F510 File Offset: 0x0001D910
		public int Count()
		{
			int result = 0;
			this.Methods.Count(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0001F53C File Offset: 0x0001D93C
		public FileStat Stat(string name)
		{
			FileStat result = default(FileStat);
			Result result2 = this.Methods.Stat(this.MethodsPtr, name, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001F57C File Offset: 0x0001D97C
		public FileStat StatAt(int index)
		{
			FileStat result = default(FileStat);
			Result result2 = this.Methods.StatAt(this.MethodsPtr, index, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001F5BC File Offset: 0x0001D9BC
		public string GetPath()
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			Result result = this.Methods.GetPath(this.MethodsPtr, stringBuilder);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001F604 File Offset: 0x0001DA04
		public IEnumerable<FileStat> Files()
		{
			int num = this.Count();
			List<FileStat> list = new List<FileStat>();
			for (int i = 0; i < num; i++)
			{
				list.Add(this.StatAt(i));
			}
			return list;
		}

		// Token: 0x040004B7 RID: 1207
		private IntPtr MethodsPtr;

		// Token: 0x040004B8 RID: 1208
		private object MethodsStructure;

		// Token: 0x020001B5 RID: 437
		internal struct FFIEvents
		{
		}

		// Token: 0x020001B6 RID: 438
		internal struct FFIMethods
		{
			// Token: 0x040004B9 RID: 1209
			internal StorageManager.FFIMethods.ReadMethod Read;

			// Token: 0x040004BA RID: 1210
			internal StorageManager.FFIMethods.ReadAsyncMethod ReadAsync;

			// Token: 0x040004BB RID: 1211
			internal StorageManager.FFIMethods.ReadAsyncPartialMethod ReadAsyncPartial;

			// Token: 0x040004BC RID: 1212
			internal StorageManager.FFIMethods.WriteMethod Write;

			// Token: 0x040004BD RID: 1213
			internal StorageManager.FFIMethods.WriteAsyncMethod WriteAsync;

			// Token: 0x040004BE RID: 1214
			internal StorageManager.FFIMethods.DeleteMethod Delete;

			// Token: 0x040004BF RID: 1215
			internal StorageManager.FFIMethods.ExistsMethod Exists;

			// Token: 0x040004C0 RID: 1216
			internal StorageManager.FFIMethods.CountMethod Count;

			// Token: 0x040004C1 RID: 1217
			internal StorageManager.FFIMethods.StatMethod Stat;

			// Token: 0x040004C2 RID: 1218
			internal StorageManager.FFIMethods.StatAtMethod StatAt;

			// Token: 0x040004C3 RID: 1219
			internal StorageManager.FFIMethods.GetPathMethod GetPath;

			// Token: 0x020001B7 RID: 439
			// (Invoke) Token: 0x06000A20 RID: 2592
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result ReadMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, byte[] data, int dataLen, ref uint read);

			// Token: 0x020001B8 RID: 440
			// (Invoke) Token: 0x06000A24 RID: 2596
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ReadAsyncCallback(IntPtr ptr, Result result, IntPtr dataPtr, int dataLen);

			// Token: 0x020001B9 RID: 441
			// (Invoke) Token: 0x06000A28 RID: 2600
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ReadAsyncMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, IntPtr callbackData, StorageManager.FFIMethods.ReadAsyncCallback callback);

			// Token: 0x020001BA RID: 442
			// (Invoke) Token: 0x06000A2C RID: 2604
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ReadAsyncPartialCallback(IntPtr ptr, Result result, IntPtr dataPtr, int dataLen);

			// Token: 0x020001BB RID: 443
			// (Invoke) Token: 0x06000A30 RID: 2608
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ReadAsyncPartialMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, ulong offset, ulong length, IntPtr callbackData, StorageManager.FFIMethods.ReadAsyncPartialCallback callback);

			// Token: 0x020001BC RID: 444
			// (Invoke) Token: 0x06000A34 RID: 2612
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result WriteMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, byte[] data, int dataLen);

			// Token: 0x020001BD RID: 445
			// (Invoke) Token: 0x06000A38 RID: 2616
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void WriteAsyncCallback(IntPtr ptr, Result result);

			// Token: 0x020001BE RID: 446
			// (Invoke) Token: 0x06000A3C RID: 2620
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void WriteAsyncMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, byte[] data, int dataLen, IntPtr callbackData, StorageManager.FFIMethods.WriteAsyncCallback callback);

			// Token: 0x020001BF RID: 447
			// (Invoke) Token: 0x06000A40 RID: 2624
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result DeleteMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name);

			// Token: 0x020001C0 RID: 448
			// (Invoke) Token: 0x06000A44 RID: 2628
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result ExistsMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, ref bool exists);

			// Token: 0x020001C1 RID: 449
			// (Invoke) Token: 0x06000A48 RID: 2632
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void CountMethod(IntPtr methodsPtr, ref int count);

			// Token: 0x020001C2 RID: 450
			// (Invoke) Token: 0x06000A4C RID: 2636
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result StatMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, ref FileStat stat);

			// Token: 0x020001C3 RID: 451
			// (Invoke) Token: 0x06000A50 RID: 2640
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result StatAtMethod(IntPtr methodsPtr, int index, ref FileStat stat);

			// Token: 0x020001C4 RID: 452
			// (Invoke) Token: 0x06000A54 RID: 2644
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetPathMethod(IntPtr methodsPtr, StringBuilder path);
		}

		// Token: 0x020001C5 RID: 453
		// (Invoke) Token: 0x06000A58 RID: 2648
		public delegate void ReadAsyncHandler(Result result, byte[] data);

		// Token: 0x020001C6 RID: 454
		// (Invoke) Token: 0x06000A5C RID: 2652
		public delegate void ReadAsyncPartialHandler(Result result, byte[] data);

		// Token: 0x020001C7 RID: 455
		// (Invoke) Token: 0x06000A60 RID: 2656
		public delegate void WriteAsyncHandler(Result result);
	}
}
