using System;
using System.Collections.Generic;
using System.Threading;
using ReflexCLI.Attributes;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense.TriFlow
{
	// Token: 0x02000804 RID: 2052
	public class TriFlowBehaviour : IslandComponent, IIslandFirstEnter, IIslandAwake, IIslandPlay, IIslandWipe
	{
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060035B1 RID: 13745 RVA: 0x000E6813 File Offset: 0x000E4C13
		private static bool useAgentColors
		{
			get
			{
				return TriFlowBehaviour.colorMode == TriFlowBehaviour.ColorMode.AgentColors;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x000E681D File Offset: 0x000E4C1D
		public bool threadActive
		{
			get
			{
				return !this.threadActiveHandle.WaitOne(0);
			}
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000E6830 File Offset: 0x000E4C30
		private void SyncedThreadedUpdate(object obj)
		{
			try
			{
				Updater updater = this.updater;
				this.workerThreadWait.WaitOne();
				while (this.threadContinue)
				{
					this.workerThreadWait.Reset();
					updater.Update();
					WaitHandle.SignalAndWait(this.mainThreadWait, this.workerThreadWait);
				}
				this.mainThreadWait.Set();
				this.threadActiveHandle.Set();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000E68C0 File Offset: 0x000E4CC0
		private void BeginThread()
		{
			using (new ScopedProfiler("Triflow - BeginThread", null))
			{
				this.threadContinue = true;
				this.threadActiveHandle.Reset();
				this.workerThreadWait.Reset();
				this.mainThreadWait.Set();
				using (new ScopedProfiler("Triflow get thread from pool", null))
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.SyncedThreadedUpdate));
				}
			}
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000E6968 File Offset: 0x000E4D68
		private void EndThread()
		{
			using (new ScopedProfiler("Triflow - EndThread", null))
			{
				this.threadContinue = false;
				this.mainThreadWait.Reset();
				WaitHandle.SignalAndWait(this.workerThreadWait, this.mainThreadWait);
			}
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x000E69CC File Offset: 0x000E4DCC
		private void ClearAdditions()
		{
			this.pendingAdditions.Clear();
			this.numPendingAdditions = 0;
			if (this.updater != null)
			{
				this.updater.SetPendingAdditions(this.pendingAdditions, 0);
			}
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000E6A00 File Offset: 0x000E4E00
		IEnumerator<GenInfo> IIslandPlay.OnIslandPlay(Island island)
		{
			this.BeginThread();
			base.enabled = true;
			yield return new GenInfo("Start Triflow", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000E6A1C File Offset: 0x000E4E1C
		IEnumerator<GenInfo> IIslandWipe.OnIslandWipe(Island island)
		{
			this.wantUpdate = false;
			this.ClearAdditions();
			this.updater.Clear();
			this.flowField.Clear();
			if (this.threadActive)
			{
				this.EndThread();
			}
			IslandGenerator.RemoveBlocker(this, this);
			base.enabled = false;
			yield return new GenInfo("End Triflow", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000E6A38 File Offset: 0x000E4E38
		private void OnDestroy()
		{
			this.ClearAdditions();
			if (this.flowField != null)
			{
				this.flowField.Clear();
			}
			if (this.updater != null)
			{
				this.updater.Clear();
			}
			if (this.threadActive)
			{
				this.EndThread();
			}
			this.flowField = null;
			this.updater = null;
			this.faction = null;
			this.navMesh = null;
			this.flowField = null;
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000E6AAC File Offset: 0x000E4EAC
		private void LateUpdate()
		{
			bool flag = this.wantUpdate;
			this.wantUpdate = (this.numPendingAdditions > 0 && this.faction.enemy.agents.Count > 0);
			if (base.island.state != Island.State.Playing)
			{
				return;
			}
			using (new ScopedStopwatch("Triflow - wait thread", null, 0.1f))
			{
				using (new ScopedProfiler("Triflow - wait thread", null))
				{
					this.mainThreadWait.WaitOne();
				}
			}
			if (this.wantUpdate)
			{
				if (!flag)
				{
					IslandGenerator.AddBlocker(this, this);
				}
				using (new ScopedProfiler("Triflow - Copy Results", null))
				{
					this.updater.ExtractResults(ref this.flowField);
				}
				using (new ScopedProfiler("Triflow - Set Pending", null))
				{
					this.updater.SetPendingAdditions(this.pendingAdditions, this.numPendingAdditions);
				}
				using (new ScopedProfiler("Triflow - trigger thread", null))
				{
					this.mainThreadWait.Reset();
					this.workerThreadWait.Set();
				}
			}
			else if (flag)
			{
				this.flowField.Clear();
				this.updater.Clear();
				IslandGenerator.RemoveBlocker(this, this);
			}
			this.numPendingAdditions = 0;
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000E6C78 File Offset: 0x000E5078
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			this.navMesh = island.navMesh;
			this.updater = new Updater(this.navMesh, this.faction.name, this.color, this.dbgGroup);
			yield return new GenInfo("TriFlowBehavior", GenInfo.Mode.interruptable);
			this.flowField = new FlowField(this.navMesh);
			yield return new GenInfo("TriFlowBehavior", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000E6C9A File Offset: 0x000E509A
		void IIslandAwake.OnIslandAwake(Island island)
		{
			base.enabled = false;
			this.faction = this.GetDisabledComponentInParent<Faction>();
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000E6CB0 File Offset: 0x000E50B0
		public void AddPending(Data data)
		{
			using (new ScopedProfiler("Triflow - Add Pending", null))
			{
				Addition addition = new Addition(data);
				if (this.numPendingAdditions < this.pendingAdditions.Count)
				{
					this.pendingAdditions[this.numPendingAdditions] = addition;
				}
				else
				{
					this.pendingAdditions.Add(addition);
				}
				this.numPendingAdditions++;
			}
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000E6D3C File Offset: 0x000E513C
		public void OnDrawGizmos()
		{
			if (this.flowField == null || this.flowField.flowContents == null)
			{
				return;
			}
			Content[] flowContents = this.flowField.flowContents;
			Gizmos.matrix = ExtraGizmos.CloserToCameraMatrix();
			switch (TriFlowBehaviour.drawMode)
			{
			case TriFlowBehaviour.DebugMode.Distance:
			{
				List<Vert> list = new List<Vert>();
				float num = 0.5f;
				for (int i = 0; i < this.navMesh.tris.Length; i++)
				{
					Tri tri = this.navMesh.tris[i];
					list.Clear();
					list.Add(tri.verts.x);
					list.Add(tri.verts.y);
					list.Add(tri.verts.z);
					list.Sort((Vert a, Vert b) => flowContents[(int)a.index].distance.CompareTo(flowContents[(int)b.index].distance));
					float num2 = flowContents[(int)list[0].index].distance / num;
					float num3 = flowContents[(int)list[1].index].distance / num;
					float num4 = flowContents[(int)list[2].index].distance / num;
					if (num2 != num4)
					{
						if (num4 - num2 <= 4f)
						{
							for (int j = Mathf.CeilToInt(num2); j <= Mathf.FloorToInt(num4); j++)
							{
								Vector3 vector = Vector3.Lerp(list[0].pos, list[2].pos, ExtraMath.RemapValue((float)j, num2, num4));
								Vector3 vector2;
								if ((float)j < num3)
								{
									vector2 = Vector3.Lerp(list[0].pos, list[1].pos, ExtraMath.RemapValue((float)j, num2, num3));
								}
								else
								{
									vector2 = Vector3.Lerp(list[1].pos, list[2].pos, ExtraMath.RemapValue((float)j, num3, num4));
								}
								Data data = this.flowField.SampleData(new NavPos(tri, (vector + vector2) / 2f));
								Gizmos.color = ((!data.agent || !TriFlowBehaviour.useAgentColors) ? this.color : data.agent.uniqueDebugColor).SetA(ExtraMath.RemapValue((float)j, 10f, 0f));
								Gizmos.DrawLine(vector, vector2);
							}
						}
					}
				}
				break;
			}
			case TriFlowBehaviour.DebugMode.Direction:
				for (int k = 0; k < this.navMesh.verts.Length; k++)
				{
					Vert vert = this.navMesh.verts[k];
					Content content = flowContents[k];
					if (TriFlowBehaviour.useAgentColors)
					{
						Gizmos.color = ((!content.data.agent) ? this.color : content.data.agent.uniqueDebugColor);
					}
					Gizmos.DrawRay(vert.pos, content.direction * 0.2f);
				}
				for (int l = 0; l < this.navMesh.tris.Length; l++)
				{
					Tri tri2 = this.navMesh.tris[l];
					NavPos navPos = tri2.navPos;
					Data data2 = this.flowField.SampleData(navPos);
					if (TriFlowBehaviour.useAgentColors)
					{
						Gizmos.color = ((!data2.agent) ? this.color : data2.agent.uniqueDebugColor);
					}
					Gizmos.DrawRay(tri2.pos, this.flowField.SampleDirection(navPos) * 0.2f);
				}
				break;
			case TriFlowBehaviour.DebugMode.Tree:
				for (int m = 0; m < this.navMesh.verts.Length; m++)
				{
					Vert vert2 = this.navMesh.verts[m];
					Content content2 = flowContents[m];
					if (TriFlowBehaviour.useAgentColors)
					{
						Gizmos.color = ((!content2.data.agent) ? this.color : content2.data.agent.uniqueDebugColor);
					}
					Gizmos.DrawRay(vert2.pos, content2.inVector);
				}
				break;
			case TriFlowBehaviour.DebugMode.Amount:
				for (int n = 0; n < this.navMesh.verts.Length; n++)
				{
					Vert vert3 = this.navMesh.verts[n];
					Content content3 = flowContents[n];
					if (TriFlowBehaviour.useAgentColors)
					{
						Gizmos.color = ((!content3.data.agent) ? this.color : content3.data.agent.uniqueDebugColor);
					}
					Gizmos.DrawWireCube(vert3.pos, content3.amount / 100000f * new Vector3(0.3f, 0f, 0.3f));
				}
				break;
			}
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000E72A4 File Offset: 0x000E56A4
		public void GLDraw()
		{
			if (!TriFlowBehaviour.debugDraw)
			{
				return;
			}
			GL.Begin(1);
			Content[] flowContents = this.flowField.flowContents;
			switch (TriFlowBehaviour.drawMode)
			{
			case TriFlowBehaviour.DebugMode.Distance:
			{
				List<Vert> list = new List<Vert>();
				float num = 0.5f;
				for (int i = 0; i < this.navMesh.tris.Length; i++)
				{
					Tri tri = this.navMesh.tris[i];
					list.Clear();
					list.Add(tri.verts.x);
					list.Add(tri.verts.y);
					list.Add(tri.verts.z);
					list.Sort((Vert a, Vert b) => flowContents[(int)a.index].distance.CompareTo(flowContents[(int)b.index].distance));
					float num2 = flowContents[(int)list[0].index].distance / num;
					float num3 = flowContents[(int)list[1].index].distance / num;
					float num4 = flowContents[(int)list[2].index].distance / num;
					if (num2 != num4)
					{
						if (num4 - num2 <= 4f)
						{
							for (int j = Mathf.CeilToInt(num2); j <= Mathf.FloorToInt(num4); j++)
							{
								Vector3 vector = Vector3.Lerp(list[0].pos, list[2].pos, ExtraMath.RemapValue((float)j, num2, num4));
								Vector3 vector2;
								if ((float)j < num3)
								{
									vector2 = Vector3.Lerp(list[0].pos, list[1].pos, ExtraMath.RemapValue((float)j, num2, num3));
								}
								else
								{
									vector2 = Vector3.Lerp(list[1].pos, list[2].pos, ExtraMath.RemapValue((float)j, num3, num4));
								}
								Data data = this.flowField.SampleData(new NavPos(tri, (vector + vector2) / 2f));
								Color color = (!data.agent || !TriFlowBehaviour.useAgentColors) ? this.color : data.agent.uniqueDebugColor;
								GL.Color(color.SetA(ExtraMath.RemapValue((float)j, 100f, 0f)));
								GL.Vertex(vector);
								GL.Vertex(vector2);
							}
						}
					}
				}
				break;
			}
			case TriFlowBehaviour.DebugMode.Direction:
				for (int k = 0; k < this.navMesh.verts.Length; k++)
				{
					Vert vert = this.navMesh.verts[k];
					Content content = flowContents[k];
					if (TriFlowBehaviour.useAgentColors)
					{
						GL.Color((!content.data.agent) ? this.color : content.data.agent.uniqueDebugColor);
					}
					GL.Vertex(vert.pos);
					GL.Vertex(vert.pos + content.direction * 0.2f);
				}
				for (int l = 0; l < this.navMesh.tris.Length; l++)
				{
					Tri tri2 = this.navMesh.tris[l];
					NavPos navPos = tri2.navPos;
					Data data2 = this.flowField.SampleData(navPos);
					if (TriFlowBehaviour.useAgentColors)
					{
						GL.Color((!data2.agent) ? this.color : data2.agent.uniqueDebugColor);
					}
					GL.Vertex(tri2.pos);
					GL.Vertex(tri2.pos + this.flowField.SampleDirection(navPos) * 0.2f);
				}
				break;
			case TriFlowBehaviour.DebugMode.Tree:
				for (int m = 0; m < this.navMesh.verts.Length; m++)
				{
					Vert vert2 = this.navMesh.verts[m];
					Content content2 = flowContents[m];
					if (TriFlowBehaviour.useAgentColors)
					{
						GL.Color((!content2.data.agent) ? this.color : content2.data.agent.uniqueDebugColor);
					}
					GL.Vertex(vert2.pos);
					GL.Vertex(vert2.pos + content2.inVector);
				}
				break;
			}
			GL.End();
		}

		// Token: 0x04002477 RID: 9335
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("TriFlow", EVerbosity.Quiet, 100);

		// Token: 0x04002478 RID: 9336
		[NonSerialized]
		public FlowField flowField;

		// Token: 0x04002479 RID: 9337
		private Updater updater;

		// Token: 0x0400247A RID: 9338
		private Faction faction;

		// Token: 0x0400247B RID: 9339
		private bool wantUpdate;

		// Token: 0x0400247C RID: 9340
		private NavigationMesh navMesh;

		// Token: 0x0400247D RID: 9341
		private List<Addition> pendingAdditions = new List<Addition>();

		// Token: 0x0400247E RID: 9342
		private int numPendingAdditions;

		// Token: 0x0400247F RID: 9343
		public Color color = Color.white;

		// Token: 0x04002480 RID: 9344
		[ConsoleCommand("")]
		private static TriFlowBehaviour.DebugMode drawMode;

		// Token: 0x04002481 RID: 9345
		[ConsoleCommand("")]
		private static TriFlowBehaviour.ColorMode colorMode;

		// Token: 0x04002482 RID: 9346
		[ConsoleCommand("")]
		private static bool debugDraw;

		// Token: 0x04002483 RID: 9347
		private EventWaitHandle workerThreadWait = new EventWaitHandle(false, EventResetMode.ManualReset);

		// Token: 0x04002484 RID: 9348
		private EventWaitHandle mainThreadWait = new EventWaitHandle(false, EventResetMode.ManualReset);

		// Token: 0x04002485 RID: 9349
		private EventWaitHandle threadActiveHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

		// Token: 0x04002486 RID: 9350
		private bool threadContinue;

		// Token: 0x02000805 RID: 2053
		private enum DebugMode
		{
			// Token: 0x04002488 RID: 9352
			Distance,
			// Token: 0x04002489 RID: 9353
			Direction,
			// Token: 0x0400248A RID: 9354
			Tree,
			// Token: 0x0400248B RID: 9355
			Amount
		}

		// Token: 0x02000806 RID: 2054
		private enum ColorMode
		{
			// Token: 0x0400248D RID: 9357
			AgentColors,
			// Token: 0x0400248E RID: 9358
			FactionColors
		}
	}
}
