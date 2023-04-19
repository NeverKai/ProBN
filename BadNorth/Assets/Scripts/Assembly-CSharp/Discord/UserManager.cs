using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x0200010D RID: 269
	public class UserManager
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x0001D44C File Offset: 0x0001B84C
		internal UserManager(IntPtr ptr, IntPtr eventsPtr, ref UserManager.FFIEvents events)
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001D4A1 File Offset: 0x0001B8A1
		private UserManager.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(UserManager.FFIMethods));
				}
				return (UserManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600074B RID: 1867 RVA: 0x0001D4D4 File Offset: 0x0001B8D4
		// (remove) Token: 0x0600074C RID: 1868 RVA: 0x0001D50C File Offset: 0x0001B90C
		public event UserManager.CurrentUserUpdateHandler OnCurrentUserUpdate;

		// Token: 0x0600074D RID: 1869 RVA: 0x0001D542 File Offset: 0x0001B942
		private void InitEvents(IntPtr eventsPtr, ref UserManager.FFIEvents events)
		{
			events.OnCurrentUserUpdate = delegate(IntPtr ptr)
			{
				if (this.OnCurrentUserUpdate != null)
				{
					this.OnCurrentUserUpdate();
				}
			};
			Marshal.StructureToPtr(events, eventsPtr, false);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001D568 File Offset: 0x0001B968
		public User GetCurrentUser()
		{
			User result = default(User);
			Result result2 = this.Methods.GetCurrentUser(this.MethodsPtr, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001D5A8 File Offset: 0x0001B9A8
		public void GetUser(long userId, UserManager.GetUserHandler callback)
		{
			UserManager.FFIMethods.GetUserCallback getUserCallback = delegate(IntPtr ptr, Result result, ref User user)
			{
				Utility.Release(ptr);
				callback(result, ref user);
			};
			this.Methods.GetUser(this.MethodsPtr, userId, Utility.Retain<UserManager.FFIMethods.GetUserCallback>(getUserCallback), getUserCallback);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001D5F0 File Offset: 0x0001B9F0
		public PremiumType GetCurrentUserPremiumType()
		{
			PremiumType result = PremiumType.None;
			Result result2 = this.Methods.GetCurrentUserPremiumType(this.MethodsPtr, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001D62C File Offset: 0x0001BA2C
		public bool CurrentUserHasFlag(UserFlag flag)
		{
			bool result = false;
			Result result2 = this.Methods.CurrentUserHasFlag(this.MethodsPtr, flag, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0400044A RID: 1098
		private IntPtr MethodsPtr;

		// Token: 0x0400044B RID: 1099
		private object MethodsStructure;

		// Token: 0x0200010E RID: 270
		internal struct FFIEvents
		{
			// Token: 0x0400044D RID: 1101
			internal UserManager.FFIEvents.CurrentUserUpdateHandler OnCurrentUserUpdate;

			// Token: 0x0200010F RID: 271
			// (Invoke) Token: 0x06000754 RID: 1876
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void CurrentUserUpdateHandler(IntPtr ptr);
		}

		// Token: 0x02000110 RID: 272
		internal struct FFIMethods
		{
			// Token: 0x0400044E RID: 1102
			internal UserManager.FFIMethods.GetCurrentUserMethod GetCurrentUser;

			// Token: 0x0400044F RID: 1103
			internal UserManager.FFIMethods.GetUserMethod GetUser;

			// Token: 0x04000450 RID: 1104
			internal UserManager.FFIMethods.GetCurrentUserPremiumTypeMethod GetCurrentUserPremiumType;

			// Token: 0x04000451 RID: 1105
			internal UserManager.FFIMethods.CurrentUserHasFlagMethod CurrentUserHasFlag;

			// Token: 0x02000111 RID: 273
			// (Invoke) Token: 0x06000758 RID: 1880
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetCurrentUserMethod(IntPtr methodsPtr, ref User currentUser);

			// Token: 0x02000112 RID: 274
			// (Invoke) Token: 0x0600075C RID: 1884
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void GetUserCallback(IntPtr ptr, Result result, ref User user);

			// Token: 0x02000113 RID: 275
			// (Invoke) Token: 0x06000760 RID: 1888
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void GetUserMethod(IntPtr methodsPtr, long userId, IntPtr callbackData, UserManager.FFIMethods.GetUserCallback callback);

			// Token: 0x02000114 RID: 276
			// (Invoke) Token: 0x06000764 RID: 1892
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetCurrentUserPremiumTypeMethod(IntPtr methodsPtr, ref PremiumType premiumType);

			// Token: 0x02000115 RID: 277
			// (Invoke) Token: 0x06000768 RID: 1896
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result CurrentUserHasFlagMethod(IntPtr methodsPtr, UserFlag flag, ref bool hasFlag);
		}

		// Token: 0x02000116 RID: 278
		// (Invoke) Token: 0x0600076C RID: 1900
		public delegate void GetUserHandler(Result result, ref User user);

		// Token: 0x02000117 RID: 279
		// (Invoke) Token: 0x06000770 RID: 1904
		public delegate void CurrentUserUpdateHandler();
	}
}
