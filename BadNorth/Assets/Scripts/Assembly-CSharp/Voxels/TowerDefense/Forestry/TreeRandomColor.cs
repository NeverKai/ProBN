using System;
using UnityEngine;

namespace Voxels.TowerDefense.Forestry
{
	// Token: 0x02000764 RID: 1892
	public class TreeRandomColor : MonoBehaviour, ITreePlanter
	{
		// Token: 0x06003142 RID: 12610 RVA: 0x000CBEBC File Offset: 0x000CA2BC
		public void OnTreePlanted(Tree tree, Shoot shoot, Vector3 normal)
		{
			SpriteRenderer component = base.GetComponent<SpriteRenderer>();
			component.color = new Color(shoot.fraction, 1f, 1f, 1f);
		}
	}
}
