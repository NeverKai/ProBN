using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200089C RID: 2204
	public class NavigationTargeter : MonoBehaviour, ITargeter
	{
		// Token: 0x0600399B RID: 14747 RVA: 0x000FC428 File Offset: 0x000FA828
		bool ITargeter.IsTargetable(NavSpot origin, NavSpot target, ref int currErrorId)
		{
			return (this.allowSelf && origin == target) || ((origin.vert.pos - target.vert.pos).sqrMagnitude <= this.distance * this.distance && (target.distanceField.SampleDistance(origin.vert) < this.distance || origin.navPos.TriCast(target.navPos)));
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000FC4BC File Offset: 0x000FA8BC
		string ITargeter.GetErrorTerm(int errorId)
		{
			return "UPGRADES/COMMON/TOOLTIPS/NO_TARGETS";
		}

		// Token: 0x040027B7 RID: 10167
		[SerializeField]
		private bool allowSelf;

		// Token: 0x040027B8 RID: 10168
		[SerializeField]
		private float distance = 1.8f;
	}
}
