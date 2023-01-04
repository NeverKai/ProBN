using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense.Forestry
{
	// Token: 0x0200075F RID: 1887
	public class ForestPlanter : IslandComponent, IIslandFirstEnter
	{
		// Token: 0x06003135 RID: 12597 RVA: 0x000CB8D4 File Offset: 0x000C9CD4
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			Transform temp = base.gameObject.AddEmptyChild("temp").transform;
			List<ForestDef> grasses = ListPool<ForestDef>.GetList(2);
			float year = island.levelNode.campaign.campaignSave.year2;
			if (island.navMesh.verts.Length > 0)
			{
				island.levelNode.levelState.GetReferencedObjects(LevelObjectReference.Key.Grass, grasses);
				for (int i = grasses.Count - 1; i >= 0; i--)
				{
					if (!grasses[i].GetAllowed(year))
					{
						grasses.RemoveAt(i);
					}
				}
				IEnumerator grassEnumerator = ForestDef.PlantForest(island.navManager.navigationMesh.verts, temp, island, grasses);
				while (grassEnumerator.MoveNext())
				{
					yield return new GenInfo("Planting grass", GenInfo.Mode.interruptable);
				}
			}
			if (island.navManager.undreachedVerts.Count > 0)
			{
				island.levelNode.levelState.GetReferencedObjects(LevelObjectReference.Key.Forest, grasses);
				for (int j = grasses.Count - 1; j >= 0; j--)
				{
					if (!grasses[j].GetAllowed(year))
					{
						grasses.RemoveAt(j);
					}
				}
				IEnumerator treeEnumerator = ForestDef.PlantForest(island.navManager.undreachedVerts, temp, island, grasses);
				while (treeEnumerator.MoveNext())
				{
					yield return new GenInfo("Planting trees", GenInfo.Mode.interruptable);
				}
			}
			grasses.ReturnToListPool<ForestDef>();
			SpriteMerger spriteMerger = base.gameObject.GetComponentInChildren<SpriteMerger>(true);
			spriteMerger.HarvestChildren(temp, SpriteMerger.Mode.none);
			UnityEngine.Object.Destroy(temp.gameObject);
			yield return new GenInfo("ForestPlanter", GenInfo.Mode.interruptable);
			yield break;
		}
	}
}
