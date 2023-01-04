using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels
{
	// Token: 0x0200066C RID: 1644
	public class RuleSetBehaviour : MonoBehaviour
	{
		// Token: 0x060029FF RID: 10751 RVA: 0x00095CC8 File Offset: 0x000940C8
		private void OnDrawGizmos()
		{
			Gizmos.color = this.color * 2f;
			Gizmos.DrawSphere(base.transform.position, 1f);
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x00095CF4 File Offset: 0x000940F4
		private void OnDrawGizmosSelected()
		{
			foreach (RuleSet.RuleSettings ruleSettings in this.ruleSet.ruleSettings)
			{
				List<Bounds> bounds = ruleSettings.moduleSet.bounds;
				Gizmos.color = ((ruleSettings.overrideMode != ModuleSet.Mode.Enabled) ? Color.red : this.color);
				Gizmos.matrix = ruleSettings.moduleSet.transform.localToWorldMatrix;
				foreach (Bounds bounds2 in bounds)
				{
					Vector3 vector = ruleSettings.moduleSet.transform.InverseTransformPoint(base.transform.position);
					Gizmos.color = Gizmos.color.SetComponent(3, 0.3f);
					Gizmos.DrawWireCube(bounds2.center, bounds2.size);
					Gizmos.color = Gizmos.color.SetComponent(3, 1f);
					Bounds bounds3 = new Bounds(bounds2.center.SetY(bounds2.min.y), bounds2.size.GetZeroY());
					Gizmos.DrawLine(bounds3.ClosestPoint(vector), vector);
					Gizmos.DrawWireCube(bounds3.center, bounds3.size);
				}
			}
		}

		// Token: 0x04001B5F RID: 7007
		public Color color = Color.yellow;

		// Token: 0x04001B60 RID: 7008
		public RuleSet ruleSet;
	}
}
