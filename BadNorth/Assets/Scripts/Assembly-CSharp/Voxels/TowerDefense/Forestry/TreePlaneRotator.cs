using System;
using UnityEngine;

namespace Voxels.TowerDefense.Forestry
{
	// Token: 0x02000763 RID: 1891
	public class TreePlaneRotator : MonoBehaviour, ITreePlanter
	{
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x0600313F RID: 12607 RVA: 0x000CBE72 File Offset: 0x000CA272
		private SpriteRenderer spriteRenderer
		{
			get
			{
				if (!this._sr)
				{
					this._sr = base.GetComponent<SpriteRenderer>();
				}
				return this._sr;
			}
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x000CBE96 File Offset: 0x000CA296
		public void OnTreePlanted(Tree tree, Shoot shoot, Vector3 normal)
		{
			base.transform.rotation = Quaternion.LookRotation(normal, base.transform.right);
		}

		// Token: 0x0400210C RID: 8460
		private SpriteRenderer _sr;
	}
}
