using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200087D RID: 2173
	public class VoxelColliders : MonoBehaviour, IIslandProcessor
	{
		// Token: 0x060038F0 RID: 14576 RVA: 0x000F73E8 File Offset: 0x000F57E8
		public void ResetWave(MultiWave multiWave)
		{
			for (int i = 0; i < this.colliders.Count; i++)
			{
				UnityEngine.Object.Destroy(this.colliders[i]);
			}
			this.colliders.Clear();
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x000F7430 File Offset: 0x000F5830
		IEnumerator<GenInfo> IIslandProcessor.OnIslandProcess(Island island, SavedWave savedWave)
		{
			base.transform.position = island.voxelSpace.moduleOffset - Vector3.one / 2f;
			List<Bounds> bounds = new List<Bounds>();
			Vector3 size = island.voxelSpace.size + Vector3.one;
			bool[] avaliables = new bool[island.voxelSpace.cornerVoxels.Length];
			Bounds maxBounds = new Bounds(Vector3.zero, Vector3.zero);
			maxBounds.max = island.voxelSpace.size;
			for (int j = 0; j < island.voxelSpace.cornerVoxels.Length; j++)
			{
				if (island.voxelSpace.cornerVoxels[j].inside)
				{
					avaliables[j] = true;
				}
			}
			yield return new GenInfo("Consolidating colliders", GenInfo.Mode.interruptable);
			for (int i = 0; i < avaliables.Length; i++)
			{
				if (avaliables[i])
				{
					Bounds item = new Bounds(ExtraMath.IndexToCoordinate(i, size), Vector3.zero);
					for (int k = 0; k < 100000; k++)
					{
						bool flag = false;
						for (int l = 0; l < 3; l++)
						{
							Bounds bounds2 = new Bounds(item.center, item.size);
							bounds2.max += Constants.directions[l];
							bounds2.min = ExtraMath.Lerp(bounds2.min, bounds2.max, Constants.directions[l]);
							if (!maxBounds.Contains(bounds2))
							{
								break;
							}
							Vector3 vector = bounds2.size + Vector3.one;
							int num = (int)vector.GetVolume();
							bool flag2 = true;
							for (int m = 0; m < num; m++)
							{
								Vector3 coordinate = bounds2.min + ExtraMath.IndexToCoordinate(m, vector);
								if (!avaliables[ExtraMath.CoordinateToIndex(coordinate, size)])
								{
									flag2 = false;
									break;
								}
							}
							if (flag2)
							{
								for (int n = 0; n < num; n++)
								{
									Vector3 coordinate2 = bounds2.min + ExtraMath.IndexToCoordinate(n, vector);
									avaliables[ExtraMath.CoordinateToIndex(coordinate2, size)] = false;
								}
								item.Encapsulate(bounds2);
								flag = true;
							}
						}
						if (!flag)
						{
							break;
						}
					}
					bounds.Add(item);
					BoxCollider boxCollider = base.gameObject.AddComponent<BoxCollider>();
					boxCollider.center = item.center;
					boxCollider.size = item.size + Vector3.one;
					this.colliders.Add(boxCollider);
				}
				yield return new GenInfo("Consolidating colliders", GenInfo.Mode.interruptable);
			}
			yield return new GenInfo("Consolidating colliders", GenInfo.Mode.interruptable);
			this.colliderCount = this.colliders.Count;
			yield break;
		}

		// Token: 0x040026E4 RID: 9956
		public List<BoxCollider> colliders = new List<BoxCollider>();

		// Token: 0x040026E5 RID: 9957
		public int colliderCount;
	}
}
