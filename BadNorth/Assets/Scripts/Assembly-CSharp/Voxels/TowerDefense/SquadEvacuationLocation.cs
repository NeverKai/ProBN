using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007EB RID: 2027
	public class SquadEvacuationLocation : MonoBehaviour, IPathTarget, ISquadSelector
	{
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060034A9 RID: 13481 RVA: 0x000E2D0F File Offset: 0x000E110F
		public float area
		{
			get
			{
				return this._area;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x000E2D17 File Offset: 0x000E1117
		public float radius
		{
			get
			{
				return this.ship.radius;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060034AB RID: 13483 RVA: 0x000E2D24 File Offset: 0x000E1124
		public NavPos entryNavPos
		{
			get
			{
				return this._entryNavPos;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060034AC RID: 13484 RVA: 0x000E2D2C File Offset: 0x000E112C
		public NavPos localNavPos
		{
			get
			{
				return this._localNavPos;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060034AD RID: 13485 RVA: 0x000E2D34 File Offset: 0x000E1134
		public DistanceField distanceField
		{
			get
			{
				return this._distanceField;
			}
		}

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x060034AE RID: 13486 RVA: 0x000E2D3C File Offset: 0x000E113C
		// (remove) Token: 0x060034AF RID: 13487 RVA: 0x000E2D74 File Offset: 0x000E1174
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onEvacuationDepart = delegate()
		{
		};

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x060034B0 RID: 13488 RVA: 0x000E2DAC File Offset: 0x000E11AC
		// (remove) Token: 0x060034B1 RID: 13489 RVA: 0x000E2DE4 File Offset: 0x000E11E4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onEvacuationConfirm = delegate()
		{
		};

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x060034B2 RID: 13490 RVA: 0x000E2E1C File Offset: 0x000E121C
		// (remove) Token: 0x060034B3 RID: 13491 RVA: 0x000E2E54 File Offset: 0x000E1254
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onEvacuationCompleted = delegate()
		{
		};

		// Token: 0x060034B4 RID: 13492 RVA: 0x000E2E8A File Offset: 0x000E128A
		public bool IsAvailable(float requiredArea = 0f)
		{
			return this.ship.landed && !this.ship.hasAnyVikings && this.area > requiredArea && !this.squad;
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000E2EC9 File Offset: 0x000E12C9
		public bool HasDeparted()
		{
			return this.departed;
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000E2ED4 File Offset: 0x000E12D4
		public void Setup()
		{
			using ("evac.Setup()")
			{
				this._area = this.ship.area;
				this._entryNavPos = this.ship.landing.navPos;
				Vector3 worldPos = this.ship.transform.position + this.ship.transform.forward * this.ship.length * 0.5f;
				this._localNavPos = new NavPos(this.ship.navigationMesh, worldPos, true, 1f);
				this._distanceField = new DistanceField(this._entryNavPos.navigationMesh, (int)this._entryNavPos.GetClosestVert().index, "EvacuationLocation");
				this.SetHighlight(false);
				this.ship.onShipExit += this.Ship_onShipExit;
				this.ship.shipLeavingUpdate += this.Ship_shipLeavingUpdate;
			}
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000E2FF4 File Offset: 0x000E13F4
		private void OnDestroy()
		{
			this.ship = null;
			this._distanceField = null;
			this.squad = null;
			this.agentsOnBoard = null;
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000E3012 File Offset: 0x000E1412
		public void AssignSquad(EnglishSquad newSquad)
		{
			this.squad = newSquad;
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000E301B File Offset: 0x000E141B
		public void SetHover(bool enabled)
		{
			this.uiAnimator.SetBool(SquadEvacuationLocation.hoverId, enabled);
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000E3033 File Offset: 0x000E1433
		public void SetHighlight(bool enabled)
		{
			if (base.gameObject.activeInHierarchy)
			{
				this.uiAnimator.SetBool(SquadEvacuationLocation.highlightId, enabled);
			}
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000E305B File Offset: 0x000E145B
		private void OnEnable()
		{
			this.SetHighlight(false);
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000E3064 File Offset: 0x000E1464
		private void OnDrawGizmos()
		{
			if (Application.isPlaying)
			{
				Gizmos.color = Color.white;
				Gizmos.DrawRay(this._entryNavPos.wPos, Vector3.up);
				ExtraGizmos.DrawCircle(this._entryNavPos.wPos, this.radius, 16);
				Gizmos.DrawRay(this._localNavPos.wPos, Vector3.up);
			}
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x000E30C7 File Offset: 0x000E14C7
		public void AddAgent(Agent agent)
		{
			this.agentsOnBoard.Add(agent);
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000E30D5 File Offset: 0x000E14D5
		public void RemoveAgent(Agent agent)
		{
			if (this.agentsOnBoard != null)
			{
				this.agentsOnBoard.Remove(agent);
			}
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000E30EF File Offset: 0x000E14EF
		public bool Contains(Agent agent)
		{
			return this.agentsOnBoard.Contains(agent);
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060034C0 RID: 13504 RVA: 0x000E30FD File Offset: 0x000E14FD
		public int agentCount
		{
			get
			{
				return this.agentsOnBoard.Count;
			}
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000E310A File Offset: 0x000E150A
		public void Launch()
		{
			this.ship.Launch();
			this.departed = true;
			this.onEvacuationDepart();
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000E3129 File Offset: 0x000E1529
		private void Ship_shipLeavingUpdate(float ratio)
		{
			if (ratio > 0.5f)
			{
				this.onEvacuationConfirm();
			}
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000E3144 File Offset: 0x000E1544
		private void Ship_onShipExit()
		{
			this.onEvacuationCompleted();
			foreach (Agent agent in this.agentsOnBoard)
			{
				if (agent)
				{
					agent.navigationState.SetActive(false);
				}
			}
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000E31BC File Offset: 0x000E15BC
		float IPathTarget.GetDistanceFrom(NavPos navPos)
		{
			if (navPos.navigationMesh && !navPos.island)
			{
				return 0f;
			}
			float num = this.distanceField.SampleDistance(navPos);
			if (num < 1.5f && navPos.TriCast(this.entryNavPos))
			{
				return Vector3.Distance(this.entryNavPos.wPos, navPos.wPos);
			}
			return num;
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000E3238 File Offset: 0x000E1638
		void IPathTarget.SampleDistanceDir(NavPos navPos, ref Vector3 dir, ref float dist)
		{
			if (navPos.navigationMesh && !navPos.island)
			{
				dir = Vector3.zero;
				dist = 0f;
			}
			else
			{
				this.distanceField.Sample(navPos, ref dir, ref dist);
				if (dist < 1.5f && navPos.TriCast(this.entryNavPos))
				{
					Vector3 vector = this.entryNavPos.wPos - navPos.wPos;
					dist = vector.magnitude;
					dir = Vector3.ClampMagnitude(vector, 1f);
				}
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x000E32DF File Offset: 0x000E16DF
		NavPos IPathTarget.navPos
		{
			get
			{
				return this._entryNavPos;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060034C7 RID: 13511 RVA: 0x000E32E8 File Offset: 0x000E16E8
		Bounds IPathTarget.endPointBounds
		{
			get
			{
				return new Bounds(this.entryNavPos.wPos, new Vector3(0.1f, 0.5f, 0.1f));
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060034C8 RID: 13512 RVA: 0x000E331C File Offset: 0x000E171C
		Vector3 IPathTarget.endPointPosition
		{
			get
			{
				return this.entryNavPos.wPos;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x000E3337 File Offset: 0x000E1737
		Mesh IPathTarget.endPointMesh
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000E333A File Offset: 0x000E173A
		EnglishSquad ISquadSelector.GetSelectableSquad()
		{
			return this.squad;
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x000E3342 File Offset: 0x000E1742
		bool ISquadSelector.wantsHoverEffect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000E3345 File Offset: 0x000E1745
		public Vector3 GetWSCursorPos()
		{
			return this.ship.navigationMesh.transform.position;
		}

		// Token: 0x040023DD RID: 9181
		[SerializeField]
		private Longship ship;

		// Token: 0x040023DE RID: 9182
		[SerializeField]
		private Animator uiAnimator;

		// Token: 0x040023DF RID: 9183
		private float _area;

		// Token: 0x040023E0 RID: 9184
		private NavPos _entryNavPos;

		// Token: 0x040023E1 RID: 9185
		private NavPos _localNavPos;

		// Token: 0x040023E2 RID: 9186
		private DistanceField _distanceField;

		// Token: 0x040023E3 RID: 9187
		private EnglishSquad squad;

		// Token: 0x040023E4 RID: 9188
		private List<Agent> agentsOnBoard = new List<Agent>();

		// Token: 0x040023E5 RID: 9189
		private bool departed;

		// Token: 0x040023E9 RID: 9193
		private static AnimId hoverId = "Hover";

		// Token: 0x040023EA RID: 9194
		private static AnimId highlightId = "Highlight";

		// Token: 0x040023EB RID: 9195
		private const float triCastRange = 1.5f;
	}
}
