using System;

namespace Steamworks
{
	// Token: 0x0200032F RID: 815
	internal class CallbackIdentities
	{
		// Token: 0x06001224 RID: 4644 RVA: 0x0002704C File Offset: 0x0002544C
		public static int GetCallbackIdentity(Type callbackStruct)
		{
			object[] customAttributes = callbackStruct.GetCustomAttributes(typeof(CallbackIdentityAttribute), false);
			int num = 0;
			if (num >= customAttributes.Length)
			{
				throw new Exception("Callback number not found for struct " + callbackStruct);
			}
			CallbackIdentityAttribute callbackIdentityAttribute = (CallbackIdentityAttribute)customAttributes[num];
			return callbackIdentityAttribute.Identity;
		}
	}
}
