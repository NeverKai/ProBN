using System;
using System.Collections.Generic;
using System.Diagnostics;
using CS.Platform;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007E2 RID: 2018
	public class Squad : MonoBehaviour
	{
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06003440 RID: 13376 RVA: 0x000C8ABD File Offset: 0x000C6EBD
		public AgentState rootState
		{
			get
			{
				return this.stateRoot.rootState;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06003441 RID: 13377 RVA: 0x000C8ACA File Offset: 0x000C6ECA
		public virtual bool alive
		{
			get
			{
				return this.livingAgents.Count > 0;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06003442 RID: 13378 RVA: 0x000C8ADA File Offset: 0x000C6EDA
		public bool dead
		{
			get
			{
				return !this.alive;
			}
		}

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x06003443 RID: 13379 RVA: 0x000C8AE8 File Offset: 0x000C6EE8
		// (remove) Token: 0x06003444 RID: 13380 RVA: 0x000C8B20 File Offset: 0x000C6F20
		
		public event Action onAllDead = delegate()
		{
		};

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x06003445 RID: 13381 RVA: 0x000C8B58 File Offset: 0x000C6F58
		// (remove) Token: 0x06003446 RID: 13382 RVA: 0x000C8B90 File Offset: 0x000C6F90
		
		public event Action onDestroy = delegate()
		{
		};

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x06003447 RID: 13383 RVA: 0x000C8BC8 File Offset: 0x000C6FC8
		// (remove) Token: 0x06003448 RID: 13384 RVA: 0x000C8C00 File Offset: 0x000C7000
		
		public event Action onAllDestroyed = delegate()
		{
		};

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x06003449 RID: 13385 RVA: 0x000C8C38 File Offset: 0x000C7038
		// (remove) Token: 0x0600344A RID: 13386 RVA: 0x000C8C70 File Offset: 0x000C7070
		
		public event Action onSquadChanged = delegate()
		{
		};

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x0600344B RID: 13387 RVA: 0x000C8CA8 File Offset: 0x000C70A8
		// (remove) Token: 0x0600344C RID: 13388 RVA: 0x000C8CE0 File Offset: 0x000C70E0
		
		public event Action<Agent> onAgentCreated = delegate(Agent A_0)
		{
		};

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x0600344D RID: 13389 RVA: 0x000C8D18 File Offset: 0x000C7118
		// (remove) Token: 0x0600344E RID: 13390 RVA: 0x000C8D50 File Offset: 0x000C7150
		
		public event Action<Agent> onAgentSpawned = delegate(Agent A_0)
		{
		};

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x0600344F RID: 13391 RVA: 0x000C8D88 File Offset: 0x000C7188
		// (remove) Token: 0x06003450 RID: 13392 RVA: 0x000C8DC0 File Offset: 0x000C71C0
		
		public event Action<Agent> onAgentRemoved = delegate(Agent A_0)
		{
		};

		// Token: 0x06003451 RID: 13393 RVA: 0x000C8DF8 File Offset: 0x000C71F8
		private NavPos GetRandomNavPos()
		{
			NavigationMesh instance = NavigationMesh.instance;
			int num = UnityEngine.Random.Range(0, 6);
			num = instance.tris.Length - num - 1;
			return new NavPos(instance.tris[num], instance.tris[num].pos);
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000C8E3C File Offset: 0x000C723C
		public virtual void ReportDead(Agent agent)
		{
			if (this.livingAgents.Remove(agent))
			{
				if (this.faction.side == Faction.Side.English)
				{
					Profile.userSave.stats.englishKilled++;
					Profile.campaign.stats.englishKilled++;
				}
				else
				{
					VikingAgent.Type type = agent.GetComponent<VikingAgent>().type;
					Profile.userSave.stats.KilledViking(type);
					Profile.campaign.stats.KilledViking(type);
					BasePlatformManager.Instance.IncrementStatistic("STAT_KILLS", 1f);
				}
				if (this.livingAgents.Count == 0)
				{
					this.onAllDead();
					Squad.allLiving.Remove(this);
					this.faction.livingSquads.Remove(this);
				}
			}
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000C8F18 File Offset: 0x000C7318
		public virtual void ReportDestroyed(Agent agent)
		{
			this.livingAgents.Remove(agent);
			if (this.agents.Remove(agent))
			{
				if (this.agents.Count == 0)
				{
					this.onAllDestroyed();
					base.gameObject.SetActive(false);
				}
				this.onAgentRemoved(agent);
				this.BroadcastChange();
			}
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000C8F7C File Offset: 0x000C737C
		public virtual void LevelEnded(EndOfLevel.Reason reason)
		{
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000C8F7E File Offset: 0x000C737E
		protected void BroadcastChange()
		{
			this.onSquadChanged();
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000C8F8C File Offset: 0x000C738C
		public Agent CreateAgent(Agent prefab, NavPos navPos)
		{
			Agent agent2;
			using ("CreateAgent")
			{
				Agent agent = UnityEngine.Object.Instantiate<GameObject>(prefab.gameObject, base.transform).GetComponent<Agent>();
				agent.faction = this.faction;
				agent.squad = this;
				if (!this.agents.Contains(agent))
				{
					this.agents.Add(agent);
				}
				agent.Setup(navPos);
				this.onAgentCreated(agent);
				AgentState spawned = agent.spawned;
				spawned.OnChange = (Action<bool>)Delegate.Combine(spawned.OnChange, new Action<bool>(delegate(bool x)
				{
					if (x)
					{
						this.livingAgents.Add(agent);
						this.onAgentSpawned(agent);
					}
					else
					{
						this.livingAgents.Remove(agent);
					}
					this.rootState.SetActive(this.alive);
				}));
				this.BroadcastChange();
				agent2 = agent;
			}
			return agent2;
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000C908C File Offset: 0x000C748C
		public Squad SpawnGetFromPrefab(NavPos navPos, Faction faction)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(base.gameObject, faction.island.runContainer);
			Squad component = gameObject.GetComponent<Squad>();
			component.navPos = navPos;
			component.faction = faction;
			component.Setup();
			return component;
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000C90CC File Offset: 0x000C74CC
		public Squad SpawnGetFromPrefab(Faction faction)
		{
			return this.SpawnGetFromPrefab(this.GetRandomNavPos(), faction);
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000C90DB File Offset: 0x000C74DB
		public void SpawnFromPrefab(Faction faction)
		{
			this.SpawnGetFromPrefab(this.GetRandomNavPos(), faction);
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000C90EC File Offset: 0x000C74EC
		public NavPos GetAverageNavPos()
		{
			if (this.agents.Count == 0)
			{
				return this.navPos;
			}
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < this.agents.Count; i++)
			{
				vector += this.agents[i].wPos;
			}
			vector /= (float)this.agents.Count;
			Agent agent = null;
			float num = float.PositiveInfinity;
			for (int j = 0; j < this.agents.Count; j++)
			{
				Agent agent2 = this.agents[j];
				if (agent2.navPos.valid)
				{
					float num2 = Vector3.SqrMagnitude(agent2.wPos - vector);
					if (num2 < num)
					{
						num = num2;
						agent = this.agents[j];
					}
				}
			}
			NavPos result = (!agent || !agent.navPos.valid) ? new NavPos(this.faction.island.navMesh, vector, true, 1f) : agent.navPos;
			result.pos = vector;
			return result;
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x000C9228 File Offset: 0x000C7628
		public virtual void Update()
		{
			this.bounds.center = this.agents[0].wPos;
			this.bounds.extents = Vector3.zero;
			for (int i = 0; i < this.agents.Count; i++)
			{
				Agent agent = this.agents[i];
				this.bounds.Encapsulate(agent.wPos);
			}
			this.bounds.extents = this.bounds.extents + Vector3.one * 0.1f;
			this.stateRoot.Update();
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000C92CC File Offset: 0x000C76CC
		public virtual void SpawnAgent(Agent agentPrefab, int count)
		{
			for (int i = 0; i < count; i++)
			{
				this.CreateAgent(agentPrefab, new NavPos(this.navPos.tri));
			}
			this.BroadcastChange();
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x000C930C File Offset: 0x000C770C
		public virtual void Setup()
		{
			this.passedClicker = base.GetComponent<IPassedClick>();
			foreach (SquadCoordinator squadCoordinator in base.GetComponentsInChildren<SquadCoordinator>())
			{
				this.specialities.Add(squadCoordinator.GetType(), squadCoordinator);
				squadCoordinator.Setup(this);
			}
			foreach (ISquadSetup squadSetup in base.GetComponentsInChildren<ISquadSetup>())
			{
				squadSetup.SquadSetup(this);
			}
			Squad.all.Add(this);
			this.faction.allSquads.Add(this);
			this.BroadcastChange();
			AgentState rootState = this.rootState;
			rootState.OnChange = (Action<bool>)Delegate.Combine(rootState.OnChange, new Action<bool>(delegate(bool x)
			{
				base.gameObject.SetActive(x);
				if (x)
				{
					this.faction.livingSquads.Add(this);
					Squad.allLiving.Add(this);
				}
				else
				{
					this.faction.livingSquads.Remove(this);
					Squad.allLiving.Remove(this);
				}
			}));
			this.rootState.SetActive(false);
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x000C93E4 File Offset: 0x000C77E4
		public virtual string GetAgentName(Agent agentPrefab)
		{
			return agentPrefab.name + "_" + this.iterator++.ToString();
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x000C9420 File Offset: 0x000C7820
		public Vector3 GetRelativePositionInBounds(Agent agent)
		{
			Vector3 result = agent.wPos - this.bounds.center;
			result.x = ((this.bounds.extents.x <= 0f) ? UnityEngine.Random.Range(-1f, 1f) : (result.x / this.bounds.extents.x));
			result.y = ((this.bounds.extents.y <= 0f) ? UnityEngine.Random.Range(-1f, 1f) : (result.y / this.bounds.extents.y));
			result.z = ((this.bounds.extents.z <= 0f) ? UnityEngine.Random.Range(-1f, 1f) : (result.z / this.bounds.extents.z));
			return result;
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x000C9544 File Offset: 0x000C7944
		private void OnDrawGizmos()
		{
			if (!this.faction)
			{
				return;
			}
			Color color = this.faction.color;
			color.a = 1f;
			Gizmos.color = color;
			Gizmos.DrawWireCube(this.bounds.center, this.bounds.size);
			Gizmos.DrawLine(this.bounds.center.SetY(this.bounds.max.y), this.bounds.center.SetY(this.bounds.max.y + 2f));
			Gizmos.color *= 2f;
			Gizmos.DrawSphere(this.bounds.center.SetY(this.bounds.max.y + 2f), 0.1f);
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x000C9633 File Offset: 0x000C7A33
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(this.bounds.center, this.bounds.size);
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x000C965C File Offset: 0x000C7A5C
		protected virtual void OnDestroy()
		{
			this.onDestroy();
			Squad.allLiving.Remove(this);
			Squad.all.Remove(this);
			this.faction.livingSquads.Remove(this);
			this.faction.allSquads.Remove(this);
			this.faction = null;
			this.agents.Clear();
			this.livingAgents.Clear();
			this.agents = null;
			this.livingAgents = null;
			this.squadSetups = null;
			this.passedClicker = null;
			this.specialities.Clear();
			this.onAllDead = null;
			this.onDestroy = null;
			this.onAllDestroyed = null;
			this.onSquadChanged = null;
			this.onAgentCreated = null;
			this.onAgentSpawned = null;
			this.onAgentRemoved = null;
			this.rootState.OnDestroy();
			this.stateRoot = null;
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x000C9738 File Offset: 0x000C7B38
		public TSpeciality GetSquadCoordinator<TSpeciality>() where TSpeciality : SquadCoordinator
		{
			Type typeFromHandle = typeof(TSpeciality);
			SquadCoordinator squadCoordinator;
			if (this.specialities.TryGetValue(typeFromHandle, out squadCoordinator))
			{
				return (TSpeciality)((object)squadCoordinator);
			}
			GameObject gameObject = base.gameObject.AddEmptyChild(typeFromHandle.Name);
			TSpeciality tspeciality = gameObject.AddComponent<TSpeciality>();
			this.specialities.Add(typeFromHandle, tspeciality);
			tspeciality.Setup(this);
			return tspeciality;
		}

		// Token: 0x040023AC RID: 9132
		[Header("Editor")]
		public Faction faction;

		// Token: 0x040023AD RID: 9133
		public int level;

		// Token: 0x040023AE RID: 9134
		[Header("Game")]
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x040023AF RID: 9135
		public List<Agent> agents = new List<Agent>();

		// Token: 0x040023B0 RID: 9136
		public List<Agent> livingAgents = new List<Agent>();

		// Token: 0x040023B1 RID: 9137
		public ISquadSetup[] squadSetups;

		// Token: 0x040023B2 RID: 9138
		public NavPos navPos;

		// Token: 0x040023B3 RID: 9139
		public IPassedClick passedClicker;

		// Token: 0x040023B4 RID: 9140
		public Bounds bounds;

		// Token: 0x040023B5 RID: 9141
		public Vector3 averageDirection;

		// Token: 0x040023B6 RID: 9142
		private Dictionary<Type, SquadCoordinator> specialities = new Dictionary<Type, SquadCoordinator>();

		// Token: 0x040023BE RID: 9150
		public static List<Squad> allLiving = new List<Squad>();

		// Token: 0x040023BF RID: 9151
		public static List<Squad> all = new List<Squad>();

		// Token: 0x040023C0 RID: 9152
		private int iterator;
	}
}
