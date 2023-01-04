using System;

namespace CS.Lights
{
	// Token: 0x02000390 RID: 912
	public static class Utility
	{
		// Token: 0x060014D1 RID: 5329 RVA: 0x0002B460 File Offset: 0x00029860
		public static bool ForDevice(DeviceType takes, DeviceType type)
		{
			return (takes & type) != DeviceType.UNKNOWN;
		}
	}
}
