using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000666 RID: 1638
	public class NavSpotter : IslandComponent, IIslandFirstEnter, IIslandWipe, IIslandEnter, IIslandDestroyEntered
	{
		// Token: 0x060029D7 RID: 10711 RVA: 0x00094314 File Offset: 0x00092714
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			GenInfo genInfo = new GenInfo("NavSpotter", GenInfo.Mode.interruptable);
			base.enabled = true;
			this.spotContainer = new GameObject("SpotContainer").transform;
			this.spotContainer.SetParent(base.transform);
			Vector2 add = island.size.GetXZ() / 2f;
			this.highlightTex = new Fake3dTex(island.aoBaker.textureAO.size, Color.clear, false, island.texturePool);
			for (int i = 0; i < island.navMesh.verts.Length; i++)
			{
				Vert vert = island.navMesh.verts[i];
				Vector2 comparePos = new Vector2(vert.pos.x + add.x, vert.pos.z + add.y);
				Vector2 floored = ExtraMath.Round(comparePos);
				if ((comparePos - floored).sqrMagnitude <= 0.001f)
				{
					NavSpot navSpot = UnityEngine.Object.Instantiate<GameObject>(this.navSpotPrefab.gameObject).GetComponent<NavSpot>();
					navSpot.transform.SetParent(this.spotContainer);
					navSpot.Setup(vert, this.meshPrefab, this, this.navSpots.Count);
					this.navSpots.Add(navSpot);
					Vector3Int coord = ExtraMath.RoundToInt(navSpot.transform.position - island.voxelSpace.cornerVoxelOffset + new Vector3(0f, 0.4f, 0f));
					navSpot.pixelIndex = this.highlightTex.GetIndex(coord);
					yield return genInfo;
				}
			}
			for (int j = 0; j < this.navSpots.Count; j++)
			{
				NavSpot navSpot2 = this.navSpots[j];
				for (int k = j + 1; k < this.navSpots.Count; k++)
				{
					NavSpot navSpot3 = this.navSpots[k];
					NavSpot.MaybeSetupNeighboirs(navSpot2, navSpot3);
				}
				yield return genInfo;
			}
			this.spotContainer.gameObject.SetActive(false);
			yield return genInfo;
			yield break;
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x00094336 File Offset: 0x00092736
		public void WipeNavMesh(NavigationMesh navMesh)
		{
			UnityEngine.Object.Destroy(this.spotContainer.gameObject);
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x00094348 File Offset: 0x00092748
		public NavSpot GetNavSpot(Vector3 pos, bool includeOccupied)
		{
			NavSpot result = null;
			float num = float.PositiveInfinity;
			for (int i = 0; i < this.navSpots.Count; i++)
			{
				NavSpot navSpot = this.navSpots[i];
				if (includeOccupied || !navSpot.isOccupied)
				{
					float num2 = Vector3.SqrMagnitude(pos - navSpot.vert.pos);
					if (num2 < num)
					{
						num = num2;
						result = navSpot;
					}
				}
			}
			return result;
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060029DA RID: 10714 RVA: 0x000943C2 File Offset: 0x000927C2
		private static LayerMask voxelLayerMask
		{
			get
			{
				if (NavSpotter._voxelLayerMask.value == 0)
				{
					NavSpotter._voxelLayerMask = LayerMask.GetMask(new string[]
					{
						"Voxels"
					});
				}
				return NavSpotter._voxelLayerMask;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x000943F5 File Offset: 0x000927F5
		private static LayerMask moduleLayerMask
		{
			get
			{
				if (NavSpotter._moduleLayerMask.value == 0)
				{
					NavSpotter._moduleLayerMask = LayerMask.GetMask(new string[]
					{
						"Modules"
					});
				}
				return NavSpotter._moduleLayerMask;
			}
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x00094428 File Offset: 0x00092828
		public NavSpot NavSpotCast(Vector2 screenPos, out RaycastHit hit)
		{
			RaycastHit hitAtMouse = NavSpotter.GetHitAtMouse(screenPos, NavSpotter.voxelLayerMask);
			RaycastHit hitAtMouse2 = NavSpotter.GetHitAtMouse(screenPos, NavSpotter.moduleLayerMask);
			hit = hitAtMouse;
			NavSpot navSpot = (!hitAtMouse.collider) ? null : this.GetNavSpot(hitAtMouse.point, true);
			NavSpot navSpot2 = (!hitAtMouse2.collider) ? null : this.GetNavSpot(hitAtMouse2.point, true);
			NavSpot navSpot3;
			if (!navSpot && !navSpot2)
			{
				navSpot3 = null;
			}
			else if (!navSpot && navSpot2)
			{
				navSpot3 = navSpot2;
				hit = hitAtMouse2;
			}
			else if (navSpot && !navSpot2)
			{
				navSpot3 = navSpot;
				hit = hitAtMouse;
			}
			else
			{
				Vector2 a = Singleton<LevelCamera>.instance.cameraRef.WorldToScreenPoint(navSpot.navPos.pos);
				Vector2 a2 = Singleton<LevelCamera>.instance.cameraRef.WorldToScreenPoint(navSpot2.navPos.pos);
				bool flag = Vector2.SqrMagnitude(a - screenPos) < Vector2.SqrMagnitude(a2 - screenPos);
				navSpot3 = ((!flag) ? navSpot2 : navSpot);
				hit = ((!flag) ? hitAtMouse2 : hitAtMouse);
			}
			if (navSpot3 && Vector3.SqrMagnitude(navSpot3.navPos.pos - hit.point) > 1f)
			{
				navSpot3 = null;
			}
			return navSpot3;
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000945C8 File Offset: 0x000929C8
		private static RaycastHit GetHitAtMouse(Vector2 screenPos, LayerMask mask)
		{
			screenPos.x /= (float)Screen.width;
			screenPos.y /= (float)Screen.height;
			Ray ray = Singleton<LevelCamera>.instance.cameraRef.ViewportPointToRay(screenPos);
			RaycastHit result;
			Physics.Raycast(ray, out result, float.PositiveInfinity, mask);
			return result;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x00094628 File Offset: 0x00092A28
		public NavSpot NavSpotCast(Vector2 screenPos)
		{
			RaycastHit raycastHit;
			return this.NavSpotCast(screenPos, out raycastHit);
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x00094640 File Offset: 0x00092A40
		IEnumerator<GenInfo> IIslandWipe.OnIslandWipe(Island island)
		{
			this.highlightTex.SetAllPixels(Color.clear);
			yield return default(GenInfo);
			yield break;
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x0009465C File Offset: 0x00092A5C
		IEnumerator<GenInfo> IIslandEnter.OnIslandEnter(Island island)
		{
			this.highlightTex.SetShaderVariables("_HighlightTex", string.Empty, string.Empty);
			yield return new GenInfo("NavSpotter", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x00094678 File Offset: 0x00092A78
		private void LateUpdate()
		{
			using ("navspotHighlight")
			{
				foreach (NavSpot navSpot in this.navSpots)
				{
					this.highlightTex.pixels[navSpot.pixelIndex] = navSpot.GetHighlightColor();
				}
			}
			using ("navspotHighlightApply")
			{
				this.highlightTex.ApplyPixels();
			}
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x00094754 File Offset: 0x00092B54
		void IIslandDestroyEntered.OnIslandDestroyEntered(Island island)
		{
			foreach (NavSpot navSpot in this.navSpots)
			{
				navSpot.Destroy();
			}
			this.spotContainer = null;
			this.navSpots = null;
			this.highlightTex.Destroy();
		}

		// Token: 0x04001B36 RID: 6966
		public NavSpot navSpotPrefab;

		// Token: 0x04001B37 RID: 6967
		public Fake3dTex highlightTex;

		// Token: 0x04001B38 RID: 6968
		private Transform spotContainer;

		// Token: 0x04001B39 RID: 6969
		public Mesh meshPrefab;

		// Token: 0x04001B3A RID: 6970
		public List<NavSpot> navSpots;

		// Token: 0x04001B3B RID: 6971
		private static LayerMask _voxelLayerMask = 0;

		// Token: 0x04001B3C RID: 6972
		private static LayerMask _moduleLayerMask = 0;
	}
}
