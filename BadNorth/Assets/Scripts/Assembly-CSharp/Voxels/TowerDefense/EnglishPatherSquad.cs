using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007A7 RID: 1959
	public class EnglishPatherSquad : SquadCoordinatorAgentTracker<EnglishPatherAgent>
	{
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060032B6 RID: 12982 RVA: 0x000D763F File Offset: 0x000D5A3F
		public IPathTarget target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x060032B7 RID: 12983 RVA: 0x000D7648 File Offset: 0x000D5A48
		// (remove) Token: 0x060032B8 RID: 12984 RVA: 0x000D7680 File Offset: 0x000D5A80
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IPathTarget> onPathTargetChanged = delegate(IPathTarget A_0)
		{
		};

		// Token: 0x060032B9 RID: 12985 RVA: 0x000D76B6 File Offset: 0x000D5AB6
		public override void Setup(Squad squad)
		{
			base.Setup(squad);
			this.pathVisualizer = base.GetComponent<EnglishPatherVisualizer>();
			base.StartCoroutine(this.UpdateWalkSpeeds());
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000D76D8 File Offset: 0x000D5AD8
		public void SetPatherTarget(IPathTarget newTarget, bool showPath)
		{
			this._target = newTarget;
			this.onPathTargetChanged(this._target);
			this.pathVisualizer.OnNewTarget(newTarget, showPath);
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000D76FF File Offset: 0x000D5AFF
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this._target = null;
			this.pathVisualizer = null;
			this.onPathTargetChanged = null;
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000D771C File Offset: 0x000D5B1C
		private void OnDrawGizmosSelected()
		{
			if (this._target == null)
			{
				return;
			}
			NavSpot navSpot = this._target as NavSpot;
			if (!navSpot)
			{
				return;
			}
			Gizmos.matrix = ExtraGizmos.CloserToCameraMatrix();
			navSpot.distanceField.DrawGizmos();
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000D7764 File Offset: 0x000D5B64
		private IEnumerator UpdateWalkSpeeds()
		{
			bool keepGoing = true;
			while (keepGoing)
			{
				if (!base.enSquad.standard)
				{
					yield return null;
				}
				else
				{
					if (base.enSquad.standard.agent.navPos.valid)
					{
						float orderDist = base.enSquad.standard.agent.orderDist;
						for (int i = 0; i < this.agentComponents.Count; i++)
						{
							EnglishPatherAgent englishPatherAgent = this.agentComponents[i];
							if (englishPatherAgent.agent.navPos.island)
							{
								float orderDist2 = englishPatherAgent.agent.orderDist;
								if (orderDist2 > 1f)
								{
									keepGoing = true;
								}
								float num = Mathf.MoveTowards(orderDist2, orderDist, 0.3f);
								englishPatherAgent.walkSpeed = ExtraMath.RemapValue(num - orderDist, -1f, 1f, 0.8f, 1.2f);
							}
						}
					}
					else
					{
						for (int j = 0; j < this.agentComponents.Count; j++)
						{
							EnglishPatherAgent englishPatherAgent2 = this.agentComponents[j];
							if (englishPatherAgent2.agent.navPos.island)
							{
								englishPatherAgent2.walkSpeed = 1f;
							}
						}
						keepGoing = true;
					}
					yield return null;
				}
			}
			for (int k = 0; k < this.agentComponents.Count; k++)
			{
				this.agentComponents[k].walkSpeed = 1f;
			}
			yield return null;
			yield break;
		}

		// Token: 0x0400226F RID: 8815
		private IPathTarget _target;

		// Token: 0x04002270 RID: 8816
		private EnglishPatherVisualizer pathVisualizer;
	}
}
