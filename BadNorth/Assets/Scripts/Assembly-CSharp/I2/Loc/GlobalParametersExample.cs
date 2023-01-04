using System;

namespace I2.Loc
{
	// Token: 0x020003C3 RID: 963
	public class GlobalParametersExample : RegisterGlobalParameters
	{
		// Token: 0x0600158F RID: 5519 RVA: 0x0002C83C File Offset: 0x0002AC3C
		public override string GetParameterValue(string ParamName)
		{
			if (ParamName == "WINNER")
			{
				return "Javier";
			}
			if (ParamName == "NUM PLAYERS")
			{
				return 5.ToString();
			}
			return null;
		}
	}
}
