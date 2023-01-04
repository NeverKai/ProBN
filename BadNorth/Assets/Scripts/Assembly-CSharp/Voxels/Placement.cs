using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels
{
	// Token: 0x02000668 RID: 1640
	[Serializable]
	public class Placement
	{
		// Token: 0x060029E8 RID: 10728 RVA: 0x0009514C File Offset: 0x0009354C
		public Placement(OrientedModule orientedModule)
		{
			this.name = orientedModule.name;
			this.modules.Add(orientedModule);
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060029E9 RID: 10729 RVA: 0x00095178 File Offset: 0x00093578
		public List<Claim> claims
		{
			get
			{
				return this.firstOrientedModule.claims;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060029EA RID: 10730 RVA: 0x00095185 File Offset: 0x00093585
		public List<ModuleSet> sets
		{
			get
			{
				return this.firstModule.sets;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060029EB RID: 10731 RVA: 0x00095192 File Offset: 0x00093592
		public OrientedModule firstOrientedModule
		{
			get
			{
				return this.modules[0];
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060029EC RID: 10732 RVA: 0x000951A0 File Offset: 0x000935A0
		public Module firstModule
		{
			get
			{
				return this.firstOrientedModule.module;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060029ED RID: 10733 RVA: 0x000951AD File Offset: 0x000935AD
		public List<Corner> corners
		{
			get
			{
				return this.firstOrientedModule.corners;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x000951BA File Offset: 0x000935BA
		public Vector3 normal
		{
			get
			{
				return this.firstOrientedModule.normal;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060029EF RID: 10735 RVA: 0x000951C7 File Offset: 0x000935C7
		public Vector3 navigationNormal
		{
			get
			{
				return this.firstOrientedModule.navigationNormal;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060029F0 RID: 10736 RVA: 0x000951D4 File Offset: 0x000935D4
		public float coverage
		{
			get
			{
				return this.firstOrientedModule.coverage;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060029F1 RID: 10737 RVA: 0x000951E1 File Offset: 0x000935E1
		public bool house
		{
			get
			{
				return this.firstModule.house;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060029F2 RID: 10738 RVA: 0x000951EE File Offset: 0x000935EE
		public bool forcedNavigability
		{
			get
			{
				return this.firstModule.forcedNavigability;
			}
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000951FC File Offset: 0x000935FC
		public void SetupClaimDict()
		{
			if (this.claims.Count == 1)
			{
				return;
			}
			this.claimDict = new Dictionary<Vector3Int, Claim>(this.claims.Count);
			for (int i = 0; i < this.claims.Count; i++)
			{
				this.claimDict.Add(this.claims[i].pos, this.claims[i]);
			}
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x00095275 File Offset: 0x00093675
		public Claim GetClaimAt(Vector3Int pos)
		{
			if (this.claims.Count == 1)
			{
				return this.claims[0];
			}
			return this.claimDict[pos];
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000952A4 File Offset: 0x000936A4
		public OrientedModule GetOrientedModule(Module module)
		{
			foreach (OrientedModule orientedModule in this.modules)
			{
				if (orientedModule.module == module)
				{
					return orientedModule;
				}
			}
			return OrientedModule.empty;
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x00095318 File Offset: 0x00093718
		public TransformSettings GetTransformSettings(Module module)
		{
			foreach (OrientedModule orientedModule in this.modules)
			{
				if (orientedModule.module == module)
				{
					return orientedModule.settings;
				}
			}
			return TransformSettings.identity;
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x00095394 File Offset: 0x00093794
		public void Append(OrientedModule orientedModule)
		{
			this.modules.Add(orientedModule);
		}

		// Token: 0x04001B47 RID: 6983
		public string name;

		// Token: 0x04001B48 RID: 6984
		public Bounds bounds;

		// Token: 0x04001B49 RID: 6985
		public Bounds navigableBounds;

		// Token: 0x04001B4A RID: 6986
		public Bounds openBounds;

		// Token: 0x04001B4B RID: 6987
		public bool navigable;

		// Token: 0x04001B4C RID: 6988
		public bool open;

		// Token: 0x04001B4D RID: 6989
		public List<OrientedModule> modules = new List<OrientedModule>(1);

		// Token: 0x04001B4E RID: 6990
		private Dictionary<Vector3Int, Claim> claimDict;
	}
}
