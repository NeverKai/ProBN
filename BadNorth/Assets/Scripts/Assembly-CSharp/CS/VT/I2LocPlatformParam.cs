using System;
using CS.Platform;
using CS.Platform.Utils;
using I2.Loc;

namespace CS.VT
{
	// Token: 0x02000388 RID: 904
	public class I2LocPlatformParam : RegisterGlobalParameters
	{
		// Token: 0x060014AE RID: 5294 RVA: 0x0002A59A File Offset: 0x0002899A
		private void Awake()
		{
			Requests.TextLocalise += this.Requests_TextLocalise;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0002A5AD File Offset: 0x000289AD
		private void OnDestroy()
		{
			Requests.TextLocalise -= this.Requests_TextLocalise;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0002A5C0 File Offset: 0x000289C0
		private string Requests_TextLocalise(string key, string[] parameters)
		{
			this.platformLocParams = parameters;
			string translation = LocalizationManager.GetTranslation(key, true, 0, true, true, null, null);
			this.platformLocParams = null;
			if (string.IsNullOrEmpty(translation))
			{
				return null;
			}
			return translation;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0002A5F8 File Offset: 0x000289F8
		public override string GetParameterValue(string Param)
		{
			if (Param != null)
			{
				if (Param == "MAIN_USER_NAME")
				{
					return BasePlatformManager.Instance.GetUserName();
				}
				if (Param == "CONTROLLER_TYPE")
				{
					return LocalizationManager.GetTranslation("SYSTEM/INPUT/DEFAULT/CONTROLLER", true, 0, true, false, null, null);
				}
			}
			if (this.platformLocParams != null && Param.Length == 7 && Param.IndexOf("PARAM_") == 0)
			{
				int num = (int)(Param[6] - '0');
				if (num < this.platformLocParams.Length)
				{
					return this.platformLocParams[num];
				}
			}
			return null;
		}

		// Token: 0x04000CD8 RID: 3288
		private string[] platformLocParams;
	}
}
