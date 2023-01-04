using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007E0 RID: 2016
	public class SpriteStamperTrunk : MonoBehaviour, IIslandWipe
	{
		// Token: 0x06003437 RID: 13367 RVA: 0x000E221C File Offset: 0x000E061C
		public SpriteStamper GetSpriteStamper(SpriteStampDef stamp)
		{
			int key = stamp.GetKey();
			SpriteStamper spriteStamper;
			if (!this.dict.TryGetValue(key, out spriteStamper))
			{
				spriteStamper = SpriteStamper.NewSpriteStamper(base.transform, this.loose, stamp, key, this.maxCount);
				this.dict.Add(key, spriteStamper);
			}
			return spriteStamper;
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000E226C File Offset: 0x000E066C
		public void Add(SpriteStampDef stamp)
		{
			this.GetSpriteStamper(stamp).Add(stamp);
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000E227C File Offset: 0x000E067C
		IEnumerator<GenInfo> IIslandWipe.OnIslandWipe(Island island)
		{
			foreach (SpriteStamper spriteStamper in this.dict.Values)
			{
				UnityEngine.Object.Destroy(spriteStamper.gameObject);
			}
			this.dict.Clear();
			yield return new GenInfo("Shedding stuck arrows", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000E2298 File Offset: 0x000E0698
		public static SpriteStamperTrunk GetNew(Transform parent, int count, bool loose)
		{
			SpriteStamperTrunk spriteStamperTrunk = parent.gameObject.AddEmptyChild("SpriteStamperTrunk " + count).AddComponent<SpriteStamperTrunk>();
			spriteStamperTrunk.maxCount = count;
			spriteStamperTrunk.loose = loose;
			return spriteStamperTrunk;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000E22D8 File Offset: 0x000E06D8
		public void Clear()
		{
			foreach (SpriteStamper spriteStamper in this.dict.Values)
			{
				spriteStamper.Clear();
			}
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000E2338 File Offset: 0x000E0738
		public void ReturnToPool()
		{
			SpriteStamperPool.instance.ReturnToPool(this);
		}

		// Token: 0x040023A7 RID: 9127
		private Dictionary<int, SpriteStamper> dict = new Dictionary<int, SpriteStamper>();

		// Token: 0x040023A8 RID: 9128
		public int maxCount = 128;

		// Token: 0x040023A9 RID: 9129
		public bool loose = true;
	}
}
