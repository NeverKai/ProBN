using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200013B RID: 315
	public class RelationshipManager
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x0001D8A0 File Offset: 0x0001BCA0
		internal RelationshipManager(IntPtr ptr, IntPtr eventsPtr, ref RelationshipManager.FFIEvents events)
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0001D8F5 File Offset: 0x0001BCF5
		private RelationshipManager.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(RelationshipManager.FFIMethods));
				}
				return (RelationshipManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x060007F6 RID: 2038 RVA: 0x0001D928 File Offset: 0x0001BD28
		// (remove) Token: 0x060007F7 RID: 2039 RVA: 0x0001D960 File Offset: 0x0001BD60
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event RelationshipManager.RefreshHandler OnRefresh;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x060007F8 RID: 2040 RVA: 0x0001D998 File Offset: 0x0001BD98
		// (remove) Token: 0x060007F9 RID: 2041 RVA: 0x0001D9D0 File Offset: 0x0001BDD0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event RelationshipManager.RelationshipUpdateHandler OnRelationshipUpdate;

		// Token: 0x060007FA RID: 2042 RVA: 0x0001DA06 File Offset: 0x0001BE06
		private void InitEvents(IntPtr eventsPtr, ref RelationshipManager.FFIEvents events)
		{
			events.OnRefresh = delegate(IntPtr ptr)
			{
				if (this.OnRefresh != null)
				{
					this.OnRefresh();
				}
			};
			events.OnRelationshipUpdate = delegate(IntPtr ptr, ref Relationship relationship)
			{
				if (this.OnRelationshipUpdate != null)
				{
					this.OnRelationshipUpdate(ref relationship);
				}
			};
			Marshal.StructureToPtr(events, eventsPtr, false);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001DA40 File Offset: 0x0001BE40
		public void Filter(RelationshipManager.FilterHandler callback)
		{
			RelationshipManager.FFIMethods.FilterCallback callback2 = delegate(IntPtr ptr, ref Relationship relationship)
			{
				return callback(ref relationship);
			};
			this.Methods.Filter(this.MethodsPtr, IntPtr.Zero, callback2);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001DA88 File Offset: 0x0001BE88
		public int Count()
		{
			int result = 0;
			Result result2 = this.Methods.Count(this.MethodsPtr, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001DAC4 File Offset: 0x0001BEC4
		public Relationship Get(long userId)
		{
			Relationship result = default(Relationship);
			Result result2 = this.Methods.Get(this.MethodsPtr, userId, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001DB04 File Offset: 0x0001BF04
		public Relationship GetAt(uint index)
		{
			Relationship result = default(Relationship);
			Result result2 = this.Methods.GetAt(this.MethodsPtr, index, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x04000462 RID: 1122
		private IntPtr MethodsPtr;

		// Token: 0x04000463 RID: 1123
		private object MethodsStructure;

		// Token: 0x0200013C RID: 316
		internal struct FFIEvents
		{
			// Token: 0x04000466 RID: 1126
			internal RelationshipManager.FFIEvents.RefreshHandler OnRefresh;

			// Token: 0x04000467 RID: 1127
			internal RelationshipManager.FFIEvents.RelationshipUpdateHandler OnRelationshipUpdate;

			// Token: 0x0200013D RID: 317
			// (Invoke) Token: 0x06000802 RID: 2050
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void RefreshHandler(IntPtr ptr);

			// Token: 0x0200013E RID: 318
			// (Invoke) Token: 0x06000806 RID: 2054
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void RelationshipUpdateHandler(IntPtr ptr, ref Relationship relationship);
		}

		// Token: 0x0200013F RID: 319
		internal struct FFIMethods
		{
			// Token: 0x04000468 RID: 1128
			internal RelationshipManager.FFIMethods.FilterMethod Filter;

			// Token: 0x04000469 RID: 1129
			internal RelationshipManager.FFIMethods.CountMethod Count;

			// Token: 0x0400046A RID: 1130
			internal RelationshipManager.FFIMethods.GetMethod Get;

			// Token: 0x0400046B RID: 1131
			internal RelationshipManager.FFIMethods.GetAtMethod GetAt;

			// Token: 0x02000140 RID: 320
			// (Invoke) Token: 0x0600080A RID: 2058
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate bool FilterCallback(IntPtr ptr, ref Relationship relationship);

			// Token: 0x02000141 RID: 321
			// (Invoke) Token: 0x0600080E RID: 2062
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void FilterMethod(IntPtr methodsPtr, IntPtr callbackData, RelationshipManager.FFIMethods.FilterCallback callback);

			// Token: 0x02000142 RID: 322
			// (Invoke) Token: 0x06000812 RID: 2066
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result CountMethod(IntPtr methodsPtr, ref int count);

			// Token: 0x02000143 RID: 323
			// (Invoke) Token: 0x06000816 RID: 2070
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetMethod(IntPtr methodsPtr, long userId, ref Relationship relationship);

			// Token: 0x02000144 RID: 324
			// (Invoke) Token: 0x0600081A RID: 2074
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetAtMethod(IntPtr methodsPtr, uint index, ref Relationship relationship);
		}

		// Token: 0x02000145 RID: 325
		// (Invoke) Token: 0x0600081E RID: 2078
		public delegate bool FilterHandler(ref Relationship relationship);

		// Token: 0x02000146 RID: 326
		// (Invoke) Token: 0x06000822 RID: 2082
		public delegate void RefreshHandler();

		// Token: 0x02000147 RID: 327
		// (Invoke) Token: 0x06000826 RID: 2086
		public delegate void RelationshipUpdateHandler(ref Relationship relationship);
	}
}
