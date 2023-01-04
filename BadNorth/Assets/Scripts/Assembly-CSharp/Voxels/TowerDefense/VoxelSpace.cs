using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000865 RID: 2149
	public class VoxelSpace : IslandComponent, IIslandProcessor
	{
		// Token: 0x06003853 RID: 14419 RVA: 0x000F3248 File Offset: 0x000F1648
		IEnumerator<GenInfo> IIslandProcessor.OnIslandProcess(Island island, SavedWave savedWave)
		{
			float height = 0f;
			foreach (SavedWave.SavedModule savedModule in savedWave.dominos)
			{
				if (!savedModule.orientedModule.module.isNull)
				{
					height = Mathf.Max(savedModule.placement.bounds.max.y + savedModule.offset.y, height);
				}
			}
			this.size = island.size.SetY((float)Mathf.RoundToInt(height)) + this.additionalSize;
			this.voxelSize = this.size + Vector3.one;
			this.voxels = new VoxelSpace.Voxel[(int)this.size.x * (int)this.size.y * (int)this.size.z];
			this.cornerVoxels = new VoxelSpace.CornerVoxel[((int)this.size.x + 1) * ((int)this.size.y + 1) * ((int)this.size.z + 1)];
			for (int i = 0; i < this.voxels.Length; i++)
			{
				this.voxels[i] = new VoxelSpace.Voxel(ExtraMath.IndexToCoordinate(i, this.size));
			}
			for (int j = 0; j < this.cornerVoxels.Length; j++)
			{
				this.cornerVoxels[j] = new VoxelSpace.CornerVoxel(ExtraMath.IndexToCoordinate(j, this.voxelSize), j);
			}
			this.cornerVoxelOffset = ((this.voxelSize - Vector3.one) / -2f).SetY(-0.5f);
			this.moduleOffset = new Vector3(-(this.size.x - 1f) / 2f, 0f, -(this.size.z - 1f) / 2f);
			this.module2World = this.moduleOffset.GetMoveMatrix();
			this.world2Module = this.module2World.inverse;
			this.voxel2world = this.module2World * Matrix4x4.TRS(-Vector3.one, Quaternion.identity, Vector3.one);
			this.world2Voxel = this.voxel2world.inverse;
			this.bounds = new Bounds((this.size - Vector3.one) / 2f, this.size);
			this.voxelBounds = new Bounds(this.size / 2f, this.size + Vector3.one);
			List<SavedWave.SavedModule> dominos = savedWave.dominos;
			foreach (SavedWave.SavedModule domino in dominos)
			{
				foreach (Claim claim in domino.placement.claims)
				{
					Vector3 vector = domino.offset + claim.pos;
					vector = savedWave.module2World.MultiplyPoint(vector);
					vector = this.world2Module.MultiplyPoint(vector);
					for (int k = 0; k < Constants.corners.Length; k++)
					{
						Vector3 coordinate = vector + Constants.corners[k] + Vector3.one / 2f;
						VoxelSpace.CornerVoxel cornerVoxel = this.GetCornerVoxel(coordinate);
						cornerVoxel.coverage = (float)((!claim.cornersInside[k]) ? 0 : 1);
					}
				}
				yield return new GenInfo("Building Voxel Space", GenInfo.Mode.interruptable);
			}
			foreach (VoxelSpace.CornerVoxel cornerVoxel2 in this.cornerVoxels)
			{
				if (cornerVoxel2.pos.y == 0)
				{
					cornerVoxel2.coverage = Mathf.Max(cornerVoxel2.coverage, 0.5f);
				}
			}
			this.UpdateNormals();
			yield return new GenInfo("Building Voxel Space", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000F3274 File Offset: 0x000F1674
		public void UpdateNormals()
		{
			using (new ScopedProfiler("Update voxel normals", null))
			{
				for (int i = 0; i < this.voxels.Length; i++)
				{
					VoxelSpace.Voxel voxel = this.voxels[i];
					Vector3 a = voxel.pos;
					Vector3 a2 = Vector3.zero;
					float num = 0f;
					for (int j = 0; j < Constants.corners.Length; j++)
					{
						Vector3 vector = Constants.corners[j];
						VoxelSpace.CornerVoxel cornerVoxel = this.GetCornerVoxel(a + vector + Vector3.one / 2f);
						num += cornerVoxel.coverage / 8f;
						a2 += vector * (0.5f - cornerVoxel.coverage);
					}
					voxel.openness = 1f - num;
					voxel.normal = a2.normalized;
				}
			}
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000F338C File Offset: 0x000F178C
		public int GetCornerIndex(Vector3 coordinate)
		{
			return ExtraMath.CoordinateToIndex(coordinate + Vector3.one * 0.1f, this.voxelSize);
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000F33AE File Offset: 0x000F17AE
		public VoxelSpace.CornerVoxel GetCornerVoxel(Vector3 coordinate)
		{
			return this.cornerVoxels[this.GetCornerIndex(coordinate)];
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000F33BE File Offset: 0x000F17BE
		public int GetModuleIndex(Vector3 coordinate)
		{
			return ExtraMath.CoordinateToIndex(coordinate + Vector3.one * 0.1f, this.size);
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x000F33E0 File Offset: 0x000F17E0
		public VoxelSpace.Voxel GetVoxel(Vector3 pos)
		{
			return this.voxels[this.GetModuleIndex(pos)];
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x000F33F0 File Offset: 0x000F17F0
		public Vector4 GetNormal(Vector3 pos)
		{
			VoxelSpace.Voxel voxel = this.GetVoxel(pos);
			return voxel.normal.SetW(voxel.openness);
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000F3418 File Offset: 0x000F1818
		public Vector3 GetClosestFloor(Vector3 worldPos)
		{
			Vector3 vector = worldPos - this.cornerVoxelOffset;
			Vector3 vector2 = ExtraMath.Round(vector);
			vector2 = Vector3.Max(vector2, Vector3.zero);
			vector2 = Vector3.Min(vector2, this.voxelSize);
			float num = float.MaxValue;
			float value = 0f;
			int num2 = 0;
			while ((float)num2 < this.voxelBounds.size.y - 1f)
			{
				Vector3 coordinate = vector2.SetY((float)num2);
				Vector3 coordinate2 = vector2.SetY((float)(num2 + 1));
				if (this.GetCornerVoxel(coordinate).inside && !this.GetCornerVoxel(coordinate2).inside)
				{
					float num3 = coordinate.y + 0.5f + this.cornerVoxelOffset.y;
					float num4 = Mathf.Abs(num3 - worldPos.y);
					if (num4 < num)
					{
						value = num3;
						num = num4;
					}
				}
				num2++;
			}
			return worldPos.SetY(value);
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x000F350C File Offset: 0x000F190C
		public bool GetInside(Vector3 worldPos)
		{
			Vector3 vector = worldPos - this.cornerVoxelOffset;
			Vector3 coordinate = ExtraMath.Round(vector);
			return this.GetCornerVoxel(coordinate).inside;
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x000F353C File Offset: 0x000F193C
		public float GetCoverageLinear(Vector3 worldPos)
		{
			Vector3 vector = worldPos - this.cornerVoxelOffset;
			Vector3 vector2 = ExtraMath.Floor(vector);
			Vector3 vector3 = vector - vector2;
			Vector3 v = Vector3.one - vector3;
			float num = 0f;
			for (int i = 0; i < 8; i++)
			{
				Vector3 vector4 = Constants.corners[i] + Vector3.one * 0.5f;
				Vector3 vector5 = vector2 + vector4;
				if (this.voxelBounds.Contains(vector5))
				{
					Vector3 vector6 = ExtraMath.Lerp(v, vector3, vector4);
					num += this.GetCornerVoxel(vector5).coverage * vector6.x * vector6.y * vector6.z;
				}
			}
			return num;
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000F3610 File Offset: 0x000F1A10
		public Vector4 GetNormalLinear(Vector3 worldPos)
		{
			Vector4 result;
			using (new ScopedProfiler("GetNormalLinear", null))
			{
				Vector3 vector = worldPos - this.moduleOffset;
				Vector3 vector2 = ExtraMath.Floor(vector);
				Vector3 vector3 = vector - vector2;
				Vector3 v = Vector3.one - vector3;
				Vector4 vector4 = Vector3.zero;
				for (int i = 0; i < 8; i++)
				{
					Vector3 vector5 = Constants.corners[i] + Vector3.one * 0.5f;
					Vector3 vector6 = vector2 + vector5;
					if (this.bounds.Contains(vector6))
					{
						Vector3 vector7 = ExtraMath.Lerp(v, vector3, vector5);
						vector4 += this.GetNormal(vector6) * vector7.x * vector7.y * vector7.z;
					}
				}
				result = vector4;
			}
			return result;
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x000F3728 File Offset: 0x000F1B28
		public void SampleNormalOpenness(Vector3 worldPos, out Vector3 normal, out float openness)
		{
			Vector4 normalLinear = this.GetNormalLinear(worldPos);
			normal = normalLinear;
			openness = normalLinear.w;
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x000F3754 File Offset: 0x000F1B54
		public Vector3 GetPosOffset(Vector3 worldPos)
		{
			Vector4 normalLinear = base.island.voxelSpace.GetNormalLinear(worldPos);
			Vector3 result = normalLinear * (0.6f - normalLinear.w) * 0.5f;
			result.y *= ExtraMath.RemapValue(worldPos.y, -0.105f, -0.004999995f);
			return result;
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x000F37BC File Offset: 0x000F1BBC
		public void AddCoverage(Bounds coverBounds, float alpha = 1f)
		{
			coverBounds.center = this.world2Voxel.MultiplyPoint(coverBounds.center);
			for (int i = Mathf.FloorToInt(coverBounds.min.x); i < Mathf.CeilToInt(coverBounds.max.x); i++)
			{
				for (int j = Mathf.FloorToInt(coverBounds.min.y); j < Mathf.CeilToInt(coverBounds.max.y); j++)
				{
					for (int k = Mathf.FloorToInt(coverBounds.min.z); k < Mathf.CeilToInt(coverBounds.max.z); k++)
					{
						Vector3 vector = new Vector3((float)i, (float)j, (float)k);
						if (this.voxelBounds.Contains(vector))
						{
							Vector3 b = Vector3.Max(vector, coverBounds.min);
							Vector3 a = Vector3.Min(vector + Vector3.one, coverBounds.max);
							Vector3 vector2 = a - b;
							float volume = vector2.GetVolume();
							VoxelSpace.CornerVoxel cornerVoxel = this.GetCornerVoxel(vector);
							cornerVoxel.coveredVolume += volume * alpha;
						}
					}
				}
			}
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000F3914 File Offset: 0x000F1D14
		public void OnDrawGizmos()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (this.cornerVoxels == null || this.voxels == null)
			{
				return;
			}
			Gizmos.matrix = this.voxel2world;
			foreach (VoxelSpace.CornerVoxel cornerVoxel in this.cornerVoxels)
			{
				Gizmos.color = new Color(1f, 1f, 1f, cornerVoxel.coverage);
				Gizmos.DrawWireCube(cornerVoxel.pos + Vector3.one / 2f, cornerVoxel.coverage * Vector3.one * 0.8f);
			}
			foreach (VoxelSpace.Voxel voxel in this.voxels)
			{
				if (!(voxel.normal == Vector3.zero))
				{
					Gizmos.color = Color.white * 2f;
					Gizmos.DrawSphere(voxel.pos + Vector3.one, 0.02f);
					Gizmos.color = Color.white;
					Gizmos.DrawRay(voxel.pos + Vector3.one, voxel.normal * 0.2f);
				}
			}
			Gizmos.color = Color.red;
		}

		// Token: 0x04002659 RID: 9817
		public Vector3 additionalSize = new Vector3(4f, 2f, 4f);

		// Token: 0x0400265A RID: 9818
		public Vector3 size;

		// Token: 0x0400265B RID: 9819
		public Vector3 voxelSize;

		// Token: 0x0400265C RID: 9820
		public Vector3 moduleOffset;

		// Token: 0x0400265D RID: 9821
		public Vector3 cornerVoxelOffset;

		// Token: 0x0400265E RID: 9822
		public Matrix4x4 module2World;

		// Token: 0x0400265F RID: 9823
		public Matrix4x4 world2Module;

		// Token: 0x04002660 RID: 9824
		public Bounds bounds;

		// Token: 0x04002661 RID: 9825
		public Bounds voxelBounds;

		// Token: 0x04002662 RID: 9826
		public Matrix4x4 world2Voxel;

		// Token: 0x04002663 RID: 9827
		public Matrix4x4 voxel2world;

		// Token: 0x04002664 RID: 9828
		public VoxelSpace.CornerVoxel[] cornerVoxels;

		// Token: 0x04002665 RID: 9829
		public VoxelSpace.Voxel[] voxels;

		// Token: 0x02000866 RID: 2150
		public class CornerVoxel
		{
			// Token: 0x06003862 RID: 14434 RVA: 0x000F3A7C File Offset: 0x000F1E7C
			public CornerVoxel(Vector3Int pos, int index)
			{
				this.index = index;
				this.pos = pos;
			}

			// Token: 0x1700080F RID: 2063
			// (get) Token: 0x06003863 RID: 14435 RVA: 0x000F3A92 File Offset: 0x000F1E92
			public bool inside
			{
				get
				{
					return this.coverage == 1f;
				}
			}

			// Token: 0x17000810 RID: 2064
			// (get) Token: 0x06003864 RID: 14436 RVA: 0x000F3AA1 File Offset: 0x000F1EA1
			// (set) Token: 0x06003865 RID: 14437 RVA: 0x000F3AB0 File Offset: 0x000F1EB0
			public float coveredArea
			{
				get
				{
					return this.coverage * this.coverage;
				}
				set
				{
					this.coverage = Mathf.Clamp01(Mathf.Sqrt(value));
				}
			}

			// Token: 0x17000811 RID: 2065
			// (get) Token: 0x06003866 RID: 14438 RVA: 0x000F3AC3 File Offset: 0x000F1EC3
			// (set) Token: 0x06003867 RID: 14439 RVA: 0x000F3AD9 File Offset: 0x000F1ED9
			public float coveredVolume
			{
				get
				{
					return this.coverage * this.coverage * this.coverage;
				}
				set
				{
					this.coverage = Mathf.Clamp01(Mathf.Pow(value, 0.33333334f));
				}
			}

			// Token: 0x04002666 RID: 9830
			public Vector3Int pos;

			// Token: 0x04002667 RID: 9831
			public float coverage;

			// Token: 0x04002668 RID: 9832
			public int index;
		}

		// Token: 0x02000867 RID: 2151
		public class Voxel
		{
			// Token: 0x06003868 RID: 14440 RVA: 0x000F3AF1 File Offset: 0x000F1EF1
			public Voxel(Vector3Int pos)
			{
				this.pos = pos;
			}

			// Token: 0x04002669 RID: 9833
			public Vector3Int pos;

			// Token: 0x0400266A RID: 9834
			public Vector3 normal;

			// Token: 0x0400266B RID: 9835
			public float openness;
		}
	}
}
