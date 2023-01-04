using System;
using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x0200040F RID: 1039
	public class LocalizationParamsManager : MonoBehaviour, ILocalizationParamsManager
	{
		// Token: 0x06001819 RID: 6169 RVA: 0x0003D288 File Offset: 0x0003B688
		public string GetParameterValue(string ParamName)
		{
			if (this._Params != null)
			{
				int i = 0;
				int count = this._Params.Count;
				while (i < count)
				{
					if (this._Params[i].Name == ParamName)
					{
						return this._Params[i].Value;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0003D2F4 File Offset: 0x0003B6F4
		public void SetParameterValue(string ParamName, string ParamValue, bool localize = true)
		{
			bool flag = false;
			int i = 0;
			int count = this._Params.Count;
			while (i < count)
			{
				if (this._Params[i].Name == ParamName)
				{
					LocalizationParamsManager.ParamValue value = this._Params[i];
					value.Value = ParamValue;
					this._Params[i] = value;
					flag = true;
					break;
				}
				i++;
			}
			if (!flag)
			{
				this._Params.Add(new LocalizationParamsManager.ParamValue
				{
					Name = ParamName,
					Value = ParamValue
				});
			}
			if (localize)
			{
				this.OnLocalize();
			}
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0003D3A4 File Offset: 0x0003B7A4
		public void OnLocalize()
		{
			Localize component = base.GetComponent<Localize>();
			if (component != null)
			{
				component.OnLocalize(true);
			}
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x0003D3CB File Offset: 0x0003B7CB
		public virtual void OnEnable()
		{
			if (this._IsGlobalManager)
			{
				this.DoAutoRegister();
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0003D3DE File Offset: 0x0003B7DE
		public void DoAutoRegister()
		{
			if (!LocalizationManager.ParamManagers.Contains(this))
			{
				LocalizationManager.ParamManagers.Add(this);
				LocalizationManager.LocalizeAll(true);
			}
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0003D401 File Offset: 0x0003B801
		public void OnDisable()
		{
			LocalizationManager.ParamManagers.Remove(this);
		}

		// Token: 0x04000EB4 RID: 3764
		[SerializeField]
		public List<LocalizationParamsManager.ParamValue> _Params = new List<LocalizationParamsManager.ParamValue>();

		// Token: 0x04000EB5 RID: 3765
		public bool _IsGlobalManager;

		// Token: 0x02000410 RID: 1040
		[Serializable]
		public struct ParamValue
		{
			// Token: 0x04000EB6 RID: 3766
			public string Name;

			// Token: 0x04000EB7 RID: 3767
			public string Value;
		}
	}
}
