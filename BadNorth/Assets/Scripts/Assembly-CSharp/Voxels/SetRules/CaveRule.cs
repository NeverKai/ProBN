using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.SetRules
{
	// Token: 0x02000643 RID: 1603
	[RequireComponent(typeof(ModuleSet))]
	public class CaveRule : SetRule, IOnPlaced
	{
		// Token: 0x060028E3 RID: 10467 RVA: 0x0008BEDC File Offset: 0x0008A2DC
		public override void GetRules(MultiWave multiwave, List<Wrapper> wrappers)
		{
			foreach (Wrapper wrapper in wrappers)
			{
				wrapper.onPlaced.Add(this);
			}
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x0008BF38 File Offset: 0x0008A338
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
						Vector3Int vector3Int = a + b;
						if (!multiWave.bounds.Contains(vector3Int))
						{
							Slot slot = multiWave.GetSlot(vector3Int);
							for (int k = slot.dominos.Count - 1; k >= 0; k--)
							{
								if (k <= slot.dominos.Count - 1)
								{
									Domino domino2 = slot.dominos[k];
									if (domino2.state == Domino.State.idle)
									{
										if (!(domino2.placement.firstModule != domino.placement.firstModule))
										{
											if (Vector3.Dot(claim.normal, domino2.GetClaim(vector3Int).normal) >= 0.5f)
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
			}
			return true;
		}
	}
}
