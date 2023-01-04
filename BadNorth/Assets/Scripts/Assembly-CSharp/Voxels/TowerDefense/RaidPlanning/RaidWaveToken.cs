using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Voxels.TowerDefense.RaidPlanning
{
	// Token: 0x020005B7 RID: 1463
	[SelectionBase]
	public class RaidWaveToken : RaidToken
	{
		// Token: 0x06002630 RID: 9776 RVA: 0x000789F6 File Offset: 0x00076DF6
		public RaidShipToken[] GetShipTokens()
		{
			return base.GetComponentsInChildren<RaidShipToken>();
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x000789FE File Offset: 0x00076DFE
		public Vector3 rightTip
		{
			get
			{
				return Vector3.right * 8f / 2f;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x00078A19 File Offset: 0x00076E19
		public Vector3 leftTip
		{
			get
			{
				return Vector3.left * 8f / 2f;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x00078A34 File Offset: 0x00076E34
		public Vector3 center
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x00078A3B File Offset: 0x00076E3B
		public Bounds lineBounds
		{
			get
			{
				return new Bounds(this.center, new Vector3(8f, 0f, 0f));
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x00078A5C File Offset: 0x00076E5C
		public override int depth
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x00078A5F File Offset: 0x00076E5F
		public override int bounty
		{
			get
			{
				return this.ships.Sum((RaidShipToken x) => x.bounty);
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x00078A89 File Offset: 0x00076E89
		public override int agentCount
		{
			get
			{
				return this.ships.Sum((RaidShipToken x) => x.agentCount);
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x00078AB3 File Offset: 0x00076EB3
		public RaidShipToken[] ships
		{
			get
			{
				if (Application.isPlaying)
				{
					if (this._ships == null)
					{
						this._ships = base.GetComponentsInChildren<RaidShipToken>();
					}
					return this._ships;
				}
				return base.GetComponentsInChildren<RaidShipToken>();
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x00078AE3 File Offset: 0x00076EE3
		public override RaidToken parent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x00078AE6 File Offset: 0x00076EE6
		public override Bounds localDropBounds
		{
			get
			{
				return new Bounds(new Vector3(0f, 0f, 3f), new Vector3(8f, 0f, 6f));
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x00078B15 File Offset: 0x00076F15
		public override Vector3 localDragPos
		{
			get
			{
				return Vector3.back * 0.6f;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x00078B26 File Offset: 0x00076F26
		public override float dragRadius
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600263D RID: 9789 RVA: 0x00078B2D File Offset: 0x00076F2D
		public float timePadding
		{
			get
			{
				return this.duration + 20f + (float)this.bounty * 0.1f;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x00078B4C File Offset: 0x00076F4C
		public float duration
		{
			get
			{
				float num = (float)this.ships.Max((RaidShipToken x) => x.localTime);
				float num2 = (float)this.ships.Min((RaidShipToken x) => x.localTime);
				return num - num2;
			}
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x00078BB0 File Offset: 0x00076FB0
		public WaveDef GetWaveDef()
		{
			WaveDef waveDef = new WaveDef();
			waveDef.shipGroups = new List<ShipGroupDef>();
			List<List<RaidShipToken>> groupedShips = this.GetGroupedShips();
			foreach (List<RaidShipToken> source in groupedShips)
			{
				float averageX = source.Average((RaidShipToken x) => x.transform.localPosition.x);
				IOrderedEnumerable<RaidShipToken> orderedEnumerable = from x in source
				orderby Mathf.Abs(x.transform.localPosition.x - averageX)
				select x;
				ShipGroupDef item = default(ShipGroupDef);
				item.ships = new List<ShipDef>();
				waveDef.shipGroups.Add(item);
				foreach (RaidShipToken raidShipToken in orderedEnumerable)
				{
					item.ships.Add(raidShipToken.GetShipDef());
				}
			}
			return waveDef;
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x00078CD8 File Offset: 0x000770D8
		public List<List<RaidShipToken>> GetGroupedShips()
		{
			RaidShipToken[] array = (from x in base.GetComponentsInChildren<RaidShipToken>()
			orderby x.transform.localPosition.x
			select x).ToArray<RaidShipToken>();
			List<List<RaidShipToken>> list = new List<List<RaidShipToken>>();
			if (array.Length > 0)
			{
				List<RaidShipToken> list2 = new List<RaidShipToken>();
				list.Add(list2);
				float num = array[0].transform.localPosition.x;
				foreach (RaidShipToken raidShipToken in array)
				{
					float num2 = raidShipToken.transform.localPosition.x - raidShipToken.radius;
					float num3 = num2 - num;
					if (num3 > 0.5f)
					{
						list2 = new List<RaidShipToken>();
						list.Add(list2);
					}
					list2.Add(raidShipToken);
					num = raidShipToken.transform.localPosition.x + raidShipToken.radius;
				}
			}
			return list;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x00078DD0 File Offset: 0x000771D0
		public override void OnDrawGizmos()
		{
			Gizmos.color = Color.HSVToRGB(0.2f, 0.5f, 1f);
			bool flag = Vector3.SqrMagnitude(Camera.current.transform.position - base.transform.position) > 900f;
			this.DrawGizmosInternal();
			if (!flag)
			{
				this.DrawGizmosShipGroups();
			}
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x00078E34 File Offset: 0x00077234
		public override void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			base.transform.position = ExtraMath.Round(base.transform.position / 10f) * 10f;
			this.DrawGizmosInternal();
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x00078E80 File Offset: 0x00077280
		public void DrawGizmosInternal()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Bounds localDropBounds = this.localDropBounds;
			Gizmos.DrawLine(this.rightTip, this.leftTip);
			Gizmos.color = Gizmos.color.SetComponent(3, 0.2f);
			Gizmos.DrawWireCube(localDropBounds.center, localDropBounds.size);
			Gizmos.color = Gizmos.color.SetComponent(3, 1f);
			base.DrawDragGizmo();
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00078EF8 File Offset: 0x000772F8
		private void DrawGizmosShipGroups()
		{
			List<List<RaidShipToken>> groupedShips = this.GetGroupedShips();
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.color = Color.white;
			for (int i = 0; i < groupedShips.Count; i++)
			{
				List<RaidShipToken> list = groupedShips[i];
				Vector3 from = new Vector3(0f, 0f, 0.1f);
				Vector3 to = new Vector3(0f, 0f, 0.1f);
				from.x = list[0].transform.localPosition.x - list[0].radius;
				to.x = list[list.Count - 1].transform.localPosition.x + list[list.Count - 1].radius;
				Gizmos.DrawLine(from, to);
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x00078FE4 File Offset: 0x000773E4
		public override void OnArranged(RaidToken parent, int i)
		{
			base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
			this.SetName();
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00079024 File Offset: 0x00077424
		private void SetName()
		{
			base.name = "Wave " + this.bounty;
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00079044 File Offset: 0x00077444
		public void OnValidate()
		{
			base.transform.localPosition = base.transform.localPosition.SetY(0f);
			base.transform.position = ExtraMath.Round(base.transform.position / 10f) * 10f;
			this.SetName();
		}

		// Token: 0x04001838 RID: 6200
		public const float width = 8f;

		// Token: 0x04001839 RID: 6201
		private RaidShipToken[] _ships;
	}
}
