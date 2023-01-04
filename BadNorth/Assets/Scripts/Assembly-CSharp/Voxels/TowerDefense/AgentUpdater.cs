using System;
using System.Collections.Generic;
using System.Threading;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000523 RID: 1315
	public class AgentUpdater : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x06002211 RID: 8721 RVA: 0x00061EBB File Offset: 0x000602BB
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			base.enabled = false;
			this.levelPauser = manager.levelPauser;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x00061ED0 File Offset: 0x000602D0
		private void Update()
		{
			if (!this.levelPauser.isPaused)
			{
				this.WaitForThread();
				this.Apply(this.set);
				float dt = Mathf.Min(Time.deltaTime, 0.1f);
				float forceDt = Mathf.Pow(Time.deltaTime * 60f, 0.25f) / 60f;
				Agent.UpdateAllAgents(dt, forceDt);
				this.Prepare(this.set);
				this.TriggerThread();
			}
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x00061F44 File Offset: 0x00060344
		private void Prepare(AgentUpdater.Set set)
		{
			using (new ScopedProfiler("Prepare", null))
			{
				set.Clear();
				foreach (Squad squad in Squad.allLiving)
				{
					AgentUpdater.PhysicsSquad item = default(AgentUpdater.PhysicsSquad);
					item.faction = squad.faction;
					item.isEnglish = (squad.faction.side == Faction.Side.English);
					item.bounds = squad.bounds;
					item.minIdx = (ulong)((long)set.agents.Count);
					item.count = 0UL;
					for (int i = 0; i < squad.livingAgents.Count; i++)
					{
						Agent agent = squad.livingAgents[i];
						NavPos navPos = agent.navPos;
						if (agent.aliveAndGrounded.active && !navPos.isNull && !navPos.isNaN && !navPos.isInfinity)
						{
							set.agents.Add(new AgentUpdater.PhysicsAgent(agent));
							item.count += 1UL;
						}
					}
					set.squads.Add(item);
				}
				int j = 0;
				int count = set.squads.Count;
				while (j < count)
				{
					j++;
				}
				set.squads.Sort();
			}
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00062104 File Offset: 0x00060504
		private void Apply(AgentUpdater.Set set)
		{
			using (new ScopedProfiler("Apply", null))
			{
				foreach (AgentUpdater.PhysicsAgent physicsAgent in set.agents)
				{
					if (physicsAgent.agent)
					{
						physicsAgent.agent.force = physicsAgent.force;
					}
				}
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000621AC File Offset: 0x000605AC
		private static void Physics(AgentUpdater.Set set)
		{
			for (int i = 0; i < set.squads.Count; i++)
			{
				AgentUpdater.PhysicsSquad physicsSquad = set.squads[i];
				Bounds bounds = physicsSquad.bounds;
				for (int j = i + 1; j < set.squads.Count; j++)
				{
					AgentUpdater.PhysicsSquad physicsSquad2 = set.squads[j];
					Bounds bounds2 = physicsSquad2.bounds;
					if (bounds.Intersects(bounds2))
					{
						bool flag = physicsSquad.faction != physicsSquad2.faction;
						for (ulong num = 0UL; num < physicsSquad.count; num += 1UL)
						{
							int index = (int)(physicsSquad.minIdx + num);
							AgentUpdater.PhysicsAgent value = set.agents[index];
							bool flag2 = false;
							if (bounds2.Contains(value.wPos))
							{
								for (ulong num2 = 0UL; num2 < physicsSquad2.count; num2 += 1UL)
								{
									int index2 = (int)(physicsSquad2.minIdx + num2);
									AgentUpdater.PhysicsAgent value2 = set.agents[index2];
									if (AgentUpdater.PushApart(ref value, ref value2, flag, !flag))
									{
										set.agents[index2] = value2;
										flag2 = true;
									}
								}
								if (flag2)
								{
									set.agents[index] = value;
								}
							}
						}
					}
				}
				for (ulong num3 = 0UL; num3 < physicsSquad.count; num3 += 1UL)
				{
					int index3 = (int)(physicsSquad.minIdx + num3);
					AgentUpdater.PhysicsAgent value3 = set.agents[index3];
					bool flag3 = false;
					for (ulong num4 = num3 + 1UL; num4 < physicsSquad.count; num4 += 1UL)
					{
						int index4 = (int)(physicsSquad.minIdx + num4);
						AgentUpdater.PhysicsAgent value4 = set.agents[index4];
						if (AgentUpdater.PushApart(ref value3, ref value4, false, physicsSquad.isEnglish && num3 == 0UL))
						{
							set.agents[index4] = value4;
							flag3 = true;
						}
					}
					if (flag3)
					{
						set.agents[index3] = value3;
					}
				}
			}
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000623DC File Offset: 0x000607DC
		private static bool PushApart(ref AgentUpdater.PhysicsAgent agent0, ref AgentUpdater.PhysicsAgent agent1, bool enemies, bool walkPast)
		{
			if (agent0.navPos.navigationMesh != agent1.navPos.navigationMesh)
			{
				return false;
			}
			Vector3 a = agent1.lPos - agent0.lPos;
			float sqrMagnitude = a.sqrMagnitude;
			float num = agent0.radius + agent1.radius;
			if (sqrMagnitude >= num * num)
			{
				return false;
			}
			float num2 = agent0.mass;
			float num3 = agent1.mass;
			if (!enemies)
			{
				num2 /= agent0.movability;
				num3 /= agent1.movability;
			}
			else
			{
				num2 /= agent0.enemyMovability;
				num3 /= agent1.enemyMovability;
			}
			float num4 = num2 / (num2 + num3);
			if (float.IsNaN(num4))
			{
				return false;
			}
			if (sqrMagnitude == 0f)
			{
				Vector3 a2 = Vector3.right * num;
				agent0.force -= a2 * (1f - num4);
				agent1.force += a2 * num4;
			}
			else
			{
				float num5 = Mathf.Sqrt(sqrMagnitude);
				float num6 = num5 / num;
				a /= num5;
				float num7 = 1f - num6;
				num7 = 3f * num7 * num7 - 2f * num7 * num7 * num7;
				num7 *= 12f;
				Vector3 a3 = a * num7;
				Vector3 vector = -a3 * (1f - num4);
				Vector3 vector2 = a3 * num4;
				if (walkPast && Vector3.Dot(agent1.walkDir, agent0.walkDir) < 0f)
				{
					float num8 = agent0.navPos.GetBorderDistance() + agent1.navPos.GetBorderDistance();
					if (num8 < num)
					{
						float num9 = ExtraMath.RemapValue(num8, num, 0f);
						num9 *= num9;
						Vector3 normalized = agent0.walkDir.normalized;
						vector -= normalized * Vector3.Dot(normalized, vector) * num9;
						Vector3 normalized2 = agent1.walkDir.normalized;
						vector2 -= normalized2 * Vector3.Dot(normalized2, vector2) * num9;
					}
				}
				agent0.force += vector;
				agent1.force += vector2;
			}
			return true;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x00062638 File Offset: 0x00060A38
		public bool threadActive
		{
			get
			{
				return !this.threadActiveHandle.WaitOne(0);
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00062649 File Offset: 0x00060A49
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.BeginThread();
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00062651 File Offset: 0x00060A51
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.EndThread();
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0006265C File Offset: 0x00060A5C
		private void BeginThread()
		{
			using (new ScopedProfiler("BeginThread", null))
			{
				this.threadContinue = true;
				this.threadActiveHandle.Reset();
				this.workerThreadWait.Reset();
				this.mainThreadWait.Set();
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.SyncedThreadedUpdate));
				base.enabled = true;
			}
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000626E0 File Offset: 0x00060AE0
		private void EndThread()
		{
			using (new ScopedProfiler("EndThread", null))
			{
				this.threadContinue = false;
				this.mainThreadWait.Reset();
				WaitHandle.SignalAndWait(this.workerThreadWait, this.mainThreadWait);
				base.enabled = false;
				this.set.Clear();
			}
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00062754 File Offset: 0x00060B54
		private void WaitForThread()
		{
			using (new ScopedProfiler("WaitOnThread", null))
			{
				this.mainThreadWait.WaitOne();
			}
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000627A0 File Offset: 0x00060BA0
		private void TriggerThread()
		{
			using (new ScopedProfiler("TriggerThread", null))
			{
				this.mainThreadWait.Reset();
				this.workerThreadWait.Set();
			}
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000627F8 File Offset: 0x00060BF8
		private void SyncedThreadedUpdate(object obj)
		{
			this.workerThreadWait.WaitOne();
			while (this.threadContinue)
			{
				this.workerThreadWait.Reset();
				try
				{
					AgentUpdater.Physics(this.set);
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
				WaitHandle.SignalAndWait(this.mainThreadWait, this.workerThreadWait);
			}
			this.mainThreadWait.Set();
			this.threadActiveHandle.Set();
		}

		// Token: 0x040014BD RID: 5309
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("AgentUpdater", EVerbosity.Quiet, 0);

		// Token: 0x040014BE RID: 5310
		private AgentUpdater.Set set = new AgentUpdater.Set(24, 96);

		// Token: 0x040014BF RID: 5311
		private LevelPauser levelPauser;

		// Token: 0x040014C0 RID: 5312
		private EventWaitHandle workerThreadWait = new EventWaitHandle(false, EventResetMode.ManualReset);

		// Token: 0x040014C1 RID: 5313
		private EventWaitHandle mainThreadWait = new EventWaitHandle(false, EventResetMode.ManualReset);

		// Token: 0x040014C2 RID: 5314
		private EventWaitHandle threadActiveHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

		// Token: 0x040014C3 RID: 5315
		private bool threadContinue;

		// Token: 0x02000524 RID: 1316
		private struct PhysicsAgent
		{
			// Token: 0x0600221F RID: 8735 RVA: 0x00062884 File Offset: 0x00060C84
			public PhysicsAgent(Agent agent)
			{
				this.agent = agent;
				this.navPos = agent.navPos;
				this.lPos = this.navPos.pos;
				this.wPos = this.navPos.transform.TransformPoint(this.lPos);
				this.force = Vector3.zero;
				this.mass = agent.mass;
				this.movability = agent.movability;
				this.enemyMovability = agent.enemyMovability;
				this.radius = agent.radius;
				this.walkDir = agent.walkDir;
			}

			// Token: 0x040014C4 RID: 5316
			public Agent agent;

			// Token: 0x040014C5 RID: 5317
			public NavPos navPos;

			// Token: 0x040014C6 RID: 5318
			public Vector3 force;

			// Token: 0x040014C7 RID: 5319
			public Vector3 wPos;

			// Token: 0x040014C8 RID: 5320
			public Vector3 lPos;

			// Token: 0x040014C9 RID: 5321
			public Vector3 walkDir;

			// Token: 0x040014CA RID: 5322
			public float mass;

			// Token: 0x040014CB RID: 5323
			public float radius;

			// Token: 0x040014CC RID: 5324
			public float movability;

			// Token: 0x040014CD RID: 5325
			public float enemyMovability;
		}

		// Token: 0x02000525 RID: 1317
		private struct PhysicsSquad : IComparable<AgentUpdater.PhysicsSquad>
		{
			// Token: 0x06002220 RID: 8736 RVA: 0x00062918 File Offset: 0x00060D18
			int IComparable<AgentUpdater.PhysicsSquad>.CompareTo(AgentUpdater.PhysicsSquad other)
			{
				return other.count.CompareTo(this.count);
			}

			// Token: 0x040014CE RID: 5326
			public Faction faction;

			// Token: 0x040014CF RID: 5327
			public Bounds bounds;

			// Token: 0x040014D0 RID: 5328
			public bool isEnglish;

			// Token: 0x040014D1 RID: 5329
			public ulong minIdx;

			// Token: 0x040014D2 RID: 5330
			public ulong count;
		}

		// Token: 0x02000526 RID: 1318
		private class Set
		{
			// Token: 0x06002221 RID: 8737 RVA: 0x0006292C File Offset: 0x00060D2C
			public Set(int maxSquads = 24, int maxAgents = 96)
			{
				this.squads = new List<AgentUpdater.PhysicsSquad>(maxSquads);
				this.agents = new List<AgentUpdater.PhysicsAgent>(maxAgents);
			}

			// Token: 0x06002222 RID: 8738 RVA: 0x00062962 File Offset: 0x00060D62
			public void Clear()
			{
				this.squads.Clear();
				this.agents.Clear();
			}

			// Token: 0x040014D3 RID: 5331
			public List<AgentUpdater.PhysicsSquad> squads = new List<AgentUpdater.PhysicsSquad>();

			// Token: 0x040014D4 RID: 5332
			public List<AgentUpdater.PhysicsAgent> agents = new List<AgentUpdater.PhysicsAgent>();
		}
	}
}
