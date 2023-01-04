using System;
using UnityEngine;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense.Forestry
{
	// Token: 0x02000765 RID: 1893
	public class TreeShadow : MonoBehaviour, ITreePlanter
	{
		// Token: 0x06003144 RID: 12612 RVA: 0x000CBEF8 File Offset: 0x000CA2F8
		public void OnTreePlanted(Tree tree, Shoot shoot, Vector3 normal)
		{
			if (shoot.navPos.GetBorderDistance() < shoot.radius * 1.4f)
			{
				base.transform.rotation = Quaternion.LookRotation(shoot.navPos.GetNormal(), shoot.navPos.GetBorderVector()) * Quaternion.Euler(0f, 0f, -45f);
			}
			MergeableSprite component = base.gameObject.GetComponent<MergeableSprite>();
			component.navPos = shoot.navPos;
		}
	}
}
