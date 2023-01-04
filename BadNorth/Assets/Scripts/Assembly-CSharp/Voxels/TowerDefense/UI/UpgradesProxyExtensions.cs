using System;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200092B RID: 2347
	public static class UpgradesProxyExtensions
	{
		// Token: 0x06003F0E RID: 16142 RVA: 0x0011C879 File Offset: 0x0011AC79
		public static bool IsUpgradingAllowed(this IUpgradesProxy p)
		{
			return p.gameOverReason == GameOverReason.None;
		}
	}
}
