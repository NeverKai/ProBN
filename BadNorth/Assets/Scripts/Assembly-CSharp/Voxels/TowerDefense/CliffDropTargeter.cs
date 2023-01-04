using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000898 RID: 2200
	public class CliffDropTargeter : MonoBehaviour, ITargeter
	{
		// Token: 0x06003991 RID: 14737 RVA: 0x000FC190 File Offset: 0x000FA590
		bool ITargeter.IsTargetable(NavSpot origin, NavSpot target, ref int currErrorId)
		{
			if (origin == target)
			{
				return false;
			}
			Vector3 position = origin.transform.position;
			Vector3 position2 = target.transform.position;
			if (position.y < 0.1f)
			{
				currErrorId = 0;
				return false;
			}
			Vector3 inVec = position2 - position;
			if (inVec.GetZeroY().sqrMagnitude > 2.1f)
			{
				return false;
			}
			if (inVec.y > -0.1f)
			{
				currErrorId = 1;
				return false;
			}
			position.y += 0.5f;
			position2.y += 0.5f;
			Vector3 vector = position2;
			vector.y = position.y;
			bool flag = (!this.RayCast(position + Vector3.forward * 0.1f, vector) || !this.RayCast(position + Vector3.right * 0.1f, vector) || !this.RayCast(position + Vector3.back * 0.1f, vector) || !this.RayCast(position + Vector3.left * 0.1f, vector)) && !this.RayCast(vector, position2);
			if (!flag)
			{
				currErrorId = 1;
				return false;
			}
			if (-inVec.y > this.maxDropHeight + 0.05f)
			{
				currErrorId = 2;
				return false;
			}
			return true;
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x000FC30C File Offset: 0x000FA70C
		string ITargeter.GetErrorTerm(int errorId)
		{
			return CliffDropTargeter.errorReasons[(CliffDropTargeter.Error)errorId];
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x000FC31C File Offset: 0x000FA71C
		private bool RayCast(Vector3 from, Vector3 to)
		{
			RaycastHit raycastHit;
			return Physics.Raycast(new Ray(from, to - from), out raycastHit, Vector3.Distance(from, to), this.layers);
		}

		// Token: 0x040027AD RID: 10157
		private const float hRangeSqr = 2.1f;

		// Token: 0x040027AE RID: 10158
		private const float heightAboveTestPos = 0.5f;

		// Token: 0x040027AF RID: 10159
		[SerializeField]
		private LayerMask layers = default(LayerMask);

		// Token: 0x040027B0 RID: 10160
		[NonSerialized]
		public float maxDropHeight = 2f;

		// Token: 0x040027B1 RID: 10161
		private static readonly Dictionary<CliffDropTargeter.Error, string> errorReasons = new Dictionary<CliffDropTargeter.Error, string>
		{
			{
				CliffDropTargeter.Error.TooLow,
				"UPGRADES/COMMON/TOOLTIPS/PLUNGE_TOO_LOW"
			},
			{
				CliffDropTargeter.Error.TooFar,
				"UPGRADES/COMMON/TOOLTIPS/PLUNGE_TOO_FAR"
			},
			{
				CliffDropTargeter.Error.TooHigh,
				"UPGRADES/COMMON/TOOLTIPS/PLUNGE_TOO_HIGH"
			}
		};

		// Token: 0x02000899 RID: 2201
		private enum Error
		{
			// Token: 0x040027B3 RID: 10163
			TooLow,
			// Token: 0x040027B4 RID: 10164
			TooFar,
			// Token: 0x040027B5 RID: 10165
			TooHigh
		}
	}
}
