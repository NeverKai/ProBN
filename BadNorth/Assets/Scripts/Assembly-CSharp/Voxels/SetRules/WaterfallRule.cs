using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.SetRules
{
	// Token: 0x0200064F RID: 1615
	[RequireComponent(typeof(ModuleSet))]
	public class WaterfallRule : SetRule
	{
		// Token: 0x060028FF RID: 10495 RVA: 0x0008CAD4 File Offset: 0x0008AED4
		public override void GetRules(MultiWave multiwave, List<Wrapper> wrappers)
		{
			WaterfallRule.WaterfallCounter waterfallCounter = new WaterfallRule.WaterfallCounter(this, multiwave);
			foreach (Wrapper wrapper in wrappers)
			{
				wrapper.onPlaced.Add(waterfallCounter);
			}
			multiwave.onReset = (Action)Delegate.Combine(multiwave.onReset, new Action(waterfallCounter.OnReset));
		}

		// Token: 0x02000650 RID: 1616
		private class WaterfallCounter : IOnPlaced
		{
			// Token: 0x06002900 RID: 10496 RVA: 0x0008CB5C File Offset: 0x0008AF5C
			public WaterfallCounter(WaterfallRule waterfallRule, MultiWave multiWave)
			{
				this.waterfallRule = waterfallRule;
				this.poleHashes = new HashSet<int>();
			}

			// Token: 0x06002901 RID: 10497 RVA: 0x0008CB76 File Offset: 0x0008AF76
			public void OnReset()
			{
				this.count = 0;
				this.poleHashes.Clear();
			}

			// Token: 0x06002902 RID: 10498 RVA: 0x0008CB8C File Offset: 0x0008AF8C
			bool IOnPlaced.OnPlaced(Domino domino, MultiWave multiWave)
			{
				List<Claim> claims = domino.placement.claims;
				for (int i = 0; i < domino.placement.claims.Count; i++)
				{
					Claim claim = domino.placement.claims[i];
					Vector3Int a = claim.pos + domino.offset;
					for (int j = 0; j < 6; j++)
					{
						Vector3Int b = Constants.directions[j];
						if ((float)Mathf.Abs(b.y) <= 0.5f)
						{
							Vector3Int pos = a + b;
							Slot slot = multiWave.GetSlot(pos);
							for (int k = slot.dominos.Count - 1; k >= 0; k--)
							{
								if (k <= slot.dominos.Count - 1)
								{
									Domino domino2 = slot.dominos[k];
									if (domino2.state == Domino.State.idle)
									{
										if (!(domino2.placement.firstModule != domino.placement.firstModule))
										{
											if (Vector3.Dot(claim.normal, domino2.GetClaim(pos).normal) >= 0.5f)
											{
												if (!multiWave.RemoveDomino(domino2))
												{
													return false;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				int item = domino.offset.x + domino.offset.z * multiWave.size.x;
				if (!this.poleHashes.Contains(item))
				{
					this.poleHashes.Add(item);
					if ((double)this.poleHashes.Count > (double)((float)multiWave.size.x * 0.8f) - (double)multiWave.size.y * 0.3)
					{
						for (int l = 0; l < multiWave.allDominos.Count; l++)
						{
							Domino domino3 = multiWave.allDominos[l];
							if (domino3.state == Domino.State.idle && this.waterfallRule.moduleSet.ContainsModule(domino3.placement.firstModule))
							{
								int item2 = domino3.offset.x + domino3.offset.z * multiWave.size.x;
								if (!this.poleHashes.Contains(item2) && !multiWave.RemoveDomino(domino3))
								{
									return false;
								}
							}
						}
					}
				}
				if (domino.offset.y >= 4)
				{
					for (int m = 0; m < multiWave.allDominos.Count; m++)
					{
						Domino domino4 = multiWave.allDominos[m];
						if (domino4.state == Domino.State.idle)
						{
							if (domino4.offset.y >= 4)
							{
								if (this.waterfallRule.moduleSet.ContainsModule(domino4.placement.firstModule))
								{
									if (!multiWave.RemoveDomino(domino4))
									{
										return false;
									}
								}
							}
						}
					}
				}
				return true;
			}

			// Token: 0x04001AAE RID: 6830
			public int count;

			// Token: 0x04001AAF RID: 6831
			public WaterfallRule waterfallRule;

			// Token: 0x04001AB0 RID: 6832
			private HashSet<int> poleHashes;
		}
	}
}
