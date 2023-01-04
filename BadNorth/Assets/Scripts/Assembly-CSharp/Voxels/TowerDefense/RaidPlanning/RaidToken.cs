using System;
using System.Linq;
using UnityEngine;

namespace Voxels.TowerDefense.RaidPlanning
{
	// Token: 0x020005B2 RID: 1458
	public abstract class RaidToken : MonoBehaviour
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x00077DE2 File Offset: 0x000761E2
		public RaidPlanningBoard board
		{
			get
			{
				return base.GetComponentInParent<RaidPlanningBoard>();
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060025F9 RID: 9721
		public abstract Bounds localDropBounds { get; }

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060025FA RID: 9722
		public abstract int depth { get; }

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060025FB RID: 9723
		public abstract int bounty { get; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060025FC RID: 9724
		public abstract int agentCount { get; }

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060025FD RID: 9725
		public abstract RaidToken parent { get; }

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060025FE RID: 9726
		public abstract Vector3 localDragPos { get; }

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060025FF RID: 9727 RVA: 0x00077DEA File Offset: 0x000761EA
		public Vector3 worldDragPos
		{
			get
			{
				return base.transform.TransformPoint(this.localDragPos);
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06002600 RID: 9728
		public abstract float dragRadius { get; }

		// Token: 0x06002601 RID: 9729 RVA: 0x00077E00 File Offset: 0x00076200
		public bool Contains(Vector3 pos)
		{
			return this.localDropBounds.Contains(base.transform.InverseTransformPoint(pos));
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x00077E28 File Offset: 0x00076228
		public void DrawDragGizmo()
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.color *= 2f;
			Gizmos.matrix = base.transform.localToWorldMatrix * Matrix4x4.TRS(this.localDragPos, Quaternion.identity, new Vector3(1f, 0.1f, 1f));
			Gizmos.DrawSphere(Vector3.zero, this.dragRadius);
			Gizmos.color /= 2f;
			Gizmos.matrix = matrix;
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x00077EB4 File Offset: 0x000762B4
		public virtual void OnDrawGizmos()
		{
			Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Bounds localDropBounds = this.localDropBounds;
			Gizmos.DrawWireCube(localDropBounds.center, localDropBounds.size);
			Gizmos.color = Color.white;
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x00077F14 File Offset: 0x00076314
		public virtual void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(1f, 1f, 0f, 0.2f);
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Bounds localDropBounds = this.localDropBounds;
			Gizmos.DrawWireCube(localDropBounds.center, localDropBounds.size);
			Gizmos.color = Color.white;
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x00077F74 File Offset: 0x00076374
		public void ArrangeChildren()
		{
			IOrderedEnumerable<RaidToken> orderedEnumerable = from x in base.GetComponentsInChildren<RaidToken>()
			where x.depth == this.depth + 1
			orderby x.transform.localPosition.z
			select x;
			int num = 0;
			foreach (RaidToken raidToken in orderedEnumerable)
			{
				raidToken.transform.SetAsLastSibling();
				raidToken.OnArranged(this, num);
				num++;
			}
			this.OnArrangeChildren();
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x0007801C File Offset: 0x0007641C
		public virtual void OnArrangeChildren()
		{
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x0007801E File Offset: 0x0007641E
		public virtual void OnArranged(RaidToken parent, int index)
		{
		}
	}
}
