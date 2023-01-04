using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x02000412 RID: 1042
	public class RegisterGlobalParameters : MonoBehaviour, ILocalizationParamsManager
	{
		// Token: 0x06001824 RID: 6180 RVA: 0x0002A55E File Offset: 0x0002895E
		public virtual void OnEnable()
		{
			if (!LocalizationManager.ParamManagers.Contains(this))
			{
				LocalizationManager.ParamManagers.Add(this);
				LocalizationManager.LocalizeAll(true);
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0002A581 File Offset: 0x00028981
		public virtual void OnDisable()
		{
			LocalizationManager.ParamManagers.Remove(this);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0002A58F File Offset: 0x0002898F
		public virtual string GetParameterValue(string ParamName)
		{
			return null;
		}
	}
}
