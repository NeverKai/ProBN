using System;
using UnityEngine;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense.Forestry
{
	// Token: 0x02000760 RID: 1888
	[SelectionBase]
	public class Tree : MonoBehaviour
	{
		// Token: 0x06003137 RID: 12599 RVA: 0x000CBC10 File Offset: 0x000CA010
		public void PlantTree(Shoot shoot, Transform container, Vector3 pos, Vector3 normal)
		{
			Tree tree = UnityEngine.Object.Instantiate<Tree>(this, container);
			Vector3 localScale = Vector3.one * (shoot.radius / this.radius);
			tree.transform.position = pos;
			tree.transform.localScale = localScale;
			tree.transform.Rotate(0f, UnityEngine.Random.value * 360f, 0f);
			foreach (SpriteRenderer spriteRenderer in tree.EnumerateComponentsInChildren(false, true))
			{
				spriteRenderer.gameObject.AddComponent<MergeableSprite>();
			}
			foreach (ITreePlanter treePlanter in tree.EnumerateComponentsInChildren(false, true))
			{
				treePlanter.OnTreePlanted(tree, shoot, normal);
			}
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000CBD1C File Offset: 0x000CA11C
		private void OnDrawGizmos()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			ExtraGizmos.DrawCircle(Vector3.zero, this.radius, 8);
		}

		// Token: 0x04002107 RID: 8455
		public float radius = 1f;
	}
}
