using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.SetRules
{
	// Token: 0x0200064B RID: 1611
	[RequireComponent(typeof(ModuleSet))]
	public class NotNextToSelf : SetRule, IOnPlaced
	{
		// Token: 0x060028F7 RID: 10487 RVA: 0x0008C628 File Offset: 0x0008AA28
		public override void GetRules(MultiWave multiwave, List<Wrapper> wrappers)
		{
			foreach (Wrapper wrapper in wrappers)
			{
				wrapper.onPlaced.Add(this);
			}
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x0008C684 File Offset: 0x0008AA84
		bool IOnPlaced.OnPlaced(Domino domino, MultiWave multiWave)
		{
			List<Claim> claims = domino.placement.claims;
			Bounds bounds = domino.GetBounds();
			Bounds bounds2 = bounds;
			bounds2.extents += Vector3.one * (float)this.outerWidth;
			bounds2.center = bounds2.center.SetY((float)domino.offset.y);
			bounds2.extents = bounds2.extents.SetY(0f);
			Bounds bounds3 = bounds;
			bounds3.extents += Vector3.one * (float)this.innerWidth;
			bounds3.center = bounds3.center.SetY((float)domino.offset.y);
			bounds3.extents = bounds3.extents.SetY(0f);
			int num = (int)bounds2.min.x;
			while ((float)num <= bounds2.max.x)
			{
				int num2 = (int)bounds2.min.y;
				while ((float)num2 <= bounds2.max.y)
				{
					int num3 = (int)bounds2.min.z;
					while ((float)num3 <= bounds2.max.z)
					{
						Vector3Int vector3Int = new Vector3Int(num, num2, num3);
						if (!bounds3.Contains(vector3Int))
						{
							if (multiWave.bounds.Contains(vector3Int))
							{
								Slot slot = multiWave.GetSlot(vector3Int);
								if (!slot.done)
								{
									List<Domino> dominos = slot.dominos;
									for (int i = dominos.Count - 1; i >= 0; i--)
									{
										if (i <= dominos.Count - 1)
										{
											Domino domino2 = dominos[i];
											if (domino2 != domino)
											{
												if (domino2.state == Domino.State.idle)
												{
													if (this.mode == NotNextToSelf.Mode.Self)
													{
														if (domino2.placement.modules[0].module != domino.placement.modules[0].module)
														{
															goto IL_256;
														}
													}
													else if (this.mode == NotNextToSelf.Mode.Set && !base.moduleSet.ContainsModule(domino2.placement.firstModule))
													{
														goto IL_256;
													}
													if (!multiWave.RemoveDomino(domino2))
													{
														return false;
													}
												}
											}
										}
										IL_256:;
									}
								}
							}
						}
						num3++;
					}
					num2++;
				}
				num++;
			}
			return true;
		}

		// Token: 0x04001AA7 RID: 6823
		public NotNextToSelf.Mode mode;

		// Token: 0x04001AA8 RID: 6824
		public int innerWidth = 2;

		// Token: 0x04001AA9 RID: 6825
		public int outerWidth = 3;

		// Token: 0x0200064C RID: 1612
		public enum Mode
		{
			// Token: 0x04001AAB RID: 6827
			Self,
			// Token: 0x04001AAC RID: 6828
			Set
		}
	}
}
