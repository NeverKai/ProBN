using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Fabric;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007EE RID: 2030
	public class SquadReplenishLocation : MonoBehaviour, IPathTarget, ISquadSelector, IIslandEnter, IIslandLeave, IIslandWipe
	{
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000E382C File Offset: 0x000E1C2C
		// (set) Token: 0x060034E0 RID: 13536 RVA: 0x000E3834 File Offset: 0x000E1C34
		public bool squadExiting { get; private set; }

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060034E1 RID: 13537 RVA: 0x000E383D File Offset: 0x000E1C3D
		// (set) Token: 0x060034E2 RID: 13538 RVA: 0x000E3845 File Offset: 0x000E1C45
		public bool squadHealing { get; private set; }

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060034E3 RID: 13539 RVA: 0x000E384E File Offset: 0x000E1C4E
		// (set) Token: 0x060034E4 RID: 13540 RVA: 0x000E3856 File Offset: 0x000E1C56
		public EnglishSquad currentSquad { get; private set; }

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060034E5 RID: 13541 RVA: 0x000E385F File Offset: 0x000E1C5F
		public bool occupied
		{
			get
			{
				return this.currentSquad;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060034E6 RID: 13542 RVA: 0x000E386C File Offset: 0x000E1C6C
		public bool available
		{
			get
			{
				return !this.occupied && this.house.intactState.active && !this.house.burning.active;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x000E38A4 File Offset: 0x000E1CA4
		public Transform entranceTransform
		{
			get
			{
				return this._entranceTransform;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060034E8 RID: 13544 RVA: 0x000E38AC File Offset: 0x000E1CAC
		public Transform flagTransform
		{
			get
			{
				return this._flagTransform;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060034E9 RID: 13545 RVA: 0x000E38B4 File Offset: 0x000E1CB4
		public DistanceField distanceField
		{
			get
			{
				return this._distanceField;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060034EA RID: 13546 RVA: 0x000E38BC File Offset: 0x000E1CBC
		public NavPos entranceNavPos
		{
			get
			{
				return this._entranceNavPos;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060034EB RID: 13547 RVA: 0x000E38C4 File Offset: 0x000E1CC4
		public int numContainedAgents
		{
			get
			{
				return this.agents.Count;
			}
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000E38D4 File Offset: 0x000E1CD4
		public bool Contains(Agent agent)
		{
			foreach (ReplenishComponent replenishComponent in this.agents)
			{
				if (replenishComponent.agent == agent)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x060034ED RID: 13549 RVA: 0x000E3944 File Offset: 0x000E1D44
		// (remove) Token: 0x060034EE RID: 13550 RVA: 0x000E397C File Offset: 0x000E1D7C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onAgentEntered = delegate()
		{
		};

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x060034EF RID: 13551 RVA: 0x000E39B4 File Offset: 0x000E1DB4
		// (remove) Token: 0x060034F0 RID: 13552 RVA: 0x000E39EC File Offset: 0x000E1DEC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int> onAgentHealed = delegate(int A_0)
		{
		};

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x060034F1 RID: 13553 RVA: 0x000E3A24 File Offset: 0x000E1E24
		// (remove) Token: 0x060034F2 RID: 13554 RVA: 0x000E3A5C File Offset: 0x000E1E5C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onSquadBeginExit = delegate()
		{
		};

		// Token: 0x060034F3 RID: 13555 RVA: 0x000E3A92 File Offset: 0x000E1E92
		EnglishSquad ISquadSelector.GetSelectableSquad()
		{
			return this.currentSquad;
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060034F4 RID: 13556 RVA: 0x000E3A9A File Offset: 0x000E1E9A
		bool ISquadSelector.wantsHoverEffect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000E3A9D File Offset: 0x000E1E9D
		private void Awake()
		{
			this.house.onTorched += this.House_onTorched;
			this.house.onDestroyed += this.House_onDestroyed;
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000E3ACD File Offset: 0x000E1ECD
		public void TriggerGold()
		{
			this.house.uiAnimator.SetTrigger(SquadReplenishLocation.goldId);
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000E3AE9 File Offset: 0x000E1EE9
		public void SetHover(bool hover)
		{
			this.house.uiAnimator.SetBool(SquadReplenishLocation.hoverId, hover);
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000E3B06 File Offset: 0x000E1F06
		public void SetHighlight(bool highlight)
		{
			this.house.uiAnimator.SetBool(SquadReplenishLocation.highlightId, highlight);
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000E3B24 File Offset: 0x000E1F24
		IEnumerator<GenInfo> IIslandEnter.OnIslandEnter(Island island)
		{
			NavigationMesh navMesh = island.navMesh;
			this._entranceVert = navMesh.GetClosestVert(this.entranceTransform.position);
			this._entranceNavPos = new NavPos(this._entranceVert);
			this._distanceField = new DistanceField(navMesh, (int)this._entranceVert.index, "ReplenishLocation");
			if (this.onPathTargetChanged == null)
			{
				this.onPathTargetChanged = new Action<IPathTarget>(this.OnPathTargetChanged);
			}
			yield return new GenInfo("SquadHealing", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000E3B48 File Offset: 0x000E1F48
		IEnumerator<GenInfo> IIslandWipe.OnIslandWipe(Island island)
		{
			if (this.occupied)
			{
				this.onSquadBeginExit();
			}
			base.StopAllCoroutines();
			this.agents.Clear();
			this.currentSquad = null;
			this.squadHealing = false;
			this.squadExiting = false;
			this.healStartTime = float.MaxValue;
			yield return new GenInfo("replenish", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000E3B64 File Offset: 0x000E1F64
		IEnumerator<GenInfo> IIslandLeave.OnIslandLeave(Island island)
		{
			yield return new GenInfo("replenish", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000E3B78 File Offset: 0x000E1F78
		public void AssignSquad(EnglishSquad squad, float replenishTime, float healTime)
		{
			this.replenishTime = replenishTime;
			this.healTime = healTime;
			this.healStartTime = float.MaxValue;
			this.currentSquad = squad;
			this.currentSquad.pather.onPathTargetChanged += this.onPathTargetChanged;
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000E3BB0 File Offset: 0x000E1FB0
		public void AgentEntered(ReplenishComponent agentComponent)
		{
			if (this.squadExiting || agentComponent.squad != this.currentSquad)
			{
				agentComponent.ExitReplenishLocation(this);
				return;
			}
			FabricWrapper.PostEvent(this.agentEnterAudio, agentComponent.agent.gameObject);
			if (agentComponent.agent == this.currentSquad.heroAgent)
			{
				this.agents.Insert(0, agentComponent);
			}
			else
			{
				this.agents.Add(agentComponent);
			}
			this.onAgentEntered();
			this.healStartTime = Time.time + this.waitForBlockedUnitsTime;
			this.CheckStartHealing();
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000E3C59 File Offset: 0x000E2059
		public void AgentDied(Agent agent)
		{
			this.CheckStartHealing();
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000E3C61 File Offset: 0x000E2061
		private void Update()
		{
			this.CheckStartHealing();
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000E3C6C File Offset: 0x000E206C
		private void CheckStartHealing()
		{
			if (!this.currentSquad || this.squadHealing || this.squadExiting)
			{
				return;
			}
			if (this.agents.Count == 0)
			{
				return;
			}
			if (this.agents.Count == this.currentSquad.livingAgents.Count || Time.time > this.healStartTime)
			{
				base.StartCoroutine(this.HealRoutine());
			}
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000E3CF0 File Offset: 0x000E20F0
		public void CallAgents(EnglishSquad squad, int numAgents)
		{
			this.AssignSquad(squad, 100f, 100f);
			for (int i = 0; i < numAgents; i++)
			{
				this.AddAgent();
			}
			this.StartExitHouse(this.perAgentExitTime);
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000E3D34 File Offset: 0x000E2134
		private void StartExitHouse(float initialWaitTime)
		{
			base.StopAllCoroutines();
			this.squadExiting = true;
			this.squadHealing = false;
			this.currentSquad.pather.onPathTargetChanged -= this.onPathTargetChanged;
			if (!this.currentSquad.navSpotOccupant.navSpot)
			{
				this.currentSquad.navSpotOccupant.SetNavSpot(this.GetBestNavSpot(), true);
			}
			base.StartCoroutine(this.ExitRoutine(initialWaitTime));
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000E3DAB File Offset: 0x000E21AB
		public void SquadDead()
		{
			base.StopAllCoroutines();
			this.squadExiting = false;
			this.squadHealing = false;
			this.currentSquad.pather.onPathTargetChanged -= this.onPathTargetChanged;
			this.currentSquad = null;
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000E3DDE File Offset: 0x000E21DE
		private void OnPathTargetChanged(IPathTarget target)
		{
			this.StartExitHouse(this.perAgentExitTime);
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000E3DEC File Offset: 0x000E21EC
		private void House_onTorched()
		{
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000E3DF0 File Offset: 0x000E21F0
		private void House_onDestroyed()
		{
			if (!this.currentSquad)
			{
				return;
			}
			this.onSquadBeginExit();
			for (int i = this.maxEscapingAgents; i < this.agents.Count; i++)
			{
				this.agents[i].agent.KillImmediate();
			}
			if (this.agents.Count > this.maxEscapingAgents)
			{
				this.agents.RemoveRange(this.maxEscapingAgents, this.agents.Count - this.maxEscapingAgents);
			}
			if (!this.squadExiting)
			{
				this.StartExitHouse(0f);
			}
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000E3E9F File Offset: 0x000E229F
		private void OnLevelWon()
		{
			if (this.currentSquad && !this.squadExiting)
			{
				this.StartExitHouse(0f);
			}
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000E3EC8 File Offset: 0x000E22C8
		private IEnumerator HealRoutine()
		{
			this.squadHealing = true;
			FabricWrapper.PostEvent(this.healLoopAudio, base.gameObject);
			for (int i = 0; i < this.currentSquad.livingAgents.Count; i++)
			{
				float t = Time.time + this.healTime;
				while (t > Time.time)
				{
					yield return null;
				}
				this.currentSquad.livingAgents[i].FillHealth();
				this.onAgentHealed(i + 1);
				FabricWrapper.PostEvent(this.healTickAudio, base.gameObject);
			}
			while (this.currentSquad.maxCount > this.currentSquad.livingAgents.Count)
			{
				float t2 = Time.time + this.replenishTime;
				while (t2 > Time.time)
				{
					yield return null;
				}
				this.AddAgent();
				this.onAgentHealed(this.agents.Count);
				FabricWrapper.PostEvent(this.healTickAudio, base.gameObject);
			}
			FabricWrapper.PostEvent(this.healCompleteAudio, base.gameObject);
			this.StartExitHouse(this.perAgentExitTime);
			yield break;
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000E3EE4 File Offset: 0x000E22E4
		private void AddAgent()
		{
			Agent agent = this.currentSquad.CreateAgent(this.currentSquad.minionPrefab, this.entranceNavPos);
			agent.Spawn();
			ReplenishComponent orAddComponent = agent.GetOrAddComponent<ReplenishComponent>();
			orAddComponent.indoors.SetActive(true);
			this.agents.Add(orAddComponent);
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000E3F34 File Offset: 0x000E2334
		private IEnumerator ExitRoutine(float initialWait)
		{
			FabricWrapper.PostEvent(this.healLoopAudio, EventAction.StopSound, base.gameObject);
			this.squadExiting = true;
			float t = Time.time + initialWait;
			while (t > Time.time)
			{
				yield return null;
			}
			this.onSquadBeginExit();
			while (this.agents.Count > 0)
			{
				this.Exit(0);
				float t2 = Time.time + this.perAgentExitTime;
				while (t2 > Time.time)
				{
					yield return null;
				}
			}
			this.currentSquad = null;
			this.squadExiting = false;
			yield break;
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000E3F58 File Offset: 0x000E2358
		private void Exit(int agentIdx)
		{
			try
			{
				ReplenishComponent replenishComponent = this.agents[agentIdx];
				this.agents[agentIdx].ExitReplenishLocation(this);
				FabricWrapper.PostEvent(this.agentExitAudio, replenishComponent.agent.gameObject);
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
				if (this.agents[agentIdx] == null)
				{
					this.AddAgent();
				}
			}
			this.agents.RemoveAt(agentIdx);
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000E3FE8 File Offset: 0x000E23E8
		public NavSpot GetBestNavSpot()
		{
			return NavSpot.GetNavSpot(this.entranceTransform.position, false);
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x000E3FFC File Offset: 0x000E23FC
		private void OnDestroy()
		{
			this._entranceTransform = null;
			this._flagTransform = null;
			this.house = null;
			this._entranceVert = null;
			this._entranceNavPos = default(NavPos);
			this._distanceField = null;
			this.agents = null;
			this.currentSquad = null;
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000E404C File Offset: 0x000E244C
		private void OnDrawGizmos()
		{
			Color color = Gizmos.color;
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(this.entranceTransform.position, 0.1f);
			Gizmos.DrawRay(this.entranceTransform.position + Vector3.up * 0.15f, this.entranceTransform.forward * 0.3f);
			Gizmos.DrawRay(this.flagTransform.position, this.flagTransform.up * 0.3f);
			Gizmos.DrawSphere(this.flagTransform.position + this.flagTransform.up * 0.3f, 0.025f);
			if (Application.isPlaying)
			{
			}
			Gizmos.color = color;
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000E411B File Offset: 0x000E251B
		private void OnDrawGizmosSelected()
		{
			if (this.distanceField)
			{
				this.distanceField.DrawGizmos();
			}
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x000E4138 File Offset: 0x000E2538
		float IPathTarget.GetDistanceFrom(NavPos navPos)
		{
			return this.distanceField.SampleDistance(navPos);
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x000E4146 File Offset: 0x000E2546
		void IPathTarget.SampleDistanceDir(NavPos navPos, ref Vector3 dir, ref float dist)
		{
			this.distanceField.Sample(navPos, ref dir, ref dist);
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x000E4156 File Offset: 0x000E2556
		NavPos IPathTarget.navPos
		{
			get
			{
				return this._entranceNavPos;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000E415E File Offset: 0x000E255E
		Bounds IPathTarget.endPointBounds
		{
			get
			{
				return new Bounds(this._entranceNavPos.wPos, new Vector3(0.1f, 0.5f, 0.1f));
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06003514 RID: 13588 RVA: 0x000E4184 File Offset: 0x000E2584
		Vector3 IPathTarget.endPointPosition
		{
			get
			{
				return this._entranceNavPos.wPos;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x000E4191 File Offset: 0x000E2591
		Mesh IPathTarget.endPointMesh
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x000E4194 File Offset: 0x000E2594
		public Vector3 GetWSCursorPos()
		{
			return base.transform.position + Vector3.up * 0.5f;
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x000E41B5 File Offset: 0x000E25B5
		public void LevelEnded()
		{
			if (!this.squadExiting && this.currentSquad && this.numContainedAgents > 0)
			{
				this.StartExitHouse(1f);
			}
		}

		// Token: 0x040023F8 RID: 9208
		[SerializeField]
		private Transform _entranceTransform;

		// Token: 0x040023F9 RID: 9209
		[SerializeField]
		private Transform _flagTransform;

		// Token: 0x040023FA RID: 9210
		[SerializeField]
		private House house;

		// Token: 0x040023FB RID: 9211
		[Header("Delays")]
		[SerializeField]
		private float perAgentExitTime = 0.15f;

		// Token: 0x040023FC RID: 9212
		private float waitForBlockedUnitsTime = 2f;

		// Token: 0x040023FD RID: 9213
		private int maxEscapingAgents = 3;

		// Token: 0x040023FE RID: 9214
		private float healStartTime = float.MaxValue;

		// Token: 0x040023FF RID: 9215
		[Header("audio")]
		[SerializeField]
		private string agentEnterAudio = "Sfx/House/Entered";

		// Token: 0x04002400 RID: 9216
		[SerializeField]
		private string agentExitAudio = "Sfx/House/Left";

		// Token: 0x04002401 RID: 9217
		[SerializeField]
		private string healLoopAudio = "Sfx/House/Left";

		// Token: 0x04002402 RID: 9218
		[SerializeField]
		private string healCompleteAudio = "Sfx/House/HealComplete";

		// Token: 0x04002403 RID: 9219
		[SerializeField]
		private string healTickAudio = "Sfx/House/HealTick";

		// Token: 0x04002404 RID: 9220
		private Vert _entranceVert;

		// Token: 0x04002405 RID: 9221
		private NavPos _entranceNavPos;

		// Token: 0x04002406 RID: 9222
		private DistanceField _distanceField;

		// Token: 0x04002407 RID: 9223
		private List<ReplenishComponent> agents = new List<ReplenishComponent>(16);

		// Token: 0x0400240A RID: 9226
		private static AnimId highlightId = "Highlight";

		// Token: 0x0400240B RID: 9227
		private static AnimId hoverId = "Hover";

		// Token: 0x0400240C RID: 9228
		private static AnimId goldId = "Gold";

		// Token: 0x04002411 RID: 9233
		private float replenishTime;

		// Token: 0x04002412 RID: 9234
		private float healTime;

		// Token: 0x04002413 RID: 9235
		private Action<IPathTarget> onPathTargetChanged;
	}
}
