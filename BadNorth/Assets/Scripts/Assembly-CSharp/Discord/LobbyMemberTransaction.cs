using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000DE RID: 222
	public struct LobbyMemberTransaction
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001C67E File Offset: 0x0001AA7E
		private LobbyMemberTransaction.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(LobbyMemberTransaction.FFIMethods));
				}
				return (LobbyMemberTransaction.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001C6B4 File Offset: 0x0001AAB4
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

		// Token: 0x0600069C RID: 1692 RVA: 0x0001C700 File Offset: 0x0001AB00
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

		// Token: 0x040003E4 RID: 996
		internal IntPtr MethodsPtr;

		// Token: 0x040003E5 RID: 997
		internal object MethodsStructure;

		// Token: 0x020000DF RID: 223
		internal struct FFIMethods
		{
			// Token: 0x040003E6 RID: 998
			internal LobbyMemberTransaction.FFIMethods.SetMetadataMethod SetMetadata;

			// Token: 0x040003E7 RID: 999
			internal LobbyMemberTransaction.FFIMethods.DeleteMetadataMethod DeleteMetadata;

			// Token: 0x020000E0 RID: 224
			// (Invoke) Token: 0x0600069E RID: 1694
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result SetMetadataMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key, [MarshalAs(UnmanagedType.LPStr)] string value);

			// Token: 0x020000E1 RID: 225
			// (Invoke) Token: 0x060006A2 RID: 1698
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result DeleteMetadataMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key);
		}
	}
}
