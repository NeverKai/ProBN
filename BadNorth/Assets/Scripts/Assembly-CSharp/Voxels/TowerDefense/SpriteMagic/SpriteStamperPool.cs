using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007DE RID: 2014
	public class SpriteStamperPool : MonoBehaviour, IslandGameplayManager.IAwake
	{
		// Token: 0x0600342E RID: 13358 RVA: 0x000E1FAA File Offset: 0x000E03AA
		private int GetKey(int count, bool loose)
		{
			return (!loose) ? (-count) : count;
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000E1FBC File Offset: 0x000E03BC
		public SpriteStamperTrunk GetSpriteStamperTrunk(int count, bool loose)
		{
			int key = this.GetKey(count, loose);
			Stack<SpriteStamperTrunk> stack;
			if (!this.pools.TryGetValue(key, out stack))
			{
				stack = new Stack<SpriteStamperTrunk>();
				this.pools.Add(key, stack);
			}
			if (stack.Count > 0)
			{
				return stack.Pop();
			}
			return SpriteStamperTrunk.GetNew(base.transform, count, loose);
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000E201C File Offset: 0x000E041C
		public void ReturnToPool(SpriteStamperTrunk trunk)
		{
			int key = this.GetKey(trunk.maxCount, trunk.loose);
			Stack<SpriteStamperTrunk> stack;
			if (!this.pools.TryGetValue(key, out stack))
			{
				stack = new Stack<SpriteStamperTrunk>();
				this.pools.Add(key, stack);
			}
			trunk.transform.SetParent(base.transform, false);
			trunk.Clear();
			stack.Push(trunk);
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000E2081 File Offset: 0x000E0481
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			SpriteStamperPool.instance = this;
			base.gameObject.SetActive(false);
		}

		// Token: 0x040023A1 RID: 9121
		public static SpriteStamperPool instance;

		// Token: 0x040023A2 RID: 9122
		private Dictionary<int, Stack<SpriteStamperTrunk>> pools = new Dictionary<int, Stack<SpriteStamperTrunk>>(4);
	}
}
