using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007D9 RID: 2009
	public class SpriteMerger : MonoBehaviour
	{
		// Token: 0x06003416 RID: 13334 RVA: 0x000E1574 File Offset: 0x000DF974
		private SpriteMergerObject GetMergedSprites(MergeableSprite sr)
		{
			Material sharedMaterial = sr.spriteRenderer.sharedMaterial;
			int key = sharedMaterial.GetHashCode() + sr.spriteRenderer.sprite.texture.GetHashCode();
			SpriteMergerObject @new;
			if (!this.mergedSpriteDict.TryGetValue(key, out @new))
			{
				@new = SpriteMergerObject.GetNew(sr);
				@new.transform.SetParent(base.transform);
				this.mergedSpriteDict.Add(key, @new);
				this.mergedSpriteList.Add(@new);
			}
			return @new;
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000E15F0 File Offset: 0x000DF9F0
		public void HarvestChildren(Transform parent, SpriteMerger.Mode mode = SpriteMerger.Mode.none)
		{
			List<MergeableSprite> list = ListPool<MergeableSprite>.GetList(512);
			parent.GetComponentsInChildren<MergeableSprite>(list);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].randomFlip)
				{
					list[i].spriteRenderer.flipX = (UnityEngine.Random.value > 0.5f);
				}
				this.GetMergedSprites(list[i]).Add(list[i]);
			}
			for (int j = 0; j < this.mergedSpriteList.Count; j++)
			{
				this.mergedSpriteList[j].Merge();
			}
			if (mode == SpriteMerger.Mode.destroyComponent)
			{
				for (int k = 0; k < list.Count; k++)
				{
					UnityEngine.Object.Destroy(list[k]);
				}
			}
			else if (mode == SpriteMerger.Mode.destroyGameObject)
			{
				for (int l = 0; l < list.Count; l++)
				{
					UnityEngine.Object.Destroy(list[l].gameObject);
				}
			}
			else if (mode == SpriteMerger.Mode.disableComponent)
			{
				for (int m = 0; m < list.Count; m++)
				{
					list[m].enabled = false;
				}
			}
			else if (mode == SpriteMerger.Mode.disableComponent)
			{
				for (int n = 0; n < list.Count; n++)
				{
					list[n].gameObject.SetActive(false);
				}
			}
			list.ReturnToListPool<MergeableSprite>();
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000E1774 File Offset: 0x000DFB74
		public void Wipe()
		{
			for (int i = 0; i < this.mergedSpriteList.Count; i++)
			{
				this.mergedSpriteList[i].Wipe();
			}
		}

		// Token: 0x04002381 RID: 9089
		private Dictionary<int, SpriteMergerObject> mergedSpriteDict = new Dictionary<int, SpriteMergerObject>();

		// Token: 0x04002382 RID: 9090
		private List<SpriteMergerObject> mergedSpriteList = new List<SpriteMergerObject>();

		// Token: 0x020007DA RID: 2010
		public enum Mode
		{
			// Token: 0x04002384 RID: 9092
			none,
			// Token: 0x04002385 RID: 9093
			disableComponent,
			// Token: 0x04002386 RID: 9094
			disableGameObject,
			// Token: 0x04002387 RID: 9095
			destroyComponent,
			// Token: 0x04002388 RID: 9096
			destroyGameObject
		}
	}
}
