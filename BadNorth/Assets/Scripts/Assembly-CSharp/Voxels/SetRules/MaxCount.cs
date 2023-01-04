using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.SetRules
{
	// Token: 0x02000646 RID: 1606
	[RequireComponent(typeof(ModuleSet))]
	public class MaxCount : SetRule
	{
		// Token: 0x060028EC RID: 10476 RVA: 0x0008C1C4 File Offset: 0x0008A5C4
		public override void GetRules(MultiWave multiwave, List<Wrapper> wrappers)
		{
			int num = Mathf.RoundToInt((float)this.maxCount + (float)multiwave.size.x * this.widthCoefficient + (float)multiwave.size.y * this.heightCoefficient);
			MaxCount.Mode mode = this.mode;
			if (mode != MaxCount.Mode.Self)
			{
				if (mode == MaxCount.Mode.Set)
				{
					MaxCount.SetCounter setCounter = new MaxCount.SetCounter(this, num);
					multiwave.onReset = (Action)Delegate.Combine(multiwave.onReset, new Action(setCounter.OnReset));
					foreach (Wrapper wrapper in wrappers)
					{
						wrapper.onPlaced.Add(setCounter);
					}
				}
			}
			else
			{
				foreach (Wrapper wrapper2 in wrappers)
				{
					MaxCount.SelfCounter selfCounter = new MaxCount.SelfCounter(wrapper2, num);
					multiwave.onReset = (Action)Delegate.Combine(multiwave.onReset, new Action(selfCounter.OnReset));
					wrapper2.onPlaced.Add(selfCounter);
				}
			}
		}

		// Token: 0x04001A9A RID: 6810
		public MaxCount.Mode mode = MaxCount.Mode.Set;

		// Token: 0x04001A9B RID: 6811
		public int maxCount = 4;

		// Token: 0x04001A9C RID: 6812
		public float widthCoefficient;

		// Token: 0x04001A9D RID: 6813
		public float heightCoefficient;

		// Token: 0x02000647 RID: 1607
		public enum Mode
		{
			// Token: 0x04001A9F RID: 6815
			Self,
			// Token: 0x04001AA0 RID: 6816
			Set
		}

		// Token: 0x02000648 RID: 1608
		private class SetCounter : IOnPlaced
		{
			// Token: 0x060028ED RID: 10477 RVA: 0x0008C328 File Offset: 0x0008A728
			public SetCounter(MaxCount setRule, int maxCount)
			{
				this.setRule = setRule;
				this.maxCount = maxCount;
				this.value = maxCount;
			}

			// Token: 0x060028EE RID: 10478 RVA: 0x0008C345 File Offset: 0x0008A745
			public void OnReset()
			{
				this.value = this.maxCount;
			}

			// Token: 0x060028EF RID: 10479 RVA: 0x0008C354 File Offset: 0x0008A754
			bool IOnPlaced.OnPlaced(Domino domino, MultiWave multiWave)
			{
				return --this.value > 0 || this.setRule.RemoveRemaining(multiWave);
			}

			// Token: 0x04001AA1 RID: 6817
			public MaxCount setRule;

			// Token: 0x04001AA2 RID: 6818
			public int maxCount;

			// Token: 0x04001AA3 RID: 6819
			public int value;
		}

		// Token: 0x02000649 RID: 1609
		private class SelfCounter : IOnPlaced
		{
			// Token: 0x060028F0 RID: 10480 RVA: 0x0008C38C File Offset: 0x0008A78C
			public SelfCounter(Wrapper wrapper, int maxCount)
			{
				this.value = maxCount;
				this.maxCount = maxCount;
				this.module = wrapper.placement.firstModule;
			}

			// Token: 0x060028F1 RID: 10481 RVA: 0x0008C3B3 File Offset: 0x0008A7B3
			public void OnReset()
			{
				this.value = this.maxCount;
			}

			// Token: 0x060028F2 RID: 10482 RVA: 0x0008C3C4 File Offset: 0x0008A7C4
			bool IOnPlaced.OnPlaced(Domino domino, MultiWave multiWave)
			{
				if (--this.value <= 0)
				{
					for (int i = 0; i < multiWave.allDominos.Count; i++)
					{
						Domino domino2 = multiWave.allDominos[i];
						if (domino2.placement.firstModule == this.module && !multiWave.RemoveDomino(domino2))
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x04001AA4 RID: 6820
			public Module module;

			// Token: 0x04001AA5 RID: 6821
			public int value;

			// Token: 0x04001AA6 RID: 6822
			public int maxCount;
		}
	}
}
