using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200089B RID: 2203
	public class LineChargeTargeter : MonoBehaviour, ITargeter
	{
		// Token: 0x06003998 RID: 14744 RVA: 0x000FC3A4 File Offset: 0x000FA7A4
		public bool IsTargetable(NavSpot origin, NavSpot target, ref int currErrorId)
		{
			NavPos navPos = origin.navPos;
			NavPos navPos2 = target.navPos;
			return origin != target && (navPos.wPos - navPos2.wPos).SetY(0f).sqrMagnitude < this.range * this.range && navPos.TriCast(navPos2);
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000FC40D File Offset: 0x000FA80D
		string ITargeter.GetErrorTerm(int errorId)
		{
			return "UPGRADES/COMMON/TOOLTIPS/NO_TARGETS";
		}

		// Token: 0x040027B6 RID: 10166
		public float range = 4.5f;
	}
}
