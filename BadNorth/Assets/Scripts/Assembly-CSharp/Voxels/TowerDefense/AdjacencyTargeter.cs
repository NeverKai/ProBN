using System;
using System.Linq;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000895 RID: 2197
	public class AdjacencyTargeter : MonoBehaviour, ITargeter
	{
		// Token: 0x06003980 RID: 14720 RVA: 0x000FBD2D File Offset: 0x000FA12D
		bool ITargeter.IsTargetable(NavSpot origin, NavSpot target, ref int currErrorId)
		{
			return (this.allowSelf && origin == target) || origin.neighbours.Contains(target);
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000FBD55 File Offset: 0x000FA155
		string ITargeter.GetErrorTerm(int errorId)
		{
			return "UPGRADES/COMMON/TOOLTIPS/NO_TARGETS";
		}

		// Token: 0x040027A2 RID: 10146
		[SerializeField]
		private bool allowSelf;
	}
}
