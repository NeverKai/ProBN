using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000D6 RID: 214
	public struct LobbyTransaction
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001C482 File Offset: 0x0001A882
		private LobbyTransaction.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(LobbyTransaction.FFIMethods));
				}
				return (LobbyTransaction.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001C4B8 File Offset: 0x0001A8B8
		public void SetType(LobbyType type)
		{
			if (this.MethodsPtr != IntPtr.Zero)
			{
				Result result = this.Methods.SetType(this.MethodsPtr, type);
				if (result != Result.Ok)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001C504 File Offset: 0x0001A904
		public void SetOwner(long ownerId)
		{
			if (this.MethodsPtr != IntPtr.Zero)
			{
				Result result = this.Methods.SetOwner(this.MethodsPtr, ownerId);
				if (result != Result.Ok)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001C550 File Offset: 0x0001A950
		public void SetCapacity(uint capacity)
		{
			if (this.MethodsPtr != IntPtr.Zero)
			{
				Result result = this.Methods.SetCapacity(this.MethodsPtr, capacity);
				if (result != Result.Ok)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001C59C File Offset: 0x0001A99C
		public void SetMetadata(string key, string value)
		{
			if (this.MethodsPtr != IntPtr.Zero)
			{
				Result result = this.Methods.SetMetadata(this.MethodsPtr, key, value);
				if (result != Result.Ok)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001C5E8 File Offset: 0x0001A9E8
		public void DeleteMetadata(string key)
		{
			if (this.MethodsPtr != IntPtr.Zero)
			{
				Result result = this.Methods.DeleteMetadata(this.MethodsPtr, key);
				if (result != Result.Ok)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001C634 File Offset: 0x0001AA34
		public void SetLocked(bool locked)
		{
			if (this.MethodsPtr != IntPtr.Zero)
			{
				Result result = this.Methods.SetLocked(this.MethodsPtr, locked);
				if (result != Result.Ok)
				{
					throw new ResultException(result);
				}
			}
		}

		// Token: 0x040003DC RID: 988
		internal IntPtr MethodsPtr;

		// Token: 0x040003DD RID: 989
		internal object MethodsStructure;

		// Token: 0x020000D7 RID: 215
		internal struct FFIMethods
		{
			// Token: 0x040003DE RID: 990
			internal LobbyTransaction.FFIMethods.SetTypeMethod SetType;

			// Token: 0x040003DF RID: 991
			internal LobbyTransaction.FFIMethods.SetOwnerMethod SetOwner;

			// Token: 0x040003E0 RID: 992
			internal LobbyTransaction.FFIMethods.SetCapacityMethod SetCapacity;

			// Token: 0x040003E1 RID: 993
			internal LobbyTransaction.FFIMethods.SetMetadataMethod SetMetadata;

			// Token: 0x040003E2 RID: 994
			internal LobbyTransaction.FFIMethods.DeleteMetadataMethod DeleteMetadata;

			// Token: 0x040003E3 RID: 995
			internal LobbyTransaction.FFIMethods.SetLockedMethod SetLocked;

			// Token: 0x020000D8 RID: 216
			// (Invoke) Token: 0x06000683 RID: 1667
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result SetTypeMethod(IntPtr methodsPtr, LobbyType type);

			// Token: 0x020000D9 RID: 217
			// (Invoke) Token: 0x06000687 RID: 1671
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result SetOwnerMethod(IntPtr methodsPtr, long ownerId);

			// Token: 0x020000DA RID: 218
			// (Invoke) Token: 0x0600068B RID: 1675
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result SetCapacityMethod(IntPtr methodsPtr, uint capacity);

			// Token: 0x020000DB RID: 219
			// (Invoke) Token: 0x0600068F RID: 1679
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result SetMetadataMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key, [MarshalAs(UnmanagedType.LPStr)] string value);

			// Token: 0x020000DC RID: 220
			// (Invoke) Token: 0x06000693 RID: 1683
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result DeleteMetadataMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key);

			// Token: 0x020000DD RID: 221
			// (Invoke) Token: 0x06000697 RID: 1687
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result SetLockedMethod(IntPtr methodsPtr, bool locked);
		}
	}
}
