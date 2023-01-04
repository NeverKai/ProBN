using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000E2 RID: 226
	public struct LobbySearchQuery
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001C74A File Offset: 0x0001AB4A
		private LobbySearchQuery.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(LobbySearchQuery.FFIMethods));
				}
				return (LobbySearchQuery.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001C780 File Offset: 0x0001AB80
		public void Filter(string key, LobbySearchComparison comparison, LobbySearchCast cast, string value)
		{
			if (this.MethodsPtr != IntPtr.Zero)
			{
				Result result = this.Methods.Filter(this.MethodsPtr, key, comparison, cast, value);
				if (result != Result.Ok)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001C7D0 File Offset: 0x0001ABD0
		public void Sort(string key, LobbySearchCast cast, string value)
		{
			if (this.MethodsPtr != IntPtr.Zero)
			{
				Result result = this.Methods.Sort(this.MethodsPtr, key, cast, value);
				if (result != Result.Ok)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001C81C File Offset: 0x0001AC1C
		public void Limit(uint limit)
		{
			if (this.MethodsPtr != IntPtr.Zero)
			{
				Result result = this.Methods.Limit(this.MethodsPtr, limit);
				if (result != Result.Ok)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001C868 File Offset: 0x0001AC68
		public void Distance(LobbySearchDistance distance)
		{
			if (this.MethodsPtr != IntPtr.Zero)
			{
				Result result = this.Methods.Distance(this.MethodsPtr, distance);
				if (result != Result.Ok)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x040003E8 RID: 1000
		internal IntPtr MethodsPtr;

		// Token: 0x040003E9 RID: 1001
		internal object MethodsStructure;

		// Token: 0x020000E3 RID: 227
		internal struct FFIMethods
		{
			// Token: 0x040003EA RID: 1002
			internal LobbySearchQuery.FFIMethods.FilterMethod Filter;

			// Token: 0x040003EB RID: 1003
			internal LobbySearchQuery.FFIMethods.SortMethod Sort;

			// Token: 0x040003EC RID: 1004
			internal LobbySearchQuery.FFIMethods.LimitMethod Limit;

			// Token: 0x040003ED RID: 1005
			internal LobbySearchQuery.FFIMethods.DistanceMethod Distance;

			// Token: 0x020000E4 RID: 228
			// (Invoke) Token: 0x060006AB RID: 1707
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result FilterMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key, LobbySearchComparison comparison, LobbySearchCast cast, [MarshalAs(UnmanagedType.LPStr)] string value);

			// Token: 0x020000E5 RID: 229
			// (Invoke) Token: 0x060006AF RID: 1711
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result SortMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key, LobbySearchCast cast, [MarshalAs(UnmanagedType.LPStr)] string value);

			// Token: 0x020000E6 RID: 230
			// (Invoke) Token: 0x060006B3 RID: 1715
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result LimitMethod(IntPtr methodsPtr, uint limit);

			// Token: 0x020000E7 RID: 231
			// (Invoke) Token: 0x060006B7 RID: 1719
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result DistanceMethod(IntPtr methodsPtr, LobbySearchDistance distance);
		}
	}
}
