using System;
using UnityEngine;
using Voxels.TowerDefense.Ballistics;

namespace Voxels.TowerDefense
{
	// Token: 0x02000896 RID: 2198
	public class ArcheryTargeter : MonoBehaviour, ITargeter
	{
		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06003983 RID: 14723 RVA: 0x000FBDA3 File Offset: 0x000FA1A3
		private bool wantBoxCast
		{
			get
			{
				return Mathf.Max(new float[]
				{
					this.boxCastHalfExtent.x,
					this.boxCastHalfExtent.y,
					this.boxCastHalfExtent.z
				}) > 0f;
			}
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000FBDE1 File Offset: 0x000FA1E1
		public bool IsTargetable(NavSpot origin, NavSpot target, ref int currErrorId)
		{
			return origin != target && this.InSight(origin.transform.position, target.transform.position);
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000FBE0E File Offset: 0x000FA20E
		string ITargeter.GetErrorTerm(int errorId)
		{
			return "UPGRADES/COMMON/TOOLTIPS/NO_TARGETS";
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000FBE18 File Offset: 0x000FA218
		public bool InSight(Vector3 testPosition, Vector3 targeterPosition)
		{
			TrajectorySample trajectorySample = this.trajectoryCalculator.Sample(testPosition - targeterPosition);
			if (!trajectorySample.valid)
			{
				return false;
			}
			Func<Vector3, Vector3, bool> func = delegate(Vector3 from, Vector3 to)
			{
				float maxDistance = Vector3.Distance(to, from);
				RaycastHit raycastHit;
				bool flag;
				if (this.boxCastHalfExtent == Vector3.zero)
				{
					flag = Physics.Raycast(from, to - from, out raycastHit, maxDistance, this.layers);
				}
				else
				{
					flag = Physics.BoxCast(from, this.boxCastHalfExtent, to - from, out raycastHit, Quaternion.identity, maxDistance, this.layers);
				}
				if (flag)
				{
					Vector3 vector2 = from + (to - from).normalized * raycastHit.distance;
				}
				return flag;
			};
			testPosition.y += this.targetHeightOffset;
			Vector3 vector = trajectorySample.midPoint + targeterPosition;
			return !func(targeterPosition, vector) && !func(vector, testPosition);
		}

		// Token: 0x040027A3 RID: 10147
		[SerializeField]
		private TrajectoryUtility trajectoryCalculator;

		// Token: 0x040027A4 RID: 10148
		[SerializeField]
		private Vector3 boxCastHalfExtent = new Vector3(0.2f, 0f, 0.2f);

		// Token: 0x040027A5 RID: 10149
		[SerializeField]
		private float targetHeightOffset = 0.2f;

		// Token: 0x040027A6 RID: 10150
		[SerializeField]
		private LayerMask layers = default(LayerMask);
	}
}
