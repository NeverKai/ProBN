using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.SetRules
{
	// Token: 0x0200064A RID: 1610
	[RequireComponent(typeof(ModuleSet))]
	public class NoMirrors : SetRule, IOnLast
	{
		// Token: 0x060028F4 RID: 10484 RVA: 0x0008C444 File Offset: 0x0008A844
		public override void GetRules(MultiWave multiwave, List<Wrapper> wrappers)
		{
			foreach (Wrapper wrapper in wrappers)
			{
				wrapper.onLast.Add(this);
			}
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x0008C4A0 File Offset: 0x0008A8A0
		bool IOnLast.OnLast(Domino domino, MultiWave multiWave, int side, Claim claim, Slot slot)
		{
			Vector3Int vector3Int = slot.pos + Constants.directions[side];
			if (multiWave.bounds.Contains(vector3Int))
			{
				Slot slot2 = multiWave.GetSlot(vector3Int);
				if (!slot2.done)
				{
					int num = claim.keys[side];
					List<Domino> list = slot2.keyCount[Constants.opposites[side], num];
					Matrix4x4 matrix = domino.placement.modules[0].settings.matrix;
					Matrix4x4 lhs = Matrix4x4.Scale(Vector3.one.SetComponent(Constants.components[side], -1f));
					Matrix4x4 rhs = lhs * matrix;
					for (int i = 0; i < list.Count; i++)
					{
						Domino domino2 = list[i];
						if (domino2 != domino)
						{
							if (!(domino2.placement.modules[0].module != domino.placement.modules[0].module))
							{
								if (!(domino2.placement.modules[0].settings.matrix != rhs))
								{
									if (domino2.state == Domino.State.idle && !multiWave.RemoveDomino(domino2))
									{
										return false;
									}
									break;
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
