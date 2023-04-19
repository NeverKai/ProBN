using System;
using System.Collections.Generic;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense.TriFlow
{
	// Token: 0x0200080B RID: 2059
	public class Updater
	{
		// Token: 0x060035DA RID: 13786 RVA: 0x000E86A1 File Offset: 0x000E6AA1
		private Updater()
		{
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000E86B4 File Offset: 0x000E6AB4
		public Updater(NavigationMesh navMesh, string name, Color color, DebugChannelGroup dbgGroup)
		{
			this._navMesh = navMesh;
			this.color = color;
			this.islandName = navMesh.island.levelNode.levelState.dbgName;
			this.dbgGroup = dbgGroup;
			this.flowField = new FlowField(navMesh);
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000E8714 File Offset: 0x000E6B14
		public void Clear()
		{
			this.flowField.Clear();
			this.pendingAdditions.Clear();
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000E872C File Offset: 0x000E6B2C
		public void SetPendingAdditions(List<Addition> newAdditions, int numAdditions)
		{
			this.pendingAdditions.Clear();
			if (numAdditions > this.pendingAdditions.Count)
			{
				this.pendingAdditions.Capacity = numAdditions;
			}
			for (int i = 0; i < numAdditions; i++)
			{
				this.pendingAdditions.Add(newAdditions[i]);
			}
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000E8785 File Offset: 0x000E6B85
		public void ExtractResults(ref FlowField results)
		{
			results.CopyFrom(this.flowField);
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000E8794 File Offset: 0x000E6B94
		public void Update()
		{
			NavigationMesh target = this._navMesh.Target;
			if (!target)
			{
				return;
			}
			this.Decay(target);
			this.ProcessPendingAdds();
			this.Propagate(target);
			this.ApplyChanges(target);
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000E87D4 File Offset: 0x000E6BD4
		private void ProcessPendingAdds()
		{
			foreach (Addition addition in this.pendingAdditions)
			{
				for (int i = 0; i < 3; i++)
				{
					Vert vert = addition.navPos.tri.verts[i];
					Content content = this.flowField.flowContents[(int)vert.index];
					float component = addition.navPos.bary.GetComponent(i);
					float num = Vector3.Distance(addition.navPos.pos, vert.pos) + addition.data.distance;
					if (num < content.newDistance)
					{
						content.occupied = true;
						content.inVector = addition.navPos.pos - vert.pos;
						content.distance = num;
						content.newDistance = num;
						content.data = addition.data;
						content.newData = addition.data;
						content.direction = content.inVector;
					}
					content.amount = Mathf.Lerp(content.amount, 100000f, component * addition.data.amount);
					this.flowField.flowContents[(int)vert.index] = content;
				}
			}
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000E896C File Offset: 0x000E6D6C
		private void Decay(NavigationMesh navMesh)
		{
			for (int i = 0; i < navMesh.verts.Length; i++)
			{
				this.flowField.flowContents[i].Decay(1000000f, 0.96f);
			}
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000E89B4 File Offset: 0x000E6DB4
		private void Propagate(NavigationMesh navMesh)
		{
			for (int i = 0; i < navMesh.pipes.Length; i++)
			{
				Pipe pipe = navMesh.pipes[i];
				int index = (int)pipe.inVert.index;
				int index2 = (int)pipe.outVert.index;
				try
				{
					Content content = this.flowField.flowContents[index];
					Content content2 = this.flowField.flowContents[index2];
					if (!content.occupied)
					{
						float num = content2.distance + pipe.edge.length;
						if (num < this.flowField.flowContents[index].newDistance)
						{
							content.inVector = pipe.outVert.pos - pipe.inVert.pos;
							content.newDistance = num;
							content.newData = content2.data;
						}
					}
					this.flowField.flowContents[index] = content;
					this.flowField.flowContents[index2] = content2;
				}
				catch (Exception)
				{
				}
			}
			int j = 0;
			int num2 = navMesh.verts.Length;
			while (j < num2)
			{
				Vert vert = navMesh.verts[j];
				Content content3 = this.flowField.flowContents[(int)vert.index];
				content3.direction = Vector3.zero;
				for (int k = 0; k < vert.pipes.Count; k++)
				{
					Pipe pipe2 = vert.pipes[k];
					Content other = this.flowField.flowContents[(int)pipe2.outVert.index];
					if (content3.Comparable(other))
					{
						float num3 = content3.newDistance - other.newDistance;
						num3 = Mathf.Clamp(num3, -pipe2.edge.length, pipe2.edge.length);
						content3.direction += pipe2.dir * (pipe2.weight * num3) / pipe2.edge.length;
					}
				}
				this.flowField.flowContents[(int)vert.index] = content3;
				j++;
			}
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000E8C30 File Offset: 0x000E7030
		private void ApplyChanges(NavigationMesh navMesh)
		{
			for (int i = 0; i < navMesh.verts.Length; i++)
			{
				Content content = this.flowField.flowContents[i];
				content.distance = content.newDistance;
				content.data = content.newData;
				this.flowField.flowContents[i] = content;
			}
		}

		// Token: 0x040024A2 RID: 9378
		private DebugChannelGroup dbgGroup;

		// Token: 0x040024A3 RID: 9379
		private List<Addition> pendingAdditions = new List<Addition>();

		// Token: 0x040024A4 RID: 9380
		private RTM.Utilities.WeakReference<NavigationMesh> _navMesh;

		// Token: 0x040024A5 RID: 9381
		private FlowField flowField;

		// Token: 0x040024A6 RID: 9382
		private Color color;

		// Token: 0x040024A7 RID: 9383
		private string islandName;
	}
}
