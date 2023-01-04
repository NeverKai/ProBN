using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Discord
{
	// Token: 0x020000FF RID: 255
	public class ApplicationManager
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x0001D210 File Offset: 0x0001B610
		internal ApplicationManager(IntPtr ptr, IntPtr eventsPtr, ref ApplicationManager.FFIEvents events)
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001D265 File Offset: 0x0001B665
		private ApplicationManager.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(ApplicationManager.FFIMethods));
				}
				return (ApplicationManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001D298 File Offset: 0x0001B698
		private void InitEvents(IntPtr eventsPtr, ref ApplicationManager.FFIEvents events)
		{
			Marshal.StructureToPtr(events, eventsPtr, false);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001D2AC File Offset: 0x0001B6AC
		public void ValidateOrExit(ApplicationManager.ValidateOrExitHandler callback)
		{
			ApplicationManager.FFIMethods.ValidateOrExitCallback validateOrExitCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.ValidateOrExit(this.MethodsPtr, Utility.Retain<ApplicationManager.FFIMethods.ValidateOrExitCallback>(validateOrExitCallback), validateOrExitCallback);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001D2F4 File Offset: 0x0001B6F4
		public string GetCurrentLocale()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			this.Methods.GetCurrentLocale(this.MethodsPtr, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001D32C File Offset: 0x0001B72C
		public string GetCurrentBranch()
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			this.Methods.GetCurrentBranch(this.MethodsPtr, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001D364 File Offset: 0x0001B764
		public void GetOAuth2Token(ApplicationManager.GetOAuth2TokenHandler callback)
		{
			ApplicationManager.FFIMethods.GetOAuth2TokenCallback getOAuth2TokenCallback = delegate(IntPtr ptr, Result result, ref OAuth2Token oauth2Token)
			{
				Utility.Release(ptr);
				callback(result, ref oauth2Token);
			};
			this.Methods.GetOAuth2Token(this.MethodsPtr, Utility.Retain<ApplicationManager.FFIMethods.GetOAuth2TokenCallback>(getOAuth2TokenCallback), getOAuth2TokenCallback);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001D3AC File Offset: 0x0001B7AC
		public void GetTicket(ApplicationManager.GetTicketHandler callback)
		{
			ApplicationManager.FFIMethods.GetTicketCallback getTicketCallback = delegate(IntPtr ptr, Result result, ref string data)
			{
				Utility.Release(ptr);
				callback(result, ref data);
			};
			this.Methods.GetTicket(this.MethodsPtr, Utility.Retain<ApplicationManager.FFIMethods.GetTicketCallback>(getTicketCallback), getTicketCallback);
		}

		// Token: 0x04000443 RID: 1091
		private IntPtr MethodsPtr;

		// Token: 0x04000444 RID: 1092
		private object MethodsStructure;

		// Token: 0x02000100 RID: 256
		internal struct FFIEvents
		{
		}

		// Token: 0x02000101 RID: 257
		internal struct FFIMethods
		{
			// Token: 0x04000445 RID: 1093
			internal ApplicationManager.FFIMethods.ValidateOrExitMethod ValidateOrExit;

			// Token: 0x04000446 RID: 1094
			internal ApplicationManager.FFIMethods.GetCurrentLocaleMethod GetCurrentLocale;

			// Token: 0x04000447 RID: 1095
			internal ApplicationManager.FFIMethods.GetCurrentBranchMethod GetCurrentBranch;

			// Token: 0x04000448 RID: 1096
			internal ApplicationManager.FFIMethods.GetOAuth2TokenMethod GetOAuth2Token;

			// Token: 0x04000449 RID: 1097
			internal ApplicationManager.FFIMethods.GetTicketMethod GetTicket;

			// Token: 0x02000102 RID: 258
			// (Invoke) Token: 0x0600071E RID: 1822
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ValidateOrExitCallback(IntPtr ptr, Result result);

			// Token: 0x02000103 RID: 259
			// (Invoke) Token: 0x06000722 RID: 1826
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ValidateOrExitMethod(IntPtr methodsPtr, IntPtr callbackData, ApplicationManager.FFIMethods.ValidateOrExitCallback callback);

			// Token: 0x02000104 RID: 260
			// (Invoke) Token: 0x06000726 RID: 1830
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void GetCurrentLocaleMethod(IntPtr methodsPtr, StringBuilder locale);

			// Token: 0x02000105 RID: 261
			// (Invoke) Token: 0x0600072A RID: 1834
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void GetCurrentBranchMethod(IntPtr methodsPtr, StringBuilder branch);

			// Token: 0x02000106 RID: 262
			// (Invoke) Token: 0x0600072E RID: 1838
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void GetOAuth2TokenCallback(IntPtr ptr, Result result, ref OAuth2Token oauth2Token);

			// Token: 0x02000107 RID: 263
			// (Invoke) Token: 0x06000732 RID: 1842
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void GetOAuth2TokenMethod(IntPtr methodsPtr, IntPtr callbackData, ApplicationManager.FFIMethods.GetOAuth2TokenCallback callback);

			// Token: 0x02000108 RID: 264
			// (Invoke) Token: 0x06000736 RID: 1846
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void GetTicketCallback(IntPtr ptr, Result result, [MarshalAs(UnmanagedType.LPStr)] ref string data);

			// Token: 0x02000109 RID: 265
			// (Invoke) Token: 0x0600073A RID: 1850
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void GetTicketMethod(IntPtr methodsPtr, IntPtr callbackData, ApplicationManager.FFIMethods.GetTicketCallback callback);
		}

		// Token: 0x0200010A RID: 266
		// (Invoke) Token: 0x0600073E RID: 1854
		public delegate void ValidateOrExitHandler(Result result);

		// Token: 0x0200010B RID: 267
		// (Invoke) Token: 0x06000742 RID: 1858
		public delegate void GetOAuth2TokenHandler(Result result, ref OAuth2Token oauth2Token);

		// Token: 0x0200010C RID: 268
		// (Invoke) Token: 0x06000746 RID: 1862
		public delegate void GetTicketHandler(Result result, ref string data);
	}
}
