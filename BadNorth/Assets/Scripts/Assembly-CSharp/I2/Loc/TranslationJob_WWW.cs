using System;
using UnityEngine.Networking;

namespace I2.Loc
{
	// Token: 0x020003DC RID: 988
	public class TranslationJob_WWW : TranslationJob
	{
		// Token: 0x06001668 RID: 5736 RVA: 0x000348DD File Offset: 0x00032CDD
		public override void Dispose()
		{
			if (this.www != null)
			{
				this.www.Dispose();
			}
			this.www = null;
		}

		// Token: 0x04000DDD RID: 3549
		public UnityWebRequest www;
	}
}
