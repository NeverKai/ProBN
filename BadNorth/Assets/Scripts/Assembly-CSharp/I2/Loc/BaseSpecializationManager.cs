using System;
using System.Collections.Generic;

namespace I2.Loc
{
	// Token: 0x020003CB RID: 971
	public class BaseSpecializationManager
	{
		// Token: 0x060015B9 RID: 5561 RVA: 0x0002D2CC File Offset: 0x0002B6CC
		public virtual void InitializeSpecializations()
		{
			this.mSpecializations = new string[]
			{
				"Any",
				"PC",
				"Touch",
				"Controller",
				"VR",
				"XBox",
				"PS4",
				"OculusVR",
				"ViveVR",
				"GearVR",
				"Android",
				"IOS"
			};
			this.mSpecializationsFallbacks = new Dictionary<string, string>
			{
				{
					"XBox",
					"Controller"
				},
				{
					"PS4",
					"Controller"
				},
				{
					"OculusVR",
					"VR"
				},
				{
					"ViveVR",
					"VR"
				},
				{
					"GearVR",
					"VR"
				},
				{
					"Android",
					"Touch"
				},
				{
					"IOS",
					"Touch"
				}
			};
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0002D3C6 File Offset: 0x0002B7C6
		public virtual string GetCurrentSpecialization()
		{
			if (this.mSpecializations == null)
			{
				this.InitializeSpecializations();
			}
			return "PC";
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0002D3E0 File Offset: 0x0002B7E0
		public virtual string GetFallbackSpecialization(string specialization)
		{
			if (this.mSpecializationsFallbacks == null)
			{
				this.InitializeSpecializations();
			}
			string result;
			if (this.mSpecializationsFallbacks.TryGetValue(specialization, out result))
			{
				return result;
			}
			return "Any";
		}

		// Token: 0x04000DB2 RID: 3506
		public string[] mSpecializations;

		// Token: 0x04000DB3 RID: 3507
		public Dictionary<string, string> mSpecializationsFallbacks;
	}
}
