using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Steamworks
{
	// Token: 0x02000331 RID: 817
	public class InteropHelp
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x000270C7 File Offset: 0x000254C7
		public static void TestIfPlatformSupported()
		{
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x000270C9 File Offset: 0x000254C9
		public static void TestIfAvailableClient()
		{
			InteropHelp.TestIfPlatformSupported();
			if (NativeMethods.SteamClient() == IntPtr.Zero)
			{
				throw new InvalidOperationException("Steamworks is not initialized.");
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x000270EF File Offset: 0x000254EF
		public static void TestIfAvailableGameServer()
		{
			InteropHelp.TestIfPlatformSupported();
			if (NativeMethods.SteamGameServerClient() == IntPtr.Zero)
			{
				throw new InvalidOperationException("Steamworks is not initialized.");
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00027118 File Offset: 0x00025518
		public static string PtrToStringUTF8(IntPtr nativeUtf8)
		{
			if (nativeUtf8 == IntPtr.Zero)
			{
				return null;
			}
			int num = 0;
			while (Marshal.ReadByte(nativeUtf8, num) != 0)
			{
				num++;
			}
			if (num == 0)
			{
				return string.Empty;
			}
			byte[] array = new byte[num];
			Marshal.Copy(nativeUtf8, array, 0, array.Length);
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x02000332 RID: 818
		public class UTF8StringHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x0600122D RID: 4653 RVA: 0x00027178 File Offset: 0x00025578
			public UTF8StringHandle(string str) : base(true)
			{
				if (str == null)
				{
					base.SetHandle(IntPtr.Zero);
					return;
				}
				byte[] array = new byte[Encoding.UTF8.GetByteCount(str) + 1];
				Encoding.UTF8.GetBytes(str, 0, str.Length, array, 0);
				IntPtr intPtr = Marshal.AllocHGlobal(array.Length);
				Marshal.Copy(array, 0, intPtr, array.Length);
				base.SetHandle(intPtr);
			}

			// Token: 0x0600122E RID: 4654 RVA: 0x000271E1 File Offset: 0x000255E1
			protected override bool ReleaseHandle()
			{
				if (!this.IsInvalid)
				{
					Marshal.FreeHGlobal(this.handle);
				}
				return true;
			}
		}

		// Token: 0x02000333 RID: 819
		public class SteamParamStringArray
		{
			// Token: 0x0600122F RID: 4655 RVA: 0x000271FC File Offset: 0x000255FC
			public SteamParamStringArray(IList<string> strings)
			{
				if (strings == null)
				{
					this.m_pSteamParamStringArray = IntPtr.Zero;
					return;
				}
				this.m_Strings = new IntPtr[strings.Count];
				for (int i = 0; i < strings.Count; i++)
				{
					byte[] array = new byte[Encoding.UTF8.GetByteCount(strings[i]) + 1];
					Encoding.UTF8.GetBytes(strings[i], 0, strings[i].Length, array, 0);
					this.m_Strings[i] = Marshal.AllocHGlobal(array.Length);
					Marshal.Copy(array, 0, this.m_Strings[i], array.Length);
				}
				this.m_ptrStrings = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * this.m_Strings.Length);
				SteamParamStringArray_t steamParamStringArray_t = new SteamParamStringArray_t
				{
					m_ppStrings = this.m_ptrStrings,
					m_nNumStrings = this.m_Strings.Length
				};
				Marshal.Copy(this.m_Strings, 0, steamParamStringArray_t.m_ppStrings, this.m_Strings.Length);
				this.m_pSteamParamStringArray = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SteamParamStringArray_t)));
				Marshal.StructureToPtr(steamParamStringArray_t, this.m_pSteamParamStringArray, false);
			}

			// Token: 0x06001230 RID: 4656 RVA: 0x0002733C File Offset: 0x0002573C
			protected void Finalize()
			{
				try
				{
					foreach (IntPtr hglobal in this.m_Strings)
					{
						Marshal.FreeHGlobal(hglobal);
					}
					if (this.m_ptrStrings != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(this.m_ptrStrings);
					}
					if (this.m_pSteamParamStringArray != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(this.m_pSteamParamStringArray);
					}
				}
				finally
				{
					//base.Finalize();
				}
			}

			// Token: 0x06001231 RID: 4657 RVA: 0x000273CC File Offset: 0x000257CC
			public static implicit operator IntPtr(InteropHelp.SteamParamStringArray that)
			{
				return that.m_pSteamParamStringArray;
			}

			// Token: 0x04000C43 RID: 3139
			private IntPtr[] m_Strings;

			// Token: 0x04000C44 RID: 3140
			private IntPtr m_ptrStrings;

			// Token: 0x04000C45 RID: 3141
			private IntPtr m_pSteamParamStringArray;
		}
	}
}
