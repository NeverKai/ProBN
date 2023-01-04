using System;

namespace Voxels.TowerDefense
{
	// Token: 0x02000869 RID: 2153
	public interface IWalletListener
	{
		// Token: 0x06003870 RID: 14448
		void OnWalletChange(int oldBalance, int newBalance, Wallet wallet);
	}
}
