using System;

namespace Voxels.TowerDefense
{
	// Token: 0x020008BA RID: 2234
	[AttributeUsage(AttributeTargets.Method)]
	public class DebugSettingAttribute : Attribute
	{
		// Token: 0x06003ABB RID: 15035 RVA: 0x00105845 File Offset: 0x00103C45
		public DebugSettingAttribute(string name = "", DebugSettingLocation location = DebugSettingLocation.All)
		{
			this.name = name;
			this.location = location;
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x00105878 File Offset: 0x00103C78
		public DebugSettingAttribute(string name, string chineseName, DebugSettingLocation location = DebugSettingLocation.All)
		{
			this.name = name;
			this.chineseName = chineseName;
			this.location = location;
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x001058B2 File Offset: 0x00103CB2
		public DebugSettingAttribute(DebugSettingLocation location)
		{
			this.location = location;
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x001058DE File Offset: 0x00103CDE
		public static implicit operator bool(DebugSettingAttribute attr)
		{
			return attr != null;
		}

		// Token: 0x040028D5 RID: 10453
		public readonly string name = string.Empty;

		// Token: 0x040028D6 RID: 10454
		public readonly string chineseName = string.Empty;

		// Token: 0x040028D7 RID: 10455
		public readonly DebugSettingLocation location = DebugSettingLocation.All;
	}
}
