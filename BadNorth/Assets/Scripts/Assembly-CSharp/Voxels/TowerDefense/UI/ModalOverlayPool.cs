using System;
using RTM.Pools;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008BE RID: 2238
	internal class ModalOverlayPool : MonoBehaviour, IGameSetup
	{
		// Token: 0x06003AF4 RID: 15092 RVA: 0x00105DB6 File Offset: 0x001041B6
		void IGameSetup.OnGameAwake()
		{
			ModalOverlayPool.pool = new LocalPool<ModalOverlay>(base.GetComponentsInChildren<ModalOverlay>(true), null);
			ModalOverlayPool.pool.ExpandTo(2);
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x00105DD5 File Offset: 0x001041D5
		public static ModalOverlay GetInstance()
		{
			return ModalOverlayPool.pool.GetInstance();
		}

		// Token: 0x040028F3 RID: 10483
		private static LocalPool<ModalOverlay> pool;
	}
}
