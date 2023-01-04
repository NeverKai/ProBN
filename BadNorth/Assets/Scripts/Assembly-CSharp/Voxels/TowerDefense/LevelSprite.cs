using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200079A RID: 1946
	public class LevelSprite : MonoBehaviour, ILevelComponent
	{
		// Token: 0x06003226 RID: 12838 RVA: 0x000D49D1 File Offset: 0x000D2DD1
		public void OnSetLevel(Agent agent, int level)
		{
			base.GetComponent<SpriteRenderer>().sprite = this.sprites[Mathf.Max(0, level - 1)];
		}

		// Token: 0x04002212 RID: 8722
		public Sprite[] sprites;
	}
}
