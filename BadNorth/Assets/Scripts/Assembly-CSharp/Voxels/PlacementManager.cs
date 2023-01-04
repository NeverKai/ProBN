using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels
{
	// Token: 0x02000667 RID: 1639
	public class PlacementManager : Singleton<PlacementManager>, IScenePostprocessor
	{
		// Token: 0x060029E5 RID: 10725 RVA: 0x00094D40 File Offset: 0x00093140
		private void Start()
		{
			foreach (Placement placement in this.all)
			{
				placement.SetupClaimDict();
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x00094DA8 File Offset: 0x000931A8
		public void AddAllModules()
		{
			this.all = new List<Placement>(this.allModules.Length * 7);
			for (int i = 0; i < this.allModules.Length; i++)
			{
				Module module = this.allModules[i];
				for (int j = 0; j < module.orientations.Count; j++)
				{
					OrientedModule orientedModule = module.orientations[j];
					int item = module.GetSetKey() + orientedModule.key;
					int num = this.allPlacementKeys.IndexOf(item);
					if (num == -1)
					{
						Placement item2 = new Placement(orientedModule);
						this.all.Add(item2);
						this.allPlacementKeys.Add(item);
					}
					else
					{
						this.all[num].Append(orientedModule);
					}
				}
			}
			foreach (Placement placement in this.all)
			{
				placement.bounds = new Bounds(placement.claims[0].pos, Vector3.zero);
				foreach (Claim claim in placement.claims)
				{
					placement.bounds.Encapsulate(claim.pos);
					if (claim.anyNavigable)
					{
						if (placement.navigable)
						{
							placement.navigableBounds.Encapsulate(claim.pos);
						}
						else
						{
							placement.navigableBounds = new Bounds(claim.pos, Vector3.zero);
							placement.navigable = true;
						}
						for (int k = 0; k < 6; k++)
						{
							if (claim.navigable[k])
							{
								Vector3Int v = claim.pos + Constants.directions[k];
								if (placement.open)
								{
									placement.openBounds.Encapsulate(claim.pos);
								}
								else
								{
									placement.openBounds = new Bounds(v, Vector3.zero);
									placement.open = true;
								}
							}
						}
					}
				}
			}
			foreach (Module module2 in this.allModules)
			{
				module2.orientations.Clear();
				module2.cells.Clear();
			}
			this.allModules = null;
			this.keyListSizes = null;
			this.allPlacementKeys.Clear();
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x000950A0 File Offset: 0x000934A0
		void IScenePostprocessor.OnPostprocessScene()
		{
			if (this.allModules != null)
			{
				ModuleProcessor[] array = UnityEngine.Object.FindObjectsOfType<ModuleProcessor>();
				this.moduleSets = this.moduleSetContainer.GetComponentsInChildren<ModuleSet>();
				foreach (ModuleProcessor moduleProcessor in array)
				{
					if (moduleProcessor.gameObject.activeInHierarchy)
					{
						moduleProcessor.PreProcessModules(this.allModules);
					}
				}
				foreach (ModuleProcessor moduleProcessor2 in array)
				{
					if (moduleProcessor2.gameObject.activeInHierarchy)
					{
						moduleProcessor2.PostProcessModules(this.allModules);
					}
				}
				this.AddAllModules();
			}
		}

		// Token: 0x04001B3D RID: 6973
		[Header("Editor")]
		public Transform moduleFbx;

		// Token: 0x04001B3E RID: 6974
		public Transform moduleContainer;

		// Token: 0x04001B3F RID: 6975
		public Transform moduleSetContainer;

		// Token: 0x04001B40 RID: 6976
		[Header("Import")]
		public int maxKeyCount;

		// Token: 0x04001B41 RID: 6977
		public int[] keyListSizes = new int[3];

		// Token: 0x04001B42 RID: 6978
		public List<int> allPlacementKeys = new List<int>();

		// Token: 0x04001B43 RID: 6979
		public Module[] allModules;

		// Token: 0x04001B44 RID: 6980
		[Header("Runtime")]
		public ModuleSet[] moduleSets;

		// Token: 0x04001B45 RID: 6981
		public EdgeModules lowEdge;

		// Token: 0x04001B46 RID: 6982
		public List<Placement> all;
	}
}
